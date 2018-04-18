namespace ChordCadenza.Forms {
  partial class frmAutoSync {
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
      this.components = new System.ComponentModel.Container();
      this.cmdShow = new System.Windows.Forms.Button();
      this.cmdMerge = new System.Windows.Forms.Button();
      this.cmdSave = new System.Windows.Forms.Button();
      this.cmdReset = new System.Windows.Forms.Button();
      this.cmdResetActive = new System.Windows.Forms.Button();
      this.cmdMoveActive = new System.Windows.Forms.Button();
      this.clbEla = new System.Windows.Forms.ListBox();
      this.txtTitle = new System.Windows.Forms.TextBox();
      this.trkPos = new System.Windows.Forms.TrackBar();
      this.lbltrkPos = new System.Windows.Forms.Label();
      this.lblLenBytes = new System.Windows.Forms.Label();
      this.lblDelimBytes = new System.Windows.Forms.Label();
      this.lblPosBytes = new System.Windows.Forms.Label();
      this.lblDelimTime = new System.Windows.Forms.Label();
      this.lblLenTime = new System.Windows.Forms.Label();
      this.chkStartRecPos = new System.Windows.Forms.CheckBox();
      this.lblPosTime = new System.Windows.Forms.Label();
      this.panPos = new System.Windows.Forms.Panel();
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSelected = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSelRemoveRecord = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSelRemovePlay = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSelCopy = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuInterpolatePlay = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuInterpolateRecord = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuPlay = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuInsertHoleNew = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuRemoveCloseHoleNew = new System.Windows.Forms.ToolStripMenuItem();
      this.cmdUndo = new System.Windows.Forms.Button();
      this.cmdRedo = new System.Windows.Forms.Button();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.cmdClose = new System.Windows.Forms.Button();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.panCmds = new System.Windows.Forms.Panel();
      this.cmdOffsetBeats = new System.Windows.Forms.Button();
      this.cmdOffsetTimes = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.trkPos)).BeginInit();
      this.panPos.SuspendLayout();
      this.mnuMain.SuspendLayout();
      this.panCmds.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmdShow
      // 
      this.cmdShow.Location = new System.Drawing.Point(6, 4);
      this.cmdShow.Name = "cmdShow";
      this.cmdShow.Size = new System.Drawing.Size(149, 32);
      this.cmdShow.TabIndex = 2;
      this.cmdShow.Text = "Update Display";
      this.cmdShow.UseVisualStyleBackColor = true;
      this.cmdShow.Click += new System.EventHandler(this.cmdShow_Click);
      // 
      // cmdMerge
      // 
      this.cmdMerge.Location = new System.Drawing.Point(5, 42);
      this.cmdMerge.Name = "cmdMerge";
      this.cmdMerge.Size = new System.Drawing.Size(149, 32);
      this.cmdMerge.TabIndex = 3;
      this.cmdMerge.Text = "Merge Record To Play";
      this.cmdMerge.UseVisualStyleBackColor = true;
      this.cmdMerge.Click += new System.EventHandler(this.Merge_Click);
      // 
      // cmdSave
      // 
      this.cmdSave.Location = new System.Drawing.Point(5, 376);
      this.cmdSave.Name = "cmdSave";
      this.cmdSave.Size = new System.Drawing.Size(149, 32);
      this.cmdSave.TabIndex = 4;
      this.cmdSave.Text = "Save To File";
      this.cmdSave.UseVisualStyleBackColor = true;
      this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
      // 
      // cmdReset
      // 
      this.cmdReset.Location = new System.Drawing.Point(6, 163);
      this.cmdReset.Name = "cmdReset";
      this.cmdReset.Size = new System.Drawing.Size(149, 32);
      this.cmdReset.TabIndex = 5;
      this.cmdReset.Text = "Clear All";
      this.cmdReset.UseVisualStyleBackColor = true;
      this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
      // 
      // cmdResetActive
      // 
      this.cmdResetActive.Location = new System.Drawing.Point(6, 80);
      this.cmdResetActive.Name = "cmdResetActive";
      this.cmdResetActive.Size = new System.Drawing.Size(149, 32);
      this.cmdResetActive.TabIndex = 6;
      this.cmdResetActive.Text = "Clear Record";
      this.cmdResetActive.UseVisualStyleBackColor = true;
      this.cmdResetActive.Click += new System.EventHandler(this.cmdResetActive_Click);
      // 
      // cmdMoveActive
      // 
      this.cmdMoveActive.Location = new System.Drawing.Point(5, 118);
      this.cmdMoveActive.Name = "cmdMoveActive";
      this.cmdMoveActive.Size = new System.Drawing.Size(149, 39);
      this.cmdMoveActive.TabIndex = 9;
      this.cmdMoveActive.Text = "Clear Play and\r\nMove Record To Play";
      this.cmdMoveActive.UseVisualStyleBackColor = true;
      this.cmdMoveActive.Click += new System.EventHandler(this.cmdMoveActive_Click);
      // 
      // clbEla
      // 
      this.clbEla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.clbEla.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.clbEla.FormattingEnabled = true;
      this.clbEla.ItemHeight = 15;
      this.clbEla.Location = new System.Drawing.Point(16, 77);
      this.clbEla.Name = "clbEla";
      this.clbEla.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.clbEla.Size = new System.Drawing.Size(689, 424);
      this.clbEla.TabIndex = 30;
      // 
      // txtTitle
      // 
      this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTitle.BackColor = System.Drawing.SystemColors.Window;
      this.txtTitle.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtTitle.Location = new System.Drawing.Point(16, 32);
      this.txtTitle.Multiline = true;
      this.txtTitle.Name = "txtTitle";
      this.txtTitle.ReadOnly = true;
      this.txtTitle.Size = new System.Drawing.Size(689, 39);
      this.txtTitle.TabIndex = 31;
      // 
      // trkPos
      // 
      this.trkPos.AutoSize = false;
      this.trkPos.LargeChange = 10;
      this.trkPos.Location = new System.Drawing.Point(95, 43);
      this.trkPos.Maximum = 100;
      this.trkPos.Name = "trkPos";
      this.trkPos.Size = new System.Drawing.Size(446, 30);
      this.trkPos.TabIndex = 10;
      this.trkPos.TickFrequency = 60;
      this.trkPos.Scroll += new System.EventHandler(this.trkPos_Scroll);
      // 
      // lbltrkPos
      // 
      this.lbltrkPos.AutoSize = true;
      this.lbltrkPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbltrkPos.Location = new System.Drawing.Point(16, 43);
      this.lbltrkPos.Name = "lbltrkPos";
      this.lbltrkPos.Size = new System.Drawing.Size(73, 20);
      this.lbltrkPos.TabIndex = 14;
      this.lbltrkPos.Text = "Position";
      this.lbltrkPos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblLenBytes
      // 
      this.lblLenBytes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLenBytes.Location = new System.Drawing.Point(762, 43);
      this.lblLenBytes.Name = "lblLenBytes";
      this.lblLenBytes.Size = new System.Drawing.Size(98, 23);
      this.lblLenBytes.TabIndex = 28;
      this.lblLenBytes.Text = "0";
      this.lblLenBytes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblDelimBytes
      // 
      this.lblDelimBytes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblDelimBytes.Location = new System.Drawing.Point(752, 44);
      this.lblDelimBytes.Name = "lblDelimBytes";
      this.lblDelimBytes.Size = new System.Drawing.Size(15, 23);
      this.lblDelimBytes.TabIndex = 27;
      this.lblDelimBytes.Text = "/";
      // 
      // lblPosBytes
      // 
      this.lblPosBytes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPosBytes.Location = new System.Drawing.Point(659, 43);
      this.lblPosBytes.Name = "lblPosBytes";
      this.lblPosBytes.Size = new System.Drawing.Size(98, 23);
      this.lblPosBytes.TabIndex = 25;
      this.lblPosBytes.Text = "0";
      this.lblPosBytes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblDelimTime
      // 
      this.lblDelimTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblDelimTime.Location = new System.Drawing.Point(752, 21);
      this.lblDelimTime.Name = "lblDelimTime";
      this.lblDelimTime.Size = new System.Drawing.Size(15, 23);
      this.lblDelimTime.TabIndex = 13;
      this.lblDelimTime.Text = "/";
      // 
      // lblLenTime
      // 
      this.lblLenTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLenTime.Location = new System.Drawing.Point(762, 20);
      this.lblLenTime.Name = "lblLenTime";
      this.lblLenTime.Size = new System.Drawing.Size(90, 23);
      this.lblLenTime.TabIndex = 12;
      this.lblLenTime.Text = "0:00.00";
      this.lblLenTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chkStartRecPos
      // 
      this.chkStartRecPos.AutoSize = true;
      this.chkStartRecPos.Location = new System.Drawing.Point(20, 13);
      this.chkStartRecPos.Name = "chkStartRecPos";
      this.chkStartRecPos.Size = new System.Drawing.Size(137, 17);
      this.chkStartRecPos.TabIndex = 36;
      this.chkStartRecPos.Text = "Start from Position Time";
      this.chkStartRecPos.UseVisualStyleBackColor = true;
      // 
      // lblPosTime
      // 
      this.lblPosTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPosTime.Location = new System.Drawing.Point(666, 20);
      this.lblPosTime.Name = "lblPosTime";
      this.lblPosTime.Size = new System.Drawing.Size(90, 23);
      this.lblPosTime.TabIndex = 11;
      this.lblPosTime.Text = "00:00.00";
      this.lblPosTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // panPos
      // 
      this.panPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panPos.Controls.Add(this.lblPosTime);
      this.panPos.Controls.Add(this.chkStartRecPos);
      this.panPos.Controls.Add(this.lblLenTime);
      this.panPos.Controls.Add(this.lblDelimTime);
      this.panPos.Controls.Add(this.lblPosBytes);
      this.panPos.Controls.Add(this.lblDelimBytes);
      this.panPos.Controls.Add(this.lblLenBytes);
      this.panPos.Controls.Add(this.lbltrkPos);
      this.panPos.Controls.Add(this.trkPos);
      this.panPos.Location = new System.Drawing.Point(16, 510);
      this.panPos.Name = "panPos";
      this.panPos.Size = new System.Drawing.Size(863, 87);
      this.panPos.TabIndex = 37;
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.ShowItemToolTips = true;
      this.mnuMain.Size = new System.Drawing.Size(884, 24);
      this.mnuMain.TabIndex = 38;
      this.mnuMain.Text = "menuStrip1";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSave});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 20);
      this.mnuFile.Text = "File";
      // 
      // mnuSave
      // 
      this.mnuSave.Name = "mnuSave";
      this.mnuSave.Size = new System.Drawing.Size(158, 22);
      this.mnuSave.Text = "Save Play to File";
      this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
      // 
      // mnuEdit
      // 
      this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSelected,
            this.mnuPlay});
      this.mnuEdit.Name = "mnuEdit";
      this.mnuEdit.Size = new System.Drawing.Size(39, 20);
      this.mnuEdit.Text = "Edit";
      // 
      // mnuSelected
      // 
      this.mnuSelected.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSelRemoveRecord,
            this.mnuSelRemovePlay,
            this.mnuSelCopy,
            this.mnuInterpolatePlay,
            this.mnuInterpolateRecord});
      this.mnuSelected.Name = "mnuSelected";
      this.mnuSelected.Size = new System.Drawing.Size(118, 22);
      this.mnuSelected.Text = "Selected";
      // 
      // mnuSelRemoveRecord
      // 
      this.mnuSelRemoveRecord.Name = "mnuSelRemoveRecord";
      this.mnuSelRemoveRecord.Size = new System.Drawing.Size(181, 22);
      this.mnuSelRemoveRecord.Text = "Remove Record";
      this.mnuSelRemoveRecord.Click += new System.EventHandler(this.mnuSelRemoveRecord_Click);
      // 
      // mnuSelRemovePlay
      // 
      this.mnuSelRemovePlay.Name = "mnuSelRemovePlay";
      this.mnuSelRemovePlay.Size = new System.Drawing.Size(181, 22);
      this.mnuSelRemovePlay.Text = "Remove Play";
      this.mnuSelRemovePlay.Click += new System.EventHandler(this.mnuSelRemovePlay_Click);
      // 
      // mnuSelCopy
      // 
      this.mnuSelCopy.Name = "mnuSelCopy";
      this.mnuSelCopy.Size = new System.Drawing.Size(181, 22);
      this.mnuSelCopy.Text = "Copy Record to Play";
      this.mnuSelCopy.Click += new System.EventHandler(this.mnuSelCopy_Click);
      // 
      // mnuInterpolatePlay
      // 
      this.mnuInterpolatePlay.ForeColor = System.Drawing.Color.Red;
      this.mnuInterpolatePlay.Name = "mnuInterpolatePlay";
      this.mnuInterpolatePlay.Size = new System.Drawing.Size(181, 22);
      this.mnuInterpolatePlay.Text = "Interpolate Play";
      this.mnuInterpolatePlay.Click += new System.EventHandler(this.mnuInterpolatePlay_Click);
      // 
      // mnuInterpolateRecord
      // 
      this.mnuInterpolateRecord.ForeColor = System.Drawing.Color.Red;
      this.mnuInterpolateRecord.Name = "mnuInterpolateRecord";
      this.mnuInterpolateRecord.Size = new System.Drawing.Size(181, 22);
      this.mnuInterpolateRecord.Text = "Interpolate Record";
      this.mnuInterpolateRecord.Click += new System.EventHandler(this.mnuInterpolateRecord_Click);
      // 
      // mnuPlay
      // 
      this.mnuPlay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInsertHoleNew,
            this.mnuRemoveCloseHoleNew});
      this.mnuPlay.ForeColor = System.Drawing.Color.Red;
      this.mnuPlay.Name = "mnuPlay";
      this.mnuPlay.Size = new System.Drawing.Size(118, 22);
      this.mnuPlay.Text = "Play";
      // 
      // mnuInsertHoleNew
      // 
      this.mnuInsertHoleNew.ForeColor = System.Drawing.Color.Red;
      this.mnuInsertHoleNew.Name = "mnuInsertHoleNew";
      this.mnuInsertHoleNew.Size = new System.Drawing.Size(200, 22);
      this.mnuInsertHoleNew.Text = "Insert Hole";
      this.mnuInsertHoleNew.Click += new System.EventHandler(this.mnuInsertHole_Click);
      // 
      // mnuRemoveCloseHoleNew
      // 
      this.mnuRemoveCloseHoleNew.ForeColor = System.Drawing.Color.Red;
      this.mnuRemoveCloseHoleNew.Name = "mnuRemoveCloseHoleNew";
      this.mnuRemoveCloseHoleNew.Size = new System.Drawing.Size(200, 22);
      this.mnuRemoveCloseHoleNew.Text = "Remove and Close Hole";
      this.mnuRemoveCloseHoleNew.Click += new System.EventHandler(this.mnuRemoveCloseHoleNew_Click);
      // 
      // cmdUndo
      // 
      this.cmdUndo.Enabled = false;
      this.cmdUndo.Location = new System.Drawing.Point(5, 319);
      this.cmdUndo.Name = "cmdUndo";
      this.cmdUndo.Size = new System.Drawing.Size(67, 32);
      this.cmdUndo.TabIndex = 39;
      this.cmdUndo.Text = "Undo";
      this.cmdUndo.UseVisualStyleBackColor = true;
      this.cmdUndo.Click += new System.EventHandler(this.cmdUndo_Click);
      // 
      // cmdRedo
      // 
      this.cmdRedo.Enabled = false;
      this.cmdRedo.Location = new System.Drawing.Point(88, 319);
      this.cmdRedo.Name = "cmdRedo";
      this.cmdRedo.Size = new System.Drawing.Size(67, 32);
      this.cmdRedo.TabIndex = 40;
      this.cmdRedo.Text = "Redo";
      this.cmdRedo.UseVisualStyleBackColor = true;
      this.cmdRedo.Click += new System.EventHandler(this.cmdRedo_Click);
      // 
      // cmdHelp
      // 
      this.cmdHelp.Location = new System.Drawing.Point(6, 427);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(67, 32);
      this.cmdHelp.TabIndex = 41;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // cmdClose
      // 
      this.cmdClose.Location = new System.Drawing.Point(87, 427);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(67, 32);
      this.cmdClose.TabIndex = 42;
      this.cmdClose.Text = "Close";
      this.cmdClose.UseVisualStyleBackColor = true;
      this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
      // 
      // timer1
      // 
      this.timer1.Interval = 1000;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // panCmds
      // 
      this.panCmds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.panCmds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panCmds.Controls.Add(this.cmdOffsetBeats);
      this.panCmds.Controls.Add(this.cmdOffsetTimes);
      this.panCmds.Controls.Add(this.cmdClose);
      this.panCmds.Controls.Add(this.cmdHelp);
      this.panCmds.Controls.Add(this.cmdRedo);
      this.panCmds.Controls.Add(this.cmdUndo);
      this.panCmds.Controls.Add(this.cmdMoveActive);
      this.panCmds.Controls.Add(this.cmdResetActive);
      this.panCmds.Controls.Add(this.cmdReset);
      this.panCmds.Controls.Add(this.cmdSave);
      this.panCmds.Controls.Add(this.cmdMerge);
      this.panCmds.Controls.Add(this.cmdShow);
      this.panCmds.Location = new System.Drawing.Point(717, 32);
      this.panCmds.Name = "panCmds";
      this.panCmds.Size = new System.Drawing.Size(162, 469);
      this.panCmds.TabIndex = 48;
      // 
      // cmdOffsetBeats
      // 
      this.cmdOffsetBeats.Location = new System.Drawing.Point(6, 266);
      this.cmdOffsetBeats.Name = "cmdOffsetBeats";
      this.cmdOffsetBeats.Size = new System.Drawing.Size(149, 32);
      this.cmdOffsetBeats.TabIndex = 44;
      this.cmdOffsetBeats.Text = "Offset Beats";
      this.cmdOffsetBeats.UseVisualStyleBackColor = true;
      this.cmdOffsetBeats.Click += new System.EventHandler(this.cmdOffsetBeats_Click);
      // 
      // cmdOffsetTimes
      // 
      this.cmdOffsetTimes.Location = new System.Drawing.Point(6, 228);
      this.cmdOffsetTimes.Name = "cmdOffsetTimes";
      this.cmdOffsetTimes.Size = new System.Drawing.Size(148, 32);
      this.cmdOffsetTimes.TabIndex = 43;
      this.cmdOffsetTimes.Text = "Offset Times";
      this.cmdOffsetTimes.UseVisualStyleBackColor = true;
      this.cmdOffsetTimes.Click += new System.EventHandler(this.cmdOffsetTimes_Click);
      // 
      // frmAutoSync
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(884, 611);
      this.Controls.Add(this.panCmds);
      this.Controls.Add(this.panPos);
      this.Controls.Add(this.txtTitle);
      this.Controls.Add(this.clbEla);
      this.Controls.Add(this.mnuMain);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.KeyPreview = true;
      this.MainMenuStrip = this.mnuMain;
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(900, 1000);
      this.MinimumSize = new System.Drawing.Size(900, 650);
      this.Name = "frmAutoSync";
      this.Text = "AudioSync - Chord Cadenza";
      this.Activated += new System.EventHandler(this.frmAutoSync_Activated);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAutoSync_FormClosing);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAutoSync_FormClosed);
      this.Load += new System.EventHandler(this.frmAutoSync_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAutoSync_KeyDown);
      ((System.ComponentModel.ISupportInitialize)(this.trkPos)).EndInit();
      this.panPos.ResumeLayout(false);
      this.panPos.PerformLayout();
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.panCmds.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button cmdShow;
    private System.Windows.Forms.Button cmdMerge;
    private System.Windows.Forms.Button cmdSave;
    private System.Windows.Forms.Button cmdReset;
    private System.Windows.Forms.Button cmdResetActive;
    private System.Windows.Forms.Button cmdMoveActive;
    private System.Windows.Forms.ListBox clbEla;
    private System.Windows.Forms.TextBox txtTitle;
    internal System.Windows.Forms.TrackBar trkPos;
    private System.Windows.Forms.Label lbltrkPos;
    private System.Windows.Forms.Label lblLenBytes;
    private System.Windows.Forms.Label lblDelimBytes;
    private System.Windows.Forms.Label lblPosBytes;
    private System.Windows.Forms.Label lblDelimTime;
    private System.Windows.Forms.Label lblLenTime;
    internal System.Windows.Forms.CheckBox chkStartRecPos;
    private System.Windows.Forms.Label lblPosTime;
    private System.Windows.Forms.Panel panPos;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuEdit;
    private System.Windows.Forms.ToolStripMenuItem mnuSave;
    private System.Windows.Forms.ToolStripMenuItem mnuSelected;
    private System.Windows.Forms.ToolStripMenuItem mnuSelRemoveRecord;
    private System.Windows.Forms.ToolStripMenuItem mnuSelRemovePlay;
    private System.Windows.Forms.ToolStripMenuItem mnuSelCopy;
    private System.Windows.Forms.ToolStripMenuItem mnuPlay;
    private System.Windows.Forms.ToolStripMenuItem mnuInsertHoleNew;
    private System.Windows.Forms.ToolStripMenuItem mnuRemoveCloseHoleNew;
    internal System.Windows.Forms.Button cmdUndo;
    internal System.Windows.Forms.Button cmdRedo;
    internal System.Windows.Forms.Button cmdHelp;
    internal System.Windows.Forms.Button cmdClose;
    internal System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuInterpolatePlay;
    private System.Windows.Forms.ToolStripMenuItem mnuInterpolateRecord;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Panel panCmds;
    private System.Windows.Forms.Button cmdOffsetTimes;
    private System.Windows.Forms.Button cmdOffsetBeats;
  }
}