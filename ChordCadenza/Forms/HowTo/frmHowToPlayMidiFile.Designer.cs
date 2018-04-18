namespace MPlay.Forms.HowTo {
  partial class frmHowToPlayMidiFile {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHowToPlayMidiFile));
      this.label1 = new System.Windows.Forms.Label();
      this.cmdShowTrackMap = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.cmdShowPlayMap = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.cmdLoadSong = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.richTextBox3 = new System.Windows.Forms.RichTextBox();
      this.cmdPlayMelody = new System.Windows.Forms.Button();
      this.cmdPlayChords = new System.Windows.Forms.Button();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 67);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(150, 13);
      this.label1.TabIndex = 28;
      this.label1.Text = "Open A Playable Window";
      // 
      // cmdShowTrackMap
      // 
      this.cmdShowTrackMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdShowTrackMap.Location = new System.Drawing.Point(496, 224);
      this.cmdShowTrackMap.Name = "cmdShowTrackMap";
      this.cmdShowTrackMap.Size = new System.Drawing.Size(220, 30);
      this.cmdShowTrackMap.TabIndex = 34;
      this.cmdShowTrackMap.Text = "Show TrackMap";
      this.cmdShowTrackMap.UseVisualStyleBackColor = true;
      this.cmdShowTrackMap.Click += new System.EventHandler(this.cmdShowTrackMap_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 197);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(90, 13);
      this.label2.TabIndex = 33;
      this.label2.Text = "The TrackMap";
      // 
      // textBox1
      // 
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Location = new System.Drawing.Point(27, 224);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(460, 77);
      this.textBox1.TabIndex = 32;
      this.textBox1.TabStop = false;
      this.textBox1.Text = resources.GetString("textBox1.Text");
      // 
      // cmdShowPlayMap
      // 
      this.cmdShowPlayMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdShowPlayMap.Location = new System.Drawing.Point(496, 358);
      this.cmdShowPlayMap.Name = "cmdShowPlayMap";
      this.cmdShowPlayMap.Size = new System.Drawing.Size(220, 30);
      this.cmdShowPlayMap.TabIndex = 37;
      this.cmdShowPlayMap.Text = "Show PlayMap";
      this.cmdShowPlayMap.UseVisualStyleBackColor = true;
      this.cmdShowPlayMap.Click += new System.EventHandler(this.cmdShowPlayMap_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(12, 331);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(81, 13);
      this.label3.TabIndex = 36;
      this.label3.Text = "The PlayMap";
      // 
      // cmdLoadSong
      // 
      this.cmdLoadSong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdLoadSong.Location = new System.Drawing.Point(493, 11);
      this.cmdLoadSong.Name = "cmdLoadSong";
      this.cmdLoadSong.Size = new System.Drawing.Size(220, 30);
      this.cmdLoadSong.TabIndex = 52;
      this.cmdLoadSong.Text = "Load Song";
      this.cmdLoadSong.UseVisualStyleBackColor = true;
      this.cmdLoadSong.Click += new System.EventHandler(this.cmdLoadSong_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(24, 20);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(380, 13);
      this.label4.TabIndex = 51;
      this.label4.Text = "If you haven\'t already done so, load a Song containing a midifile and a chordfile" +
    "";
      // 
      // richTextBox3
      // 
      this.richTextBox3.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox3.Location = new System.Drawing.Point(27, 95);
      this.richTextBox3.Name = "richTextBox3";
      this.richTextBox3.ReadOnly = true;
      this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox3.Size = new System.Drawing.Size(460, 76);
      this.richTextBox3.TabIndex = 103;
      this.richTextBox3.Text = resources.GetString("richTextBox3.Text");
      // 
      // cmdPlayMelody
      // 
      this.cmdPlayMelody.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdPlayMelody.Location = new System.Drawing.Point(496, 394);
      this.cmdPlayMelody.Name = "cmdPlayMelody";
      this.cmdPlayMelody.Size = new System.Drawing.Size(220, 45);
      this.cmdPlayMelody.TabIndex = 104;
      this.cmdPlayMelody.Text = "HowTo Play a Melody\r\nalong with a MidiFile";
      this.cmdPlayMelody.UseVisualStyleBackColor = true;
      this.cmdPlayMelody.Click += new System.EventHandler(this.cmdPlayMelody_Click);
      // 
      // cmdPlayChords
      // 
      this.cmdPlayChords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdPlayChords.Location = new System.Drawing.Point(496, 445);
      this.cmdPlayChords.Name = "cmdPlayChords";
      this.cmdPlayChords.Size = new System.Drawing.Size(220, 45);
      this.cmdPlayChords.TabIndex = 105;
      this.cmdPlayChords.Text = "HowTo Play a Chords \r\nalong with a MidiFile";
      this.cmdPlayChords.UseVisualStyleBackColor = true;
      this.cmdPlayChords.Click += new System.EventHandler(this.cmdPlayChords_Click);
      // 
      // richTextBox1
      // 
      this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Location = new System.Drawing.Point(27, 358);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox1.Size = new System.Drawing.Size(460, 96);
      this.richTextBox1.TabIndex = 106;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // frmHowToPlayMidiFile
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(734, 514);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.cmdPlayChords);
      this.Controls.Add(this.cmdPlayMelody);
      this.Controls.Add(this.richTextBox3);
      this.Controls.Add(this.cmdLoadSong);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.cmdShowPlayMap);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.cmdShowTrackMap);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.label1);
      this.Name = "frmHowToPlayMidiFile";
      this.Text = "How To Play a MidiFile";
      this.Load += new System.EventHandler(this.frmHowToPlayMidiFile_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button cmdShowTrackMap;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button cmdShowPlayMap;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button cmdLoadSong;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.RichTextBox richTextBox3;
    private System.Windows.Forms.Button cmdPlayMelody;
    private System.Windows.Forms.Button cmdPlayChords;
    private System.Windows.Forms.RichTextBox richTextBox1;
  }
}