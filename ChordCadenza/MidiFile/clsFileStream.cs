#undef DebugTicks
#undef DebugTicksAndTrks
#undef DebugStream

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace ChordCadenza {
  public partial class clsFileStream {
    //* recstrm index of last ev for note[n]
    private clsF.clsindSave _indSave = new clsF.clsindSave();  //FSTrackMap only (ignore for FSConv)
    private int?[] LastEv = new int?[128];  // ON = index, OFF = ~index
    internal List<clsEvShort> RecStrmNew = null;  
    internal clsNoteMapMidi NoteMap;
    internal clsStrmLL StrmLL;
    internal clsEvStrm[] Strm;   //[seq]
    internal clsMap<int> TempoMap = new clsMap<int>(500000);  //before TempoFactor adjustment
    internal List<string> Text00 = new List<string>();  //text on cond trk at ticks 0
    internal clsMidiCtlrs MidiCtlrs;
    internal clsLoadMidiFile LoadMidiFile;
    internal clsTrks.Array<bool> TrkSelect;  //[trk]
    internal enum eTrkType: int { Empty, Percussion, Melody, AChords, BChords, Bass, Sparse, NoStyle };
    internal clsTrks.Array<eTrkType> TrkType;  //[trk]
    internal clsTrks.Array<float> ChordNeg;  //[trk]
    internal clsTrks.Array<float> Poly;  //[trk]
    internal clsTrks.Array<int> OnCount;  //[trk]
    internal clsTrks.Array<int[]> ChanOnCount;  //[trk][chan]
    internal clsTrks.Array<int[]> ChanAllCount;  //[trk][chan]
    internal clsTrks.Array<int> OnCountX10;
    internal clsTrks.Array<int> TrkMaxPitch;
    internal clsTrks.Array<int> TrkMinPitch;
    internal clsTrks.Array<string> Title;  //track title
    internal string ProjectTitle = "";
    internal int MidiTempoMax;
    internal int MidiTempoMin;
    internal clsTrks.Array<bool> indPitchBend;
    internal bool ExclPB;
    //internal int NumTrks {
    //  get {
    //    if (!P.F.MidiFileLoaded) return 0;
    //    return LoadMidiFile.NumTrks;
    //  }
    //}  //excl. conductor trk
    internal static bool[] BassPatches = new bool[128];
    internal int RecChan = -1;
    private int[] OnDI = new int[128];   //[pitch]
    private delegate void delegRefresh();
    private int DeleteNotes_TicksLo = 0;
    private int DeleteNotes_TicksHi = 0;

    static clsFileStream() {
      //P.F.QIdd = Cfg.QIdd;
      BassPatches[32] = true;  //Acoustic Bass
      BassPatches[33] = true;  //Electric Bass (finger)
      BassPatches[34] = true;  //Electric Bass (pick)
      BassPatches[35] = true;  //Fretless Bass
      BassPatches[36] = true;  //Slap Bass 1
      BassPatches[37] = true;  //Slap Bass 2
      BassPatches[38] = true;  //Synth Bass 1
      BassPatches[39] = true;  //Synth Bass 2
      BassPatches[43] = true;  //Contrabass
      BassPatches[58] = true;  //Tuba
    }

    internal clsFileStream() {  //default filestream (no midifile)
      ResetOnQI();
      TrkSelect = new clsTrks.Array<bool>(true);
      TrkMinPitch = new clsTrks.Array<int>(127);
      TrkMaxPitch = new clsTrks.Array<int>(0);
    }

    internal clsFileStream(string filename) {  //readheader only
      //* create mtime
      //* ticksperqnote from midifile
      //* tsig 4/4 (default)
      ResetOnQI();
      LoadMidiFile = new clsLoadMidiFile(this);
    }

    internal clsFileStream(string filename, clsTrks.Array<bool> trkselect, bool excl10, bool firsttime, bool exclpb) {
      ResetOnQI();
      ExclPB = exclpb;
      //OldStrmLL = new LinkedList<clsEvStrm>();
      StrmLL = new clsStrmLL(this);
      LoadMidiFile = new clsLoadMidiFile(this, filename, trkselect, excl10, firsttime);
      MidiTempoMin = 60000000 / Forms.frmStart.TempoMax;  //max -> min (assumes TSigDD = 4)
      MidiTempoMax = 60000000 / Forms.frmStart.TempoMin;  //min -> max (assumes TSigDD = 4)
      P.F.SendInit();
    }

    //private int GetRecTrk() {
    //  if (P.F.frmTrackMap == null) return NumTrks - 1;
    //  int trk = -1;
    //  for (int t = 0; t < P.F.frmTrackMap.Chks.Length; t++) {
    //    CheckBox chk = P.F.frmTrackMap.Chks[t];
    //    if (chk != null && chk.Checked) {
    //      if (trk < 0) trk = t; else return -2;  //-2 if multiple chks checked
    //    }
    //  }
    //  if (trk == -1) {  //nothing checked
    //    //trk = P.F.frmMultiMap.Chks.Length - 1;  //no trks selected - use last trk
    //    for (int t = 0; t < P.F.frmTrackMap.Chks.Length; t++) {
    //      if (P.F.frmTrackMap.Chks[t] != null && OnCount[t] == 0 && Title[t].Length == 0) {
    //        P.F.frmTrackMap.Chks[t].Checked = true;
    //        return t;   //first empty trk with no title
    //      }
    //    }
    //  }
    //  return trk;
    //}

    internal bool indSave {
      get { return _indSave.Ind; }
      set { _indSave.Ind = value; }
    }

    internal void InitRecStrm(long ticks) {  //new
      RecStrmNew.Add(new clsEvShort(0,
        (int)ticks,
        P.F.frmTrackMap.RecTrk.TrkNum,
        (byte)(0xb0 | RecChan),
        0x40, 0));  //sustain off

      RecStrmNew.Add(new clsEvShort(0,
        (int)ticks,
        P.F.frmTrackMap.RecTrk.TrkNum,
        (byte)(0xe0 | RecChan),
        0, 0x40));  //pitchwheel centred

      //bool multiple;
      //int patch = MidiCtlrs.GetMainValue(clsMidiCtlrs.eCtlr.Patch, RecChan, out multiple);
      //if (patch < 0) {  //new trk (patch etc. not set)
      //  int msg, data;
      //  clsTrks.T rectrk = P.F.frmTrackMap.RecTrk;

      //  //* set chan
      //  P.F.frmTrackMap.nudChans[rectrk].Value = String.Format("C{0,-2}", RecChan + 1);
      //  P.F.Chan[rectrk] = RecChan;

      //  //* reload trackpans using RecChan
      //  P.F.frmTrackMap.LoadTrackPan(rectrk);  

      //  //* set patch
      //  msg = P.frmSC.cmbKBChanPatch.SelectedIndex - 1;
      //  RecStrmNew.Add(new clsEvShort(0, -1, (byte)(0xc0 & RecChan), (byte)msg));  //patch
      //  P.F.frmTrackMap.cmbPatches[rectrk].SelectedIndex = msg;
      //  P.F.Patch[RecChan] = msg;

      //  //* set vol
      //  data = (P.frmSC == null) ? 80 : P.frmSC.trkKBChanVol.Value;  //vol
      //  RecStrmNew.Add(new clsEvShort(0, -1, (byte)(0xb0 & RecChan), 7, (byte)data));
      //  if (P.F.frmTrackMap.optVol.Checked) P.F.frmTrackMap.TrkBar[rectrk].Value = data;
      //  P.F.Vol[RecChan] = msg;

      //  //* set pan
      //  data = (P.frmSC == null) ? 64 : P.frmSC.trkKBChanPan.Value;  //pan
      //  RecStrmNew.Add(new clsEvShort(0, -1, (byte)(0xb0 & RecChan), 10, (byte)data));
      //  if (P.F.frmTrackMap.optPan.Checked) P.F.frmTrackMap.TrkBar[rectrk].Value = data;
      //  P.F.Pan[RecChan] = msg;
      //}
    }

    //internal bool InitRecWipe() {
    //  //* called from cmdWipe (not cmdWipeAndRecord)
    //  //* invalidate channel
    //  RecTrk = GetRecTrk();
    //  if (RecTrk < 0) return false;
    //  P.F.Chan[RecTrk] = -1;
    //  WipeTrack();
    //  MidiCtlrs = new clsMidiCtlrs(this);
    //  return true;
    //}

    //internal bool InitRecNew(bool wipe) {
    //  //* record on empty trk (from cmdWipeAndRecord or cmdRecord)
    //  RecTrk = GetRecTrk();
    //  if (RecTrk < 0) return false;
    //  if (wipe) WipeTrack();  //no change to P.F.Chan[RecTrk]

    //  if (P.frmStart.chkAutoRecChan.Checked) {
    //    bool[] indchan = new bool[16];
    //    foreach (int chan in P.F.Chan) {
    //      if (chan >= 0 && chan <= 15) indchan[chan] = true;
    //    }
    //    for (int chan = 0; chan < 16; chan++) {  //find first free chan
    //      if (chan != 9 && !indchan[chan]) {
    //        P.F.Chan[RecTrk] = chan;
    //        MidiPlay.KBOutChan = chan;
    //        P.frmStart.Bypass_Event = true;
    //        P.frmSC.nudKBChanOut.Value = MidiPlay.KBOutChan + 1;
    //        P.frmStart.Bypass_Event = false;
    //        break;
    //      }
    //    }
    //  } else if (P.F.Chan[RecTrk] < 0) {  
    //    P.F.Chan[RecTrk] = MidiPlay.KBOutChan;
    //  } else {  //
    //    MidiPlay.KBOutChan = P.F.Chan[RecTrk];
    //    //MidiPlay.KBOutChanAutoRiff = P.F.Chan[RecTrk];
    //    P.frmStart.Bypass_Event = true;
    //    P.frmSC.nudKBChanOut.Value = MidiPlay.KBOutChan + 1;
    //    P.frmStart.Bypass_Event = false;
    //    //* set patch, vol, pan on 
    //    bool multiple;

    //    int patch = MidiCtlrs.GetMainValue(clsMidiCtlrs.eCtlr.Patch, MidiPlay.KBOutChan, out multiple);
    //    if (patch >= 0 && patch <= 127) {
    //      P.frmSC.cmbKBChanPatch.SelectedIndex = patch + 1;  //no bypass - SendShortMsg(...)
    //    }

    //    int vol = MidiCtlrs.GetMainValue(clsMidiCtlrs.eCtlr.Vol, MidiPlay.KBOutChan, out multiple);
    //    if (vol >= 0) {
    //      if (P.frmSC != null) P.frmSC.trkKBChanVol.Value = vol;
    //      MidiPlay.OutMKB.SendShortMsg(0xb0 | P.F.Chan[RecTrk], 7, vol);  //7 = chanvol
    //    }

    //    int pan = MidiCtlrs.GetMainValue(clsMidiCtlrs.eCtlr.Pan, MidiPlay.KBOutChan, out multiple);
    //    if (pan >= 0) {
    //      if (P.frmSC != null) P.frmSC.trkKBChanPan.Value = pan;
    //      MidiPlay.OutMKB.SendShortMsg(0xb0 | P.F.Chan[RecTrk], 10, pan);  //10 = chanpan
    //    }
    //  }

    //  MidiCtlrs = new clsMidiCtlrs(this);
    //  indRec = true;
    //  RecStrm = new List<clsEvShort>(500);
    //  int recchan = P.F.Chan[RecTrk];
    //  int status, msg, data;

    //  status = 0xc0 | recchan;
    //  msg = P.frmSC.cmbKBChanPatch.SelectedIndex - 1;
    //  RecStrm.Add(new clsEvShort(0, RecTrk, (byte)status, (byte)msg));

    //  status = 0xb0 | recchan;
    //  msg = 7;  //vol
    //  data = (P.frmSC == null) ? 80 : P.frmSC.trkKBChanVol.Value;
    //  RecStrm.Add(new clsEvShort(0, RecTrk, (byte)status, (byte)msg, (byte)data));

    //  status = 0xb0 | recchan;
    //  msg = 10;  //pan
    //  data = (P.frmSC == null) ? 64 : P.frmSC.trkKBChanPan.Value;
    //  RecStrm.Add(new clsEvShort(0, RecTrk, (byte)status, (byte)msg, (byte)data));
    //  return true;
    //}

    //private void InitRecFields() {
    //  TrkMaxPitch[RecTrk] = 0;
    //  TrkMinPitch[RecTrk] = 127;
    //  OnCount[RecTrk] = 0;
    //  OnCountX10[RecTrk] = 0;
    //  for (int chan = 0; chan < 16; chan++) ChanOnCount[RecTrk, chan] = 0;
    //  for (int i = 0; i < 128; i++) LastEv[i] = null;
    //}

    internal int MaxTicks {
      get {
        int t0 = Strm[Strm.Length - 1].Ticks;
        return t0;
      }
    }

    //internal void RecordEv(long ticks, byte b0, byte b1, byte b2) {
    //  if (!indRec) return;
    //  if (P.frmSC == null) return;
    //  P.frmSC.BeginInvoke(new delegRecordEvExec(RecordEvExec), ticks, b0, b1, b2);
    //}

    //internal void SetKBChanAndPatch(clsTrks.T trk) {
    //  RecChan = SetNewChan(trk);
    //  P.frmSC.nudKBChanOut.Value = RecChan;
    //  MidiPlay.KBOutChan = RecChan;
    //  bool multiple;
    //  int patch = MidiCtlrs.GetMainValue(clsMidiCtlrs.eCtlr.Patch, RecChan, out multiple);
    //  if (patch >= 0 && !multiple) P.frmSC.cmbKBChanPatch.SelectedIndex = patch + 1;
    //  ResetOnQI();
    //}

    internal void ResetOnQI() {
      for (int i = 0; i < 128; i++) OnDI[i] = -1;
    }

    internal int SetNewTrkChan(clsTrks.T trk) {
      if (P.F.Chan[trk] > 0) return P.F.Chan[trk];

      int outchan = -1;
      //int chordchan = (P.frmSaveMidiFileAs.chkSaveChordTrack.Checked) ?
      //  (int)P.frmSaveMidiFileAs.nudChordChan.Value : -1;
      bool[] indchan = new bool[16];

      foreach (int chan in P.F.Chan) {
        if (chan >= 0 && chan <= 15) indchan[chan] = true;
      }

      //* try KBOutChan
      //if (MidiPlay.KBOutChan >= 0 && !indchan[MidiPlay.KBOutChan] && chordchan != MidiPlay.KBOutChan) {
      if (MidiPlay.KBOutChan >= 0 && !indchan[MidiPlay.KBOutChan]) {
          outchan = MidiPlay.KBOutChan;
      } else {
        //* try first free chan
        for (int chan = 0; chan < 16; chan++) {  //find first free chan
          if (chan == 9) continue;
          if (indchan[chan]) continue;
          //if (chordchan == chan) continue;
          outchan = chan;
          break;
        }
      }

      //* find chan on muted trk
      if (outchan < 0 && !P.frmSaveMidiFileAs.chkSaveMutedTrks.Checked) {
        //for (int trk = 0; trk < NumTrks; trk++) {
        foreach (clsTrks.T t in P.F.Chan.Next) {
          if (P.F.Mute[t]) {
            outchan = P.F.Chan[t];
            break;
          }
        }
      }

      if (outchan < 0) {  //all chans in use
        outchan = MidiPlay.KBOutChan;  //it'll have to do
      }

      return outchan;
    }

    internal void RecordBeatNM() {
      //* update all positive (and zero) OnQI[] from prevbeat 
      if (P.F?.frmTrackMap?.RecTrk == null) return;
      int qthis = P.F.CurrentBBT.Ticks / P.F.TicksPerQI;
      //int qprev = P.F.CurrentBBT.GetPrevBeat().Ticks / P.F.TicksPerQI;
      int qnext = P.F.CurrentBBT.GetNextBeat().Ticks / P.F.TicksPerQI;
      //qnext = Math.Min(qnext, NoteMap.GetLengthQTime());
      qnext = Math.Min(qnext, P.F.MaxBBT.QI);
      for (int p = 0; p < 128; p++) {
        if (OnDI[p] < 0) continue;
        int qlo = Math.Max(qthis, OnDI[p]);
        for (int q = qlo; q <= qnext; q++) {
          NoteMap[q, p, P.F.frmTrackMap.RecTrk, false] = true;
          NoteMap[q, p.Mod12(), P.F.frmTrackMap.RecTrk, true] = true;
        }
      }
      P.F.frmTrackMap.BeginInvoke(new delegRefresh(P.F.frmTrackMap.RefreshRec));
    }

    internal void RecordEvNM(long ticks, byte b0, byte b1, byte b2) {
      if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.MidiStream) return;
      if (P.F?.frmTrackMap?.RecTrk == null || RecChan < 0) return;
      int status = b0 & 0xf0;
      if (status < 0x80 || status >= 0xf0) return;  //not channel status 
      bool? on = null;
      if (status == 0x90) on = (b2 > 0);  //note ON/OFF
      else if (status == 0x80) on = false;  //note OFF
      int qi = (int)ticks / P.F.TicksPerQI;

      if (on.HasValue) {
        if (on.Value) {  //ON ev
          if (OnDI[b1] < 0) OnDI[b1] = qi;
          UpdateChanOnTotals(P.F.frmTrackMap.RecTrk, b0 & 0x0f, b1);  //min, max, tots
        } else {  //OFF ev
          if (OnDI[b1] < 0) return;  //no ON ev preceding
          if (OnDI[b1] < qi) {
            for (int q = OnDI[b1]; q <= qi; q++) {
              NoteMap[q, (int)b1, P.F.frmTrackMap.RecTrk, false] = true;
              NoteMap[q, ((int)b1).Mod12(), P.F.frmTrackMap.RecTrk, true] = true;
            }
            OnDI[b1] = -1;
            P.F.frmTrackMap.BeginInvoke(new delegRefresh(P.F.frmTrackMap.RefreshRec));
          }
        }
      }

      RecordEvStrm(ticks, b0, b1, b2);  //-> RecStrmNew
    }

    private void RecordEvStrm(long ticks, byte b0, byte b1, byte b2) {  //new
      if (RecStrmNew == null) return;
      if (P.F.frmTrackMap == null) return;
      int status = b0 & 0xf0;

      bool? on = null;
      if (status == 0x90) on = (b2 > 0);  //note ON/OFF
      else if (status == 0x80) on = false;  //note OFF

      if (on.HasValue) {
        if (on.Value) {  //ON
          //* don't play ON if another ON already played
          if (LastEv[b1].HasValue && LastEv[b1].Value >= 0) {
            //Debug.WriteLine("clsFileStream: RecordEvStrm: 2nd ON: " + b0 + ' ' + b1);
            return;  //2nd. ON
          }
          LastEv[b1] = RecStrmNew.Count;  //ON to be added at end
        } else { //OFF
          //* don't play OFF if another OFF already played
          if (LastEv[b1].HasValue && LastEv[b1].Value < 0) {
            //Debug.WriteLine("clsFileStream: RecordEvStrm: 2nd OFF: " + b0 + ' ' + b1);
            return;  //2nd. OFF
          }
          LastEv[b1] = ~RecStrmNew.Count;  //to be added at end
        }
      } else {  //not ON or OFF
        if (status == 0xb0 && b1 == 0x7b) {  //allnotesoff
          //* turn any ON notes OFF (use instead of AllNotesOff() to avoid AllNotesOff() in RecStrm)
          for (int n = 0; n < LastEv.Length; n++) {
            if (LastEv[n].HasValue && LastEv[n].Value >= 0) {  //hanging ON ev
              int chan = b0 & 0x0f;
              RecordEvStrm(ticks, (byte)(0x80 & chan), (byte)n, 0);  //recursive
            }
          }
          return;
        //} else if (ticks > 0 && (b0 == 0xb0 || b0 == 0xc0)) {  //ctlr or patch
        //  return;
        }
      }

      //* update play stream
      clsEvShort ev = new clsEvShort(0, (int)ticks, P.F.frmTrackMap.RecTrk.TrkNum, b0, b1, b2);
      //ev.QTime = GetQTime(ev.Ticks);
      //if (status == 0xe0) Debug.WriteLine("PW: " + b1 + ' ' + b2);
      RecStrmNew.Add(ev);
      indSave = true;

      //if (on.HasValue) {  //ON or OFF ev
        //int pitch = b1;
        //int vel = b2;
        //OO[RecTrk].Add(new clsOO((int)ticks, on.Value, chan, pitch, vel, P.F.TicksPerQI));
        //if (on.Value) LastEv[b1] = ev.Ticks;  //ON ev
      //}
    }

    internal void MergeRecStrm() {  //called from QIPlay.FinalizeStop()
      //* merge recstrm
      if (RecStrmNew == null) return;
      foreach (clsEvShort ev in RecStrmNew) {
        //int s = ev.Status & 0xf0;
        //if (s != 0x90 && s != 0x80) continue;  //not ON or OFF  
        StrmLL.InsertEv(ev);  
      }
      Strm = StrmLL.GetArray();

      //* propagate PW
      if (P.F.frmTrackMap?.RecTrk != null) {
        NoteMap.PropagatePW(this, P.F.MaxBBT.MaxNoteMapQI, P.F.frmTrackMap.RecTrk);
      }
    }

    internal void UpdateChanOnTotals(clsTrks.T trk, int chan, int pitch) {
      if (chan != 9) OnCountX10[trk]++;  //chan9 percussion
      TrkMaxPitch[trk] = Math.Max(TrkMaxPitch[trk], pitch);
      TrkMinPitch[trk] = Math.Min(TrkMinPitch[trk], pitch);
      OnCount[trk]++;
      ChanOnCount[trk][chan]++;
    }

    //private bool indWipeEv(clsEvStrm ev) {
    //  //* return true if ev to be removed from Strm
    //  if (ev.Trk != RecTrk) return false;
    //  if (ev.Ticks > 0) return true;  //remove all non-initial evs
    //  if (ev is clsEvShort) {
    //    clsEvShort evshort = (clsEvShort)ev;
    //    if ((evshort.Status & 0xf0) == 0xb0) return false;  //don't remove initial ctlr values
    //    if ((evshort.Status & 0xf0) == 0xc0) return false;  //don't remove initial patch
    //  }
    //  return true;
    //}

    //private void WipeTrack() {
    //  //if (indRec) return;   //should not happen
    //  //if (RecStrm == null) return;

    //  lock (ChordCadenza.clsPlay.TimerLock) {  //lock MidiInKB etc
    //    Cursor.Current = Cursors.WaitCursor;

    //    //* remove rectrk evs from Strm
    //    List<clsEvStrm> newevs = new List<clsEvStrm>(Strm.Length);
    //    foreach (clsEvStrm ev in Strm) {
    //      if (ev.Trk != RecTrk) newevs.Add(ev);
    //      //if (!indWipeEv(ev)) newevs.Add(ev);
    //    }
    //    Strm = newevs.ToArray();
    //    RecStrm = null;

    //    //* remove rectrk from OO
    //    OO[RecTrk] = new List<clsOO>(500);

    //    //* initialize rec fields
    //    InitRecFields();

    //    //MidiCtlrs = new clsMidiCtlrs(this);

    //    for (int q = 0; q < OnOff.Length; q++) {
    //      for (int n = 0; n < 128; n++) {
    //        OnOff[q, n, RecTrk, false] = 0;
    //      }
    //    }

    //    NoteMap = new clsNoteMapMidi(OnOff, NumTrks, TrkMinPitch, TrkMaxPitch, this);

    //    Cursor.Current = Cursors.Default;
    //  }
    //}

    //internal bool SaveRecord() {
    //  //if (KBOutChan >= 0) MidiPlay.KBOutChan = KBOutChan;  //restore from value saved before rec
    //  if (!indRec) return false;
    //  lock (ChordCadenza.clsPlay.TimerLock) {  //lock MidiInKB etc
    //    indRec = false;

    //    if (RecStrm.Count == 0) {
    //      RecStrm = null;
    //    } else {
    //      Cursor.Current = Cursors.WaitCursor;
    //      Stopwatch watch = new Stopwatch();
    //      watch.Start();

    //      //* update totals
    //      foreach (clsEvStrm ev in RecStrm) {
    //        if (ev is clsEvShort) {
    //          clsEvShort evshort = (clsEvShort)ev;
    //          if (evshort.IsOnEv()) UpdateTotals(RecTrk, evshort.Status & 0x0f, evshort.Msg);
    //        }
    //      }

    //      //* merge RecStrm into Strm
    //      for (int i = 0; i < RecStrm.Count; i++) {
    //        RecStrm[i].QTime = GetQTime(RecStrm[i].Ticks);
    //        RecStrm[i].Seq = i;
    //      }
    //      for (int i = 0; i < Strm.Length; i++) {
    //        Strm[i].Seq = i;
    //      }
    //      List<clsEvStrm> list = Strm.ToList();
    //      list.AddRange(RecStrm);
    //      list.Sort();
    //      Strm = list.ToArray();

    //      //* set P.F.Chan etc.
    //      //SetChan(RecTrk);

    //      UpdateFromStrm(RecTrk);

    //      watch.Stop();
    //      Debug.WriteLine("SaveRecord msecs = " + watch.ElapsedMilliseconds);
    //      if (P.frmSC != null) {
    //        //P.F.frmMSC.cmdWipeTrack.Enabled = true;
    //      }
    //      Cursor.Current = Cursors.Default;
    //    }

    //    if (P.F.frmTrackMap != null) P.F.frmTrackMap.UpdateTrk(RecTrk);
    //    return true;
    //  }
    //}

//    private void UpdateFromStrm(clsTrks.T trk) {
//      //* recreate Strm, oo, OnOff, NoteMap 
//#if DEBUG
//      Stopwatch sw = new Stopwatch();
//      sw.Start();
//#endif
//      List<clsFileStream.clsEvStrm> liststrm = new List<clsFileStream.clsEvStrm>(1000);
//      List<clsOO> listoo = new List<clsOO>();
//      clsTrks.Array<List<clsOO>> oo = new clsTrks.Array<List<clsOO>>();
//      foreach (clsEvStrm ev in Strm) {
//        //if (ev.Trk != null) liststrm.Add(ev);  //all trks
//        if (!ev.Selected) liststrm.Add(ev);  //all trks
//        if (ev is clsEvShort && ev.Trk == trk && !ev.Selected) {
//          clsEvShort evshort = (clsEvShort)ev;
//          if ((evshort.Status & 0xf0) == 0x80 || (evshort.Status & 0xf0) == 0x90) {
//            listoo.Add(new clsOO(ev.Ticks, evshort.IsOnEv(), evshort.Status & 0x0f, evshort.Msg, evshort.Data, P.F.TicksPerQI));
//            if (evshort.IsOnEv()) UpdateTotals(trk, evshort.Status & 0x0f, evshort.Msg);
//          }
//        }
//        oo[trk] = listoo;
//      }
//      Strm = liststrm.ToArray(); StrmMsg(sw, 0);
//      //* update midi ctlrs
//      MidiCtlrs = new clsMidiCtlrs(this); StrmMsg(sw, 1);
//      clsOnOff onoff = new clsOnOff(P.F.MaxQTime + 2); StrmMsg(sw, 2);
//      CreateOnOffTrk(oo, true, onoff, trk); StrmMsg(sw, 3);
//      NoteMap.UpdateTrk(onoff, trk, TrkMinPitch[trk], TrkMaxPitch[trk], this); StrmMsg(sw, 4);
//    }

    private void UpdateFromStrm(clsTrks.T trk, int tickslo, int tickshi) {
      Stopwatch sw = new Stopwatch();
#if DEBUG
      sw.Start();
#endif
      ResetCounts(trk);

      //* remove deleted notes from Strm and StrmLL
      List<clsFileStream.clsEvStrm> liststrm = new List<clsFileStream.clsEvStrm>(1000);

      foreach (clsEvStrm ev in Strm) {
        //* copy other tracks and metaevs 
        if (!(ev is clsEvShort) || ev.Trk != trk) {
          liststrm.Add(ev);
          continue;
        }

        clsEvShort evshort = (clsEvShort)ev;
        int status = evshort.Status & 0xf0;

        if (status == 0x80 || status == 0x90) {  //ON or OFF ev
          if (evshort.Selected) continue;  //delete    
          //* update ON totals
          if (evshort.Trk == trk && evshort.IsOnEv()) {
            UpdateChanOnTotals(trk, evshort.Status & 0x0f, evshort.Msg);
          }
          liststrm.Add(ev);  //all trks
        } else {  //not ON/OFF or metaev (controller) - remove if in range
          if (evshort.Trk != trk || evshort.Ticks < tickslo || evshort.Ticks > tickshi) {
            liststrm.Add(ev);
          }
        }
      }
      Strm = liststrm.ToArray(); StrmMsg(sw, 0);

      MidiCtlrs = new clsMidiCtlrs(Strm); StrmMsg(sw, 1);
      NoteMap.OnOffPairs = null;  //recreate as required
      StrmLL = new clsStrmLL(this, Strm);

      //* remove deleted notes from NoteMap
      for (int q = DeleteNotes_TicksLo / P.F.TicksPerQI; q < DeleteNotes_TicksHi / P.F.TicksPerQI; q++) {
        for (int note = 0; note < 128; note++) {
          if (NoteMap.Delete[q, note]) {
            NoteMap[q, note, trk, false] = false;  //not modded 
            NoteMap.SyncPCFromPitches(q, note.Mod12(), trk);
          }
        }
      }
      NoteMap.Delete = null;
      //ResetMinMaxPitches(trk);

      //* recreate PB[trk] from stream
      NoteMap.PropagatePW(this, P.F.MaxBBT.MaxNoteMapQI, trk);
    }

    private void StrmMsg(Stopwatch sw, int seq) {
#if DEBUG
      Debug.WriteLine("Strm Elapsed #" + seq + " ms = " + sw.ElapsedMilliseconds);
#endif
    }

    internal int DeleteNotes(clsTrks.T trk, int tickslo, int tickshi) {
      //* delete any notes marked for deletion (NoteMap.Trk_Selected[] set)
      int count = 0;
      for (int i = 0; i < Strm.Length; i++) {
        if (Strm[i].Selected) count++;
      }
      if (count == 0) return 0;

      //* recreate Strm, oo, OnOff, NoteMap 
      UpdateFromStrm(trk, tickslo, tickshi);  //->UpdateTotals() (incl. Trk{Max|Min}Pitch)
      return count;
    }

    //internal void CalcTrkTotals(clsTrks.T trk) {
    //  //* null trk: all trks
    //  //* reset totals
    //  foreach (clsTrks.T t in OnCount.Next) {
    //    if (trk != null && trk != t) continue;
    //    OnCount[t] = 0;
    //    ChanOnCount[t] = new int[16];
    //    OnCountX10[t] = 0;
    //    TrkMaxPitch[t] = 0;
    //    TrkMinPitch[t] = 127;
    //  }

    //  //* process strm evs
    //  foreach (clsEvStrm ev in Strm) {
    //    if (trk != null && ev.Trk != trk) continue;
    //    if (!(ev is clsEvShort)) continue;
    //    clsEvShort evshort = (clsEvShort)ev;
    //    if ((evshort.Status & 0xf0) != 0x90) continue;  //not ON ev
    //    if (evshort.Data == 0) continue;  //ON vel 0
    //    int chan = (evshort.Status & 0x0f);
    //    OnCount[trk]++;
    //    ChanOnCount[trk][chan]++;
    //    if (chan == 9) OnCountX10[trk]++;
    //    if (P.F.frmTrackMap?.RecTrk != trk) {
    //      TrkMinPitch[trk] = Math.Min(TrkMinPitch[trk], evshort.Msg);
    //      TrkMaxPitch[trk] = Math.Max(TrkMaxPitch[trk], evshort.Msg);
    //    }
    //  }

    //  //* process trks
    //  foreach (clsTrks.T t in OnCount.Next) {
    //    if (trk != null && trk != t) continue;
    //    if (P.F.frmTrackMap?.RecTrk == t || TrkMinPitch[t] == 127) {
    //      //* allow for wide range of recorded notes
    //      TrkMinPitch[t] = 24;
    //      TrkMaxPitch[t] = 84;
    //    }
    //    P.F.frmTrackMap?.SetOctaves(t);
    //  }
    //}

    //internal void ResetMinMaxPitches(clsTrks.T trk) {
    //  if (TrkMinPitch[trk] == 127 && TrkMaxPitch[trk] == 0) {
    //    TrkMinPitch[trk] = 24;
    //    TrkMaxPitch[trk] = 84;
    //  }
    //  P.F.frmTrackMap?.SetOctaves(trk);
    //}

    private void ResetCounts(clsTrks.T trk) {
      foreach (clsTrks.T t in OnCount.Next) {
        if (trk != null && trk != t) continue;
        OnCount[t] = 0;
        ChanOnCount[t] = new int[16];
        ChanAllCount[t] = new int[16];
        OnCountX10[t] = 0;
        TrkMaxPitch[t] = 0;
        TrkMinPitch[t] = 127;
      }
    }

    internal int MarkDeleteNotes(clsTrks.T trk, int tickslo, int tickshi) {
      //* mark notes for deletion (set NoteMap.Trk_Selected)
      //* only delete notes that are fully contained in the select area
      //* return count of notes Selected for deletion
      //* called from TrackMap MouseClick
      DeleteNotes_TicksLo = tickslo;
      DeleteNotes_TicksHi = tickshi;
      int count = 0;
      NoteMap.Delete = new bool[P.F.MaxBBT.MaxNoteMapQI, 128];

      //* reset any existing Selected evs
      for (int i = 0; i < Strm.Length; i++) Strm[i].Selected = false;

      //* set Selected on/off pairs
      for (int i = 0; i < Strm.Length; i++) {
        //* get ON ev
        clsEvStrm ev = Strm[i];
        if (ev.Trk != trk) continue;
        if (!(ev is clsEvShort)) continue;
        if (ev.Ticks < tickslo) continue;
        if (ev.Ticks > tickshi) break;
        clsEvShort evshort = (clsEvShort)ev;
        if (!evshort.IsOnEv()) continue;  //not ON ev
        //evon.Selected = true;  //evon.Trk = null;  //int.MinValue;  //to be deleted
        //count++;
        int j = i + 1;
        for (; j < Strm.Length; j++) {
          //* get OFF ev
          clsEvStrm evoff = Strm[j];
          if (evoff.Ticks > tickshi) break;
          if (evoff.Trk != trk) continue;
          if (!(evoff is clsEvShort)) continue;
          clsEvShort evoffshort = (clsEvShort)evoff;
          if (evoffshort.IsOffEv()) {
            if (evoffshort.Msg == evshort.Msg) {  //pitches match
              ev.Selected = true;  //to be deleted
              evoff.Selected = true;  //to be deleted
              count++;
              for (int q = ev.QTime; q <= evoff.QTime; q++) {
                NoteMap.Delete[q, evshort.Msg] = true;
              }
              break;
            }
          }
        }
        if (j >= Strm.Length) {  //no matching OFF
          LogicError.Throw(eLogicError.X088);
          return 0;
        }
      }

      //* recreate StrmLL

      return count;
    }

    internal void SetChan(clsTrks.T trk) {
      if (P.F.Chan[trk] < 0) P.F.Chan[trk] = CheckChan(trk);
    }

    internal int CheckChan(clsTrks.T trk) {
      int chan = -1, cnt;
      int max = 0;
      bool multichan = false;  //more than one channel on one track
      for (int ch = 0; ch < 16; ch++) {
        //cnt = ChanOnCount[trk][ch];
        cnt = ChanAllCount[trk][ch];
        if (max > 0 && cnt > 0) multichan = true;
        if (cnt > max) {
          max = cnt;
          chan = ch;
        }
      }
      if (multichan) {  //can't use MessageBox - may be obscured by windows explorer window
        string msg = "Warning: Multiple channels found on track " + trk.ToString();
        P.MMSW?.WriteLine(msg);
        Debug.WriteLine(msg);
      }
      return chan;
    }

    internal void SetMaxTicks(int maxmiditicks) {  //calculate maxticks - bar boundary...
      if (P.F.MaxBBT == null) {  //not set by previous load midifile
        //int ticks = Strm[Strm.Length - 1].Ticks;
        //clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
        //bbt = new clsMTime.clsBBT(bbt.Bar + 1, 0, 0);
        //P.F.MaxTicks = bbt.Ticks;
        //clsMTime.clsBBT bbt = new clsMTime.clsBBT(P.F._MaxMidiTicks);
        P.F.MaxBBT = new clsMaxBBT(maxmiditicks);
      }
    }

    internal void CreateData(clsTrks.Array<List<clsOO>> oo, int maxmiditicks) {
      //* StreamLL, OO -> OnOff -> NoteMap
      CreateStrmBeats();
      Strm = StrmLL.GetArray();
      MidiCtlrs = new clsMidiCtlrs(Strm);
      SetMaxTicks(maxmiditicks);
      //for (int i = 0; i < Strm.Length; i++) Strm[i].QTime = GetQTime(Strm[i].Ticks);
      //CreateQIStrm();
      foreach (clsTrks.T trk in oo.Next) {
        int errcount = clsOO.GetCorrOOList(oo[trk]);
        if (errcount > 0) Debug.WriteLine("GetCorrOO errcount for trk " + trk.ToString() + " = " + errcount);
      }
      clsOnOff onoff = CreateOnOff(oo, TrkSelect, true);
      NoteMap = new clsNoteMapMidi(onoff, TrkMinPitch, TrkMaxPitch, this);
    }

    private void CreateStrmBeats() {
      for (clsMTime.clsBBT bbt = new clsMTime.clsBBT(0); bbt.Ticks <= StrmLL.GetLastNode().Value.Ticks; bbt.NextBeat()) {
        StrmLL.InsertBeatEv(bbt.Copy());
      } 
    }

    internal void RefreshStrmBeats() {
      //* recreate strm for new tsigs
      StrmLL.RemoveStrmBeats();
      CreateStrmBeats();
      Strm = StrmLL.GetArray();
    }

    internal void CreateTrkTypes() {
      foreach (clsTrks.T trk in TrkType.Next) {
        float chordneg, poly;
        TrkType[trk] = CreateTrkType(trk, out chordneg, out poly);
        ChordNeg[trk] = chordneg;
        Poly[trk] = poly;
        //if (P.F.frmTrackMap != null && P.F.frmTrackMap.lblTrkTypes[trk] != null) {
        //  P.F.frmTrackMap.lblTrkTypes[trk].Text = TrkType[trk].ToString();
        //}
      }
    }

    private eTrkType CreateTrkType(clsTrks.T trk, out float chordnotesmissing, out float poly) {
      poly = -1;
      chordnotesmissing = -1;
      if (OnCount[trk] == 0) return eTrkType.Empty;
      if (P.F.Chan[trk] == 9) return eTrkType.Percussion;

      ////* check if sparse
      //int totticks = 0;
      //int ontime = 0;
      //foreach (clsOO oo in OO[trk]) {
      //  if (oo.On) ontime = oo.Ticks; else totticks += oo.Ticks - ontime;
      //}
      //int fillpercent = (totticks * 100) / P.F.MaxTicks;
      //if (fillpercent <= Cfg.TrkType_SparsePercent) return eTrkType.Sparse;

      //* check if sparse
      {
        int totqi = 0;
        //int lenqi = NoteMap.GetLengthQTime();
        int lenqi = P.F.MaxBBT.QI;
        for (int q = 0; q < lenqi; q++) {
          if (NoteMap.Filled(trk, q)) totqi++;
        }
        int fillpercent = (totqi * 100) / lenqi;
        if (fillpercent <= Cfg.TrkType_SparsePercent) return eTrkType.Sparse;
      }

      //* check if chords by polyphony
      {
        int totnoteqi = 0;
        int totqi = 0;
        for (int q = 0; q < P.F.MaxBBT.QI; q++) {
          int notecnt = NoteMap.NoteCount(q, trk);
          if (notecnt > 0) {
            totnoteqi += notecnt;
            totqi++;
          }
        }
        if (totqi == 0) return eTrkType.Empty;
        poly = (float)totnoteqi / totqi;
        if (poly > Cfg.TrkType_PolyChord) return eTrkType.BChords;
      }

      //* check if bass
      {
        if (TrkMaxPitch[trk] <= Cfg.TrkType_MaxPitchBass) return eTrkType.Bass;
        if (P.F.Chan[trk] < 0) return eTrkType.Bass;
        bool multiple;
        int patch = MidiCtlrs.GetMainValue(clsMidiCtlrs.eCtlr.Patch, P.F.Chan[trk], out multiple);
        if (patch <= 127 && patch >= 0 && BassPatches[patch]) {
          if (TrkMaxPitch[trk] <= Cfg.TrkType_MaxPitchBassPatched) return eTrkType.Bass;
        }
      }

      //* check if chords by comparing with chord file (if present)
      {
        if (P.F.CF != null && P.F.CF.Evs.Count > 0 && !P.F.CF.NoChords) {
          chordnotesmissing = P.F.CF.GetChordNotesMissing(this, trk);
          if (chordnotesmissing < Cfg.TrkType_ChordNotes) return eTrkType.AChords;
        }
      }
      return eTrkType.Melody;
    }

    internal void SetMuteOn(params eTrkType[] trktypes) {
      if (P.F.frmTrackMap == null) return;
      foreach (clsTrks.T trk in TrkType.Next) {
        if (P.F.frmTrackMap.chkMutes[trk] == null) continue;
        bool mute = false;
        foreach (eTrkType trktype in trktypes) {  //set mute on if any of the trktypes match
          if (TrkType[trk] == trktype) mute = true;
        }
        P.F.frmTrackMap.chkMutes[trk].Checked = mute;
      }
    }

    //internal bool SetPatchAndChan(eTrkType trktype) {
    //  //* find trk with most ON evs
    //  int trkmaxon = -1;
    //  int maxoncnt = -1;
    //  foreach (clsTrks.T trk in TrkType.Next) {
    //    if (TrkType[trk] == trktype && OnCount[trk] > maxoncnt) {
    //      trkmaxon = trk.TrkNum;
    //      maxoncnt = OnCount[trk];
    //    }
    //  }

    //  if (trkmaxon >= 0) return SetPatchAndChan(new clsTrks.T(P.F.Trks, trkmaxon));  //at least one trk found
    //  else return false;
    //}

    //internal bool SetPatchAndChan(clsTrks.Array<bool> trkselect) {
    //  //* find trk with most ON evs
    //  int trkmaxon = -1;
    //  int maxoncnt = -1;
    //  foreach (clsTrks.T trk in trkselect.Next) {
    //    if (trkselect[trk] && OnCount[trk] > maxoncnt) {
    //      trkmaxon = trk.TrkNum;
    //      maxoncnt = OnCount[trk];
    //    }
    //  }

    //  if (trkmaxon >= 0) return SetPatchAndChan(new clsTrks.T(P.F.Trks, trkmaxon));  //at least one trk found
    //  else return false;
    //}

    //internal bool SetPatchAndChan(clsTrks.T trk) {
    //  bool multiple;
    //  int patch = MidiCtlrs.GetMainValue(clsMidiCtlrs.eCtlr.Patch, P.F.Chan[trk], out multiple);
    //  if (patch >= 0 && !multiple && P.frmSC != null) {
    //    P.frmSC.cmbKBChanPatch.SelectedIndex = patch + 1;
    //    if (P.F.Chan[trk] >= 0) P.frmSC.nudKBChanOut.Value = P.F.Chan[trk] + 1;

    //    //P.frmSC.cmbRiffChanPatch.SelectedIndex = patch + 1;
    //    //if (P.F.Chan[trk] >= 0) P.frmSC.nudRiffChanOut.Value = P.F.Chan[trk] + 1;
    //    return true;
    //  }
    //  return false;
    //}

    internal static clsOnOff CreateOnOff1(clsTrks.Array<List<clsOO>> ooarray, bool sepnotes) {
      clsOnOff onoff = new clsOnOff(P.F.MaxBBT.QI + 2);
      CreateOnOffTrk(ooarray, sepnotes, onoff, new clsTrks.T(P.F.CFTrks, 0));
      return onoff;
    }

    internal static clsOnOff CreateOnOff(clsTrks.Array<List<clsOO>> ooarray, clsTrks.Array<bool> trkselect, bool sepnotes) {
      //clsOnOff onoff = new clsOnOff(P.F.MaxQTime + 2, numtrks);
      clsOnOff onoff = new clsOnOff(P.F.MaxBBT.QI);
      foreach (clsTrks.T trk in trkselect.Next) {
        if (!trkselect[trk]) continue;
        CreateOnOffTrk(ooarray, sepnotes, onoff, trk);
      }
      return onoff;
    }

    private static void CreateOnOffTrk(clsTrks.Array<List<clsOO>> ooarray, bool sepnotes, clsOnOff onoff, clsTrks.T trk) {
      int[,] lastonqtime = new int[16, 128];  //[chan, pitch]
      int[,] lastoffqtime = new int[16, 128];  //[chan, pitch]
      foreach (clsOO oo in ooarray[trk]) {
        //if (oo.Chan == 9) continue;  //excl. chan10
        if (oo.On) {  //ON event (vel > 0)
          lastonqtime[oo.Chan, oo.Pitch] = oo.QTime;
          //* lastofftime too close to thisontime to show up in notemap: switchon later
          if (sepnotes && lastoffqtime[oo.Chan, oo.Pitch] == oo.QTime - 1) {
            onoff[oo.QTime + 1, oo.Pitch, trk, false]++;
          } else if (sepnotes && lastoffqtime[oo.Chan, oo.Pitch] == oo.QTime) {
            onoff[oo.QTime + 2, oo.Pitch, trk, false]++;
          } else {
            onoff[oo.QTime, oo.Pitch, trk, false]++;
          }
          lastoffqtime[oo.Chan, oo.Pitch] = 0;
        } else {  //OFF
          lastoffqtime[oo.Chan, oo.Pitch] = oo.QTime;
          //* lastontime == thisofftime: delay thisofftime by one qi
          if (sepnotes && lastonqtime[oo.Chan, oo.Pitch] == oo.QTime) {
            onoff[oo.QTime + 1, oo.Pitch, trk, false]--;
          } else {
            onoff[oo.QTime, oo.Pitch, trk, false]--;
          }
          lastonqtime[oo.Chan, oo.Pitch] = 0;
        }
      }
    }

    internal static byte[] ConvUIntF(uint ui, int len) {
      //convert uint to 1-4 byte array
      if (len > 4 || len < 1) {
        throw new FatalException();
      }
      byte[] xbytes = new byte[len];
      for (int i = len - 1; i >= 0; i--) {
        xbytes[i] = (byte)(ui & 0xff);
        ui >>= 8;
      }
      return xbytes;
    }

    internal class clsStrmLL {
      private LinkedList<clsEvStrm> LL = new LinkedList<clsEvStrm>();
      private LinkedListNode<clsEvStrm> CurrentLLN;
      private clsFileStream FS;

      internal clsStrmLL(clsFileStream fs) {
        //* from clsFileStream constructor
        FS = fs;
      }

      internal clsStrmLL(clsFileStream fs, clsEvStrm[] Strm) : this(fs) {
        //* recreate from Strm (called from UpdateFromStrm()) 
        if (Strm == null) return;
        foreach (clsEvStrm ev in Strm) LL.AddLast(ev);
        CurrentLLN = LL.First;
      }

      internal clsStrmLL(clsFileStream fs, List<clsEvShort> list) : this(fs) {
        LL = new LinkedList<clsEvStrm>(list);
        CurrentLLN = LL.First;
      }

      internal void ResetCurrentLLN() {
        if (CurrentLLN == null) return;
        CurrentLLN = LL.First;
      }

      internal void RemoveStrmBeats() {
        CurrentLLN = LL.First;
        while (CurrentLLN != null) {  //remove clsEvBeat nodes
          LinkedListNode<clsEvStrm> node = CurrentLLN;
          CurrentLLN = CurrentLLN.Next;
          if (node.Value is clsEvBeat) LL.Remove(node);
        }
      }

      internal void InsertShortEv(int seq, int ticks, int trk, byte status, byte msg) {
        //* insert short event (2 bytes) into stream somewhere after the current position
        clsEvShort ev = new clsEvShort(seq, ticks, trk, status, msg);
        InsertEv(ev);
      }

      internal void InsertShortEv(int seq, int ticks, int trk, byte status, byte msg, byte data) {
        //* insert short event (3 bytes) into stream somewhere after the current position
        clsEvShort ev = new clsEvShort(seq, ticks, trk, status, msg, data);
        InsertEv(ev);
      }

      internal void InsertTempoEv(int seq, int ticks, int tempo) {
        clsEvTempo ev = new clsEvTempo(seq, ticks, tempo);
        InsertEv(ev);
      }

      internal void InsertSystemEv(int seq, int ticks, int trk, byte status, byte[] data) {
        clsEvSystem ev = new clsEvSystem(seq, ticks, trk, status, data);
        InsertEv(ev);
      }

      internal void InsertMetaEv(int seq, int ticks, int trk, byte type, byte[] data) {
        clsEvMeta ev = new clsEvMeta(seq, ticks, trk, type, data);
        InsertEv(ev);
      }

      internal void InsertTitleEv(int seq, int ticks, int trk, byte[] data) {
        clsEvTitle ev = new clsEvTitle(seq, ticks, trk, data);
        InsertEv(ev);
      }

      internal void InsertMetaEv(int seq, int ticks, int trk, byte type, int len, int intdata) {
        clsEvMeta ev = new clsEvMeta(seq, ticks, trk, type, len, intdata);
        InsertEv(ev);
      }

      internal void InsertBeatEv(clsMTime.clsBBT bbt) {
        clsEvBeat ev = new clsEvBeat(bbt);
        InsertEv(ev);
      }

      internal void InsertEv(clsEvStrm ev) {
        if (LL.Count == 0) {
          LL.AddLast(ev);
          this.CurrentLLN = LL.Last;
          return;
        }
        if (CurrentLLN == null) CurrentLLN = LL.Last;

        if (CurrentLLN.Value.GT(ev)) {  //comp ticks and track
          while (CurrentLLN != null && CurrentLLN.Value.GT(ev)) CurrentLLN = CurrentLLN.Previous;
          if (CurrentLLN == null) {  //at the start or empty linkedlist
            LL.AddFirst(ev);
            CurrentLLN = LL.First;
          } else {
            LL.AddAfter(CurrentLLN, ev);
            CurrentLLN = CurrentLLN.Next;  //new
          }
        } else if (CurrentLLN.Value.LT(ev)) {  //comp ticks and track
          while (CurrentLLN != null && CurrentLLN.Value.LT(ev)) CurrentLLN = CurrentLLN.Next;
          if (CurrentLLN == null) {  //at the end or empty linkedlist
            LL.AddLast(ev);
            CurrentLLN = LL.Last;
          } else {
            LL.AddBefore(CurrentLLN, ev);
            CurrentLLN = CurrentLLN.Previous;  //new
          }
        } else {  //ticks and trk equal
          LL.AddAfter(CurrentLLN, ev);
          CurrentLLN = CurrentLLN.Next;  //new
        }

#if DebugTicksAndTrks
        LinkedListNode<clsEvStrm> lln = LL.First; 
        while (lln.Next != null) {
          if (lln.Next.Value.Ticks < lln.Value.Ticks) Debugger.Break();
          if (lln.Next.Value.Ticks == lln.Value.Ticks) {
            if (lln.Next.Value.Trk.TrkNum < lln.Value.Trk.TrkNum) Debugger.Break();
          }
          lln = lln.Next;
        }
#endif
      }

      internal clsEvStrm[] GetArray() {
        ResetCurrentLLN();

        //* convert StreamLL to Stream (array)
#if DebugTicks
        //* check ticks are in ascending order
        clsEvStrm prevev = null;
        foreach (clsEvStrm ev in StrmLL) {
          if (prevev != null) {
            if (prevev.Ticks > ev.Ticks) throw new LogicException();
          }
          prevev = ev;
        }
#endif
#if DebugStream
        //* print dump of Strm
        string filename = Cfg.DebugPath + @"\Strm.debug.txt";
        Stream xstream = new FileStream(filename, FileMode.Create, FileAccess.Write);  //overwrite
        StreamWriter xsw = new StreamWriter(xstream);
        //string fmt = "{0,6} {1,6} {2:X2} {3:X2} {4:X2}";
        xsw.WriteLine(new clsEvShort(0, 0, 0, 0).DebugFormat, "Index", "Ticks", "B0", "B1", "B2");
        for (int i = 0; i < Strm.Length; i++) {
          clsEvStrm ev = Strm[i];
          xsw.WriteLine(ev.DebugFormat, ev.GetDebugParams(i));
          //xsw.WriteLine(fmt, i, ev.Ticks, ev.Status, ev.Msg, ev.Data);
        }
        xsw.Close();
#endif
        return LL.ToArray();
      }

      internal LinkedListNode<clsEvStrm> GetLastNode() {
        return LL.Last;
      }
    }

    internal class clsOO : IComparable<clsOO> {
      internal int Ticks;
      internal bool On;
      internal clsOO CorrOO;
      internal int Chan;
      internal int Pitch;
      internal int Vel;
      internal int QTime;

      internal clsOO(int ticks, bool on, int chan, int pitch, int vel, int ticksperqi) {
        Ticks = ticks;
        On = on;
        Chan = chan;
        Pitch = pitch;
        Vel = vel;
        //QTime = Ticks / ticksperqi;
        int rem;
        QTime = Math.DivRem(Ticks, ticksperqi, out rem);
        if (rem >= ticksperqi / 2) QTime++;   //round to nearest integer
      }

      private clsOO(int ticks) {  //for binarysearch
        Ticks = ticks;
      }

      public int CompareTo(clsOO oo) {  //use Ticks as sorted key for BinarySearch
        if (Ticks < oo.Ticks) return -1;
        if (Ticks > oo.Ticks) return 1;
        return 0;   //equals 
      }

      internal static int Search(int ticks, List<clsOO> oolist) {
        //* return index of first ev on or after ticks
        int res = oolist.BinarySearch(new clsOO(ticks));
        if (res < 0) res = ~res;
        return res;  //may equal Count
      }

      internal static int GetCorrOOList(List<clsOO> oolist) {
        //* create/update all CorrOO in oolist
        int errcount = 0;
        for (int i = 0; i < oolist.Count; i++) {
          if (oolist[i].On) {  //look for OFF OO
            for (int j = i + 1; j < oolist.Count; j++) {
              if (GetCorrOO(ref errcount, oolist[i], oolist[j])) break;
            }
          } else {
            for (int j = i - 1; j >= 0; j--) {
              if (GetCorrOO(ref errcount, oolist[i], oolist[j])) break;
            }
          }
        }
        return errcount;
      }

      private static bool GetCorrOO(ref int errcount, clsOO ooi, clsOO ooj) {
        //* return true if corresponding OO found, or error encountered
        if (ooi.Pitch == ooj.Pitch) {
          if (ooi.On == ooj.On) errcount++;
          else ooi.CorrOO = ooj;  //corresponding OO found
          return true;
        }
        return false;
      }
    }

    internal abstract class clsEvStrm : IComparable<clsEvStrm> {
      //internal string DebugFormat;
      internal int Ticks;  //absolute
      //internal int PITime;
      internal int QTime;
      internal int Seq;  //for sorting
      internal clsTrks.T Trk;
      //internal int Seq = 0;  //may be used during sort of evs with equal ticks 
      internal bool Selected = false;  //frmTrackMap
      protected abstract int SortOrder { get; }  //0 sorts first

      internal clsEvStrm(int seq, int ticks) {
        Ticks = ticks;
        //PITime = GetPITime(ticks);
        QTime = GetQTime(ticks);
        Seq = seq;
      }

      //internal abstract int GetMessage();
      //internal abstract object[] GetDebugParams(int i);
      internal abstract void WriteMsg(clsSaveMidiFile savemidifile, int delta);
      internal virtual clsEvStrm Transpose() { return this; }  //no action except for evshort
      internal virtual bool indWrite { get { return true; } }

      //internal static int GetPITime(int time) {  //copied from clsCSVFile
      //  int qtime = time / P.F.TicksPerPI;
      //  if ((time % P.F.TicksPerPI) > P.F.HalfTicksPerPI) qtime++;  //quantize to nearest QI  
      //  return qtime;
      //}

      internal static int GetQTime(int time) {  //copied from clsCSVFile
        int qtime = time / P.F.TicksPerQI;
        if ((time % P.F.TicksPerQI) > P.F.HalfTicksPerQI) qtime++;  //quantize to nearest QI  
        return qtime;
      }

      public virtual int CompareTo(clsEvStrm ev) {  //use Ticks as sorted key for BinarySearch
        if (Ticks < ev.Ticks) return -1;
        if (Ticks > ev.Ticks) return 1;

        if (Trk == null && ev.Trk != null) return -1;
        if (Trk != null && ev.Trk == null) return 1;

        if (Trk != null && ev.Trk != null) {
          if (Trk.TrkNum < ev.Trk.TrkNum) return -1;
          if (Trk.TrkNum > ev.Trk.TrkNum) return 1;
        }

        if (Seq < ev.Seq) return -1;
        if (Seq > ev.Seq) return 1;

        if (SortOrder < ev.SortOrder) return -1;
        if (SortOrder > ev.SortOrder) return 1;

        return 0;   //equals 
      }

      public bool GT(clsEvStrm ev) {
        return (CompareTo(ev) == 1);
      }

      public bool LT(clsEvStrm ev) {
        return (CompareTo(ev) == -1);
      }
    }

    internal class clsEvShort : clsEvStrm {
      internal byte Status;
      internal byte Msg;
      internal byte Data;

      static clsEvShort() { }

      internal clsEvShort(int seq, int ticks, int trk, byte status, byte msg) : base(seq, ticks) {
        //DebugFormat = "{0,6} {1,6} {2:X2} {3:X2} {4:X2}";
        Trk = (trk < 0) ? null : new clsTrks.T(P.F.Trks, trk);
        Status = status;
        Msg = msg;
      }

      internal clsEvShort(int seq, int ticks, int trk, byte status, byte msg, byte data) :
        this(seq, ticks, trk, status, msg) {
        Data = data;
      }

      protected override int SortOrder { get { return 3; } }

      public override string ToString() {
        return string.Format("{0:X2} {1:X2} {2:X2}", Status, Msg, Data);
      }

      internal override clsEvStrm Transpose() {
        if (P.frmSC.nudTransposeStreamPitch.Value == 0) return this;
        int m = Msg;
        int s = Status & 0xf0;
        if (s == 0x80 || s == 0x90) {
          if ((Status & 0x0f) != 9) {  //not chan'10' percussion
            int transpose = 0;
            if (P.frmSC != null) transpose = (int)P.frmSC.nudTransposeStreamPitch.Value;
            m += transpose;
            m = (byte)Math.Min(127, Math.Max(0, m));
          }
          return new clsEvShort(Seq, Ticks, Trk.TrkNum, Status, (byte)m, Data);
        }
        return this;
      }

      //internal int GetMessage() {
      //  clsEvShort ev = Transpose();
      //  return (((Data << 8) | ev.Msg) << 8) | Status;
      //}

      internal override void WriteMsg(clsSaveMidiFile savemidifile, int delta) {
        clsSaveMidiFile.clsMWriter mw = savemidifile.MWriterTrk;
        mw.WriteIntV(delta);
        savemidifile.CheckStatus(Status);
        mw.WriteByte((byte)Msg);
        int statusprefix = Status & 0xf0;
        if (statusprefix != 0xc0 && statusprefix != 0xd0) mw.WriteByte((byte)Data);  //2 bytes of data
      }

      internal bool IsOnEv() {
        return ((Status & 0xf0) == 0x90 && Data > 0);
      }

      internal bool IsOffEv() {
        return (((Status & 0xf0) == 0x80) || ((Status & 0xf0) == 0x90 && Data == 0));
      }

      internal clsEvShort Copy() {
        return new clsEvShort(Seq, Ticks, Trk.TrkNum, Status, Msg, Data);
      }

      //internal override object[] GetDebugParams(int i) {
      //  object[] ret = new object[5];
      //  ret[0] = i;
      //  ret[1] = Ticks;
      //  ret[2] = Status;
      //  ret[3] = Msg;
      //  ret[4] = Data;
      //  return ret;
      //}
    }

    internal class clsEvSystem : clsEvStrm {
      internal byte Status;  //status = xf0, xf7
      internal byte[] Data;  //incl. len
      protected override int SortOrder { get { return 2; } }

      internal clsEvSystem(int seq, int ticks, int trk, byte status, byte[] data)
        : base(seq, ticks) {
        Trk = (trk < 0) ? null : new clsTrks.T(P.F.Trks, trk);
        Status = status;
        Data = data;
      }

      internal override void WriteMsg(clsSaveMidiFile savemidifile, int delta) {
        clsSaveMidiFile.clsMWriter mw = savemidifile.MWriterTrk;
        mw.WriteIntV(delta);
        savemidifile.SystemStatus(Status, Data.Length);
        mw.WriteBytes(Data);  //excl. len
      }
    }

    internal class clsEvMeta : clsEvStrm {
      internal byte Type;
      internal byte[] Data;  //incl. len
      protected override int SortOrder { get { return 1; } }  

      internal clsEvMeta(int seq, int ticks, int trk, byte type, byte[] data)
        : base(seq, ticks) {
        Trk = (trk < 0) ? null : new clsTrks.T(P.F.Trks, trk);
        Type = type;
        Data = data;  //excl. len
      }

      internal clsEvMeta(int seq, int ticks, int trk, byte type, int len, int intdata)
        : base(seq, ticks) {
        //* keysig, tsig, tempo etc.
        Trk = (trk < 0) ? null : new clsTrks.T(P.F.Trks, trk);
        Type = type;
        Data = clsFileStream.ConvUIntF((uint)intdata, len);  //excl. len
      }

      public override int CompareTo(clsEvStrm ev) { 
        int basecomp = base.CompareTo(ev);
        if (basecomp != 0) return basecomp;  //not equal
        if (!(ev is clsEvMeta)) return basecomp;
        clsEvMeta evmeta = (clsEvMeta)ev;
        return Type.CompareTo(evmeta.Type);
      }

      internal override bool indWrite {
        get {
          if (Type == 0x58 || Type == 0x59) return false;  //tsig, keysig (tempo overidden elsewhere) 
          return true;
        }
      }

      internal override clsEvStrm Transpose() {
        if (P.frmSC.nudTransposeStreamPitch.Value == 0) return this;
        if (Type == 0x59) {  //keysig (Data[] = sf, mi)
          //* not tested...
          string scale = (Data[1] == 0) ? "major" : "minor";
          int intmidikey = (int)(sbyte)Data[0];
          //int midikey = (Data[0] > 7) ? Data[0] - 256 : Data[0];
          clsKeyTicks key = new clsKeyTicks(intmidikey, scale, 0);
          key = key.GetTranspose((int)P.frmSC.nudTransposeStreamPitch.Value);
          byte bytemidikey = (byte)(sbyte)key.MidiKey;
          //intmidikey = (key.MidiKey < 0) ? key.MidiKey + 256 : key.MidiKey;
          return new clsEvMeta(Seq, Ticks, Trk.TrkNum, Type, new byte[] { bytemidikey, Data[1] }); 
        }
        return this;
      }

      internal override void WriteMsg(clsSaveMidiFile savemidifile, int delta) {
        clsSaveMidiFile.clsMWriter mw = savemidifile.MWriterTrk;
        mw.WriteIntV(delta);
        savemidifile.MetaStatus(Type, (byte)Data.Length);
        mw.WriteBytes(Data);  //excl. len
      }
    }

    internal class clsEvTempo : clsEvMeta {
      internal int MidiTempo;  //microsecs per qnote

      internal clsEvTempo(int seq, int ticks, int miditempo)
        : base(seq, ticks, -1, 0x51, 3, miditempo) {
        //DebugFormat = "{0,6} {1,6} Tempo: {2,6}";
        MidiTempo = miditempo;
      }

      internal int GetMessage() {
        //* tempo should be 3 bytes (MSB of int = 0)
        return MidiTempo | (MMConstants.MEVT_TEMPO << 24);
      }

      internal override bool indWrite { get { return false; } }
    }

    internal class clsEvBeat : clsEvStrm {
      internal clsMTime.clsBBT BBT;
      protected override int SortOrder { get { return 0; } }

      internal clsEvBeat(clsMTime.clsBBT bbt) : base(0, bbt.Ticks) {
        BBT = bbt;
        //Trk = new clsTrks.T(P.F.Trks, -1);
      }

      public override string ToString() {
        return string.Format("BBT: " + BBT.ToString());
      }

      internal override void WriteMsg(clsSaveMidiFile savemidifile, int delta) {
        throw new NotImplementedException();  //should not happen!
      }

      internal override bool indWrite { get { return false; } }
    }

    internal class clsEvTitle : clsEvMeta {
      internal clsEvTitle(int seq, int ticks, int trk, byte[] data) :
        base(seq, ticks, trk, 0x03, data) {
      }

      internal override void WriteMsg(clsSaveMidiFile savemidifile, int delta) {
        if (Trk != null) return;  //only for song title
        WriteTitle(savemidifile, delta);
      }

      internal void WriteTitle(clsSaveMidiFile savemidifile, int delta) {
        clsSaveMidiFile.clsMWriter mw = savemidifile.MWriterTrk;
        mw.WriteIntV(delta);
        savemidifile.MetaStatus(Type, (byte)Data.Length);
        mw.WriteBytes(Data);  //excl. len
      }

      //internal override object[] GetDebugParams(int i) {
      //  object[] ret = new object[3];
      //  ret[0] = i;
      //  ret[1] = Ticks;
      //  ret[2] = MidiTempo;
      //  return ret;
      //}
    }


    internal class clsController {
      internal byte Status;  //0xB<chan>
      internal byte Msg;     //controller number
      internal byte Data;    //value
      internal clsController(byte status, byte msg, byte data) {
        Status = status;
        Msg = msg;
        Data = data;
      }
    }

    //internal class clsMap<T> {
    //  private SortedList<int, T> Map = new SortedList<int, T>();
    //  private int Index = 0;
    //  private T Dflt;

    //  internal clsMap(T dflt) {
    //    Dflt = dflt;
    //  }

    //  internal void Add(int ticks, T val) {
    //    if (val == null) {
    //      LogicError.Throw(eLogicError.X052);
    //      return;
    //    }
    //    if (Map.ContainsKey(ticks)) return;  //use first key only
    //    Map.Add(ticks, val);
    //  }

    //  internal int Count { get { return Map.Count; } }

    //  internal T ValByIndex(int index) {
    //    return Map.Values[index];
    //  }

    //  internal int KeyByIndex(int index) {
    //    return Map.Keys[index];
    //  }

    //  internal T this[int ticks] {
    //    get {
    //      if (Map.Count == 0) return Dflt;
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
  }
}
