namespace ChordCadenza.Forms {
  partial class dlgExit {
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
      this.cmdExitAndSave = new System.Windows.Forms.Button();
      this.cmdExitNoSave = new System.Windows.Forms.Button();
      this.cmdContinue = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // cmdExitAndSave
      // 
      this.cmdExitAndSave.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdExitAndSave.Location = new System.Drawing.Point(32, 16);
      this.cmdExitAndSave.Name = "cmdExitAndSave";
      this.cmdExitAndSave.Size = new System.Drawing.Size(229, 32);
      this.cmdExitAndSave.TabIndex = 0;
      this.cmdExitAndSave.Text = "Save Settings and Exit Program";
      this.cmdExitAndSave.UseVisualStyleBackColor = true;
      // 
      // cmdExitNoSave
      // 
      this.cmdExitNoSave.DialogResult = System.Windows.Forms.DialogResult.Ignore;
      this.cmdExitNoSave.Location = new System.Drawing.Point(32, 54);
      this.cmdExitNoSave.Name = "cmdExitNoSave";
      this.cmdExitNoSave.Size = new System.Drawing.Size(229, 32);
      this.cmdExitNoSave.TabIndex = 1;
      this.cmdExitNoSave.Text = "Exit Program without Saving Settings";
      this.cmdExitNoSave.UseVisualStyleBackColor = true;
      // 
      // cmdContinue
      // 
      this.cmdContinue.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdContinue.Location = new System.Drawing.Point(32, 92);
      this.cmdContinue.Name = "cmdContinue";
      this.cmdContinue.Size = new System.Drawing.Size(229, 32);
      this.cmdContinue.TabIndex = 2;
      this.cmdContinue.Text = "Continue";
      this.cmdContinue.UseVisualStyleBackColor = true;
      // 
      // frmExit
      // 
      this.AcceptButton = this.cmdExitAndSave;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdContinue;
      this.ClientSize = new System.Drawing.Size(284, 139);
      this.ControlBox = false;
      this.Controls.Add(this.cmdContinue);
      this.Controls.Add(this.cmdExitNoSave);
      this.Controls.Add(this.cmdExitAndSave);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmExit";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Exit - Chord Cadenza";
      this.Load += new System.EventHandler(this.frmExit_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button cmdExitAndSave;
    private System.Windows.Forms.Button cmdExitNoSave;
    private System.Windows.Forms.Button cmdContinue;
  }
}