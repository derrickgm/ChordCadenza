#undef DebugCallbackInSync
#undef DebugCallbackInKB
#undef DebugCallbackOutStream
#undef DebugMidiOut
#define DebugRunningStatus

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace ChordCadenza {
  internal abstract class clsMDevs {
    protected clsMidiDevs MidiDevs;
    //internal clsBASSOutDevs BASSOutDevs;
    internal string[] Devs;
    internal abstract string[] GetDevs();
  }

  internal class clsMDevsIn : clsMDevs {
    internal clsMDevsIn() {
      MidiDevs = new clsMidiInDevs();
      Devs = GetDevs();
    }

    internal override string[] GetDevs() {
      return MidiDevs._GetDevs();
    }
  }

  internal class clsMDevsOut : clsMDevs {
    internal clsMDevsOut() {
      MidiDevs = new clsMidiOutDevs();
      //BASSOutDevs = new clsBASSOutDevs();
      Devs = GetDevs();
    }

    internal override string[] GetDevs() {
      //if (BASSOutDevs == null) return MidiDevs._GetDevs();
      if (MidiDevs == null) return clsBASSOutDev.GetBASSDevs();
      List<string> list = MidiDevs._GetDevs().ToList();
      list.AddRange(clsBASSOutDev.GetBASSDevs()); 
      return list.ToArray();
    }
  }

  internal abstract class clsMidiDevs {
    protected delegate int delegGetNumDevs();
    protected delegate int delegGetDevCaps (int uDeviceID, ref MIDIINCAPS lpMidiInCaps, int cbMidiInCaps);

    protected delegGetNumDevs GetNumDevs;
    protected delegGetDevCaps GetDevCaps;

    protected struct MIDIINCAPS {
      internal short wMid;                       //WORD
      internal short wPid;                       //WORD
      internal int vDriverVersion;             //MMVERSION
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      internal string szPname;                    //CHAR[MAXPNAMELEN] =[32] (incl. null)
      internal int dwSupport;                  //DWORD
    }

    private void Dummy() {
      //* never called - only here to stop compiler warnings!!!
      MIDIINCAPS m = new MIDIINCAPS();
      m.wMid = 0;
      m.wPid = 0;
      m.vDriverVersion = 0;
      m.szPname = "";
      m.dwSupport = 0;
    }

    internal string[] Devs;

    internal clsMidiDevs() {}

    internal string[] _GetDevs() {
      string[] devs = new string[GetNumDevs()];
      MIDIINCAPS midiincaps = new MIDIINCAPS();
      for (int d = 0; d < devs.Length; d++) {
        int ret = GetDevCaps(d, ref midiincaps, Marshal.SizeOf(midiincaps));
        if (ret != 0) throw new MidiIOException(ret, d.ToString());
        devs[d] = midiincaps.szPname;
      }
      return devs;
    }
  }

  internal class clsMidiInDevs : clsMidiDevs {
    [DllImport("winmm.dll")] private static extern int midiInGetNumDevs();
    [DllImport("winmm.dll")] private static extern int midiInGetDevCaps(int uDeviceID, ref MIDIINCAPS lpMidiInCaps, int cbMidiInCaps);

    internal clsMidiInDevs() {
      GetNumDevs = new delegGetNumDevs(midiInGetNumDevs);
      GetDevCaps = new delegGetDevCaps(midiInGetDevCaps);
      Devs = _GetDevs();
    }
  }

  internal class clsMidiOutDevs : clsMidiDevs {
    [DllImport("winmm.dll")] private static extern int midiOutGetNumDevs();
    [DllImport("winmm.dll")] private static extern int midiOutGetDevCaps(int uDeviceID, ref MIDIINCAPS lpMidiInCaps, int cbMidiOutCaps);

    internal clsMidiOutDevs() {
      GetNumDevs = new delegGetNumDevs(midiOutGetNumDevs);
      GetDevCaps = new delegGetDevCaps(midiOutGetDevCaps);
      Devs = _GetDevs();
    }
  }

  //***********************************************************************************************

  internal abstract class clsBassMidiInOut {
    internal static string MidiDevNameOutKB = "";
    internal static string MidiDevNameOutStream = "";
    internal static string MidiDevNameInKB = "";
    internal static string MidiDevNameInSync = "";
    public abstract bool Opened();
    //internal abstract string MidiDevName { get; set; }
    internal abstract string MidiDevName { get; }
    public abstract void Close();
    internal enum eType { OutKB, OutStream, OutKBStream, InKB, InSync };
    internal eType Type;

    internal int FXTypeSeq {
      get {
        return (Type == eType.OutStream) ? 1 : 0;  //0: OutKB or OutKBStream
      }
    } 

    internal bool IsOutKB {
      get {
        return (Type == eType.OutKB || Type == eType.OutKBStream);
      }
    }

    internal bool IsOutStream {
      get {
        return (Type == eType.OutStream || Type == eType.OutKBStream);
      }
    }
  }

  internal abstract class clsMidiInOut : clsBassMidiInOut {
    protected delegate void delegCallback(IntPtr hMidiIn, int wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2);
    //protected delegate int delegOpen(out IntPtr hMidiInOut, int uDeviceID, delegCallback dwCallback, int dwInstance, int fdwOpen);
    //protected delegate int delegClose(IntPtr hMidiInOut);

    //protected delegClose CloseDev;
    protected delegCallback dCallback;
    protected virtual void Callback(IntPtr handle, int wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2) { }
    protected abstract int CallBackFlags { get; }
    public override abstract void Close();
    internal IntPtr Handle;

    internal override string MidiDevName {
      get {
        return GetMidiDevName(Type);
      }
      //set {
      //  SetMidiDevName(Type, value);
      //}
    }

    internal static string GetMidiDevName(clsMidiInOut.eType type) {
      switch (type) {
        case eType.InKB:
          return MidiDevNameInKB;
        case eType.InSync:
          return MidiDevNameInSync;
        case eType.OutKB:
          return MidiDevNameOutKB;
        case eType.OutKBStream:
          return MidiDevNameOutKB;  //should be the same as MidiDevNameOutStream
        case eType.OutStream:
          return MidiDevNameOutStream;
        default:
          LogicError.Throw(eLogicError.X129);
          return "";
      }
    }

    //internal static void SetMidiDevName(clsMidiInOut.eType type, string name) {
    //  switch (type) {
    //    case eType.InKB:
    //      MidiDevNameInKB = name;
    //      break;
    //    case eType.InSync:
    //      MidiDevNameInSync = name;
    //      break;
    //    case eType.OutKB:
    //      MidiDevNameOutKB = name;
    //      break;
    //    case eType.OutStream:
    //      MidiDevNameOutStream = name;
    //      break;
    //    case eType.OutKBStream:
    //      MidiDevNameOutKB = name;
    //      MidiDevNameOutStream = name;
    //      break;
    //    default:
    //      LogicError.Throw(eLogicError.X129);
    //      break;
    //  }
    //}

    protected void Init(string[] devs, string devname, eType type) {
      Type = type;
      int devnum = -1;
      //MidiDevName = devname;
      if (devname.Trim() == "" || devname.Trim().ToLower() == "none") return;
      if (devname == "***") {  //default
        devnum = GetDefaultDevName(devs, type);
      } else {
        for (int i = 0; i < devs.Length; i++) {
          if (devname == devs[i]) {
            devnum = i;
            break;
          }
        }
      }
      if (devnum < 0 || devnum >= devs.Length) {
        devname = "***";
        devnum = GetDefaultDevName(devs, type);
      }
      Init(devnum);
    }

    private int GetDefaultDevName(string[] devs, eType type) {
      int devnum = 0;
      if (devs.Length > 0) {
        //MidiDevName = devs[0];
        switch (type) {
          case eType.InKB:
            Cfg.MidiInKB = MidiDevName;
            break;
          case eType.InSync:
            Cfg.MidiInSync = MidiDevName;
            break;
          case eType.OutKB:
            Cfg.MidiOutKB = MidiDevName;
            break;
          case eType.OutStream:
            Cfg.MidiOutStream = MidiDevName;
            break;
          case eType.OutKBStream:
            Cfg.MidiOutKB = MidiDevName;
            Cfg.MidiOutStream = MidiDevName;
            break;
        }
      }
      return devnum;
    }

    protected void CheckMidiAction(int rtn, string action) {
      string msg = action + " " + MidiDevName + " (" + Handle + ")";
      if (rtn != 0) throw new MidiIOException(rtn, msg);
      else Debug.WriteLine(msg);
    }

    public override bool Opened() {
      return Handle != IntPtr.Zero;
    }

    protected void Init(int devnum) {
      if (devnum < 0) return;
      if (CallBackFlags != 0 && dCallback == null) {
        dCallback = new delegCallback(Callback);
      }
      Handle = Open(devnum);
    }

    protected abstract IntPtr Open(int devnum);

    //public void Close() {
    //  if (Handle.HasValue) CloseDev(Handle.Value);
    //  //dCallbackPinHandle.Free();
    //}

    //public void Close() {
    //  if (Handle != IntPtr.Zero) CloseDevice();
    //}

    public void EndCallback() {
      //* does this work? does it do anything?

      if (dCallback != null) GC.KeepAlive(dCallback);
    }
  }

  //***********************************************************************************************

  internal class clsMidiIn : clsMidiInOut {
    protected static int MsgCount = 0;

    [DllImport("winmm.dll")] private static extern int midiInOpen
      (ref IntPtr hMidiIn, int uDeviceID, delegCallback dwCallback, int dwInstance, int fdwOpen);
    [DllImport("winmm.dll")] private static extern int midiInReset (IntPtr hMidiIn);
    [DllImport("winmm.dll")] private static extern int midiInClose(IntPtr hMidiIn);
    [DllImport("winmm.dll")] private static extern int midiInStart (IntPtr hMidiIn);

    protected override int CallBackFlags { get { return 0x30000; } }

    protected clsMidiIn(string[] devs, string devname, eType type) {
      //CloseDev = new delegClose(midiInClose);
      Init(devs, devname, type);
      if (Handle != IntPtr.Zero) {
        int rtn = midiInStart(Handle);
        if (rtn != 0) throw new MidiIOException(rtn, devname);
      }
    }

    public override void Close() {
      if (Handle == IntPtr.Zero) return;
      {
        int rtn = midiInReset(Handle);
        string msg = "clsMidiIn midiInReset " + MidiDevName;
        CheckMidiAction(rtn, "MidiInReset");
      }
      {
        int rtn = midiInClose(Handle);
        string msg = "clsMidiIn midiInClose " + MidiDevName;
        CheckMidiAction(rtn, "MidiInClose");
        if (rtn == 0) Handle = IntPtr.Zero;
      }
    }

    protected override IntPtr Open(int devnum) {
      //IntPtr h = IntPtr.Zero;
      if (Handle != IntPtr.Zero) return Handle;  //already open
      int rtn = midiInOpen(ref Handle, devnum, this.dCallback, 0, CallBackFlags);
      CheckMidiAction(rtn, "MidiInOpen");
      return Handle;
    }
  }

  //***********************************************************************************************

  internal class clsMidiInSync : clsMidiIn {
    internal clsMidiInSync(string[] devs, string devname) : base(devs, devname, eType.InSync) { }

    //internal clsMidiInSync(ComboBox cmb, bool none) : base(cmb, none) { }

    protected override void Callback(IntPtr hMidiIn, int wMsg, IntPtr Instance, IntPtr Param1, IntPtr Param2) {
      /*
      wMsg     = 963(MIM_DATA) 961(open) 962(close)
      dwParam1 = midi data (4 bytes: msg, data1, data2, 0) 
      dwParam2 = timestamp                      
      963: runs on thread with 'Highest' priority 
      961, 962: runs on FrmThread 
      */
      //OutMsg.Info("tmp", "MidiIn wMsg = {0}", wMsg);

      switch (wMsg) {
        case MMConstants.MIM_DATA:  //midi data - carry on
          break;
        case MMConstants.MIM_ERROR:
        case MMConstants.MIM_LONGERROR:
          new MidiIOWarning(wMsg, "clsMidiIn callback called with error code");
          return;
        case MMConstants.MIM_CLOSE:
        case MMConstants.MIM_OPEN:
          return;    //called from FrmThread
        case MMConstants.MIM_LONGDATA:
        case MMConstants.MIM_MOREDATA:
          throw new NotYetCodedException();  //not yet coded
      }

      int dwParam1 = Param1.ToInt32();
      //System.Diagnostics.Debug.WriteLine("midiin callback entered");
      if (dwParam1 == 254) return;   //active sensing
      //if (dwParam1 == 248) return;   //timing signal

      byte[] b = new byte[3];

      /*      
      System.Diagnostics.Debug.WriteLine("MidiInCallBack entered: "
        + hMidiIn
        + " " + wMsg 
        + " " + dwInstance
        + " " + dwParam1
        + " " + dwParam2);
      */

      b[0] = (byte)(dwParam1 & 0xff);  //msg
      dwParam1 >>= 8;
      b[1] = (byte)(dwParam1 & 0xff);  //pitch
      dwParam1 >>= 8;
      b[2] = (byte)(dwParam1 & 0xff);  //vel

#if DebugCallbackInSync
        string msg = String.Format("{0,5} {1:X2} {2:X2} {3:X2}", ++MsgCount, b[0], b[1], b[2]);
        Debug.WriteLine("MidiInCallBack entered: " + msg);
#endif

      int xmsgtype = b[0] & 0xf0;
      int xchan = b[0] & 0x0f;  //not always!
      //byte xkbvel;

      switch (b[0]) {
        case 0xf2:  //SPP
          if (!P.frmStart.chkMidiSPP.Checked) break;
          int ptr = b[1] + (b[2] << 7);
          MidiPlay.Sync.CallBack(clsSync.eMidiMsgType.SPP, ptr);
          Debug.WriteLine("Sync: SPP: " + ptr);
          break;
        case 0xfa:  //start
          if (!P.frmStart.chkMidiStartStop.Checked) break;
          MidiPlay.Sync.CallBack(clsSync.eMidiMsgType.Start);
          Debug.WriteLine("Sync: Start");
          break;
        case 0xfc:  //stop
          if (!P.frmStart.chkMidiStartStop.Checked) break;
          MidiPlay.Sync.CallBack(clsSync.eMidiMsgType.Stop);
          Debug.WriteLine("Sync: Stop");
          break;
        case 0xfb:  //continue
          if (!P.frmStart.chkMidiStartStop.Checked) break;
          MidiPlay.Sync.CallBack(clsSync.eMidiMsgType.Continue);
          Debug.WriteLine("Sync: Continue");
          break;
        case 0xf8:  //midiclock
          if (!P.frmStart.chkMidiStartStop.Checked) break;
          MidiPlay.Sync.CallBack(clsSync.eMidiMsgType.MidiClock);
          Debug.WriteLine("Sync: MidiClock");
          break;
      }
      return;
    }

  }

  //***********************************************************************************************

  internal class clsMidiInKB : clsMidiIn {
    private delegate void delegRefresh();

    internal object SwitchKeyLock = new object();  //manages sustain pedal/checkbox

    private delegate void delegSetChkSwitch(CheckBox chk, bool down);
    private delegSetChkSwitch dSetChkSwitch;

    private delegate void delegSetNoteName(int note);
    private delegSetNoteName dSetNoteName;

    private delegate void delegSCAlign(int note);
    private delegSCAlign dSCAlign;

    //internal bool Qwerty;
    internal static Bezier[] Beziers = new Bezier[Cfg.BezierName.Length];
    internal long? Ticks = null;
    private byte RunningStatus = 0x80;  //midi OFF
    private Stopwatch SW = new Stopwatch();
    private long? PrevEla = null;
    private long[] DoubleHit = new long[128];  //time of last on/off event

    static clsMidiInKB() {
      InitBeziers();
    }

    internal clsMidiInKB(string[] devs, string devname) : base(devs, devname, eType.InKB) {
      //InitBeziers();
      dSetChkSwitch = new ChordCadenza.clsMidiInKB.delegSetChkSwitch(SetChkSwitch);
      SW.Start();
    }

    private static void InitBeziers() {
      for (int i = 0; i < Cfg.BezierName.Length; i++) {  //for all bezier occurences
        PointF[] xpoints = new PointF[] {
          new PointF(0, 0),
          new PointF(Cfg.Bezier1X[i], Cfg.Bezier1Y[i]),
          new PointF(Cfg.Bezier2X[i], Cfg.Bezier2Y[i]),
          new PointF(127, 127)
        };
        Beziers[i] = new Bezier();
        Beziers[i].CalcVelocities(i);
      }
    }

    //internal void QwertyKeyUpDown(KeyEventArgs e, bool keydown) {
    //  if (!Qwerty) return;
    //  Ticks = clsPlay.GetTicks();
    //  byte[] b = clsQwerty.GetMidiIn(e, keydown);
    //  if (b.Length != 3) return;  //len 0: inactive key
    //  if (P.F.MidiFileLoaded) {
    //    P.frmSC.Play.InMidi(b);
    //  } else {
    //    MidiPlay.OutMKB.SendShortMsg(b[0], b[1], b[2]);
    //    lock (clsPlay.KBPitchList) {
    //      if (keydown) {
    //        clsPlay.KBPitchList.Add(b[1], b[1]);  //ON
    //        P.frmSC.BeginInvoke(new delegRefresh(P.frmSC.picBottom.Refresh));
    //        P.frmSC.BeginInvoke(new delegRefresh(P.frmSC.picChords.Refresh));
    //      } else {
    //        clsPlay.KBPitchList_MidiOff(b);   //OFF
    //      }
    //    }
    //  }
    //}

     protected override void Callback(IntPtr hMidiIn, int wMsg, IntPtr Instance, IntPtr Param1, IntPtr Param2) {
      /*
      wMsg     = 963(MIM_DATA) 961(open) 962(close)
      dwParam1 = midi data (4 bytes: msg, data1, data2, 0) 
      dwParam2 = timestamp                      
      963: runs on thread with 'Highest' priority 
      961, 962: runs on FrmThread 
      */
      //OutMsg.Info("tmp", "MidiIn wMsg = {0}", wMsg);
      switch (wMsg) {
        case MMConstants.MIM_DATA:  //midi data - carry on
          break;
        case MMConstants.MIM_ERROR:
        case MMConstants.MIM_LONGERROR:
          new MidiIOWarning(wMsg, "clsMidiIn: callback called with error code");
          return;
        case MMConstants.MIM_CLOSE:
        case MMConstants.MIM_OPEN:
          return;    //called from FrmThread
        case MMConstants.MIM_LONGDATA:
        case MMConstants.MIM_MOREDATA:
          throw new NotYetCodedException();  //not yet coded
      }

      int dwParam1 = Param1.ToInt32();
      //System.Diagnostics.Debug.WriteLine("midiin callback entered");
      if (dwParam1 == 254) return;   //active sensing
      //if (dwParam1 == 248) return;   //timing signal

      byte[] b = new byte[3];

      /*      
      System.Diagnostics.Debug.WriteLine("MidiInCallBack entered: "
        + hMidiIn
        + " " + wMsg 
        + " " + dwInstance
        + " " + dwParam1
        + " " + dwParam2);
      */

      b[0] = (byte)(dwParam1 & 0xff);  //msg
      bool indrunning = (b[0] < 0x80);
      if (b[0] < 0x80) b[0] = RunningStatus; else dwParam1 >>= 8;
      RunningStatus = b[0];
      b[1] = (byte)(dwParam1 & 0xff);  //pitch
      dwParam1 >>= 8;
      b[2] = (byte)(dwParam1 & 0xff);  //vel
#if DebugRunningStatus
      if (indrunning) {
        string errmsg = string.Format("Running Status = {0:X2} Bytes = {1:X2} {2:X2} {3:X2}",
          RunningStatus, b[0], b[1], b[2]);
        LogicError.Throw(eLogicError.X090, errmsg);
      }
#endif

#if DebugCallbackInKB
        string msg = String.Format("{0,5} {1:X2} {2:X2} {3:X2}", ++MsgCount, b[0], b[1], b[2]);
        Debug.WriteLine("MidiInCallBack entered: " + msg);
#endif
      //if (((b[0] & 0xf0) == 0xd0) || ((b[0] & 0xf0) == 0xa0)) Debug.WriteLine("Aftertouch: " + b[1]);  //channel pressure (aftertouch)

      if (MidiPlay.OutMRec == null) return;  //can't send any output

      int xmsgtype = b[0] & 0xf0;
      if (xmsgtype >= 0xf0) {  //system msg
        CallbackKeyboard(b);
      } else {  //channel msg
        int xchan = b[0] & 0x0f;
        xchan = MidiPlay.KBOutChanRec;
        if (xchan < 0) return;
        b[0] = (byte)((b[0] & 0xf0) | xchan);

        //* no ccmap conversion
        CallbackKeyboard(b);    //send (converted) ctlr or midi ev
      }
    }

    //private int GetChan(int chan) {
    //* filter out if not omni and channel does not match
    //  if (MidiPlay.KBInChan >= 0 && MidiPlay.KBInChan != chan) return -1;
    ////* check if channel needs changing
    //  if (MidiPlay.KBOutChan >= 0) return MidiPlay.KBOutChan; else return chan;
    //}

    private void CallbackKeyboard(byte[] b) {  //called for all messages from keyboard port
      //if ((b[0] & 0xf0) == 0xd0) Debug.WriteLine("Aftertouch: " + b[1]);  //channel pressure (aftertouch)
      if (b[1] > 127 || b[2] > 127) {
        //LogicError.Throw(eStopError.Y002);
        LogicError.Throw(eLogicError.X102, "bytes = " + b[0] + b[1] + b[2]);
        //* try correcting the error!
        if (b[2] < 128 && (b[0] & 0xf0) == 0x90 && (b[1] & 0xf0) == 0x90) {  //ON status sent twice (may be vel 0)
          b[1] = b[2];  //probably pitch
          List<clsPlay.clsKBPitch> listkb = clsPlay.KBPitchList.GetKBPitches(b[1]);
          if (listkb.Count > 0) {
            b[2] = 0;  //assume OFF event was meant 
          } else {
            b[2] = 80;  //assume ON event 
          }
        } 
        return;
      }

      Ticks = clsPlay.GetTicks();
      if (SustainPedal(b)) return;
      ApplyBezier(b);
      //if (P.F != null && P.frmSC != null && P.F.MidiFileLoaded) {

      bool? on = null;
      if ((b[0] & 0xf0) == 0x90 && b[2] > 0) on = true;
      else if (((b[0] & 0xf0) == 0x80) || ((b[0] & 0xf0) == 0x90) && b[2] == 0) on = false;

      //if (on.HasValue && CheckDoubleHit(b)) {
      //  Debug.WriteLine("DoubleHit detected at " + DateTime.Now);
      //  return;
      //}

      if (P.frmSCAlign != null && on.HasValue && on.Value) {  //ON
        dSCAlign = new delegSCAlign(SCAlign);
        P.frmSCAlign.BeginInvoke(dSCAlign, b[1]);
      }

      //if (P.frmSC.Play != null || clsPlay.ManChordsActive) {
      if (P.frmSC.Play != null) {
        if (!CheckSwitch(b)) P.frmSC.Play.InMidi(b);
      } else {  //passthru' all (default)
        //MidiPlay.OutMKB.SendShortMsg(b[0], b[1], b[2]);
        if (on.HasValue) {  //ON or OFF
          //if (P.F.AutoSync != null && CheckSwitch(b)) return;
          if (!CheckSwitch(b)) {
            int pitch = b[1] + clsPlay.TransposeKBPitch;
            lock (clsPlay.KBPitchList) {
              if (on.Value) {  //ON 
                MidiPlay.OutMRec.SendShortMsg(b[0], pitch, b[2]);
                clsPlay.KBPitchList.Add(pitch, pitch);  //ON
                P.frmSC.BeginInvoke(new delegRefresh(P.frmSC.picBottom.Refresh));
                P.frmSC.BeginInvoke(new delegRefresh(P.frmSC.picChords.Refresh));
              } else {
                clsPlay.KBPitchList_MidiOff(new byte[] { b[0], (byte)pitch, b[2] });   //OFF
              }
            }
          }
        } else {
          MidiPlay.OutMRec.SendShortMsg(b[0], b[1], b[2]);
        }
      }

      //* set kb range using untransposed kb notes
      if (P.frmSCOctaves != null //kb ranges
      && on.HasValue && on.Value) { //ON ev  
        dSetNoteName = new delegSetNoteName(SetNoteName);
        P.frmSCOctaves.BeginInvoke(dSetNoteName, b[1]);
      }
    }

    private bool CheckDoubleHit(byte[] b) {
      //* return true if double hit (on or off ev)
      long time = SW.ElapsedMilliseconds;
      bool hit = (time - DoubleHit[b[1]] < 20);
      DoubleHit[b[1]] = time;
      return hit;
    }

    private void SCAlign(int note) {
      int cnote = (note / 12) * 12;
      P.frmSC.SetRanges(cnote);
      P.frmSCAlign.Close();
    }

    private void SetNoteName(int note) {
      string txt = NoteName.MidiToNoteNameAndOctave(note);
      txt += " (" + note + ")";
      P.frmSCOctaves.lblNoteName.Text = txt;
      //P.frmSCOctaves.cmdSetLowShowC.Enabled = true;
      //P.frmSCOctaves.cmdSetPlayLoC.Enabled = true;
    }

    private bool CheckBytes(byte[] b) {
      //* check that b is valid!
      if (b[0] < 0x80) return false;  //status
      if (b[1] >= 0x80 || b[2] >= 0x80) return false;
      return true;
    }

    private void ApplyBezier(byte[] b) {
      int status = b[0] & 0xf0;
      int outval = 0;
      if (status == 0x90 && b[2] > 0 && Cfg.BezierVel >= 0) {  //ON ev (-1 = "None")
        if (b[2] > 127) {
          LogicError.Throw(eLogicError.X095);
        } else {
          outval = Beziers[Cfg.BezierVel].KBtoMidiVel[b[2]];
        }
        if (P.frmCfgBezier != null && P.frmCfgBezier.IsHandleCreated) {
          P.frmCfgBezier.Monitor(b[2], outval, true);
        }
        b[2] = (byte)outval;
      } else if (status == 0xd0) {  //aftertouch
        int inval = b[1];
        outval = Beziers[Cfg.BezierATouch].KBtoMidiVel[b[1]];
        if (P.frmCfgBezier != null && P.frmCfgBezier.IsHandleCreated) {
          P.frmCfgBezier.Monitor(inval, outval, false);
        }
        b[1] = (byte)outval;
      }
    }

    private bool CheckSwitch(byte[] b) {  //return true if switchkey
      int keyswitch = CheckSwitchKey(b);
      if (keyswitch < 0) return false;
      lock (SwitchKeyLock) {
        int status = b[0] & 0xf0;
        bool down = false;
        if (status == 0x90 && b[2] > 0) down = true;   //note ON
        foreach (string action in Forms.frmSwitch.KeyToActions[keyswitch]) {
          if (action != "") {
            Forms.frmSwitch.Delegs[action](down);
            if (action == "Sustain") {
              P.frmSC.BeginInvoke(dSetChkSwitch, P.frmSC.chkSwitchSustain, down);
            } else if (action == "KB Chord") {
              P.frmSC.BeginInvoke(dSetChkSwitch, P.frmSC.chkSwitchKBChord, down);
            }
          }
        }
        //CheckBox chk;
        //switch (keyswitch) {
        //  case 9:
        //    chk = P.frmSC.chkSwitchA;
        //    break;
        //  case 11:
        //    chk = P.frmSC.chkSwitchB;
        //    break;
        //  case 0:
        //    chk = P.frmSC.chkSwitchC;
        //    break;
        //  default:
        //    chk = null;
        //    break;
        //}
        //if (chk != null) P.frmSC.BeginInvoke(dSetChkSwitch, chk, down);

        return true;
      }
    }

    private int CheckSwitchKey(byte[] b) {  //return switchkey (0-11) or -1 (playable)
      //* check if midiin (b) is a keyswitch note (0-11)
      if (P.F == null || P.frmSC == null) return -1;
      int status = b[0] & 0xf0;
      if (status != 0x90 && status != 0x80) return -1;
      if (b[1] < Forms.frmSC.valPlayLoC) return ((int)b[1]).Mod12();  //switchkey (normal)
      #if ADVANCED
        if (b[1] > Forms.frmSC.valPlayHiC) return ((int)b[1]).Mod12();  //switchkey (chordset)
      #endif
      return -1;  //playable note
    }

    private bool SustainPedal(byte[] b) {
      //* return true if sustain pedal (handled by this method)
      if ((b[0] & 0xf0) != 0xb0) return false;  //not controller
      if (b[1] != 0x40) return false;  //not sustain pedal
      lock (SwitchKeyLock) {
        bool down = (b[2] != 0);

        long ela = (int)SW.ElapsedMilliseconds;
        //if (PrevEla.HasValue) {
        //  Debug.WriteLine("clsMidiInKB: SustainPedal: Elapsed Diff = " + (ela - PrevEla.Value));
        //}
        if (PrevEla.HasValue && ela - PrevEla.Value < 10) return false; //too close together
        PrevEla = ela;

        foreach (string action in Forms.frmSwitch.KeyToActionsPedal) {  //key12 = pedal
          if (action != "") {
            Forms.frmSwitch.Delegs[action](down);
            if (action == "Sustain") {
              P.frmSC.BeginInvoke(dSetChkSwitch, P.frmSC.chkSwitchSustain, down);
            } else if (action == "KB Chord") {
              P.frmSC.BeginInvoke(dSetChkSwitch, P.frmSC.chkSwitchKBChord, down);
            }
          }
        }
        //P.frmSC.BeginInvoke(dSetChkSwitch, P.frmSC.chkSwitchPedal, down);
        return true;
      }
    }

    private static void SetChkSwitch(CheckBox chk, bool down) {
      P.frmSC.Bypass_Event = true;
      chk.Checked = down;
      P.frmSC.Bypass_Event = false;
    }

    internal static bool IsBlackKey(int zkeynum) {  //return true if zkeynum is black
      //* used only by chordplay, non-qwerty
      switch (zkeynum.Mod12()) {
        case 1: case 3: case 6: case 8: case 10:   //black
          return true;
      }
      return false;
    }
  }

  //***********************************************************************************************

  internal class clsMidiOut : clsMidiInOut, iBassMidiOut {
    [DllImport("winmm.dll")] private static extern int midiOutOpen
      (out IntPtr hMidiOut, int uDeviceID, delegCallback dwCallback, int dwInstance, int fdwOpen);
    [DllImport("winmm.dll")] private static extern int midiOutClose(IntPtr hMidiOut);
    [DllImport("winmm.dll")] protected static extern int midiOutShortMsg (IntPtr hMidiOut, int dwMsg);  

    protected override int CallBackFlags { get { return 0; } }
    //protected bool indKB;
 
    internal clsMidiOut(string[] devs, string devname, eType type) {
      //indKB = true;
      //CloseDev = new delegClose(midiOutClose);
      Init(devs, devname, type);
    }

    public override void Close() {
      if (Handle == IntPtr.Zero) return;
      int rtn = midiOutClose(Handle);
      CheckMidiAction(rtn, "MidiOutClose");
      if (rtn == 0) Handle = IntPtr.Zero;
    }

    //protected clsMidiOut() {  //used by clsMidiOutStream
    //  //indKB = false;
    //}  

    public virtual int GetTicks() {  //implemented only by clsMidiOutStream
      throw new FatalException();
    }

    protected override IntPtr Open(int devnum) {
      //System.Timers.Timer timer = new System.Timers.Timer(10);
      //timer.AutoReset = false;
      //timer.Elapsed += Timer_Elapsed;
      //timer.Start();

      //int callbackflags = 0x30000;
      //dCallback = new delegCallback(Callback);
      //MessageBox.Show("MidiOutOpen DevNum" + devnum);
      //int rtn = midiOutOpen(out Handle, devnum, dCallback, 0, callbackflags);

      int rtn = midiOutOpen(out Handle, devnum, dCallback, 0, CallBackFlags);
      CheckMidiAction(rtn, "MidiOutOpen");
      return Handle;
    }

    //private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
    //  int rtn = midiOutOpen(out Handle, 0, dCallback, 0, CallBackFlags);
    //}

    public void SetStreamVol(int val) {
      //* obsolete
      byte b0 = (byte)0xb0;  //status (controller.channel)
      byte b1 = (byte)3;  //SONAR device vol (all chans)
      byte b2 = (byte)val;
      MidiPlay.OutMKB.SendShortMsg(b0, b1, b2);
    }

    public void SetStreamPan(int val) {
      //* obsolete
    }

    public void SetFineTuning(int val) {
      if (P.frmMidiDevs != null) {
        //* setting value should not raise scroll event
        if (IsOutKB) P.frmMidiDevs.trkMidiOutKBFineTuning.Value = val;
        if (IsOutStream) P.frmMidiDevs.trkMidiStreamFineTuning.Value = val;
      }
      for (int ch = 0; ch < 16; ch++) {
        byte status = (byte)(0xb0 | ch);
        SendShortMsg(status, 101, 1);  //set fine tuning MSB
        SendShortMsg(status, 100, 1);  //set fine tuning LSB

        SendShortMsg(status, 6, val);  //data fine tuning MSB
        SendShortMsg(status, 38, 0);  //data fine tuning LSB

        SendShortMsg(status, 100, 127);  //reset RPN
        SendShortMsg(status, 101, 127);  //reset RPN
      }
    }

    public int GetStreamVol() {
      //* obsolete
      return 80;
    }

    public int GetStreamPan() {
      //* obsolete
      return 0;
    }

    //private bool ToBeRecorded(int chan) {
    //  if (MidiDevName != MidiPlay.MidiOutKB.MidiDevName) return false;
    //  if (MidiDevName != MidiPlay.MidiOutStream.MidiDevName) return true;
    //  if (chan == P.F.FileStreamMM.Re
    //}

    //public void SendShortMsgAndRecord(int status, int msg, int data) {
    //  if (MidiPlay.MidiInKB != null && MidiPlay.MidiInKB.Ticks.HasValue) {
    //    SendShortMsg(status, msg, data);
    //    if (P.F != null && P.F.FSTrackMap != null) {
    //      P.F.FSTrackMap.RecordEv(MidiPlay.MidiInKB.Ticks.Value, (byte)status, (byte)msg, (byte)data);
    //      //Debug.WriteLine("{0,-10} {1:X2} {2:X2} {3:X2}", "RecordEv: ", status, msg, data);
    //    }
    //  }
    //}

    public void SendShortMsg(int status, int msg, int data) {
      //* not from QIPlay
      //* send midiout without channel change (2 or 3 bytes)
      if (Handle == IntPtr.Zero) return;
      int xword = (((data << 8) | msg) << 8) | status;
      midiOutShortMsg(Handle, xword);
      if (P.F.FSTrackMap != null && MidiPlay.Sync.indPlayActive == clsSync.ePlay.MidiStream) {
        int chan = status & 0x0f;
        long? ticks = clsPlay.GetTicks();
        if (ticks.HasValue && chan == P.F.FSTrackMap.RecChan) {
          P.F.FSTrackMap.RecordEvNM(ticks.Value, (byte)status, (byte)msg, (byte)data);
        }
      }
#if DebugMidiOut
      Trace(zmsg, zpitch, zvel);
#endif
    }

    public void SendShortMsgQI(int status, int msg, int data) {
      //* from QIPlay only
      if (Handle == IntPtr.Zero) return;
      int xword = (((data << 8) | msg) << 8) | status;
      midiOutShortMsg(Handle, xword);
    }

    public void SendEvents(byte[] bytes) {
      //* QIPlay only
      if (bytes.Length < 3) return;
      if (bytes[1] < 0x80) {  //no initial status byte
        LogicError.Throw(eLogicError.X058);
        return;
      }
      byte[] msg = new byte[4];  //status, msg, data, 0 (time - ignore)
      int msgindex = 0;
      for (int i = 1; i < bytes.Length; i++) {  //ignore first 0
        byte b = bytes[i];
        if (b >= 0x80 && i > 1) {
          SendShortMsgQI(msg[0], msg[1], msg[2]);
          msgindex = 0;
          msg.Initialize();
        }
        msg[msgindex++] = b;
      }
      SendShortMsgQI(msg[0], msg[1], msg[2]);
    }

    internal void Trace(int zmsg, int zpitch, int zvel) {
      //* trace George & Ira Gershwin - Summertime From `Porgy & Bess`.mid
      //* T7 C7 "STRINGS" (2nd. string track)
      if (zmsg == 0xe7) Debug.WriteLine("Pitchbend ch7(0) : " + zpitch + " " + zvel);
      Debug.WriteLine(zmsg + " " + zpitch + " " + zvel);
    }

    public void AllNotesOff() {
      for (int ch = 0; ch <= 15; ch++) {
        SendShortMsg(0xb0 | ch, 123, 0);  //allnotesoff
        SendShortMsg(0xb0 | ch, 64, 0);  //sustain off
      }
    }

    //public void ResetAllControllers() {
    //  int sustaindata = (P.frmStart.optSustainOn.Checked) ? 127 : 0;
    //  for (int ch = 0; ch <= 15; ch++) {
    //    SendShortMsg(0xb0 | ch, 121, sustaindata);
    //  }
    //}
  }

  //***********************************************************************************************

  //internal class clsMidiOutStream : clsMidiOut {
  //  [DllImport("winmm.dll")]
  //  private static extern int midiStreamOpen(
  //    out IntPtr handle, ref int deviceID, int reserved, delegCallback proc, IntPtr instance, int flag);
  //  [DllImport("winmm.dll")]
  //  private static extern int midiStreamClose(IntPtr handle);
  //  [DllImport("winmm.dll")]
  //  private static extern int midiStreamPosition(IntPtr handle, ref MMTime mmt, int cbmmt);

  //  protected override int CallBackFlags { get { return 0x30000; } }
  //  internal clsFileStream FileStream;

  //  internal clsMidiOutStream(string[] devs, string devname) {
  //    //CloseDev = new delegClose(midiStreamClose);
  //    Init(devs, devname);
  //  }

  //  protected override IntPtr Open(int devnum) {
  //    IntPtr h;
  //    int rtn = midiStreamOpen(out h, ref devnum, 1, dCallback, IntPtr.Zero, CallBackFlags);
  //    CheckMidiAction(rtn, "MidiOutStreamOpen", h);
  //    return h;
  //  }

  //  protected override void CloseDevice(IntPtr hMidiInOut) {
  //    int rtn = midiStreamClose(hMidiInOut);
  //    CheckMidiAction(rtn, "MidiStreamClose", hMidiInOut);
  //  }
  //}

  //internal class clsMidiOutStream : clsMidiOut {
  //  [DllImport("winmm.dll")]
  //  private static extern int midiOutOpen(
  //    out IntPtr handle, ref int deviceID, delegCallback proc, IntPtr instance, int flag);
  //  [DllImport("winmm.dll")]
  //  private static extern int midiOutClose(IntPtr handle);
  // // [DllImport("winmm.dll")]
  // // private static extern int midiStreamPosition(IntPtr handle, ref MMTime mmt, int cbmmt);

  //  protected override int CallBackFlags { get { return 0x30000; } }
  //  //internal clsFileStream FileStream;

  //  internal clsMidiOutStream(string[] devs, string devname) {
  //    //CloseDev = new delegClose(midiStreamClose);
  //    Init(devs, devname);
  //  }

  //  protected override IntPtr Open(int devnum) {
  //    IntPtr h;
  //    int rtn = midiOutOpen(out h, ref devnum, dCallback, IntPtr.Zero, CallBackFlags);
  //    CheckMidiAction(rtn, "MidiOutStreamOpen", h);
  //    return h;
  //  }

  //  protected override void CloseDevice(IntPtr hMidiInOut) {
  //    int rtn = midiOutClose(hMidiInOut);
  //    CheckMidiAction(rtn, "MidiStreamClose", hMidiInOut);
  //  }
  //}
}
