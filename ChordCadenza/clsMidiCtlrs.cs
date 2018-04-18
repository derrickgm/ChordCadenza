#undef DebugSend

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace ChordCadenza {
  internal class clsMidiCtlrs {
    //internal class clsMap<T> where T: struct {
    //  private SortedList<int, T> Map = new SortedList<int, T>();  //<ticks, value> 
    //  private int Index = 0;
    //  private T Dflt;

    //  internal clsMap(T dflt) {
    //    Dflt = dflt;
    //  }

    //  internal void Add(int ticks, T val) {
    //    //if (val == null) throw new LogicException();
    //    if (Map.ContainsKey(ticks)) return;  //use first key only
    //    Map.Add(ticks, val);
    //  }

    //  internal int Count { get { return Map.Count; } }

    //  internal T ValByIndex(int index) {  //midi data
    //    return Map.Values[index];
    //  }

    //  internal int KeyByIndex(int index) {  //ticks
    //    return Map.Keys[index];
    //  }

    //  internal T? this[int ticks] {
    //    get {
    //      //* Index used to remember last call to this and avoid searching through Map 
    //      if (Map.Count == 0 || ticks < Map.ElementAt(0).Key) return null;
    //      int keylo = Map.Keys[Index];
    //      int keyhi = int.MaxValue;
    //      if (Index < Map.Count - 1) keyhi = Map.Keys[Index + 1];
    //      if (ticks >= keylo && ticks < keyhi) return Map[keylo];
    //      Index = GetIndex(ticks);
    //      return Map.Values[Index];
    //    }
    //  }

    //  private int GetIndex(int key) {
    //    int i = 1;
    //    for (; i < Map.Count; i++) if (Map.ElementAt(i).Key > key) break;
    //    return i - 1;
    //  }
    //}

    internal clsMidiCtlrs(clsFileStream.clsEvStrm[] strm) {
      for (int i = 0; i < 129; i++) Dflt[i] = 0;
      Dflt[7] = 127;   //vol: max
      Dflt[10] = 64;  //pan: mid
      //Dflt[PatchCtlrNum] = 0;  //patch: piano
      Dflt[PatchCtlrNum] = -1;  //no patch

      Override[7] = P.F.Vol;
      Override[10] = P.F.Pan;
      Override[PatchCtlrNum] = P.F.Patch;

      CreateDataAll(strm);
      CreateDataLast(strm);
    }

    internal struct sMsgDataTicks {
      internal sMsgDataTicks(byte msg, byte data, int ticks) {
        Msg = msg;  //midi message (B1)
        Data = data;  //midi data (B2)
        Ticks = ticks;
      }
      internal byte Msg;
      internal byte Data;
      internal int Ticks;
    }

    //* MIDI CONTROLLERS
    //* 0-95: 
    //*    normal - only need to send last one during locate
    //*    except 6 & 38 (data entry)
    //*    saved in DataLast 
    //* 6, 38, 96-101:
    //*    send all in same order across all of these controllers
    //*    saved in DataAll
    //* 102-119:
    //*     undefined 
    //* 120-127:
    //*     realtime controllers
    //*     ignore (should not be present in midi file)
    //* 102: psuedo-controller for patch

    internal enum eCtlr : int { Vol = 7, Pan = 10, Patch = PatchCtlrNum }; 
    //internal const int NumCtlrs = 103;  //0-101 & Patch(102)
    internal const int PatchCtlrNum = 128;
    internal clsMap<int>[,] DataLast = new clsMap<int>[129, 16];  //[midictlr, chan]
    internal List<sMsgDataTicks>[] DataAll = new List<sMsgDataTicks>[16];   //[chan]     
    internal int[] Dflt = new int[129];  //only required for Pan, Vol, Patch
    internal int[][] Override = new int[129][];  //pointers to P.F.Vol/Pan/Patch [ctlr][chan]

#if DebugSend
    private string DebugFmt;
    private Stream XStreamDebug;
    private StreamWriter XSWDebug;
    private static int FileNum = 0;
#endif

    private void CreateDataAll(clsFileStream.clsEvStrm[] stream) {
      for (int pc = 0; pc < 16; pc++) DataAll[pc] = new List<sMsgDataTicks>();
      if (stream == null) return;

      //* create dataall
      foreach (clsFileStream.clsEvShort ev in stream.OfType<clsFileStream.clsEvShort>()) {
        if ((ev.Status & 0xf0) != 0xb0) continue;
        if (!IsDataAll(ev.Msg)) continue;
        int chan = ev.Status & 0x0f;
        DataAll[chan].Add(new sMsgDataTicks(ev.Msg, ev.Data, ev.Ticks));
      }

      //* check for warning msg for non-default pitchwheel range
      List<int> pwchlist = new List<int>();
      for (int ch = 0; ch < 16; ch++) {
        bool indpw = false;
        foreach (sMsgDataTicks e in DataAll[ch]) {
          if (e.Msg == 99 || e.Msg == 101) {  //RPN/NRPN MSB
            indpw = (e.Msg == 101 && e.Data == 0);  //RPN0 (pitchwheel range) MSB 
          } else if (e.Msg == 6) {  //data MSB
            if (indpw && e.Data != 2) pwchlist.Add(ch);
          }
        }
      }
      /*
      if (pwchlist.Count > 0 && !P.F.LoadWarningShown) {
        P.F.LoadWarningShown = true;
        string msg = "Warning: non-default pitchbend range on channels: ";
        foreach (int ch in pwchlist) msg += (ch + 1) + " ";
        MessageBox.Show(P.frmStart, msg);
      }
      */
    }

    private void CreateDataLast(clsFileStream.clsEvStrm[] stream) {
      if (stream == null) return;
      foreach (clsFileStream.clsEvShort ev in stream.OfType<clsFileStream.clsEvShort>()) {
        //if (ev.Port == 1) Debugger.Break();
        int status = ev.Status & 0xf0;
        if ((status != 0xb0) && (status != 0xc0)) continue;  //not ctlr or patch
        if (status == 0xb0 && !IsDataLast(ev.Msg)) continue;  //ctlr but dataall
        int chan = ev.Status & 0x0f;
        int ctlr = ev.Msg;  
        if (status == 0xc0) ctlr = PatchCtlrNum;  //patch: last ctlr
        if (DataLast[ctlr, chan] == null) DataLast[ctlr, chan] = new clsMap<int>(Dflt[ctlr]);
        int data = ev.Data;
        if (status == 0xc0) data = ev.Msg;
        DataLast[ctlr, chan].Add(ev.Ticks, data);  
      }
    }

    private bool IsDataAll(int evmsg) {
      if (evmsg >= 120) return false;  //includes patchctrlrnumber
      return (evmsg == 6 || evmsg == 38 || evmsg >= 96);  //data or data entry/RPN etc.
    }
    private bool IsDataLast(int evmsg) {
      if (evmsg == PatchCtlrNum) return true;
      return (evmsg != 6 && evmsg != 38 && evmsg < 96);
    }

    //internal void SetDataLastStart(int chan, int ctlr, int val) { 
    //  //* maybe not in use
    //  //* set start value for a controller (if empty)
    //  clsMap<int> map = DataLast[ctlr, chan];
    //  if (map != null) return;
    //  map = new clsMap<int>(Dflt[ctlr]);
    //  map.Add(0, val);
    //}

    internal void SendCtlrs(int ticks, int chan) {
      SendCtlrs(ticks, chan, chan);
    }

    internal void SendCtlrs(int ticks) {
      SendCtlrs(ticks, 0, 15);
    }

    private void SendCtlrs(int ticks, int chanlo, int chanhi) {
      //* send current midi ctlr values (or sequence of values) for ticks
      //* for all channels  
      //* used by streamplay locate and frmTrackMap record
      //* ticks= -1: send ticks0 DataAll and first DataLast controller 

      //xplay.ResetAllCtlrs();
      clsFileStream.clsPlay.ResetAllCtlrsStatic(MidiPlay.OutMStream);

#if DebugSend
      XStreamDebug = new FileStream(Cfg.CfgPath + @"\MidiCtlrsSend.debug." + FileNum++ + ".txt", FileMode.Create, FileAccess.Write);  //overwrite
      using (XSWDebug = new StreamWriter(XStreamDebug)) {
      //XSWDebug = new StreamWriter(XStreamDebug);
      DebugFmt = "{0,-4} {1:X2} {2:X2} {3:X2}";
      XSWDebug.WriteLine(DebugFmt, "src", "B0", "B1", "B2");
#endif

      //* send all
      for (int chan = chanlo; chan <= chanhi; chan++) {
        foreach (sMsgDataTicks ev in DataAll[chan]) {
          if (ev.Ticks > Math.Max(0, ticks)) break;
          //if (port != play.Port) continue;
          int status = 0xb0 | chan;
          int msg = ev.Msg;
          int data = ev.Data;
#if DebugSend
          XSWDebug.WriteLine(DebugFmt, "all", status, msg, data);
#endif
          //xplay.SendDirect(status, msg, data);
          MidiPlay.OutMStream.SendShortMsg(status, msg, data);
        }
      }

      //* send last
      for (int ctlr = 0; ctlr < 129; ctlr++) {
        //if (ctlr == PatchCtlrNum) Debugger.Break();
        if (!IsDataLast(ctlr)) continue;
        for (int chan = chanlo; chan <= chanhi; chan++) {
          //if (port != play.Port) continue;
          int? val = null;
          if (Override[ctlr] != null && Override[ctlr][chan] >= 0) {  //overridden
            val = Override[ctlr][chan];
          } else {
            clsMap<int> map = DataLast[ctlr, chan];
            if (map != null) {
              //if (ticks < 0 && map.Count > 0) val = map.ValByIndex(0);  //first value
              if (ticks < 0 && map.Count > 0) val = map.GetFirstValue();
              else val = map[ticks];  //map[ticks] may be null
            }
          }
          if (val == null) continue;
          int status, msg, data;
          if (ctlr == PatchCtlrNum) {
            status = 0xc0 | chan;
            msg = val.Value;
            data = 0;
          } else {  //not patch
            status = 0xb0 | chan;  
            msg = ctlr;
            data = (byte)val;
            if (msg == 0 && P.frmStart.chkFilterMidiBank.Checked) continue; 
          }
#if DebugSend
          XSWDebug.WriteLine(DebugFmt, "last", status, msg, data);
          //if (status == 0xb8 && msg == 0) Debugger.Break(); 
#endif
          //xplay.SendDirect(status, msg, data);
          MidiPlay.OutMStream.SendShortMsg(status, msg, data);
        }
      }
#if DebugSend
    }
      //XSWDebug.Close();
#endif
    }

    internal bool Bypass(clsFileStream.clsEvShort ev) {
      //* used by play stream to determine if ev should be bypassed
      int status = ev.Status & 0xf0;
      if (status != 0xb0 && status != 0xc0) return false;  //retain
      int chan = ev.Status & 0x0f;
      int ctlr;
      if (status == 0xc0) ctlr = PatchCtlrNum; else ctlr = ev.Msg;
      if (status == 0xb0 && ctlr == 0 && P.frmStart.chkFilterMidiBank.Checked) return true;
      if (ctlr != PatchCtlrNum && ctlr >= 120) return true;  //bypass realtime msgs
      if (Override[ctlr] != null && Override[ctlr][chan] >= 0) return true;
      return false;
    }

    internal int GetMainValue(eCtlr ectlr, int portchan, out bool multiple) {
      //* return single value for a controller, allowing for overrides
      //* used by frmMultiMap to display a value for Pan, Vol, Patch
      //* multiple = true if ctlr not overridden, and more than one ctlr in portchan
      //* return -1 if patch and chan == 9 (percussion)
      multiple = false;
      int ctlr = (int)ectlr;
      if (portchan < 0) return Dflt[ctlr];
      if (Override[ctlr] == null) {
        LogicError.Throw(eLogicError.X015);
        return Dflt[ctlr];
      }
      if (Override[ctlr][portchan] >= 0) return Override[ctlr][portchan];
      clsMap<int> map = DataLast[ctlr, portchan];
      if (map == null) return Dflt[ctlr];
      if (map.Count > 1) multiple = true;
      if (ctlr == PatchCtlrNum && portchan % 16 == 9) return -1;  //channel 10 percussion 
      //return map.ValByIndex(0);
      return map.GetFirstValue();
    }

    internal string[] GetPatchList(int portchan) {
      //* used by frmSummary
      clsMap<int> map = DataLast[PatchCtlrNum, portchan];
      if (map == null) return new String[] { "" };
      List<string> list = new List<string>();
      //for (int i = 0; i < map.Count; i++) list.Add(GeneralMidiList.Desc[map.ValByIndex(i)]);
      foreach (KeyValuePair<int, int> pair in map) list.Add(GeneralMidiList.Desc[pair.Value]);
      if (list.Count == 0) list.Add("");
      return list.ToArray();
    }
  }
}
