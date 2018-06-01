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
using ExtensionMethods;

namespace ChordCadenza.Forms {
  internal interface IFrmDGV {
    //* interface for forms containing DGV (chords)
    //* frmNoteMap & frmMultiMap
    int TicksToPix(int ticks);
    int TransposeChordNamesVal { get; }
    //int TransposeDisplayNotesVal { get; }
    //int ChordTransposeNotesVal { get; }
    int ChordTranspose(int pc);
    int ChordTransposeReverse(int pc);
    void RefreshpicNoteMapFile();
    void SetNoteMapFileChanged(bool undoredo, bool indqi);
    DataGridView Prop_dgvLyrics { get; }
    }

  internal partial class frmChordMap :
    Form, IFormPlayable, IFormStream, Forms.IShowNoteMap, Forms.IFrmDGV, IFormProjectName, ITT {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    internal enum eSnapTo { Notes, Selected, Bar, HalfBar, Beat };
    //private clsUndoRedoTSigs UndoRedoTSigs;
    internal bool Bypass_Event = false;

    private clsMTime.clsBBT Mouse_BBT;
    private int Mouse_Index;
    //private bool picTSigActive = false;

    //private static Rectangle Rect = new Rectangle(0, 0, 0, 0);
    //internal static bool SnapNote = false;
    //internal static bool SnapTime = true;
    //internal static string SnapTo = "Bar";
    internal static bool CfgShowBeats = true;
    internal static bool CfgShowKB = true;
    internal static bool CfgOneOctave = true;
    internal static int CfgVPixPerNote = 10;
    internal static int StaticTransposeChordNamesVal = 0;

    //internal bool Simple = false;
    //internal clsChordSegs ChordSegs;
    internal clsBeatChords BeatChords;
    public int HFactor { get { return HPixPerQI.NN; } }
    public int VFactor { get { return VPixPerNote; } }
    public int HDiv { get { return HPixPerQI.DD; } }
    public clsFileStream FileStream { get { return CSVFileConv; } }
    public bool ShowBeats { get { return chkShowBeats.Checked; } }
    public bool OneOct { get { return MidiHeight == 0; } }
    public bool ShowKB { get { return chkShowKB.Checked; } }
    public Font BarFont { get { return _BarFont; } }
    public int Octaves { get { return _Octaves; } }
    public int MinC { get { return _MinC; } }

    internal int MidiHeight = 0;  //0 = one octave, else multioctave   
    internal Font _BarFont = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);
    internal IntDiv HPixPerQI;
    internal int VPixPerNote;
    //private StreamWriter xsw;
    //private MemoryStream xstream;
    internal clsFileStream CSVFileConv;
    //internal clsFileStream FSMultiMap;  //before trk select
    //private clsPicNoteMapNM PicNoteMap;

    internal int MinLenTicks = 0;
    internal int NoteFilterTicks = 0;
    internal int CloseGapTicks = 0;
    internal int QuantizeTicks = 0;
    internal int TrimInnerQI = 0;
    internal int TrimOuterQI = 0;
    internal eAlign Align = eAlign.Interval;
    internal eAlign AlignTrim = eAlign.Bar;
    private eAlign AlignChords = eAlign.None;

    private int ScrollMarginLo = 100;
    private int ScrollMarginHi = 200;
    //private clsNoteMap UnfilteredNoteMap;
    //internal bool indChanged = false;

    //internal int CsrPixPos = -1;  //cursor
    internal int CsrPixPos {
      get { return CsrPixLo; }
      set { CsrPixLo = value; }
    }

    private int LastCsrPos = -1;
    private int _CsrPixLo, _CsrPixHi;  //selected area
    private int _CsrQILo, _CsrQIHi;  //selected area
    //private int MaxQTime;
    private clsMute Mute;
    //internal bool ShowChordBoxes;

    private bool[] NoteMap_AllNotesOn = new bool[12];
    private bool[] NoteMap_AllNotesOff = new bool[12];
    //private clsKeys MKeys;

    private clsPicNoteMapNM PicNMMidi;
    private clsPicNoteMapNM PicNMQuant;
    private clsPicNoteMapNM PicNMFile;
    private int _Octaves = 1;
    private int _MinC = 127;

    private const int ChSymVGap = 5;
    //private clsBarBU BarBU;
    internal bool Bypass_DGV = false;
    private DataGridView NoSelectDGV = null;
    private bool Form_Loaded = false;
    internal static bool ScrollFromPan = false;
    internal static bool ScrollFromDGV = false;
    internal static bool ScrollFromdgvLyrics = false;

    internal frmChordMap(clsFileStream csvfileconv) {
      InitializeComponent();
      Forms.frmSC.ZZZSetPCKBEvs(this);

      VPixPerNote = 10;  //should be overridden later with Static...
      bool indsavesave = (P.F.CF == null) ? false : P.F.CF.indSave;
      try {
        //dgvLyrics.GotFocus += dgvLyrics_GotFocus;
        //DGV.GotFocus += DGV_GotFocus;
        MidiHeight = (chkOneOct.Checked) ? 0 : 1;
        //dgvLyrics.CurrentCellChanged += dgvLyrics_CurrentCellChanged;
        dgvLyrics.CellClick += dgvLyrics_CellClick;
        DGV.CurrentCellChanged += DGV_CurrentCellChanged;
        //UndoRedoTSigs = new clsUndoRedoTSigs(this);
        Width = 1500;
        picBars.Width -= SystemInformation.VerticalScrollBarWidth;
        DGV.Width -= SystemInformation.VerticalScrollBarWidth;
        dgvLyrics.Width -= SystemInformation.VerticalScrollBarWidth;
        picMargins.Width -= SystemInformation.VerticalScrollBarWidth;
        if (csvfileconv == null) {
          CSVFileConv = new clsFileStream();
          //panVMidi.Hide();
          picNoteMapMidi.Hide();
          picNoteMapQuant.Hide();
          lblMidiLit.Hide();
          cmdLoadMidi.Enabled = false;
          grpChordsDiv.Enabled = false;
          cmdExec.Enabled = false;
          cmdConfigChords.Enabled = false;
          grpCopyOptions.Enabled = false;
          cmdAdvanced.Enabled = false;
          cmdPlayMidi.Enabled = false;
          chkShowQuant.Enabled = false;
          lblTracksSelected.Text = "No\r\nMidiFile";
        } else {
          CSVFileConv = csvfileconv;
          lblTracksSelected.Text = UpdateTrksSelected(CSVFileConv.TrkSelect);
          //FSMultiMap = fsmultimap;
          //if (simple) InitSimple();
        }
        //P.Forms.Add(this);
        //DGV.DoubleBuffered(true);  //extension method
        BeatChords = new clsBeatChords(this, DGV);
        Mute = new clsMute();
        foreach (clsTrks.T trk in CSVFileConv.TrkSelect.Next) {
          Mute[trk] = !CSVFileConv.TrkSelect[trk];
        }
        Mute.ExclCh10 = true;

        //switch (P.F.QIPerNote) {
        //  case 16:
        //    HPixPerQI = new IntDiv(4, 1); break;
        //  case 32:
        //    HPixPerQI = new IntDiv(2, 1); break;
        //  case 64:
        //    HPixPerQI = new IntDiv(1, 1); break;
        //  default:
        //    LogicError.Throw(eLogicError.X040);
        //    HPixPerQI = new IntDiv(2, 1); break;
        //}

        HPixPerQI = InitQIPerNote();

        //HPixPerQI = 2;
        CsrPixLo = 0;
        //CsrPixHi = P.F.MaxBBT.QI * HPixPerQI;
        CsrPixHi = 0;
        //CsrPixTxtHi = P.F.CFtxt.NoteMap.GetLengthQTime() * HPixPerQI;
        for (int note = 0; note < 12; note++) {
          NoteMap_AllNotesOff[note] = false;
          NoteMap_AllNotesOn[note] = true;
        }
      }
      finally {
        if (P.F.CF != null) P.F.CF.indSave = indsavesave;
      }
    }

    //protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
    //  bool? ret = Forms.frmSC.StaticProcessCmdKey(ref msg, keyData);
    //  if (!ret.HasValue) return base.ProcessCmdKey(ref msg, keyData);
    //  return ret.Value;
    //}

    internal static IntDiv InitQIPerNote() {
      if (P.F.QIPerNote <= 16) return new IntDiv(4, 1);
      if (P.F.QIPerNote <= 32) return new IntDiv(2, 1);
      if (P.F.QIPerNote <= 64) return new IntDiv(1, 1);
      if (P.F.QIPerNote <= 128) return new IntDiv(1, 2);
      if (P.F.QIPerNote <= 192) return new IntDiv(1, 3);
      if (P.F.QIPerNote <= 256) return new IntDiv(1, 4);
      return new IntDiv(1, 5);
    }

    private void frmNoteMap_Load(object sender, EventArgs e) {
      bool indsavesave = (P.F.CF == null) ? false : P.F.CF.indSave;
      try {
        BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
        clsTT.LoadToolTips(this);
        //LoadStatic();
        //P.F.NewMTime = null;
        #if !ADVANCED
          panAdvanced.Hide();
          cmdChordinate.Hide();
          cmdTonnetz.Hide();
          cmdTest.Hide();
        #endif
        SetFormTitle();
        if (P.F.CF != null) txtCHPDesc.Text = P.F.CF.Description;
        clsKeyTicks key = P.F.Keys[0];  //[ticks]
        picNoteMapMidi.BackColor = P.ColorsNoteMap["Background"].Co;
        picNoteMapQuant.BackColor = P.ColorsNoteMap["Background"].Co;
        picNoteMapFile.BackColor = P.ColorsNoteMap["Background"].Co;

        //frmStart.SetNudAndTag(nudTSigDD);

        TrimInnerQI = P.F.MTime.DD2DI((int)P.frmCfgChords.nudTrimInnerNN.Value, (int)P.frmCfgChords.nudTrimInnerDD.Value);
        TrimOuterQI = P.F.MTime.DD2DI((int)P.frmCfgChords.nudTrimOuterNN.Value, (int)P.frmCfgChords.nudTrimOuterDD.Value);

        P.frmCfgChords.SetoptAlignTrim();
        SetoptChords();

        //lblKey.Text = "Key: " + NoteName.ToSharpFlat(key.KeyNoteStr) + " " + key.Scale;
        //lblTransposedKey.Text = "TransKey: " + ChordKey;
        //lblTSig.Text = "TSig: " + P.F.MTime.GetTSig(0).TxtExt;
        //ShowKeys();
        //LastChord = CSVFileConv.NoteMap.GetChord(0, 1);
        //picNoteMapCF.Top = picNoteMap.Top + picNoteMap.Height + 10;

        PlayableForms.CmdState_Set();
        //if (MidiPlay.Sync.indPlayActive == clsSync.ePlay.Stream) StreamPlayOn();
        //else if (MidiPlay.Sync.indPlayActive == clsSync.ePlay.SyncEnabled) SyncPlayOn();
        //else StreamPlayOff();

        if (P.F.frmTrackMap != null) {  //update mutes, solos, vol/pans, patches
          P.F.frmTrackMap.UpdateControls();
        }

        PicNMMidi = new clsPicNoteMapNM(this, picNoteMapMidi, false);
        PicNMQuant = new clsPicNoteMapNM(this, picNoteMapQuant, true);
        PicNMFile = new clsPicNoteMapNM(this, P.F.CF, picNoteMapFile);
        _Octaves = CalcOctaves(CSVFileConv, out _MinC);

        InitDGV();
        if (P.F.Lyrics.LyricsExist) {
          P.F.Lyrics.InitDGV(this);
        } else {
          dgvLyrics.Hide();
          lblLyricsLit.Hide();
        }

        PopulateCmbSnapTo();

        Bypass_Event = true;
        cmbSnapTo.SelectedItem = Cfg.frmChordMap_SnapTo;
        //cmbSnapTimes.SelectedItem = SnapTo;
        //chkSnapNote.Checked = SnapNote;
        //chkSnapTime.Checked = SnapTime;
        chkShowBeats.Checked = CfgShowBeats;
        chkShowKB.Checked = CfgShowKB;
        chkOneOct.Checked = CfgOneOctave;
        VPixPerNote = CfgVPixPerNote;
        Bypass_Event = false;

        PopulateMnuCmbKeys(mnuModHitcmbChange);
        PopulateMnuCmbKeys(mnuModMisscmbNew);

        PopulateMnuCmbTSigs(mnucmbTSigEndCommon, clsMTime.clsTSig.CommonTSigs);
        PopulateMnuCmbTSigs(mnucmbTSigEndAny, clsMTime.clsTSig.AllTSigs);

        PopulateMnuCmbTSigs(mnucmbTSigAreaCommon, clsMTime.clsTSig.CommonTSigs);
        PopulateMnuCmbTSigs(mnucmbTSigAreaAny, clsMTime.clsTSig.AllTSigs);

        PopulateMnuCmbTSigs(mnucmbTSigInsertCommon, clsMTime.clsTSig.CommonTSigs);
        PopulateMnuCmbTSigs(mnucmbTSigInsertAny, clsMTime.clsTSig.AllTSigs);

        //if (P.frmSC != null || P.F.frmTrackMap != null) RefreshBBT(P.F.CurrentBBT);
        RefreshBBT(P.F.CurrentBBT);
        Cfg.DictFormProps[Name].SetForm(this);
        VResize();
        HResize();

        cmdMultiMap.Enabled = P.F.Project.MidiExists;
        cmdSaveProject.Enabled = P.F.SaveProject(null, true, false);
        P.F.CF.UndoRedoCF = new clsCF.clsUndoRedoCF(this);
        mnuChangeLength.Enabled = !P.F.Project.MidiExists;
        //clsAudioSync.SetPlayAudioText(P.F?.AudioSync);

        nudTransposeChordNames.Value = StaticTransposeChordNamesVal;
        Form_Loaded = true;
      }
      finally {
        if (P.F.CF != null) P.F.CF.indSave = indsavesave;
      }
    }

    public DataGridView Prop_dgvLyrics { get { return dgvLyrics; } }
    public int TransposeChordNamesVal { get { return StaticTransposeChordNamesVal; } }

    //public int ChordTransposeNotesVal {
    //  get {
    //    return (int)nudTransposeChordNotes.Value;
    //  }
    //}

    //public int ChordTranspose(int pc) {
    //  if (ChordTransposeNamesVal == 0) return pc;
    //  return (pc + ChordTransposeNamesVal).Mod12();
    //}
    //public int ChordTransposeReverse(int pc) {
    //  if (ChordTransposeNamesVal == 0) return pc;
    //  return (pc - ChordTransposeNamesVal).Mod12();
    //}

    public int ChordTranspose(int pc) {
      //int diff = ChordTransposeNamesVal - ChordTransposeNotesVal;
      int diff = StaticTransposeChordNamesVal;
      if (diff == 0) return pc;
      return (pc + diff).Mod12();
    }
    public int ChordTransposeReverse(int pc) {
      //int diff = ChordTransposeNamesVal - ChordTransposeNotesVal;
      int diff = StaticTransposeChordNamesVal;
      if (diff == 0) return pc;
      return (pc - diff).Mod12();
    }

    internal int CsrPixLo {
      get {
        return _CsrPixLo;
      }
      set {
        _CsrPixLo = value;
        _CsrQILo = value / HPixPerQI;
      }
    }
    internal int CsrPixHi {
      get {
        return _CsrPixHi;
      }
      set {
        _CsrPixHi = value;
        _CsrQIHi = value / HPixPerQI;
      }
    }
    private int CsrQILo {
      get {
        return _CsrQILo;
      }
      set {
        _CsrQILo = value;
        _CsrPixLo = value * HPixPerQI;
      }
    }
    private int CsrQIHi {
      get {
        return _CsrQIHi;
      }
      set {
        _CsrQIHi = value;
        _CsrPixHi = value * HPixPerQI;
      }
    }

    internal int PixToBars(int pix) {
      int ticks = (pix * P.F.TicksPerQI) / HPixPerQI;
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
      if (bbt.TicksRemBar != 0) return -1;
      return bbt.Bar;
    }

    private clsMTime.clsBBT PixToBBTBar(int pix) {
      int ticks = (pix * P.F.TicksPerQI) / HPixPerQI;
      int bar = new clsMTime.clsBBT(ticks).Bar;
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(bar, 0, 0);
      return bbt;
    }

    private clsMTime.clsBBT PixToBBTBar(clsMTime mtime, int pix) {
      int ticks = (pix * P.F.TicksPerQI) / HPixPerQI;
      int bar = new clsMTime.clsBBT(mtime, ticks).Bar;
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(mtime, bar, 0, 0);
      return bbt;
    }

    private clsMTime.clsBBT PixToBBT(int pix) {
      int ticks = (pix * P.F.TicksPerQI) / HPixPerQI;
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
      return bbt;
    }

    private clsMTime.clsBBT PixToBBT(clsMTime mtime, int pix) {
      int ticks = (pix * P.F.TicksPerQI) / HPixPerQI;
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(mtime, ticks);
      return bbt;
    }

    private int BarsToPix(int bars) {
      int ticks = new clsMTime.clsBBT(bars, 0, 0).Ticks;
      return (ticks * HPixPerQI) / P.F.TicksPerQI;
    }

    private int BeatsToPix(clsMTime mtime, int beats) {
      int ticks = new clsMTime.clsBBT(mtime, beats, true).Ticks;
      return (ticks * HPixPerQI) / P.F.TicksPerQI;
    }

    public int TicksToPix(int ticks) {
      return (ticks * HPixPerQI) / P.F.TicksPerQI;
    }

    internal int PixToTicks(int pix) {
      return (pix * P.F.TicksPerQI) / HPixPerQI;
    }

    //internal enum eShow { All, NoMidi, Simple };
    //private eShow ShowType;

    public void FormStreamOnOff(bool on) {
      //on = (P.F.MidiFileLoaded && on); 
      panFiles.Enabled = !on;
      panSelect.Enabled = !on;
      panEdit.Enabled = !on;
      panMisc.Enabled = !on;
      //panActionsMidi.Enabled = !on;
      //panChords.Enabled = !on;

      grpTransposeNotes.Enabled = !on;
      grpTransposeDisplay.Enabled = !on;

      cmdPicQuantToFile.Enabled = !on;
      cmdChordinate.Enabled = !on;
      //cmdClearFile.Enabled = !on;
      if (on) {  //start play
        cmdUndo.Enabled = false;
        cmdRedo.Enabled = false;
      } else {  //stop play
        P.F.CF.UndoRedoCF.SetCmdStateUndoRedo();
      }
    }

    internal static string UpdateTrksSelected(clsTrks.Array<bool> trkselect) {
      if (trkselect == null) return ("All Tracks\r\nSelected");
      int selcnt = 0, nonselcnt = 0;
      foreach (bool ts in trkselect) {  //check if all, none, or some trks selected
        if (ts) selcnt++; else nonselcnt++;
      }
      return (nonselcnt == 0) ? "All Tracks\r\nSelected" : selcnt + " Tracks\r\nSelected";
    }

    //private void dgvLyrics_CurrentCellChanged(object sender, EventArgs e) {
    private void dgvLyrics_CellClick(object sender, DataGridViewCellEventArgs e) {
      if (Bypass_DGV) return;
      if (!Form_Loaded) return;
      if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.None) return;
      if (dgvLyrics.CurrentCell == null) return;
      //if (P.F.CurrentBBT.Bar == e.ColumnIndex) return;
      //clsMTime.clsBBT bbt = new clsMTime.clsBBT(dgvLyrics.CurrentCell.ColumnIndex, dgvLyrics.CurrentCell.RowIndex, 0);  //locate to bar/beat
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(e.ColumnIndex, 0, 0);  //locate to bar, first beat
      //Debug.WriteLine("frmChordMap: dgvLyrics_CellEnter:"
      //  + " CurrentBBT = " + P.F.CurrentBBT.ToStringBase0()
      //  + " Col.Row = " + e.ColumnIndex + "." + e.RowIndex);
      //if (P.F.CurrentBBT.Bar == e.ColumnIndex && P.F.CurrentBBT.Beats == e.RowIndex) return;
      Bypass_DGV = true;
      NoSelectDGV = dgvLyrics;
      P.frmSC.Play?.NewReset();
      clsIShowNoteMap.SetPlayCsr(bbt);
      RefreshAll();
      NoSelectDGV = null;
      Bypass_DGV = false;
    }

    private void DGV_CurrentCellChanged(object sender, EventArgs e) {
      if (Bypass_DGV) return;
      if (!Form_Loaded) return;
      if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.None) return;
      if (DGV.CurrentCell == null) return;
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(DGV.CurrentCell.ColumnIndex, DGV.CurrentCell.RowIndex, 0);  //locate to bar/beat
      //Debug.WriteLine("frmChordMap: DGV_CurrentCellChanged:"
      //  + " CurrentBBT = " + P.F.CurrentBBT.ToStringBase0()
      //  + " Col.Row = " + DGV.CurrentCell.ColumnIndex + "." + DGV.CurrentCell.RowIndex);
      //if (P.F.CurrentBBT.Bar == e.ColumnIndex && P.F.CurrentBBT.Beats == e.RowIndex) return;

      if (!NoRefreshDGV) {
        Bypass_DGV = true;
        NoSelectDGV = DGV;
        clsIShowNoteMap.SetPlayCsrNoSetStartBar(bbt);
        RefreshAll();
        NoSelectDGV = null;
        Bypass_DGV = false;
      }
    }

    //private bool indFocusdgvLyrics = false;
    //private void dgvLyrics_GotFocus(object sender, EventArgs e) {
    //  indFocusdgvLyrics = true;
    //}

    //private bool indFocusDGV = false;
    //private void DGV_GotFocus(object sender, EventArgs e) {
    //  indFocusDGV = true;
    //}

    //private void InitSimple() {
    //  Simple = true;
    //  panCmds.Hide();
    //  panVMidi.Hide();
    //  picNoteMapMidi.Hide();
    //  picNoteMapQuant.Hide();
    //  picNoteMapFile.Hide();
    //  lblMidiLit.Hide();
    //  lblChordsLit.Hide();

    //  cmdStop.Hide();
    //  cmdPause.Hide();
    //  cmdPlayWithMutes.Hide();
    //  cmdPlayAll.Hide();
    //  cmdPlayThis.Hide();
    //  cmdStartSync.Hide();
    //  cmdGoToStart.Hide();
    //  cmdRecordSync.Hide();
    //  cmdAudioSync.Hide();

    //  DGV.ReadOnly = true;
    //}

    internal void InitDGV() {
      DGV.DoubleBuffered(true);
      BeatChords.Init();
      DGV.ContextMenuStrip = new ContextMenuStrip();
      foreach (KeyValuePair<string, ChordAnalysis.clsTemplate> pair in ChordAnalysis.NameToTemplate) {
        DGV.ContextMenuStrip.Items.Add(pair.Key);
        DGV.ContextMenuStrip.ItemClicked += DGV_ContextMenuItemClicked;
      }
    }

    private void DGV_ContextMenuItemClicked(Object sender, ToolStripItemClickedEventArgs e) {
      string txt = e.ClickedItem.Text;
      Clipboard.SetText(txt);
      DGV.ContextMenuStrip.Close();
    }

    public void SetFormTitle() {  //interface IFormProjectName
      SetFormTitle(P.F.Project);
    }

    public void SetFormTitle(clsProject project) {  //interface IFormProjectName
      string name = project.CHPPath;  //including CHPExt
      if (name == "") name = project.PathAndName;
      Text = "CHORDMAP: " + name + " - Chord Cadenza";
    }

    //private void PopulateCmbSnapTimes() {
    //  cmbSnapTimes.Items.Add("Bar");
    //  cmbSnapTimes.Items.Add("Bar/2");
    //  cmbSnapTimes.Items.Add("Beat");
    //  cmbSnapTimes.Items.Add("Beat/2");
    //  cmbSnapTimes.Items.Add("Beat/4");
    //  //cmbSnapTimes.SelectedIndex = 0;
    //}

    private void PopulateCmbSnapTo() {
      foreach (eSnapTo snap in Enum.GetValues(typeof(eSnapTo))) {
        cmbSnapTo.Items.Add(snap);
      }
      cmbSnapTo.SelectedIndex = 0;
    }

    //public void NewCSVFileConv(clsFileStream csvfileconv) {
    //  //* to allow for a different trkselect[]
    //  //* assume midifile format and transpose unchanged
    //  CSVFileConv = csvfileconv;
    //  for (int i = 0; i < CSVFileConv.TrkSelect.Length; i++) {
    //    Mute[i] = !CSVFileConv.TrkSelect[i];
    //  }
    //  Mute.ExclCh10 = true;
    //  //MaxQTime = P.F.MaxQTime;
    //  _Octaves = CalcOctaves(CSVFileConv, out _MinC);
    //  RefreshBBT(P.F.CurrentBBT);
    //}

    internal static int CalcOctaves(clsFileStream fs, out int minc) {  //return Octaves
      //* calculate number of octaves in track (C...Bb)
      int octaves = 1;
      minc = 127;
      int octloall = 11, octhiall = 1;  //11 = 127/12
      foreach (clsTrks.T trk in fs.TrkSelect.Next) {
        if (!fs.TrkSelect[trk]) continue;
        int octlo = fs.TrkMinPitch[trk] / 12;
        int octhi = fs.TrkMaxPitch[trk] / 12;
        octloall = Math.Min(octloall, octlo);
        octhiall = Math.Max(octhiall, octhi);
        minc = Math.Min(minc, octlo * 12);
      }
      octaves = octhiall - octloall + 1;
      return octaves;
    }

    public int GetTrkHeight(int pixpernote) {
      //* <= TrkCtlHeight
      if (OneOct) return VPixPerNote * 12;
      int i = pixpernote;
      if (i == 0) {  //0 causes picTrk to deactivate paint event
        float f = GetPixPerNoteFloat() * Octaves * 12;
        return (int)f;
      }
      return i * Octaves * 12;
    }

    private void picNoteMap_Paint(object sender, PaintEventArgs e) {
      PictureBox pic = (PictureBox)sender;
      clsPicNoteMapNM picnm = GetPicNM(pic);
      //Debug.WriteLine(DateTime.Now.Millisecond + ": " + pic.Name + " Clip Rectangle = " + e.ClipRectangle);

      if ((pic == picNoteMapFile)) {
        if (P.F.CF == null) {
          picNoteMapFile.Hide();
          return;
        }
      }

      Graphics xgr = e.Graphics;

      int pixlo = e.ClipRectangle.X;
      int pixhi = e.ClipRectangle.X + e.ClipRectangle.Width;

      clsNoteMap notemap;
      if (pic == picNoteMapFile) notemap = P.F.CF.NoteMap;
      else notemap = CSVFileConv.NoteMap;
      picnm.PaintMap(notemap, null, xgr, pixlo, pixhi, P.F.MTime);
      //if (pic == picNoteMapMidi) return;

      //* draw selected area 
      //if (pic != picNoteMapMidi) DrawSelectedArea(e, pic.Height, P.ColorsNoteMap["Selected Area"].Br, false, CsrPixLo, CsrPixHi, false);
      DrawSelectedArea(e, pic.Height, P.ColorsNoteMap["Selected Area"].Br, false, CsrPixLo, CsrPixHi, false, BarFont);

      //* show marked chords
      //* may be outside cliprectangle

      Pen pencsr = new Pen(P.ColorsNoteMap["Play Cursor"].Co, 5);  //was green
      e.Graphics.DrawLine(pencsr, CsrPixPos + 1, BarFont.Height, CsrPixPos + 1, pic.Height);  //play csr
    }

    //internal static void DrawSelectedArea(PaintEventArgs e, int height, Color color, int alpha, bool dash, int csrpixlo, int csrpixhi, bool border) {
    internal static void DrawSelectedArea(PaintEventArgs e,
      int height, Brush brushselect, bool dash, int csrpixlo, int csrpixhi, bool border, Font barfont) {
      Pen penlo = new Pen(Color.Red, 3);
      Pen penhi = new Pen(Color.Blue, 3);
      int fontheight = (barfont == null) ? 0 : barfont.Height;
      int penwidth = 6;
      //Brush brushselect = new SolidBrush(Color.FromArgb(alpha, color));
      Pen penselect = new Pen(new SolidBrush(Color.Black), 6);
      if (dash) penselect.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
      //* define draw area to be inside cliprectangle (more efficient)
      int csrarealo = Math.Max(csrpixlo, e.ClipRectangle.X - 2);
      int csrareahi = Math.Min(csrpixhi, e.ClipRectangle.X + e.ClipRectangle.Width + 2);
      if (csrpixlo >= 0 && csrpixlo < csrpixhi) {  //select area (lo to hi)
        if (csrarealo < csrareahi) { //area at least partially inside clip rectangle
          e.Graphics.FillRectangle(brushselect, csrarealo - 1, fontheight, csrareahi - csrarealo, height);
          if (border) {
            e.Graphics.DrawRectangle(penselect, csrarealo - 1, fontheight, csrareahi - csrarealo - penwidth + 5, height - penwidth + 2);
          }
        }
      } else {
        //* check if csrlo/hi could at least be partially in clip rectangle
        //if (csrpixlo > e.ClipRectangle.X - 2 && csrpixlo < e.ClipRectangle.X + e.ClipRectangle.Width + 4) {  //lo csr
        //  e.Graphics.DrawLine(penlo, csrpixlo + 2, fontheight, csrpixlo + 2, height);
        //}
        if (csrpixhi != csrpixlo) {
          if (csrpixhi > e.ClipRectangle.X - 2 && csrpixhi < e.ClipRectangle.X + e.ClipRectangle.Width + 4) {  //hi csr
            e.Graphics.DrawLine(penhi, csrpixhi + 2, fontheight, csrpixhi + 2, height);
          }
        }
      }
    }

    private clsPicNoteMapNM GetPicNM(PictureBox pic) {
      clsPicNoteMapNM picnm;
      if (pic == picNoteMapMidi) picnm = PicNMMidi;
      else if (pic == picNoteMapQuant) picnm = PicNMQuant;
      else if (pic == picNoteMapFile) picnm = PicNMFile;
      else {
        LogicError.Throw(eLogicError.X041);
        picnm = null;
      }
      return picnm;
    }

    private int GetY(int note) {  //return top of note rectangle
      return (picNoteMapQuant.ClientSize.Height - (note + 1) * VPixPerNote);
    }

    private int GetNote(int y) {
      int note = (picNoteMapQuant.ClientSize.Height - y) / VPixPerNote;
      return note;
    }

    public int nudHeightVal {
      get { return MidiHeight; }
    }

    public int GetPixPerNoteInt() {
      if (OneOct) return VPixPerNote;
      //return VPixPerNote / Octaves;   //0 causes picTrk to deactivate paint event
      return VPixPerNote;   //0 causes picTrk to deactivate paint event
    }

    private float GetPixPerNoteFloat() {
      if (OneOct) return VPixPerNote;
      //return (float)VPixPerNote / (float)Octaves;   //0 causes picTrk to deactivate paint event
      return (float)VPixPerNote;   //0 causes picTrk to deactivate paint event
    }

    internal int GetQTime(int X) { //get qtime for X location
      if (X == -1) return -1;
      return X / HPixPerQI;
    }

    internal int GetX(int qtime) {
      if (qtime == -1) return -1;
      return qtime * HPixPerQI;
    }

    internal int _GetX(int qtime) {
      return qtime * HPixPerQI;
    }

    //internal void frmNoteMap_Resize(object sender, EventArgs e) {
    //if (this.ClientSize.Width <= 0) return;
    //int w = this.ClientSize.Width - panNoteMap.Left - 10;
    //if (w != panNoteMap.Width) {
    //  panNoteMap.Width = w;
    //  DGV.Width = w;
    //  dgvLyrics.Width = w;
    //}
    //RefreshAll();
    //}

    private void cmdXPos_Click(object sender, EventArgs e) {
      if (HPixPerQI.NN >= 10) return;  //smallest scale
      if (picNoteMapFile.Width > 16380) return;  //MouseClick limit approx 32768
      IntDiv oldhpixperqi = new IntDiv(HPixPerQI.NN, HPixPerQI.DD);
      HPixPerQI++;
      CalcCsrs(oldhpixperqi, HPixPerQI);
      HResize();
      SetScroll();
      Refresh();
    }

    private void cmdXNeg_Click(object sender, EventArgs e) {
      if (HPixPerQI.DD >= 4) return;  //largest scale
      IntDiv oldhpixperqi = new IntDiv(HPixPerQI.NN, HPixPerQI.DD);
      HPixPerQI--;
      CalcCsrs(oldhpixperqi, HPixPerQI);
      HResize();
      SetScroll();
      Refresh();
    }

    private void CalcCsrs(IntDiv oldhpixperqi, IntDiv newhpixperqi) {
      CsrPixLo = (CsrPixLo * newhpixperqi) / oldhpixperqi;
      CsrPixHi = (CsrPixHi * newhpixperqi) / oldhpixperqi;
    }

    private void cmdYPos_Click(object sender, EventArgs e) {
      VPixPerNote++;
      VPixPerNote = Math.Min(Math.Max(VPixPerNote, 1), 30);
      CfgVPixPerNote = VPixPerNote;
      VResize();
      ////***** test
      //for (int i = 0; i < 100; i++) {
      //  panNoteMap.ClientSize = new Size(panNoteMap.ClientSize.Width, panNoteMap.ClientSize.Height);
      //}
      ////***** test
    }

    private void cmdYNeg_Click(object sender, EventArgs e) {
      VPixPerNote--;
      if (VPixPerNote < 1) VPixPerNote = 1;
      CfgVPixPerNote = VPixPerNote;
      VResize();
    }

    //private void cmdVLessMidi_Click(object sender, EventArgs e) {
    //  MidiHeight--;
    //  if (MidiHeight < 2) MidiHeight = 0;
    //  //if (MidiHeight < 0) MidiHeight = 0;
    //  VResize();
    //}

    //private void cmdVMoreMidi_Click(object sender, EventArgs e) {
    //  MidiHeight++;
    //  if (MidiHeight < 2) MidiHeight = 2;
    //  if (MidiHeight > 8) MidiHeight = 8;
    //  VResize();
    //}

    private void chkOneOct_CheckedChanged(object sender, EventArgs e) {
      CfgOneOctave = chkOneOct.Checked;
      MidiHeight = (chkOneOct.Checked) ? 0 : 1;
      VResize();
    }

    internal void HResize() {
      BeatChords.SetColumnWidth();
      P.F.Lyrics.SetColumnWidth(this);
      PicNMMidi.SetPicSize(null);
      //picNoteMapMidi.Refresh();
      PicNMQuant.SetPicSize(null);
      //picNoteMapQuant.Refresh();
      PicNMFile.SetPicSize(null);
      //picNoteMapFile.Refresh();
      picModNotes.Width = picNoteMapFile.Width;
      picModNotes.Refresh();
      picModNames.Width = picNoteMapFile.Width;
      picModNames.Refresh();
      picTSig.Width = picNoteMapFile.Width;
      picTSig.Refresh();
      picBars.Width = picMargins.Width;
      picBars.Refresh();
      Forms.frmStart.RefreshBBT(P.F.CurrentBBT);

    }

    internal void VResize() {
      //* layout within panNoteMap
      picNoteMapQuant.Visible = (P.F.MidiFileLoaded && chkShowQuant.Checked);
      picNoteMapMidi.Visible = (P.F.MidiFileLoaded && !chkShowQuant.Checked);

      PictureBox picNoteMapTop = (chkShowQuant.Checked) ? picNoteMapQuant : picNoteMapMidi;
      PicNMMidi.SetPicSize(null);
      picNoteMapMidi.Refresh();

      picNoteMapQuant.Top = picNoteMapMidi.Top;
      PicNMQuant.SetPicSize(null);
      picNoteMapQuant.Refresh();

      if (!P.F.MidiFileLoaded) picNoteMapFile.Top = 10;
      else picNoteMapFile.Top = picNoteMapTop.Top + picNoteMapTop.Height + 10;
      PicNMFile.SetPicSize(null);
      picNoteMapFile.Refresh();

      picModNames.Width = picNoteMapFile.Width;
      picModNames.Refresh();

      picModNotes.Top = picNoteMapFile.Top + picNoteMapFile.Height + 10;
      picModNotes.Width = picNoteMapFile.Width;
      picModNotes.Refresh();

      picTSig.Top = picModNotes.Top + picModNotes.Height + 10;
      picTSig.Width = picModNotes.Width;
      picTSig.Refresh();

      //* layout associated with panNoteMap
      //panVMidi.Visible = !chkShowQuant.Checked;
      chkOneOct.Visible = !chkShowQuant.Checked;
      //panNoteMap.ClientSize = new Size(panNoteMap.ClientSize.Width, picTSig.Bottom + SystemInformation.HorizontalScrollBarHeight + 5);
      panNoteMap.Height = picTSig.Bottom + SystemInformation.HorizontalScrollBarHeight + 15;

      //* layout outside panNoteMap
      //picBars.Top = 5;
      //DGV.Top = picBars.Bottom + 10;
      //picMargins.Top = DGV.Bottom + 10;
      //if (dgvLyrics.Visible) {
      //  dgvLyrics.Top = picMargins.Bottom + 10;
      //  panNoteMap.Top = dgvLyrics.Bottom + 10;
      //} else {
      //  panNoteMap.Top = picMargins.Bottom + 10;
      //}

      picMargins.Top = 5 + panMain.AutoScrollPosition.Y;
      picBars.Top = picMargins.Bottom + 10;
      if (dgvLyrics.Visible) {
        dgvLyrics.Top = picBars.Bottom + 10;
        DGV.Top = dgvLyrics.Bottom + 10;
      } else {
        DGV.Top = picBars.Bottom + 10;
      }
      panNoteMap.Top = DGV.Bottom + 10;

      LocateLblLit(panNoteMap, picModNames, lblKeyNamesLit);
      LocateLblLit(panNoteMap, picNoteMapMidi, lblMidiLit);
      LocateLblLit(panNoteMap, picNoteMapFile, lblChordsLit);
      LocateLblLit(panNoteMap, picModNotes, lblKeyNotesLit);
      LocateLblLit(panNoteMap, picTSig, lblTSigLit);
      LocateLblLit(null, picMargins, lblMarginLit);
      LocateLblLit(null, DGV, lblChordNamesLit);
      LocateLblLit(null, dgvLyrics, lblLyricsLit);
      LocateLblLit(null, picBars, lblBarsLit);

      //this.ClientSize = new Size(ClientSize.Width, panNoteMap.Bottom + 10); 
    }

    private void LocateLblLit(Panel pan, Control ctl, Label lbllit) {
      int pantop = (pan == null) ? 0 : pan.Top;
      int ctlmiddle = pantop + (ctl.Top + ctl.Bottom) / 2;
      lbllit.Top = ctlmiddle - lbllit.Height / 2;
    }

    public void StartSub(clsMTime.clsBBT bbt) {
      //nothing to do
    }

    internal void RefreshAll() {
      //ShowKeys();
      Refresh();
    }

    public void RefreshBBT(clsMTime.clsBBT bbt) {
      //* draw line showing current position
      //Debug.WriteLine(DateTime.Now.Millisecond + " RefreshBBT bar = " + bbt.Bar);
      if (bbt.MTime != P.F.MTime) return;
      if (!Visible) return;
      P.F.CurrentBBT = bbt;
      //Debug.WriteLine("frmChordMap: RefreshBBT @ " + DateTime.Now + " bbt = " + bbt.ToString());
      int qtime = P.F.CurrentBBT.Ticks / P.F.TicksPerQI;
      LastCsrPos = CsrPixPos;
      CsrPixPos = qtime * HPixPerQI;
      CsrPixHi = CsrPixPos;  //to make selected area zero 
      //P.F.Lyrics.SelectBar(this, bbt.Bar);
      SelectBarBeat(DGV, dgvLyrics, NoSelectDGV, bbt.Bar, bbt.BeatsRemBar);
      int vismin = -panNoteMap.AutoScrollPosition.X;
      int vismax = vismin + panNoteMap.ClientSize.Width;
      if (vismax - CsrPixPos < ScrollMarginHi || CsrPixPos - vismin < ScrollMarginLo) {
        SetScroll();
      }

      int csrthis = CsrPixPos;
      int csrlast = LastCsrPos;
      if (csrthis < csrlast) { csrthis -= 2; csrlast += 2; }
      else if (csrthis > csrlast) { csrthis += 2; csrlast -= 2; }
      clsIShowNoteMap.InvalidatePic(null, picNoteMapMidi, PicNMMidi, csrthis, csrlast, BarFont.Height);
      clsIShowNoteMap.InvalidatePic(null, picNoteMapQuant, PicNMQuant, csrthis, csrlast, BarFont.Height);
      clsIShowNoteMap.InvalidatePic(null, picNoteMapFile, PicNMFile, csrthis, csrlast, BarFont.Height);

      #if ADVANCED
        lblTicks.Text = bbt.Ticks.ToString();
        lblQI.Text = (bbt.Ticks / P.F.TicksPerQI).ToString();
      #endif
    }

    private void SetScroll() {
      int maxwidth = P.F.MaxBBT.QI * HPixPerQI;
      int pos = Math.Min(Math.Max(0, CsrPixPos - ScrollMarginLo), maxwidth);
      panNoteMap.AutoScrollPosition = new Point(pos, 0);
      //Debug.WriteLine("panautoscroll: " + pos);

      DGV.HorizontalScrollingOffset = pos;
      DGV.Refresh();

      dgvLyrics.HorizontalScrollingOffset = pos;
      dgvLyrics.Refresh();
    }

    internal static void SelectBarBeat(DataGridView dgvchords, DataGridView dgvlyrics,
      DataGridView noselectdgv, int bar, int beat) {
      //* dgv is the datagridview to NOT update to avoid re-entrant error
      //if (DGV.Rows.Count == 0 || dgvLyrics.Rows.Count == 0) return;
      if (dgvchords != null && dgvchords.Rows.Count != 0 && (noselectdgv != dgvchords)) {
        if (dgvchords.Rows[beat].Cells.Count > bar) dgvchords.Rows[beat].Cells[bar].Selected = true;
      }
      if (dgvlyrics != null && dgvlyrics.Rows.Count != 0 && (noselectdgv != dgvlyrics)) {
        if (dgvlyrics.Rows[0].Cells.Count > bar) dgvlyrics.Rows[0].Cells[bar].Selected = true;
      }
    }

    //internal void ShowKeys() {
    //  clsKey key = P.F.Keys[P.F.CurrentBBT.Ticks];
    //  int keynote = ChordTranspose(key.KeyNote);
    //  lblTransposedKey.Text = NoteName.ToSharpFlat(NoteName.PitchToKeyStr(keynote, key.Scale)) + " " + key.Scale;
    //}

    private void chkShowBeats_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      CfgShowBeats = chkShowBeats.Checked;
      RefreshAll();
    }

    private void chkShowKB_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      CfgShowKB = chkShowKB.Checked;
      RefreshAll();
    }

    internal static int GetnudValLog2(int val) {
      //assumes nud range = -10 to 26 increment 10
      switch (val) {
        //* UP
        case 10: return 0;   // 0 ->  0
        case 26: return 0;   //16 -> 0
        case 18: return 16;  // 8 -> 16
        case 14: return 8;   // 4 ->  8
        case 12: return 4;   // 2 ->  4
        case 11: return 2;   // 1 ->  2

        //* DOWN
        case -10: return 16; // 0 -> 16
        case 6: return 8;    //16 ->  8
        case -2: return 4;   // 8 ->  4
        case -6: return 2;   // 4 ->  2
        case -8: return 1;   // 2 ->  1
        case -9: return 1;   // 1 ->  1

        default: return 0;  //should not happen
      }
    }

    private void optChordAlign_CheckedChanged(object sender, EventArgs e) {
      RadioButton opt = (RadioButton)sender;
      if (!opt.Checked) return;
      SetoptChords();
    }

    private void SetoptChords() {
      if (chkOptChordsNone.Checked) AlignChords = eAlign.None;  //advanced
      else if (optChordsBar.Checked) AlignChords = eAlign.Bar;
      else if (optChordsHalfBar.Checked) AlignChords = eAlign.HalfBar;
      else if (optChordsBeat.Checked) AlignChords = eAlign.Beat;
      //else if (optChordsNone.Checked) AlignChords = eAlign.None;
      else if (optChordsAuto.Checked) AlignChords = eAlign.Auto;
      else {
        LogicError.Throw(eLogicError.X042);
        AlignChords = eAlign.HalfBar;
      }
    }

    internal void ApplyFilter(eAlign alignchords, bool setatts, out clsChordSegs chordsegs) {
      chordsegs = null;
      int qilo = CsrQILo;
      int qihi = CsrQIHi;
      Forms.frmChordMap frmchordmap = this;
      if (qilo >= qihi) {  //default to select all
        qilo = 0;
        qihi = P.F.MaxBBT.QI - 1;
        frmchordmap = null;
      }

      //qihi = Math.Min(CSVFileConv.NoteMap.FilterLen - 1, qihi);
      qihi = Math.Min(qihi, P.F.MaxBBT.QI - 1);
      CSVFileConv.NoteMap.InitFilter(qilo, qihi);

      int qicnt = QuantizeTicks / P.F.TicksPerQI;
      CSVFileConv.NoteMap.ApplyTrim(AlignTrim, qilo, qihi, qicnt, TrimInnerQI, TrimOuterQI);

      if (P.F.frmChordMapAdv != null) {
        qicnt = CloseGapTicks / P.F.TicksPerQI;
        CSVFileConv.NoteMap.ApplyGapFilter(qicnt, qilo, qihi);

        qicnt = NoteFilterTicks / P.F.TicksPerQI;
        CSVFileConv.NoteMap.ApplyNoteFilter(qicnt, qilo, qihi);

        qicnt = MinLenTicks / P.F.TicksPerQI;
        CSVFileConv.NoteMap.ApplyMinLenFilter(qicnt, qilo, qihi);

        qicnt = QuantizeTicks / P.F.TicksPerQI;
        CSVFileConv.NoteMap.FillSegments(Align, qilo, qihi, (int)P.F.frmChordMapAdv.nudFillPercent.Value, qicnt);
      }

      if (alignchords != eAlign.None) chordsegs = new clsChordSegs(CSVFileConv, frmchordmap, alignchords, setatts);

      if (setatts) {
        SetNoteMapFileChanged();
        //CreateChordComment();
      }
    }

    internal static void ApplyFilterStatic(eAlign alignchords, clsFileStream filestream,
      out clsChordSegs chordsegs) {
      //* called from frmMultiMap
      //* using default settings
      chordsegs = null;
      int qilo = 0;
      int qihi = P.F.MaxBBT.QI;

      //qihi = Math.Min(filestream.NoteMap.FilterLen - 1, qihi);
      qihi = Math.Min(qihi, P.F.MaxBBT.QI - 1);
      filestream.NoteMap.InitFilter(qilo, qihi);

      int qicnt = 0;
      int triminnerqi = P.F.MTime.DD2DI((int)P.frmCfgChords.nudTrimInnerNN.Value, (int)P.frmCfgChords.nudTrimInnerDD.Value);
      int trimouterqi = P.F.MTime.DD2DI((int)P.frmCfgChords.nudTrimOuterNN.Value, (int)P.frmCfgChords.nudTrimOuterDD.Value);

      filestream.NoteMap.ApplyTrim(eAlign.Bar, qilo, qihi, qicnt, triminnerqi, trimouterqi);

      if (alignchords != eAlign.None) chordsegs = new clsChordSegs(filestream, null, alignchords, true);

      P.F.CF.CreateEvs();
      P.F.frmChordMap?.RefreshDGV();
      P.frmSC.ReInitPlayMode();  //to allow for transposition, ...
      if (P.F.frmTonnetz != null) P.F.frmTonnetz.Refresh();
      P.frmSC.Refresh();
      P.F.CF.indSave = true;
      P.F.frmChordMap?.Refresh();
    }

    //private void cmdShowChords_Click(object sender, EventArgs e) {
    //  ShowChords();
    //}

    //internal static void ShowChords() {
    //  //if (showmidi) {
    //  //  P.F.CFNotes = new clsCFPitch(false);
    //  //  P.F.CFNotes.CreateEvsFromMidi();
    //  //}
    //  P.frmSC.Show();
    //  //P.frmSC.chkShowTracks.Checked = true;  //false by default
    //  //P.frmSC.Text = "PlayMap: " + P.F.MidiFilePath;
    //  P.frmSC.chkShowChordNames.Checked = false;
    //  //P.frmSC.cmdMultiMap.Enabled = true;
    //  P.frmSC.cmdStartPlay.Enabled = (P.F.MidiFileLoaded);
    //  P.frmSC.cmdStartSync.Enabled = (P.F.AutoSync != null);
    //  P.frmSC.cmdWipeAndRecord.Enabled = (P.F.MidiFileLoaded);
    //  P.frmSC.cmdRecord.Enabled = (P.F.MidiFileLoaded);
    //  P.frmSC.Refresh();
    //}

    //private void cmdSaveChordFileAs_Click(object sender, EventArgs e) {
    //  Cursor.Current = Cursors.WaitCursor;
    //  string ret = P.frmSC.SaveChordFile();
    //  if (ret != "") MessageBox.Show("ChordFile not saved: " + ret);
    //  Cursor.Current = Cursors.Default;
    //}

    //internal static string SaveTxtFile() {  //return error msg or ""
    //  if (P.F == null || P.F.CF == null) return "No Chord Data to save";
    //  P.F.CF.CreateEvs();

    //  try {
    //    if (!P.F.CF.SaveTxtFile()) return "Error saving ChordFile";
    //  }
    //  catch (Exception exc) {
    //    return exc.Message;
    //  }

    //  if (P.frmStart.chkSaveMidiFile.Checked) {
    //    clsSaveMidiFile savemidifile = new clsSaveMidiFile(P.F.FSTrackMap);
    //    string midifilepath = Path.GetDirectoryName(P.F.CHPFilePath) + "\\"
    //      + Path.GetFileNameWithoutExtension(P.F.CHPFilePath) + ".Chords.mid";
    //    bool res = savemidifile.Save(midifilepath);
    //    if (!res) MessageBox.Show("MidiFile not saved");
    //  }

    //  P.frmStart.lblChpLoad.Text = "saved";
    //  return "";
    //}

    //private void CheckSaveFile(FormClosingEventArgs e) {
    //  if (P.F.CF.indChanged) {
    //    DialogResult res = MessageBox.Show(this, "Chords have unsaved changes - Save?",
    //      "Unsaved Changes Warning",
    //      MessageBoxButtons.YesNoCancel);
    //    switch (res) {
    //      case DialogResult.No:
    //        e.Cancel = false;
    //        break;
    //      case DialogResult.Yes:
    //        string ret = P.frmSC.SaveChordFile();
    //        if (ret != "") {
    //          string msg = "Chord File not saved: " + ret;
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
    //  P.F.CF.indChanged = false;
    //}

    //internal static string GetTxtFileNameFromSFD(SaveFileDialog sfdCHP) {
    //  //***************** test code start
    //  //return P.F.LoadCSV.FileName.Substring(0, P.F.LoadCSV.FileName.Length - 4) + ".test.txt";
    //  //***************** test code end

    //  string dir = (P.F.ProjectPathAndName == "") ? Cfg.ProjectDir : Path.GetDirectoryName(P.F.ProjectPathAndName);
    //  if (!Directory.Exists(dir)) dir = Cfg.UserMusicPath;  //should always exist
    //  //*throw new ApplicationException("Testing");

    //  //* set up dialog
    //  sfdCHP.InitialDirectory = dir;
    //  string txtfilename = Path.GetFileName(P.F.CHPFilePath);
    //  if (txtfilename == "") {
    //    if (!P.F.MidiFileLoaded) {
    //      txtfilename = "New.chp";
    //    } else {
    //      txtfilename = Path.GetFileName(P.F.Project.MidiPath);
    //      txtfilename = txtfilename.Substring(0, txtfilename.Length - 4) + ".chp";
    //    }
    //  }
    //  sfdCHP.FileName = txtfilename;
    //  sfdCHP.Filter = "ChordFiles|*.chp";
    //  sfdCHP.FilterIndex = 1;
    //  sfdCHP.RestoreDirectory = false;
    //  sfdCHP.Title = "Save ChordFile";

    //  //* run dialog
    //  if (sfdCHP.ShowDialog() != DialogResult.OK) return "***";

    //  //* process result
    //  if (sfdCHP.FileName == null) return "";
    //  return sfdCHP.FileName;
    //}

    private void cmdPausePlay_Click(object sender, EventArgs e) {
      //CSVFileConv.StopPlay();
      MidiPlay.Sync.Pause();
    }

    public Button[] Cmds_Locate { get { return new Button[] { cmdGoToStart }; } }
    //public Button[] Cmds_Play { get { return new Button[0]; } }
    public Button[] Cmds_Play_Midi { get { return new Button[] { cmdPlayMidiThis, cmdPlayMidi }; } }
    public Button[] Cmds_Play_And_Sync { get { return new Button[] { cmdPlayAndRecordAudio }; } }
    public Button[] Cmds_Play_Audio { get { return new Button[] { cmdPlayAudio }; } }
    public Button[] Cmds_Sync_Audio { get { return new Button[] { cmdSyncAudio }; } }
    public Button[] Cmds_Stop { get { return new Button[] { cmdStopPlay }; } }
    public Button[] Cmds_Pause { get { return new Button[] { cmdPausePlay }; } }

    private void frmNoteMap_FormClosed(object sender, FormClosedEventArgs e) {
      //P.F.CFtxt = null;
      //P.F.CF.UndoRedoCF.ClearStacks();
      //P.frmCfgChords.Hide();
      P.F.CF.UndoRedoCF = null;
      P.F.frmChordMap = null;
      //SaveStatic();
    }

    private void frmNoteMap_FormClosing(object sender, FormClosingEventArgs e) {
      Cfg.DictFormProps[Name] = new clsFormProps(this);
      //if (P.F.CloseFormsUnconditional) return;
      //Forms.frmTrackMap.CheckSaveFile(this, e, "ChordFile", ref P.F.CF.indChanged,
      //  new Forms.frmTrackMap.delegSave(P.frmSC.SaveChordFile));
    }

    private void cmdGoToStart_Click(object sender, EventArgs e) {
      //P.F.CurrentBBT = new clsMTime.clsBBT(0);
      //RefreshBBT(new clsMTime.clsBBT(0));
      P.frmSC.Play?.NewReset();
      Forms.frmStart.RefreshBBT(new clsMTime.clsBBT(0));
    }

    private void cmdPlayMidiThis_Click(object sender, EventArgs e) {
      //* play trks selected for NoteMap (excl chan10)
      MidiPlay.Sync.StartPlay(this, CSVFileConv, Mute);
    }

    private void cmdPlayMidi_Click(object sender, EventArgs e) {
      //* play trks selected on frmMultiPlay
      MidiPlay.Sync.StartPlay(this, CSVFileConv, P.F.Mute);
    }

    //private void cmdPlayMidiAll_Click(object sender, EventArgs e) {
    //  //* play all trks
    //  MidiPlay.Sync.StartPlay(this, CSVFileConv, new clsMute());
    //}

    private void cmdStopPlay_Click(object sender, EventArgs e) {
      if (cmdStopPlay.BackColor == Color.Red) {
        Forms.frmSC.SyncBarActive(false);  //-> all playable forms
      } else {
        MidiPlay.Sync.Stop();
      }
    }

    //private Point Temp_MousePointLo, Temp_MousePointHi;
    private void picNoteMapFile_MouseClick(object sender, MouseEventArgs e) {
      if (!P.frmSC.IsPlayClickable()) return;
      if (e.X >= TicksToPix(P.F.MaxBBT.Ticks)) return;
      //cmdSelectNone.Select();  //default
      //if (!Header_MouseClick(sender, e, P.F.CF.NoteMap)) {
      if (Control.ModifierKeys == Keys.Shift) {  //shift only
        SetCsrHiLo(e);
      } else if (e.Button == MouseButtons.Right) {  //update
        int n = GetNote(e.Y);
        int q = e.X / HPixPerQI;
        //int qilo, qihi;
        if (UpdateNote(!P.F.CF.NoteMap[q, n], q, n)) {
          SetNoteMapFileChanged(undoredo: true, indqi: true);
        }
      } else {  //locate (left mouse button)
        clsIShowNoteMap.SetPlayCsr((e.X * P.F.TicksPerQI) / HPixPerQI);
      }
      RefreshAll();
    }

    //private bool Header_MouseClick(object sender, MouseEventArgs e, IQNEle map) {
    //  if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.None) return false;
    //  PictureBox pic = (PictureBox)sender;
    //  clsPicNoteMapNM picnm = GetPicNM(pic);
    //  if (e.Y > picnm.BarFont.Height) return false; 
    //  SetCsrHiLo(e);
    //  return true;
    //}

    private void SetCsrHiLo(MouseEventArgs e) {
      int ticks = (e.X * P.F.TicksPerQI) / HPixPerQI;
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
      if (e.Button == MouseButtons.Left) {
        bbt.RoundDownToBar();
        CsrPixLo = (bbt.Ticks * HPixPerQI) / P.F.TicksPerQI;
        //Forms.frmStart.RefreshBBT(bbt);
        //P.F.StartBar = bbt.Bar;
      } else {
        bbt.RoundUpToBar();
        if (bbt.Ticks > P.F.MaxBBT.Ticks) bbt = P.F.MaxBBT.BBTCopy;
        CsrPixHi = (bbt.Ticks * HPixPerQI) / P.F.TicksPerQI;
      }
    }

    //private int GetNextEvent(int qi, IQNEle map) {
    //  //return qi of nearest (next) event (bar/beat/note change) to qi
    //  for (int q = 0; q < 999; q++) {
    //    int qpos = qi + q;
    //    if (qpos < P.F.MaxBBT.QI && IsAligned(qpos, map)) return qpos;
    //    //qpos = qi - q;
    //    //if (qpos >= 0 && IsAligned(qpos, map)) return qpos;
    //  }
    //  return qi;
    //}

    //private int GetPrevEvent(int qi, IQNEle map) {
    //  //return qi of nearest (prev) event (bar/beat/note change) to qi
    //  for (int q = 0; q < 999; q++) {
    //    int qpos = qi - q;
    //    if (qpos == 0 || (qpos > 0 && IsAligned(qpos, map))) return qpos;
    //  }
    //  return qi;
    //}

    //private bool IsAligned(int qpos, IQNEle map) {
    //  return (IsAlignedToTime(qpos) || IsAlignedToNote(map, qpos));
    //}

    //private bool IsAlignedToNote(IQNEle map, int qpos) {  //on->off or off->on...
    //  if (!chkSnapNote.Checked) return false;
    //  if (qpos > 0 && !map[qpos].SequenceEqual(map[qpos - 1])) {
    //    //for (int n = 0; n < 12; n++) {
    //    //  if (map[qpos, n] && !map[qpos - 1, n]) return true;
    //    //}
    //    return true;
    //  }
    //  return false;
    //}

    //private bool NoteChange(bool allnotes, bool[] csrmap, int q, int n) {
    //  if (!chkSnapNote.Checked) return false;
    //  if ((eSnapTo)cmbSnapTo.SelectedItem != eSnapTo.Note) return false;
    //  if (allnotes) return !P.F.CF.NoteMap[q].SequenceEqual(csrmap);  //all notes (bool[])
    //  else return P.F.CF.NoteMap[q, n] != csrmap[n];   //this note
    //}

    //private bool NoteChange(bool allnotes, bool[] csrmap, ushort csrushort, int q, int n) {
    //  if ((eSnapTo)cmbSnapTo.SelectedItem != eSnapTo.Note) return false;
    //  if (allnotes) return (P.F.CF.NoteMap.GetMap(q) != csrushort);  //all notes 
    //  else return (P.F.CF.NoteMap[q, n] != csrmap[n]);   //this note (bool)
    //}

    //private bool IsAlignedToTime(int q) {
    //  //* return true if aligned to bar, beat, or beat/n
    //  if (!chkSnapTime.Checked) return false;
    //  clsMTime.clsBBT bbt = new clsMTime.clsBBT(q * P.F.TicksPerQI);
    //  switch ((string)cmbSnapTimes.SelectedItem) {
    //    case "Bar":
    //      return (bbt.TicksRemBar == 0);
    //    case "Bar/2":
    //      if (bbt.TSig.NN % 2 > 0) return (bbt.TicksRemBar == 0);  //odd tsig - whole bar
    //      if (bbt.TicksRemBeat > 0) return false;
    //      if (bbt.BeatsRemBar == 0 || bbt.BeatsRemBar == bbt.TSig.NN / 2) return true;
    //      return false;
    //    case "Beat":
    //      return (bbt.TicksRemBeat == 0);
    //    case "Beat/2":
    //      return IsAlignedToBeatDiv(bbt, 2);
    //    case "Beat/4":
    //      return IsAlignedToBeatDiv(bbt, 4);
    //    default:
    //      LogicError.Throw(eLogicError.X037);
    //      return (bbt.TicksRemBar == 0);
    //  }
    //}

    //private bool IsAlignedToBeatDiv(clsMTime.clsBBT bbt, int div) {
    //  if (bbt.TicksRemBeat == 0) return true;
    //  int rem;
    //  int qirembeat = Math.DivRem(bbt.TicksRemBeat, P.F.TicksPerQI, out rem);
    //  if (rem != 0) {
    //    LogicError.Throw(eLogicError.X038);
    //    return false;
    //  }
    //  int qiperbeat = P.F.QIPerNote / bbt.TSig.DD;
    //  int qialign = Math.DivRem(qiperbeat, div, out rem);
    //  if (rem != 0) {
    //    LogicError.Throw(eLogicError.X039);
    //    return false;
    //  }
    //  for (int i = 1; i < div; i++) {
    //    if (qirembeat == qialign * i) return true;  //exact alignment 
    //  }
    //  return false;
    //}

    private bool NoteChange(bool[] csrmap, ushort csrushort, int q, int n) {
      //* return true if note at q is different to csrmap/csrushort
      return (P.F.CF.NoteMap.GetMap(q) != csrushort);  //all notes 
    }

    private bool IsAlignedToTime(int q) {
      //* return true if aligned to bar, beat, or beat/n
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(q * P.F.TicksPerQI);
      switch ((eSnapTo)cmbSnapTo.SelectedItem) {
        case eSnapTo.Bar:
          return (bbt.TicksRemBar == 0);
        case eSnapTo.HalfBar:
          if (bbt.TSig.NN % 2 > 0) return (bbt.TicksRemBar == 0);  //odd tsig - whole bar
          if (bbt.TicksRemBeat > 0) return false;
          if (bbt.BeatsRemBar == 0 || bbt.BeatsRemBar == bbt.TSig.NN / 2) return true;
          return false;
        case eSnapTo.Beat:
          return (bbt.TicksRemBeat == 0);
        default:
          LogicError.Throw(eLogicError.X037);
          return (bbt.TicksRemBar == 0);
      }
    }

    private bool UpdateNote(bool add, int qi, int note) {
      //* add or delete note in range that note(s) stay the same
      //* don't go past csr hi or lo
      //qilo = -1;
      //qihi = -1;

      //* check that we are not updating all qi's
      //if (!chkSnapTime.Checked && !chkSnapNote.Checked && (qi < CsrQILo || qi > CsrQIHi)) return false;
      if ((eSnapTo)cmbSnapTo.SelectedItem == eSnapTo.Selected) {
        if (qi < CsrQILo || qi > CsrQIHi) return false;
      }

      bool[] csrmap = P.F.CF.NoteMap[qi];  //mouseclick position
      ushort csrushort = P.F.CF.NoteMap.GetMap(qi);
      int q;

      //* update before csr
      for (q = qi; q >= 0; q--) {  //reverse
        if ((eSnapTo)cmbSnapTo.SelectedItem == eSnapTo.Notes) {
          if (NoteChange(csrmap, csrushort, q, note)) break;
          P.F.CF.NoteMap.SetChordAndAtts(q, note, add);
        } else if ((eSnapTo)cmbSnapTo.SelectedItem == eSnapTo.Selected) {
          P.F.CF.NoteMap.SetChordAndAtts(q, note, add);
          if (q == CsrQILo || q == CsrQIHi) break;
        } else {  //bar, bar/2, beat
          P.F.CF.NoteMap.SetChordAndAtts(q, note, add);
          if (IsAlignedToTime(q)) break;
        }
      }
      //qilo = q;

      //* update after csr
      for (q = qi + 1; q < P.F.MaxBBT.QI; q++) {  //forward
        if ((eSnapTo)cmbSnapTo.SelectedItem == eSnapTo.Notes) {
          if (NoteChange(csrmap, csrushort, q, note)) break;
        } else if ((eSnapTo)cmbSnapTo.SelectedItem == eSnapTo.Selected) {
          if (q == CsrQILo || q == CsrQIHi) break;
        } else {
          if (IsAlignedToTime(q)) break;
        }
        P.F.CF.NoteMap.SetChordAndAtts(q, note, add);
      }
      //qihi = q;  //qi after area updated (like csrtxthi)

      return true;
    }

    internal void RefreshDGV() {
      BeatChords.ShowDGVCells();
      picNoteMapFile.Refresh();
      picModNames.Refresh();
      picModNotes.Refresh();
      picTSig.Refresh();
    }

    internal void SetNoteMapFileChanged() {
      P.F.CF.SetNoteMapFileChanged(undoredo: true, indqi: true);
    }

    public void SetNoteMapFileChanged(bool undoredo, bool indqi) {
      P.F.CF.SetNoteMapFileChanged(undoredo, indqi);
    }

    private static int BarToQI(int bar) {
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(bar, 0, 0);
      return bbt.Ticks / P.F.TicksPerQI;
    }

    private void CopyNoteMapQuantToFile() {
      //* advanced only
      //* CSVFileConv.NoteMap.EleW[q, n] to P.F.CFtxt.NoteMap[q, n]
      clsNoteMapCF dest = P.F.CF.NoteMap;
      clsNoteMapMidi.clsW src = CSVFileConv.NoteMap.EleW;
      //if (!(P.F.CF is clsCF)) throw new LogicException();
      int qilo = CsrQILo;
      int qihi = CsrQIHi;
      //qihi = Math.Min(qihi, CSVFileConv.NoteMap.GetLengthQTime());
      //qihi = Math.Min(qihi, dest.GetLengthQTime());
      qihi = Math.Min(qihi, P.F.MaxBBT.QI);
      //P.F.CF.UndoRedoCF.StartEvs();
      for (int q = qilo; q < qihi; q++) {
        if (P.F.frmChordMap.optCopyToEmpty.Checked) {
          if (!dest[q].SequenceEqual(NoteMap_AllNotesOff)) continue;
        }
        dest.NullifyChordinateStatus(q);  //only if NoteMapCF

        bool[] chord = new bool[12];
        for (int n = 0; n < 12; n++) {
          if (P.F.frmChordMap.optMerge.Checked) chord[n] = src[q, n] || dest[q, n];
          else chord[n] = src[q, n];
        }
        //dest.SetChord(q, chord);

        //get chord the old way (matches only)
        dest.SetChordAndAtts(q, chord);
      }
    }

    private void cmdSelectAll_Click(object sender, EventArgs e) {
      CsrPixLo = 0;
      CsrPixHi = P.F.MaxBBT.QI * HPixPerQI;
      RefreshAll();
    }

    private void cmdSelectNone_Click(object sender, EventArgs e) {
      CsrPixLo = 0;
      CsrPixHi = 0;
      RefreshAll();
    }

    private void cmdUndo_Click(object sender, EventArgs e) {
      //int? minqi, maxqi;
      using (new clsWaitCursor()) {
        P.F.CF.UndoMap();
      }
      //if (!minqi.HasValue) return;  //nothing changed
      //SetNoteMapFileChanged(false, false, minqi.Value, maxqi.Value);
      //picNoteMapFile.Refresh();
      //ShowChSymUndoRedo(minqi, maxqi);
    }

    private void cmdRedo_Click(object sender, EventArgs e) {
      //int? minqi, maxqi;
      using (new clsWaitCursor()) {
        P.F.CF.RedoMap();
      }
      //if (!minqi.HasValue) return;  //nothing changed
      //SetNoteMapFileChanged(false, false, minqi.Value, maxqi.Value);
      //picNoteMapFile.Refresh();
      //ShowChSymUndoRedo(minqi, maxqi);
    }

    internal void panNoteMap_Scroll(object sender, ScrollEventArgs e) {
      //Debug.WriteLine("panNoteMap_Scroll: " + e.NewValue);
      //if (++Scroll_Count % 4 != 0) return;
      if (NoScroll) {
        e.NewValue = e.OldValue;
        return;
      }
      ScrollFromPan = true;
      if (!ScrollFromDGV && !ScrollFromdgvLyrics) {
        DGV.HorizontalScrollingOffset = e.NewValue;
        //DGV.Refresh();
        dgvLyrics.HorizontalScrollingOffset = e.NewValue;
        //dgvLyrics.Refresh();
      }
      RefreshAll();
      //panNoteMap.Refresh();
      //picBarsBottom.Refresh();
      ScrollFromPan = false;
    }

    private void cmdPicQuantToFile_Click(object sender, EventArgs e) {
      //* advanced only
      CopyNoteMapQuantToFile();
      SetNoteMapFileChanged();
    }

    //private void cmdClearFile_Click(object sender, EventArgs e) {
    //  //* create null from CsrPosTxtLo to CsrPosTxtHi
    //  if (!(P.F.CF is clsCFPC)) return;
    //  if (CsrPixHi <= CsrPixLo) {
    //    MessageBox.Show("Selection Area is null - nothing selected!");
    //    return;
    //  }
    //  using (new clsWaitCursor()) {
    //    for (int qi = CsrQILo; qi < CsrQIHi; qi++) {
    //      P.F.CF.NoteMap.RemoveNotes(qi);
    //    }
    //    SetNoteMapFileChanged();
    //  }
    //}

    /*
        internal class clsChordInfo {
          internal ChordAnalysis.clsScore Score;
          internal clsMTime.clsSegBarBeat Segment;
          internal string ChordName;
          internal List<int> ChNotes;
          internal bool Mark;

          internal bool Equiv(clsChordInfo ci) {
            if (ci == null) return false;
            if (ChNotes == null) return (ci.ChNotes == null);
            if (ci.ChNotes == null) return (ChNotes == null);
            return (ChordName == ci.ChordName && ChNotes.SequenceEqual(ci.ChNotes));
          }

          internal string ScoreToString() {
            if (Score == null) return "null";
            return Score.Score.ToString();
          }
        }

        internal void GetDiffs(out int bardiff, out int halfbardiff) {
          if (ListCIArr == null || ListCIArr.Length < 2) SetAlignArrays();
          List<clsChordInfo> listci = SelectAlign();
          bardiff = listci.Count - ListCIBar.Count;
          halfbardiff = ListCIHalfBar.Count - listci.Count;
        }

        private List<clsChordInfo>[] ListCIArr = null;
        private List<clsChordInfo> ListCIBar, ListCIHalfBar;
        private void ApplyChords() {
          if (AlignChords == eAlign.None) return;
          //* set up align arrays
          SetAlignArrays();

          //* select align
          List<clsChordInfo> listci = SelectAlign();

          //* set chords attributes
          SetChordAtts(listci);
        }

        private void SetAlignArrays() {
          eAlign[] arralign = new eAlign[] { AlignChords };
          ListCIArr = new List<clsChordInfo>[1];  //chordlist for bar only
          if (AlignChords == eAlign.Auto) {  //try bar, half-bar, beat
            arralign = new eAlign[] { eAlign.Bar, eAlign.HalfBar };
            ListCIArr = new List<clsChordInfo>[2];  //chordlists for bar & halfbar
          }
          for (int i = 0; i < arralign.Length; i++) {
            ListCIArr[i] = GetChordScores(@"D:\1\Sonar\Debug\DumpChords[" + arralign[i] + "]", arralign[i]);
          }
        }

        private List<clsChordInfo> SelectAlign() {
          //* create listci from ListCIArr[0/1]
          List<clsChordInfo> listci = new List<clsChordInfo>(250);  //final chord list
          if (AlignChords == eAlign.Auto) {
            //* create listci from listciarr[0/1]
            ListCIBar = ListCIArr[0];
            ListCIHalfBar = ListCIArr[1];
            int j = 0;  //index to halfbar segments list
            for (int i = 0; i < ListCIBar.Count; i++) {  //i = index to bar segments list
              List<clsChordInfo> listcihalfbarinbar = new List<clsChordInfo>(4);
              clsChordInfo cibar = ListCIBar[i];
              for (; j < ListCIHalfBar.Count; j++) {
                clsChordInfo cihalfbar = ListCIHalfBar[j];
                if (cihalfbar.Segment.SegQILo > cibar.Segment.SegQIHi) break;
                listcihalfbarinbar.Add(cihalfbar);
              }
              //* decide on bar/halfbar (listcihalfbarinbar:cibar -> listci) 
              clsChordInfo[] ciauto = SelectAuto(cibar, listcihalfbarinbar.ToArray());
              listci.AddRange(ciauto);
            }
          } else {
            listci = ListCIArr[0];
          }
          if (P.frmCfgChords.IsHandleCreated) {

          }
          return listci;
        }

        private clsChordInfo[] SelectAuto(clsChordInfo cibar, clsChordInfo[] cihalfbarinbararr) {
          clsChordInfo[] cibararr = new clsChordInfo[] { cibar };  //one element

          //* debug scores
          Debug.Write("Bar: " + new clsMTime.clsBBT(cibar.Segment.SegQILo * P.F.TicksPerQI).Bar);
          Debug.Write(" bar score = " + cibar.ScoreToString());
          Debug.Write(" halfbar scores = " + cihalfbarinbararr[0].ScoreToString());
          Debug.WriteLine(" " + cihalfbarinbararr[1].ScoreToString());

          //* check if halfbar chords are same as bar chord
          bool diff = false;
          foreach (clsChordInfo ci in cihalfbarinbararr) {
            if (!ci.Equiv(cibar)) { diff = true; break; }
          }
          if (!diff) return cibararr;  //all listcihalfbarinbar same as cibar

          //* check scores
          if (cibar.Score == null) return cibararr;
          foreach (clsChordInfo ci in cihalfbarinbararr) {
            if (ci.Score == null) return cibararr;
            if (ci.Score.Score - cibar.Score.Score < (int)P.frmCfgChords.trkSegment.Value) return cibararr;
          }
          return cihalfbarinbararr;
        }

        private static void SetChordAtts(List<clsChordInfo> listci) {
          foreach (clsChordInfo ci in listci) {
            if (ci.Score != null) {
              bool[] chord = new bool[12];
              for (int n = 0; n < ci.ChNotes.Count; n++) chord[ci.ChNotes[n]] = true;
              int root = ci.Score.Root;
              sbyte qualifier = (sbyte)(ci.Score.TIndex + 1);
              ChordAnalysis.eStatus status = ChordAnalysis.eStatus.Match; //?
              clsNoteMapCF.sChordAtt att = new clsNoteMapCF.sChordAtt((sbyte)root, qualifier, status, ci.Mark);
              for (int q = ci.Segment.SegQILo; q <= ci.Segment.SegQIHi; q++) {
                P.F.CF.NoteMap.SetChordAndAtts(q, chord, att);
              }
            } else {  //null score
              //* leave alone?
            }
          }
        }
    */

    internal static List<clsChordInfo> GetChordScores(
      clsFileStream filestream, Forms.frmChordMap frmnotemap, int csrqilo, int csrqihi, string name, eAlign align) {
      //* get scores for all segments
      if (frmnotemap != null && csrqilo == -1 && csrqihi == -1) {
        csrqilo = P.F.frmChordMap.CsrQILo;
        csrqihi = P.F.frmChordMap.CsrQIHi;
      } else {
        csrqilo = 0;
        csrqihi = P.F.MaxBBT.QI - 1;
      }
      //ChordAnalysis.SetParams((int)P.frmStart.nudMaxChordSize.Value, (int)P.frmCfgChords.nudMaxChordType.Value);
      ChordAnalysis.SetParams((int)P.frmStart.nudMaxChordSize.Value, Forms.frmChordRanks.MaxRankMatch);
      #if DEBUG
        ChordAnalysis.OpenDumpChords(name, true);
      #endif
      clsMTime.clsSegBarBeat startseg = (clsMTime.clsSegBarBeat)clsNoteMapMidi.GetStartSeg(
        align, csrqilo, csrqihi, 0, P.F.TicksPerQI);
      List<clsChordInfo> listchordinfo = new List<clsChordInfo>(250);
      for (clsMTime.clsSegment seg = startseg; !seg.OOR; seg++) {
        clsMTime.clsSegBarBeat segment = ((clsMTime.clsSegBarBeat)seg).Copy();
        clsChordInfo chordinfo = new clsChordInfo(align);
        chordinfo.Segment = segment;
        clsKeyTicks key = P.F.Keys[segment.SegQILo * P.F.TicksPerQI];
        int[] percent = filestream.NoteMap.GetFillPercent(filestream, segment, false);
        if (GetIntArrayNonZeroCount(percent) < 2) {  //less than 2 notes in segment
          chordinfo.Score = null;
        } else {
          int[] basspercent = filestream.NoteMap.GetFillPercent(filestream, segment, true);
          chordinfo.Score = ChordAnalysis.GetTopChordSeg(percent, basspercent, key, segment,
            out chordinfo.ChordName, out chordinfo.ChNotes);
        }
        listchordinfo.Add(chordinfo);
      }
      #if DEBUG
        ChordAnalysis.CloseDumpChords();
      #endif
      return listchordinfo;
    }

    private static int GetIntArrayNonZeroCount(int[] array) {
      int ret = 0;
      foreach (int val in array) {
        if (val != 0) ret++;
      }
      return ret;
    }

    /*
    private void CreateChordComment() {
      if (chkComments.Checked && CsrPixLo == 0 && CsrPixHi == P.F.MaxQTime * HPixPerQI) {
        string txt = "*Chords created on " + DateTime.Now + " using ";
        if (chkTrimPost.Checked || chkTrimPost.Checked) {
          txt += "Inner:" + (int)nudTrimInnerNN.Value + "/" + (int)nudTrimInnerDD.Value;
          txt += ", Outer:" + (int)nudTrimOuterNN.Value + "/" + (int)nudTrimOuterDD.Value;
          if (chkTrimPre.Checked) txt += ", pretrim";
          if (chkTrimPost.Checked) txt += ", posttrim";
          txt += ", " + AlignTrim.ToString();
        }
        if (AlignChords != eAlign.None) {
          txt += ", " + AlignChords.ToString();
          if (P.frmCfgChords.chkWeightedScores.Checked) txt += ", weightedscores";
          txt += ", Range:" + (int)P.frmCfgChords.nudScoreRange.Value;
          txt += ", Adder:" + (int)P.frmCfgChords.nudAdder.Value;
          txt += ", Factor:" + (int)P.frmCfgChords.nudChordFactor.Value;
          txt += ", MaxType:" + (int)P.frmCfgChords.nudMaxChordType.Value;
        }

        P.F.CF.Lines_Comments = new List<string>(3);  //delete previous comments
        P.F.CF.Lines_Comments.Add(txt);
      }
    }
    */

    private void cmdChordinate_Click(object sender, EventArgs e) {
      if (CsrPixHi <= CsrPixLo) {
        MessageBox.Show("Selection Area is null - nothing selected!");
        return;
      }
      using (new clsWaitCursor()) {
        #if (DEBUG && DumpTopScores)
          ChordAnalysis.OpenDumpChords(Cfg.DebugPath + @"\DumpChords", true);
        #endif
        ChordAnalysis.SetParams((int)P.frmStart.nudMaxChordSize.Value, Forms.frmChordRanks.MaxRankMatch);
        bool[] prevchord = new bool[12];
        bool[] thischord;
        bool[] newchord = new bool[12];
        clsCFPC.clsEvPC ev;
        P.F.CF.Evs = null;  //recreate on save

        //* create temporary ev(s) for the chord 
        //ChordAnalysis.eStatus status = ChordAnalysis.eStatus.NotChordinated;
        sbyte qualifier = 0;
        int root = -1;
        for (int qi = CsrQILo; qi < CsrQIHi; qi++) {  //this section
          thischord = new bool[12];
          for (int n = 0; n < 12; n++) thischord[n] = P.F.CF.NoteMap.IsF(qi, n);
          if (!thischord.SequenceEqual(prevchord)) {
            clsMTime.clsBBT bbt = new clsMTime.clsBBT(qi * P.F.TicksPerQI);
            //if (bbt.Bar == 9) Debugger.Break();
            ev = new clsCFPC.clsEvPC(P.F.CF, bbt, thischord, null);
            ChordAnalysis.clsScore score = P.F.CF.ChordinateEv(ref ev);
            newchord = new bool[12];
            for (int i = 0; i < ev.Notes.Length; i++) newchord[ev.Notes[i].PC[eKBTrans.None]] = true;
            if (score != null) {
              //if (score.Score == score.ChNotes.Count) status = ChordAnalysis.eStatus.Match;
              //else status = ChordAnalysis.eStatus.Nomatch;
              root = score.Root;
              qualifier = (sbyte)(score.TIndex + 1);
            }
            prevchord = thischord.ToArray();  //copy
          }
          //* update notemap with undo/redo
          //P.F.CFTxt.NoteMap.SetChord(qi, newchord);
          //P.F.CFTxt.NoteMap[qi] = newchord;
          P.F.CF.NoteMap.SetChordAndAtts(qi, newchord,
            new clsNoteMapCF.sChordAtt((sbyte)root, qualifier));
        }
        #if DEBUG
          ChordAnalysis.CloseDumpChords();
        #endif
        SetNoteMapFileChanged();
      }
    }

    private void picNoteMapQuant_MouseClick(object sender, MouseEventArgs e) {
      if (!P.frmSC.IsPlayClickable()) return;
      if (e.X >= TicksToPix(P.F.MaxBBT.Ticks)) return;
      cmdSelectNone.Select();  //default
      //if (!Header_MouseClick(sender, e, CSVFileConv.NoteMap.EleW)) {
      if (Control.ModifierKeys == Keys.Shift) {  //shift only
        SetCsrHiLo(e);
      } else {
        clsIShowNoteMap.SetPlayCsr((e.X * P.F.TicksPerQI) / HPixPerQI);
      }
      RefreshAll();
    }

    private void picNoteMapMidi_MouseClick(object sender, MouseEventArgs e) {
      if (!P.frmSC.IsPlayClickable()) return;
      if (e.X >= TicksToPix(P.F.MaxBBT.Ticks)) return;
      cmdSelectNone.Select();  //default
      if (Control.ModifierKeys == Keys.Shift) {  //shift only
        SetCsrHiLo(e);
      } else {
        clsIShowNoteMap.SetPlayCsr((e.X * P.F.TicksPerQI) / HPixPerQI);
      }
      RefreshAll();
    }

    //private void picNoteMapFile_MouseHover(object sender, EventArgs e) {
    //  Point pos = picNoteMapFile.PointToClient(Cursor.Position);
    //  Debug.WriteLine("mousehover X = " + pos.X);
    //}

    /*
    private static int MouseMove_Root = -2;
    private static string MouseMove_Desc = "?";
    private void picNoteMapFile_MouseMove(object sender, MouseEventArgs e) {
      //* set mouse cursor (icon)
      SetMouseCursor(picNoteMapFile, e);
      //* show chord tooltip
      ShowMouseChord(sender, e);
    }
    */

    //private void ShowMouseChord(object sender, MouseEventArgs e) {
    //  //* show chord tooltip
    //  //Debug.WriteLine("mousemove X = " + e.X);
    //  PictureBox pic = (PictureBox)sender;
    //  int q = e.X / HPixPerQI;
    //  int ticks = q * P.F.TicksPerQI;
    //  if (pic == picNoteMapFile) {
    //    clsNoteMapCF ncf = P.F.CF.NoteMap;
    //    string desc = clsNoteMap.PtrToDesc(ncf.ChordAtt[q].Qualifier);
    //    if (ncf.ChordAtt[q].Root == MouseMove_Root && desc == MouseMove_Desc) return;
    //    MouseMove_Root = ncf.ChordAtt[q].Root;
    //    MouseMove_Desc = desc;
    //  } else {
    //    bool[] boolmap;
    //    if (pic == picNoteMapMidi) {
    //      boolmap = CSVFileConv.NoteMap[q];
    //    } else if (pic == picNoteMapQuant) {
    //      boolmap = CSVFileConv.NoteMap.IsF(q);
    //    } else return;  //should not happen
    //    ChordDB.clsDesc chdesc = ChordDB.GetChord(boolmap);
    //    if (chdesc == null) return;
    //    if (chdesc.Root == MouseMove_Root && chdesc.Qualifier == MouseMove_Desc) return;
    //    MouseMove_Root = chdesc.Root;
    //    MouseMove_Desc = chdesc.Qualifier;

    //  }
    //  if (MouseMove_Root < 0) {
    //    //ttPic.SetToolTip(pic, "no chord");
    //    ttPic.Active = false;
    //  } else {
    //    string txt = "";
    //    txt += NoteName.ToSharpFlat(NoteName.GetNoteName(P.F.Keys[ticks].MidiKey, MouseMove_Root).Trim());
    //    txt += MouseMove_Desc;
    //    ttPic.Active = true;
    //    ttPic.SetToolTip(pic, txt);
    //  }
    //}

    private void SetMouseCursor(PictureBox pic, MouseEventArgs e) {
      if (e.Y < GetPicNM(pic).BarFont.Height) pic.Cursor = Cursors.VSplit;  //header (bar numbers)
      else pic.Cursor = Cursors.Default;
    }

    /*
    private void picNoteMapFile_MouseLeave(object sender, EventArgs e) {
      MouseMove_Root = -2;
      MouseMove_Desc = "?";
    }
    */

    private void cmdLoadMidi_Click(object sender, EventArgs e) {
      if (P.F.frmTrackMap == null) {
        MessageBox.Show("Load failed - no TrackMap");
        return;
      }
      clsTrks.Array<bool> selectedtrks = P.F.frmTrackMap.GetSelectedTrks();
      if (selectedtrks == null) {
        MessageBox.Show("Load failed - no selected tracks");
        return;
      }
      //int transpose = 0;
      Forms.frmTrackMap.CreateCSVFileConv(selectedtrks);
      lblTracksSelected.Text = UpdateTrksSelected(CSVFileConv.TrkSelect);
    }

    //private void chkChordTranspose_Click(object sender, EventArgs e) {
    //  //if (!ShowChordBoxes) return;
    //  ShowCellsAndKeys();
    //}

    private void cmdGoToStart_Click_1(object sender, EventArgs e) {
      P.frmSC.Play?.NewReset();
      Forms.frmStart.RefreshBBT(new clsMTime.clsBBT(0));
    }

    //private void picNoteMap_MouseMove(object sender, MouseEventArgs e) {
    //  //* set mouse cursor (icon)
    //  PictureBox pic = (PictureBox)sender;
    //  SetMouseCursor(pic, e);
    //  //* show chord tooltip
    //  //ShowMouseChord(sender, e);
    //}

    /*
    private void picNoteMapMidi_MouseMove(object sender, MouseEventArgs e) {
      //* set mouse cursor (icon)
      SetMouseCursor(picNoteMapMidi, e);
      //* show chord tooltip
      //* <--not really useful!--> ShowMouseChord(sender, e);
    }
    */

    //internal void cmdSaveChordFile_Click(object sender, EventArgs e) {
    //  Cursor.Current = Cursors.WaitCursor;
    //  string msg = P.frmSC.SaveChordFile();
    //  if (msg != "") MessageBox.Show("ChordFile not saved: " + msg);
    //  Cursor.Current = Cursors.Default;
    //}

    internal void cmdSaveAs_Click(object sender, EventArgs e) {
      string[] dirs = new string[11];
      dirs[0] = P.F.Project.PathAndName + ".chp";
      for (int i = 0; i < 10; i++) {  //get all possible .chp &  .chp<n> files
        dirs[i + 1] = P.F.Project.PathAndName + ".chp" + i;
      }
      dlgCHP frm = new dlgCHP(dirs, false);
      if (frm.ShowDialog(this) == DialogResult.Cancel) return;
      //try {
      //  MessageBox.CacheOn();
      using (new clsWaitCursor()) {
        P.F.Project.CHPExt = frm.OK_Ext;
        string msg = P.frmSC.SaveChordFile();
        if (msg != "") MessageBox.Show("ChordFile not saved: " + msg);
      }
      //}
      //finally {
      //  MessageBox.CacheOff();
      //}
    }

    //private void cmdMod_Click(object sender, EventArgs e) {
    //  P.CloseFrm(P.frmMod);
    //  P.frmMod = new Forms.frmMod();
    //  P.frmMod.ShowDialog(this);
    //}

    //private void chkChordTranspose_CheckedChanged(object sender, EventArgs e) {
    //  ShowCellsAndKeys();
    //}

    //internal void ShowCellsAndKeys() {
    //  //ShowKeys();
    //  BeatChords.ShowDGVCells();
    //}

    /* insert/delete flawed - does not allow for tsig changes, keys, ...
    private void cmdDelete_Click(object sender, EventArgs e) {
      //* delete current selection and close hole
      if (CsrQILo >= CsrQIHi) return;
      int len = CsrQIHi - CsrQILo;

      //* copy
      for (int qsrc = CsrQIHi, qdest = CsrQILo; qsrc <= P.F.MaxQTime; qsrc++, qdest++) {
        P.F.CF.NoteMap.CopyQI(qsrc, qdest); 
      }

      //* nullify end
      NullifyHole(P.F.MaxQTime - len, P.F.MaxQTime);

      //CsrQIHi = CsrQILo;
      SetNoteMapFileChanged(true, -1, -1);
      RefreshAll();
    }

    private void cmdInsert_Click(object sender, EventArgs e) {
      //* insert at start of current selection, for length of current selection
      if (CsrQILo >= CsrQIHi) return;
      int len = CsrQIHi - CsrQILo;

      //* copy
      for (int qsrc = P.F.MaxQTime - len, qdest = P.F.MaxQTime; qsrc >= CsrQILo; qsrc--, qdest--) {
        P.F.CF.NoteMap.CopyQI(qsrc, qdest);
      }

      //* nullify hole
      NullifyHole(CsrQILo, CsrQIHi);

      //* finalize
      //CsrQIHi = Math.Min(P.F.MaxQTime, CsrQIHi + len);
      SetNoteMapFileChanged(true, -1, -1);
      RefreshAll();
    }
    
    private void NullifyHole(int qilo, int qihi) {
      bool[] nullchord = new bool[12];
      for (int i = 0; i < 12; i++) nullchord[i] = false;
      for (int q = qilo; q < qihi; q++) {
        P.F.CF.NoteMap.SetChordAndAtts(q, new bool[12], new clsNoteMapCF.sChordAtt(0));
      }
    }
    */

    //private void DGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e) {
    //  DataGridViewCell cell = DGV.Rows[e.RowIndex].Cells[e.ColumnIndex];
    //  if (cell.ReadOnly) e.Cancel = true;
    //}

    private void DGV_Scroll(object sender, ScrollEventArgs e) {  //not active?
      //Debug.WriteLine("DGV_Scroll entered: " + e.NewValue);
      if (NoScroll) {
        e.NewValue = e.OldValue;
        return;
      }
      ScrollFromDGV = true;
      if (!ScrollFromPan && !ScrollFromdgvLyrics) {
        panNoteMap.AutoScrollPosition = new Point(e.NewValue, panNoteMap.AutoScrollPosition.Y);
        dgvLyrics.HorizontalScrollingOffset = e.NewValue;
        RefreshAll();
        //Debug.WriteLine("DGV_Scroll actioned");
      }
      ScrollFromDGV = false;
      //Debug.WriteLine("DGV_Scroll exited");
    }

    private void dgvLyrics_Scroll(object sender, ScrollEventArgs e) {  //not active?
      //Debug.WriteLine("dgvLyrics_Scroll entered: " + e.NewValue);
      if (NoScroll) {
        e.NewValue = e.OldValue;
        return;
      }
      ScrollFromdgvLyrics = true;
      if (!ScrollFromPan && !ScrollFromDGV) {
        panNoteMap.AutoScrollPosition = new Point(e.NewValue, panNoteMap.AutoScrollPosition.Y);
        DGV.HorizontalScrollingOffset = e.NewValue;
        RefreshAll();
        //Debug.WriteLine("dgvLyrics_Scroll actioned");
      }
      ScrollFromdgvLyrics = false;
      //Debug.WriteLine("dgvLyrics_Scroll exited");
    }

    internal void optChord_CheckedChanged() {
      RefreshAll();
    }

    private void cmdColours_Click(object sender, EventArgs e) {
      Utils.FormAct(P.ColorsNoteMap.FrmColours);  //show new colours as they are changed
    }

    private void cmdExec_Click(object sender, EventArgs e) {
      using (new clsWaitCursor()) {
        //Stopwatch watch = new Stopwatch();
        //watch.Start();
        if (P.F._CFKeys == null && P.F._MidiKeys != null) {
          string msg = "Keys have not yet been generated or edited.";
          msg += "\r\nIt is recommended to use CalcKeys to ensure generated chords are as accurate as possible,";
          msg += " unless you are confident the keys from the MidiFile are reliable."; 
          msg += "\r\nDo you want to use CalcKeys to generate the keys?";
          msg += "\r\nClick 'Yes' to show CalcKeys window.";
          msg += "\r\nClick 'No' to use MidiFile keys.";
          msg += "\r\nClick 'Cancel' to cancel Generate Chords.";
          DialogResult result = MessageBox.Show(msg, MessageBoxButtons.YesNoCancel);
          if (result == DialogResult.Cancel) return;
          else if (result == DialogResult.Yes) {
            P.CloseFrm(P.F.frmCalcKeys);
            clsTrks.Array<bool> trkselectall = new clsTrks.Array<bool>(true);  //all trks
            P.F.frmCalcKeys = new frmCalcKeys(trkselectall);  //dialog
          }
          if (P.F._CFKeys == null) {
            P.F._CFKeys = new clsKeysTicks(P.F._MidiKeys);
            //P.F.Keys.indCF = true;
          }
        }
        clsChordSegs chordsegs;
        ApplyFilter(AlignChords, true, out chordsegs);
        //Debug.WriteLine("Chords Exec Millisecs = " + watch.ElapsedMilliseconds);
        //watch.Stop();
        Refresh();
      }
    }

    /* set new P.F.MTime - not used, but...
    internal void ActivateMTime(clsMTime newmtime) {
      if (newmtime != null) {  //null = P.F.MTime already set
        if (newmtime.IsEquiv(P.F.MTime)) return;  //no change
        clsMTime mtimecopy = new clsMTime(P.F.MTime);
        P.F.MTime = newmtime;
      }

      if (P.F.CF != null) {
        P.F.CF.CreateEvs();
        P.F.CF.indChanged = true;
      }
      BeatChords = new clsBeatChords(this, DGV);
      BeatChords.Init();
      //InitDGV();
      VResize();
      frmNoteMap_Resize(null, null);
      if (P.frmSC != null) {
        P.frmSC.ReInitPlayMode();  //to allow for transposition, ...
        P.frmSC.nudStartBar.Value = 2;  //barnumbers may change - go back to start
        P.frmSC.nudStartBar.Value = 1;  //make sure it drives ValueChanged events
      }

      //* push onto stack...
      //UndoPush(new clsUndoRedoTSigsLive(mtimecopy));
      if (P.F.frmMultiMap != null) P.F.frmMultiMap.Refresh();
      Refresh();
    }
    */

    private void cmdConfigChords_Click(object sender, EventArgs e) {
      //P.frmCfgChords.Show();
      //P.frmCfgChords.Activate();
      Utils.FormAct(P.frmCfgChords);
    }

    private void cmdAdvanced_Click(object sender, EventArgs e) {
      if (P.F.frmChordMapAdv == null) P.F.frmChordMapAdv = new frmChordMapAdv();
      //P.F.frmChordMapAdv.Show();
      //P.F.frmChordMapAdv.Activate();
      Utils.FormAct(P.F.frmChordMapAdv);
    }

    //private void cmbSnapTimes_SelectedIndexChanged(object sender, EventArgs e) {
    //  if (Bypass_Event) return;
    //  SnapTo = (string)cmbSnapTimes.SelectedItem;
    //}

    //private void chkSnapNote_CheckedChanged(object sender, EventArgs e) {
    //  if (Bypass_Event) return;
    //  SnapNote = chkSnapNote.Checked;
    //}

    //private void chkSnapTime_CheckedChanged(object sender, EventArgs e) {
    //  if (Bypass_Event) return;
    //  SnapTime = chkSnapTime.Checked;
    //}

    private void picMod_Paint(object sender, PaintEventArgs e) {
      PictureBox pic = (PictureBox)sender;
      Graphics xgr = e.Graphics;
      Brush brush = new SolidBrush(Color.Black);
      StringFormat fmt = GetStringFmt();
      //foreach (clsKey key in P.F.Keys) {

      Brush barbrush = new SolidBrush(Color.Red);
      int maxbar = P.F.MaxBBT.Bar;
      Pen barpen = new Pen(barbrush, 2);
      for (int bar = 0; bar <= maxbar; bar++) {
        clsMTime.clsBBT bbt = new clsMTime.clsBBT(P.F.MTime, bar, 0, 0);
        int pixbar = TicksToPix(bbt.Ticks);
        xgr.DrawLine(barpen, pixbar, 0, pixbar, picTSig.Height);
      }

      for (int i = 0; i < P.F.Keys.Keys.Count; i++) {
        clsKeyTicks key = P.F.Keys.GetKey(i, kbtrans: false);
        if (pic == picModNames) {
          key = key.GetTransposeNames(TransposeChordNamesVal); 
        }
        if (key.BBT.TicksRemBar != 0) {
          LogicError.Throw(eLogicError.X164);
          key.BBT.RoundDownToBar();
        }
        clsMTime.clsBBT bbtbar = key.BBT;  //key should be aligned to bar
        int qbar = (bbtbar.Ticks * P.F.QIPerNote) / (4 * P.F.MTime.TicksPerQNote);
        int pixbar = qbar * HPixPerQI;
        Rectangle rectf = new Rectangle(pixbar, 0, 200, pic.ClientRectangle.Height);
        xgr.DrawString(key.KeyStrLong, BarFont, brush, rectf, fmt);
      }
    }

    private void picTSig_Paint(object sender, PaintEventArgs e) {
      Graphics xgr = e.Graphics;
      Brush brush = new SolidBrush(Color.Black);
      Brush barbrush = new SolidBrush(Color.Red);
      Brush beatbrush = new SolidBrush(Color.LightGray);
      StringFormat fmt = GetStringFmt();
      clsMTime mtime = P.F.MTime;
      foreach (clsMTime.clsTSigBB tsig in mtime.TSigs) {
        clsMTime.clsBBT bbtbar = new clsMTime.clsBBT(mtime, tsig.Bar, 0, 0);
        int qbar = (bbtbar.Ticks * P.F.QIPerNote) / (4 * mtime.TicksPerQNote);
        int pixbar = qbar * HPixPerQI;
        //Rectangle rectf = new Rectangle(pixbar, 0, 200, picModNotes.ClientRectangle.Height);
        Rectangle rectf = new Rectangle(pixbar, 0, 200, picTSig.ClientRectangle.Height);
        xgr.DrawString(tsig.Txt, BarFont, brush, rectf, fmt);
      }
      //int maxbeat = new clsMTime.clsBBT(mtime, P.F.MaxTicks).Beats;
      int maxbeat = P.F.MaxBBT.Beats;
      Pen barpen = new Pen(barbrush, 2);
      Pen beatpen = new Pen(beatbrush);
      for (int beat = 0; beat <= maxbeat; beat++) {
        clsMTime.clsBBT bbt = new clsMTime.clsBBT(mtime, beat, true);
        int pixbeat = TicksToPix(bbt.Ticks);
        Pen pen = (bbt.BeatsRemBar == 0) ? barpen : beatpen;
        xgr.DrawLine(pen, pixbeat, 0, pixbeat, picTSig.Height);
      }
    }

    private static StringFormat GetStringFmt() {
      StringFormat fmt = new StringFormat();
      fmt.Alignment = StringAlignment.Near;  //horizontal
      fmt.LineAlignment = StringAlignment.Center;  //vertical 
      return fmt;
    }

    private void picMod_MouseClick(object sender, MouseEventArgs e) {
      if (!P.frmSC.IsPlayClickable()) return;
      PictureBox pic = (PictureBox)sender;
      int trans = (pic == picModNames) ? TransposeChordNamesVal : 0;

      Mouse_BBT = PixToBBTBar(e.X);
      if (Mouse_BBT.Ticks < 0 || Mouse_BBT.Ticks >= P.F.MaxBBT.Ticks) return;
      for (Mouse_Index = 0; Mouse_Index < P.F.Keys.Keys.Count; Mouse_Index++) {
        clsKeyTicks k = P.F.Keys.Keys[Mouse_Index];
        if (k.BBT.Bar == Mouse_BBT.Bar) break;
      }
      clsKeyTicks keyticks = (pic == picModNames && trans != 0) ?
        keyticks = P.F.Keys[Mouse_BBT.Ticks].GetTransposeNames(trans) :
        keyticks = P.F.Keys[Mouse_BBT.Ticks].GetTranspose(trans);

      Bypass_Event = true;
      ContextMenuStrip mnu;
      if (Mouse_Index < P.F.Keys.Keys.Count) {
        mnu = mnuModHit;
        mnuModHitRemove.Tag = trans;
        mnuModHitRemove.Enabled = (Mouse_Index > 0);
        mnuModHitcmbChange.Tag = trans;
        mnuModHitcmbChange.SelectedItem = keyticks.KeyStrLong;
      } else {
        mnu = mnuModMiss;
        mnuModMisscmbNew.Tag = trans;
        mnuModMisscmbNew.SelectedItem = keyticks.KeyStrLong;
      }
      mnu.Tag = trans;
      Bypass_Event = false;
      int pixclick = BarsToPix(Mouse_BBT.Bar);
      mnu.Show(pic, new Point(pixclick, pic.Height), ToolStripDropDownDirection.Right);
    }

    private void mnuModMisscmbNew_SelectedIndexChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      using (new clsWaitCursor()) {
        using (new clsChordMapDis(this)) {
          ToolStripComboBox mnucmb = (ToolStripComboBox)sender;
          clsKeyTicks keyticksnotes = GetKeyTicksNotes(mnucmb);
          //clsKeyTicks keyticks = GetSelectedKey(mnuModMisscmbNew).GetTranspose(-trans);
          P.F.Keys.Insert(keyticksnotes);
          P.F.CopyDefaultKeys();
          SetNoteMapFileChanged();
        }
        mnuModMiss.Close();
        RefreshAll();
        P.frmSC.Refresh();
      }
    }

    private clsKeyTicks GetKeyTicksNotes(ToolStripComboBox mnucmb) {
      int trans = (int)mnucmb.Tag;
      clsKeyTicks keyticksnames = GetSelectedKey(mnucmb);
      clsKeyTicks keyticksnotes = keyticksnames.GetTranspose(-trans);
      if (trans != 0) {
        if (keyticksnames.MidiKey > 0) keyticksnotes.TransposeNamesSharp = true;
        else if (keyticksnames.MidiKey < 0) keyticksnotes.TransposeNamesSharp = false;
        //* else leave as null
      }

      return keyticksnotes;
    }

    private void mnuModHitcmbChange_SelectedIndexChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      using (new clsWaitCursor()) {
        using (new clsChordMapDis(this)) {
          ToolStripComboBox mnucmb = (ToolStripComboBox)sender;
          clsKeyTicks keyticksnotes = GetKeyTicksNotes(mnucmb);
          if (P.F._CFKeys == null) P.F._CFKeys = new clsKeysTicks(P.F._MidiKeys);
          P.F.Keys.Change(Mouse_Index, keyticksnotes);
          P.F.CopyDefaultKeys();
          SetNoteMapFileChanged();
          mnuModHit.Close();
        }
        P.frmSC.Refresh();
      }
    }

    private void mnuModHitRemove_Click(object sender, EventArgs e) {
      using (new clsWaitCursor()) {
        using (new clsChordMapDis(this)) {
          P.F.Keys.RemoveAt(Mouse_Index);
          P.F.CopyDefaultKeys();
          SetNoteMapFileChanged();
        }
        mnuModHit.Close();
        P.frmSC.Refresh();
      }
    }

    private void picTSig_MouseClick(object sender, MouseEventArgs e) {
      if (!P.frmSC.IsPlayClickable()) return;
      Mouse_BBT = PixToBBTBar(e.X);
      if (Mouse_BBT.Ticks < 0 || Mouse_BBT.Ticks >= P.F.MaxBBT.Ticks) return;

      int pixclick = BarsToPix(Mouse_BBT.Bar);
      mnuTSig.Show(picTSig, new Point(pixclick, picTSig.Height), ToolStripDropDownDirection.Right);
    }

    private clsKeyTicks GetSelectedKey(ToolStripComboBox mnucmb) {
      int mod = (int)mnucmb.SelectedIndex;
      if (mod < 15) {  //major: 0-6(flatkeys) 7(C) 8-14(sharpkeys) 
        int midikey = mod - 7;  //convert 0 to 14 -> -7 to +7
        return new clsKeyTicks(midikey, "major", Mouse_BBT.Ticks);
      } else {  //minor: 15-21(flatkeys) 22(C) 23-29(sharpkeys) 
        int midikey = mod - 22;  //convert 15 - 29 -> -7 - +7 
        return new clsKeyTicks(midikey, "minor", Mouse_BBT.Ticks);
      }
    }

    private void chkShowQuant_CheckedChanged(object sender, EventArgs e) {
      VResize();
    }

    //private void mnuTSigActivateSub_Click(object sender, EventArgs e) {
    //  string msg = "Do you want to activate the time signature control?\r\n";
    //  msg += "\r\nAfter activation, you can update the time signatures.";
    //  msg += "\r\nWhen you have finished updating, you will need to save the chord file (.chp),";
    //  msg += "\r\nand reload for the new time signatures to come into effect.";
    //  if (MessageBox.Show(msg, "Activate TSig Control", MessageBoxButtons.OKCancel) == DialogResult.OK) {
    //    //P.F.NewMTime = new clsMTime(P.F.MTime);
    //    P.F.NewMTime = P.F.MTime;
    //  }
    //}

    private void mnuTSigRemove_Click(object sender, EventArgs e) {
      if (CsrQILo < CsrQIHi) {
        using (new clsWaitCursor()) {
          using (new clsChordMapDis(this)) {
            clsMTime.clsTSigBB[] tsigs = P.F.MTime.TSigs.ToArray();
            try {
              string msg = "Remove time signatures from selected area - Confirm";
              if (MessageBox.Show(msg, MessageBoxButtons.YesNo) == DialogResult.No) return;
              clsMTime.clsBBT bbtlo = new clsMTime.clsBBT(CsrQILo * P.F.TicksPerQI);
              clsMTime.clsBBT bbthi = new clsMTime.clsBBT(CsrQIHi * P.F.TicksPerQI);
              RemoveTSig(bbtlo, bbthi);
            }
            catch (TSigException) {
              MessageBox.Show("Error creating new time signatures - no changes made");
              P.F.MTime.TSigs = tsigs;
            }
          }
        }
      }
      mnuTSig.Close();
      //P.F.frmTrackMap?.Refresh();
      //P.frmSC.Refresh();
    }

    internal void RemoveTSig(clsMTime.clsBBT bbtlo, clsMTime.clsBBT bbthi) {
      //* validate
      if (bbtlo.Bar == bbthi.Bar) return;
      if (bbtlo.TicksRemBar != 0) throw new TSigException();
      if (bbthi != null && bbthi.TicksRemBar != 0) throw new TSigException();

      //* execute
      List<clsBuffQI> buff = new List<clsBuffQI>(5000);
      LiveToBuff(buff, 0, P.F.MaxBBT.QI, indtsigs: true);
      buff.RemoveRange(CsrQILo, CsrQIHi - CsrQILo);
      BuffToLive(buff, indchords: false, indtsigs: true, indkeys: false);
      //P.frmSC.ResizeForm();
    }


    private void mnucmbTSigEnd_SelectedIndexChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      TSigIndexChanged(sender, false);
      mnuTSig.Close();
    }

    private void mnucmbTSigArea_SelectedIndexChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      if (CsrQILo < CsrQIHi) TSigIndexChanged(sender, true);
      mnuTSig.Close();
    }

    private void mnucmbTSigInsert_SelectedIndexChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      if (CsrQILo < CsrQIHi) {
        //* get tsig nndd
        int qilen = CsrQIHi - CsrQILo;
        clsNNDD nndd = GetTSigNNDD(sender);
        if (nndd == null) return;

        //* create live buffer
        List<clsBuffQI> buff = new List<clsBuffQI>(5000);
        LiveToBuff(buff, 0, P.F.MaxBBT.QI, indtsigs: true);

        //* create insert buffer
        clsMTime.clsBBT bbtlo = new clsMTime.clsBBT(CsrQILo * P.F.TicksPerQI);
        clsMTime.clsBBT bbthi = new clsMTime.clsBBT(CsrQIHi * P.F.TicksPerQI);
        List<clsBuffQI> buffinsert = new List<Forms.frmChordMap.clsBuffQI>(1000);
        clsBuffQI ele = new clsBuffQI();
        ele.TSig = new clsMTime.clsTSig(P.F.MTime, nndd.NN, nndd.DD);
        for (int b = bbtlo.Bar; b < bbthi.Bar; b++) {
          for (int q = 0; q < ele.TSig.TicksPerBar / P.F.TicksPerQI; q++) {
            buffinsert.Add(ele);
          }
        }

        //* action
        buff.InsertRange(CsrQILo, buffinsert);  //will be oversized
        BuffToLive(buff, indchords: false, indtsigs: true, indkeys: false);
        //P.frmSC.ResizeForm();
      }
      mnuTSig.Close();
    }

    private static clsNNDD GetTSigNNDD(object sender) {
      ToolStripComboBox cmb = (ToolStripComboBox)sender;
      if (cmb.SelectedIndex == 0) return null;  //first item
      string selecteditem = (string)cmb.SelectedItem;
      clsNNDD nndd = clsNNDD.GetNNDD(selecteditem);
      return nndd;
    }

    private void TSigIndexChanged(object sender, bool indselectedarea) {
      using (new clsWaitCursor()) {
        using (new clsChordMapDis(this)) {
          clsMTime.clsTSigBB[] tsigs = P.F.MTime.TSigs.ToArray();
          try {
            clsNNDD nndd = GetTSigNNDD(sender);
            if (nndd == null) return;
            if (indselectedarea) {
              clsMTime.clsBBT bbtlo = new clsMTime.clsBBT(CsrQILo * P.F.TicksPerQI);
              clsMTime.clsBBT bbthi = new clsMTime.clsBBT(CsrQIHi * P.F.TicksPerQI);
              P.F.MTime.ChangeTSig(nndd.NN, nndd.DD, bbtlo, bbthi);
            } else {
              P.F.MTime.ChangeTSig(nndd.NN, nndd.DD, Mouse_BBT, null);
            }

            NewMTime();
            P.F.CF.indSave = true;
            P.F.CF.UndoRedoCF.Update();  //Undo/Redo cmds on frmChordMap
          }
          catch (TSigException) {
            MessageBox.Show("Error creating new time signatures - no changes made");
            P.F.MTime.TSigs = tsigs;
          }
        }
        P.F.frmTrackMap?.Refresh();
        //P.frmSC.Refresh();
        P.frmSC.SetEndBBTRefresh();  //recalc EndBBT
      }
    }

    internal void NewMTime() {
      ++P.F.MTime.Gen;  //update any bbt on first access after this
      P.F.MaxBBT.InitBBT();

      //* align keys
      foreach (clsKeyTicks key in P.F.Keys.Keys) {
        if (key.BBT.TicksRemBar != 0) {  //keys should aligned to bar 
          clsMTime.clsBBT bbt = key.BBT;
          bbt.RoundToBar();
          key.Ticks = bbt.Ticks;
        }
      }
      for (int i = 1; i < P.F.Keys.Keys.Count; i++) {
        if (P.F.Keys.Keys[i].BBT.Bar == P.F.Keys.Keys[i - 1].BBT.Bar) {  //theoretically possible
          P.F.Keys.Keys.RemoveAt(i);
          i--;
        }
      }
      P.F.Keys.Finish();  //tidy up

      //* update frmSC
      P.frmSC.vScrollBar1.Maximum = P.F.MaxBBT.Bar;
      P.frmSC.nudStartBar.Maximum = P.F.MaxBBT.Bar;

      //* update dgvLyrics
      if (P.F.frmLyrics != null) P.F.frmLyrics.Close();
      if (P.F.Lyrics.LyricsExist) {
        P.F.Lyrics.InitDGV(this);
        if (P.F.frmTrackMap != null) P.F.Lyrics.InitDGV(P.F.frmTrackMap);
      }

      //* update Strms
      P.F.FSTrackMap?.RefreshStrmBeats();
      P.F.FileStreamConv?.RefreshStrmBeats();

      //* close autosync
      if (P.F.frmAutoSync != null) P.F.frmAutoSync.Close();  //data probably invalid

      //* update DGV
      DGVNewMTime();

      //* refresh
      P.F?.frmChordMap?.Refresh();
      P.F?.frmTrackMap?.Refresh();

      Debug.WriteLine("frmChordMap: MTime Gen = " + P.F.MTime.Gen);
    }

    private void DGVNewMTime() {
      //* update DGV
      DGV.Rows.Clear();
      DGV.CellValidating -= BeatChords.DGV_CellValidating_Handler;
      BeatChords = new clsBeatChords(this, DGV);
      BeatChords.Init();  //-> new DGV_CellValidating_Handler
      VResize();
    }

    //internal class clsUndoRedoTSigs {
    //  private Forms.frmChordMap Frm;
    //  internal static Stack<clsMTime> UndoStack = new Stack<clsMTime>();
    //  internal static Stack<clsMTime> RedoStack = new Stack<clsMTime>();

    //  internal clsUndoRedoTSigs(Forms.frmChordMap frm) {
    //    Frm = frm;
    //  }

    //  internal void Update() {  //called before making change
    //    UndoStack.Push(new clsMTime(P.F.MTime));
    //    SetMnuState();
    //  }

    //  internal void Undo() {
    //    if (UndoStack.Count == 0) return;
    //    RedoStack.Push(new clsMTime(P.F.MTime));
    //    Forms.frmStart.RefreshBBT(new clsMTime.clsBBT(0));  //current bar may become OOR
    //    P.F.MTime = UndoStack.Pop();
    //    Frm.NewMTime();
    //    SetMnuState();
    //  }

    //  internal void Redo() {
    //    if (RedoStack.Count == 0) return;
    //    UndoStack.Push(new clsMTime(P.F.MTime));
    //    Forms.frmStart.RefreshBBT(new clsMTime.clsBBT(0));  //current bar may become OOR
    //    P.F.MTime = RedoStack.Pop();
    //    Frm.NewMTime();
    //    SetMnuState();
    //  }

    //  internal void SetMnuState() {
    //    Frm.mnuTSigUndo.Enabled = (UndoStack.Count > 0);
    //    Frm.mnuTSigRedo.Enabled = (RedoStack.Count > 0);
    //  }
    //}

    //private void mnuTSigUndo_Click(object sender, EventArgs e) {
    //  UndoRedoTSigs.Undo();
    //}

    //private void mnuTSigRedo_Click(object sender, EventArgs e) {
    //  UndoRedoTSigs.Redo();
    //}

    //private void mnuTSigReset_Click(object sender, EventArgs e) {
    //  //P.F.NewMTime = null;
    //  P.F.CF.indChanged = true;
    //  picTSig.Refresh();
    //}

    private void picTSig_Click(object sender, EventArgs e) {
      Bypass_Event = true;
      mnucmbTSigEndCommon.SelectedIndex = 0;
      mnucmbTSigEndAny.SelectedIndex = 0;
      mnucmbTSigAreaCommon.SelectedIndex = 0;
      mnucmbTSigAreaAny.SelectedIndex = 0;
      mnucmbTSigInsertCommon.SelectedIndex = 0;
      mnucmbTSigInsertAny.SelectedIndex = 0;
      Bypass_Event = false;
    }

    //private void mnuModUndo_Click(object sender, EventArgs e) {
    //  P.F.UndoRedoKeys.Undo();
    //  RefreshAll();
    //  P.frmSC.Refresh();
    //}

    //private void mnuModRedo_Click(object sender, EventArgs e) {
    //  P.F.UndoRedoKeys.Redo();
    //  RefreshAll();
    //  P.frmSC.Refresh();
    //}

    //private void dgvLyrics_CellClick(object sender, DataGridViewCellEventArgs e) {
    //  if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.None) return;
    //  clsMTime.clsBBT bbt = new MPlay.clsMTime.clsBBT(e.ColumnIndex, 0, 0);  //locate to bar
    //  clsIShowNoteMap.SetPlayCsr(bbt);
    //  RefreshAll();
    //}

    private void picBars_Paint(object sender, PaintEventArgs e) {
      Forms.frmTrackMap.Bars_Paint(e, picBars, panNoteMap, BarFont, HFactor, HDiv);
    }

    private void picMargins_Paint(object sender, PaintEventArgs e) {
      Graphics gr = e.Graphics;
      Brush brush = Brushes.SkyBlue;
      int height = picMargins.ClientSize.Height;
      int width = picMargins.ClientSize.Width;
      gr.FillRectangle(brush, 0, 0, ScrollMarginLo, height);
      gr.FillRectangle(brush, width - ScrollMarginHi, 0, ScrollMarginHi, height);
    }

    private void picMargins_MouseClick(object sender, MouseEventArgs e) {
      //int mid = picMargins.Width / 2;
      //if (e.X < mid) ScrollMarginLo = e.X; else ScrollMarginHi = picMargins.Width - e.X;
      int lenlo = picMargins.Width / 3;
      int lenhi = picMargins.Width * 2 / 3;
      if (e.X < lenlo) ScrollMarginLo = e.X;
      else if (e.X > lenhi) ScrollMarginHi = picMargins.Width - e.X;
      picMargins.Refresh();
    }

    private void frmNoteMap_Resize(object sender, EventArgs e) {
      Refresh();
      //picMargins.Refresh();
      //picBars.Refresh();
    }

    private void cmdPlayAudio_Click(object sender, EventArgs e) {
      if (P.F.AudioSync != null) P.F.AudioSync.StartCmdSyncPlay();
    }

    private void cmdPlayAndRecordAudio_Click(object sender, EventArgs e) {
      if (P.F.AudioSync != null) P.F.AudioSync.StartCmdSyncPlayAndRecord();
    }

    private void cmdTransposeSelected_Click(object sender, EventArgs e) {
      if (CsrQILo >= CsrQIHi) {
        MessageBox.Show("Transpose Select terminated - no selected area");
        return;
      }
      Forms.dlgNud dlg = new Forms.dlgNud();
      dlg.lblPrompt.Text = "Enter transposition interval (-11 to + 11)";
      dlg.lblMsg.Text = "";
      dlg.nud1.Minimum = -11;
      dlg.nud1.Maximum = 11;
      dlg.nud1.Value = 0;
      DialogResult res = dlg.ShowDialog();
      if (res == DialogResult.Cancel) return;
      if (dlg.nud1.Value == 0) return;
      for (int qi = CsrQILo; qi < CsrQIHi; qi++) {
        P.F.CF.NoteMap.TransposeChord(qi, (int)dlg.nud1.Value);
      }
      SetNoteMapFileChanged(undoredo: true, indqi: true);
    }

    private void cmdTransposeChordNotesAndKeys_Click(object sender, EventArgs e) {
      Button cmd = (Button)sender;
      ExecTransposeChordNotes(cmd == cmdTransposeChordNotesAndKeysPos, true);
    }

    private void cmdTransposeChordNotes_Click(object sender, EventArgs e) {
      Button cmd = (Button)sender;
      ExecTransposeChordNotes(cmd == cmdTransposeChordNotesPos, false);
    }

    private void ExecTransposeChordNotes(bool indpos, bool indkeys) {
      int diff = (indpos) ? 1 : -1;
      if (diff == 1 && StaticTransposeChordNamesVal <= -11) return;
      if (diff == -1 && StaticTransposeChordNamesVal >= 11) return;

      //Buff_Copy = new List<clsBuffQI>();
      P.F.CF.TransposeNoteMap(diff);
      if (indkeys) {
        for (int i = 0; i < P.F.Keys.Keys.Count; i++) {
          P.F.Keys.TransposeKey(i, diff);
        }
      }
      SetNoteMapFileChanged(undoredo: true, indqi: true);

      int namesval = StaticTransposeChordNamesVal;
      namesval -= diff;  //notes up -> namesd diff down
      namesval = Math.Min(Math.Max(namesval, -11), 11);
      //Bypass_Event = true;
      nudTransposeChordNames.Value = namesval;
      //Bypass_Event = false;
    }

    private void nudTransposeChordNames_ValueChanged(object sender, EventArgs e) {
      StaticTransposeChordNamesVal = (int)nudTransposeChordNames.Value;
      P.F.CF.indSave = true;
      //if (Bypass_Event) return;
      BeatChords.ShowDGVCells();
      picModNames.Refresh();
      picModNotes.Refresh();  //not required, but make sure this hasn't changed
    }

    private void cmdTransposeKeys_Click(object sender, EventArgs e) {
      Button cmd = (Button)sender;
      int diff = (cmd == cmdTransposeKeysPos) ? 1 : -1;
      for (int i = 0; i < P.F.Keys.Keys.Count; i++) {
        P.F.Keys.TransposeKey(i, diff);
      }
      SetNoteMapFileChanged(undoredo: true, indqi: true);
    }

    private void cmdSyncAudio_Click(object sender, EventArgs e) {
      if (P.F.AudioSync != null) P.F.AudioSync.StartCmdSyncRecord();
    }

    private void picBarsBottom_MouseClick(object sender, MouseEventArgs e) {
      Forms.frmTrackMap.Bars_MouseClick(e, HDiv, HFactor, panNoteMap);
    }

    private void cmdShowAudioSyncWindow_Click(object sender, EventArgs e) {
      if (P.F.frmAutoSync == null && P.F != null && P.F.AudioSync != null) {
        P.F.frmAutoSync = new frmAutoSync(P.F.AudioSync);
        //P.F.frmAutoSync.Show();
      }
      //P.F.frmAutoSync.cmdShow_Click(null, null);  //update lists
      //P.F.frmAutoSync.Activate();
      Utils.FormAct(P.F.frmAutoSync);
    }

    private void PopulateMnuCmbKeys(ToolStripComboBox mnucmb) {
      foreach (string key in NoteName.MajKeys) mnucmb.Items.Add(NoteName.ToSharpFlat(key) + " Major");
      foreach (string key in NoteName.MinKeys) mnucmb.Items.Add(NoteName.ToSharpFlat(key) + " Minor");
    }

    //private void dgvLyrics_CellEnter(object sender, DataGridViewCellEventArgs e) {
    ////* This event may occur twice for a single click if the control does not have input focus
    ////* and the clicked cell was not previously the current cell. (Microsoft)
    //if (Bypass_DGV) return;
    //if (!Form_Loaded) return;
    //if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.None) return;
    ////if (indFocusdgvLyrics) {
    ////  indFocusdgvLyrics = false;
    ////  return;
    ////}
    //clsMTime.clsBBT bbt = new clsMTime.clsBBT(e.ColumnIndex, e.RowIndex, 0);  //locate to bar/beat
    //Debug.WriteLine("frmChordMap: dgvLyrics_CellEnter:"
    //  + " CurrentBBT = " + P.F.CurrentBBT.ToStringBase0()
    //  + " Col.Row = " + e.ColumnIndex + "." + e.RowIndex);
    //if (P.F.CurrentBBT.Bar == e.ColumnIndex && P.F.CurrentBBT.Beats == e.RowIndex) return;
    //Bypass_DGV = true;
    //NoSelectDGV = dgvLyrics;
    //clsIShowNoteMap.SetPlayCsr(bbt);
    //RefreshAll();
    //NoSelectDGV = null;
    //Bypass_DGV = false;
    //}

    //private void DGV_CellEnter(object sender, DataGridViewCellEventArgs e) {
    ////* This event may occur twice for a single click if the control does not have input focus
    ////* and the clicked cell was not previously the current cell. (Microsoft)
    //if (Bypass_DGV) return;
    //if (!Form_Loaded) return;
    //if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.None) return;
    ////if (indFocusDGV) {
    ////  indFocusDGV = false;
    ////  return;
    ////}
    //clsMTime.clsBBT bbt = new clsMTime.clsBBT(e.ColumnIndex, e.RowIndex, 0);  //locate to bar/beat
    //Debug.WriteLine("frmChordMap: DGV_CellEnter:" 
    //  + " CurrentBBT = " + P.F.CurrentBBT.ToStringBase0()
    //  + " Col.Row = " + e.ColumnIndex + "." + e.RowIndex);
    //Bypass_DGV = true;
    //NoSelectDGV = DGV;
    //clsIShowNoteMap.SetPlayCsr(bbt);
    //RefreshAll();
    //NoSelectDGV = null;
    //Bypass_DGV = false;
    //}

    private void PopulateMnuCmbTSigs(ToolStripComboBox mnucmb, IEnumerable<clsNNDD> tsigs) {
      mnucmb.Items.Add("Select...");
      foreach (clsNNDD nndd in tsigs) {
        mnucmb.Items.Add(nndd.ToString());
      }
    }

    public void RefreshpicNoteMapFile() {
      picNoteMapFile.Refresh();
    }

    //private void SaveStatic() {
    //  if (WindowState == FormWindowState.Normal) {  //save location and size
    //    Rect = new Rectangle(Location.X, Location.Y, Width, Height);
    //  }
    //}

    //private void LoadStatic() {
    //  if (WindowState == FormWindowState.Normal && Rect.Width != 0) {  //default 0, 0, 0, 0
    //    Location = Rect.Location;
    //    Width = Rect.Width;
    //    Height = Rect.Height;
    //  }
    //}

    //private void DGV_CellClick(object sender, DataGridViewCellEventArgs e) {
      //clsPlay.SetBeatChord_FirstTime = true;
    //}

    //private void dgvLyrics_CellClick(object sender, DataGridViewCellEventArgs e) {
      //clsPlay.SetBeatChord_FirstTime = true;
    //}

    private void cmdPanic_Click(object sender, EventArgs e) {
      P.F.Panic();
    }

    //internal static bool IniToStatic(string[] f) {
    //  switch (f[0]) {
    //    case "frmNMRect":
    //      string[] ff = f[1].Split(new char[] { ',' });
    //      int f0 = int.Parse(ff[0]);
    //      int f1 = int.Parse(ff[1]);
    //      int f2 = int.Parse(ff[2]);
    //      int f3 = int.Parse(ff[3]);
    //      //Rect = new Rectangle(f0, f1, f2, f3);
    //      return true;
    //  }
    //  return false;
    //}

    //internal static void StaticToIni(StreamWriter sw) {
    //  if (P.F != null && P.F.frmChordMap != null) P.F.frmChordMap.SaveStatic();
    //  if (Rect.Width > 0) {  //default: 0, 0, 0, 0
    //    sw.WriteLine("frmNMRect = "
    //      + Rect.X + ", "
    //      + Rect.Y + ", "
    //      + Rect.Width + ", "
    //      + Rect.Height);
    //  }
    //}

    private void cmdHelp_Click(object sender, EventArgs e) {
      Utils.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_ChordMap_Intro.htm");
    }

    private void chkOptChordsNone_CheckedChanged(object sender, EventArgs e) {
      if (!chkOptChordsNone.Checked) return;
      SetoptChords();
    }

    private void cmdUpdateLyrics_Click(object sender, EventArgs e) {
      //indAutoScrollCmd = true;
      //panNoteMap_AutoScrollPosX = -panNoteMap.AutoScrollPosition.X;
      if (P.F.frmLyrics == null) {
        P.F.frmLyrics = new frmLyrics();
        //P.F.frmLyrics.Show();
      }
      Utils.FormAct(P.F.frmLyrics);
    }

    internal class clsBuffQI {
      internal ushort Chord;
      internal clsNoteMapCF.sChordAtt ChordAtt;
      internal clsMTime.clsTSig TSig;  //no bar/beat ref
      internal clsKey Key;  //no ticks ref
    }

    private int Buff_Copy_QILo = 0;
    private int Buff_Copy_QIHi = 0;
    private List<clsBuffQI> Buff_Copy;
    private List<clsBuffQI> Buff_Paste;
    internal bool NoRefreshDGV = false;

    private void cmdCut_Click(object sender, EventArgs e) {
      if (!(P.F.CF is clsCFPC)) return;
      if (CsrPixHi <= CsrPixLo) {
        MessageBox.Show("Selection Area is null - nothing selected!");
        return;
      }
      using (new clsWaitCursor()) {
        CopySelected();
        //DeleteBars(copytsigskeys: true);
        for (int qi = CsrQILo; qi < CsrQIHi; qi++) {
          P.F.CF.NoteMap.RemoveNotes(qi);
        }
        SetNoteMapFileChanged();
      }
    }

    private void cmdCopy_Click(object sender, EventArgs e) {
      //* validate
      if (!(P.F.CF is clsCFPC)) return;
      if (CsrPixHi <= CsrPixLo) {
        MessageBox.Show("Selection Area is null - nothing selected!");
        return;
      }
      using (new clsWaitCursor()) {
        CopySelected();
      }
    }

    private void CopySelected() {
      //* copy selected area to Buff_Copy
      Buff_Copy = new List<clsBuffQI>();
      Buff_Copy_QILo = PixToTicks(CsrPixLo) / P.F.TicksPerQI;
      Buff_Copy_QIHi = PixToTicks(CsrPixHi) / P.F.TicksPerQI;
      LiveToBuff(Buff_Copy, Buff_Copy_QILo, Buff_Copy_QIHi, indchords: true, indtsigs: true, indkeys: true);
    }

    private bool ValidatePaste() {
      if (Buff_Copy == null || Buff_Copy.Count == 0) {
        MessageBox.Show("Paste terminated - copy buffer is empty");
        return false;
      }
      if (CsrPixLo > CsrPixHi) {    //can be equal
        MessageBox.Show("Paste terminated - no selected area");
        return false;
      }
      return true;
    }

    private void cmdPasteSpecial_Click(object sender, EventArgs e) {
      if (!ValidatePaste()) return;
      using (new clsWaitCursor()) {
        Forms.dlgPasteSpecial frm = new Forms.dlgPasteSpecial(Buff_Copy_QILo, Buff_Copy_QIHi);
        if (frm.ShowDialog() == DialogResult.Cancel) return;

        //if (frm.optRemoveInsert.Checked) {
        //  Paste(copytsigsandkeys: false, replace: false, nullify: false);
        //} else if (frm.optReplaceNullify.Checked) {
        //  Paste(copytsigsandkeys: false, replace: true, nullify: true);
        //} else if (frm.optReplaceRetain.Checked) {
        //  Paste(copytsigsandkeys: false, replace: true, nullify: false);
        //}

        if (frm.optPasteAll.Checked) {
          Paste(copytsigskeys: true, replace: false, nullify: false);
        } else if (frm.optRemoveInsert.Checked) {
          Paste(copytsigskeys: false, replace: false, nullify: false);
        //} else if (frm.optReplaceNullify.Checked) {
        //  Paste(copytsigsandkeys: false, replace: true, nullify: true);
        } else if (frm.optReplaceRetain.Checked) {
          Paste(copytsigskeys: false, replace: true, nullify: false);
        }
      }
    }

    //private void cmdPasteAll_Click(object sender, EventArgs e) {
    //  if (!ValidatePaste()) return;
    //  using (new clsWaitCursor()) {
    //    Paste(copytsigskeys: true, replace: false, nullify: false);
    //  }
    //}

    //private void cmdPasteChords_Click(object sender, EventArgs e) {
    //  Cursor.Current = Cursors.WaitCursor;
    //  Paste(false, replace: false, nullify: false);
    //  Cursor.Current = Cursors.Default;
    //}

    //private void cmdReplaceChords_Click(object sender, EventArgs e) {
    //  Cursor.Current = Cursors.WaitCursor;
    //  Paste(false, replace: true, nullify: false);
    //  Cursor.Current = Cursors.Default;
    //}

    private void Paste(bool copytsigskeys, bool replace, bool nullify) {
      int csrtickslo = PixToTicks(CsrPixLo);
      int csrtickshi = PixToTicks(CsrPixHi);

      //* validate
      int totticks = P.F.MaxBBT.Ticks + Buff_Copy.Count - (csrtickshi - csrtickslo);
      if (totticks >= P.F.MaxBBT.MaxNoteMapTicks) {
        MessageBox.Show("Paste terminated - max ticks exceeded");
        return;
      }

      //* copy notemap to Buff_Paste
      Buff_Paste = new List<clsBuffQI>();
      LiveToBuff(Buff_Paste, 0, P.F.MaxBBT.QI, indchords: true, indtsigs: copytsigskeys, indkeys: copytsigskeys);

      //* replace or remove/insert selected area
      int csrqilo = csrtickslo / P.F.TicksPerQI;
      int csrqihi = csrtickshi / P.F.TicksPerQI;
      if (replace) {
        int qfrom = 0;
        for (int qto = csrqilo; qto < csrqihi; qto++) {
          if (qfrom >= Buff_Copy.Count) {
            if (!nullify) break;
            Buff_Paste[qto].Chord = 0;
            Buff_Paste[qto].ChordAtt = new clsNoteMapCF.sChordAtt(0);
          } else {
            Buff_Paste[qto] = Buff_Copy[qfrom];
          }
          qfrom++;
        }
      } else {  //remove/insert
        //* remove selected area
        Buff_Paste.RemoveRange(csrqilo, csrqihi - csrqilo);
        CsrPixHi = CsrPixLo;
        //* copy copybuffer to paste buffer
        Buff_Paste.InsertRange(csrqilo, Buff_Copy);
      }

      BuffToLive(Buff_Paste, indchords: true, indtsigs: copytsigskeys, indkeys: copytsigskeys);
    }

    internal bool NoScroll = false;
    private void BuffToLive(List<clsBuffQI> buff, bool indchords, bool indtsigs, bool indkeys) {
      using (new clsChordMapDis(this)) {
        if (buff.Count == 0) {
          buff.Add(new clsBuffQI());
          buff[0].TSig = new clsMTime.clsTSig(P.F.MTime, 4, 4);  //default 4/4
          buff[0].Key = new clsKeyTicks("C", "major", 0);  //default
        }
        if (indtsigs) {
          //* validate and create tsigs[]
          List<clsMTime.clsTSigBB> tsigbblist = new List<clsMTime.clsTSigBB>();
          clsMTime.clsTSigBB tsigbbprev = null;
          for (int q = 0; q < buff.Count; q++) {
            if (q >= P.F.MaxBBT.QI) break;
            clsMTime.clsTSig tsig = buff[q].TSig;
            if (tsig.IsEquiv(tsigbbprev)) continue;
            clsMTime.clsTSigBB tsigbb = null;
            try {
              tsigbb = new clsMTime.clsTSigBB(tsig, tsigbbprev, q * P.F.TicksPerQI);
            }
            catch (TSigException) {
              MessageBox.Show("Paste: Time Signature Exception");
              NoRefreshDGV = false;
              return;
            }
            tsigbblist.Add(tsigbb);
            tsigbbprev = tsigbb;
          }
          P.F.MTime.TSigs = tsigbblist.ToArray();
          NewMTime();
        }

          //* create keys
        if (indkeys) {
          List<clsKeyTicks> keytickslist = new List<clsKeyTicks>();
          clsKeyTicks keyticksprev = null;
          for (int q = 0; q < buff.Count; q++) {
            if (q >= P.F.MaxBBT.QI) break;
            clsKey key = buff[q].Key;
            if (key.IsEquiv(keyticksprev)) continue;
            clsKeyTicks keyticks = null;
            keyticks = new clsKeyTicks(key, q * P.F.TicksPerQI);
            keytickslist.Add(keyticks);
            keyticksprev = keyticks;
          }
          P.F.SetKeys(new clsKeysTicks(keytickslist));
        }

        if (indchords) {
          P.F.CF.NoteMap.NewMapAndAtts();
          for (int q = 0; q < buff.Count; q++) {
            //if (q >= P.F.MaxBBT.MaxNoteMapQI) break;
            if (q >= P.F.MaxBBT.QI) break;
            P.F.CF.NoteMap.SetMap(q, buff[q].Chord);
            P.F.CF.NoteMap.SetChordAtt(q, buff[q].ChordAtt);
          }
        }
      }
      //NoScroll = false;
      //currentbbt.RoundToBar();
      //Forms.frmStart.RefreshBBT(currentbbt);
      //Refresh();
      //NoRefreshDGV = false;
      SetNoteMapFileChanged();
      if (indtsigs) P.frmSC.SetEndBBTRefresh();  //recalc EndBBT
    }

    private void cmdTest_Click(object sender, EventArgs e) {
      List<clsBuffQI> bufflist = new List<clsBuffQI>();
      int[] bararr = new int[] { 30, 31, 32, 14, 15, 16, 17 };  //bar(0)
      LiveToBuff(bufflist, bararr.ToList(), indchords: true, indtsigs: true, indkeys: true);
      BuffToLive(bufflist, true, true, true);
    }

    private static void LiveToBuff(List<clsBuffQI> bufflist, List<int> barlist, 
      bool indchords, bool indtsigs, bool indkeys) {  //used by cmdTest (only?)
      foreach (int bar in barlist) {
        clsMTime.clsBBT bbtlo = new clsMTime.clsBBT(bar, 0, 0);
        int qilo = bbtlo.Ticks / P.F.TicksPerQI;
        int qihi = qilo + bbtlo.TicksPerBar / P.F.TicksPerQI;
        LiveToBuff(bufflist, qilo, qihi, indchords, indtsigs, indkeys);
      }
    }

    private static void LiveToBuff(List<clsBuffQI> bufflist, int qilo, int qihi, 
      bool indchords = false, bool indtsigs = false, bool indkeys = false) {
      //* null tsig: get tsig at tickslo
      int tickslo = qilo * P.F.TicksPerQI;
      int itsig;
      P.F.MTime.FindTSigTick(tickslo, out itsig);
      clsMTime.clsTSigBB[] tsigs = P.F.MTime.TSigs;

      int ikey = P.F.Keys.GetIndex(tickslo);
      clsKeysTicks keys = P.F.Keys;

      for (int q = qilo; q < qihi; q++) {
        int ticks = q * P.F.TicksPerQI;
        clsBuffQI buffqi = new clsBuffQI();

        if (indchords) {
          buffqi.Chord = P.F.CF.NoteMap.GetMap(q);
          buffqi.ChordAtt = P.F.CF.NoteMap.GetChordAtt(q);
        }

        if (indtsigs) {
          if (itsig == tsigs.Length - 1 || tsigs[itsig + 1].Tick > ticks) {
            buffqi.TSig = tsigs[itsig];
          } else {
            buffqi.TSig = P.F.MTime.FindTSigTick(ticks, out itsig);
          }
        }

        if (indkeys) {
          if (ikey == keys.Keys.Count - 1 || keys.Keys[ikey + 1].Ticks > ticks) {
            buffqi.Key = keys.Keys[ikey];
          } else {
            buffqi.Key = keys.Keys[P.F.Keys.GetIndex(ticks)];
          }
        }

        bufflist.Add(buffqi);
      }
    }

    private void cmdDeleteBars_Click(object sender, EventArgs e) {
      if (CsrPixHi <= CsrPixLo) {
        MessageBox.Show("Selection Area is null - nothing selected!");
        return;
      }
      string msg = "Are you sure you want to delete all bars in the Selected Area?";
      if (MessageBox.Show(msg, MessageBoxButtons.YesNo) == DialogResult.No) return;
      using (new clsWaitCursor()) {
        DeleteBars(copytsigskeys: true);
      }
    }

    internal int GetPixNextBar(int pix) {
      int bar = PixToBars(pix);
      if (bar < 0 || bar >= P.F.MaxBBT.Bar) return pix;
      return BarsToPix(bar + 1);
    }

    private void DeleteBars(bool copytsigskeys) {
      int csrtickslo = PixToTicks(CsrPixLo);
      int csrtickshi = PixToTicks(CsrPixHi);

      //* validate
      if (CsrPixLo >= CsrPixHi) return;
      if (csrtickslo == 0 && csrtickshi >= P.F.MaxBBT.Ticks - 1) {
        MessageBox.Show("'Select All' invalid for this function");
        return;
      }

      //* copy notemap/tsigs/keys to buffer
      List<clsBuffQI> buff_all = new List<clsBuffQI>();
      LiveToBuff(buff_all, 0, P.F.MaxBBT.QI, indchords: true, indtsigs: copytsigskeys, indkeys: copytsigskeys);

      if (buff_all.Count == 0) {
        MessageBox.Show("Selection Area invalid for this function");
        return;
      }

      //* remove selected area
      int csrqilo = csrtickslo / P.F.TicksPerQI;
      int csrqihi = csrtickshi / P.F.TicksPerQI;
      buff_all.RemoveRange(csrqilo, csrqihi - csrqilo);
      //CsrPixHi = GetPixNextBar(CsrPixLo);  //selected area length one bar (or zero if at end)
      CsrPixHi = CsrPixLo;
      //* make live
      BuffToLive(buff_all, indchords: true, indtsigs: copytsigskeys, indkeys: copytsigskeys);
    }

    private void cmdInsertBars_Click(object sender, EventArgs e) {
      if (CsrPixHi <= CsrPixLo) {
        MessageBox.Show("Selection Area is null - nothing selected!");
        return;
      }
      int barlo = PixToBars(CsrPixLo);
      int barhi = PixToBars(CsrPixHi);
      //if (barlo < 0 || barhi < 0 || barlo >= barhi) {
      //  MessageBox.Show("Insert Bars terminated - Selection area is invalid");
      //  return;
      //}
      Forms.dlgInsertBars frm = new dlgInsertBars(this, barlo, barhi);
      if (frm.ShowDialog() == DialogResult.Cancel) return;

      using (new clsWaitCursor()) {
        InsertBars(frm.TSig);
      }
    }

    private void InsertBars(clsMTime.clsTSig tsig) {  //insert null bars
      int csrtickslo = PixToTicks(CsrPixLo);
      int csrtickshi = PixToTicks(CsrPixHi);
      int barlo = PixToBars(CsrPixLo);
      int barhi = PixToBars(CsrPixHi);

      //* validate
      if (CsrPixLo >= CsrPixHi) return;

      //* copy notemap/tsigs/keys to buffer
      List<clsBuffQI> buff_all = new List<clsBuffQI>();
      LiveToBuff(buff_all, 0, P.F.MaxBBT.QI, indchords: true, indtsigs: true, indkeys: true);

      //* copy null bars to buffer
      int csrqilo = csrtickslo / P.F.TicksPerQI;
      int csrqihi = csrtickshi / P.F.TicksPerQI;
      List<clsBuffQI> buff_null = new List<clsBuffQI>();
      clsBuffQI nullqi = new clsBuffQI();
      nullqi.Chord = 0;
      nullqi.ChordAtt = new clsNoteMapCF.sChordAtt(0);
      nullqi.Key = P.F.Keys[csrtickslo];
      //nullqi.TSig = P.F.MTime.GetTSig(csrtickslo);
      nullqi.TSig = tsig;
      int qilen = (tsig.TicksPerBar * (barhi - barlo)) / P.F.TicksPerQI;
      for (int q = csrqilo; q < csrqilo + qilen; q++) {
        buff_null.Add(nullqi);
      }

      //* copy null buffer to all buffer
      buff_all.InsertRange(csrqilo, buff_null);

      //* make live
      //CsrPixHi = GetPixNextBar(CsrPixLo);  //selected area length zero
      CsrPixHi = CsrPixLo;
      BuffToLive(buff_all, indchords: true, indtsigs: true, indkeys: true);
    }

    //private void cmdPaste_Click(object sender, EventArgs e) {
    //  //* delete selected area (may be null) and insert data at start of selected area (csrpixlo)

    //  //* validate
    //  if (Copy_BarsLo < 0 || Copy_BarsHi < 0) return;
    //  if (CsrPixLo > CsrPixHi) return;  //can be equal

    //  int qicopylo = new clsMTime.clsBBT(Copy_BarsLo, 0, 0).Ticks / P.F.TicksPerQI;
    //  int qicopyhi = new clsMTime.clsBBT(Copy_BarsHi, 0, 0).Ticks / P.F.TicksPerQI;
    //  int diffdelqi = qicopyhi - qicopylo;

    //  int qicsrlo = PixToTicks(CsrPixLo) / P.F.TicksPerQI;
    //  int qicsrhi = PixToTicks(CsrPixHi) / P.F.TicksPerQI;
    //  int diffaddqi = qicsrhi - qicsrlo;

    //  if (P.F.MaxBBT.QI + (diffaddqi - diffdelqi) >= P.F._MaxMidiTicks / P.F.TicksPerQI) {
    //    MessageBox.Show("Paste terminated - Max Bars exceeded");
    //    return;
    //  }

    //  //* delete selected area
    //  for (int qfrom = qicsrhi, qto = qicsrlo; qfrom < P.F.MaxBBT.QI; qfrom++, qto++) {
    //    int ticksfrom = qfrom * P.F.TicksPerQI;
    //    int ticksto = qto * P.F.TicksPerQI;

    //    //* copy notemap elements
    //    P.F.CF.NoteMap[qto] = P.F.CF.NoteMap[qfrom];
    //    P.F.CF.NoteMap.SetChordAtt(qto, P.F.CF.NoteMap.GetChordAtt(qfrom));

    //    //* copy tsig
    //    clsMTime.clsTSigBB tsigfrom = P.F.MTime.GetTSig(ticksfrom);
    //    clsMTime.clsTSigBB tsigto = P.F.MTime.GetTSig(ticksto);
    //    if (!tsigfrom.IsEquiv(tsigto)) {
    //      clsMTime.clsBBT bbt = new clsMTime.clsBBT(qto + P.F.TicksPerQI);
    //      if (bbt.TicksRemBar != 0) {
    //        MessageBox.Show("Paste terminated - invalid data");
    //        return;
    //      }
    //      P.F.MTime.AddTSigTruncate(tsigfrom.NN, tsigfrom.DD, new clsMTime.clsBBT(tsigto.Tick));
    //    }

    //    //* copy key
    //    clsKeyTicks keyfrom = P.F.Keys[ticksfrom];
    //    clsKeyTicks keyto = P.F.Keys[ticksto];
    //    if (!keyfrom.IsEquiv(keyto)) {
    //      clsKeyTicks newkeyto = new clsKeyTicks(keyfrom);
    //      newkeyto.Ticks = ticksto;
    //      P.F.Keys.Insert(newkeyto);
    //    }

    //  }
    //  //* finalize and  create evs and update undo
    //  NewMTime();
    //  TransposeMidi(0);  //to force enharmonic changes (spelling) -> SetNoteMapFileChanged();
    //}


    //private void cmdCopy_Click(object sender, EventArgs e) {
    //  if (CsrPixLo == CsrPixHi) return;
    //  //* data format:
    //  //* #CCC startbeat endbeat qualifier root note ... (note = non-root note)
    //  //* #CCC startbeat endbeat *         note note ... (no root or qualifier)
    //  //* ...

    //  StringBuilder data = new StringBuilder();
    //  int tickslo = PixToTicks(CsrPixLo);
    //  int tickshi = PixToTicks(CsrPixHi);
    //  int beatshi = (new clsMTime.clsBBT(tickshi)).Beats;
    //  clsMTime.clsBBT bbtlo = PixToBBT(CsrPixLo);
    //  //clsMTime.clsBBT bbthi = PixToBBT(CsrPixHi);
    //  int index = P.F.CF.FindCFEv(bbtlo);
    //  clsCF.clsEv ev = P.F.CF.Evs[index];
    //  while (ev.OnTime >= tickslo) {
    //    if (ev.OnTime >= tickshi) break;
    //    data.Append("#CCC ");
    //    data.Append(ev.OnBBT.Beats.ToString() + ' ');
    //    data.Append(Math.Min(beatshi, ev.OffBBT.Beats).ToString() + ' ');
    //    if (ev.Root && !String.IsNullOrEmpty(ev.ChordQualifier)) data.Append(ev.ChordQualifier + ' ');
    //    else data.Append("* ");
    //    foreach (clsCF.clsEv.clsNote note in ev.Notes) {
    //      data.Append(note.PC[eKBTrans.None].ToString() + ' ');
    //    }
    //    data.AppendLine();
    //    if (++index >= P.F.CF.Evs.Count) break;
    //    ev = P.F.CF.Evs[index];
    //  }
    //  Clipboard.SetText(data.ToString());
    //}

    //private void cmdPaste_Click(object sender, EventArgs e) {
    //  string msg = Paste();
    //  if (msg != "") MessageBox.Show("Paste terminated: " + msg);
    //}

    //private string Paste() {
    //  //* check if valid paste
    //  //* create arrays and tables
    //  //* 0='#CCC', 1=startbeat, 2=endbeat, 3=qualifier|'*', 4=note(root), 5=note, ...
    //  string text = Clipboard.GetText();
    //  if (String.IsNullOrEmpty(text)) return "empty clipboard data";

    //  string[] textarray = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
    //  int len = textarray.Length;
    //  if (len < 1) return "too few lines";

    //  int[] startbeatarray = new int[len];
    //  int[] endbeatarray = new int[len];
    //  string[] qualifierarray = new string[len];  //not currently used
    //  int[][] notetable = new int[len][];
    //  bool[][] chordtable = new bool[len][];
    //  char[] delimspace = new char[] { ' ' };

    //  for (int i = 0; i < len; i++) {
    //    string[] f = textarray[i].Split(delimspace, StringSplitOptions.RemoveEmptyEntries);
    //    if (f.Length < 5) return "line too short at or around line " + i;
    //    if (f[0] != "#CCC") return "#CCC id mssing at or near line " + i;
    //    if (!int.TryParse(f[1], out startbeatarray[i])) return "non-integer startbeat found at or around line: " + i;
    //    if (!int.TryParse(f[2], out endbeatarray[i])) return "non-integer endbeat found at or around line: " + i;
    //    if (startbeatarray[i] > endbeatarray[i]) return "startbeat/endbeat sequence error";
    //    if (i > 0 && startbeatarray[i] != endbeatarray[i - 1]) return "inconsistent startbeat/endbeat";
    //    qualifierarray[i] = f[3];
    //    chordtable[i] = new bool[12];
    //    notetable[i] = new int[f.Length - 4];
    //    for (int j = 4; j < f.Length; j++) {
    //      int pc;
    //      if (!int.TryParse(f[j], out pc)) return "non-integer pc found at or around line: " + i;
    //      if (pc < 0 || pc > 11) return "pitchclass out of range at or near line " + i;
    //      chordtable[i][pc] = true;
    //      notetable[i][j - 4] = pc;
    //    }
    //  }

    //  //* insert new chords
    //  //P.F.CF.UndoRedoCF.StartEvs();
    //  int offset = P.F.CurrentBBT.Beats - startbeatarray[0];
    //  for (int i = 0; i < len; i++) {
    //    for (int outbeat = startbeatarray[i] + offset; outbeat < endbeatarray[i] + offset; outbeat++) {
    //      if (outbeat >= P.F.MaxBBT.Beats) break;
    //      clsMTime.clsBBT outbbt = new clsMTime.clsBBT(outbeat, true);
    //      int outqilo = outbbt.Ticks / P.F.TicksPerQI;  //start of beat
    //      int outqihi = outqilo + outbbt.TicksPerBeat / P.F.TicksPerQI;  //end of beat
    //      for (int qi = outqilo; qi < outqihi; qi++) {
    //        //chordtable[i] = new bool[12];  //temp
    //        P.F.CF.NoteMap.SetChordAndAtts(qi, chordtable[i]);  
    //      }
    //    }
    //  }

    //  SetNoteMapFileChanged();
    //  return "";

    //* create hole to contain new chords
    //int beatlo = P.F.CurrentBBT.Beats;
    //int beathi = beatlo + offset;
    //clsMTime.clsBBT bbtlo = new clsMTime.clsBBT(beatlo, true);
    //clsMTime.clsBBT bbthi = new clsMTime.clsBBT(beathi, true);
    //int qilo = bbtlo.Ticks / P.F.TicksPerQI;
    //int qihi = bbthi.Ticks / P.F.TicksPerQI;
    //for (int qi = qilo; qi < qihi; qi++) P.F.CF.NoteMap.RemoveNotes(qi);

    //List<clsCF.clsEv> evs = P.F.CF.Evs.ToList();
    //int ihi = P.F.CF.FindCFEv(bbthi);  //prev if no hit
    //int ilo = P.F.CF.FindCFEv(P.F.CurrentBBT);  //prev if no hit
    //if (evs[ihi].OnBBT.Beats == beathi) evs.RemoveRange(ilo, ihi - 1);
    //else evs.RemoveRange(ilo, ihi);

    ////* insert new chords
    //List<clsCF.clsEv> newevs = new List<clsCF.clsEv>();
    //for (int i = 0; i < len; i++) {
    //  bool[] chord = new bool[12];
    //  foreach (int pc in notetable[i]) {
    //    if (pc < 0 || pc > 11) return "pitchclass out of range at or near line " + i;
    //    chord[pc] = true;
    //  }
    //  clsMTime.clsBBT onbbt = new clsMTime.clsBBT(startbeatarray[i], true);
    //  clsMTime.clsBBT offbbt = new clsMTime.clsBBT(endbeatarray[i], true);
    //  clsCF.clsEv newev = new clsCF.clsEvPC(P.F.CF, onbbt, chord, null);
    //  newev.OffBBT = offbbt;
    //  //newevs.Add(new clsCF.clsEvPC(P.F.CF, onbbt, chord, null)); 
    //  if (qualifierarray[i] == "*") {
    //    newev.Root = false;
    //  } else {
    //    newev.Root = true;
    //    newev.ChordQualifier = qualifierarray[i];
    //  }
    //  newevs.Add(newev);
    //}
    //evs.InsertRange(ilo, newevs);
    //P.F.CF.Evs = evs;
    //P.frmSC.NewPlay();
    //P.F.CF.SyncEvsToKeys();
    //P.F.CF.CreateNoteMap();
    //P.F.CF.NoteMap.DelimitUndo();
    //P.F.CF.indChanged = true;
    //RefreshDGV();
    ////P.frmSC.ReInitPlayMode();  //to allow for transposition, ...
    //P.frmSC.Refresh();
    //return "";
    //}

    private void frmChordMap_KeyDown(object sender, KeyEventArgs e) {
      if (txtCHPDesc.Focused) return;  //allow for pasting into Desc
      if (e.Control) {
        switch (e.KeyCode) {
          case Keys.A:
            cmdSelectAll_Click(null, null);
            break;
          case Keys.C:
            cmdCopy_Click(null, null);
            break;
          case Keys.D:
            cmdSelectNone_Click(null, null);
            break;
          case Keys.V:
            cmdPasteSpecial_Click(null, null);
            break;
          case Keys.X:
            cmdCut_Click(null, null);
            break;
          case Keys.Y:
            if (!cmdRedo.Enabled) return;
            cmdRedo_Click(null, null);
            break;
          case Keys.Z:
            if (!cmdUndo.Enabled) return;
            cmdUndo_Click(null, null);
            break;
        }
      }
    }

    //private int TransKeysValPrev = 0;
    //private void nudTransposeKeys_ValueChanged(object sender, EventArgs e) {
    //  int val = (int)nudTransposeKeys.Value;
    //  int diff = val - TransKeysValPrev;
    //  for (int i = 0; i < P.F.Keys.Keys.Count; i++) {
    //    clsKeyTicks oldkey = P.F.Keys.Keys[i];
    //    clsKeyTicks newkey = new clsKeyTicks((oldkey.KeyNote + diff).Mod12(), oldkey.Scale, oldkey.Ticks, true);
    //    P.F.Keys.Keys[i] = newkey;
    //  }
    //  TransKeysValPrev = val;
    //  SetNoteMapFileChanged();
    //}

    //private void cmdTransposeAllPos_Click(object sender, EventArgs e) {
    //  if ((int)nudTransposeChordNames.Value >= 11) return;
    //  if ((int)nudTransposeChordNotes.Value >= 11) return;
    //  if ((int)nudTransposeKeys.Value >= 11) return;
    //  nudTransposeChordNames.Value += 1;
    //  nudTransposeChordNotes.Value += 1;
    //  nudTransposeKeys.Value += 1;
    //}

    //private void cmdTransposeAllNeg_Click(object sender, EventArgs e) {
    //  if ((int)nudTransposeChordNames.Value <= -11) return;
    //  if ((int)nudTransposeChordNotes.Value <= -11) return;
    //  if ((int)nudTransposeKeys.Value <= -11) return;
    //  nudTransposeChordNames.Value -= 1;
    //  nudTransposeChordNotes.Value -= 1;
    //  nudTransposeKeys.Value -= 1;
    //}

    //private void cmdInsertBar_Click(object sender, EventArgs e) {
    //  //* insert bar, without changing overall length
    //  //* last bar of original data will be lost
    //  clsMTime.clsBBT bbt = new clsMTime.clsBBT(P.F.CurrentBBT.Bar, 0, 0);
    //  int qistart = bbt.Ticks / P.F.TicksPerQI;
    //  int qibar = bbt.TicksPerBar / P.F.TicksPerQI;  //same barlen to be moved
    //  int qilen = P.F.MaxBBT.QI;
    //  ushort[] map = new ushort[qilen];
    //  clsNoteMapCF.sChordAtt[] chordatt = new clsNoteMapCF.sChordAtt[qilen];

    //  for (int qi = P.F.MaxBBT.QI - 1; qi >= qistart; qi--) {
    //    int qito = qi - qibar;
    //    if (qito < 0) break;
    //    P.F.CF.NoteMap.SetMap(qi, P.F.CF.NoteMap.GetMap(qito));
    //    P.F.CF.NoteMap.SetChordAtt(qi, P.F.CF.NoteMap.GetChordAtt(qito));
    //  }

    //  SetNoteMapFileChanged(undoredo: true, indqi: true);
    //}

    //private void cmdDeleteBar_Click(object sender, EventArgs e) {
    //  //* set deleted bar to a null chord
    //  clsMTime.clsBBT bbt = new clsMTime.clsBBT(P.F.CurrentBBT.Bar, 0, 0);
    //  int qilo = bbt.Ticks / P.F.TicksPerQI;
    //  int qihi = bbt.GetNextBar().Ticks / P.F.TicksPerQI;
    //  bool[] nochord = new bool[12];
    //  bool changed = false;
    //  for (int q = qilo; q < qihi; q++) {
    //    if (P.F.CF.NoteMap[q] == nochord) continue;
    //    P.F.CF.NoteMap[q] = nochord;
    //    P.F.CF.NoteMap.SetChordAtt(q, new clsNoteMapCF.sChordAtt(0));
    //    changed = true;
    //  }
    //  if (changed) P.F.CF.CreateEvs();

    //  //* delete bar hole
    //  int index = P.F.CF.FindCFEv(bbt);  //match or previous (currentbbt)
    //  //* get match or next
    //  if (P.F.CF.Evs[index].OnBBT.Ticks != bbt.Ticks || P.F.CF.Evs[index].Notes.Length != 0) {
    //    LogicError.Throw(eLogicError.X151);  //should be null chord created above
    //    return;
    //  }
    //  P.F.CF.Evs.RemoveAt(index);  //remove null chord

    //  int ticks = P.F.CF.Evs[index].OnBBT.TicksPerBar;  //delete barlength as at deletion point
    //  for (int i = index; i < P.F.CF.Evs.Count; i++) {  
    //    if (!P.F.CF.Evs[i].SubTicks(ticks)) {
    //      LogicError.Throw(eLogicError.X150);
    //      return;
    //    }
    //  }
    //  SetNoteMapFileChanged(undoredo: true, indqi: false);  
    //}

    private void cmdShowSumm_Click(object sender, EventArgs e) {
      P.CloseFrm(P.F.frmSummary);
      P.F.frmSummary = new frmSummary();
      //P.F.frmSummary.Show();
      Utils.FormAct(P.F.frmSummary);
    }

    private void dgvLyrics_VisibleChanged(object sender, EventArgs e) {
      VResize();
    }

    private void cmdNew_Click(object sender, EventArgs e) {
      P.frmSC.mnuNew_Click(null, null);
    }

    private void cmdLoadProject_Click(object sender, EventArgs e) {
      P.frmSC.LoadProjectClick();
    }

    private void cmdSaveProject_Click(object sender, EventArgs e) {
      P.frmSC.mnuSaveProject_Click(null, null);
    }

    private void cmdSaveProjectAs_Click(object sender, EventArgs e) {
      P.frmSC.mnuSaveProjectAs_Click(null, null);
    }

    //private void cmdPlayMap_Click(object sender, EventArgs e) {
    //  Utils.FormAct(P.frmSC);
    //}

    private void cmdMultiMap_Click(object sender, EventArgs e) {
      Forms.frmSC.ShowMultiMap();
      //P.frmSC.cmbPlayStyle_SelectedIndexChanged(null, null);
    }

    private void cmdTonnetz_Click(object sender, EventArgs e) {
      P.frmSC.cmdTonnetz_Click(null, null);
    }

    private void cmdCalcKeys_Click(object sender, EventArgs e) {
      CalcKeys();
    }

    internal static void CalcKeys() {
      //if (P.F.Project.MidiExists) {
      //  string msg = "CalcKeys from ChordFile: This may be less accurate than invoking CalcKeys from the TrackMap, which uses the MidiFile.";
      //  msg += "\r\nClick 'Yes' to continue.";
      //  if (MessageBox.Show(msg, MessageBoxButtons.YesNo) == DialogResult.No) return;
      //}

      bool indmidi = P.F.Project.MidiExists;
      bool indchord = (P.F.CF?.Evs != null && P.F.CF.Evs.Count != 0);
      clsTrks.Array<bool> trksselectall = new clsTrks.Array<bool>(true);  //all trks
      clsTrks.Array<bool> trksselectnone = new clsTrks.Array<bool>(false);  //no trks
      clsTrks.Array<bool> trksselected = P.F.frmTrackMap?.GetSelectedTrks();
      if (trksselected == null) trksselected = trksselectnone;
      bool indselected =   //true if not all trks and not no trks
        (!trksselected.SequenceEqual(trksselectnone) 
        && !trksselected.SequenceEqual(trksselectall)); 

      if (!indmidi) {
        if (!indchord) {
          MessageBox.Show("MidiFile and ChordFile missing or empty");
        } else {
          P.F.frmCalcKeys = new frmCalcKeys();  //using ChordFile
        }
      } else {  //indmidi
        if (!indchord && !indselected) {  
          P.F.frmCalcKeys = new frmCalcKeys(trksselectall);
        } else {
          Forms.dlgCalcKeysOpts dlg = new dlgCalcKeysOpts();
          dlg.optMidiFileSelected.Enabled = indselected;
          dlg.optChordFile.Enabled = indchord;
          if (dlg.ShowDialog() == DialogResult.Cancel) return;
          P.CloseFrm(P.F.frmCalcKeys);
          using (new clsWaitCursor()) {
            if (dlg.optChordFile.Checked) {
              P.F.frmCalcKeys = new frmCalcKeys();  //using ChordFile
            } else if (dlg.optMidiFileSelected.Checked) {  //only possible if enabled
              clsTrks.Array<bool> trkselect = trksselected;
              P.F.frmCalcKeys = new frmCalcKeys(trkselect);
            } else {
              P.F.frmCalcKeys = new frmCalcKeys(trksselectall);
            }
          }
        }
      }
    }

    private void cmdSaveProject_MouseUp(object sender, MouseEventArgs e) {
      if (e.Button == MouseButtons.Right) {  //Click not raised
        P.F.SaveProject(null);  //check and save
      }
    }

    //private void frmChordMap_Deactivate(object sender, EventArgs e) {
    //  //Debug.WriteLine("frmChordMap: Deactivate: indscrollcmd = " + indAutoScrollCmd + " autoscrollposx = " + panNoteMap.AutoScrollPosition.X);
    //  if (indAutoScrollCmd && panNoteMap_AutoScrollPosX.HasValue) {
    //    panNoteMap.AutoScrollPosition = new Point(panNoteMap_AutoScrollPosX.Value, 0);
    //  }
    //  indAutoScrollCmd = false;
    //  panMain_AutoScrollPosY = panMain.AutoScrollPosition.Y;
    //}

    //private void frmChordMap_Activated(object sender, EventArgs e) {
    //  //Debug.WriteLine("frmChordMap: Activate: autoscrollposx = " + panNoteMap_AutoScrollPosX);
    //  if (panMain_AutoScrollPosY != null) panMain.AutoScrollPosition = new Point(0, -panMain_AutoScrollPosY.Value);
    //  //if (panNoteMap_AutoScrollPosX != null) panNoteMap.AutoScrollPosition = new Point(-panNoteMap_AutoScrollPosX.Value, 0);
    //}

    private void frmNoteMap_DragDrop(object sender, DragEventArgs e) {
      P.frmStart.frmStart_DragDrop(sender, e);
    }

    private void DGV_KeyDown(object sender, KeyEventArgs e) {
      if ((e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) && DGV.CurrentCell.Selected) {
        DGV.CurrentCell.Value = null;
        e.Handled = true;
      }
    }

    private void mnuChangeLength_Click(object sender, EventArgs e) {
      if (P.F.CF?.Evs == null) return;
      Forms.dlgChangeSongLength frm = new Forms.dlgChangeSongLength();
      frm.ShowDialog();
    }

    private void DGV_ColumnAdded(object sender, DataGridViewColumnEventArgs e) {
      e.Column.FillWeight = 10;  //to get round column limit (totalfillweight < 65535) default 100
    }

    private void dgvLyrics_ColumnAdded(object sender, DataGridViewColumnEventArgs e) {
      e.Column.FillWeight = 10;  //to get round column limit (totalfillweight < 65535) default 100
    }

    private void txtCHPDesc_TextChanged(object sender, EventArgs e) {
      if (P.F.CF != null) P.F.CF.indSave = true;
    }

    private void mnuTSigHelp_Click(object sender, EventArgs e) {
      Utils.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "HowTo_UpdateTSigs.htm");
    }

    private void frmNoteMap_DragEnter(object sender, DragEventArgs e) {
      P.frmStart.frmStart_DragEnter(sender, e);
    }

    internal int AlignCsrToBar(int csrpix) {
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(PixToTicks(csrpix));
      bbt.RoundToBar();
      return TicksToPix(bbt.Ticks);
    }
  }

  internal interface IShowNoteMap {
    int HFactor { get; }
    int VFactor { get; }
    int HDiv { get; }
    clsFileStream FileStream { get; }
    bool ShowBeats { get; }
    bool OneOct { get; }
    bool ShowKB { get; }
    Font BarFont { get; }
    int GetPixPerNoteInt();
    int nudHeightVal { get; }
    int GetTrkHeight(int pixpernote);
    int Octaves { get; }
    int MinC { get; }
  }

  internal static class clsIShowNoteMap {
    internal static void InvalidatePic(Forms.frmTrackMap frmmm, PictureBox pic, clsPicNoteMap picnm, int csrpos, int csrposlast, int height) {
      picnm.SetPicSize(frmmm);
      if (csrpos < 0 || csrposlast < 0 || csrpos <= csrposlast) {
        pic.Invalidate();  //redraw all visible area
      } else {  //redraw after csr move
        int xlo = Math.Max(0, csrposlast - 2);
        int xhi = csrpos + 1;
        pic.Invalidate(new Rectangle(xlo, height, xhi - xlo, pic.ClientSize.Height));
      }
    }

    internal static void SetPlayCsr(int ticks) {  //always align to bar
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
      bbt.RoundDownToBar();
      P.frmSC.Play?.NewReset();
      Forms.frmStart.RefreshBBT(bbt);
      P.F.StartBar = bbt.Bar;
      //if (P.frmSC != null) P.frmSC.nudStartBar.Value = P.F.StartBar + 1;
    }

    internal static void SetPlayCsr(clsMTime.clsBBT bbt) {  //assume bbt on bar
      P.F.CurrentBBT = bbt;
      P.F.StartBar = bbt.Bar;
      Forms.frmStart.RefreshBBT(bbt);
    }

    internal static void SetPlayCsrNoSetStartBar(clsMTime.clsBBT bbt) {  //assume bbt on bar
      P.F.CurrentBBT = bbt;
      Forms.frmStart.RefreshBBT(bbt);
    }
  }
}
