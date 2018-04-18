namespace MPlay.Forms.HowTo {
  partial class frmHowToPlayMelody {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHowToPlayMelody));
      this.cmdLoadSong = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.cmdShowPlayMap = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.cmdKeyboardMode = new System.Windows.Forms.Button();
      this.cmdChordMode = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.richTextBox3 = new System.Windows.Forms.RichTextBox();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.richTextBox2 = new System.Windows.Forms.RichTextBox();
      this.richTextBox4 = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // cmdLoadSong
      // 
      this.cmdLoadSong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdLoadSong.Location = new System.Drawing.Point(506, 138);
      this.cmdLoadSong.Name = "cmdLoadSong";
      this.cmdLoadSong.Size = new System.Drawing.Size(220, 30);
      this.cmdLoadSong.TabIndex = 35;
      this.cmdLoadSong.Text = "Load Song";
      this.cmdLoadSong.UseVisualStyleBackColor = true;
      this.cmdLoadSong.Click += new System.EventHandler(this.cmdLoadSong_Click);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(27, 147);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(380, 13);
      this.label5.TabIndex = 34;
      this.label5.Text = "If you haven\'t already done so, load a Song containing a chordfile and a midifile" +
    "";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 195);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(115, 13);
      this.label1.TabIndex = 33;
      this.label1.Text = "Open The PlayMap";
      // 
      // cmdShowPlayMap
      // 
      this.cmdShowPlayMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdShowPlayMap.Location = new System.Drawing.Point(506, 224);
      this.cmdShowPlayMap.Name = "cmdShowPlayMap";
      this.cmdShowPlayMap.Size = new System.Drawing.Size(220, 30);
      this.cmdShowPlayMap.TabIndex = 38;
      this.cmdShowPlayMap.Text = "Show PlayMap";
      this.cmdShowPlayMap.UseVisualStyleBackColor = true;
      this.cmdShowPlayMap.Click += new System.EventHandler(this.cmdShowPlayMap_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 343);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(117, 13);
      this.label2.TabIndex = 40;
      this.label2.Text = "Select a Play Mode";
      // 
      // textBox3
      // 
      this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox3.Location = new System.Drawing.Point(27, 39);
      this.textBox3.Multiline = true;
      this.textBox3.Name = "textBox3";
      this.textBox3.ReadOnly = true;
      this.textBox3.Size = new System.Drawing.Size(463, 93);
      this.textBox3.TabIndex = 41;
      this.textBox3.TabStop = false;
      this.textBox3.Text = resources.GetString("textBox3.Text");
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(9, 514);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(95, 13);
      this.label3.TabIndex = 43;
      this.label3.Text = "Keyboard Mode";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(12, 658);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(75, 13);
      this.label4.TabIndex = 45;
      this.label4.Text = "Chord Mode";
      // 
      // cmdKeyboardMode
      // 
      this.cmdKeyboardMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdKeyboardMode.Location = new System.Drawing.Point(506, 543);
      this.cmdKeyboardMode.Name = "cmdKeyboardMode";
      this.cmdKeyboardMode.Size = new System.Drawing.Size(220, 30);
      this.cmdKeyboardMode.TabIndex = 46;
      this.cmdKeyboardMode.Text = "Set to Keyboard Mode";
      this.cmdKeyboardMode.UseVisualStyleBackColor = true;
      this.cmdKeyboardMode.Click += new System.EventHandler(this.cmdKeyboardMode_Click);
      // 
      // cmdChordMode
      // 
      this.cmdChordMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdChordMode.Location = new System.Drawing.Point(509, 687);
      this.cmdChordMode.Name = "cmdChordMode";
      this.cmdChordMode.Size = new System.Drawing.Size(220, 30);
      this.cmdChordMode.TabIndex = 47;
      this.cmdChordMode.Text = "Set to Chord Mode";
      this.cmdChordMode.UseVisualStyleBackColor = true;
      this.cmdChordMode.Click += new System.EventHandler(this.cmdChordMode_Click);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(14, 9);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(75, 13);
      this.label6.TabIndex = 48;
      this.label6.Text = "Introduction";
      // 
      // richTextBox3
      // 
      this.richTextBox3.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox3.Location = new System.Drawing.Point(27, 224);
      this.richTextBox3.Name = "richTextBox3";
      this.richTextBox3.ReadOnly = true;
      this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox3.Size = new System.Drawing.Size(463, 98);
      this.richTextBox3.TabIndex = 103;
      this.richTextBox3.Text = resources.GetString("richTextBox3.Text");
      // 
      // richTextBox1
      // 
      this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Location = new System.Drawing.Point(27, 375);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox1.Size = new System.Drawing.Size(463, 98);
      this.richTextBox1.TabIndex = 104;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // richTextBox2
      // 
      this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox2.Location = new System.Drawing.Point(27, 543);
      this.richTextBox2.Name = "richTextBox2";
      this.richTextBox2.ReadOnly = true;
      this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox2.Size = new System.Drawing.Size(463, 98);
      this.richTextBox2.TabIndex = 105;
      this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
      // 
      // richTextBox4
      // 
      this.richTextBox4.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox4.Location = new System.Drawing.Point(27, 687);
      this.richTextBox4.Name = "richTextBox4";
      this.richTextBox4.ReadOnly = true;
      this.richTextBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox4.Size = new System.Drawing.Size(463, 40);
      this.richTextBox4.TabIndex = 106;
      this.richTextBox4.Text = "With^ChordMode,$only the notes of the current chord on the white keys are played." +
    " This is easier to play, but does not allow non-chord notes to be played\n\n\n";
      // 
      // frmHowToPlayMelody
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(737, 765);
      this.Controls.Add(this.richTextBox4);
      this.Controls.Add(this.richTextBox2);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.richTextBox3);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.cmdChordMode);
      this.Controls.Add(this.cmdKeyboardMode);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.textBox3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cmdShowPlayMap);
      this.Controls.Add(this.cmdLoadSong);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label1);
      this.Name = "frmHowToPlayMelody";
      this.Text = "How To Improvise a Melody along with a MidiFile";
      this.Load += new System.EventHandler(this.frmHowToPlayMelody_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdLoadSong;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button cmdShowPlayMap;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button cmdKeyboardMode;
    private System.Windows.Forms.Button cmdChordMode;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.RichTextBox richTextBox3;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.RichTextBox richTextBox2;
    private System.Windows.Forms.RichTextBox richTextBox4;
  }
}