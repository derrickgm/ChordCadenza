namespace ChordCadenza.Forms {
  partial class dlgCalcKeysOpts {
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
      this.cmdOK = new System.Windows.Forms.Button();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.optMidiFileAll = new System.Windows.Forms.RadioButton();
      this.grpSource = new System.Windows.Forms.GroupBox();
      this.optChordFile = new System.Windows.Forms.RadioButton();
      this.optMidiFileSelected = new System.Windows.Forms.RadioButton();
      this.grpSource.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmdOK
      // 
      this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdOK.Location = new System.Drawing.Point(235, 120);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(49, 45);
      this.cmdOK.TabIndex = 0;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      // 
      // cmdCancel
      // 
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Location = new System.Drawing.Point(290, 120);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(49, 45);
      this.cmdCancel.TabIndex = 1;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      // 
      // optMidiFileAll
      // 
      this.optMidiFileAll.AutoSize = true;
      this.optMidiFileAll.Checked = true;
      this.optMidiFileAll.Location = new System.Drawing.Point(15, 19);
      this.optMidiFileAll.Name = "optMidiFileAll";
      this.optMidiFileAll.Size = new System.Drawing.Size(116, 17);
      this.optMidiFileAll.TabIndex = 0;
      this.optMidiFileAll.TabStop = true;
      this.optMidiFileAll.Text = "MidiFile (All Tracks)";
      this.optMidiFileAll.UseVisualStyleBackColor = true;
      // 
      // grpSource
      // 
      this.grpSource.Controls.Add(this.optChordFile);
      this.grpSource.Controls.Add(this.optMidiFileSelected);
      this.grpSource.Controls.Add(this.optMidiFileAll);
      this.grpSource.Location = new System.Drawing.Point(12, 21);
      this.grpSource.Name = "grpSource";
      this.grpSource.Size = new System.Drawing.Size(327, 84);
      this.grpSource.TabIndex = 3;
      this.grpSource.TabStop = false;
      this.grpSource.Text = "Source";
      // 
      // optChordFile
      // 
      this.optChordFile.AutoSize = true;
      this.optChordFile.Location = new System.Drawing.Point(15, 57);
      this.optChordFile.Name = "optChordFile";
      this.optChordFile.Size = new System.Drawing.Size(178, 17);
      this.optChordFile.TabIndex = 2;
      this.optChordFile.Text = "ChordFile (may be less accurate)";
      this.optChordFile.UseVisualStyleBackColor = true;
      // 
      // optMidiFileSelected
      // 
      this.optMidiFileSelected.AutoSize = true;
      this.optMidiFileSelected.Location = new System.Drawing.Point(15, 38);
      this.optMidiFileSelected.Name = "optMidiFileSelected";
      this.optMidiFileSelected.Size = new System.Drawing.Size(147, 17);
      this.optMidiFileSelected.TabIndex = 1;
      this.optMidiFileSelected.Text = "MidiFile (Selected Tracks)";
      this.optMidiFileSelected.UseVisualStyleBackColor = true;
      // 
      // dlgCalcKeysOpts
      // 
      this.AcceptButton = this.cmdOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(351, 177);
      this.Controls.Add(this.grpSource);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.cmdOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "dlgCalcKeysOpts";
      this.Text = "CalcKeys Options - Chord Cadenza";
      this.grpSource.ResumeLayout(false);
      this.grpSource.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.GroupBox grpSource;
    internal System.Windows.Forms.RadioButton optMidiFileAll;
    internal System.Windows.Forms.RadioButton optChordFile;
    internal System.Windows.Forms.RadioButton optMidiFileSelected;
  }
}