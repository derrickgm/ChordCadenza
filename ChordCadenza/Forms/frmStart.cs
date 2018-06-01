#undef Testing
#undef TraceLoad

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

//* values correspond to ChordSet scale number
internal enum eMinorKeyType { Harmonic = 1, MelodicUp = 2, MelodicDown = 3, Special = 4 };

namespace ChordCadenza.Forms {
  internal partial class frmStart : Form, IFormStream, ITT {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }
    
    public FileStream stFile;
    internal float TempoFactor = 1;
    //internal bool indTempoChanged = false;
    internal const int TempoMax = 480;
    internal const int TempoMin = 20;
    private bool RightMouseButton = false;
    internal bool Bypass_Event = false;
    internal clsNNDD Syncopation;
    internal bool NoSaveIni = false;

    internal frmStart() {
      //P.Forms.Add(this);
      InitializeComponent();
      Forms.frmSC.ZZZSetPCKBEvs(this);
      //chkSustainAuto.Checked = SustainAutoStatic;

      Text = "Miscellaneous Configuration";
//      Text += Environment.Is64BitProcess ? "64" : "";  //32-bit is default
//#if DEBUG
//      Text += " (Debug)";
//#else
//      //mnuDebug.Visible = false;
//#endif
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
      bool? ret = Forms.frmSC.StaticProcessCmdKey(ref msg, keyData);
      if (!ret.HasValue) return base.ProcessCmdKey(ref msg, keyData);
      return ret.Value;
    }

    internal void PostInit() {
      clsTT.LoadToolTips(this);

      #if !ADVANCED
        grpMidiPos.Hide();
        grpChordSetMinorKey.Hide();
        lblAutoSyncChordDelay.Hide();
        lblAutoSyncChordDelayMS.Hide();
        nudAutoSyncChordDelay.Hide();
        chkKBChordMatch.Hide();
        chkAutoRecChan.Hide();
        chkIgnoreNullChords.Hide();
        //chkPCKB.Hide();
        //nudChunksPerQNote.Hide();
        //lblnudChunksPerQNote.Hide();
        grpMidiPlayAdvanced.Hide();
        //cmdRenameIni.Hide();
        //cmdRestoreIni.Hide();
        grpOther.Hide();
      #endif

      cmdRestoreIni.Enabled = CheckRestoreIni();
      chkLoadMM.Checked = Cfg.LoadMMInitial;

      //SetNud(nudChunksPerQNote);

      SetNudAndTag(nudDIdd, Cfg.DIdd);
      nudTPDI.Value = Cfg.TPDI;

      //SetNudAndTag(nudPIdd, Cfg.PIdd);
      //nudTPPI.Value = Cfg.TPPI;

      Syncopation = new clsNNDD(nudSyncopationNN, nudSyncopationDD);
      SetNudAndTag(nudSyncopationDD);
    }

    private void frmStart_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      Cfg.DictFormProps[Name].SetForm(this);
#if ADVANCED
      FormBorderStyle = FormBorderStyle.Sizable;
#endif
      //nudMaxBarsNoMidiFile.Value = clsF.DefaultSongLength;
    }

    internal bool indConstantChordPlay {
      get {
        if (P.frmSC.chkManSyncChord.Checked) return false;
        return (P.PCKB == null) ? 
          chkConstantChordPlayMidiKB.Checked : 
          chkConstantChordPlayPCKB.Checked;
      }
    }

    public void FormStreamOnOff(bool on) {
      /*
      grpSustainAction.Enabled = !on;
      grpMisc.Enabled = !on;
      grpTimers.Enabled = !on;
      grpMidi.Enabled = !on;
      grpMidiPos.Enabled = !on;
      grpChordPlay.Enabled = !on;
      grpKBOutput.Enabled = !on;
      menuStrip1.Enabled = !on;
      */
    }

    public void NewStFile(string filename) {
      try {
        stFile = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Write);
      }
      catch {
        throw;
      }
      finally {
        if (stFile != null) stFile.Dispose();
      }
    }

    internal void TraceLoad(string txt) {
#if TraceLoad
      Debug.WriteLine(txt);
#endif
    }

    internal bool LoadChordFileAndMidi() {
      if (!P.F.CloseCancellableForms()) return false;
      clsFileStream fs;
      try {
        if (!File.Exists(P.F.Project.CHPPath)) return false;
        fs = LoadMidiFileHeaderFromCF();  //may not exist
        P.frmSC.NewEmpty();

        //* load chpfile before sections and notes 
        if (!P.frmSC.InitShowChordsTxt()) {
          clsF.NewEmpty();
          return false;
        }

        //* load midifile tracks
        if (fs != null) {
          P.F.FSTrackMap = new clsFileStream(P.F.Project.MidiPath, null, false, false, false);
          PlayableForms.CmdState_Set();
        }

        //* load chpfile sections and notes
        P.F.CF.PostInit();
        //if (P.F.CF.Lines_Notes.Count == 0) MessageBox.Show("Warning - no chords in .chp file");

        if (fs != null) {
          //P.F.indCalcKeys = true;
          //P.frmSC.cmbPlayStyle_SelectedIndexChanged(null, null);
          if (chkLoadMM.Checked) Forms.frmSC.ShowMultiMap();
          //P.frmSC.cmbPlayStyle_SelectedIndexChanged(null, null);
        }
        P.F.CurrentBBT = new clsMTime.clsBBT(0);
        P.frmSC.SetProps();
        P.frmSC.Play?.NewReset();
        RefreshBBT(P.F.CurrentBBT);
        //P.frmSC.ResizeForm();
      }
      catch (Exception exc) {
        clsF.NewEmpty();
        MessageBox.Show("Load Project failed: " + exc.Message);
        return false;
      }
      //P.frmStart.lblChpLoad.Text = "loaded";
      return true;
    }

    internal clsFileStream LoadMidiFileHeaderFromCF() {
      //* filename = ???.chp (from load chord file)
      //P.F.ProjectPath = GetProjectPath(chpfilename);
      //if (midifilename == "") {
      //  try {
      //    string dir = Path.GetDirectoryName(chpfilename);
      //    midifilename = GetFileName("MidiFiles|*.mid", "Midi File", dir);
      //    if (midifilename == "") return null;
      //  }
      //  catch (MidiChordFileException) {
      //    return null;
      //  }
      //}
      P.F.Project.MidiExists = File.Exists(P.F.Project.MidiPath);
      if (!P.F.Project.MidiExists) return null;
      //P.frmStart.lblMidLoad.Text = "loaded";

      clsFileStream fs;
      fs = new clsFileStream(P.F.Project.MidiPath);  //readheader only

      //EnablePlayCtls();
      return fs;
    }

    //private string GetMidiFileName(string chppath) {
    //  string chpfile = Path.GetFileName(chppath);
    //  string chpdir = Path.GetDirectoryName(chppath);
    //  string[] f = chpfile.Split(new char[] { '.' });
    //  for (int i = f.Length - 1; i > 0; i--) {  //excl. .chp
    //    string midi = chpdir + '\\';
    //    for (int j = 0; j < i; j++) midi += f[j] + '.';
    //    midi += "mid";
    //    if (File.Exists(midi)) return midi;
    //  }
    //  return "";
    //}

    //private static string GetProjectPath(string fullpath) {
    //  if (fullpath == null || fullpath == "") return "";
    //  string filename = Path.GetFileNameWithoutExtension(fullpath);
    //  string chpdir = Path.GetDirectoryName(fullpath);
    //  return chpdir + "\\" + filename;
    //}

    internal static string GetFileNameFromOFD(OpenFileDialog ofd, string filter, string title, string dir) {
      if (!Directory.Exists(dir)) dir = Cfg.UserMusicPath;

      //* set up dialog
      ofd.InitialDirectory = dir;
      ofd.Filter = filter;
      ofd.FilterIndex = 1;
      ofd.RestoreDirectory = false;
      ofd.Title = title;
      ofd.FileName = "";

      //* run dialog
      if (ofd.ShowDialog() != DialogResult.OK) return "***";

      //* process result
      return ofd.FileName;
    }

    internal void LoadLyricsFile() {
      //if (P.F.CHPFilePath.Length == 0) return;
      //string chlfilename = Path.GetDirectoryName(P.F.CHPFilePath) + "\\" + Path.GetFileNameWithoutExtension(P.F.CHPFilePath) + ".chl";
      string chlfilename = P.F.Project.LyricsPath;
      if (!P.F.Lyrics.LoadLyricsFile(chlfilename)) return;
      if (P.F.frmTrackMap != null) P.F.frmTrackMap.chkShowLyrics_CheckedChanged(null, null);
      //P.frmStart.lblLyrLoad.Text = "loaded";
      P.F.Project.LyricsExists = true;
    }

    internal string GetSoundFontsFileName() {
      //* use ofd to get soundfont dir and return fontfile name, or ""
      return GetFileNameFromOFD(ofd, "SoundFonts|*.sf2", "Load SoundFont", Cfg.SoundFontsPath);
    }

    internal bool LoadMidiFile() {  
      //*OFD if loading midifile only, else use Project
      if (!P.F.CloseCancellableForms()) return false;
      string loadfilename;

      try {
        loadfilename = P.F.Project.MidiPath;  
        //P.F = (indsong) ? new clsF(GetProjectPath(filename)) : new clsF();
        P.F.LoadCSV = new clsLoadCSV();
        P.F.FSTrackMap = new clsFileStream(loadfilename, null, false, true, false);
        //Debug.WriteLine("MaxTicks =  " + P.F.MaxTicks);
      }
      catch (MidiFileException) {
        clsF.NewEmpty();
        return false;
      }
      catch (Exception exc) {
        MessageBox.Show("Error loading MidiFile: " + exc.Message);
        clsF.NewEmpty();
        return false;
      }

      //if (indsong) {
      //  P.F.ProjectPathAndName = GetProjectPath(filename);
      //  P.F.CF = new clsCFPC(true);  //no text file
      //} else {
      //  P.F.Project.MidiPath = filename;
      //  P.F.ProjectPathAndName = "";
      //  P.F.CF = null;
      //}

      P.F.Project.MidiExists = true;
      P.frmSC.NewEmpty();
      P.frmSC.InitShowChords();
      P.frmSC.SetProps();

      //* show trktypes (excl. those requiring chp file)
      //P.frmSC.cmbPlayStyle_SelectedIndexChanged(null, null);

      P.CloseFrm(P.F.frmTrackMap);
      P.F.frmTrackMap = new Forms.frmTrackMap();
      Utils.FormAct(P.F.frmTrackMap);

      return true;
      ////* show trktypes (excl. those requiring chp file)
      //P.frmSC.cmbPlayStyle_SelectedIndexChanged(null, null);
    }

    internal void StreamPlayOffAll() {
      MidiPlay.Sync.indPlayActive = clsSync.ePlay.None;
      PlayableForms.CmdState_Stopped();
      //if (P.frmSC != null && P.F.FSTrackMap != null) P.F.FSTrackMap.SaveRecord();
      FormsStreamOnOff(false);
      if (P.F?.frmTrackMap != null) P.F.frmTrackMap.PlayOff();
    }

    internal void StreamPlayOnAll() {  //stream
      if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.None) return;
      MidiPlay.Sync.indPlayActive = (clsAudioSync.ActiveStatic) ? clsSync.ePlay.AudioStream : clsSync.ePlay.MidiStream;
      PlayableForms.CmdState_Playing();
    }

    internal void UpdateLblTempo(int miditempo, int ticks) {
      if (P.frmSC == null) return;
      int dd = P.F.MTime.GetTSig(ticks).DD;
      P.frmSC.lblTempo.Text = ((dd * 60000000) / (4 * miditempo)).ToString();
      //if (P.frmSC != null) P.frmSC.lblTempo.Text = lblTempo.Text;
      //lblTempo.Text = (miditempo / 10000).ToString();
    }

    //private bool SaveIniOnClose = true;

    internal void CloseForm(bool saveini) {
      //P.Action = P.eAction.Terminate;
      //SaveIniOnClose = saveini;
      //Close();
    }

    //private void frmStart_FormClosing(object sender, FormClosingEventArgs e) {
    //  if (P.frmSC.Terminate) return;  //app closed from frmSC
    //  e.Cancel = true;
    //  Hide();
    //  return;
    //}

    //private void frmStart_FormClosing(object sender, FormClosingEventArgs e) {
    //if (P.Action != P.eAction.Terminate) {
    //  e.Cancel = true;
    //  Hide();
    //  return;
    //}
    //e.Cancel = CheckCloseForms();
    //if (!e.Cancel) {
    //  if (SaveIniOnClose) {
    //    Utils.FormToProps(this);  //not really needed
    //    SaveAllIni();
    //  }
    //}
    ////FrmStartClosedByUser = true;  //not really needed here
    //SaveIniOnClose = true;
    //Program.AppContext.ExitThread();  //close app
    //}

    internal static void SaveAllIni() {
      string msg = Cfg.WriteIniFile();

      msg += Forms.frmSwitch.SaveIniFile();
      msg += clsPCKB.SaveIniFile();
      msg += P.ColorsNoteMap.SaveFile();
      msg += P.ColorsTonnetz.SaveFile();
      msg += P.ColorsShowChords.SaveFile();
      if (P.frmCfgChords.indFrmLoaded) msg += P.frmCfgChords.SaveChordIniFile();  
      msg += Forms.frmChordRanks.Save();

      if (msg != "") MessageBox.Show(msg);
    }

    private void nudTimerSustain_ValueChanged(object sender, EventArgs e) {
      //if (Bypass) return;
      if (P.F != null && P.frmSC != null) {
        if (P.frmSC.Play == null) return;
        clsPlay.Sustain = clsPlay.clsSustain.New(null);
      }
    }

    internal void NewFilesDisplay() {
      lblChpLoad.Text = "";
      lblMidLoad.Text = "";
      lblChtLoad.Text = "";
      lblLyrLoad.Text = "";
      lblAudLoad.Text = "";
    }

    internal void frmStart_DragEnter(object sender, DragEventArgs e) {
      if (DragDropFileName(e) != "") e.Effect = DragDropEffects.Link;
      else e.Effect = DragDropEffects.None;
      RightMouseButton = ((e.KeyState & 2) == 2);
    }

    internal void frmStart_DragDrop(object sender, DragEventArgs e) {
      try {
        //MessageBox.CacheOn();
        string filename = DragDropFileName(e);
        if (filename.Length == 0) return;
        MidiPlay.Sync.Stop();
        if (P.F != null && !P.F.SaveProject(null)) return;  //check and save
        if (!clsF.NewF(ref P.F)) return;
        P.F.SetEmpty(clsCF.DefaultSongLength);
        if (!LoadProject(filename, true)) {
          MessageBox.Show("Load Project failed - no valid file found");
        }
      }
      catch {
        //* programming error or corrupt/inconsistent files, ...
        MessageBox.Show("Load Project failed");
        //frmStart.CheckCloseForms();
        //P.F = new clsF();
      }
      //finally {
      //  MessageBox.CacheOff();
      //}
    }

    internal bool LoadProject(string filename, bool updaterecent) { 
      //* filename = "" (button) or DragDrop file
      MidiPlay.Sync.Stop();
      P.F.StartBar = 0;  //else Stop will set CurrentBBT to old StartBar!!!
      //if (!P.F.CloseCancellableForms()) return false;

      string chpext;
      if (filename == "") {  //Button
        filename = GetFileNameFromOFD(ofd,
          "Project Files|*.chp*;*.mid;*.chl;*.chtc;*.mp3|ChordFiles|*.chp*|All Files|*.*",
          "Load Project", Cfg.ProjectDir);
        if (filename == "***") return false;
      }
      if (!P.F.CloseCancellableForms()) return false;

      using (new clsWaitCursor()) {
        using (new clsLoadingProject()) {
          //P.frmStart.nudMaxBarsNoMidiFile.Value = clsF.DefaultSongLength;
          if (Path.GetExtension(filename).StartsWith(".chp")) {
            chpext = Path.GetExtension(filename);
          } else {  //get all .chp<n> files in folder
            string[] dirs = Directory.GetFiles(
              Path.GetDirectoryName(filename),
              Path.GetFileNameWithoutExtension(filename) + ".chp?");  //? = zero or one char
            if (dirs.Length > 1) {  //multiple .chp files
              dlgCHP dialogfrm = new dlgCHP(dirs, true);
              if (dialogfrm.CHPDescs == null || dialogfrm.CHPDescs.Length == 0) return false;
              if (dialogfrm.ShowDialog(P.frmSC) == DialogResult.Cancel) return true;
              chpext = dialogfrm.OK_Ext;
            } else if (dirs.Length == 1) {  //one .chp file only
              chpext = Path.GetExtension(dirs[0]);
            } else {  //no .chp files
              chpext = ".chp0";  //new file
            }
          }

          P.frmSC.panTrkAudio.Enabled = false;
          if (!clsF.NewF(ref P.F)) return false;  //should not happen - already checked earlier
          P.F.Project = new clsProject(filename, chpext);
          if (!LoadChordFileAndMidi()) {  //clsF.NewEmpty() if false
            if (File.Exists(P.F.Project.MidiPath)) {
              if (!LoadMidiFile()) return false;   //no .chp file - load .mid
            } else {
              P.F.SetEmpty(clsCF.DefaultSongLength);
            }
          }
          P.F.AudioSync = clsAudioSync.New();
          LoadLyricsFile();
          P.frmSC.NewMidiOrCF();
          //P.frmSC.cmbPlayStyle_SelectedIndexChanged(null, null);
          PlayableForms.CmdState_Stopped();
          //if (updaterecent) P.frmSC.UpdateRecentProjects();
          if (updaterecent) P.frmSC.UpdateRecentProjects();
          P.frmSC.Play = (P.frmSC.optModeChords.Checked) ?
            clsPlay.NewPlay(frmSC.ePlayMode.Chords) : clsPlay.NewPlay(frmSC.ePlayMode.KB);
          //if (P.F.CF != null) P.F.CF.indSave = false;
          if (P.F.frmTrackMap != null) P.F.frmTrackMap.Refresh();
          if (P.F.frmChordMap != null) P.F.frmChordMap.Refresh();
          return true;
        }
      }
    }

    private string DragDropFileName(DragEventArgs e) {
      if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return "";
      string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
      if (paths.Length != 1) return "";
      string path = paths[0];
      return GetProjectFilePath(path);
    }

    internal string GetProjectFilePath(string path) {
      //* check dir is valid project dir and return dir\\dir.xxx
      try {
        FileAttributes attr = File.GetAttributes(path);
        if (attr.HasFlag(FileAttributes.Directory)) {
          string ff = "";
          string[] dirs = path.Split(new char[] { '\\' });
          string lastdir = dirs.Last();
          foreach (string f in Directory.GetFiles(path)) {
            string ext = Path.GetExtension(f);
            if (!dlgNewProject.AllFilter.Contains(ext.ToLower())) continue;  //ext not recognised
            //* folder dragdrop - folder must match filename without ext, excluding .chp
            if (!Path.GetFileNameWithoutExtension(f).StartsWith(lastdir)) continue;
            ff = f;
          }
          if (ff == "") return "";
          return path + "\\" + lastdir + ".xxx";   //virtual file (to avoid .<mod>.chp)
        } else {  //not a folder
          string ext = Path.GetExtension(path);
          if (!dlgNewProject.AllFilter.Contains(ext.ToLower())) return "";
          return path;
        }
      }
      catch {
        return "";
      }
    }

    private void ConstantChordDisplay_Changed(object sender, EventArgs e) {
      if (P.F == null || P.frmSC == null) return;
      if (P.frmSC.PlayMode == Forms.frmSC.ePlayMode.Chords) {
        P.frmSC.Play = new clsPlayChords();
      }
      P.frmSC.SetEndBBTRefresh();
    }

    internal static void RefreshBBT() {
      RefreshBBT(P.F.CurrentBBT); 
    }

    internal static void RefreshBBT(clsMTime.clsBBT bbt) {
      //Debug.WriteLine("global refreshbbt = " + bbt.ToStringBBT());
      if (P.F?.MTime == null) return;
      if (bbt.MTime != P.F.MTime) return;
      foreach (IFormPlayable pf in PlayableForms.Active) pf.RefreshBBT(bbt);

      if (Forms.frmSC.MenuMonitor /*&& clsPlay.PlayExists()*/) {  
        lock (clsPlay.MonitorTimes) {
          MidiPlay.Sync.StopWatch.Stop();
          int msecs = (int)MidiPlay.Sync.StopWatch.ElapsedMilliseconds;
          clsPlay.MonitorTimes.Add(-msecs - 1);
          clsPlay.GCCnt0.Add(GC.CollectionCount(0));
          clsPlay.GCCnt1.Add(GC.CollectionCount(1));
          clsPlay.GCCnt2.Add(GC.CollectionCount(2));
          //* add to totals
          P.frmSC.Play.AddToMonitorDTimes(msecs);
        }
      }
      //P.F.frmLyrics?.UpdateCaret(bbt.Bar);
    }


    internal static void SetNudAndTag(NumericUpDown nud) {
      SetNudAndTag(nud, null);
    }

    private static bool NudCtl_Bypass = false;

    internal static void SetNudAndTag(NumericUpDown nud, int? val) {
      //* set nud control to a value, bypassing ValueChanged event
      NudCtl_Bypass = true;
      if (val.HasValue) nud.Value = val.Value;
      nud.Tag = (int)nud.Value;
      NudCtl_Bypass = false;
    }

    internal static int SetNudExp2(NumericUpDown nud) {
      //* convert new nud.Value to exponential of 2
      //* return with new value if nud value changed, else -1
      //* Tag contains previous value
      //* zero value not allowed
      //* called from nud_ValueChanged event
      if (NudCtl_Bypass) return -1;
      NudCtl_Bypass = true;
      int newval;
      if ((int)nud.Value > (int)nud.Tag) {  //increase
        if (nud.Value == 1) newval = 1;  //0 -> 1
        else newval = Math.Min((int)nud.Maximum, ((int)nud.Value - 1) * 2);
      } else {  //decrease
        newval = Math.Max((int)nud.Minimum, ((int)nud.Value + 1) / 2);
      }
      nud.Value = newval;
      nud.Tag = (int)nud.Value;
      NudCtl_Bypass = false;
      return newval;
    }

    internal void FormsStreamOnOff(bool streamon) {
      //* enable/disable controls (not StartPlay/StopPlay etc.)
      //* streamon = true, false, or unsure (enable all controls)
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      List<Form> forms = new List<Form>();  //copy OpenForms - may change
      foreach (Form form in Application.OpenForms) {
        forms.Add(form);  
      }
      foreach (Form form in forms) {  
        if (form == null || !(form is IFormStream)) continue;
        IFormStream iform = (IFormStream)form;
        if (!form.IsHandleCreated) continue;
        iform.FormStreamOnOff(streamon);
      }

      if (P.frmMidiDevs != null) {
        P.frmMidiDevs.cmdExecInKB.Enabled = !streamon;
        P.frmMidiDevs.cmdExecOutStream.Enabled = !streamon;
        P.frmMidiDevs.cmdExecOutKB.Enabled = !streamon;
      }
      //Debug.WriteLine("elapsed time = " + stopwatch.ElapsedMilliseconds);
      stopwatch.Stop();
    }

    private void nudDIdd_ValueChanged(object sender, EventArgs e) {
      if (SetNudExp2(nudDIdd) < 0) return;
      Cfg.DIdd = (int)nudDIdd.Value;
    }

    private void nudTPDI_ValueChanged(object sender, EventArgs e) {
      Cfg.TPDI = (int)nudTPDI.Value;
      nudDIdd.Enabled = (Cfg.TPDI == 0);
    }

    //private void nudPIdd_ValueChanged(object sender, EventArgs e) {
    //  if (SetNudExp2(nudPIdd) < 0) return;
    //  Cfg.PIdd = (int)nudPIdd.Value;
    //}

    //private void nudTPPI_ValueChanged(object sender, EventArgs e) {
    //  Cfg.TPPI = (int)nudTPPI.Value;
    //  nudPIdd.Enabled = (Cfg.TPPI == 0);
    //}

    private void nudSyncopationDD_ValueChanged(object sender, EventArgs e) {
      //* read cfg 1/4 -> gets set to 1/2 here
      //* don't need int dd here - needs sorting...
      //if (Bypass) return;
      Syncopation.DD = (int)nudSyncopationDD.Value;
      if (Bypass_Event) return;
      Bypass_Event = true;
      int val = frmStart.SetNudExp2(nudSyncopationDD);
      if (val >= 0) nudSyncopationDD.Value = val;
      Bypass_Event = false;
      if (P.F?.CF != null) P.F.CF.indSave = true;
    }

    private void nudSyncopationNN_ValueChanged(object sender, EventArgs e) {
      //if (Bypass) return;
      Syncopation.NN = (int)nudSyncopationNN.Value;
      if (P.F?.CF != null) P.F.CF.indSave = true;
    }

    private void cmdResetPlay_Click(object sender, EventArgs e) {
      PlayableForms.CmdState_Stopped();
    }

//*******************************************************************************

    internal eMinorKeyType MinorKeyType {
      get {
        if (optMinorHarmonic.Checked) return eMinorKeyType.Harmonic;
        else if (optMinorMelodicUp.Checked) return eMinorKeyType.MelodicUp;
        else if (optMinorMelodicDown.Checked) return eMinorKeyType.MelodicDown;
        else if (optMinorSpecial.Checked) return eMinorKeyType.Special;
        else LogicError.Throw(eLogicError.X092);
        return eMinorKeyType.Harmonic;
      }
      set {
        optMinorHarmonic.Checked = (value == eMinorKeyType.Harmonic);
        optMinorMelodicUp.Checked = (value == eMinorKeyType.MelodicUp);
        optMinorMelodicDown.Checked = (value == eMinorKeyType.MelodicDown);
        optMinorSpecial.Checked = (value == eMinorKeyType.Special);
      }
    }

    //internal static bool ShowMsgYesNo(string msg, string caption) {
    //  //* return true if 'Yes' chosen
    //  return MessageBox.Show(msg, caption, MessageBoxButtons.YesNo) == DialogResult.Yes;
    //}

    //internal static bool CheckCloseForms() {
    //  //* return false if form close cancelled by user
    //  if (P.F == null) return true;
    //  if (P.F.frmChordMap != null) {
    //    P.F.frmChordMap.Close();
    //    if (P.F.frmChordMap != null) return false;
    //  }
    //  if (P.F.frmAutoSync != null) {
    //    P.F.frmAutoSync.Close();
    //    if (P.F.frmAutoSync != null) return false;
    //  }
    //  if (P.F.frmLyrics != null) {
    //    P.F.frmLyrics.Close();
    //    if (P.F.frmLyrics != null) return false;
    //  }
    //  return true;
    //}

    //private void optPedalSustain_CheckedChanged(object sender, EventArgs e) {
    //  //if (Bypass) return;
    //  if (clsPlay.PlayExists()) {
    //    clsPlay.Sustain = clsPlay.clsSustain.New(null);  //new sustain
    //  } else {
    //    clsPlay.clsSustain.PlayPedalStatic(false);
    //  }
    //}

    //private void chkSustainAuto_CheckedChanged(object sender, EventArgs e) {
    //  //if (Bypass) return;
    //  //SustainAutoStatic = chkSustainAuto.Checked;
    //}

    private void frmStart_FormClosing(object sender, FormClosingEventArgs e) {
      Cfg.DictFormProps[Name] = new clsFormProps(this);
      if (e.CloseReason == CloseReason.UserClosing) {
        e.Cancel = true;
        Hide();
      }
    }

    private void optShowNote_CheckedChanged(object sender, EventArgs e) {
      if (P.frmSC != null) P.frmSC.Refresh();
    }

    private void chkRunChordNotes_CheckedChanged(object sender, EventArgs e) {
      if (P.frmSC != null) P.frmSC.Refresh();
    }

    private void cmbChordNotes_SelectedIndexChanged(object sender, EventArgs e) {
      if (P.frmSC != null) P.frmSC.picChordNames.Refresh();
    }

    //private void chkSaveMidiFile_CheckedChanged(object sender, EventArgs e) {
    //  panSaveMidiFile.Enabled = chkSaveMidiFile.Checked;
    //}

    private void cmdHelp_Click(object sender, EventArgs e) {
      Utils.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_Misc_Intro.htm");
    }

    private void chkTTActive_CheckedChanged(object sender, EventArgs e) {
      //* Control tooltips
      foreach (Form frm in Application.OpenForms) {
        if (frm is ITT) {
          ((ITT)frm).TT.Active = chkTTActive.Checked;
        }
      }
      if (P.F != null && P.F.frmTrackMap != null) P.F.frmTrackMap.TTTrks.Active = chkTTActive.Checked;

      //* Menu tooltips
      if (P.frmSC != null) {
        foreach (ToolStripMenuItem item in clsTT.frmSC_MenuItems) {
          clsTT.LoadToolTipMenuItem(P.frmSC, item);
        }
      }
      if (P.F != null && P.F.frmAutoSync != null) {
        foreach (ToolStripMenuItem item in clsTT.frmAutoSync_MenuItems) {
          clsTT.LoadToolTipMenuItem(P.F.frmAutoSync, item);
        }
      }

      //* Forms not in help system (should be in Form constructor also).
      if (P.F != null && P.F.frmLyrics != null) P.F.frmLyrics.toolTip1.Active = chkTTActive.Checked;

      /*
      //* this doesn't work (maybe because only first level menu items affected?)
      P.frmSC.menuStrip1.ShowItemToolTips = chkTTActive.Checked;
      if (P.F != null && P.F.frmAutoSync != null) {
        P.F.frmAutoSync.mnuMain.ShowItemToolTips = chkTTActive.Checked;
      }
      */
    }

    private void cmdRenameIni_Click(object sender, EventArgs e) {
      string msg = "This will rename all .Ini files to .Ini.BU, overwriting any existing .Ini.BU files.";
      msg += "\r\nIt will then restart the application, using default values.";
      msg += "\r\nClick 'OK' to continue, else 'Cancel'";
      if (MessageBox.Show(msg, MessageBoxButtons.OKCancel) == DialogResult.OK) {
        if (!RenameIni(Cfg.MainIniFilePath)) return;
        if (!RenameIni(Cfg.FrmNMColoursIniFilePath)) return;
        if (!RenameIni(Cfg.FrmSCColoursIniFilePath)) return;
        if (!RenameIni(Cfg.SwitchIniFilePath)) return;
        if (!RenameIni(Cfg.PCKBIniFilePath)) return;
        if (!RenameIni(Cfg.ChordCfgIniFilePath)) return;
        if (!RenameIni(Cfg.ChordNamesRankIniFilePath)) return;
        if (!RenameIni(Cfg.FrmTonnetzColoursIniFilePath)) return;  //advanced only

        NoSaveIni = true;
        P.frmSC.RestartApp();
      }
    }

    private static bool RenameIni(string path) {
      try {
        if (File.Exists(path)) {
          File.Delete(path + ".BU");  //no exception if file does not exist
          File.Move(path, path + ".BU");
        }
      }
      catch (Exception exc) {
        MessageBox.Show("Error renaming file " + path + ". " + exc.Message);
        return false;
      }
      return true;
    }

    private void cmdRestoreIni_Click(object sender, EventArgs e) {
      string msg = "This will rename all .Ini.BU files to Ini, overwriting any existing Ini files.";
      msg += "\r\n It will then restart the application, using the new Ini files";
      msg += "\r\nClick 'OK' to continue, else 'Cancel'";
      if (MessageBox.Show(msg, MessageBoxButtons.OKCancel) == DialogResult.OK) {
        if (!RestoreIni(Cfg.MainIniFilePath)) return;
        if (!RestoreIni(Cfg.FrmNMColoursIniFilePath)) return;
        if (!RestoreIni(Cfg.FrmSCColoursIniFilePath)) return;
        if (!RestoreIni(Cfg.SwitchIniFilePath)) return;
        if (!RestoreIni(Cfg.PCKBIniFilePath)) return;
        if (!RestoreIni(Cfg.ChordCfgIniFilePath)) return;
        if (!RestoreIni(Cfg.ChordNamesRankIniFilePath)) return;
        if (!RestoreIni(Cfg.FrmTonnetzColoursIniFilePath)) return;

        NoSaveIni = true;
        P.frmSC.RestartApp();
      }
    }

    private static bool RestoreIni(string path) {
      try {
        if (File.Exists(path + ".BU")) {
          File.Delete(path);  //no exception if file does not exist
          File.Move(path + ".BU", path);
        }
      }
      catch (Exception exc) {
        MessageBox.Show("Error restoring file " + path + ". " + exc.Message);
        return false;
      }
      return true;
    }

    private bool CheckRestoreIni() {
      if (!File.Exists(Cfg.MainIniFilePath + ".BU")) return false;
      if (!File.Exists(Cfg.FrmNMColoursIniFilePath + ".BU")) return false;
      if (!File.Exists(Cfg.FrmSCColoursIniFilePath + ".BU")) return false;
      //if (!File.Exists(Cfg.FrmTonnetzColoursIniFilePath + ".BU")) return false;
      if (!File.Exists(Cfg.SwitchIniFilePath + ".BU")) return false;
      if (!File.Exists(Cfg.PCKBIniFilePath + ".BU")) return false;
      return true;
    }

    //private void chkPCKB_CheckedChanged(object sender, EventArgs e) {
    //  P.PCKB = (chkPCKB.Checked) ? new clsPCKB() : null;
    //  if (chkPCKB.Checked) {
    //    chkConstantChordPlayMidiKB.Checked = true;
    //    P.frmSC.optShowPCKBChar.Checked = true;
    //  }
    //}

    private void cmdChordCfg_Click(object sender, EventArgs e) {
      Utils.FormAct(P.frmCfgChords);
    }

    private void cmdRanks_Click(object sender, EventArgs e) {
      if (P.frmChordRanks == null) P.frmChordRanks = new Forms.frmChordRanks();
      Utils.FormAct(P.frmChordRanks);
    }

    private void chkNoAudioSync_CheckedChanged(object sender, EventArgs e) {
      clsAudioSync.SetPlayAudioText(P.F?.AudioSync); 
    }

    private void chkDisablePCKB_CheckedChanged(object sender, EventArgs e) {
      CheckBox chk = (CheckBox)sender;
      if (chk.Checked) clsPCKB.NullifyPCKB(); else P.PCKB = clsPCKB.NewPCKB();
    }

    //private void chkSustainAuto_CheckedChanged_1(object sender, EventArgs e) {
    //  if (chkSustainAuto.Checked) P.frmSC?.SetAutoSustain();
    //}
  }
}
