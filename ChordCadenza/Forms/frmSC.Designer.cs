namespace ChordCadenza.Forms {
  partial class frmSC {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
      this.txtChordBottom = new System.Windows.Forms.TextBox();
      this.fontDialog1 = new System.Windows.Forms.FontDialog();
      this.trkKBChanVol = new System.Windows.Forms.TrackBar();
      this.trkKBChanPan = new System.Windows.Forms.TrackBar();
      this.chkAlignKB = new System.Windows.Forms.CheckBox();
      this.chkShowChords = new System.Windows.Forms.CheckBox();
      this.chkShowTracks = new System.Windows.Forms.CheckBox();
      this.chkShowChordNames = new System.Windows.Forms.CheckBox();
      this.cmdGoToStart = new System.Windows.Forms.Button();
      this.cmdPlayMidi = new System.Windows.Forms.Button();
      this.cmdStopPlay = new System.Windows.Forms.Button();
      this.chkShowBeats = new System.Windows.Forms.CheckBox();
      this.cmdPausePlay = new System.Windows.Forms.Button();
      this.lblnudSyncopation = new System.Windows.Forms.Label();
      this.lblnudSyncopationNote = new System.Windows.Forms.Label();
      this.lblnudSyncopationSlash = new System.Windows.Forms.Label();
      this.nudSyncopationNN = new System.Windows.Forms.NumericUpDown();
      this.nudSyncopationDD = new System.Windows.Forms.NumericUpDown();
      this.lbltrkKBChanVol = new System.Windows.Forms.Label();
      this.lblnudOctaves = new System.Windows.Forms.Label();
      this.nudOctaves = new System.Windows.Forms.NumericUpDown();
      this.lblnudStartBar = new System.Windows.Forms.Label();
      this.nudStartBar = new System.Windows.Forms.NumericUpDown();
      this.lblnudBeatHeight = new System.Windows.Forms.Label();
      this.nudBeatHeight = new System.Windows.Forms.NumericUpDown();
      this.lbltrkKBChanPan = new System.Windows.Forms.Label();
      this.panControls = new System.Windows.Forms.Panel();
      this.trkPCKBVel = new System.Windows.Forms.TrackBar();
      this.chkManSyncChord = new System.Windows.Forms.CheckBox();
      this.cmdResetPlay = new System.Windows.Forms.Button();
      this.chkShowChordsRel = new System.Windows.Forms.CheckBox();
      this.grpSustainAction = new System.Windows.Forms.GroupBox();
      this.optSustainNormal = new System.Windows.Forms.RadioButton();
      this.optSustainSendCtlr = new System.Windows.Forms.RadioButton();
      this.optSustainReplay = new System.Windows.Forms.RadioButton();
      this.cmdTonnetz = new System.Windows.Forms.Button();
      this.lblRangeTrk = new System.Windows.Forms.Label();
      this.chkSwitchKBChord = new System.Windows.Forms.CheckBox();
      this.panDisplay = new System.Windows.Forms.Panel();
      this.cmdNoteFont = new System.Windows.Forms.Button();
      this.cmdChordFont = new System.Windows.Forms.Button();
      this.cmdColours = new System.Windows.Forms.Button();
      this.lbllblRangeTrk = new System.Windows.Forms.Label();
      this.lblKeyVel = new System.Windows.Forms.Label();
      this.chkSwitchSustain = new System.Windows.Forms.CheckBox();
      this.panForms = new System.Windows.Forms.Panel();
      this.cmdChordMap = new System.Windows.Forms.Button();
      this.cmdMultiMap = new System.Windows.Forms.Button();
      this.cmdUpdateLyrics = new System.Windows.Forms.Button();
      this.cmdShowSumm = new System.Windows.Forms.Button();
      this.cmdShowAudioSyncWindow = new System.Windows.Forms.Button();
      this.lblKeyThis = new System.Windows.Forms.Label();
      this.panPlay = new System.Windows.Forms.Panel();
      this.cmdPlayAndRecordAudio = new System.Windows.Forms.Button();
      this.cmdPanic = new System.Windows.Forms.Button();
      this.cmdSyncAudio = new System.Windows.Forms.Button();
      this.cmdPlayAudio = new System.Windows.Forms.Button();
      this.panFiles = new System.Windows.Forms.Panel();
      this.cmdNew = new System.Windows.Forms.Button();
      this.cmdLoadProject = new System.Windows.Forms.Button();
      this.cmdSaveProject = new System.Windows.Forms.Button();
      this.cmdSaveProjectAs = new System.Windows.Forms.Button();
      this.lblKeyNext = new System.Windows.Forms.Label();
      this.lblRangeVis = new System.Windows.Forms.Label();
      this.lbllblRangeVis = new System.Windows.Forms.Label();
      this.panTrkStream = new System.Windows.Forms.Panel();
      this.lbltrkStreamVol = new System.Windows.Forms.Label();
      this.trkStreamVol = new System.Windows.Forms.TrackBar();
      this.trkStreamPan = new System.Windows.Forms.TrackBar();
      this.lbltrkStreamPan = new System.Windows.Forms.Label();
      this.panTrkKB = new System.Windows.Forms.Panel();
      this.lbltrkKBVol = new System.Windows.Forms.Label();
      this.trkKBVol = new System.Windows.Forms.TrackBar();
      this.trkKBPan = new System.Windows.Forms.TrackBar();
      this.lbltrkKBPan = new System.Windows.Forms.Label();
      this.panTrkAudio = new System.Windows.Forms.Panel();
      this.lbltrkAudioVol = new System.Windows.Forms.Label();
      this.trkAudioVol = new System.Windows.Forms.TrackBar();
      this.trkAudioPan = new System.Windows.Forms.TrackBar();
      this.lbltrkAudioPan = new System.Windows.Forms.Label();
      this.lbllblKeyNext = new System.Windows.Forms.Label();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.lbllblKeyThis = new System.Windows.Forms.Label();
      this.lblcmbFirstNote = new System.Windows.Forms.Label();
      this.grpNoteDisplay = new System.Windows.Forms.GroupBox();
      this.chkShowPCKBChar = new System.Windows.Forms.CheckBox();
      this.chkRunChordNotes = new System.Windows.Forms.CheckBox();
      this.optShowNone = new System.Windows.Forms.RadioButton();
      this.optShowNoteName = new System.Windows.Forms.RadioButton();
      this.optShowSolfa = new System.Windows.Forms.RadioButton();
      this.cmbFirstNote = new System.Windows.Forms.ComboBox();
      this.chkShowChordNotes = new System.Windows.Forms.CheckBox();
      this.cmdAlign = new System.Windows.Forms.Button();
      this.grpOctTrans = new System.Windows.Forms.GroupBox();
      this.lblnudOctTransposeKB = new System.Windows.Forms.Label();
      this.nudOctTransposeKB = new System.Windows.Forms.NumericUpDown();
      this.lblnudOctTransposeDisplay = new System.Windows.Forms.Label();
      this.nudOctTransposeDisplay = new System.Windows.Forms.NumericUpDown();
      this.lblnudOctTransposeKBPitch = new System.Windows.Forms.Label();
      this.nudOctTransposeKBPitch = new System.Windows.Forms.NumericUpDown();
      this.grpCapitalizeRoots = new System.Windows.Forms.GroupBox();
      this.chkCapitalizeRoots = new System.Windows.Forms.CheckBox();
      this.chkAutoCapitalize = new System.Windows.Forms.CheckBox();
      this.grpMisc = new System.Windows.Forms.GroupBox();
      this.grpPlayMode = new System.Windows.Forms.GroupBox();
      this.optModeKB = new System.Windows.Forms.RadioButton();
      this.optModeChords = new System.Windows.Forms.RadioButton();
      this.grpSemitoneTrans = new System.Windows.Forms.GroupBox();
      this.lblnudTransposeStreamPitch = new System.Windows.Forms.Label();
      this.nudTransposeStreamPitch = new System.Windows.Forms.NumericUpDown();
      this.lblnudTransposeKB = new System.Windows.Forms.Label();
      this.nudTransposeKB = new System.Windows.Forms.NumericUpDown();
      this.lblnudTransposeKBPitch = new System.Windows.Forms.Label();
      this.nudTransposeKBPitch = new System.Windows.Forms.NumericUpDown();
      this.lblKBDisplacement = new System.Windows.Forms.Label();
      this.nudKBDisplacement = new System.Windows.Forms.NumericUpDown();
      this.cmdTempoReset = new System.Windows.Forms.Button();
      this.lblTempo = new System.Windows.Forms.Label();
      this.lbltrkTempo = new System.Windows.Forms.Label();
      this.trkTempo = new System.Windows.Forms.TrackBar();
      this.grpKBChan = new System.Windows.Forms.GroupBox();
      this.lblnudKBChanOut = new System.Windows.Forms.Label();
      this.lblcmbKBChanPatch = new System.Windows.Forms.Label();
      this.nudKBChanOut = new System.Windows.Forms.NumericUpDown();
      this.cmbKBChanPatch = new System.Windows.Forms.ComboBox();
      this.cmdXNeg = new System.Windows.Forms.Button();
      this.cmdXPos = new System.Windows.Forms.Button();
      this.lstTrks = new System.Windows.Forms.ListBox();
      this.mnuWindow = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuLoadProject = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuRecent = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuReloadProject = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuImport = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuLoadMultiMidi = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuSaveProject = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSaveProjectAs = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSaveMidiFileAs = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuSaveSettings = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuRestart = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuConfig = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuCfgAudio = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuCfgMidi = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuKeyboardRanges = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuCfgBezier = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuCfgBezierVelocity = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuCfgBezierAfterTouch = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuCfgSwitch = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuPaths = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuPathProject = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuPathSoundFonts = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuPathMidiFiles = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuPathAudioFiles = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuManChordSync = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuPCKBKeys = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuCfgMisc = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuShowAudioSyncWindow = new System.Windows.Forms.ToolStripMenuItem();
      this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMap = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuControls = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuHelpContents = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuShowInitialScreen = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuWebPage = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSoundFonts = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuDebug = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuShowFrmTest = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuShowFrmConsole = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuShowDebugInfo = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMonitorStats = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMonitorTimeline = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMonitor = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuTestMidiMon = new System.Windows.Forms.ToolStripMenuItem();
      this.fbd = new System.Windows.Forms.FolderBrowserDialog();
      this.helpProvider1 = new System.Windows.Forms.HelpProvider();
      this.panMaps = new System.Windows.Forms.Panel();
      this.chkScroll = new System.Windows.Forms.CheckBox();
      this.panNoteMap = new System.Windows.Forms.Panel();
      this.picChordNamesX = new System.Windows.Forms.PictureBox();
      this.picNoteMap = new System.Windows.Forms.PictureBox();
      this.dgvLyrics = new System.Windows.Forms.DataGridView();
      this.picBarsX = new System.Windows.Forms.PictureBox();
      this.picBottom = new System.Windows.Forms.PictureBox();
      this.picChordNames = new System.Windows.Forms.PictureBox();
      this.picChords = new System.Windows.Forms.PictureBox();
      this.picBars = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.trkKBChanVol)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkKBChanPan)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudSyncopationNN)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudSyncopationDD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudOctaves)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudStartBar)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudBeatHeight)).BeginInit();
      this.panControls.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trkPCKBVel)).BeginInit();
      this.grpSustainAction.SuspendLayout();
      this.panDisplay.SuspendLayout();
      this.panForms.SuspendLayout();
      this.panPlay.SuspendLayout();
      this.panFiles.SuspendLayout();
      this.panTrkStream.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trkStreamVol)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkStreamPan)).BeginInit();
      this.panTrkKB.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trkKBVol)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkKBPan)).BeginInit();
      this.panTrkAudio.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trkAudioVol)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkAudioPan)).BeginInit();
      this.grpNoteDisplay.SuspendLayout();
      this.grpOctTrans.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudOctTransposeKB)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudOctTransposeDisplay)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudOctTransposeKBPitch)).BeginInit();
      this.grpCapitalizeRoots.SuspendLayout();
      this.grpMisc.SuspendLayout();
      this.grpPlayMode.SuspendLayout();
      this.grpSemitoneTrans.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudTransposeStreamPitch)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudTransposeKB)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudTransposeKBPitch)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudKBDisplacement)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkTempo)).BeginInit();
      this.grpKBChan.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudKBChanOut)).BeginInit();
      this.mnuWindow.SuspendLayout();
      this.panMaps.SuspendLayout();
      this.panNoteMap.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picChordNamesX)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picNoteMap)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvLyrics)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picBarsX)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picBottom)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picChordNames)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picChords)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picBars)).BeginInit();
      this.SuspendLayout();
      // 
      // vScrollBar1
      // 
      this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.vScrollBar1.LargeChange = 1;
      this.vScrollBar1.Location = new System.Drawing.Point(1500, 492);
      this.vScrollBar1.Name = "vScrollBar1";
      this.vScrollBar1.Size = new System.Drawing.Size(16, 71);
      this.vScrollBar1.TabIndex = 63;
      this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
      // 
      // txtChordBottom
      // 
      this.txtChordBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.txtChordBottom.BackColor = System.Drawing.SystemColors.Control;
      this.txtChordBottom.Location = new System.Drawing.Point(1404, 570);
      this.txtChordBottom.Multiline = true;
      this.txtChordBottom.Name = "txtChordBottom";
      this.txtChordBottom.ReadOnly = true;
      this.txtChordBottom.Size = new System.Drawing.Size(90, 44);
      this.txtChordBottom.TabIndex = 65;
      this.txtChordBottom.WordWrap = false;
      // 
      // trkKBChanVol
      // 
      this.trkKBChanVol.AutoSize = false;
      this.trkKBChanVol.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      this.trkKBChanVol.LargeChange = 8;
      this.trkKBChanVol.Location = new System.Drawing.Point(70, 80);
      this.trkKBChanVol.Maximum = 127;
      this.trkKBChanVol.Name = "trkKBChanVol";
      this.trkKBChanVol.Size = new System.Drawing.Size(126, 16);
      this.trkKBChanVol.TabIndex = 131;
      this.trkKBChanVol.TabStop = false;
      this.trkKBChanVol.TickFrequency = 8;
      this.trkKBChanVol.Value = 80;
      this.trkKBChanVol.Scroll += new System.EventHandler(this.trkKBChanVol_Scroll);
      // 
      // trkKBChanPan
      // 
      this.trkKBChanPan.AutoSize = false;
      this.trkKBChanPan.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      this.trkKBChanPan.LargeChange = 8;
      this.trkKBChanPan.Location = new System.Drawing.Point(70, 100);
      this.trkKBChanPan.Maximum = 127;
      this.trkKBChanPan.Name = "trkKBChanPan";
      this.trkKBChanPan.Size = new System.Drawing.Size(126, 16);
      this.trkKBChanPan.TabIndex = 132;
      this.trkKBChanPan.TabStop = false;
      this.trkKBChanPan.TickFrequency = 64;
      this.trkKBChanPan.Value = 64;
      this.trkKBChanPan.Scroll += new System.EventHandler(this.trkKBChanPan_Scroll);
      // 
      // chkAlignKB
      // 
      this.chkAlignKB.AutoSize = true;
      this.chkAlignKB.ForeColor = System.Drawing.Color.Red;
      this.chkAlignKB.Location = new System.Drawing.Point(1346, 103);
      this.chkAlignKB.Name = "chkAlignKB";
      this.chkAlignKB.Size = new System.Drawing.Size(97, 17);
      this.chkAlignKB.TabIndex = 159;
      this.chkAlignKB.Text = "Align Keyboard";
      this.chkAlignKB.UseVisualStyleBackColor = true;
      this.chkAlignKB.CheckedChanged += new System.EventHandler(this.chkAlignKB_CheckedChanged);
      // 
      // chkShowChords
      // 
      this.chkShowChords.AutoSize = true;
      this.chkShowChords.Checked = true;
      this.chkShowChords.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkShowChords.Location = new System.Drawing.Point(400, 157);
      this.chkShowChords.Name = "chkShowChords";
      this.chkShowChords.Size = new System.Drawing.Size(89, 17);
      this.chkShowChords.TabIndex = 147;
      this.chkShowChords.Text = "Show Chords";
      this.chkShowChords.UseVisualStyleBackColor = true;
      this.chkShowChords.CheckedChanged += new System.EventHandler(this.chkShowChords_CheckedChanged_1);
      // 
      // chkShowTracks
      // 
      this.chkShowTracks.AutoSize = true;
      this.chkShowTracks.Location = new System.Drawing.Point(400, 140);
      this.chkShowTracks.Name = "chkShowTracks";
      this.chkShowTracks.Size = new System.Drawing.Size(89, 17);
      this.chkShowTracks.TabIndex = 111;
      this.chkShowTracks.Text = "Show Tracks";
      this.chkShowTracks.UseVisualStyleBackColor = true;
      this.chkShowTracks.CheckedChanged += new System.EventHandler(this.chkShowTracks_CheckedChanged);
      // 
      // chkShowChordNames
      // 
      this.chkShowChordNames.AutoSize = true;
      this.chkShowChordNames.Checked = true;
      this.chkShowChordNames.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkShowChordNames.Location = new System.Drawing.Point(400, 80);
      this.chkShowChordNames.Name = "chkShowChordNames";
      this.chkShowChordNames.Size = new System.Drawing.Size(120, 17);
      this.chkShowChordNames.TabIndex = 110;
      this.chkShowChordNames.Text = "Show Chord Names";
      this.chkShowChordNames.UseVisualStyleBackColor = true;
      this.chkShowChordNames.CheckedChanged += new System.EventHandler(this.chkShowChords_CheckedChanged);
      // 
      // cmdGoToStart
      // 
      this.cmdGoToStart.Location = new System.Drawing.Point(0, 0);
      this.cmdGoToStart.Name = "cmdGoToStart";
      this.cmdGoToStart.Size = new System.Drawing.Size(50, 48);
      this.cmdGoToStart.TabIndex = 12;
      this.cmdGoToStart.TabStop = false;
      this.cmdGoToStart.Text = "Goto Start";
      this.cmdGoToStart.UseVisualStyleBackColor = true;
      this.cmdGoToStart.Click += new System.EventHandler(this.cmdGoToStart_Click);
      // 
      // cmdPlayMidi
      // 
      this.cmdPlayMidi.Enabled = false;
      this.cmdPlayMidi.Location = new System.Drawing.Point(50, 0);
      this.cmdPlayMidi.Name = "cmdPlayMidi";
      this.cmdPlayMidi.Size = new System.Drawing.Size(50, 48);
      this.cmdPlayMidi.TabIndex = 32;
      this.cmdPlayMidi.TabStop = false;
      this.cmdPlayMidi.Text = "Play Midi";
      this.cmdPlayMidi.UseVisualStyleBackColor = true;
      this.cmdPlayMidi.Click += new System.EventHandler(this.cmdPlayMidi_Click);
      // 
      // cmdStopPlay
      // 
      this.cmdStopPlay.Enabled = false;
      this.cmdStopPlay.Location = new System.Drawing.Point(250, 0);
      this.cmdStopPlay.Name = "cmdStopPlay";
      this.cmdStopPlay.Size = new System.Drawing.Size(50, 48);
      this.cmdStopPlay.TabIndex = 33;
      this.cmdStopPlay.TabStop = false;
      this.cmdStopPlay.Text = "Stop Play";
      this.cmdStopPlay.UseVisualStyleBackColor = true;
      this.cmdStopPlay.Click += new System.EventHandler(this.cmdStopPlay_Click);
      // 
      // chkShowBeats
      // 
      this.chkShowBeats.AutoSize = true;
      this.chkShowBeats.Checked = true;
      this.chkShowBeats.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkShowBeats.Location = new System.Drawing.Point(400, 62);
      this.chkShowBeats.Name = "chkShowBeats";
      this.chkShowBeats.Size = new System.Drawing.Size(83, 17);
      this.chkShowBeats.TabIndex = 19;
      this.chkShowBeats.Text = "Show Beats";
      this.chkShowBeats.UseVisualStyleBackColor = true;
      this.chkShowBeats.CheckedChanged += new System.EventHandler(this.chkShowBeats_CheckedChanged);
      // 
      // cmdPausePlay
      // 
      this.cmdPausePlay.Enabled = false;
      this.cmdPausePlay.Location = new System.Drawing.Point(300, 0);
      this.cmdPausePlay.Name = "cmdPausePlay";
      this.cmdPausePlay.Size = new System.Drawing.Size(50, 48);
      this.cmdPausePlay.TabIndex = 44;
      this.cmdPausePlay.TabStop = false;
      this.cmdPausePlay.Text = "Pause Play";
      this.cmdPausePlay.UseVisualStyleBackColor = true;
      this.cmdPausePlay.Click += new System.EventHandler(this.cmdPausePlay_Click);
      // 
      // lblnudSyncopation
      // 
      this.lblnudSyncopation.AutoSize = true;
      this.lblnudSyncopation.Location = new System.Drawing.Point(0, 124);
      this.lblnudSyncopation.Name = "lblnudSyncopation";
      this.lblnudSyncopation.Size = new System.Drawing.Size(66, 13);
      this.lblnudSyncopation.TabIndex = 146;
      this.lblnudSyncopation.Text = "Syncopation";
      // 
      // lblnudSyncopationNote
      // 
      this.lblnudSyncopationNote.AutoSize = true;
      this.lblnudSyncopationNote.Location = new System.Drawing.Point(170, 123);
      this.lblnudSyncopationNote.Name = "lblnudSyncopationNote";
      this.lblnudSyncopationNote.Size = new System.Drawing.Size(28, 13);
      this.lblnudSyncopationNote.TabIndex = 145;
      this.lblnudSyncopationNote.Text = "note";
      // 
      // lblnudSyncopationSlash
      // 
      this.lblnudSyncopationSlash.AutoSize = true;
      this.lblnudSyncopationSlash.Location = new System.Drawing.Point(111, 122);
      this.lblnudSyncopationSlash.Name = "lblnudSyncopationSlash";
      this.lblnudSyncopationSlash.Size = new System.Drawing.Size(12, 13);
      this.lblnudSyncopationSlash.TabIndex = 144;
      this.lblnudSyncopationSlash.Text = "/";
      // 
      // nudSyncopationNN
      // 
      this.nudSyncopationNN.BackColor = System.Drawing.SystemColors.Window;
      this.nudSyncopationNN.Location = new System.Drawing.Point(69, 121);
      this.nudSyncopationNN.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
      this.nudSyncopationNN.Name = "nudSyncopationNN";
      this.nudSyncopationNN.ReadOnly = true;
      this.nudSyncopationNN.Size = new System.Drawing.Size(40, 20);
      this.nudSyncopationNN.TabIndex = 143;
      this.nudSyncopationNN.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudSyncopationNN.ValueChanged += new System.EventHandler(this.nudSyncopationNN_ValueChanged);
      // 
      // nudSyncopationDD
      // 
      this.nudSyncopationDD.BackColor = System.Drawing.SystemColors.Window;
      this.nudSyncopationDD.Location = new System.Drawing.Point(124, 121);
      this.nudSyncopationDD.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
      this.nudSyncopationDD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudSyncopationDD.Name = "nudSyncopationDD";
      this.nudSyncopationDD.ReadOnly = true;
      this.nudSyncopationDD.Size = new System.Drawing.Size(40, 20);
      this.nudSyncopationDD.TabIndex = 142;
      this.nudSyncopationDD.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
      this.nudSyncopationDD.ValueChanged += new System.EventHandler(this.nudSyncopationDD_ValueChanged);
      // 
      // lbltrkKBChanVol
      // 
      this.lbltrkKBChanVol.AutoSize = true;
      this.lbltrkKBChanVol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbltrkKBChanVol.Location = new System.Drawing.Point(15, 83);
      this.lbltrkKBChanVol.Name = "lbltrkKBChanVol";
      this.lbltrkKBChanVol.Size = new System.Drawing.Size(49, 13);
      this.lbltrkKBChanVol.TabIndex = 134;
      this.lbltrkKBChanVol.Text = "KBChVol";
      // 
      // lblnudOctaves
      // 
      this.lblnudOctaves.AutoSize = true;
      this.lblnudOctaves.Location = new System.Drawing.Point(166, 85);
      this.lblnudOctaves.Name = "lblnudOctaves";
      this.lblnudOctaves.Size = new System.Drawing.Size(47, 13);
      this.lblnudOctaves.TabIndex = 132;
      this.lblnudOctaves.Text = "Octaves";
      // 
      // nudOctaves
      // 
      this.nudOctaves.Location = new System.Drawing.Point(218, 83);
      this.nudOctaves.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
      this.nudOctaves.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudOctaves.Name = "nudOctaves";
      this.nudOctaves.Size = new System.Drawing.Size(40, 20);
      this.nudOctaves.TabIndex = 131;
      this.nudOctaves.TabStop = false;
      this.nudOctaves.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
      this.nudOctaves.ValueChanged += new System.EventHandler(this.nudOctaves_ValueChanged);
      // 
      // lblnudStartBar
      // 
      this.lblnudStartBar.AutoSize = true;
      this.lblnudStartBar.Location = new System.Drawing.Point(16, 10);
      this.lblnudStartBar.Name = "lblnudStartBar";
      this.lblnudStartBar.Size = new System.Drawing.Size(48, 13);
      this.lblnudStartBar.TabIndex = 130;
      this.lblnudStartBar.Text = "Start Bar";
      // 
      // nudStartBar
      // 
      this.nudStartBar.Location = new System.Drawing.Point(66, 8);
      this.nudStartBar.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.nudStartBar.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudStartBar.Name = "nudStartBar";
      this.nudStartBar.Size = new System.Drawing.Size(40, 20);
      this.nudStartBar.TabIndex = 127;
      this.nudStartBar.TabStop = false;
      this.nudStartBar.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudStartBar.ValueChanged += new System.EventHandler(this.nudStartBar_ValueChanged);
      // 
      // lblnudBeatHeight
      // 
      this.lblnudBeatHeight.AutoSize = true;
      this.lblnudBeatHeight.Location = new System.Drawing.Point(1, 34);
      this.lblnudBeatHeight.Name = "lblnudBeatHeight";
      this.lblnudBeatHeight.Size = new System.Drawing.Size(63, 13);
      this.lblnudBeatHeight.TabIndex = 129;
      this.lblnudBeatHeight.Text = "Beat Height";
      // 
      // nudBeatHeight
      // 
      this.nudBeatHeight.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
      this.nudBeatHeight.Location = new System.Drawing.Point(66, 32);
      this.nudBeatHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudBeatHeight.Name = "nudBeatHeight";
      this.nudBeatHeight.Size = new System.Drawing.Size(40, 20);
      this.nudBeatHeight.TabIndex = 128;
      this.nudBeatHeight.TabStop = false;
      this.nudBeatHeight.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
      this.nudBeatHeight.ValueChanged += new System.EventHandler(this.nudBeatHeight_ValueChanged);
      // 
      // lbltrkKBChanPan
      // 
      this.lbltrkKBChanPan.AutoSize = true;
      this.lbltrkKBChanPan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbltrkKBChanPan.Location = new System.Drawing.Point(11, 103);
      this.lbltrkKBChanPan.Name = "lbltrkKBChanPan";
      this.lbltrkKBChanPan.Size = new System.Drawing.Size(53, 13);
      this.lbltrkKBChanPan.TabIndex = 135;
      this.lbltrkKBChanPan.Text = "KBChPan";
      // 
      // panControls
      // 
      this.panControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panControls.Controls.Add(this.trkPCKBVel);
      this.panControls.Controls.Add(this.chkManSyncChord);
      this.panControls.Controls.Add(this.cmdResetPlay);
      this.panControls.Controls.Add(this.chkShowChordsRel);
      this.panControls.Controls.Add(this.grpSustainAction);
      this.panControls.Controls.Add(this.cmdTonnetz);
      this.panControls.Controls.Add(this.lblRangeTrk);
      this.panControls.Controls.Add(this.chkSwitchKBChord);
      this.panControls.Controls.Add(this.panDisplay);
      this.panControls.Controls.Add(this.lbllblRangeTrk);
      this.panControls.Controls.Add(this.lblKeyVel);
      this.panControls.Controls.Add(this.chkSwitchSustain);
      this.panControls.Controls.Add(this.panForms);
      this.panControls.Controls.Add(this.lblKeyThis);
      this.panControls.Controls.Add(this.panPlay);
      this.panControls.Controls.Add(this.panFiles);
      this.panControls.Controls.Add(this.lblKeyNext);
      this.panControls.Controls.Add(this.lblnudOctaves);
      this.panControls.Controls.Add(this.nudOctaves);
      this.panControls.Controls.Add(this.lblRangeVis);
      this.panControls.Controls.Add(this.lbllblRangeVis);
      this.panControls.Controls.Add(this.panTrkStream);
      this.panControls.Controls.Add(this.panTrkKB);
      this.panControls.Controls.Add(this.panTrkAudio);
      this.panControls.Controls.Add(this.lbllblKeyNext);
      this.panControls.Controls.Add(this.cmdHelp);
      this.panControls.Controls.Add(this.lbllblKeyThis);
      this.panControls.Controls.Add(this.lblcmbFirstNote);
      this.panControls.Controls.Add(this.grpNoteDisplay);
      this.panControls.Controls.Add(this.cmbFirstNote);
      this.panControls.Controls.Add(this.chkShowChordNotes);
      this.panControls.Controls.Add(this.cmdAlign);
      this.panControls.Controls.Add(this.grpOctTrans);
      this.panControls.Controls.Add(this.grpCapitalizeRoots);
      this.panControls.Controls.Add(this.grpMisc);
      this.panControls.Controls.Add(this.grpPlayMode);
      this.panControls.Controls.Add(this.grpSemitoneTrans);
      this.panControls.Controls.Add(this.lblKBDisplacement);
      this.panControls.Controls.Add(this.nudKBDisplacement);
      this.panControls.Controls.Add(this.cmdTempoReset);
      this.panControls.Controls.Add(this.lblTempo);
      this.panControls.Controls.Add(this.lbltrkTempo);
      this.panControls.Controls.Add(this.trkTempo);
      this.panControls.Controls.Add(this.chkAlignKB);
      this.panControls.Controls.Add(this.lblnudSyncopation);
      this.panControls.Controls.Add(this.lblnudSyncopationNote);
      this.panControls.Controls.Add(this.chkShowBeats);
      this.panControls.Controls.Add(this.lblnudSyncopationSlash);
      this.panControls.Controls.Add(this.nudSyncopationNN);
      this.panControls.Controls.Add(this.chkShowChords);
      this.panControls.Controls.Add(this.nudSyncopationDD);
      this.panControls.Controls.Add(this.chkShowChordNames);
      this.panControls.Controls.Add(this.chkShowTracks);
      this.panControls.Controls.Add(this.grpKBChan);
      this.panControls.Location = new System.Drawing.Point(4, 283);
      this.panControls.Name = "panControls";
      this.panControls.Size = new System.Drawing.Size(1355, 204);
      this.panControls.TabIndex = 0;
      // 
      // trkPCKBVel
      // 
      this.trkPCKBVel.AutoSize = false;
      this.trkPCKBVel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      this.trkPCKBVel.LargeChange = 16;
      this.trkPCKBVel.Location = new System.Drawing.Point(151, 61);
      this.trkPCKBVel.Maximum = 127;
      this.trkPCKBVel.Minimum = 1;
      this.trkPCKBVel.Name = "trkPCKBVel";
      this.trkPCKBVel.Size = new System.Drawing.Size(109, 16);
      this.trkPCKBVel.SmallChange = 4;
      this.trkPCKBVel.TabIndex = 246;
      this.trkPCKBVel.TabStop = false;
      this.trkPCKBVel.TickFrequency = 16;
      this.trkPCKBVel.Value = 80;
      this.trkPCKBVel.Scroll += new System.EventHandler(this.trkPCKBVel_Scroll);
      // 
      // chkManSyncChord
      // 
      this.chkManSyncChord.AutoSize = true;
      this.chkManSyncChord.Enabled = false;
      this.chkManSyncChord.Location = new System.Drawing.Point(3, 97);
      this.chkManSyncChord.Name = "chkManSyncChord";
      this.chkManSyncChord.Size = new System.Drawing.Size(156, 17);
      this.chkManSyncChord.TabIndex = 253;
      this.chkManSyncChord.Text = "ManSync Chord Positioning";
      this.chkManSyncChord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.chkManSyncChord.UseVisualStyleBackColor = true;
      this.chkManSyncChord.CheckedChanged += new System.EventHandler(this.chkManSyncChord_CheckedChanged);
      // 
      // cmdResetPlay
      // 
      this.cmdResetPlay.ForeColor = System.Drawing.Color.Red;
      this.cmdResetPlay.Location = new System.Drawing.Point(1346, 56);
      this.cmdResetPlay.Name = "cmdResetPlay";
      this.cmdResetPlay.Size = new System.Drawing.Size(55, 39);
      this.cmdResetPlay.TabIndex = 252;
      this.cmdResetPlay.Text = "Reset Play";
      this.cmdResetPlay.UseVisualStyleBackColor = true;
      this.cmdResetPlay.Click += new System.EventHandler(this.cmdResetPlay_Click);
      // 
      // chkShowChordsRel
      // 
      this.chkShowChordsRel.AutoSize = true;
      this.chkShowChordsRel.Location = new System.Drawing.Point(400, 176);
      this.chkShowChordsRel.Name = "chkShowChordsRel";
      this.chkShowChordsRel.Size = new System.Drawing.Size(131, 17);
      this.chkShowChordsRel.TabIndex = 251;
      this.chkShowChordsRel.Text = "Show Relative Chords";
      this.chkShowChordsRel.UseVisualStyleBackColor = true;
      this.chkShowChordsRel.CheckedChanged += new System.EventHandler(this.chkShowChordsRel_CheckedChanged);
      // 
      // grpSustainAction
      // 
      this.grpSustainAction.Controls.Add(this.optSustainNormal);
      this.grpSustainAction.Controls.Add(this.optSustainSendCtlr);
      this.grpSustainAction.Controls.Add(this.optSustainReplay);
      this.grpSustainAction.Location = new System.Drawing.Point(271, 58);
      this.grpSustainAction.Name = "grpSustainAction";
      this.grpSustainAction.Size = new System.Drawing.Size(119, 67);
      this.grpSustainAction.TabIndex = 250;
      this.grpSustainAction.TabStop = false;
      this.grpSustainAction.Text = "Sustain Action";
      // 
      // optSustainNormal
      // 
      this.optSustainNormal.AutoSize = true;
      this.optSustainNormal.Location = new System.Drawing.Point(6, 46);
      this.optSustainNormal.Name = "optSustainNormal";
      this.optSustainNormal.Size = new System.Drawing.Size(58, 17);
      this.optSustainNormal.TabIndex = 4;
      this.optSustainNormal.Text = "Normal";
      this.optSustainNormal.UseVisualStyleBackColor = true;
      this.optSustainNormal.CheckedChanged += new System.EventHandler(this.optSustain_CheckedChanged);
      // 
      // optSustainSendCtlr
      // 
      this.optSustainSendCtlr.AutoSize = true;
      this.optSustainSendCtlr.Location = new System.Drawing.Point(6, 30);
      this.optSustainSendCtlr.Name = "optSustainSendCtlr";
      this.optSustainSendCtlr.Size = new System.Drawing.Size(96, 17);
      this.optSustainSendCtlr.TabIndex = 3;
      this.optSustainSendCtlr.Text = "Sustain On/Off";
      this.optSustainSendCtlr.UseVisualStyleBackColor = true;
      this.optSustainSendCtlr.CheckedChanged += new System.EventHandler(this.optSustain_CheckedChanged);
      // 
      // optSustainReplay
      // 
      this.optSustainReplay.AutoSize = true;
      this.optSustainReplay.Checked = true;
      this.optSustainReplay.Location = new System.Drawing.Point(6, 14);
      this.optSustainReplay.Name = "optSustainReplay";
      this.optSustainReplay.Size = new System.Drawing.Size(84, 17);
      this.optSustainReplay.TabIndex = 2;
      this.optSustainReplay.TabStop = true;
      this.optSustainReplay.Text = "Note On/Off";
      this.optSustainReplay.UseVisualStyleBackColor = true;
      this.optSustainReplay.CheckedChanged += new System.EventHandler(this.optSustain_CheckedChanged);
      // 
      // cmdTonnetz
      // 
      this.cmdTonnetz.ForeColor = System.Drawing.Color.Red;
      this.cmdTonnetz.Location = new System.Drawing.Point(1346, 123);
      this.cmdTonnetz.Name = "cmdTonnetz";
      this.cmdTonnetz.Size = new System.Drawing.Size(54, 28);
      this.cmdTonnetz.TabIndex = 241;
      this.cmdTonnetz.Text = "Tonnetz";
      this.cmdTonnetz.UseVisualStyleBackColor = true;
      this.cmdTonnetz.Click += new System.EventHandler(this.cmdTonnetz_Click);
      // 
      // lblRangeTrk
      // 
      this.lblRangeTrk.AutoSize = true;
      this.lblRangeTrk.Location = new System.Drawing.Point(1251, 41);
      this.lblRangeTrk.Name = "lblRangeTrk";
      this.lblRangeTrk.Size = new System.Drawing.Size(25, 13);
      this.lblRangeTrk.TabIndex = 249;
      this.lblRangeTrk.Text = "???";
      // 
      // chkSwitchKBChord
      // 
      this.chkSwitchKBChord.AutoSize = true;
      this.chkSwitchKBChord.Location = new System.Drawing.Point(3, 62);
      this.chkSwitchKBChord.Name = "chkSwitchKBChord";
      this.chkSwitchKBChord.Size = new System.Drawing.Size(68, 17);
      this.chkSwitchKBChord.TabIndex = 203;
      this.chkSwitchKBChord.Text = "KBChord";
      this.chkSwitchKBChord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.chkSwitchKBChord.UseVisualStyleBackColor = true;
      this.chkSwitchKBChord.CheckedChanged += new System.EventHandler(this.chkSwitchKBChord_CheckedChanged);
      // 
      // panDisplay
      // 
      this.panDisplay.Controls.Add(this.cmdNoteFont);
      this.panDisplay.Controls.Add(this.cmdChordFont);
      this.panDisplay.Controls.Add(this.cmdColours);
      this.panDisplay.Location = new System.Drawing.Point(891, 2);
      this.panDisplay.Name = "panDisplay";
      this.panDisplay.Size = new System.Drawing.Size(154, 48);
      this.panDisplay.TabIndex = 226;
      // 
      // cmdNoteFont
      // 
      this.cmdNoteFont.Location = new System.Drawing.Point(0, 0);
      this.cmdNoteFont.Name = "cmdNoteFont";
      this.cmdNoteFont.Size = new System.Drawing.Size(50, 48);
      this.cmdNoteFont.TabIndex = 230;
      this.cmdNoteFont.Text = "Note\r\nFont";
      this.cmdNoteFont.UseVisualStyleBackColor = true;
      this.cmdNoteFont.Click += new System.EventHandler(this.cmdNoteFont_Click);
      // 
      // cmdChordFont
      // 
      this.cmdChordFont.Location = new System.Drawing.Point(50, 0);
      this.cmdChordFont.Name = "cmdChordFont";
      this.cmdChordFont.Size = new System.Drawing.Size(50, 48);
      this.cmdChordFont.TabIndex = 231;
      this.cmdChordFont.Text = "Chord\r\nFont";
      this.cmdChordFont.UseVisualStyleBackColor = true;
      this.cmdChordFont.Click += new System.EventHandler(this.cmdChordFont_Click);
      // 
      // cmdColours
      // 
      this.cmdColours.Location = new System.Drawing.Point(99, 0);
      this.cmdColours.Name = "cmdColours";
      this.cmdColours.Size = new System.Drawing.Size(50, 48);
      this.cmdColours.TabIndex = 232;
      this.cmdColours.Text = "Colours";
      this.cmdColours.UseVisualStyleBackColor = true;
      this.cmdColours.Click += new System.EventHandler(this.cmdColors_Click);
      // 
      // lbllblRangeTrk
      // 
      this.lbllblRangeTrk.AutoSize = true;
      this.lbllblRangeTrk.Location = new System.Drawing.Point(1177, 41);
      this.lbllblRangeTrk.Name = "lbllblRangeTrk";
      this.lbllblRangeTrk.Size = new System.Drawing.Size(70, 13);
      this.lbllblRangeTrk.TabIndex = 248;
      this.lbllblRangeTrk.Text = "Track Range";
      // 
      // lblKeyVel
      // 
      this.lblKeyVel.AutoSize = true;
      this.lblKeyVel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lblKeyVel.Location = new System.Drawing.Point(72, 63);
      this.lblKeyVel.Name = "lblKeyVel";
      this.lblKeyVel.Size = new System.Drawing.Size(75, 13);
      this.lblKeyVel.TabIndex = 243;
      this.lblKeyVel.Text = "PCKB Velocity";
      // 
      // chkSwitchSustain
      // 
      this.chkSwitchSustain.AutoSize = true;
      this.chkSwitchSustain.Location = new System.Drawing.Point(3, 79);
      this.chkSwitchSustain.Name = "chkSwitchSustain";
      this.chkSwitchSustain.Size = new System.Drawing.Size(61, 17);
      this.chkSwitchSustain.TabIndex = 202;
      this.chkSwitchSustain.Text = "Sustain";
      this.chkSwitchSustain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.chkSwitchSustain.UseVisualStyleBackColor = true;
      this.chkSwitchSustain.CheckedChanged += new System.EventHandler(this.chkSwitchSustain_CheckedChanged);
      // 
      // panForms
      // 
      this.panForms.Controls.Add(this.cmdChordMap);
      this.panForms.Controls.Add(this.cmdMultiMap);
      this.panForms.Controls.Add(this.cmdUpdateLyrics);
      this.panForms.Controls.Add(this.cmdShowSumm);
      this.panForms.Controls.Add(this.cmdShowAudioSyncWindow);
      this.panForms.Location = new System.Drawing.Point(629, 2);
      this.panForms.Name = "panForms";
      this.panForms.Size = new System.Drawing.Size(256, 48);
      this.panForms.TabIndex = 226;
      // 
      // cmdChordMap
      // 
      this.cmdChordMap.Location = new System.Drawing.Point(0, 0);
      this.cmdChordMap.Name = "cmdChordMap";
      this.cmdChordMap.Size = new System.Drawing.Size(50, 48);
      this.cmdChordMap.TabIndex = 222;
      this.cmdChordMap.Text = "Chord\r\nMap";
      this.cmdChordMap.UseVisualStyleBackColor = true;
      this.cmdChordMap.Click += new System.EventHandler(this.cmdChordMap_Click);
      // 
      // cmdMultiMap
      // 
      this.cmdMultiMap.Enabled = false;
      this.cmdMultiMap.Location = new System.Drawing.Point(50, 0);
      this.cmdMultiMap.Name = "cmdMultiMap";
      this.cmdMultiMap.Size = new System.Drawing.Size(50, 48);
      this.cmdMultiMap.TabIndex = 190;
      this.cmdMultiMap.Text = "Track Map";
      this.cmdMultiMap.UseVisualStyleBackColor = true;
      this.cmdMultiMap.Click += new System.EventHandler(this.cmdMultiMap_Click);
      // 
      // cmdUpdateLyrics
      // 
      this.cmdUpdateLyrics.Location = new System.Drawing.Point(200, 0);
      this.cmdUpdateLyrics.Margin = new System.Windows.Forms.Padding(0);
      this.cmdUpdateLyrics.Name = "cmdUpdateLyrics";
      this.cmdUpdateLyrics.Size = new System.Drawing.Size(50, 48);
      this.cmdUpdateLyrics.TabIndex = 249;
      this.cmdUpdateLyrics.Text = "Edit\r\nLyrics";
      this.cmdUpdateLyrics.UseVisualStyleBackColor = true;
      this.cmdUpdateLyrics.Click += new System.EventHandler(this.cmdUpdateLyrics_Click);
      // 
      // cmdShowSumm
      // 
      this.cmdShowSumm.Location = new System.Drawing.Point(150, 0);
      this.cmdShowSumm.Name = "cmdShowSumm";
      this.cmdShowSumm.Size = new System.Drawing.Size(50, 48);
      this.cmdShowSumm.TabIndex = 221;
      this.cmdShowSumm.Text = "Show\r\nSumm";
      this.cmdShowSumm.UseVisualStyleBackColor = true;
      this.cmdShowSumm.Click += new System.EventHandler(this.cmdShowSumm_Click);
      // 
      // cmdShowAudioSyncWindow
      // 
      this.cmdShowAudioSyncWindow.Location = new System.Drawing.Point(100, 0);
      this.cmdShowAudioSyncWindow.Margin = new System.Windows.Forms.Padding(0);
      this.cmdShowAudioSyncWindow.Name = "cmdShowAudioSyncWindow";
      this.cmdShowAudioSyncWindow.Size = new System.Drawing.Size(50, 48);
      this.cmdShowAudioSyncWindow.TabIndex = 248;
      this.cmdShowAudioSyncWindow.Text = "Audio\r\nSync\r\nConfig";
      this.cmdShowAudioSyncWindow.UseVisualStyleBackColor = true;
      this.cmdShowAudioSyncWindow.Click += new System.EventHandler(this.cmdShowAudioSyncWindow_Click);
      // 
      // lblKeyThis
      // 
      this.lblKeyThis.Location = new System.Drawing.Point(1250, 2);
      this.lblKeyThis.Name = "lblKeyThis";
      this.lblKeyThis.Size = new System.Drawing.Size(52, 15);
      this.lblKeyThis.TabIndex = 247;
      this.lblKeyThis.Text = "???";
      // 
      // panPlay
      // 
      this.panPlay.Controls.Add(this.cmdGoToStart);
      this.panPlay.Controls.Add(this.cmdPlayMidi);
      this.panPlay.Controls.Add(this.cmdStopPlay);
      this.panPlay.Controls.Add(this.cmdPausePlay);
      this.panPlay.Controls.Add(this.cmdPlayAndRecordAudio);
      this.panPlay.Controls.Add(this.cmdPanic);
      this.panPlay.Controls.Add(this.cmdSyncAudio);
      this.panPlay.Controls.Add(this.cmdPlayAudio);
      this.panPlay.Location = new System.Drawing.Point(215, 2);
      this.panPlay.Name = "panPlay";
      this.panPlay.Size = new System.Drawing.Size(408, 48);
      this.panPlay.TabIndex = 226;
      // 
      // cmdPlayAndRecordAudio
      // 
      this.cmdPlayAndRecordAudio.Location = new System.Drawing.Point(200, 0);
      this.cmdPlayAndRecordAudio.Name = "cmdPlayAndRecordAudio";
      this.cmdPlayAndRecordAudio.Size = new System.Drawing.Size(50, 48);
      this.cmdPlayAndRecordAudio.TabIndex = 246;
      this.cmdPlayAndRecordAudio.Text = "Play &&\r\nSync\r\nAudio";
      this.cmdPlayAndRecordAudio.UseVisualStyleBackColor = true;
      this.cmdPlayAndRecordAudio.Click += new System.EventHandler(this.cmdPlayAndRecord_Click);
      // 
      // cmdPanic
      // 
      this.cmdPanic.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdPanic.Location = new System.Drawing.Point(350, 0);
      this.cmdPanic.Name = "cmdPanic";
      this.cmdPanic.Size = new System.Drawing.Size(50, 48);
      this.cmdPanic.TabIndex = 191;
      this.cmdPanic.Text = "!";
      this.cmdPanic.UseVisualStyleBackColor = true;
      this.cmdPanic.Click += new System.EventHandler(this.cmdPanic_Click);
      // 
      // cmdSyncAudio
      // 
      this.cmdSyncAudio.Enabled = false;
      this.cmdSyncAudio.Location = new System.Drawing.Point(150, 0);
      this.cmdSyncAudio.Name = "cmdSyncAudio";
      this.cmdSyncAudio.Size = new System.Drawing.Size(50, 48);
      this.cmdSyncAudio.TabIndex = 245;
      this.cmdSyncAudio.Text = "Sync\r\nAudio";
      this.cmdSyncAudio.UseVisualStyleBackColor = true;
      this.cmdSyncAudio.Click += new System.EventHandler(this.cmdSyncAudio_Click);
      // 
      // cmdPlayAudio
      // 
      this.cmdPlayAudio.Enabled = false;
      this.cmdPlayAudio.Location = new System.Drawing.Point(100, 0);
      this.cmdPlayAudio.Name = "cmdPlayAudio";
      this.cmdPlayAudio.Size = new System.Drawing.Size(50, 48);
      this.cmdPlayAudio.TabIndex = 224;
      this.cmdPlayAudio.TabStop = false;
      this.cmdPlayAudio.Text = "Play\r\nAudio";
      this.cmdPlayAudio.UseVisualStyleBackColor = true;
      this.cmdPlayAudio.Click += new System.EventHandler(this.cmdPlayAudio_Click);
      // 
      // panFiles
      // 
      this.panFiles.Controls.Add(this.cmdNew);
      this.panFiles.Controls.Add(this.cmdLoadProject);
      this.panFiles.Controls.Add(this.cmdSaveProject);
      this.panFiles.Controls.Add(this.cmdSaveProjectAs);
      this.panFiles.Location = new System.Drawing.Point(1, 2);
      this.panFiles.Name = "panFiles";
      this.panFiles.Size = new System.Drawing.Size(208, 48);
      this.panFiles.TabIndex = 226;
      // 
      // cmdNew
      // 
      this.cmdNew.Location = new System.Drawing.Point(0, 0);
      this.cmdNew.Name = "cmdNew";
      this.cmdNew.Size = new System.Drawing.Size(50, 48);
      this.cmdNew.TabIndex = 237;
      this.cmdNew.Text = "New Project";
      this.cmdNew.UseVisualStyleBackColor = true;
      this.cmdNew.Click += new System.EventHandler(this.mnuNew_Click);
      // 
      // cmdLoadProject
      // 
      this.cmdLoadProject.Location = new System.Drawing.Point(50, 0);
      this.cmdLoadProject.Name = "cmdLoadProject";
      this.cmdLoadProject.Size = new System.Drawing.Size(50, 48);
      this.cmdLoadProject.TabIndex = 188;
      this.cmdLoadProject.Text = "Load Project";
      this.cmdLoadProject.UseVisualStyleBackColor = true;
      this.cmdLoadProject.Click += new System.EventHandler(this.cmdLoadProject_Click);
      // 
      // cmdSaveProject
      // 
      this.cmdSaveProject.Enabled = false;
      this.cmdSaveProject.Location = new System.Drawing.Point(100, 0);
      this.cmdSaveProject.Name = "cmdSaveProject";
      this.cmdSaveProject.Size = new System.Drawing.Size(50, 48);
      this.cmdSaveProject.TabIndex = 240;
      this.cmdSaveProject.Text = "Save Project";
      this.cmdSaveProject.UseVisualStyleBackColor = true;
      this.cmdSaveProject.Click += new System.EventHandler(this.mnuSaveProject_Click);
      this.cmdSaveProject.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cmdSaveProject_MouseUp);
      // 
      // cmdSaveProjectAs
      // 
      this.cmdSaveProjectAs.Enabled = false;
      this.cmdSaveProjectAs.Location = new System.Drawing.Point(150, 0);
      this.cmdSaveProjectAs.Name = "cmdSaveProjectAs";
      this.cmdSaveProjectAs.Size = new System.Drawing.Size(50, 48);
      this.cmdSaveProjectAs.TabIndex = 244;
      this.cmdSaveProjectAs.Text = "Save\r\nProject\r\nAs...";
      this.cmdSaveProjectAs.UseVisualStyleBackColor = true;
      this.cmdSaveProjectAs.Click += new System.EventHandler(this.mnuSaveProjectAs_Click);
      // 
      // lblKeyNext
      // 
      this.lblKeyNext.Location = new System.Drawing.Point(1250, 15);
      this.lblKeyNext.Name = "lblKeyNext";
      this.lblKeyNext.Size = new System.Drawing.Size(52, 15);
      this.lblKeyNext.TabIndex = 246;
      this.lblKeyNext.Text = "???";
      // 
      // lblRangeVis
      // 
      this.lblRangeVis.AutoSize = true;
      this.lblRangeVis.Location = new System.Drawing.Point(1251, 28);
      this.lblRangeVis.Name = "lblRangeVis";
      this.lblRangeVis.Size = new System.Drawing.Size(25, 13);
      this.lblRangeVis.TabIndex = 245;
      this.lblRangeVis.Text = "???";
      // 
      // lbllblRangeVis
      // 
      this.lbllblRangeVis.AutoSize = true;
      this.lbllblRangeVis.Location = new System.Drawing.Point(1177, 28);
      this.lbllblRangeVis.Name = "lbllblRangeVis";
      this.lbllblRangeVis.Size = new System.Drawing.Size(72, 13);
      this.lbllblRangeVis.TabIndex = 244;
      this.lbllblRangeVis.Text = "Visible Range";
      // 
      // panTrkStream
      // 
      this.panTrkStream.Controls.Add(this.lbltrkStreamVol);
      this.panTrkStream.Controls.Add(this.trkStreamVol);
      this.panTrkStream.Controls.Add(this.trkStreamPan);
      this.panTrkStream.Controls.Add(this.lbltrkStreamPan);
      this.panTrkStream.Enabled = false;
      this.panTrkStream.Location = new System.Drawing.Point(1143, 155);
      this.panTrkStream.Name = "panTrkStream";
      this.panTrkStream.Size = new System.Drawing.Size(197, 46);
      this.panTrkStream.TabIndex = 227;
      // 
      // lbltrkStreamVol
      // 
      this.lbltrkStreamVol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbltrkStreamVol.Location = new System.Drawing.Point(3, 8);
      this.lbltrkStreamVol.Name = "lbltrkStreamVol";
      this.lbltrkStreamVol.Size = new System.Drawing.Size(60, 13);
      this.lbltrkStreamVol.TabIndex = 255;
      this.lbltrkStreamVol.Text = "StreamVol";
      this.lbltrkStreamVol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // trkStreamVol
      // 
      this.trkStreamVol.AutoSize = false;
      this.trkStreamVol.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      this.trkStreamVol.LargeChange = 8;
      this.trkStreamVol.Location = new System.Drawing.Point(68, 5);
      this.trkStreamVol.Maximum = 100;
      this.trkStreamVol.Name = "trkStreamVol";
      this.trkStreamVol.Size = new System.Drawing.Size(126, 16);
      this.trkStreamVol.TabIndex = 253;
      this.trkStreamVol.TabStop = false;
      this.trkStreamVol.TickFrequency = 10;
      this.trkStreamVol.Value = 50;
      this.trkStreamVol.Scroll += new System.EventHandler(this.trkStreamVol_Scroll);
      // 
      // trkStreamPan
      // 
      this.trkStreamPan.AutoSize = false;
      this.trkStreamPan.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      this.trkStreamPan.LargeChange = 8;
      this.trkStreamPan.Location = new System.Drawing.Point(68, 25);
      this.trkStreamPan.Maximum = 50;
      this.trkStreamPan.Minimum = -50;
      this.trkStreamPan.Name = "trkStreamPan";
      this.trkStreamPan.Size = new System.Drawing.Size(126, 16);
      this.trkStreamPan.TabIndex = 254;
      this.trkStreamPan.TabStop = false;
      this.trkStreamPan.TickFrequency = 50;
      this.trkStreamPan.Scroll += new System.EventHandler(this.trkStreamPan_Scroll);
      // 
      // lbltrkStreamPan
      // 
      this.lbltrkStreamPan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbltrkStreamPan.Location = new System.Drawing.Point(3, 28);
      this.lbltrkStreamPan.Name = "lbltrkStreamPan";
      this.lbltrkStreamPan.Size = new System.Drawing.Size(60, 13);
      this.lbltrkStreamPan.TabIndex = 256;
      this.lbltrkStreamPan.Text = "StreamPan";
      this.lbltrkStreamPan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // panTrkKB
      // 
      this.panTrkKB.Controls.Add(this.lbltrkKBVol);
      this.panTrkKB.Controls.Add(this.trkKBVol);
      this.panTrkKB.Controls.Add(this.trkKBPan);
      this.panTrkKB.Controls.Add(this.lbltrkKBPan);
      this.panTrkKB.Enabled = false;
      this.panTrkKB.Location = new System.Drawing.Point(1143, 106);
      this.panTrkKB.Name = "panTrkKB";
      this.panTrkKB.Size = new System.Drawing.Size(197, 46);
      this.panTrkKB.TabIndex = 227;
      // 
      // lbltrkKBVol
      // 
      this.lbltrkKBVol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbltrkKBVol.Location = new System.Drawing.Point(1, 8);
      this.lbltrkKBVol.Name = "lbltrkKBVol";
      this.lbltrkKBVol.Size = new System.Drawing.Size(60, 13);
      this.lbltrkKBVol.TabIndex = 251;
      this.lbltrkKBVol.Text = "KBVol";
      this.lbltrkKBVol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // trkKBVol
      // 
      this.trkKBVol.AutoSize = false;
      this.trkKBVol.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      this.trkKBVol.LargeChange = 8;
      this.trkKBVol.Location = new System.Drawing.Point(66, 5);
      this.trkKBVol.Maximum = 100;
      this.trkKBVol.Name = "trkKBVol";
      this.trkKBVol.Size = new System.Drawing.Size(126, 16);
      this.trkKBVol.TabIndex = 249;
      this.trkKBVol.TabStop = false;
      this.trkKBVol.TickFrequency = 10;
      this.trkKBVol.Value = 80;
      this.trkKBVol.Scroll += new System.EventHandler(this.trkKBVol_Scroll);
      // 
      // trkKBPan
      // 
      this.trkKBPan.AutoSize = false;
      this.trkKBPan.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      this.trkKBPan.LargeChange = 8;
      this.trkKBPan.Location = new System.Drawing.Point(66, 25);
      this.trkKBPan.Maximum = 50;
      this.trkKBPan.Minimum = -50;
      this.trkKBPan.Name = "trkKBPan";
      this.trkKBPan.Size = new System.Drawing.Size(126, 16);
      this.trkKBPan.TabIndex = 250;
      this.trkKBPan.TabStop = false;
      this.trkKBPan.TickFrequency = 50;
      this.trkKBPan.Scroll += new System.EventHandler(this.trkKBPan_Scroll);
      // 
      // lbltrkKBPan
      // 
      this.lbltrkKBPan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbltrkKBPan.Location = new System.Drawing.Point(1, 28);
      this.lbltrkKBPan.Name = "lbltrkKBPan";
      this.lbltrkKBPan.Size = new System.Drawing.Size(60, 13);
      this.lbltrkKBPan.TabIndex = 252;
      this.lbltrkKBPan.Text = "KBPan";
      this.lbltrkKBPan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // panTrkAudio
      // 
      this.panTrkAudio.Controls.Add(this.lbltrkAudioVol);
      this.panTrkAudio.Controls.Add(this.trkAudioVol);
      this.panTrkAudio.Controls.Add(this.trkAudioPan);
      this.panTrkAudio.Controls.Add(this.lbltrkAudioPan);
      this.panTrkAudio.Enabled = false;
      this.panTrkAudio.Location = new System.Drawing.Point(1143, 57);
      this.panTrkAudio.Name = "panTrkAudio";
      this.panTrkAudio.Size = new System.Drawing.Size(197, 46);
      this.panTrkAudio.TabIndex = 226;
      // 
      // lbltrkAudioVol
      // 
      this.lbltrkAudioVol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbltrkAudioVol.Location = new System.Drawing.Point(3, 7);
      this.lbltrkAudioVol.Name = "lbltrkAudioVol";
      this.lbltrkAudioVol.Size = new System.Drawing.Size(60, 13);
      this.lbltrkAudioVol.TabIndex = 247;
      this.lbltrkAudioVol.Text = "AudioVol";
      this.lbltrkAudioVol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // trkAudioVol
      // 
      this.trkAudioVol.AutoSize = false;
      this.trkAudioVol.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      this.trkAudioVol.LargeChange = 8;
      this.trkAudioVol.Location = new System.Drawing.Point(68, 4);
      this.trkAudioVol.Maximum = 100;
      this.trkAudioVol.Name = "trkAudioVol";
      this.trkAudioVol.Size = new System.Drawing.Size(126, 16);
      this.trkAudioVol.TabIndex = 245;
      this.trkAudioVol.TabStop = false;
      this.trkAudioVol.TickFrequency = 10;
      this.trkAudioVol.Value = 50;
      this.trkAudioVol.Scroll += new System.EventHandler(this.trkAudioVol_Scroll);
      // 
      // trkAudioPan
      // 
      this.trkAudioPan.AutoSize = false;
      this.trkAudioPan.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      this.trkAudioPan.LargeChange = 8;
      this.trkAudioPan.Location = new System.Drawing.Point(68, 24);
      this.trkAudioPan.Maximum = 50;
      this.trkAudioPan.Minimum = -50;
      this.trkAudioPan.Name = "trkAudioPan";
      this.trkAudioPan.Size = new System.Drawing.Size(126, 16);
      this.trkAudioPan.TabIndex = 246;
      this.trkAudioPan.TabStop = false;
      this.trkAudioPan.TickFrequency = 50;
      this.trkAudioPan.Scroll += new System.EventHandler(this.trkAudioPan_Scroll);
      // 
      // lbltrkAudioPan
      // 
      this.lbltrkAudioPan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbltrkAudioPan.Location = new System.Drawing.Point(3, 27);
      this.lbltrkAudioPan.Name = "lbltrkAudioPan";
      this.lbltrkAudioPan.Size = new System.Drawing.Size(60, 13);
      this.lbltrkAudioPan.TabIndex = 248;
      this.lbltrkAudioPan.Text = "AudioPan";
      this.lbltrkAudioPan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lbllblKeyNext
      // 
      this.lbllblKeyNext.AutoSize = true;
      this.lbllblKeyNext.Location = new System.Drawing.Point(1177, 15);
      this.lbllblKeyNext.Name = "lbllblKeyNext";
      this.lbllblKeyNext.Size = new System.Drawing.Size(50, 13);
      this.lbllblKeyNext.TabIndex = 234;
      this.lbllblKeyNext.Text = "Next Key";
      // 
      // cmdHelp
      // 
      this.cmdHelp.Location = new System.Drawing.Point(1107, 2);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(50, 48);
      this.cmdHelp.TabIndex = 236;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // lbllblKeyThis
      // 
      this.lbllblKeyThis.AutoSize = true;
      this.lbllblKeyThis.Location = new System.Drawing.Point(1175, 2);
      this.lbllblKeyThis.Name = "lbllblKeyThis";
      this.lbllblKeyThis.Size = new System.Drawing.Size(48, 13);
      this.lbllblKeyThis.TabIndex = 235;
      this.lbllblKeyThis.Text = "This Key";
      // 
      // lblcmbFirstNote
      // 
      this.lblcmbFirstNote.AutoSize = true;
      this.lblcmbFirstNote.Enabled = false;
      this.lblcmbFirstNote.Location = new System.Drawing.Point(469, 119);
      this.lblcmbFirstNote.Name = "lblcmbFirstNote";
      this.lblcmbFirstNote.Size = new System.Drawing.Size(52, 13);
      this.lblcmbFirstNote.TabIndex = 40;
      this.lblcmbFirstNote.Text = "First Note";
      // 
      // grpNoteDisplay
      // 
      this.grpNoteDisplay.Controls.Add(this.chkShowPCKBChar);
      this.grpNoteDisplay.Controls.Add(this.chkRunChordNotes);
      this.grpNoteDisplay.Controls.Add(this.optShowNone);
      this.grpNoteDisplay.Controls.Add(this.optShowNoteName);
      this.grpNoteDisplay.Controls.Add(this.optShowSolfa);
      this.grpNoteDisplay.Location = new System.Drawing.Point(1017, 61);
      this.grpNoteDisplay.Name = "grpNoteDisplay";
      this.grpNoteDisplay.Size = new System.Drawing.Size(122, 110);
      this.grpNoteDisplay.TabIndex = 233;
      this.grpNoteDisplay.TabStop = false;
      this.grpNoteDisplay.Text = "Note Display";
      // 
      // chkShowPCKBChar
      // 
      this.chkShowPCKBChar.AutoSize = true;
      this.chkShowPCKBChar.Checked = true;
      this.chkShowPCKBChar.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkShowPCKBChar.Location = new System.Drawing.Point(19, 69);
      this.chkShowPCKBChar.Name = "chkShowPCKBChar";
      this.chkShowPCKBChar.Size = new System.Drawing.Size(96, 17);
      this.chkShowPCKBChar.TabIndex = 39;
      this.chkShowPCKBChar.Text = "Keyboard Char";
      this.chkShowPCKBChar.UseVisualStyleBackColor = true;
      this.chkShowPCKBChar.CheckedChanged += new System.EventHandler(this.optShowNote_CheckedChanged);
      // 
      // chkRunChordNotes
      // 
      this.chkRunChordNotes.AutoSize = true;
      this.chkRunChordNotes.Checked = true;
      this.chkRunChordNotes.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkRunChordNotes.Location = new System.Drawing.Point(19, 87);
      this.chkRunChordNotes.Name = "chkRunChordNotes";
      this.chkRunChordNotes.Size = new System.Drawing.Size(94, 17);
      this.chkRunChordNotes.TabIndex = 38;
      this.chkRunChordNotes.Text = "Across Chords";
      this.chkRunChordNotes.UseVisualStyleBackColor = true;
      this.chkRunChordNotes.CheckedChanged += new System.EventHandler(this.chkRunChordNotes_CheckedChanged);
      // 
      // optShowNone
      // 
      this.optShowNone.AutoSize = true;
      this.optShowNone.Location = new System.Drawing.Point(19, 49);
      this.optShowNone.Name = "optShowNone";
      this.optShowNone.Size = new System.Drawing.Size(51, 17);
      this.optShowNone.TabIndex = 5;
      this.optShowNone.Text = "None";
      this.optShowNone.UseVisualStyleBackColor = true;
      this.optShowNone.CheckedChanged += new System.EventHandler(this.optShowNote_CheckedChanged);
      // 
      // optShowNoteName
      // 
      this.optShowNoteName.AutoSize = true;
      this.optShowNoteName.Location = new System.Drawing.Point(19, 32);
      this.optShowNoteName.Name = "optShowNoteName";
      this.optShowNoteName.Size = new System.Drawing.Size(79, 17);
      this.optShowNoteName.TabIndex = 4;
      this.optShowNoteName.Text = "Note Name";
      this.optShowNoteName.UseVisualStyleBackColor = true;
      this.optShowNoteName.CheckedChanged += new System.EventHandler(this.optShowNote_CheckedChanged);
      // 
      // optShowSolfa
      // 
      this.optShowSolfa.AutoSize = true;
      this.optShowSolfa.Checked = true;
      this.optShowSolfa.Location = new System.Drawing.Point(19, 16);
      this.optShowSolfa.Name = "optShowSolfa";
      this.optShowSolfa.Size = new System.Drawing.Size(49, 17);
      this.optShowSolfa.TabIndex = 3;
      this.optShowSolfa.TabStop = true;
      this.optShowSolfa.Text = "Solfa";
      this.optShowSolfa.UseVisualStyleBackColor = true;
      this.optShowSolfa.CheckedChanged += new System.EventHandler(this.optShowNote_CheckedChanged);
      // 
      // cmbFirstNote
      // 
      this.cmbFirstNote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbFirstNote.Enabled = false;
      this.cmbFirstNote.FormattingEnabled = true;
      this.cmbFirstNote.Items.AddRange(new object[] {
            "Root",
            "C",
            "Db",
            "D",
            "Eb",
            "E",
            "F",
            "F#",
            "G",
            "Ab",
            "A",
            "Bb",
            "B"});
      this.cmbFirstNote.Location = new System.Drawing.Point(400, 115);
      this.cmbFirstNote.Name = "cmbFirstNote";
      this.cmbFirstNote.Size = new System.Drawing.Size(65, 21);
      this.cmbFirstNote.TabIndex = 39;
      this.cmbFirstNote.SelectedIndexChanged += new System.EventHandler(this.cmbChordNotes_SelectedIndexChanged);
      // 
      // chkShowChordNotes
      // 
      this.chkShowChordNotes.AutoSize = true;
      this.chkShowChordNotes.Location = new System.Drawing.Point(400, 97);
      this.chkShowChordNotes.Name = "chkShowChordNotes";
      this.chkShowChordNotes.Size = new System.Drawing.Size(115, 17);
      this.chkShowChordNotes.TabIndex = 229;
      this.chkShowChordNotes.Text = "Show Chord Notes";
      this.chkShowChordNotes.UseVisualStyleBackColor = true;
      this.chkShowChordNotes.CheckedChanged += new System.EventHandler(this.chkShowChordNotes_CheckedChanged);
      // 
      // cmdAlign
      // 
      this.cmdAlign.ForeColor = System.Drawing.Color.Red;
      this.cmdAlign.Location = new System.Drawing.Point(1325, 3);
      this.cmdAlign.Name = "cmdAlign";
      this.cmdAlign.Size = new System.Drawing.Size(50, 21);
      this.cmdAlign.TabIndex = 207;
      this.cmdAlign.Text = "Align\r\n";
      this.cmdAlign.UseVisualStyleBackColor = true;
      this.cmdAlign.Click += new System.EventHandler(this.cmdAlign_Click);
      // 
      // grpOctTrans
      // 
      this.grpOctTrans.Controls.Add(this.lblnudOctTransposeKB);
      this.grpOctTrans.Controls.Add(this.nudOctTransposeKB);
      this.grpOctTrans.Controls.Add(this.lblnudOctTransposeDisplay);
      this.grpOctTrans.Controls.Add(this.nudOctTransposeDisplay);
      this.grpOctTrans.Controls.Add(this.lblnudOctTransposeKBPitch);
      this.grpOctTrans.Controls.Add(this.nudOctTransposeKBPitch);
      this.grpOctTrans.Location = new System.Drawing.Point(737, 61);
      this.grpOctTrans.Name = "grpOctTrans";
      this.grpOctTrans.Size = new System.Drawing.Size(139, 90);
      this.grpOctTrans.TabIndex = 176;
      this.grpOctTrans.TabStop = false;
      this.grpOctTrans.Text = "Octave Transposition";
      // 
      // lblnudOctTransposeKB
      // 
      this.lblnudOctTransposeKB.AutoSize = true;
      this.lblnudOctTransposeKB.Location = new System.Drawing.Point(27, 44);
      this.lblnudOctTransposeKB.Name = "lblnudOctTransposeKB";
      this.lblnudOctTransposeKB.Size = new System.Drawing.Size(52, 13);
      this.lblnudOctTransposeKB.TabIndex = 117;
      this.lblnudOctTransposeKB.Text = "Keyboard";
      // 
      // nudOctTransposeKB
      // 
      this.nudOctTransposeKB.Increment = new decimal(new int[] {
            12,
            0,
            0,
            0});
      this.nudOctTransposeKB.Location = new System.Drawing.Point(83, 40);
      this.nudOctTransposeKB.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
      this.nudOctTransposeKB.Minimum = new decimal(new int[] {
            48,
            0,
            0,
            -2147483648});
      this.nudOctTransposeKB.Name = "nudOctTransposeKB";
      this.nudOctTransposeKB.Size = new System.Drawing.Size(40, 20);
      this.nudOctTransposeKB.TabIndex = 116;
      this.nudOctTransposeKB.ValueChanged += new System.EventHandler(this.nudOctTransposeKB_ValueChanged);
      // 
      // lblnudOctTransposeDisplay
      // 
      this.lblnudOctTransposeDisplay.AutoSize = true;
      this.lblnudOctTransposeDisplay.Location = new System.Drawing.Point(37, 64);
      this.lblnudOctTransposeDisplay.Name = "lblnudOctTransposeDisplay";
      this.lblnudOctTransposeDisplay.Size = new System.Drawing.Size(41, 13);
      this.lblnudOctTransposeDisplay.TabIndex = 115;
      this.lblnudOctTransposeDisplay.Text = "Display";
      // 
      // nudOctTransposeDisplay
      // 
      this.nudOctTransposeDisplay.Increment = new decimal(new int[] {
            12,
            0,
            0,
            0});
      this.nudOctTransposeDisplay.Location = new System.Drawing.Point(83, 61);
      this.nudOctTransposeDisplay.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
      this.nudOctTransposeDisplay.Minimum = new decimal(new int[] {
            48,
            0,
            0,
            -2147483648});
      this.nudOctTransposeDisplay.Name = "nudOctTransposeDisplay";
      this.nudOctTransposeDisplay.Size = new System.Drawing.Size(40, 20);
      this.nudOctTransposeDisplay.TabIndex = 114;
      this.nudOctTransposeDisplay.ValueChanged += new System.EventHandler(this.nudOctTransposeDisplay_ValueChanged);
      // 
      // lblnudOctTransposeKBPitch
      // 
      this.lblnudOctTransposeKBPitch.AutoSize = true;
      this.lblnudOctTransposeKBPitch.Location = new System.Drawing.Point(30, 22);
      this.lblnudOctTransposeKBPitch.Name = "lblnudOctTransposeKBPitch";
      this.lblnudOctTransposeKBPitch.Size = new System.Drawing.Size(48, 13);
      this.lblnudOctTransposeKBPitch.TabIndex = 113;
      this.lblnudOctTransposeKBPitch.Text = "KB Pitch";
      // 
      // nudOctTransposeKBPitch
      // 
      this.nudOctTransposeKBPitch.Increment = new decimal(new int[] {
            12,
            0,
            0,
            0});
      this.nudOctTransposeKBPitch.Location = new System.Drawing.Point(83, 19);
      this.nudOctTransposeKBPitch.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
      this.nudOctTransposeKBPitch.Minimum = new decimal(new int[] {
            48,
            0,
            0,
            -2147483648});
      this.nudOctTransposeKBPitch.Name = "nudOctTransposeKBPitch";
      this.nudOctTransposeKBPitch.Size = new System.Drawing.Size(40, 20);
      this.nudOctTransposeKBPitch.TabIndex = 0;
      this.nudOctTransposeKBPitch.ValueChanged += new System.EventHandler(this.nudOctTransposeKBPitch_ValueChanged);
      // 
      // grpCapitalizeRoots
      // 
      this.grpCapitalizeRoots.Controls.Add(this.chkCapitalizeRoots);
      this.grpCapitalizeRoots.Controls.Add(this.chkAutoCapitalize);
      this.grpCapitalizeRoots.Location = new System.Drawing.Point(102, 145);
      this.grpCapitalizeRoots.Name = "grpCapitalizeRoots";
      this.grpCapitalizeRoots.Size = new System.Drawing.Size(158, 53);
      this.grpCapitalizeRoots.TabIndex = 196;
      this.grpCapitalizeRoots.TabStop = false;
      // 
      // chkCapitalizeRoots
      // 
      this.chkCapitalizeRoots.AutoSize = true;
      this.chkCapitalizeRoots.Checked = true;
      this.chkCapitalizeRoots.CheckState = System.Windows.Forms.CheckState.Indeterminate;
      this.chkCapitalizeRoots.Enabled = false;
      this.chkCapitalizeRoots.Location = new System.Drawing.Point(7, 12);
      this.chkCapitalizeRoots.Name = "chkCapitalizeRoots";
      this.chkCapitalizeRoots.Size = new System.Drawing.Size(128, 17);
      this.chkCapitalizeRoots.TabIndex = 185;
      this.chkCapitalizeRoots.Text = "Capitalize Root Notes";
      this.chkCapitalizeRoots.UseVisualStyleBackColor = true;
      this.chkCapitalizeRoots.CheckedChanged += new System.EventHandler(this.chkCapitalizeRoots_CheckedChanged);
      // 
      // chkAutoCapitalize
      // 
      this.chkAutoCapitalize.AutoSize = true;
      this.chkAutoCapitalize.Checked = true;
      this.chkAutoCapitalize.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkAutoCapitalize.Location = new System.Drawing.Point(7, 31);
      this.chkAutoCapitalize.Name = "chkAutoCapitalize";
      this.chkAutoCapitalize.Size = new System.Drawing.Size(153, 17);
      this.chkAutoCapitalize.TabIndex = 186;
      this.chkAutoCapitalize.Text = "Auto Capitalize Root Notes";
      this.chkAutoCapitalize.UseVisualStyleBackColor = true;
      this.chkAutoCapitalize.CheckedChanged += new System.EventHandler(this.chkAutoCapitalize_CheckedChanged);
      // 
      // grpMisc
      // 
      this.grpMisc.Controls.Add(this.lblnudStartBar);
      this.grpMisc.Controls.Add(this.lblnudBeatHeight);
      this.grpMisc.Controls.Add(this.nudStartBar);
      this.grpMisc.Controls.Add(this.nudBeatHeight);
      this.grpMisc.Location = new System.Drawing.Point(267, 144);
      this.grpMisc.Name = "grpMisc";
      this.grpMisc.Size = new System.Drawing.Size(109, 56);
      this.grpMisc.TabIndex = 195;
      this.grpMisc.TabStop = false;
      // 
      // grpPlayMode
      // 
      this.grpPlayMode.Controls.Add(this.optModeKB);
      this.grpPlayMode.Controls.Add(this.optModeChords);
      this.grpPlayMode.Location = new System.Drawing.Point(7, 144);
      this.grpPlayMode.Name = "grpPlayMode";
      this.grpPlayMode.Size = new System.Drawing.Size(91, 54);
      this.grpPlayMode.TabIndex = 184;
      this.grpPlayMode.TabStop = false;
      this.grpPlayMode.Text = "Play Mode";
      // 
      // optModeKB
      // 
      this.optModeKB.AutoSize = true;
      this.optModeKB.Checked = true;
      this.optModeKB.Location = new System.Drawing.Point(15, 29);
      this.optModeKB.Name = "optModeKB";
      this.optModeKB.Size = new System.Drawing.Size(70, 17);
      this.optModeKB.TabIndex = 3;
      this.optModeKB.TabStop = true;
      this.optModeKB.Text = "Keyboard";
      this.optModeKB.UseVisualStyleBackColor = true;
      this.optModeKB.CheckedChanged += new System.EventHandler(this.optModeKB_CheckedChanged);
      // 
      // optModeChords
      // 
      this.optModeChords.AutoSize = true;
      this.optModeChords.Location = new System.Drawing.Point(15, 14);
      this.optModeChords.Name = "optModeChords";
      this.optModeChords.Size = new System.Drawing.Size(58, 17);
      this.optModeChords.TabIndex = 0;
      this.optModeChords.Text = "Chords";
      this.optModeChords.UseVisualStyleBackColor = true;
      this.optModeChords.CheckedChanged += new System.EventHandler(this.optModeChords_CheckedChanged);
      // 
      // grpSemitoneTrans
      // 
      this.grpSemitoneTrans.Controls.Add(this.lblnudTransposeStreamPitch);
      this.grpSemitoneTrans.Controls.Add(this.nudTransposeStreamPitch);
      this.grpSemitoneTrans.Controls.Add(this.lblnudTransposeKB);
      this.grpSemitoneTrans.Controls.Add(this.nudTransposeKB);
      this.grpSemitoneTrans.Controls.Add(this.lblnudTransposeKBPitch);
      this.grpSemitoneTrans.Controls.Add(this.nudTransposeKBPitch);
      this.grpSemitoneTrans.Location = new System.Drawing.Point(882, 61);
      this.grpSemitoneTrans.Name = "grpSemitoneTrans";
      this.grpSemitoneTrans.Size = new System.Drawing.Size(131, 90);
      this.grpSemitoneTrans.TabIndex = 179;
      this.grpSemitoneTrans.TabStop = false;
      this.grpSemitoneTrans.Text = "Semitone Transposition";
      // 
      // lblnudTransposeStreamPitch
      // 
      this.lblnudTransposeStreamPitch.AutoSize = true;
      this.lblnudTransposeStreamPitch.Location = new System.Drawing.Point(8, 64);
      this.lblnudTransposeStreamPitch.Name = "lblnudTransposeStreamPitch";
      this.lblnudTransposeStreamPitch.Size = new System.Drawing.Size(67, 13);
      this.lblnudTransposeStreamPitch.TabIndex = 128;
      this.lblnudTransposeStreamPitch.Text = "Stream Pitch";
      // 
      // nudTransposeStreamPitch
      // 
      this.nudTransposeStreamPitch.Location = new System.Drawing.Point(78, 61);
      this.nudTransposeStreamPitch.Maximum = new decimal(new int[] {
            36,
            0,
            0,
            0});
      this.nudTransposeStreamPitch.Minimum = new decimal(new int[] {
            36,
            0,
            0,
            -2147483648});
      this.nudTransposeStreamPitch.Name = "nudTransposeStreamPitch";
      this.nudTransposeStreamPitch.Size = new System.Drawing.Size(40, 20);
      this.nudTransposeStreamPitch.TabIndex = 127;
      this.nudTransposeStreamPitch.ValueChanged += new System.EventHandler(this.nudTransposeStreamPitch_ValueChanged);
      // 
      // lblnudTransposeKB
      // 
      this.lblnudTransposeKB.AutoSize = true;
      this.lblnudTransposeKB.Location = new System.Drawing.Point(23, 22);
      this.lblnudTransposeKB.Name = "lblnudTransposeKB";
      this.lblnudTransposeKB.Size = new System.Drawing.Size(52, 13);
      this.lblnudTransposeKB.TabIndex = 126;
      this.lblnudTransposeKB.Text = "Keyboard";
      // 
      // nudTransposeKB
      // 
      this.nudTransposeKB.Location = new System.Drawing.Point(78, 19);
      this.nudTransposeKB.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
      this.nudTransposeKB.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            -2147483648});
      this.nudTransposeKB.Name = "nudTransposeKB";
      this.nudTransposeKB.Size = new System.Drawing.Size(40, 20);
      this.nudTransposeKB.TabIndex = 125;
      this.nudTransposeKB.ValueChanged += new System.EventHandler(this.nudTransposeKB_ValueChanged);
      // 
      // lblnudTransposeKBPitch
      // 
      this.lblnudTransposeKBPitch.AutoSize = true;
      this.lblnudTransposeKBPitch.Location = new System.Drawing.Point(27, 43);
      this.lblnudTransposeKBPitch.Name = "lblnudTransposeKBPitch";
      this.lblnudTransposeKBPitch.Size = new System.Drawing.Size(48, 13);
      this.lblnudTransposeKBPitch.TabIndex = 124;
      this.lblnudTransposeKBPitch.Text = "KB Pitch";
      // 
      // nudTransposeKBPitch
      // 
      this.nudTransposeKBPitch.Location = new System.Drawing.Point(78, 40);
      this.nudTransposeKBPitch.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
      this.nudTransposeKBPitch.Minimum = new decimal(new int[] {
            11,
            0,
            0,
            -2147483648});
      this.nudTransposeKBPitch.Name = "nudTransposeKBPitch";
      this.nudTransposeKBPitch.Size = new System.Drawing.Size(40, 20);
      this.nudTransposeKBPitch.TabIndex = 123;
      this.nudTransposeKBPitch.ValueChanged += new System.EventHandler(this.nudTransposeKBPitch_ValueChanged);
      // 
      // lblKBDisplacement
      // 
      this.lblKBDisplacement.AutoSize = true;
      this.lblKBDisplacement.ForeColor = System.Drawing.Color.Red;
      this.lblKBDisplacement.Location = new System.Drawing.Point(1009, 174);
      this.lblKBDisplacement.Name = "lblKBDisplacement";
      this.lblKBDisplacement.Size = new System.Drawing.Size(88, 13);
      this.lblKBDisplacement.TabIndex = 178;
      this.lblKBDisplacement.Text = "KB Displacement";
      // 
      // nudKBDisplacement
      // 
      this.nudKBDisplacement.ForeColor = System.Drawing.Color.Red;
      this.nudKBDisplacement.Location = new System.Drawing.Point(1100, 171);
      this.nudKBDisplacement.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
      this.nudKBDisplacement.Minimum = new decimal(new int[] {
            11,
            0,
            0,
            -2147483648});
      this.nudKBDisplacement.Name = "nudKBDisplacement";
      this.nudKBDisplacement.Size = new System.Drawing.Size(40, 20);
      this.nudKBDisplacement.TabIndex = 177;
      this.nudKBDisplacement.ValueChanged += new System.EventHandler(this.nudKBDisplacement_ValueChanged);
      // 
      // cmdTempoReset
      // 
      this.cmdTempoReset.Location = new System.Drawing.Point(960, 162);
      this.cmdTempoReset.Name = "cmdTempoReset";
      this.cmdTempoReset.Size = new System.Drawing.Size(20, 19);
      this.cmdTempoReset.TabIndex = 175;
      this.cmdTempoReset.Text = "R";
      this.cmdTempoReset.UseVisualStyleBackColor = true;
      this.cmdTempoReset.Click += new System.EventHandler(this.cmdTempoReset_Click);
      // 
      // lblTempo
      // 
      this.lblTempo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTempo.Location = new System.Drawing.Point(926, 164);
      this.lblTempo.Name = "lblTempo";
      this.lblTempo.Size = new System.Drawing.Size(30, 15);
      this.lblTempo.TabIndex = 174;
      // 
      // lbltrkTempo
      // 
      this.lbltrkTempo.AutoSize = true;
      this.lbltrkTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbltrkTempo.Location = new System.Drawing.Point(739, 164);
      this.lbltrkTempo.Name = "lbltrkTempo";
      this.lbltrkTempo.Size = new System.Drawing.Size(40, 13);
      this.lbltrkTempo.TabIndex = 173;
      this.lbltrkTempo.Text = "Tempo";
      // 
      // trkTempo
      // 
      this.trkTempo.AutoSize = false;
      this.trkTempo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      this.trkTempo.Location = new System.Drawing.Point(785, 162);
      this.trkTempo.Maximum = 150;
      this.trkTempo.Minimum = -150;
      this.trkTempo.Name = "trkTempo";
      this.trkTempo.Size = new System.Drawing.Size(132, 17);
      this.trkTempo.TabIndex = 172;
      this.trkTempo.TickFrequency = 150;
      this.trkTempo.Scroll += new System.EventHandler(this.trkTempo_Scroll);
      // 
      // grpKBChan
      // 
      this.grpKBChan.Controls.Add(this.lblnudKBChanOut);
      this.grpKBChan.Controls.Add(this.lblcmbKBChanPatch);
      this.grpKBChan.Controls.Add(this.nudKBChanOut);
      this.grpKBChan.Controls.Add(this.cmbKBChanPatch);
      this.grpKBChan.Controls.Add(this.lbltrkKBChanVol);
      this.grpKBChan.Controls.Add(this.trkKBChanVol);
      this.grpKBChan.Controls.Add(this.trkKBChanPan);
      this.grpKBChan.Controls.Add(this.lbltrkKBChanPan);
      this.grpKBChan.Location = new System.Drawing.Point(522, 61);
      this.grpKBChan.Name = "grpKBChan";
      this.grpKBChan.Size = new System.Drawing.Size(209, 130);
      this.grpKBChan.TabIndex = 215;
      this.grpKBChan.TabStop = false;
      this.grpKBChan.Text = "Keyboard Channel";
      // 
      // lblnudKBChanOut
      // 
      this.lblnudKBChanOut.AutoSize = true;
      this.lblnudKBChanOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblnudKBChanOut.Location = new System.Drawing.Point(14, 25);
      this.lblnudKBChanOut.Name = "lblnudKBChanOut";
      this.lblnudKBChanOut.Size = new System.Drawing.Size(86, 13);
      this.lblnudKBChanOut.TabIndex = 13;
      this.lblnudKBChanOut.Text = "Channel Number";
      // 
      // lblcmbKBChanPatch
      // 
      this.lblcmbKBChanPatch.AutoSize = true;
      this.lblcmbKBChanPatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblcmbKBChanPatch.Location = new System.Drawing.Point(15, 52);
      this.lblcmbKBChanPatch.Name = "lblcmbKBChanPatch";
      this.lblcmbKBChanPatch.Size = new System.Drawing.Size(35, 13);
      this.lblcmbKBChanPatch.TabIndex = 46;
      this.lblcmbKBChanPatch.Text = "Patch";
      // 
      // nudKBChanOut
      // 
      this.nudKBChanOut.Location = new System.Drawing.Point(103, 23);
      this.nudKBChanOut.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
      this.nudKBChanOut.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudKBChanOut.Name = "nudKBChanOut";
      this.nudKBChanOut.Size = new System.Drawing.Size(40, 20);
      this.nudKBChanOut.TabIndex = 14;
      this.nudKBChanOut.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudKBChanOut.ValueChanged += new System.EventHandler(this.nudKBChanOut_ValueChanged);
      // 
      // cmbKBChanPatch
      // 
      this.cmbKBChanPatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbKBChanPatch.FormattingEnabled = true;
      this.cmbKBChanPatch.Location = new System.Drawing.Point(56, 49);
      this.cmbKBChanPatch.Name = "cmbKBChanPatch";
      this.cmbKBChanPatch.Size = new System.Drawing.Size(140, 21);
      this.cmbKBChanPatch.TabIndex = 45;
      this.cmbKBChanPatch.SelectedIndexChanged += new System.EventHandler(this.cmbKBChanPatch_SelectedIndexChanged);
      // 
      // cmdXNeg
      // 
      this.cmdXNeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdXNeg.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.cmdXNeg.Location = new System.Drawing.Point(34, 4);
      this.cmdXNeg.Name = "cmdXNeg";
      this.cmdXNeg.Size = new System.Drawing.Size(29, 28);
      this.cmdXNeg.TabIndex = 255;
      this.cmdXNeg.Text = "-";
      this.cmdXNeg.UseVisualStyleBackColor = true;
      this.cmdXNeg.Click += new System.EventHandler(this.cmdXNeg_Click);
      // 
      // cmdXPos
      // 
      this.cmdXPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdXPos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.cmdXPos.Location = new System.Drawing.Point(3, 4);
      this.cmdXPos.Name = "cmdXPos";
      this.cmdXPos.Size = new System.Drawing.Size(29, 28);
      this.cmdXPos.TabIndex = 254;
      this.cmdXPos.Text = "+";
      this.cmdXPos.UseVisualStyleBackColor = true;
      this.cmdXPos.Click += new System.EventHandler(this.cmdXPos_Click);
      // 
      // lstTrks
      // 
      this.lstTrks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.lstTrks.FormattingEnabled = true;
      this.lstTrks.Location = new System.Drawing.Point(3, 39);
      this.lstTrks.Name = "lstTrks";
      this.lstTrks.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
      this.lstTrks.Size = new System.Drawing.Size(117, 147);
      this.lstTrks.TabIndex = 68;
      this.lstTrks.SelectedIndexChanged += new System.EventHandler(this.lstTrks_SelectedIndexChanged);
      // 
      // mnuWindow
      // 
      this.mnuWindow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuConfig,
            this.windowToolStripMenuItem,
            this.mnuHelp,
            this.mnuDebug});
      this.mnuWindow.Location = new System.Drawing.Point(0, 0);
      this.mnuWindow.Name = "mnuWindow";
      this.mnuWindow.ShowItemToolTips = true;
      this.mnuWindow.Size = new System.Drawing.Size(1527, 24);
      this.mnuWindow.TabIndex = 66;
      this.mnuWindow.Text = "menuStrip1";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew,
            this.mnuLoadProject,
            this.mnuRecent,
            this.mnuReloadProject,
            this.mnuImport,
            this.mnuLoadMultiMidi,
            this.toolStripSeparator1,
            this.mnuSaveProject,
            this.mnuSaveProjectAs,
            this.mnuSaveMidiFileAs,
            this.toolStripSeparator7,
            this.mnuSaveSettings,
            this.mnuExit,
            this.mnuRestart});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 20);
      this.mnuFile.Text = "File";
      // 
      // mnuNew
      // 
      this.mnuNew.Name = "mnuNew";
      this.mnuNew.Size = new System.Drawing.Size(197, 22);
      this.mnuNew.Text = "New Project";
      this.mnuNew.Click += new System.EventHandler(this.mnuNew_Click);
      // 
      // mnuLoadProject
      // 
      this.mnuLoadProject.Name = "mnuLoadProject";
      this.mnuLoadProject.Size = new System.Drawing.Size(197, 22);
      this.mnuLoadProject.Text = "Load Project";
      this.mnuLoadProject.Click += new System.EventHandler(this.mnuLoadProject_Click);
      // 
      // mnuRecent
      // 
      this.mnuRecent.Name = "mnuRecent";
      this.mnuRecent.Size = new System.Drawing.Size(197, 22);
      this.mnuRecent.Text = "Recent Projects";
      // 
      // mnuReloadProject
      // 
      this.mnuReloadProject.Enabled = false;
      this.mnuReloadProject.Name = "mnuReloadProject";
      this.mnuReloadProject.Size = new System.Drawing.Size(197, 22);
      this.mnuReloadProject.Text = "Reload Project";
      this.mnuReloadProject.Click += new System.EventHandler(this.mnuReloadProject_Click);
      // 
      // mnuImport
      // 
      this.mnuImport.Enabled = false;
      this.mnuImport.Name = "mnuImport";
      this.mnuImport.Size = new System.Drawing.Size(197, 22);
      this.mnuImport.Text = "Import Files";
      this.mnuImport.Click += new System.EventHandler(this.mnuImport_Click);
      // 
      // mnuLoadMultiMidi
      // 
      this.mnuLoadMultiMidi.ForeColor = System.Drawing.Color.Red;
      this.mnuLoadMultiMidi.Name = "mnuLoadMultiMidi";
      this.mnuLoadMultiMidi.Size = new System.Drawing.Size(197, 22);
      this.mnuLoadMultiMidi.Text = "Load Multiple MidiFiles";
      this.mnuLoadMultiMidi.Click += new System.EventHandler(this.mnuLoadMultiMidi_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(194, 6);
      // 
      // mnuSaveProject
      // 
      this.mnuSaveProject.Enabled = false;
      this.mnuSaveProject.Name = "mnuSaveProject";
      this.mnuSaveProject.Size = new System.Drawing.Size(197, 22);
      this.mnuSaveProject.Text = "Save Project";
      this.mnuSaveProject.Click += new System.EventHandler(this.mnuSaveProject_Click);
      // 
      // mnuSaveProjectAs
      // 
      this.mnuSaveProjectAs.Enabled = false;
      this.mnuSaveProjectAs.Name = "mnuSaveProjectAs";
      this.mnuSaveProjectAs.Size = new System.Drawing.Size(197, 22);
      this.mnuSaveProjectAs.Text = "Save Project As...";
      this.mnuSaveProjectAs.Click += new System.EventHandler(this.mnuSaveProjectAs_Click);
      // 
      // mnuSaveMidiFileAs
      // 
      this.mnuSaveMidiFileAs.Enabled = false;
      this.mnuSaveMidiFileAs.Name = "mnuSaveMidiFileAs";
      this.mnuSaveMidiFileAs.Size = new System.Drawing.Size(197, 22);
      this.mnuSaveMidiFileAs.Text = "Save Midi File As...";
      this.mnuSaveMidiFileAs.Click += new System.EventHandler(this.mnuSaveMidiFileAs_Click);
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(194, 6);
      // 
      // mnuSaveSettings
      // 
      this.mnuSaveSettings.Name = "mnuSaveSettings";
      this.mnuSaveSettings.Size = new System.Drawing.Size(197, 22);
      this.mnuSaveSettings.Text = "Save Settings";
      this.mnuSaveSettings.Click += new System.EventHandler(this.mnuSaveSettings_Click);
      // 
      // mnuExit
      // 
      this.mnuExit.Name = "mnuExit";
      this.mnuExit.Size = new System.Drawing.Size(197, 22);
      this.mnuExit.Text = "Exit Application";
      this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
      // 
      // mnuRestart
      // 
      this.mnuRestart.ForeColor = System.Drawing.Color.Red;
      this.mnuRestart.Name = "mnuRestart";
      this.mnuRestart.Size = new System.Drawing.Size(197, 22);
      this.mnuRestart.Text = "Restart Application";
      this.mnuRestart.Click += new System.EventHandler(this.mnuRestart_Click);
      // 
      // mnuConfig
      // 
      this.mnuConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCfgAudio,
            this.mnuCfgMidi,
            this.mnuKeyboardRanges,
            this.mnuCfgBezier,
            this.mnuCfgSwitch,
            this.mnuPaths,
            this.mnuManChordSync,
            this.mnuPCKBKeys,
            this.mnuCfgMisc,
            this.toolStripSeparator3,
            this.mnuShowAudioSyncWindow});
      this.mnuConfig.Name = "mnuConfig";
      this.mnuConfig.Size = new System.Drawing.Size(72, 20);
      this.mnuConfig.Text = "Configure";
      // 
      // mnuCfgAudio
      // 
      this.mnuCfgAudio.Name = "mnuCfgAudio";
      this.mnuCfgAudio.Size = new System.Drawing.Size(210, 22);
      this.mnuCfgAudio.Text = "Audio Devices";
      this.mnuCfgAudio.Click += new System.EventHandler(this.mnuCfgAudio_Click);
      // 
      // mnuCfgMidi
      // 
      this.mnuCfgMidi.Name = "mnuCfgMidi";
      this.mnuCfgMidi.Size = new System.Drawing.Size(210, 22);
      this.mnuCfgMidi.Text = "Midi Devices";
      this.mnuCfgMidi.Click += new System.EventHandler(this.mnuCfgMidi_Click);
      // 
      // mnuKeyboardRanges
      // 
      this.mnuKeyboardRanges.Name = "mnuKeyboardRanges";
      this.mnuKeyboardRanges.Size = new System.Drawing.Size(210, 22);
      this.mnuKeyboardRanges.Text = "Midi Keyboard Ranges";
      this.mnuKeyboardRanges.Click += new System.EventHandler(this.mnuRanges_Click);
      // 
      // mnuCfgBezier
      // 
      this.mnuCfgBezier.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCfgBezierVelocity,
            this.mnuCfgBezierAfterTouch});
      this.mnuCfgBezier.Name = "mnuCfgBezier";
      this.mnuCfgBezier.Size = new System.Drawing.Size(210, 22);
      this.mnuCfgBezier.Text = "Bezier";
      // 
      // mnuCfgBezierVelocity
      // 
      this.mnuCfgBezierVelocity.Name = "mnuCfgBezierVelocity";
      this.mnuCfgBezierVelocity.Size = new System.Drawing.Size(133, 22);
      this.mnuCfgBezierVelocity.Text = "Velocity";
      this.mnuCfgBezierVelocity.Click += new System.EventHandler(this.mnuCfgBezierVelocity_Click);
      // 
      // mnuCfgBezierAfterTouch
      // 
      this.mnuCfgBezierAfterTouch.Name = "mnuCfgBezierAfterTouch";
      this.mnuCfgBezierAfterTouch.Size = new System.Drawing.Size(133, 22);
      this.mnuCfgBezierAfterTouch.Text = "AfterTouch";
      this.mnuCfgBezierAfterTouch.Click += new System.EventHandler(this.mnuCfgBezierAfterTouch_Click);
      // 
      // mnuCfgSwitch
      // 
      this.mnuCfgSwitch.Name = "mnuCfgSwitch";
      this.mnuCfgSwitch.Size = new System.Drawing.Size(210, 22);
      this.mnuCfgSwitch.Text = "SwitchKeys/Pedal";
      this.mnuCfgSwitch.Click += new System.EventHandler(this.mnuCfgSwitch_Click);
      // 
      // mnuPaths
      // 
      this.mnuPaths.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPathProject,
            this.mnuPathSoundFonts,
            this.mnuPathMidiFiles,
            this.mnuPathAudioFiles});
      this.mnuPaths.Name = "mnuPaths";
      this.mnuPaths.Size = new System.Drawing.Size(210, 22);
      this.mnuPaths.Text = "Paths";
      // 
      // mnuPathProject
      // 
      this.mnuPathProject.Name = "mnuPathProject";
      this.mnuPathProject.Size = new System.Drawing.Size(137, 22);
      this.mnuPathProject.Text = "Project";
      this.mnuPathProject.Click += new System.EventHandler(this.mnuPathProject_Click);
      // 
      // mnuPathSoundFonts
      // 
      this.mnuPathSoundFonts.Name = "mnuPathSoundFonts";
      this.mnuPathSoundFonts.Size = new System.Drawing.Size(137, 22);
      this.mnuPathSoundFonts.Text = "SoundFonts";
      this.mnuPathSoundFonts.Click += new System.EventHandler(this.mnuPathSoundFonts_Click);
      // 
      // mnuPathMidiFiles
      // 
      this.mnuPathMidiFiles.Name = "mnuPathMidiFiles";
      this.mnuPathMidiFiles.Size = new System.Drawing.Size(137, 22);
      this.mnuPathMidiFiles.Text = "Midi Files";
      this.mnuPathMidiFiles.Click += new System.EventHandler(this.mnuPathMidiFiles_Click);
      // 
      // mnuPathAudioFiles
      // 
      this.mnuPathAudioFiles.Name = "mnuPathAudioFiles";
      this.mnuPathAudioFiles.Size = new System.Drawing.Size(137, 22);
      this.mnuPathAudioFiles.Text = "Audio Files";
      this.mnuPathAudioFiles.Click += new System.EventHandler(this.mnuPathAudioFiles_Click);
      // 
      // mnuManChordSync
      // 
      this.mnuManChordSync.Name = "mnuManChordSync";
      this.mnuManChordSync.Size = new System.Drawing.Size(210, 22);
      this.mnuManChordSync.Text = "Manual Chord Sync";
      this.mnuManChordSync.Click += new System.EventHandler(this.mnuManChordSync_Click);
      // 
      // mnuPCKBKeys
      // 
      this.mnuPCKBKeys.Name = "mnuPCKBKeys";
      this.mnuPCKBKeys.Size = new System.Drawing.Size(210, 22);
      this.mnuPCKBKeys.Text = "PCKB Keys";
      this.mnuPCKBKeys.Click += new System.EventHandler(this.mnuPCKBKeys_Click);
      // 
      // mnuCfgMisc
      // 
      this.mnuCfgMisc.Name = "mnuCfgMisc";
      this.mnuCfgMisc.Size = new System.Drawing.Size(210, 22);
      this.mnuCfgMisc.Text = "Miscellaneous";
      this.mnuCfgMisc.Click += new System.EventHandler(this.mnuCfgMisc_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(207, 6);
      // 
      // mnuShowAudioSyncWindow
      // 
      this.mnuShowAudioSyncWindow.Enabled = false;
      this.mnuShowAudioSyncWindow.Name = "mnuShowAudioSyncWindow";
      this.mnuShowAudioSyncWindow.Size = new System.Drawing.Size(210, 22);
      this.mnuShowAudioSyncWindow.Text = "Show AudioSync Window";
      this.mnuShowAudioSyncWindow.Click += new System.EventHandler(this.mnuShowAudioSyncWindow_Click);
      // 
      // windowToolStripMenuItem
      // 
      this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMap,
            this.mnuControls,
            this.toolStripSeparator2});
      this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
      this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
      this.windowToolStripMenuItem.Text = "Window";
      // 
      // mnuMap
      // 
      this.mnuMap.CheckOnClick = true;
      this.mnuMap.Name = "mnuMap";
      this.mnuMap.Size = new System.Drawing.Size(119, 22);
      this.mnuMap.Text = "Map";
      this.mnuMap.Click += new System.EventHandler(this.mnuMap_Click);
      // 
      // mnuControls
      // 
      this.mnuControls.Checked = true;
      this.mnuControls.CheckOnClick = true;
      this.mnuControls.CheckState = System.Windows.Forms.CheckState.Checked;
      this.mnuControls.Name = "mnuControls";
      this.mnuControls.Size = new System.Drawing.Size(119, 22);
      this.mnuControls.Text = "Controls";
      this.mnuControls.Click += new System.EventHandler(this.mnuControls_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(116, 6);
      // 
      // mnuHelp
      // 
      this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpContents,
            this.mnuShowInitialScreen,
            this.mnuWebPage,
            this.mnuSoundFonts,
            this.mnuAbout});
      this.mnuHelp.Name = "mnuHelp";
      this.mnuHelp.Size = new System.Drawing.Size(44, 20);
      this.mnuHelp.Text = "Help";
      // 
      // mnuHelpContents
      // 
      this.mnuHelpContents.Name = "mnuHelpContents";
      this.mnuHelpContents.Size = new System.Drawing.Size(211, 22);
      this.mnuHelpContents.Text = "Contents";
      this.mnuHelpContents.Click += new System.EventHandler(this.mnuHelpContents_Click);
      // 
      // mnuShowInitialScreen
      // 
      this.mnuShowInitialScreen.Name = "mnuShowInitialScreen";
      this.mnuShowInitialScreen.Size = new System.Drawing.Size(211, 22);
      this.mnuShowInitialScreen.Text = "Show Initial Screen";
      this.mnuShowInitialScreen.Click += new System.EventHandler(this.mnuShowInitialScreen_Click);
      // 
      // mnuWebPage
      // 
      this.mnuWebPage.Name = "mnuWebPage";
      this.mnuWebPage.Size = new System.Drawing.Size(211, 22);
      this.mnuWebPage.Text = "Chord Cadenza Web Page";
      this.mnuWebPage.Click += new System.EventHandler(this.mnuWebPage_Click);
      // 
      // mnuSoundFonts
      // 
      this.mnuSoundFonts.Name = "mnuSoundFonts";
      this.mnuSoundFonts.Size = new System.Drawing.Size(211, 22);
      this.mnuSoundFonts.Text = "SoundFonts Links";
      this.mnuSoundFonts.Click += new System.EventHandler(this.mnuSoundFonts_Click);
      // 
      // mnuAbout
      // 
      this.mnuAbout.Name = "mnuAbout";
      this.mnuAbout.Size = new System.Drawing.Size(211, 22);
      this.mnuAbout.Text = "About...";
      this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
      // 
      // mnuDebug
      // 
      this.mnuDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowFrmTest,
            this.mnuShowFrmConsole,
            this.mnuShowDebugInfo,
            this.mnuMonitorStats,
            this.mnuMonitorTimeline,
            this.mnuMonitor,
            this.mnuTestMidiMon});
      this.mnuDebug.ForeColor = System.Drawing.Color.Red;
      this.mnuDebug.Name = "mnuDebug";
      this.mnuDebug.Size = new System.Drawing.Size(54, 20);
      this.mnuDebug.Text = "Debug";
      // 
      // mnuShowFrmTest
      // 
      this.mnuShowFrmTest.ForeColor = System.Drawing.Color.Red;
      this.mnuShowFrmTest.Name = "mnuShowFrmTest";
      this.mnuShowFrmTest.Size = new System.Drawing.Size(263, 22);
      this.mnuShowFrmTest.Text = "Show frmTest";
      this.mnuShowFrmTest.Click += new System.EventHandler(this.mnuShowFrmTest_Click);
      // 
      // mnuShowFrmConsole
      // 
      this.mnuShowFrmConsole.ForeColor = System.Drawing.Color.Red;
      this.mnuShowFrmConsole.Name = "mnuShowFrmConsole";
      this.mnuShowFrmConsole.Size = new System.Drawing.Size(263, 22);
      this.mnuShowFrmConsole.Text = "Show frmConsole";
      this.mnuShowFrmConsole.Click += new System.EventHandler(this.mnuShowFrmConsole_Click);
      // 
      // mnuShowDebugInfo
      // 
      this.mnuShowDebugInfo.ForeColor = System.Drawing.Color.Red;
      this.mnuShowDebugInfo.Name = "mnuShowDebugInfo";
      this.mnuShowDebugInfo.Size = new System.Drawing.Size(263, 22);
      this.mnuShowDebugInfo.Text = "Show Debug Info";
      this.mnuShowDebugInfo.Click += new System.EventHandler(this.mnuShowDebugInfo_Click);
      // 
      // mnuMonitorStats
      // 
      this.mnuMonitorStats.ForeColor = System.Drawing.Color.Red;
      this.mnuMonitorStats.Name = "mnuMonitorStats";
      this.mnuMonitorStats.Size = new System.Drawing.Size(263, 22);
      this.mnuMonitorStats.Text = "Show MidiOnOff Monitor Totals";
      this.mnuMonitorStats.Click += new System.EventHandler(this.mnuMonitorTotals_Click);
      // 
      // mnuMonitorTimeline
      // 
      this.mnuMonitorTimeline.ForeColor = System.Drawing.Color.Red;
      this.mnuMonitorTimeline.Name = "mnuMonitorTimeline";
      this.mnuMonitorTimeline.Size = new System.Drawing.Size(263, 22);
      this.mnuMonitorTimeline.Text = "Show MidiOn/Off Monitor Timeline";
      this.mnuMonitorTimeline.Click += new System.EventHandler(this.mnuMonitorTimeline_Click);
      // 
      // mnuMonitor
      // 
      this.mnuMonitor.CheckOnClick = true;
      this.mnuMonitor.ForeColor = System.Drawing.Color.Red;
      this.mnuMonitor.Name = "mnuMonitor";
      this.mnuMonitor.Size = new System.Drawing.Size(263, 22);
      this.mnuMonitor.Text = "Monitor MidiInKB On/Off";
      this.mnuMonitor.Click += new System.EventHandler(this.mnuMonitor_Click);
      // 
      // mnuTestMidiMon
      // 
      this.mnuTestMidiMon.ForeColor = System.Drawing.Color.Red;
      this.mnuTestMidiMon.Name = "mnuTestMidiMon";
      this.mnuTestMidiMon.Size = new System.Drawing.Size(263, 22);
      this.mnuTestMidiMon.Text = "Test MidiMon";
      this.mnuTestMidiMon.Click += new System.EventHandler(this.mnuTestMidiMon_Click);
      // 
      // helpProvider1
      // 
      this.helpProvider1.HelpNamespace = "D:\\D2\\Dev\\CS.Express\\Expression\\Help\\Test02\\Test02.chm";
      // 
      // panMaps
      // 
      this.panMaps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panMaps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panMaps.Controls.Add(this.chkScroll);
      this.panMaps.Controls.Add(this.cmdXNeg);
      this.panMaps.Controls.Add(this.panNoteMap);
      this.panMaps.Controls.Add(this.cmdXPos);
      this.panMaps.Controls.Add(this.dgvLyrics);
      this.panMaps.Controls.Add(this.picBarsX);
      this.panMaps.Controls.Add(this.lstTrks);
      this.panMaps.Location = new System.Drawing.Point(4, 31);
      this.panMaps.Name = "panMaps";
      this.panMaps.Size = new System.Drawing.Size(1509, 194);
      this.panMaps.TabIndex = 67;
      this.panMaps.Visible = false;
      // 
      // chkScroll
      // 
      this.chkScroll.AutoSize = true;
      this.chkScroll.Location = new System.Drawing.Point(68, 12);
      this.chkScroll.Name = "chkScroll";
      this.chkScroll.Size = new System.Drawing.Size(52, 17);
      this.chkScroll.TabIndex = 253;
      this.chkScroll.Text = "Scroll";
      this.chkScroll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.chkScroll.UseVisualStyleBackColor = true;
      // 
      // panNoteMap
      // 
      this.panNoteMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panNoteMap.AutoScroll = true;
      this.panNoteMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panNoteMap.Controls.Add(this.picChordNamesX);
      this.panNoteMap.Controls.Add(this.picNoteMap);
      this.panNoteMap.Location = new System.Drawing.Point(124, 63);
      this.panNoteMap.Name = "panNoteMap";
      this.panNoteMap.Size = new System.Drawing.Size(1379, 123);
      this.panNoteMap.TabIndex = 116;
      this.panNoteMap.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panNoteMap_Scroll);
      // 
      // picChordNamesX
      // 
      this.picChordNamesX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picChordNamesX.Location = new System.Drawing.Point(-1, 3);
      this.picChordNamesX.Name = "picChordNamesX";
      this.picChordNamesX.Size = new System.Drawing.Size(1518, 34);
      this.picChordNamesX.TabIndex = 114;
      this.picChordNamesX.TabStop = false;
      this.picChordNamesX.Paint += new System.Windows.Forms.PaintEventHandler(this.picChordNamesX_Paint);
      this.picChordNamesX.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseClick);
      // 
      // picNoteMap
      // 
      this.picNoteMap.BackColor = System.Drawing.Color.White;
      this.picNoteMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picNoteMap.Location = new System.Drawing.Point(0, 41);
      this.picNoteMap.Name = "picNoteMap";
      this.picNoteMap.Size = new System.Drawing.Size(1515, 56);
      this.picNoteMap.TabIndex = 0;
      this.picNoteMap.TabStop = false;
      this.picNoteMap.Paint += new System.Windows.Forms.PaintEventHandler(this.picNoteMap_Paint);
      this.picNoteMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseClick);
      // 
      // dgvLyrics
      // 
      this.dgvLyrics.AllowUserToAddRows = false;
      this.dgvLyrics.AllowUserToDeleteRows = false;
      this.dgvLyrics.AllowUserToResizeColumns = false;
      this.dgvLyrics.AllowUserToResizeRows = false;
      this.dgvLyrics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgvLyrics.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvLyrics.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgvLyrics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgvLyrics.ColumnHeadersVisible = false;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvLyrics.DefaultCellStyle = dataGridViewCellStyle2;
      this.dgvLyrics.EnableHeadersVisualStyles = false;
      this.dgvLyrics.Location = new System.Drawing.Point(124, 26);
      this.dgvLyrics.MultiSelect = false;
      this.dgvLyrics.Name = "dgvLyrics";
      this.dgvLyrics.ReadOnly = true;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvLyrics.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
      this.dgvLyrics.RowHeadersVisible = false;
      this.dgvLyrics.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.dgvLyrics.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
      this.dgvLyrics.Size = new System.Drawing.Size(1379, 31);
      this.dgvLyrics.TabIndex = 115;
      this.dgvLyrics.Visible = false;
      this.dgvLyrics.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLyrics_CellClick);
      this.dgvLyrics.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvLyrics_ColumnAdded);
      this.dgvLyrics.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvLyrics_Scroll);
      // 
      // picBarsX
      // 
      this.picBarsX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.picBarsX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picBarsX.Location = new System.Drawing.Point(124, 3);
      this.picBarsX.Name = "picBarsX";
      this.picBarsX.Size = new System.Drawing.Size(1380, 17);
      this.picBarsX.TabIndex = 112;
      this.picBarsX.TabStop = false;
      this.picBarsX.Paint += new System.Windows.Forms.PaintEventHandler(this.picBarsX_Paint);
      this.picBarsX.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBarsX_MouseClick);
      // 
      // picBottom
      // 
      this.picBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.picBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picBottom.Location = new System.Drawing.Point(56, 570);
      this.picBottom.Name = "picBottom";
      this.picBottom.Size = new System.Drawing.Size(948, 41);
      this.picBottom.TabIndex = 64;
      this.picBottom.TabStop = false;
      this.picBottom.Paint += new System.Windows.Forms.PaintEventHandler(this.picBottom_Paint);
      this.picBottom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picChordsBottom_MouseDown);
      this.picBottom.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picChordsBottom_MouseMove);
      this.picBottom.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picChordsBottom_MouseUp);
      // 
      // picChordNames
      // 
      this.picChordNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.picChordNames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picChordNames.Location = new System.Drawing.Point(1404, 492);
      this.picChordNames.Name = "picChordNames";
      this.picChordNames.Size = new System.Drawing.Size(90, 71);
      this.picChordNames.TabIndex = 40;
      this.picChordNames.TabStop = false;
      this.picChordNames.Paint += new System.Windows.Forms.PaintEventHandler(this.picChordNames_Paint);
      // 
      // picChords
      // 
      this.picChords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.picChords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picChords.Location = new System.Drawing.Point(56, 492);
      this.picChords.Name = "picChords";
      this.picChords.Size = new System.Drawing.Size(1342, 71);
      this.picChords.TabIndex = 39;
      this.picChords.TabStop = false;
      this.picChords.Paint += new System.Windows.Forms.PaintEventHandler(this.picChords_Paint);
      this.picChords.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picChordsBottom_MouseDown);
      this.picChords.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picChordsBottom_MouseMove);
      this.picChords.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picChordsBottom_MouseUp);
      // 
      // picBars
      // 
      this.picBars.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picBars.Location = new System.Drawing.Point(3, 492);
      this.picBars.Name = "picBars";
      this.picBars.Size = new System.Drawing.Size(48, 48);
      this.picBars.TabIndex = 38;
      this.picBars.TabStop = false;
      this.picBars.Paint += new System.Windows.Forms.PaintEventHandler(this.picBars_Paint);
      // 
      // frmSC
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1527, 618);
      this.Controls.Add(this.panMaps);
      this.Controls.Add(this.mnuWindow);
      this.Controls.Add(this.txtChordBottom);
      this.Controls.Add(this.picBottom);
      this.Controls.Add(this.vScrollBar1);
      this.Controls.Add(this.picChordNames);
      this.Controls.Add(this.picChords);
      this.Controls.Add(this.picBars);
      this.Controls.Add(this.panControls);
      this.HelpButton = true;
      this.KeyPreview = true;
      this.Name = "frmSC";
      this.Text = "PLAYMAP";
      this.Activated += new System.EventHandler(this.frmSC_Activated);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSC_FormClosing);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSC_FormClosed);
      this.Load += new System.EventHandler(this.frmSC_Load);
      this.ResizeEnd += new System.EventHandler(this.frmSC_ResizeEnd);
      this.SizeChanged += new System.EventHandler(this.frmSC_SizeChanged);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmSC_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmSC_DragEnter);
      ((System.ComponentModel.ISupportInitialize)(this.trkKBChanVol)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkKBChanPan)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudSyncopationNN)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudSyncopationDD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudOctaves)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudStartBar)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudBeatHeight)).EndInit();
      this.panControls.ResumeLayout(false);
      this.panControls.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trkPCKBVel)).EndInit();
      this.grpSustainAction.ResumeLayout(false);
      this.grpSustainAction.PerformLayout();
      this.panDisplay.ResumeLayout(false);
      this.panForms.ResumeLayout(false);
      this.panPlay.ResumeLayout(false);
      this.panFiles.ResumeLayout(false);
      this.panTrkStream.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.trkStreamVol)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkStreamPan)).EndInit();
      this.panTrkKB.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.trkKBVol)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkKBPan)).EndInit();
      this.panTrkAudio.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.trkAudioVol)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkAudioPan)).EndInit();
      this.grpNoteDisplay.ResumeLayout(false);
      this.grpNoteDisplay.PerformLayout();
      this.grpOctTrans.ResumeLayout(false);
      this.grpOctTrans.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudOctTransposeKB)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudOctTransposeDisplay)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudOctTransposeKBPitch)).EndInit();
      this.grpCapitalizeRoots.ResumeLayout(false);
      this.grpCapitalizeRoots.PerformLayout();
      this.grpMisc.ResumeLayout(false);
      this.grpMisc.PerformLayout();
      this.grpPlayMode.ResumeLayout(false);
      this.grpPlayMode.PerformLayout();
      this.grpSemitoneTrans.ResumeLayout(false);
      this.grpSemitoneTrans.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudTransposeStreamPitch)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudTransposeKB)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudTransposeKBPitch)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudKBDisplacement)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkTempo)).EndInit();
      this.grpKBChan.ResumeLayout(false);
      this.grpKBChan.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudKBChanOut)).EndInit();
      this.mnuWindow.ResumeLayout(false);
      this.mnuWindow.PerformLayout();
      this.panMaps.ResumeLayout(false);
      this.panMaps.PerformLayout();
      this.panNoteMap.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.picChordNamesX)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picNoteMap)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvLyrics)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picBarsX)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picBottom)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picChordNames)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picChords)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picBars)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.PictureBox picBars;
    internal System.Windows.Forms.PictureBox picChords;
    internal System.Windows.Forms.VScrollBar vScrollBar1;
    internal System.Windows.Forms.PictureBox picBottom;
    internal System.Windows.Forms.TextBox txtChordBottom;
    internal System.Windows.Forms.PictureBox picChordNames;
    private System.Windows.Forms.FontDialog fontDialog1;
    internal System.Windows.Forms.TrackBar trkKBChanVol;
    internal System.Windows.Forms.TrackBar trkKBChanPan;
    internal System.Windows.Forms.CheckBox chkAlignKB;
    internal System.Windows.Forms.CheckBox chkShowChords;
    private System.Windows.Forms.Label lblnudSyncopation;
    private System.Windows.Forms.Label lblnudSyncopationNote;
    private System.Windows.Forms.Label lblnudSyncopationSlash;
    internal System.Windows.Forms.NumericUpDown nudSyncopationNN;
    internal System.Windows.Forms.NumericUpDown nudSyncopationDD;
    internal System.Windows.Forms.CheckBox chkShowTracks;
    internal System.Windows.Forms.CheckBox chkShowChordNames;
    private System.Windows.Forms.Button cmdGoToStart;
    internal System.Windows.Forms.Button cmdPlayMidi;
    internal System.Windows.Forms.Button cmdStopPlay;
    internal System.Windows.Forms.CheckBox chkShowBeats;
    internal System.Windows.Forms.Button cmdPausePlay;
    private System.Windows.Forms.Label lbltrkKBChanVol;
    private System.Windows.Forms.Label lblnudOctaves;
    internal System.Windows.Forms.NumericUpDown nudOctaves;
    private System.Windows.Forms.Label lblnudStartBar;
    internal System.Windows.Forms.NumericUpDown nudStartBar;
    private System.Windows.Forms.Label lblnudBeatHeight;
    internal System.Windows.Forms.NumericUpDown nudBeatHeight;
    private System.Windows.Forms.Label lbltrkKBChanPan;
    private System.Windows.Forms.Panel panControls;
    private System.Windows.Forms.Button cmdTempoReset;
    internal System.Windows.Forms.Label lblTempo;
    private System.Windows.Forms.Label lbltrkTempo;
    internal System.Windows.Forms.TrackBar trkTempo;
    private System.Windows.Forms.GroupBox grpOctTrans;
    private System.Windows.Forms.Label lblnudOctTransposeKBPitch;
    internal System.Windows.Forms.NumericUpDown nudOctTransposeKBPitch;
    internal System.Windows.Forms.NumericUpDown nudOctTransposeKB;
    internal System.Windows.Forms.NumericUpDown nudOctTransposeDisplay;
    private System.Windows.Forms.Label lblKBDisplacement;
    internal System.Windows.Forms.NumericUpDown nudKBDisplacement;
    private System.Windows.Forms.GroupBox grpSemitoneTrans;
    private System.Windows.Forms.Label lblnudTransposeStreamPitch;
    internal System.Windows.Forms.NumericUpDown nudTransposeStreamPitch;
    private System.Windows.Forms.Label lblnudTransposeKB;
    internal System.Windows.Forms.NumericUpDown nudTransposeKB;
    private System.Windows.Forms.Label lblnudTransposeKBPitch;
    internal System.Windows.Forms.NumericUpDown nudTransposeKBPitch;
    private System.Windows.Forms.Label lblnudKBChanOut;
    private System.Windows.Forms.Label lblcmbKBChanPatch;
    internal System.Windows.Forms.ComboBox cmbKBChanPatch;
    internal System.Windows.Forms.NumericUpDown nudKBChanOut;
    private System.Windows.Forms.GroupBox grpPlayMode;
    internal System.Windows.Forms.RadioButton optModeKB;
    internal System.Windows.Forms.RadioButton optModeChords;
    internal System.Windows.Forms.CheckBox chkAutoCapitalize;
    internal System.Windows.Forms.CheckBox chkCapitalizeRoots;
    internal System.Windows.Forms.Button cmdLoadProject;
    internal System.Windows.Forms.Button cmdMultiMap;
    internal System.Windows.Forms.Button cmdPanic;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuNew;
    internal System.Windows.Forms.ToolStripMenuItem mnuLoadProject;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem mnuConfig;
    private System.Windows.Forms.ToolStripMenuItem mnuCfgAudio;
    private System.Windows.Forms.ToolStripMenuItem mnuCfgMidi;
    private System.Windows.Forms.ToolStripMenuItem mnuCfgBezier;
    private System.Windows.Forms.ToolStripMenuItem mnuCfgBezierVelocity;
    private System.Windows.Forms.ToolStripMenuItem mnuCfgBezierAfterTouch;
    private System.Windows.Forms.ToolStripMenuItem mnuCfgSwitch;
    private System.Windows.Forms.ToolStripMenuItem mnuPaths;
    private System.Windows.Forms.ToolStripMenuItem mnuPathProject;
    private System.Windows.Forms.ToolStripMenuItem mnuPathSoundFonts;
    private System.Windows.Forms.ToolStripMenuItem mnuDebug;
    private System.Windows.Forms.ToolStripMenuItem mnuShowDebugInfo;
    private System.Windows.Forms.ToolStripMenuItem mnuMonitorStats;
    private System.Windows.Forms.ToolStripMenuItem mnuMonitorTimeline;
    private System.Windows.Forms.FolderBrowserDialog fbd;
    private System.Windows.Forms.ToolStripMenuItem mnuKeyboardRanges;
    private System.Windows.Forms.ToolStripMenuItem mnuCfgMisc;
    private System.Windows.Forms.GroupBox grpCapitalizeRoots;
    private System.Windows.Forms.GroupBox grpMisc;
    private System.Windows.Forms.ToolStripMenuItem mnuExit;
    private System.Windows.Forms.ToolStripMenuItem mnuSaveSettings;
    internal System.Windows.Forms.Button cmdAlign;
    private System.Windows.Forms.GroupBox grpKBChan;
    internal System.Windows.Forms.Button cmdChordMap;
    internal System.Windows.Forms.Button cmdShowSumm;
    internal System.Windows.Forms.Button cmdPlayAudio;
    internal System.Windows.Forms.CheckBox chkShowChordNotes;
    private System.Windows.Forms.HelpProvider helpProvider1;
    private System.Windows.Forms.ToolStripMenuItem mnuHelp;
    private System.Windows.Forms.ToolStripMenuItem mnuHelpContents;
    private System.Windows.Forms.ToolStripMenuItem mnuMonitor;
    private System.Windows.Forms.ToolStripMenuItem mnuShowInitialScreen;
    internal System.Windows.Forms.Button cmdColours;
    internal System.Windows.Forms.Button cmdChordFont;
    internal System.Windows.Forms.Button cmdNoteFont;
    private System.Windows.Forms.GroupBox grpNoteDisplay;
    private System.Windows.Forms.Label lblcmbFirstNote;
    internal System.Windows.Forms.ComboBox cmbFirstNote;
    internal System.Windows.Forms.CheckBox chkRunChordNotes;
    internal System.Windows.Forms.RadioButton optShowNone;
    internal System.Windows.Forms.RadioButton optShowNoteName;
    internal System.Windows.Forms.RadioButton optShowSolfa;
    private System.Windows.Forms.Label lbllblKeyThis;
    private System.Windows.Forms.Label lbllblKeyNext;
    internal System.Windows.Forms.Button cmdHelp;
    internal System.Windows.Forms.ToolStripMenuItem mnuShowAudioSyncWindow;
    private System.Windows.Forms.ToolStripMenuItem mnuPathMidiFiles;
    private System.Windows.Forms.ToolStripMenuItem mnuPathAudioFiles;
    internal System.Windows.Forms.Button cmdNew;
    internal System.Windows.Forms.MenuStrip mnuWindow;
    private System.Windows.Forms.ToolStripMenuItem mnuRestart;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    internal System.Windows.Forms.Button cmdSaveProject;
    private System.Windows.Forms.Label lbltrkAudioVol;
    internal System.Windows.Forms.TrackBar trkAudioVol;
    internal System.Windows.Forms.TrackBar trkAudioPan;
    private System.Windows.Forms.Label lbltrkAudioPan;
    private System.Windows.Forms.Label lbltrkStreamVol;
    internal System.Windows.Forms.TrackBar trkStreamVol;
    internal System.Windows.Forms.TrackBar trkStreamPan;
    private System.Windows.Forms.Label lbltrkStreamPan;
    private System.Windows.Forms.Label lbltrkKBVol;
    internal System.Windows.Forms.TrackBar trkKBVol;
    internal System.Windows.Forms.TrackBar trkKBPan;
    private System.Windows.Forms.Label lbltrkKBPan;
    internal System.Windows.Forms.Panel panTrkAudio;
    internal System.Windows.Forms.Panel panTrkStream;
    internal System.Windows.Forms.Panel panTrkKB;
    private System.Windows.Forms.ToolStripMenuItem mnuRecent;
    internal System.Windows.Forms.Button cmdTonnetz;
    internal System.Windows.Forms.ToolStripMenuItem mnuSaveMidiFileAs;
    private System.Windows.Forms.ToolStripMenuItem mnuAbout;
    private System.Windows.Forms.Label lblKeyVel;
    internal System.Windows.Forms.Button cmdSaveProjectAs;
    internal System.Windows.Forms.Button cmdSyncAudio;
    internal System.Windows.Forms.Button cmdPlayAndRecordAudio;
    protected System.Windows.Forms.Button cmdShowAudioSyncWindow;
    private System.Windows.Forms.Button cmdUpdateLyrics;
    private System.Windows.Forms.Panel panPlay;
    private System.Windows.Forms.Panel panFiles;
    private System.Windows.Forms.Panel panForms;
    private System.Windows.Forms.Panel panDisplay;
    internal System.Windows.Forms.ToolStripMenuItem mnuSaveProject;
    internal System.Windows.Forms.ToolStripMenuItem mnuSaveProjectAs;
    private System.Windows.Forms.ToolStripMenuItem mnuLoadMultiMidi;
    internal System.Windows.Forms.CheckBox chkSwitchKBChord;
    internal System.Windows.Forms.CheckBox chkSwitchSustain;
    private System.Windows.Forms.Label lbllblRangeVis;
    private System.Windows.Forms.Label lblRangeVis;
    private System.Windows.Forms.Label lblKeyThis;
    private System.Windows.Forms.Label lblKeyNext;
    private System.Windows.Forms.Label lblRangeTrk;
    private System.Windows.Forms.Label lbllblRangeTrk;
    internal System.Windows.Forms.GroupBox grpSustainAction;
    internal System.Windows.Forms.RadioButton optSustainNormal;
    internal System.Windows.Forms.RadioButton optSustainSendCtlr;
    internal System.Windows.Forms.RadioButton optSustainReplay;
    internal System.Windows.Forms.CheckBox chkShowChordsRel;
    private System.Windows.Forms.Button cmdResetPlay;
    internal System.Windows.Forms.PictureBox picBarsX;
    private System.Windows.Forms.Panel panNoteMap;
    private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuControls;
    private System.Windows.Forms.Button cmdXNeg;
    private System.Windows.Forms.Button cmdXPos;
    internal System.Windows.Forms.PictureBox picChordNamesX;
    internal System.Windows.Forms.DataGridView dgvLyrics;
    internal System.Windows.Forms.Panel panMaps;
    internal System.Windows.Forms.PictureBox picNoteMap;
    private System.Windows.Forms.ListBox lstTrks;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    internal System.Windows.Forms.CheckBox chkScroll;
    private System.Windows.Forms.ToolStripMenuItem mnuManChordSync;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    internal System.Windows.Forms.CheckBox chkManSyncChord;
    internal System.Windows.Forms.ToolStripMenuItem mnuMap;
    private System.Windows.Forms.ToolStripMenuItem mnuShowFrmConsole;
    internal System.Windows.Forms.ToolStripMenuItem mnuReloadProject;
    private System.Windows.Forms.ToolStripMenuItem mnuShowFrmTest;
    private System.Windows.Forms.ToolStripMenuItem mnuWebPage;
    private System.Windows.Forms.ToolStripMenuItem mnuSoundFonts;
    private System.Windows.Forms.ToolStripMenuItem mnuTestMidiMon;
    internal System.Windows.Forms.ToolStripMenuItem mnuImport;
    internal System.Windows.Forms.CheckBox chkShowPCKBChar;
    private System.Windows.Forms.ToolStripMenuItem mnuPCKBKeys;
    internal System.Windows.Forms.TrackBar trkPCKBVel;
    internal System.Windows.Forms.Label lblnudOctTransposeKB;
    internal System.Windows.Forms.Label lblnudOctTransposeDisplay;
  }
}