namespace ChordCadenza.Forms {
  partial class frmColours {
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
      this.colorDialog1 = new System.Windows.Forms.ColorDialog();
      this.cmdSetDefaults = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // cmdClose
      // 
      this.cmdClose.Location = new System.Drawing.Point(320, 502);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(50, 50);
      this.cmdClose.TabIndex = 15;
      this.cmdClose.Text = "Close";
      this.cmdClose.UseVisualStyleBackColor = true;
      this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
      // 
      // cmdSetDefaults
      // 
      this.cmdSetDefaults.Location = new System.Drawing.Point(138, 502);
      this.cmdSetDefaults.Name = "cmdSetDefaults";
      this.cmdSetDefaults.Size = new System.Drawing.Size(50, 50);
      this.cmdSetDefaults.TabIndex = 18;
      this.cmdSetDefaults.Text = "Load\r\nDefault\r\nValues";
      this.cmdSetDefaults.UseVisualStyleBackColor = true;
      this.cmdSetDefaults.Click += new System.EventHandler(this.cmdSetDefaults_Click);
      // 
      // frmColours
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(402, 564);
      this.ControlBox = false;
      this.Controls.Add(this.cmdSetDefaults);
      this.Controls.Add(this.cmdClose);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "frmColours";
      this.Text = "frmColours";
      this.Load += new System.EventHandler(this.frmColours_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ColorDialog colorDialog1;
    protected System.Windows.Forms.Button cmdClose;
    protected System.Windows.Forms.Button cmdSetDefaults;
  }
}