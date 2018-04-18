namespace MPlay.Forms.HowTo {
  partial class frmHowToPlayChords {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHowToPlayChords));
      this.label6 = new System.Windows.Forms.Label();
      this.cmdLoadSong = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.cmdShowPlayMap = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.cmdKeyboardMode = new System.Windows.Forms.Button();
      this.cmdChordMode = new System.Windows.Forms.Button();
      this.label7 = new System.Windows.Forms.Label();
      this.cmdSwitchMode = new System.Windows.Forms.Button();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.richTextBox2 = new System.Windows.Forms.RichTextBox();
      this.richTextBox3 = new System.Windows.Forms.RichTextBox();
      this.richTextBox4 = new System.Windows.Forms.RichTextBox();
      this.richTextBox5 = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(12, 207);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(226, 13);
      this.label6.TabIndex = 52;
      this.label6.Text = "Choose which Method you want to use";
      // 
      // cmdLoadSong
      // 
      this.cmdLoadSong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdLoadSong.Location = new System.Drawing.Point(496, 17);
      this.cmdLoadSong.Name = "cmdLoadSong";
      this.cmdLoadSong.Size = new System.Drawing.Size(220, 30);
      this.cmdLoadSong.TabIndex = 50;
      this.cmdLoadSong.Text = "Load Song";
      this.cmdLoadSong.UseVisualStyleBackColor = true;
      this.cmdLoadSong.Click += new System.EventHandler(this.cmdLoadSong_Click);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(27, 26);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(380, 13);
      this.label5.TabIndex = 49;
      this.label5.Text = "If you haven\'t already done so, load a Song containing a midifile and a chordfile" +
    "";
      // 
      // cmdShowPlayMap
      // 
      this.cmdShowPlayMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdShowPlayMap.Location = new System.Drawing.Point(498, 94);
      this.cmdShowPlayMap.Name = "cmdShowPlayMap";
      this.cmdShowPlayMap.Size = new System.Drawing.Size(220, 30);
      this.cmdShowPlayMap.TabIndex = 55;
      this.cmdShowPlayMap.Text = "Show PlayMap";
      this.cmdShowPlayMap.UseVisualStyleBackColor = true;
      this.cmdShowPlayMap.Click += new System.EventHandler(this.cmdShowPlayMap_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(14, 65);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(115, 13);
      this.label1.TabIndex = 54;
      this.label1.Text = "Open The PlayMap";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 409);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(176, 13);
      this.label2.TabIndex = 57;
      this.label2.Text = "Play Chords using ChordMode";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(14, 553);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(224, 13);
      this.label3.TabIndex = 59;
      this.label3.Text = "Play Chords using KBChord SwitchKey";
      // 
      // cmdKeyboardMode
      // 
      this.cmdKeyboardMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdKeyboardMode.Location = new System.Drawing.Point(496, 583);
      this.cmdKeyboardMode.Name = "cmdKeyboardMode";
      this.cmdKeyboardMode.Size = new System.Drawing.Size(220, 30);
      this.cmdKeyboardMode.TabIndex = 61;
      this.cmdKeyboardMode.Text = "Set to Keyboard Mode";
      this.cmdKeyboardMode.UseVisualStyleBackColor = true;
      this.cmdKeyboardMode.Click += new System.EventHandler(this.cmdKeyboardMode_Click);
      // 
      // cmdChordMode
      // 
      this.cmdChordMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdChordMode.Location = new System.Drawing.Point(494, 437);
      this.cmdChordMode.Name = "cmdChordMode";
      this.cmdChordMode.Size = new System.Drawing.Size(220, 30);
      this.cmdChordMode.TabIndex = 62;
      this.cmdChordMode.Text = "Set to Chord Mode";
      this.cmdChordMode.UseVisualStyleBackColor = true;
      this.cmdChordMode.Click += new System.EventHandler(this.cmdChordMode_Click);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(14, 328);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(196, 13);
      this.label7.TabIndex = 64;
      this.label7.Text = "Play Chords using KeyboardMode";
      // 
      // cmdSwitchMode
      // 
      this.cmdSwitchMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdSwitchMode.Location = new System.Drawing.Point(494, 356);
      this.cmdSwitchMode.Name = "cmdSwitchMode";
      this.cmdSwitchMode.Size = new System.Drawing.Size(220, 30);
      this.cmdSwitchMode.TabIndex = 66;
      this.cmdSwitchMode.Text = "Set to Keyboard Mode";
      this.cmdSwitchMode.UseVisualStyleBackColor = true;
      this.cmdSwitchMode.Click += new System.EventHandler(this.cmdSwitchMode_Click);
      // 
      // richTextBox1
      // 
      this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Location = new System.Drawing.Point(32, 94);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox1.Size = new System.Drawing.Size(458, 95);
      this.richTextBox1.TabIndex = 100;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // richTextBox2
      // 
      this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox2.Location = new System.Drawing.Point(30, 230);
      this.richTextBox2.Name = "richTextBox2";
      this.richTextBox2.ReadOnly = true;
      this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox2.Size = new System.Drawing.Size(458, 83);
      this.richTextBox2.TabIndex = 101;
      this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
      // 
      // richTextBox3
      // 
      this.richTextBox3.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox3.Location = new System.Drawing.Point(30, 356);
      this.richTextBox3.Name = "richTextBox3";
      this.richTextBox3.ReadOnly = true;
      this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox3.Size = new System.Drawing.Size(458, 35);
      this.richTextBox3.TabIndex = 102;
      this.richTextBox3.Text = "With^KeyboardMode,$the pitches that would normally play on a midi keyboard are pl" +
    "ayed, subject to any transposition that may be set.\n";
      // 
      // richTextBox4
      // 
      this.richTextBox4.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox4.Location = new System.Drawing.Point(30, 437);
      this.richTextBox4.Name = "richTextBox4";
      this.richTextBox4.ReadOnly = true;
      this.richTextBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox4.Size = new System.Drawing.Size(458, 104);
      this.richTextBox4.TabIndex = 103;
      this.richTextBox4.Text = resources.GetString("richTextBox4.Text");
      // 
      // richTextBox5
      // 
      this.richTextBox5.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox5.Location = new System.Drawing.Point(32, 583);
      this.richTextBox5.Name = "richTextBox5";
      this.richTextBox5.ReadOnly = true;
      this.richTextBox5.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox5.Size = new System.Drawing.Size(458, 284);
      this.richTextBox5.TabIndex = 104;
      this.richTextBox5.Text = resources.GetString("richTextBox5.Text");
      // 
      // frmHowToPlayChords
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(764, 571);
      this.Controls.Add(this.richTextBox5);
      this.Controls.Add(this.richTextBox4);
      this.Controls.Add(this.richTextBox3);
      this.Controls.Add(this.richTextBox2);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.cmdSwitchMode);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.cmdChordMode);
      this.Controls.Add(this.cmdKeyboardMode);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cmdShowPlayMap);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.cmdLoadSong);
      this.Controls.Add(this.label5);
      this.Name = "frmHowToPlayChords";
      this.Text = "How To Play Chords along with a MidiFile";
      this.Load += new System.EventHandler(this.frmHowToPlayChords_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button cmdLoadSong;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button cmdShowPlayMap;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button cmdKeyboardMode;
    private System.Windows.Forms.Button cmdChordMode;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button cmdSwitchMode;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.RichTextBox richTextBox2;
    private System.Windows.Forms.RichTextBox richTextBox3;
    private System.Windows.Forms.RichTextBox richTextBox4;
    private System.Windows.Forms.RichTextBox richTextBox5;
  }
}