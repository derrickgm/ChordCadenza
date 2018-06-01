namespace ChordCadenza.Forms {
  partial class frmSCOctaves {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSCOctaves));
      this.cmbLowShowC = new System.Windows.Forms.ComboBox();
      this.lblLowC = new System.Windows.Forms.Label();
      this.lblPlayLoC = new System.Windows.Forms.Label();
      this.cmbPlayLoC = new System.Windows.Forms.ComboBox();
      this.lblPlayHiC = new System.Windows.Forms.Label();
      this.cmbPlayHiC = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.lblNoteName = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.cmdClose = new System.Windows.Forms.Button();
      this.cmdApply = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // cmbLowShowC
      // 
      this.cmbLowShowC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbLowShowC.FormattingEnabled = true;
      this.cmbLowShowC.Location = new System.Drawing.Point(164, 100);
      this.cmbLowShowC.Name = "cmbLowShowC";
      this.cmbLowShowC.Size = new System.Drawing.Size(83, 21);
      this.cmbLowShowC.TabIndex = 0;
      this.cmbLowShowC.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
      // 
      // lblLowC
      // 
      this.lblLowC.AutoSize = true;
      this.lblLowC.Location = new System.Drawing.Point(60, 105);
      this.lblLowC.Name = "lblLowC";
      this.lblLowC.Size = new System.Drawing.Size(100, 13);
      this.lblLowC.TabIndex = 4;
      this.lblLowC.Text = "Lowest Displayed C";
      // 
      // lblPlayLoC
      // 
      this.lblPlayLoC.AutoSize = true;
      this.lblPlayLoC.Location = new System.Drawing.Point(59, 45);
      this.lblPlayLoC.Name = "lblPlayLoC";
      this.lblPlayLoC.Size = new System.Drawing.Size(102, 26);
      this.lblPlayLoC.TabIndex = 6;
      this.lblPlayLoC.Text = "Lowest Playable C\r\n(Midi Keyboard only)";
      // 
      // cmbPlayLoC
      // 
      this.cmbPlayLoC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbPlayLoC.FormattingEnabled = true;
      this.cmbPlayLoC.Location = new System.Drawing.Point(164, 45);
      this.cmbPlayLoC.Name = "cmbPlayLoC";
      this.cmbPlayLoC.Size = new System.Drawing.Size(83, 21);
      this.cmbPlayLoC.TabIndex = 5;
      this.cmbPlayLoC.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
      // 
      // lblPlayHiC
      // 
      this.lblPlayHiC.AutoSize = true;
      this.lblPlayHiC.ForeColor = System.Drawing.Color.Red;
      this.lblPlayHiC.Location = new System.Drawing.Point(64, 172);
      this.lblPlayHiC.Name = "lblPlayHiC";
      this.lblPlayHiC.Size = new System.Drawing.Size(96, 13);
      this.lblPlayHiC.TabIndex = 8;
      this.lblPlayHiC.Text = "Highest Playable C";
      // 
      // cmbPlayHiC
      // 
      this.cmbPlayHiC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbPlayHiC.ForeColor = System.Drawing.Color.Red;
      this.cmbPlayHiC.FormattingEnabled = true;
      this.cmbPlayHiC.Location = new System.Drawing.Point(164, 167);
      this.cmbPlayHiC.Name = "cmbPlayHiC";
      this.cmbPlayHiC.Size = new System.Drawing.Size(83, 21);
      this.cmbPlayHiC.TabIndex = 7;
      this.cmbPlayHiC.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 14);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(151, 13);
      this.label3.TabIndex = 9;
      this.label3.Text = "Last Note Played on Keyboard";
      // 
      // lblNoteName
      // 
      this.lblNoteName.AllowDrop = true;
      this.lblNoteName.BackColor = System.Drawing.Color.White;
      this.lblNoteName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblNoteName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblNoteName.Location = new System.Drawing.Point(169, 9);
      this.lblNoteName.Name = "lblNoteName";
      this.lblNoteName.Size = new System.Drawing.Size(78, 23);
      this.lblNoteName.TabIndex = 10;
      this.lblNoteName.Text = "???";
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(263, 45);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(465, 39);
      this.label2.TabIndex = 13;
      this.label2.Text = "This should be set to the lowest \'C\' on your midi keyboard. \r\nIf you want to use " +
    "the  bottom octave for SwitchKeys, set this to one octave above the lowest \'C\' o" +
    "n your keyboard.";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(263, 97);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(465, 52);
      this.label4.TabIndex = 14;
      this.label4.Text = resources.GetString("label4.Text");
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(263, 14);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(431, 13);
      this.label5.TabIndex = 15;
      this.label5.Text = "Play a note on your midi or PC keyboard to display the midi pitch that is actuall" +
    "y generated.";
      // 
      // cmdClose
      // 
      this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdClose.Location = new System.Drawing.Point(632, 177);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(89, 37);
      this.cmdClose.TabIndex = 16;
      this.cmdClose.Text = "Close Window";
      this.cmdClose.UseVisualStyleBackColor = true;
      this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
      // 
      // cmdApply
      // 
      this.cmdApply.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdApply.Location = new System.Drawing.Point(537, 177);
      this.cmdApply.Name = "cmdApply";
      this.cmdApply.Size = new System.Drawing.Size(89, 37);
      this.cmdApply.TabIndex = 17;
      this.cmdApply.Text = "Apply";
      this.cmdApply.UseVisualStyleBackColor = true;
      this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
      // 
      // frmSCOctaves
      // 
      this.AcceptButton = this.cmdApply;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdClose;
      this.ClientSize = new System.Drawing.Size(735, 226);
      this.Controls.Add(this.cmdApply);
      this.Controls.Add(this.cmdClose);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.lblNoteName);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.lblPlayHiC);
      this.Controls.Add(this.cmbPlayHiC);
      this.Controls.Add(this.lblPlayLoC);
      this.Controls.Add(this.cmbPlayLoC);
      this.Controls.Add(this.lblLowC);
      this.Controls.Add(this.cmbLowShowC);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmSCOctaves";
      this.Text = "Config Keyboard Ranges - Chord Cadenza";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSCOctaves_FormClosed);
      this.Load += new System.EventHandler(this.frmOctaves_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox cmbLowShowC;
    private System.Windows.Forms.Label lblLowC;
    private System.Windows.Forms.Label lblPlayHiC;
    private System.Windows.Forms.ComboBox cmbPlayHiC;
    private System.Windows.Forms.Label label3;
    internal System.Windows.Forms.Label lblNoteName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button cmdClose;
    private System.Windows.Forms.Button cmdApply;
    internal System.Windows.Forms.Label lblPlayLoC;
    internal System.Windows.Forms.ComboBox cmbPlayLoC;
  }
}