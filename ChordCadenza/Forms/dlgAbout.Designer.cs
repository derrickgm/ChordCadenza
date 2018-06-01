namespace ChordCadenza.Forms {
  partial class dlgAbout {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAbout));
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.lblName = new System.Windows.Forms.Label();
      this.lblVRMF = new System.Windows.Forms.Label();
      this.lblCopyright = new System.Windows.Forms.Label();
      this.txtInfo = new System.Windows.Forms.TextBox();
      this.cmdOK = new System.Windows.Forms.Button();
      this.lnkHomePage = new System.Windows.Forms.LinkLabel();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lnkEmail = new System.Windows.Forms.LinkLabel();
      this.panLinks = new System.Windows.Forms.Panel();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.panLinks.SuspendLayout();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(33, 15);
      this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(75, 75);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      // 
      // lblName
      // 
      this.lblName.AutoSize = true;
      this.lblName.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblName.Location = new System.Drawing.Point(146, 39);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(218, 32);
      this.lblName.TabIndex = 1;
      this.lblName.Text = "Chord Cadenza";
      // 
      // lblVRMF
      // 
      this.lblVRMF.AutoSize = true;
      this.lblVRMF.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblVRMF.Location = new System.Drawing.Point(69, 134);
      this.lblVRMF.Name = "lblVRMF";
      this.lblVRMF.Size = new System.Drawing.Size(97, 16);
      this.lblVRMF.TabIndex = 2;
      this.lblVRMF.Text = "Chord Cadenza";
      // 
      // lblCopyright
      // 
      this.lblCopyright.AutoSize = true;
      this.lblCopyright.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCopyright.Location = new System.Drawing.Point(69, 169);
      this.lblCopyright.Name = "lblCopyright";
      this.lblCopyright.Size = new System.Drawing.Size(193, 32);
      this.lblCopyright.TabIndex = 3;
      this.lblCopyright.Text = "© Copyright 2018 Derrick Maule\r\nAll Rights Reserved";
      // 
      // txtInfo
      // 
      this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtInfo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtInfo.Location = new System.Drawing.Point(72, 226);
      this.txtInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtInfo.Multiline = true;
      this.txtInfo.Name = "txtInfo";
      this.txtInfo.ReadOnly = true;
      this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtInfo.Size = new System.Drawing.Size(347, 181);
      this.txtInfo.TabIndex = 4;
      // 
      // cmdOK
      // 
      this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdOK.Location = new System.Drawing.Point(337, 492);
      this.cmdOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(82, 34);
      this.cmdOK.TabIndex = 5;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
      // 
      // lnkHomePage
      // 
      this.lnkHomePage.AutoSize = true;
      this.lnkHomePage.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lnkHomePage.Location = new System.Drawing.Point(86, 9);
      this.lnkHomePage.Name = "lnkHomePage";
      this.lnkHomePage.Size = new System.Drawing.Size(111, 16);
      this.lnkHomePage.TabIndex = 6;
      this.lnkHomePage.TabStop = true;
      this.lnkHomePage.Text = "chordcadenza.org";
      this.lnkHomePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHomePage_LinkClicked);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(73, 16);
      this.label1.TabIndex = 7;
      this.label1.Text = "Web Page:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 36);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(45, 16);
      this.label2.TabIndex = 9;
      this.label2.Text = "Email:";
      // 
      // lnkEmail
      // 
      this.lnkEmail.AutoSize = true;
      this.lnkEmail.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lnkEmail.Location = new System.Drawing.Point(86, 36);
      this.lnkEmail.Name = "lnkEmail";
      this.lnkEmail.Size = new System.Drawing.Size(188, 16);
      this.lnkEmail.TabIndex = 8;
      this.lnkEmail.TabStop = true;
      this.lnkEmail.Text = "appsupport@chordcadenza.org";
      this.lnkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEmail_LinkClicked);
      // 
      // panLinks
      // 
      this.panLinks.Controls.Add(this.label1);
      this.panLinks.Controls.Add(this.label2);
      this.panLinks.Controls.Add(this.lnkHomePage);
      this.panLinks.Controls.Add(this.lnkEmail);
      this.panLinks.Location = new System.Drawing.Point(72, 414);
      this.panLinks.Name = "panLinks";
      this.panLinks.Size = new System.Drawing.Size(330, 71);
      this.panLinks.TabIndex = 10;
      // 
      // dlgAbout
      // 
      this.AcceptButton = this.cmdOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdOK;
      this.ClientSize = new System.Drawing.Size(440, 536);
      this.ControlBox = false;
      this.Controls.Add(this.panLinks);
      this.Controls.Add(this.cmdOK);
      this.Controls.Add(this.txtInfo);
      this.Controls.Add(this.lblCopyright);
      this.Controls.Add(this.lblVRMF);
      this.Controls.Add(this.lblName);
      this.Controls.Add(this.pictureBox1);
      this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "dlgAbout";
      this.Text = "About - Chord Cadenza";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.frmAbout_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.panLinks.ResumeLayout(false);
      this.panLinks.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.Label lblVRMF;
    private System.Windows.Forms.Label lblCopyright;
    private System.Windows.Forms.TextBox txtInfo;
    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.LinkLabel lnkHomePage;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.LinkLabel lnkEmail;
    private System.Windows.Forms.Panel panLinks;
  }
}