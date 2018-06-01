using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
//using Un4seen.Bass;
//using Un4seen.Bass.AddOn.Mix;
using ManagedBass;
using System.IO;
using static ChordCadenza.clsBassOutMidi;  //CheckOK etc.

/* 16/10/2017
  cmdPlay
    locate redline to start point
    cmdStartPlay
      play mp3
      if startpoint not synced, goto prev active or beat 0
    switchkey not used
    cmdStop (stop mp3)
  
  cmdRecord
    locate redline to start play point
    locate red rectangle to start record point
    cmdRecord
      start playing (no sync)
    first switchkey
      record first beat
      red highlight on cmdRecord
    other switchkeys
      record beat
      move to next beat (skip if necessary)

  cmdPlayAndRecord
    like play, but wait for sync key at red rectangle

*/

namespace ChordCadenza {
  internal partial class clsMP3Bass {
    protected clsAudioSync AudioSync;
    internal bool indSave = false;

    protected bool CheckCmdSyncSwitchKey() {
      string key = Forms.frmSwitch.ActionToKey["Sync"];
      if (key == "None" || key == "") {
        string msg = "Switch Key not set.";
        MessageBox.Show(msg, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        return false;
      }
      return true;
    }

    //internal virtual bool CheckCmdSync() {  //base play or record
    //  if (AutoSync.indRecord && !AutoSync.Record_LastUsed.SequenceEqual(AutoSync.Elapsed.Record)) {
    //    if (MessageBox.Show("Record Track has not yet been merged or copied - continue?",
    //      "Record Track Warning", MessageBoxButtons.YesNo) == DialogResult.No) return false;
    //    else AutoSync.Record_LastUsed = AutoSync.Elapsed.Record.ToList();
    //  }

    //  clsMTime.clsBBT firstsync = AutoSync.Elapsed.GetFirstBBTPlay();
    //  if (P.F.StartBar == 0 && firstsync != null) {
    //    CurrentBBT = firstsync;
    //  } else {
    //    CurrentBBT = new clsMTime.clsBBT(P.F.StartBar, 0, 0);
    //  }

    //  Forms.frmStart.RefreshBBT(CurrentBBT);
    //  return true;
    //}

    protected clsMTime.clsBBT CurrentBBT {
      get { return AudioSync.CurrentBBT; }
      set { AudioSync.CurrentBBT = value; }
    }

    protected clsAudioSync.clsElapsed Elapsed {
      get { return AudioSync.Elapsed; }
      set { AudioSync.Elapsed = value; }
    }

    //private clsMTime.clsBBT BarPaneBBTLo {
    //  get { return P.F.BarPaneBBTLo; }
    //}

    //private clsMTime.clsBBT BarPaneBBTHi {
    //  get { return P.F.BarPaneBBTHi; }
    //}

    protected long OnMP3Timer() {
      AudioSync.RefreshBBT();
      int? beat = Elapsed.GetNextActiveBeat(CurrentBBT.Beats);
      if (!beat.HasValue) return -1;
      long nextpos = Elapsed.Play[beat.Value];
      CurrentBBT = new clsMTime.clsBBT(beat.Value, true);  //refreshBBT on next OnMP3Timer call
      return nextpos;
    }

    internal int StreamHandle;
    private SyncProcedure delegOnEnd;
    private SyncProcedure delegOnPos;
    internal string FilePath;
    internal long StartPosBytes = 0;

    //internal clsMTime.clsBBT AudioPlayStartBBT {  //Bar pane (blue highlight)
    //  get {
    //    //return Forms.frmTrackMap.BarPaneBBT;
    //    return P.F.CurrentBBT;
    //  }
    //}

    internal clsMP3Bass(clsAudioSync autosync, string filepath) {
      AudioSync = autosync;
      FilePath = filepath;
      OpenStream();
      delegOnEnd = new SyncProcedure(OnEnd);
      delegOnPos = new SyncProcedure(OnPos);
      P.frmSC.panTrkAudio.Enabled = true;
      Vol = P.frmSC.trkAudioVol.Value;
      Pan = P.frmSC.trkAudioPan.Value;
    }

    private void OpenStream() {
      if (P.BASSOutDev != null) P.BASSOutDev.ConnectFile(this);
    }

    public void CloseStream() {
      if (P.BASSOutDev != null) P.BASSOutDev.DisconnectFile(this);
    }

    private void OnEnd(int syncHandle, int channel, int data, IntPtr user) {
      lock (AudioSync) {
        EndSyncHandle = 0;
        MidiPlay.Sync.indPause = false;
        AudioSync.FinalizeStop(false);
        CurrentBBT = new clsMTime.clsBBT(P.F.StartBar, 0, 0);
        P.frmSC.Play?.NewReset();
        AudioSync.RefreshBBT();
      }
    }

    private void OnPos(int syncHandle, int channel, int data, IntPtr user) {
      lock (AudioSync) {
        if (PosSyncHandle == 0) return;  //set to zero by RemovePosSync()
        PosSyncHandle = 0;
        //Debug.WriteLine("clsMP3Bass: OnPos: Beats = " + CurrentBBT.Beats);
        if (CurrentBBT.Beats >= Elapsed.Play.Count) {
          //PlayableForms.CmdState_Stopped();
          //MidiPlay.Sync.indPause = false;
          //AudioSync.Stop();
          return;
        }
        long userpos = user.ToInt64();
        if (Elapsed.Play[CurrentBBT.Beats] != userpos) {
          //* should not happen!!!
          //Debug.WriteLine("clsMP3Bass: OnPos: Beats = " + CurrentBBT.Beats
          //  + " currentpos = " + Elapsed.Play[CurrentBBT.Beats]
          //  + " userpos = " + userpos);
          int newbeat = CurrentBBT.Beats; 
          //* try to get actual beat corresponding to userpos
          while (Elapsed.Play[newbeat] > userpos) newbeat--;
          while (Elapsed.Play[newbeat] < userpos) newbeat++;
          if (newbeat != CurrentBBT.Beats) CurrentBBT = new clsMTime.clsBBT(newbeat, true);
        }
        if (clsAudioSync.Cmd == clsAudioSync.eCmd.PlayAndRecord
        && CurrentBBT.Beats >= P.F.BarPaneBBTLo.Beats) {  //wait for switchkey
          RemovePosSync();
          return;
        }
        long sigpos = OnMP3Timer();
        if (sigpos <= 0) return;
        SetPosSync(sigpos);
      }
    }

    //* play or record
    private void PlayFile(long startposbytes, long? sigposbytes) {  
      //////if (startposbytes < 0) return;  commented out to support audio pause (no sync)
      StartPosBytes = startposbytes;
      PlayFile(-1, startposbytes, sigposbytes);
    }

    //* record
    private void PlayFile(double startposseconds, long? sigposbytes) { 
      if (startposseconds < 0) return;
      StartPosBytes = Bass.ChannelSeconds2Bytes(StreamHandle, startposseconds);
      PlayFile(startposseconds, -1, sigposbytes);
    }

    private void PlayFile(double startposseconds, long startposbytes, long? sigposbytes) {
      //StreamHandle = clsBassOut.CheckHandle(Bass.BASS_StreamCreateFile(FilePath, 0, 0, BASSFlag.BASS_STREAM_PRESCAN));
      if (startposbytes >= 0) {
        CheckOK(Bass.ChannelSetPosition(StreamHandle, startposbytes));
      } else if (startposseconds >= 0) {
        long bytes = Bass.ChannelSeconds2Bytes(StreamHandle, startposseconds);
        CheckOK(Bass.ChannelSetPosition(StreamHandle, bytes));
      }
      if (P.BASSOutDev is clsBASSOutDevAsio) {  //ASIO
      } else { //non-ASIO
        CheckOK(Bass.ChannelPlay(StreamHandle, false));
      }

      SetEndSync();
      if (sigposbytes.HasValue) SetPosSync(sigposbytes.Value);
    }

    internal bool Stopped {
      //* return false if playing, paused, or stalled
      get {
        if (StreamHandle == 0) return false;
        return (Bass.ChannelIsActive(StreamHandle) == PlaybackState.Stopped);
      }
    }

    public void StopPlay() {
      if (StreamHandle == 0) {
        Debug.WriteLine("clsMP3Bass.StopPlay: Error: StreamHandle is zero");
        return;
      }
      CheckOK(Bass.ChannelStop(StreamHandle));
    }

    public void PausePlay() {
      //* this may fail in dev (32-bit) but seems to work in release!!!
      if (StreamHandle == 0) {
        Debug.WriteLine("clsMP3Bass.PausePlay: Error: StreamHandle is zero");
        return;
      }
      CheckOK(Bass.ChannelPause(StreamHandle));
    }

    public bool IsPaused() {
      if (StreamHandle == 0) {
        Debug.WriteLine("clsMP3Bass.PausePlay: Error: StreamHandle is zero");
        return false;
      }
      PlaybackState active = Bass.ChannelIsActive(StreamHandle);
      return (active == PlaybackState.Paused);
    }

    public double GetPosSeconds() {
      if (StreamHandle == 0) return 0;
      long bytes = Bass.ChannelGetPosition(StreamHandle);
      return Bass.ChannelBytes2Seconds(StreamHandle, bytes);  //may be approximate
    }

    public long GetPosBytes() {  //bytes
      if (StreamHandle == 0) return 0;
      return Bass.ChannelGetPosition(StreamHandle);
    }

    public long GetSeconds2Units(double secs) {  //seconds to bytes
      return Bass.ChannelSeconds2Bytes(StreamHandle, secs);  //may be approximate
    }

    public double GetUnits2Seconds(long bytes) {  //bytes to seconds
      return Bass.ChannelBytes2Seconds(StreamHandle, bytes);  //may be approximate
    }

    public void SetPosSeconds(double secs) {
      if (StreamHandle == 0) return;
      if (Bass.ChannelIsActive(StreamHandle) == PlaybackState.Playing) return;
      if (secs > GetDurationSeconds()) return;
      long bytes = Bass.ChannelSeconds2Bytes(StreamHandle, secs);
      CheckOK(Bass.ChannelSetPosition(StreamHandle, bytes));
      CheckOK(Bass.ChannelStop(StreamHandle));
    }

    public double GetDurationSeconds() {  //seconds
      if (StreamHandle == 0) return 0;
      long bytes = Bass.ChannelGetLength(StreamHandle);
      return Bass.ChannelBytes2Seconds(StreamHandle, bytes);
    }

    public long GetDurationBytes() {  //seconds
      if (StreamHandle == 0) return 0;
      return Bass.ChannelGetLength(StreamHandle);
    }

    internal int GetSampleRate() {
      if (StreamHandle == 0) return 0;
      ChannelInfo info = Bass.ChannelGetInfo(StreamHandle);
      return info.Frequency;
    }

    public int Vol {  //0-100
      set {
        if (StreamHandle == 0) return;
        Bass.ChannelSetAttribute(StreamHandle, ChannelAttribute.Volume, (float)value / 100);
      }
      get {
        float val = 1.0f;
        if (StreamHandle == 0) return 50;
        CheckOK(Bass.ChannelGetAttribute(StreamHandle, ChannelAttribute.Volume, out val));
        return (int)(val * 100);
      }
    }

    public int Pan {  //-50 - +50
      set {
        if (StreamHandle == 0) return;
        Bass.ChannelSetAttribute(StreamHandle, ChannelAttribute.Pan, (float)value / 50
          );
      }
      get {
        float val = 0f;
        if (StreamHandle == 0) return 0;
        CheckOK(Bass.ChannelGetAttribute(StreamHandle, ChannelAttribute.Pan, out val));
        return (int)(val * 50);
      }
    }

    public void Free() {
      RemovePosSync();
      RemoveEndSync();
      clsBassOutMidi.CheckOKSoft(Bass.ChannelStop(StreamHandle));
      clsBassOutMidi.CheckOKSoft(Bass.StreamFree(StreamHandle));
      StreamHandle = 0;
    }

    public long GetStartPosUnits() {
      return StartPosBytes;
    }

    public void RemovePosSync() {
      //* avoid calling BASS_ChannelRemoveSync - doesn't seem to work?
      PosSyncHandle = 0;
    }

    internal bool CheckCmdSync() {  //internal play or record
      //if (!AutoSync.indRecord) P.F.StartBar = P.F.CurrentBBT.Bar;
      if (clsAudioSync.Cmd != clsAudioSync.eCmd.Record) P.F.StartBar = P.F.CurrentBBT.Bar;

      if (clsAudioSync.Cmd != clsAudioSync.eCmd.Play && !AudioSync.Elapsed.RecordIsEmpty) {
        if (MessageBox.Show("Audio Record Track not moved/merged/cleared - - clear it and continue?",
          MessageBoxButtons.YesNo) == DialogResult.No) return false;
        //* clear record track
        AudioSync.UpdateUndo();
        AudioSync.Elapsed.ResetRecord();
        P.F?.frmAutoSync?.ShowList();
        if (P.F?.frmTrackMap != null) P.F.frmTrackMap.picBars.Refresh();
        if (P.F?.frmChordMap != null) P.F.frmChordMap.picBars.Refresh();
      }

      //clsMTime.clsBBT firstsync = AutoSync.Elapsed.GetFirstBBTPlay();
      //if (P.F.StartBar == 0 && firstsync != null) {
      //  CurrentBBT = firstsync;
      //} else {
      //  CurrentBBT = new clsMTime.clsBBT(P.F.StartBar, 0, 0);
      //}
      CurrentBBT = new clsMTime.clsBBT(P.F.StartBar, 0, 0);
      P.frmSC.Play?.NewReset();
      Forms.frmStart.RefreshBBT(CurrentBBT);

      if (clsAudioSync.Cmd != clsAudioSync.eCmd.Play) { //(AutoSync.indRecord) {
        if (!CheckCmdSyncSwitchKey()) return false;
        if (CurrentBBT.Bar > P.F.BarPaneBBTLo.Bar) {

          string msg = "The sync start bar (highlighted and set in the Bar Pane) must be at or after the";
          msg += " start play position (Current Cursor position).";
          msg += "\r\n\r\nYou need to relocate one of these before synchronising is possible.";

          //string msg = "\r\nYou need to set an Audio Start Bar that is at or before the Current Cursor Position,";
          //msg += " using the BarPane of the ChordMap or TrackMap,";
          //msg += " or set the Current Cursor Position after the Audio Start Bar.";

          MessageBox.Show(msg, MessageBoxButtons.OK, MessageBoxIcon.Stop);
          return false;
        }
      }

      if (clsAudioSync.Cmd != clsAudioSync.eCmd.Record && !IsPaused()) { 
        //if (CurrentBBT.Beats > 0 && !AutoSync.IsEmpty() 
        if (CurrentBBT.Beats > 0 && clsAudioSync.StaticAudioSyncEna 
        && (Elapsed.Play.Count <= CurrentBBT.Beats || Elapsed.Play[CurrentBBT.Beats] == 0)) {
          if (P.F.frmAutoSync == null || !P.F.frmAutoSync.trkPos.Enabled || !P.F.frmAutoSync.chkStartRecPos.Checked) {
            string msg = "\r\nUnable to locate to Current Cursor Position.";
            MessageBox.Show(msg, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return false;
          }
        }
      }

      //if (P.frmSC.Play != null) {
      //  P.frmSC.Play.DeferredKB = null;
      //  P.frmSC.Play.ChordNoteCount = 0;
      //}

      return true;
    }

    internal void StartSync() {
      long startpos;
      if (IsPaused()) {
        startpos = -1;
      } else if (!clsAudioSync.StaticAudioSyncEna) {
        startpos = 0;
      } else {
        startpos = (P.F.CurrentBBT.Beats < Elapsed.Play.Count
          && Elapsed.Play[P.F.CurrentBBT.Beats] > 0) ?
          Elapsed.Play[P.F.CurrentBBT.Beats] : 
          Elapsed.GetPrevActivePos(P.F.CurrentBBT.Beats);
      }
   
      int? nextbeat = null;
      //long? playsigbytes = (!P.frmStart.chkNoAudioSync.Checked && AutoSync.Cmd != clsAutoSync.eCmd.Record) ?
      long? playsigbytes = (clsAudioSync.StaticAudioSyncEna && clsAudioSync.Cmd != clsAudioSync.eCmd.Record) ?
        Elapsed.GetNextActive(CurrentBBT.Beats, out nextbeat) : null;
      try {
        if (P.F.frmAutoSync != null && !IsPaused() 
        && P.F.frmAutoSync.trkPos.Enabled && P.F.frmAutoSync.chkStartRecPos.Checked) {
          long pos = GetSeconds2Units(P.F.frmAutoSync.trkPos.Value);
          int index = Elapsed.GetPlayIndex(pos) + 1;  //one after next
          //if (!P.frmStart.chkNoAudioSync.Checked) {
          if (clsAudioSync.StaticAudioSyncEna) {
            playsigbytes = (index < Elapsed.Play.Count) ? Elapsed.Play[index] : (long?)null;
          }
          PlayFile((double)P.F.frmAutoSync.trkPos.Value, playsigbytes);
        } else {
          PlayFile(startpos, playsigbytes);  //play and wait for switchkey
        }
        P.F.frmAutoSync?.StartPlay();
      }
      catch (AudioIOException) {
        return;
      }

      MidiPlay.Sync.RefreshBBTTimer.Stop();
      MidiPlay.Sync.BBTQueue.Clear();

      //* refreshBBT on next OnMP3Timer call
      if (nextbeat.HasValue) CurrentBBT = new clsMTime.clsBBT(nextbeat.Value, true);

      if (P.F.frmAutoSync != null) P.F.frmAutoSync.UpdateCurrentPos();
      P.frmStart.StreamPlayOnAll();  //enable/disable StartPlay/StopPlay etc.
      P.frmStart.FormsStreamOnOff(true);   //enable/disable controls (not StartPlay/StopPlay etc.)
    }

    public void SwitchKey() {
      lock (P.F.AudioSync) {
        if (!clsAudioSync.StaticMP3Playing || clsAudioSync.Cmd == clsAudioSync.eCmd.Play) return;
        if (clsBASSOutDev.Disconnected) return;
        long posbytes = GetPosBytes();
        using (new clsPlay.clsMonitor()) {
          if (clsAudioSync.indFirstSwitchKey) {  //assumes CurrentBBT.BeatRemBars = 0
            RemovePosSync();
            Elapsed.ResetRecord();
            //AutoSync.SetCmdRecordActive();
            clsMTime.clsBBT bbt = P.F.BarPaneBBTLo;
            if (clsAudioSync.Cmd == clsAudioSync.eCmd.PlayAndRecord
            && P.F.BarPaneBBTHi.Bar > P.F.BarPaneBBTLo.Bar
            && CurrentBBT.Beats == P.F.BarPaneBBTLo.Beats) {
              for (int b = P.F.BarPaneBBTLo.Beats; b <= P.F.BarPaneBBTHi.Beats; b++) {
                Elapsed.SetRecord(b, -1);  //jump
              }
              bbt = P.F.BarPaneBBTHi;  //jump to this
            }
            Elapsed.SetRecord(bbt.Beats, posbytes);
            CurrentBBT = bbt.Copy();
          } else {
            //AutoSync.SetCmdRecordInactive();
            CurrentBBT.NextBeat();
            Elapsed.SetRecord(CurrentBBT.Beats, GetPosBytes());
            indSave = true;
          }
        }
        clsAudioSync.indFirstSwitchKey = false;
        AudioSync.RefreshBBT();
        if (P.F.frmAutoSync != null) {
          P.F.frmAutoSync.BeginInvoke(new clsAudioSync.delegUpdateCurrentPos(P.F.frmAutoSync.UpdateCurrentPos));
        }
      }
    }
  }
}

//* to be deleted...
////* check if we need to go back a beat
//int beat = CurrentBBT.Beats;
//long posbytes = GetPosBytes();
//long diffthis = posbytes - Elapsed.Play[beat];
//if (Elapsed.Play.Count > beat + 1 && Elapsed.Play[beat + 1] > 0 && Elapsed.Play[beat] > 0) {
//  long diffnext = Elapsed.Play[beat + 1] - posbytes;
//  if (diffthis < diffnext) CurrentBBT.PrevBeat();
//  Debug.WriteLine("clsAutoSync: P&R: beat = " + beat + " diffthis = " + diffthis + " diffnext = " + diffnext);
//}

