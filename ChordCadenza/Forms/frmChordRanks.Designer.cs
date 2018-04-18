namespace ChordCadenza.Forms {
  partial class frmChordRanks {
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
      this.components = new System.ComponentModel.Container();
      this.panChords = new System.Windows.Forms.Panel();
      this.panNoMatch = new System.Windows.Forms.Panel();
      this.label9 = new System.Windows.Forms.Label();
      this.optNoMatch4 = new System.Windows.Forms.RadioButton();
      this.optNoMatch3 = new System.Windows.Forms.RadioButton();
      this.optNoMatch2 = new System.Windows.Forms.RadioButton();
      this.optNoMatch1 = new System.Windows.Forms.RadioButton();
      this.panMatch = new System.Windows.Forms.Panel();
      this.label8 = new System.Windows.Forms.Label();
      this.optMatch4 = new System.Windows.Forms.RadioButton();
      this.optMatch3 = new System.Windows.Forms.RadioButton();
      this.optMatch2 = new System.Windows.Forms.RadioButton();
      this.optMatch1 = new System.Windows.Forms.RadioButton();
      this.label7 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.cmdLoadDflts = new System.Windows.Forms.Button();
      this.cmdClose = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.panChords.SuspendLayout();
      this.panNoMatch.SuspendLayout();
      this.panMatch.SuspendLayout();
      this.SuspendLayout();
      // 
      // panChords
      // 
      this.panChords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panChords.AutoScroll = true;
      this.panChords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panChords.Controls.Add(this.panNoMatch);
      this.panChords.Controls.Add(this.panMatch);
      this.panChords.Controls.Add(this.label7);
      this.panChords.Controls.Add(this.label6);
      this.panChords.Controls.Add(this.label5);
      this.panChords.Controls.Add(this.label4);
      this.panChords.Controls.Add(this.label1);
      this.panChords.Controls.Add(this.label3);
      this.panChords.Controls.Add(this.label2);
      this.panChords.Location = new System.Drawing.Point(12, 12);
      this.panChords.Name = "panChords";
      this.panChords.Size = new System.Drawing.Size(285, 696);
      this.panChords.TabIndex = 1;
      // 
      // panNoMatch
      // 
      this.panNoMatch.Controls.Add(this.label9);
      this.panNoMatch.Controls.Add(this.optNoMatch4);
      this.panNoMatch.Controls.Add(this.optNoMatch3);
      this.panNoMatch.Controls.Add(this.optNoMatch2);
      this.panNoMatch.Controls.Add(this.optNoMatch1);
      this.panNoMatch.Location = new System.Drawing.Point(0, 53);
      this.panNoMatch.Name = "panNoMatch";
      this.panNoMatch.Size = new System.Drawing.Size(281, 19);
      this.panNoMatch.TabIndex = 13;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.Location = new System.Drawing.Point(72, 3);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(163, 13);
      this.label9.TabIndex = 21;
      this.label9.Text = "Maximum Non-Matching Rank ";
      this.toolTip1.SetToolTip(this.label9, "Set the maximum rank of a chord that is not a match of the notes on the selected " +
        "tracks.\r\nThis must be less than or equal to the maximum matching rank.\r\n");
      // 
      // optNoMatch4
      // 
      this.optNoMatch4.AutoSize = true;
      this.optNoMatch4.Location = new System.Drawing.Point(54, 3);
      this.optNoMatch4.Name = "optNoMatch4";
      this.optNoMatch4.Size = new System.Drawing.Size(14, 13);
      this.optNoMatch4.TabIndex = 20;
      this.optNoMatch4.TabStop = true;
      this.toolTip1.SetToolTip(this.optNoMatch4, "Set the maximum rank of a chord that is not a match of the notes on the selected " +
        "tracks.\r\nThis must be less than or equal to the maximum matching rank.\r\n");
      this.optNoMatch4.UseVisualStyleBackColor = true;
      this.optNoMatch4.Click += new System.EventHandler(this.optNoMatch_Click);
      // 
      // optNoMatch3
      // 
      this.optNoMatch3.AutoSize = true;
      this.optNoMatch3.Location = new System.Drawing.Point(37, 3);
      this.optNoMatch3.Name = "optNoMatch3";
      this.optNoMatch3.Size = new System.Drawing.Size(14, 13);
      this.optNoMatch3.TabIndex = 19;
      this.optNoMatch3.TabStop = true;
      this.toolTip1.SetToolTip(this.optNoMatch3, "Set the maximum rank of a chord that is not a match of the notes on the selected " +
        "tracks.\r\nThis must be less than or equal to the maximum matching rank.\r\n");
      this.optNoMatch3.UseVisualStyleBackColor = true;
      this.optNoMatch3.Click += new System.EventHandler(this.optNoMatch_Click);
      // 
      // optNoMatch2
      // 
      this.optNoMatch2.AutoSize = true;
      this.optNoMatch2.Location = new System.Drawing.Point(20, 3);
      this.optNoMatch2.Name = "optNoMatch2";
      this.optNoMatch2.Size = new System.Drawing.Size(14, 13);
      this.optNoMatch2.TabIndex = 18;
      this.optNoMatch2.TabStop = true;
      this.toolTip1.SetToolTip(this.optNoMatch2, "Set the maximum rank of a chord that is not a match of the notes on the selected " +
        "tracks.\r\nThis must be less than or equal to the maximum matching rank.\r\n");
      this.optNoMatch2.UseVisualStyleBackColor = true;
      this.optNoMatch2.Click += new System.EventHandler(this.optNoMatch_Click);
      // 
      // optNoMatch1
      // 
      this.optNoMatch1.AutoSize = true;
      this.optNoMatch1.Location = new System.Drawing.Point(3, 3);
      this.optNoMatch1.Name = "optNoMatch1";
      this.optNoMatch1.Size = new System.Drawing.Size(14, 13);
      this.optNoMatch1.TabIndex = 17;
      this.optNoMatch1.TabStop = true;
      this.toolTip1.SetToolTip(this.optNoMatch1, "Set the maximum rank of a chord that is not a match of the notes on the selected " +
        "tracks.\r\nThis must be less than or equal to the maximum matching rank.\r\n");
      this.optNoMatch1.UseVisualStyleBackColor = true;
      this.optNoMatch1.Click += new System.EventHandler(this.optNoMatch_Click);
      // 
      // panMatch
      // 
      this.panMatch.Controls.Add(this.label8);
      this.panMatch.Controls.Add(this.optMatch4);
      this.panMatch.Controls.Add(this.optMatch3);
      this.panMatch.Controls.Add(this.optMatch2);
      this.panMatch.Controls.Add(this.optMatch1);
      this.panMatch.Location = new System.Drawing.Point(0, 35);
      this.panMatch.Name = "panMatch";
      this.panMatch.Size = new System.Drawing.Size(281, 19);
      this.panMatch.TabIndex = 12;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(72, 3);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(139, 13);
      this.label8.TabIndex = 17;
      this.label8.Text = "Maximum Matching Rank ";
      this.toolTip1.SetToolTip(this.label8, "Set the maximum rank of a chord that is an exact match of the notes on the select" +
        "ed tracks.\r\nThis must be greater than or equal to the maximum non-matching rank." +
        "");
      // 
      // optMatch4
      // 
      this.optMatch4.AutoSize = true;
      this.optMatch4.Location = new System.Drawing.Point(54, 3);
      this.optMatch4.Name = "optMatch4";
      this.optMatch4.Size = new System.Drawing.Size(14, 13);
      this.optMatch4.TabIndex = 16;
      this.optMatch4.TabStop = true;
      this.toolTip1.SetToolTip(this.optMatch4, "Set the maximum rank of a chord that is an exact match of the notes on the select" +
        "ed tracks.\r\nThis must be greater than or equal to the maximum non-matching rank." +
        "");
      this.optMatch4.UseVisualStyleBackColor = true;
      this.optMatch4.Click += new System.EventHandler(this.optMatch_Click);
      // 
      // optMatch3
      // 
      this.optMatch3.AutoSize = true;
      this.optMatch3.Location = new System.Drawing.Point(37, 3);
      this.optMatch3.Name = "optMatch3";
      this.optMatch3.Size = new System.Drawing.Size(14, 13);
      this.optMatch3.TabIndex = 15;
      this.optMatch3.TabStop = true;
      this.toolTip1.SetToolTip(this.optMatch3, "Set the maximum rank of a chord that is an exact match of the notes on the select" +
        "ed tracks.\r\nThis must be greater than or equal to the maximum non-matching rank." +
        "");
      this.optMatch3.UseVisualStyleBackColor = true;
      this.optMatch3.Click += new System.EventHandler(this.optMatch_Click);
      // 
      // optMatch2
      // 
      this.optMatch2.AutoSize = true;
      this.optMatch2.Location = new System.Drawing.Point(20, 3);
      this.optMatch2.Name = "optMatch2";
      this.optMatch2.Size = new System.Drawing.Size(14, 13);
      this.optMatch2.TabIndex = 14;
      this.optMatch2.TabStop = true;
      this.toolTip1.SetToolTip(this.optMatch2, "Set the maximum rank of a chord that is an exact match of the notes on the select" +
        "ed tracks.\r\nThis must be greater than or equal to the maximum non-matching rank." +
        "");
      this.optMatch2.UseVisualStyleBackColor = true;
      this.optMatch2.Click += new System.EventHandler(this.optMatch_Click);
      // 
      // optMatch1
      // 
      this.optMatch1.AutoSize = true;
      this.optMatch1.Location = new System.Drawing.Point(3, 3);
      this.optMatch1.Name = "optMatch1";
      this.optMatch1.Size = new System.Drawing.Size(14, 13);
      this.optMatch1.TabIndex = 0;
      this.optMatch1.TabStop = true;
      this.toolTip1.SetToolTip(this.optMatch1, "Set the maximum rank of a chord that is an exact match of the notes on the select" +
        "ed tracks.\r\nThis must be greater than or equal to the maximum non-matching rank." +
        "");
      this.optMatch1.UseVisualStyleBackColor = true;
      this.optMatch1.Click += new System.EventHandler(this.optMatch_Click);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(53, 19);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(14, 15);
      this.label7.TabIndex = 11;
      this.label7.Text = "4";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(36, 19);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(14, 15);
      this.label6.TabIndex = 10;
      this.label6.Text = "3";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(19, 19);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(14, 15);
      this.label5.TabIndex = 9;
      this.label5.Text = "2";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(2, 19);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(14, 15);
      this.label4.TabIndex = 8;
      this.label4.Text = "1";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(135, 4);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(77, 15);
      this.label1.TabIndex = 7;
      this.label1.Text = "Notes on C";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(72, 4);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(42, 15);
      this.label3.TabIndex = 6;
      this.label3.Text = "Chord";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(2, 4);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(35, 15);
      this.label2.TabIndex = 5;
      this.label2.Text = "Rank";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // cmdLoadDflts
      // 
      this.cmdLoadDflts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdLoadDflts.Location = new System.Drawing.Point(12, 720);
      this.cmdLoadDflts.Name = "cmdLoadDflts";
      this.cmdLoadDflts.Size = new System.Drawing.Size(50, 50);
      this.cmdLoadDflts.TabIndex = 145;
      this.cmdLoadDflts.Text = "Load\r\nDefault\r\nValues";
      this.toolTip1.SetToolTip(this.cmdLoadDflts, "Restore all values back to the default (factory setting).");
      this.cmdLoadDflts.UseVisualStyleBackColor = true;
      this.cmdLoadDflts.Click += new System.EventHandler(this.cmdLoadDflts_Click);
      // 
      // cmdClose
      // 
      this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdClose.Location = new System.Drawing.Point(247, 720);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(50, 50);
      this.cmdClose.TabIndex = 146;
      this.cmdClose.Text = "Close";
      this.toolTip1.SetToolTip(this.cmdClose, "Close this window");
      this.cmdClose.UseVisualStyleBackColor = true;
      this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
      // 
      // frmChordRanks
      // 
      this.AcceptButton = this.cmdClose;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdClose;
      this.ClientSize = new System.Drawing.Size(309, 782);
      this.Controls.Add(this.cmdClose);
      this.Controls.Add(this.cmdLoadDflts);
      this.Controls.Add(this.panChords);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmChordRanks";
      this.Text = "Chord Ranks - Chord Cadenza";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmChordRanks_FormClosed);
      this.Load += new System.EventHandler(this.frmChordRanks_Load);
      this.panChords.ResumeLayout(false);
      this.panChords.PerformLayout();
      this.panNoMatch.ResumeLayout(false);
      this.panNoMatch.PerformLayout();
      this.panMatch.ResumeLayout(false);
      this.panMatch.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Panel panChords;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button cmdLoadDflts;
    private System.Windows.Forms.Button cmdClose;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Panel panMatch;
    private System.Windows.Forms.Panel panNoMatch;
    private System.Windows.Forms.RadioButton optMatch1;
    private System.Windows.Forms.RadioButton optMatch4;
    private System.Windows.Forms.RadioButton optMatch3;
    private System.Windows.Forms.RadioButton optMatch2;
    private System.Windows.Forms.RadioButton optNoMatch4;
    private System.Windows.Forms.RadioButton optNoMatch3;
    private System.Windows.Forms.RadioButton optNoMatch2;
    private System.Windows.Forms.RadioButton optNoMatch1;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.ToolTip toolTip1;
  }
}