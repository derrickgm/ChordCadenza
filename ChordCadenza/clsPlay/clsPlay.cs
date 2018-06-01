using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Runtime;

namespace ChordCadenza {
  internal abstract partial class clsPlay {
    static clsPlay() {
      Sustain = clsSustain.New(null);
      //SetManChordSyncOpts();
    }

    protected clsPlay(clsPlay oldplay) {  //switch
      Sustain = (oldplay == null) ? clsSustain.New(null) : clsSustain.New(Sustain);
    }

    internal clsPlay() {  //initial
      PitchMid = ((int)Forms.frmSC.valPlayLoC + (int)Forms.frmSC.valPlayHiC) / 2;
      ///////KBPitchList = new clsKBPitch.Lst();  //does this work OK?
      Sustain = clsSustain.New(null);  //needed for first frmSC load
      P.frmSC.ResetSwitches();
    }

    //protected void CheckSyncs() {
    //  if (P.frmSC.chkManSync.Checked) {
    //    Forms.frmSwitch.NewManSync(this, P.frmSC.chkManSync.Checked);
    //  //} else if (P.frmSC.chkAutoSync.Checked) {
    //  //  Forms.frmSwitch.NewAutoSync(this, P.frmSC.chkAutoSync.Checked);
    //  }
    //}

    internal static clsPlay NewPlay(Forms.frmSC.ePlayMode mode) {
      if (P.F.CF != null && P.F.CF.Evs.Count > 0) {
        if (mode == Forms.frmSC.ePlayMode.Chords) return new clsPlayChords();
        if (mode == Forms.frmSC.ePlayMode.KB) return new clsPlayKeyboard();
      }
      //if (P.F.frmTrackMap != null) {
      //  if (mode == Forms.frmSC.ePlayMode.Step) return new clsPlayStep(P.F.frmTrackMap?.FileStream);
      //}
      return null;
    }

    private struct sMidiOnTime {
      internal int KB;  //keyboard key
      internal long MS;  //millisecs
      internal sMidiOnTime(int kb, long ms) {
        KB = kb;
        MS = ms;
      }
    }

    internal Forms.frmSC frmSC { get { return P.frmSC; } }

    protected clsCF CF {
      get {
        return P.F.CF;
      }
    }

    //internal clsManSync ManSync;
    //internal clsAutoSyncZZZ AutoSyncZZZ;
    internal clsManChords ManChords;
    //internal clsRiffChord RiffChord;
    //internal bool KBScale = false;

    //protected clsChordEv MidiOn_NextPlayChord;
    protected clsChordEv _MidiOn_ThisPlayChord;
    protected clsChordEv MidiOn_ThisPlayChord {
      get {
        if (ManChords != null) return ManChords.PlayChord;
        else if (P.F?.frmTonnetz?.PlayChord != null) return P.F.frmTonnetz.PlayChord;
        //else if (clsAutoSync.ActiveStatic && !P.frmStart.chkNoAudioSync.Checked) return P.F.AudioSync.PlayChord;
        else if (clsAudioSync.ActiveStatic && clsAudioSync.StaticAudioSyncEna) return P.F.AudioSync.PlayChord;
        else return _MidiOn_ThisPlayChord;
      }
      set {
        _MidiOn_ThisPlayChord = value;
      }
    }

    internal int KBLo { get { return frmSC.ShowLowPitch; } }
    internal int KBMid { get { return frmSC.ShowLowPitch + frmSC.valOctaves * 6; } }
    internal int KBHi { get { return frmSC.ShowHighPitch; } }

    //protected abstract void Switch(clsPlay play);
    protected abstract void MidiOnSub();
    internal abstract int TransposeKB { get; }
    //protected abstract bool PlayMult();
    internal abstract void ShowCurrentChord();
    protected abstract void ResetSub();
    internal abstract int LastActiveKeyOn { get; }
    internal abstract int LastActivePitchOn { get; }
    internal abstract void ShowPicChord(clsCFPC.clsEv ev, Graphics xgr, bool roundup);
    internal abstract void ShowPicBottom(Graphics xgr);

    internal static int PitchMid;

    internal static int TransposeKBPitch {
      get { return P.frmSC.OctTransposeKBPitch + (int)P.frmSC.nudTransposeKBPitch.Value; }
    }

    internal static clsKBPitch.Lst KBPitchList = new clsKBPitch.Lst(); 
    internal static object TimerLock = new object();
    private static Queue<sMidiOnTime> MidiOnTimes = new Queue<sMidiOnTime>();
    //internal static bool indNextChord = false;  //switchkey

    internal static Stopwatch Stopwatch = new Stopwatch();
    internal static  int[] MonitorTimesTotals = new int[251];
    internal static  int[] MonitorDTimesTotals = new int[251];
    internal static  List<int> GCCnt0 = new List<int>();
    internal static  List<int> GCCnt1 = new List<int>();
    internal static  List<int> GCCnt2 = new List<int>();
    internal static  List<int> MonitorTimes = new List<int>();

    /*
    private PerformanceCounter CPUCounter =
       new PerformanceCounter("Processor", "% Processor Time", "_Total");
    private DateTime LastPerformanceTime = DateTime.Now;
    private List<int> PerformanceData = new List<int>();
    */

    private bool indNoReset = false;
    protected int LastKeyOn = -1;
    protected int LastPitchOn = -1;

    internal static clsSustain Sustain;

    protected byte[] B;
    internal delegate void delegResizeForm();
    internal delegate void delegRefresh();
    internal delegate void delegRefreshNoteMap();
    internal delegate void delegRefreshBBT(clsMTime.clsBBT bbt);
    //internal delegate void delegRefreshBBT();

    internal void NewReset() { Reset(); }  //new calls to Reset()
    internal void Reset() {
      if (indNoReset) return;
      KBPitchList = new clsKBPitch.Lst();
      MidiPlay.OutMKB.AllNotesOff();  
      DeferredKB = null;
      ChordNoteCount = 0;
      MaxChordNoteCount = 0;
      Array.Clear(KeyDown, 0, KeyDown.Length);
      //indNextKey = false;
      //indSkipKey = false;
      indReloc = true;
      //indNextBarBeat = false;
      ResetSub();
    }

    internal bool ATouchReady = true;
    internal static bool PlayExists() {
      return (P.F != null && P.frmSC != null && P.frmSC.Play != null);
    }

    internal static bool ManChordsActive {
      get {
        if (P.frmSC == null || P.frmSC.Play == null) return false;
        if (P.frmSC.Play.ManChords == null) return false;
        return true;
      }
    }

    internal static bool IsPlayKeyboard {
      get {
        if (P.frmSC.Play == null) return false;
        if (!(P.frmSC.Play is clsPlayKeyboard)) return false;
        return true;
      }
    }

    internal void InMidi(byte[] b) {
      lock (TimerLock) {
        B = b;
        int status = b[0] & 0xf0;
        if (P.F.frmTrackMap?.RecTrk != null && P.F.FSTrackMap.RecChan >= 0) {
          b[0] = (byte)(status | P.F.FSTrackMap.RecChan);
        }
        //* check for controller (not sustain) or aftertouch or pitchwheel
        if (status == 0xb0 || status == 0xd0 ||status == 0xe0) {  
          MidiPlay.OutMRec.SendShortMsg(b[0], b[1], b[2]);
          //    if (SustainPedal(b)) return;
          //  MidiPlay.OutMKB.SendShortMsgAndRecord(b[0], b[1], b[2]);
          //  MidiPlay.OutMKB.SendShortMsgAndRecord(b[0], b[1], b[2]);
          //  if (b[1] == 0x40) {  //sustain pedal
          //    if (SustainPedal(b)) return;
          //  }
          //  MidiPlay.OutMKB.SendShortMsgAndRecord(b[0], b[1], b[2]);
          //} else if (status == 0xe0) {  //pitchwheel
          //  PitchWheel.PWChange(B);
          //  PitchWheelChange();
        } else if ((status == 0x80) || (status == 0x90)) {  //note ON or OFF
          MidiOnOff();
        } else {  //default
        //  MidiPlay.OutMKB.SendShortMsgAndRecord(b[0], b[1], b[2]);
          //if (((b[0] & 0xf0) == 0xd0) || ((b[0] & 0xf0) == 0xa0)) Debug.WriteLine("Aftertouch: " + b[1]);  //channel pressure (aftertouch)
        }
      }
    }

    //private int[] NormalizeChord(int[] inchord) {
    //  //* return converted inchord (e.g. FCA to FAC) - assume first note is root
    //  if (inchord == null || inchord.Length == 0) throw new LogicException();  //avoid endless loop
    //  bool[] chbool = new bool[12];
    //  List<int> outlist = new List<int>();
    //  foreach (int n in inchord) chbool[n] = true;
    //  for (int i = inchord[0]; outlist.Count == inchord.Length; i++) {
    //    if (i > 11) i %= 12;
    //    if (chbool[i]) outlist.Add(i);
    //  }
    //  return outlist.ToArray();
    //}

    //private clsCF.clsEv.sNote[] CreateNotes(List<int> chlist, 
    //  clsMTime.clsBBT onbbt, clsMTime.clsBBT offbbt) {
    //  clsCF.clsEv.sNote[] notes = new clsCF.clsEv.sNote[chlist.Count];
    //  for (int i = 0; i < chlist.Count; i++) {
    //    notes[i] = new clsCF.clsEv.sNote();
    //    notes[i].Pitch_NoKBTrans = chlist[i];
    //    notes[i].OnBBT = onbbt;
    //    notes[i].OffBBT = offbbt;
    //  }
    //  return notes;
    //}

    //internal void SetTransposeKBPitch() {
    //  TransposeKBPitch = clsPlay.OctTransposeKBPitch + (int)P.frmStart.nudTransposeKBPitch.Value;
    //  //if (P.frmSC != null) P.frmSC.lblOctPitch.Text = (clsPlay.OctTransposeKBPitch / 12).ToString();
    //}

    //private void SendDirect() {
    //  MidiPlay.OutMKB.SendShortMsg(B[0], B[1], B[2]);
    //}

    internal static void SendDirect(int msg, int key, int vel) {
      //if (vel > 0) Debug.WriteLine("SendDirect: Pitch: " + key + " " + key.Mod12()); 
      MidiPlay.OutMRec.SendShortMsg(msg, key, vel);
    }

    internal static void InitStopwatch() {
      lock (MonitorTimes) {
        MonitorTimesTotals = new int[MonitorTimesTotals.Length];
        MonitorDTimesTotals = new int[MonitorDTimesTotals.Length];
        MonitorTimes.Clear();
        GCCnt0.Clear();
        GCCnt1.Clear();
        GCCnt2.Clear();
      }
    }

    internal static string ShowMonitorTotals() {
      string msg = "MidiOnOff_Elapsed_msecs : Count\n";
      msg += "'D'RefreshBBT_Elapsed_msecs : Count\n";
      for (int i = 0; i < MonitorTimesTotals.Length; i++) {
        if (MonitorTimesTotals[i] == 0) continue;
        msg += i + ":" + MonitorTimesTotals[i] + "   ";
      }
      msg += '\n';
      for (int i = 0; i < MonitorDTimesTotals.Length; i++) {
        if (MonitorDTimesTotals[i] == 0) continue;
        msg += "D" + i + ":" + MonitorDTimesTotals[i] + "   ";
      }
      return msg;
    }

    internal static string ShowMonitorTimeline() {
      lock (MonitorTimes) {
        string msg = "";
        for (int i = 0; i < MonitorTimes.Count; i++) {
          string mon = MonitorTimes[i].ToString();
          if (MonitorTimes[i] == int.MinValue) mon = "\n<";
          else if (MonitorTimes[i] < 0) mon = "d" + (-MonitorTimes[i] - 1).ToString() + "> ";
          else mon += "  ";
          if (i == 0 || i == MonitorTimes.Count - 1 
          || GCCnt0[i - 1] != GCCnt0[i]
          || GCCnt1[i - 1] != GCCnt1[i]
          || GCCnt2[i - 1] != GCCnt2[i]) {
            msg += mon + "(" + GCCnt0[i] + "/" + GCCnt1[i] + "/" + GCCnt2[i] + ")" + "  ";
          } else {
            msg += mon;
          }
        }
        return msg;
      }
    }

    private void MidiOnOff() {
      bool on = true;
      if ((B[0] & 0xf0) == 0x80) {
        on = false;
      } else if ((B[0] & 0xf0) == 0x90) {
        if (B[2] == 0) on = false;
      }
      using (new clsMonitor()) {
        if (frmSC.OctTransposeKB != 0) {
          int pitch = B[1];
          pitch += frmSC.OctTransposeKB;
          if (pitch > 127) pitch = 127; else if (pitch < 0) pitch = 0;
          B[1] = (byte)pitch;
        }
        indNoReset = true;
        if (on) MidiOn(); else MidiOff();  //begininvoke refreshbbt...
        indNoReset = false;
      }
    }

    internal class clsGC : IDisposable {   //not yet used?
      private static bool Active = false;
      internal clsGC() {
        if (Active || GCSettings.LatencyMode != GCLatencyMode.Interactive) return;  //default
        Active = true;
        Stopwatch = new Stopwatch();
        Stopwatch.Start();
        GC.Collect();  //force blocking garbage collection on all generations
        Debug.WriteLine("GCCollect millsecs = " + Stopwatch.ElapsedMilliseconds);
        Stopwatch.Stop();
        GCSettings.LatencyMode = GCLatencyMode.LowLatency;
      }

      public void Dispose() {
        if (Forms.frmSC.MenuMonitor) {
          MonitorMidiOnOff();
        } else {
          GCSettings.LatencyMode = GCLatencyMode.Interactive;
          Active = false;
        }
      }
    }

    internal class clsMonitor : IDisposable {
      public clsMonitor() {
        if (Forms.frmSC.MenuMonitor) {
          Stopwatch = new Stopwatch();
          Stopwatch.Start();
        }
      }
      public void Dispose() {
        if (Forms.frmSC.MenuMonitor) {
          MonitorMidiOnOff();
        }
      }
    }

    private static void MonitorMidiOnOff() {
      lock (MonitorTimes) {
        //* monitor midion/off response times
        Stopwatch.Stop();
        int msecs = (int)Stopwatch.ElapsedMilliseconds;
        MonitorTimes.Add(msecs);

        //* get GCCollection stuff
        GCCnt0.Add(GC.CollectionCount(0));
        GCCnt1.Add(GC.CollectionCount(1));
        GCCnt2.Add(GC.CollectionCount(2));

        //* add to totals
        if (msecs >= MonitorTimesTotals.Length) msecs = MonitorTimesTotals.Length - 1;
        MonitorTimesTotals[msecs]++;

        /*
        //* get performance statistics
        DateTime now = DateTime.Now;
        TimeSpan diff = now.Subtract(LastPerformanceTime);
        if (diff.Milliseconds > 1000) {
          LastPerformanceTime = now;
          PerformanceData.Add((int)CPUCounter.NextValue());
        } else {
          PerformanceData.Add(-1);
        }
        */
      }
    }

    internal void AddToMonitorDTimes(int msecs) {
      //* add to totals
      if (msecs >= MonitorDTimesTotals.Length) msecs = MonitorDTimesTotals.Length - 1;
      MonitorDTimesTotals[msecs]++;
      //MaxMonitorDTime = Math.Max(MaxMonitorDTime, msecs);
    }

    protected void MidiOff() {
      MidiMon.KeyUp(B[1]);
      //Debug.WriteLine("clsPlay: MidiOff: ChordNoteCount = " + ChordNoteCount);
      KeyDown[B[1]] = false;
      if (ManChords != null && ManChords.MidiOff(B[1])) return;
      if (IsManSync && IsKBChordPlay) {
        if (!indSwitchKey && MaxChordNoteCount == 1) {  //last up
          bool dup = (NoSkipAfterReloc && !indReloc) ? true : false;  //dup if indReloc not set
          if (GetSingleKeyAction(B[1]) == eManSyncAction.Next) {
            PrevNextChordPos(true, true, dup);
            indReloc = true;
          } else if (GetSingleKeyAction(B[1]) == eManSyncAction.Prev) {
            PrevNextChordPos(false, true, false);
            indReloc = true;
          }  //else no action (Play)
        }
        DeferredKB = null;
      }
      KBPitchList_MidiOff(B);
      ChordNoteCount--;
      if (ChordNoteCount < 0) ChordNoteCount = 0;  //should not happen
      if (ChordNoteCount == 0) {
        MaxChordNoteCount = 0;
        //indNextKey = false;
        //indSkipKey = false;
      }
      P.frmSC.BeginInvoke(new delegRefresh(ShowCurrentChord));
      MidiMon.CheckAllOff();
    }

    private bool IsManSyncNextKey(byte b) {
      if (clsMidiInKB.IsBlackKey(b)) return indNextBlack;
      return indNextWhite;
    }

    private eManSyncAction GetSingleKeyAction(byte b) {
      if (clsMidiInKB.IsBlackKey(b)) return SingleBlackAction;
      return SingleWhiteAction;
    }

    private bool IsKBChordPlay {
      get {
        if (P.frmSC.PlayMode == Forms.frmSC.ePlayMode.Chords) return false;   //chordmode 
        if (P.frmStart.indConstantChordPlay) return false;
        return clsPlayKeyboard.PlayNearestChordNote;  //KBChord
      }
    }

    internal static bool KBPitchList_MidiOff(byte[] b) {  // return false if error
      lock (clsPlay.KBPitchList) {
        int chan = (b[0] & 0x0f);
        if (!KBPitchList.ContainsKB(b[1])) {
          //Debug.WriteLine("MidiOff: kb missing at " + DateTime.Now);  //is there a hanging MidiON?
          return false;
        }
        List<clsKBPitch> lst = KBPitchList.GetKBPitches(b[1]);  //get pitches for this KB
        foreach (clsKBPitch kbp in lst) {
          if (!Sustain.MidiOff(kbp)) {
            //* process midi OFF if this is the last key that points to pitch
            int pitch = kbp.Pitch;
            //* get all kbp that point to this pitch
            List<clsKBPitch> kbplist = KBPitchList.GetPitch(null, pitch);
            foreach (clsKBPitch kbpitch in kbplist) {  //must be at least one entry
              if (kbpitch.KB == b[1]) KBPitchList.Remove(kbpitch);
            }
            if (kbplist.Count > 1) continue; //another KB is pointing to this pitch - don't switch off yet
            //* do normal Midi OFF process on B[1]
            SendDirect(0x80 | chan, pitch, 0);
          }
        }
      }  //end lock
      P.frmSC.BeginInvoke(new delegRefresh(P.frmSC.picBottom.Refresh));
      P.frmSC.BeginInvoke(new delegRefresh(P.frmSC.picChords.Refresh));
      return true;
    }

    //private bool SyncActive() {
    //  if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.None
    //   && MidiPlay.Sync.indPlayActive != clsSync.ePlay.Tempo) return true;
    //  //if (!P.F.AudioSync.IsEmpty()) return true;
    //  if (clsAutoSync.ActiveStatic) return true;
    //  if (P.frmSC.CurrentEvIndex < 0) return true;
    //  return false;
    //}

    internal bool IsManSync {
      get {
        if (MidiPlay.Sync.indPlayActive == clsSync.ePlay.MidiStream) return false;  //midi sync
        if (clsAudioSync.StaticAudioSyncEna) return false;  //auto sync
        //&& MidiPlay.Sync.indPlayActive == clsSync.ePlay.Tempo) return false;  //auto sync
        return true;
      }
    }

    //internal clsMTime.clsBBT BBTSwitchNextBar = null;
    //internal clsMTime.clsBBT BBTSwitchNextBeat = null;
    //internal clsMTime.clsBBT BBTSwitchNextChord = null;

    //internal class clsBBTSwitch {
    //  internal clsMTime.clsBBT NextBar = null;
    //  internal clsMTime.clsBBT NextBeat = null;
    //  internal clsMTime.clsBBT NextChord = null;

    //  internal void Nullify() {
    //    NextBar = null;
    //    NextBeat = null;
    //    NextChord = null;
    //  }

    //  internal clsMTime.clsBBT GetBBT() {
    //    if (NextBeat != null) return NextBeat;
    //    if (NextBar != null) return NextBar;
    //    return NextChord;  //may be null
    //  }
    //}

    //internal clsBBTSwitch BBTSwitch = new clsBBTSwitch();
    internal clsMTime.clsBBT BBTSwitch;
    private delegate void delSetChkBarBeat(bool val);

    //private static bool _indNextBarBeat = true;  //else chord
    internal static bool indSwitchKey {
      get {
        //if (Forms.frmManChordSync.Disabled) return true;
        return !P.frmSC.chkManSyncChord.Checked;
        //return _indNextBarBeat;
      }
      set {
        if (P.frmSC != null) P.frmSC.BeginInvoke(new delSetChkBarBeat(SetChkChord), !value);
        //_indNextBarBeat = value;
      }
    }

    private static void SetChkChord(bool val) {  //called by delegate
      P.frmSC.chkManSyncChord.Checked = val;
    }

    //internal void NextSwitchChordUpDown(bool down) {
    //  //* move on keyup, update on keydown 
    //  if (!P.frmStart.chkNoSync.Checked && SyncActive()) return;
    //  indNextBeat = false;

    //  if (down) {  //set CurrentBBT if previous UP ok
    //    if (BBTChord != null) {
    //      if (BBTChord.Beats == CF.Evs[P.frmSC.CurrentEvIndex].OnBBT.Beats) P.F.CurrentBBT = BBTChord;
    //      BBTChord = null;
    //      P.frmSC.BeginInvoke(new delegRefreshBBT(Forms.frmStart.RefreshBBT), P.F.CurrentBBT);
    //    }
    //    P.F.AudioSync.indFirstSwitchKey = false;  //may not be necessary here
    //  } else {  //up - set BBTChord

    //    //P.frmSC.CurrentEvIndex++;
    //    if (P.F.CurrentBBT.Ticks < Forms.frmTrackMap.BarPaneBBTLo.Ticks) {
    //      P.F.CurrentBBT = Forms.frmTrackMap.BarPaneBBTLo.Copy();
    //      P.frmSC.CurrentEvIndex = P.F.CF.FindCFEv(P.F.CurrentBBT);
    //    } else {
    //      P.frmSC.CurrentEvIndex++;
    //    }

    //    while (P.F.CF.Evs[P.frmSC.CurrentEvIndex].Notes.Length == 0) {  //null chord
    //      P.frmSC.CurrentEvIndex++;
    //      if (P.frmSC.CurrentEvIndex >= P.F.CF.Evs.Count - 1) break;
    //    }
    //    if (P.frmSC.CurrentEvIndex >= CF.Evs.Count) P.frmSC.CurrentEvIndex = CF.Evs.Count - 1;
    //    BBTChord = CF.Evs[P.frmSC.CurrentEvIndex].OnBBT.Copy();
    //  }
    //}

    //internal static bool indFirstSwitchChordBarBeat = true;

    //internal void NextSwitchBarBeatChord(bool indbar, bool down) {
    //  if (!IsManSync) return;
    //  indSwitchKey = true;

    //  if (down) {  //set CurrentBBT if previous UP ok
    //    clsMTime.clsBBT bbtbarbeat = (indbar) ? BBTSwitchNextBar : BBTSwitchNextBeat;
    //    if (bbtbarbeat != null) {
    //      clsMTime.clsBBT bbt = GetNextBarBeat(indbar);
    //      if (bbt.Ticks == bbtbarbeat.Ticks) P.F.CurrentBBT = bbtbarbeat;
    //      BBTSwitchNextBeat = null;
    //      BBTSwitchNextBar = null;
    //    }
    //    P.frmSC.BeginInvoke(new delegRefreshBBT(Forms.frmStart.RefreshBBT), P.F.CurrentBBT);
    //  } else {  //up - set BBTChord
    //    NextSwitchUp();
    //  }
    //}

    //internal void NextSwitchBeat(bool down) {  //called from frmSwitch
    //  if (!IsManSync) return;
    //  indSwitchKey = true;

    //  if (down) {  //set CurrentBBT if previous UP ok
    //    clsMTime.clsBBT bbtsw = BBTSwitch;
    //    if (bbtsw != null) {
    //      clsMTime.clsBBT bbt = P.F.CurrentBBT.GetNextBeat();
    //      if (bbt.Ticks == bbtsw.Ticks) P.F.CurrentBBT = bbtsw;
    //      BBTSwitch = null;
    //    }
    //    P.frmSC.BeginInvoke(new delegRefreshBBT(Forms.frmStart.RefreshBBT), P.F.CurrentBBT);
    //  } else {  //up - set BBTChord
    //    //NextSwitchUp();
    //    clsMTime.clsBBT bbtbeat = P.F.CurrentBBT.GetNextBeat();
    //    if (bbtbeat.Ticks < P.F.MaxBBT.Ticks) BBTSwitch = bbtbeat;
    //  }
    //}

    internal void PrevSwitch(eSwitchInterval interval, bool down) {  //called from frmSwitch
      //* just set bbt (and chord index) and indreloc
      if (!IsManSync) return;
      indSwitchKey = true;
      int index;  //not used
      if (down) {  //set CurrentBBT if previous UP ok
        clsMTime.clsBBT bbt = GetPrevNextSwitch(interval, false, out index);
        if (bbt.Ticks < 0) bbt = new clsMTime.clsBBT(0);
        if (bbt.Ticks > P.F.MaxBBT.Ticks) bbt = P.F.MaxBBT.BBTCopy;
        P.F.CurrentBBT = bbt;
        if (interval == eSwitchInterval.Chord) P.frmSC.CurrentEvIndex = index;
        BBTSwitch = bbt;
        indReloc = true;
        P.frmSC.BeginInvoke(new delegRefreshBBT(Forms.frmStart.RefreshBBT), P.F.CurrentBBT);
      }
    }

    internal void NextSwitch(eSwitchInterval interval, bool down) {  //called from frmSwitch
      if (!IsManSync) return;
      indSwitchKey = true;
      int index;  //not used

      if (down) {  //set CurrentBBT if previous UP ok
        clsMTime.clsBBT bbtsw = BBTSwitch;
        if (bbtsw != null) {
          //if (bbt.Ticks == bbtsw.Ticks) P.F.CurrentBBT = bbtsw;
          if (!indReloc) {
            clsMTime.clsBBT bbt = GetPrevNextSwitch(interval, true, out index);
            if (bbt.Ticks != bbtsw.Ticks) {  //different interval or direction
              if (bbt.Ticks > 0 && bbt.Ticks < P.F.MaxBBT.Ticks) BBTSwitch = bbt;
            }
            if (interval == eSwitchInterval.Chord) P.frmSC.CurrentEvIndex = index;
            P.F.CurrentBBT = bbt;
          }
          BBTSwitch = null;
        }
        P.frmSC.BeginInvoke(new delegRefreshBBT(Forms.frmStart.RefreshBBT), P.F.CurrentBBT);
        //P.frmSC.BeginInvoke(new delegRefreshBBT(Forms.frmStart.RefreshBBT));
      } else {  //up - set BBTChord
        clsMTime.clsBBT bbt = GetPrevNextSwitch(interval, true, out index);
        if (bbt.Ticks > 0 && bbt.Ticks < P.F.MaxBBT.Ticks) BBTSwitch = bbt;
      }
      indReloc = false;
    }

    private clsMTime.clsBBT GetPrevNextSwitch(eSwitchInterval interval, bool indnext, out int index) {
      index = -1;
      switch (interval) {
        case eSwitchInterval.Beat:
          return (indnext) ? P.F.CurrentBBT.GetNextBeat() : P.F.CurrentBBT.GetPrevBeat();
        case eSwitchInterval.Bar:
          return (indnext) ? P.F.CurrentBBT.GetNextBar() : P.F.CurrentBBT.GetPrevBar();
        case eSwitchInterval.Chord:
          return GetPrevNextChord(indnext, out index);
        default:
          LogicError.Throw(eLogicError.X163);
          return null;
      }
    }

    //internal void NextSwitchChord(bool down) {  //called from frmSwitch
    //  if (!IsManSync) return;
    //  indSwitchKey = true;

    //  if (down) {  //set CurrentBBT if previous UP ok
    //    clsMTime.clsBBT bbtsw = BBTSwitch;
    //    if (bbtsw != null) {
    //      int index;
    //      clsMTime.clsBBT bbt = GetPrevNextChord(true, out index);
    //      if (bbt.Ticks == bbtsw.Ticks) {
    //        P.F.CurrentBBT = bbtsw;
    //        P.frmSC.CurrentEvIndex = index;
    //      }
    //      BBTSwitch = null;
    //    }
    //    P.frmSC.BeginInvoke(new delegRefreshBBT(Forms.frmStart.RefreshBBT), P.F.CurrentBBT);
    //  } else {  //up - set BBTChord
    //    //NextSwitchUp();
    //    int index;
    //    clsMTime.clsBBT bbtchord = GetPrevNextChord(true, out index);
    //    if (bbtchord.Ticks < P.F.MaxBBT.Ticks) BBTSwitch = bbtchord;
    //  }
    //}

    //private void NextSwitchUp() {
    //  clsMTime.clsBBT bbtbar = P.F.CurrentBBT.GetNextBar();
    //  clsMTime.clsBBT bbtbeat = P.F.CurrentBBT.GetNextBeat();
    //  int index;
    //  clsMTime.clsBBT bbtchord = GetNextChord(out index);
    //  if (bbtbar.Ticks < P.F.MaxBBT.Ticks && bbtchord.Ticks < P.F.MaxBBT.Ticks) {
    //    BBTSwitch.NextBeat = bbtbeat;
    //    BBTSwitch.NextBar = bbtbar;
    //    BBTSwitch.NextChord = bbtchord;
    //  }
    //}

    //private clsMTime.clsBBT GetNextBarBeat(bool indbar) {
    //  if (indbar) return P.F.CurrentBBT.GetNextBar();
    //  else return P.F.CurrentBBT.GetNextBeat();
    //}

    private clsMTime.clsBBT GetPrevNextChord(bool indnext, out int index) {
      //* get current chord
      if (CF.Evs[P.frmSC.CurrentEvIndex].OnTime > P.F.CurrentBBT.Ticks
      || CF.Evs[P.frmSC.CurrentEvIndex].OffTime <= P.F.CurrentBBT.Ticks) {
        P.frmSC.CurrentEvIndex = CF.FindCFEv(P.F.CurrentBBT);
        Debug.WriteLine("clsPlay: GetNextChord: FindCFEv at " + P.F.CurrentBBT.ToString()); 
      }
      //* get next chord
      index = (indnext) ? NextChord(false) : PrevChord(false);
      return CF.Evs[index].OnBBT;
    }

    internal void PrevNextChordPos(bool indnext, bool indaction, bool dup) {
      //* move and update on keydown
      if (!IsManSync) return;
      BBTSwitch = null;

      int index = (indnext) ? NextChord(dup) : PrevChord(dup);
      P.frmSC.CurrentEvIndex = index;

      P.F.CurrentBBT = CF.Evs[P.frmSC.CurrentEvIndex].OnBBT.Copy();
      P.frmSC.BeginInvoke(new delegRefreshBBT(Forms.frmStart.RefreshBBT), P.F.CurrentBBT);
      //P.frmSC.BeginInvoke(new delegRefreshBBT(Forms.frmStart.RefreshBBT));
      if (indaction) MidiOn_ThisPlayChord = P.F.CF.Evs[P.frmSC.CurrentEvIndex].PlayChord;
    }

    private static int PrevChord(bool dup) {
      int cnt = (dup) ? 2 : 1;
      int index = P.frmSC.CurrentEvIndex;
      if (index > 0) {
        for (int i = 0; i < cnt; i++) {
          if (--index <= 0) break;
          while (P.F.CF.Evs[index].Notes.Length == 0) {  //null chord
            index--;
            if (index <= 0) break;
          }
          if (index < 0) index = 0;
        }
      }
      return index;
    }

    private int NextChord(bool dup) {
      int cnt = (dup) ? 2 : 1;
      int index = P.frmSC.CurrentEvIndex;
      if (index < P.F.CF.Evs.Count - 1) {
        for (int i = 0; i < cnt; i++) {
          if (++index >= P.F.CF.Evs.Count - 1) break;
          while (P.F.CF.Evs[index].Notes.Length == 0) {  //null chord
            index++;
            if (index >= P.F.CF.Evs.Count - 1) break;
          }
          if (index >= CF.Evs.Count) index = CF.Evs.Count - 1;
        }
      }
      return index;
    }

    //internal void MidiOn(byte[] b) {  //called from AutoSync timer
    //  B = b;
    //  MidiOn();
    //}

    internal byte[] DeferredKB = null;
    internal int ChordNoteCount = 0;
    internal int MaxChordNoteCount = 0;
    internal bool[] KeyDown = new bool[128];  //check for missing OFF evs etc.
    //internal bool indNextKey = false;
    //internal bool indSkipKey = false;
    internal bool indReloc = false;
    internal enum eManSyncAction { Play, Prev, Next };
    internal enum eSwitchInterval { Beat, Bar, Chord };

    //*frmManChordSync settings
    internal static eManSyncAction SingleBlackAction = eManSyncAction.Next;
    internal static eManSyncAction SingleWhiteAction = eManSyncAction.Play;
    internal static bool NoSkipAfterReloc = false;
    internal static bool indNextBlack = false;
    internal static bool indNextWhite = true;

    //internal static void SetManChordSyncOpts() {
    //  if (Forms.frmManChordSync.Disabled) {
    //    indNextBarBeat = true;
    //    SingleBlackAction = eManSyncAction.Play;
    //    SingleWhiteAction = eManSyncAction.Play;
    //    NoSkipAfterReloc = true;
    //    indNextBlack = false;
    //    indNextWhite = false;
    //  } else {
    //    indNextBarBeat = false;
    //    SingleBlackAction = Forms.frmManChordSync.SingleBlackAction;
    //    SingleWhiteAction = Forms.frmManChordSync.SingleWhiteAction;
    //    NoSkipAfterReloc = Forms.frmManChordSync.NoSkipAfterReloc;
    //    indNextBlack = Forms.frmManChordSync.indNextBlack;
    //    indNextWhite = Forms.frmManChordSync.indNextWhite;
    //  }
    //}

    private void MidiOn() {
      MidiMon.KeyDown(B[1]);
      //Debug.WriteLine("clsPlay: MidiOn: ChordNoteCount = " + ChordNoteCount);
      //Debug.WriteLine("MidiIn thread: "
      //  + System.Threading.Thread.CurrentThread.Name
      //  + " " + System.Threading.Thread.CurrentThread.ManagedThreadId);

      if (!KeyDown[B[1]]) ChordNoteCount++;  //only if previous ON switched OFF
      KeyDown[B[1]] = true;
      MaxChordNoteCount = Math.Max(MaxChordNoteCount, ChordNoteCount);

      long? ticks = GetTicks();
      //Debug.WriteLine("clsPlay: MidiOn: Unsustained Count = " + KBPitchList.GetUnsustained().Count);

      if (!IsManSync) {  //auto sync
        GetPlayChord(ticks.Value, Forms.frmSC.SyncopationDefault.Ticks);
        if (!indSwitchKey) indReloc = false;
        MidiOn_Final();
        return;
      }

      if (!IsKBChordPlay) {
        //* no action

      } else if (!indSwitchKey && ChordNoteCount == 1) {
        if (GetSingleKeyAction(B[1]) != eManSyncAction.Play) {
          DeferredKB = B.ToArray();
          return;
        } else {
          if (IsManSyncNextKey(B[1])) {
            DeferredKB = B.ToArray();
            return;
          }
        }
      }

      if (!indSwitchKey && ChordNoteCount == 2) {
        if (IsManSyncNextKey(B[1])) {
          if (!NoSkipAfterReloc || !indReloc) PrevNextChordPos(true, true, false);
          else MidiOn_ThisPlayChord = P.F.CF.Evs[P.frmSC.CurrentEvIndex].PlayChord;
        }
        if (DeferredKB != null) PlayDeferred();

      } else if (indSwitchKey) {  //manual sync - beats
          //Debug.WriteLine("clsPlay: MidiOut: BBTSwitchNextBar = " + BBTSwitchNextBar
          //  + " BBTSwitchNextBeat = " + BBTSwitchNextBeat);
        if (BBTSwitch == null) {
          GetPlayChord(ticks.Value, Forms.frmSC.SyncopationDefault.Ticks);
          if (DeferredKB != null) PlayDeferred();
        } else {  //manual sync - chords
          int index = P.F.CF.FindCFEv(BBTSwitch);
          MidiOn_ThisPlayChord = P.F.CF.Evs[index].PlayChord;
          if (DeferredKB != null) PlayDeferred();
        }
      //} else {  //NextChord
      //  MidiOn_ThisPlayChord = P.F.CF.Evs[P.frmSC.CurrentEvIndex].PlayChord;
      }

      if (!indSwitchKey) indReloc = false;
      MidiOn_Final();
    }

    private void PlayDeferred() {
      byte[] bcopy = B.ToArray();
      B = DeferredKB;
      MidiOn_Final();
      B = bcopy;
      DeferredKB = null;
    }

    private void MidiOn_Final() {
      lock (KBPitchList) {
        if (clsAudioSync.ActiveStatic) P.F.AudioSync.MidiOn(B);
        if (ManChords != null && ManChords.MidiOn(B[1])) return; 
        MidiOnSub();
      }  //end lock

      P.frmSC.BeginInvoke(new delegRefresh(P.frmSC.picBottom.Refresh));

      //* next line may be commented out to try to stop deferred currentbbt from 
      //* switchkey next event (next beat) from being displayed too early
      P.frmSC.BeginInvoke(new delegRefresh(P.frmSC.SetEndBBTNoRefresh));
      P.frmSC.BeginInvoke(new delegRefresh(ShowCurrentChord));
    }

    protected void MidiOn_Play(int[] pp) {
      if (pp == null) return;

      //Debug.Write("clsPlay: pitches: ");
      //foreach (int p in pp) Debug.Write(p + " ");
      //Debug.WriteLine("");

      foreach (int pitch in pp) {  //pp.Length > 1 for PlayChords
        Sustain.MidiOn();
        if (KBPitchList.GetPitch(null, pitch).Count == 0) {  //pitch not already played
          SendDirect(B[0], pitch, B[2]);
        } else {  //playing note that is already sounding
          if (!Sustain.MidiOnDup(pitch)) {
            SendDirect(B[0], pitch, B[2]);  //playKBChords
          }
        }
        KBPitchList.Add(B[1], pitch);
        //Debug.WriteLine(DateTime.Now +  " KBPitchList.Count = " + KBPitchList.Count);
      }
    }

    internal static void ShowCurrentNotes(System.Windows.Forms.PictureBox pic, Graphics bgr) {
      //* called by paint routine
      lock (clsPlay.KBPitchList) {
        //Debug.WriteLine(DateTime.Now + " Show Current NOTES ");
        foreach (clsPlay.clsKBPitch pitch in KBPitchList.GetUnsustained()) {
          if (!pitch.KB.HasValue) {
            LogicError.Throw(eLogicError.X077);
            return;
          }
          int kb = pitch.KB.Value;  //not sustained - KB should not be null
          P.frmSC.ShowCurrentNote(pic, bgr, kb);
        }
      }
    }

    internal clsChordEvTimed ThisPlayChord {
      get {
        int currentev = frmSC.CurrentEvIndex;
        if (currentev < 0 || currentev > P.F.CF.Evs.Count - 1) return null;
        return P.F.CF.Evs[currentev].PlayChord;
      }
    }

    protected clsChordEvTimed NextPlayChord {
      get {
        int currentev = frmSC.CurrentEvIndex;
        if (currentev < 0 || currentev > P.F.CF.Evs.Count - 2) return null;
        return P.F.CF.Evs[currentev + 1].PlayChord;
      }
    }

    protected clsChordEvTimed NextNextPlayChord {
      get {
        int currentev = frmSC.CurrentEvIndex;
        if (currentev < 0 || currentev > P.F.CF.Evs.Count - 3) return null;
        return P.F.CF.Evs[currentev + 2].PlayChord;
      }
    }

    protected static bool GetPlayChord_FirstTime = true;
    protected static string GetPlayChord_fmt = "{0,-11} {1,-12} {2,-7} {3,-13} {4,-13} {5,-8} {6,-8}";
    protected clsChordEvTimed GetPlayChordBase(out clsChordEvTimed nextplaychord, long ticks, int ticksmargin) {
      bool debug = false;
      if (debug && GetPlayChord_FirstTime) {
        GetPlayChord_FirstTime = false;
        Debug.WriteLine(String.Format(GetPlayChord_fmt, "TicksMargin", "StartBBTicks", "KBTicks", "ThisPlayChord", "NextPlayChord", "ThisDiff", "NextDiff"));
      }
      nextplaychord = null;
      //if (ThisPlayChord == null) return null;
      if (NextPlayChord == null) return ThisPlayChord;
      clsCF cf = CF;
      //calculate ticks since last beat (to determine if 'next' chord should be played)
      //if (P.frmStart.optMidiClocks.Checked) {
      //Debug.WriteLine("LeadInClocks = " +  frmSC.LeadInClocks + " MidiClockCount = " + frmSC.MidiClockCount);

      //* calculate ticks from start of song
      // **************************************************************************************************
      //*** better to use midiStreamPosition() to get ticks directly - will only work with stream
      //*** midiStreamPosition() returns ticks from start of play (not start of song)
      // **************************************************************************************************
      //ticks = GetTicks();
      if (debug) {
        if (ThisPlayChord != null && NextPlayChord != null) {
          Debug.WriteLine(String.Format(GetPlayChord_fmt,
            ticksmargin,
            P.F.CurrentBBT.Ticks,
            ticks,
            ThisPlayChord.OnTime,
            NextPlayChord.OnTime,
            ThisPlayChord.OnTime - ticks,
            NextPlayChord.OnTime - ticks));
        }
      }
      //Debug.WriteLine("ticks = " + ticks 
      //  + " OnTime = " + NextPlayChord.OnTime
      //  + " Diff = " + (NextPlayChord.OnTime - ticks)
      //  + " Sustain = " + Sustain);

      //if (indNextChord && ThisPlayChord != null) {
      //  if ((ticks - ThisPlayChord.OnTime) >= ticksmargin) {
      //    if ((NextPlayChord.OnTime - ticks) < ticksmargin) indNextChord = false;
      //    nextplaychord = NextNextPlayChord;
      //    return NextPlayChord;
      //  }
      //} else
      long tickslatency = 0;
      if (Cfg.LatencyKB != 0) {
        tickslatency = (long)((Cfg.LatencyKB * P.F.MTime.TicksPerQNote * 1000L)) / (P.F.FSTrackMap.TempoMap[(int)ticks]);
        //*            (msecs * ticks * microsecs * qnote     
        //*                     qnote * msecs     * microsecs 
      } 
      if ((NextPlayChord.OnTime - ticks) <= ticksmargin + tickslatency) {
        //indNextChord = false;
        nextplaychord = NextNextPlayChord;
        return NextPlayChord;
      }
      nextplaychord = NextPlayChord;
      return ThisPlayChord;

    }

    internal static long? GetTicks() {
      if (P.F == null || P.F.CurrentBBT == null) return null;
      //////////if (P.F.FileStreamMM == null) return null; 
      long ticks = P.F.CurrentBBT.Ticks;
      if (MidiPlay.Sync.indPlayActive == clsSync.ePlay.SyncEnabled) {
        long clocks = MidiPlay.Sync.MidiClockCount;
        if (clocks > 0) {
          ticks = clocks * P.F.MTime.TicksPerQNote / P.F.MTime.MidiClocksPerQNote;
        }
      } else if (MidiPlay.Sync.indPlayActive == clsSync.ePlay.MidiStream) {
        try {
          //ticks = P.F.QIPlay.GetTicks();
          return (long?)P.F.WaitPlay.GetTicks();
        }
        catch (MidiIOException) {
          return null;
        }
      } 
      return ticks;
    }

    //protected clsChordEv GetPlayChord(out clsChordEv nextplaychord, long ticks, int ticksmargin) {
    //  clsChordEvBase nextplaychordbase;
    //  clsChordEvBase ret = GetPlayChordBase(out nextplaychordbase, ticks, ticksmargin);
    //  nextplaychord = (clsChordEv)nextplaychordbase;
    //  int index = frmSC.CurrentEvIndex;
    //  while (ret == null && index > 0) {
    //    ret = CF.Evs[--index].PlayChord;  //get previous chord if this is null
    //  }
    //  return (clsChordEv)ret;
    //}

    protected void GetPlayChord(long ticks, int ticksmargin) {
      clsChordEvTimed nextplaychordbase;
      clsChordEvTimed ths = GetPlayChordBase(out nextplaychordbase, ticks, ticksmargin);
      clsChordEvTimed nxt = nextplaychordbase;
      int index = frmSC.CurrentEvIndex;
      while (ths == null && index > 0 && CF.Evs.Count >= frmSC.CurrentEvIndex) {
        ths = (clsChordEvTimed)CF.Evs[--index].PlayChord;  //get previous chord if this is null
      }
      //if (ths == null || MidiOn_ThisPlayChord == null || ths.OnTime > MidiOn_ThisPlayChord.OnTime) {
      MidiOn_ThisPlayChord = ths;
      //MidiOn_NextPlayChord = nxt;
      //}  //else leave alone
      //string txt = "clsPlay: GetPlayChord: MidiOn_ThisPlayChord = ";
      //foreach (int pc in MidiOn_ThisPlayChord.Chord) txt += pc + " ";
      //Debug.WriteLine(txt);
    }

    internal void ResetPlayChords() {
      MidiOn_ThisPlayChord = null;
      //MidiOn_NextPlayChord = null;
    }
  }
}
