using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ChordCadenza {
  internal partial class clsAudioSync {
    protected static object TimerLock = new object();
    protected static int StartBar {
      get { return P.F.StartBar; }
      set { P.F.StartBar = value; }
    }

    protected const int Latency = 0;  //msecs
    protected const int MinBeatTime = 100;  //msecs
    protected const int MaxBeatTime = 2000;  //msecs

    protected delegate void delegRefreshBBT(clsMTime.clsBBT bbt);
    protected delegate void delegRefreshBBTNoParam();
    protected delegate void delegPlayOffAll();

    protected string CHTFilePath = "";
    internal clsPlay.clsChordEvTimed PlayChord;
    //internal int MaxBar;
    internal int MaxBeatMidi;
    //protected int BeatInterval;
    //protected bool PlayReady = false;
    //internal bool indSave.Ind = false;
    internal clsMTime.clsBBT CurrentBBT = new clsMTime.clsBBT(0);  //current location (bar or chord)

    internal static clsAudioSync New() {  //called from load .chp
      try {
        List<string> lines;
        //string chtcfilepath, chtxfilepath;

        //* look for existing bars internal (mp3) (.chtc)
        if (!clsAudioSync.GetAudioFilePath()) return null;
        if (File.Exists(P.F.Project.AudioSyncPath)) {
          lines = Utils.ReadLinesIgnoreComments(P.F.Project.AudioSyncPath);
          if (lines != null) {
            //P.frmStart.lblChtLoad.Text = "loaded";
            P.F.Project.AudioSyncExists = true;
            return new clsAudioSync("", lines);
          } else {
            return new clsAudioSync(""); 
          }
        } else {
          return new clsAudioSync("");  //new .chtc (int mp3)
        }
      }

      catch (AudioIOException) {
        return null;
      }
    }

    //internal static clsAutoSync New(string chtfilepath) {  //load chtfilepath
    //  if (!File.Exists(chtfilepath)) return null;
    //  List<string> lines = Utils.ReadLines(chtfilepath);
    //  try {
    //    if (lines.Count == 0) return null;  //empty file
    //    //if (lines[0] == "#Sections") {
    //    if (lines[0].Trim().Contains(" ")) {  //sections (2 integers)
    //      return new clsAutoSyncSection(chtfilepath, lines);
    //      //} else if (lines[0] == "#Bars") {
    //    } else {  //bars (1 integer)
    //      return new clsAutoSyncBar(chtfilepath, lines);
    //    }
    //    //} else {
    //    //  return null;  //invalid header on .cht file  
    //    //}
    //  }
    //  catch {  //eg invalid .cht file
    //    MessageBox.Show("Invalid or missing .cht file");
    //    return null;
    //  }
    //}

    protected void BaseConstructor() {  //was internal clsAutoSync() {  //called from load 
      //if (P.F.CHPFilePath.Length == 0) return;
      //clsMTime.clsBBT maxbbt = new clsMTime.clsBBT(P.F.MaxTicks);
      //MaxBar = maxbbt.Bar;
      //MaxBeatMidi = maxbbt.Beats;
      //MaxBar = P.F.MaxBBT.Bar;
      MaxBeatMidi = P.F.MaxBBT.Beats;

      int capacity = (int)P.frmStart.nudUndoRedoCapacity.Value;
      UndoStack = new LLStack<clsElapsed>(capacity);
      RedoStack = new LLStack<clsElapsed>(capacity);

      CurrentBBT = new clsMTime.clsBBT(StartBar, 0, 0);
      if (P.F.BarPaneBBTLo == null) P.F.BarPaneBBTLo = new clsMTime.clsBBT(0);
      if (P.F.BarPaneBBTHi == null) P.F.BarPaneBBTHi = new clsMTime.clsBBT(0);
      dStreamEnd = new delegStreamEnd(P.frmStart.StreamPlayOffAll);
    }

    internal static bool GetAudioFilePath() {
      //P.F.Project.AudioSyncExt = ".chtx";
      P.F.Project.AudioExt = "";
      foreach (string ext in Forms.dlgNewProject.AudioFilter) {
        if (File.Exists(P.F.Project.PathAndName + ext)) {
          P.F.Project.AudioExt = ext;
          //P.F.Project.AudioSyncExt = ".chtc";
          break;
        }
      }
      if (P.F.Project.AudioExt == "") return false;
      return true;
    }

    //internal static string GetCHTCFilePath() {  //sync bars int (mp3)
    //  //return Path.GetDirectoryName(P.F.CHPFilePath)
    //  //  + "\\" + Path.GetFileNameWithoutExtension(P.F.CHPFilePath)
    //  //  + ".chtc";
    //  return P.F.ProjectPathAndName + ".chtc";
    //}

    //internal static string GetCHTXFilePath() {  //sync bars ext 
    //  //return Path.GetDirectoryName(P.F.CHPFilePath)
    //  //  + "\\" + Path.GetFileNameWithoutExtension(P.F.CHPFilePath)
    //  //  + ".chtx";
    //  return P.F.ProjectPathAndName + ".chtx";
    //}

    //internal static string GetCHTBFilePath() {  //sync bars ext (stream)
    //  return Path.GetDirectoryName(P.F.CHPFilePath)
    //    + "\\" + Path.GetFileNameWithoutExtension(P.F.CHPFilePath)
    //    + ".chtb";
    //}

    //internal static string GetCHTSFilePath() {  //sync sections
    //  return Path.GetDirectoryName(P.F.CHPFilePath)
    //    + "\\" + Path.GetFileNameWithoutExtension(P.F.CHPFilePath)
    //    + ".chts";
    //}

    //protected void UpdateLblsAbstract(int delta) {
    //  P.frmSC.lblAutoMsecsPerBeat.Text = delta.ToString();
    //  int tempo = (delta == 0) ? 0 : DivRound(60000, delta);
    //  P.frmSC.lblAutoBeatsPerMinute.Text = tempo.ToString();
    //}

    //internal void Start() {
    //  //* Form thread - called from StartSync_Click on IPlayable form
    //  if (!StartSyncPlay()) return;
    //  //PlayReady = true;
    //  PlayableForms.CmdState_Playing();
    //  P.frmStart.FormsStreamOnOff(true);   //enable/disable controls (not StartPlay/StopPlay etc.)
    //}

    //internal virtual void StartSyncRecord() {
    //  Start();
    //}

    internal void Pause() {
      //MP3Player.StopPlay();
      MP3Player.PausePlay();
      FinalizeStop(true);
      //MP3Player.PausePlay();
      //SetCmdRecordInactive();
      //P.frmSC.Invoke(dStreamEnd);
      //CurrentBBT = new clsMTime.clsBBT(CurrentBBT.Bar, 0, 0);
      //P.frmSC.nudStartBar.Value = P.F.CurrentBBT.Bar + 1;
      //RefreshBBT();
    }

    internal static bool ActiveStatic {
      get {
        return (P.F != null && P.F.AudioSync != null && P.F.AudioSync.Active);
      }
    }

    //protected virtual bool Active {  //overridden in SyncInt
    //  get {
    //    return SyncTimer.Active;
    //  }
    //}

    internal static bool StaticEmpty {
      get {
        return (P.F.AudioSync == null || P.F.AudioSync.IsEmpty());
      }
    }

    internal bool AudioSyncEna {
      get {
        return (!P.frmStart.chkNoAudioSync.Checked && !IsEmpty());
      }
    }
    internal static bool StaticAudioSyncEna {
      get {
        if (P.F.AudioSync == null) return false;
        return (!P.frmStart.chkNoAudioSync.Checked && !StaticEmpty);
      }
    }

    internal void RefreshBBT() {
      P.frmSC.BeginInvoke(new delegRefreshBBT(Forms.frmStart.RefreshBBT), CurrentBBT);
    }

    private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e) {
      //* static to allow this to exist after AutoSync nulled
      lock (TimerLock) {
        if (P.F == null || P.F.AudioSync == null) return;
        P.F.AudioSync.TimedEvent();
      }
    }

    //internal static string SaveFile(string chtfilepath, int tempo) {
    //  SortedList<int, int> deltas = new SortedList<int, int>(1);
    //  deltas.Add(0, tempo);
    //  return SaveFile(chtfilepath, deltas);
    //}

    internal string SaveFile() {
      //* return any error msg
      string msg = Utils.SaveFile(P.F.Project.AudioSyncPath, SaveFileSub);
      if (msg != "") return msg;
      indSave = false;
      //Elapsed_File = Elapsed.Copy();
      //Play_LastSaved = Elapsed.Play.ToList();
      return "";
    }

    internal bool MidiOn(byte[] B) {
      lock (TimerLock) {
        return MidiOnPlay(B);
      }
    }

    private bool MidiOnPlay(byte[] B) {
      //Debug.WriteLine("clsAutoSync: MidiOnPlay: Elapsed = " + SyncTimer.Elapsed);
      int kb = B[1];
      PlayChord = GetCurrentChord();
      //if (PlayChord == null) return true;
      return false;
    }

    internal static int DivRound(int dividend, int divisor) {
      int rem;
      int millisecs = Math.DivRem(dividend, divisor, out rem);
      if (rem > 500) millisecs += 1;
      return millisecs;
    }

    private clsPlay.clsChordEvTimed GetCurrentChord() {
      //int ticks = GetTicks() + P.frmSC.SyncopationCurrent.Ticks;
      if (P.F.CF.Evs.Count == 0) return null;
      int ticks = GetTicks() + Forms.frmSC.SyncopationDefault.Ticks;
      if (ticks < 0) return null;
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
      int index = P.F.CF.FindCFEv(bbt);
      while (P.F.CF.Evs[index].PlayChord == null && index > 0) index--;
      return (clsPlay.clsChordEvTimed)P.F.CF.Evs[index].PlayChord;
    }
  }
}