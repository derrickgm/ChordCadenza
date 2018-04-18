using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

/*
 NOTE: everything here that is beat, is bar in clsAutoSyncBar!!!
.chtc file format (Beat):
  one line per beat, starting beat 1(0)
  <WMP position (msecs)>  
  start beat and before: elapsed = 0

clsElapsed
  one entry for each beat

Sync on beat

cmdPlaySync (play)
  * no support for "Align" - use latency (if necessary)
  * just play until end or stop

  load .chtc file (non-empty)
  locate to first non-zero beat
  play MP3 from start
  wait for first non-zero beat msecs (WMP)
  ^  locate to beat (again)
  |  set sigpos to next beat msecs
  | wait for beat (WMP)

cmdPlaySync with Modifier key (record)
  * record sync times
  * locate to first sync bar before cmdPlaySync
  
  load .chtc file (may be empty)
  play mp3 from start
  on stop, check that next beat is not 0 (from a previous recording)

SwitchKey
  only active during record
  set current beat to WMP.Position (msecs)
  goto next beat

clsElapsed
  beats!!!

cmdRecSync
  active whenever cmdPlay active (.chp file present, not currenty playing)
  messagebox if no .mp3 file
  create empty .chtc file if none exists

cmdPlaySync
  active if cmdPlay active and clsAutoSync exists (+ Elapsed exists (and not empty))

RECORD
  empty Elapsed.Active
  record to Elapsed.Active 
    options (if Elapsed.Active not empty)
      cmd: merge Elapsed.Active with Elapsed.File 
      cmd: copy Elapsed.Active to Elapsed.File (overwrite) 
      checkbox: play Elapsed.Active (to check it)
      showsummary columns: Elapsed.Active and Elapsed.File
      (else will be discarded after next record or reload)
    NOTE: default play Elapsed.File (not Active)
*/

namespace ChordCadenza {
  internal partial class clsAudioSync {
    internal clsElapsed Elapsed;
    //private clsElapsed Elapsed_File; 
    //internal List<long> Play_LastSaved;
    //internal List<long> Record_LastUsed;  //since last Merge, Copy, Clear
    internal clsMP3Bass MP3Player;
    //private clsWMP WMP;
    //private clsBassMP3 BassMP3;
    //private bool indStartSync = true;
    internal static bool indFirstSwitchKey = true;
    //internal bool indRecord = false;
    internal enum eCmd { Play, Record, PlayAndRecord };
    internal static eCmd Cmd;
    protected bool _indMP3Playing = false;  //play or record
    private clsF.clsindSave _indSave = new clsF.clsindSave();

    internal static bool StaticMP3Playing {
      get {
        if (P.F == null || P.F.AudioSync == null) return false;
        return P.F.AudioSync._indMP3Playing;
      }
    }

    //internal bool indSave {
    //  get {
    //    _indSave.Ind = !MatchesFile();
    //    return _indSave.Ind;
    //  }
    //}

    internal bool indSave {
      get {
        return _indSave.Ind;
      }
      set {
        _indSave.Ind = value;
      }
    }

    //private bool MatchesFile() {
    //  if (Elapsed.PlayIsEmpty != Elapsed_File.PlayIsEmpty) return false;
    //  if (Elapsed.RecordIsEmpty != Elapsed_File.RecordIsEmpty) return false;

    //  if (Elapsed.PlayIsEmpty && Elapsed_File.PlayIsEmpty) return true;
    //  if (Elapsed.RecordIsEmpty && Elapsed_File.RecordIsEmpty) return true;

    //  if (!Elapsed.Play.SequenceEqual(Elapsed_File.Play)) return false;
    //  if (!Elapsed.Record.SequenceEqual(Elapsed_File.Record)) return false;

    //  return true;
    //}

    //private clsMTime.clsBBT BarPaneBBTHi {
    //  get { return Forms.frmTrackMap.BarPaneBBTHi; }
    //}

    //internal string MP3FilePath;
    //private bool indWMP = false;  //WMP or BassMP3
    //private int BeatOffset = 0;
    //private int MaxBeatMP3;
    private delegate void delegUpdatePos(long bytes);
    internal delegate void delegUpdateCurrentPos();

    internal clsAudioSync(string audiofilepath) {   //audiofilepath used as flag
      //* create default .chtc file
      BaseConstructor();
      //CHTFilePath = P.F.Project.AudioSyncPath;
      InitMP3Player(P.F.Project.AudioPath);
      Elapsed = new clsElapsed(this);
      SetPlayAudioText(this);
    }

    internal clsAudioSync(string audiofilepath, List<string> lines) {  //audiofilepath used as flag
      //* load .chtc file
      BaseConstructor();
      InitMP3Player(P.F.Project.AudioPath);
      Elapsed = new clsElapsed(this, lines);
      SetPlayAudioText(this);
    }

    //internal clsAutoSync(List<string> lines) {
    //  //* load .chtx file
    //  BaseConstructor();
    //  //CHTFilePath = P.F.Project.AudioSyncPath;
    //  Elapsed = new clsElapsed(this, lines);
    //  InitMP3Player();
    //  Play_LastSaved = Elapsed.Play.ToList();
    //  Record_LastUsed = Elapsed.Record.ToList();
    //  P.frmSC.lblTopSync.Text = "Sync: External";
    //}

    //internal clsAutoSync() {
    //  //* create default .chtx file
    //  BaseConstructor();
    //  //CHTFilePath = P.F.Project.AudioSyncPath;
    //  Elapsed = new clsElapsed(this);
    //  InitMP3Player();
    //  Play_LastSaved = Elapsed.Play.ToList();
    //  Record_LastUsed = Elapsed.Record.ToList();
    //  P.frmSC.lblTopSync.Text = "Sync: External";
    //}

    //internal void CheckIndChanged(FormClosingEventArgs e) {
    //  if (indSave) {
    //    DialogResult res = MessageBox.Show("AutoSync.Play File has unsaved changes - Save it?",
    //      "Unsaved Changes Warning",
    //      MessageBoxButtons.YesNoCancel);
    //    switch (res) {
    //      case DialogResult.No:
    //        e.Cancel = false;
    //        break;
    //      case DialogResult.Yes:
    //        if (SaveFile() != "") {  //error
    //          e.Cancel = true;
    //          return;
    //        }
    //        e.Cancel = false;
    //        break;
    //      case DialogResult.Cancel:
    //        e.Cancel = true;
    //        return;
    //    }
    //  }
    //  indSave = false;
    //}

    internal static void SetPlayAudioText(clsAudioSync audiosync) {
      string txt = "Play\r\nAudio";
      if (audiosync?.Elapsed == null || !audiosync.AudioSyncEna || P.F.Project.AudioPath == "") {
        txt = "Play\r\nAudio\r\nNoSyn";
        if (P.frmSC != null) P.frmSC.chkManSyncChord.Enabled = true;  
      } else {
        if (P.frmSC != null) {
          P.frmSC.chkManSyncChord.Enabled = false;  
          P.frmSC.chkManSyncChord.Checked = false;  
        }
      }
      if (P.frmSC != null) P.frmSC.cmdPlayAudio.Text = txt;
      if (P.F?.frmTrackMap != null) P.F.frmTrackMap.cmdPlayAudio.Text = txt;
      if (P.F?.frmChordMap != null) P.F.frmChordMap.cmdPlayAudio.Text = txt;
    }

    internal void InitMP3Player(string audiofilepath) {  //internal (mp3)
      MP3Player = new clsMP3Bass(this, audiofilepath);
    }

    //private void InitMP3Player() {  //external
    //  MP3Player = new clsMP3Ext(this);
    //}

    //Elapsed.MP3Resolution = (int)P.frmStart.nudMP3Res.Value * 1000;
    //if (Elapsed.Play.Count < 2 || Elapsed.Play[0] != 0) {
    //  FinalizeStop();
    //  return;
    //}

    //CurrentBBT = P.F.CurrentBBT.Copy();
    //int startbeat = CurrentBBT.Beats;
    ////int playstartbeat = (Elapsed.Play[startbeat] > 0) ? startbeat : Elapsed.GetPrevActiveBeat(startbeat);
    //long playstartpos = (Elapsed.Play[startbeat] > 0) ? 
    //  Elapsed.Play[startbeat] : Elapsed.GetPrevActivePos(startbeat);
    //clsMTime.clsBBT bbt = new MPlay.clsMTime.clsBBT(startbeat, true);
    //StartBar = bbt.Bar;

    //int? playsigbeat = Elapsed.GetNextActiveBeat(startbeat);
    //if (!playsigbeat.HasValue) {
    //  FinalizeStop();
    //  return;
    //}

    //MP3Player.PlayFile(this, playstartpos, Elapsed.Play[playsigbeat.Value]);  //wait for OnWMPTimer() trigger
    //if (P.F.frmAutoSync != null) P.F.frmAutoSync.UpdateCurrentPos();
    ////indStartSync = true;
    //indMP3Playing = true;
    //P.frmStart.StreamPlayOnAll();  //enable/disable StartPlay/StopPlay etc.
    //P.frmStart.FormsStreamOnOff(true);   //enable/disable controls (not StartPlay/StopPlay etc.)
    //CurrentBBT = new MPlay.clsMTime.clsBBT(playsigbeat.Value, true);  //refreshBBT on next OnMP3Timer call
    ////Debug.WriteLine("AutoSyncBeat: StartSyncPlay: CurrentBBT.Beats = " + CurrentBBT.Beats);
    //return;
    //}

    internal void StartCmdSyncPlay() {  //play
      Cmd = eCmd.Play; //indRecord = false;
      if (P.F.AudioSync != null) {
        if (!MP3Player.CheckCmdSync()) return;
      }
      if (P.frmSC.Play != null) P.frmSC.Play.BBTSwitch = null;
      lock (this) {
        _indMP3Playing = true;
        StartBar = P.F.CurrentBBT.Bar;
        indFirstSwitchKey = true;
        //clsPlay.indFirstSwitchChordBarBeat = true;
        MP3Player.StartSync();
      }
    }

    internal void StartCmdSyncRecord() {  //record
      Cmd = eCmd.Record; //indRecord = true;
      if (!MP3Player.CheckCmdSync()) return;
      if (P.frmSC.Play != null) P.frmSC.Play.BBTSwitch = null;
      if (Forms.frmSC.MenuMonitor) clsPlay.InitStopwatch();

      lock (this) {
        _indMP3Playing = true;
        StartBar = P.F.CurrentBBT.Bar;
        indFirstSwitchKey = true;
        //clsPlay.indFirstSwitchChordBarBeat = true;
        UpdateUndo();  //could go in first SwitchKey() ...
        MP3Player.StartSync();
      }
    }

    internal void StartCmdSyncPlayAndRecord() {
      Cmd = eCmd.PlayAndRecord;
      if (!MP3Player.CheckCmdSync()) return;
      if (P.frmSC.Play != null) P.frmSC.Play.BBTSwitch = null;
      if (Forms.frmSC.MenuMonitor) clsPlay.InitStopwatch();

      lock (this) {
        _indMP3Playing = true;
        StartBar = P.F.CurrentBBT.Bar;
        indFirstSwitchKey = true;
        //clsPlay.indFirstSwitchChordBarBeat = true;
        UpdateUndo();  //could go in first SwitchKey() ...
        MP3Player.StartSync();
      }
    }

    //internal void SwitchKey() {  
    //  lock (this) {
    //    MP3Player.SwitchKey();
    //  }
    //}

    //internal void SetCmdRecordActive() {
    //  foreach (IFormPlayable frm in PlayableForms.Active) {
    //    if (frm.cmdRecSync == null || !frm.cmdRecSync.Enabled) continue;
    //    frm.cmdRecSync.BackColor = Color.Red;
    //    frm.cmdRecSync.UseVisualStyleBackColor = false;
    //  }
    //}

    //internal void SetCmdRecordInactive() {
    //  //* normal colour
    //  foreach (IFormPlayable frm in PlayableForms.Active) {
    //    if (frm.cmdRecSync == null || !frm.cmdRecSync.Enabled) continue;
    //    frm.cmdRecSync.BackColor = SystemColors.Control;
    //    frm.cmdRecSync.UseVisualStyleBackColor = true;
    //  }
    //}

    //internal void UpdateLbls(int bar) {
    //  //* Form thread
    //  int nn = new clsMTime.clsBBT(bar, 0, 0).TSig.NN;
    //  if (Elapsed.Count <= bar + nn) return;
    //  int delta = (Elapsed[bar + nn] - Elapsed[bar]) / nn;
    //  UpdateLblsAbstract(delta);
    //}

    internal void Stop() {
      //PlayReady = false;
      MP3Player.StopPlay();  //--> FinalizeStop() (WMP only)
      FinalizeStop(false);  //BASSMP3 (FinalizeStop() only called at end)
    }

    private delegate void delegStreamEnd();
    private delegStreamEnd dStreamEnd;
    //private delegate void delegUpdatePos();
    //private delegRefreshBBT dUpdatePos;
    internal void FinalizeStop(bool indpause) {  //called by callback or cmdStop
      _indMP3Playing = false;
      MP3Player.RemovePosSync();
      MP3Player.RemoveEndSync();
      P.F.frmAutoSync?.StopPlay();
      MidiPlay.Sync.CallBack(clsSync.eMidiMsgType.StreamEnded);
      //SetCmdRecordInactive();
      //if (Cmd != eCmd.Play && P.frmStart.chkAutoSyncMerge.Checked) MergeElapsed();
      P.frmSC.Invoke(dStreamEnd);
      int startbar;
      //if (P.frmStart.chkNoAudioSync.Checked) CurrentBBT = P.F.CurrentBBT.Copy();
      if (!StaticAudioSyncEna) CurrentBBT = P.F.CurrentBBT.Copy();
      if (indpause) {
        int beat = CurrentBBT.Beats - 1;
        if (beat < 0) beat = 0;
        if (Elapsed.Play.Count - 1 >= beat && Elapsed.Play[beat] == 0) {
          Elapsed.GetPrevActive(beat, out beat);
        }
        startbar = new clsMTime.clsBBT(beat, true).Bar;
      } else {
        startbar = StartBar;
      }
      //int startbar = (indpause) ? CurrentBBT.Bar : StartBar;
      CurrentBBT = new clsMTime.clsBBT(startbar, 0, 0);
      //RefreshBBT(); 
      P.frmSC.Play?.NewReset();
      P.frmSC.BeginInvoke(new delegRefreshBBTNoParam(Forms.frmStart.RefreshBBT));
      //if (P.F.frmAutoSync != null) {
      //P.F.frmAutoSync.BeginInvoke(
      //  new delegUpdatePos(P.F.frmAutoSync.UpdatePos), MP3Player.GetStartPosUnits());
      if (indpause) {
        if (Elapsed.Play.Count - 1 >= CurrentBBT.Beats) {
          long pos = Elapsed.Play[CurrentBBT.Beats];
          P.frmSC.BeginInvoke(
            new delegUpdatePos(UpdatePos), pos);
        }
      } else {
        P.frmSC?.Play?.Reset();
        P.frmSC.BeginInvoke(
          new delegUpdatePos(UpdatePos), MP3Player.GetStartPosUnits());
      }
      //}
    }

    private void UpdatePos(long bytes) {
      if (Cmd != eCmd.Play && !Elapsed.RecordIsEmpty) {
        if (P.F.frmAutoSync == null) {
          P.F.frmAutoSync = new Forms.frmAutoSync(this);
          //P.F.frmAutoSync.Show();
        }
        Utils.FormAct(P.F.frmAutoSync);
        //P.F.frmAutoSync.Activate();
      }
      if (P.F.frmAutoSync != null) {
        P.F.frmAutoSync.UpdatePos(bytes);
      }
      //* update picBars with newly recorded beats
      if (P.F.frmTrackMap != null) P.F.frmTrackMap.picBars.Refresh();
      if (P.F.frmChordMap != null) P.F.frmChordMap.picBars.Refresh();
    }

    protected void TimedEvent() { }  //not used

    internal int GetTicks() {
      lock (this) {
        if (CurrentBBT.Beats >= Elapsed.Play.Count - 1) return -1;
        long units = MP3Player.GetPosBytes();  //millisecs(ext) or bytes(BASS)
        long delta = Elapsed.Play[CurrentBBT.Beats + 1] - Elapsed.Play[CurrentBBT.Beats];
        if (delta < 0) return CurrentBBT.Ticks;  //gap - can't calculate it
        float unitspertick = (float)delta / CurrentBBT.TicksPerBeat;
        float diffticks = (float)(units - Elapsed.Play[CurrentBBT.Beats]) / unitspertick;
        long ticks = CurrentBBT.Ticks + (int)diffticks;
        return (int)ticks;
      }
    }

    internal void SyncBarOff() { }  //not used

    internal void Reset() {  //clear Active and File
      if (_indMP3Playing) return;
      //UpdateUndo();
      Elapsed.Reset();
    }

    internal void ResetActive() {  //clear Active
      if (_indMP3Playing) return;
      //UpdateUndo();
      Elapsed.ResetRecord();
    }

    internal bool IsEmpty() {
      return (Elapsed.Play.Count < 2);
    }

    protected void SaveFileSub(StreamWriter xsw) {
      for (int i = 0; i < Elapsed.Play.Count; i++) {
        xsw.WriteLine(Elapsed.Play[i]);
      }
      //foreach (long e in Elapsed.Play) xsw.WriteLine(e);
      if (!Elapsed.RecordIsEmpty) {
        xsw.WriteLine("+++");
        for (int i = 0; i < Elapsed.Record.Count; i++) {
          xsw.WriteLine(Elapsed.Record[i]);
        }
        //foreach (long e in Elapsed.Record) xsw.WriteLine(e);
      }
    }

    protected bool Active {
      get { return _indMP3Playing; }
    }

    internal SortedList<int, int> GetShowList(out List<long> elalist) {
      //* obsolete
      SortedList<int, int> deltas = new SortedList<int, int>();
      elalist = new List<long>();
      for (int beat = 0; beat < Elapsed.Play.Count - 1; beat++) {
        //int nn = new clsMTime.clsBBT(beat, 0, 0).TSig.NN;
        long elanext = Elapsed.Play[beat + 1];
        long elathis = Elapsed.Play[beat];
        int diff = (int)(elanext - elathis);
        deltas.Add(beat, diff);
        elalist.Add(elathis);
      }
      return deltas;
    }

    internal string[] ShowList(out string[] titles) {
      //* return non-title lines
      string[] lines;
      string fmtlist = "{0,11} {1,11} {2,-8}";  //bytes, diff, minsecs
      string fmtline = "{0,3}.{1,-2}  {2,-5}  |  {3,33}  |  {4,33}";  //bar.beat beat, act, file
      //*            bar ms/beat msecs
      int maxbeat = Math.Max(Elapsed.Record.Count, Elapsed.Play.Count);
      lines = new string[maxbeat];
      titles = new string[2];
      titles[0] = "               |   RECORD                            |   PLAY";
      string headlist = string.Format(fmtlist, "Bytes", "Bytes/Beat", "mm:ss");
      titles[1] = string.Format(fmtline, "Bar", "Bt", "Beat", headlist, headlist);
      int lnum = 0;

      for (int b = 0; b < maxbeat; b++) {
        string rec = GetCols(Elapsed.Record, fmtlist, b);
        string play = GetCols(Elapsed.Play, fmtlist, b);
        clsMTime.clsBBT bbt = new ChordCadenza.clsMTime.clsBBT(b, true);
        int bar = bbt.Bar + 1;
        int beatsrembar = bbt.BeatsRemBar + 1;
        lines[lnum++] = String.Format(fmtline, bar, beatsrembar, (b + 1), rec, play);
      }
      return lines;
    }

    private string GetCols(clsElapsed.clsList list, string fmtlist, int b) {
      long bytes = 0;
      int diff = 0;
      string cols = "";
      if (b < list.Count) {
        //clsElapsed.ShowList(b, list, out bytes, out diff);
        bytes = list[b];
        diff = (int)((b >= list.Count - 1) ? 0 : list[b + 1] - bytes);
        if (bytes <= 0) {
          string str = (bytes < 0) ? "***" : "";
          cols = string.Format(fmtlist, str, "", "");
        } else {
          double secs = MP3Player.GetUnits2Seconds(bytes);
          string minsecs = Forms.frmAutoSync.GetMinSecs(secs);
          if (diff <= 0) cols = string.Format(fmtlist, bytes, "*", minsecs);
          else cols = string.Format(fmtlist, bytes, diff, minsecs);
        }
      }
      return cols;
    }

    internal void MergeElapsed() {
      if (_indMP3Playing) return;
      clsElapsed elapsed = Elapsed.Copy();
      elapsed.Merge();
      int b = elapsed.ValidatePlay(prompt: false);
      if (b >= 0) {
        MessageBox.Show("Merge failed - invalid output at beat " + (b + 1));
        return;
      }
      UpdateUndo();
      elapsed.ResetRecord();
      Elapsed = elapsed;

      //indSave.Ind = true;
      //Record_LastUsed = Elapsed.Record.ToList();
    }

    internal void MoveActiveToFileElapsed() {
      if (_indMP3Playing) return;
      clsElapsed elapsed = Elapsed.Copy();
      elapsed.CopyRecordToPlay();
      int b = elapsed.ValidatePlay(prompt: false);
      if (b >= 0) {
        MessageBox.Show("Copy failed - invalid output at beat " + (b + 1));
        return;
      }
      //UpdateUndo();
      elapsed.ResetRecord();
      Elapsed = elapsed;
      //indSave.Ind = true;
      //Record_LastUsed = Elapsed.Record.ToList();
    }

    //private Stack<clsElapsed> UndoStack = new Stack<clsElapsed>(9);
    //private Stack<clsElapsed> RedoStack = new Stack<clsElapsed>(9);
    private LLStack<clsElapsed> UndoStack;
    private LLStack<clsElapsed> RedoStack;

    internal void SetUndoRedoDisplay() {
      if (P.F.frmAutoSync == null) return;
      P.F.frmAutoSync.cmdUndo.Text = "Undo (" + UndoStack.Count + ")";
      P.F.frmAutoSync.cmdRedo.Text = "Redo (" + RedoStack.Count + ")";
      P.F.frmAutoSync.cmdUndo.Enabled = (UndoStack.Count > 0);
      P.F.frmAutoSync.cmdRedo.Enabled = (RedoStack.Count > 0);
    }

    internal void UpdateUndo() {
      UndoStack.Push(Elapsed.Copy());
      SetUndoRedoDisplay();
    }

    internal void Undo() {
      if (_indMP3Playing) return;
      if (UndoStack.Count == 0) return;
      RedoStack.Push(Elapsed);
      Elapsed = UndoStack.Pop();
      //indSave.Ind = true;
      SetUndoRedoDisplay();
    }

    internal void Redo() {
      if (_indMP3Playing) return;
      if (RedoStack.Count == 0) return;
      UndoStack.Push(Elapsed);
      Elapsed = RedoStack.Pop();
      //indSave.Ind = true;
      SetUndoRedoDisplay();
    }

    //**********************************************************************************************

    internal class clsElapsed {
      internal class clsList {
        internal clsList(clsAudioSync autosync) {
          AudioSync = autosync;
          List = new List<long>(200);
        }

        internal clsList(clsAudioSync autosync, List<long> list) {
          AudioSync = autosync;
          List = list;
        }

        private clsAudioSync AudioSync;
        private List<long> List;

        internal long this[int index] {
          get { return List[index]; }
          set {
            List[index] = value;
            AudioSync.indSave = true;
          }
        }

        internal clsList ToList() {
          return new clsList(AudioSync, List.ToList());
        }

        internal int BinarySearch(long pos) {
          return List.BinarySearch(pos);
        }

        internal int Count { get { return List.Count; } }

        internal bool SequenceEqual(clsList list) {
          return List.SequenceEqual(list.List);
        }

        internal void Add(long val) {
          List.Add(val);
          AudioSync.indSave = true;
        }

        internal clsList GetRange(int index, int count) {
          return new clsList(AudioSync, List.GetRange(index, count));
        }
      }

      internal clsList Record;
      internal clsList Play;

      private clsAudioSync AudioSync;

      internal clsElapsed(clsElapsed elapsed) {  //copy
        Record = elapsed.Record.ToList();
        Play = elapsed.Play.ToList();
        AudioSync = elapsed.AudioSync;
      }

      internal clsElapsed(clsAudioSync autosync) {  //empty 
        AudioSync = autosync;
        Reset();
        AudioSync.indSave = false;
      }

      internal clsElapsed(clsAudioSync autosync, List<string> lines) {  //from .cht file
        //* create list from .cht file
        AudioSync = autosync;
        int i = 0;
        clsList list;
        ReadSection(lines, ref i, out list);  //to "+++" or EOF
        Play = list;
        ReadSection(lines, ref i, out list);
        if (list.Count == 0) ResetRecord(); else Record = list;
        AudioSync.indSave = false;
      }

      private void ReadSection(List<string> lines, ref int i, out clsList list) {
        list = new clsList(AudioSync);
        for (; i < lines.Count; i++) {
          string l = lines[i];
          if (l.Trim() == "+++") {  //End of Play / Start of Record 
            i++;
            break;  
          }
          long elapsed;
          long.TryParse(l, out elapsed);  //0 by default
          list.Add(elapsed);
        }
      }

      internal clsMTime.clsBBT GetFirstBBTPlay() {
        for (int i = 0; i < Play.Count; i++) {
          if (Play[i] > 0) return new clsMTime.clsBBT(i, true);
        }
        return null;
      }

      //internal void GetNextActiveBeat(ref int beat) {
      //  ++beat;
      //  GetFileBytes(ref beat);  //skip if necessary
      //}

      internal long? GetNextActivePos(int inbeat) {
        int? outbeat;
        return GetNextActive(inbeat, out outbeat);
      }

      internal long GetPrevActivePos(int inbeat) {
        int outbeat;
        return GetPrevActive(inbeat, out outbeat);
      }

      internal int? GetNextActiveBeat(int inbeat) {
        int? outbeat;
        GetNextActive(inbeat, out outbeat);
        return outbeat;
      }

      internal clsMTime.clsBBT GetNextActiveBBT(clsMTime.clsBBT bbt) {
        int inbeat = bbt.Beats;
        int? outbeat;
        if (!GetNextActive(inbeat, out outbeat).HasValue) return null;
        return new clsMTime.clsBBT(outbeat.Value, true);
      }

      //internal clsMTime.clsBBT GetPrevActiveBBT(clsMTime.clsBBT bbt) {
      //  int inbeat = bbt.Beats;
      //  int outbeat;
      //  GetPrevActive(inbeat, out outbeat);
      //  return new clsMTime.clsBBT(outbeat, true);
      //}

      internal long GetPrevActive(int inbeat, out int outbeat) {
        //* get prev beat, skipping backward to first non-empty
        //* return 0 if no non-empty entry (start)
        if (Play.Count < 2) {
          outbeat = 0;
          return 0;
        }
        inbeat--;
        int start = Math.Min(inbeat, Play.Count - 1);
        for (int i = start; i >= 0; i--) {
          //if (Play[i] > 0 && (inbeat >= Play.Count - 1 || Play[inbeat + 1] - Play[i] > AutoSync.MP3Player.Resolution)) {
          if (Play[i] > 0) {
            outbeat = i;
            return Play[i];
          }
        }
        outbeat = 0;
        return 0;
      }

      internal long? GetNextActive(int inbeat, out int? outbeat) {
        //* get next beat, skipping forward to first non-empty
        //* return null if no non-empty entry 
        inbeat++;
        for (int i = inbeat; i < Play.Count; i++) {
          //if (Play[i] > 0 && Play[i] - Play[inbeat - 1] > AutoSync.MP3Player.Resolution) {
          if (Play[i] > 0) {
              outbeat = i;
            return Play[i];
          }
        }
        outbeat = null;
        return null;
      }

      internal int GetPlayIndex(long pos) {
        //* find index of first sync'ed beat on or after pos
        int index = Play.BinarySearch(pos);
        if (index < 0) index = ~index;  //next element (may be off the end)
        return index;
      }

      internal void SetRecord(int beat, long val) {
        if (beat < Record.Count) {
          Record[beat] = val;
        } else {
          for (int i = Record.Count; i < beat; i++) Record.Add(0);
          Record.Add(val);
        }
      }

      internal void SetPlay(int beat, long val) {
        if (beat < Play.Count) {
          Play[beat] = val;
        } else {
          for (int i = Play.Count; i < beat; i++) Play.Add(0);
          Play.Add(val);
        }
      }

      internal clsElapsed Copy() {
        return new clsElapsed(this);
      }

      internal int ValidatePlay(bool prompt) {  //check Play only
        //* return beatnumber of first error, or -1
        //if (Play[0] != 0) return 0;  //doesn't start with 0
        int startbeat = 1;
        for (; startbeat < Play.Count; startbeat++) {  //locate to first non-zero
          if (Play[startbeat] != 0) break;
        }
        if (startbeat == Play.Count) return Play.Count;  //no non-zero values
        long lastpos = 0;  //last non-zero position
        for (int b = startbeat; b < Play.Count; b++) {
          //if (Play[b] > 0 && Play[b] <= Play[b - 1]) {
          if (Play[b] > 0) {
            if (Play[b] <= lastpos) {
              string msg = "File sequence error at beat " + (b + 1);
              if (!prompt) {
                MessageBox.Show(msg);
                return b;
              }
              if (MessageBox.Show(msg + " - truncate file?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                Play = Play.GetRange(0, b);
                return -1;
              }
              return b;
            }
            lastpos = Play[b];
          }
        }
        return -1;
      }

      //internal int Count { get { return Active.Count; } }

      //internal void Add(int ela) {
      //  Active.Add(ela);
      //}

      internal void TruncateEmpty() {
        //* clear Play/Record if empty
        int i;
        for (i = 0; i < Record.Count; i++) {
          if (Record[i] > 0)  break; 
        }
        if (i == Record.Count) ResetRecord();
        for (i = 0; i < Play.Count; i++) {
          if (Play[i] > 0) break; 
        }
        if (i == Play.Count) ResetPlay();
      }

      internal void ResetRecord() {
        //* clear Record
        Record = new clsList(AudioSync);
        Record.Add(0);
        //AutoSync.Record_LastUsed = ela.Record.ToList();
      }

      internal void ResetPlay() {
        //* clear Play
        Play = new clsList(AudioSync);
        Play.Add(0);
      }

      internal void Reset() {
        //* clear Active and File
        ResetRecord();
        ResetPlay();
      }

      internal bool PlayIsEmpty {
        get {
          if (Play.Count == 0) return true;
          return (Play.Count == 1 && Play[0] == 0);
        }
      }

      internal bool RecordIsEmpty {
        get {
          if (Record.Count == 0) return true;
          return (Record.Count == 1 && Record[0] <= 0);
        }
      }

      //internal static void ShowList(int beat, clsElapsed.clsList list, 
      //  out long bytes, out int diff) {
      //  bytes = list[beat];
      //  diff = (int)((beat >= list.Count - 1) ? 0 : list[beat + 1] - bytes);
      //}

      internal void Merge() {  //merge Record to Play
        //* ignore if elapsed <= 0
        for (int b = 0; b < Record.Count; b++) {
          if (Record[b] < 0) {  //delete play (jump)
            if (b < Play.Count) Play[b] = 0;
          } else if (Record[b] == 0) {  //empty
            if (b >= Play.Count) Play.Add(0);
          } else {
            long play = Math.Max(0, Record[b]);  //-1 -> 0
            if (b < Play.Count) Play[b] = play; else Play.Add(play);
          }
        }
      }

      internal void CopyRecordToPlay() {
        //Play = Record.ToList();
        Play = new clsList(AudioSync);
        for (int i = 0; i < Record.Count; i++) {
          Play.Add(Math.Max(0, Record[i]));
        }
        //foreach (long l in Record) {
        //  Play.Add(Math.Max(0, l));
        //}
      }

      internal void OffsetTimes(int offset) {
        //* adjust all beat times by offset (pos or neg)
        if (P.F?.AudioSync?.MP3Player == null) return;
        double secs = offset / 1000f;
        long bytes = P.F.AudioSync.MP3Player.GetSeconds2Units(secs);
        for (int i = 0; i < Play.Count; i++) {
          if (Play[i] == 0) continue;
          Play[i] += bytes;
          if (Play[i] < 0) Play[i] = 0;
        }
        for (int i = 0; i < Record.Count; i++) {
          if (Record[i] == 0) continue;
          Record[i] += bytes;
          if (Record[i] < 0) Record[i] = 0;
        }
      }

      internal void OffsetBeats(int offset) {
        //* adjust all beats by offset (pos or neg)
        if (P.F?.AudioSync?.MP3Player == null) return;
        if (offset < 0) {
          for (int i = 0; i < Play.Count; i++) {
            if (i >= Play.Count + offset) { Play[i] = 0; continue; }
            Play[i] = Play[i - offset];
          }
          for (int i = 0; i < Record.Count; i++) {
            if (i >= Record.Count + offset) { Record[i] = 0; continue; }
            Record[i] = Record[i - offset];
          }
        } else {  //offset >= 0
          for (int i = Play.Count - 1; i >= 0; i--) {
            if (i < offset) { Play[i] = 0; continue; }
            Play[i] = Play[i - offset];
          }
          for (int i = Record.Count - 1; i >= 0; i--) {
            if (i < offset) { Record[i] = 0; continue; }
            Record[i] = Record[i - offset];
          }
        }
      }
    }
  }
}