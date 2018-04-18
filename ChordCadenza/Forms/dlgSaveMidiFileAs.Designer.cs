namespace ChordCadenza.Forms {
  partial class dlgSaveMidiFileAs {
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
      this.chkChordRoot = new System.Windows.Forms.CheckBox();
      this.chkSaveChordLabels = new System.Windows.Forms.CheckBox();
      this.lblnudChordChan = new System.Windows.Forms.Label();
      this.chkSaveMutedTrks = new System.Windows.Forms.CheckBox();
      this.nudChordChan = new System.Windows.Forms.NumericUpDown();
      this.chkSaveChordTrack = new System.Windows.Forms.CheckBox();
      this.lblChordTrackTitle = new System.Windows.Forms.Label();
      this.txtChordTrackTitle = new System.Windows.Forms.TextBox();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.cmdOK = new System.Windows.Forms.Button();
      this.cmdHelp = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.nudChordChan)).BeginInit();
      this.SuspendLayout();
      // 
      // chkChordRoot
      // 
      this.chkChordRoot.AutoSize = true;
      this.chkChordRoot.Location = new System.Drawing.Point(28, 72);
      this.chkChordRoot.Name = "chkChordRoot";
      this.chkChordRoot.Size = new System.Drawing.Size(120, 17);
      this.chkChordRoot.TabIndex = 196;
      this.chkChordRoot.Text = "Chord Root Position";
      this.chkChordRoot.UseVisualStyleBackColor = true;
      // 
      // chkSaveChordLabels
      // 
      this.chkSaveChordLabels.AutoSize = true;
      this.chkSaveChordLabels.Checked = true;
      this.chkSaveChordLabels.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkSaveChordLabels.Location = new System.Drawing.Point(28, 36);
      this.chkSaveChordLabels.Name = "chkSaveChordLabels";
      this.chkSaveChordLabels.Size = new System.Drawing.Size(166, 17);
      this.chkSaveChordLabels.TabIndex = 192;
      this.chkSaveChordLabels.Text = "Save Chord Names as Labels";
      this.chkSaveChordLabels.UseVisualStyleBackColor = true;
      // 
      // lblnudChordChan
      // 
      this.lblnudChordChan.AutoSize = true;
      this.lblnudChordChan.Location = new System.Drawing.Point(68, 95);
      this.lblnudChordChan.Name = "lblnudChordChan";
      this.lblnudChordChan.Size = new System.Drawing.Size(77, 13);
      this.lblnudChordChan.TabIndex = 195;
      this.lblnudChordChan.Text = "Chord Channel";
      // 
      // chkSaveMutedTrks
      // 
      this.chkSaveMutedTrks.AutoSize = true;
      this.chkSaveMutedTrks.Checked = true;
      this.chkSaveMutedTrks.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkSaveMutedTrks.Location = new System.Drawing.Point(28, 18);
      this.chkSaveMutedTrks.Name = "chkSaveMutedTrks";
      this.chkSaveMutedTrks.Size = new System.Drawing.Size(120, 17);
      this.chkSaveMutedTrks.TabIndex = 190;
      this.chkSaveMutedTrks.Text = "Save Muted Tracks";
      this.chkSaveMutedTrks.UseVisualStyleBackColor = true;
      // 
      // nudChordChan
      // 
      this.nudChordChan.Location = new System.Drawing.Point(28, 92);
      this.nudChordChan.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
      this.nudChordChan.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudChordChan.Name = "nudChordChan";
      this.nudChordChan.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.nudChordChan.Size = new System.Drawing.Size(34, 20);
      this.nudChordChan.TabIndex = 194;
      this.nudChordChan.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
      // 
      // chkSaveChordTrack
      // 
      this.chkSaveChordTrack.AutoSize = true;
      this.chkSaveChordTrack.Location = new System.Drawing.Point(28, 54);
      this.chkSaveChordTrack.Name = "chkSaveChordTrack";
      this.chkSaveChordTrack.Size = new System.Drawing.Size(113, 17);
      this.chkSaveChordTrack.TabIndex = 193;
      this.chkSaveChordTrack.Text = "Save Chord Track";
      this.chkSaveChordTrack.UseVisualStyleBackColor = true;
      // 
      // lblChordTrackTitle
      // 
      this.lblChordTrackTitle.AutoSize = true;
      this.lblChordTrackTitle.Location = new System.Drawing.Point(25, 121);
      this.lblChordTrackTitle.Name = "lblChordTrackTitle";
      this.lblChordTrackTitle.Size = new System.Drawing.Size(89, 13);
      this.lblChordTrackTitle.TabIndex = 198;
      this.lblChordTrackTitle.Text = "Chord Track Title";
      // 
      // txtChordTrackTitle
      // 
      this.txtChordTrackTitle.Location = new System.Drawing.Point(120, 118);
      this.txtChordTrackTitle.Name = "txtChordTrackTitle";
      this.txtChordTrackTitle.Size = new System.Drawing.Size(205, 20);
      this.txtChordTrackTitle.TabIndex = 199;
      // 
      // cmdCancel
      // 
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdCancel.Location = new System.Drawing.Point(266, 156);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(59, 29);
      this.cmdCancel.TabIndex = 202;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      // 
      // cmdOK
      // 
      this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdOK.Location = new System.Drawing.Point(169, 156);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(91, 29);
      this.cmdOK.TabIndex = 203;
      this.cmdOK.Text = "Save As ...";
      this.cmdOK.UseVisualStyleBackColor = true;
      // 
      // cmdHelp
      // 
      this.cmdHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdHelp.Location = new System.Drawing.Point(12, 156);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(59, 29);
      this.cmdHelp.TabIndex = 204;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // frmSaveMidiFileAs
      // 
      this.AcceptButton = this.cmdOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(342, 194);
      this.ControlBox = false;
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.cmdOK);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.txtChordTrackTitle);
      this.Controls.Add(this.lblChordTrackTitle);
      this.Controls.Add(this.chkChordRoot);
      this.Controls.Add(this.chkSaveChordLabels);
      this.Controls.Add(this.lblnudChordChan);
      this.Controls.Add(this.chkSaveMutedTrks);
      this.Controls.Add(this.nudChordChan);
      this.Controls.Add(this.chkSaveChordTrack);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "frmSaveMidiFileAs";
      this.Text = "Save MidiFile As - Chord Cadenza";
      this.Load += new System.EventHandler(this.frmSaveMidiFileAs_Load);
      ((System.ComponentModel.ISupportInitialize)(this.nudChordChan)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    internal System.Windows.Forms.CheckBox chkChordRoot;
    internal System.Windows.Forms.CheckBox chkSaveChordLabels;
    private System.Windows.Forms.Label lblnudChordChan;
    internal System.Windows.Forms.CheckBox chkSaveMutedTrks;
    internal System.Windows.Forms.NumericUpDown nudChordChan;
    internal System.Windows.Forms.CheckBox chkSaveChordTrack;
    private System.Windows.Forms.Label lblChordTrackTitle;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.Button cmdOK;
    internal System.Windows.Forms.TextBox txtChordTrackTitle;
    private System.Windows.Forms.Button cmdHelp;
  }
}