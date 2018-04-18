namespace ChordCadenza.Forms {
  partial class frmFX {
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
      this.grpFreeverb = new System.Windows.Forms.GroupBox();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.lblWidth = new System.Windows.Forms.Label();
      this.trkWidth = new System.Windows.Forms.TrackBar();
      this.cmdResetFreeverb = new System.Windows.Forms.Button();
      this.lblDamp = new System.Windows.Forms.Label();
      this.chkEnableFreeverb = new System.Windows.Forms.CheckBox();
      this.trkDamp = new System.Windows.Forms.TrackBar();
      this.lblRoomSize = new System.Windows.Forms.Label();
      this.trkRoomSize = new System.Windows.Forms.TrackBar();
      this.lblWetMix = new System.Windows.Forms.Label();
      this.trkWetMix = new System.Windows.Forms.TrackBar();
      this.lblDryMix = new System.Windows.Forms.Label();
      this.trkDryMix = new System.Windows.Forms.TrackBar();
      this.grpFreeverb.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trkWidth)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkDamp)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkRoomSize)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkWetMix)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkDryMix)).BeginInit();
      this.SuspendLayout();
      // 
      // grpFreeverb
      // 
      this.grpFreeverb.Controls.Add(this.cmdHelp);
      this.grpFreeverb.Controls.Add(this.lblWidth);
      this.grpFreeverb.Controls.Add(this.trkWidth);
      this.grpFreeverb.Controls.Add(this.cmdResetFreeverb);
      this.grpFreeverb.Controls.Add(this.lblDamp);
      this.grpFreeverb.Controls.Add(this.chkEnableFreeverb);
      this.grpFreeverb.Controls.Add(this.trkDamp);
      this.grpFreeverb.Controls.Add(this.lblRoomSize);
      this.grpFreeverb.Controls.Add(this.trkRoomSize);
      this.grpFreeverb.Controls.Add(this.lblWetMix);
      this.grpFreeverb.Controls.Add(this.trkWetMix);
      this.grpFreeverb.Controls.Add(this.lblDryMix);
      this.grpFreeverb.Controls.Add(this.trkDryMix);
      this.grpFreeverb.Location = new System.Drawing.Point(12, 12);
      this.grpFreeverb.Name = "grpFreeverb";
      this.grpFreeverb.Size = new System.Drawing.Size(471, 154);
      this.grpFreeverb.TabIndex = 1;
      this.grpFreeverb.TabStop = false;
      this.grpFreeverb.Text = "Freeverb";
      // 
      // cmdHelp
      // 
      this.cmdHelp.Location = new System.Drawing.Point(19, 111);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(52, 22);
      this.cmdHelp.TabIndex = 10;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // lblWidth
      // 
      this.lblWidth.AutoSize = true;
      this.lblWidth.Location = new System.Drawing.Point(93, 116);
      this.lblWidth.Name = "lblWidth";
      this.lblWidth.Size = new System.Drawing.Size(35, 13);
      this.lblWidth.TabIndex = 9;
      this.lblWidth.Text = "Width";
      // 
      // trkWidth
      // 
      this.trkWidth.AutoSize = false;
      this.trkWidth.Location = new System.Drawing.Point(160, 113);
      this.trkWidth.Maximum = 100;
      this.trkWidth.Name = "trkWidth";
      this.trkWidth.Size = new System.Drawing.Size(272, 24);
      this.trkWidth.TabIndex = 8;
      this.trkWidth.TickFrequency = 10;
      this.trkWidth.Scroll += new System.EventHandler(this.trkWidth_Scroll);
      // 
      // cmdResetFreeverb
      // 
      this.cmdResetFreeverb.Location = new System.Drawing.Point(19, 45);
      this.cmdResetFreeverb.Name = "cmdResetFreeverb";
      this.cmdResetFreeverb.Size = new System.Drawing.Size(52, 22);
      this.cmdResetFreeverb.TabIndex = 2;
      this.cmdResetFreeverb.Text = "Reset";
      this.cmdResetFreeverb.UseVisualStyleBackColor = true;
      this.cmdResetFreeverb.Click += new System.EventHandler(this.cmdResetFreeverb_Click);
      // 
      // lblDamp
      // 
      this.lblDamp.AutoSize = true;
      this.lblDamp.Location = new System.Drawing.Point(93, 93);
      this.lblDamp.Name = "lblDamp";
      this.lblDamp.Size = new System.Drawing.Size(35, 13);
      this.lblDamp.TabIndex = 7;
      this.lblDamp.Text = "Damp";
      // 
      // chkEnableFreeverb
      // 
      this.chkEnableFreeverb.AutoSize = true;
      this.chkEnableFreeverb.Location = new System.Drawing.Point(19, 22);
      this.chkEnableFreeverb.Name = "chkEnableFreeverb";
      this.chkEnableFreeverb.Size = new System.Drawing.Size(56, 17);
      this.chkEnableFreeverb.TabIndex = 1;
      this.chkEnableFreeverb.Text = "Active";
      this.chkEnableFreeverb.UseVisualStyleBackColor = true;
      this.chkEnableFreeverb.CheckedChanged += new System.EventHandler(this.chkEnableFreeverb_CheckedChanged);
      // 
      // trkDamp
      // 
      this.trkDamp.AutoSize = false;
      this.trkDamp.Location = new System.Drawing.Point(160, 90);
      this.trkDamp.Maximum = 100;
      this.trkDamp.Name = "trkDamp";
      this.trkDamp.Size = new System.Drawing.Size(272, 24);
      this.trkDamp.TabIndex = 6;
      this.trkDamp.TickFrequency = 10;
      this.trkDamp.Scroll += new System.EventHandler(this.trkDamp_Scroll);
      // 
      // lblRoomSize
      // 
      this.lblRoomSize.AutoSize = true;
      this.lblRoomSize.Location = new System.Drawing.Point(93, 70);
      this.lblRoomSize.Name = "lblRoomSize";
      this.lblRoomSize.Size = new System.Drawing.Size(58, 13);
      this.lblRoomSize.TabIndex = 5;
      this.lblRoomSize.Text = "Room Size";
      // 
      // trkRoomSize
      // 
      this.trkRoomSize.AutoSize = false;
      this.trkRoomSize.Location = new System.Drawing.Point(160, 67);
      this.trkRoomSize.Maximum = 100;
      this.trkRoomSize.Name = "trkRoomSize";
      this.trkRoomSize.Size = new System.Drawing.Size(272, 24);
      this.trkRoomSize.TabIndex = 4;
      this.trkRoomSize.TickFrequency = 10;
      this.trkRoomSize.Value = 30;
      this.trkRoomSize.Scroll += new System.EventHandler(this.trkRoomSize_Scroll);
      // 
      // lblWetMix
      // 
      this.lblWetMix.AutoSize = true;
      this.lblWetMix.Location = new System.Drawing.Point(93, 45);
      this.lblWetMix.Name = "lblWetMix";
      this.lblWetMix.Size = new System.Drawing.Size(46, 13);
      this.lblWetMix.TabIndex = 3;
      this.lblWetMix.Text = "Wet Mix";
      // 
      // trkWetMix
      // 
      this.trkWetMix.AutoSize = false;
      this.trkWetMix.Location = new System.Drawing.Point(160, 42);
      this.trkWetMix.Maximum = 100;
      this.trkWetMix.Name = "trkWetMix";
      this.trkWetMix.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.trkWetMix.Size = new System.Drawing.Size(272, 24);
      this.trkWetMix.TabIndex = 2;
      this.trkWetMix.TickFrequency = 10;
      this.trkWetMix.Scroll += new System.EventHandler(this.trkWetMix_Scroll);
      // 
      // lblDryMix
      // 
      this.lblDryMix.AutoSize = true;
      this.lblDryMix.Location = new System.Drawing.Point(93, 22);
      this.lblDryMix.Name = "lblDryMix";
      this.lblDryMix.Size = new System.Drawing.Size(42, 13);
      this.lblDryMix.TabIndex = 1;
      this.lblDryMix.Text = "Dry Mix";
      // 
      // trkDryMix
      // 
      this.trkDryMix.AutoSize = false;
      this.trkDryMix.Location = new System.Drawing.Point(160, 19);
      this.trkDryMix.Maximum = 100;
      this.trkDryMix.Name = "trkDryMix";
      this.trkDryMix.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.trkDryMix.Size = new System.Drawing.Size(272, 24);
      this.trkDryMix.TabIndex = 0;
      this.trkDryMix.TickFrequency = 10;
      this.trkDryMix.Scroll += new System.EventHandler(this.trkDryMix_Scroll);
      // 
      // frmFX
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(501, 174);
      this.Controls.Add(this.grpFreeverb);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmFX";
      this.Text = "frmFX - Chord Cadenza";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFX_FormClosed);
      this.Load += new System.EventHandler(this.frmFX_Load);
      this.grpFreeverb.ResumeLayout(false);
      this.grpFreeverb.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trkWidth)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkDamp)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkRoomSize)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkWetMix)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkDryMix)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.GroupBox grpFreeverb;
    private System.Windows.Forms.Button cmdResetFreeverb;
    private System.Windows.Forms.Label lblDamp;
    private System.Windows.Forms.CheckBox chkEnableFreeverb;
    private System.Windows.Forms.TrackBar trkDamp;
    private System.Windows.Forms.Label lblRoomSize;
    private System.Windows.Forms.TrackBar trkRoomSize;
    private System.Windows.Forms.Label lblWetMix;
    private System.Windows.Forms.TrackBar trkWetMix;
    private System.Windows.Forms.Label lblDryMix;
    private System.Windows.Forms.TrackBar trkDryMix;
    private System.Windows.Forms.Label lblWidth;
    private System.Windows.Forms.TrackBar trkWidth;
    private System.Windows.Forms.Button cmdHelp;
  }
}