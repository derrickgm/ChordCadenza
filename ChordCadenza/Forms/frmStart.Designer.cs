namespace ChordCadenza.Forms {
  partial class frmStart {
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
        if (stFile != null) stFile.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.grpChordPlay = new System.Windows.Forms.GroupBox();
      this.chkConstantChordPlayPCKB = new System.Windows.Forms.CheckBox();
      this.chkConstantChordPlayMidiKB = new System.Windows.Forms.CheckBox();
      this.nudNotesPerChordPlay = new System.Windows.Forms.NumericUpDown();
      this.lblnudNotesPerChordPlay = new System.Windows.Forms.Label();
      this.chkConstantChordDisplay = new System.Windows.Forms.CheckBox();
      this.nudNotesPerChordDisplay = new System.Windows.Forms.NumericUpDown();
      this.lblnudNotesPerChordDisplay = new System.Windows.Forms.Label();
      this.grpMidi = new System.Windows.Forms.GroupBox();
      this.nudDefaultSongLength = new System.Windows.Forms.NumericUpDown();
      this.lblnudDefaultSongLength = new System.Windows.Forms.Label();
      this.nudDIdd = new System.Windows.Forms.NumericUpDown();
      this.lblnudDIdd = new System.Windows.Forms.Label();
      this.nudTPDI = new System.Windows.Forms.NumericUpDown();
      this.lblnudTPDI = new System.Windows.Forms.Label();
      this.lblnudMinChordSize = new System.Windows.Forms.Label();
      this.nudMinChordSize = new System.Windows.Forms.NumericUpDown();
      this.lblnudMaxChordSize = new System.Windows.Forms.Label();
      this.nudMaxChordSize = new System.Windows.Forms.NumericUpDown();
      this.lblReload = new System.Windows.Forms.Label();
      this.nudPlayResolution = new System.Windows.Forms.NumericUpDown();
      this.lblnudPlayResolution = new System.Windows.Forms.Label();
      this.grpMidiPos = new System.Windows.Forms.GroupBox();
      this.chkMidiStartStop = new System.Windows.Forms.CheckBox();
      this.chkMidiSPP = new System.Windows.Forms.CheckBox();
      this.grpTimers = new System.Windows.Forms.GroupBox();
      this.lbllitSyncopation = new System.Windows.Forms.Label();
      this.lblAutoSyncChordDelayMS = new System.Windows.Forms.Label();
      this.lblAutoSyncChordDelay = new System.Windows.Forms.Label();
      this.nudAutoSyncChordDelay = new System.Windows.Forms.NumericUpDown();
      this.lblnudSyncopation = new System.Windows.Forms.Label();
      this.lblnudSyncopationNote = new System.Windows.Forms.Label();
      this.lblnudSyncopationSlash = new System.Windows.Forms.Label();
      this.nudSyncopationNN = new System.Windows.Forms.NumericUpDown();
      this.nudSyncopationDD = new System.Windows.Forms.NumericUpDown();
      this.lblnudPlayResolutionMS = new System.Windows.Forms.Label();
      this.lblnudTimerSustainMS = new System.Windows.Forms.Label();
      this.lblnudTimerSustain = new System.Windows.Forms.Label();
      this.nudTimerSustain = new System.Windows.Forms.NumericUpDown();
      this.grpMisc = new System.Windows.Forms.GroupBox();
      this.nudUndoRedoCapacity = new System.Windows.Forms.NumericUpDown();
      this.lblUndoRedoCapacity = new System.Windows.Forms.Label();
      this.chkExitPrompt = new System.Windows.Forms.CheckBox();
      this.chkTTActive = new System.Windows.Forms.CheckBox();
      this.chkAllNotesOffAfterSustain = new System.Windows.Forms.CheckBox();
      this.chkNoAudioSync = new System.Windows.Forms.CheckBox();
      this.chkLoadMM = new System.Windows.Forms.CheckBox();
      this.chkIgnoreNullChords = new System.Windows.Forms.CheckBox();
      this.cmdResetPlay = new System.Windows.Forms.Button();
      this.grpChordSetMinorKey = new System.Windows.Forms.GroupBox();
      this.optMinorSpecial = new System.Windows.Forms.RadioButton();
      this.optMinorMelodicDown = new System.Windows.Forms.RadioButton();
      this.optMinorMelodicUp = new System.Windows.Forms.RadioButton();
      this.optMinorHarmonic = new System.Windows.Forms.RadioButton();
      this.ofd = new System.Windows.Forms.OpenFileDialog();
      this.sfd = new System.Windows.Forms.SaveFileDialog();
      this.fbd = new System.Windows.Forms.FolderBrowserDialog();
      this.grpMidiPlayAdvanced = new System.Windows.Forms.GroupBox();
      this.chkFilterMidiBank = new System.Windows.Forms.CheckBox();
      this.chkKBChordMatch = new System.Windows.Forms.CheckBox();
      this.chkAutoRecChan = new System.Windows.Forms.CheckBox();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.grpFiles = new System.Windows.Forms.GroupBox();
      this.lblAudLoad = new System.Windows.Forms.Label();
      this.lblLyrLoad = new System.Windows.Forms.Label();
      this.lblChtLoad = new System.Windows.Forms.Label();
      this.lblMidLoad = new System.Windows.Forms.Label();
      this.lblChpLoad = new System.Windows.Forms.Label();
      this.lblAudLoadLit = new System.Windows.Forms.Label();
      this.lblLyrLoadLit = new System.Windows.Forms.Label();
      this.lblChtLoadLit = new System.Windows.Forms.Label();
      this.lblMidLoadLit = new System.Windows.Forms.Label();
      this.lblChpLoadLit = new System.Windows.Forms.Label();
      this.cmdRenameIni = new System.Windows.Forms.Button();
      this.cmdRestoreIni = new System.Windows.Forms.Button();
      this.cmdChordCfg = new System.Windows.Forms.Button();
      this.cmdChordRanks = new System.Windows.Forms.Button();
      this.grpOther = new System.Windows.Forms.GroupBox();
      this.chkDisablePCKB = new System.Windows.Forms.CheckBox();
      this.grpChordPlay.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudNotesPerChordPlay)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudNotesPerChordDisplay)).BeginInit();
      this.grpMidi.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudDefaultSongLength)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudDIdd)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudTPDI)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMinChordSize)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMaxChordSize)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudPlayResolution)).BeginInit();
      this.grpMidiPos.SuspendLayout();
      this.grpTimers.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudAutoSyncChordDelay)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudSyncopationNN)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudSyncopationDD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudTimerSustain)).BeginInit();
      this.grpMisc.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudUndoRedoCapacity)).BeginInit();
      this.grpChordSetMinorKey.SuspendLayout();
      this.grpMidiPlayAdvanced.SuspendLayout();
      this.grpFiles.SuspendLayout();
      this.grpOther.SuspendLayout();
      this.SuspendLayout();
      // 
      // grpChordPlay
      // 
      this.grpChordPlay.Controls.Add(this.chkConstantChordPlayPCKB);
      this.grpChordPlay.Controls.Add(this.chkConstantChordPlayMidiKB);
      this.grpChordPlay.Controls.Add(this.nudNotesPerChordPlay);
      this.grpChordPlay.Controls.Add(this.lblnudNotesPerChordPlay);
      this.grpChordPlay.Controls.Add(this.chkConstantChordDisplay);
      this.grpChordPlay.Controls.Add(this.nudNotesPerChordDisplay);
      this.grpChordPlay.Controls.Add(this.lblnudNotesPerChordDisplay);
      this.grpChordPlay.Location = new System.Drawing.Point(226, 150);
      this.grpChordPlay.Name = "grpChordPlay";
      this.grpChordPlay.Size = new System.Drawing.Size(268, 83);
      this.grpChordPlay.TabIndex = 29;
      this.grpChordPlay.TabStop = false;
      this.grpChordPlay.Text = "ChordPlay";
      // 
      // chkConstantChordPlayPCKB
      // 
      this.chkConstantChordPlayPCKB.AutoSize = true;
      this.chkConstantChordPlayPCKB.Checked = true;
      this.chkConstantChordPlayPCKB.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkConstantChordPlayPCKB.Location = new System.Drawing.Point(197, 61);
      this.chkConstantChordPlayPCKB.Name = "chkConstantChordPlayPCKB";
      this.chkConstantChordPlayPCKB.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.chkConstantChordPlayPCKB.Size = new System.Drawing.Size(57, 17);
      this.chkConstantChordPlayPCKB.TabIndex = 237;
      this.chkConstantChordPlayPCKB.Text = "PC KB";
      this.chkConstantChordPlayPCKB.UseVisualStyleBackColor = true;
      // 
      // chkConstantChordPlayMidiKB
      // 
      this.chkConstantChordPlayMidiKB.AutoSize = true;
      this.chkConstantChordPlayMidiKB.Location = new System.Drawing.Point(197, 42);
      this.chkConstantChordPlayMidiKB.Name = "chkConstantChordPlayMidiKB";
      this.chkConstantChordPlayMidiKB.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.chkConstantChordPlayMidiKB.Size = new System.Drawing.Size(62, 17);
      this.chkConstantChordPlayMidiKB.TabIndex = 45;
      this.chkConstantChordPlayMidiKB.Text = "Midi KB";
      this.chkConstantChordPlayMidiKB.UseVisualStyleBackColor = true;
      // 
      // nudNotesPerChordPlay
      // 
      this.nudNotesPerChordPlay.Location = new System.Drawing.Point(151, 48);
      this.nudNotesPerChordPlay.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
      this.nudNotesPerChordPlay.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
      this.nudNotesPerChordPlay.Name = "nudNotesPerChordPlay";
      this.nudNotesPerChordPlay.Size = new System.Drawing.Size(40, 20);
      this.nudNotesPerChordPlay.TabIndex = 44;
      this.nudNotesPerChordPlay.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
      // 
      // lblnudNotesPerChordPlay
      // 
      this.lblnudNotesPerChordPlay.AutoSize = true;
      this.lblnudNotesPerChordPlay.Location = new System.Drawing.Point(19, 50);
      this.lblnudNotesPerChordPlay.Name = "lblnudNotesPerChordPlay";
      this.lblnudNotesPerChordPlay.Size = new System.Drawing.Size(107, 13);
      this.lblnudNotesPerChordPlay.TabIndex = 43;
      this.lblnudNotesPerChordPlay.Text = "Play Notes per Chord";
      // 
      // chkConstantChordDisplay
      // 
      this.chkConstantChordDisplay.AutoSize = true;
      this.chkConstantChordDisplay.Location = new System.Drawing.Point(197, 17);
      this.chkConstantChordDisplay.Name = "chkConstantChordDisplay";
      this.chkConstantChordDisplay.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.chkConstantChordDisplay.Size = new System.Drawing.Size(15, 14);
      this.chkConstantChordDisplay.TabIndex = 42;
      this.chkConstantChordDisplay.UseVisualStyleBackColor = true;
      this.chkConstantChordDisplay.CheckedChanged += new System.EventHandler(this.ConstantChordDisplay_Changed);
      // 
      // nudNotesPerChordDisplay
      // 
      this.nudNotesPerChordDisplay.Location = new System.Drawing.Point(151, 14);
      this.nudNotesPerChordDisplay.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
      this.nudNotesPerChordDisplay.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
      this.nudNotesPerChordDisplay.Name = "nudNotesPerChordDisplay";
      this.nudNotesPerChordDisplay.Size = new System.Drawing.Size(40, 20);
      this.nudNotesPerChordDisplay.TabIndex = 16;
      this.nudNotesPerChordDisplay.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
      this.nudNotesPerChordDisplay.ValueChanged += new System.EventHandler(this.ConstantChordDisplay_Changed);
      // 
      // lblnudNotesPerChordDisplay
      // 
      this.lblnudNotesPerChordDisplay.AutoSize = true;
      this.lblnudNotesPerChordDisplay.Location = new System.Drawing.Point(19, 18);
      this.lblnudNotesPerChordDisplay.Name = "lblnudNotesPerChordDisplay";
      this.lblnudNotesPerChordDisplay.Size = new System.Drawing.Size(121, 13);
      this.lblnudNotesPerChordDisplay.TabIndex = 15;
      this.lblnudNotesPerChordDisplay.Text = "Display Notes per Chord";
      // 
      // grpMidi
      // 
      this.grpMidi.Controls.Add(this.nudDefaultSongLength);
      this.grpMidi.Controls.Add(this.lblnudDefaultSongLength);
      this.grpMidi.Controls.Add(this.nudDIdd);
      this.grpMidi.Controls.Add(this.lblnudDIdd);
      this.grpMidi.Location = new System.Drawing.Point(489, 6);
      this.grpMidi.Name = "grpMidi";
      this.grpMidi.Size = new System.Drawing.Size(182, 80);
      this.grpMidi.TabIndex = 39;
      this.grpMidi.TabStop = false;
      this.grpMidi.Tag = "0";
      this.grpMidi.Text = "Midi";
      // 
      // nudDefaultSongLength
      // 
      this.nudDefaultSongLength.BackColor = System.Drawing.SystemColors.Window;
      this.nudDefaultSongLength.Location = new System.Drawing.Point(125, 49);
      this.nudDefaultSongLength.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.nudDefaultSongLength.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.nudDefaultSongLength.Name = "nudDefaultSongLength";
      this.nudDefaultSongLength.ReadOnly = true;
      this.nudDefaultSongLength.Size = new System.Drawing.Size(41, 20);
      this.nudDefaultSongLength.TabIndex = 79;
      this.nudDefaultSongLength.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
      // 
      // lblnudDefaultSongLength
      // 
      this.lblnudDefaultSongLength.AutoSize = true;
      this.lblnudDefaultSongLength.Location = new System.Drawing.Point(15, 44);
      this.lblnudDefaultSongLength.Name = "lblnudDefaultSongLength";
      this.lblnudDefaultSongLength.Size = new System.Drawing.Size(105, 26);
      this.lblnudDefaultSongLength.TabIndex = 78;
      this.lblnudDefaultSongLength.Text = "Default Song Length\r\n(WholeNotes)";
      // 
      // nudDIdd
      // 
      this.nudDIdd.BackColor = System.Drawing.SystemColors.Window;
      this.nudDIdd.Location = new System.Drawing.Point(132, 16);
      this.nudDIdd.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
      this.nudDIdd.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
      this.nudDIdd.Name = "nudDIdd";
      this.nudDIdd.ReadOnly = true;
      this.nudDIdd.Size = new System.Drawing.Size(34, 20);
      this.nudDIdd.TabIndex = 59;
      this.nudDIdd.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
      this.nudDIdd.ValueChanged += new System.EventHandler(this.nudDIdd_ValueChanged);
      // 
      // lblnudDIdd
      // 
      this.lblnudDIdd.AutoSize = true;
      this.lblnudDIdd.Location = new System.Drawing.Point(15, 14);
      this.lblnudDIdd.Name = "lblnudDIdd";
      this.lblnudDIdd.Size = new System.Drawing.Size(118, 26);
      this.lblnudDIdd.TabIndex = 58;
      this.lblnudDIdd.Text = "Display Resolution\r\n(requires Project reload)";
      // 
      // nudTPDI
      // 
      this.nudTPDI.BackColor = System.Drawing.SystemColors.Window;
      this.nudTPDI.ForeColor = System.Drawing.Color.Red;
      this.nudTPDI.Location = new System.Drawing.Point(159, 57);
      this.nudTPDI.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
      this.nudTPDI.Name = "nudTPDI";
      this.nudTPDI.ReadOnly = true;
      this.nudTPDI.Size = new System.Drawing.Size(34, 20);
      this.nudTPDI.TabIndex = 75;
      this.nudTPDI.ValueChanged += new System.EventHandler(this.nudTPDI_ValueChanged);
      // 
      // lblnudTPDI
      // 
      this.lblnudTPDI.AutoSize = true;
      this.lblnudTPDI.ForeColor = System.Drawing.Color.Red;
      this.lblnudTPDI.Location = new System.Drawing.Point(16, 59);
      this.lblnudTPDI.Name = "lblnudTPDI";
      this.lblnudTPDI.Size = new System.Drawing.Size(131, 13);
      this.lblnudTPDI.TabIndex = 74;
      this.lblnudTPDI.Text = "Ticks/ShowInt (0=ignore)*";
      // 
      // lblnudMinChordSize
      // 
      this.lblnudMinChordSize.AutoSize = true;
      this.lblnudMinChordSize.ForeColor = System.Drawing.Color.Red;
      this.lblnudMinChordSize.Location = new System.Drawing.Point(16, 17);
      this.lblnudMinChordSize.Name = "lblnudMinChordSize";
      this.lblnudMinChordSize.Size = new System.Drawing.Size(78, 13);
      this.lblnudMinChordSize.TabIndex = 57;
      this.lblnudMinChordSize.Text = "Min Chord Size";
      // 
      // nudMinChordSize
      // 
      this.nudMinChordSize.BackColor = System.Drawing.SystemColors.Window;
      this.nudMinChordSize.ForeColor = System.Drawing.Color.Red;
      this.nudMinChordSize.Location = new System.Drawing.Point(159, 15);
      this.nudMinChordSize.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
      this.nudMinChordSize.Name = "nudMinChordSize";
      this.nudMinChordSize.ReadOnly = true;
      this.nudMinChordSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.nudMinChordSize.Size = new System.Drawing.Size(34, 20);
      this.nudMinChordSize.TabIndex = 56;
      // 
      // lblnudMaxChordSize
      // 
      this.lblnudMaxChordSize.AutoSize = true;
      this.lblnudMaxChordSize.ForeColor = System.Drawing.Color.Red;
      this.lblnudMaxChordSize.Location = new System.Drawing.Point(16, 38);
      this.lblnudMaxChordSize.Name = "lblnudMaxChordSize";
      this.lblnudMaxChordSize.Size = new System.Drawing.Size(81, 13);
      this.lblnudMaxChordSize.TabIndex = 55;
      this.lblnudMaxChordSize.Text = "Max Chord Size";
      // 
      // nudMaxChordSize
      // 
      this.nudMaxChordSize.BackColor = System.Drawing.SystemColors.Window;
      this.nudMaxChordSize.ForeColor = System.Drawing.Color.Red;
      this.nudMaxChordSize.Location = new System.Drawing.Point(159, 36);
      this.nudMaxChordSize.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
      this.nudMaxChordSize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
      this.nudMaxChordSize.Name = "nudMaxChordSize";
      this.nudMaxChordSize.ReadOnly = true;
      this.nudMaxChordSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.nudMaxChordSize.Size = new System.Drawing.Size(34, 20);
      this.nudMaxChordSize.TabIndex = 54;
      this.nudMaxChordSize.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
      // 
      // lblReload
      // 
      this.lblReload.AutoSize = true;
      this.lblReload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblReload.ForeColor = System.Drawing.Color.Red;
      this.lblReload.Location = new System.Drawing.Point(16, 80);
      this.lblReload.Name = "lblReload";
      this.lblReload.Size = new System.Drawing.Size(139, 13);
      this.lblReload.TabIndex = 73;
      this.lblReload.Text = "* = Project Reload Required";
      // 
      // nudPlayResolution
      // 
      this.nudPlayResolution.BackColor = System.Drawing.SystemColors.Window;
      this.nudPlayResolution.Location = new System.Drawing.Point(152, 36);
      this.nudPlayResolution.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
      this.nudPlayResolution.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudPlayResolution.Name = "nudPlayResolution";
      this.nudPlayResolution.ReadOnly = true;
      this.nudPlayResolution.Size = new System.Drawing.Size(40, 20);
      this.nudPlayResolution.TabIndex = 77;
      this.nudPlayResolution.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
      // 
      // lblnudPlayResolution
      // 
      this.lblnudPlayResolution.AutoSize = true;
      this.lblnudPlayResolution.Location = new System.Drawing.Point(18, 38);
      this.lblnudPlayResolution.Name = "lblnudPlayResolution";
      this.lblnudPlayResolution.Size = new System.Drawing.Size(93, 13);
      this.lblnudPlayResolution.TabIndex = 76;
      this.lblnudPlayResolution.Text = "Stream Resolution";
      // 
      // grpMidiPos
      // 
      this.grpMidiPos.Controls.Add(this.chkMidiStartStop);
      this.grpMidiPos.Controls.Add(this.chkMidiSPP);
      this.grpMidiPos.ForeColor = System.Drawing.Color.Red;
      this.grpMidiPos.Location = new System.Drawing.Point(806, 114);
      this.grpMidiPos.Name = "grpMidiPos";
      this.grpMidiPos.Size = new System.Drawing.Size(146, 57);
      this.grpMidiPos.TabIndex = 38;
      this.grpMidiPos.TabStop = false;
      this.grpMidiPos.Text = "Midi Positioning";
      // 
      // chkMidiStartStop
      // 
      this.chkMidiStartStop.AutoSize = true;
      this.chkMidiStartStop.Checked = true;
      this.chkMidiStartStop.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkMidiStartStop.Location = new System.Drawing.Point(13, 36);
      this.chkMidiStartStop.Name = "chkMidiStartStop";
      this.chkMidiStartStop.Size = new System.Drawing.Size(75, 17);
      this.chkMidiStartStop.TabIndex = 1;
      this.chkMidiStartStop.Text = "Start/Stop";
      this.chkMidiStartStop.UseVisualStyleBackColor = true;
      // 
      // chkMidiSPP
      // 
      this.chkMidiSPP.AutoSize = true;
      this.chkMidiSPP.Checked = true;
      this.chkMidiSPP.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkMidiSPP.Location = new System.Drawing.Point(13, 17);
      this.chkMidiSPP.Name = "chkMidiSPP";
      this.chkMidiSPP.Size = new System.Drawing.Size(135, 17);
      this.chkMidiSPP.TabIndex = 0;
      this.chkMidiSPP.Text = "Project Position Pointer";
      this.chkMidiSPP.UseVisualStyleBackColor = true;
      // 
      // grpTimers
      // 
      this.grpTimers.Controls.Add(this.lbllitSyncopation);
      this.grpTimers.Controls.Add(this.lblAutoSyncChordDelayMS);
      this.grpTimers.Controls.Add(this.lblAutoSyncChordDelay);
      this.grpTimers.Controls.Add(this.nudPlayResolution);
      this.grpTimers.Controls.Add(this.nudAutoSyncChordDelay);
      this.grpTimers.Controls.Add(this.lblnudPlayResolution);
      this.grpTimers.Controls.Add(this.lblnudSyncopation);
      this.grpTimers.Controls.Add(this.lblnudSyncopationNote);
      this.grpTimers.Controls.Add(this.lblnudSyncopationSlash);
      this.grpTimers.Controls.Add(this.nudSyncopationNN);
      this.grpTimers.Controls.Add(this.nudSyncopationDD);
      this.grpTimers.Controls.Add(this.lblnudPlayResolutionMS);
      this.grpTimers.Controls.Add(this.lblnudTimerSustainMS);
      this.grpTimers.Controls.Add(this.lblnudTimerSustain);
      this.grpTimers.Controls.Add(this.nudTimerSustain);
      this.grpTimers.Location = new System.Drawing.Point(226, 6);
      this.grpTimers.Name = "grpTimers";
      this.grpTimers.Size = new System.Drawing.Size(247, 138);
      this.grpTimers.TabIndex = 55;
      this.grpTimers.TabStop = false;
      this.grpTimers.Text = "Timers ";
      // 
      // lbllitSyncopation
      // 
      this.lbllitSyncopation.AutoSize = true;
      this.lbllitSyncopation.Location = new System.Drawing.Point(18, 84);
      this.lbllitSyncopation.Name = "lbllitSyncopation";
      this.lbllitSyncopation.Size = new System.Drawing.Size(187, 13);
      this.lbllitSyncopation.TabIndex = 78;
      this.lbllitSyncopation.Text = "*only used if not overriden by PlayMap";
      // 
      // lblAutoSyncChordDelayMS
      // 
      this.lblAutoSyncChordDelayMS.AutoSize = true;
      this.lblAutoSyncChordDelayMS.ForeColor = System.Drawing.Color.Red;
      this.lblAutoSyncChordDelayMS.Location = new System.Drawing.Point(198, 113);
      this.lblAutoSyncChordDelayMS.Name = "lblAutoSyncChordDelayMS";
      this.lblAutoSyncChordDelayMS.Size = new System.Drawing.Size(20, 13);
      this.lblAutoSyncChordDelayMS.TabIndex = 72;
      this.lblAutoSyncChordDelayMS.Text = "ms";
      // 
      // lblAutoSyncChordDelay
      // 
      this.lblAutoSyncChordDelay.AutoSize = true;
      this.lblAutoSyncChordDelay.ForeColor = System.Drawing.Color.Red;
      this.lblAutoSyncChordDelay.Location = new System.Drawing.Point(18, 115);
      this.lblAutoSyncChordDelay.Name = "lblAutoSyncChordDelay";
      this.lblAutoSyncChordDelay.Size = new System.Drawing.Size(114, 13);
      this.lblAutoSyncChordDelay.TabIndex = 71;
      this.lblAutoSyncChordDelay.Text = "AutoSync Chord Delay";
      // 
      // nudAutoSyncChordDelay
      // 
      this.nudAutoSyncChordDelay.ForeColor = System.Drawing.Color.Red;
      this.nudAutoSyncChordDelay.Location = new System.Drawing.Point(152, 110);
      this.nudAutoSyncChordDelay.Name = "nudAutoSyncChordDelay";
      this.nudAutoSyncChordDelay.Size = new System.Drawing.Size(40, 20);
      this.nudAutoSyncChordDelay.TabIndex = 70;
      // 
      // lblnudSyncopation
      // 
      this.lblnudSyncopation.AutoSize = true;
      this.lblnudSyncopation.Location = new System.Drawing.Point(18, 55);
      this.lblnudSyncopation.Name = "lblnudSyncopation";
      this.lblnudSyncopation.Size = new System.Drawing.Size(70, 26);
      this.lblnudSyncopation.TabIndex = 69;
      this.lblnudSyncopation.Text = "Default\r\nSyncopation*";
      // 
      // lblnudSyncopationNote
      // 
      this.lblnudSyncopationNote.AutoSize = true;
      this.lblnudSyncopationNote.Location = new System.Drawing.Point(198, 59);
      this.lblnudSyncopationNote.Name = "lblnudSyncopationNote";
      this.lblnudSyncopationNote.Size = new System.Drawing.Size(28, 13);
      this.lblnudSyncopationNote.TabIndex = 67;
      this.lblnudSyncopationNote.Text = "note";
      // 
      // lblnudSyncopationSlash
      // 
      this.lblnudSyncopationSlash.AutoSize = true;
      this.lblnudSyncopationSlash.Location = new System.Drawing.Point(139, 59);
      this.lblnudSyncopationSlash.Name = "lblnudSyncopationSlash";
      this.lblnudSyncopationSlash.Size = new System.Drawing.Size(12, 13);
      this.lblnudSyncopationSlash.TabIndex = 66;
      this.lblnudSyncopationSlash.Text = "/";
      // 
      // nudSyncopationNN
      // 
      this.nudSyncopationNN.BackColor = System.Drawing.SystemColors.Window;
      this.nudSyncopationNN.Location = new System.Drawing.Point(97, 57);
      this.nudSyncopationNN.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
      this.nudSyncopationNN.Name = "nudSyncopationNN";
      this.nudSyncopationNN.ReadOnly = true;
      this.nudSyncopationNN.Size = new System.Drawing.Size(40, 20);
      this.nudSyncopationNN.TabIndex = 65;
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
      this.nudSyncopationDD.Location = new System.Drawing.Point(152, 57);
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
      this.nudSyncopationDD.TabIndex = 63;
      this.nudSyncopationDD.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
      this.nudSyncopationDD.ValueChanged += new System.EventHandler(this.nudSyncopationDD_ValueChanged);
      // 
      // lblnudPlayResolutionMS
      // 
      this.lblnudPlayResolutionMS.AutoSize = true;
      this.lblnudPlayResolutionMS.Location = new System.Drawing.Point(198, 38);
      this.lblnudPlayResolutionMS.Name = "lblnudPlayResolutionMS";
      this.lblnudPlayResolutionMS.Size = new System.Drawing.Size(20, 13);
      this.lblnudPlayResolutionMS.TabIndex = 62;
      this.lblnudPlayResolutionMS.Text = "ms";
      // 
      // lblnudTimerSustainMS
      // 
      this.lblnudTimerSustainMS.AutoSize = true;
      this.lblnudTimerSustainMS.Location = new System.Drawing.Point(199, 18);
      this.lblnudTimerSustainMS.Name = "lblnudTimerSustainMS";
      this.lblnudTimerSustainMS.Size = new System.Drawing.Size(20, 13);
      this.lblnudTimerSustainMS.TabIndex = 61;
      this.lblnudTimerSustainMS.Text = "ms";
      // 
      // lblnudTimerSustain
      // 
      this.lblnudTimerSustain.AutoSize = true;
      this.lblnudTimerSustain.Location = new System.Drawing.Point(17, 18);
      this.lblnudTimerSustain.Name = "lblnudTimerSustain";
      this.lblnudTimerSustain.Size = new System.Drawing.Size(45, 13);
      this.lblnudTimerSustain.TabIndex = 52;
      this.lblnudTimerSustain.Text = "Sustain ";
      // 
      // nudTimerSustain
      // 
      this.nudTimerSustain.Location = new System.Drawing.Point(152, 15);
      this.nudTimerSustain.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.nudTimerSustain.Name = "nudTimerSustain";
      this.nudTimerSustain.Size = new System.Drawing.Size(40, 20);
      this.nudTimerSustain.TabIndex = 51;
      this.nudTimerSustain.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
      this.nudTimerSustain.ValueChanged += new System.EventHandler(this.nudTimerSustain_ValueChanged);
      // 
      // grpMisc
      // 
      this.grpMisc.Controls.Add(this.chkDisablePCKB);
      this.grpMisc.Controls.Add(this.nudUndoRedoCapacity);
      this.grpMisc.Controls.Add(this.lblUndoRedoCapacity);
      this.grpMisc.Controls.Add(this.chkExitPrompt);
      this.grpMisc.Controls.Add(this.chkTTActive);
      this.grpMisc.Controls.Add(this.chkAllNotesOffAfterSustain);
      this.grpMisc.Controls.Add(this.chkNoAudioSync);
      this.grpMisc.Controls.Add(this.chkLoadMM);
      this.grpMisc.Location = new System.Drawing.Point(20, 6);
      this.grpMisc.Name = "grpMisc";
      this.grpMisc.Size = new System.Drawing.Size(197, 157);
      this.grpMisc.TabIndex = 56;
      this.grpMisc.TabStop = false;
      this.grpMisc.Text = "General";
      // 
      // nudUndoRedoCapacity
      // 
      this.nudUndoRedoCapacity.Location = new System.Drawing.Point(7, 126);
      this.nudUndoRedoCapacity.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
      this.nudUndoRedoCapacity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudUndoRedoCapacity.Name = "nudUndoRedoCapacity";
      this.nudUndoRedoCapacity.Size = new System.Drawing.Size(39, 20);
      this.nudUndoRedoCapacity.TabIndex = 236;
      this.nudUndoRedoCapacity.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
      // 
      // lblUndoRedoCapacity
      // 
      this.lblUndoRedoCapacity.AutoSize = true;
      this.lblUndoRedoCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblUndoRedoCapacity.Location = new System.Drawing.Point(53, 129);
      this.lblUndoRedoCapacity.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.lblUndoRedoCapacity.Name = "lblUndoRedoCapacity";
      this.lblUndoRedoCapacity.Size = new System.Drawing.Size(108, 13);
      this.lblUndoRedoCapacity.TabIndex = 235;
      this.lblUndoRedoCapacity.Text = "Undo/Redo Capacity";
      this.lblUndoRedoCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chkExitPrompt
      // 
      this.chkExitPrompt.AutoSize = true;
      this.chkExitPrompt.Location = new System.Drawing.Point(7, 70);
      this.chkExitPrompt.Name = "chkExitPrompt";
      this.chkExitPrompt.Size = new System.Drawing.Size(175, 17);
      this.chkExitPrompt.TabIndex = 195;
      this.chkExitPrompt.Text = "Prompt to Save Settings on Exit";
      this.chkExitPrompt.UseVisualStyleBackColor = true;
      // 
      // chkTTActive
      // 
      this.chkTTActive.AutoSize = true;
      this.chkTTActive.Checked = true;
      this.chkTTActive.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkTTActive.Location = new System.Drawing.Point(7, 54);
      this.chkTTActive.Name = "chkTTActive";
      this.chkTTActive.Size = new System.Drawing.Size(100, 17);
      this.chkTTActive.TabIndex = 193;
      this.chkTTActive.Text = "ToolTips Active";
      this.chkTTActive.UseVisualStyleBackColor = true;
      this.chkTTActive.CheckedChanged += new System.EventHandler(this.chkTTActive_CheckedChanged);
      // 
      // chkAllNotesOffAfterSustain
      // 
      this.chkAllNotesOffAfterSustain.AutoSize = true;
      this.chkAllNotesOffAfterSustain.ForeColor = System.Drawing.SystemColors.ControlText;
      this.chkAllNotesOffAfterSustain.Location = new System.Drawing.Point(7, 37);
      this.chkAllNotesOffAfterSustain.Name = "chkAllNotesOffAfterSustain";
      this.chkAllNotesOffAfterSustain.Size = new System.Drawing.Size(186, 17);
      this.chkAllNotesOffAfterSustain.TabIndex = 188;
      this.chkAllNotesOffAfterSustain.Text = "Send AllNotesOff after Sustain Up";
      this.chkAllNotesOffAfterSustain.UseVisualStyleBackColor = true;
      // 
      // chkNoAudioSync
      // 
      this.chkNoAudioSync.AutoSize = true;
      this.chkNoAudioSync.Location = new System.Drawing.Point(7, 86);
      this.chkNoAudioSync.Name = "chkNoAudioSync";
      this.chkNoAudioSync.Size = new System.Drawing.Size(178, 17);
      this.chkNoAudioSync.TabIndex = 237;
      this.chkNoAudioSync.Text = "Disable AutoSync on Audio Play";
      this.chkNoAudioSync.UseVisualStyleBackColor = true;
      this.chkNoAudioSync.CheckedChanged += new System.EventHandler(this.chkNoAudioSync_CheckedChanged);
      // 
      // chkLoadMM
      // 
      this.chkLoadMM.AutoSize = true;
      this.chkLoadMM.Location = new System.Drawing.Point(7, 20);
      this.chkLoadMM.Name = "chkLoadMM";
      this.chkLoadMM.Size = new System.Drawing.Size(168, 17);
      this.chkLoadMM.TabIndex = 60;
      this.chkLoadMM.Text = "Load TrackMap on .chp Load";
      this.chkLoadMM.UseVisualStyleBackColor = true;
      // 
      // chkIgnoreNullChords
      // 
      this.chkIgnoreNullChords.AutoSize = true;
      this.chkIgnoreNullChords.ForeColor = System.Drawing.Color.Red;
      this.chkIgnoreNullChords.Location = new System.Drawing.Point(19, 114);
      this.chkIgnoreNullChords.Name = "chkIgnoreNullChords";
      this.chkIgnoreNullChords.Size = new System.Drawing.Size(155, 17);
      this.chkIgnoreNullChords.TabIndex = 55;
      this.chkIgnoreNullChords.Text = "Ignore Null Chords on Load";
      this.chkIgnoreNullChords.UseVisualStyleBackColor = true;
      // 
      // cmdResetPlay
      // 
      this.cmdResetPlay.ForeColor = System.Drawing.Color.Red;
      this.cmdResetPlay.Location = new System.Drawing.Point(682, 194);
      this.cmdResetPlay.Name = "cmdResetPlay";
      this.cmdResetPlay.Size = new System.Drawing.Size(55, 39);
      this.cmdResetPlay.TabIndex = 74;
      this.cmdResetPlay.Text = "Reset Play";
      this.cmdResetPlay.UseVisualStyleBackColor = true;
      this.cmdResetPlay.Click += new System.EventHandler(this.cmdResetPlay_Click);
      // 
      // grpChordSetMinorKey
      // 
      this.grpChordSetMinorKey.Controls.Add(this.optMinorSpecial);
      this.grpChordSetMinorKey.Controls.Add(this.optMinorMelodicDown);
      this.grpChordSetMinorKey.Controls.Add(this.optMinorMelodicUp);
      this.grpChordSetMinorKey.Controls.Add(this.optMinorHarmonic);
      this.grpChordSetMinorKey.ForeColor = System.Drawing.Color.Red;
      this.grpChordSetMinorKey.Location = new System.Drawing.Point(806, 8);
      this.grpChordSetMinorKey.Name = "grpChordSetMinorKey";
      this.grpChordSetMinorKey.Size = new System.Drawing.Size(138, 100);
      this.grpChordSetMinorKey.TabIndex = 145;
      this.grpChordSetMinorKey.TabStop = false;
      this.grpChordSetMinorKey.Text = "ChordSet Minor Key";
      // 
      // optMinorSpecial
      // 
      this.optMinorSpecial.AutoSize = true;
      this.optMinorSpecial.Location = new System.Drawing.Point(19, 73);
      this.optMinorSpecial.Name = "optMinorSpecial";
      this.optMinorSpecial.Size = new System.Drawing.Size(60, 17);
      this.optMinorSpecial.TabIndex = 3;
      this.optMinorSpecial.Text = "Special";
      this.optMinorSpecial.UseVisualStyleBackColor = true;
      // 
      // optMinorMelodicDown
      // 
      this.optMinorMelodicDown.AutoSize = true;
      this.optMinorMelodicDown.Location = new System.Drawing.Point(19, 55);
      this.optMinorMelodicDown.Name = "optMinorMelodicDown";
      this.optMinorMelodicDown.Size = new System.Drawing.Size(93, 17);
      this.optMinorMelodicDown.TabIndex = 2;
      this.optMinorMelodicDown.Text = "Melodic Down";
      this.optMinorMelodicDown.UseVisualStyleBackColor = true;
      // 
      // optMinorMelodicUp
      // 
      this.optMinorMelodicUp.AutoSize = true;
      this.optMinorMelodicUp.Location = new System.Drawing.Point(19, 37);
      this.optMinorMelodicUp.Name = "optMinorMelodicUp";
      this.optMinorMelodicUp.Size = new System.Drawing.Size(79, 17);
      this.optMinorMelodicUp.TabIndex = 1;
      this.optMinorMelodicUp.Text = "Melodic Up";
      this.optMinorMelodicUp.UseVisualStyleBackColor = true;
      // 
      // optMinorHarmonic
      // 
      this.optMinorHarmonic.AutoSize = true;
      this.optMinorHarmonic.Checked = true;
      this.optMinorHarmonic.Location = new System.Drawing.Point(19, 19);
      this.optMinorHarmonic.Name = "optMinorHarmonic";
      this.optMinorHarmonic.Size = new System.Drawing.Size(70, 17);
      this.optMinorHarmonic.TabIndex = 0;
      this.optMinorHarmonic.TabStop = true;
      this.optMinorHarmonic.Text = "Harmonic";
      this.optMinorHarmonic.UseVisualStyleBackColor = true;
      // 
      // grpMidiPlayAdvanced
      // 
      this.grpMidiPlayAdvanced.Controls.Add(this.chkFilterMidiBank);
      this.grpMidiPlayAdvanced.Controls.Add(this.chkKBChordMatch);
      this.grpMidiPlayAdvanced.Controls.Add(this.chkAutoRecChan);
      this.grpMidiPlayAdvanced.ForeColor = System.Drawing.Color.Red;
      this.grpMidiPlayAdvanced.Location = new System.Drawing.Point(803, 177);
      this.grpMidiPlayAdvanced.Name = "grpMidiPlayAdvanced";
      this.grpMidiPlayAdvanced.Size = new System.Drawing.Size(203, 77);
      this.grpMidiPlayAdvanced.TabIndex = 189;
      this.grpMidiPlayAdvanced.TabStop = false;
      this.grpMidiPlayAdvanced.Text = "MidiPlay Advanced";
      // 
      // chkFilterMidiBank
      // 
      this.chkFilterMidiBank.AutoSize = true;
      this.chkFilterMidiBank.Checked = true;
      this.chkFilterMidiBank.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkFilterMidiBank.ForeColor = System.Drawing.Color.Red;
      this.chkFilterMidiBank.Location = new System.Drawing.Point(16, 19);
      this.chkFilterMidiBank.Name = "chkFilterMidiBank";
      this.chkFilterMidiBank.Size = new System.Drawing.Size(169, 17);
      this.chkFilterMidiBank.TabIndex = 195;
      this.chkFilterMidiBank.Text = "Filter Out Midi Bank Messages";
      this.chkFilterMidiBank.UseVisualStyleBackColor = true;
      // 
      // chkKBChordMatch
      // 
      this.chkKBChordMatch.AutoSize = true;
      this.chkKBChordMatch.Checked = true;
      this.chkKBChordMatch.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkKBChordMatch.ForeColor = System.Drawing.Color.Red;
      this.chkKBChordMatch.Location = new System.Drawing.Point(16, 36);
      this.chkKBChordMatch.Name = "chkKBChordMatch";
      this.chkKBChordMatch.Size = new System.Drawing.Size(158, 17);
      this.chkKBChordMatch.TabIndex = 192;
      this.chkKBChordMatch.Text = "KBChord Match Note Count";
      this.chkKBChordMatch.UseVisualStyleBackColor = true;
      // 
      // chkAutoRecChan
      // 
      this.chkAutoRecChan.AutoSize = true;
      this.chkAutoRecChan.Checked = true;
      this.chkAutoRecChan.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkAutoRecChan.ForeColor = System.Drawing.Color.Red;
      this.chkAutoRecChan.Location = new System.Drawing.Point(16, 53);
      this.chkAutoRecChan.Name = "chkAutoRecChan";
      this.chkAutoRecChan.Size = new System.Drawing.Size(185, 17);
      this.chkAutoRecChan.TabIndex = 193;
      this.chkAutoRecChan.Text = "Use First Free Recording Channel";
      this.chkAutoRecChan.UseVisualStyleBackColor = true;
      // 
      // cmdHelp
      // 
      this.cmdHelp.Location = new System.Drawing.Point(682, 153);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(55, 39);
      this.cmdHelp.TabIndex = 190;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // grpFiles
      // 
      this.grpFiles.Controls.Add(this.lblAudLoad);
      this.grpFiles.Controls.Add(this.lblLyrLoad);
      this.grpFiles.Controls.Add(this.lblChtLoad);
      this.grpFiles.Controls.Add(this.lblMidLoad);
      this.grpFiles.Controls.Add(this.lblChpLoad);
      this.grpFiles.Controls.Add(this.lblAudLoadLit);
      this.grpFiles.Controls.Add(this.lblLyrLoadLit);
      this.grpFiles.Controls.Add(this.lblChtLoadLit);
      this.grpFiles.Controls.Add(this.lblMidLoadLit);
      this.grpFiles.Controls.Add(this.lblChpLoadLit);
      this.grpFiles.Location = new System.Drawing.Point(680, 12);
      this.grpFiles.Name = "grpFiles";
      this.grpFiles.Size = new System.Drawing.Size(113, 103);
      this.grpFiles.TabIndex = 239;
      this.grpFiles.TabStop = false;
      this.grpFiles.Text = "File Extensions";
      // 
      // lblAudLoad
      // 
      this.lblAudLoad.Location = new System.Drawing.Point(72, 80);
      this.lblAudLoad.Name = "lblAudLoad";
      this.lblAudLoad.Size = new System.Drawing.Size(40, 13);
      this.lblAudLoad.TabIndex = 19;
      // 
      // lblLyrLoad
      // 
      this.lblLyrLoad.Location = new System.Drawing.Point(72, 65);
      this.lblLyrLoad.Name = "lblLyrLoad";
      this.lblLyrLoad.Size = new System.Drawing.Size(40, 13);
      this.lblLyrLoad.TabIndex = 18;
      // 
      // lblChtLoad
      // 
      this.lblChtLoad.Location = new System.Drawing.Point(72, 50);
      this.lblChtLoad.Name = "lblChtLoad";
      this.lblChtLoad.Size = new System.Drawing.Size(40, 13);
      this.lblChtLoad.TabIndex = 17;
      // 
      // lblMidLoad
      // 
      this.lblMidLoad.Location = new System.Drawing.Point(72, 35);
      this.lblMidLoad.Name = "lblMidLoad";
      this.lblMidLoad.Size = new System.Drawing.Size(40, 13);
      this.lblMidLoad.TabIndex = 16;
      // 
      // lblChpLoad
      // 
      this.lblChpLoad.Location = new System.Drawing.Point(72, 20);
      this.lblChpLoad.Name = "lblChpLoad";
      this.lblChpLoad.Size = new System.Drawing.Size(40, 13);
      this.lblChpLoad.TabIndex = 15;
      // 
      // lblAudLoadLit
      // 
      this.lblAudLoadLit.AutoSize = true;
      this.lblAudLoadLit.Location = new System.Drawing.Point(16, 80);
      this.lblAudLoadLit.Name = "lblAudLoadLit";
      this.lblAudLoadLit.Size = new System.Drawing.Size(50, 13);
      this.lblAudLoadLit.TabIndex = 14;
      this.lblAudLoadLit.Text = "AudioFile";
      // 
      // lblLyrLoadLit
      // 
      this.lblLyrLoadLit.AutoSize = true;
      this.lblLyrLoadLit.Location = new System.Drawing.Point(16, 65);
      this.lblLyrLoadLit.Name = "lblLyrLoadLit";
      this.lblLyrLoadLit.Size = new System.Drawing.Size(50, 13);
      this.lblLyrLoadLit.TabIndex = 13;
      this.lblLyrLoadLit.Text = "LyricsFile";
      // 
      // lblChtLoadLit
      // 
      this.lblChtLoadLit.AutoSize = true;
      this.lblChtLoadLit.Location = new System.Drawing.Point(16, 50);
      this.lblChtLoadLit.Name = "lblChtLoadLit";
      this.lblChtLoadLit.Size = new System.Drawing.Size(47, 13);
      this.lblChtLoadLit.TabIndex = 12;
      this.lblChtLoadLit.Text = "SyncFile";
      // 
      // lblMidLoadLit
      // 
      this.lblMidLoadLit.AutoSize = true;
      this.lblMidLoadLit.Location = new System.Drawing.Point(16, 35);
      this.lblMidLoadLit.Name = "lblMidLoadLit";
      this.lblMidLoadLit.Size = new System.Drawing.Size(42, 13);
      this.lblMidLoadLit.TabIndex = 11;
      this.lblMidLoadLit.Text = "MidiFile";
      // 
      // lblChpLoadLit
      // 
      this.lblChpLoadLit.AutoSize = true;
      this.lblChpLoadLit.Location = new System.Drawing.Point(16, 20);
      this.lblChpLoadLit.Name = "lblChpLoadLit";
      this.lblChpLoadLit.Size = new System.Drawing.Size(51, 13);
      this.lblChpLoadLit.TabIndex = 10;
      this.lblChpLoadLit.Text = "ChordFile";
      // 
      // cmdRenameIni
      // 
      this.cmdRenameIni.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmdRenameIni.Location = new System.Drawing.Point(621, 153);
      this.cmdRenameIni.Name = "cmdRenameIni";
      this.cmdRenameIni.Size = new System.Drawing.Size(55, 39);
      this.cmdRenameIni.TabIndex = 240;
      this.cmdRenameIni.Text = "Reset\r\nSettings";
      this.cmdRenameIni.UseVisualStyleBackColor = true;
      this.cmdRenameIni.Click += new System.EventHandler(this.cmdRenameIni_Click);
      // 
      // cmdRestoreIni
      // 
      this.cmdRestoreIni.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmdRestoreIni.Location = new System.Drawing.Point(621, 194);
      this.cmdRestoreIni.Name = "cmdRestoreIni";
      this.cmdRestoreIni.Size = new System.Drawing.Size(55, 39);
      this.cmdRestoreIni.TabIndex = 241;
      this.cmdRestoreIni.Text = "Restore\r\nSettings";
      this.cmdRestoreIni.UseVisualStyleBackColor = true;
      this.cmdRestoreIni.Click += new System.EventHandler(this.cmdRestoreIni_Click);
      // 
      // cmdChordCfg
      // 
      this.cmdChordCfg.Location = new System.Drawing.Point(743, 194);
      this.cmdChordCfg.Name = "cmdChordCfg";
      this.cmdChordCfg.Size = new System.Drawing.Size(55, 39);
      this.cmdChordCfg.TabIndex = 242;
      this.cmdChordCfg.Text = "Chord\r\nConfig";
      this.cmdChordCfg.UseVisualStyleBackColor = true;
      this.cmdChordCfg.Click += new System.EventHandler(this.cmdChordCfg_Click);
      // 
      // cmdChordRanks
      // 
      this.cmdChordRanks.Location = new System.Drawing.Point(743, 153);
      this.cmdChordRanks.Name = "cmdChordRanks";
      this.cmdChordRanks.Size = new System.Drawing.Size(55, 39);
      this.cmdChordRanks.TabIndex = 243;
      this.cmdChordRanks.Text = "Chord\r\nRanks";
      this.cmdChordRanks.UseVisualStyleBackColor = true;
      this.cmdChordRanks.Click += new System.EventHandler(this.cmdRanks_Click);
      // 
      // grpOther
      // 
      this.grpOther.Controls.Add(this.lblnudMinChordSize);
      this.grpOther.Controls.Add(this.lblReload);
      this.grpOther.Controls.Add(this.nudTPDI);
      this.grpOther.Controls.Add(this.nudMaxChordSize);
      this.grpOther.Controls.Add(this.lblnudMaxChordSize);
      this.grpOther.Controls.Add(this.nudMinChordSize);
      this.grpOther.Controls.Add(this.lblnudTPDI);
      this.grpOther.Controls.Add(this.chkIgnoreNullChords);
      this.grpOther.ForeColor = System.Drawing.Color.Red;
      this.grpOther.Location = new System.Drawing.Point(958, 8);
      this.grpOther.Name = "grpOther";
      this.grpOther.Size = new System.Drawing.Size(207, 142);
      this.grpOther.TabIndex = 244;
      this.grpOther.TabStop = false;
      this.grpOther.Text = "Other";
      // 
      // chkDisablePCKB
      // 
      this.chkDisablePCKB.AutoSize = true;
      this.chkDisablePCKB.Location = new System.Drawing.Point(7, 104);
      this.chkDisablePCKB.Name = "chkDisablePCKB";
      this.chkDisablePCKB.Size = new System.Drawing.Size(92, 17);
      this.chkDisablePCKB.TabIndex = 238;
      this.chkDisablePCKB.Text = "Disable PCKB";
      this.chkDisablePCKB.UseVisualStyleBackColor = true;
      this.chkDisablePCKB.CheckedChanged += new System.EventHandler(this.chkDisablePCKB_CheckedChanged);
      // 
      // frmStart
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1212, 240);
      this.Controls.Add(this.grpOther);
      this.Controls.Add(this.cmdChordRanks);
      this.Controls.Add(this.cmdChordCfg);
      this.Controls.Add(this.cmdRestoreIni);
      this.Controls.Add(this.cmdRenameIni);
      this.Controls.Add(this.grpFiles);
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.grpMidiPlayAdvanced);
      this.Controls.Add(this.grpChordSetMinorKey);
      this.Controls.Add(this.cmdResetPlay);
      this.Controls.Add(this.grpMidi);
      this.Controls.Add(this.grpMisc);
      this.Controls.Add(this.grpTimers);
      this.Controls.Add(this.grpMidiPos);
      this.Controls.Add(this.grpChordPlay);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "frmStart";
      this.Text = "Miscellaneous Configuration - Chord Cadenza";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStart_FormClosing);
      this.Load += new System.EventHandler(this.frmStart_Load);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmStart_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmStart_DragEnter);
      this.grpChordPlay.ResumeLayout(false);
      this.grpChordPlay.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudNotesPerChordPlay)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudNotesPerChordDisplay)).EndInit();
      this.grpMidi.ResumeLayout(false);
      this.grpMidi.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudDefaultSongLength)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudDIdd)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudTPDI)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMinChordSize)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMaxChordSize)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudPlayResolution)).EndInit();
      this.grpMidiPos.ResumeLayout(false);
      this.grpMidiPos.PerformLayout();
      this.grpTimers.ResumeLayout(false);
      this.grpTimers.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudAutoSyncChordDelay)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudSyncopationNN)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudSyncopationDD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudTimerSustain)).EndInit();
      this.grpMisc.ResumeLayout(false);
      this.grpMisc.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudUndoRedoCapacity)).EndInit();
      this.grpChordSetMinorKey.ResumeLayout(false);
      this.grpChordSetMinorKey.PerformLayout();
      this.grpMidiPlayAdvanced.ResumeLayout(false);
      this.grpMidiPlayAdvanced.PerformLayout();
      this.grpFiles.ResumeLayout(false);
      this.grpFiles.PerformLayout();
      this.grpOther.ResumeLayout(false);
      this.grpOther.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.GroupBox grpChordPlay;
    internal System.Windows.Forms.NumericUpDown nudNotesPerChordDisplay;
    private System.Windows.Forms.Label lblnudNotesPerChordDisplay;
    private System.Windows.Forms.GroupBox grpMidi;
    private System.Windows.Forms.GroupBox grpMidiPos;
    internal System.Windows.Forms.CheckBox chkMidiStartStop;
    internal System.Windows.Forms.CheckBox chkMidiSPP;
    private System.Windows.Forms.GroupBox grpTimers;
    private System.Windows.Forms.Label lblnudTimerSustain;
    internal System.Windows.Forms.NumericUpDown nudTimerSustain;
    private System.Windows.Forms.GroupBox grpMisc;
    private System.Windows.Forms.Label lblnudMinChordSize;
    internal System.Windows.Forms.NumericUpDown nudMinChordSize;
    private System.Windows.Forms.Label lblnudMaxChordSize;
    internal System.Windows.Forms.NumericUpDown nudMaxChordSize;
    internal System.Windows.Forms.CheckBox chkConstantChordDisplay;
    internal System.Windows.Forms.CheckBox chkIgnoreNullChords;
    internal System.Windows.Forms.NumericUpDown nudDIdd;
    private System.Windows.Forms.Label lblnudDIdd;
    private System.Windows.Forms.Label lblnudTimerSustainMS;
    private System.Windows.Forms.Label lblnudPlayResolutionMS;
    private System.Windows.Forms.Label lblnudSyncopationSlash;
    internal System.Windows.Forms.NumericUpDown nudSyncopationNN;
    internal System.Windows.Forms.NumericUpDown nudSyncopationDD;
    private System.Windows.Forms.Label lblnudSyncopationNote;
    private System.Windows.Forms.Label lblReload;
    private System.Windows.Forms.Button cmdResetPlay;
    internal System.Windows.Forms.CheckBox chkLoadMM;
    internal System.Windows.Forms.CheckBox chkConstantChordPlayMidiKB;
    internal System.Windows.Forms.NumericUpDown nudNotesPerChordPlay;
    private System.Windows.Forms.Label lblnudNotesPerChordPlay;
    private System.Windows.Forms.GroupBox grpChordSetMinorKey;
    internal System.Windows.Forms.RadioButton optMinorMelodicDown;
    internal System.Windows.Forms.RadioButton optMinorMelodicUp;
    internal System.Windows.Forms.RadioButton optMinorHarmonic;
    internal System.Windows.Forms.RadioButton optMinorSpecial;
    private System.Windows.Forms.OpenFileDialog ofd;
    internal System.Windows.Forms.SaveFileDialog sfd;
    private System.Windows.Forms.FolderBrowserDialog fbd;
    private System.Windows.Forms.Label lblnudSyncopation;
    private System.Windows.Forms.Label lblAutoSyncChordDelayMS;
    private System.Windows.Forms.Label lblAutoSyncChordDelay;
    internal System.Windows.Forms.NumericUpDown nudAutoSyncChordDelay;
    internal System.Windows.Forms.CheckBox chkAllNotesOffAfterSustain;
    private System.Windows.Forms.GroupBox grpMidiPlayAdvanced;
    internal System.Windows.Forms.CheckBox chkFilterMidiBank;
    internal System.Windows.Forms.CheckBox chkKBChordMatch;
    internal System.Windows.Forms.CheckBox chkAutoRecChan;
    private System.Windows.Forms.Button cmdHelp;
    internal System.Windows.Forms.GroupBox grpFiles;
    internal System.Windows.Forms.Label lblAudLoad;
    internal System.Windows.Forms.Label lblLyrLoad;
    internal System.Windows.Forms.Label lblChtLoad;
    internal System.Windows.Forms.Label lblMidLoad;
    internal System.Windows.Forms.Label lblChpLoad;
    private System.Windows.Forms.Label lblAudLoadLit;
    private System.Windows.Forms.Label lblLyrLoadLit;
    private System.Windows.Forms.Label lblChtLoadLit;
    private System.Windows.Forms.Label lblMidLoadLit;
    private System.Windows.Forms.Label lblChpLoadLit;
    internal System.Windows.Forms.CheckBox chkTTActive;
    private System.Windows.Forms.Button cmdRenameIni;
    private System.Windows.Forms.Button cmdRestoreIni;
    internal System.Windows.Forms.NumericUpDown nudTPDI;
    private System.Windows.Forms.Label lblnudTPDI;
    internal System.Windows.Forms.NumericUpDown nudPlayResolution;
    private System.Windows.Forms.Label lblnudPlayResolution;
    internal System.Windows.Forms.NumericUpDown nudDefaultSongLength;
    private System.Windows.Forms.Label lblnudDefaultSongLength;
    internal System.Windows.Forms.CheckBox chkExitPrompt;
    internal System.Windows.Forms.NumericUpDown nudUndoRedoCapacity;
    private System.Windows.Forms.Label lblUndoRedoCapacity;
    private System.Windows.Forms.Button cmdChordCfg;
    private System.Windows.Forms.Button cmdChordRanks;
    private System.Windows.Forms.GroupBox grpOther;
    internal System.Windows.Forms.CheckBox chkNoAudioSync;
    private System.Windows.Forms.Label lbllitSyncopation;
    internal System.Windows.Forms.CheckBox chkConstantChordPlayPCKB;
    internal System.Windows.Forms.CheckBox chkDisablePCKB;
  }
}