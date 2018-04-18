#undef DebugStream

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace ChordCadenza {
  internal interface IFormPlayable {
    void RefreshBBT(clsMTime.clsBBT bbt);
    void StartSub(clsMTime.clsBBT bbt);
    //void StreamPlayOn();  //disable StartPlay, enable StopPlay, etc.
    //void StreamPlayOff();   //enable StartPlay, disable StopPlay, etc.
    //void StreamPlayDisable();   //enable/disable StartPlay, enable StopPlay, etc.
    //void SyncPlayOn();
    Button[] Cmds_Locate { get; }
    Button[] Cmds_Play_Midi { get; }
    Button[] Cmds_Play_And_Sync { get; }
    Button[] Cmds_Play_Audio { get; }
    Button[] Cmds_Sync_Audio { get; }
    Button[] Cmds_Pause { get; }
    Button[] Cmds_Stop { get; }
    //Button cmdRecSync { get; }
  }

  internal static class PlayableForms {
    internal static List<IFormPlayable> Active {
      get {
        List<IFormPlayable> list = new List<IFormPlayable>(3);
        if (IsActive(P.frmSC)) list.Add((IFormPlayable)P.frmSC);
        if (IsActive(P.F.frmChordMap)) list.Add((IFormPlayable)P.F.frmChordMap);
        if (IsActive(P.F.frmTrackMap)) list.Add((IFormPlayable)P.F.frmTrackMap);
        //if (IsActive(P.F.frmCompare)) list.Add((IPlayable)P.F.frmCompare);
        return list;
      }
    }

    private static bool IsActive(Form frm) {
      if (frm != null && frm is IFormPlayable && frm.IsHandleCreated) return true;
      return false;
    }

    internal static void CmdState_Playing() {  //was PlayOn
      bool sync = !(MidiPlay.Sync.indPlayActive == clsSync.ePlay.SyncActive 
        || MidiPlay.Sync.indPlayActive == clsSync.ePlay.SyncEnabled);
      bool syncrec = sync && clsAudioSync.Cmd == clsAudioSync.eCmd.Play;
      foreach (IFormPlayable pf in Active) {
        foreach (Button cmd in pf.Cmds_Locate) cmd.Enabled = false;
        foreach (Button cmd in pf.Cmds_Play_Midi) cmd.Enabled = false;
        foreach (Button cmd in pf.Cmds_Play_And_Sync) cmd.Enabled = false;
        foreach (Button cmd in pf.Cmds_Play_Audio) cmd.Enabled = false;
        foreach (Button cmd in pf.Cmds_Sync_Audio) cmd.Enabled = false;
        foreach (Button cmd in pf.Cmds_Stop) cmd.Enabled = sync;
        //foreach (Button cmd in pf.Cmds_Pause) cmd.Enabled = pausesync;
        foreach (Button cmd in pf.Cmds_Pause) cmd.Enabled = syncrec;
      }
    }

    internal static void CmdState_Stopped() {  //was PlayOff
      bool sync = (MidiPlay.Sync.indPlayActive == clsSync.ePlay.SyncActive
        || MidiPlay.Sync.indPlayActive == clsSync.ePlay.SyncEnabled);
      foreach (IFormPlayable pf in Active) {
        foreach (Button cmd in pf.Cmds_Locate)
          cmd.Enabled = !sync && P.F.Project.Name.Length > 0;
        foreach (Button cmd in pf.Cmds_Play_Midi)
          cmd.Enabled = !(MidiPlay.OutMStream is clsBassMidiOutNull) && (P.F.MidiFileLoaded);
        foreach (Button cmd in pf.Cmds_Play_And_Sync)
          cmd.Enabled = !clsBASSOutDev.Disconnected && !clsAudioSync.StaticEmpty;
        foreach (Button cmd in pf.Cmds_Play_Audio) cmd.Enabled = 
            !clsBASSOutDev.Disconnected && (P.F.AudioSync != null);
        foreach (Button cmd in pf.Cmds_Sync_Audio) cmd.Enabled = 
            !clsBASSOutDev.Disconnected && (P.F.AudioSync != null);
        foreach (Button cmd in pf.Cmds_Stop)
          cmd.Enabled = false;
        foreach (Button cmd in pf.Cmds_Pause)
          cmd.Enabled = false;
      }
      //if (Active.Contains(P.frmSC)) {
      //  P.frmSC.SetRecordColour(P.frmSC.cmdWipeAndRecord, false);
      //  P.frmSC.SetRecordColour(P.frmSC.cmdRecord, false);
      //}
    }

    internal static void CmdState_Disable() {  //was Disable
      foreach (IFormPlayable pf in Active) {
        foreach (Button cmd in pf.Cmds_Locate) cmd.Enabled = false;
        foreach (Button cmd in pf.Cmds_Play_Midi) cmd.Enabled = false;
        foreach (Button cmd in pf.Cmds_Play_And_Sync) cmd.Enabled = false;
        foreach (Button cmd in pf.Cmds_Stop) cmd.Enabled = false;
        foreach (Button cmd in pf.Cmds_Pause) cmd.Enabled = false;
      }
    }

    internal static void CmdState_Set() {
      if (P.F.FSTrackMap == null && P.F.AudioSync == null) {
        CmdState_Disable();
        return;
      }
      switch (MidiPlay.Sync.indPlayActive) {
        case clsSync.ePlay.MidiStream:
        case clsSync.ePlay.AudioStream:
          CmdState_Playing();
          return;
        case clsSync.ePlay.SyncActive:
          CmdState_Disable();
          return;
        case clsSync.ePlay.SyncEnabled:
          CmdState_Disable();
          return;
        case clsSync.ePlay.None:
          CmdState_Stopped();
          return;
        default:
          LogicError.Throw(eLogicError.X100);
          return;
      }
    }
  }

  internal class clsSync {

    //* after sequencer play start, should get:
    //* SPP, Start or Continue, Midiclocks..., Stop
    //* should get midiclock immediately after Start or Continue if on a beat
    //* should get Start only if playing from beginning of song  
    //* else get Continue
    //* SPP (Project Position Pointer) is position in midibeats
    //* midibeat = 1/16 note (16 midibeats per 4/4 bar)
    //* Midiclock = 1/24 of 1/4 note (96 midiclocks per 4/4 bar)
    //* 6 midiclocks = 1 midibeat
    //* Sonar: midiclocks continue after Stop and no longer playing anything!

    //* NOTE ********************************************************
    //* CHORDPLAY REQUIRES MIDICLOCKS 
    //* WILL NEED TO OUTPUT MIDICLOCKS ETC. FROM STREAM...
    //* *************************************************************

    internal enum eMidiMsgType { SPP, MidiClock, StreamEnded, StreamBASS, Start, Stop, Continue };
    //private int LeadInClocks = 0;
    internal int MidiClockCount = 0;
    internal int StartMidiClockCount = 0;
    internal int ChunkCount = 0;
    internal int StartChunkCount = 0;
    //private clsMTime.clsBBT StreamBB;
    internal clsMTime.clsBBT StreamStartBB;
    internal enum ePlay { None, SyncEnabled, SyncActive, MidiStream, AudioStream };
    internal ePlay indPlayActive = ePlay.None;
    internal delegate void delegBBT(clsMTime.clsBBT bbt);
    internal bool indPause = true;
    //internal clsBASSPlay BASSPlay { get { return P.F.PFPlay; } }
    internal clsWaitPlay WaitPlay { get { return P.F.WaitPlay; } }
    internal int BeatCount = 0;
    //internal bool MicrosoftMidiStreaming = false;
    internal Stopwatch StopWatch = new Stopwatch();
    //private System.Threading.Timer StopTimeOut;
    internal Object TimerLock = new Object();
    //private bool PlayCmdOn = false;

    private void DebugPlay() {
      LogicError.Throw(eLogicError.X999);
    }

    internal clsSync() {
      RefreshBBTTimer.Elapsed += MidiPlayTimer_Elapsed;
      RefreshBBTTimer.AutoReset = false;
    }

    //internal void StartRecord(Form playform, clsFileStream filestream, clsMute mute) {
    //  if (P.F.FileStreamMM != null) P.F.FileStreamMM.InitRec();
    //  StartPlayRecord(playform, filestream, mute);
    //}

    //internal void StartPlay(Form playform, clsFileStream filestream, clsMute mute) {
    //  StartPlayRecord(playform, filestream, mute);
    //}

    internal void StartPlay(Form playform, clsFileStream filestream, clsMute mute) {
      //* called from form thread
      //if (PlayCmdOn) DebugPlay();  //already playing?
      PlayableForms.CmdState_Stopped();  //before play starts - in case it fails
      RefreshBBTTimer.Stop();
      BBTQueue.Clear();

      //if (P.F.AutoSync != null && (!P.F.MidiFileLoaded || !P.frmStart.chkPlayMidiPriority.Checked)) {
      if (MidiPlay.OutMStream == null) return;
      P.F.Mute.StartPlay();
      //if (P.F.CurrentBBT.Bar >= new clsMTime.clsBBT(P.F.MaxTicks).Bar - 2) return;  //don't start too near the end
      if (P.F.CurrentBBT.Bar >= P.F.MaxBBT.Bar - 2) return;  //don't start too near the end
      if (/*clsPlay.PlayExists() &&*/ Forms.frmSC.MenuMonitor) {
        clsPlay.InitStopwatch();
      }
      if (P.frmSC.Play != null) {
        P.frmSC.Play.BBTSwitch = null;
      }
      ShowGCCollectionCounts("Start Play");
      P.F.WaitPlay = new clsWaitPlay(filestream, MidiPlay.OutMStream, mute);
      StreamStartBB = P.F.CurrentBBT.Copy();
      BeatCount = StreamStartBB.Bar;
      try {
        //lock (TimerLock) {
          P.F.WaitPlay.Start();
        //}
      }
      catch {
        DebugPlay();
        return;
      }

      //if (P.frmStart.chkPlaySustain.Checked) clsPlay.clsSustain.PlayPedalStatic(true);
      //clsPlayKeyboard.PlayNearestChordNote = P.frmStart.chkPlayKBChord.Checked;
      P.frmStart.StreamPlayOnAll();  //enable/disable StartPlay/StopPlay etc.
      P.frmStart.FormsStreamOnOff(true);   //enable/disable controls (not StartPlay/StopPlay etc.)
    }

    private static void ShowGCCollectionCounts(string txt) {
      /*
      Debug.WriteLine(DateTime.Now + " " + txt + " - GCCollectionCounts: "
        + GC.CollectionCount(0) + " "
        + GC.CollectionCount(1) + " "
        + GC.CollectionCount(2));
      */
    }

    internal void Pause() {
      //if (!PlayCmdOn) DebugPlay();
      //PlayCmdOn = false;
      PlayableForms.CmdState_Stopped();
      //foreach (IPlayable pf in PlayableForms.Active) {
      //  pf.StreamPlayOff();  //enable general controls
      //}

      indPause = true;
      //if (P.F.PFPlay != null && P.F.PFPlay.XPlay != null) {
      if (P.F.WaitPlay != null) {
        //ScheduleStopTimeOut();
        //lock (TimerLock) {
          P.F.WaitPlay.Stop();
          P.F.WaitPlay = null;
        //}
      } else if (P.F.AudioSync != null) {
        P.F.AudioSync.Pause();
        P.frmStart.StreamPlayOffAll();
      }

      if (P.frmSC != null) P.frmSC.nudStartBar.Value = 
          Math.Max(0, Math.Min(P.frmSC.nudStartBar.Maximum - 1, P.F.CurrentBBT.Bar + 1));
      //if (P.frmStart.chkPlaySustain.Checked) clsPlay.clsSustain.PlayPedalStatic(false);
      //if (P.frmStart.chkPlayKBChord.Checked) clsPlayKeyboard.PlayNearestChordNote = true;
      ShowGCCollectionCounts("Stop Play");
      //CheckMarkAndSave();
    }

    internal void Stop() {
      //if (!PlayCmdOn) DebugPlay();
      //PlayCmdOn = false;
      PlayableForms.CmdState_Stopped();
      //foreach (IPlayable pf in PlayableForms.Active) {
      //  pf.StreamPlayOff();  //enable general controls
      //}

      indPause = false;
      //if (P.F.PFPlay != null && P.F.PFPlay.XPlay != null) {
      if (P.F.WaitPlay != null) {
        //lock (TimerLock) {
          P.F.WaitPlay.Stop();
          P.F.WaitPlay = null;
        //}
      }
      if (P.F.AudioSync != null) {
        P.F.AudioSync.Stop();
        //P.frmStart.StreamPlayOffAll();
      }
      ShowGCCollectionCounts("Stop Play");
    }

    //private void ScheduleStopTimeOut() {
    //  StopTimeOut = new System.Threading.Timer(
    //    StopTimeOutCallback,
    //    null,
    //    2000,  //2000 msecs
    //    System.Threading.Timeout.Infinite);  //no periodic callbacks
    //}

    //private void StopTimeOutCallback(object state) {
    //  LogicError.Throw(eStopError.Y000, "Stream Stop Failure");
    //  P.frmStart.StreamPlayOffAll();
    //  StopTimeOut = null;
    //}

    //private void KillStopTimeOut() {
    //  if (StopTimeOut == null) return;
    //  StopTimeOut.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
    //}

    //private void CheckMarkAndSave() {
    //  if (P.F.frmShowChords == null || !P.F.frmShowChords.Play.NewEvsChanged) return;
    //  P.F.CFTxt.Evs = P.F.CFTxt.NewEvs;
    //  P.F.CFTxt.NewEvs = new List<clsCF.clsEv>();
    //  foreach (clsCF.clsEv ev in P.F.CFTxt.Evs) P.F.CFTxt.NewEvs.Add(new clsCF.clsEv(ev));  //copy
    //  P.F.frmShowChords.Play.NewEvsChanged = false;
    //  P.F.frmShowChords.Refresh();  
    //}

    private void StartSub() {
      BBTQueue.Clear();
      foreach (IFormPlayable pf in PlayableForms.Active) ((Form)pf).BeginInvoke(new delegBBT(pf.StartSub), P.F.CurrentBBT);
    }

    internal System.Timers.Timer RefreshBBTTimer = new System.Timers.Timer();
    internal Queue<clsMTime.clsBBT> BBTQueue = new Queue<clsMTime.clsBBT>();
    internal void RefreshBBT(clsMTime.clsBBT bbt) {
      if (Forms.frmSC.MenuMonitor /*&& clsPlay.PlayExists()*/) {
        lock (clsPlay.MonitorTimes) {
          StopWatch.Reset();
          StopWatch.Start();
          clsPlay.MonitorTimes.Add(int.MinValue);
          //int cnt = GC.CollectionCount(2);
          clsPlay.GCCnt0.Add(GC.CollectionCount(0));
          clsPlay.GCCnt1.Add(GC.CollectionCount(1));
          clsPlay.GCCnt2.Add(GC.CollectionCount(2));
        }
      }
      if (Cfg.LatencyMidiPlay <= 0) {
        P.frmSC.BeginInvoke(new delegBBT(Forms.frmStart.RefreshBBT), bbt);
      } else {
        BBTQueue.Enqueue(bbt.Copy());
        RefreshBBTTimer.Interval = Cfg.LatencyMidiPlay;
        RefreshBBTTimer.Start();
      }
    }

    private void MidiPlayTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
      if (BBTQueue.Count == 0) return;
      clsMTime.clsBBT bbt = BBTQueue.Dequeue();
      P.frmSC.BeginInvoke(new delegBBT(Forms.frmStart.RefreshBBT), bbt);
    }

    private void StartSync() {
      StartSub();
    }

    //private void LocateTicks(int ticks) {
    //  RefreshBBT(new clsMTime.clsBBT(ticks));
    //}

    //private void LocateBeat(int beatnum) {
    //  RefreshBBT(new clsMTime.clsBBT(beatnum, true));
    //}

    internal void CallBack(eMidiMsgType mtype) {
      //* called from sync or beat pedal
      CallBack(mtype, null, null, null);
    }

    internal void CallBack(eMidiMsgType mtype, clsFileStream filestream) {
      //* called from stream
      CallBack(mtype, null, filestream, null);
    }

    internal void CallBack(eMidiMsgType mtype, int val) {
      //* called from sync
      CallBack(mtype, val, null, null);
    }

    internal void CallBack(eMidiMsgType mtype, int? val, clsFileStream filestream, clsMTime.clsBBT BASSbbt) {
      //Debug.Write("*");
      //MTime = P.frmStart.GetMTime();
      //if (MTime == null) return;
      switch (mtype) {
        /* may want use in future???
        case eMidiMsgType.Beat:  //from sustain pedal - beats only (no bars)
          LocateBeat(BeatCount++);
          break;
        */
        case eMidiMsgType.SPP:
          //* sent by Sonar
          //* not sent by BIAB
          //if (indPlayActive != ePlay.None) return;
          //if (indPlayActive == ePlay.SyncEnabled) indPlayActive = ePlay.SyncActive;
          MidiClockCount = val.Value * 6;
          Debug.WriteLine("Callback: SPP: MidiClockCount = " + MidiClockCount);
          StartMidiClockCount = MidiClockCount;
          int ticksspp = (MidiClockCount * P.F.MTime.TicksPerQNote) / P.F.MTime.MidiClocksPerQNote;
          RefreshBBT(new clsMTime.clsBBT(ticksspp));
          //LocateBeat(MidiClockCount / MTime.MidiClocksPerBeat);
          break;
        //case clsMidiIn.eMidiMsgType.BarBeat:  //midistream
        //  LocateBeat(BeatCount++);
        //  break;
        case eMidiMsgType.StreamBASS:  //waitplay
          if (BASSbbt.TicksRemBeat == 0) {
            RefreshBBT(BASSbbt);
            //Debug.WriteLine("global refreshbbt = " + BASSbbt.ToStringBBT());
            //P.F.frmTrackMap.FileStream.RecordBeatNM();
            P.F.FSTrackMap.RecordBeatNM();
          }
          return;
        case eMidiMsgType.MidiClock:  //external sequencer with start/stop/continue/clock
          if (indPlayActive != ePlay.SyncActive && indPlayActive != ePlay.SyncEnabled) return;
          if (P.frmStart.chkMidiStartStop.Checked && indPlayActive != ePlay.SyncActive) return;
          //if (filestream == null) {  //sync
          //  if (!P.frmStart.chkMidiStartStop.Checked) return;
          //  if (indPlayActive == ePlay.Stream) return;
          //}

          //{
          //  int ticks = (MidiClockCount * P.F.MTime.TicksPerQNote) / P.F.MTime.MidiClocksPerQNote;
          //  Debug.WriteLine("MidiClockCount = " + MidiClockCount
          //    + " Ticks = " + ticks
          //    + " QNotes*100 = " + (ticks * 100) / P.F.MTime.TicksPerQNote);
          //}

          int ticks = (MidiClockCount * P.F.MTime.TicksPerQNote) / P.F.MTime.MidiClocksPerQNote;
          if (ticks <= P.F.MaxBBT.Ticks) {
            if (ticks >= 0) {
              clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
              //if (bbt.TicksRemBeat == 0 && MidiClockCount > (StartMidiClockCount + LeadInClocks)) {
              if (bbt.TicksRemBeat == 0 && MidiClockCount > StartMidiClockCount) {
                //P.F.CurrentBBT = bbt;
                RefreshBBT(bbt);
              }
              Debug.WriteLine("Callback: MidiClock: MidiClockCount = " + MidiClockCount);

              //if (((MidiClockCount % MTime.MidiClocksPerBeat) == 0)  //next beat
              //&& (MidiClockCount > StartMidiClockCount + LeadInClocks)) {
              //  LocateBeat(MidiClockCount / MTime.MidiClocksPerBeat);
              //}
            }
            MidiClockCount++;
          }
          return;
        case eMidiMsgType.StreamEnded:
          if (!indPause) {
            //P.F.CurrentBBT = new clsMTime.clsBBT(0);
            P.frmSC.Play?.NewReset();
            RefreshBBT(new clsMTime.clsBBT(P.F.StartBar, 0, 0));
            indPause = true;
          }
          return;
        case eMidiMsgType.Stop:
          if (MidiPlay.MidiInSync != null) indPlayActive = ePlay.SyncEnabled;
          return;
        case eMidiMsgType.Start:
          //* should only be called if starting from beginning of song
          //* BIAB sends this whenever song is started, from any position
          if (P.frmStart.chkMidiStartStop.Checked) {
            if (indPlayActive == ePlay.SyncEnabled) indPlayActive = ePlay.SyncActive;
            //StartMidiClockCount = 0;
            //LeadInClocks = P.F.MTime.GetTSig(0).MidiClocksPerBar * (int)P.frmStart.nudLeadInBars.Value;
            //MidiClockCount = -LeadInClocks;
            //P.F.CurrentBBT = new clsMTime.clsBBT(0);
            //StartSync();
          }
          return;
        case eMidiMsgType.Continue:  //does not appear to be sent by BIAB
          if (P.frmStart.chkMidiStartStop.Checked) {
            if (indPlayActive == ePlay.SyncEnabled) indPlayActive = ePlay.SyncActive;
            StartSync();
          }
          return;
      }
    }
  }
}
