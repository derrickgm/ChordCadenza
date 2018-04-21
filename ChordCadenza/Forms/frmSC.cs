#undef Testing

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
using System.Runtime.InteropServices;
using System.Threading;

namespace ChordCadenza.Forms {
  //internal interface IFormNuds {
  //  List<NumericUpDown> Nuds { get; }
  //}
  
  internal partial class frmSC : Form, IFormPlayable, IFormStream, IFormProjectName, ITT, IFrmDGV {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    private static frmSC This;
    private delegate void delegSwitchSyncopation();
    private delegate void delSetOptMode(RadioButton opt);
    internal int valShowLowC { get { return valShowLowCDflt + OctTransposeDisplay; } }
    internal static bool AutoCapitalizeStatic = true;
    internal static bool CapitalizeRootsStatic = true;
    internal static int valShowLowCDflt = 60;  //on load chp or unload trk
    internal static int valPlayLoC = 48;
    internal static int valPlayHiC = 108;
    internal static int valBeatHeight = 25;
   // internal static int valFontReduction = 20;
    //internal static bool indSolfa = true;
    //internal static bool indBeats = true;
    internal bool AltFormat = false;
    private clsMTime.clsBBT EndBB;
    //private string FileName = "";
    internal bool Bypass_Event = false;

    internal static bool MenuMonitor = false;
    //internal static bool MenuFilterBank = true;
    //internal static Color ColorAfterBlackNext = Color.SkyBlue;  //background (reverse)
    //internal static Color ColorAfterBlackSame = Color.Yellow;  //background (reverse)
    private bool NoEvent = false;
    internal bool indStopped = true;
    internal int MidiClockCount = 0;
    //internal static Font MainFont = new Font("Consolas", 16);
    //internal static Font ChordFont = new Font("Times New Roman", 14);
    internal static Font MainFont = new Font("Arial", 16);
    internal static Font ChordFont = new Font("Arial", 12, FontStyle.Bold | FontStyle.Italic);
    internal static Color ChordColor = Color.Black;
    //private int MainFontSize = 16;
    internal enum ePlayMode { Chords, KB }
    internal ePlayMode PlayMode = ePlayMode.KB;
    private delegate void delegShowPlayMode();
    internal bool nudCurrentBar_NoEv = false;
    internal clsPlay Play;
    private clsKPos KPos;
    internal int CurrentEvIndex = -1;
    //internal int CurrentEvIndexChords = -1;
    //private string[] RiffLines = new string[12];

    private Color ColorRoot { get { return P.ColorsShowChords.RootNoteColor; } }
    private Color ColorDominant { get { return P.ColorsShowChords.DominantNoteColor; } }
    private Color ColorChord { get { return P.ColorsShowChords.ChordNoteColor; } }

    internal static clsNNDD SyncopationDefault { get { return clsCF.Syncopation; } }
    internal static Mutex Mtx;
    private List<string> RecentProjects;  //most recent first

    //* map extension
    internal Font BarFont = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);
    private Font ChordMapFont = new Font(new FontFamily("Arial"), 10, FontStyle.Regular);
    internal IntDiv HPixPerQI = new IntDiv(1, 1);
    private int _MapCsrQI = 0;
    private int _MapCsrPix = 0;
    private int ScrollMarginLo = 100;
    private int ScrollMarginHi = 200;
    //private static bool ScrollFromPan = false;
    private static bool ScrollFromdgvLyrics = false;
    private static bool ScrollFromPan = false;
    private static bool ScrollFromRefreshBBT = false;
    private DataGridView NoSelectDGV = null;
    private clsPicNoteMapSC PicNMSC;
    internal int VFactor = 2;
    internal List<clsTrks.T> MapTrks;
    private bool Bypass_DGV = false;

    internal int MapCsrPix {
      get {
        return _MapCsrPix;
      }
      set {
        _MapCsrPix = value;
        _MapCsrQI = value / HPixPerQI;
      }
    }
    private int MapCsrQI {
      get {
        return _MapCsrQI;
      }
      set {
        _MapCsrQI = value;
        _MapCsrPix = value * HPixPerQI;
      }
    }

     public DataGridView Prop_dgvLyrics {
      get { return dgvLyrics; }
    }

    public int TicksToPix(int ticks) {
      return (ticks * HPixPerQI) / P.F.TicksPerQI;
    }

    public int TransposeChordNamesVal {
      get { return 0; }
    }

    public int ChordTransposeNotesVal {
      get { return 0; }
    }

    public int ChordTranspose(int pc) {
      return pc;
    }

    public int ChordTransposeReverse(int pc) {
      return pc;
    }

    public void RefreshpicNoteMapFile() {
      return;
    }

    public void SetNoteMapFileChanged(bool createevs, bool undoredo) {
      return;
    }

    private static bool GetMutex() {
      //* return true if mutex exists
      Guid guid = new Guid("3C5CB8B3-77CB-4EEF-8C6D-3FEA869AFEAA");
      string msgsuffix = "\r\nIf so, this instance will start with all midi and audio devices disconnected.";
      msgsuffix += "\r\nYou can reconnect them from the Midi and Audio Config windows if required.";
      msgsuffix += "\r\nThe background colour of windows on this invocation will be cyan.";
      try {
        Mtx = new Mutex(false, guid.ToString());
        if (!Mtx.WaitOne(0)) { //mutex already owned
          Mtx = null;
          string msg = "An instance of Chord Cadenza appears to be already running - do you really want to start another one?";
          msg += msgsuffix;
          //string caption = "Multiple Instances";
          if (MessageBox.Show(msg, MessageBoxButtons.YesNo) != DialogResult.Yes) {
            Environment.Exit(1);
          }
          return true;
        }
      }
      catch (AbandonedMutexException exc) {
        Debug.WriteLine("Abandoned Mutex: " + exc.Message);
        string msg = "An instance of Chord Cadenza appears to have been closed abruptly - do you really want to start another one?";
        msg += msgsuffix;
        if (MessageBox.Show(msg, MessageBoxButtons.YesNo) != DialogResult.Yes) {
          if (Mtx != null) Mtx.ReleaseMutex();
          Environment.Exit(1);
        }
        return true;
      }
      return false;
    }

    public frmSC() {

      //* test code start
      //clsWaitPlay waitplay = new clsWaitPlay();
      //waitplay.Start();
      //* test code end

      InitializeComponent();

      Debug.WriteLine("Machine Name = <" + Environment.MachineName + ">");

      //if (Utils.GetFileVersion() != "???" && Utils.GetFileVersion() != P.Version) {
      //  MessageBox.Show("File Version: " + Utils.GetFileVersion()
      //    + " does not match Program Version: " + P.Version);
      //}

      #if ADVANCED
        P.frmConsole = new Forms.frmConsole();
      #endif

      //CmdsEnable(true);
      Un4seen.Bass.BassNet.Registration("xxx@yyy.zzz", "XXX");

      //P.Forms.Add(this);
      P.frmStart = new Forms.frmStart();
      P.frmSC_Temp = this;
      P.frmSaveMidiFileAs = new dlgSaveMidiFileAs();  //to retain params - not yet shown 
      Cfg.Ind = true;  //--> initialize static Cfg

      //Utils.BackColor = Utils.GetBackColor();
      P.frmStart.PostInit();
      Forms.frmSwitch.InitStatic();
      P.frmCfgChords = new frmCfgChords();  //default values
      MidiPlay.Sync = new clsSync();
      clsF.NewEmpty();

      //bool mutex = GetMutex();
      //if (!mutex) {
      //  try {
      //    MidiPlay.OpenAudioDevs();
      //    MidiPlay.OpenMidiDevs(this, Cfg.MidiInKB, Cfg.MidiInSync, Cfg.MidiOutStream, Cfg.MidiOutKB, false);
      //  }
      //  catch (Exception exc) {
      //    MessageBox.Show("Error opening midi/audio devices: " + exc.Message);
      //  }
      //}

      Bypass_Event = true;
      grpCapitalizeRoots.Enabled = !chkShowTracks.Checked;  //capitalize and showtrks = messy display
                                                            //chkShowChords.Checked = showchords;
      nudOctaves.Value = valOctavesDflt;
      //nudFontReduction.Value = valFontReduction;
      Bypass_Event = false;

      chkAutoCapitalize.Checked = AutoCapitalizeStatic;
      if (!AutoCapitalizeStatic) chkCapitalizeRoots.Checked = CapitalizeRootsStatic;
    }

    //internal void EvsHandler(bool enable) {
    //  //* subscribe/unsubscribe before/after MidiPlay/AudioPlay etc.
    //  if (enable) {
    //    cmdNew.Click += mnuNew_Click;
    //    cmdLoadProject.Click += cmdLoadProject_Click;
    //    cmdSaveProject.Click += mnuSaveProject_Click;
    //    cmdGoToStart.Click += cmdGoToStart_Click;
    //  } else {
    //    cmdNew.Click -= mnuNew_Click;
    //    cmdLoadProject.Click -= cmdLoadProject_Click;
    //    cmdSaveProject.Click -= mnuSaveProject_Click;
    //    cmdGoToStart.Click -= cmdGoToStart_Click;
    //  }
    //}

    //internal void CmdsEnable(bool enable) {
    //  cmdNew.Enabled = enable;
    //  cmdLoadProject.Enabled = enable;
    //  cmdSaveProject.Enabled = enable;
    //  cmdGoToStart.Enabled = enable;
    //}

    private void frmSC_Load(object sender, EventArgs e) {
      if (This != null && This.IsHandleCreated) {
        This.Close();  //not working...
        //* load .chp
        //* open frmStart (and change chkRiffAutoAlignChords)
        //* close frmStart
        //* load same .chp again
        //* get 2 * frmSC
      }
      This = this;

      bool mutex = GetMutex();
      if (!mutex) {
        try {
          MidiPlay.OpenAudioDevs();
        }
        catch (Exception exc) {
          MessageBox.Show("Error opening audio device: " + exc.Message);
        }
        try {
          MidiPlay.OpenMidiDevs(Cfg.MidiInKB, Cfg.MidiInSync, Cfg.MidiOutStream, Cfg.MidiOutKB, false);
        }
        catch (Exception exc) {
          MessageBox.Show("Error opening midi devices: " + exc.Message);
        }
      }
      clsTT.LoadToolTips(this);

      BackColor = Utils.SetBackColor(Mtx, BackColor);
      //if (Utils.BackColor != null) BackColor = Utils.BackColor.Value; 
      //else if (Mtx == null) BackColor = Color.Cyan;

      AddMenuRecentProjects();

      panMaps.Visible = mnuMap.Checked;
      MapsEnabled(mnuMap.Checked);

      #if !ADVANCED
        chkAlignKB.Hide();
        lblKBDisplacement.Hide();
        nudKBDisplacement.Hide();
        nudOctTransposeMulti.Hide();
        //cmdRecord.Hide();
        //cmdWipeAndRecord.Hide();
        //cmdWipeTrack.Hide();
        cmdAlign.Hide();
        mnuDebug.Visible = false;
        optShowPCKBChar.Hide();
        cmdTonnetz.Hide();
        mnuFile.DropDownItems.Remove(mnuLoadMultiMidi);
        lblKeyVel.Hide();
        nudKeyVel.Hide();
        mnuRestart.Visible = false;
        cmdResetPlay.Hide();  //Panic should do the same, and more 
      #endif

      //PopulateCmbFirstNote();

      mnuMonitor.Checked = MenuMonitor;
      ShowRanges();
      picChords.BackColor = P.ColorsShowChords.MainBackgroundColor;
      picBottom.BackColor = P.ColorsShowChords.BottomBackgroundColor;
      txtChordBottom.Font = ChordFont;

      //* form position and size set in P.F.LoadProps() - not here!!!!!!!!
      vScrollBar1.Value = 0;

      if (P.F.CF.Evs == null) P.F.CF.CreateEvs();

      //if (P.F.MaxBBT != null) {
      //  SetStartBar();
      //}
      SetFormTitle();

      PlayableForms.CmdState_Set();

      //Bypass_Event = true;
      //optModeChords.Checked = (Cfg.InitialMode == ePlayMode.Chords);
      //optModeKB.Checked = (Cfg.InitialMode == ePlayMode.KB);
      //Bypass_Event = false;

      //if (optModeChords.Checked) PlayMode = ePlayMode.Chords;
      //else if (optModeKB.Checked) PlayMode = ePlayMode.KB;
      //else {
      //  LogicError.Throw(eLogicError.X045);
      //  PlayMode = ePlayMode.KB;
      //}
      PlayMode = ePlayMode.KB;  //this is the only option that makes sense before loading a project
      Play = clsPlay.NewPlay(PlayMode);
      //chkShowTracks.Checked = (PlayMode == ePlayMode.KB);
      chkShowTracks.Checked = false;

      //Utils.PropsToForm(this);
      Cfg.DictFormProps[Name].SetForm(this);
      LoadSettings();
      ShowRanges();
      ResizeForm();
      //PopulateCmbPlayStyle();
      PopulateCmbPatch(cmbKBChanPatch);
      nudKBChanOut.Value = Cfg.KBOutChan + 1;
      trkKBChanPan_Scroll(null, null);
      trkKBChanVol_Scroll(null, null);

      panTrkAudio.Enabled = (P.F.AudioSync != null && P.F.AudioSync.MP3Player is clsMP3Bass);
      panTrkKB.Enabled = (MidiPlay.OutMKB is clsBassOutMidi);
      panTrkStream.Enabled = (MidiPlay.OutMStream is clsBassOutMidi);
      cmdSaveProject.Enabled = (P.F != null && P.F.SaveProject(null, true, false));
      mnuSaveProject.Enabled = cmdSaveProject.Enabled;

      trkAudioVol_Scroll(null, null);
      trkAudioPan_Scroll(null, null);
      trkKBVol_Scroll(null, null);
      trkKBPan_Scroll(null, null);
      trkStreamVol_Scroll(null, null);
      trkStreamPan_Scroll(null, null);

      if (indShowTracks) LoadTracks(true);
      if (P.frmStart.chkPCKB.Checked) P.PCKB = new clsPCKB();

      SetInitialOcts(Cfg.OctTransposeKBPitch, 0, 0);
      InitSyncopation();
      if (chkSustainAuto.Checked) SetAutoSustain();

      if (File.Exists(Cfg.InitialScreenDatFilePath)) {
        Forms.frmInitial frm = new Forms.frmInitial();
        Action action = delegate() { Utils.FormAct(frm); };
        BeginInvoke(action);
        Utils.FormAct(frm);  
      }
    }

    private void AddMenuRecentProjects() {
      if (!File.Exists(Cfg.RecentProjectsLines)) {
        RecentProjects = new List<string>(12);
        return;
      }

      RecentProjects = Utils.ReadLines(Cfg.RecentProjectsLines);  //Path\Name
      //RecentProjects = new List<KeyValuePair<string, string>>(10);
      //List<string> lines = Utils.ReadLines(Cfg.RecentProjectsLines);  //Name?Path
      //foreach (string l in lines) {
      //  string[] ll = l.Split(new char[] { '?' });
      //  if (ll.Length != 2) {
      //    LogicError.Throw(eLogicError.X158);
      //    return;
      //  }
      //  KeyValuePair<string, string> pair = new KeyValuePair<string, string>(ll[0], ll[1]);
      //  NewRecentProjects.Add(pair);
      //}

      ToolStripItem[] array = new ToolStripItem[RecentProjects.Count];
      for (int i = 0; i < array.Length;  i++) {
        array[i] = new ToolStripMenuItem();
        array[i].Tag = i;
        array[i].Text = RecentProjects[i];
        array[i].Click += Recent_Click;
      }
      mnuRecent.DropDownItems.Clear();
      mnuRecent.DropDownItems.AddRange(array);
    }

    private void Recent_Click(object sender, EventArgs e) {
      ToolStripItem item = (ToolStripItem)sender;
      string dir = Path.GetDirectoryName(item.Text);  //remove "\[Name]"  
      string name = Path.GetFileName(item.Text);  //{Name}
      try {
        if (!Directory.Exists(dir)) {
          MessageBox.Show("Folder " + dir + " not found");
          RemoveRecentProject(item.Text);
          return;
        }
        if (Directory.GetFiles(dir, name + ".*", SearchOption.TopDirectoryOnly).Length == 0) {
          //* may pick up invalid filenames (eg {Name}.test.chp0, but shouldn't matter
          MessageBox.Show("Folder " + dir + " does not contain any files starting with " + name);
          RemoveRecentProject(item.Text);
          return;
        }
        //if (!P.F.CloseCancellableForms()) return;
        MidiPlay.Sync.Stop();
        //string path = P.frmStart.GetProjectFilePath(dir);  //check that dir and a file exists
        //if (!P.frmStart.LoadProject(this, path) /*&& !CheckTextFile(true)*/) {
        if (P.F != null && !P.F.SaveProject(null)) return;  //check and save
        if (!P.frmStart.LoadProject(item.Text + ".xxx", true)) {
          MessageBox.Show("Load Project failed - no valid file found");
          return;
        }
      }
      catch (Exception exc) {
        MessageBox.Show("Load Project failed: " + exc.Message);
      }
    }

    //private void SetToolTips() {
    //  toolTip1.SetToolTip(optModeChords, tip);
    //  toolTip1.SetToolTip(optModeKB, "KBMode ...");
    //}

    //private void MoveHowTo(ToolStripMenuItem item, ToolStripMenuItem to) {
    //  mnuHelpHowTo.DropDownItems.Remove(item);
    //  to.DropDownItems.Add(item);
    //}

    private void InitSyncopation() {
      Bypass_Event = true;
      nudKBChanOut.Value = MidiPlay.KBOutChan + 1;
      //nudRiffChanOut.Value = MidiPlay.KBOutChanAutoRiff + 1;

      nudSyncopationNN.Value = clsCF.Syncopation.NN;
      Forms.frmStart.SetNudAndTag(nudSyncopationDD, clsCF.Syncopation.DD);

      //nudAlternateSyncopationNN.Value = clsCF.AlternateSyncopation.NN;
      //Forms.frmStart.SetNudAndTag(nudAlternateSyncopationDD, clsCF.AlternateSyncopation.DD);

      //SyncopationCurrent = SyncopationDefault;
      Bypass_Event = false;
    }

    internal void NewEmpty() {
      //mnuSaveAutoSyncFile.Enabled = false;
      //mnuRestoreSyncFile.Enabled = false;
      //mnuCreateSyncFile.Enabled = false;
      //vScrollBar1.Maximum = P.F.MaxBBT.Bar;
      nudTransposeKBPitch.Value = 0;
      nudTransposeStreamPitch.Value = 0;
      nudTransposeKB.Value = 0;
      nudStartBar.Value = 1;
      if (P.F?.MaxBBT != null) nudStartBar.Maximum = P.F.MaxBBT.Bar + 1;

      NoEvent = false;
      nudCurrentBar_NoEv = false;
      Bypass_Event = true;
      lstTrks.Items.Clear();
      Bypass_Event = false;
      MapsEnabled(false);
      CurrentEvIndex = -1;
      InitSyncopation();
      HPixPerQI = Forms.frmChordMap.InitQIPerNote();
      PicNMSC = null;
      //ResizeForm();  //update TicksPerBeat
      //Refresh();
    }

    //internal void UpdateRiffLbls() {
    //  lblRiffG.Text = "G: " + GetRiffLine(0);
    //  lblRiffA.Text = "A: " + GetRiffLine(1);
    //  lblRiffB.Text = "B: " + GetRiffLine(2);
    //}

    internal bool MidiOrCFExists() {
      return (P.F.Project.MidiExists || (P.F.CF?.Evs != null && P.F.CF.Evs.Count > 0));
    }

    internal void NewMidiOrCF() {
      if (!panMaps.Visible) return;
      HPixPerQI = Forms.frmChordMap.InitQIPerNote();
      if (P.F.Lyrics.LyricsExist) {
        dgvLyrics.Visible = true;
        P.F.Lyrics.InitDGV(this);
      } else {
        dgvLyrics.Hide();
      }
      //panNoteMap.Visible = MidiOrCFEXists();  

      //nudTrk.Enabled = P.F.Project.MidiExists;
      //if (P.F.Project.MidiExists) {
      //  int trk = (int)nudTrk.Value - 1;
      //  if (trk >= P.F.Trks.NumTrks) {
      //    trk = 0;
      //    nudTrk.Value = 1;
      //  }
      //  Trk = new clsTrks.T(P.F.Trks, trk);
      //  nudTrk.Maximum = P.F.Trks.NumTrks;  //1 to NumTrks
      //  PicNMSC = new clsPicNoteMapSC(picNoteMap, Trk);
      //}

      MapsEnabled(true);
      if (P.F.Project.MidiExists) {
        PopulatelstTrks();
        Bypass_Event = true;
        foreach (clsTrks.T t in MapTrks) {
          int trknum = t.TrkNum;
          if (trknum >= lstTrks.Items.Count) {
            MapTrks.Remove(t);
            continue;
          }
          lstTrks.SetSelected(trknum, true);
        }
        if (MapTrks.Count == 0) MapTrks.Add(new clsTrks.T(P.F.Trks, 0));
        Bypass_Event = false;
        PicNMSC = new clsPicNoteMapSC(picNoteMap);
      } else {
        lstTrks.Items.Clear();
        lstTrks.Hide();
      }

      panMaps.Refresh();
    }

    //private void PopulatecmbTrks() {
    //  foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {
    //    cmbTrk.Items.Add(trk.ToString() + " " + P.F.FSTrackMap.Title[trk]);
    //  }
    //}

    private void PopulatelstTrks() {
      foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {
        lstTrks.Items.Add(trk.ToString() + " " + P.F.FSTrackMap.Title[trk]);
      }
    }

    private void dgvLyrics_CellClick(object sender, DataGridViewCellEventArgs e) {
      //* don't need to use CellEnter - not editable, so only locate by MouseClick
      //* CellEnter would be called when cell changed programmatically
      if (!IsPlayClickable()) return;
      if (Bypass_DGV) return;
      if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.None) return;
      //if (P.F.CurrentBBT.Bar == e.ColumnIndex && P.F.CurrentBBT.BeatsRemBar == e.RowIndex) return;
      //if (P.F.CurrentBBT.Bar == e.ColumnIndex) return;
      Bypass_DGV = true;
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(e.ColumnIndex, 0, 0);  //locate to bar, first beat
      //if (P.F.indDGVReverse) bbt = new clsMTime.clsBBT(bbt.Bar, bbt.TSig.NN - 1, 0);  //last beat of bar

      NoSelectDGV = dgvLyrics;
      P.frmSC.Play?.NewReset();
      clsIShowNoteMap.SetPlayCsr(bbt);  //-> RefreshBBT()
      Refresh();
      NoSelectDGV = null;
      Bypass_DGV = false;
    }

    //private void dgvLyrics_CurrentCellChanged(object sender, EventArgs e) {
    //  //* coded as dgvLyrics_CellEnter(...) in frmTrackMap
    //  if (Bypass_DGV) return;
    //  if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.None) return;
    //  if (dgvLyrics.CurrentCell == null) return;
    //  clsMTime.clsBBT bbt = new clsMTime.clsBBT(dgvLyrics.CurrentCell.ColumnIndex, dgvLyrics.CurrentCell.RowIndex, 0);  //locate to bar/beat
    //  //Debug.WriteLine("frmChordMap: dgvLyrics_CellEnter:"
    //  //  + " CurrentBBT = " + P.F.CurrentBBT.ToStringBase0()
    //  //  + " Col.Row = " + e.ColumnIndex + "." + e.RowIndex);
    //  //if (P.F.CurrentBBT.Bar == e.ColumnIndex && P.F.CurrentBBT.Beats == e.RowIndex) return;
    //  Bypass_DGV = true;
    //  clsIShowNoteMap.SetPlayCsr(bbt);
    //  Refresh();
    //  Bypass_DGV = false;
    //}

    private void dgvLyrics_Scroll(object sender, ScrollEventArgs e) {
      ScrollFromdgvLyrics = true;
      if (!ScrollFromPan && !ScrollFromRefreshBBT) {
        panNoteMap.AutoScrollPosition = new Point(e.NewValue, panNoteMap.AutoScrollPosition.Y);
        //DGV.HorizontalScrollingOffset = e.NewValue;
        Refresh();
      }
      ScrollFromdgvLyrics = false;
    }

    private void SetInitialOcts(int kbpitch, int display, int kb) {
      OctTransposeKBPitch = kbpitch * 12;
      OctTransposeDisplay = display * 12;
      OctTransposeKB = kb * 12;
      AlignKB();
      NewKPos();
    }

    private void AlignKB() {
      ShowRanges();
      if (P.F.CFNotes != null) {
        //lblTrkRange.Text = NoteName.GetPitchStr(P.F.CFNotes.MinPitch, false)
        //  + " - "
        //  + NoteName.GetPitchStr(P.F.CFNotes.MaxPitch, false);
        if (chkAlignKB.Checked) {
          KBDisplacement = P.F.CFNotes.MinPitch.Mod12();
          //if (nudKBDisplacement.Value < -5) clsPlay.OctTransposeKB += 12;
        } else {
          KBDisplacement = 0;
        }
      } else {
        KBDisplacement = 0;
        //lblTrkRange.Text = "";
      }
    }

    internal int KBDisplacement {
      get {
        return (int)nudKBDisplacement.Value;
      }
      set {
        nudKBDisplacement.Value = value;
      }
    }

    internal static int valOctavesDflt = 2;
    internal int valOctaves { get { return (int)nudOctaves.Value; } }
    //internal int valOctaves {
    //  get {
    //    return _valOctaves;
    //  }
    //  set {
    //    _valOctaves = value;
    //    UpdateLblDisplayedRange();
    //  }
    //}

    //private int _valShowLowC = 60;
    //internal int valShowLowC {
    //  get {
    //    return _valShowLowC;
    //  }
    //  set {
    //    _valShowLowC = value;
    //    UpdateLblDisplayedRange();
    //  }
    //}

    internal int OctTransposeKBPitch {
      get {
        return (int)nudOctTransposeKBPitch.Value;
      }
      set {
        nudOctTransposeKBPitch.Value = value;
      }
    }
    internal int OctTransposeKB {
      get {
        return (int)nudOctTransposeKB.Value;
      }
      set {
        nudOctTransposeKB.Value = value;
      }
    }

    internal int OctTransposeDisplay {
      get {
        return (int)nudOctTransposeDisplay.Value;
      }
      set {
        nudOctTransposeDisplay.Value = value;
      }
    }

    internal bool indShowTracks {
      get {
        if (Play == null) return chkShowTracks.Checked;
        if (PlayMode == ePlayMode.Chords) return false;
        //if (PlayMode == ePlayMode.Step) return true;
        //if (Play.KBScale) return false;
        return chkShowTracks.Checked;
      }
    }

    //private clsCFPC CFChords {
    //  get {
    //    return P.F.CF;
    //  }
    //}

    //private clsCF CFNotes {
    //  get {
    //    if (P.F.CFNotes == null) return P.F.CF; else return P.F.CFNotes;
    //    //return (P.F.CFNotes == null) ? P.F.CF : P.F.CFNotes;  //WHY DOESN'T THIS WORK???
    //  }
    //}

    internal int ShowLowPitch {
      get {
        return valShowLowC + KBDisplacement;
      }
    }

    internal int ShowHighPitch {
      get {
        return ShowLowPitch + valOctaves * 12;  //eg C...C
      }
    }


    //internal int Octaves {
    //  get {
    //    //return Forms.frmStart.frmShowChordsSettings.valOctaves;
    //    return Forms.frmSC.valOctaves;
    //  }
    //}

    //internal int LowShowC {
    //  get {
    //    //return Forms.frmStart.frmShowChordsSettings.valShowLowC;
    //    return Forms.frmSC.valShowLowC;
    //  }
    //}

    internal int PlayLoC {
      get {
        //return Math.Min(LowShowC, Forms.frmStart.frmShowChordsSettings.valPlayLoC);
        return Math.Min(valShowLowC, Forms.frmSC.valPlayLoC);
      }
    }

    internal int PlayHiC {
      get {
        //return Math.Max(LowShowC + Octaves * 12, Forms.frmStart.frmShowChordsSettings.valPlayHiC);
        return Math.Max(valShowLowC + valOctaves * 12, Forms.frmSC.valPlayHiC);
      }
    }

    private int PixsPerBeat {
      get {
        return (int)P.frmSC.nudBeatHeight.Value;
      }
    }

    //internal void SwitchSyncopation() {
    //  if (SyncopationCurrent.IsEquiv(SyncopationDefault)) {
    //    SyncopationCurrent = SyncopationAlternate;
    //    SyncopationNonCurrent = SyncopationDefault;
    //    lblSyncopation.ForeColor = SystemColors.ButtonShadow;
    //    lblAlternateSyncopation.ForeColor = SystemColors.ControlText;
    //  } else if (SyncopationCurrent.IsEquiv(SyncopationAlternate)) {
    //    SyncopationCurrent = SyncopationDefault;
    //    SyncopationNonCurrent = SyncopationAlternate;
    //    lblSyncopation.ForeColor = SystemColors.ControlText;
    //    lblAlternateSyncopation.ForeColor = SystemColors.ButtonShadow;
    //  } else {
    //    LogicError.Throw(eLogicError.X101);
    //  }
    //}

    //internal void SwitchSyncopationOpt() {
    //  if (P.frmStart.chkSyncoOnce.Checked && !SyncopationCurrent.IsEquiv(SyncopationDefault)) {
    //    BeginInvoke(new delegSwitchSyncopation(SwitchSyncopation));
    //  }
    //}

    //internal void AddForm(frmShowChords frmsc) {
    //  frmsc.MdiParent = this;
    //  P.F.ListSC.Add(frmsc);
    //  Refresh();
    //}

    public void SetFormTitle() {  //interface IFormProjectName
      SetFormTitle(P.F.Project);
    }

    public void SetFormTitle(clsProject project) {  //interface IFormProjectName
      //this.Text = "PLAYMAP: " + project.PathAndName + " - Chord Cadenza";
      string name = project.CHPPath;  //including CHPExt
      if (name == "") name = project.PathAndName;
      Text = "PLAYMAP: " + name + " - Chord Cadenza";
    }

    //internal static void ReadSettings(string[] f) {  //called by Cfg
    //  string[] ff = f[1].Split(new char[] { ',' });
    //  valOctavesDflt = int.Parse(ff[0]);  //before frmSC loaded
    //  valShowLowCDflt = int.Parse(ff[1]);  //assumes P.frmSC not yet constructed
    //  valPlayLoC = int.Parse(ff[2]);
    //  valPlayHiC = int.Parse(ff[3]);
    //  valBeatHeight = int.Parse(ff[4]);
    //  indSolfa = bool.Parse(ff[5]);
    //  indBeats = bool.Parse(ff[6]);
    //}

    internal static void ReadSettings(string[] f) {  //called by Cfg
      string[] ff = f[1].Split(new char[] { ',' });
      valOctavesDflt = int.Parse(ff[0]);  //before frmSC loaded
      valShowLowCDflt = int.Parse(ff[1]); //assumes P.frmSC not yet constructed
      valPlayLoC = int.Parse(ff[2]);
      valPlayHiC = int.Parse(ff[3]);
      valBeatHeight = int.Parse(ff[4]);
      //indBeats = bool.Parse(ff[5]);  //was ff[6]
    }

    internal void LoadSettings() {
      NoEvent = true;
      nudBeatHeight.Value = valBeatHeight;
      //chkShowSolfa.Checked = indSolfa;
      //chkShowBeats.Checked = indBeats;
      NoEvent = false;
    }

    internal bool InitShowChordsTxt() {
      //string filename = P.F.ProjectPath + ".chp";
      try {
        P.F.CF = new clsCFPC();
      }
      catch (ChordFileException) {
        return false;
      }
      InitShowChords();
      return true;
    }

    internal void InitShowChords() {  
      //FileName = filename;
      //Text = "PLAYMAP   " + P.F.CHPFilePath;
      SetFormTitle();
      Utils.FormAct(this);
      ResizeForm();
    }

    public void FormStreamOnOff(bool on) {
      /*
      panNuds.Enabled = !on;
      cmdGoToStart.Enabled = !on;
      P.frmStart.cmdModulate.Enabled = !on;
      P.frmStart.cmdSwitchKeys.Enabled = !on;
      P.frmStart.cmdMultiMap.Enabled = !on;
      P.frmStart.cmdPanic.Enabled = !on;
      */
      panFiles.Enabled = !on;
      nudKBChanOut.Enabled = !on;
      nudStartBar.Enabled = !on;
      nudTransposeStreamPitch.Enabled = !on;
      mnuFile.Enabled = !on;
      mnuConfig.Enabled = !on;
    }

    internal void Start() {
      if (P.frmSC.Play != null) P.frmSC.Play.ResetPlayChords();
      MidiClockCount = 0;
      //nudCurrentBar_NoEv = true;
      //nudCurrentBar.Value = P.F.StartBar + 1;
      //nudCurrentBar_NoEv = false;
      //P.F.CurrentBBT = new clsMTime.clsBBT((int)nudCurrentBar.Value - 1, 0, 0);
      P.F.CurrentBBT = new clsMTime.clsBBT(StartBar, 0, 0);
      MidiPlay.Sync.BeatCount = P.F.CurrentBBT.Beats;
      SetEndBBTRefresh();
    }

    //internal void InvokeShowPlayMode() {
    //  //BeginInvoke(new delegShowPlayMode(ShowPlayMode));
    //  BeginInvoke(new delegShowPlayMode(ShowRanges));
    //}

    //internal void ShowPlayMode() {
    //  if (PlayMode == ePlayMode.Chords) lblMode.Text = "ChordMode";
    //  else if (PlayMode == ePlayMode.KB) lblMode.Text = "KBMode";
    //  //else if (PlayMode == frmShowChords.ePlayMode.RootC) lblMode.Text = "RootC";
    //  else {
    //    LogicError.Throw(eLogicError.X044);
    //    lblMode.Text = "???";
    //  }
    //}

    internal void ShowRanges() {
      ////* get playmode
      //string mode;
      //if (PlayMode == ePlayMode.Chords) mode = "ChordMode";
      //else if (PlayMode == ePlayMode.KB) mode = "KBMode";
      //else {
      //  LogicError.Throw(eLogicError.X044);
      //  mode = "???";
      //}

      //* get track range
      string trkrange = "";
      if (indShowTracks && P.F.CFNotes != null && P.F.CFNotes.Evs.Count > 0) {
        trkrange = NoteName.GetPitchStr(P.F.CFNotes.MinPitch)
          + " - "
          + NoteName.GetPitchStr(P.F.CFNotes.MaxPitch);
      }

      //* get displayed range
      string disrange;
      disrange = NoteName.GetPitchStr(ShowLowPitch)
        + " - "
        + NoteName.GetPitchStr(ShowHighPitch);

      //* update range labels
      lblRangeVis.Text = disrange;
      lblRangeTrk.Text = trkrange; 
    }

    //internal void ResizeForm() {
    //  if (this.WindowState == FormWindowState.Minimized) return;
    //  int w = ((this.ClientSize.Width - 4) / valOctaves) * valOctaves;  //make width exactly divisible by number of octaves
    //  //int h = ((this.ClientSize.Height - panTop.Height - panTop.Location.Y - picBottom.Height - 5) / PixsPerBeat) * PixsPerBeat;
    //  int pantopheight = (panControls.Visible) ? panControls.Height : 0;
    //  int h = (this.ClientSize.Height - pantopheight - panControls.Location.Y - picBottom.Height - 15);

    //  int beats = h / PixsPerBeat + P.F.CurrentBBT.Beats;
    //  EndBB = new clsMTime.clsBBT(beats, true);
    //  w = ((this.ClientSize.Width - 180) / valOctaves) * valOctaves;  //make width exactly divisible by number of octaves
    //  picChords.ClientSize = new Size(w, h);
    //  picChords.Top = panControls.Top + pantopheight + 5;
    //  picBottom.ClientSize = new Size(w, picBottom.ClientSize.Height);
    //  //picChordNames.Height = picChords.Height - PixsPerBeat;  //first beat unused
    //  picChordNames.Height = picChords.Height;
    //  picChordNames.Location = new Point(picChordNames.Location.X, picChords.Location.Y);
    //  //lblActive.Left = picChordNames.Left;
    //  picChordNames.Invalidate();

    //  vScrollBar1.Height = picChords.Height - PixsPerBeat;  //first beat unused
    //  vScrollBar1.Location = new Point(vScrollBar1.Location.X, picChords.Location.Y);

    //  //picBars.Height = picChords.Height - PixsPerBeat;  //first beat unused
    //  picBars.Height = picChords.Height;  
    //  picBars.Location = new Point(picBars.Location.X, picChords.Location.Y);
    //  picBars.Invalidate();

    //  //GetFonts();
    //  //ChordFont = new Font("Times New Roman", MainFont.Size - 2);
    //  //NewKPos();
    //  KPos = new clsKPos(this, picChords.ClientSize.Width, valOctaves);
    //  //SizeMainFont();
    //  picChords.Invalidate();  //Refresh() doesn't do it!
    //  picBottom.Invalidate();
    //}

    internal void ResizeForm() {
      if (this.WindowState == FormWindowState.Minimized) return;
      if (panMaps.Visible) {
        //* arrange controls in panMaps
        P.F.Lyrics.SetColumnWidth(this);
        if (PicNMSC != null) PicNMSC.SetPicSize(null);
        int hlyrics = (dgvLyrics.Visible) ? dgvLyrics.Height : 0;
        //picChordNamesX.Visible = (P.F.CF?.Evs != null && P.F.CF.Evs.Count != 0);
        //if (picChordNamesX.Visible) {
        if (P.F?.MaxBBT != null) {
          picChordNamesX.Width = (P.F.MaxBBT.Ticks * HPixPerQI) / P.F.TicksPerQI;
        }
        //}
        picNoteMap.Visible = P.F.Project.MidiExists;
        lstTrks.Visible = P.F.Project.MidiExists;
        panNoteMap.Height = SystemInformation.HorizontalScrollBarHeight + 5;
        panNoteMap.Height += (picChordNamesX.Visible) ? picChordNamesX.Height + 5 : 0;
        panNoteMap.Height += (picNoteMap.Visible) ? picNoteMap.Height + 5 : 0;
        picNoteMap.Top = (picChordNamesX.Visible) ? picChordNamesX.Bottom + 5 : 0;
        panNoteMap.Top = dgvLyrics.Top + 5 + hlyrics;
        panMaps.Height = dgvLyrics.Top + hlyrics + panNoteMap.Height + 15;
        //panNoteMap.Left = (lstTrks.Visible) ? lstTrks.Right + 5 : 5;
        //dgvLyrics.Left = panNoteMap.Left;
        //picBarsX.Left = panNoteMap.Left;
      }

      int w = ((this.ClientSize.Width - 4) / valOctaves) * valOctaves;  //make width exactly divisible by number of octaves
      int pancontrolsheight = (panControls.Visible) ? panControls.Height : 0;
      int panmapsheight = (panMaps.Visible) ? panMaps.Height : 0;
      panControls.Top = panmapsheight + mnuFile.Height + 5;

      SetEndBBTNoRefresh();

      w = ((this.ClientSize.Width - 180) / valOctaves) * valOctaves;  //make width exactly divisible by number of octaves
      int h = (this.ClientSize.Height - pancontrolsheight - panControls.Location.Y - picBottom.Height - 15);
      picChords.ClientSize = new Size(w, h);
      picChords.Top = panControls.Top + pancontrolsheight + 5;
      picBottom.ClientSize = new Size(w, picBottom.ClientSize.Height);
      picChordNames.Height = picChords.Height;
      picChordNames.Location = new Point(picChordNames.Location.X, picChords.Location.Y);
      picChordNames.Invalidate();

      vScrollBar1.Height = picChords.Height - PixsPerBeat;  //first beat unused
      vScrollBar1.Location = new Point(vScrollBar1.Location.X, picChords.Location.Y);

      picBars.Height = picChords.Height;
      picBars.Location = new Point(picBars.Location.X, picChords.Location.Y);
      picBars.Invalidate();

      KPos = new clsKPos(this, picChords.ClientSize.Width, valOctaves);
      picChords.Invalidate();  //Refresh() doesn't do it!
      picBottom.Invalidate();
    }

    internal void SetEndBBTRefresh() {
      SetEndBBTOpts(true);
    }

    internal void SetEndBBTNoRefresh() {
      SetEndBBTOpts(false);
    }

    private void SetEndBBTOpts(bool indrefresh) {
      if (P.F.CurrentBBT == null) P.F.SetEmpty(clsCF.DefaultSongLength);
      int pancontrolsh = (panControls.Visible) ? panControls.Height : 0;
      int h = (this.ClientSize.Height - pancontrolsh - panControls.Location.Y - picBottom.Height - 15);
      int beatsdiff = h / PixsPerBeat;
      int beats = (P.F?.CurrentBBT == null) ? beatsdiff : beatsdiff + P.F.CurrentBBT.Beats;
      //* don't display picBottom if form resized near to panControls bottom
      picBottom.Visible = (beatsdiff > 1);
      txtChordBottom.Visible = (beatsdiff > 1);
      EndBB = new clsMTime.clsBBT(beats, true);
      if (indrefresh) Refresh();
    }

    //internal void GetFonts() {
    //int octwidth = picChords.ClientSize.Width / Octaves;
    //if (octwidth > 500) MainFontSize = 16;
    //else if (octwidth > 350) MainFontSize = 14;
    //else if (octwidth > 250) MainFontSize = 12;
    //else MainFontSize = 10;

    //MainFont = new Font(MainFont.Name, MainFontSize);
    //ChordFont = new Font("Times New Roman", MainFont.Size - 2);
    //}

    internal void ShowKeysTextBox(int ticks) {
      //show this
      clsKeyTicks key = P.F.Keys[ticks, kbtrans: true];
      //string keystr = key.KBTrans_KeyNoteStr;
      string keystr = key.KeyNoteStr;
      keystr = NoteName.ToSharpFlat(keystr);
      lblKeyThis.Text = keystr + " " + key.Scale;

      //show next
      key = P.F.Keys.GetNext(ticks, kbtrans: true);
      if (key == null) lblKeyNext.Text = "";
      else {
        //keystr = key.KBTrans_KeyNoteStr;
        keystr = key.KeyNoteStr;
        keystr = NoteName.ToSharpFlat(keystr);
        lblKeyNext.Text = NoteName.ToSharpFlat(keystr) + " " + key.Scale;
      }
    }

    //internal void ActivateMain() {
    //  ActivateMdiChild(P.F.frmShowChords);
    //}

    //internal void ActNext() {
    //  for (int i = 0; i < MdiChildren.Length; i++) {
    //    if (MdiChildren[i] == ActiveMdiChild) {
    //      //string txt = "active child: " + ActiveMdiChild.Name + "(" + i + ")";
    //      int j = (i + 1) % MdiChildren.Length;
    //      ActivateMdiChild(MdiChildren[j]);
    //      ActivateMdiChild(MdiChildren[j]);  //needed doing twice in frmMDIParent
    //      //txt += " -> " + MdiChildren[j].Name + "/" + ActiveMdiChild.Name + "(" + j + ")";
    //      Refresh();
    //      //Debug.WriteLine(txt);
    //      return;
    //    }
    //  }
    //  if (MdiChildren.Length > 0) ActivateMdiChild(MdiChildren[0]);
    //  //Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!");
    //  Refresh();
    //}

    //internal void AddChild(frmShowChords frm) {
    //  //* add new mdichild so that it displays at top of horizontal tiling
    //  frmShowChords[] mdichildren = new frmShowChords[MdiChildren.Length];
    //  for (int i = 0; i < MdiChildren.Length; i++) {
    //    mdichildren[i] = (frmShowChords)MdiChildren[i];
    //    MdiChildren[i].MdiParent = null;
    //  }
    //  frm.MdiParent = this;
    //  for (int i = 0; i < mdichildren.Length; i++) {
    //    mdichildren[i].MdiParent = this;
    //  }
    //  Refresh();
    //}

    //internal void AddChild(frmShowChords frm) {
    //  //* setting MdiParent appears to change frm height and changes picBottom anchoring
    //  //* use offset to reposition picBottom to anchor
    //  int offset = frm.Height - frm.picBottom.Top;
    //  frm.MdiParent = this;
    //  frm.picBottom.Top = frm.Height - offset;
    //  frm.lblActive.Top = frm.Height - offset;
    //}

    //internal void ExecLayout() {
    //  foreach (frmShowChords f in MdiChildren) {
    //    //@@f.FormBorderStyle = FormBorderStyle.Sizable;
    //  }
    //  ResizeForm();
    //  foreach (frmShowChords f in MdiChildren) {
    //    //@@f.FormBorderStyle = FormBorderStyle.FixedSingle;
    //  }
    //  Refresh();
    //}

    //internal void Tile() {
    //  //int top = panCmds.Bottom;
    //  int stupidfrigbecausemdisarecrap = 5;
    //  int top = 0;
    //  int bottom = ClientSize.Height;
    //  int childheight = (bottom - panCmds.Bottom) / MdiChildren.Length;
    //  int j = 0;
    //  for (int i = 0; i < P.F.ListSC.Count; i++) {  //[0] = main
    //    frmShowChords f = P.F.ListSC[i];
    //    //@@if (i > 0) f.picBottom.Top = P.F.ListSC[0].picBottom.Top;  
    //    if (chkActiveAtBottom.Checked) {
    //      if (f == (frmShowChords)ActiveMdiChild) {
    //        f.Top = top + (P.F.ListSC.Count - 1) * childheight;  //bottom position
    //      } else {
    //        f.Top = top + j++ * childheight;
    //      }
    //    } else {  //main on bottom
    //      f.Top = top + (P.F.ListSC.Count - 1 - i) * childheight;
    //    }
    //    f.Height = childheight - stupidfrigbecausemdisarecrap;
    //    f.Width = ClientSize.Width - stupidfrigbecausemdisarecrap;
    //    f.Left = 0;
    //    f.Refresh();
    //    //f.WindowState = FormWindowState.Maximized;
    //    //Size s = f.Size;
    //  }
    //  Refresh();
    //}

    //*** events start here *****************************************************************

    private void cmdGoToStart_Click(object sender, EventArgs e) {
      //P.frmSC.nudStartBar.Value = 1;
      P.F.StartBar = 0;
      Start();
      SetEndBBTRefresh();
      P.frmSC.Play?.NewReset();
      Forms.frmStart.RefreshBBT(P.F.CurrentBBT);  //RefreshBBTOthers();
    }

    private void cmdColors_Click(object sender, EventArgs e) {
      Utils.FormAct(P.ColorsShowChords.FrmColours);  //show new colours as they are changed
    }

    private void mnuRanges_Click(object sender, EventArgs e) {
      //if (P.frmCfgBezier == null) P.frmSCOctaves = new Forms.frmSCOctaves(P.frmSC);
      if (P.frmSCOctaves == null) P.frmSCOctaves = new Forms.frmSCOctaves(P.frmSC);
      Utils.FormAct(P.frmSCOctaves);
      SetEndBBTRefresh();
      P.F.Panic();
    }

    //private void cmdSaveRecord_Click(object sender, EventArgs e) {
    //  Cursor.Current = Cursors.WaitCursor;
    //  P.F.FileStreamMM.WipeRecord();
    //  MidiPlay.Sync.StartRecord(this, P.F.FileStreamMM, P.F.Mute);
    //  Cursor.Current = Cursors.Default;
    //  cmdSaveRecord.Enabled = false;
    //}

    //private void cmdWipeAndRecord_Click(object sender, EventArgs e) {
    //  //for (int trk = 0; trk < P.F.FileStreamMM.NumTrks - 1; trk++) {  //not last trk (rectrk)
    //  //  if (!P.F.Mute[trk] && P.F.Chan[trk] == MidiPlay.KBOutChan) {
    //  //    string msg = "Out KB Channel " + (MidiPlay.KBOutChan + 1) + " already in use - continue recording?";
    //  //    if (MessageBox.Show(msg, "Confirm Record", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
    //  //  }
    //  //}
    //  //P.frmStart.Hide();
    //  Start();
    //  if (!P.F.FSTrackMap.InitRecNew(true)) {
    //    MessageBox.Show("Multiple or missing track selection on Multimap - recording terminated");
    //    return;
    //  }
    //  MidiPlay.Sync.StartPlay(this, P.F.FSTrackMap, P.F.Mute);
    //  SetRecordColour(cmdWipeAndRecord, true);
    //  //cmdWipeAndRecord.BackColor = Color.Red;
    //  //cmdWipeAndRecord.UseVisualStyleBackColor = false;
    //}

    //private void cmdRecord_Click(object sender, EventArgs e) {
    //  //P.frmStart.Hide();
    //  Start();
    //  if (!P.F.FSTrackMap.InitRecNew(false)) {
    //    MessageBox.Show("Multiple or missing track selection on Multimap - recording terminated");
    //    return;
    //  }
    //  MidiPlay.Sync.StartPlay(this, P.F.FSTrackMap, P.F.Mute);
    //  SetRecordColour(cmdRecord, true);
    //  //cmdRecord.BackColor = Color.Red;
    //  //cmdRecord.UseVisualStyleBackColor = false;
    //}

    //private Stopwatch TempSW = new Stopwatch();
    private void cmdPlayMidi_Click(object sender, EventArgs e) {
      Start();
      MidiPlay.Sync.StartPlay(this, P.F.FSTrackMap, P.F.Mute);
    }

    private void cmdStopPlay_Click(object sender, EventArgs e) {
      //Debug.WriteLine("TempSW elapsed = " + TempSW.ElapsedMilliseconds);
      MidiPlay.Sync.Stop();
    }

    private void cmdPausePlay_Click(object sender, EventArgs e) {
      MidiPlay.Sync.Pause();
      //P.F.FileStreamMM.SaveRecord();  //only if RecStrm not empty
    }

    //internal void SetAutoSyncRecordActive() {
    //  //* after first switchkey in AutoSyncBeats
    //  cmdRecordSync.BackColor = Color.Red;
    //  cmdRecordSync.UseVisualStyleBackColor = false;
    //}

    //internal void SetRecordInactive() {
    //  //* normal colour
    //  cmdRecordSync.BackColor = SystemColors.Control;
    //  cmdRecordSync.UseVisualStyleBackColor = true;
    //}

    //private void cmdOctDecr_Click(object sender, EventArgs e) {
    //  OctTranspose = Math.Max(OctTranspose - 12, -48);
    //  Play.SetTransposeKBPitch();
    //}

    //private void cmdOctIncr_Click(object sender, EventArgs e) {
    //  OctTranspose = Math.Min(OctTranspose + 12, 48);
    //  Play.SetTransposeKBPitch();
    //}

    //private void nudCurrentBar_ValueChanged(object sender, EventArgs e) {
    //  if (!nudCurrentBar_NoEv) {
    //    P.F.CurrentBBT = new clsMTime.clsBBT((int)nudCurrentBar.Value - 1, 0, 0);
    //    ResizeForm();
    //    P.frmStart.RefreshBBT(P.F.CurrentBBT);
    //  }
    //  int maxbar = Math.Max(1, new clsMTime.clsBBT(P.F.MaxTicks).Bar);
    //  vScrollBar1.Value = Math.Max(0, 100 - (100 * ((int)nudCurrentBar.Value - 1)) / maxbar);
    //}

    //internal void CloseForm(Forms.frmSC frm) {
    //  //FrmSCClosedByUser = false;
    //  Utils.FormToProps(this);  //not really needed
    //  //this.Close();
    //  frm.Close();
    //}

    //internal bool CloseSC = false;  //true if frmsc (and app) is going to close
    internal void frmSC_FormClosing(object sender, FormClosingEventArgs e) {
      if (Bypass_Event) return;
      if (MidiPlay.Sync.indPlayActive == clsSync.ePlay.MidiStream
      || MidiPlay.Sync.indPlayActive == clsSync.ePlay.AudioStream) MidiPlay.Sync.Stop();

      if (P.F?.frmLyrics != null && P.F.frmLyrics.indTextChanged) {
        DialogResult res = MessageBox.Show("Lyrics text has changed - update Lyrics?", MessageBoxButtons.YesNoCancel);
        if (res == DialogResult.Yes) P.F.frmLyrics.UpdateText();
        else if (res == DialogResult.Cancel) {
          e.Cancel = true;
          return;
        }
      }

      if (P.F != null && !P.F.SaveProject(null, false, true)) {  //check and save (with cancel button)
        e.Cancel = true;
        return;  
      }

      if (!P.frmStart.NoSaveIni) {
        if (!P.frmStart.chkExitPrompt.Checked) {
          Forms.frmStart.SaveAllIni();
        } else {
          Forms.dlgExit frmexit = new dlgExit();
          DialogResult res = frmexit.ShowDialog(this);
          if (res == DialogResult.Cancel) {
            e.Cancel = true;
            return;
          }
          if (res != DialogResult.Ignore) Forms.frmStart.SaveAllIni();
        }
      }

      Cfg.DictFormProps[Name] = new clsFormProps(this);
#if DEBUG
      LogicError.ShowTotals();
#endif
    }

    //private void frmMSC_Resize(object sender, EventArgs e) {
    //  ResizeForm();
    //}

    //private int ResizeCnt;
    private void frmSC_ResizeEnd(object sender, EventArgs e) {
      //Debug.WriteLine(++ResizeCnt + ": frmSC ResizeEnd");
      ResizeForm();
    }

    //private void frmMSC_ResizeBegin(object sender, EventArgs e) {
    //  foreach (frmShowChords f in P.F.ListSC) {
    //    //@@f.FormBorderStyle = FormBorderStyle.Sizable;
    //    //Application.DoEvents();
    //    ResizeForm();
    //    //Application.DoEvents();
    //  }
    //}

    //private void frmMSC_ResizeEnd(object sender, EventArgs e) {
    //  foreach (frmShowChords f in P.F.ListSC) {
    //    frmMSC_ResizeEnd(null, null);
    //  }
    //}

    //private void frmMSC_Shown(object sender, EventArgs e) {
    //  foreach (frmShowChords f in MdiChildren) {
    //    f.Refresh();
    //    //@@f.FormBorderStyle = FormBorderStyle.FixedSingle;
    //  }
    //}

    //private void chkActiveAtBottom_CheckedChanged(object sender, EventArgs e) {
    //  Tile();
    //}

    //private void frmMSC_MdiChildActivate(object sender, EventArgs e) {
    //  if (chkActiveAtBottom.Checked) Tile();
    //  P.F.frmShowChordsActive = (frmShowChords)ActiveMdiChild;
    //  foreach (frmShowChords f in MdiChildren) {
    //    if (f == ActiveMdiChild) f.lblActive.Text = "Active"; else f.lblActive.Text = ""; 
    //  }
    //}

    private void chkShowBeats_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      Refresh();
    }

    private void chkShowCurrent_CheckedChanged(object sender, EventArgs e) {
      Refresh();
    }

    //private void cmdWipeTrack_Click(object sender, EventArgs e) {
    //  if (!P.F.FSTrackMap.InitRecWipe()) {
    //    MessageBox.Show("Multiple or missing track selection on Multimap - recording terminated");
    //    return;
    //  }
    //  //P.F.FileStreamMM.WipeTrack();
    //  if (P.F.frmTrackMap != null) P.F.frmTrackMap.UpdateTrk(P.F.FSTrackMap.RecTrk);
    //  //cmdWipeTrack.Enabled = false;
    //}

    private void nudStartBar_ValueChanged(object sender, EventArgs e) {
      //nudCurrentBar.Value = nudStartBar.Value;
      _StartBar = (int)nudStartBar.Value - 1;
      P.F.CurrentBBT = new clsMTime.clsBBT(_StartBar, 0, 0);
      Forms.frmStart.RefreshBBT(P.F.CurrentBBT);
      //if (P.F != null) P.F.StartBar = (int)nudStartBar.Value - 1;
      //Play?.Reset();
    }

    private int _StartBar = 0;
    internal int StartBar { get { return _StartBar; } }

    private void nudBeatHeight_ValueChanged(object sender, EventArgs e) {
      if (P.frmSC == null) return;
      int val = (int)nudBeatHeight.Value;
      if (val <= 5) nudBeatHeight.Increment = 1; else nudBeatHeight.Increment = 5;
      if (val == 6) nudBeatHeight.Value = 10;
      if (P.frmSC.NoEvent) return;
      P.frmSC.SetEndBBTRefresh();
    }

    //internal void trkTempo_Scroll(object sender, EventArgs e) {
    //  P.frmStart.trkTempo.Value = trkTempo.Value;  //should not call scroll event
    //  P.frmStart.trkTempo_Scroll(null, null);
    //}

    internal void trkKBChanVol_Scroll(object sender, EventArgs e) {
      if (MidiPlay.OutMKB != null) MidiPlay.OutMKB.SendShortMsg(0xb0 | MidiPlay.KBOutChan, 7, trkKBChanVol.Value);  //7 = chanvol
    }

    //internal void trkRiffChanVol_Scroll(object sender, EventArgs e) {
    //  if (MidiPlay.OutMKB != null) MidiPlay.OutMKB.SendShortMsg(0xb0 | MidiPlay.KBOutChanAutoRiff, 7, trkRiffChanVol.Value);  //7 = chanvol
    //}

    internal void trkKBChanPan_Scroll(object sender, EventArgs e) {
      if (MidiPlay.OutMKB != null) MidiPlay.OutMKB.SendShortMsg(0xb0 | MidiPlay.KBOutChan, 10, trkKBChanPan.Value);  //10 = pan
    }

    //internal void trkRiffChanPan_Scroll(object sender, EventArgs e) {
    //  if (MidiPlay.OutMKB != null) MidiPlay.OutMKB.SendShortMsg(0xb0 | MidiPlay.KBOutChanAutoRiff, 10, trkRiffChanPan.Value);  //10 = pan
    //}

    //internal void UpdateTempo(int miditempo, int ticks) {
    //  int dd = P.F.MTime.GetTSig(ticks).DD;
    //  lblTempo.Text = ((dd * 60000000) / (4 * miditempo)).ToString();
    //  //lblTempo.Text = (miditempo / 10000).ToString();
    //}

    //private void UpdatenudCurrentBar() {
    //  nudCurrentBar_NoEv = true;
    //  nudCurrentBar.Value = Math.Min(nudCurrentBar.Maximum, P.F.CurrentBBT.Bar + 1);
    //  nudCurrentBar_NoEv = false;
    //  int maxbar = Math.Max(new clsMTime.clsBBT(P.F.MaxTicks).Bar, 1);
    //  int val = Math.Max(100 - (100 * (int)nudCurrentBar.Value) / maxbar, 1);
    //  vScrollBar1.Value = Math.Max(Math.Min(val, vScrollBar1.Maximum), vScrollBar1.Minimum);
    //}

    internal static void SyncBarActive(bool val) {
      //* enable/disable cmdStartPlay 
      //* called from AutoSyncBar.Sync()
      //* show as red if disabled (Sync running)
      if (!val && P.F != null && P.F.AudioSync != null) P.F.AudioSync.SyncBarOff();
      foreach (IFormPlayable frm in PlayableForms.Active) {
        foreach (Button cmd in frm.Cmds_Stop) {
          cmd.BackColor = (val) ? Color.Red : SystemColors.Control;
          cmd.UseVisualStyleBackColor = !val;
          cmd.Enabled = val;
        }
        foreach (Button cmd in frm.Cmds_Locate) cmd.Enabled = !val;
      }
    }

    //*** IPlay interface *****************************************************

    public void RefreshBBT(clsMTime.clsBBT bbt) {
      if (bbt.MTime != P.F.MTime) return;
      MidiClockCount = MidiPlay.Sync.MidiClockCount;
      //SuspendLayout();
      P.F.CurrentBBT = bbt;
      if (panMaps.Visible) {
        MapCsrQI = P.F.CurrentBBT.Ticks / P.F.TicksPerQI;
        if (dgvLyrics.Visible) {
          Forms.frmChordMap.SelectBarBeat(null, dgvLyrics, NoSelectDGV, bbt.Bar, bbt.BeatsRemBar);
        }
        if (chkScroll.Checked) {  //scroll each time
          SetScroll();
        } else {  //scroll only when margins reached
          int vismin = -panNoteMap.AutoScrollPosition.X;
          int vismax = vismin + panNoteMap.ClientSize.Width;
          if (vismax - MapCsrPix < ScrollMarginHi || MapCsrPix - vismin < ScrollMarginLo) {
            SetScroll();
          }
        }
      }
      //ResizeForm();
      SetEndBBTRefresh();
      //ResumeLayout();
    }

    public void StartSub(clsMTime.clsBBT bbt) {  //start playing midi file (IPlayable interface)
      Debug.WriteLine("frmShowChords: StartSync: bbt = " + bbt.Bar + " " + bbt.BeatsRemBar + " " + bbt.TicksRemBeat);
      //Debug.WriteLine("frmShowChords: StartSync: bbt = " + bbt.Bar + " " + bbt.Beat + " " + bbt.RemTicks);
      MidiClockCount = MidiPlay.Sync.MidiClockCount;
      P.F.CurrentBBT = bbt;
      /////////NewPlay();
      //CF.ChordPlayStart();
      //UpdatenudCurrentBar();
      ResizeForm();
    }

    public Button[] Cmds_Locate { get { return new Button[] { cmdGoToStart }; } }
    //public Button[] Cmds_Play { get { return new Button[0]; } }
    //public Button[] Cmds_Play_Midi { get { return new Button[] { cmdPlayMidi, cmdWipeAndRecord, cmdRecord, cmdWipeTrack, }; } }
    public Button[] Cmds_Play_Midi { get { return new Button[] { cmdPlayMidi }; } }
    public Button[] Cmds_Play_And_Sync { get { return new Button[] { cmdPlayAndRecordAudio }; } }
    //public Button[] Cmds_Record_Sync { get { return new Button[] { cmdSyncAudio }; } }
    //public Button[] Cmds_Record_Sync { get { return new Button[0]; } }
    public Button[] Cmds_Play_Audio { get { return new Button[] { cmdPlayAudio }; } }
    public Button[] Cmds_Sync_Audio { get { return new Button[] { cmdSyncAudio }; } }
    public Button[] Cmds_Stop { get { return new Button[] { cmdStopPlay }; } }
    public Button[] Cmds_Pause { get { return new Button[] { cmdPausePlay }; } }

    //public void StreamPlayDisable() {
    //  bool val = false;  //true doesn't make sense
    //  cmdStartPlay.Enabled = val;
    //  cmdWipeAndRecord.Enabled = val;
    //  cmdRecord.Enabled = val;
    //  cmdMultiMap.Enabled = val;
    //  //P.frmStart.cmdMultiMapNP.Enabled = val;
    //  cmdStopPlay.Enabled = val;
    //  cmdPausePlay.Enabled = val;
    //  //if (val) {
    //  //  cmdRecord.BackColor = SystemColors.Control;
    //  //  cmdRecord.UseVisualStyleBackColor = true;
    //  //  cmdWipeAndRecord.BackColor = SystemColors.Control;
    //  //}
    //}

    //public void StreamPlayOn() {
    //  cmdStartPlay.Enabled = false;
    //  cmdWipeAndRecord.Enabled = false;
    //  cmdRecord.Enabled = false;
    //  cmdMultiMap.Enabled = false;
    //  //P.frmStart.cmdMultiMapNP.Enabled = false;
    //  cmdStopPlay.Enabled = true;
    //  cmdPausePlay.Enabled = true;
    //}

    //public void StreamPlayOff() {
    //  cmdStartPlay.Enabled = true;
    //  cmdWipeAndRecord.Enabled = true;
    //  cmdRecord.Enabled = true;
    //  cmdMultiMap.Enabled = true;
    //  //P.frmStart.cmdMultiMapNP.Enabled = true;
    //  cmdStopPlay.Enabled = false;
    //  cmdPausePlay.Enabled = false;
    //  cmdRecord.BackColor = SystemColors.Control;
    //  cmdRecord.UseVisualStyleBackColor = true;
    //  cmdWipeAndRecord.BackColor = SystemColors.Control;
    //}

    //public void SyncPlayOn() {
    //  cmdStartPlay.Enabled = false;
    //  cmdWipeAndRecord.Enabled = false;
    //  cmdRecord.Enabled = false;
    //  cmdMultiMap.Enabled = false;
    //  //P.frmStart.cmdMultiMapNP.Enabled = false;
    //  cmdStopPlay.Enabled = false;
    //  cmdPausePlay.Enabled = false;
    //}

    //*** end if IPlay interface *************************************

    //internal void SetStartBar() {
    //  clsMTime.clsBBT bbt = new clsMTime.clsBBT(P.F.MaxTicks);
    //  //nudCurrentBar.Maximum = 1 + bbt.Bar;
    //  //nudStartBar.Maximum = nudCurrentBar.Maximum;
    //}

    //internal void NewPlay() {
    //  switch (PlayMode) {
    //    case ePlayMode.Chords:
    //      Play = new clsPlayChords();
    //      return;
    //    case ePlayMode.KB:
    //      Play = new clsPlayKeyboard();
    //      return;
    //    default:
    //      LogicError.Throw(eLogicError.X046);
    //      Play = new clsPlayKeyboard();
    //      return;
    //  }
    //}

    internal void ReInitPlayMode() {
      if (Play == null) {
        if (PlayMode == ePlayMode.Chords) {
          Play = new clsPlayChords();
        } else if (PlayMode == ePlayMode.KB) {
          Play = new clsPlayKeyboard();
        }
      } else {
        if (PlayMode == ePlayMode.Chords) {
          Play = new clsPlayChords(Play.LastActiveKeyOn, Play.LastActivePitchOn, Play);
        } else if (PlayMode == ePlayMode.KB) {
          Play = new clsPlayKeyboard(Play.LastActiveKeyOn, Play.LastActivePitchOn, Play);
        }
      }
    }

    //private Color picChordsBackColor;

    private void ShowRectNote(Graphics xgr, Pen pen, Brush brush, int onpix, int offpix, int pitch) {
      int width = KPos.GetKeyWidth(pitch);
      int height = onpix - offpix;
      int? x = KPos[pitch];
      if (!x.HasValue) return;
      xgr.FillRectangle(brush, x.Value, offpix, width, height);
      xgr.DrawLine(pen, x.Value, offpix, x.Value + width, offpix);
    }

    private void ShowSolfaNote(Graphics xgr, Brush brush, int onpix, int ontime, 
    int pitch, bool root) {
      //* called indirectly from clsPlayKeyboard & clsRootC
      //* get sf, adjusted for ModeRootC (%12 done by Notename.GetSolfa)
      string sf;
      if (P.PCKB != null && optShowPCKBChar.Checked) {
        int pp = pitch - ShowLowPitch;
        sf = (pp < P.PCKB.Chars.Length) ? P.PCKB.Chars[pp] : "?";
      } else {
        sf = NoteName.GetNoteNameOrSolfa(pitch, P.F.Keys[ontime, kbtrans: true]);
      }
      //if (CapitalizeRootsStatic && !chkShowTracks.Checked && root) sf = sf.ToUpper();
      if (CapitalizeRootsStatic && !indShowTracks && root) {
        sf = sf.Substring(0, 1).ToUpper() + sf.Substring(1);  //first char only
      }
      int? pos = KPos[pitch];
      if (!pos.HasValue) return;
      RectangleF rectf = new Rectangle(pos.Value, onpix - valBeatHeight, KPos.GetKeyWidth(pitch), valBeatHeight);
      ShowSolfaNote(xgr, brush, sf, rectf);
    }

    //private void ShowSolfaNoteRiff(Graphics xgr, Brush brush, int onpix, int offpix,
    //int pitchpos, int pitchsf, clsKey key, bool cap) {
    //  //* called from ShowChordNoteRiff
    //  string sf = NoteName.GetNoteNameOrSolfa(pitchsf, key);
    //  int? pos = KPos[pitchpos];
    //  if (!pos.HasValue) return;
    //  //if (pitchpos.Mod12() == 0) sf = sf.ToUpper();
    //  if (cap) sf = sf.ToUpper();
    //  RectangleF rectf = new Rectangle(pos.Value, onpix - valBeatHeight, KPos.GetKeyWidth(pitchpos), valBeatHeight);
    //  ShowSolfaNote(xgr, brush, sf, rectf);
    //}

    private void ShowSolfaNote(Graphics xgr, Brush brush, string sf, RectangleF rectf) {
      StringFormat fmt = new StringFormat();
      fmt.Alignment = StringAlignment.Center;  //horizontal
      fmt.LineAlignment = StringAlignment.Center;  //vertical 
      xgr.DrawString(sf, MainFont, brush, rectf, fmt);
    }

    //private int? GetStringPos(int pitch, string sf) {
    //  int? xstart = KPos[pitch];
    //  if (!xstart.HasValue) return null;
    //  int xmid = xstart.Value + KPos.GetKeyWidth(pitch) / 2;
    //  int pos = xmid - (MainFontSize / 2 + 2);
    //  if (sf.Length > 1) pos -= MainFontSize / 4 + 1;
    //  if (pos < xstart) pos = xstart.Value;
    //  return pos;
    //}

    internal void ShowChordNote(Graphics xgr, clsCFPC.clsEv.sPitchPC pitchpc, int chordontime, 
      int noteontime, int offtime, bool root, bool dom, bool showrect, bool roundup) {
      //* called from clsPlayKeyboard
      //* show note for each octave on displayed keyboard
      if (!pitchpc.indPC && (pitchpc.PitchPC < ShowLowPitch || pitchpc.PitchPC > ShowHighPitch)) return;
      Brush brushchar = P.ColorsShowChords.SolfaBrush;
      Brush brushmelody;
      if (pitchpc.indPC) {  //pitchclass (show chords)
        //if (CapitalizeRootsStatic && !chkShowTracks.Checked && root) brushmelody = P.ColorsShowChords.RootNoteBrush;
        //else if (CapitalizeRootsStatic && !chkShowTracks.Checked && dom) brushmelody = P.ColorsShowChords.DominantNoteBrush;
        //else brushmelody = P.ColorsShowChords.ChordNoteBrush;
        if (CapitalizeRootsStatic && !indShowTracks && root) brushmelody = P.ColorsShowChords.RootNoteBrush;
        else if (CapitalizeRootsStatic && !indShowTracks && dom) brushmelody = P.ColorsShowChords.DominantNoteBrush;
        else brushmelody = P.ColorsShowChords.ChordNoteBrush;
      } else {  //pitch (show tracks)
        brushmelody = P.ColorsShowChords.TrackNoteBrush;
      }
      Pen blackpen = new Pen(Color.Black);  //width 1

      int onpixchord = TicksToPixs(chordontime);
      int onpixchordrndup = (roundup) ? TicksToPixsUpStart(chordontime) : TicksToPixs(chordontime);
      int onpixnoterndup = (roundup) ? TicksToPixsUpStart(noteontime) : TicksToPixs(noteontime);
      int onpixsf = (chkRunChordNotes.Checked) ?onpixnoterndup : onpixchordrndup;
      int offpix = TicksToPixs(offtime);
      int[] pitches;
      if (!pitchpc.indPC) {  //actual pitch, not pitchclass
        pitches = new int[] { pitchpc.PitchPC };
      } else {
        pitches = KPos.GetPitchesPC(pitchpc.PitchPC);
      }
      //if (P.frmSC.chkShowSolfa.Checked) {  //show solfa characters
        foreach (int p in pitches) {
          if (showrect) {  //show rectangles
            ShowRectNote(xgr, blackpen, brushmelody, onpixchord, offpix, p);
          }
          ShowSolfaNote(xgr, brushchar, onpixsf, chordontime, p, root);  
        }
      //} else {
      //  foreach (int p in pitches) {
      //    ShowRectNote(xgr, brushmelody, onpixchord, offpix, p);
      //  }
      //}
    }

    /*riffnew*/
    //internal void ShowScaleRiff(Graphics xgr, int cmajpc, int chordpc, clsCF.clsEv ev) {
    //  Brush brushchar = P.ColorsShowChords.SolfaBrush;
    //  Brush brushmelody = P.ColorsShowChords.ChordNoteBrush;
    //  int onpix = TicksToPixsUpStart(ev.OnTime);
    //  int offpix = TicksToPixs(ev.OffTime);
    //  int[] pitches = KPos.GetPitchesPC(cmajpc);
    //  int[] chord = null;
    //  if (ev.PlayChord != null) chord = ev.PlayChord.Chord;
    //  if (chord == null) chord = new int[0];
    //  clsKey key = P.F.Keys[ev.OnTime];
    //  foreach (int pitch in pitches) {
    //    int pcsf = (pitch + (chordpc - cmajpc)).Mod12();  //used for solfa only
    //    if (pcsf == chord[0]) brushmelody = P.ColorsShowChords.RootNoteBrush;
    //    bool cap = (chord.Contains(pcsf)) ? true : false; 
    //    ShowRectNote(xgr, brushmelody, onpix, offpix, pitch);
    //    ShowSolfaNoteRiff(xgr, brushchar, onpix, offpix, pitch, pcsf, key, cap);
    //  }
    //}

    //internal void ShowChordNoteRiff(Graphics xgr, clsCF.clsEv ev, int pc, int pcsf) {
    //  Brush brushchar = P.ColorsShowChords.SolfaBrush;
    //  Brush brushmelody = P.ColorsShowChords.ChordNoteBrush;
    //  int onpix = TicksToPixsUpStart(ev.OnTime);
    //  int offpix = TicksToPixs(ev.OffTime);
    //  int[] pitches = KPos.GetPitchesPC(pc);
    //  clsKey key = P.F.Keys[ev.OnTime];
    //  if (pcsf == ev.PlayChord.Chord[0]) brushmelody = P.ColorsShowChords.RootNoteBrush;
    //  foreach (int pitch in pitches) {
    //    ShowRectNote(xgr, brushmelody, onpix, offpix, pitch);
    //    ShowSolfaNoteRiff(xgr, brushchar, onpix, offpix, pitch, pcsf, key, false);
    //  }
    //}

    internal void ShowNullChord(Graphics xgr, int ontime) {
      int onpix = TicksToPixs(ontime);
      for (int p = Play.KBLo; p <= Play.KBHi; p++) {
        int? pos = KPos[p];
        if (!pos.HasValue) continue;
        //xgr.DrawString("X", MainFont, P.ColorsShowChords.SolfaBrush, pos.Value, onpix - MainFont.Height);
        RectangleF rectf = new RectangleF(pos.Value, onpix - valBeatHeight, KPos.GetKeyWidth(p), valBeatHeight);
        ShowSolfaNote(xgr, P.ColorsShowChords.SolfaBrush, "X", rectf);
      }
    }

    internal void ShowChord(Graphics xgr, clsPlay.clsChordEvTimed playchord, bool roundup) {
      //called from clsPlayChords - not clsPlayKeyboard
      int ontime = playchord.OnTime;
      //int offtime = playchord.OffTime;
      int onpix = (roundup) ? TicksToPixsUpStart(ontime) : TicksToPixs(ontime);
      //int offpix = TicksToPixs(offtime);
      //int height = PixsPerBeat;
      //int height = (int)MainFont.GetHeight(); 
      for (int p = Play.KBLo; p <= Play.KBHi; p++) {
        int n = playchord[p];
        if (n < 0) continue;
        int note = n.Mod12();
        string sf;
        if (P.PCKB != null && optShowPCKBChar.Checked) {
          int pp = n - ShowLowPitch;
          sf = (pp < P.PCKB.Chars.Length && pp >= 0) ? P.PCKB.Chars[pp] : "?";
        } else {
          sf = NoteName.GetNoteNameOrSolfa(note, P.F.Keys[ontime, kbtrans: true]);
        }
        //int? pos = GetStringPos(p, sf);
        int? pos = KPos[p];
        if (!pos.HasValue) continue;
        Color c = ColorChord;
        //if ((callcount == 1 || callcount == 2) && blackpitch >= 0 && blackdir != 0) {
        //  int diff = n - blackpitch;
        //  if (diff == 0) c = ColorAfterBlackSame;
        //  else {
        //    if (blackdir == 1 && (diff == 1 || diff == 2)) c = ColorAfterBlackNext;
        //    else if (blackdir == -1 && (diff == -1 || diff == -2)) c = ColorAfterBlackNext;
        //  }
        //}
        if (c == ColorChord) {  //not root or dominant
          if (playchord.IsRoot(note)) c = ColorRoot;
          if (playchord.IsDominant(note)) c = ColorDominant;
        }
        //if (CapitalizeRootsStatic && !chkShowTracks.Checked && playchord.IsRoot(note)) sf = sf.ToUpper();
        if (CapitalizeRootsStatic && !indShowTracks && playchord.IsRoot(note)) {
          sf = sf.Substring(0, 1).ToUpper() + sf.Substring(1);  //first char only
        }
        if (c != ColorChord) {
          int? x = KPos[p];
          if (!x.HasValue) continue;
          int width = KPos.GetKeyWidth(p);
          //xgr.FillRectangle(new SolidBrush(c), x.Value, onpix - height, width, height);
          xgr.FillRectangle(new SolidBrush(c), x.Value, onpix - valBeatHeight, width, valBeatHeight);
        }
        //Brush brush = new SolidBrush(playchord.Color);
        Brush brush = P.ColorsShowChords.SolfaBrush;

        //xgr.DrawString(sf, MainFont, brush, pos.Value, onpix - MainFont.Height);
        RectangleF rectf = new RectangleF(pos.Value, onpix - valBeatHeight, KPos.GetKeyWidth(p), valBeatHeight);
        ShowSolfaNote(xgr, brush, sf, rectf);
      }
    }

    internal void ShowCurrentNote(PictureBox pic, Graphics xgr, int kb) {
      //Debug.WriteLine("Show Current Note: " + kb);
      //* show actual note being played (or similar)
      if (kb < ShowLowPitch || kb > ShowHighPitch) return;
      int? cx = KPos[kb] + KPos.GetKeyWidth(kb) / 2;  //centre of circle (x co-ord)
      if (!cx.HasValue) return;
      int d = Math.Min(KPos.GetBlackHalfWidth(), PixsPerBeat);  //diameter of circle
      int r = d / 2;

      int cy = (pic == picBottom) ?
      pic.ClientSize.Height / 2 :  //centre of circle (y co-ord)
      pic.ClientSize.Height - PixsPerBeat / 2;  //centre of circle (y co-ord)

      //if ((pic == picChords && P.ColorsShowChords.ShowPlayedNote(1))
      //|| (pic == picBottom && P.ColorsShowChords.ShowPlayedNote(0)))
      //  xgr.FillEllipse(P.ColorsShowChords.PlayedNoteBrush, cx.Value - r, cy - r, d, d);  //x,y,w,h
      //if (pic == picChords && P.ColorsShowChords.ShowPlayedNote(2)) {
      //  cy -= PixsPerBeat;
      //  xgr.FillEllipse(P.ColorsShowChords.PlayedNoteBrush, cx.Value - r, cy - r, d, d);  //x,y,w,h
      //}
      if (pic == picBottom && P.ColorsShowChords.ShowPlayedNote()) {
        xgr.FillEllipse(P.ColorsShowChords.PlayedNoteBrush, cx.Value - r, cy - r, d, d);  //x,y,w,h
      }
    }



    private int TicksToPixs(int ticks) {  //convert ticks to picShowChords Height
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
      return TicksToPixs(bbt);
    }

    private int TicksToPixsUpStart(int ticks) {  //convert ticks to picShowChords Height
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
      return TicksToPixsRoundUpStart(bbt);
    }

    private int TicksToPixsRoundUpStart(clsMTime.clsBBT bbt) {  //convert ticks to picShowChords Height
      return _TicksToPixs(bbt, true);
    }

    private int TicksToPixs(clsMTime.clsBBT bbt) {  //convert ticks to picShowChords Height
      return _TicksToPixs(bbt, false);
    }

    private int _TicksToPixs(clsMTime.clsBBT bbt, bool roundup) {  //convert ticks to picShowChords Height
      int pix = picChords.ClientSize.Height + PixsPerBeat * (P.F.CurrentBBT.Beats - bbt.Beats);
      if (bbt.Twelfth != 0) pix -= (bbt.Twelfth * PixsPerBeat) / 12;
      int pixstart = PixsPerBeat * (EndBB.Beats - P.F.CurrentBBT.Beats + 1);
      if (roundup) {
        if (pix >= pixstart) {
          int rempix = picChords.ClientSize.Height - PixsPerBeat * (EndBB.Beats - P.F.CurrentBBT.Beats);
          return pixstart - (PixsPerBeat - rempix);
        }
      }
      if (pix < 0) return 0;
      //if (pix >= picChords.ClientSize.Height) return picChords.ClientSize.Height;
      //if (pix < 0) return 0;
      return pix;
    }

    ////*** class clsKPos **************************************************************
    //internal class clsKPos {
    //  internal int OctWidth;
    //  private int[] Q = new int[] { 0, 5, 9, 15, 19, 24, 29, 33, 38, 42, 47, 51 };
    //  private int LastQ = 56;
    //  private int[,] Pix;  //[oct, pitchclass]
    //  internal int[] KeyLen = new int[12];
    //  internal const int ExtraOcts = 2;  //kbdisplacement

      //  //construct when picShowChord width or kbdisplacement changed
      //  internal clsKPos(frmSC frmsc, int width, int octaves) {  //picShowChords.ClientSize.Width
      //    Pix = new int[octaves + 2, 12];
      //    for (int oct = 0; oct < Pix.GetLength(0); oct++) {  //incl. extra octave
      //      for (int pc = 0; pc < 12; pc++) Pix[oct, pc] = -1;  //-1 = OOR, don't display
      //    }
      //    int kbdisp = ((int)frmsc.nudKBDisplacement.Value).Mod12();
      //    OctWidth = width / octaves;  //pixels

      //    int offsetoct = (frmsc.nudKBDisplacement.Value < 0) ? -LastQ * 3 : -LastQ * 2;
      //    if (frmsc.nudKBDisplacement.Value == 12) offsetoct = -LastQ;
      //    int offset = (kbdisp > 0) ? LastQ - Q[12 - kbdisp] : 0;
      //    for (int oct = 0; oct < Pix.GetLength(0); oct++) {  //incl. extra octave
      //      for (int pc = 0; pc < 12; pc++) {  //pc = pitchclass
      //        int pos = (pc + kbdisp).Mod12();  //pos = position onscreen (1st displayed octave)
      //        if (pc == 0) offsetoct += LastQ;
      //        int abspos = offsetoct + offset + Q[pc];
      //        if (abspos > octaves * LastQ) break;
      //        Pix[oct, pc] = ((offsetoct + offset + Q[pc]) * OctWidth) / LastQ;  //avoid rounding errors 0,42,...
      //      }
      //    }
      //    for (int pc = 0; pc < 12; pc++) {
      //      if (pc < 11) KeyLen[pc] = ((Q[pc + 1] - Q[pc]) * OctWidth) / LastQ;
      //      else KeyLen[pc] = ((LastQ - Q[11]) * OctWidth) / LastQ;
      //    }
      //  }

      //  internal int? GetPix(int pitchclass, int oct) {
      //    if (oct < 0 || oct >= Pix.GetLength(0)) return null;
      //    int ret = Pix[oct, pitchclass];
      //    if (ret >= 0) return ret; else return null;
      //  }

      //*** class clsKPos **************************************************************
    internal class clsKPos {
      internal int OctWidth;
      private int[] Q = new int[] { 0, 5, 9, 15, 19, 24, 29, 33, 38, 42, 47, 51, 56 }; //[0-12]
      private string Black = "010100101010";
      //private int LastQ = 56;
      private int[] Pix;  //[pitch]
      private int[] KeyWidth = new int[128];  //[pitchclass]
      private List<int>[] PitchesPC;  //[pc][oct]  PC -> pitches
      internal int MinKeyWidth = int.MaxValue;

      //construct when picShowChord width or kbdisplacement changed
      internal clsKPos(frmSC frmsc, int width, int octaves) {  //picShowChords.ClientSize.Width
        PitchesPC = new List<int>[12];
        for (int pc = 0; pc < 12; pc++) PitchesPC[pc] = new List<int>(6);  
        Pix = new int[128];
        for (int p = 0; p < 128; p++) Pix[p] = -1;  //-1 = OOR, don't display
        int kbdisp = frmsc.KBDisplacement;  //0-11  eg 1 = C#..C# ; -1 = B..B
        int lowc = frmsc.valShowLowC;
        OctWidth = width / octaves;  //pixels

        //* calc Pix[pitch] and PitchesPC
        int offset = (kbdisp < 0) ? Q[12 + kbdisp] - Q[12] : Q[kbdisp];  //eg C#=1 ; B=-1 
        int offsetoct = (kbdisp <= 0) ? -1 : 0;
        int lo = lowc + kbdisp;
        if (lo < 0) lo = 0;
        int hi = 1 + lowc + kbdisp + octaves * 12;  //extra one at end needed to calc KeyWidth .//hi OOR...
        if (hi > 127) hi = 127;
        for (int p = lo; p <= hi; p++) {
          int pc1 = kbdisp.Mod12();  //0-11
          int pc = p.Mod12();
          if (pc == 0) offsetoct++;
          //Pix[p] = ((Q[pc] - offset + Q[12] * offsetoct) * OctWidth) / Q[12];
          int qpos = Q[pc] - offset + Q[12] * offsetoct;
          Pix[p] = (qpos * width) / (Q[12] * octaves + Q[1]);  //last one used to get KeyWidth
          if (p == hi) break;  //don't need extra one here
          PitchesPC[p.Mod12()].Add(p);
        }

        for (int p = lo; p < hi; p++) {
          KeyWidth[p] = Pix[p + 1] - Pix[p];  //last one invalid
          if (KeyWidth[p] < MinKeyWidth) MinKeyWidth = KeyWidth[p];
        }
        Pix[hi] = -1;
        if (hi >= 12) KeyWidth[hi] = KeyWidth[hi - 12];

        ////* calc KeyWidth[pc]
        //for (int pc = 0; pc < 12; pc++) {
        //  KeyWidth[pc] = ((Q[pc + 1] - Q[pc]) * OctWidth) / Q[12];
        //}

        ////* calc PitchesPC
        //for (int p = lowc + kbdisp; p < lowc + kbdisp + octaves * 12; p++) {
        //  PitchesPC[p.Mod12()].Add(p);
        //}
      }

      internal int? this[int pitch] {
        get {
          if (pitch < 0 || pitch >= Pix.Length) return null;
          if (Pix[pitch] < 0) return null;
          return Pix[pitch];
        }
      }

      internal int[] GetPitchesPC(int pc) {
        return PitchesPC[pc].ToArray();
      }

      internal int GetKeyWidth(int pitch) {
        if (KeyWidth[pitch] <= 0) {
          LogicError.Throw(eLogicError.X098);
          return 0;
        }
        return KeyWidth[pitch];
      }

      internal bool IsBlack(int pitch) {
        return (Black[pitch.Mod12()] == '1');
      }

      internal int GetBlackHalfWidth() {  //use with caution (rounding errors!)
        return OctWidth / 28;  //approx
      }
    }

    private void picBars_Paint(object sender, PaintEventArgs e) {
      if (P.F?.MTime == null) return;
      Graphics xgr = e.Graphics;
      Brush brush = new SolidBrush(ChordColor);
      for (int bar = P.F.CurrentBBT.Bar; bar <= EndBB.Bar; bar++) {
        int onpix = TicksToPixsRoundUpStart(new clsMTime.clsBBT(bar, 0, 0));
        xgr.DrawString((bar + 1).ToString(), ChordFont, brush, 5, onpix - ChordFont.Height);
      }
    }

    private void picBottom_Paint(object sender, PaintEventArgs e) {
      if (P.F?.MTime == null) return;
      Graphics bgr = e.Graphics;
      //NewKPos();
      bgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      ShowKeyboard(picBottom, bgr);
      if (Play != null) Play.ShowPicBottom(bgr);
      clsPlay.ShowCurrentNotes((PictureBox)sender, bgr);
    }

    //private int PaintSeq = 0; 
    private void picChords_Paint(object sender, PaintEventArgs e) {
      //Debug.WriteLine("frmSC.picChords_Paint seq: " + (++PaintSeq));
      if (P.F?.MTime == null) return;
#if Testing
      Stopwatch sw = new Stopwatch();
      sw.Start();
      for (int i = 0; i < 100; i++) {
#endif
      //Debug.WriteLine(DateTime.Now +  " frmShowChords.picChords_Paint");
      Graphics chgr = e.Graphics;
      //NewKPos();
      chgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      //MainFont = SizeFont(chgr, "m-");
      ShowBarBeats(chgr);
      ShowKeys(chgr);
      ShowKeyboard(picChords, chgr);
      ShowChords(chgr);
      //if (Play != null) Play.ShowCurrentNotes((PictureBox)sender, chgr);
      clsPlay.ShowCurrentNotes((PictureBox)sender, chgr);
#if Testing
      }
      Debug.WriteLine("frmShowChords Paint millisecs = " + sw.ElapsedMilliseconds);
#endif
    }

    //private void SizeMainFont() {
    //  //* return resized solfa Font that will accommodate txt 
    //  Graphics xgr = picChords.CreateGraphics();  //should also work for picBottom
    //  int minsize = 8;
    //  Font font = new Font(MainFont.Name, minsize);
    //  int s = minsize;
    //  float factor = 1;
    //  //if (valFontReduction > 0) factor = (float)(100 - valFontReduction) / 100f;
    //  factor = (float)(100 - valFontReduction) / 100f;
    //  for (; s < 24; s++) {  //s = fontsize
    //    Font newfont = new Font(MainFont.Name, s);
    //    SizeF sizef = xgr.MeasureString("m-", newfont);  //should be maximum solfa width
    //    if (sizef.Width >= (float)KPos.MinKeyWidth * factor) break;
    //    font = newfont;
    //  }
    //  //MainFont = new Font(MainFont.Name, Math.Max(minsize, s - 4));
    //  MainFont = font;
    //}

    private void picChordNames_Paint(object sender, PaintEventArgs e) {
      if (P.F?.MTime == null) return;
      if (P.F.CF == null) return;
      if (!chkShowChordNames.Checked && !chkShowChordNotes.Checked) return;
      Graphics xgr = e.Graphics;
      //Font MainFont = new Font("Arial", 14);
      Brush brush = new SolidBrush(ChordColor);
      for (int i = 0; i < P.F.CF.Evs.Count; i++) {
        clsCFPC.clsEv ev = P.F.CF.Evs[i];
        clsKeyTicks key = P.F.Keys[ev.OnTime, kbtrans: true];
        if (ev.OffTime <= P.F.CurrentBBT.Ticks) continue;
        if (ev.OnTime >= EndBB.Ticks) break;
        int onpix = TicksToPixsRoundUpStart(ev.OnBBT);

        if (chkShowChordNames.Checked) {
          string name;
          if (chkShowChordsRel.Checked) {
            name = ev.ChordNameRoman(key, eKBTrans.None);
          } else {
            name = ev.ChordName(eKBTrans.Add, kbtranskey: true);
          }
          onpix -= ChordFont.Height;
          if (chkShowChordNotes.Checked) onpix -= ChordFont.Height;
          xgr.DrawString(name, ChordFont, brush, 5, onpix);
        }

        if (chkShowChordNotes.Checked && ev.Notes.Length > 0) {  //not null
          ////* check for overlap with previous chord
          //if (chkShowChordNames.Checked && i > 0 && P.F.CF.Evs[i - 1].OnBBT.Beats == ev.OnBBT.Beats - 1) return;

          Font fontreg = new Font(ChordFont, FontStyle.Regular);
          //Font fontbold = new Font(ChordFont, FontStyle.Bold);
          string strnotes = "";
          int start = cmbFirstNote.SelectedIndex - 1;  //-1=root, 0=C, ...
          if (start < 0) {  //start at root
            for (int j = 0; j < ev.Notes.Length; j++) {
              clsCF.clsEv.clsNote n = ev.Notes[j];
              //strnotes += NoteName.GetNoteName(key.MidiKey, n.PC_NoKBTrans) + " ";
              //strnotes += NoteName.GetNoteName(key.MidiKey, n.PC_KBTrans) + " ";
              strnotes += NoteName.GetNoteName(key, n.PC[eKBTrans.Add]) + " ";
            }
          } else {  //start at selected note
            //* find first note(PC) at or above start(PC)  
            int minindex = -1, min = 12;
            for (int j = 0; j < ev.Notes.Length; j++) {
              //int test = (ev.Notes[j].PC_NoKBTrans - start).Mod12();
              //int test = (ev.Notes[j].PC_KBTrans - start).Mod12();
              int test = (ev.Notes[j].PC[eKBTrans.Add] - start).Mod12();
              if (test < min) {
                min = test;
                minindex = j;
              }
            }
            if (min == 12 || minindex == -1) {
              LogicError.Throw(eLogicError.X125);
              return;
            }
            List<int> noteslist = new List<int>(5);
            for (int j = 0; j < ev.Notes.Length; j++) {
              int k = j + minindex;
              if (k >= ev.Notes.Length) k %= ev.Notes.Length;
              //strnotes += NoteName.GetNoteName(key.MidiKey, ev.Notes[k].PC_NoKBTrans).TrimEnd() + " ";
              //strnotes += NoteName.GetNoteName(key.MidiKey, ev.Notes[k].PC_KBTrans).TrimEnd() + " ";
              strnotes += NoteName.GetNoteName(key, ev.Notes[k].PC[eKBTrans.Add]).TrimEnd() + " ";
            }
          }
          if (chkShowChordNames.Checked) {
            onpix += ChordFont.Height;
          } else {
            onpix -= ChordFont.Height;
          }
          xgr.DrawString(NoteName.ToSharpFlat(strnotes), fontreg, brush, 5, onpix);
        }
      }
    }

    //private void ShowChords(Graphics chgr) {   //.chp (not .csv)
    //  //* all playmodes (main picbox)
    //  //* called from picChords_Paint
    //  clsCF cfshow = (chkShowTracks.Checked) ? CFNotes : P.F.CF;
    //  int callcount = 0;

    //  //* show chord and calc CurrentEvIndexNotes
    //  for (int i = 0; i < cfshow.Evs.Count; i++) {
    //    clsCFPC.clsEv ev = cfshow.Evs[i];
    //    if (ev.OffTime <= P.F.CurrentBBT.Ticks) continue;
    //    if (ev.OnTime >= EndBB.Ticks) break;
    //    callcount++;
    //    if (callcount == 1) CurrentEvIndexNotes = i;
    //    if (Play != null) Play.ShowPicChord(ev, i, chgr, callcount);
    //  }

    //  //* show keys
    //  P.frmSC.ShowKeysTextBox(P.F.CurrentBBT.Ticks);

    //  //* calc CurrentEvIndexChords (-> ThisPlayChord, NextPlayChord, NextNextPlayChord
    //  if (cfshow == P.F.CF) CurrentEvIndexChords = CurrentEvIndexNotes;
    //  else {
    //    callcount = 0;
    //    for (int i = 0; i < CFChords.Evs.Count; i++) {
    //      clsCFPC.clsEv ev = CFChords.Evs[i];
    //      if (ev.OffTime <= P.F.CurrentBBT.Ticks) continue;
    //      if (ev.OnTime >= EndBB.Ticks) break;
    //      callcount++;
    //      if (callcount == 1) CurrentEvIndexChords = i;
    //    }
    //  }
    //}

    private void ShowChords(Graphics chgr) {   //.chp (not .csv)
      //* all playmodes (main picbox)
      //* called from picChords_Paint
      int callcount = 0;

      //* show P.F.CF chord and calc CurrentEvIndexNotes
      if (P.F.CF != null && chkShowChords.Checked && Play != null) {
        for (int i = 0; i < P.F.CF.Evs.Count; i++) {
          clsCFPC.clsEv ev = P.F.CF.Evs[i];
          if (ev.OffTime <= P.F.CurrentBBT.Ticks) continue;
          if (ev.OnTime >= EndBB.Ticks) break;
          callcount++;
          if (callcount == 1) CurrentEvIndex = i;
          Play.ShowPicChord(ev, chgr, true);
        }
      }

      //* show P.F.CFNotes
      //if (P.F.CFNotes != null && chkShowTracks.Checked && PlayMode == ePlayMode.KB
      //&& Play != null && Play.Riff == null) {
      if (P.F.CFNotes != null && indShowTracks 
      && PlayMode == ePlayMode.KB) {
        for (int i = 0; i < P.F.CFNotes.Evs.Count; i++) {
          clsCFPC.clsEv ev = P.F.CFNotes.Evs[i];
          if (ev.OffTime <= P.F.CurrentBBT.Ticks) continue;
          if (ev.OnTime >= EndBB.Ticks) break;
          clsPlayKeyboard.ShowPicChordStatic(ev, chgr, false);  
        }
      }

      //* show keys
      P.frmSC.ShowKeysTextBox(P.F.CurrentBBT.Ticks);
    }

    //private void ShowKey(TextBox textbox, int evindex) {
    //  clsCFPC.clsEv ev = CFChords.Evs[evindex];
    //  clsKey key = P.F.Keys[ev.OnTime];
    //  string keystr = key.KBTrans_KeyNoteStr;
    //  keystr = NoteName.ToSharpFlat(keystr);
    //  textbox.Text = keystr + " " + key.Scale;
    //}

    private void ShowKeyboard(PictureBox pic, Graphics xgr) {
      float penwidth = 1;
      Pen pen = new Pen(Color.Black, penwidth);  //lines
      pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
      //int alpha = 64;  //opacity (0 - 255) of black key
      //Brush BlackKeyboardBrush = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0));
      int keyheight = pic.Height;

      //* show white note delimiters
      xgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
      for (int pitch = ShowLowPitch; pitch <= ShowHighPitch; pitch++) {
        int? pos;
        pos = KPos[pitch];
        if (!pos.HasValue) continue;
        int pc = pitch.Mod12();
        if ((pc == 0 && pitch != ShowLowPitch) || pc == 5) { //C or F (not first C)
          xgr.DrawLine(pen, pos.Value, 0, pos.Value, keyheight);  //line between 2 adjacent white notes (CD)
        }
        DrawBlock(xgr, P.ColorsShowChords.BlackKeyboardBrush, keyheight, pitch);  //black notes
      }
      xgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

      //calculate shownotes circle diameter
      //max = minmum of black note width and beat height
      //Diameter = Math.Min(KPos.KeyLen[1], PixsPerBeat) * CirclePercent / 100;
    }

    private void DrawBlock(Graphics xgr, Brush brush, int keyheight, int pitch) {
      int? kpos = KPos[pitch];
      if (!kpos.HasValue) return;
      int width = KPos.GetKeyWidth(pitch);
      if (!KPos.IsBlack(pitch)) return;
      xgr.FillRectangle(brush, kpos.Value, 0, KPos.GetKeyWidth(pitch), keyheight);
    }

    private void ShowBarBeats(Graphics xgr) {
      //Debug.WriteLine("ShowBeats entered");
      xgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
      if (P.F.CurrentBBT.TicksRemBeat != 0) {
        LogicError.Throw(eLogicError.X047);
      }
      //* update for possible new TicksPerQNote
      if (P.F.CurrentBBT.Ticks == 0) P.F.CurrentBBT = new clsMTime.clsBBT(0);
      //P.F.CurrentBBT = new clsMTime.clsBBT(P.F.CurrentBBT.Ticks); 

      Pen beatpen = new Pen(Color.Black, 1);
      Pen barpen = new Pen(Color.Black, 2);
      if (!P.frmSC.chkShowBeats.Checked) barpen = new Pen(Color.Black, 1);
      //Brush highlightbrush = new SolidBrush(Color.Yellow);
      Brush highlightbrush = P.ColorsShowChords.CurrentBackgroundBrush;
      beatpen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
      Pen pen;
      int w = picChords.ClientSize.Width;
      bool first = true;

      for (clsMTime.clsBBT bbt = P.F.CurrentBBT.Copy(); bbt.Ticks <= EndBB.Ticks; bbt.NextBeat()) {
        pen = beatpen;
        if (bbt.BeatsRemBar == 0) pen = barpen;
        //else if (!chkShowBeats.Checked) continue;
        //int p = TicksToPixsBeats(bbt);
        int p = TicksToPixs(bbt);
        if (first && bbt.Ticks >= P.F.CurrentBBT.Ticks) {  //highlight current beat
          int y = p - PixsPerBeat;
          //if (AltFormat) y = p - PixsPerBeat * 2;
          xgr.FillRectangle(highlightbrush, 0, y, w, PixsPerBeat);
          //DrawBigBlocks(xgr, highlightbrush, p - PixsPerBeat);
          first = false;
        }
        if (bbt.BeatsRemBar == 0 || P.frmSC.chkShowBeats.Checked) xgr.DrawLine(pen, 0, p, w, p);
      }

      xgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
    }

    private void ShowKeys(Graphics xgr) {
      xgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
      Pen pen = new Pen(Color.Blue, 8);
      pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
      clsKeyTicks[] keys = P.F.Keys.GetChanges(P.F.CurrentBBT.Ticks, EndBB.Ticks);
      int w = picChords.ClientSize.Width;
      foreach (clsKeyTicks key in keys) {
        int p = TicksToPixs(key.Ticks);
        xgr.DrawLine(pen, 0, p, w, p);
      }
      xgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
    }

    internal void ShowBottomNote(Graphics bgr, int pitch) {
      //* show note on picBottom
      //* pitch = actual pitch played (subject to keyboard transposition) 

      Brush brush = P.ColorsShowChords.ScaleBrush;
      string sf = "";
      if (P.PCKB != null) {
        int p = pitch - ShowLowPitch;
        sf = (p < P.PCKB.Chars.Length) ? P.PCKB.Chars[p] : "?";
      } else {
        int sfpitch = pitch.Mod12();
        clsKeyTicks key = P.F.Keys[P.F.CurrentBBT.Ticks, kbtrans: true];
        sf = NoteName.GetNoteNameOrSolfa(sfpitch, key);
      }
      int? pos = KPos[pitch];
      if (!pos.HasValue) return;
      RectangleF rectf = new Rectangle(pos.Value, 0, KPos.GetKeyWidth(pitch), picBottom.ClientSize.Height);
      ShowSolfaNote(bgr, brush, sf, rectf);
    }

    //private void cmdTempoReset_Click(object sender, EventArgs e) {
    //  //trkTempo.Value = 0;
    //  //trkTempo_Scroll(sender, e);
    //  P.frmStart.trkTempo.Value = 0;
    //  P.frmStart.trkTempo_Scroll(sender, e);
    //}

    private void frmSC_DragDrop(object sender, DragEventArgs e) {
      P.frmStart.frmStart_DragDrop(sender, e);
    }

    private void frmSC_DragEnter(object sender, DragEventArgs e) {
      P.frmStart.frmStart_DragEnter(sender, e);
    }

    //private void vScrollBar1_Scroll(object sender, ScrollEventArgs e) {
    //  //* assumes vScrollBar1.Minimum = 0
    //  int maxbar = new clsMTime.clsBBT(P.F.MaxTicks).Bar;
    //  int max = vScrollBar1.Maximum - vScrollBar1.LargeChange + 1;
    //  int bar = ((max - vScrollBar1.Value) * maxbar) / max;
    //  bar = Math.Min(maxbar, Math.Max(0, bar));
    //  clsMTime.clsBBT bbt = new clsMTime.clsBBT(bar, 0, 0);
    //  P.F.StartBar = bbt.Bar;
    //  //nudStartBar.Value = P.F.StartBar + 1;
    //  //P.frmStart.RefreshBBT();
    //}

    internal bool ScrollEvActive = false;
    private void vScrollBar1_Scroll(object sender, ScrollEventArgs e) {
      if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.None) return;
      P.F.StartBar = e.NewValue;
      P.F.CurrentBBT = new clsMTime.clsBBT(e.NewValue, 0, 0);
      ScrollEvActive = true;
      Forms.frmStart.RefreshBBT(P.F.CurrentBBT);
      ScrollEvActive = false;
    }

    //private void oldvScrollBar1_Scroll(object sender, ScrollEventArgs e) {
    //  //* min = -(maxbeats * PixPerBeat)
    //  //* max = 0
    //  Debug.WriteLine("Scrollbar NewValue = " + e.NewValue + " OldValue = " + e.OldValue);
    //  clsMTime.clsBBT newbbt;
    //  if (e.NewValue >= -vScrollBar1.LargeChange) {
    //    newbbt = new clsMTime.clsBBT(0);
    //  } else {
    //    if (e.NewValue == e.OldValue) return;
    //    int beat = -e.NewValue / PixsPerBeat;
    //    newbbt = new clsMTime.clsBBT(beat, true);
    //    bool up = ((e.NewValue - e.OldValue) < 0);  //false if approaching 0
    //    if (up) newbbt.RoundUpToBar(); else newbbt.RoundDownToBar();
    //    if (newbbt.Ticks > P.F.MaxTicks) {
    //      newbbt = new clsMTime.clsBBT(P.F.MaxTicks);
    //      newbbt.RoundDownToBar();
    //    }
    //  }
    //  //clsMTime.clsBBT startbbt = new clsMTime.clsBBT(P.F.StartBar, 0, 0);
    //  //if (up && (newbbt.Beats - startbbt.Beats < 0)) newbbt = startbbt;
    //  //if (!up && (newbbt.Beats - startbbt.Beats > 0)) newbbt = startbbt;
    //  P.F.StartBar = newbbt.Bar;
    //  P.F.CurrentBBT = newbbt;
    //  ScrollEvActive = true;
    //  P.frmStart.RefreshBBT(P.F.CurrentBBT);
    //  ScrollEvActive = false;
    //}

    internal void SetProps() {
      if (P.F.MaxBBT == null) {
        LogicError.Throw(eLogicError.X124);
        return;
      }
      vScrollBar1.Minimum = 0;
      //clsMTime.clsBBT bbtmax = new clsMTime.clsBBT(P.F.MaxTicks);
      //vScrollBar1.Maximum = bbtmax.Bar;
      vScrollBar1.Maximum = P.F.MaxBBT.Bar;
      vScrollBar1.LargeChange = 5;
      vScrollBar1.SmallChange = 1;
      //nudStartBar.Maximum = Math.Min(nudStartBar.Maximum, P.F.MaxBBT.Bar);
      nudStartBar.Maximum = Math.Max(1, P.F.MaxBBT.Bar - 2);
    }

    internal void SetScrollBarBBT(clsMTime.clsBBT bbt) {
      if (ScrollEvActive) return;
      if (P.F?.MTime == null || P.F.MaxBBT == null) return;
      clsMTime.clsBBT bbtrnd = (bbt.BeatsRemBar > 0) ? bbt.GetRoundDownToBar() : bbt;
      int pix = bbtrnd.Beats * PixsPerBeat;
      //clsMTime.clsBBT maxbbt = new clsMTime.clsBBT(P.F.MaxTicks); 
      //vScrollBar1.Value = Math.Min(maxbbt.Bar, Math.Max(0, bbtrnd.Bar));
      vScrollBar1.Value = Math.Min(P.F.MaxBBT.Bar, Math.Max(0, bbtrnd.Bar));
    }

    private void chkShowChords_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      picChordNames.Refresh();
    }

    internal void chkShowTracks_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      if (P.F == null) return;
      clsMTime.clsBBT bbt = P.F.CurrentBBT.Copy();

      if (chkShowTracks.Checked) LoadTracks(false); else P.F.CFNotes = new clsCFPitch(0);
      if (indShowTracks) NewKPos();
      //if (indShowTracks) {
      //  LoadTracks();
      //  NewKPos();
      //} else {
      //  P.F.CFNotes = new clsCFPitch(false);
      //}

      grpCapitalizeRoots.Enabled = !chkShowTracks.Checked;  //capitalize and showtrks = messy display
      P.F.CurrentBBT = bbt;
      ShowRanges();
      Refresh();
    }

    private void nudSyncopationDD_ValueChanged(object sender, EventArgs e) {
      //* read cfg 1/4 -> gets set to 1/2 here
      clsCF.Syncopation.DD = (int)nudSyncopationDD.Value;
      if (Bypass_Event) return;
      Bypass_Event = true;
      int val = frmStart.SetNudExp2(nudSyncopationDD);
      if (val >= 0) nudSyncopationDD.Value = val;
      Bypass_Event = false;
      if (P.F?.CF != null) P.F.CF.indSave = true;
    }

    //private void nudAlternateSyncopationDD_ValueChanged(object sender, EventArgs e) {
    //  //* read cfg 1/4 -> gets set to 1/2 here
    //  clsCF.AlternateSyncopation.DD = (int)nudAlternateSyncopationDD.Value;
    //  if (Bypass_Event) return;
    //  Bypass_Event = true;
    //  int val = frmStart.SetNudExp2(nudAlternateSyncopationDD);
    //  if (val >= 0) nudAlternateSyncopationDD.Value = val;
    //  Bypass_Event = false;
    //}

    private void nudSyncopationNN_ValueChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      clsCF.Syncopation.NN = (int)nudSyncopationNN.Value;
      if (P.F?.CF != null) P.F.CF.indSave = true;
    }

    //private void nudAlternateSyncopationNN_ValueChanged(object sender, EventArgs e) {
    //  clsCF.AlternateSyncopation.NN = (int)nudAlternateSyncopationNN.Value;
    //}

    private void chkShowChords_CheckedChanged_1(object sender, EventArgs e) {
      if (Bypass_Event) return;
      Refresh();
    }

    internal void LoadTracks(bool setocts) {
      if (P.F.frmTrackMap == null) return; 
      clsTrks.Array<bool> selectedtrks = P.F.frmTrackMap.GetSelectedTrks();
      if (selectedtrks == null) {
        //MessageBox.Show("Error: No Track Selected");
        P.F.CFNotes = new clsCFPitch(0);
        return;
      }

      //if (!P.F.indCalcKeys) {
      //  //* show frmCalcKeys for ALL tracks by default
      //  P.CloseFrm(P.F.frmCalcKeys);
      //  clsTrks.Array<bool> trkselectall = new clsTrks.Array<bool>(true);
      //  P.F.frmCalcKeys = new frmCalcKeys(trkselectall);
      //}

      clsFileStream filestream;
      try {
        filestream = new clsFileStream(P.F.Project.MidiPath, selectedtrks, true, false, true);
      }
      catch (MidiFileException) {
        return;
      }

      //Forms.frmNoteMap.ShowChords(true);
      P.F.CFNotes = new clsCFPitch(0);
      P.F.CFNotes.CreateEvsFromMidi(filestream);
      //cmbPlayStyle.SelectedItem = "None";
      int val = P.F.CFNotes.MinPitch / 12 - 5;
      if (setocts) SetInitialOcts(0, val, val);

      //filestream.SetPatchAndChan(filestream.TrkSelect);
    }

    //private void cmdOctIncrRange_Click(object sender, EventArgs e) {  //"Range"
    //  valShowLowC = Math.Min(valShowLowC + 12, 108);
    //  lblOctRange.Text = valShowLowC.ToString();
    //  Refresh();
    //}

    //private void cmdOctDecrRange_Click(object sender, EventArgs e) {  //"Range"
    //  valShowLowC = Math.Max(valShowLowC - 12, 0);
    //  lblOctRange.Text = valShowLowC.ToString();
    //  Refresh();
    //}

    //private void cmdOctIIncrDisplay_Click(object sender, EventArgs e) {
    //  SetOctDisplay(Math.Min(120, valShowLowC + 12));
    //  NewKPos();
    //  Refresh();
    //}

    //private void cmdOctDecrDisplay_Click(object sender, EventArgs e) {
    //  SetOctDisplay(Math.Max(12, valShowLowC - 12));
    //  NewKPos();
    //  Refresh();
    //}

    private void nudOctTransposeDisplay_ValueChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      //UpdateLblDisplayedRange();
      ShowRanges();
      NewKPos();
    }

    //private void cmdOctIncrPitch_Click(object sender, EventArgs e) { 
    //  Play.OctIncrPitch();  //-> OctTransposeKBPitch
    //  NewKPos();
    //  Refresh();
    //}

    //private void cmdOctDecrPitch_Click(object sender, EventArgs e) {
    //  Play.OctDecrPitch();  //-> OctTransposeKBPitch
    //  NewKPos();
    //  Refresh();
    //}

    private void nudOctTransposeKBPitch_ValueChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      NewKPos();
    }

    internal void NewKPos() {
      KPos = new clsKPos(this, picChords.ClientSize.Width, valOctaves);
      //SizeMainFont();
      Refresh();
    }

    //internal void SetOctDisplay(int val) {
    //  clsPlay.OctTransposeKB = val - valShowLowCDflt + OctOffset;
    //  if (val.Mod12() != 0) {
    //    LogicError.Throw(eLogicError.X097);
    //    return;
    //  }
    //  valShowLowC = val;
    //  //lblOctDisplay.Text = ((valShowLowC - valShowLowCDflt) / 12).ToString();
    //  //lblOctTransposeKB.Text = ((clsPlay.OctTransposeKB - (valShowLowC - valShowLowCDflt)) / 12).ToString();
    //  Bypass_Event = true;
    //  nudOctDisplay.Value = (valShowLowC - valShowLowCDflt) / 12;
    //  nudOctTransposeKB.Value = (clsPlay.OctTransposeKB - (valShowLowC - valShowLowCDflt)) / 12;
    //  Bypass_Event = false;
    //}

    //private void cmdKBDisplacmentDecr_Click(object sender, EventArgs e) {
    //  KBDisplacement = Math.Max(-11, KBDisplacement - 1);
    //  lblKBDisplacement.Text = KBDisplacement.ToString();
    //  NewKPos();
    //}

    //private void cmdKBDisplacementIncr_Click(object sender, EventArgs e) {
    //  KBDisplacement = Math.Min(11, KBDisplacement + 1);
    //  lblKBDisplacement.Text = KBDisplacement.ToString();
    //  NewKPos();
    //}

    private void nudKBDisplacement_ValueChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      //UpdateLblDisplayedRange();
      ShowRanges();
      NewKPos();
    }

    private void chkAlignKB_CheckedChanged(object sender, EventArgs e) {
      AlignKB();
      NewKPos();
    }

    private void cmdNoteFont_Click(object sender, EventArgs e) {
      fontDialog1.ShowColor = false;
      fontDialog1.Font = MainFont;
      //fontDialog1.MinSize = (int)Math.Round((double)MainFont.Size, MidpointRounding.ToEven);  
      //fontDialog1.MaxSize = fontDialog1.MinSize;  //current size (changes with Resize()) 
      if (fontDialog1.ShowDialog(this) != DialogResult.Cancel) MainFont = fontDialog1.Font;
      //SizeMainFont();
      Refresh();
    }

    private void cmdChordFont_Click(object sender, EventArgs e) {
      fontDialog1.ShowColor = true;
      fontDialog1.Font = ChordFont;
      fontDialog1.Color = ChordColor;
      if (fontDialog1.ShowDialog(this) != DialogResult.Cancel) {
        ChordFont = fontDialog1.Font;
        ChordColor = fontDialog1.Color;
      }
      Refresh();
    }

    private void nudOctaves_ValueChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      valOctavesDflt = (int)nudOctaves.Value;
      //UpdateLblDisplayedRange();
      ShowRanges();
      NewKPos();
    }

    //private void nudFontReduction_ValueChanged(object sender, EventArgs e) {
    //  if (Bypass_Event) return;
    //  valFontReduction = (int)nudFontReduction.Value;
    //  SizeMainFont();
    //  Refresh();
    //}

    //private void nudFontReduction_ValueChanged(object sender, EventArgs e) {  
    //  if (Bypass_Event) return;
    //  valFontReduction = (int)nudFontReduction.Value;
    //  SizeMainFont();
    //  Refresh();
    //}

    //private void UpdateLblDisplayedRange() {
    //  lblDisplayedRange.Text = NoteName.GetPitchStr(ShowLowPitch, false)
    //    + " - "
    //    + NoteName.GetPitchStr(ShowHighPitch, false);
    //}

    //private int OctOffset = 0;
    private void nudOctTransposeKB_ValueChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      NewKPos();
    }

    //private void cmdOctTransposeKBDecr_Click(object sender, EventArgs e) {
    //  clsPlay.OctTransposeKB -= 12;
    //  if (clsPlay.OctTransposeKB < -60) {
    //    clsPlay.OctTransposeKB = -60;
    //    return;
    //  }
    //  OctOffset -= 12;
    //  //lblOctDisplay.Text = ((valShowLowC - valShowLowCDflt) / 12).ToString();
    //  lblOctTransposeKB.Text = ((clsPlay.OctTransposeKB - (valShowLowC - valShowLowCDflt)) / 12).ToString();
    //  NewKPos();
    //  Refresh();
    //}

    //private void cmdOctTransposeKBIncr_Click(object sender, EventArgs e) {
    //  clsPlay.OctTransposeKB += 12;
    //  if (clsPlay.OctTransposeKB > 60) {
    //    clsPlay.OctTransposeKB = 60;
    //    return;
    //  }
    //  OctOffset += 12;
    //  //lblOctDisplay.Text = ((valShowLowC - valShowLowCDflt) / 12).ToString();
    //  lblOctTransposeKB.Text = ((clsPlay.OctTransposeKB - (valShowLowC - valShowLowCDflt)) / 12).ToString();
    //  NewKPos();
    //  Refresh();
    //}

    //private void pic_MouseClick(object sender, MouseEventArgs e) {
    //  panControls.Visible = !panControls.Visible;
    //  SetEndBBT();
    //}

    internal void trkTempo_Scroll(object sender, EventArgs e) {
      //* sender and e may be null 
      //* logarithmic * 100
      //* centre = 0 (2^0 = 1)
      //* max = 200 (2^2 = 4)
      //* min = -200 (2^-2 = 1/4)
      P.frmStart.TempoFactor = (float)Math.Pow(2, (float)trkTempo.Value / 100f);
      if (P.F?.CF != null) P.F.CF.indSave = true;
      //P.frmStart.indTempoChanged = true;
      //* update frmMSC trkTempo if scroll started here
      //* should not call frmMSC scroll event
      //if (sender != null && P.frmSC != null) P.frmSC.trkTempo.Value = trkTempo.Value;  
    }

    private void cmdTempoReset_Click(object sender, EventArgs e) {
      trkTempo.Value = 0;
      trkTempo_Scroll(sender, e);
    }

    private void nudOctTransposeMulti_ValueChanged(object sender, EventArgs e) {
      //* value: 1=increment ; -1=decrement
      if (Bypass_Event) return;
      int diff = (nudOctTransposeMulti.Value > 0) ? 12 : -12;
      if (diff > 0 && nudOctTransposeDisplay.Value == nudOctTransposeDisplay.Maximum) return;
      if (diff < 0 && nudOctTransposeDisplay.Value == nudOctTransposeDisplay.Minimum) return;
      nudOctTransposeDisplay.Value += diff;
      nudOctTransposeKB.Value += diff;
      Bypass_Event = true;
      nudOctTransposeMulti.Value = 0;
      Bypass_Event = false;
    }

    private void nudTransposeKB_ValueChanged(object sender, EventArgs e) {
      Refresh();
      if (P.F?.frmTonnetz != null) P.F.frmTonnetz.Refresh();
      if (P.F?.CF != null) P.F.CF.indSave = true;
    }

    private void nudTransposeKBPitch_ValueChanged(object sender, EventArgs e) {
      Refresh();
      if (P.F?.CF != null) P.F.CF.indSave = true;
    }

    //private void PopulateCmbPlayStyle() {
    //  cmbPlayStyle.Items.Add("None");
    //  cmbPlayStyle.Items.Add("Chords");  //AChords and BChords
    //  cmbPlayStyle.Items.Add("AChords");
    //  cmbPlayStyle.Items.Add("BChords");
    //  cmbPlayStyle.Items.Add("Bass");
    //  cmbPlayStyle.Items.Add("Melody");
    //  cmbPlayStyle.SelectedIndex = 0;  //"None"
    //}

    //internal void cmbPlayStyle_SelectedIndexChanged(object sender, EventArgs e) {
    //  if (P.F == null || P.F.FSTrackMap == null) return;
    //  P.F.FSTrackMap.CreateTrkTypes();
    //  if (cmbPlayStyle.SelectedIndex == -1) cmbPlayStyle.SelectedIndex = 0;  //"None"
    //  if (cmbPlayStyle.SelectedItem.ToString() != "None") {
    //    if (P.frmSC != null) P.frmSC.chkShowTracks.Checked = false;
    //  }

    //  switch (cmbPlayStyle.SelectedItem.ToString()) {
    //    case "None":
    //      //SetOctTranspose(0);
    //      SetInitialOcts(Cfg.OctTransposeKBPitch, 0, 0);
    //      //P.frmStart.chkAutoCapitalize.Checked = true;
    //      //Forms.frmSwitch.SetAction("KB Chord", "Pedal");
    //      break;
    //    case "Chords":
    //      P.F.FSTrackMap.SetMuteOn(clsFileStream.eTrkType.AChords, clsFileStream.eTrkType.BChords);
    //      //SetOctTranspose(-1);
    //      SetInitialOcts(0, -1, -1);
    //      chkAutoCapitalize.Checked = true;
    //      if (!P.F.FSTrackMap.SetPatchAndChan(clsFileStream.eTrkType.BChords)) {
    //        P.F.FSTrackMap.SetPatchAndChan(clsFileStream.eTrkType.AChords);
    //      }
    //      Forms.frmSwitch.SetAction("KB Chord", "Pedal");
    //      break;
    //    case "AChords":
    //      P.F.FSTrackMap.SetMuteOn(clsFileStream.eTrkType.AChords);
    //      //SetOctTranspose(-1);
    //      SetInitialOcts(0, -1, -1);
    //      chkAutoCapitalize.Checked = true;
    //      P.F.FSTrackMap.SetPatchAndChan(clsFileStream.eTrkType.AChords);
    //      Forms.frmSwitch.SetAction("KB Chord", "Pedal");
    //      break;
    //    case "BChords":
    //      P.F.FSTrackMap.SetMuteOn(clsFileStream.eTrkType.BChords);
    //      //SetOctTranspose(-1);
    //      SetInitialOcts(0, -1, -1);
    //      chkAutoCapitalize.Checked = true;
    //      P.F.FSTrackMap.SetPatchAndChan(clsFileStream.eTrkType.BChords);
    //      Forms.frmSwitch.SetAction("KB Chord", "Pedal");
    //      break;
    //    case "Bass":
    //      P.F.FSTrackMap.SetMuteOn(clsFileStream.eTrkType.Bass);
    //      //SetOctTranspose(-1);
    //      SetInitialOcts(0, -2, -2);
    //      chkAutoCapitalize.Checked = false;
    //      chkCapitalizeRoots.Checked = true;
    //      P.F.FSTrackMap.SetPatchAndChan(clsFileStream.eTrkType.Bass);
    //      Forms.frmSwitch.SetAction("KB Chord", "None");
    //      break;
    //    case "Melody":
    //      P.F.FSTrackMap.SetMuteOn(clsFileStream.eTrkType.Melody);
    //      //SetOctTranspose(1);
    //      SetInitialOcts(0, 1, 1);
    //      chkAutoCapitalize.Checked = true;
    //      P.F.FSTrackMap.SetPatchAndChan(clsFileStream.eTrkType.Melody);
    //      Forms.frmSwitch.SetAction("KB Chord", "None");
    //      break;
    //    default:
    //      LogicError.Throw(eLogicError.X091);
    //      break;
    //  }
    //}

    private void nudKBChanOut_ValueChanged(object sender, EventArgs e) {
      if (MidiPlay.OutMKB == null) return;
      SetAutoSustain();
      if (Bypass_Event) return;
      MidiPlay.OutMKB.AllNotesOff();  //use KBOutChan (before update)
      //Debug.WriteLine("KBOutChan = " + KBOutChan + " nud-1 = " + (nudKBOutChan.Value-1));
      MidiPlay.KBOutChan = (int)nudKBChanOut.Value - 1;
      cmbKBChanPatch_SelectedIndexChanged(cmbKBChanPatch, EventArgs.Empty);
      if (P.F != null && P.frmSC != null) {
        P.frmSC.trkKBChanVol_Scroll(null, null);
        P.frmSC.trkKBChanPan_Scroll(null, null);
      }
    }

    //private void nudRiffChanOut_ValueChanged(object sender, EventArgs e) {
    //  SetAutoSustain();
    //  if (Bypass_Event) return;
    //  MidiPlay.OutMKB.AllNotesOff();  //use KBOutChan (before update)
    //  //Debug.WriteLine("KBOutChan = " + KBOutChan + " nud-1 = " + (nudKBOutChan.Value-1));
    //  MidiPlay.KBOutChanAutoRiff = (int)nudRiffChanOut.Value - 1;
    //  cmbRiffChanPatch_SelectedIndexChanged(cmbRiffChanPatch, EventArgs.Empty);
    //  if (P.F != null && P.frmSC != null) {
    //    P.frmSC.trkRiffChanVol_Scroll(null, null);
    //    P.frmSC.trkRiffChanPan_Scroll(null, null);
    //  }
    //}

    internal void cmbKBChanPatch_SelectedIndexChanged(object sender, EventArgs e) {
      if (cmbKBChanPatch.SelectedIndex < 1) return;  //no selection or "None"
      int patch = cmbKBChanPatch.SelectedIndex - 1;  //2nd. selection = first patch = index 1 
      int chan = MidiPlay.KBOutChan;
      if (MidiPlay.OutMKB != null) MidiPlay.OutMKB.SendShortMsg(0xc0 | chan, patch, 0);
      SetAutoSustain();
    }

    //internal void cmbRiffChanPatch_SelectedIndexChanged(object sender, EventArgs e) {
    //  if (cmbRiffChanPatch.SelectedIndex < 1) return;  //no selection or "None"
    //  int patch = cmbRiffChanPatch.SelectedIndex - 1;  //2nd. selection = first patch = index 1 
    //  int chan = MidiPlay.KBOutChanAutoRiff;
    //  MidiPlay.OutMKB.SendShortMsg(0xc0 | chan, patch, 0);
    //  //SetAutoSustain();
    //}

    private void PopulateCmbPatch(ComboBox cmb) {
      cmb.Items.Clear();
      cmb.Items.Add("None");
      for (int i = 0; i < GeneralMidiList.Desc.Length; i++) {
        string desc = GeneralMidiList.Desc[i];
        cmb.Items.Add(desc);
      }
      cmb.SelectedIndex = Cfg.KBPatchSelectedIndex;  //0="None"
    }

    private void optModeChords_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      if (!optModeChords.Checked) return;
      SwitchMode(ePlayMode.Chords);
      chkShowTracks.Enabled = false;
    }

    private void optModeKB_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      if (!optModeKB.Checked) return;
      SwitchMode(ePlayMode.KB);
      chkShowTracks.Enabled = true;
    }

    //private void optModeStep_CheckedChanged(object sender, EventArgs e) {
    //  if (Bypass_Event) return;
    //  if (!optModeStep.Checked) return;
    //  SwitchMode(ePlayMode.Step);
    //}

    internal void SwitchMode(ePlayMode mode) {  //called from optModeKB/optModeChords event
      //if (PlayMode != mode) {
        Play = clsPlay.NewPlay(mode);
        SetEndBBTRefresh();
      //}
    }

    internal void SwitchPlayMode() {  //called from switchkey delegate
    Bypass_Event = true;
      if (PlayMode == ePlayMode.Chords) {
        Play = new clsPlayKeyboard(Play.LastActiveKeyOn, Play.LastActivePitchOn, Play);
        BeginInvoke(new delSetOptMode(SetOptMode), optModeKB);
      } else if (PlayMode == ePlayMode.KB) {
        Play = new clsPlayChords(Play.LastActiveKeyOn, Play.LastActivePitchOn, Play);
        BeginInvoke(new delSetOptMode(SetOptMode), optModeChords);
      }
      Bypass_Event = false;
    }

    private void SetOptMode(RadioButton opt) {
      opt.Checked = true;
    }

    private void chkCapitalizeRoots_CheckedChanged(object sender, EventArgs e) {
      if (AutoCapitalizeClicked) return;
      CapitalizeRootsStatic = chkCapitalizeRoots.Checked;
      if (P.F != null && P.F.CF != null && P.frmSC != null) {
        P.F.CF.SyncEvsToKeys();
        P.frmSC.SetEndBBTRefresh();
      }
      Refresh();
    }

    private bool AutoCapitalizeClicked = false;
    private void chkAutoCapitalize_CheckedChanged(object sender, EventArgs e) {
      AutoCapitalizeClicked = true;
      AutoCapitalizeStatic = chkAutoCapitalize.Checked;
      if (chkAutoCapitalize.Checked) {
        chkCapitalizeRoots.CheckState = CheckState.Indeterminate;
        chkCapitalizeRoots.Enabled = false;
        if (P.F != null && P.frmSC != null) {
          CapitalizeRootsStatic = (P.frmSC.PlayMode == Forms.frmSC.ePlayMode.Chords);
        }
      } else {
        chkCapitalizeRoots.Enabled = true;
        if (chkCapitalizeRoots.Checked) {
          chkCapitalizeRoots.CheckState = CheckState.Checked;
        } else {
          chkCapitalizeRoots.CheckState = CheckState.Unchecked;
        }
        CapitalizeRootsStatic = chkCapitalizeRoots.Checked;
      }
      if (P.F != null && P.frmSC != null) {
        P.F.CF.SyncEvsToKeys();
        P.frmSC.SetEndBBTRefresh();
      }
      AutoCapitalizeClicked = false;
    }

    //private void mnuModulate_Click(object sender, EventArgs e) {
    //  P.CloseFrm(P.frmMod);
    //  P.frmMod = new Forms.frmMod();
    //  P.frmMod.ShowDialog(this);
    //}

    internal void cmdMultiMap_Click(object sender, EventArgs e) {
      ShowMultiMap();
      //P.frmSC.cmbPlayStyle_SelectedIndexChanged(null, null);
    }

    //private void cmdLoadMidiFile_Click(object sender, EventArgs e) {
    //  mnuLoadCSV_Click(null, null);
    //}

    internal void cmdLoadProject_Click(object sender, EventArgs e) {
      LoadProjectClick();
    }

    private void cmdPanic_Click(object sender, EventArgs e) {
      P.F.Panic();
    }

    internal void mnuNew_Click(object sender, EventArgs e) {
      //if (!P.F.CloseCancellableForms()) return;
      if (P.F != null && !P.F.SaveProject(null)) return;  //check and save

      //* show new song dialog
      dlgNewProject frm = new dlgNewProject();
      DialogResult result = frm.ShowDialog(this);
      if (result == DialogResult.Cancel) return;
      //P.frmStart.nudMaxBarsNoMidiFile.Value = clsF.DefaultSongLength;
      using (new clsLoadingProject()) {}  //switch off indSave.Loading if no loadproject before this
    }

    private void mnuLoadProject_Click(object sender, EventArgs e) {
      LoadProjectClick();
    }

    internal void LoadProjectClick() {
      try {
        //MessageBox.CacheOn();
        //if (!P.F.CloseCancellableForms()) return;
        if (P.F != null && !P.F.SaveProject(null)) return;  //check and save
        MidiPlay.Sync.Stop();
        //P.F = new clsF();
        if (!P.frmStart.LoadProject("", true) /*&& !CheckTextFile(true)*/) {
          MessageBox.Show("Load Project failed - no valid file found");
          return;  //-> reload frmShowChords
        }
      }
      catch (Exception exc) {
        //* programming error or corrupt/inconsistent files, ...
        MessageBox.Show("Load Project failed: " + exc.Message);
        //frmStart.CheckCloseForms();
        //P.F = new clsF();
      }
      //finally {
        //MessageBox.CacheOff();
      //}
    }

    //internal void mnuLoadCSV_Click(object sender, EventArgs e) {
    //  P.F.StartBar = 0;  //else Stop will set CurrentBBT to old StartBar!!!
    //  MidiPlay.Sync.Stop();
    //  P.frmStart.LoadMidiFile("");
    //  P.F.CurrentBBT = new clsMTime.clsBBT(0);
    //  P.frmStart.RefreshBBT(P.F.CurrentBBT);
    //  Activate();
    //}

    //private void mnuSaveChordFile_Click(object sender, EventArgs e) {
    //  Cursor.Current = Cursors.WaitCursor;
    //  string ret = Forms.frmChordMap.SaveTxtFile();
    //  if (ret != "") MessageBox.Show("ChordFile nor saved: " + ret);
    //  Cursor.Current = Cursors.Default;
    //}

    //internal void mnuSaveAutoSyncFile_Click(object sender, EventArgs e) {
    //  if (P.F == null || P.F.AutoSync == null) return;
    //  if (clsAutoSync.ActiveStatic) return;
    //  P.F.AutoSync.SaveFile();
    //}

    //internal void mnuResetSync_Click(object sender, EventArgs e) {
    //  if (P.F == null || P.F.AutoSync == null) return;
    //  if (clsAutoSync.ActiveStatic) return;
    //  if (MessageBox.Show("Confirm Sync Reset (Clear Active and File lists)", "Sync Reset", MessageBoxButtons.YesNo)
    //    == DialogResult.No) return; 
    //  P.F.AutoSync.Reset();
    //}

    //private void mnuSaveChordFileAs_Click(object sender, EventArgs e) {
    //  Cursor.Current = Cursors.WaitCursor;
    //  string ret = Forms.frmChordMap.SaveTxtAndMidiFile("");
    //  if (ret != "") MessageBox.Show("ChordFile not saved: " + ret); 
    //  Cursor.Current = Cursors.Default;
    //}

    //private void mnuSaveMidiFile_Click(object sender, EventArgs e) {
    //  clsSaveMidiFile.SaveMidiFile(P.F.MidiFilePath);
    //}

    //private void mnuSaveMidiFileAs_Click(object sender, EventArgs e) {
    //  clsSaveMidiFile.SaveMidiFileAs();
    //}

    internal static void ShowMultiMap() {
      if (!P.F.MidiFileLoaded) {
        MessageBox.Show("MidiFile not loaded");
        return;
      }
      if (P.F.frmTrackMap != null && !P.F.frmTrackMap.Visible) {
        Utils.FormAct(P.F.frmTrackMap);
        return;
      }
      using (new clsWaitCursor()) {
        if (P.F.frmTrackMap != null && P.F.frmTrackMap.IsHandleCreated) P.F.frmTrackMap.Close();
        if (P.F.LoadCSV == null) {
          try {
            P.F.LoadCSV = new clsLoadCSV();
          }
          catch (MidiFileException) {
            clsF.NewEmpty();
            return;
          }
        }
        P.F.frmTrackMap = new Forms.frmTrackMap();
        Utils.FormAct(P.F.frmTrackMap);
        //cmbPlayStyle_SelectedIndexChanged(null, null);
        //P.F.FileStreamMM.CreateTrkTypes();
      }
    }

    private void mnuCfgAudio_Click(object sender, EventArgs e) {
      if (P.frmConfigBass == null) P.frmConfigBass = new frmConfigBass();
      Utils.FormAct(P.frmConfigBass);
      //P.frmConfigBass.Show();
    }

    private void mnuCfgMidi_Click(object sender, EventArgs e) {
      //P.CloseFrm(P.frmMidiChannels);
      if (P.frmMidiDevs == null || !P.frmMidiDevs.IsHandleCreated) {
        P.frmMidiDevs = new frmMidiDevs();
      }
      Utils.FormAct(P.frmMidiDevs);
    }

    private void mnuCfgBezierVelocity_Click(object sender, EventArgs e) {
      //if (Cfg.BezierVel < 0) return;  //"None"
      P.CloseFrm(P.frmCfgBezier);
      P.frmCfgBezier = new Forms.frmCfgBezier(Cfg.BezierVel, true);
      //P.frmCfgBezier.Show();
      Utils.FormAct(P.frmCfgBezier);
    }

    private void mnuCfgBezierAfterTouch_Click(object sender, EventArgs e) {
      //if (Cfg.BezierATouch < 0) return;  //"None"
      P.CloseFrm(P.frmCfgBezier);
      P.frmCfgBezier = new Forms.frmCfgBezier(Cfg.BezierATouch, false);
      //P.frmCfgBezier.Show();
      Utils.FormAct(P.frmCfgBezier);
    }

    internal void mnuCfgSwitch_Click(object sender, EventArgs e) {
      if (P.frmSwitch == null) P.frmSwitch = new frmSwitch();
      //if (!P.frmSwitch.Visible) P.frmSwitch.Show();
      //P.frmSwitch.Activate();
      Utils.FormAct(P.frmSwitch);
    }

    //private void mnuPathMP3_Click(object sender, EventArgs e) {
    //  string dir = GetDirectory("Select Audio File Folder", Cfg.MP3DirPath);
    //  if (dir == "") return;
    //  Cfg.MP3DirPath = dir;
    //}

    private void mnuPathProject_Click(object sender, EventArgs e) {
      string dir = GetUserDirectory("Select Project Folder", Cfg.ProjectDir);
      if (dir == "") return;
      Cfg.ProjectDir = dir;
    }

    private void mnuPathSoundFonts_Click(object sender, EventArgs e) {
      string dir = GetUserDirectory("Select SoundFonts Folder", Cfg.SoundFontsPath);
      if (dir == "") return;
      Cfg.SoundFontsPath = dir;
      if (P.frmMidiDevs != null) P.frmMidiDevs.CheckConnectStates();
    }

    internal string GetUserDirectory(string title, string dir) {
      if (dir == "") dir = Cfg.UserMusicPath;
      try {
        if (!Directory.Exists(dir)) {
          MessageBox.Show(dir + " Directory not found - using " + Cfg.UserMusicPath + " instead");
          dir = Cfg.UserMusicPath;
        }
      }
      catch (Exception exc) {
        MessageBox.Show("GetUserDirectory Exception: " + exc.Message);
      }

      //* set up dialog
      fbd.RootFolder = Environment.SpecialFolder.MyComputer;
      fbd.SelectedPath = dir;
      fbd.ShowNewFolderButton = true;
      fbd.Description = title;

      //* run dialog
      if (fbd.ShowDialog(this) != DialogResult.OK) return "";

      //* process result
      return fbd.SelectedPath;
    }

    private void mnuShowDebugInfo_Click(object sender, EventArgs e) {
      Utils.FormAct(new Forms.frmShowList(Forms.frmShowList.eList.Debug));
    }

    private void mnuMonitorTotals_Click(object sender, EventArgs e) {
      if (clsPlay.PlayExists()) {
        MessageBox.Show(clsPlay.ShowMonitorTotals());
      }
    }

    private void mnuMonitorTimeline_Click(object sender, EventArgs e) {
      if (clsPlay.PlayExists()) {
        string[] arr = clsPlay.ShowMonitorTimeline().Split(new char[] { '\n' });
        Utils.FormAct(new Forms.frmShowList(arr));
        //MessageBox.Show(P.F.frmShowChords.Play.ShowMonitorTimeline(), "MidiOn/Off Monitor Timeline");
      }
    }

    private void mnuMonitor_Click(object sender, EventArgs e) {
      MenuMonitor = mnuMonitor.Checked;
    }

    //private void mnuFile_Click(object sender, EventArgs e) {
    //  mnuSaveChordFile.Enabled = (P.F.CF != null);
    //  mnuSaveChordFileAs.Enabled = (P.F.CF != null);
    //  //mnuSaveMidiFile.Enabled = (P.F.MidiFilePath.Length > 0);
    //  //mnuSaveMidiFileAs.Enabled = (P.F.MidiFilePath.Length > 0);
    //}

    private void mnuCfgMisc_Click(object sender, EventArgs e) {
      //P.frmStart.Top = Top + 30;  //cascade
      //P.frmStart.Left = Left + 30;  //cascade
      Utils.FormAct(P.frmStart);  //already loaded
      //P.frmStart.Activate();
      //Utils.FormAct(P.frmStart);
    }

    private void mnuExit_Click(object sender, EventArgs e) {
      Close();
      //Application.Exit();
    }

    //private void chkSwitchPedal_CheckedChanged(object sender, EventArgs e) {
    //  if (Bypass_Event) return;
    //  if (MidiPlay.MidiInKB == null) return;
    //  lock (MidiPlay.MidiInKB.SwitchKeyLock) {
    //    foreach (string action in Forms.frmSwitch.KeyToActionsPedal) {  
    //      if (action != "") Forms.frmSwitch.Delegs[action](chkSwitchPedal.Checked);
    //    }
    //  }
    //}

    //private void chkSwitchA_CheckedChanged(object sender, EventArgs e) {
    //  SwitchKeyEv(chkSwitchA, 9);
    //}

    //private void chkSwitchB_CheckedChanged(object sender, EventArgs e) {
    //  SwitchKeyEv(chkSwitchB, 11);
    //}

    //private void chkSwitchC_CheckedChanged(object sender, EventArgs e) {
    //  SwitchKeyEv(chkSwitchC, 0);
    //}

    private void chkSwitchSustain_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      if (MidiPlay.MidiInKB == null) return;
      CheckBox chk = (CheckBox)sender;
      lock (MidiPlay.MidiInKB.SwitchKeyLock) {
        Forms.frmSwitch.Delegs["Sustain"](chk.Checked);
      }
    }

    private void chkSwitchKBChord_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      if (MidiPlay.MidiInKB == null) return;
      CheckBox chk = (CheckBox)sender;
      lock (MidiPlay.MidiInKB.SwitchKeyLock) {
        Forms.frmSwitch.Delegs["KB Chord"](chk.Checked);
      }
    }

    internal void ResetSwitches() {
      //chkSwitchA.Checked = false;
      //chkSwitchB.Checked = false;
      //chkSwitchC.Checked = false;
      //chkSwitchPedal.Checked = false;
      chkSwitchSustain.Checked = false;
      chkSwitchKBChord.Checked = false;
    }

    //private void SwitchKeyEv(CheckBox chk, int pc) {  //pc = pitchclass of switchkey
    //  if (Bypass_Event) return;
    //  if (MidiPlay.MidiInKB == null) return;
    //  lock (MidiPlay.MidiInKB.SwitchKeyLock) {
    //    Forms.frmSwitch.frmSCSwitch = true;
    //    foreach (string action in Forms.frmSwitch.KeyToActions[pc]) {  
    //      if (action != "") Forms.frmSwitch.Delegs[action](chk.Checked);
    //    }
    //    Forms.frmSwitch.frmSCSwitch = false;
    //  }
    //}

    //internal void SetChkSwitch(int key, List<string> actions) {
    //  string txt;
    //  if (actions.Count == 0) txt = "Switch";
    //  else if (actions.Count > 1) txt = "*";  //multiple actions on one key
    //  else txt = actions[0];
    //  switch (key) {  //pitchclass/pedal
    //    case 9:
    //      chkSwitchA.Text = txt + "\r\nA";
    //      break;
    //    case 11:
    //      chkSwitchB.Text = txt + "\r\nB";
    //      break;
    //    case 0:
    //      chkSwitchC.Text = txt + "\r\nC";
    //      break;
    //    case 12:
    //      if (txt == "None") chkSwitchPedal.Text = "Pedal";
    //      else chkSwitchPedal.Text = txt + "\r\nPedal";
    //      break;
    //    default:
    //      break;
    //  }
    //}

    //internal void SetChkSwitch(int key, List<string> actions) {
    //  switch (key) {  //pitchclass/pedal
    //    case 9:
    //      SetChkSwitchX("A", chkSwitchA, actions);
    //      break;
    //    case 11:
    //      SetChkSwitchX("B", chkSwitchB, actions);
    //      break;
    //    case 0:
    //      SetChkSwitchX("C", chkSwitchC, actions);
    //      break;
    //    case 12:  //pedal
    //      SetChkSwitchX("Pedal", chkSwitchPedal, actions);
    //      break;
    //    default:
    //      break;
    //  }
    //}

    //private void SetChkSwitchX(string keyname, CheckBox chk, List<string> actions) {
    //  string txt;
    //  if (actions.Count == 0) {
    //    txt = "Switch " + keyname;
    //    chk.Enabled = false;
    //  } else {
    //    chk.Enabled = true;
    //    txt = keyname + ": ";
    //    foreach (string t in actions) txt += t + "/";
    //    txt = txt.Substring(0, txt.Length - 1);  //remove last '/'
    //  }
    //  //} else if (actions.Count > 1) {
    //  //  chk.Enabled = true;
    //  //  txt = "*** " + keyname;  //multiple actions on one key
    //  //} else {
    //  //  chk.Enabled = true;
    //  //  string[] tt = actions[0].Split(new char[] { ' ' });
    //  //  if (tt.Length > 1) txt = tt[0] + "\r\n" + tt[1];
    //  //  else txt = tt[0];
    //  //  txt = actions[0];
    //  //}
    //  chk.Text = txt;
    //}

    private void mnuSaveSettings_Click(object sender, EventArgs e) {
      Forms.frmStart.SaveAllIni();
    }

    private void cmdAlign_Click(object sender, EventArgs e) {
      P.frmSCAlign = new Forms.dlgSCAlign();
      P.frmSCAlign.ShowDialog(this);
    }

    internal void SetRanges(int showloc) {
      SetRanges(valPlayLoC, showloc, valPlayHiC);
    }

    internal void SetRanges(int playloc, int showloc, int playhic) {
      if (playloc >= playhic) {
        if (showloc < playhic) {
          playloc = showloc;
        } else {
          playloc = playhic - 12;
        }
      } else {  //playloc < playhic
        if (showloc < playloc) showloc = playloc;
      }

      valShowLowCDflt = showloc;
      valPlayLoC = playloc;
      valPlayHiC = playhic;

      //clsPlay.NewPlay(PlayMode);  //???
      NewKPos();
      ShowRanges();
      Refresh();
    }

    internal void SetRecordColour(Button cmd, bool indred) {
      cmd.BackColor = (indred) ? Color.Red : SystemColors.Control;
      cmd.UseVisualStyleBackColor = !indred;
    }

    //private void mnuMidiFilesSearch_Click(object sender, EventArgs e) {
    //  try {
    //    Process.Start("http://www.google.com/search?q=<midi file: beatles michelle>");
    //  }
    //  catch (Exception) {
    //  }
    //}

    private void frmSC_FormClosed(object sender, FormClosedEventArgs e) {
      MidiPlay.CloseAllMidi(false);
      MidiPlay.CloseAllBass();
      if (Mtx != null) Mtx.ReleaseMutex();
      if (P.F.AudioSync != null) P.F.AudioSync.MP3Player.Free(); 
      //if (NAudio != null) NAudio.Dispose();
    }

    //private void chkKBOrRiffChan_CheckedChanged(object sender, EventArgs e) {
    //  if (Bypass_Event) return;
    //  CheckBox chk = (CheckBox)sender;
    //  Bypass_Event = true;
    //  chkKBChan.Checked = chk.Checked;
    //  chkRiffChan.Checked = chk.Checked;
    //  Bypass_Event = false;
    //  grpKBChan.Visible = !chk.Checked;
    //  grpRiffChan.Visible = chk.Checked;
    //}

    //private void cmdBarUp_Click(object sender, EventArgs e) {
    //  int maxbar = new clsMTime.clsBBT(P.F.MaxTicks).Bar;
    //  int newbar = Math.Min(maxbar, P.F.CurrentBBT.Bar + 1);
    //  nudStartBar.Value = newbar + 1;
    //  P.F.CurrentBBT = new clsMTime.clsBBT(newbar, 0, 0);
    //}

    //private void cmdBarDown_Click(object sender, EventArgs e) {
    //  int newbar = Math.Max(0, P.F.CurrentBBT.Bar - 1);
    //  nudStartBar.Value = newbar + 1;
    //  P.F.CurrentBBT = new clsMTime.clsBBT(newbar, 0, 0);
    //}

    private void cmdShowSumm_Click(object sender, EventArgs e) {
      P.CloseFrm(P.F.frmSummary);
      P.F.frmSummary = new frmSummary();
      //P.F.frmSummary.Show();
      Utils.FormAct(P.F.frmSummary);
    }

    internal void cmdChordMap_Click(object sender, EventArgs e) {
      if (P.F.frmTrackMap != null) {
        P.F.frmTrackMap.cmdChordMap_Click(null, null);
      } else {
        if (P.F.MidiFileLoaded) {
          Forms.frmTrackMap.ConvertCSV(new clsTrks.Array<bool>(true));
          //Forms.frmTrackMap.CheckfrmCalcKeys();
        } else {
          P.CloseFrm(P.F.frmChordMap);
          //if (!CheckTextFile(false)) return;
          if (!CheckTextFile()) return;
          P.F.frmChordMap = new Forms.frmChordMap(null);
          Utils.FormAct(P.F.frmChordMap);
        }
      }
    }

    internal bool CheckTextFile() {
      if (!P.F.MidiFileLoaded && 
      (P.F.CF == null || P.F.CF.Evs == null)) {  //no midifile or chord file
        Forms.dlgNud frm = new Forms.dlgNud();
        frm.Text = "New Chord File";
        //frm.cmdCancel.Hide();
        frm.lblPrompt.Text = "Enter Maximum Number of WholeNotes";
        frm.lblMsg.Text = "Time signature will be set to 4/4 by default.";
        frm.lblMsg.Text += "\r\nKey will be set to C Major by default.";
        frm.lblMsg.Text += "\r\nUse ChordMap to change these if required.";
        frm.nud1.Minimum = 10;
        frm.nud1.Maximum = 999;
        frm.nud1.Increment = 10;
        //frm.nud1.Value = P.frmStart.nudMaxBarsNoMidiFile.Value;
        frm.nud1.Value = clsCF.DefaultSongLength;
        if (frm.ShowDialog(P.frmSC) != DialogResult.OK) return false;
        int wholenotes = (int)frm.nud1.Value;
        //if (newf) clsF.NewEmpty(bars); else P.F.SetEmpty(bars);
        P.F.SetEmpty(wholenotes);
        //clsF.InitNullMidiFile(bars);
        //P.F.frmChordMap = new frmChordMap(null);
        //P.F.frmChordMap.Show();
      }
      return true;
    }

    //private void cmdLyrics_Click(object sender, EventArgs e) {
    //  if (P.F.frmMultiMap != null) {
    //    P.F.frmMultiMap.cmdLyrics_Click(null, null);
    //  } else {
    //    P.CloseFrm(P.F.frmNoteMap);
    //    if (!CheckTextFile()) return;
    //    P.F.frmNoteMap = new Forms.frmNoteMap(null, true);
    //    P.F.frmNoteMap.Show();
    //  }
    //}

    //private void mnuCreateSyncFile_Click(object sender, EventArgs e) {
    //}

    //private void mnuCreateSyncFileSections_Click(object sender, EventArgs e) {
    //  int mintempo = 30;
    //  int maxtempo = 480;
    //  Forms.frmDialogNud frm = new frmDialogNud();
    //  frm.Text = "Create Default Sync File";
    //  frm.lbl1.Text = "Enter Tempo (Beats per Minute)";
    //  frm.nud1.Minimum = mintempo;
    //  frm.nud1.Maximum = maxtempo;
    //  frm.nud1.Increment = 1;
    //  clsFileStream fs = null;
    //  if (P.F.FileStreamMM != null) fs = P.F.FileStreamMM;
    //  else if (P.F.FileStreamConv != null) fs = P.F.FileStreamConv;
    //  if (fs != null) {
    //    int dd = P.F.MTime.GetTSig(0).DD;
    //    int tempo = clsAutoSync.DivRound(dd * 60000000, 4 * fs.TempoMap[0]);
    //    frm.nud1.Value = Math.Min(maxtempo, Math.Max(mintempo, tempo));
    //  } else {
    //    frm.nud1.Value = 120;
    //  }
    //  if (frm.ShowDialog(this) == DialogResult.OK) {
    //    int tempo = (int)frm.nud1.Value;
    //    string chtfilepath = clsAutoSync.GetCHTSFilePath();
    //    int delta = clsAutoSync.DivRound(60000, tempo);   //default 4/4 tsig
    //    P.F.AutoSync = new clsAutoSyncSection(chtfilepath, delta);
    //    PlayableForms.CmdState_Stopped();
    //  }
    //}

    //private void mnuCreateSyncFileBars_Click(object sender, EventArgs e) {
    //  string chtfilepath = clsAutoSync.GetCHTBFilePath();
    //  P.F.AutoSync = new clsAutoSyncBar(chtfilepath);
    //  PlayableForms.CmdState_Stopped();
    //}

    //private void mnuCreateSyncFileBeats_Click(object sender, EventArgs e) {
    //  //string chtfilepath = clsAutoSync.GetCHTCFilePath();
    //  //string mp3filepath = clsAutoSync.GetMP3FilePath();
    //  P.F.AutoSync = new clsAutoSyncBeat();
    //  PlayableForms.CmdState_Stopped();
    //}

    private void cmdPlayAudio_Click(object sender, EventArgs e) {
      if (P.F.AudioSync != null) P.F.AudioSync.StartCmdSyncPlay();
    }

    //private void mnuClearSync_Click(object sender, EventArgs e) {
    //  if (P.F == null || P.F.AutoSync == null) return;
    //  if (clsAutoSync.Active) return;
    //  P.F.AutoSync.Clear();
    //}

    //internal void SetVScroll(int val) {
    //  val = Math.Max(Math.Min(val, vScrollBar1.Maximum), vScrollBar1.Minimum);
    //  vScrollBar1.Value = val;
    //}

    //private void mnuHowToGenKeys_Click(object sender, EventArgs e) {
    //  Forms.HowTo.frmHowToGenKeys frm = new HowTo.frmHowToGenKeys();
    //  frm.Show();
    //}

    //private void mnuHowToChords_Click(object sender, EventArgs e) {
    //  Forms.HowTo.frmHowToChords frm = new HowTo.frmHowToChords();
    //  frm.Show();
    //}

    //private void mnuHowToPlayMelody_Click(object sender, EventArgs e) {
    //  Forms.HowTo.frmHowToPlayMelody frm = new HowTo.frmHowToPlayMelody();
    //  frm.Show();
    //}

    //private void mnuHowToPlayChords_Click(object sender, EventArgs e) {
    //  Forms.HowTo.frmHowToPlayChords frm = new HowTo.frmHowToPlayChords();
    //  frm.Show();
    //}

    //private void mnuHowToPlayMidiFile_Click(object sender, EventArgs e) {
    //  Forms.HowTo.frmHowToPlayMidiFile frm = new HowTo.frmHowToPlayMidiFile();
    //  frm.Show();
    //}

    //private void mnuHowToSyncAudioSource_Click(object sender, EventArgs e) {
    //  Forms.HowTo.frmHowToSyncAudioSource frm = new HowTo.frmHowToSyncAudioSource();
    //  frm.Show();
    //}

    //private void mnuHowToPlayAudio_Click(object sender, EventArgs e) {
    //  Forms.HowTo.frmHowToPlayAudio frm = new HowTo.frmHowToPlayAudio();
    //  frm.Show();
    //}

    //private void mnuHowToUpdateKeys_Click(object sender, EventArgs e) {
    //  Forms.HowTo.frmHowToUpdateKeys frm = new HowTo.frmHowToUpdateKeys();
    //  frm.Show();
    //}

    //private void mnuHowToUpdateTSigs_Click(object sender, EventArgs e) {
    //  Forms.HowTo.frmHowToUpdateTSigs frm = new HowTo.frmHowToUpdateTSigs();
    //  frm.Show();
    //}

    //private void mnuHowToBezier_Click(object sender, EventArgs e) {
    //  Forms.HowTo.frmHowToBezier frm = new HowTo.frmHowToBezier();
    //  frm.Show();
    //}

    //private void mnuHowToAutoSyncWindow_Click(object sender, EventArgs e) {
    //  Forms.HowTo.frmHowToAutoSyncWindow frm = new HowTo.frmHowToAutoSyncWindow();
    //  frm.Show();
    //}

    //private void mnuHowToKeySwitches_Click(object sender, EventArgs e) {
    //  Forms.HowTo.frmHowToKeySwitches frm = new HowTo.frmHowToKeySwitches();
    //  frm.Show();
    //}

    //private void mnuHowToInsertChords_Click(object sender, EventArgs e) {
    //  Forms.HowTo.frmHowToInsertChords frm = new HowTo.frmHowToInsertChords();
    //  frm.Show();
    //}

    //private void cmdSyncAudio_Click(object sender, EventArgs e) {
    //  //if (P.F.AutoSync == null) P.F.AutoSync = new clsAutoSyncBeat();
    //  if (P.F.AutoSync != null) P.F.AutoSync.StartCmdSyncRecord();
    //}

    private void mnuShowAudioSyncWindow_Click(object sender, EventArgs e) {
      if (P.F == null || P.F.AudioSync == null) return;
      if (P.F.frmAutoSync == null) {
        P.F.frmAutoSync = new frmAutoSync(P.F.AudioSync);
        //P.F.frmAutoSync.Show();
      }
      //P.F.frmAutoSync.cmdShow_Click(null, null);  //update lists
      //P.F.frmAutoSync.Activate();
      Utils.FormAct(P.F.frmAutoSync);
    }

    private void chkShowChordNotes_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      cmbFirstNote.Enabled = chkShowChordNotes.Checked;
      lblcmbFirstNote.Enabled = chkShowChordNotes.Checked;
      picChordNames.Refresh();
    }

    private void mnuHelpContents_Click(object sender, EventArgs e) {
      HelpNavigator navigator = HelpNavigator.TableOfContents;
      //string helpfile = Cfg.CfgPath + @"\MainHelp\ChordCadenza.chm";
      //Help.ShowHelp(this, helpfile, navigator, "PlayMap.htm");
      Help.ShowHelp(this, Cfg.HelpFilePath, navigator);
    }

    private void mnuShowInitialScreen_Click(object sender, EventArgs e) {
      if (P.frmInitial == null || !P.frmInitial.IsHandleCreated) {
        P.frmInitial = new Forms.frmInitial();
      }
      //P.frmInitial.Show();
      //P.frmInitial.Activate();
      Utils.FormAct(P.frmInitial);
    }

    private void optShowNote_CheckedChanged(object sender, EventArgs e) {
      if (P.frmSC != null) P.frmSC.Refresh();
      P.F?.frmTonnetz?.CreateNodes();
    }

    private void chkRunChordNotes_CheckedChanged(object sender, EventArgs e) {
      if (P.frmSC != null) P.frmSC.Refresh();
    }

    private void cmbChordNotes_SelectedIndexChanged(object sender, EventArgs e) {
      if (P.frmSC != null) P.frmSC.picChordNames.Refresh();
    }

    //private void PopulateCmbFirstNote() {
    //  cmbFirstNote.Items.Add("Root");
    //  foreach (string n in NoteName.MajKeyNames) {
    //    cmbFirstNote.Items.Add(n);
    //  }
    //  cmbFirstNote.SelectedIndex = 0;  //root
    //}

    private void cmdHelp_Click(object sender, EventArgs e) {
      //Help.ShowHelp(this, helpfile, navigator, "PlayMap.htm");
      Help.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_PlayMap_Intro.htm");
    }

    private void mnuPathMidiFiles_Click(object sender, EventArgs e) {
      string dir = GetUserDirectory("Select Folder to Copy MidiFiles from when creating New Project", Cfg.MidiFilesPath);
      if (dir == "") return;
      Cfg.MidiFilesPath = dir;
    }

    private void mnuPathAudioFiles_Click(object sender, EventArgs e) {
      string dir = GetUserDirectory("Select Folder to Copy AudioFiles from when creating New Project", Cfg.AudioFilesPath);
      if (dir == "") return;
      Cfg.AudioFilesPath = dir;
    }

    //private void mnuLoadMidiFile_Click(object sender, EventArgs e) {
    //  P.frmStart.LoadMidiFile(true);
    //}

    private void mnuRestart_Click(object sender, EventArgs e) {
      string msg = "This will restart the application, after prompting to save any changes.";
      msg += "\r\nReply OK to Continue, else Cancel";
      if (MessageBox.Show(msg, MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
      RestartApp();
    }

    internal void RestartApp() {
      FormClosingEventArgs e = new FormClosingEventArgs(CloseReason.None, false);
      frmSC_FormClosing(null, e);
      if (e.Cancel) return;
      Bypass_Event = true;
      Application.Restart();
    }

    internal void mnuSaveProject_Click(object sender, EventArgs e) {
      if (P.F == null || P.F.CF == null) return;

      if (P.F.Project.PathAndName == "") {
        MessageBox.Show("Project not found - nothing saved");
        return;
      }

      //try {
      //  MessageBox.CacheOn();
        using (new clsWaitCursor()) {
          bool ok = P.F.SaveProject(P.F.Project);
        }
      //}
      //finally {
      //  MessageBox.CacheOff();
      //}
    }

    internal void mnuSaveProjectAs_Click(object sender, EventArgs e) {
      //* check current project exists
      if (P.F == null || P.F.CF == null || P.F.Project.PathAndName == "") {
        MessageBox.Show("Project not found - nothing to save");
        return;
      }

      //* show SaveAs dialog
      //try {
      //  MessageBox.CacheOn();
        dlgSaveProjectAs frm = new Forms.dlgSaveProjectAs();
        frm.ShowDialog();
      //}
      //finally {
      //  MessageBox.CacheOff();
      //}
    }

    internal void UpdateRecentProjects() {
      //* UpdateRecentProjects with P.F.Project.ProjectPath
      if (P.F.Project.ProjectPath == "") return;
      string txt = P.F.Project.ProjectPath + "\\" + P.F.Project.Name;
      if (RecentProjects.Contains(txt)) RecentProjects.Remove(txt);
      RecentProjects.Insert(0, txt);  //most recent first
      if (RecentProjects.Count > 10) RecentProjects.RemoveRange(10, RecentProjects.Count - 10);
      string recentmsg = Utils.SaveFile(Cfg.RecentProjectsLines, SaveFileSub);
      if (recentmsg != "") Debug.WriteLine("Error saving Recent Projects list: " + recentmsg);
      else AddMenuRecentProjects();
    }

    internal void RemoveRecentProject(string dir) {
      RecentProjects.Remove(dir);
      string recentmsg = Utils.SaveFile(Cfg.RecentProjectsLines, SaveFileSub);
      if (recentmsg != "") Debug.WriteLine("Error saving Recent Projects list: " + recentmsg);
      else AddMenuRecentProjects();
    }

    private void SaveFileSub(StreamWriter xsw) {
      foreach (string f in RecentProjects) xsw.WriteLine(f);
    }

    internal string SaveChordFile() {
      P.F.CF.CreateEvs();
      try {
        //if (P.F.CF.SaveTxtFile()) {
        //  MessageBox.Show("Error saving ChordFile");
        //  return false;
        //}
        return P.F.CF.SaveTxtFile();
      }
      catch (Exception exc) {
      //  MessageBox.Show("Error saving ChordFile: " + exc.Message);
      //  return false;
        return exc.Message;
      }
      //return "";
    }

    private void trkAudioVol_Scroll(object sender, EventArgs e) {
      if (P.F == null || P.F.AudioSync == null) return;
      P.F.AudioSync.MP3Player.Vol = trkAudioVol.Value;
    }

    private void trkAudioPan_Scroll(object sender, EventArgs e) {
      if (P.F == null || P.F.AudioSync == null) return;
      P.F.AudioSync.MP3Player.Pan = trkAudioPan.Value;
    }

    private void trkKBVol_Scroll(object sender, EventArgs e) {
      if (MidiPlay.OutMKB != null) MidiPlay.OutMKB.SetStreamVol(trkKBVol.Value);
    }

    private void trkKBPan_Scroll(object sender, EventArgs e) {
      if (MidiPlay.OutMKB != null) MidiPlay.OutMKB.SetStreamPan(trkKBPan.Value);
    }

    private void trkStreamVol_Scroll(object sender, EventArgs e) {
      if (MidiPlay.OutMStream != null) MidiPlay.OutMStream.SetStreamVol(trkStreamVol.Value);
    }

    private void trkStreamPan_Scroll(object sender, EventArgs e) {
      if (MidiPlay.OutMStream != null) MidiPlay.OutMStream.SetStreamPan(trkStreamPan.Value);
    }

    internal void cmdTonnetz_Click(object sender, EventArgs e) {
      if (P.F == null) return;
      if (P.F.frmTonnetz == null) P.F.frmTonnetz = new Forms.frmTonnetz(this);  //-->Show()
      Utils.FormAct(P.F.frmTonnetz);
    }

    internal void mnuSaveMidiFileAs_Click(object sender, EventArgs e) {
      SaveMidiFileAs();
    }

    internal string SaveMidiFileAs() {
      if (P.F.Project.MidiExists) {
        //try {
        //  MessageBox.CacheOn();
          if (P.frmSaveMidiFileAs.ShowDialog(this) == DialogResult.Cancel) return "Save MidiFile cancelled";
          clsSaveMidiFile savemidifile = new clsSaveMidiFile(P.F.FSTrackMap);
          //if (P.F.frmTrackMap != null) P.F.frmTrackMap.indChanged = false;
          //bool res = savemidifile.SaveAs();
          //if (!res) {
          //  MessageBox.Show("MidiFile not saved");
          //  return false;
          //}
          return savemidifile.SaveAs();
        //}
        //finally {
        //  MessageBox.CacheOff();
        //}
      }
      return "";
    }

    private void mnuAbout_Click(object sender, EventArgs e) {
      Forms.dlgAbout frm = new dlgAbout();
      frm.ShowDialog(this);
    }

    //private void frmSC_KeyPress(object sender, KeyPressEventArgs e) {
    //  //Debug.WriteLine("KeyPress: " + e.KeyChar);
    //  if (P.PCKB == null) return;
    //  e.Handled = true;  //kb events at form level only
    //  P.PCKB.KeyPress(sender, e);
    //}

    private void frmSC_KeyUp(object sender, KeyEventArgs e) {
      if (P.PCKB == null) return;
      P.PCKB.KeyUpDown(e, false);
    }

    private void frmSC_KeyDown(object sender, KeyEventArgs e) {
      if (P.PCKB == null) return;
      P.PCKB.KeyUpDown(e, true);
    }

    private void frmSC_KeyPress(object sender, KeyPressEventArgs e) {
      if (P.PCKB == null) return;
      e.Handled = true;  //kb events at form level only (assumes KeyPreview set)
    }

    private void cmdSyncAudio_Click(object sender, EventArgs e) {
      if (P.F.AudioSync != null) P.F.AudioSync.StartCmdSyncRecord();
    }

    private void cmdPlayAndRecord_Click(object sender, EventArgs e) {
      if (P.F.AudioSync != null) P.F.AudioSync.StartCmdSyncPlayAndRecord();
    }

    private void cmdShowAudioSyncWindow_Click(object sender, EventArgs e) {
      if (P.F.frmAutoSync == null && P.F != null && P.F.AudioSync != null) {
        P.F.frmAutoSync = new frmAutoSync(P.F.AudioSync);
      }
      Utils.FormAct(P.F.frmAutoSync);
    }

    private void cmdUpdateLyrics_Click(object sender, EventArgs e) {
      if (P.F.frmLyrics == null) {
        P.F.frmLyrics = new frmLyrics();
      }
      Utils.FormAct(P.F.frmLyrics);
    }

    private void cmdSaveProject_MouseUp(object sender, MouseEventArgs e) {
      if (e.Button == MouseButtons.Right) {  //Click not raised
        P.F.SaveProject(null);  //check and save
      }
    }

    private void mnuLoadMultiMidi_Click(object sender, EventArgs e) {
      string dir = @"D:\D0\Music\Midi Files\Temp\Temp";
      string debugpath = @"D:\Temp\MultiMidiTest.txt";
      using (P.MMSW = new StreamWriter(debugpath, false)) {   //overwrite or create (not append)
        P.MMSW.AutoFlush = true;
        string[] paths = Directory.GetFiles(dir, "*.mid", SearchOption.TopDirectoryOnly);
        Array.Sort(paths);
        foreach (string path in paths) {
          try {
            //MessageBox.CacheOn();
            P.MMSW.WriteLine("    " + path + " load started...");
            if (P.frmStart.LoadProject(path, updaterecent: false)) {
              P.MMSW.WriteLine("    " + path + " loaded");
              clsSaveMidiFile savemidifile = new clsSaveMidiFile(P.F.FSTrackMap);
              string savename = Path.GetFileName(path) + ".chca.mid";
              string savepath = dir + "\\Saved\\" + savename;
              savemidifile.Save(savepath);
              P.MMSW.WriteLine("    " + savepath + " saved");
            } else {
              P.MMSW.WriteLine("*** " + path + " load failed");
            }
            P.MMSW.WriteLine("    -----------------------------------------------------------------------------------------------");
          }
          catch (Exception exc) {
            P.MMSW.WriteLine("*** EXCEPTION: " + exc.Message);
            continue;
          }
          //finally {
          //  MessageBox.CacheOff();
          //}
        }
      }
    }

    private void nudTransposeStreamPitch_ValueChanged(object sender, EventArgs e) {
      if (P.F?.CF != null) P.F.CF.indSave = true;
    }

    private void optSustain_CheckedChanged(object sender, EventArgs e) {
      //if (Bypass) return;
      if (clsPlay.PlayExists()) {
        clsPlay.Sustain = clsPlay.clsSustain.New(null);  //new sustain
      } else {
        clsPlay.clsSustain.PlayPedalStatic(false);
      }
    }

    private void chkSustainAuto_CheckedChanged(object sender, EventArgs e) {
      if (chkSustainAuto.Checked) SetAutoSustain();
    }

    internal void SetAutoSustain() {
      if (!chkSustainAuto.Checked) return;
      //if (P.F == null || P.F.FSTrackMap == null) return;
      int patch = cmbKBChanPatch.SelectedIndex - 1;
      if (patch < 0 || patch > 127) return;
      if (Cfg.Patches_SustainCarryOver[patch]) optSustainCarryOver.Checked = true;
      else if (patch < 40) optSustainReplay.Checked = true;  //percussion instrument
      //* else leave alone
    }

    private void chkShowChordsRel_CheckedChanged(object sender, EventArgs e) {
      //if (Bypass) return;
      picChordNames.Refresh();
    }

    private void cmdResetPlay_Click(object sender, EventArgs e) {
      PlayableForms.CmdState_Stopped();
    }

    private void mnuMap_Click(object sender, EventArgs e) {
      panMaps.Visible = mnuMap.Checked;
      MapsEnabled(mnuMap.Checked);
      if (panMaps.Visible) NewMidiOrCF();
      ResizeForm();
    }

    internal void MapsEnabled(bool ind) {
      //cmdXPos.Visible = ind && MidiOrCFEXists();
      //cmdXNeg.Visible = ind && MidiOrCFEXists();
      //chkScroll.Visible = ind && MidiOrCFEXists();
      lstTrks.Visible = ind && P.F.Project.MidiExists;
    }

    private void mnuControls_Click(object sender, EventArgs e) {
      panControls.Visible = mnuControls.Checked;
      ResizeForm();
    }

    private void picBarsX_Paint(object sender, PaintEventArgs e) {
      Forms.frmTrackMap.Bars_Paint(e, picBarsX, panNoteMap, BarFont, HPixPerQI.NN, HPixPerQI.DD);
    }

    private void picBarsX_MouseClick(object sender, MouseEventArgs e) {
      Forms.frmTrackMap.Bars_MouseClick(e, HPixPerQI.DD, HPixPerQI.NN, panNoteMap);
    }

    private void picChordNamesX_Paint(object sender, PaintEventArgs e) {
      if (P.F.CF?.Evs == null || P.F.CF.Evs.Count == 0) return;
      Graphics xgr = e.Graphics;
      Panel pan = panNoteMap;
      Brush blackbrush = Brushes.Black;
      Brush altbrush = new SolidBrush(Color.FromArgb(127, Color.SkyBlue));
      Brush currentbrush = new SolidBrush(Color.FromArgb(127, Color.Green));
      int ypos1 = picChordNamesX.Height / 2;
      int ypos = 0;
      int prevontime = -99999;  //don't use int.MinValue (overflow)
      //int minspace = P.F.MTime.TicksPerQNote;
      //int minpix = 100;
      bool coloured = false;

      //* highlight alternate chords
      int onpix, offpix;
      List<clsCF.clsEv> evs = P.F.CF.Evs;
      bool prevposlow = false;
      bool prevoversize = false;
      for (int i = 0; i < evs.Count; i++) {
        clsCF.clsEv ev = evs[i];
        onpix = ev.OnTime * HPixPerQI / P.F.TicksPerQI;
        offpix = ev.OffTime * HPixPerQI / P.F.TicksPerQI;
        int xpos = onpix;
        string txt = ev.ChordName(eKBTrans.Add, true);
        if (prevposlow) {
          ypos = 0;
          prevposlow = false;
        } else {
          ypos = (prevoversize) ? ypos1 : 0;
          prevposlow = (ypos > 0);
        }
        int width = (int)xgr.MeasureString(txt, ChordMapFont).Width; 
        prevoversize = (offpix - onpix < width + 10);
        //ypos = (ev.OnTime - prevontime <= minspace) ? ypos1 : 0;
        if (coloured) xgr.FillRectangle(altbrush, onpix, 0, offpix - onpix, picChordNamesX.Height); 
        xgr.DrawString(txt, ChordMapFont, blackbrush, xpos, ypos);
        prevontime = (ypos == ypos1) ? -99999 : ev.OnTime;
        if (i < evs.Count -1 && evs[i + 1].Notes.Length > 0) coloured = !coloured;
      }

      //* highlight current chord
      int index = P.F.CF.FindCFEv(P.F.CurrentBBT);
      clsCF.clsEv eva = evs[index];
      if (eva.Notes.Length > 0) {
        onpix = (eva.OnTime * HPixPerQI) / P.F.TicksPerQI;
        if (index < evs.Count - 1 && evs[index + 1].Notes.Length == 0) {
          offpix = (evs[index + 1].OffTime * HPixPerQI) / P.F.TicksPerQI;
        } else {
          offpix = (eva.OffTime * HPixPerQI) / P.F.TicksPerQI;
        }
        xgr.FillRectangle(currentbrush, onpix, 0, offpix - onpix, picChordNamesX.Height);
      }

    }

    private void cmdXPos_Click(object sender, EventArgs e) {
      if (HPixPerQI.NN >= 10) return;  //smallest scale
      if (picNoteMap.Width > 16380) return;  //MouseClick limit approx 32768
      IntDiv oldhpixperqi = new IntDiv(HPixPerQI.NN, HPixPerQI.DD);
      HPixPerQI++;
      MapCsrPix = (MapCsrPix * HPixPerQI) / oldhpixperqi;
      ResizeForm();
      SetScroll();
      Refresh();
    }

    private void cmdXNeg_Click(object sender, EventArgs e) {
      if (HPixPerQI.DD >= 4) return;  //largest scale
      IntDiv oldhpixperqi = new IntDiv(HPixPerQI.NN, HPixPerQI.DD);
      HPixPerQI--;
      MapCsrPix = (MapCsrPix * HPixPerQI) / oldhpixperqi;
      ResizeForm();
      SetScroll();
      Refresh();
    }

    private void SetScroll() {
      ScrollFromRefreshBBT = true;
      int maxwidth = P.F.MaxBBT.QI * HPixPerQI;
      int posx = Math.Min(Math.Max(0, MapCsrPix - ScrollMarginLo), maxwidth);  //positive
                                                                          //int poslyrics = Math.Max(0, posx - SystemInformation.VerticalScrollBarWidth);
      int posy = panNoteMap.AutoScrollPosition.Y;  //zero or negative
      panNoteMap.AutoScrollPosition = new Point(posx, -posy);  //(positive, -negative)
      int poslyrics = -panNoteMap.AutoScrollPosition.X;
      dgvLyrics.HorizontalScrollingOffset = poslyrics;
      dgvLyrics.Refresh();
      picBars.Refresh();
      ScrollFromRefreshBBT = false;
    }

    //private void SetScroll() {
    //  ScrollFromRefreshBBT = true;
    //  int maxwidth = P.F.MaxBBT.QI * HPixPerQI;
    //  int pos = Math.Min(Math.Max(0, MapCsrPix - ScrollMarginLo), maxwidth);
    //  panNoteMap.AutoScrollPosition = new Point(pos, 0);
    //  dgvLyrics.HorizontalScrollingOffset = pos;
    //  dgvLyrics.Refresh();
    //  ScrollFromRefreshBBT = false;
    //}

    private void dgvLyrics_ColumnAdded(object sender, DataGridViewColumnEventArgs e) {
      e.Column.FillWeight = 10;  //to get round column limit (totalfillweight < 65535) default 100
    }

    internal void panNoteMap_Scroll(object sender, ScrollEventArgs e) {
      ScrollFromPan = true;
      if (!ScrollFromdgvLyrics) {
        dgvLyrics.HorizontalScrollingOffset = e.NewValue;
      }
      ScrollFromPan = false;
      Refresh();
    }

    private void picNoteMap_Paint(object sender, PaintEventArgs e) {
      if (PicNMSC == null) return;  

      PictureBox pic = (PictureBox)sender;
      Graphics xgr = e.Graphics;

      int pixlo = e.ClipRectangle.X;
      int pixhi = e.ClipRectangle.X + e.ClipRectangle.Width;

      PicNMSC.PaintMap(P.F.FSTrackMap.NoteMap, null, xgr, pixlo, pixhi, P.F.MTime);
      Pen pencsr = new Pen(P.ColorsNoteMap["Play Cursor"].Co, 3);  //was green
      e.Graphics.DrawLine(pencsr, MapCsrPix, 0, MapCsrPix, pic.Height);  //play csr
    }

    //private void nudTrk_Click(object sender, EventArgs e) {
    //  if (!P.F.Project.MidiExists) return;
    //  panNoteMap.Show();
    //  int trk = (int)nudTrk.Value - 1;
    //  if (trk >= P.F.Trks.NumTrks) trk = 0;
    //  Trk = new clsTrks.T(P.F.Trks, trk);  
    //  PicNMSC = new clsPicNoteMapSC(picNoteMap, Trk);
    //  ResizeForm();
    //}

    private void lstTrks_SelectedIndexChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      if (!P.F.Project.MidiExists) return;
      //panNoteMap.Show(); 
      ListBox.SelectedIndexCollection trks = lstTrks.SelectedIndices;
      MapTrks.Clear();
      foreach (int trk in trks) {
        if (trk < 0 || trk >= P.F.Trks.NumTrks) continue;
        MapTrks.Add(new clsTrks.T(P.F.Trks, trk));
      }
      if (MapTrks.Count == 0) MapTrks.Add(new clsTrks.T(P.F.Trks, 0));
      PicNMSC = new clsPicNoteMapSC(picNoteMap);
      if (P.F.CF != null) P.F.CF.indSave = true;
      ResizeForm();
    }

    private void picMap_MouseClick(object sender, MouseEventArgs e) {
      //if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.None) return;
      if (!IsPlayClickable()) return;
      int ticks = (e.X * P.F.TicksPerQI) / HPixPerQI;
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
      bbt.RoundDownToBar();
      P.frmSC.Play?.NewReset();
      Forms.frmStart.RefreshBBT(bbt);
      P.F.StartBar = bbt.Bar;
    }

    internal bool IsPlayClickable() {
      if (MidiPlay.Sync.indPlayActive == clsSync.ePlay.None) return true;
      if (Play == null) return true;
      return Play.IsManSync;
    }

    private void mnuManChordSync_Click(object sender, EventArgs e) {
      if (P.frmManChordSync == null) P.frmManChordSync = new Forms.frmManChordSync();
      Utils.FormAct(P.frmManChordSync);
    }

    private void chkManSyncChord_CheckedChanged(object sender, EventArgs e) {
      //clsPlay.indNextBarBeat = chkManSyncBarBeat.Checked;
      clsPlay.indSwitchKey = !chkManSyncChord.Checked;
      if (chkManSyncChord.Checked) {  //man chord positioning
        chkSwitchKBChord.Checked = true;
        P.frmStart.chkConstantChordPlay.Checked = false;
        if (P.frmManChordSync == null) P.frmManChordSync = new Forms.frmManChordSync();  //!!!//
        Utils.FormAct(P.frmManChordSync);  //!!!//
      }
    }

    private void mnuShowFrmConsole_Click(object sender, EventArgs e) {
      #if ADVANCED
        P.frmConsole.Show();
      #endif
    }

    private void mnuReloadProject_Click(object sender, EventArgs e) {
      try {
        if (P.F.Project.CHPPath == "") return;
        if (P.F != null && !P.F.SaveProject(null)) return;  //check and save
        MidiPlay.Sync.Stop();
        if (!P.frmStart.LoadProject(P.F.Project.CHPPath, false)) {
          MessageBox.Show("Load Project failed - no valid file found");
          return;  //-> reload frmShowChords
        }
      }
      catch (Exception exc) {
        //* programming error or corrupt/inconsistent files, ...
        MessageBox.Show("Reload Project failed: " + exc.Message);
      }
    }

    private void frmSC_Activated(object sender, EventArgs e) {
      ResizeForm();
    }

    //private int SizeCnt = 0;
    private void frmSC_SizeChanged(object sender, EventArgs e) {
      //Debug.WriteLine(++SizeCnt + ": Size Changed");
      ResizeForm();  //--> no action if minimized
    }

    private void mnuShowFrmTest_Click(object sender, EventArgs e) {
      frmTest frm = new frmTest();
      frm.Show();
    }

    private void mnuWebPage_Click(object sender, EventArgs e) {
      Process.Start("http://ChordCadenza.org");
      //Process.Start("http://localhost");
    }

    private void mnuSoundFonts_Click(object sender, EventArgs e) {
      Process.Start("http://ChordCadenza.org/#loc_soundfonts");
      //Process.Start("http://localhost/#loc_soundfonts");
    }

    /*
    //*************Testing******************************************************************************
    //private struct TIMECAPS {
    //  internal int wPeriodMin;  //UINT
    //  internal int wPeriodMax;  //UINT
    //}

    [Flags]
    internal enum TimerType : uint {
    OneShot = 0x0000,
    Periodic = 0x0001,
    //* NOTE: setting the KillSynchronous flag may cause hangs!!!
    KillSynchronous = 0x0100  //This flag prevents the event from occurring after the user calls timeKillEvent() to destroy it.            
    }

    //[DllImport("winmm.dll")]
    //private static extern int timeGetDevCaps(ref TIMECAPS ptc, int cbtc);
    [DllImport("winmm.dll")]
    private static extern int timeBeginPeriod(uint uPeriod);
    [DllImport("winmm.dll")]
    private static extern int timeEndPeriod(uint uPeriod);
    [DllImport("winmm.dll")]
    private static extern int timeGetTime();
    [DllImport("winmm.dll")]
    private static extern uint timeSetEvent(
    uint uDelay,        //UINT
    uint uResolution,   //UINT
    delegCallback lpTimeProc,    //LPTIMECALLBACK
    uint dwUser,        //DWORD_PTR
    uint fuEvent        //UINT
    );
    [DllImport("winmm.dll")]
    private static extern uint timeKillEvent(uint uTimerID);
    private delegate void delegCallback(uint uID, uint uMsg, uint dwUser, int dw1, int dw2);
    //private static uint MinPeriod { get { return (uint)ptc.wPeriodMin; } }
    //private static uint MaxPeriod { get { return (uint)ptc.wPeriodMax; } }
    //private static TIMECAPS ptc;
    private static delegCallback Test_MM_dCallback;
    private static uint TimerID;

    private static int Test_Count = 0;
    private static Stopwatch Test_SW = new Stopwatch();
    private System.Timers.Timer Test_Sys_Timer = new System.Timers.Timer(100);  //ms
    private static int PrevTime = 0;

    private static void Test_MM_Callback(uint uID, uint uMsg, uint dwUser, int dw1, int dw2) {
    Test_Count++;
    if (Test_Count % 10 == 0) {
    Debug.WriteLine("TimeDiff: " + (Test_SW.ElapsedMilliseconds - PrevTime)
    + "ms, Timer Count: " + Test_Count);
    }
    PrevTime = (int)Test_SW.ElapsedMilliseconds;
    }

    private static void Test_Sys_OnTimedEvent(object source, System.Timers.ElapsedEventArgs e) {
    Test_Count++;
    if (Test_Count % 10 == 0) {
    Debug.WriteLine("TimeDiff: " + (Test_SW.ElapsedMilliseconds - PrevTime)
    + "ms, Timer Count: " + Test_Count);
    }
    PrevTime = (int)Test_SW.ElapsedMilliseconds;
    }

    private void cmdTest_Click_Sys(object sender, EventArgs e) {
    Debug.WriteLine("Stopwatch: Frequency = " + Stopwatch.Frequency + " HighResolution = " + Stopwatch.IsHighResolution);
    Test_SW.Start();
    Test_Sys_Timer.AutoReset = true;
    Test_Sys_Timer.Elapsed += Test_Sys_OnTimedEvent;
    Test_Sys_Timer.Start();
    }

    private void cmdTest_Stop_Click_Sys(object sender, EventArgs e) {
    Test_Sys_Timer.Stop();
    }

    private void cmdTest_Click(object sender, EventArgs e) {
    Test_MM_dCallback = Test_MM_Callback;
    Debug.WriteLine("Stopwatch: Frequency = " + Stopwatch.Frequency + " HighResolution = " + Stopwatch.IsHighResolution);
    Test_SW.Start();
    uint resolution = 20;
    uint freq = 100;
    TimerID = timeSetEvent(freq, resolution, Test_MM_dCallback, (uint)0, (uint)TimerType.Periodic);
    }

    private void cmdTest_Stop_Click(object sender, EventArgs e) {
    timeKillEvent(TimerID);
    }

    //***end testing********************************************************************************
    */

  }
}
