namespace ChordCadenza.Forms {
  partial class frmTrackMap {
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      this.cmdYNeg = new System.Windows.Forms.Button();
      this.cmdYPos = new System.Windows.Forms.Button();
      this.cmdXNeg = new System.Windows.Forms.Button();
      this.cmdXPos = new System.Windows.Forms.Button();
      this.cmdPlayMidi = new System.Windows.Forms.Button();
      this.chkShowKB = new System.Windows.Forms.CheckBox();
      this.chkShowBeats = new System.Windows.Forms.CheckBox();
      this.splitContainer = new System.Windows.Forms.SplitContainer();
      this.cmdPausePlay = new System.Windows.Forms.Button();
      this.cmdGoToStart = new System.Windows.Forms.Button();
      this.optVol = new System.Windows.Forms.RadioButton();
      this.optPan = new System.Windows.Forms.RadioButton();
      this.cmdStopPlay = new System.Windows.Forms.Button();
      this.panChecks = new System.Windows.Forms.Panel();
      this.cmdTrkStyles = new System.Windows.Forms.Button();
      this.cmdCheckAll = new System.Windows.Forms.Button();
      this.cmdClear = new System.Windows.Forms.Button();
      this.cmdUncheckAll = new System.Windows.Forms.Button();
      this.cmdUpdateLyrics = new System.Windows.Forms.Button();
      this.cmdPlayAndRecordAudio = new System.Windows.Forms.Button();
      this.cmdSyncAudio = new System.Windows.Forms.Button();
      this.cmdPlayAudio = new System.Windows.Forms.Button();
      this.cmdPanic = new System.Windows.Forms.Button();
      this.panTop = new System.Windows.Forms.Panel();
      this.cmdAllUp = new System.Windows.Forms.Button();
      this.cmdAllDown = new System.Windows.Forms.Button();
      this.panel6 = new System.Windows.Forms.Panel();
      this.chkOneOctave = new System.Windows.Forms.CheckBox();
      this.chkShowLyrics = new System.Windows.Forms.CheckBox();
      this.chkEvenSpacing = new System.Windows.Forms.CheckBox();
      this.panEdit = new System.Windows.Forms.Panel();
      this.cmdRedo = new System.Windows.Forms.Button();
      this.cmdUndo = new System.Windows.Forms.Button();
      this.cmdAddTrack = new System.Windows.Forms.Button();
      this.cmdDeleteNotes = new System.Windows.Forms.Button();
      this.cmdSaveMidi = new System.Windows.Forms.Button();
      this.cmdCalcKeys = new System.Windows.Forms.Button();
      this.cmdTonnetz = new System.Windows.Forms.Button();
      this.cmdShowAudioSyncWindow = new System.Windows.Forms.Button();
      this.cmdChordMap = new System.Windows.Forms.Button();
      this.cmdSummary = new System.Windows.Forms.Button();
      this.lblTicks = new System.Windows.Forms.Label();
      this.lblLitTicks = new System.Windows.Forms.Label();
      this.picBars = new System.Windows.Forms.PictureBox();
      this.lblLyricsLit = new System.Windows.Forms.Label();
      this.dgvLyrics = new System.Windows.Forms.DataGridView();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.lblBarsLit = new System.Windows.Forms.Label();
      this.cmdSaveProjectAs = new System.Windows.Forms.Button();
      this.cmdSaveProject = new System.Windows.Forms.Button();
      this.cmdNew = new System.Windows.Forms.Button();
      this.cmdLoadProject = new System.Windows.Forms.Button();
      this.panMisc = new System.Windows.Forms.Panel();
      this.panFiles = new System.Windows.Forms.Panel();
      this.panPlay = new System.Windows.Forms.Panel();
      this.PanForms = new System.Windows.Forms.Panel();
      this.cmdUncollapseAll = new System.Windows.Forms.Button();
      this.cmdCollapseAll = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
      this.splitContainer.SuspendLayout();
      this.panChecks.SuspendLayout();
      this.panTop.SuspendLayout();
      this.panel6.SuspendLayout();
      this.panEdit.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picBars)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvLyrics)).BeginInit();
      this.panMisc.SuspendLayout();
      this.panFiles.SuspendLayout();
      this.panPlay.SuspendLayout();
      this.PanForms.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmdYNeg
      // 
      this.cmdYNeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdYNeg.Location = new System.Drawing.Point(650, 89);
      this.cmdYNeg.Name = "cmdYNeg";
      this.cmdYNeg.Size = new System.Drawing.Size(29, 27);
      this.cmdYNeg.TabIndex = 4;
      this.cmdYNeg.Text = "-";
      this.cmdYNeg.UseVisualStyleBackColor = true;
      this.cmdYNeg.Click += new System.EventHandler(this.cmdYNeg_Click);
      // 
      // cmdYPos
      // 
      this.cmdYPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdYPos.Location = new System.Drawing.Point(650, 62);
      this.cmdYPos.Name = "cmdYPos";
      this.cmdYPos.Size = new System.Drawing.Size(29, 27);
      this.cmdYPos.TabIndex = 3;
      this.cmdYPos.Text = "+";
      this.cmdYPos.UseVisualStyleBackColor = true;
      this.cmdYPos.Click += new System.EventHandler(this.cmdYPos_Click);
      // 
      // cmdXNeg
      // 
      this.cmdXNeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdXNeg.Location = new System.Drawing.Point(714, 62);
      this.cmdXNeg.Name = "cmdXNeg";
      this.cmdXNeg.Size = new System.Drawing.Size(29, 27);
      this.cmdXNeg.TabIndex = 4;
      this.cmdXNeg.Text = "-";
      this.cmdXNeg.UseVisualStyleBackColor = true;
      this.cmdXNeg.Click += new System.EventHandler(this.cmdXNeg_Click);
      // 
      // cmdXPos
      // 
      this.cmdXPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdXPos.Location = new System.Drawing.Point(683, 62);
      this.cmdXPos.Name = "cmdXPos";
      this.cmdXPos.Size = new System.Drawing.Size(29, 27);
      this.cmdXPos.TabIndex = 3;
      this.cmdXPos.Text = "+";
      this.cmdXPos.UseVisualStyleBackColor = true;
      this.cmdXPos.Click += new System.EventHandler(this.cmdXPos_Click);
      // 
      // cmdPlayMidi
      // 
      this.cmdPlayMidi.Location = new System.Drawing.Point(50, 0);
      this.cmdPlayMidi.Margin = new System.Windows.Forms.Padding(0);
      this.cmdPlayMidi.Name = "cmdPlayMidi";
      this.cmdPlayMidi.Size = new System.Drawing.Size(50, 48);
      this.cmdPlayMidi.TabIndex = 7;
      this.cmdPlayMidi.Text = "Play Midi";
      this.cmdPlayMidi.UseVisualStyleBackColor = true;
      this.cmdPlayMidi.Click += new System.EventHandler(this.cmdPlayMidi_Click);
      // 
      // chkShowKB
      // 
      this.chkShowKB.AutoSize = true;
      this.chkShowKB.Location = new System.Drawing.Point(3, 20);
      this.chkShowKB.Name = "chkShowKB";
      this.chkShowKB.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.chkShowKB.Size = new System.Drawing.Size(101, 17);
      this.chkShowKB.TabIndex = 13;
      this.chkShowKB.Text = "Show Keyboard";
      this.chkShowKB.UseVisualStyleBackColor = true;
      this.chkShowKB.CheckedChanged += new System.EventHandler(this.chkShowKB_CheckedChanged);
      // 
      // chkShowBeats
      // 
      this.chkShowBeats.AutoSize = true;
      this.chkShowBeats.Location = new System.Drawing.Point(3, 3);
      this.chkShowBeats.Name = "chkShowBeats";
      this.chkShowBeats.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.chkShowBeats.Size = new System.Drawing.Size(83, 17);
      this.chkShowBeats.TabIndex = 14;
      this.chkShowBeats.Text = "Show Beats";
      this.chkShowBeats.UseVisualStyleBackColor = true;
      this.chkShowBeats.CheckedChanged += new System.EventHandler(this.chkShowBeats_CheckedChanged);
      // 
      // splitContainer
      // 
      this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer.IsSplitterFixed = true;
      this.splitContainer.Location = new System.Drawing.Point(10, 342);
      this.splitContainer.Name = "splitContainer";
      // 
      // splitContainer.Panel1
      // 
      this.splitContainer.Panel1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      // 
      // splitContainer.Panel2
      // 
      this.splitContainer.Panel2.AutoScroll = true;
      this.splitContainer.Panel2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.splitContainer_Panel2_Scroll);
      this.splitContainer.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer_Panel2_Paint);
      this.splitContainer.Size = new System.Drawing.Size(1219, 291);
      this.splitContainer.SplitterDistance = 310;
      this.splitContainer.TabIndex = 17;
      // 
      // cmdPausePlay
      // 
      this.cmdPausePlay.Enabled = false;
      this.cmdPausePlay.Location = new System.Drawing.Point(300, 0);
      this.cmdPausePlay.Margin = new System.Windows.Forms.Padding(0);
      this.cmdPausePlay.Name = "cmdPausePlay";
      this.cmdPausePlay.Size = new System.Drawing.Size(50, 48);
      this.cmdPausePlay.TabIndex = 18;
      this.cmdPausePlay.Text = "Pause Play";
      this.cmdPausePlay.UseVisualStyleBackColor = true;
      this.cmdPausePlay.Click += new System.EventHandler(this.cmdPausePlay_Click);
      // 
      // cmdGoToStart
      // 
      this.cmdGoToStart.Location = new System.Drawing.Point(0, 0);
      this.cmdGoToStart.Margin = new System.Windows.Forms.Padding(0);
      this.cmdGoToStart.Name = "cmdGoToStart";
      this.cmdGoToStart.Size = new System.Drawing.Size(50, 48);
      this.cmdGoToStart.TabIndex = 19;
      this.cmdGoToStart.Text = "GoTo Start";
      this.cmdGoToStart.UseVisualStyleBackColor = true;
      this.cmdGoToStart.Click += new System.EventHandler(this.cmdGoToStart_Click);
      // 
      // optVol
      // 
      this.optVol.Checked = true;
      this.optVol.Location = new System.Drawing.Point(6, 28);
      this.optVol.Name = "optVol";
      this.optVol.Size = new System.Drawing.Size(44, 17);
      this.optVol.TabIndex = 1;
      this.optVol.TabStop = true;
      this.optVol.Text = "Vol";
      this.optVol.UseVisualStyleBackColor = true;
      this.optVol.CheckedChanged += new System.EventHandler(this.optVol_CheckedChanged);
      // 
      // optPan
      // 
      this.optPan.Location = new System.Drawing.Point(6, 9);
      this.optPan.Name = "optPan";
      this.optPan.Size = new System.Drawing.Size(44, 17);
      this.optPan.TabIndex = 0;
      this.optPan.Text = "Pan ";
      this.optPan.UseVisualStyleBackColor = true;
      // 
      // cmdStopPlay
      // 
      this.cmdStopPlay.Enabled = false;
      this.cmdStopPlay.Location = new System.Drawing.Point(250, 0);
      this.cmdStopPlay.Margin = new System.Windows.Forms.Padding(0);
      this.cmdStopPlay.Name = "cmdStopPlay";
      this.cmdStopPlay.Size = new System.Drawing.Size(50, 48);
      this.cmdStopPlay.TabIndex = 29;
      this.cmdStopPlay.Text = "Stop  Play";
      this.cmdStopPlay.UseVisualStyleBackColor = true;
      this.cmdStopPlay.Click += new System.EventHandler(this.cmdStopPlay_Click);
      // 
      // panChecks
      // 
      this.panChecks.Controls.Add(this.cmdTrkStyles);
      this.panChecks.Controls.Add(this.cmdCheckAll);
      this.panChecks.Controls.Add(this.cmdClear);
      this.panChecks.Controls.Add(this.cmdUncheckAll);
      this.panChecks.Location = new System.Drawing.Point(7, 62);
      this.panChecks.Name = "panChecks";
      this.panChecks.Size = new System.Drawing.Size(207, 48);
      this.panChecks.TabIndex = 30;
      // 
      // cmdTrkStyles
      // 
      this.cmdTrkStyles.Location = new System.Drawing.Point(150, 0);
      this.cmdTrkStyles.Margin = new System.Windows.Forms.Padding(0);
      this.cmdTrkStyles.Name = "cmdTrkStyles";
      this.cmdTrkStyles.Size = new System.Drawing.Size(50, 48);
      this.cmdTrkStyles.TabIndex = 21;
      this.cmdTrkStyles.Text = "Track\r\nStyles";
      this.cmdTrkStyles.UseVisualStyleBackColor = true;
      this.cmdTrkStyles.Click += new System.EventHandler(this.cmdTrkStyles_Click);
      // 
      // cmdCheckAll
      // 
      this.cmdCheckAll.Location = new System.Drawing.Point(0, 0);
      this.cmdCheckAll.Margin = new System.Windows.Forms.Padding(0);
      this.cmdCheckAll.Name = "cmdCheckAll";
      this.cmdCheckAll.Size = new System.Drawing.Size(50, 48);
      this.cmdCheckAll.TabIndex = 11;
      this.cmdCheckAll.Text = "Check  All";
      this.cmdCheckAll.UseVisualStyleBackColor = true;
      this.cmdCheckAll.Click += new System.EventHandler(this.cmdCheckAll_Click);
      // 
      // cmdClear
      // 
      this.cmdClear.Location = new System.Drawing.Point(100, 0);
      this.cmdClear.Margin = new System.Windows.Forms.Padding(0);
      this.cmdClear.Name = "cmdClear";
      this.cmdClear.Size = new System.Drawing.Size(50, 48);
      this.cmdClear.TabIndex = 20;
      this.cmdClear.Text = "Clear\r\nMutes\r\n&& Solos";
      this.cmdClear.UseVisualStyleBackColor = true;
      this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
      // 
      // cmdUncheckAll
      // 
      this.cmdUncheckAll.Location = new System.Drawing.Point(50, 0);
      this.cmdUncheckAll.Margin = new System.Windows.Forms.Padding(0);
      this.cmdUncheckAll.Name = "cmdUncheckAll";
      this.cmdUncheckAll.Size = new System.Drawing.Size(50, 48);
      this.cmdUncheckAll.TabIndex = 12;
      this.cmdUncheckAll.Text = "Unchk All";
      this.cmdUncheckAll.UseVisualStyleBackColor = true;
      this.cmdUncheckAll.Click += new System.EventHandler(this.cmdUncheckAll_Click);
      // 
      // cmdUpdateLyrics
      // 
      this.cmdUpdateLyrics.Location = new System.Drawing.Point(151, 0);
      this.cmdUpdateLyrics.Margin = new System.Windows.Forms.Padding(0);
      this.cmdUpdateLyrics.Name = "cmdUpdateLyrics";
      this.cmdUpdateLyrics.Size = new System.Drawing.Size(50, 48);
      this.cmdUpdateLyrics.TabIndex = 34;
      this.cmdUpdateLyrics.Text = "Edit\r\nLyrics";
      this.cmdUpdateLyrics.UseVisualStyleBackColor = true;
      this.cmdUpdateLyrics.Click += new System.EventHandler(this.cmdUpdateLyrics_Click);
      // 
      // cmdPlayAndRecordAudio
      // 
      this.cmdPlayAndRecordAudio.Location = new System.Drawing.Point(200, 0);
      this.cmdPlayAndRecordAudio.Margin = new System.Windows.Forms.Padding(0);
      this.cmdPlayAndRecordAudio.Name = "cmdPlayAndRecordAudio";
      this.cmdPlayAndRecordAudio.Size = new System.Drawing.Size(50, 48);
      this.cmdPlayAndRecordAudio.TabIndex = 33;
      this.cmdPlayAndRecordAudio.Text = "Play &&\r\nSync\r\nAudio\r\n";
      this.cmdPlayAndRecordAudio.UseVisualStyleBackColor = true;
      this.cmdPlayAndRecordAudio.Click += new System.EventHandler(this.cmdPlayAndRecord_Click);
      // 
      // cmdSyncAudio
      // 
      this.cmdSyncAudio.Location = new System.Drawing.Point(150, 0);
      this.cmdSyncAudio.Margin = new System.Windows.Forms.Padding(0);
      this.cmdSyncAudio.Name = "cmdSyncAudio";
      this.cmdSyncAudio.Size = new System.Drawing.Size(50, 48);
      this.cmdSyncAudio.TabIndex = 32;
      this.cmdSyncAudio.Text = "Sync\r\nAudio";
      this.cmdSyncAudio.UseVisualStyleBackColor = true;
      this.cmdSyncAudio.Click += new System.EventHandler(this.cmdSyncAudio_Click);
      // 
      // cmdPlayAudio
      // 
      this.cmdPlayAudio.Location = new System.Drawing.Point(100, 0);
      this.cmdPlayAudio.Margin = new System.Windows.Forms.Padding(0);
      this.cmdPlayAudio.Name = "cmdPlayAudio";
      this.cmdPlayAudio.Size = new System.Drawing.Size(50, 48);
      this.cmdPlayAudio.TabIndex = 31;
      this.cmdPlayAudio.Text = "Play\r\nAudio";
      this.cmdPlayAudio.UseVisualStyleBackColor = true;
      this.cmdPlayAudio.Click += new System.EventHandler(this.cmdPlayAudio_Click);
      // 
      // cmdPanic
      // 
      this.cmdPanic.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdPanic.Location = new System.Drawing.Point(349, 0);
      this.cmdPanic.Margin = new System.Windows.Forms.Padding(0);
      this.cmdPanic.Name = "cmdPanic";
      this.cmdPanic.Size = new System.Drawing.Size(50, 48);
      this.cmdPanic.TabIndex = 30;
      this.cmdPanic.Text = "!";
      this.cmdPanic.UseVisualStyleBackColor = true;
      this.cmdPanic.Click += new System.EventHandler(this.cmdPanic_Click);
      // 
      // panTop
      // 
      this.panTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panTop.Controls.Add(this.cmdAllUp);
      this.panTop.Controls.Add(this.cmdAllDown);
      this.panTop.Controls.Add(this.optPan);
      this.panTop.Controls.Add(this.optVol);
      this.panTop.Location = new System.Drawing.Point(863, 62);
      this.panTop.Name = "panTop";
      this.panTop.Size = new System.Drawing.Size(96, 58);
      this.panTop.TabIndex = 32;
      // 
      // cmdAllUp
      // 
      this.cmdAllUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdAllUp.Location = new System.Drawing.Point(59, 3);
      this.cmdAllUp.Name = "cmdAllUp";
      this.cmdAllUp.Size = new System.Drawing.Size(29, 22);
      this.cmdAllUp.TabIndex = 35;
      this.cmdAllUp.Text = "+";
      this.cmdAllUp.UseVisualStyleBackColor = true;
      this.cmdAllUp.Click += new System.EventHandler(this.cmdAllUp_Click);
      // 
      // cmdAllDown
      // 
      this.cmdAllDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdAllDown.Location = new System.Drawing.Point(59, 30);
      this.cmdAllDown.Name = "cmdAllDown";
      this.cmdAllDown.Size = new System.Drawing.Size(29, 22);
      this.cmdAllDown.TabIndex = 36;
      this.cmdAllDown.Text = "-";
      this.cmdAllDown.UseVisualStyleBackColor = true;
      this.cmdAllDown.Click += new System.EventHandler(this.cmdAllDown_Click);
      // 
      // panel6
      // 
      this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel6.Controls.Add(this.chkOneOctave);
      this.panel6.Controls.Add(this.chkShowLyrics);
      this.panel6.Controls.Add(this.chkShowBeats);
      this.panel6.Controls.Add(this.chkShowKB);
      this.panel6.Controls.Add(this.chkEvenSpacing);
      this.panel6.Location = new System.Drawing.Point(970, 8);
      this.panel6.Name = "panel6";
      this.panel6.Size = new System.Drawing.Size(132, 94);
      this.panel6.TabIndex = 33;
      // 
      // chkOneOctave
      // 
      this.chkOneOctave.AutoSize = true;
      this.chkOneOctave.Location = new System.Drawing.Point(3, 37);
      this.chkOneOctave.Name = "chkOneOctave";
      this.chkOneOctave.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.chkOneOctave.Size = new System.Drawing.Size(114, 17);
      this.chkOneOctave.TabIndex = 15;
      this.chkOneOctave.Text = "Show One Octave";
      this.chkOneOctave.UseVisualStyleBackColor = true;
      this.chkOneOctave.CheckedChanged += new System.EventHandler(this.chkOneOctave_CheckedChanged);
      // 
      // chkShowLyrics
      // 
      this.chkShowLyrics.AutoSize = true;
      this.chkShowLyrics.Checked = true;
      this.chkShowLyrics.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkShowLyrics.Location = new System.Drawing.Point(3, 54);
      this.chkShowLyrics.Name = "chkShowLyrics";
      this.chkShowLyrics.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.chkShowLyrics.Size = new System.Drawing.Size(83, 17);
      this.chkShowLyrics.TabIndex = 117;
      this.chkShowLyrics.Text = "Show Lyrics";
      this.chkShowLyrics.UseVisualStyleBackColor = true;
      this.chkShowLyrics.CheckedChanged += new System.EventHandler(this.chkShowLyrics_CheckedChanged);
      // 
      // chkEvenSpacing
      // 
      this.chkEvenSpacing.AutoSize = true;
      this.chkEvenSpacing.Location = new System.Drawing.Point(3, 71);
      this.chkEvenSpacing.Name = "chkEvenSpacing";
      this.chkEvenSpacing.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.chkEvenSpacing.Size = new System.Drawing.Size(112, 17);
      this.chkEvenSpacing.TabIndex = 131;
      this.chkEvenSpacing.Text = "Even Trk Spacing";
      this.chkEvenSpacing.UseVisualStyleBackColor = true;
      this.chkEvenSpacing.CheckedChanged += new System.EventHandler(this.chkEvenSpacing_CheckedChanged);
      // 
      // panEdit
      // 
      this.panEdit.Controls.Add(this.cmdRedo);
      this.panEdit.Controls.Add(this.cmdUndo);
      this.panEdit.Controls.Add(this.cmdAddTrack);
      this.panEdit.Controls.Add(this.cmdDeleteNotes);
      this.panEdit.Location = new System.Drawing.Point(331, 62);
      this.panEdit.Name = "panEdit";
      this.panEdit.Size = new System.Drawing.Size(205, 48);
      this.panEdit.TabIndex = 34;
      // 
      // cmdRedo
      // 
      this.cmdRedo.Enabled = false;
      this.cmdRedo.Location = new System.Drawing.Point(147, 0);
      this.cmdRedo.Margin = new System.Windows.Forms.Padding(0);
      this.cmdRedo.Name = "cmdRedo";
      this.cmdRedo.Size = new System.Drawing.Size(50, 48);
      this.cmdRedo.TabIndex = 45;
      this.cmdRedo.Text = "Redo";
      this.cmdRedo.UseVisualStyleBackColor = true;
      this.cmdRedo.Click += new System.EventHandler(this.cmdRedo_Click);
      // 
      // cmdUndo
      // 
      this.cmdUndo.Enabled = false;
      this.cmdUndo.Location = new System.Drawing.Point(98, 0);
      this.cmdUndo.Margin = new System.Windows.Forms.Padding(0);
      this.cmdUndo.Name = "cmdUndo";
      this.cmdUndo.Size = new System.Drawing.Size(50, 48);
      this.cmdUndo.TabIndex = 44;
      this.cmdUndo.Text = "Undo";
      this.cmdUndo.UseVisualStyleBackColor = true;
      this.cmdUndo.Click += new System.EventHandler(this.cmdUndo_Click);
      // 
      // cmdAddTrack
      // 
      this.cmdAddTrack.Location = new System.Drawing.Point(49, 0);
      this.cmdAddTrack.Margin = new System.Windows.Forms.Padding(0);
      this.cmdAddTrack.Name = "cmdAddTrack";
      this.cmdAddTrack.Size = new System.Drawing.Size(50, 48);
      this.cmdAddTrack.TabIndex = 43;
      this.cmdAddTrack.Text = "Add Track";
      this.cmdAddTrack.UseVisualStyleBackColor = true;
      this.cmdAddTrack.Click += new System.EventHandler(this.cmdAddTrack_Click);
      // 
      // cmdDeleteNotes
      // 
      this.cmdDeleteNotes.Enabled = false;
      this.cmdDeleteNotes.Location = new System.Drawing.Point(0, 0);
      this.cmdDeleteNotes.Margin = new System.Windows.Forms.Padding(0);
      this.cmdDeleteNotes.Name = "cmdDeleteNotes";
      this.cmdDeleteNotes.Size = new System.Drawing.Size(50, 48);
      this.cmdDeleteNotes.TabIndex = 37;
      this.cmdDeleteNotes.Text = "Delete Notes";
      this.cmdDeleteNotes.UseVisualStyleBackColor = true;
      this.cmdDeleteNotes.Click += new System.EventHandler(this.cmdDeleteNotes_Click);
      // 
      // cmdSaveMidi
      // 
      this.cmdSaveMidi.Location = new System.Drawing.Point(52, 0);
      this.cmdSaveMidi.Margin = new System.Windows.Forms.Padding(0);
      this.cmdSaveMidi.Name = "cmdSaveMidi";
      this.cmdSaveMidi.Size = new System.Drawing.Size(50, 48);
      this.cmdSaveMidi.TabIndex = 46;
      this.cmdSaveMidi.Text = "Save\r\nMidiFile\r\nAs...";
      this.cmdSaveMidi.UseVisualStyleBackColor = true;
      this.cmdSaveMidi.Click += new System.EventHandler(this.cmdSaveMidi_Click);
      // 
      // cmdCalcKeys
      // 
      this.cmdCalcKeys.Location = new System.Drawing.Point(1, 0);
      this.cmdCalcKeys.Margin = new System.Windows.Forms.Padding(0);
      this.cmdCalcKeys.Name = "cmdCalcKeys";
      this.cmdCalcKeys.Size = new System.Drawing.Size(50, 48);
      this.cmdCalcKeys.TabIndex = 23;
      this.cmdCalcKeys.Text = "Calc Keys";
      this.cmdCalcKeys.UseVisualStyleBackColor = true;
      this.cmdCalcKeys.Click += new System.EventHandler(this.cmdCalcKeys_Click);
      // 
      // cmdTonnetz
      // 
      this.cmdTonnetz.ForeColor = System.Drawing.Color.Red;
      this.cmdTonnetz.Location = new System.Drawing.Point(201, 0);
      this.cmdTonnetz.Margin = new System.Windows.Forms.Padding(0);
      this.cmdTonnetz.Name = "cmdTonnetz";
      this.cmdTonnetz.Size = new System.Drawing.Size(54, 48);
      this.cmdTonnetz.TabIndex = 42;
      this.cmdTonnetz.Text = "Tonnetz";
      this.cmdTonnetz.UseVisualStyleBackColor = true;
      this.cmdTonnetz.Click += new System.EventHandler(this.cmdTonnetz_Click);
      // 
      // cmdShowAudioSyncWindow
      // 
      this.cmdShowAudioSyncWindow.Location = new System.Drawing.Point(50, 0);
      this.cmdShowAudioSyncWindow.Margin = new System.Windows.Forms.Padding(0);
      this.cmdShowAudioSyncWindow.Name = "cmdShowAudioSyncWindow";
      this.cmdShowAudioSyncWindow.Size = new System.Drawing.Size(50, 48);
      this.cmdShowAudioSyncWindow.TabIndex = 41;
      this.cmdShowAudioSyncWindow.Text = "Audio\r\nSync\r\nConfig";
      this.cmdShowAudioSyncWindow.UseVisualStyleBackColor = true;
      this.cmdShowAudioSyncWindow.Click += new System.EventHandler(this.cmdShowAudioSyncWindow_Click);
      // 
      // cmdChordMap
      // 
      this.cmdChordMap.Location = new System.Drawing.Point(0, 0);
      this.cmdChordMap.Margin = new System.Windows.Forms.Padding(0);
      this.cmdChordMap.Name = "cmdChordMap";
      this.cmdChordMap.Size = new System.Drawing.Size(50, 48);
      this.cmdChordMap.TabIndex = 25;
      this.cmdChordMap.Text = "Chord Map";
      this.cmdChordMap.UseVisualStyleBackColor = true;
      this.cmdChordMap.Click += new System.EventHandler(this.cmdChordMap_Click);
      // 
      // cmdSummary
      // 
      this.cmdSummary.Location = new System.Drawing.Point(101, 0);
      this.cmdSummary.Margin = new System.Windows.Forms.Padding(0);
      this.cmdSummary.Name = "cmdSummary";
      this.cmdSummary.Size = new System.Drawing.Size(50, 48);
      this.cmdSummary.TabIndex = 24;
      this.cmdSummary.Text = "Show\r\nSumm";
      this.cmdSummary.UseVisualStyleBackColor = true;
      this.cmdSummary.Click += new System.EventHandler(this.cmdSummary_Click);
      // 
      // lblTicks
      // 
      this.lblTicks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblTicks.Location = new System.Drawing.Point(787, 95);
      this.lblTicks.Name = "lblTicks";
      this.lblTicks.Size = new System.Drawing.Size(49, 20);
      this.lblTicks.TabIndex = 36;
      this.lblTicks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblLitTicks
      // 
      this.lblLitTicks.AutoSize = true;
      this.lblLitTicks.Location = new System.Drawing.Point(752, 98);
      this.lblLitTicks.Name = "lblLitTicks";
      this.lblLitTicks.Size = new System.Drawing.Size(33, 13);
      this.lblLitTicks.TabIndex = 35;
      this.lblLitTicks.Text = "Ticks";
      // 
      // picBars
      // 
      this.picBars.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.picBars.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picBars.Location = new System.Drawing.Point(330, 128);
      this.picBars.Name = "picBars";
      this.picBars.Size = new System.Drawing.Size(899, 20);
      this.picBars.TabIndex = 36;
      this.picBars.TabStop = false;
      this.picBars.Paint += new System.Windows.Forms.PaintEventHandler(this.picBars_Paint);
      this.picBars.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBars_MouseClick);
      // 
      // lblLyricsLit
      // 
      this.lblLyricsLit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lblLyricsLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLyricsLit.Location = new System.Drawing.Point(268, 288);
      this.lblLyricsLit.Name = "lblLyricsLit";
      this.lblLyricsLit.Size = new System.Drawing.Size(58, 27);
      this.lblLyricsLit.TabIndex = 115;
      this.lblLyricsLit.Text = "Lyrics";
      this.lblLyricsLit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lblLyricsLit.Visible = false;
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
      this.dgvLyrics.Location = new System.Drawing.Point(330, 288);
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
      this.dgvLyrics.Size = new System.Drawing.Size(899, 48);
      this.dgvLyrics.TabIndex = 114;
      this.dgvLyrics.Visible = false;
      this.dgvLyrics.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLyrics_CellClick);
      this.dgvLyrics.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvLyrics_ColumnAdded);
      this.dgvLyrics.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvLyrics_Scroll);
      // 
      // cmdHelp
      // 
      this.cmdHelp.Location = new System.Drawing.Point(909, 8);
      this.cmdHelp.Margin = new System.Windows.Forms.Padding(0);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(50, 48);
      this.cmdHelp.TabIndex = 126;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // lblBarsLit
      // 
      this.lblBarsLit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lblBarsLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblBarsLit.Location = new System.Drawing.Point(286, 124);
      this.lblBarsLit.Name = "lblBarsLit";
      this.lblBarsLit.Size = new System.Drawing.Size(40, 27);
      this.lblBarsLit.TabIndex = 127;
      this.lblBarsLit.Text = "Bar";
      this.lblBarsLit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // cmdSaveProjectAs
      // 
      this.cmdSaveProjectAs.Location = new System.Drawing.Point(150, 0);
      this.cmdSaveProjectAs.Name = "cmdSaveProjectAs";
      this.cmdSaveProjectAs.Size = new System.Drawing.Size(50, 48);
      this.cmdSaveProjectAs.TabIndex = 248;
      this.cmdSaveProjectAs.Text = "Save\r\nProject\r\nAs...";
      this.cmdSaveProjectAs.UseVisualStyleBackColor = true;
      this.cmdSaveProjectAs.Click += new System.EventHandler(this.cmdSaveProjectAs_Click);
      // 
      // cmdSaveProject
      // 
      this.cmdSaveProject.Enabled = false;
      this.cmdSaveProject.Location = new System.Drawing.Point(100, 0);
      this.cmdSaveProject.Name = "cmdSaveProject";
      this.cmdSaveProject.Size = new System.Drawing.Size(50, 48);
      this.cmdSaveProject.TabIndex = 247;
      this.cmdSaveProject.Text = "Save Project";
      this.cmdSaveProject.UseVisualStyleBackColor = true;
      this.cmdSaveProject.Click += new System.EventHandler(this.cmdSaveProject_Click);
      this.cmdSaveProject.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cmdSaveProject_MouseUp);
      // 
      // cmdNew
      // 
      this.cmdNew.Location = new System.Drawing.Point(0, 0);
      this.cmdNew.Name = "cmdNew";
      this.cmdNew.Size = new System.Drawing.Size(50, 48);
      this.cmdNew.TabIndex = 246;
      this.cmdNew.Text = "New Project";
      this.cmdNew.UseVisualStyleBackColor = true;
      this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
      // 
      // cmdLoadProject
      // 
      this.cmdLoadProject.Location = new System.Drawing.Point(50, 0);
      this.cmdLoadProject.Name = "cmdLoadProject";
      this.cmdLoadProject.Size = new System.Drawing.Size(50, 48);
      this.cmdLoadProject.TabIndex = 245;
      this.cmdLoadProject.Text = "Load Project";
      this.cmdLoadProject.UseVisualStyleBackColor = true;
      this.cmdLoadProject.Click += new System.EventHandler(this.cmdLoadProject_Click);
      // 
      // panMisc
      // 
      this.panMisc.Controls.Add(this.cmdSaveMidi);
      this.panMisc.Controls.Add(this.cmdCalcKeys);
      this.panMisc.Location = new System.Drawing.Point(220, 62);
      this.panMisc.Name = "panMisc";
      this.panMisc.Size = new System.Drawing.Size(106, 48);
      this.panMisc.TabIndex = 249;
      // 
      // panFiles
      // 
      this.panFiles.Controls.Add(this.cmdNew);
      this.panFiles.Controls.Add(this.cmdLoadProject);
      this.panFiles.Controls.Add(this.cmdSaveProject);
      this.panFiles.Controls.Add(this.cmdSaveProjectAs);
      this.panFiles.Location = new System.Drawing.Point(7, 9);
      this.panFiles.Name = "panFiles";
      this.panFiles.Size = new System.Drawing.Size(207, 48);
      this.panFiles.TabIndex = 250;
      // 
      // panPlay
      // 
      this.panPlay.Controls.Add(this.cmdGoToStart);
      this.panPlay.Controls.Add(this.cmdPlayMidi);
      this.panPlay.Controls.Add(this.cmdPlayAudio);
      this.panPlay.Controls.Add(this.cmdSyncAudio);
      this.panPlay.Controls.Add(this.cmdPanic);
      this.panPlay.Controls.Add(this.cmdStopPlay);
      this.panPlay.Controls.Add(this.cmdPlayAndRecordAudio);
      this.panPlay.Controls.Add(this.cmdPausePlay);
      this.panPlay.Location = new System.Drawing.Point(220, 8);
      this.panPlay.Name = "panPlay";
      this.panPlay.Size = new System.Drawing.Size(407, 48);
      this.panPlay.TabIndex = 251;
      // 
      // PanForms
      // 
      this.PanForms.Controls.Add(this.cmdChordMap);
      this.PanForms.Controls.Add(this.cmdSummary);
      this.PanForms.Controls.Add(this.cmdShowAudioSyncWindow);
      this.PanForms.Controls.Add(this.cmdUpdateLyrics);
      this.PanForms.Controls.Add(this.cmdTonnetz);
      this.PanForms.Location = new System.Drawing.Point(634, 8);
      this.PanForms.Name = "PanForms";
      this.PanForms.Size = new System.Drawing.Size(263, 48);
      this.PanForms.TabIndex = 252;
      // 
      // cmdUncollapseAll
      // 
      this.cmdUncollapseAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdUncollapseAll.Image = global::ChordCadenza.Properties.Resources.GlyphDown_16x;
      this.cmdUncollapseAll.Location = new System.Drawing.Point(573, 72);
      this.cmdUncollapseAll.Name = "cmdUncollapseAll";
      this.cmdUncollapseAll.Size = new System.Drawing.Size(29, 27);
      this.cmdUncollapseAll.TabIndex = 255;
      this.cmdUncollapseAll.UseVisualStyleBackColor = true;
      this.cmdUncollapseAll.Click += new System.EventHandler(this.cmdUncollapseAll_Click);
      // 
      // cmdCollapseAll
      // 
      this.cmdCollapseAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdCollapseAll.Image = global::ChordCadenza.Properties.Resources.GlyphUp_16x;
      this.cmdCollapseAll.Location = new System.Drawing.Point(542, 72);
      this.cmdCollapseAll.Name = "cmdCollapseAll";
      this.cmdCollapseAll.Size = new System.Drawing.Size(29, 27);
      this.cmdCollapseAll.TabIndex = 254;
      this.cmdCollapseAll.UseVisualStyleBackColor = true;
      this.cmdCollapseAll.Click += new System.EventHandler(this.cmdCollapseAll_Click);
      // 
      // frmTrackMap
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(1232, 642);
      this.Controls.Add(this.cmdUncollapseAll);
      this.Controls.Add(this.cmdCollapseAll);
      this.Controls.Add(this.PanForms);
      this.Controls.Add(this.panPlay);
      this.Controls.Add(this.panFiles);
      this.Controls.Add(this.panMisc);
      this.Controls.Add(this.lblBarsLit);
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.cmdXNeg);
      this.Controls.Add(this.cmdYNeg);
      this.Controls.Add(this.cmdXPos);
      this.Controls.Add(this.cmdYPos);
      this.Controls.Add(this.lblLyricsLit);
      this.Controls.Add(this.dgvLyrics);
      this.Controls.Add(this.picBars);
      this.Controls.Add(this.panEdit);
      this.Controls.Add(this.lblTicks);
      this.Controls.Add(this.lblLitTicks);
      this.Controls.Add(this.panel6);
      this.Controls.Add(this.panTop);
      this.Controls.Add(this.panChecks);
      this.Controls.Add(this.splitContainer);
      this.KeyPreview = true;
      this.Name = "frmTrackMap";
      this.Text = "TRACKMAP";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMultiMap_FormClosing);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMultiMap_FormClosed);
      this.Load += new System.EventHandler(this.frmMultiMap_Load);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmMultiMap_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmMultiMap_DragEnter);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTrackMap_KeyDown);
      this.Resize += new System.EventHandler(this.frmMultiMap_Resize);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
      this.splitContainer.ResumeLayout(false);
      this.panChecks.ResumeLayout(false);
      this.panTop.ResumeLayout(false);
      this.panel6.ResumeLayout(false);
      this.panel6.PerformLayout();
      this.panEdit.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.picBars)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvLyrics)).EndInit();
      this.panMisc.ResumeLayout(false);
      this.panFiles.ResumeLayout(false);
      this.panPlay.ResumeLayout(false);
      this.PanForms.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button cmdYNeg;
    private System.Windows.Forms.Button cmdYPos;
    private System.Windows.Forms.Button cmdXNeg;
    private System.Windows.Forms.Button cmdXPos;
    private System.Windows.Forms.Button cmdPlayMidi;
    private System.Windows.Forms.SplitContainer splitContainer;
    private System.Windows.Forms.Button cmdPausePlay;
    private System.Windows.Forms.Button cmdGoToStart;
    private System.Windows.Forms.Button cmdStopPlay;
    private System.Windows.Forms.Panel panChecks;
    private System.Windows.Forms.Button cmdCheckAll;
    private System.Windows.Forms.Button cmdUncheckAll;
    private System.Windows.Forms.Button cmdClear;
    private System.Windows.Forms.Panel panTop;
    private System.Windows.Forms.Panel panel6;
    private System.Windows.Forms.Panel panEdit;
    private System.Windows.Forms.Button cmdCalcKeys;
    private System.Windows.Forms.Button cmdSummary;
    private System.Windows.Forms.Button cmdAllUp;
    private System.Windows.Forms.Button cmdAllDown;
    private System.Windows.Forms.Button cmdPanic;
    private System.Windows.Forms.Label lblLitTicks;
    private System.Windows.Forms.Label lblTicks;
    internal System.Windows.Forms.CheckBox chkOneOctave;
    internal System.Windows.Forms.CheckBox chkShowBeats;
    internal System.Windows.Forms.CheckBox chkShowKB;
    protected System.Windows.Forms.Button cmdChordMap;
    protected System.Windows.Forms.Button cmdDeleteNotes;
    protected System.Windows.Forms.Button cmdShowAudioSyncWindow;
    internal System.Windows.Forms.DataGridView dgvLyrics;
    internal System.Windows.Forms.CheckBox chkShowLyrics;
    protected System.Windows.Forms.Button cmdHelp;
    internal System.Windows.Forms.Label lblLyricsLit;
    internal System.Windows.Forms.Button cmdSyncAudio;
    internal System.Windows.Forms.Button cmdPlayAudio;
    internal System.Windows.Forms.Label lblBarsLit;
    internal System.Windows.Forms.PictureBox picBars;
    protected System.Windows.Forms.Button cmdTonnetz;
    protected System.Windows.Forms.Button cmdAddTrack;
    protected System.Windows.Forms.Button cmdRedo;
    protected System.Windows.Forms.Button cmdUndo;
    internal System.Windows.Forms.RadioButton optVol;
    internal System.Windows.Forms.RadioButton optPan;
    protected System.Windows.Forms.Button cmdSaveMidi;
    internal System.Windows.Forms.CheckBox chkEvenSpacing;
    internal System.Windows.Forms.Button cmdPlayAndRecordAudio;
    private System.Windows.Forms.Button cmdUpdateLyrics;
    internal System.Windows.Forms.Button cmdSaveProjectAs;
    internal System.Windows.Forms.Button cmdSaveProject;
    internal System.Windows.Forms.Button cmdNew;
    internal System.Windows.Forms.Button cmdLoadProject;
    private System.Windows.Forms.Panel panMisc;
    private System.Windows.Forms.Panel panFiles;
    private System.Windows.Forms.Panel panPlay;
    private System.Windows.Forms.Panel PanForms;
    private System.Windows.Forms.Button cmdUncollapseAll;
    private System.Windows.Forms.Button cmdCollapseAll;
    private System.Windows.Forms.Button cmdTrkStyles;
  }
}