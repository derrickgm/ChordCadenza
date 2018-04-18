namespace ChordCadenza.Forms {
  partial class dlgChangeSongLength {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgChangeSongLength));
      this.cmdCancel = new System.Windows.Forms.Button();
      this.cmdOK = new System.Windows.Forms.Button();
      this.lblInfo = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.lblCurrentLen = new System.Windows.Forms.Label();
      this.lblMinLen = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.nudNewLen = new System.Windows.Forms.NumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      this.panel1 = new System.Windows.Forms.Panel();
      this.lblNewBars = new System.Windows.Forms.Label();
      this.lblMinBars = new System.Windows.Forms.Label();
      this.lblCurrentBars = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.nudNewLen)).BeginInit();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmdCancel
      // 
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Location = new System.Drawing.Point(410, 299);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(50, 50);
      this.cmdCancel.TabIndex = 8;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      // 
      // cmdOK
      // 
      this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdOK.Location = new System.Drawing.Point(356, 299);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(50, 50);
      this.cmdOK.TabIndex = 7;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
      // 
      // lblInfo
      // 
      this.lblInfo.AutoSize = true;
      this.lblInfo.Location = new System.Drawing.Point(26, 24);
      this.lblInfo.Name = "lblInfo";
      this.lblInfo.Size = new System.Drawing.Size(431, 78);
      this.lblInfo.TabIndex = 9;
      this.lblInfo.Text = resources.GetString("lblInfo.Text");
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 32);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(80, 13);
      this.label1.TabIndex = 10;
      this.label1.Text = "Current Length:";
      // 
      // lblCurrentLen
      // 
      this.lblCurrentLen.AutoSize = true;
      this.lblCurrentLen.Location = new System.Drawing.Point(96, 32);
      this.lblCurrentLen.Name = "lblCurrentLen";
      this.lblCurrentLen.Size = new System.Drawing.Size(25, 13);
      this.lblCurrentLen.TabIndex = 11;
      this.lblCurrentLen.Text = "???";
      // 
      // lblMinLen
      // 
      this.lblMinLen.AutoSize = true;
      this.lblMinLen.Location = new System.Drawing.Point(96, 55);
      this.lblMinLen.Name = "lblMinLen";
      this.lblMinLen.Size = new System.Drawing.Size(25, 13);
      this.lblMinLen.TabIndex = 13;
      this.lblMinLen.Text = "???";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 55);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(87, 13);
      this.label3.TabIndex = 12;
      this.label3.Text = "Minimum Length:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(3, 79);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(68, 13);
      this.label4.TabIndex = 14;
      this.label4.Text = "New Length:";
      // 
      // nudNewLen
      // 
      this.nudNewLen.Location = new System.Drawing.Point(99, 77);
      this.nudNewLen.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.nudNewLen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudNewLen.Name = "nudNewLen";
      this.nudNewLen.Size = new System.Drawing.Size(41, 20);
      this.nudNewLen.TabIndex = 15;
      this.nudNewLen.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.nudNewLen.ValueChanged += new System.EventHandler(this.nudNewLen_ValueChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(26, 242);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(316, 26);
      this.label2.TabIndex = 16;
      this.label2.Text = "Note: The minimum length may be greater than the current length.\r\nThis is to allo" +
    "w for rounding errors and expansion.\r\n";
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel1.Controls.Add(this.lblNewBars);
      this.panel1.Controls.Add(this.lblMinBars);
      this.panel1.Controls.Add(this.lblCurrentBars);
      this.panel1.Controls.Add(this.label6);
      this.panel1.Controls.Add(this.label5);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.lblCurrentLen);
      this.panel1.Controls.Add(this.nudNewLen);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.label4);
      this.panel1.Controls.Add(this.lblMinLen);
      this.panel1.Location = new System.Drawing.Point(29, 115);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(431, 110);
      this.panel1.TabIndex = 17;
      // 
      // lblNewBars
      // 
      this.lblNewBars.AutoSize = true;
      this.lblNewBars.Location = new System.Drawing.Point(173, 81);
      this.lblNewBars.Name = "lblNewBars";
      this.lblNewBars.Size = new System.Drawing.Size(25, 13);
      this.lblNewBars.TabIndex = 20;
      this.lblNewBars.Text = "???";
      // 
      // lblMinBars
      // 
      this.lblMinBars.AutoSize = true;
      this.lblMinBars.Location = new System.Drawing.Point(173, 55);
      this.lblMinBars.Name = "lblMinBars";
      this.lblMinBars.Size = new System.Drawing.Size(25, 13);
      this.lblMinBars.TabIndex = 19;
      this.lblMinBars.Text = "???";
      // 
      // lblCurrentBars
      // 
      this.lblCurrentBars.AutoSize = true;
      this.lblCurrentBars.Location = new System.Drawing.Point(173, 32);
      this.lblCurrentBars.Name = "lblCurrentBars";
      this.lblCurrentBars.Size = new System.Drawing.Size(25, 13);
      this.lblCurrentBars.TabIndex = 18;
      this.lblCurrentBars.Text = "???";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(173, 8);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(109, 13);
      this.label6.TabIndex = 17;
      this.label6.Text = "Bars (incl. End Buffer)";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(96, 8);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(66, 13);
      this.label5.TabIndex = 16;
      this.label5.Text = "WholeNotes";
      // 
      // frmChangeSongLength
      // 
      this.AcceptButton = this.cmdOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(474, 363);
      this.ControlBox = false;
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.lblInfo);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.cmdOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmChangeSongLength";
      this.Text = "Change Song Length";
      this.Load += new System.EventHandler(this.frmChangeSongLength_Load);
      ((System.ComponentModel.ISupportInitialize)(this.nudNewLen)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.Label lblInfo;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblCurrentLen;
    private System.Windows.Forms.Label lblMinLen;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown nudNewLen;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label lblNewBars;
    private System.Windows.Forms.Label lblMinBars;
    private System.Windows.Forms.Label lblCurrentBars;
  }
}