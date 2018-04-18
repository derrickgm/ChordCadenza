namespace ChordCadenza.Forms {
  partial class frmCalcKeys {
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
      this.cmdCalculate = new System.Windows.Forms.Button();
      this.lvMod = new System.Windows.Forms.ListView();
      this.grpAlgorithm = new System.Windows.Forms.GroupBox();
      this.optAlgWeighted = new System.Windows.Forms.RadioButton();
      this.optAlgFlat = new System.Windows.Forms.RadioButton();
      this.chkLoadTxt = new System.Windows.Forms.CheckBox();
      this.grpProfile = new System.Windows.Forms.GroupBox();
      this.optProfileSpecial = new System.Windows.Forms.RadioButton();
      this.optProfileJazz = new System.Windows.Forms.RadioButton();
      this.optProfileDefault = new System.Windows.Forms.RadioButton();
      this.grpInertia = new System.Windows.Forms.GroupBox();
      this.optPenalty110 = new System.Windows.Forms.RadioButton();
      this.optPenalty90 = new System.Windows.Forms.RadioButton();
      this.optPenalty70 = new System.Windows.Forms.RadioButton();
      this.optPenalty50 = new System.Windows.Forms.RadioButton();
      this.optPenalty30 = new System.Windows.Forms.RadioButton();
      this.optPenalty0 = new System.Windows.Forms.RadioButton();
      this.cmdApply = new System.Windows.Forms.Button();
      this.cmdOK = new System.Windows.Forms.Button();
      this.grpMinorKeyType = new System.Windows.Forms.GroupBox();
      this.chkUseHighestMinorKeyType = new System.Windows.Forms.CheckBox();
      this.lblTotSpecial = new System.Windows.Forms.Label();
      this.lblTotMelDown = new System.Windows.Forms.Label();
      this.lblTotMelUp = new System.Windows.Forms.Label();
      this.lblTotHarmonic = new System.Windows.Forms.Label();
      this.lblMinorSpecial = new System.Windows.Forms.Label();
      this.lblMinorMelDown = new System.Windows.Forms.Label();
      this.lblMinorMelUp = new System.Windows.Forms.Label();
      this.lblMinorHarmonic = new System.Windows.Forms.Label();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.chkChordFile = new System.Windows.Forms.CheckBox();
      this.lblTracksSelected = new System.Windows.Forms.Label();
      this.grpAlgorithm.SuspendLayout();
      this.grpProfile.SuspendLayout();
      this.grpInertia.SuspendLayout();
      this.grpMinorKeyType.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmdCalculate
      // 
      this.cmdCalculate.Location = new System.Drawing.Point(9, 316);
      this.cmdCalculate.Name = "cmdCalculate";
      this.cmdCalculate.Size = new System.Drawing.Size(111, 35);
      this.cmdCalculate.TabIndex = 37;
      this.cmdCalculate.Text = "Recalculate";
      this.cmdCalculate.UseVisualStyleBackColor = true;
      this.cmdCalculate.Click += new System.EventHandler(this.cmdCalculate_Click);
      // 
      // lvMod
      // 
      this.lvMod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.lvMod.FullRowSelect = true;
      this.lvMod.GridLines = true;
      this.lvMod.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lvMod.HideSelection = false;
      this.lvMod.Location = new System.Drawing.Point(141, 12);
      this.lvMod.Name = "lvMod";
      this.lvMod.ShowGroups = false;
      this.lvMod.Size = new System.Drawing.Size(333, 649);
      this.lvMod.TabIndex = 38;
      this.lvMod.UseCompatibleStateImageBehavior = false;
      this.lvMod.View = System.Windows.Forms.View.Details;
      // 
      // grpAlgorithm
      // 
      this.grpAlgorithm.Controls.Add(this.optAlgWeighted);
      this.grpAlgorithm.Controls.Add(this.optAlgFlat);
      this.grpAlgorithm.Location = new System.Drawing.Point(9, 109);
      this.grpAlgorithm.Name = "grpAlgorithm";
      this.grpAlgorithm.Size = new System.Drawing.Size(109, 59);
      this.grpAlgorithm.TabIndex = 40;
      this.grpAlgorithm.TabStop = false;
      this.grpAlgorithm.Text = "Algorithm";
      // 
      // optAlgWeighted
      // 
      this.optAlgWeighted.AutoSize = true;
      this.optAlgWeighted.Checked = true;
      this.optAlgWeighted.Location = new System.Drawing.Point(16, 35);
      this.optAlgWeighted.Name = "optAlgWeighted";
      this.optAlgWeighted.Size = new System.Drawing.Size(71, 17);
      this.optAlgWeighted.TabIndex = 1;
      this.optAlgWeighted.TabStop = true;
      this.optAlgWeighted.Text = "Weighted";
      this.optAlgWeighted.UseVisualStyleBackColor = true;
      // 
      // optAlgFlat
      // 
      this.optAlgFlat.AutoSize = true;
      this.optAlgFlat.Location = new System.Drawing.Point(16, 18);
      this.optAlgFlat.Name = "optAlgFlat";
      this.optAlgFlat.Size = new System.Drawing.Size(42, 17);
      this.optAlgFlat.TabIndex = 0;
      this.optAlgFlat.Text = "Flat";
      this.optAlgFlat.UseVisualStyleBackColor = true;
      // 
      // chkLoadTxt
      // 
      this.chkLoadTxt.AutoSize = true;
      this.chkLoadTxt.Checked = true;
      this.chkLoadTxt.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkLoadTxt.Location = new System.Drawing.Point(12, 60);
      this.chkLoadTxt.Name = "chkLoadTxt";
      this.chkLoadTxt.Size = new System.Drawing.Size(100, 17);
      this.chkLoadTxt.TabIndex = 41;
      this.chkLoadTxt.Text = "Load Text Keys";
      this.chkLoadTxt.UseVisualStyleBackColor = true;
      // 
      // grpProfile
      // 
      this.grpProfile.Controls.Add(this.optProfileSpecial);
      this.grpProfile.Controls.Add(this.optProfileJazz);
      this.grpProfile.Controls.Add(this.optProfileDefault);
      this.grpProfile.ForeColor = System.Drawing.Color.Red;
      this.grpProfile.Location = new System.Drawing.Point(9, 578);
      this.grpProfile.Name = "grpProfile";
      this.grpProfile.Size = new System.Drawing.Size(112, 85);
      this.grpProfile.TabIndex = 46;
      this.grpProfile.TabStop = false;
      this.grpProfile.Text = "Profile";
      // 
      // optProfileSpecial
      // 
      this.optProfileSpecial.AutoSize = true;
      this.optProfileSpecial.Location = new System.Drawing.Point(12, 59);
      this.optProfileSpecial.Name = "optProfileSpecial";
      this.optProfileSpecial.Size = new System.Drawing.Size(60, 17);
      this.optProfileSpecial.TabIndex = 2;
      this.optProfileSpecial.Text = "Special";
      this.optProfileSpecial.UseVisualStyleBackColor = true;
      // 
      // optProfileJazz
      // 
      this.optProfileJazz.AutoSize = true;
      this.optProfileJazz.Location = new System.Drawing.Point(12, 39);
      this.optProfileJazz.Name = "optProfileJazz";
      this.optProfileJazz.Size = new System.Drawing.Size(77, 17);
      this.optProfileJazz.TabIndex = 1;
      this.optProfileJazz.Text = "Jazz/Blues";
      this.optProfileJazz.UseVisualStyleBackColor = true;
      // 
      // optProfileDefault
      // 
      this.optProfileDefault.AutoSize = true;
      this.optProfileDefault.Checked = true;
      this.optProfileDefault.Location = new System.Drawing.Point(12, 19);
      this.optProfileDefault.Name = "optProfileDefault";
      this.optProfileDefault.Size = new System.Drawing.Size(59, 17);
      this.optProfileDefault.TabIndex = 0;
      this.optProfileDefault.TabStop = true;
      this.optProfileDefault.Text = "Default";
      this.optProfileDefault.UseVisualStyleBackColor = true;
      // 
      // grpInertia
      // 
      this.grpInertia.Controls.Add(this.optPenalty110);
      this.grpInertia.Controls.Add(this.optPenalty90);
      this.grpInertia.Controls.Add(this.optPenalty70);
      this.grpInertia.Controls.Add(this.optPenalty50);
      this.grpInertia.Controls.Add(this.optPenalty30);
      this.grpInertia.Controls.Add(this.optPenalty0);
      this.grpInertia.Location = new System.Drawing.Point(9, 174);
      this.grpInertia.Name = "grpInertia";
      this.grpInertia.Size = new System.Drawing.Size(73, 135);
      this.grpInertia.TabIndex = 48;
      this.grpInertia.TabStop = false;
      this.grpInertia.Text = "Inertia";
      // 
      // optPenalty110
      // 
      this.optPenalty110.AutoSize = true;
      this.optPenalty110.Location = new System.Drawing.Point(18, 110);
      this.optPenalty110.Name = "optPenalty110";
      this.optPenalty110.Size = new System.Drawing.Size(43, 17);
      this.optPenalty110.TabIndex = 5;
      this.optPenalty110.Text = "110";
      this.optPenalty110.UseVisualStyleBackColor = true;
      this.optPenalty110.CheckedChanged += new System.EventHandler(this.optPenalty_CheckedChanged);
      // 
      // optPenalty90
      // 
      this.optPenalty90.AutoSize = true;
      this.optPenalty90.Checked = true;
      this.optPenalty90.Location = new System.Drawing.Point(18, 92);
      this.optPenalty90.Name = "optPenalty90";
      this.optPenalty90.Size = new System.Drawing.Size(37, 17);
      this.optPenalty90.TabIndex = 4;
      this.optPenalty90.TabStop = true;
      this.optPenalty90.Text = "90";
      this.optPenalty90.UseVisualStyleBackColor = true;
      this.optPenalty90.CheckedChanged += new System.EventHandler(this.optPenalty_CheckedChanged);
      // 
      // optPenalty70
      // 
      this.optPenalty70.AutoSize = true;
      this.optPenalty70.Location = new System.Drawing.Point(18, 74);
      this.optPenalty70.Name = "optPenalty70";
      this.optPenalty70.Size = new System.Drawing.Size(37, 17);
      this.optPenalty70.TabIndex = 3;
      this.optPenalty70.Text = "70";
      this.optPenalty70.UseVisualStyleBackColor = true;
      this.optPenalty70.CheckedChanged += new System.EventHandler(this.optPenalty_CheckedChanged);
      // 
      // optPenalty50
      // 
      this.optPenalty50.AutoSize = true;
      this.optPenalty50.Location = new System.Drawing.Point(18, 56);
      this.optPenalty50.Name = "optPenalty50";
      this.optPenalty50.Size = new System.Drawing.Size(37, 17);
      this.optPenalty50.TabIndex = 2;
      this.optPenalty50.Text = "50";
      this.optPenalty50.UseVisualStyleBackColor = true;
      this.optPenalty50.CheckedChanged += new System.EventHandler(this.optPenalty_CheckedChanged);
      // 
      // optPenalty30
      // 
      this.optPenalty30.AutoSize = true;
      this.optPenalty30.Location = new System.Drawing.Point(18, 38);
      this.optPenalty30.Name = "optPenalty30";
      this.optPenalty30.Size = new System.Drawing.Size(37, 17);
      this.optPenalty30.TabIndex = 1;
      this.optPenalty30.Text = "30";
      this.optPenalty30.UseVisualStyleBackColor = true;
      this.optPenalty30.CheckedChanged += new System.EventHandler(this.optPenalty_CheckedChanged);
      // 
      // optPenalty0
      // 
      this.optPenalty0.AutoSize = true;
      this.optPenalty0.Location = new System.Drawing.Point(18, 20);
      this.optPenalty0.Name = "optPenalty0";
      this.optPenalty0.Size = new System.Drawing.Size(31, 17);
      this.optPenalty0.TabIndex = 0;
      this.optPenalty0.Text = "0";
      this.optPenalty0.UseVisualStyleBackColor = true;
      this.optPenalty0.CheckedChanged += new System.EventHandler(this.optPenalty_CheckedChanged);
      // 
      // cmdApply
      // 
      this.cmdApply.Location = new System.Drawing.Point(9, 357);
      this.cmdApply.Name = "cmdApply";
      this.cmdApply.Size = new System.Drawing.Size(111, 27);
      this.cmdApply.TabIndex = 50;
      this.cmdApply.Text = "Apply";
      this.cmdApply.UseVisualStyleBackColor = true;
      this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
      // 
      // cmdOK
      // 
      this.cmdOK.Location = new System.Drawing.Point(8, 390);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(111, 27);
      this.cmdOK.TabIndex = 51;
      this.cmdOK.Text = "Apply and Close";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
      // 
      // grpMinorKeyType
      // 
      this.grpMinorKeyType.Controls.Add(this.chkUseHighestMinorKeyType);
      this.grpMinorKeyType.Controls.Add(this.lblTotSpecial);
      this.grpMinorKeyType.Controls.Add(this.lblTotMelDown);
      this.grpMinorKeyType.Controls.Add(this.lblTotMelUp);
      this.grpMinorKeyType.Controls.Add(this.lblTotHarmonic);
      this.grpMinorKeyType.Controls.Add(this.lblMinorSpecial);
      this.grpMinorKeyType.Controls.Add(this.lblMinorMelDown);
      this.grpMinorKeyType.Controls.Add(this.lblMinorMelUp);
      this.grpMinorKeyType.Controls.Add(this.lblMinorHarmonic);
      this.grpMinorKeyType.ForeColor = System.Drawing.Color.Red;
      this.grpMinorKeyType.Location = new System.Drawing.Point(9, 456);
      this.grpMinorKeyType.Name = "grpMinorKeyType";
      this.grpMinorKeyType.Size = new System.Drawing.Size(112, 116);
      this.grpMinorKeyType.TabIndex = 53;
      this.grpMinorKeyType.TabStop = false;
      this.grpMinorKeyType.Text = "Minor Key Scores";
      // 
      // chkUseHighestMinorKeyType
      // 
      this.chkUseHighestMinorKeyType.Checked = true;
      this.chkUseHighestMinorKeyType.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkUseHighestMinorKeyType.Location = new System.Drawing.Point(8, 88);
      this.chkUseHighestMinorKeyType.Name = "chkUseHighestMinorKeyType";
      this.chkUseHighestMinorKeyType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.chkUseHighestMinorKeyType.Size = new System.Drawing.Size(95, 19);
      this.chkUseHighestMinorKeyType.TabIndex = 54;
      this.chkUseHighestMinorKeyType.Text = "Use Highest";
      this.chkUseHighestMinorKeyType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.chkUseHighestMinorKeyType.UseVisualStyleBackColor = true;
      // 
      // lblTotSpecial
      // 
      this.lblTotSpecial.Location = new System.Drawing.Point(70, 71);
      this.lblTotSpecial.Name = "lblTotSpecial";
      this.lblTotSpecial.Size = new System.Drawing.Size(30, 13);
      this.lblTotSpecial.TabIndex = 61;
      this.lblTotSpecial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblTotMelDown
      // 
      this.lblTotMelDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTotMelDown.Location = new System.Drawing.Point(70, 53);
      this.lblTotMelDown.Name = "lblTotMelDown";
      this.lblTotMelDown.Size = new System.Drawing.Size(30, 13);
      this.lblTotMelDown.TabIndex = 60;
      this.lblTotMelDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblTotMelUp
      // 
      this.lblTotMelUp.Location = new System.Drawing.Point(70, 34);
      this.lblTotMelUp.Name = "lblTotMelUp";
      this.lblTotMelUp.Size = new System.Drawing.Size(30, 13);
      this.lblTotMelUp.TabIndex = 59;
      this.lblTotMelUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblTotHarmonic
      // 
      this.lblTotHarmonic.Location = new System.Drawing.Point(70, 16);
      this.lblTotHarmonic.Name = "lblTotHarmonic";
      this.lblTotHarmonic.Size = new System.Drawing.Size(30, 13);
      this.lblTotHarmonic.TabIndex = 58;
      this.lblTotHarmonic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblMinorSpecial
      // 
      this.lblMinorSpecial.AutoSize = true;
      this.lblMinorSpecial.Location = new System.Drawing.Point(9, 71);
      this.lblMinorSpecial.Name = "lblMinorSpecial";
      this.lblMinorSpecial.Size = new System.Drawing.Size(42, 13);
      this.lblMinorSpecial.TabIndex = 57;
      this.lblMinorSpecial.Text = "Special";
      // 
      // lblMinorMelDown
      // 
      this.lblMinorMelDown.AutoSize = true;
      this.lblMinorMelDown.Location = new System.Drawing.Point(9, 53);
      this.lblMinorMelDown.Name = "lblMinorMelDown";
      this.lblMinorMelDown.Size = new System.Drawing.Size(55, 13);
      this.lblMinorMelDown.TabIndex = 56;
      this.lblMinorMelDown.Text = "Mel Down";
      // 
      // lblMinorMelUp
      // 
      this.lblMinorMelUp.AutoSize = true;
      this.lblMinorMelUp.Location = new System.Drawing.Point(9, 34);
      this.lblMinorMelUp.Name = "lblMinorMelUp";
      this.lblMinorMelUp.Size = new System.Drawing.Size(41, 13);
      this.lblMinorMelUp.TabIndex = 55;
      this.lblMinorMelUp.Text = "Mel Up";
      // 
      // lblMinorHarmonic
      // 
      this.lblMinorHarmonic.AutoSize = true;
      this.lblMinorHarmonic.Location = new System.Drawing.Point(9, 16);
      this.lblMinorHarmonic.Name = "lblMinorHarmonic";
      this.lblMinorHarmonic.Size = new System.Drawing.Size(52, 13);
      this.lblMinorHarmonic.TabIndex = 54;
      this.lblMinorHarmonic.Text = "Harmonic";
      // 
      // cmdHelp
      // 
      this.cmdHelp.Location = new System.Drawing.Point(8, 423);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(111, 27);
      this.cmdHelp.TabIndex = 54;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // chkChordFile
      // 
      this.chkChordFile.AutoSize = true;
      this.chkChordFile.Location = new System.Drawing.Point(12, 81);
      this.chkChordFile.Name = "chkChordFile";
      this.chkChordFile.Size = new System.Drawing.Size(92, 17);
      this.chkChordFile.TabIndex = 55;
      this.chkChordFile.Text = "Use ChordFile";
      this.chkChordFile.UseVisualStyleBackColor = true;
      this.chkChordFile.CheckedChanged += new System.EventHandler(this.chkChordFile_CheckedChanged);
      // 
      // lblTracksSelected
      // 
      this.lblTracksSelected.AutoSize = true;
      this.lblTracksSelected.Location = new System.Drawing.Point(9, 12);
      this.lblTracksSelected.Name = "lblTracksSelected";
      this.lblTracksSelected.Size = new System.Drawing.Size(61, 26);
      this.lblTracksSelected.TabIndex = 56;
      this.lblTracksSelected.Text = "??? Tracks\r\nSelected";
      // 
      // frmCalcKeys
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(480, 673);
      this.Controls.Add(this.lblTracksSelected);
      this.Controls.Add(this.chkChordFile);
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.grpMinorKeyType);
      this.Controls.Add(this.cmdOK);
      this.Controls.Add(this.cmdCalculate);
      this.Controls.Add(this.cmdApply);
      this.Controls.Add(this.grpInertia);
      this.Controls.Add(this.grpProfile);
      this.Controls.Add(this.chkLoadTxt);
      this.Controls.Add(this.grpAlgorithm);
      this.Controls.Add(this.lvMod);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "frmCalcKeys";
      this.Text = "frmCalcKeys";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCalcKeys_FormClosed);
      this.Load += new System.EventHandler(this.frmCalcKeys_Load);
      this.grpAlgorithm.ResumeLayout(false);
      this.grpAlgorithm.PerformLayout();
      this.grpProfile.ResumeLayout(false);
      this.grpProfile.PerformLayout();
      this.grpInertia.ResumeLayout(false);
      this.grpInertia.PerformLayout();
      this.grpMinorKeyType.ResumeLayout(false);
      this.grpMinorKeyType.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdCalculate;
    private System.Windows.Forms.ListView lvMod;
    private System.Windows.Forms.GroupBox grpAlgorithm;
    private System.Windows.Forms.CheckBox chkLoadTxt;
    private System.Windows.Forms.RadioButton optAlgWeighted;
    private System.Windows.Forms.RadioButton optAlgFlat;
    private System.Windows.Forms.GroupBox grpProfile;
    internal System.Windows.Forms.RadioButton optProfileSpecial;
    internal System.Windows.Forms.RadioButton optProfileJazz;
    internal System.Windows.Forms.RadioButton optProfileDefault;
    private System.Windows.Forms.GroupBox grpInertia;
    private System.Windows.Forms.RadioButton optPenalty30;
    private System.Windows.Forms.RadioButton optPenalty0;
    private System.Windows.Forms.RadioButton optPenalty110;
    private System.Windows.Forms.RadioButton optPenalty90;
    private System.Windows.Forms.RadioButton optPenalty70;
    private System.Windows.Forms.RadioButton optPenalty50;
    private System.Windows.Forms.Button cmdApply;
    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.GroupBox grpMinorKeyType;
    private System.Windows.Forms.Label lblMinorMelUp;
    private System.Windows.Forms.Label lblMinorHarmonic;
    private System.Windows.Forms.Label lblMinorSpecial;
    private System.Windows.Forms.Label lblMinorMelDown;
    private System.Windows.Forms.Label lblTotSpecial;
    private System.Windows.Forms.Label lblTotMelDown;
    private System.Windows.Forms.Label lblTotMelUp;
    private System.Windows.Forms.Label lblTotHarmonic;
    private System.Windows.Forms.CheckBox chkUseHighestMinorKeyType;
    private System.Windows.Forms.Button cmdHelp;
    private System.Windows.Forms.CheckBox chkChordFile;
    private System.Windows.Forms.Label lblTracksSelected;
  }
}