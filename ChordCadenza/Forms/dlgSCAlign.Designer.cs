namespace ChordCadenza.Forms {
  partial class dlgSCAlign {
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
      this.label1 = new System.Windows.Forms.Label();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(15, 33);
      this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(491, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "Play a note on your keyboard to align to the bottom octave of the PlayMap display" +
    "";
      // 
      // cmdCancel
      // 
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Location = new System.Drawing.Point(433, 74);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(72, 33);
      this.cmdCancel.TabIndex = 1;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
      // 
      // frmSCAlign
      // 
      this.AcceptButton = this.cmdCancel;
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(517, 119);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.label1);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Margin = new System.Windows.Forms.Padding(4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmSCAlign";
      this.Text = "Align PlayMap Display - Chord Cadenza";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSCAlign_FormClosed);
      this.Load += new System.EventHandler(this.frmSCAlign_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button cmdCancel;
  }
}