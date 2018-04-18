namespace ChordCadenza.Forms {
  partial class frmChordMapAdv {
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
      this.grpQuantize = new System.Windows.Forms.GroupBox();
      this.label16 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.optAlignBeat = new System.Windows.Forms.RadioButton();
      this.nudFilterDD = new System.Windows.Forms.NumericUpDown();
      this.nudFillPercent = new System.Windows.Forms.NumericUpDown();
      this.optAlignHalfBar = new System.Windows.Forms.RadioButton();
      this.label9 = new System.Windows.Forms.Label();
      this.optAlignBar = new System.Windows.Forms.RadioButton();
      this.label13 = new System.Windows.Forms.Label();
      this.optAlignQuantize = new System.Windows.Forms.RadioButton();
      this.nudCloseGapDD = new System.Windows.Forms.NumericUpDown();
      this.nudMinLenNN = new System.Windows.Forms.NumericUpDown();
      this.label7 = new System.Windows.Forms.Label();
      this.label14 = new System.Windows.Forms.Label();
      this.nudQuantizeDD = new System.Windows.Forms.NumericUpDown();
      this.nudMinLenDD = new System.Windows.Forms.NumericUpDown();
      this.nudCloseGapNN = new System.Windows.Forms.NumericUpDown();
      this.label12 = new System.Windows.Forms.Label();
      this.nudFilterNN = new System.Windows.Forms.NumericUpDown();
      this.label11 = new System.Windows.Forms.Label();
      this.nudQuantizeNN = new System.Windows.Forms.NumericUpDown();
      this.label5 = new System.Windows.Forms.Label();
      this.grpChordColours = new System.Windows.Forms.GroupBox();
      this.optChordWeights = new System.Windows.Forms.RadioButton();
      this.optChordBlack = new System.Windows.Forms.RadioButton();
      this.optChordMatch = new System.Windows.Forms.RadioButton();
      this.grpQuantize.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudFilterDD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudFillPercent)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudCloseGapDD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMinLenNN)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudQuantizeDD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMinLenDD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudCloseGapNN)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudFilterNN)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudQuantizeNN)).BeginInit();
      this.grpChordColours.SuspendLayout();
      this.SuspendLayout();
      // 
      // grpQuantize
      // 
      this.grpQuantize.Controls.Add(this.label16);
      this.grpQuantize.Controls.Add(this.label8);
      this.grpQuantize.Controls.Add(this.optAlignBeat);
      this.grpQuantize.Controls.Add(this.nudFilterDD);
      this.grpQuantize.Controls.Add(this.nudFillPercent);
      this.grpQuantize.Controls.Add(this.optAlignHalfBar);
      this.grpQuantize.Controls.Add(this.label9);
      this.grpQuantize.Controls.Add(this.optAlignBar);
      this.grpQuantize.Controls.Add(this.label13);
      this.grpQuantize.Controls.Add(this.optAlignQuantize);
      this.grpQuantize.Controls.Add(this.nudCloseGapDD);
      this.grpQuantize.Controls.Add(this.nudMinLenNN);
      this.grpQuantize.Controls.Add(this.label7);
      this.grpQuantize.Controls.Add(this.label14);
      this.grpQuantize.Controls.Add(this.nudQuantizeDD);
      this.grpQuantize.Controls.Add(this.nudMinLenDD);
      this.grpQuantize.Controls.Add(this.nudCloseGapNN);
      this.grpQuantize.Controls.Add(this.label12);
      this.grpQuantize.Controls.Add(this.nudFilterNN);
      this.grpQuantize.Controls.Add(this.label11);
      this.grpQuantize.Controls.Add(this.nudQuantizeNN);
      this.grpQuantize.Controls.Add(this.label5);
      this.grpQuantize.Location = new System.Drawing.Point(26, 23);
      this.grpQuantize.Name = "grpQuantize";
      this.grpQuantize.Size = new System.Drawing.Size(232, 106);
      this.grpQuantize.TabIndex = 4;
      this.grpQuantize.TabStop = false;
      this.grpQuantize.Text = "Quantize";
      // 
      // label16
      // 
      this.label16.AutoSize = true;
      this.label16.Location = new System.Drawing.Point(215, 82);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(15, 13);
      this.label16.TabIndex = 56;
      this.label16.Text = "%";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(24, 86);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(49, 13);
      this.label8.TabIndex = 29;
      this.label8.Text = "Quantize";
      // 
      // optAlignBeat
      // 
      this.optAlignBeat.AutoSize = true;
      this.optAlignBeat.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.optAlignBeat.Location = new System.Drawing.Point(174, 58);
      this.optAlignBeat.Name = "optAlignBeat";
      this.optAlignBeat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.optAlignBeat.Size = new System.Drawing.Size(47, 17);
      this.optAlignBeat.TabIndex = 56;
      this.optAlignBeat.Text = "Beat";
      this.optAlignBeat.UseVisualStyleBackColor = true;
      this.optAlignBeat.CheckedChanged += new System.EventHandler(this.optAlign_CheckedChanged);
      // 
      // nudFilterDD
      // 
      this.nudFilterDD.Location = new System.Drawing.Point(127, 35);
      this.nudFilterDD.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
      this.nudFilterDD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudFilterDD.Name = "nudFilterDD";
      this.nudFilterDD.ReadOnly = true;
      this.nudFilterDD.Size = new System.Drawing.Size(40, 20);
      this.nudFilterDD.TabIndex = 24;
      this.nudFilterDD.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
      this.nudFilterDD.ValueChanged += new System.EventHandler(this.nudFilter_ValueChanged);
      // 
      // nudFillPercent
      // 
      this.nudFillPercent.Location = new System.Drawing.Point(173, 78);
      this.nudFillPercent.Name = "nudFillPercent";
      this.nudFillPercent.ReadOnly = true;
      this.nudFillPercent.Size = new System.Drawing.Size(40, 20);
      this.nudFillPercent.TabIndex = 55;
      this.nudFillPercent.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.nudFillPercent.ValueChanged += new System.EventHandler(this.optAlign_CheckedChanged);
      // 
      // optAlignHalfBar
      // 
      this.optAlignHalfBar.AutoSize = true;
      this.optAlignHalfBar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.optAlignHalfBar.Location = new System.Drawing.Point(174, 42);
      this.optAlignHalfBar.Name = "optAlignHalfBar";
      this.optAlignHalfBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.optAlignHalfBar.Size = new System.Drawing.Size(52, 17);
      this.optAlignHalfBar.TabIndex = 55;
      this.optAlignHalfBar.Text = "Bar/2";
      this.optAlignHalfBar.UseVisualStyleBackColor = true;
      this.optAlignHalfBar.CheckedChanged += new System.EventHandler(this.optAlign_CheckedChanged);
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(27, 38);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(47, 13);
      this.label9.TabIndex = 25;
      this.label9.Text = "Remove";
      // 
      // optAlignBar
      // 
      this.optAlignBar.AutoSize = true;
      this.optAlignBar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.optAlignBar.Location = new System.Drawing.Point(174, 26);
      this.optAlignBar.Name = "optAlignBar";
      this.optAlignBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.optAlignBar.Size = new System.Drawing.Size(41, 17);
      this.optAlignBar.TabIndex = 54;
      this.optAlignBar.Text = "Bar";
      this.optAlignBar.UseVisualStyleBackColor = true;
      this.optAlignBar.CheckedChanged += new System.EventHandler(this.optAlign_CheckedChanged);
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(112, 61);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(12, 13);
      this.label13.TabIndex = 43;
      this.label13.Text = "/";
      // 
      // optAlignQuantize
      // 
      this.optAlignQuantize.AutoSize = true;
      this.optAlignQuantize.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.optAlignQuantize.Checked = true;
      this.optAlignQuantize.Location = new System.Drawing.Point(174, 10);
      this.optAlignQuantize.Name = "optAlignQuantize";
      this.optAlignQuantize.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.optAlignQuantize.Size = new System.Drawing.Size(60, 17);
      this.optAlignQuantize.TabIndex = 53;
      this.optAlignQuantize.TabStop = true;
      this.optAlignQuantize.Text = "Interval";
      this.optAlignQuantize.UseVisualStyleBackColor = true;
      this.optAlignQuantize.CheckedChanged += new System.EventHandler(this.optAlign_CheckedChanged);
      // 
      // nudCloseGapDD
      // 
      this.nudCloseGapDD.Location = new System.Drawing.Point(127, 10);
      this.nudCloseGapDD.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
      this.nudCloseGapDD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudCloseGapDD.Name = "nudCloseGapDD";
      this.nudCloseGapDD.ReadOnly = true;
      this.nudCloseGapDD.Size = new System.Drawing.Size(40, 20);
      this.nudCloseGapDD.TabIndex = 26;
      this.nudCloseGapDD.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
      this.nudCloseGapDD.ValueChanged += new System.EventHandler(this.nudCloseGap_ValueChanged);
      // 
      // nudMinLenNN
      // 
      this.nudMinLenNN.Location = new System.Drawing.Point(79, 59);
      this.nudMinLenNN.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
      this.nudMinLenNN.Name = "nudMinLenNN";
      this.nudMinLenNN.ReadOnly = true;
      this.nudMinLenNN.Size = new System.Drawing.Size(30, 20);
      this.nudMinLenNN.TabIndex = 42;
      this.nudMinLenNN.ValueChanged += new System.EventHandler(this.nudMinLen_ValueChanged);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(17, 13);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(56, 13);
      this.label7.TabIndex = 27;
      this.label7.Text = "Close Gap";
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(14, 61);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(60, 13);
      this.label14.TabIndex = 41;
      this.label14.Text = "Min Length";
      // 
      // nudQuantizeDD
      // 
      this.nudQuantizeDD.Location = new System.Drawing.Point(127, 83);
      this.nudQuantizeDD.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
      this.nudQuantizeDD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudQuantizeDD.Name = "nudQuantizeDD";
      this.nudQuantizeDD.ReadOnly = true;
      this.nudQuantizeDD.Size = new System.Drawing.Size(40, 20);
      this.nudQuantizeDD.TabIndex = 28;
      this.nudQuantizeDD.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
      this.nudQuantizeDD.ValueChanged += new System.EventHandler(this.nudQuantize_ValueChanged);
      // 
      // nudMinLenDD
      // 
      this.nudMinLenDD.Location = new System.Drawing.Point(127, 59);
      this.nudMinLenDD.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
      this.nudMinLenDD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudMinLenDD.Name = "nudMinLenDD";
      this.nudMinLenDD.ReadOnly = true;
      this.nudMinLenDD.Size = new System.Drawing.Size(40, 20);
      this.nudMinLenDD.TabIndex = 40;
      this.nudMinLenDD.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
      this.nudMinLenDD.ValueChanged += new System.EventHandler(this.nudMinLen_ValueChanged);
      // 
      // nudCloseGapNN
      // 
      this.nudCloseGapNN.ForeColor = System.Drawing.SystemColors.WindowText;
      this.nudCloseGapNN.Location = new System.Drawing.Point(79, 10);
      this.nudCloseGapNN.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
      this.nudCloseGapNN.Name = "nudCloseGapNN";
      this.nudCloseGapNN.ReadOnly = true;
      this.nudCloseGapNN.Size = new System.Drawing.Size(30, 20);
      this.nudCloseGapNN.TabIndex = 30;
      this.nudCloseGapNN.ValueChanged += new System.EventHandler(this.nudCloseGap_ValueChanged);
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(112, 85);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(12, 13);
      this.label12.TabIndex = 35;
      this.label12.Text = "/";
      // 
      // nudFilterNN
      // 
      this.nudFilterNN.Location = new System.Drawing.Point(79, 35);
      this.nudFilterNN.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
      this.nudFilterNN.Name = "nudFilterNN";
      this.nudFilterNN.ReadOnly = true;
      this.nudFilterNN.Size = new System.Drawing.Size(30, 20);
      this.nudFilterNN.TabIndex = 31;
      this.nudFilterNN.ValueChanged += new System.EventHandler(this.nudFilter_ValueChanged);
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(112, 37);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(12, 13);
      this.label11.TabIndex = 34;
      this.label11.Text = "/";
      // 
      // nudQuantizeNN
      // 
      this.nudQuantizeNN.Location = new System.Drawing.Point(79, 83);
      this.nudQuantizeNN.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
      this.nudQuantizeNN.Name = "nudQuantizeNN";
      this.nudQuantizeNN.ReadOnly = true;
      this.nudQuantizeNN.Size = new System.Drawing.Size(30, 20);
      this.nudQuantizeNN.TabIndex = 32;
      this.nudQuantizeNN.ValueChanged += new System.EventHandler(this.nudQuantize_ValueChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(112, 12);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(12, 13);
      this.label5.TabIndex = 33;
      this.label5.Text = "/";
      // 
      // grpChordColours
      // 
      this.grpChordColours.Controls.Add(this.optChordWeights);
      this.grpChordColours.Controls.Add(this.optChordBlack);
      this.grpChordColours.Controls.Add(this.optChordMatch);
      this.grpChordColours.Location = new System.Drawing.Point(166, 135);
      this.grpChordColours.Name = "grpChordColours";
      this.grpChordColours.Size = new System.Drawing.Size(116, 76);
      this.grpChordColours.TabIndex = 97;
      this.grpChordColours.TabStop = false;
      this.grpChordColours.Text = "Chord Colours";
      // 
      // optChordWeights
      // 
      this.optChordWeights.AutoSize = true;
      this.optChordWeights.Checked = true;
      this.optChordWeights.Location = new System.Drawing.Point(11, 20);
      this.optChordWeights.Name = "optChordWeights";
      this.optChordWeights.Size = new System.Drawing.Size(78, 17);
      this.optChordWeights.TabIndex = 3;
      this.optChordWeights.TabStop = true;
      this.optChordWeights.Text = "Weightings";
      this.optChordWeights.UseVisualStyleBackColor = true;
      // 
      // optChordBlack
      // 
      this.optChordBlack.AutoSize = true;
      this.optChordBlack.Location = new System.Drawing.Point(11, 52);
      this.optChordBlack.Name = "optChordBlack";
      this.optChordBlack.Size = new System.Drawing.Size(52, 17);
      this.optChordBlack.TabIndex = 2;
      this.optChordBlack.Text = "Black";
      this.optChordBlack.UseVisualStyleBackColor = true;
      this.optChordBlack.CheckedChanged += new System.EventHandler(this.optChordMatch_CheckedChanged);
      // 
      // optChordMatch
      // 
      this.optChordMatch.AutoSize = true;
      this.optChordMatch.Location = new System.Drawing.Point(11, 36);
      this.optChordMatch.Name = "optChordMatch";
      this.optChordMatch.Size = new System.Drawing.Size(105, 17);
      this.optChordMatch.TabIndex = 1;
      this.optChordMatch.Text = "Match Chord File";
      this.optChordMatch.UseVisualStyleBackColor = true;
      this.optChordMatch.CheckedChanged += new System.EventHandler(this.optChordMatch_CheckedChanged);
      // 
      // frmChordMapAdv
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
      this.ClientSize = new System.Drawing.Size(426, 315);
      this.Controls.Add(this.grpChordColours);
      this.Controls.Add(this.grpQuantize);
      this.ForeColor = System.Drawing.Color.Red;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmChordMapAdv";
      this.Text = "frmChordMapAdv - Chord Cadenza";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmNoteMapAdv_FormClosed);
      this.Load += new System.EventHandler(this.frmNoteMapAdv_Load);
      this.grpQuantize.ResumeLayout(false);
      this.grpQuantize.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudFilterDD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudFillPercent)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudCloseGapDD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMinLenNN)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudQuantizeDD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMinLenDD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudCloseGapNN)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudFilterNN)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudQuantizeNN)).EndInit();
      this.grpChordColours.ResumeLayout(false);
      this.grpChordColours.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox grpQuantize;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.RadioButton optAlignBeat;
    internal System.Windows.Forms.NumericUpDown nudFilterDD;
    internal System.Windows.Forms.NumericUpDown nudFillPercent;
    private System.Windows.Forms.RadioButton optAlignHalfBar;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.RadioButton optAlignBar;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.RadioButton optAlignQuantize;
    internal System.Windows.Forms.NumericUpDown nudCloseGapDD;
    internal System.Windows.Forms.NumericUpDown nudMinLenNN;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label14;
    internal System.Windows.Forms.NumericUpDown nudQuantizeDD;
    internal System.Windows.Forms.NumericUpDown nudMinLenDD;
    internal System.Windows.Forms.NumericUpDown nudCloseGapNN;
    private System.Windows.Forms.Label label12;
    internal System.Windows.Forms.NumericUpDown nudFilterNN;
    private System.Windows.Forms.Label label11;
    internal System.Windows.Forms.NumericUpDown nudQuantizeNN;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.GroupBox grpChordColours;
    internal System.Windows.Forms.RadioButton optChordBlack;
    internal System.Windows.Forms.RadioButton optChordMatch;
    internal System.Windows.Forms.RadioButton optChordWeights;
  }
}