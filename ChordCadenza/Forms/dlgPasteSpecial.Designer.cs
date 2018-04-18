namespace ChordCadenza.Forms {
  partial class dlgPasteSpecial {
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
      this.cmdCancel = new System.Windows.Forms.Button();
      this.cmdOK = new System.Windows.Forms.Button();
      this.grpOptions = new System.Windows.Forms.GroupBox();
      this.optPasteAll = new System.Windows.Forms.RadioButton();
      this.optRemoveInsert = new System.Windows.Forms.RadioButton();
      this.optReplaceRetain = new System.Windows.Forms.RadioButton();
      this.lblCopy_Buff = new System.Windows.Forms.Label();
      this.grpOptions.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmdCancel
      // 
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Location = new System.Drawing.Point(397, 262);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(50, 50);
      this.cmdCancel.TabIndex = 6;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      // 
      // cmdOK
      // 
      this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdOK.Location = new System.Drawing.Point(343, 262);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(50, 50);
      this.cmdOK.TabIndex = 5;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      // 
      // grpOptions
      // 
      this.grpOptions.Controls.Add(this.optPasteAll);
      this.grpOptions.Controls.Add(this.optRemoveInsert);
      this.grpOptions.Controls.Add(this.optReplaceRetain);
      this.grpOptions.Location = new System.Drawing.Point(12, 82);
      this.grpOptions.Name = "grpOptions";
      this.grpOptions.Size = new System.Drawing.Size(435, 164);
      this.grpOptions.TabIndex = 7;
      this.grpOptions.TabStop = false;
      this.grpOptions.Text = "Paste Options";
      // 
      // optPasteAll
      // 
      this.optPasteAll.AutoSize = true;
      this.optPasteAll.Checked = true;
      this.optPasteAll.Location = new System.Drawing.Point(16, 29);
      this.optPasteAll.Name = "optPasteAll";
      this.optPasteAll.Size = new System.Drawing.Size(384, 43);
      this.optPasteAll.TabIndex = 3;
      this.optPasteAll.TabStop = true;
      this.optPasteAll.Text = "Remove bars in the Selected Area..\r\nInsert bars containing chords, time signature" +
    "s, and keys from the Copy buffer\r\nto the start of Selected Area (or Current Curs" +
    "or).";
      this.optPasteAll.UseVisualStyleBackColor = true;
      // 
      // optRemoveInsert
      // 
      this.optRemoveInsert.AutoSize = true;
      this.optRemoveInsert.Location = new System.Drawing.Point(16, 80);
      this.optRemoveInsert.Name = "optRemoveInsert";
      this.optRemoveInsert.Size = new System.Drawing.Size(259, 43);
      this.optRemoveInsert.TabIndex = 2;
      this.optRemoveInsert.Text = "Remove bars in the Selected Area.\r\nInsert bars containing chords from the Copy bu" +
    "ffer\r\nto the start of Selected Area (or Current Cursor).";
      this.optRemoveInsert.UseVisualStyleBackColor = true;
      // 
      // optReplaceRetain
      // 
      this.optReplaceRetain.AutoSize = true;
      this.optReplaceRetain.Location = new System.Drawing.Point(16, 134);
      this.optReplaceRetain.Name = "optReplaceRetain";
      this.optReplaceRetain.Size = new System.Drawing.Size(202, 17);
      this.optReplaceRetain.TabIndex = 1;
      this.optReplaceRetain.Text = "Replace chords in the Selected Area.";
      this.optReplaceRetain.UseVisualStyleBackColor = true;
      // 
      // lblCopy_Buff
      // 
      this.lblCopy_Buff.AutoSize = true;
      this.lblCopy_Buff.Location = new System.Drawing.Point(12, 19);
      this.lblCopy_Buff.Name = "lblCopy_Buff";
      this.lblCopy_Buff.Size = new System.Drawing.Size(28, 13);
      this.lblCopy_Buff.TabIndex = 8;
      this.lblCopy_Buff.Text = "       ";
      // 
      // dlgPasteSpecial
      // 
      this.AcceptButton = this.cmdOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(459, 325);
      this.ControlBox = false;
      this.Controls.Add(this.lblCopy_Buff);
      this.Controls.Add(this.grpOptions);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.cmdOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "dlgPasteSpecial";
      this.Text = "Paste - Chord Cadenza";
      this.grpOptions.ResumeLayout(false);
      this.grpOptions.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.GroupBox grpOptions;
    internal System.Windows.Forms.RadioButton optReplaceRetain;
    internal System.Windows.Forms.RadioButton optRemoveInsert;
    internal System.Windows.Forms.RadioButton optPasteAll;
    private System.Windows.Forms.Label lblCopy_Buff;
  }
}