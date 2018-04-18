namespace ChordCadenza.Forms {
  partial class frmTonnetz {
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
      this.picPC = new System.Windows.Forms.PictureBox();
      this.cmdThicker = new System.Windows.Forms.Button();
      this.cmdThinner = new System.Windows.Forms.Button();
      this.grpCirclePen = new System.Windows.Forms.GroupBox();
      this.grpSpace = new System.Windows.Forms.GroupBox();
      this.cmdSpaceLess = new System.Windows.Forms.Button();
      this.cmdSpaceMore = new System.Windows.Forms.Button();
      this.grpDiameter = new System.Windows.Forms.GroupBox();
      this.cmdDiameterLess = new System.Windows.Forms.Button();
      this.cmdDiameterMore = new System.Windows.Forms.Button();
      this.nudLookAhead = new System.Windows.Forms.NumericUpDown();
      this.lblLookAhead = new System.Windows.Forms.Label();
      this.lblLookAheadBeats = new System.Windows.Forms.Label();
      this.lblThisChordLit = new System.Windows.Forms.Label();
      this.lblThisChord = new System.Windows.Forms.Label();
      this.lblNewChord = new System.Windows.Forms.Label();
      this.lblNewChordLit = new System.Windows.Forms.Label();
      this.lblNextChord = new System.Windows.Forms.Label();
      this.lblNextChordLit = new System.Windows.Forms.Label();
      this.lblKey = new System.Windows.Forms.Label();
      this.lblKeyLit = new System.Windows.Forms.Label();
      this.cmdColours = new System.Windows.Forms.Button();
      this.cmdHelp = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.picPC)).BeginInit();
      this.grpCirclePen.SuspendLayout();
      this.grpSpace.SuspendLayout();
      this.grpDiameter.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudLookAhead)).BeginInit();
      this.SuspendLayout();
      // 
      // picPC
      // 
      this.picPC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.picPC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picPC.Location = new System.Drawing.Point(12, 115);
      this.picPC.Name = "picPC";
      this.picPC.Size = new System.Drawing.Size(836, 224);
      this.picPC.TabIndex = 0;
      this.picPC.TabStop = false;
      this.picPC.Paint += new System.Windows.Forms.PaintEventHandler(this.picPC_Paint);
      this.picPC.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPC_MouseDown);
      this.picPC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picPC_MouseUp);
      this.picPC.Resize += new System.EventHandler(this.picPC_Resize);
      // 
      // cmdThicker
      // 
      this.cmdThicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdThicker.Location = new System.Drawing.Point(18, 19);
      this.cmdThicker.Name = "cmdThicker";
      this.cmdThicker.Size = new System.Drawing.Size(31, 29);
      this.cmdThicker.TabIndex = 1;
      this.cmdThicker.Text = "+";
      this.cmdThicker.UseVisualStyleBackColor = true;
      this.cmdThicker.Click += new System.EventHandler(this.cmdThicker_Click);
      // 
      // cmdThinner
      // 
      this.cmdThinner.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdThinner.Location = new System.Drawing.Point(55, 19);
      this.cmdThinner.Name = "cmdThinner";
      this.cmdThinner.Size = new System.Drawing.Size(31, 29);
      this.cmdThinner.TabIndex = 2;
      this.cmdThinner.Text = "-";
      this.cmdThinner.UseVisualStyleBackColor = true;
      this.cmdThinner.Click += new System.EventHandler(this.cmdThinner_Click);
      // 
      // grpCirclePen
      // 
      this.grpCirclePen.Controls.Add(this.cmdThinner);
      this.grpCirclePen.Controls.Add(this.cmdThicker);
      this.grpCirclePen.Location = new System.Drawing.Point(12, 12);
      this.grpCirclePen.Name = "grpCirclePen";
      this.grpCirclePen.Size = new System.Drawing.Size(101, 56);
      this.grpCirclePen.TabIndex = 3;
      this.grpCirclePen.TabStop = false;
      this.grpCirclePen.Text = "Circle Outline";
      // 
      // grpSpace
      // 
      this.grpSpace.Controls.Add(this.cmdSpaceLess);
      this.grpSpace.Controls.Add(this.cmdSpaceMore);
      this.grpSpace.Location = new System.Drawing.Point(133, 12);
      this.grpSpace.Name = "grpSpace";
      this.grpSpace.Size = new System.Drawing.Size(101, 56);
      this.grpSpace.TabIndex = 4;
      this.grpSpace.TabStop = false;
      this.grpSpace.Text = "Spacing";
      // 
      // cmdSpaceLess
      // 
      this.cmdSpaceLess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdSpaceLess.Location = new System.Drawing.Point(55, 19);
      this.cmdSpaceLess.Name = "cmdSpaceLess";
      this.cmdSpaceLess.Size = new System.Drawing.Size(31, 29);
      this.cmdSpaceLess.TabIndex = 2;
      this.cmdSpaceLess.Text = "-";
      this.cmdSpaceLess.UseVisualStyleBackColor = true;
      this.cmdSpaceLess.Click += new System.EventHandler(this.cmdSpaceLess_Click);
      // 
      // cmdSpaceMore
      // 
      this.cmdSpaceMore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdSpaceMore.Location = new System.Drawing.Point(18, 19);
      this.cmdSpaceMore.Name = "cmdSpaceMore";
      this.cmdSpaceMore.Size = new System.Drawing.Size(31, 29);
      this.cmdSpaceMore.TabIndex = 1;
      this.cmdSpaceMore.Text = "+";
      this.cmdSpaceMore.UseVisualStyleBackColor = true;
      this.cmdSpaceMore.Click += new System.EventHandler(this.cmdSpaceMore_Click);
      // 
      // grpDiameter
      // 
      this.grpDiameter.Controls.Add(this.cmdDiameterLess);
      this.grpDiameter.Controls.Add(this.cmdDiameterMore);
      this.grpDiameter.Location = new System.Drawing.Point(252, 12);
      this.grpDiameter.Name = "grpDiameter";
      this.grpDiameter.Size = new System.Drawing.Size(101, 56);
      this.grpDiameter.TabIndex = 5;
      this.grpDiameter.TabStop = false;
      this.grpDiameter.Text = "Diameter";
      // 
      // cmdDiameterLess
      // 
      this.cmdDiameterLess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdDiameterLess.Location = new System.Drawing.Point(55, 19);
      this.cmdDiameterLess.Name = "cmdDiameterLess";
      this.cmdDiameterLess.Size = new System.Drawing.Size(31, 29);
      this.cmdDiameterLess.TabIndex = 2;
      this.cmdDiameterLess.Text = "-";
      this.cmdDiameterLess.UseVisualStyleBackColor = true;
      this.cmdDiameterLess.Click += new System.EventHandler(this.cmdDiameterLess_Click);
      // 
      // cmdDiameterMore
      // 
      this.cmdDiameterMore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdDiameterMore.Location = new System.Drawing.Point(18, 19);
      this.cmdDiameterMore.Name = "cmdDiameterMore";
      this.cmdDiameterMore.Size = new System.Drawing.Size(31, 29);
      this.cmdDiameterMore.TabIndex = 1;
      this.cmdDiameterMore.Text = "+";
      this.cmdDiameterMore.UseVisualStyleBackColor = true;
      this.cmdDiameterMore.Click += new System.EventHandler(this.cmdDiameterMore_Click);
      // 
      // nudLookAhead
      // 
      this.nudLookAhead.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nudLookAhead.Location = new System.Drawing.Point(419, 31);
      this.nudLookAhead.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
      this.nudLookAhead.Name = "nudLookAhead";
      this.nudLookAhead.Size = new System.Drawing.Size(37, 22);
      this.nudLookAhead.TabIndex = 8;
      this.nudLookAhead.ValueChanged += new System.EventHandler(this.nudLookAhead_ValueChanged);
      // 
      // lblLookAhead
      // 
      this.lblLookAhead.AutoSize = true;
      this.lblLookAhead.Location = new System.Drawing.Point(378, 29);
      this.lblLookAhead.Name = "lblLookAhead";
      this.lblLookAhead.Size = new System.Drawing.Size(38, 26);
      this.lblLookAhead.TabIndex = 9;
      this.lblLookAhead.Text = "Look\r\nAhead";
      this.lblLookAhead.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblLookAheadBeats
      // 
      this.lblLookAheadBeats.AutoSize = true;
      this.lblLookAheadBeats.Location = new System.Drawing.Point(460, 35);
      this.lblLookAheadBeats.Name = "lblLookAheadBeats";
      this.lblLookAheadBeats.Size = new System.Drawing.Size(34, 13);
      this.lblLookAheadBeats.TabIndex = 10;
      this.lblLookAheadBeats.Text = "Beats";
      this.lblLookAheadBeats.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblThisChordLit
      // 
      this.lblThisChordLit.AutoSize = true;
      this.lblThisChordLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblThisChordLit.Location = new System.Drawing.Point(12, 84);
      this.lblThisChordLit.Name = "lblThisChordLit";
      this.lblThisChordLit.Size = new System.Drawing.Size(73, 16);
      this.lblThisChordLit.TabIndex = 14;
      this.lblThisChordLit.Text = "This Chord";
      this.lblThisChordLit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblThisChord
      // 
      this.lblThisChord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblThisChord.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblThisChord.Location = new System.Drawing.Point(89, 80);
      this.lblThisChord.Name = "lblThisChord";
      this.lblThisChord.Size = new System.Drawing.Size(83, 25);
      this.lblThisChord.TabIndex = 15;
      this.lblThisChord.Text = "null";
      this.lblThisChord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblNewChord
      // 
      this.lblNewChord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblNewChord.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblNewChord.Location = new System.Drawing.Point(283, 80);
      this.lblNewChord.Name = "lblNewChord";
      this.lblNewChord.Size = new System.Drawing.Size(83, 25);
      this.lblNewChord.TabIndex = 17;
      this.lblNewChord.Text = "null";
      this.lblNewChord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblNewChordLit
      // 
      this.lblNewChordLit.AutoSize = true;
      this.lblNewChordLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblNewChordLit.Location = new System.Drawing.Point(193, 84);
      this.lblNewChordLit.Name = "lblNewChordLit";
      this.lblNewChordLit.Size = new System.Drawing.Size(88, 16);
      this.lblNewChordLit.TabIndex = 16;
      this.lblNewChordLit.Text = "Mouse Chord";
      this.lblNewChordLit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblNextChord
      // 
      this.lblNextChord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblNextChord.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblNextChord.Location = new System.Drawing.Point(472, 80);
      this.lblNextChord.Name = "lblNextChord";
      this.lblNextChord.Size = new System.Drawing.Size(83, 25);
      this.lblNextChord.TabIndex = 19;
      this.lblNextChord.Text = "null";
      this.lblNextChord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblNextChordLit
      // 
      this.lblNextChordLit.AutoSize = true;
      this.lblNextChordLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblNextChordLit.Location = new System.Drawing.Point(395, 84);
      this.lblNextChordLit.Name = "lblNextChordLit";
      this.lblNextChordLit.Size = new System.Drawing.Size(74, 16);
      this.lblNextChordLit.TabIndex = 18;
      this.lblNextChordLit.Text = "Next Chord";
      this.lblNextChordLit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblKey
      // 
      this.lblKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblKey.Location = new System.Drawing.Point(626, 80);
      this.lblKey.Name = "lblKey";
      this.lblKey.Size = new System.Drawing.Size(93, 25);
      this.lblKey.TabIndex = 21;
      this.lblKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblKeyLit
      // 
      this.lblKeyLit.AutoSize = true;
      this.lblKeyLit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblKeyLit.Location = new System.Drawing.Point(592, 84);
      this.lblKeyLit.Name = "lblKeyLit";
      this.lblKeyLit.Size = new System.Drawing.Size(31, 16);
      this.lblKeyLit.TabIndex = 20;
      this.lblKeyLit.Text = "Key";
      this.lblKeyLit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // cmdColours
      // 
      this.cmdColours.Location = new System.Drawing.Point(519, 27);
      this.cmdColours.Name = "cmdColours";
      this.cmdColours.Size = new System.Drawing.Size(56, 31);
      this.cmdColours.TabIndex = 23;
      this.cmdColours.Text = "Colours";
      this.cmdColours.UseVisualStyleBackColor = true;
      this.cmdColours.Click += new System.EventHandler(this.cmdColours_Click);
      // 
      // cmdHelp
      // 
      this.cmdHelp.Location = new System.Drawing.Point(663, 27);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(56, 31);
      this.cmdHelp.TabIndex = 24;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // frmTonnetz
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(860, 351);
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.cmdColours);
      this.Controls.Add(this.lblKey);
      this.Controls.Add(this.lblKeyLit);
      this.Controls.Add(this.lblNextChord);
      this.Controls.Add(this.lblNextChordLit);
      this.Controls.Add(this.lblNewChord);
      this.Controls.Add(this.lblNewChordLit);
      this.Controls.Add(this.lblThisChord);
      this.Controls.Add(this.lblThisChordLit);
      this.Controls.Add(this.lblLookAheadBeats);
      this.Controls.Add(this.lblLookAhead);
      this.Controls.Add(this.nudLookAhead);
      this.Controls.Add(this.grpDiameter);
      this.Controls.Add(this.grpSpace);
      this.Controls.Add(this.grpCirclePen);
      this.Controls.Add(this.picPC);
      this.Name = "frmTonnetz";
      this.Text = "frmTonnetz";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTonnetz_FormClosing);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTonnetz_FormClosed);
      this.Load += new System.EventHandler(this.frmTonnetz_Load);
      ((System.ComponentModel.ISupportInitialize)(this.picPC)).EndInit();
      this.grpCirclePen.ResumeLayout(false);
      this.grpSpace.ResumeLayout(false);
      this.grpDiameter.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.nudLookAhead)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button cmdThicker;
    private System.Windows.Forms.Button cmdThinner;
    private System.Windows.Forms.GroupBox grpCirclePen;
    private System.Windows.Forms.GroupBox grpSpace;
    private System.Windows.Forms.Button cmdSpaceLess;
    private System.Windows.Forms.Button cmdSpaceMore;
    private System.Windows.Forms.GroupBox grpDiameter;
    private System.Windows.Forms.Button cmdDiameterLess;
    private System.Windows.Forms.Button cmdDiameterMore;
    private System.Windows.Forms.NumericUpDown nudLookAhead;
    private System.Windows.Forms.Label lblLookAhead;
    private System.Windows.Forms.Label lblLookAheadBeats;
    private System.Windows.Forms.Label lblThisChordLit;
    private System.Windows.Forms.Label lblThisChord;
    private System.Windows.Forms.Label lblNewChord;
    private System.Windows.Forms.Label lblNewChordLit;
    private System.Windows.Forms.Label lblNextChord;
    private System.Windows.Forms.Label lblNextChordLit;
    private System.Windows.Forms.Label lblKey;
    private System.Windows.Forms.Label lblKeyLit;
    private System.Windows.Forms.Button cmdColours;
    internal System.Windows.Forms.PictureBox picPC;
    private System.Windows.Forms.Button cmdHelp;
  }
}