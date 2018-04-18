namespace ChordCadenza.Forms {
  partial class frmManChordSync {
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
      this.cmdClose = new System.Windows.Forms.Button();
      this.grpAll = new System.Windows.Forms.GroupBox();
      this.panChks = new System.Windows.Forms.Panel();
      this.chkNextBlack = new System.Windows.Forms.CheckBox();
      this.chkNoSkipAfterReloc = new System.Windows.Forms.CheckBox();
      this.chkNextWhite = new System.Windows.Forms.CheckBox();
      this.grpSingleWhite = new System.Windows.Forms.GroupBox();
      this.optSingleWhitePrev = new System.Windows.Forms.RadioButton();
      this.optSingleWhiteNext = new System.Windows.Forms.RadioButton();
      this.optSingleWhitePlay = new System.Windows.Forms.RadioButton();
      this.grpSingleBlack = new System.Windows.Forms.GroupBox();
      this.optSingleBlackPrev = new System.Windows.Forms.RadioButton();
      this.optSingleBlackNext = new System.Windows.Forms.RadioButton();
      this.optSingleBlackPlay = new System.Windows.Forms.RadioButton();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.grpAll.SuspendLayout();
      this.panChks.SuspendLayout();
      this.grpSingleWhite.SuspendLayout();
      this.grpSingleBlack.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmdClose
      // 
      this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdClose.Location = new System.Drawing.Point(386, 123);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(65, 45);
      this.cmdClose.TabIndex = 2;
      this.cmdClose.Text = "Close\r\nWindow";
      this.cmdClose.UseVisualStyleBackColor = true;
      this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
      // 
      // grpAll
      // 
      this.grpAll.Controls.Add(this.panChks);
      this.grpAll.Controls.Add(this.grpSingleWhite);
      this.grpAll.Controls.Add(this.grpSingleBlack);
      this.grpAll.Location = new System.Drawing.Point(12, 12);
      this.grpAll.Name = "grpAll";
      this.grpAll.Size = new System.Drawing.Size(446, 105);
      this.grpAll.TabIndex = 246;
      this.grpAll.TabStop = false;
      this.grpAll.Text = "Manual Chord Sync";
      // 
      // panChks
      // 
      this.panChks.Controls.Add(this.chkNextBlack);
      this.panChks.Controls.Add(this.chkNoSkipAfterReloc);
      this.panChks.Controls.Add(this.chkNextWhite);
      this.panChks.Location = new System.Drawing.Point(296, 26);
      this.panChks.Name = "panChks";
      this.panChks.Size = new System.Drawing.Size(139, 67);
      this.panChks.TabIndex = 242;
      // 
      // chkNextBlack
      // 
      this.chkNextBlack.AutoSize = true;
      this.chkNextBlack.Location = new System.Drawing.Point(3, 8);
      this.chkNextBlack.Name = "chkNextBlack";
      this.chkNextBlack.Size = new System.Drawing.Size(131, 17);
      this.chkNextBlack.TabIndex = 238;
      this.chkNextBlack.Text = "Next Chord if All Black";
      this.chkNextBlack.UseVisualStyleBackColor = true;
      // 
      // chkNoSkipAfterReloc
      // 
      this.chkNoSkipAfterReloc.AutoSize = true;
      this.chkNoSkipAfterReloc.ForeColor = System.Drawing.Color.Red;
      this.chkNoSkipAfterReloc.Location = new System.Drawing.Point(3, 40);
      this.chkNoSkipAfterReloc.Name = "chkNoSkipAfterReloc";
      this.chkNoSkipAfterReloc.Size = new System.Drawing.Size(135, 17);
      this.chkNoSkipAfterReloc.TabIndex = 239;
      this.chkNoSkipAfterReloc.Text = "No Skip After Relocate";
      this.chkNoSkipAfterReloc.UseVisualStyleBackColor = true;
      // 
      // chkNextWhite
      // 
      this.chkNextWhite.AutoSize = true;
      this.chkNextWhite.Checked = true;
      this.chkNextWhite.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkNextWhite.Location = new System.Drawing.Point(3, 24);
      this.chkNextWhite.Name = "chkNextWhite";
      this.chkNextWhite.Size = new System.Drawing.Size(132, 17);
      this.chkNextWhite.TabIndex = 240;
      this.chkNextWhite.Text = "Next Chord if All White";
      this.chkNextWhite.UseVisualStyleBackColor = true;
      // 
      // grpSingleWhite
      // 
      this.grpSingleWhite.Controls.Add(this.optSingleWhitePrev);
      this.grpSingleWhite.Controls.Add(this.optSingleWhiteNext);
      this.grpSingleWhite.Controls.Add(this.optSingleWhitePlay);
      this.grpSingleWhite.Location = new System.Drawing.Point(149, 19);
      this.grpSingleWhite.Name = "grpSingleWhite";
      this.grpSingleWhite.Size = new System.Drawing.Size(134, 74);
      this.grpSingleWhite.TabIndex = 1;
      this.grpSingleWhite.TabStop = false;
      this.grpSingleWhite.Text = "Single White Key";
      // 
      // optSingleWhitePrev
      // 
      this.optSingleWhitePrev.AutoSize = true;
      this.optSingleWhitePrev.Location = new System.Drawing.Point(18, 51);
      this.optSingleWhitePrev.Name = "optSingleWhitePrev";
      this.optSingleWhitePrev.Size = new System.Drawing.Size(97, 17);
      this.optSingleWhitePrev.TabIndex = 2;
      this.optSingleWhitePrev.Text = "Previous Chord";
      this.optSingleWhitePrev.UseVisualStyleBackColor = true;
      // 
      // optSingleWhiteNext
      // 
      this.optSingleWhiteNext.AutoSize = true;
      this.optSingleWhiteNext.Location = new System.Drawing.Point(18, 34);
      this.optSingleWhiteNext.Name = "optSingleWhiteNext";
      this.optSingleWhiteNext.Size = new System.Drawing.Size(78, 17);
      this.optSingleWhiteNext.TabIndex = 1;
      this.optSingleWhiteNext.Text = "Next Chord";
      this.optSingleWhiteNext.UseVisualStyleBackColor = true;
      // 
      // optSingleWhitePlay
      // 
      this.optSingleWhitePlay.AutoSize = true;
      this.optSingleWhitePlay.Checked = true;
      this.optSingleWhitePlay.Location = new System.Drawing.Point(18, 17);
      this.optSingleWhitePlay.Name = "optSingleWhitePlay";
      this.optSingleWhitePlay.Size = new System.Drawing.Size(72, 17);
      this.optSingleWhitePlay.TabIndex = 0;
      this.optSingleWhitePlay.TabStop = true;
      this.optSingleWhitePlay.Text = "No Action";
      this.optSingleWhitePlay.UseVisualStyleBackColor = true;
      // 
      // grpSingleBlack
      // 
      this.grpSingleBlack.Controls.Add(this.optSingleBlackPrev);
      this.grpSingleBlack.Controls.Add(this.optSingleBlackNext);
      this.grpSingleBlack.Controls.Add(this.optSingleBlackPlay);
      this.grpSingleBlack.Location = new System.Drawing.Point(6, 19);
      this.grpSingleBlack.Name = "grpSingleBlack";
      this.grpSingleBlack.Size = new System.Drawing.Size(134, 74);
      this.grpSingleBlack.TabIndex = 0;
      this.grpSingleBlack.TabStop = false;
      this.grpSingleBlack.Text = "Single Black Key";
      // 
      // optSingleBlackPrev
      // 
      this.optSingleBlackPrev.AutoSize = true;
      this.optSingleBlackPrev.Location = new System.Drawing.Point(18, 51);
      this.optSingleBlackPrev.Name = "optSingleBlackPrev";
      this.optSingleBlackPrev.Size = new System.Drawing.Size(97, 17);
      this.optSingleBlackPrev.TabIndex = 2;
      this.optSingleBlackPrev.Text = "Previous Chord";
      this.optSingleBlackPrev.UseVisualStyleBackColor = true;
      // 
      // optSingleBlackNext
      // 
      this.optSingleBlackNext.AutoSize = true;
      this.optSingleBlackNext.Checked = true;
      this.optSingleBlackNext.Location = new System.Drawing.Point(18, 34);
      this.optSingleBlackNext.Name = "optSingleBlackNext";
      this.optSingleBlackNext.Size = new System.Drawing.Size(78, 17);
      this.optSingleBlackNext.TabIndex = 1;
      this.optSingleBlackNext.TabStop = true;
      this.optSingleBlackNext.Text = "Next Chord";
      this.optSingleBlackNext.UseVisualStyleBackColor = true;
      // 
      // optSingleBlackPlay
      // 
      this.optSingleBlackPlay.AutoSize = true;
      this.optSingleBlackPlay.Location = new System.Drawing.Point(18, 17);
      this.optSingleBlackPlay.Name = "optSingleBlackPlay";
      this.optSingleBlackPlay.Size = new System.Drawing.Size(72, 17);
      this.optSingleBlackPlay.TabIndex = 0;
      this.optSingleBlackPlay.Text = "No Action";
      this.optSingleBlackPlay.UseVisualStyleBackColor = true;
      // 
      // cmdHelp
      // 
      this.cmdHelp.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdHelp.Location = new System.Drawing.Point(308, 123);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(65, 45);
      this.cmdHelp.TabIndex = 247;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // frmManChordSync
      // 
      this.AcceptButton = this.cmdClose;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(465, 179);
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.grpAll);
      this.Controls.Add(this.cmdClose);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmManChordSync";
      this.Text = "Manual Chord Sync - Chord Cadenza";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmManChordSync_FormClosed);
      this.Load += new System.EventHandler(this.frmManChordSync_Load);
      this.grpAll.ResumeLayout(false);
      this.panChks.ResumeLayout(false);
      this.panChks.PerformLayout();
      this.grpSingleWhite.ResumeLayout(false);
      this.grpSingleWhite.PerformLayout();
      this.grpSingleBlack.ResumeLayout(false);
      this.grpSingleBlack.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Button cmdClose;
    private System.Windows.Forms.GroupBox grpAll;
    private System.Windows.Forms.Panel panChks;
    internal System.Windows.Forms.CheckBox chkNextBlack;
    internal System.Windows.Forms.CheckBox chkNoSkipAfterReloc;
    internal System.Windows.Forms.CheckBox chkNextWhite;
    private System.Windows.Forms.GroupBox grpSingleWhite;
    internal System.Windows.Forms.RadioButton optSingleWhitePrev;
    internal System.Windows.Forms.RadioButton optSingleWhiteNext;
    internal System.Windows.Forms.RadioButton optSingleWhitePlay;
    private System.Windows.Forms.GroupBox grpSingleBlack;
    internal System.Windows.Forms.RadioButton optSingleBlackPrev;
    internal System.Windows.Forms.RadioButton optSingleBlackNext;
    internal System.Windows.Forms.RadioButton optSingleBlackPlay;
    private System.Windows.Forms.Button cmdHelp;
  }
}