namespace ChordCadenza.Forms {
  partial class frmChordMap {
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
        BarFont.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      this.chkShowBeats = new System.Windows.Forms.CheckBox();
      this.panChords = new System.Windows.Forms.Panel();
      this.lblTracksSelected = new System.Windows.Forms.Label();
      this.cmdExec = new System.Windows.Forms.Button();
      this.grpChordsDiv = new System.Windows.Forms.GroupBox();
      this.optChordsAuto = new System.Windows.Forms.RadioButton();
      this.optChordsBeat = new System.Windows.Forms.RadioButton();
      this.optChordsBar = new System.Windows.Forms.RadioButton();
      this.optChordsHalfBar = new System.Windows.Forms.RadioButton();
      this.cmdConfigChords = new System.Windows.Forms.Button();
      this.cmdPausePlay = new System.Windows.Forms.Button();
      this.cmdPlayMidi = new System.Windows.Forms.Button();
      this.cmdStopPlay = new System.Windows.Forms.Button();
      this.optCopyToEmpty = new System.Windows.Forms.RadioButton();
      this.optMerge = new System.Windows.Forms.RadioButton();
      this.optReplace = new System.Windows.Forms.RadioButton();
      this.grpCopyOptions = new System.Windows.Forms.GroupBox();
      this.cmdPicQuantToFile = new System.Windows.Forms.Button();
      this.chkShowKB = new System.Windows.Forms.CheckBox();
      this.cmdGoToStart = new System.Windows.Forms.Button();
      this.nudTransposeChordNames = new System.Windows.Forms.NumericUpDown();
      this.cmdAdvanced = new System.Windows.Forms.Button();
      this.mnuModHit = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuModHitRemove = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuModHitChange = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuModHitcmbChange = new System.Windows.Forms.ToolStripComboBox();
      this.mnuModMiss = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuModMissNew = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuModMisscmbNew = new System.Windows.Forms.ToolStripComboBox();
      this.chkShowQuant = new System.Windows.Forms.CheckBox();
      this.mnuTSig = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuTSigEnd = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuTSigEndCommon = new System.Windows.Forms.ToolStripMenuItem();
      this.mnucmbTSigEndCommon = new System.Windows.Forms.ToolStripComboBox();
      this.mnuTSigEndAny = new System.Windows.Forms.ToolStripMenuItem();
      this.mnucmbTSigEndAny = new System.Windows.Forms.ToolStripComboBox();
      this.mnuTSigAreaAny = new System.Windows.Forms.ToolStripMenuItem();
      this.commonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnucmbTSigAreaCommon = new System.Windows.Forms.ToolStripComboBox();
      this.anyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnucmbTSigAreaAny = new System.Windows.Forms.ToolStripComboBox();
      this.mnuTSigRemove = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuTSigInsert = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuTSigInsertCommon = new System.Windows.Forms.ToolStripMenuItem();
      this.mnucmbTSigInsertCommon = new System.Windows.Forms.ToolStripComboBox();
      this.mnuTSigInsertAny = new System.Windows.Forms.ToolStripMenuItem();
      this.mnucmbTSigInsertAny = new System.Windows.Forms.ToolStripComboBox();
      this.mnuChangeLength = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuTSigHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.cmdPlayAndRecordAudio = new System.Windows.Forms.Button();
      this.cmdPanic = new System.Windows.Forms.Button();
      this.cmdSyncAudio = new System.Windows.Forms.Button();
      this.cmdPlayAudio = new System.Windows.Forms.Button();
      this.lblTransposeChordNames = new System.Windows.Forms.Label();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.panAdvanced = new System.Windows.Forms.Panel();
      this.lblQI = new System.Windows.Forms.Label();
      this.chkOptChordsNone = new System.Windows.Forms.CheckBox();
      this.lblLitQI = new System.Windows.Forms.Label();
      this.cmdChordinate = new System.Windows.Forms.Button();
      this.lblTicks = new System.Windows.Forms.Label();
      this.lblLitTicks = new System.Windows.Forms.Label();
      this.lblCHPDescLit = new System.Windows.Forms.Label();
      this.txtCHPDesc = new System.Windows.Forms.TextBox();
      this.panFiles = new System.Windows.Forms.Panel();
      this.cmdNew = new System.Windows.Forms.Button();
      this.cmdLoadProject = new System.Windows.Forms.Button();
      this.cmdSaveProject = new System.Windows.Forms.Button();
      this.cmdSaveProjectAs = new System.Windows.Forms.Button();
      this.panPlay = new System.Windows.Forms.Panel();
      this.cmdSaveAs = new System.Windows.Forms.Button();
      this.cmdLoadMidi = new System.Windows.Forms.Button();
      this.cmdShowSumm = new System.Windows.Forms.Button();
      this.cmdInsertBars = new System.Windows.Forms.Button();
      this.cmdDeleteBars = new System.Windows.Forms.Button();
      this.cmdCopy = new System.Windows.Forms.Button();
      this.cmdUpdateLyrics = new System.Windows.Forms.Button();
      this.cmdUndo = new System.Windows.Forms.Button();
      this.cmdSelectAll = new System.Windows.Forms.Button();
      this.cmdSelectNone = new System.Windows.Forms.Button();
      this.cmdShowAudioSyncWindow = new System.Windows.Forms.Button();
      this.cmdRedo = new System.Windows.Forms.Button();
      this.panForms = new System.Windows.Forms.Panel();
      this.cmdTonnetz = new System.Windows.Forms.Button();
      this.cmdMultiMap = new System.Windows.Forms.Button();
      this.panEdit = new System.Windows.Forms.Panel();
      this.cmdTransposeSelected = new System.Windows.Forms.Button();
      this.cmdPasteSpecial = new System.Windows.Forms.Button();
      this.cmdCut = new System.Windows.Forms.Button();
      this.panSelect = new System.Windows.Forms.Panel();
      this.panMisc = new System.Windows.Forms.Panel();
      this.cmdCalcKeys = new System.Windows.Forms.Button();
      this.cmdColours = new System.Windows.Forms.Button();
      this.cmdPlayMidiThis = new System.Windows.Forms.Button();
      this.cmdYPos = new System.Windows.Forms.Button();
      this.cmdYNeg = new System.Windows.Forms.Button();
      this.cmdXPos = new System.Windows.Forms.Button();
      this.cmdXNeg = new System.Windows.Forms.Button();
      this.chkOneOct = new System.Windows.Forms.CheckBox();
      this.lblSnapTo = new System.Windows.Forms.Label();
      this.cmbSnapTo = new System.Windows.Forms.ComboBox();
      this.cmdTransposeChordNotesAndKeysNeg = new System.Windows.Forms.Button();
      this.cmdTransposeChordNotesAndKeysPos = new System.Windows.Forms.Button();
      this.lblTransposeChordNotesAndKeys = new System.Windows.Forms.Label();
      this.lblTransposeKeys = new System.Windows.Forms.Label();
      this.cmdTransposeKeysNeg = new System.Windows.Forms.Button();
      this.cmdTransposeKeysPos = new System.Windows.Forms.Button();
      this.grpTransposeNotes = new System.Windows.Forms.GroupBox();
      this.lblTransposeChordNotes = new System.Windows.Forms.Label();
      this.cmdTransposeChordNotesPos = new System.Windows.Forms.Button();
      this.cmdTransposeChordNotesNeg = new System.Windows.Forms.Button();
      this.grpTransposeDisplay = new System.Windows.Forms.GroupBox();
      this.cmdTest = new System.Windows.Forms.Button();
      this.panMain = new ChordCadenza.PanelNoScrollOnFocus();
      this.lblKeyNamesLit = new System.Windows.Forms.Label();
      this.lblBarsLit = new System.Windows.Forms.Label();
      this.lblChordNamesLit = new System.Windows.Forms.Label();
      this.picBars = new System.Windows.Forms.PictureBox();
      this.lblMarginLit = new System.Windows.Forms.Label();
      this.picMargins = new System.Windows.Forms.PictureBox();
      this.lblLyricsLit = new System.Windows.Forms.Label();
      this.dgvLyrics = new System.Windows.Forms.DataGridView();
      this.lblTSigLit = new System.Windows.Forms.Label();
      this.lblChordsLit = new System.Windows.Forms.Label();
      this.lblMidiLit = new System.Windows.Forms.Label();
      this.lblKeyNotesLit = new System.Windows.Forms.Label();
      this.DGV = new System.Windows.Forms.DataGridView();
      this.panNoteMap = new ChordCadenza.PanelNoScrollOnFocus();
      this.picModNames = new System.Windows.Forms.PictureBox();
      this.picTSig = new System.Windows.Forms.PictureBox();
      this.picModNotes = new System.Windows.Forms.PictureBox();
      this.picNoteMapMidi = new System.Windows.Forms.PictureBox();
      this.picNoteMapFile = new System.Windows.Forms.PictureBox();
      this.picNoteMapQuant = new System.Windows.Forms.PictureBox();
      this.panChords.SuspendLayout();
      this.grpChordsDiv.SuspendLayout();
      this.grpCopyOptions.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudTransposeChordNames)).BeginInit();
      this.mnuModHit.SuspendLayout();
      this.mnuModMiss.SuspendLayout();
      this.mnuTSig.SuspendLayout();
      this.panAdvanced.SuspendLayout();
      this.panFiles.SuspendLayout();
      this.panPlay.SuspendLayout();
      this.panForms.SuspendLayout();
      this.panEdit.SuspendLayout();
      this.panSelect.SuspendLayout();
      this.panMisc.SuspendLayout();
      this.grpTransposeNotes.SuspendLayout();
      this.grpTransposeDisplay.SuspendLayout();
      this.panMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picBars)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picMargins)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvLyrics)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
      this.panNoteMap.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picModNames)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picTSig)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picModNotes)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picNoteMapMidi)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picNoteMapFile)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picNoteMapQuant)).BeginInit();
      this.SuspendLayout();
      // 
      // chkShowBeats
      // 
      this.chkShowBeats.AutoSize = true;
      this.chkShowBeats.Checked = true;
      this.chkShowBeats.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkShowBeats.Location = new System.Drawing.Point(949, 106);
      this.chkShowBeats.Name = "chkShowBeats";
      this.chkShowBeats.Size = new System.Drawing.Size(83, 17);
      this.chkShowBeats.TabIndex = 13;
      this.chkShowBeats.Text = "Show Beats";
      this.chkShowBeats.UseVisualStyleBackColor = true;
      this.chkShowBeats.CheckedChanged += new System.EventHandler(this.chkShowBeats_CheckedChanged);
      // 
      // panChords
      // 
      this.panChords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panChords.Controls.Add(this.lblTracksSelected);
      this.panChords.Controls.Add(this.cmdExec);
      this.panChords.Controls.Add(this.grpChordsDiv);
      this.panChords.Location = new System.Drawing.Point(906, 6);
      this.panChords.Name = "panChords";
      this.panChords.Size = new System.Drawing.Size(140, 75);
      this.panChords.TabIndex = 28;
      // 
      // lblTracksSelected
      // 
      this.lblTracksSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTracksSelected.Location = new System.Drawing.Point(63, 42);
      this.lblTracksSelected.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.lblTracksSelected.Name = "lblTracksSelected";
      this.lblTracksSelected.Size = new System.Drawing.Size(72, 27);
      this.lblTracksSelected.TabIndex = 104;
      this.lblTracksSelected.Text = "??? Tracks\r\nSelected";
      this.lblTracksSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // cmdExec
      // 
      this.cmdExec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdExec.Location = new System.Drawing.Point(66, 2);
      this.cmdExec.Name = "cmdExec";
      this.cmdExec.Size = new System.Drawing.Size(69, 37);
      this.cmdExec.TabIndex = 97;
      this.cmdExec.Text = "Generate\r\nChords";
      this.cmdExec.UseVisualStyleBackColor = true;
      this.cmdExec.Click += new System.EventHandler(this.cmdExec_Click);
      // 
      // grpChordsDiv
      // 
      this.grpChordsDiv.Controls.Add(this.optChordsAuto);
      this.grpChordsDiv.Controls.Add(this.optChordsBeat);
      this.grpChordsDiv.Controls.Add(this.optChordsBar);
      this.grpChordsDiv.Controls.Add(this.optChordsHalfBar);
      this.grpChordsDiv.Location = new System.Drawing.Point(4, -3);
      this.grpChordsDiv.Name = "grpChordsDiv";
      this.grpChordsDiv.Size = new System.Drawing.Size(61, 75);
      this.grpChordsDiv.TabIndex = 98;
      this.grpChordsDiv.TabStop = false;
      // 
      // optChordsAuto
      // 
      this.optChordsAuto.AutoSize = true;
      this.optChordsAuto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.optChordsAuto.Checked = true;
      this.optChordsAuto.Location = new System.Drawing.Point(4, 56);
      this.optChordsAuto.Name = "optChordsAuto";
      this.optChordsAuto.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.optChordsAuto.Size = new System.Drawing.Size(47, 17);
      this.optChordsAuto.TabIndex = 58;
      this.optChordsAuto.TabStop = true;
      this.optChordsAuto.Text = "Auto";
      this.optChordsAuto.UseVisualStyleBackColor = true;
      this.optChordsAuto.CheckedChanged += new System.EventHandler(this.optChordAlign_CheckedChanged);
      // 
      // optChordsBeat
      // 
      this.optChordsBeat.AutoSize = true;
      this.optChordsBeat.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.optChordsBeat.Location = new System.Drawing.Point(4, 40);
      this.optChordsBeat.Name = "optChordsBeat";
      this.optChordsBeat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.optChordsBeat.Size = new System.Drawing.Size(47, 17);
      this.optChordsBeat.TabIndex = 56;
      this.optChordsBeat.Text = "Beat";
      this.optChordsBeat.UseVisualStyleBackColor = true;
      this.optChordsBeat.CheckedChanged += new System.EventHandler(this.optChordAlign_CheckedChanged);
      // 
      // optChordsBar
      // 
      this.optChordsBar.AutoSize = true;
      this.optChordsBar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.optChordsBar.Location = new System.Drawing.Point(4, 8);
      this.optChordsBar.Name = "optChordsBar";
      this.optChordsBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.optChordsBar.Size = new System.Drawing.Size(41, 17);
      this.optChordsBar.TabIndex = 54;
      this.optChordsBar.Text = "Bar";
      this.optChordsBar.UseVisualStyleBackColor = true;
      this.optChordsBar.CheckedChanged += new System.EventHandler(this.optChordAlign_CheckedChanged);
      // 
      // optChordsHalfBar
      // 
      this.optChordsHalfBar.AutoSize = true;
      this.optChordsHalfBar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.optChordsHalfBar.Location = new System.Drawing.Point(4, 24);
      this.optChordsHalfBar.Name = "optChordsHalfBar";
      this.optChordsHalfBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.optChordsHalfBar.Size = new System.Drawing.Size(52, 17);
      this.optChordsHalfBar.TabIndex = 55;
      this.optChordsHalfBar.Text = "Bar/2";
      this.optChordsHalfBar.UseVisualStyleBackColor = true;
      this.optChordsHalfBar.CheckedChanged += new System.EventHandler(this.optChordAlign_CheckedChanged);
      // 
      // cmdConfigChords
      // 
      this.cmdConfigChords.ForeColor = System.Drawing.Color.Red;
      this.cmdConfigChords.Location = new System.Drawing.Point(3, 0);
      this.cmdConfigChords.Name = "cmdConfigChords";
      this.cmdConfigChords.Size = new System.Drawing.Size(70, 40);
      this.cmdConfigChords.TabIndex = 98;
      this.cmdConfigChords.Text = "Advanced\r\nConfig";
      this.cmdConfigChords.UseVisualStyleBackColor = true;
      this.cmdConfigChords.Click += new System.EventHandler(this.cmdConfigChords_Click);
      // 
      // cmdPausePlay
      // 
      this.cmdPausePlay.Enabled = false;
      this.cmdPausePlay.Location = new System.Drawing.Point(300, 0);
      this.cmdPausePlay.Name = "cmdPausePlay";
      this.cmdPausePlay.Size = new System.Drawing.Size(50, 48);
      this.cmdPausePlay.TabIndex = 43;
      this.cmdPausePlay.Text = "Pause Play";
      this.cmdPausePlay.UseVisualStyleBackColor = true;
      this.cmdPausePlay.Click += new System.EventHandler(this.cmdPausePlay_Click);
      // 
      // cmdPlayMidi
      // 
      this.cmdPlayMidi.Location = new System.Drawing.Point(50, 0);
      this.cmdPlayMidi.Name = "cmdPlayMidi";
      this.cmdPlayMidi.Size = new System.Drawing.Size(50, 48);
      this.cmdPlayMidi.TabIndex = 45;
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
      this.cmdStopPlay.TabIndex = 47;
      this.cmdStopPlay.Text = "Stop\r\nPlay";
      this.cmdStopPlay.UseVisualStyleBackColor = true;
      this.cmdStopPlay.Click += new System.EventHandler(this.cmdStopPlay_Click);
      // 
      // optCopyToEmpty
      // 
      this.optCopyToEmpty.AutoSize = true;
      this.optCopyToEmpty.Location = new System.Drawing.Point(16, 48);
      this.optCopyToEmpty.Name = "optCopyToEmpty";
      this.optCopyToEmpty.Size = new System.Drawing.Size(70, 17);
      this.optCopyToEmpty.TabIndex = 2;
      this.optCopyToEmpty.Text = "To Empty";
      this.optCopyToEmpty.UseVisualStyleBackColor = true;
      // 
      // optMerge
      // 
      this.optMerge.AutoSize = true;
      this.optMerge.Location = new System.Drawing.Point(16, 32);
      this.optMerge.Name = "optMerge";
      this.optMerge.Size = new System.Drawing.Size(55, 17);
      this.optMerge.TabIndex = 1;
      this.optMerge.Text = "Merge";
      this.optMerge.UseVisualStyleBackColor = true;
      // 
      // optReplace
      // 
      this.optReplace.AutoSize = true;
      this.optReplace.Checked = true;
      this.optReplace.Location = new System.Drawing.Point(16, 16);
      this.optReplace.Name = "optReplace";
      this.optReplace.Size = new System.Drawing.Size(65, 17);
      this.optReplace.TabIndex = 0;
      this.optReplace.TabStop = true;
      this.optReplace.Text = "Replace";
      this.optReplace.UseVisualStyleBackColor = true;
      // 
      // grpCopyOptions
      // 
      this.grpCopyOptions.Controls.Add(this.optCopyToEmpty);
      this.grpCopyOptions.Controls.Add(this.optReplace);
      this.grpCopyOptions.Controls.Add(this.optMerge);
      this.grpCopyOptions.ForeColor = System.Drawing.Color.Red;
      this.grpCopyOptions.Location = new System.Drawing.Point(172, 3);
      this.grpCopyOptions.Name = "grpCopyOptions";
      this.grpCopyOptions.Size = new System.Drawing.Size(87, 68);
      this.grpCopyOptions.TabIndex = 54;
      this.grpCopyOptions.TabStop = false;
      this.grpCopyOptions.Text = "Copy Options";
      // 
      // cmdPicQuantToFile
      // 
      this.cmdPicQuantToFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdPicQuantToFile.ForeColor = System.Drawing.Color.Red;
      this.cmdPicQuantToFile.Location = new System.Drawing.Point(3, 40);
      this.cmdPicQuantToFile.Name = "cmdPicQuantToFile";
      this.cmdPicQuantToFile.Size = new System.Drawing.Size(50, 34);
      this.cmdPicQuantToFile.TabIndex = 58;
      this.cmdPicQuantToFile.Text = "Copy \r\nMidi";
      this.cmdPicQuantToFile.UseVisualStyleBackColor = true;
      this.cmdPicQuantToFile.Click += new System.EventHandler(this.cmdPicQuantToFile_Click);
      // 
      // chkShowKB
      // 
      this.chkShowKB.AutoSize = true;
      this.chkShowKB.Checked = true;
      this.chkShowKB.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkShowKB.Location = new System.Drawing.Point(949, 123);
      this.chkShowKB.Name = "chkShowKB";
      this.chkShowKB.Size = new System.Drawing.Size(101, 17);
      this.chkShowKB.TabIndex = 80;
      this.chkShowKB.Text = "Show Keyboard";
      this.chkShowKB.UseVisualStyleBackColor = true;
      this.chkShowKB.CheckedChanged += new System.EventHandler(this.chkShowKB_CheckedChanged);
      // 
      // cmdGoToStart
      // 
      this.cmdGoToStart.Location = new System.Drawing.Point(0, 0);
      this.cmdGoToStart.Name = "cmdGoToStart";
      this.cmdGoToStart.Size = new System.Drawing.Size(50, 48);
      this.cmdGoToStart.TabIndex = 81;
      this.cmdGoToStart.Text = "GoTo Start";
      this.cmdGoToStart.UseVisualStyleBackColor = true;
      this.cmdGoToStart.Click += new System.EventHandler(this.cmdGoToStart_Click_1);
      // 
      // nudTransposeChordNames
      // 
      this.nudTransposeChordNames.Location = new System.Drawing.Point(89, 16);
      this.nudTransposeChordNames.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
      this.nudTransposeChordNames.Minimum = new decimal(new int[] {
            11,
            0,
            0,
            -2147483648});
      this.nudTransposeChordNames.Name = "nudTransposeChordNames";
      this.nudTransposeChordNames.Size = new System.Drawing.Size(36, 20);
      this.nudTransposeChordNames.TabIndex = 90;
      this.nudTransposeChordNames.ValueChanged += new System.EventHandler(this.nudTransposeChordNames_ValueChanged);
      // 
      // cmdAdvanced
      // 
      this.cmdAdvanced.ForeColor = System.Drawing.Color.Red;
      this.cmdAdvanced.Location = new System.Drawing.Point(79, 0);
      this.cmdAdvanced.Name = "cmdAdvanced";
      this.cmdAdvanced.Size = new System.Drawing.Size(56, 25);
      this.cmdAdvanced.TabIndex = 98;
      this.cmdAdvanced.Text = "Adv\'ced";
      this.cmdAdvanced.UseVisualStyleBackColor = true;
      this.cmdAdvanced.Click += new System.EventHandler(this.cmdAdvanced_Click);
      // 
      // mnuModHit
      // 
      this.mnuModHit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuModHitRemove,
            this.mnuModHitChange});
      this.mnuModHit.Name = "mnuModHit";
      this.mnuModHit.Size = new System.Drawing.Size(140, 48);
      // 
      // mnuModHitRemove
      // 
      this.mnuModHitRemove.Name = "mnuModHitRemove";
      this.mnuModHitRemove.Size = new System.Drawing.Size(139, 22);
      this.mnuModHitRemove.Text = "Remove Key";
      this.mnuModHitRemove.Click += new System.EventHandler(this.mnuModHitRemove_Click);
      // 
      // mnuModHitChange
      // 
      this.mnuModHitChange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuModHitcmbChange});
      this.mnuModHitChange.Name = "mnuModHitChange";
      this.mnuModHitChange.Size = new System.Drawing.Size(139, 22);
      this.mnuModHitChange.Text = "Change Key";
      // 
      // mnuModHitcmbChange
      // 
      this.mnuModHitcmbChange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.mnuModHitcmbChange.Name = "mnuModHitcmbChange";
      this.mnuModHitcmbChange.Size = new System.Drawing.Size(121, 23);
      this.mnuModHitcmbChange.SelectedIndexChanged += new System.EventHandler(this.mnuModHitcmbChange_SelectedIndexChanged);
      // 
      // mnuModMiss
      // 
      this.mnuModMiss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuModMissNew});
      this.mnuModMiss.Name = "mnuModNoHit";
      this.mnuModMiss.Size = new System.Drawing.Size(121, 26);
      // 
      // mnuModMissNew
      // 
      this.mnuModMissNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuModMisscmbNew});
      this.mnuModMissNew.Name = "mnuModMissNew";
      this.mnuModMissNew.Size = new System.Drawing.Size(120, 22);
      this.mnuModMissNew.Text = "New Key";
      // 
      // mnuModMisscmbNew
      // 
      this.mnuModMisscmbNew.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.mnuModMisscmbNew.Name = "mnuModMisscmbNew";
      this.mnuModMisscmbNew.Size = new System.Drawing.Size(121, 23);
      this.mnuModMisscmbNew.SelectedIndexChanged += new System.EventHandler(this.mnuModMisscmbNew_SelectedIndexChanged);
      // 
      // chkShowQuant
      // 
      this.chkShowQuant.AutoSize = true;
      this.chkShowQuant.ForeColor = System.Drawing.Color.Red;
      this.chkShowQuant.Location = new System.Drawing.Point(59, 41);
      this.chkShowQuant.Name = "chkShowQuant";
      this.chkShowQuant.Size = new System.Drawing.Size(107, 30);
      this.chkShowQuant.TabIndex = 101;
      this.chkShowQuant.Text = "Show Quantized \r\nMidi Notes";
      this.chkShowQuant.UseVisualStyleBackColor = true;
      this.chkShowQuant.CheckedChanged += new System.EventHandler(this.chkShowQuant_CheckedChanged);
      // 
      // mnuTSig
      // 
      this.mnuTSig.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTSigEnd,
            this.mnuTSigAreaAny,
            this.mnuTSigRemove,
            this.mnuTSigInsert,
            this.mnuChangeLength,
            this.toolStripSeparator1,
            this.mnuTSigHelp});
      this.mnuTSig.Name = "mnuModHit";
      this.mnuTSig.Size = new System.Drawing.Size(252, 164);
      // 
      // mnuTSigEnd
      // 
      this.mnuTSigEnd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTSigEndCommon,
            this.mnuTSigEndAny});
      this.mnuTSigEnd.Name = "mnuTSigEnd";
      this.mnuTSigEnd.Size = new System.Drawing.Size(251, 22);
      this.mnuTSigEnd.Text = "Set TSig to End";
      // 
      // mnuTSigEndCommon
      // 
      this.mnuTSigEndCommon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnucmbTSigEndCommon});
      this.mnuTSigEndCommon.Name = "mnuTSigEndCommon";
      this.mnuTSigEndCommon.Size = new System.Drawing.Size(152, 22);
      this.mnuTSigEndCommon.Text = "Common";
      // 
      // mnucmbTSigEndCommon
      // 
      this.mnucmbTSigEndCommon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.mnucmbTSigEndCommon.Name = "mnucmbTSigEndCommon";
      this.mnucmbTSigEndCommon.Size = new System.Drawing.Size(121, 23);
      this.mnucmbTSigEndCommon.SelectedIndexChanged += new System.EventHandler(this.mnucmbTSigEnd_SelectedIndexChanged);
      // 
      // mnuTSigEndAny
      // 
      this.mnuTSigEndAny.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnucmbTSigEndAny});
      this.mnuTSigEndAny.Name = "mnuTSigEndAny";
      this.mnuTSigEndAny.Size = new System.Drawing.Size(152, 22);
      this.mnuTSigEndAny.Text = "Any";
      // 
      // mnucmbTSigEndAny
      // 
      this.mnucmbTSigEndAny.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.mnucmbTSigEndAny.Name = "mnucmbTSigEndAny";
      this.mnucmbTSigEndAny.Size = new System.Drawing.Size(121, 23);
      this.mnucmbTSigEndAny.SelectedIndexChanged += new System.EventHandler(this.mnucmbTSigEnd_SelectedIndexChanged);
      // 
      // mnuTSigAreaAny
      // 
      this.mnuTSigAreaAny.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commonToolStripMenuItem,
            this.anyToolStripMenuItem});
      this.mnuTSigAreaAny.Name = "mnuTSigAreaAny";
      this.mnuTSigAreaAny.Size = new System.Drawing.Size(251, 22);
      this.mnuTSigAreaAny.Text = "Change TSigs in Selected Area";
      // 
      // commonToolStripMenuItem
      // 
      this.commonToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnucmbTSigAreaCommon});
      this.commonToolStripMenuItem.Name = "commonToolStripMenuItem";
      this.commonToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.commonToolStripMenuItem.Text = "Common";
      // 
      // mnucmbTSigAreaCommon
      // 
      this.mnucmbTSigAreaCommon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.mnucmbTSigAreaCommon.Name = "mnucmbTSigAreaCommon";
      this.mnucmbTSigAreaCommon.Size = new System.Drawing.Size(121, 23);
      this.mnucmbTSigAreaCommon.SelectedIndexChanged += new System.EventHandler(this.mnucmbTSigArea_SelectedIndexChanged);
      // 
      // anyToolStripMenuItem
      // 
      this.anyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnucmbTSigAreaAny});
      this.anyToolStripMenuItem.Name = "anyToolStripMenuItem";
      this.anyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.anyToolStripMenuItem.Text = "Any";
      // 
      // mnucmbTSigAreaAny
      // 
      this.mnucmbTSigAreaAny.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.mnucmbTSigAreaAny.Name = "mnucmbTSigAreaAny";
      this.mnucmbTSigAreaAny.Size = new System.Drawing.Size(121, 23);
      this.mnucmbTSigAreaAny.SelectedIndexChanged += new System.EventHandler(this.mnucmbTSigArea_SelectedIndexChanged);
      // 
      // mnuTSigRemove
      // 
      this.mnuTSigRemove.Name = "mnuTSigRemove";
      this.mnuTSigRemove.Size = new System.Drawing.Size(251, 22);
      this.mnuTSigRemove.Text = "Remove TSigs from Selected Area";
      this.mnuTSigRemove.Click += new System.EventHandler(this.mnuTSigRemove_Click);
      // 
      // mnuTSigInsert
      // 
      this.mnuTSigInsert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTSigInsertCommon,
            this.mnuTSigInsertAny});
      this.mnuTSigInsert.Name = "mnuTSigInsert";
      this.mnuTSigInsert.Size = new System.Drawing.Size(251, 22);
      this.mnuTSigInsert.Text = "Insert TSig in Selected Area";
      // 
      // mnuTSigInsertCommon
      // 
      this.mnuTSigInsertCommon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnucmbTSigInsertCommon});
      this.mnuTSigInsertCommon.Name = "mnuTSigInsertCommon";
      this.mnuTSigInsertCommon.Size = new System.Drawing.Size(152, 22);
      this.mnuTSigInsertCommon.Text = "Common";
      // 
      // mnucmbTSigInsertCommon
      // 
      this.mnucmbTSigInsertCommon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.mnucmbTSigInsertCommon.Name = "mnucmbTSigInsertCommon";
      this.mnucmbTSigInsertCommon.Size = new System.Drawing.Size(121, 23);
      this.mnucmbTSigInsertCommon.SelectedIndexChanged += new System.EventHandler(this.mnucmbTSigInsert_SelectedIndexChanged);
      // 
      // mnuTSigInsertAny
      // 
      this.mnuTSigInsertAny.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnucmbTSigInsertAny});
      this.mnuTSigInsertAny.Name = "mnuTSigInsertAny";
      this.mnuTSigInsertAny.Size = new System.Drawing.Size(152, 22);
      this.mnuTSigInsertAny.Text = "Any";
      // 
      // mnucmbTSigInsertAny
      // 
      this.mnucmbTSigInsertAny.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.mnucmbTSigInsertAny.Name = "mnucmbTSigInsertAny";
      this.mnucmbTSigInsertAny.Size = new System.Drawing.Size(121, 23);
      this.mnucmbTSigInsertAny.SelectedIndexChanged += new System.EventHandler(this.mnucmbTSigInsert_SelectedIndexChanged);
      // 
      // mnuChangeLength
      // 
      this.mnuChangeLength.Enabled = false;
      this.mnuChangeLength.Name = "mnuChangeLength";
      this.mnuChangeLength.Size = new System.Drawing.Size(251, 22);
      this.mnuChangeLength.Text = "Change Song Length";
      this.mnuChangeLength.Click += new System.EventHandler(this.mnuChangeLength_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(248, 6);
      // 
      // mnuTSigHelp
      // 
      this.mnuTSigHelp.Name = "mnuTSigHelp";
      this.mnuTSigHelp.Size = new System.Drawing.Size(251, 22);
      this.mnuTSigHelp.Text = "Help";
      this.mnuTSigHelp.Click += new System.EventHandler(this.mnuTSigHelp_Click);
      // 
      // cmdPlayAndRecordAudio
      // 
      this.cmdPlayAndRecordAudio.Location = new System.Drawing.Point(200, 0);
      this.cmdPlayAndRecordAudio.Name = "cmdPlayAndRecordAudio";
      this.cmdPlayAndRecordAudio.Size = new System.Drawing.Size(50, 48);
      this.cmdPlayAndRecordAudio.TabIndex = 117;
      this.cmdPlayAndRecordAudio.Text = "Play &&\r\nSync\r\nAudio";
      this.cmdPlayAndRecordAudio.UseVisualStyleBackColor = true;
      this.cmdPlayAndRecordAudio.Click += new System.EventHandler(this.cmdPlayAndRecordAudio_Click);
      // 
      // cmdPanic
      // 
      this.cmdPanic.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdPanic.Location = new System.Drawing.Point(350, 0);
      this.cmdPanic.Margin = new System.Windows.Forms.Padding(0);
      this.cmdPanic.Name = "cmdPanic";
      this.cmdPanic.Size = new System.Drawing.Size(50, 48);
      this.cmdPanic.TabIndex = 69;
      this.cmdPanic.Text = "!";
      this.cmdPanic.UseVisualStyleBackColor = true;
      this.cmdPanic.Click += new System.EventHandler(this.cmdPanic_Click);
      // 
      // cmdSyncAudio
      // 
      this.cmdSyncAudio.Location = new System.Drawing.Point(150, 0);
      this.cmdSyncAudio.Name = "cmdSyncAudio";
      this.cmdSyncAudio.Size = new System.Drawing.Size(50, 48);
      this.cmdSyncAudio.TabIndex = 113;
      this.cmdSyncAudio.Text = "Sync Audio";
      this.cmdSyncAudio.UseVisualStyleBackColor = true;
      this.cmdSyncAudio.Click += new System.EventHandler(this.cmdSyncAudio_Click);
      // 
      // cmdPlayAudio
      // 
      this.cmdPlayAudio.Location = new System.Drawing.Point(100, 0);
      this.cmdPlayAudio.Name = "cmdPlayAudio";
      this.cmdPlayAudio.Size = new System.Drawing.Size(50, 48);
      this.cmdPlayAudio.TabIndex = 110;
      this.cmdPlayAudio.Text = "Play\r\nAudio";
      this.cmdPlayAudio.UseVisualStyleBackColor = true;
      this.cmdPlayAudio.Click += new System.EventHandler(this.cmdPlayAudio_Click);
      // 
      // lblTransposeChordNames
      // 
      this.lblTransposeChordNames.AutoSize = true;
      this.lblTransposeChordNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTransposeChordNames.Location = new System.Drawing.Point(14, 19);
      this.lblTransposeChordNames.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.lblTransposeChordNames.Name = "lblTransposeChordNames";
      this.lblTransposeChordNames.Size = new System.Drawing.Size(71, 13);
      this.lblTransposeChordNames.TabIndex = 102;
      this.lblTransposeChordNames.Text = "Chord Names";
      this.lblTransposeChordNames.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // cmdHelp
      // 
      this.cmdHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdHelp.Location = new System.Drawing.Point(150, 0);
      this.cmdHelp.Margin = new System.Windows.Forms.Padding(0);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(50, 48);
      this.cmdHelp.TabIndex = 70;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // panAdvanced
      // 
      this.panAdvanced.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panAdvanced.Controls.Add(this.lblQI);
      this.panAdvanced.Controls.Add(this.chkOptChordsNone);
      this.panAdvanced.Controls.Add(this.lblLitQI);
      this.panAdvanced.Controls.Add(this.cmdChordinate);
      this.panAdvanced.Controls.Add(this.cmdConfigChords);
      this.panAdvanced.Controls.Add(this.lblTicks);
      this.panAdvanced.Controls.Add(this.cmdPicQuantToFile);
      this.panAdvanced.Controls.Add(this.lblLitTicks);
      this.panAdvanced.Controls.Add(this.cmdAdvanced);
      this.panAdvanced.Controls.Add(this.chkShowQuant);
      this.panAdvanced.Controls.Add(this.grpCopyOptions);
      this.panAdvanced.Location = new System.Drawing.Point(1190, 5);
      this.panAdvanced.Name = "panAdvanced";
      this.panAdvanced.Size = new System.Drawing.Size(268, 112);
      this.panAdvanced.TabIndex = 116;
      // 
      // lblQI
      // 
      this.lblQI.AutoSize = true;
      this.lblQI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblQI.ForeColor = System.Drawing.Color.Red;
      this.lblQI.Location = new System.Drawing.Point(36, 92);
      this.lblQI.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.lblQI.Name = "lblQI";
      this.lblQI.Size = new System.Drawing.Size(25, 13);
      this.lblQI.TabIndex = 236;
      this.lblQI.Text = "???";
      this.lblQI.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // chkOptChordsNone
      // 
      this.chkOptChordsNone.AutoSize = true;
      this.chkOptChordsNone.ForeColor = System.Drawing.Color.Red;
      this.chkOptChordsNone.Location = new System.Drawing.Point(79, 26);
      this.chkOptChordsNone.Name = "chkOptChordsNone";
      this.chkOptChordsNone.Size = new System.Drawing.Size(99, 17);
      this.chkOptChordsNone.TabIndex = 102;
      this.chkOptChordsNone.Text = "ChordDiv None";
      this.chkOptChordsNone.UseVisualStyleBackColor = true;
      this.chkOptChordsNone.CheckedChanged += new System.EventHandler(this.chkOptChordsNone_CheckedChanged);
      // 
      // lblLitQI
      // 
      this.lblLitQI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLitQI.ForeColor = System.Drawing.Color.Red;
      this.lblLitQI.Location = new System.Drawing.Point(1, 91);
      this.lblLitQI.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.lblLitQI.Name = "lblLitQI";
      this.lblLitQI.Size = new System.Drawing.Size(36, 13);
      this.lblLitQI.TabIndex = 235;
      this.lblLitQI.Text = "QI:";
      this.lblLitQI.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // cmdChordinate
      // 
      this.cmdChordinate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdChordinate.ForeColor = System.Drawing.Color.Red;
      this.cmdChordinate.Location = new System.Drawing.Point(85, 70);
      this.cmdChordinate.Name = "cmdChordinate";
      this.cmdChordinate.Size = new System.Drawing.Size(50, 34);
      this.cmdChordinate.TabIndex = 65;
      this.cmdChordinate.Text = "Check\r\nChords";
      this.cmdChordinate.UseVisualStyleBackColor = true;
      this.cmdChordinate.Click += new System.EventHandler(this.cmdChordinate_Click);
      // 
      // lblTicks
      // 
      this.lblTicks.AutoSize = true;
      this.lblTicks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTicks.ForeColor = System.Drawing.Color.Red;
      this.lblTicks.Location = new System.Drawing.Point(36, 78);
      this.lblTicks.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.lblTicks.Name = "lblTicks";
      this.lblTicks.Size = new System.Drawing.Size(25, 13);
      this.lblTicks.TabIndex = 234;
      this.lblTicks.Text = "???";
      this.lblTicks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblLitTicks
      // 
      this.lblLitTicks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLitTicks.ForeColor = System.Drawing.Color.Red;
      this.lblLitTicks.Location = new System.Drawing.Point(1, 77);
      this.lblLitTicks.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.lblLitTicks.Name = "lblLitTicks";
      this.lblLitTicks.Size = new System.Drawing.Size(33, 13);
      this.lblLitTicks.TabIndex = 233;
      this.lblLitTicks.Text = "Ticks:";
      this.lblLitTicks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblCHPDescLit
      // 
      this.lblCHPDescLit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lblCHPDescLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCHPDescLit.Location = new System.Drawing.Point(8, 121);
      this.lblCHPDescLit.Name = "lblCHPDescLit";
      this.lblCHPDescLit.Size = new System.Drawing.Size(58, 24);
      this.lblCHPDescLit.TabIndex = 117;
      this.lblCHPDescLit.Text = "Desc";
      this.lblCHPDescLit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // txtCHPDesc
      // 
      this.txtCHPDesc.CausesValidation = false;
      this.txtCHPDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtCHPDesc.Location = new System.Drawing.Point(70, 123);
      this.txtCHPDesc.Name = "txtCHPDesc";
      this.txtCHPDesc.Size = new System.Drawing.Size(549, 22);
      this.txtCHPDesc.TabIndex = 118;
      this.txtCHPDesc.TextChanged += new System.EventHandler(this.txtCHPDesc_TextChanged);
      // 
      // panFiles
      // 
      this.panFiles.Controls.Add(this.cmdNew);
      this.panFiles.Controls.Add(this.cmdLoadProject);
      this.panFiles.Controls.Add(this.cmdSaveProject);
      this.panFiles.Controls.Add(this.cmdSaveProjectAs);
      this.panFiles.Location = new System.Drawing.Point(5, 5);
      this.panFiles.Name = "panFiles";
      this.panFiles.Size = new System.Drawing.Size(208, 48);
      this.panFiles.TabIndex = 227;
      // 
      // cmdNew
      // 
      this.cmdNew.Location = new System.Drawing.Point(0, 0);
      this.cmdNew.Name = "cmdNew";
      this.cmdNew.Size = new System.Drawing.Size(50, 48);
      this.cmdNew.TabIndex = 237;
      this.cmdNew.Text = "New Project";
      this.cmdNew.UseVisualStyleBackColor = true;
      this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
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
      this.cmdSaveProject.Click += new System.EventHandler(this.cmdSaveProject_Click);
      this.cmdSaveProject.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cmdSaveProject_MouseUp);
      // 
      // cmdSaveProjectAs
      // 
      this.cmdSaveProjectAs.Location = new System.Drawing.Point(150, 0);
      this.cmdSaveProjectAs.Name = "cmdSaveProjectAs";
      this.cmdSaveProjectAs.Size = new System.Drawing.Size(50, 48);
      this.cmdSaveProjectAs.TabIndex = 244;
      this.cmdSaveProjectAs.Text = "Save\r\nProject\r\nAs...";
      this.cmdSaveProjectAs.UseVisualStyleBackColor = true;
      this.cmdSaveProjectAs.Click += new System.EventHandler(this.cmdSaveProjectAs_Click);
      // 
      // panPlay
      // 
      this.panPlay.Controls.Add(this.cmdGoToStart);
      this.panPlay.Controls.Add(this.cmdPlayMidi);
      this.panPlay.Controls.Add(this.cmdPlayAudio);
      this.panPlay.Controls.Add(this.cmdPlayAndRecordAudio);
      this.panPlay.Controls.Add(this.cmdSyncAudio);
      this.panPlay.Controls.Add(this.cmdStopPlay);
      this.panPlay.Controls.Add(this.cmdPanic);
      this.panPlay.Controls.Add(this.cmdPausePlay);
      this.panPlay.Location = new System.Drawing.Point(219, 5);
      this.panPlay.Name = "panPlay";
      this.panPlay.Size = new System.Drawing.Size(407, 48);
      this.panPlay.TabIndex = 228;
      // 
      // cmdSaveAs
      // 
      this.cmdSaveAs.Cursor = System.Windows.Forms.Cursors.Default;
      this.cmdSaveAs.Location = new System.Drawing.Point(0, 0);
      this.cmdSaveAs.Name = "cmdSaveAs";
      this.cmdSaveAs.Size = new System.Drawing.Size(50, 48);
      this.cmdSaveAs.TabIndex = 69;
      this.cmdSaveAs.Text = "Save\r\nChords\r\nAs...";
      this.cmdSaveAs.UseVisualStyleBackColor = true;
      this.cmdSaveAs.Click += new System.EventHandler(this.cmdSaveAs_Click);
      // 
      // cmdLoadMidi
      // 
      this.cmdLoadMidi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdLoadMidi.Location = new System.Drawing.Point(50, 0);
      this.cmdLoadMidi.Name = "cmdLoadMidi";
      this.cmdLoadMidi.Size = new System.Drawing.Size(50, 48);
      this.cmdLoadMidi.TabIndex = 68;
      this.cmdLoadMidi.Text = "Reload\r\nMidi\r\nTracks";
      this.cmdLoadMidi.UseVisualStyleBackColor = true;
      this.cmdLoadMidi.Click += new System.EventHandler(this.cmdLoadMidi_Click);
      // 
      // cmdShowSumm
      // 
      this.cmdShowSumm.Location = new System.Drawing.Point(100, 0);
      this.cmdShowSumm.Name = "cmdShowSumm";
      this.cmdShowSumm.Size = new System.Drawing.Size(50, 48);
      this.cmdShowSumm.TabIndex = 122;
      this.cmdShowSumm.Text = "Show\r\nSumm";
      this.cmdShowSumm.UseVisualStyleBackColor = true;
      this.cmdShowSumm.Click += new System.EventHandler(this.cmdShowSumm_Click);
      // 
      // cmdInsertBars
      // 
      this.cmdInsertBars.Location = new System.Drawing.Point(0, 0);
      this.cmdInsertBars.Name = "cmdInsertBars";
      this.cmdInsertBars.Size = new System.Drawing.Size(50, 48);
      this.cmdInsertBars.TabIndex = 120;
      this.cmdInsertBars.Text = "Insert\r\nBars";
      this.cmdInsertBars.UseVisualStyleBackColor = true;
      this.cmdInsertBars.Click += new System.EventHandler(this.cmdInsertBars_Click);
      // 
      // cmdDeleteBars
      // 
      this.cmdDeleteBars.Location = new System.Drawing.Point(50, 0);
      this.cmdDeleteBars.Name = "cmdDeleteBars";
      this.cmdDeleteBars.Size = new System.Drawing.Size(50, 48);
      this.cmdDeleteBars.TabIndex = 121;
      this.cmdDeleteBars.Text = "Delete\r\nBars";
      this.cmdDeleteBars.UseVisualStyleBackColor = true;
      this.cmdDeleteBars.Click += new System.EventHandler(this.cmdDeleteBars_Click);
      // 
      // cmdCopy
      // 
      this.cmdCopy.Location = new System.Drawing.Point(150, 0);
      this.cmdCopy.Name = "cmdCopy";
      this.cmdCopy.Size = new System.Drawing.Size(50, 48);
      this.cmdCopy.TabIndex = 115;
      this.cmdCopy.Text = "Copy";
      this.cmdCopy.UseVisualStyleBackColor = true;
      this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
      // 
      // cmdUpdateLyrics
      // 
      this.cmdUpdateLyrics.Location = new System.Drawing.Point(150, 0);
      this.cmdUpdateLyrics.Name = "cmdUpdateLyrics";
      this.cmdUpdateLyrics.Size = new System.Drawing.Size(50, 48);
      this.cmdUpdateLyrics.TabIndex = 119;
      this.cmdUpdateLyrics.Text = "Edit Lyrics";
      this.cmdUpdateLyrics.UseVisualStyleBackColor = true;
      this.cmdUpdateLyrics.Click += new System.EventHandler(this.cmdUpdateLyrics_Click);
      // 
      // cmdUndo
      // 
      this.cmdUndo.Enabled = false;
      this.cmdUndo.Location = new System.Drawing.Point(300, 0);
      this.cmdUndo.Name = "cmdUndo";
      this.cmdUndo.Size = new System.Drawing.Size(50, 48);
      this.cmdUndo.TabIndex = 51;
      this.cmdUndo.Text = "Undo";
      this.cmdUndo.UseVisualStyleBackColor = true;
      this.cmdUndo.Click += new System.EventHandler(this.cmdUndo_Click);
      // 
      // cmdSelectAll
      // 
      this.cmdSelectAll.Location = new System.Drawing.Point(100, 0);
      this.cmdSelectAll.Name = "cmdSelectAll";
      this.cmdSelectAll.Size = new System.Drawing.Size(50, 48);
      this.cmdSelectAll.TabIndex = 50;
      this.cmdSelectAll.Text = "Select All";
      this.cmdSelectAll.UseVisualStyleBackColor = true;
      this.cmdSelectAll.Click += new System.EventHandler(this.cmdSelectAll_Click);
      // 
      // cmdSelectNone
      // 
      this.cmdSelectNone.Location = new System.Drawing.Point(150, 0);
      this.cmdSelectNone.Name = "cmdSelectNone";
      this.cmdSelectNone.Size = new System.Drawing.Size(50, 48);
      this.cmdSelectNone.TabIndex = 67;
      this.cmdSelectNone.Text = "Select None";
      this.cmdSelectNone.UseVisualStyleBackColor = true;
      this.cmdSelectNone.Click += new System.EventHandler(this.cmdSelectNone_Click);
      // 
      // cmdShowAudioSyncWindow
      // 
      this.cmdShowAudioSyncWindow.Location = new System.Drawing.Point(50, 0);
      this.cmdShowAudioSyncWindow.Name = "cmdShowAudioSyncWindow";
      this.cmdShowAudioSyncWindow.Size = new System.Drawing.Size(50, 48);
      this.cmdShowAudioSyncWindow.TabIndex = 114;
      this.cmdShowAudioSyncWindow.Text = "Audio\r\nSync\r\nConfig";
      this.cmdShowAudioSyncWindow.UseVisualStyleBackColor = true;
      this.cmdShowAudioSyncWindow.Click += new System.EventHandler(this.cmdShowAudioSyncWindow_Click);
      // 
      // cmdRedo
      // 
      this.cmdRedo.Enabled = false;
      this.cmdRedo.Location = new System.Drawing.Point(350, 0);
      this.cmdRedo.Name = "cmdRedo";
      this.cmdRedo.Size = new System.Drawing.Size(50, 48);
      this.cmdRedo.TabIndex = 52;
      this.cmdRedo.Text = "Redo";
      this.cmdRedo.UseVisualStyleBackColor = true;
      this.cmdRedo.Click += new System.EventHandler(this.cmdRedo_Click);
      // 
      // panForms
      // 
      this.panForms.Controls.Add(this.cmdTonnetz);
      this.panForms.Controls.Add(this.cmdShowSumm);
      this.panForms.Controls.Add(this.cmdMultiMap);
      this.panForms.Controls.Add(this.cmdShowAudioSyncWindow);
      this.panForms.Controls.Add(this.cmdUpdateLyrics);
      this.panForms.Location = new System.Drawing.Point(632, 5);
      this.panForms.Name = "panForms";
      this.panForms.Size = new System.Drawing.Size(262, 48);
      this.panForms.TabIndex = 229;
      // 
      // cmdTonnetz
      // 
      this.cmdTonnetz.ForeColor = System.Drawing.Color.Red;
      this.cmdTonnetz.Location = new System.Drawing.Point(200, 0);
      this.cmdTonnetz.Name = "cmdTonnetz";
      this.cmdTonnetz.Size = new System.Drawing.Size(54, 48);
      this.cmdTonnetz.TabIndex = 242;
      this.cmdTonnetz.Text = "Tonnetz";
      this.cmdTonnetz.UseVisualStyleBackColor = true;
      this.cmdTonnetz.Click += new System.EventHandler(this.cmdTonnetz_Click);
      // 
      // cmdMultiMap
      // 
      this.cmdMultiMap.Enabled = false;
      this.cmdMultiMap.Location = new System.Drawing.Point(0, 0);
      this.cmdMultiMap.Name = "cmdMultiMap";
      this.cmdMultiMap.Size = new System.Drawing.Size(50, 48);
      this.cmdMultiMap.TabIndex = 191;
      this.cmdMultiMap.Text = "Track Map";
      this.cmdMultiMap.UseVisualStyleBackColor = true;
      this.cmdMultiMap.Click += new System.EventHandler(this.cmdMultiMap_Click);
      // 
      // panEdit
      // 
      this.panEdit.Controls.Add(this.cmdTransposeSelected);
      this.panEdit.Controls.Add(this.cmdPasteSpecial);
      this.panEdit.Controls.Add(this.cmdCut);
      this.panEdit.Controls.Add(this.cmdUndo);
      this.panEdit.Controls.Add(this.cmdCopy);
      this.panEdit.Controls.Add(this.cmdDeleteBars);
      this.panEdit.Controls.Add(this.cmdRedo);
      this.panEdit.Controls.Add(this.cmdInsertBars);
      this.panEdit.Location = new System.Drawing.Point(219, 63);
      this.panEdit.Name = "panEdit";
      this.panEdit.Size = new System.Drawing.Size(407, 48);
      this.panEdit.TabIndex = 230;
      // 
      // cmdTransposeSelected
      // 
      this.cmdTransposeSelected.Location = new System.Drawing.Point(250, 0);
      this.cmdTransposeSelected.Name = "cmdTransposeSelected";
      this.cmdTransposeSelected.Size = new System.Drawing.Size(50, 48);
      this.cmdTransposeSelected.TabIndex = 126;
      this.cmdTransposeSelected.Text = "Trans\r\nSelect";
      this.cmdTransposeSelected.UseVisualStyleBackColor = true;
      this.cmdTransposeSelected.Click += new System.EventHandler(this.cmdTransposeSelected_Click);
      // 
      // cmdPasteSpecial
      // 
      this.cmdPasteSpecial.Location = new System.Drawing.Point(200, 0);
      this.cmdPasteSpecial.Name = "cmdPasteSpecial";
      this.cmdPasteSpecial.Size = new System.Drawing.Size(50, 48);
      this.cmdPasteSpecial.TabIndex = 125;
      this.cmdPasteSpecial.Text = "Paste";
      this.cmdPasteSpecial.UseVisualStyleBackColor = true;
      this.cmdPasteSpecial.Click += new System.EventHandler(this.cmdPasteSpecial_Click);
      // 
      // cmdCut
      // 
      this.cmdCut.Location = new System.Drawing.Point(100, 0);
      this.cmdCut.Name = "cmdCut";
      this.cmdCut.Size = new System.Drawing.Size(50, 48);
      this.cmdCut.TabIndex = 124;
      this.cmdCut.Text = "Cut";
      this.cmdCut.UseVisualStyleBackColor = true;
      this.cmdCut.Click += new System.EventHandler(this.cmdCut_Click);
      // 
      // panSelect
      // 
      this.panSelect.Controls.Add(this.cmdSaveAs);
      this.panSelect.Controls.Add(this.cmdSelectNone);
      this.panSelect.Controls.Add(this.cmdSelectAll);
      this.panSelect.Controls.Add(this.cmdLoadMidi);
      this.panSelect.Location = new System.Drawing.Point(5, 63);
      this.panSelect.Name = "panSelect";
      this.panSelect.Size = new System.Drawing.Size(208, 48);
      this.panSelect.TabIndex = 231;
      // 
      // panMisc
      // 
      this.panMisc.Controls.Add(this.cmdCalcKeys);
      this.panMisc.Controls.Add(this.cmdColours);
      this.panMisc.Controls.Add(this.cmdPlayMidiThis);
      this.panMisc.Controls.Add(this.cmdHelp);
      this.panMisc.Location = new System.Drawing.Point(632, 63);
      this.panMisc.Name = "panMisc";
      this.panMisc.Size = new System.Drawing.Size(207, 48);
      this.panMisc.TabIndex = 232;
      // 
      // cmdCalcKeys
      // 
      this.cmdCalcKeys.Location = new System.Drawing.Point(0, 0);
      this.cmdCalcKeys.Name = "cmdCalcKeys";
      this.cmdCalcKeys.Size = new System.Drawing.Size(50, 48);
      this.cmdCalcKeys.TabIndex = 73;
      this.cmdCalcKeys.Text = "Calc\r\nKeys";
      this.cmdCalcKeys.UseVisualStyleBackColor = true;
      this.cmdCalcKeys.Click += new System.EventHandler(this.cmdCalcKeys_Click);
      // 
      // cmdColours
      // 
      this.cmdColours.Location = new System.Drawing.Point(50, 0);
      this.cmdColours.Name = "cmdColours";
      this.cmdColours.Size = new System.Drawing.Size(50, 48);
      this.cmdColours.TabIndex = 72;
      this.cmdColours.Text = "Colours";
      this.cmdColours.UseVisualStyleBackColor = true;
      this.cmdColours.Click += new System.EventHandler(this.cmdColours_Click);
      // 
      // cmdPlayMidiThis
      // 
      this.cmdPlayMidiThis.Location = new System.Drawing.Point(100, 0);
      this.cmdPlayMidiThis.Name = "cmdPlayMidiThis";
      this.cmdPlayMidiThis.Size = new System.Drawing.Size(50, 48);
      this.cmdPlayMidiThis.TabIndex = 42;
      this.cmdPlayMidiThis.Text = "Play\r\nMidi\r\nThis";
      this.cmdPlayMidiThis.UseVisualStyleBackColor = true;
      this.cmdPlayMidiThis.Click += new System.EventHandler(this.cmdPlayMidiThis_Click);
      // 
      // cmdYPos
      // 
      this.cmdYPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdYPos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.cmdYPos.Location = new System.Drawing.Point(845, 63);
      this.cmdYPos.Name = "cmdYPos";
      this.cmdYPos.Size = new System.Drawing.Size(29, 34);
      this.cmdYPos.TabIndex = 1;
      this.cmdYPos.Text = "+";
      this.cmdYPos.UseVisualStyleBackColor = true;
      this.cmdYPos.Click += new System.EventHandler(this.cmdYPos_Click);
      // 
      // cmdYNeg
      // 
      this.cmdYNeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdYNeg.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.cmdYNeg.Location = new System.Drawing.Point(845, 98);
      this.cmdYNeg.Name = "cmdYNeg";
      this.cmdYNeg.Size = new System.Drawing.Size(29, 34);
      this.cmdYNeg.TabIndex = 2;
      this.cmdYNeg.Text = "-";
      this.cmdYNeg.UseVisualStyleBackColor = true;
      this.cmdYNeg.Click += new System.EventHandler(this.cmdYNeg_Click);
      // 
      // cmdXPos
      // 
      this.cmdXPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdXPos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.cmdXPos.Location = new System.Drawing.Point(880, 98);
      this.cmdXPos.Name = "cmdXPos";
      this.cmdXPos.Size = new System.Drawing.Size(29, 34);
      this.cmdXPos.TabIndex = 3;
      this.cmdXPos.Text = "+";
      this.cmdXPos.UseVisualStyleBackColor = true;
      this.cmdXPos.Click += new System.EventHandler(this.cmdXPos_Click);
      // 
      // cmdXNeg
      // 
      this.cmdXNeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdXNeg.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.cmdXNeg.Location = new System.Drawing.Point(911, 98);
      this.cmdXNeg.Name = "cmdXNeg";
      this.cmdXNeg.Size = new System.Drawing.Size(29, 34);
      this.cmdXNeg.TabIndex = 4;
      this.cmdXNeg.Text = "-";
      this.cmdXNeg.UseVisualStyleBackColor = true;
      this.cmdXNeg.Click += new System.EventHandler(this.cmdXNeg_Click);
      // 
      // chkOneOct
      // 
      this.chkOneOct.AutoSize = true;
      this.chkOneOct.Checked = true;
      this.chkOneOct.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkOneOct.Location = new System.Drawing.Point(949, 89);
      this.chkOneOct.Name = "chkOneOct";
      this.chkOneOct.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.chkOneOct.Size = new System.Drawing.Size(84, 17);
      this.chkOneOct.TabIndex = 233;
      this.chkOneOct.Text = "One Octave";
      this.chkOneOct.UseVisualStyleBackColor = true;
      this.chkOneOct.CheckedChanged += new System.EventHandler(this.chkOneOct_CheckedChanged);
      // 
      // lblSnapTo
      // 
      this.lblSnapTo.AutoSize = true;
      this.lblSnapTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblSnapTo.Location = new System.Drawing.Point(629, 128);
      this.lblSnapTo.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.lblSnapTo.Name = "lblSnapTo";
      this.lblSnapTo.Size = new System.Drawing.Size(48, 13);
      this.lblSnapTo.TabIndex = 234;
      this.lblSnapTo.Text = "Snap To";
      this.lblSnapTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // cmbSnapTo
      // 
      this.cmbSnapTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbSnapTo.FormattingEnabled = true;
      this.cmbSnapTo.Location = new System.Drawing.Point(679, 124);
      this.cmbSnapTo.Name = "cmbSnapTo";
      this.cmbSnapTo.Size = new System.Drawing.Size(78, 21);
      this.cmbSnapTo.TabIndex = 235;
      // 
      // cmdTransposeChordNotesAndKeysNeg
      // 
      this.cmdTransposeChordNotesAndKeysNeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdTransposeChordNotesAndKeysNeg.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.cmdTransposeChordNotesAndKeysNeg.Location = new System.Drawing.Point(102, 70);
      this.cmdTransposeChordNotesAndKeysNeg.Name = "cmdTransposeChordNotesAndKeysNeg";
      this.cmdTransposeChordNotesAndKeysNeg.Size = new System.Drawing.Size(18, 23);
      this.cmdTransposeChordNotesAndKeysNeg.TabIndex = 237;
      this.cmdTransposeChordNotesAndKeysNeg.Text = "-";
      this.cmdTransposeChordNotesAndKeysNeg.UseVisualStyleBackColor = true;
      this.cmdTransposeChordNotesAndKeysNeg.Click += new System.EventHandler(this.cmdTransposeChordNotesAndKeys_Click);
      // 
      // cmdTransposeChordNotesAndKeysPos
      // 
      this.cmdTransposeChordNotesAndKeysPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdTransposeChordNotesAndKeysPos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.cmdTransposeChordNotesAndKeysPos.Location = new System.Drawing.Point(80, 70);
      this.cmdTransposeChordNotesAndKeysPos.Name = "cmdTransposeChordNotesAndKeysPos";
      this.cmdTransposeChordNotesAndKeysPos.Size = new System.Drawing.Size(18, 23);
      this.cmdTransposeChordNotesAndKeysPos.TabIndex = 236;
      this.cmdTransposeChordNotesAndKeysPos.Text = "+";
      this.cmdTransposeChordNotesAndKeysPos.UseVisualStyleBackColor = true;
      this.cmdTransposeChordNotesAndKeysPos.Click += new System.EventHandler(this.cmdTransposeChordNotesAndKeys_Click);
      // 
      // lblTransposeChordNotesAndKeys
      // 
      this.lblTransposeChordNotesAndKeys.AutoSize = true;
      this.lblTransposeChordNotesAndKeys.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTransposeChordNotesAndKeys.Location = new System.Drawing.Point(15, 68);
      this.lblTransposeChordNotesAndKeys.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.lblTransposeChordNotesAndKeys.Name = "lblTransposeChordNotesAndKeys";
      this.lblTransposeChordNotesAndKeys.Size = new System.Drawing.Size(66, 26);
      this.lblTransposeChordNotesAndKeys.TabIndex = 238;
      this.lblTransposeChordNotesAndKeys.Text = "Chord Notes\r\nand Keys";
      this.lblTransposeChordNotesAndKeys.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblTransposeKeys
      // 
      this.lblTransposeKeys.AutoSize = true;
      this.lblTransposeKeys.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTransposeKeys.Location = new System.Drawing.Point(46, 49);
      this.lblTransposeKeys.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.lblTransposeKeys.Name = "lblTransposeKeys";
      this.lblTransposeKeys.Size = new System.Drawing.Size(30, 13);
      this.lblTransposeKeys.TabIndex = 241;
      this.lblTransposeKeys.Text = "Keys";
      this.lblTransposeKeys.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // cmdTransposeKeysNeg
      // 
      this.cmdTransposeKeysNeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdTransposeKeysNeg.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.cmdTransposeKeysNeg.Location = new System.Drawing.Point(102, 44);
      this.cmdTransposeKeysNeg.Name = "cmdTransposeKeysNeg";
      this.cmdTransposeKeysNeg.Size = new System.Drawing.Size(18, 23);
      this.cmdTransposeKeysNeg.TabIndex = 240;
      this.cmdTransposeKeysNeg.Text = "-";
      this.cmdTransposeKeysNeg.UseVisualStyleBackColor = true;
      this.cmdTransposeKeysNeg.Click += new System.EventHandler(this.cmdTransposeKeys_Click);
      // 
      // cmdTransposeKeysPos
      // 
      this.cmdTransposeKeysPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdTransposeKeysPos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.cmdTransposeKeysPos.Location = new System.Drawing.Point(80, 44);
      this.cmdTransposeKeysPos.Name = "cmdTransposeKeysPos";
      this.cmdTransposeKeysPos.Size = new System.Drawing.Size(18, 23);
      this.cmdTransposeKeysPos.TabIndex = 239;
      this.cmdTransposeKeysPos.Text = "+";
      this.cmdTransposeKeysPos.UseVisualStyleBackColor = true;
      this.cmdTransposeKeysPos.Click += new System.EventHandler(this.cmdTransposeKeys_Click);
      // 
      // grpTransposeNotes
      // 
      this.grpTransposeNotes.Controls.Add(this.lblTransposeChordNotes);
      this.grpTransposeNotes.Controls.Add(this.cmdTransposeChordNotesPos);
      this.grpTransposeNotes.Controls.Add(this.cmdTransposeChordNotesNeg);
      this.grpTransposeNotes.Controls.Add(this.lblTransposeChordNotesAndKeys);
      this.grpTransposeNotes.Controls.Add(this.cmdTransposeChordNotesAndKeysPos);
      this.grpTransposeNotes.Controls.Add(this.cmdTransposeChordNotesAndKeysNeg);
      this.grpTransposeNotes.Controls.Add(this.lblTransposeKeys);
      this.grpTransposeNotes.Controls.Add(this.cmdTransposeKeysPos);
      this.grpTransposeNotes.Controls.Add(this.cmdTransposeKeysNeg);
      this.grpTransposeNotes.Location = new System.Drawing.Point(1051, 51);
      this.grpTransposeNotes.Name = "grpTransposeNotes";
      this.grpTransposeNotes.Size = new System.Drawing.Size(129, 101);
      this.grpTransposeNotes.TabIndex = 242;
      this.grpTransposeNotes.TabStop = false;
      this.grpTransposeNotes.Text = "Transpose Notes";
      // 
      // lblTransposeChordNotes
      // 
      this.lblTransposeChordNotes.AutoSize = true;
      this.lblTransposeChordNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTransposeChordNotes.Location = new System.Drawing.Point(15, 23);
      this.lblTransposeChordNotes.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.lblTransposeChordNotes.Name = "lblTransposeChordNotes";
      this.lblTransposeChordNotes.Size = new System.Drawing.Size(66, 13);
      this.lblTransposeChordNotes.TabIndex = 244;
      this.lblTransposeChordNotes.Text = "Chord Notes";
      this.lblTransposeChordNotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // cmdTransposeChordNotesPos
      // 
      this.cmdTransposeChordNotesPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdTransposeChordNotesPos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.cmdTransposeChordNotesPos.Location = new System.Drawing.Point(80, 18);
      this.cmdTransposeChordNotesPos.Name = "cmdTransposeChordNotesPos";
      this.cmdTransposeChordNotesPos.Size = new System.Drawing.Size(18, 23);
      this.cmdTransposeChordNotesPos.TabIndex = 242;
      this.cmdTransposeChordNotesPos.Text = "+";
      this.cmdTransposeChordNotesPos.UseVisualStyleBackColor = true;
      this.cmdTransposeChordNotesPos.Click += new System.EventHandler(this.cmdTransposeChordNotes_Click);
      // 
      // cmdTransposeChordNotesNeg
      // 
      this.cmdTransposeChordNotesNeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdTransposeChordNotesNeg.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.cmdTransposeChordNotesNeg.Location = new System.Drawing.Point(102, 18);
      this.cmdTransposeChordNotesNeg.Name = "cmdTransposeChordNotesNeg";
      this.cmdTransposeChordNotesNeg.Size = new System.Drawing.Size(18, 23);
      this.cmdTransposeChordNotesNeg.TabIndex = 243;
      this.cmdTransposeChordNotesNeg.Text = "-";
      this.cmdTransposeChordNotesNeg.UseVisualStyleBackColor = true;
      this.cmdTransposeChordNotesNeg.Click += new System.EventHandler(this.cmdTransposeChordNotes_Click);
      // 
      // grpTransposeDisplay
      // 
      this.grpTransposeDisplay.Controls.Add(this.lblTransposeChordNames);
      this.grpTransposeDisplay.Controls.Add(this.nudTransposeChordNames);
      this.grpTransposeDisplay.Location = new System.Drawing.Point(1051, 4);
      this.grpTransposeDisplay.Name = "grpTransposeDisplay";
      this.grpTransposeDisplay.Size = new System.Drawing.Size(129, 41);
      this.grpTransposeDisplay.TabIndex = 243;
      this.grpTransposeDisplay.TabStop = false;
      this.grpTransposeDisplay.Text = "Transpose Display";
      // 
      // cmdTest
      // 
      this.cmdTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdTest.ForeColor = System.Drawing.Color.Red;
      this.cmdTest.Location = new System.Drawing.Point(782, 118);
      this.cmdTest.Margin = new System.Windows.Forms.Padding(0);
      this.cmdTest.Name = "cmdTest";
      this.cmdTest.Size = new System.Drawing.Size(50, 26);
      this.cmdTest.TabIndex = 244;
      this.cmdTest.Text = "Test";
      this.cmdTest.UseVisualStyleBackColor = true;
      this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
      // 
      // panMain
      // 
      this.panMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panMain.AutoScroll = true;
      this.panMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panMain.Controls.Add(this.lblKeyNamesLit);
      this.panMain.Controls.Add(this.lblBarsLit);
      this.panMain.Controls.Add(this.lblChordNamesLit);
      this.panMain.Controls.Add(this.picBars);
      this.panMain.Controls.Add(this.lblMarginLit);
      this.panMain.Controls.Add(this.picMargins);
      this.panMain.Controls.Add(this.lblLyricsLit);
      this.panMain.Controls.Add(this.dgvLyrics);
      this.panMain.Controls.Add(this.lblTSigLit);
      this.panMain.Controls.Add(this.lblChordsLit);
      this.panMain.Controls.Add(this.lblMidiLit);
      this.panMain.Controls.Add(this.lblKeyNotesLit);
      this.panMain.Controls.Add(this.DGV);
      this.panMain.Controls.Add(this.panNoteMap);
      this.panMain.Location = new System.Drawing.Point(5, 158);
      this.panMain.Name = "panMain";
      this.panMain.Size = new System.Drawing.Size(1187, 495);
      this.panMain.TabIndex = 121;
      // 
      // lblKeyNamesLit
      // 
      this.lblKeyNamesLit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lblKeyNamesLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblKeyNamesLit.Location = new System.Drawing.Point(-2, 2);
      this.lblKeyNamesLit.Name = "lblKeyNamesLit";
      this.lblKeyNamesLit.Size = new System.Drawing.Size(67, 35);
      this.lblKeyNamesLit.TabIndex = 116;
      this.lblKeyNamesLit.Text = "Key\r\n(Names)";
      this.lblKeyNamesLit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblBarsLit
      // 
      this.lblBarsLit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lblBarsLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblBarsLit.Location = new System.Drawing.Point(1, 299);
      this.lblBarsLit.Name = "lblBarsLit";
      this.lblBarsLit.Size = new System.Drawing.Size(60, 21);
      this.lblBarsLit.TabIndex = 115;
      this.lblBarsLit.Text = "Bar";
      this.lblBarsLit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblChordNamesLit
      // 
      this.lblChordNamesLit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lblChordNamesLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblChordNamesLit.Location = new System.Drawing.Point(3, 335);
      this.lblChordNamesLit.Name = "lblChordNamesLit";
      this.lblChordNamesLit.Size = new System.Drawing.Size(58, 33);
      this.lblChordNamesLit.TabIndex = 112;
      this.lblChordNamesLit.Text = "Chord\r\nNames";
      this.lblChordNamesLit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // picBars
      // 
      this.picBars.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.picBars.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picBars.Location = new System.Drawing.Point(65, 303);
      this.picBars.Name = "picBars";
      this.picBars.Size = new System.Drawing.Size(1117, 17);
      this.picBars.TabIndex = 111;
      this.picBars.TabStop = false;
      this.picBars.Paint += new System.Windows.Forms.PaintEventHandler(this.picBars_Paint);
      this.picBars.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBarsBottom_MouseClick);
      // 
      // lblMarginLit
      // 
      this.lblMarginLit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lblMarginLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMarginLit.Location = new System.Drawing.Point(5, 430);
      this.lblMarginLit.Name = "lblMarginLit";
      this.lblMarginLit.Size = new System.Drawing.Size(58, 24);
      this.lblMarginLit.TabIndex = 109;
      this.lblMarginLit.Text = "Margin";
      this.lblMarginLit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // picMargins
      // 
      this.picMargins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.picMargins.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picMargins.Location = new System.Drawing.Point(64, 435);
      this.picMargins.Name = "picMargins";
      this.picMargins.Size = new System.Drawing.Size(1118, 17);
      this.picMargins.TabIndex = 107;
      this.picMargins.TabStop = false;
      this.picMargins.Paint += new System.Windows.Forms.PaintEventHandler(this.picMargins_Paint);
      this.picMargins.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picMargins_MouseClick);
      // 
      // lblLyricsLit
      // 
      this.lblLyricsLit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lblLyricsLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLyricsLit.Location = new System.Drawing.Point(2, 391);
      this.lblLyricsLit.Name = "lblLyricsLit";
      this.lblLyricsLit.Size = new System.Drawing.Size(58, 24);
      this.lblLyricsLit.TabIndex = 106;
      this.lblLyricsLit.Text = "Lyrics";
      this.lblLyricsLit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
      this.dgvLyrics.Location = new System.Drawing.Point(65, 380);
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
      this.dgvLyrics.Size = new System.Drawing.Size(1117, 48);
      this.dgvLyrics.TabIndex = 105;
      this.dgvLyrics.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvLyrics_ColumnAdded);
      this.dgvLyrics.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvLyrics_Scroll);
      this.dgvLyrics.VisibleChanged += new System.EventHandler(this.dgvLyrics_VisibleChanged);
      // 
      // lblTSigLit
      // 
      this.lblTSigLit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lblTSigLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTSigLit.Location = new System.Drawing.Point(2, 254);
      this.lblTSigLit.Name = "lblTSigLit";
      this.lblTSigLit.Size = new System.Drawing.Size(58, 24);
      this.lblTSigLit.TabIndex = 104;
      this.lblTSigLit.Text = "TSig";
      this.lblTSigLit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblChordsLit
      // 
      this.lblChordsLit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lblChordsLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblChordsLit.Location = new System.Drawing.Point(2, 166);
      this.lblChordsLit.Name = "lblChordsLit";
      this.lblChordsLit.Size = new System.Drawing.Size(58, 35);
      this.lblChordsLit.TabIndex = 103;
      this.lblChordsLit.Text = "Chord\r\nNotes";
      this.lblChordsLit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblMidiLit
      // 
      this.lblMidiLit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lblMidiLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMidiLit.Location = new System.Drawing.Point(2, 50);
      this.lblMidiLit.Name = "lblMidiLit";
      this.lblMidiLit.Size = new System.Drawing.Size(58, 24);
      this.lblMidiLit.TabIndex = 102;
      this.lblMidiLit.Text = "Midi";
      this.lblMidiLit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblKeyNotesLit
      // 
      this.lblKeyNotesLit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lblKeyNotesLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblKeyNotesLit.Location = new System.Drawing.Point(1, 218);
      this.lblKeyNotesLit.Name = "lblKeyNotesLit";
      this.lblKeyNotesLit.Size = new System.Drawing.Size(59, 35);
      this.lblKeyNotesLit.TabIndex = 100;
      this.lblKeyNotesLit.Text = "Key\r\n(Notes)";
      this.lblKeyNotesLit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // DGV
      // 
      this.DGV.AllowUserToAddRows = false;
      this.DGV.AllowUserToDeleteRows = false;
      this.DGV.AllowUserToResizeColumns = false;
      this.DGV.AllowUserToResizeRows = false;
      this.DGV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.DGV.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
      this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.DGV.ColumnHeadersVisible = false;
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.DGV.DefaultCellStyle = dataGridViewCellStyle5;
      this.DGV.EnableHeadersVisualStyles = false;
      this.DGV.Location = new System.Drawing.Point(65, 326);
      this.DGV.MultiSelect = false;
      this.DGV.Name = "DGV";
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.DGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
      this.DGV.RowHeadersVisible = false;
      this.DGV.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
      this.DGV.Size = new System.Drawing.Size(1117, 48);
      this.DGV.TabIndex = 85;
      this.DGV.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DGV_ColumnAdded);
      this.DGV.Scroll += new System.Windows.Forms.ScrollEventHandler(this.DGV_Scroll);
      this.DGV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGV_KeyDown);
      // 
      // panNoteMap
      // 
      this.panNoteMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panNoteMap.AutoScroll = true;
      this.panNoteMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panNoteMap.Controls.Add(this.picModNames);
      this.panNoteMap.Controls.Add(this.picTSig);
      this.panNoteMap.Controls.Add(this.picModNotes);
      this.panNoteMap.Controls.Add(this.picNoteMapMidi);
      this.panNoteMap.Controls.Add(this.picNoteMapFile);
      this.panNoteMap.Controls.Add(this.picNoteMapQuant);
      this.panNoteMap.Location = new System.Drawing.Point(65, 3);
      this.panNoteMap.Name = "panNoteMap";
      this.panNoteMap.Size = new System.Drawing.Size(1117, 284);
      this.panNoteMap.TabIndex = 0;
      this.panNoteMap.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panNoteMap_Scroll);
      // 
      // picModNames
      // 
      this.picModNames.BackColor = System.Drawing.Color.White;
      this.picModNames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picModNames.Location = new System.Drawing.Point(-2, 3);
      this.picModNames.Name = "picModNames";
      this.picModNames.Size = new System.Drawing.Size(227, 24);
      this.picModNames.TabIndex = 5;
      this.picModNames.TabStop = false;
      this.picModNames.Paint += new System.Windows.Forms.PaintEventHandler(this.picMod_Paint);
      this.picModNames.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picMod_MouseClick);
      // 
      // picTSig
      // 
      this.picTSig.BackColor = System.Drawing.Color.White;
      this.picTSig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picTSig.Location = new System.Drawing.Point(-2, 251);
      this.picTSig.Name = "picTSig";
      this.picTSig.Size = new System.Drawing.Size(227, 24);
      this.picTSig.TabIndex = 4;
      this.picTSig.TabStop = false;
      this.picTSig.Click += new System.EventHandler(this.picTSig_Click);
      this.picTSig.Paint += new System.Windows.Forms.PaintEventHandler(this.picTSig_Paint);
      this.picTSig.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picTSig_MouseClick);
      // 
      // picModNotes
      // 
      this.picModNotes.BackColor = System.Drawing.Color.White;
      this.picModNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picModNotes.Location = new System.Drawing.Point(-2, 219);
      this.picModNotes.Name = "picModNotes";
      this.picModNotes.Size = new System.Drawing.Size(227, 24);
      this.picModNotes.TabIndex = 3;
      this.picModNotes.TabStop = false;
      this.picModNotes.Paint += new System.Windows.Forms.PaintEventHandler(this.picMod_Paint);
      this.picModNotes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picMod_MouseClick);
      // 
      // picNoteMapMidi
      // 
      this.picNoteMapMidi.BackColor = System.Drawing.Color.White;
      this.picNoteMapMidi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picNoteMapMidi.Location = new System.Drawing.Point(-2, 34);
      this.picNoteMapMidi.Name = "picNoteMapMidi";
      this.picNoteMapMidi.Size = new System.Drawing.Size(228, 48);
      this.picNoteMapMidi.TabIndex = 2;
      this.picNoteMapMidi.TabStop = false;
      this.picNoteMapMidi.Paint += new System.Windows.Forms.PaintEventHandler(this.picNoteMap_Paint);
      this.picNoteMapMidi.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picNoteMapMidi_MouseClick);
      // 
      // picNoteMapFile
      // 
      this.picNoteMapFile.BackColor = System.Drawing.Color.White;
      this.picNoteMapFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picNoteMapFile.Location = new System.Drawing.Point(-2, 157);
      this.picNoteMapFile.Name = "picNoteMapFile";
      this.picNoteMapFile.Size = new System.Drawing.Size(228, 43);
      this.picNoteMapFile.TabIndex = 1;
      this.picNoteMapFile.TabStop = false;
      this.picNoteMapFile.Paint += new System.Windows.Forms.PaintEventHandler(this.picNoteMap_Paint);
      this.picNoteMapFile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picNoteMapFile_MouseClick);
      // 
      // picNoteMapQuant
      // 
      this.picNoteMapQuant.BackColor = System.Drawing.Color.White;
      this.picNoteMapQuant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picNoteMapQuant.Location = new System.Drawing.Point(-2, 102);
      this.picNoteMapQuant.Name = "picNoteMapQuant";
      this.picNoteMapQuant.Size = new System.Drawing.Size(228, 48);
      this.picNoteMapQuant.TabIndex = 0;
      this.picNoteMapQuant.TabStop = false;
      this.picNoteMapQuant.Paint += new System.Windows.Forms.PaintEventHandler(this.picNoteMap_Paint);
      this.picNoteMapQuant.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picNoteMapQuant_MouseClick);
      // 
      // frmChordMap
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1204, 665);
      this.Controls.Add(this.cmdTest);
      this.Controls.Add(this.grpTransposeDisplay);
      this.Controls.Add(this.grpTransposeNotes);
      this.Controls.Add(this.cmbSnapTo);
      this.Controls.Add(this.lblSnapTo);
      this.Controls.Add(this.chkOneOct);
      this.Controls.Add(this.cmdYPos);
      this.Controls.Add(this.cmdXNeg);
      this.Controls.Add(this.cmdXPos);
      this.Controls.Add(this.cmdYNeg);
      this.Controls.Add(this.panMisc);
      this.Controls.Add(this.panSelect);
      this.Controls.Add(this.panEdit);
      this.Controls.Add(this.panForms);
      this.Controls.Add(this.panPlay);
      this.Controls.Add(this.panFiles);
      this.Controls.Add(this.panMain);
      this.Controls.Add(this.txtCHPDesc);
      this.Controls.Add(this.lblCHPDescLit);
      this.Controls.Add(this.chkShowKB);
      this.Controls.Add(this.chkShowBeats);
      this.Controls.Add(this.panAdvanced);
      this.Controls.Add(this.panChords);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.KeyPreview = true;
      this.Name = "frmChordMap";
      this.Text = "CHORDMAP";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNoteMap_FormClosing);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmNoteMap_FormClosed);
      this.Load += new System.EventHandler(this.frmNoteMap_Load);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmNoteMap_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmNoteMap_DragEnter);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmChordMap_KeyDown);
      this.Resize += new System.EventHandler(this.frmNoteMap_Resize);
      this.panChords.ResumeLayout(false);
      this.grpChordsDiv.ResumeLayout(false);
      this.grpChordsDiv.PerformLayout();
      this.grpCopyOptions.ResumeLayout(false);
      this.grpCopyOptions.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudTransposeChordNames)).EndInit();
      this.mnuModHit.ResumeLayout(false);
      this.mnuModMiss.ResumeLayout(false);
      this.mnuTSig.ResumeLayout(false);
      this.panAdvanced.ResumeLayout(false);
      this.panAdvanced.PerformLayout();
      this.panFiles.ResumeLayout(false);
      this.panPlay.ResumeLayout(false);
      this.panForms.ResumeLayout(false);
      this.panEdit.ResumeLayout(false);
      this.panSelect.ResumeLayout(false);
      this.panMisc.ResumeLayout(false);
      this.grpTransposeNotes.ResumeLayout(false);
      this.grpTransposeNotes.PerformLayout();
      this.grpTransposeDisplay.ResumeLayout(false);
      this.grpTransposeDisplay.PerformLayout();
      this.panMain.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.picBars)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picMargins)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvLyrics)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
      this.panNoteMap.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.picModNames)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picTSig)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picModNotes)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picNoteMapMidi)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picNoteMapFile)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picNoteMapQuant)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdYPos;
    private System.Windows.Forms.Button cmdYNeg;
    private System.Windows.Forms.Button cmdXNeg;
    private System.Windows.Forms.Button cmdXPos;
    private System.Windows.Forms.Panel panChords;
    private System.Windows.Forms.Button cmdPlayMidiThis;
    private System.Windows.Forms.Button cmdPausePlay;
    private System.Windows.Forms.Button cmdPlayMidi;
    private System.Windows.Forms.Button cmdStopPlay;
    private System.Windows.Forms.Button cmdSelectAll;
    private System.Windows.Forms.GroupBox grpCopyOptions;
    private System.Windows.Forms.RadioButton optCopyToEmpty;
    private System.Windows.Forms.RadioButton optMerge;
    private System.Windows.Forms.RadioButton optReplace;
    internal System.Windows.Forms.CheckBox chkShowBeats;
    private System.Windows.Forms.Button cmdPicQuantToFile;
    private System.Windows.Forms.Button cmdChordinate;
    internal System.Windows.Forms.Button cmdUndo;
    internal System.Windows.Forms.Button cmdRedo;
    private System.Windows.Forms.Button cmdLoadMidi;
    private System.Windows.Forms.Button cmdSelectNone;
    internal System.Windows.Forms.PictureBox picNoteMapMidi;
    internal System.Windows.Forms.CheckBox chkShowKB;
    private System.Windows.Forms.Button cmdGoToStart;
    internal System.Windows.Forms.PictureBox picNoteMapFile;
    //internal System.Windows.Forms.Panel panNoteMap;
    internal ChordCadenza.PanelNoScrollOnFocus panNoteMap;
    internal System.Windows.Forms.NumericUpDown nudTransposeChordNames;
    internal System.Windows.Forms.PictureBox picNoteMapQuant;
    private System.Windows.Forms.Button cmdColours;
    private System.Windows.Forms.RadioButton optChordsBeat;
    private System.Windows.Forms.RadioButton optChordsHalfBar;
    private System.Windows.Forms.RadioButton optChordsBar;
    private System.Windows.Forms.Button cmdExec;
    private System.Windows.Forms.GroupBox grpChordsDiv;
    internal System.Windows.Forms.DataGridView DGV;
    private System.Windows.Forms.Button cmdConfigChords;
    private System.Windows.Forms.RadioButton optChordsAuto;
    private System.Windows.Forms.Button cmdAdvanced;
    internal System.Windows.Forms.PictureBox picModNotes;
    private System.Windows.Forms.ContextMenuStrip mnuModHit;
    private System.Windows.Forms.ToolStripMenuItem mnuModHitRemove;
    private System.Windows.Forms.ContextMenuStrip mnuModMiss;
    private System.Windows.Forms.ToolStripMenuItem mnuModMissNew;
    private System.Windows.Forms.ToolStripMenuItem mnuModHitChange;
    private System.Windows.Forms.ToolStripComboBox mnuModHitcmbChange;
    private System.Windows.Forms.ToolStripComboBox mnuModMisscmbNew;
    private System.Windows.Forms.Label lblKeyNotesLit;
    private System.Windows.Forms.CheckBox chkShowQuant;
    private System.Windows.Forms.Label lblMidiLit;
    private System.Windows.Forms.Label lblChordsLit;
    internal System.Windows.Forms.PictureBox picTSig;
    private System.Windows.Forms.Label lblTSigLit;
    private System.Windows.Forms.ContextMenuStrip mnuTSig;
    private System.Windows.Forms.ToolStripMenuItem mnuTSigEnd;
    internal System.Windows.Forms.DataGridView dgvLyrics;
    private System.Windows.Forms.Label lblMarginLit;
    private System.Windows.Forms.Label lblTransposeChordNames;
    private System.Windows.Forms.Label lblChordNamesLit;
    internal System.Windows.Forms.PictureBox picBars;
    private System.Windows.Forms.Button cmdShowAudioSyncWindow;
    private System.Windows.Forms.Button cmdPanic;
    private System.Windows.Forms.Label lblBarsLit;
    private System.Windows.Forms.Button cmdHelp;
    private System.Windows.Forms.Panel panAdvanced;
    internal System.Windows.Forms.CheckBox chkOptChordsNone;
    private System.Windows.Forms.Label lblCHPDescLit;
    internal System.Windows.Forms.TextBox txtCHPDesc;
    private System.Windows.Forms.Button cmdSaveAs;
    private System.Windows.Forms.Button cmdUpdateLyrics;
    internal System.Windows.Forms.Label lblLyricsLit;
    internal System.Windows.Forms.Button cmdSyncAudio;
    internal System.Windows.Forms.Button cmdPlayAudio;
    internal System.Windows.Forms.Button cmdCopy;
    internal System.Windows.Forms.Button cmdPlayAndRecordAudio;
    //private System.Windows.Forms.Panel panMain;
    private ChordCadenza.PanelNoScrollOnFocus panMain;
    internal System.Windows.Forms.Button cmdInsertBars;
    internal System.Windows.Forms.Button cmdDeleteBars;
    private System.Windows.Forms.Button cmdShowSumm;
    private System.Windows.Forms.Panel panFiles;
    internal System.Windows.Forms.Button cmdNew;
    internal System.Windows.Forms.Button cmdLoadProject;
    internal System.Windows.Forms.Button cmdSaveProject;
    internal System.Windows.Forms.Button cmdSaveProjectAs;
    private System.Windows.Forms.Panel panPlay;
    private System.Windows.Forms.Panel panForms;
    internal System.Windows.Forms.Button cmdMultiMap;
    internal System.Windows.Forms.Button cmdTonnetz;
    private System.Windows.Forms.Panel panEdit;
    private System.Windows.Forms.Panel panSelect;
    private System.Windows.Forms.Panel panMisc;
    private System.Windows.Forms.Button cmdCalcKeys;
    private System.Windows.Forms.Label lblLitTicks;
    private System.Windows.Forms.Label lblTicks;
    private System.Windows.Forms.Label lblQI;
    private System.Windows.Forms.Label lblLitQI;
    internal System.Windows.Forms.PictureBox picMargins;
    internal System.Windows.Forms.Button cmdCut;
    internal System.Windows.Forms.Button cmdPasteSpecial;
    private System.Windows.Forms.ToolStripMenuItem mnuChangeLength;
    private System.Windows.Forms.Label lblTracksSelected;
    private System.Windows.Forms.ToolStripMenuItem mnuTSigAreaAny;
    private System.Windows.Forms.ToolStripMenuItem mnuTSigEndCommon;
    private System.Windows.Forms.ToolStripMenuItem mnuTSigEndAny;
    private System.Windows.Forms.ToolStripComboBox mnucmbTSigEndCommon;
    private System.Windows.Forms.ToolStripComboBox mnucmbTSigEndAny;
    private System.Windows.Forms.ToolStripMenuItem commonToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem anyToolStripMenuItem;
    private System.Windows.Forms.ToolStripComboBox mnucmbTSigAreaCommon;
    private System.Windows.Forms.ToolStripComboBox mnucmbTSigAreaAny;
    private System.Windows.Forms.ToolStripMenuItem mnuTSigRemove;
    private System.Windows.Forms.ToolStripMenuItem mnuTSigInsert;
    private System.Windows.Forms.ToolStripMenuItem mnuTSigInsertCommon;
    private System.Windows.Forms.ToolStripComboBox mnucmbTSigInsertCommon;
    private System.Windows.Forms.ToolStripMenuItem mnuTSigInsertAny;
    private System.Windows.Forms.ToolStripComboBox mnucmbTSigInsertAny;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem mnuTSigHelp;
    private System.Windows.Forms.Label lblSnapTo;
    private System.Windows.Forms.ComboBox cmbSnapTo;
    private System.Windows.Forms.Label lblKeyNamesLit;
    internal System.Windows.Forms.PictureBox picModNames;
    private System.Windows.Forms.Button cmdTransposeChordNotesAndKeysNeg;
    private System.Windows.Forms.Button cmdTransposeChordNotesAndKeysPos;
    private System.Windows.Forms.Label lblTransposeChordNotesAndKeys;
    private System.Windows.Forms.Label lblTransposeKeys;
    private System.Windows.Forms.Button cmdTransposeKeysNeg;
    private System.Windows.Forms.Button cmdTransposeKeysPos;
    private System.Windows.Forms.GroupBox grpTransposeNotes;
    private System.Windows.Forms.GroupBox grpTransposeDisplay;
    private System.Windows.Forms.Label lblTransposeChordNotes;
    private System.Windows.Forms.Button cmdTransposeChordNotesPos;
    private System.Windows.Forms.Button cmdTransposeChordNotesNeg;
    private System.Windows.Forms.Button cmdTest;
    internal System.Windows.Forms.CheckBox chkOneOct;
    internal System.Windows.Forms.Button cmdTransposeSelected;
  }
}