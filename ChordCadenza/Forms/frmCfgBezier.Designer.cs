namespace ChordCadenza.Forms {
  partial class frmCfgBezier {
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
      this.pic = new System.Windows.Forms.PictureBox();
      this.lblLitIn = new System.Windows.Forms.Label();
      this.lblLitOut = new System.Windows.Forms.Label();
      this.cmdClose = new System.Windows.Forms.Button();
      this.cmdLoadDflts = new System.Windows.Forms.Button();
      this.cmdHelp = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
      this.SuspendLayout();
      // 
      // pic
      // 
      this.pic.BackColor = System.Drawing.Color.White;
      this.pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pic.Location = new System.Drawing.Point(62, 22);
      this.pic.Name = "pic";
      this.pic.Size = new System.Drawing.Size(454, 454);
      this.pic.TabIndex = 0;
      this.pic.TabStop = false;
      this.pic.Paint += new System.Windows.Forms.PaintEventHandler(this.pic_Paint);
      this.pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);
      this.pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_MouseMove);
      this.pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_MouseUp);
      // 
      // lblLitIn
      // 
      this.lblLitIn.AutoSize = true;
      this.lblLitIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLitIn.Location = new System.Drawing.Point(74, 479);
      this.lblLitIn.Name = "lblLitIn";
      this.lblLitIn.Size = new System.Drawing.Size(25, 20);
      this.lblLitIn.TabIndex = 1;
      this.lblLitIn.Text = "In";
      // 
      // lblLitOut
      // 
      this.lblLitOut.AutoSize = true;
      this.lblLitOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLitOut.Location = new System.Drawing.Point(12, 445);
      this.lblLitOut.Name = "lblLitOut";
      this.lblLitOut.Size = new System.Drawing.Size(38, 20);
      this.lblLitOut.TabIndex = 2;
      this.lblLitOut.Text = "Out";
      // 
      // cmdClose
      // 
      this.cmdClose.Location = new System.Drawing.Point(466, 488);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(50, 50);
      this.cmdClose.TabIndex = 9;
      this.cmdClose.Text = "Close";
      this.cmdClose.UseVisualStyleBackColor = true;
      this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
      // 
      // cmdLoadDflts
      // 
      this.cmdLoadDflts.Location = new System.Drawing.Point(325, 488);
      this.cmdLoadDflts.Name = "cmdLoadDflts";
      this.cmdLoadDflts.Size = new System.Drawing.Size(50, 50);
      this.cmdLoadDflts.TabIndex = 11;
      this.cmdLoadDflts.Text = "Load\r\nDefault\r\nValues";
      this.cmdLoadDflts.UseVisualStyleBackColor = true;
      this.cmdLoadDflts.Click += new System.EventHandler(this.cmdLoadDflts_Click);
      // 
      // cmdHelp
      // 
      this.cmdHelp.Location = new System.Drawing.Point(410, 488);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(50, 50);
      this.cmdHelp.TabIndex = 12;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // frmCfgBezier
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(552, 553);
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.cmdLoadDflts);
      this.Controls.Add(this.cmdClose);
      this.Controls.Add(this.lblLitOut);
      this.Controls.Add(this.lblLitIn);
      this.Controls.Add(this.pic);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmCfgBezier";
      this.Text = "Configure Bezier Curve - Chord Cadenza";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCfgBezier_FormClosed);
      this.Load += new System.EventHandler(this.frmNewCfgBezier_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pic;
    private System.Windows.Forms.Label lblLitIn;
    private System.Windows.Forms.Label lblLitOut;
    private System.Windows.Forms.Button cmdClose;
    private System.Windows.Forms.Button cmdLoadDflts;
    private System.Windows.Forms.Button cmdHelp;
  }
}