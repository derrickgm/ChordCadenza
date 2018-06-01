#undef Testing
#undef recordmidi

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
using System.Numerics;

namespace ChordCadenza.Forms {
  internal partial class frmTrackMap :
    Form, IFormPlayable, IFormStream, Forms.IFrmDGV, IFormProjectName, ITT {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    public void FormStreamOnOff(bool on) {
      panEdit.Enabled = !on;
      panMisc.Enabled = !on;
      panFiles.Enabled = !on;
      cmdGoToStart.Enabled = !on;
      foreach (clsTrks.T trk in ctlChans.Next) {
        if (ctlChans[trk] == null) continue;
        ctlChans[trk].Enabled = !on;
        if (Records[trk] != null) Records[trk].Enabled = !on;
      }
    }

    internal frmTrackMap() {
      bool indsavesave = (P.F.CF == null) ? false : P.F.CF.indSave;
      try {
        InitializeComponent();
        Forms.frmSC.ZZZSetPCKBEvs(this);

        VFactor = InitialVFactor;  //should be overridden later with Static...
        //VFactor = (chkEvenSpacing.Checked) ? 6 : 3; 
        //ExclCh9 = exclch9;
        //P.Forms.Add(this);
        //NumTrks = FileStream.NumTrks;
        Title = FileStream.Title;
        //OnCount = FileStream.OnCount;
        //ChanOnCount = FileStream.ChanOnCount;

        //if (chkShowChordNames.Checked) BeatChords = new clsBeatChords(this, DGV);
        InitTTTrk();
      }
      finally {
        if (P.F.CF != null) P.F.CF.indSave = indsavesave;
      }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
      bool? ret = Forms.frmSC.StaticProcessCmdKey(ref msg, keyData);
      if (!ret.HasValue) return base.ProcessCmdKey(ref msg, keyData);
      return ret.Value;
    }

    private int InitialVFactor {
      get {
        return (chkEvenSpacing.Checked) ? 8 : 3;
      }
    }

    //private static Rectangle Rect = new Rectangle(0, 0, 0, 0);
    //private Screen Scrn = null;
    private bool Bypass_DGV = false;
    private DataGridView NoSelectDGV = null;
    private bool Form_Loaded = false;
    private static bool ScrollFromRefreshBBT = false;
    internal static bool ScrollFromPan = false;
    //internal static bool ScrollFromDGV = false;
    internal static bool ScrollFromdgvLyrics = false;
    //private clsBeatChords BeatChords;

    internal static bool Static_ShowBeats = false;
    internal static bool Static_ShowKB = false;
    internal static bool Static_OneOct = false;
    internal static bool Static_EvenSpacing = false;
    internal static int Static_VFactor = 3;  //EvenSpacing false 
    //internal static int BarLabels = 1;
    internal bool Bypass_Event = false;
    //internal bool ExclCh9;
    private clsFileStream FileStream { get { return P.F.FSTrackMap; } }
    internal Font BarFont = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);

    //private int?[] YLoc;
    //private bool[] indShow;
    private clsTrks.Array<clsPicNoteMapMM> PicNoteMaps;
    private clsTrks.Array<PictureBox> Pics;
    internal clsTrks.Array<CheckBox> Chks;
    private clsTrks.Array<Panel> Pans;
    internal clsTrks.Array<CheckBox> chkMutes;
    private clsTrks.Array<CheckBox> Solos;
    private clsTrks.Array<CheckBox> Records;
    private clsTrks.Array<TrackBar> TrkBar;
    private clsTrks.Array<ComboBox> cmbPatches;
    private clsTrks.Array<Control> ctlChans;
    internal clsTrks.Array<TextBox> txtTitles;
    //internal clsTrks.Array<Label> lblTrkTypes;
    internal clsTrks.Array<CheckBox> chkCollapses;

    internal clsTrks.T RecTrk;
    internal int VFactor;  //pix/note...
    internal int HFactor = 1;  //pix/qi
    internal int HDiv = 1;
    private const int PicMargin = 5;
    //internal clsTrks.Array<int> Octaves;
    //internal clsTrks.Array<int> MinC;

    internal clsNoteMapMidi NoteMap { get { return FileStream.NoteMap; } }
    //internal int MaxQTime;
    //internal int NumTrks;
    //internal string FileName;
    //internal int QIdd;
    //internal int TicksPerQI;
    internal clsTrks.Array<string> Title;  //trk title
    //internal clsTrks.Array<int> OnCount;  //[trk]
    //internal clsTrks.Array<int[]> ChanOnCount;  //[trk, chan] -> [trk][chan]
    //internal int[] Channel;
    private int CsrPos = -1;
    private int LastCsrPos = -1;
    private const int ScrollMargin = 100;  //50
    internal clsTrks.Array<bool> TrkShow;
    private int CsrQILo = 0;
    private int CsrQIHi = 0;
    internal clsTrks.T MouseTrk;
    //public Button cmdRecSync { get { return cmdSyncAudio; } }
    internal clsUndoRedo Do;
    //private int DeleteNotesCount = 0;
    //internal bool indChanged = false;
    private const int picBarsKludge = 9; 

    internal void LoadDGVLyrics() {
      if (P.F.Lyrics.LyricsExist) {
        dgvLyrics.Show();
        lblLyricsLit.Show();
        P.F.Lyrics.InitDGV(this);
      } else {
        dgvLyrics.Hide();
        lblLyricsLit.Hide();
      }
    }

    public DataGridView Prop_dgvLyrics {
      get { return dgvLyrics; }
    }

    public int TicksToPix(int ticks) {
      //int tickslo = (CsrPixLo * HDiv * P.F.TicksPerQI) / HFactor;
      return (ticks * HFactor) / (HDiv * P.F.TicksPerQI);
    }

    public int TransposeChordNamesVal {
      get { return 0; }
    }

    //public int TransposeDisplayNotesVal {
    //  get { return 0; }
    //}

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


    internal void PicRefresh() {
      foreach (PictureBox pic in Pics) {
        if (pic == null) continue;
        PicNoteMaps[(clsTrks.T)pic.Tag].SetPicSize(this);
      }
      P.F.Lyrics.SetColumnWidth(this);
      LocatePics();
      Refresh();
    }

    private void frmMultiMap_Load(object sender, EventArgs e) {
      bool indsavesave = (P.F.CF == null) ? false : P.F.CF.indSave;
      try {
        BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
        #if !ADVANCED
          cmdTonnetz.Hide();
        #endif

        #if !recordmidi
          cmdAddTrack.Hide();
          cmdDeleteNotes.Hide();
          cmdUndo.Hide();
          cmdRedo.Hide();
        #endif

        clsTT.LoadToolTips(this);
        Do = new clsUndoRedo(this);
        P.F.FSTrackMap.CreateTrkTypes();

        Pics = new clsTrks.Array<PictureBox>();
        PicNoteMaps = new clsTrks.Array<clsPicNoteMapMM>();
        Chks = new clsTrks.Array<CheckBox>();
        chkMutes = new clsTrks.Array<CheckBox>();
        Solos = new clsTrks.Array<CheckBox>();
        Records = new clsTrks.Array<CheckBox>();
        Pans = new clsTrks.Array<Panel>();
        TrkBar = new clsTrks.Array<TrackBar>();
        cmbPatches = new clsTrks.Array<ComboBox>();
        ctlChans = new clsTrks.Array<Control>();
        //lblTrkTypes = new clsTrks.Array<Label>();
        txtTitles = new clsTrks.Array<TextBox>();
        chkCollapses = new clsTrks.Array<CheckBox>();

        //Octaves = new clsTrks.Array<int>();
        //MinC = new clsTrks.Array<int>();

        Bypass_Event = true;
        LoadStatic();
        Bypass_Event = false;
        VResize();

        //Bypass_Event = true;
        //nudBars.Value = BarLabels;
        //Bypass_Event = false;

        PlayableForms.CmdState_Set();

        //switch (P.F.QIPerNote) {
        //  case 16:
        //    HFactor = 2; HDiv = 1; break;
        //  case 32:
        //    HFactor = 1; HDiv = 1; break;
        //  case 64:
        //    HFactor = 1; HDiv = 2; break;
        //  default:
        //    LogicError.Throw(eLogicError.X036);
        //    HFactor = 1; HDiv = 1; break;
        //}

        if (P.F.QIPerNote <= 16) { HFactor = 2; HDiv = 1; } 
        else if (P.F.QIPerNote <= 32) { HFactor = 1; HDiv = 1; } 
        else if (P.F.QIPerNote <= 64) { HFactor = 1; HDiv = 2; } 
        else if (P.F.QIPerNote <= 128) { HFactor = 1; HDiv = 4; } 
        else if (P.F.QIPerNote <= 192) { HFactor = 1; HDiv = 6; } 
        else if (P.F.QIPerNote <= 256) { HFactor = 1; HDiv = 8; } 
        else { HFactor = 1; HDiv = 8; }

        splitContainer.Panel2.MouseWheel += new MouseEventHandler(splitContainer_Panel2_MouseWheel);
        splitContainer.Panel2.AutoScrollMargin = new Size(0, 20);

        SetFormTitle();

        //SetTrkSelect();
        TrkShow = new clsTrks.Array<bool>(true);
        foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {
          if (FileStream.TrkType[trk] == clsFileStream.eTrkType.Empty) TrkShow[trk] = false;
        }

        LoadTracks();
        cmdSaveProject.Enabled = P.F.SaveProject(null, true, false);
        //if (P.F.AutoSync != null) P.F.AutoSync.MP3Player.SetToolTips();
        Cfg.DictFormProps[Name].SetForm(this);
        //clsAudioSync.SetPlayAudioText(P.F?.AudioSync);
        chkShowLyrics_CheckedChanged(null, null);
        Form_Loaded = true;
        RefreshBBT(P.F.CurrentBBT);
      }
      finally {
        if (P.F.CF != null) P.F.CF.indSave = indsavesave;
      }
    }

    public void SetFormTitle() {  //interface IFormProjectName
      SetFormTitle(P.F.Project);
    }

    public void SetFormTitle(clsProject project) {  //interface IFormProjectName
      //int format = FileStream.LoadMidiFile.Format;
      //Text = "TRACKMAP   (F" + format + ") : " + project.MidiPath;
      Text = "TRACKMAP: " + project.MidiPath + " - Chord Cadenza";
    }

    private void LoadTracks() {
      //SetTrkSelect();
      lblTicks.Text = "0";
      splitContainer.Panel1.Controls.Clear();
      splitContainer.Panel2.Controls.Clear();
      TTTrks.RemoveAll();  //appears to be necessary for TTTrks to work!!!

      foreach (clsTrks.T trk in TrkShow.Next) {
        if (TrkShow[trk]) LoadTrackPicPan(trk);
      }

      LocatePics();
    }

    private void LoadTrack(clsTrks.T trk) {
      Point scroll = splitContainer.Panel2.AutoScrollPosition;
      //bool hit = false;
      //foreach (clsTrks.T t in P.F.Chan.Next) {
      //  if (t == trk) hit = true;
      //  if (!hit) continue;
      //  LoadTrackPicPan(t);
      //}
      LoadTrackPicPan(trk);
      LocatePics();
      if (trk.TrkNum == P.F.Trks.NumTrks - 1) {
        int max = splitContainer.Panel2.VerticalScroll.Maximum;
        splitContainer.Panel2.AutoScrollPosition = new Point(-scroll.X, max);
      } else {
        splitContainer.Panel2.AutoScrollPosition = new Point(-scroll.X, -scroll.Y);
      }
    }

    internal void LoadTrackPicPan(clsTrks.T trk) {
      int xloc = 5;
      if (Pics[trk] != null) {
        splitContainer.Panel1.Controls.Remove(Pans[trk]);
        splitContainer.Panel2.Controls.Remove(Pics[trk]);
      }
      //SetOctaves(trk);

      //* load picTrks onto panNoteMap    
      PictureBox pic = new PictureBox();
      //pic.Left = xloc;
      pic.Left = xloc + splitContainer.Panel2.AutoScrollPosition.X;
      pic.BorderStyle = BorderStyle.FixedSingle;
      pic.BackColor = Color.White;
      pic.Tag = trk;  //identifier
      pic.Paint += new PaintEventHandler(picTrk_Paint);
      pic.MouseClick += new MouseEventHandler(picTrk_MouseClick);
      pic.SizeMode = PictureBoxSizeMode.Normal;
      splitContainer.Panel2.Controls.Add(pic);
      PicNoteMaps[trk] = new clsPicNoteMapMM(this, pic, trk);
      Pics[trk] = pic;
      //pic.Refresh();   //not called if pic is below bottom of visible screen
      LoadTTTrk(pic, "Trk:pic");
      //int xloc = 5;  //X location of top picNoteMap
      if (Pans[trk] != null) splitContainer.Panel1.Controls.Remove(Pans[trk]);

      //* load multiple panControlTrk onto panControls 
      Panel pan = new Panel();
      //pan.Location = new Point(xloc, YLoc[trk].Value);
      pan.Left = xloc;
      //if (firsttrk && (int)nudBars.Value > 0) yloc += PicNoteMaps[trk].BarFont.Height;
      pan.BorderStyle = BorderStyle.FixedSingle;
      pan.BackColor = Color.White;
      pan.AutoScroll = false;
      pan.Tag = trk;  //identifier
      //pan.Height = GetTrkCtlHeight();
      //pan.Height = pic.Height;
      //pan.Height = 12 * VFactor;
      pan.Width = 300;
      //pan.Paint += new PaintEventHandler(panCtl_Paint);
      splitContainer.Panel1.Controls.Add(pan);
      Pans[trk] = pan;
      //pan.Refresh();   

      //* load select check boxes onto panControlTrks
      CheckBox chk = new CheckBox();
      chk.Checked = false;  //default
      //chk.Checked = !FileStream.indPitchBend[trk];  
      chk.Location = new Point(3, 4);
      chk.AutoSize = true;
      chk.Tag = trk;
      chk.CheckedChanged += new EventHandler(chk_CheckedChanged);
      pan.Controls.Add(chk);
      Chks[trk] = chk;
      chk.Refresh();
      LoadTTTrk(chk, "Trk:chk");

      //* load mute buttons
      CheckBox mute = new CheckBox();
      // set default value...
      mute.Location = new Point(24, 0);
      mute.Appearance = Appearance.Button;
      mute.BackColor = Color.LightGray;
      mute.UseVisualStyleBackColor = false;
      mute.AutoCheck = true;
      mute.Text = "M";
      mute.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular);
      mute.AutoSize = false;
      mute.Size = new System.Drawing.Size(23, 20);
      mute.Tag = trk;
      mute.CheckedChanged += new EventHandler(mute_CheckedChanged);
      mute.Checked = P.F.Mute[trk];
      pan.Controls.Add(mute);
      chkMutes[trk] = mute;
      mute.Refresh();
      LoadTTTrk(mute, "Trk:mute");

      //* load solo buttons
      CheckBox solo = new CheckBox();
      solo.Location = new Point(54, 0);
      solo.Appearance = Appearance.Button;
      solo.BackColor = Color.LightGray;
      solo.UseVisualStyleBackColor = false;
      solo.AutoCheck = true;
      solo.Text = "S";
      solo.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular);
      solo.AutoSize = false;
      solo.Size = new System.Drawing.Size(23, 20);
      solo.Tag = trk;
      solo.CheckedChanged += new EventHandler(solo_CheckedChanged);
      solo.Checked = P.F.Mute.IsSolo(trk);
      pan.Controls.Add(solo);
      Solos[trk] = solo;
      solo.Refresh();
      LoadTTTrk(solo, "Trk:solo");

      #if recordmidi
        //* load record buttons
        CheckBox record = new CheckBox();
        record.Location = new Point(84, 0);
        record.Appearance = Appearance.Button;
        record.BackColor = (trk == RecTrk) ? Color.Red : Color.LightGray;
        record.UseVisualStyleBackColor = false;
        record.AutoCheck = true;
        record.Text = "R";
        record.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular);
        record.AutoSize = false;
        record.Size = new System.Drawing.Size(23, 20);
        record.Tag = trk;
        record.CheckedChanged += new EventHandler(record_CheckedChanged);
        Bypass_Event = true;
        record.Checked = (trk == RecTrk);
        Bypass_Event = false;
        pan.Controls.Add(record);
        Records[trk] = record;
        record.Refresh();
        LoadTTTrk(record, "Trk:record");
      #endif

      //* load text labels for track and totals
      Label lbltrk = new Label();
      lbltrk.AutoSize = true;
      //lbltrk.Text = string.Format("T{0,-2} ({1})", trk + 1, OnCount[trk]);
      lbltrk.Location = new Point(111, 3);
      lbltrk.Text = string.Format("T{0,-2}", trk.ToString());
      lbltrk.BorderStyle = (FileStream.indPitchBend[trk]) ? BorderStyle.FixedSingle : BorderStyle.None;
      pan.Controls.Add(lbltrk);
      LoadTTTrk(lbltrk, "Trk:lbltrk");

      //* load trkvols/pans 
      TrackBar trkbar = new TrackBar();
      trkbar.Location = new Point(160, 0);
      trkbar.AutoSize = false;
      trkbar.Size = new System.Drawing.Size(128, 20);
      trkbar.Minimum = 0;
      trkbar.Maximum = 127;
      trkbar.TickFrequency = 64;
      trkbar.LargeChange = 5;
      trkbar.SmallChange = 1;
      trkbar.Tag = trk;
      trkbar.Scroll += new EventHandler(trkbar_Scroll);
      pan.Controls.Add(trkbar);
      TrkBar[trk] = trkbar;
      SetTrkBar(trk);
      trkbar.Refresh();
      LoadTTTrk(trkbar, "Trk:trkbar");

      ////* load labels for channels 
      //Label lblchan = new Label();
      //lblchan.Location = new Point(chk.Left, lbltrk.Bottom + 12);
      //lblchan.Tag = TrkBar;
      //lblchan.AutoSize = true;
      //if (P.F.Chan[trk] < 0) {
      //  lblchan.Text = "***";
      //} else {
      //  lblchan.Text = String.Format("C{0,-2}", P.F.Chan[trk] + 1);
      //}
      //if (FileStream.indPitchBend[trk]) lblchan.BorderStyle = BorderStyle.FixedSingle;
      //pan.Controls.Add(lblchan);
      //lblChans[trk] = lblchan;
      //LoadTTTrk(lblchan, "Trk:lblchan");

      //* load nuds or labels for channels 
      Control ctlchan;
#if recordmidi
      ctlchan = new NumericUpDown();
      NumericUpDown nudchan = (NumericUpDown)ctlchan;
      nudchan.Minimum = 0;  //variable
      nudchan.Maximum = 16;  //chan + 1
      nudchan.Increment = 1;
      nudchan.ValueChanged += nudchan_ValueChanged;
      nudchan.Value = P.F.Chan[trk] + 1;  //may be 0
      LoadTTTrk(nudchan, "Trk:nudchan");  //need to update Help if this is active
#else
      ctlchan = new Label();
      Label lblchan = (Label)ctlchan;
      lblchan.Text = (P.F.Chan[trk] < 0) ? 
        "***" : 
        lblchan.Text = String.Format("C{0,-2}", P.F.Chan[trk] + 1);
      lblchan.BorderStyle = BorderStyle.FixedSingle;
      lblchan.TextAlign = ContentAlignment.MiddleCenter;
      LoadTTTrk(lblchan, "Trk:lblchan");
#endif
      ctlchan.Location = new Point(chk.Left, lbltrk.Bottom + 9);
      ctlchan.Tag = trk;
      ctlchan.AutoSize = false;
      ctlchan.Width = 35;
      ctlchan.Height = 20;
      pan.Controls.Add(ctlchan);
      ctlChans[trk] = ctlchan;

      //* add combo boxes for Patches
      ComboBox cmb = new ComboBox();
      cmb.Location = new Point(chk.Left + 45, lbltrk.Bottom + 9);
      cmb.Tag = trk;
      cmb.Width = 235;
      cmb.DropDownStyle = ComboBoxStyle.DropDownList;  //non-editable
      cmb.SelectedIndex = -1;
      cmb.Enabled = false;
      cmb.BackColor = GetBackColor(false);
      cmb.SelectedIndexChanged += cmb_SelectedIndexChanged;
      pan.Controls.Add(cmb);
      cmbPatches[trk] = cmb;
      if (P.F.Chan[trk] != 9 && P.F.Chan[trk] >= 0) {
        SetCmbPatch(trk);
      }
      cmb.Refresh();
      LoadTTTrk(cmb, "Trk:cmb");

      //* load collapse checkboxes (buttons)
      CheckBox chkcollapse = chkCollapses[trk];
      if (chkCollapses[trk] == null) {  //retain between LoadTracks()
        chkcollapse = new CheckBox();
        chkcollapse.Appearance = Appearance.Button;
        chkcollapse.Tag = trk;
        chkcollapse.Size = new Size(20, 20);
        chkcollapse.Left = chk.Left;
        chkcollapse.Image = global::ChordCadenza.Properties.Resources.GlyphUp_16x;
        chkcollapse.UseVisualStyleBackColor = false;
        chkcollapse.Checked = false;
        chkcollapse.CheckedChanged += chkcollapse_CheckedChanged;
        chkCollapses[trk] = chkcollapse;
        LoadTTTrk(chkcollapse, "Trk:chkcollapse");
      }
      pan.Controls.Add(chkcollapse);

      //* load titles 
      TextBox txttitle = new TextBox();
      txttitle.AutoSize = false;
      txttitle.TextAlign = HorizontalAlignment.Left;
      txttitle.Size = new Size(140, 13);
      txttitle.BorderStyle = BorderStyle.FixedSingle;
      txttitle.Location = new Point(chkcollapse.Right + 4, cmb.Bottom + 3);
      if (Title[trk] != null) txttitle.Text = Title[trk].Trim(new char[] { '"' });
      pan.Controls.Add(txttitle);
      //if (trk == 9 && txttitle.Text == "Drums") {
      //  pan.BackColor = Color.Red;
      //  //return;
      //}
      txtTitles[trk] = txttitle;
      LoadTTTrk(txttitle, "Trk:txttitle");

      //* load track type labels
      Label lbltrktype = new Label();
      lbltrktype.AutoSize = true;
      if (FileStream.TrkType[trk] == clsFileStream.eTrkType.NoStyle) lbltrktype.Text = "";
      else lbltrktype.Text = FileStream.TrkType[trk].ToString();
      lbltrktype.Location = new Point(txttitle.Right + 30, txttitle.Top);
      pan.Controls.Add(lbltrktype);
      //lblTrkTypes[trk] = lbltrktype;
      LoadTTTrk(lbltrktype, "Trk:lbltrktype");

      //* relocate to switch top and bottom rows of pan
      int locbottom = cmb.Bottom + 3;  //3rd (bottom) line
      int loctop = 4;  //top line

      txttitle.Top = loctop;
      lbltrktype.Top = loctop;
      chkcollapse.Top = loctop - 4;

      chk.Top = locbottom + 4;
      mute.Top = locbottom;
      solo.Top = locbottom;
#if recordmidi
      record.Top = locbottom;
#endif
      lbltrk.Top = locbottom + 4;
      trkbar.Top = locbottom;
    }

    private void cmb_SelectedIndexChanged(object sender, EventArgs e) {
      if (P.F?.CF != null) P.F.CF.indSave = true;
    }

    private void chkcollapse_CheckedChanged(object sender, EventArgs e) {
      CheckBox chk = (CheckBox)sender;
      clsTrks.T trk = (clsTrks.T)chk.Tag;
      if (chk.Checked) {  //collapse
        chk.Image = global::ChordCadenza.Properties.Resources.GlyphDown_16x;
        Pans[trk].Height = ctlChans[trk].Top;
        Pics[trk].Hide();
      } else {  //not collapsed
        chk.Image = global::ChordCadenza.Properties.Resources.GlyphUp_16x;
        Pans[trk].Height = Pics[trk].Height;
        Pics[trk].Show();
      }
      if (indLocatePics) {
        LocatePics();
        P.F.frmTrackStyles?.SetChks();
      }
    }

#if recordmidi
    private void nudchan_ValueChanged(object sender, EventArgs e) {
      NumericUpDown nud = (NumericUpDown)sender;
      clsTrks.T trk = (clsTrks.T)nud.Tag;
      int chan = (int)nud.Value - 1;
      P.F.Chan[trk] = chan;  //may be -1
      if (chan >= 0) {
        foreach (clsTrks.T t in P.F.Chan.Next) {
          if (t != trk && P.F.Chan[trk] == P.F.Chan[t]) {
            Bypass_Event = true;
            if (cmbPatches[trk] != null && cmbPatches[t] != null) {
              cmbPatches[trk].SelectedIndex = cmbPatches[t].SelectedIndex;  //just change display
            }
            if (TrkBar[trk] != null && TrkBar[t] != null) {
              TrkBar[trk].Value = TrkBar[t].Value;  //just change display
            }
            Bypass_Event = false;
            break;
          }
        }
        for (int i = 0; i < FileStream.Strm.Length; i++) {
          if (FileStream.Strm[i].Trk != trk) continue;
          if (!(FileStream.Strm[i] is clsFileStream.clsEvShort)) continue;
          clsFileStream.clsEvShort ev = (clsFileStream.clsEvShort)FileStream.Strm[i];
          if (ev.Status >= 0xf0) continue;  //system etc.
          int status = (ev.Status & 0xf0) | chan;
          ev.Status = (byte)status;
        }
        FileStream.MidiCtlrs = new clsMidiCtlrs(FileStream.Strm);
        if (P.F?.CF != null) P.F.CF.indSave = true;
      }
    }
#endif

    internal ToolTip TTTrks;
    private void InitTTTrk() {
      TTTrks = new ToolTip();
      TTTrks.Active = P.frmStart.chkTTActive.Checked;
      TTTrks.ShowAlways = true;
      TTTrks.AutoPopDelay = 15000;
      TTTrks.InitialDelay = 1000;
      TTTrks.ReshowDelay = 500;
      // Force the ToolTip text to be displayed whether or not the form is active.
      TTTrks.ShowAlways = true;
    }

    private void LoadTTTrk(Control ctrl, string name) {
      clsTT.LoadToolTipCtrlMM(TTTrks, this, ctrl, name);
    }

    private void Panel2_MouseWheel(object sender, MouseEventArgs e) {
      throw new NotImplementedException();
    }

    private void SetCmbPatch(clsTrks.T trk) {
      ComboBox cmb = cmbPatches[trk];
      cmb.Items.AddRange(GeneralMidiList.Desc);
      bool multiple = false;
      int patch = FileStream.MidiCtlrs.GetMainValue(clsMidiCtlrs.eCtlr.Patch, P.F.Chan[trk], out multiple);
      cmb.SelectedIndex = (patch > 127) ? -1 : patch;
      cmb.SelectedIndexChanged += new EventHandler(cmbPatch_SelectedIndexChanged);
      cmb.Enabled = true;  //not percussion or multiple channels on track
      cmb.BackColor = GetBackColor(multiple);
    }

    //internal void UpdateTrk(clsTrks.T trk) {
    //  if (P.F.Chan[trk] < 0) {
    //    lblChans[trk].Text = "***";
    //  } else {
    //    lblChans[trk].Text = String.Format("C{0,-2}", P.F.Chan[trk] + 1);
    //  }
    //  SetOctaves(trk);
    //  SetTrkBar(trk);
    //  SetCmbPatch(trk);
    //  UpdateControlsTrk(trk);  //vol, pan, patch
    //  PicNoteMaps[trk] = new clsPicNoteMapMM(this, Pics[trk], trk, PicNoteMaps[trk].FirstTrk);  
    //  Pics[trk].Refresh();
    //}

    private void SetTrkBar(clsTrks.T trk) {
      TrackBar trkbar = TrkBar[trk];
      bool multiple;
      if (optVol.Checked) {
        trkbar.Value = FileStream.MidiCtlrs.GetMainValue(clsMidiCtlrs.eCtlr.Vol, P.F.Chan[trk], out multiple);
      } else {
        trkbar.Value = FileStream.MidiCtlrs.GetMainValue(clsMidiCtlrs.eCtlr.Pan, P.F.Chan[trk], out multiple);
      }
      trkbar.BackColor = GetBackColor(multiple);
      trkbar.Enabled = (P.F.Chan[trk] >= 0);
    }

    //internal void SetOctaves(clsTrks.T trk) {
    //  //* null trk: all trks
    //  //* calculate number of octaves in track (C...Bb)
    //  foreach (clsTrks.T t in FileStream.OnCount.Next) {
    //    if (trk != null && t != trk) continue;
    //    int octlo = FileStream.TrkMinPitch[trk] / 12;
    //    int octhi = FileStream.TrkMaxPitch[trk] / 12;
    //    Octaves[trk] = octhi - octlo + 1;
    //    MinC[trk] = octlo * 12;
    //  }
    //}

    internal int GetPixPerNoteInt(int octaves) {
      if (chkOneOctave.Checked) return VFactor;
      if (chkEvenSpacing.Checked) {
        return VFactor / octaves;   //0 causes picTrk to deactivate paint event
      } else {
        return VFactor;
      }
    }

    internal float GetPixPerNoteFloat(int octaves) {
      if (chkOneOctave.Checked) return VFactor;
      if (chkEvenSpacing.Checked) {
        return (float)VFactor / (float)octaves;   //0 causes picTrk to deactivate paint event
      } else {
        return (float)VFactor;
      }
    }

    internal int GetTrkHeight(int octaves) {
      if (chkOneOctave.Checked) return VFactor * 12;
      int i = GetPixPerNoteInt(octaves);
      if (i == 0) {  //0 causes picTrk to deactivate paint event
        float f = GetPixPerNoteFloat(octaves) * octaves * 12;
        return (int)f;
      }
      return i * octaves * 12;
    }

    private void cmbPatch_SelectedIndexChanged(object sender, EventArgs e) {
      //* send patch on midistream
      if (Bypass_Event) return;
      ComboBox cmb = (ComboBox)sender;
      clsTrks.T trk = (clsTrks.T)cmb.Tag;
      int chan = P.F.Chan[trk];
      if (chan >= 0) {
        P.F.Patch[chan] = cmb.SelectedIndex;
        P.F.SendPatch(trk);
        foreach (clsTrks.T t in P.F.Chan.Next) {  //check for other tracks on same channel
          if (t != trk && P.F.Chan[t] == chan) {
            Bypass_Event = true;
            cmbPatches[t].SelectedIndex = cmb.SelectedIndex;  //just need to change display
            Bypass_Event = false;
          }
        }
      }
      cmb.BackColor = GetBackColor(false);
    }

    private void trkbar_Scroll(object sender, EventArgs e) {
      if (Bypass_Event) return;
      TrackBar trkbar = (TrackBar)sender;
      clsTrks.T trk = (clsTrks.T)trkbar.Tag;
      int portchan = P.F.Chan[trk];
      if (optVol.Checked) {
        P.F.Vol[portchan] = trkbar.Value;
        P.F.SendVol(trk);
      } else {
        P.F.Pan[portchan] = trkbar.Value;
        P.F.SendPan(trk);
      }
      trkbar.BackColor = GetBackColor(false);
      foreach (clsTrks.T t in P.F.Chan.Next) {
        if (t != trk && P.F.Chan[trk] == P.F.Chan[t]) {
          Bypass_Event = true;
          TrkBar[t].Value = TrkBar[trk].Value;  //just change display
          Bypass_Event = false;
        }
      }
      if (P.F?.CF != null) P.F.CF.indSave = true;
    }

    private void picTrk_MouseClick(object sender, MouseEventArgs e) {
      if (!P.frmSC.IsPlayClickable()) return;
      for (int i = 0; i < FileStream.Strm.Length; i++) FileStream.Strm[i].Selected = false;
      //if (chkSelectArea.Checked) {
      if (Control.ModifierKeys == Keys.Shift) {  //shift only
        PictureBox pic = (PictureBox)sender;
        MouseTrk = (clsTrks.T)pic.Tag;
        if (e.Button == MouseButtons.Left) {
          CsrQILo = (e.X * HDiv) / HFactor;
        } else {
          CsrQIHi = (e.X * HDiv) / HFactor;
        }
        if (!chkOneOctave.Checked) {
          int tickslo = CsrQILo * P.F.TicksPerQI;
          int tickshi = CsrQIHi * P.F.TicksPerQI;
          if (tickshi > tickslo) {
            int cnt = FileStream.MarkDeleteNotes(MouseTrk, tickslo, tickshi);
            cmdDeleteNotes.Enabled = (cnt > 0);
          }
        }

        //splitContainer.Panel2.Refresh();
        foreach (PictureBox p in Pics) {
          if (p != null) p.Refresh();  //need to refresh any pic with old selected area 
        }
      } else {  //current position
        CsrQILo = 0;
        CsrQIHi = 0;
        cmdDeleteNotes.Enabled = false;
        NoteMap.Delete = null;
        MouseTrk = null;
        int ticks = (e.X * HDiv * P.F.TicksPerQI) / HFactor;
        clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
        bbt.RoundDownToBar();
        P.frmSC.Play?.NewReset();
        Forms.frmStart.RefreshBBT(bbt);
        P.F.StartBar = bbt.Bar;
        //if (P.frmSC != null) P.frmSC.nudStartBar.Value = P.F.StartBar + 1;
        lblTicks.Text = bbt.Ticks.ToString();
      }
    }

    //private static int picTrk_Paint_Cnt = 0;  
    private void picTrk_Paint(object sender, PaintEventArgs e) {
      PictureBox pic = (PictureBox)sender;
      clsTrks.T trk = (clsTrks.T)pic.Tag;
      //if (trk == RecTrk) {
      //  Debug.WriteLine("paint trk " + trk + " ClipRectangle = "
      //    + e.ClipRectangle.X +
      //    ", " + e.ClipRectangle.Y
      //    + ", " + e.ClipRectangle.Width
      //    + ", " + e.ClipRectangle.Height);
      //}
      //if (trk == 0) {
      //  Debug.WriteLine("picTrk_Paint trk " + trk
      //    + " Clip Rectangle = " + e.ClipRectangle
      //    + " Count " + ++picTrk_Paint_Cnt);
      //}
#if Testing
      Stopwatch sw = new Stopwatch(); 
      if (trk == 0) sw.Start();
      for (int i = 0; i < 100; i++) { 
#endif
      Forms.frmTrackMap frm = null;
      frm = this;
      int clippixlo = e.ClipRectangle.X;
      int clippixhi = e.ClipRectangle.X + e.ClipRectangle.Width;
      //if (trk == 0) Debug.WriteLine("PicTrks_Paint pixlo = " + pixlo + " pixhi = " + pixhi);
      PicNoteMaps[trk].PaintMap(NoteMap, this, e.Graphics, clippixlo, clippixhi, P.F.MTime);
      Pen pen = new Pen(Color.Red, 3);
      int linepos = Math.Max(CsrPos - 1, 0);
      //int y = (PicNoteMaps[trk].FirstTrk && (int)nudBars.Value > 0) ? BarFont.Height : 0;
      e.Graphics.DrawLine(pen, linepos, 0, linepos, pic.Height);  //draw line csr
      if (trk == MouseTrk) {
        //Brush selectbrush = P.ColorsNoteMap["Selected Area"].Br;
        Brush selectbrush = new SolidBrush(Color.FromArgb(106, 192, 192, 192));
        int pixlo = (CsrQILo * HFactor) / HDiv;
        int pixhi = (CsrQIHi * HFactor) / HDiv;
        frmChordMap.DrawSelectedArea(e, pic.Height, selectbrush, false, pixlo, pixhi, false, null);
      }
#if Testing
        if (trk > 0) break;  //only monitor trk0
      }  
      if (trk == 0) Debug.WriteLine("Trk0_Paint millisecs = " + sw.ElapsedMilliseconds);
#endif
    }

    //private sPaintVars GetPaintVars() {
    //  sPaintVars pv = new sPaintVars(
    //    chkOneOctave.Checked,
    //    chkShowBeats.Checked,
    //    (int)nudBars.Value,
    //    HFactor,
    //    HDiv,
    //    VFactor,
    //    chkShowKB.Checked);
    //  return pv;
    //}

    private void VResize() {
      int top = picBars.Bottom + 10;
      //DGV.Top = top;
      //top += VisHeight(DGV);
      dgvLyrics.Top = top;
      //lblLyricsLit.Top = top;
      //LocateLblLit(DGV, lblChordNamesLit);
      LocateLblLit(dgvLyrics, lblLyricsLit);
      top += VisHeight(dgvLyrics);
      splitContainer.Top = top;
      splitContainer.Height = this.ClientSize.Height - splitContainer.Top - 10;
    }

    private void LocateLblLit(Control ctl, Label lbllit) {
      int ctlmiddle = (ctl.Top + ctl.Bottom) / 2;
      lbllit.Top = ctlmiddle - lbllit.Height / 2;
    }

    private int VisHeight(Control ctl) {
      return (ctl.Visible) ? ctl.Height + 10 : 0;
    }

    private void cmdYPos_Click(object sender, EventArgs e) {
      VFactor += (chkEvenSpacing.Checked) ? 2 : 1;
      VFactor = Math.Min(Math.Max(VFactor, 1), 30);
      Static_VFactor = VFactor;
      PicRefresh();
    }

    private void cmdYNeg_Click(object sender, EventArgs e) {
      VFactor -= (chkEvenSpacing.Checked) ? 2 : 1;
      VFactor = Math.Min(Math.Max(VFactor, 1), 30);
      Static_VFactor = VFactor;
      PicRefresh();
    }

    private void cmdXPos_Click(object sender, EventArgs e) {
      clsTrks.T trk0 = new clsTrks.T(P.F.Trks, 0);
      if (Pics[trk0] != null && Pics[trk0].Width > 16380) return;  //MouseClick limit approx 32768
      if (HDiv == 1) {
        HFactor = Math.Min(Math.Max(++HFactor, 1), 30);
      } else {
        HDiv = Math.Min(Math.Max(--HDiv, 1), 10);
      }
      //ResetAutoScroll();
      PicRefresh();
      RefreshBBTOpts(P.F.CurrentBBT, true);
      //SetScroll();
    }

    private void cmdXNeg_Click(object sender, EventArgs e) {
      if (HFactor > 1) {
        HFactor = Math.Min(Math.Max(--HFactor, 1), 30);
      } else {
        HDiv = Math.Min(Math.Max(++HDiv, 1), 4);
      }
      PicRefresh();
      RefreshBBTOpts(P.F.CurrentBBT, true);
      //SetScroll();
    }

    internal clsTrks.Array<bool> GetSelectedTrks() {
      bool indselect = false;
      clsTrks.Array<bool> selectedtrks = new clsTrks.Array<bool>(false);
      foreach (CheckBox chk in Chks) {
        if (chk != null && chk.Checked) {
          indselect = true;
          clsTrks.T trk = (clsTrks.T)chk.Tag;
          selectedtrks[trk] = true;
        }
      }
      if (!indselect) return null;
      return selectedtrks;
    }

    internal void SetSelectedTrks( clsTrks.Array<bool> selectedtrks) {
      Bypass_Event = true;
      foreach (CheckBox chk in Chks) {
        if (chk != null) {
          clsTrks.T trk = (clsTrks.T)chk.Tag;
          chk.Checked = selectedtrks[trk];
        }
      }
      Bypass_Event = false;
      P.frmSC.chkShowTracks_CheckedChanged(null, null);
      P.F.frmTrackStyles?.SetChks();
    }

    //internal int? GetFirstSelectedTrk() {
    //  foreach (CheckBox chk in Chks) {
    //    if (chk != null && chk.Checked) return (int)chk.Tag;
    //  }
    //  return null;
    //}

    //private clsTrks.Array<bool> SelectAllTrks() {
    //  bool[] selectedtrks = new bool[NumTrks];
    //  for (int i = 0; i < selectedtrks.Length; i++) selectedtrks[i] = true;
    //  return selectedtrks;
    //}

    //private void nudBars_ValueChanged(object sender, EventArgs e) {
    //  if (Bypass_Event) return;
    //  BarLabels = (int)nudBars.Value;
    //  PicRefresh();
    //}

    //private void cmdConvert_Click(object sender, EventArgs e) {
    //  bool[] selectedtrks = GetSelectedTrks();
    //  if (selectedtrks == null) {
    //    MessageBox.Show("Error: No Track Selected");
    //    return;
    //  }
    //  ConvertCSV(selectedtrks);
    //}

    internal void cmdChordMap_Click(object sender, EventArgs e) {
      using (new clsWaitCursor()) {
        clsTrks.Array<bool> selectedtrks = GetSelectedTrks();
        if (selectedtrks == null) selectedtrks = new clsTrks.Array<bool>(true);
        ConvertCSV(selectedtrks);
        //CheckfrmCalcKeys();
      }
    }

    internal static void CheckfrmCalcKeys() {  //show frmCalcKeys (if necessary)
      if (P.F.CF == null || P.F.CF.Evs.Count == 0) {
        //* show frmCalcKeys for ALL tracks by default
        P.CloseFrm(P.F.frmCalcKeys);
        clsTrks.Array<bool> trkselectall = new clsTrks.Array<bool>(true);
        P.F.frmCalcKeys = new frmCalcKeys(trkselectall);
      }
    }

    //private void cmdPlayMap_Click(object sender, EventArgs e) {
    //  ShowTracks();
    //}

    private void chk_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      //CheckBox chk = (CheckBox)sender;
      P.frmSC.chkShowTracks_CheckedChanged(null, null);
      P.F.frmTrackStyles?.SetChks();
      if (P.F?.CF != null) P.F.CF.indSave = true;
      //int trk = Array.IndexOf(VChan, (int)chk.Tag);
      //int trk = (int)chk.Tag;
      //MutedTracks[trk] = !(chk.Checked);
    }

    private void mute_CheckedChanged(object sender, EventArgs e) {
      CheckBox chk = (CheckBox)sender;
      clsTrks.T trk = (clsTrks.T)chk.Tag;
      if (chk.Checked) chk.BackColor = Color.SkyBlue;
      else chk.BackColor = Color.LightGray;
      P.F.Mute[trk] = chk.Checked;
      if (indLocatePics) P.F.frmTrackStyles?.SetChks();
      if (P.F?.CF != null) P.F.CF.indSave = true;
    }

    private void solo_CheckedChanged(object sender, EventArgs e) {
      CheckBox chk = (CheckBox)sender;
      clsTrks.T trk = (clsTrks.T)chk.Tag;
      if (chk.Checked) chk.BackColor = Color.LawnGreen;
      else chk.BackColor = Color.LightGray;
      P.F.Mute.SetSolo(trk, chk.Checked);
    }

    private void record_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      CheckBox chk = (CheckBox)sender;
      clsTrks.T trk = (clsTrks.T)chk.Tag;

      if (chk.Checked) {
        RecTrk = trk.Copy();
        FileStream.RecChan = P.F.Chan[trk];
        FileStream.MidiCtlrs.SendCtlrs(-1, FileStream.RecChan);
        chk.BackColor = Color.Red;
      } else {
        if (RecTrk.Equals(trk)) RecTrk = null;
        chk.BackColor = Color.LightGray;
      }

      //ReloadTracks();  //RefreshPic(trk);
      LoadTrack(trk);
      Bypass_Event = true;
      foreach (CheckBox c in Records) {
        if (c != null && (clsTrks.T)c.Tag != RecTrk && c.Checked) {
          c.Checked = false;  //only one record track checked
          c.BackColor = Color.LightGray;
          LoadTrack((clsTrks.T)c.Tag);
        }
      }
      Bypass_Event = false;
      //Refresh();
    }

    //private void ReloadTracks() {
    //  splitContainer.Panel2.SuspendLayout();
    //  Point scroll = splitContainer.Panel2.AutoScrollPosition;
    //  LoadTracks();  //LoadTrackPicPan(trk);
    //  Refresh();
    //  splitContainer.Panel2.AutoScrollPosition = new Point(-scroll.X, -scroll.Y);
    //  splitContainer.Panel2.ResumeLayout();
    //}

    internal void RefreshRec() {
      Pics[RecTrk].Refresh();
    }

    internal void cmdCheckAll_Click(object sender, EventArgs e) {
      Bypass_Event = true;  //avoid multiple calls to chk_CheckedChanged
      foreach (CheckBox chk in Chks) if (chk != null) chk.Checked = true;
      Bypass_Event = false;
      P.F.frmTrackStyles?.SetChks();
      chk_CheckedChanged(null, null);
    }

    private void cmdUncheckAll_Click(object sender, EventArgs e) {
      Bypass_Event = true;
      foreach (CheckBox chk in Chks) if (chk != null) chk.Checked = false;
      Bypass_Event = false;
      P.F.frmTrackStyles?.SetChks();
      chk_CheckedChanged(null, null);
      //for (int trk = 0; trk < NumTrks; trk++) MutedTracks[trk] = true;
    }

    private void chkShowKB_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      Static_ShowKB = chkShowKB.Checked;
      PicRefresh();
    }

    private void chkShowBeats_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      Static_ShowBeats = chkShowBeats.Checked;
      PicRefresh();
    }

    private void frmMultiMap_Resize(object sender, EventArgs e) {
      VResize();
      splitContainer.Refresh();
      picBars.Width = splitContainer.Width - picBars.Left;
      dgvLyrics.Width = splitContainer.Width - dgvLyrics.Left - picBarsKludge; 
      picBars.Refresh();
      dgvLyrics.Refresh();
    }

    //private void ResetAutoScroll() {
    //  splitContainer.Panel2.AutoScrollPosition = new Point(0, 0);
    //}

    //private static bool ScrollEvOff = false;

    private void splitContainer_Panel2_MouseWheel(object sender, MouseEventArgs e) {
      splitContainer_Panel2_Scroll(sender, null);  //vertical scroll
    }

    private void splitContainer_Panel2_Scroll(object sender, ScrollEventArgs e) {
      //* synchronize panel1/panel2 vertical scrolls
      if (e != null && e.ScrollOrientation == ScrollOrientation.HorizontalScroll) {
        ScrollFromPan = true;
        if (dgvLyrics.Visible && !ScrollFromdgvLyrics) {
          int val = -splitContainer.Panel2.AutoScrollPosition.X;
          dgvLyrics.HorizontalScrollingOffset = val;   //e.NewValue;
          //dgvLyrics.Refresh();
        }
        picBars.Refresh();
        dgvLyrics.Refresh();
        ScrollFromPan = false;
      }
      splitContainer.Panel1.Refresh();
      splitContainer.Panel2.Refresh();
      //Debug.WriteLine("pan1inner.Top scroll = " + pan1inner.Top);
    }

    //private void splitContainer_Panel1_Scroll(object sender, ScrollEventArgs e) {
    //  //* synchronize panel1/panel2 vertical scrolls
    //  if (ScrollEvOff) return;
    //  ScrollEvOff = true;
    //  //splitContainer.Panel2.VerticalScroll.Value = splitContainer.Panel1.VerticalScroll.Value;
    //  //splitContainer.Panel2.PerformLayout();
    //  //splitContainer.Panel1.Refresh();
    //  splitContainer.Panel1.Refresh(); 
    //  splitContainer.Panel2.Refresh(); 
    //  ScrollEvOff = false;
    //}


    internal void LocatePics() {
      int yloc = splitContainer.Panel2.AutoScrollPosition.Y;
      foreach (clsTrks.T trk in PicNoteMaps.Next) {
        if (PicNoteMaps[trk] == null) continue;
        PictureBox pic = PicNoteMaps[trk].Pic;
        Panel pan = Pans[trk];
        pic.Top = yloc;
        if (chkCollapses[trk] != null && TrkShow[trk]) {
          pan.Height = (chkCollapses[trk].Checked) ? ctlChans[trk].Top : pic.Height;
          pic.Visible = !chkCollapses[trk].Checked;
          yloc += pan.Height + PicMargin;
        }
      }
      //splitContainer.Panel2.Refresh();
      splitContainer.Refresh();
    }

    //private void splitContainer_Paint(object sender, PaintEventArgs e) {
    //  int yloc1 = 0;
    //  int yloc2 = splitContainer.Panel2.AutoScrollPosition.Y;
    //  //bool firsttrk = true;
    //  //ResetAutoScroll();
    //  foreach (clsTrks.T trk in PicNoteMaps.Next) {
    //    if (PicNoteMaps[trk] == null) continue;
    //    PictureBox pic = PicNoteMaps[trk].Pic;
    //    Panel pan = Pans[trk];
    //    //indShow[trk] = (OnCount[trk] != 0);
    //    //if (!indShow[trk]) {
    //    if (TrkSelect[trk]) {
    //      //update pic
    //      if (PicNoteMaps[trk] == null) continue;
    //      pic.Location = new Point(pic.Location.X, yloc2);

    //      //update panctl
    //      pan.Top = yloc1;

    //      //set up next controls
    //      yloc1 += GetTrkDistance();
    //      //Debug.WriteLine("trk: " + trk + " yloc1: " + yloc1);
    //      //if (firsttrk && (int)nudBars.Value > 0 && BarFont != null) {
    //      //  //yloc1 += PicNoteMaps[trk].BarFont.Height;
    //      //  yloc1 += BarFont.Height;
    //      //}
    //      yloc2 += GetTrkDistance();
    //      //if (firsttrk && (int)nudBars.Value > 0 && BarFont != null) {
    //      //  //yloc2 += PicNoteMaps[trk].BarFont.Height;
    //      //  yloc2 += BarFont.Height;
    //      //  firsttrk = false;
    //      //}
    //    }
    //  }

    //  //pan1inner.Height = splitContainer.Panel1.Height;

    //  //splitContainer.Panel2.Update();
    //  //Debug.WriteLine("splitContainer.Panel2.DisplayRectangle.Height = " + splitContainer.Panel2.DisplayRectangle.Height);
    //  pan1inner.Height = splitContainer.Panel2.DisplayRectangle.Height + 50;

    //  //Debug.WriteLine("pan1inner.Top paint = " + pan1inner.Top);
    //  //pan1inner.Top = 0;  //splitContainer.Panel2.AutoScrollPosition.Y;
    //  //Debug.WriteLine("pan1inner.Top after = " + pan1inner.Top);
    //}

    //private void splitContainer_Panel2_Paint(object sender, PaintEventArgs e) {
    //Graphics xgr = e.Graphics;
    //Pen pen = new Pen(Color.Red, 2);
    //xgr.DrawLine(pen, CsrPos, 0, CsrPos, splitContainer.Panel2.Height);
    //}

    public void RefreshBBT(clsMTime.clsBBT bbt) {
      RefreshBBTOpts(bbt, false);
    }
    
    private void RefreshBBTOpts(clsMTime.clsBBT bbt, bool setcsr) {
      if (bbt.MTime != P.F.MTime) return;
      P.F.CurrentBBT = bbt;
      if (!Visible) return;
      //Debug.WriteLine("frmMultiMap: RefreshScrollBBT: bbt = " + StartBB.Bar + " " + StartBB.BeatsRemBar + " " + StartBB.TicksRemBeat);

      CsrQILo = 0;
      CsrQIHi = 0;
      cmdDeleteNotes.Enabled = false;
      NoteMap.Delete = null;

      SuspendLayout();  //not really necessary
      int qtime = P.F.CurrentBBT.Ticks / P.F.TicksPerQI;
      LastCsrPos = CsrPos;
      CsrPos = (qtime * HFactor) / HDiv;
      if (Form_Loaded) Forms.frmChordMap.SelectBarBeat(null, dgvLyrics, NoSelectDGV, bbt.Bar, bbt.BeatsRemBar);
      int vismin = -splitContainer.Panel2.AutoScrollPosition.X;
      int vismax = vismin + splitContainer.Panel2.ClientSize.Width;
      if (setcsr || (vismax - CsrPos < ScrollMargin || CsrPos - vismin < ScrollMargin)) {
        SetScroll();
      }
      foreach (PictureBox pic in Pics) {
        if (pic == null) continue;
        clsTrks.T trk = (clsTrks.T)pic.Tag;
        clsIShowNoteMap.InvalidatePic(this, pic, PicNoteMaps[trk], CsrPos, LastCsrPos, 0);
      }
      ResumeLayout();  //not really necessary
    }

    private void SetScroll() {
      ScrollFromRefreshBBT = true;
      int maxwidth = (P.F.MaxBBT.QI * HFactor) / HDiv;
      int posx = Math.Min(Math.Max(0, CsrPos - ScrollMargin), maxwidth);  //positive
                                                                          //int poslyrics = Math.Max(0, posx - SystemInformation.VerticalScrollBarWidth);
      int posy = splitContainer.Panel2.AutoScrollPosition.Y;  //zero or negative
      splitContainer.Panel2.AutoScrollPosition = new Point(posx, -posy);  //(positive, -negative)
      int poslyrics = -splitContainer.Panel2.AutoScrollPosition.X;
      dgvLyrics.HorizontalScrollingOffset = poslyrics;
      dgvLyrics.Refresh();
      picBars.Refresh();
      ScrollFromRefreshBBT = false;
    }

    //internal void SelectBarBeat(int bar, int beat) {
    //  //* dgv is the datagridview to NOT update to avoid re-entrant error
    //  if (DGV.Rows.Count == 0 || dgvLyrics.Rows.Count == 0) return;
    //  if (NoSelectDGV != DGV) DGV.Rows[beat].Cells[bar].Selected = true;
    //  if (NoSelectDGV != dgvLyrics) dgvLyrics.Rows[0].Cells[bar].Selected = true;
    //  //???if (!DGV.Rows[beat].Cells[bar].Selected) DGV.Rows[beat].Cells[bar].Selected = true;
    //  //???if (!dgvLyrics.Rows[0].Cells[bar].Selected) dgvLyrics.Rows[0].Cells[bar].Selected = true;
    //}

    public void StartSub(clsMTime.clsBBT bbt) {
      //* nothing to do!
    }

    private void cmdPausePlay_Click(object sender, EventArgs e) {
      //Debug.WriteLine("****Pause****");
      MidiPlay.Sync.Pause();
    }

    protected void cmdPlayMidi_Click(object sender, EventArgs e) {
      //Debug.WriteLine("***Start***");
      MidiPlay.Sync.StartPlay(this, FileStream, P.F.Mute);
    }

    public Button[] Cmds_Locate { get { return new Button[] { cmdGoToStart }; } }
    //public Button[] Cmds_Play { get { return new Button[0]; } }
    public Button[] Cmds_Play_Midi { get { return new Button[] { cmdPlayMidi }; } }
    public Button[] Cmds_Play_And_Sync { get { return new Button[] { cmdPlayAndRecordAudio }; } }
    public Button[] Cmds_Play_Audio { get { return new Button[] { cmdPlayAudio }; } }
    public Button[] Cmds_Sync_Audio { get { return new Button[] { cmdSyncAudio }; } }
    public Button[] Cmds_Stop { get { return new Button[] { cmdStopPlay }; } }
    public Button[] Cmds_Pause { get { return new Button[] { cmdPausePlay }; } }

    //public void StreamPlayDisable() {
    //  bool val = false;
    //  cmdStartPlay.Enabled = val;
    //  cmdPause.Enabled = val;
    //  cmdStop.Enabled = val;
    //}

    //public void StreamPlayOn() {
    //  cmdStartPlay.Enabled = false;
    //  cmdPause.Enabled = true;
    //  cmdStop.Enabled = true;
    //}

    //public void StreamPlayOff() {
    //  cmdStartPlay.Enabled = true;
    //  cmdPause.Enabled = false;
    //  cmdStop.Enabled = false;
    //}

    //public void SyncPlayOn() {
    //  StreamPlayDisable();
    //}

    private void frmMultiMap_FormClosed(object sender, FormClosedEventArgs e) {
      P.F.frmTrackMap = null;
      if (P.F.frmTrackStyles != null) P.F.frmTrackStyles.Close();
      SaveStatic();  //save location and size of window
    }

    //////private void frmMultiMap_FormClosing(object sender, FormClosingEventArgs e) {
    //////  //* UserClosing includes program calls to Close()
    //////  Cfg.DictFormProps[Name] = new clsFormProps(this);
    //////  if (P.F.CloseFormsUnconditional || e.CloseReason != CloseReason.UserClosing) return;
    //////  e.Cancel = true;
    //////  FormClosingEventArgs ee = new FormClosingEventArgs(CloseReason.None, false);
    //////  CheckSaveFile(this, ee, "MidiFile", ref indChanged, new delegSave(P.frmSC.SaveMidiFileAs));  //-> set ee.Cancel
    //////  if (ee.Cancel) return;
    //////  SaveStatic();  //save location and size of window
    //////  if (!P.F.CloseFormsAll) Hide();
    //////  e.Cancel = false;
    //////}

    private void frmMultiMap_FormClosing(object sender, FormClosingEventArgs e) {
      //* UserClosing includes program calls to Close()
      Cfg.DictFormProps[Name] = new clsFormProps(this);
      //return;
      //if (P.F.CloseFormsUnconditional || e.CloseReason != CloseReason.UserClosing) return;
      //CheckSaveFile(this, e, "MidiFile", ref indChanged, new delegSave(P.frmSC.SaveMidiFileAs));  //-> set ee.Cancel
      //if (e.Cancel) return;
      //e.Cancel = true;
      //Hide();
    }

    //internal delegate string delegSave();
    //internal static void CheckSaveFile(Form owner, FormClosingEventArgs e, string name, ref bool indchanged, delegSave save) {
    //  if (indchanged) {
    //    //* e.Cancel = false; indChanged = true;
    //    DialogResult res = MessageBox.Show(name + " has unsaved changes - Save?",
    //      "Unsaved Changes Warning",
    //      MessageBoxButtons.YesNoCancel);
    //    switch (res) {
    //      case DialogResult.No:
    //        indchanged = false;
    //        break;
    //      case DialogResult.Yes:
    //        string ret = save();
    //        if (ret != "") MessageBox.Show(name + " not saved: " + ret);
    //        indchanged = false;
    //        break;
    //      case DialogResult.Cancel:
    //        break;
    //      default:
    //        LogicError.Throw(eLogicError.X148);
    //        indchanged = false;
    //        break;
    //    }
    //    e.Cancel = indchanged;
    //  }
    //}

    //private void SetTrkSelect() {
    //  TrkShow = new clsTrks.Array<bool>(true);
    //  foreach (clsTrks.T trk in TrkShow.Next) {
    //    if (FileStream.OnCount[trk] <= 0 && !P.F.Added[trk]) {
    //      TrkShow[trk] = chkShowEmpty.Checked;
    //    } else {
    //      if (!chkShowPercussion.Checked && P.F.Chan[trk] == 9) TrkShow[trk] = false;

    //      else if (!chkShowMelody.Checked
    //        && FileStream.TrkType[trk] == clsFileStream.eTrkType.Melody) TrkShow[trk] = false;

    //      else if (!chkShowChords.Checked
    //        && FileStream.TrkType[trk] == clsFileStream.eTrkType.AChords) TrkShow[trk] = false;

    //      else if (!chkShowChords.Checked
    //        && FileStream.TrkType[trk] == clsFileStream.eTrkType.BChords) TrkShow[trk] = false;

    //      else if (!chkShowBass.Checked
    //        && FileStream.TrkType[trk] == clsFileStream.eTrkType.Bass) TrkShow[trk] = false;

    //      else if (!chkShowSparse.Checked
    //        && FileStream.TrkType[trk] == clsFileStream.eTrkType.Sparse) TrkShow[trk] = false;

    //      else if (!chkShowNoStyle.Checked
    //        && FileStream.TrkType[trk] == clsFileStream.eTrkType.NoStyle) TrkShow[trk] = false;
    //    }
    //  }
    //}

    internal static void StaticToIni(StreamWriter sw) {
      if (P.F != null && P.F.frmTrackMap != null) P.F.frmTrackMap.SaveStatic();

      //if (Rect.Width > 0) {  //default: 0, 0, 0, 0
      //  sw.WriteLine("frmMMRect = "
      //    + Rect.X + ", "
      //    + Rect.Y + ", "
      //    + Rect.Width + ", "
      //    + Rect.Height);
      //}

      //sw.WriteLine("frmMMShowChordNames = " + Static_ChordNames);
      //sw.WriteLine("frmMMShowLyrics = " + Static_Lyrics);
      //sw.WriteLine("frmMMShowEmpty = " + Static_ShowEmpty);
      //sw.WriteLine("frmMMShowPercussion = " + Static_Percussion);
      sw.WriteLine("frmMMShowBeats = " + Static_ShowBeats);
      sw.WriteLine("frmMMEvenSpacing = " + Static_EvenSpacing);
      sw.WriteLine("frmMMShowKB = " + Static_ShowKB);
      sw.WriteLine("frmMMShowOneOct = " + Static_OneOct);
      sw.WriteLine("frmMMVFactor = " + Static_VFactor);
      //sw.WriteLine("frmMMShowMelody = " + Static_Melody);
      //sw.WriteLine("frmMMShowChords = " + Static_Chords);
      //sw.WriteLine("frmMMShowBass = " + Static_Bass);
      //sw.WriteLine("frmMMShowSparse = " + Static_Sparse);
      //sw.WriteLine("frmMMShowNoStyle = " + Static_NoStyle);
    }

    internal static bool IniToStatic(string[] f) {
      switch (f[0]) {
        case "frmMMShowBeats":
          Static_ShowBeats = bool.Parse(f[1]);
          return true;
        case "frmMMEvenSpacing":
          Static_EvenSpacing = bool.Parse(f[1]);
          return true;
        case "frmMMShowKB":
          Static_ShowKB = bool.Parse(f[1]);
          return true;
        case "frmMMShowOneOct":
          Static_OneOct = bool.Parse(f[1]);
          return true;
        case "frmMMVFactor":
          Static_VFactor = int.Parse(f[1]);
          return true;
      }
      return false;
    }

    private void LoadStatic() {  //called from Load event
      //if (WindowState == FormWindowState.Normal && Rect.Width != 0) {  //default 0, 0, 0, 0 
      //  //if (Scrn != null && Screen.AllScreens.Contains(Scrn)) {
      //  //  //...
      //  //}
      //  Location = Rect.Location;
      //  Width = Rect.Width;
      //  Height = Rect.Height;
      //}

      //chkShowChordNames.Checked = Static_ChordNames;
      //chkShowLyrics.Checked = Static_Lyrics;
      //chkShowEmpty.Checked = Static_ShowEmpty;
      //chkShowPercussion.Checked = Static_Percussion;
      chkShowBeats.Checked = Static_ShowBeats;
      chkEvenSpacing.Checked = Static_EvenSpacing;
      chkShowKB.Checked = Static_ShowKB;
      chkOneOctave.Checked = Static_OneOct;
      VFactor = Static_VFactor;
      //chkShowMelody.Checked = Static_Melody;
      //chkShowChords.Checked = Static_Chords;
      //chkShowBass.Checked = Static_Bass;
      //chkShowSparse.Checked = Static_Sparse;
      //chkShowNoStyle.Checked = Static_NoStyle;
    }

    private void SaveStatic() {
      if (WindowState == FormWindowState.Normal) {  //save location and size
        //Rect = new Rectangle(Location.X, Location.Y, Width, Height);
        //Scrn = Screen.FromControl(this);
      }
      //Static_Lyrics = chkShowLyrics.Checked;
      //Static_ShowEmpty = chkShowEmpty.Checked;
      //Static_Percussion = chkShowPercussion.Checked;
      Static_ShowBeats = chkShowBeats.Checked;
      Static_EvenSpacing = chkEvenSpacing.Checked;
      Static_ShowKB = chkShowKB.Checked;
      Static_OneOct = chkOneOctave.Checked;
      Static_VFactor = VFactor;
      //Static_Melody = chkShowMelody.Checked;
      //Static_Chords = chkShowChords.Checked;
      //Static_Bass = chkShowBass.Checked;
      //Static_Sparse = chkShowSparse.Checked;
      //Static_NoStyle = chkShowNoStyle.Checked;
    }

    private void cmdGoToStart_Click(object sender, EventArgs e) {
      //P.F.CurrentBBT = new clsMTime.clsBBT(0);
      P.frmSC.Play?.NewReset();
      Forms.frmStart.RefreshBBT(new clsMTime.clsBBT(0));
      if (P.frmSC != null) P.frmSC.nudStartBar.Value = 1;
    }

    private void cmdClear_Click(object sender, EventArgs e) {
      foreach (CheckBox mute in chkMutes) if (mute != null) mute.Checked = false;
      foreach (CheckBox solo in Solos) if (solo != null) solo.Checked = false;
    }

    private void optVol_CheckedChanged(object sender, EventArgs e) {
      foreach (clsTrks.T trk in P.F.Chan.Next) {
        int portchan = P.F.Chan[trk];
        if (portchan % 16 == 9) continue;
        if (TrkBar[trk] == null) continue;
        bool multiple;
        if (optVol.Checked) {
          TrkBar[trk].Value = FileStream.MidiCtlrs.GetMainValue(clsMidiCtlrs.eCtlr.Vol, portchan, out multiple);
        } else {
          TrkBar[trk].Value = FileStream.MidiCtlrs.GetMainValue(clsMidiCtlrs.eCtlr.Pan, portchan, out multiple);
        }
        TrkBar[trk].BackColor = GetBackColor(multiple);
      }
    }

    internal static void ConvertCSV(clsTrks.Array<bool> trkselect) {
      if (trkselect == null) return;
      if (P.F.CF == null) CreateCFtxt();
      CreateCSVFileConv(trkselect);
    }

    //internal bool LoadCHPFile() {
    //  if (P.F.CF == null) CreateCFtxt();
    //  return true;
    //}

    internal static void CreateCSVFileConv(clsTrks.Array<bool> trkselect) {
      try {
        P.F.FileStreamConv = new clsFileStream(P.F.Project.MidiPath, trkselect, true, false, true);
      }
      catch (MidiFileException) {
        return;
      }

      //if (P.F.frmNoteMap != null && P.F.frmNoteMap.IsHandleCreated && simple == P.F.frmNoteMap.Simple) { 
      //  P.F.frmNoteMap.NewCSVFileConv(P.F.FileStreamConv);
      //} else {  //new frmNoteMap
      P.CloseFrm(P.F.frmChordMap);
      P.F.frmChordMap = new Forms.frmChordMap(P.F.FileStreamConv);
      Utils.FormAct(P.F.frmChordMap);
      //}
    }

    private static void CreateCFtxt() {
      //string defaulttxtfilename = P.F.MidiFilePath.Substring(0, P.F.MidiFilePath.Length - 4) + ".chp";
      string defaulttxtfilename = P.F.Project.CHPPath;
      if (File.Exists(defaulttxtfilename)) {
        try {
          P.F.CF = new clsCFPC();
          P.F.CF.PostInit();
          //P.F.CFTxt = (clsCFtxt)P.F.CFTxt;
          //cftxtkey = true;
        }
        catch (ChordFileException) { }
      }
      if (P.F.CF == null) {
        P.F.CF = new clsCFPC(0);  //null txt file
        //P.F.CFTxt = P.F.CFTxt;
      }
      //return cftxtkey;
    }

    private void cmdStopPlay_Click(object sender, EventArgs e) {
      if (cmdStopPlay.BackColor == Color.Red) {
        Forms.frmSC.SyncBarActive(false);  //-> all playable forms
      } else {
        MidiPlay.Sync.Stop();
      }
    }

    //internal void cmdCalcKeys_Click(object sender, EventArgs e) {
    //  clsTrks.Array<bool> trkselect = GetSelectedTrks();
    //  if (trkselect == null) trkselect = new clsTrks.Array<bool>(true);  //select all
    //  using (new clsWaitCursor()) {
    //    P.CloseFrm(P.F.frmCalcKeys);
    //    P.F.frmCalcKeys = new frmCalcKeys(P.F.Project.MidiPath, trkselect, false);
    //    //P.F.frmCalcKeys.Show();
    //  }
    //}

    internal void cmdCalcKeys_Click(object sender, EventArgs e) {
      Forms.frmChordMap.CalcKeys();
    }

    private void cmdSummary_Click(object sender, EventArgs e) {
      P.CloseFrm(P.F.frmSummary);
      P.F.frmSummary = new frmSummary();
      //P.F.frmSummary.Show();
      Utils.FormAct(P.F.frmSummary);
    }

    private void cmdAllUp_Click(object sender, EventArgs e) {
      ChangeAllTrkBars(8, 8);
    }

    private void cmdAllDown_Click(object sender, EventArgs e) {
      ChangeAllTrkBars(-8, -8);
    }

    private void ChangeAllTrkBars(int fpan, int fvol) {
      foreach (clsTrks.T trk in TrkBar.Next) {
        if (TrkBar[trk] == null) continue;
        //if (Vs[trk].Checked) continue;
        if (!Chks[trk].Checked) continue;
        int portchan = P.F.Chan[trk];
        if (portchan < 0) continue;
        if (optVol.Checked) {
          int val = TrkBar[trk].Value;
          if (fvol > 1) val = (val * (fvol + 1)) / fvol;
          else if (fvol < -1) val = (val * (-fvol - 1)) / -fvol;
          val = Math.Max(0, Math.Min(127, val));
          TrkBar[trk].Value = val;
          P.F.Vol[portchan] = val;
          P.F.SendVol(trk);
        } else {
          TrkBar[trk].Value = Math.Max(0, Math.Min(127, TrkBar[trk].Value + fpan));
          P.F.Pan[portchan] = TrkBar[trk].Value;
          P.F.SendPan(trk);
        }
        TrkBar[trk].BackColor = GetBackColor(false);  //single value
        TrkBar[trk].Refresh();
      }
    }

    private void cmdPanic_Click(object sender, EventArgs e) {
      P.F.Panic();
    }

    private void chkOneOctave_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      cmdDeleteNotes.Enabled = !chkOneOctave.Checked;
      Static_OneOct = chkOneOctave.Checked;
      PicRefresh();
    }

    internal void UpdateControls() {
      //* update pan, vol, patch with new overrides
      optVol_CheckedChanged(null, null);
      foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {
        UpdateControlsTrk(trk);
      }
    }

    internal void UpdateControlsTrk(clsTrks.T trk) {
      //if (P.F.Chan[trk] != 9 && P.F.Chan[trk] >= 0) {  //not percussion or multiple channels
      if (TrkShow[trk] && P.F.Chan[trk] >= 0) {  //not percussion or multiple channels
        int pc = P.F.Chan[trk];
        if (pc >= 0) {  //not multiple channels on trk?
          bool multiple;
          int patch = FileStream.MidiCtlrs.GetMainValue(clsMidiCtlrs.eCtlr.Patch, pc, out multiple);
          cmbPatches[trk].SelectedIndex = (patch > 127) ? -1 : patch;
          cmbPatches[trk].BackColor = GetBackColor(multiple);
          chkMutes[trk].Checked = P.F.Mute[trk];
          Solos[trk].Checked = P.F.Mute.IsSolo(trk);
        }
      }
    }

    private Color GetBackColor(bool multiple) {
      if (multiple) return System.Drawing.SystemColors.ControlLight;
      else return System.Drawing.SystemColors.ButtonHighlight;
    }

    //private void cmdCompMaps_Click(object sender, EventArgs e) {
    //  if (P.F.frmCompare != null) P.F.frmCompare.Close(); 
    //  bool[] selectedtrks = GetSelectedTrks();
    //  if (selectedtrks == null) {
    //    MessageBox.Show("Error: No Track Selected");
    //    return;
    //  }
    //  if (P.F.CF == null) CreateCFtxt();

    //  try {
    //    P.F.frmCompare = new Forms.frmCompare(P.F.MidiFilePath, selectedtrks);
    //  }
    //  catch (MidiFileException) {
    //    return;
    //  }
    //  P.F.frmCompare.Show();
    //}

    private void cmdDeleteNotes_Click(object sender, EventArgs e) {
      using (new clsWaitCursor()) {
        Do.UpdateToBuffer(MouseTrk);
        if (CsrQIHi <= CsrQILo) return;
        int tickslo = CsrQILo * P.F.TicksPerQI;
        int tickshi = CsrQIHi * P.F.TicksPerQI;
        int count = FileStream.DeleteNotes(MouseTrk, tickslo, tickshi);
        if (count > 0) {
          Do.UpdateFromBuffer();
          FileStream.indSave = true;
        }
        CsrQILo = 0;
        CsrQIHi = 0;
        cmdDeleteNotes.Enabled = false;
        Pics[MouseTrk].Refresh();
      }
    }

    //private void cmdSaveMidi_Click(object sender, EventArgs e) {
    //  clsSaveMidiFile.SaveMidiFile();
    //}

    private void frmMultiMap_DragDrop(object sender, DragEventArgs e) {
      P.frmStart.frmStart_DragDrop(sender, e);
    }

    private void frmMultiMap_DragEnter(object sender, DragEventArgs e) {
      P.frmStart.frmStart_DragEnter(sender, e);
    }

    //private void cmdPlayMap_Click(object sender, EventArgs e) {
    //  Utils.FormAct(P.frmSC);
    //}

    //private void cmdPlayMap_Click(object sender, EventArgs e) {
    //  Utils.FormAct(P.frmSC);
    //  P.frmSC.chkShowChords.Checked = false;
    //  P.frmSC.chkShowTracks.Checked = true;
    //  //P.frmSC.Activate();
    //  Utils.FormAct(P.frmSC);
    //}

    //private void cmdCreateChords_Click(object sender, EventArgs e) {
    //  using (new clsWaitCursor()) {
    //    Stopwatch watch = new Stopwatch();
    //    watch.Start();

    //    //* create filestream from selected trks (if necessary)
    //    clsTrks.Array<bool> trkselect = GetSelectedTrks();
    //    if (trkselect == null) trkselect = new clsTrks.Array<bool>(false);
    //    clsFileStream fs;
    //    bool? sel = trkselect[new clsTrks.T(P.F.Trks, 0)];
    //    foreach (bool ts in trkselect) {  //check if all, none, or some trks selected
    //      if (ts != sel) { sel = null; break; }  //some, but not all, trks selected 
    //    }
    //    if (sel == null) {  //some, but not all, trks selected 
    //      try {
    //        fs = new clsFileStream(P.F.Project.MidiPath, trkselect, true, false, true);
    //      }
    //      catch (MidiFileException) {
    //        return;
    //      }
    //    } else {
    //      fs = FileStream;  //all or no trks selected
    //    }

    //    ////* calckeys (all trks)
    //    //P.F.CF = new clsCFPC(false);
    //    //clsTrks.Array<bool> trkselectall = new clsTrks.Array<bool>(true);
    //    //new Forms.frmCalcKeys(FileStream, trkselectall, 90);  //calc without showing form

    //    //* create chords (selected trks)
    //    clsChordSegs chordsegs;
    //    Forms.frmChordMap.ApplyFilterStatic(eAlign.Auto, fs, out chordsegs);
    //    Debug.WriteLine("Chords Exec Millisecs = " + watch.ElapsedMilliseconds);
    //    watch.Stop();
    //  }
    //}

    //internal void cmdLyrics_Click(object sender, EventArgs e) {
    //  ConvertCSV(SelectAllTrks(), true);
    //}

    private void picBars_Paint(object sender, PaintEventArgs e) {
      Bars_Paint(e, picBars, splitContainer.Panel2, BarFont, HFactor, HDiv);
    }

    internal static void Bars_Paint(PaintEventArgs e, PictureBox picbars, Panel pan,
      Font barfont, int hfactor, int hdiv) {
      Graphics xgr = e.Graphics;

      if (P.F.AudioSync != null) {
        Brush recbrushlo = new SolidBrush(Color.FromArgb(64, Color.Red));
        Brush recbrushhi = new SolidBrush(Color.FromArgb(64, Color.Green));
        Brush playbrush = new SolidBrush(Color.FromArgb(255, Color.Yellow));
        int pixlo, width;

        //* paint occupied Elapsed.Play[] bars
        pixlo = e.ClipRectangle.X;
        int pixhi = e.ClipRectangle.X + e.ClipRectangle.Width;
        for (int beat = 0; beat < P.F.AudioSync.Elapsed.Play.Count; beat++) {
          clsMTime.clsBBT bbt = new clsMTime.clsBBT(beat, true);
          int pix = (bbt.Ticks * hfactor) / (hdiv * P.F.TicksPerQI) + pan.AutoScrollPosition.X;
          if (pix > pixhi) break;
          if (pix >= pixlo && P.F.AudioSync.Elapsed.Play[beat] > 0) {
            width = (bbt.TicksPerBeat * hfactor) / (hdiv * P.F.TicksPerQI);
            xgr.FillRectangle(playbrush, pix, 0, width, picbars.ClientSize.Height);
          }
        }

        //* paint BarPaneLo/BarPaneHi csrs
        PaintRecBar(picbars, pan, hfactor, hdiv, xgr, recbrushlo, P.F.BarPaneBBTLo);
        PaintRecBar(picbars, pan, hfactor, hdiv, xgr, recbrushhi, P.F.BarPaneBBTHi);
      }

      Brush brush = Brushes.Black;
      //int maxbar = new clsMTime.clsBBT(P.F.MaxTicks).Bar;
      int maxbar = P.F.MaxBBT.Bar;
      int vismin = -pan.AutoScrollPosition.X;
      int vismax = vismin + pan.ClientSize.Width;
      for (int bar = 0; bar < maxbar; bar++) {
        clsMTime.clsBBT bbtbar = new clsMTime.clsBBT(bar, 0, 0);
        int qbar = bbtbar.Ticks / P.F.TicksPerQI;
        int pix = (qbar * hfactor) / hdiv;
        if (pix < vismin) continue;
        if (pix > vismax) break;
        int xpos = pix + pan.AutoScrollPosition.X;
        xgr.DrawString((bar + 1).ToString(), barfont, brush, xpos, 0);
      }
    }

    private static void PaintRecBar(PictureBox picbars, Panel pan, int hfactor, int hdiv, Graphics xgr, 
      Brush recbrush, clsMTime.clsBBT bbt) {
      //* paint BarPaneLo csr
      int startbarrecord = bbt.Bar;
      int tickslo = new clsMTime.clsBBT(startbarrecord, 0, 0).Ticks;
      int tickshi = new clsMTime.clsBBT(startbarrecord + 1, 0, 0).Ticks;
      int pixlo = ((tickslo * hfactor) / (hdiv * P.F.TicksPerQI)) + pan.AutoScrollPosition.X;
      int width = ((tickshi - tickslo) * hfactor) / (hdiv * P.F.TicksPerQI);
      xgr.FillRectangle(recbrush, pixlo, 0, width, picbars.ClientSize.Height);
    }

    private void cmdPlayAudio_Click(object sender, EventArgs e) {
      if (P.F.AudioSync != null) P.F.AudioSync.StartCmdSyncPlay();
    }

    private void cmdSyncAudio_Click(object sender, EventArgs e) {
      //if (P.F.AutoSync == null) P.F.AutoSync = new clsAutoSyncBeat();
      if (P.F.AudioSync != null) P.F.AudioSync.StartCmdSyncRecord();
    }

    private void cmdPlayAndRecord_Click(object sender, EventArgs e) {
      if (P.F.AudioSync != null) P.F.AudioSync.StartCmdSyncPlayAndRecord();
    }

    private void cmdShowAudioSyncWindow_Click(object sender, EventArgs e) {
      if (P.F.frmAutoSync == null && P.F != null && P.F.AudioSync != null) {
        using (new clsWaitCursor()) {
          P.F.frmAutoSync = new frmAutoSync(P.F.AudioSync);
        }
      }
      //P.F.frmAutoSync.cmdShow_Click(null, null);  //update lists
      Utils.FormAct(P.F.frmAutoSync);
      //P.F.frmAutoSync.Show();
      //P.F.frmAutoSync.WindowState = FormWindowState.Normal;
      //P.F.frmAutoSync.Activate();
    }

    private void picBars_MouseClick(object sender, MouseEventArgs e) {
      Bars_MouseClick(e, HDiv, HFactor, splitContainer.Panel2);
    }

    //internal static clsMTime.clsBBT BarPaneBBTLo, BarPaneBBTHi;
    internal static void Bars_MouseClick(MouseEventArgs e, int hdiv, int hfactor, Panel pan) {
      //if (P.F.AudioSync == null) return;
      int pix = e.X - pan.AutoScrollPosition.X;
      int ticks = (pix * hdiv * P.F.TicksPerQI) / hfactor;
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
      bbt.RoundDownToBar();
      if (e.Button == MouseButtons.Left) {
        P.F.BarPaneBBTLo = bbt;
        P.F.BarPaneBBTHi = bbt;
      } else {
        if (bbt.Bar > P.F.BarPaneBBTLo.Bar) P.F.BarPaneBBTHi = bbt;
      }
      if (P.F.CF != null) P.F.CF.indSave = true;
      P.frmSC.picBarsX.Refresh();
      if (P.F.frmTrackMap != null) P.F.frmTrackMap.picBars.Refresh();
      if (P.F.frmChordMap != null) P.F.frmChordMap.picBars.Refresh();
      if (P.frmSC.panMaps.Visible) P.frmSC.picBarsX.Refresh();
    }

    internal void chkShowLyrics_CheckedChanged(object sender, EventArgs e) {
      //Static_Lyrics = chkShowLyrics.Checked;
      dgvLyrics.Visible = chkShowLyrics.Checked;
      lblLyricsLit.Visible = chkShowLyrics.Checked;
      if (chkShowLyrics.Checked) LoadDGVLyrics();  //hide if lyrics don't exist
      VResize();
    }

    private void dgvLyrics_Scroll(object sender, ScrollEventArgs e) {
      ScrollFromdgvLyrics = true;
      if (!ScrollFromPan && !ScrollFromRefreshBBT) {
        splitContainer.Panel2.AutoScrollPosition = new Point(e.NewValue, splitContainer.Panel2.AutoScrollPosition.Y);
        //DGV.HorizontalScrollingOffset = e.NewValue;
        Refresh();
      }
      ScrollFromdgvLyrics = false;
    }

    //private void chkShowCheckedChanged_Live() {
    //  Point scroll = splitContainer.Panel2.AutoScrollPosition;
    //  Point setscroll = new Point(-scroll.X, -scroll.Y);
    //  LoadTracks();
    //  splitContainer.Panel2.AutoScrollPosition = setscroll;
    //  pan1inner.Top = splitContainer.Panel2.AutoScrollPosition.Y;
    //  Refresh();
    //}

    internal void chkShowCheckedChanged() {
      chkShowCheckedChanged(false, true);
    }

    internal void chkShowCheckedChanged(bool indmax, bool indtrkselect) {
      if (Bypass_Event) return;
      Point scroll = splitContainer.Panel2.AutoScrollPosition;
      //if (indtrkselect) SetTrkSelect();
      LoadTracks();
      Refresh();
      if (indmax) {
        int max = splitContainer.Panel2.VerticalScroll.Maximum;
        splitContainer.Panel2.AutoScrollPosition = new Point(-scroll.X, max);
      } else {
        splitContainer.Panel2.AutoScrollPosition = new Point(-scroll.X, -scroll.Y);
      }
    }

    //private void chkShowEmpty_CheckedChanged(object sender, EventArgs e) {
    //  if (Bypass_Event) return;
    //  Static_ShowEmpty = chkShowEmpty.Checked;
    //  chkShowCheckedChanged();
    //}

    //private void chkShowPercussion_CheckedChanged(object sender, EventArgs e) {
    //  Static_Percussion = chkShowPercussion.Checked;
    //  chkShowCheckedChanged();
    //}

    //private void chkShowMelody_CheckedChanged(object sender, EventArgs e) {
    //  Static_Melody = chkShowMelody.Checked;
    //  chkShowCheckedChanged();
    //}

    //private void chkShowChords_CheckedChanged(object sender, EventArgs e) {
    //  Static_Chords = chkShowChords.Checked;
    //  chkShowCheckedChanged();
    //}

    //private void chkShowBass_CheckedChanged(object sender, EventArgs e) {
    //  Static_Bass = chkShowBass.Checked;
    //  chkShowCheckedChanged();
    //}

    //private void chkShowSparse_CheckedChanged(object sender, EventArgs e) {
    //  Static_Sparse = chkShowSparse.Checked;
    //  chkShowCheckedChanged();
    //}

    //private void chkShowNoStyle_CheckedChanged(object sender, EventArgs e) {
    //  Static_NoStyle = chkShowNoStyle.Checked;
    //  chkShowCheckedChanged();
    //}

    private void dgvLyrics_CellClick(object sender, DataGridViewCellEventArgs e) {
      if (!P.frmSC.IsPlayClickable()) return;
      if (Bypass_DGV) return;
      if (!Form_Loaded) return;
      if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.None) return;
      //if (P.F.CurrentBBT.Bar == e.ColumnIndex) return;
      Bypass_DGV = true;
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(e.ColumnIndex, 0, 0);  //locate to bar/beat
      NoSelectDGV = dgvLyrics;
      P.frmSC.Play?.NewReset();
      clsIShowNoteMap.SetPlayCsr(bbt);  //-> RefreshBBT()
      Refresh();
      lblTicks.Text = bbt.Ticks.ToString();
      NoSelectDGV = null;
      Bypass_DGV = false;
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Utils.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_TrackMap_Intro.htm");
    }

    private void cmdTonnetz_Click(object sender, EventArgs e) {
      if (P.F == null) return;
      if (P.F.frmTonnetz == null) {
        P.F.frmTonnetz = new Forms.frmTonnetz(this);  //-->Show()
      }
      //P.F.frmTonnetz.Activate();
      Utils.FormAct(P.F.frmTonnetz);
    }

    private void cmdAddTrack_Click(object sender, EventArgs e) {
      Do.UpdateToBuffer(null);
      if (!AddTrack(true)) return;
      Do.UpdateFromBuffer();
    }

    //private void AddTrack(bool indmax) {
    //  clsTrks.T trk = P.F.Trks.AddTrack();
    //  if (trk == null) return;
    //  if (chkShowEmpty.Checked) {
    //    chkShowCheckedChanged(indmax);
    //    //SetTrkSelect();
    //    //LoadTrackPic(trk);
    //    //LoadTrackPan(trk);
    //    //PicRefresh();
    //  } else {
    //    chkShowEmpty.Checked = true;  //-> LoadTracks() 
    //  }
    //}

    private void SetNewTrkCtls(clsTrks.T trk, int chan) {
      //* set patch, vol, pan on new track
      int patchval = P.frmSC.cmbKBChanPatch.SelectedIndex - 1;

      P.F.Chan[trk] = chan;
      if (patchval >= 0) P.F.Patch[chan] = patchval;
      P.F.Vol[chan] = P.frmSC.trkKBChanVol.Value;
      P.F.Pan[chan] = P.frmSC.trkKBChanPan.Value;
      //if (patchval >= 0) MidiCtlrs.SetDataLastStart(chan, clsMidiCtlrs.PatchCtlrNum, patchval);
      //MidiCtlrs.SetDataLastStart(chan, 7, P.frmSC.trkKBChanVol.Value);
      //MidiCtlrs.SetDataLastStart(chan, 10, P.frmSC.trkKBChanPan.Value);

      //if (patchval >= 0) cmbPatches[trk].SelectedIndex = patchval;
      //TrkBar[trk].Value = (optPan.Checked) ? P.frmSC.trkKBChanPan.Value : P.frmSC.trkKBChanVol.Value;
    }

    private bool AddTrack(bool indmax) {
      clsTrks.T trk = P.F.Trks.AddTrack();
      if (trk == null) return false;
      P.F.Added[trk] = true;
      int chan = FileStream.SetNewTrkChan(trk);
      SetNewTrkCtls(trk, chan);
      FileStream.MidiCtlrs.SendCtlrs(P.F.CurrentBBT.Ticks, chan);
      NoteMap.InitPWTrk(trk);  //PW centre (default)
      //chkShowCheckedChanged(indmax);  // -> LoadTracks()
      LoadTrack(trk);
      P.F.frmTrackStyles?.SetChks();
      P.F.frmTrackStyles?.UpdateCounts();
      return true;
    }

    private void SubTrack() {
      foreach (clsTrks.T t in P.F.Added.Next) P.F.Added[t] = false;
      clsTrks.T trk = P.F.Trks.SubTrack();
      if (trk == null) return;
      chkShowCheckedChanged();
      P.F.frmTrackStyles?.SetChks();
      P.F.frmTrackStyles?.UpdateCounts();
      //if (chkShowEmpty.Checked) {
      //  chkShowCheckedChanged();
      //} else {
      //  chkShowEmpty.Checked = true;  //-> LoadTracks() 
      //}
    }

    //private bool ClearUndoRedo() {  //not currently in use
    //  //* return false if cancelled
    //  if (UndoStack.Count == 0 && RedoStack.Count == 0) return true;
    //  string msg = "This will clear any Undo or Redo actions";
    //  msg += "\r\nClick 'OK' to continue, or 'Cancel' to abandon";
    //  if (MessageBox.Show(msg, MessageBoxButtons.OKCancel)
    //    == DialogResult.Cancel) return false;
    //  cmdUndo.Enabled = false;
    //  cmdRedo.Enabled = false;
    //  UndoStack.Clear();
    //  RedoStack.Clear();
    //  return true;
    //}

    internal void PlayOff() {
      //* called from frmStart.StreamPlayOffAll()
      if (RecTrk != null && FileStream?.RecStrmNew != null && FileStream.RecStrmNew.Count > 0) {
        //* look for on/off evs
        bool onoff = false;
        foreach (clsFileStream.clsEvShort ev in FileStream.RecStrmNew) {
          int status = ev.Status & 0xf0;
          if (status == 0x80 || status == 0x90) {  //ON or OFF ev
            onoff = true;
            break;
          }
        }
        if (onoff) Do.UpdateFromBuffer();
      }
    }

    private void cmdUndo_Click(object sender, EventArgs e) {
      Do.Undo();
      FileStream.indSave = true;
      Refresh();
    }

    private void cmdRedo_Click(object sender, EventArgs e) {
      Do.ReDo();
      FileStream.indSave = true;
      Refresh();
    }

    internal class clsUndoRedo {
      private Forms.frmTrackMap Frm;
      private LLStack<clsItem> UndoStack;
      private LLStack<clsItem> RedoStack;
      private clsItem UndoBuffer;

      internal clsUndoRedo(Forms.frmTrackMap frm) {
        Frm = frm;
        int capacity = (int)P.frmStart.nudUndoRedoCapacity.Value;
        UndoStack = new LLStack<clsItem>(capacity);
        RedoStack = new LLStack<clsItem>(capacity);
      }

      internal void UpdateFromBuffer() {
        if (UndoBuffer == null) {
          //* may happen because called multiple times
          //LogicError.Throw(eLogicError.X155);
          return;
        }
        UndoStack.Push(UndoBuffer);  //should be same as current state
        UndoBuffer = null;
        SetCmdStateUndoRedo();
      }

      internal void UpdateToBuffer(clsTrks.T trk) {
        //* called before delete notes or playmidi with recording track active
        clsItem item = new clsItem(Frm);
        item.CopyLive(trk);
        UndoBuffer = item;
      }

      internal void Undo() {
        if (UndoStack.Count == 0) return;
        clsItem item = new clsItem(Frm);
        clsTrks.T trk = UndoStack.Peek().Trk;
        item.CopyLive(trk);
        RedoStack.Push(item);
        UndoStack.Pop().MakeLive();
        SetCmdStateUndoRedo();
      }

      internal void ReDo() {
        if (RedoStack.Count == 0) return;
        clsItem item = new clsItem(Frm);
        clsTrks.T trk = RedoStack.Peek().Trk;
        item.CopyLive(trk);
        UndoStack.Push(item);
        RedoStack.Pop().MakeLive();
        SetCmdStateUndoRedo();
      }

      internal void SetCmdStateUndoRedo() {
        Frm.cmdUndo.Enabled = (UndoStack.Count > 0);
        Frm.cmdRedo.Enabled = (RedoStack.Count > 0);
        Frm.cmdUndo.Text = (UndoStack.Count > 0) ?
          "Undo\r\n(" + UndoStack.Count + ")" : "Undo";
        Frm.cmdRedo.Text = (RedoStack.Count > 0) ?
          "Redo\r\n(" + RedoStack.Count + ")" : "Redo";
      }

      private class clsItem {
        internal clsItem(Forms.frmTrackMap frm) {
          Frm = frm;
        }

        private Forms.frmTrackMap Frm;
        private int NumTrks;
        internal clsTrks.T Trk;
        //private bool indChanged;
        private ushort[] Trk_Map;
        private short[] Trk_PB;  //pitchbend [qtime]
        private BigInteger[] Trk_FullMap;
        private int Trk_TrkMinPitch;
        private int Trk_TrkMaxPitch;
        private int Trk_OnCount;
        private int[] Trk_ChanOnCount;
        private int[] Trk_ChanAllCount;
        private clsFileStream.clsEvStrm[] Strm;

        internal clsItem Copy() {
          //* excluding Trk_Selected
          clsItem copy = new clsItem(Frm);
          copy.NumTrks = NumTrks;
          if (Trk == null) return copy;  //addtrk/subtrk only
                                         //copy.indChanged = indChanged;
          copy.Trk = Trk.Copy();
          copy.Trk_Map = Trk_Map.ToArray();
          copy.Trk_FullMap = Trk_FullMap.ToArray();
          copy.Trk_PB = Trk_PB.ToArray();
          copy.Trk_TrkMinPitch = Trk_TrkMinPitch;
          copy.Trk_TrkMaxPitch = Trk_TrkMaxPitch;
          copy.Trk_OnCount = Trk_OnCount;
          copy.Trk_ChanOnCount = Trk_ChanOnCount.ToArray();
          copy.Trk_ChanAllCount = Trk_ChanAllCount.ToArray();
          copy.Strm = Strm.ToArray();
          return copy;
        }

        internal void CopyLive(clsTrks.T trk) {
          //* excluding Trk_Selected
          NumTrks = P.F.Trks.NumTrks;
          if (trk == null) return;  //addtrk/subtrk only
          Trk = trk.Copy();
          //indChanged = Frm.indChanged;
          clsFileStream fs = Frm.FileStream;
          Trk_Map = fs.NoteMap.Map_GetTrk(Trk).ToArray();
          Trk_FullMap = fs.NoteMap.FullMap_GetTrk(Trk).ToArray();
          Trk_PB = fs.NoteMap.PB[Trk].ToArray();
          Trk_TrkMinPitch = fs.TrkMinPitch[Trk];
          Trk_TrkMaxPitch = fs.TrkMaxPitch[Trk];
          Trk_OnCount = fs.OnCount[Trk];
          Trk_ChanOnCount = fs.ChanOnCount[Trk].ToArray();
          Trk_ChanAllCount = fs.ChanAllCount[Trk].ToArray();
          Strm = fs.Strm.ToArray();
        }

        internal void MakeLive() {
          Frm.MouseTrk = null;
          Frm.CsrQILo = 0;
          Frm.CsrQIHi = 0;
          if (P.F.Trks.NumTrks != NumTrks) {
            if (NumTrks == P.F.Trks.NumTrks + 1) Frm.AddTrack(false);
            else if (NumTrks == P.F.Trks.NumTrks - 1) Frm.SubTrack();
            else LogicError.Throw(eLogicError.X147);
          }
          //Frm.RecTrk = Trk;
          if (Trk == null) return;  //addtrk/subtrk only
          clsFileStream fs = Frm.FileStream;
          //Frm.indChanged = indChanged;
          fs.NoteMap.Delete = null;
          fs.NoteMap.Map_SetTrk(Trk, Trk_Map);
          fs.NoteMap.FullMap_SetTrk(Trk, Trk_FullMap);
          fs.NoteMap.PB[Trk] = Trk_PB;
          fs.TrkMinPitch[Trk] = Trk_TrkMinPitch;
          fs.TrkMaxPitch[Trk] = Trk_TrkMaxPitch;
          fs.OnCount[Trk] = Trk_OnCount;
          fs.ChanOnCount[Trk] = Trk_ChanOnCount;
          fs.ChanAllCount[Trk] = Trk_ChanAllCount;
          fs.Strm = Strm;
          fs.StrmLL = new clsFileStream.clsStrmLL(fs, Strm);

          //Frm.ReloadTracks();  //Frm.RefreshPic(Trk);
          Frm.LoadTrack(Trk);
        }
      }
    }

    //private void chkSelectArea_CheckedChanged(object sender, EventArgs e) {
    //  DeleteNotesCount = 0;
    //  CsrQILo = 0;
    //  CsrQIHi = 0;
    //  NoteMap.Delete = null;
    //  cmdDeleteNotes.Enabled = false;
    //  Refresh();
    //}

    private void cmdSaveMidi_Click(object sender, EventArgs e) {
      P.frmSC.SaveMidiFileAs();
    }

    private void frmTrackMap_KeyDown(object sender, KeyEventArgs e) {
      if (e.Control) {
        switch (e.KeyCode) {
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

    private void splitContainer_Panel2_Paint(object sender, PaintEventArgs e) {
      foreach (clsTrks.T trk in Pans.Next) {
        if (TrkShow[trk]) {
          Pans[trk].Top = Pics[trk].Top;
          Pans[trk].Height = (chkCollapses[trk].Checked) ? ctlChans[trk].Top : Pics[trk].Height;
        }
      }
    }

    private void chkEvenSpacing_CheckedChanged(object sender, EventArgs e) {
      VFactor = InitialVFactor;
      PicRefresh();
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

    private void cmdSaveProject_MouseUp(object sender, MouseEventArgs e) {
      if (e.Button == MouseButtons.Right) {  //Click not raised
        P.F.SaveProject(null);  //check and save
      }
    }

    private void dgvLyrics_ColumnAdded(object sender, DataGridViewColumnEventArgs e) {
      e.Column.FillWeight = 10;  //to get round column limit (totalfillweight < 65535) default 100
    }

    internal bool indLocatePics = true;
    private void cmdCollapseAll_Click(object sender, EventArgs e) {
      indLocatePics = false;
      foreach (CheckBox chk in chkCollapses) if (chk != null) chk.Checked = true;
      LocatePics();
      P.F.frmTrackStyles?.SetChks();
      indLocatePics = true;
    }

    private void cmdUncollapseAll_Click(object sender, EventArgs e) {
      indLocatePics = false;
      foreach (CheckBox chk in chkCollapses) if (chk != null) chk.Checked = false;
      LocatePics();
      indLocatePics = true;
      P.F.frmTrackStyles?.SetChks();
    }

    private void cmdTrkStyles_Click(object sender, EventArgs e) {
      if (P.F.frmTrackStyles == null) P.F.frmTrackStyles = new frmTrackStyles();
      Utils.FormAct(P.F.frmTrackStyles);
    }

    private void cmdUpdateLyrics_Click(object sender, EventArgs e) {
      if (P.F.frmLyrics == null) {
        P.F.frmLyrics = new frmLyrics();
        //P.F.frmLyrics.Show();
      }
      //P.F.frmLyrics.Activate();
      Utils.FormAct(P.F.frmLyrics);
    }
  }
}
