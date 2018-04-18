namespace MPlay.Forms.HowTo {
  partial class frmHowToSyncAudioStream {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHowToSyncAudioStream));
      this.cmdAutoSyncWindow = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.cmdShowChordMap = new System.Windows.Forms.Button();
      this.cmdShowTrackMap = new System.Windows.Forms.Button();
      this.cmdShowPlayMap = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.richTextBox3 = new System.Windows.Forms.RichTextBox();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.richTextBox2 = new System.Windows.Forms.RichTextBox();
      this.richTextBox5 = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // cmdAutoSyncWindow
      // 
      this.cmdAutoSyncWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdAutoSyncWindow.Location = new System.Drawing.Point(538, 722);
      this.cmdAutoSyncWindow.Name = "cmdAutoSyncWindow";
      this.cmdAutoSyncWindow.Size = new System.Drawing.Size(220, 30);
      this.cmdAutoSyncWindow.TabIndex = 86;
      this.cmdAutoSyncWindow.Text = "Show AudioSync Window";
      this.cmdAutoSyncWindow.UseVisualStyleBackColor = true;
      this.cmdAutoSyncWindow.Click += new System.EventHandler(this.cmdAutoSyncWindow_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 696);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(121, 13);
      this.label1.TabIndex = 84;
      this.label1.Text = "Save the Sync Data";
      // 
      // cmdShowChordMap
      // 
      this.cmdShowChordMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdShowChordMap.Location = new System.Drawing.Point(535, 301);
      this.cmdShowChordMap.Name = "cmdShowChordMap";
      this.cmdShowChordMap.Size = new System.Drawing.Size(220, 30);
      this.cmdShowChordMap.TabIndex = 83;
      this.cmdShowChordMap.Text = "Show ChordMap";
      this.cmdShowChordMap.UseVisualStyleBackColor = true;
      this.cmdShowChordMap.Click += new System.EventHandler(this.cmdShowChordMap_Click);
      // 
      // cmdShowTrackMap
      // 
      this.cmdShowTrackMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdShowTrackMap.Location = new System.Drawing.Point(535, 251);
      this.cmdShowTrackMap.Name = "cmdShowTrackMap";
      this.cmdShowTrackMap.Size = new System.Drawing.Size(220, 30);
      this.cmdShowTrackMap.TabIndex = 82;
      this.cmdShowTrackMap.Text = "Show TrackMap";
      this.cmdShowTrackMap.UseVisualStyleBackColor = true;
      this.cmdShowTrackMap.Click += new System.EventHandler(this.cmdShowTrackMap_Click);
      // 
      // cmdShowPlayMap
      // 
      this.cmdShowPlayMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdShowPlayMap.Location = new System.Drawing.Point(533, 150);
      this.cmdShowPlayMap.Name = "cmdShowPlayMap";
      this.cmdShowPlayMap.Size = new System.Drawing.Size(220, 30);
      this.cmdShowPlayMap.TabIndex = 81;
      this.cmdShowPlayMap.Text = "Show PlayMap";
      this.cmdShowPlayMap.UseVisualStyleBackColor = true;
      this.cmdShowPlayMap.Click += new System.EventHandler(this.cmdShowPlayMap_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(9, 224);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(220, 13);
      this.label4.TabIndex = 79;
      this.label4.Text = "Position the ChordFile to a Cue Point ";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(9, 420);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(151, 13);
      this.label3.TabIndex = 77;
      this.label3.Text = "Sync to the Audio Stream";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(9, 130);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(157, 13);
      this.label2.TabIndex = 75;
      this.label2.Text = "Setup the Sync SwitchKey";
      // 
      // textBox1
      // 
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Location = new System.Drawing.Point(27, 41);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(492, 60);
      this.textBox1.TabIndex = 74;
      this.textBox1.TabStop = false;
      this.textBox1.Text = resources.GetString("textBox1.Text");
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(12, 19);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(259, 13);
      this.label5.TabIndex = 72;
      this.label5.Text = "If you haven\'t already done so, load a Song ";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(535, 354);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(159, 13);
      this.label6.TabIndex = 89;
      this.label6.Text = "***Different from Audio File";
      // 
      // richTextBox3
      // 
      this.richTextBox3.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox3.Location = new System.Drawing.Point(27, 150);
      this.richTextBox3.Name = "richTextBox3";
      this.richTextBox3.ReadOnly = true;
      this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox3.Size = new System.Drawing.Size(492, 53);
      this.richTextBox3.TabIndex = 104;
      this.richTextBox3.Text = "Goto^PlayMap Menu > Configure > KeySwitches/Pedal.$\n\nSet the^Sync$dropdown box to" +
    " a SwitchKey or Pedal.";
      // 
      // richTextBox1
      // 
      this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Location = new System.Drawing.Point(27, 255);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox1.Size = new System.Drawing.Size(492, 144);
      this.richTextBox1.TabIndex = 105;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // richTextBox2
      // 
      this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox2.Location = new System.Drawing.Point(27, 450);
      this.richTextBox2.Name = "richTextBox2";
      this.richTextBox2.ReadOnly = true;
      this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox2.Size = new System.Drawing.Size(492, 211);
      this.richTextBox2.TabIndex = 106;
      this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
      // 
      // richTextBox5
      // 
      this.richTextBox5.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox5.Location = new System.Drawing.Point(27, 722);
      this.richTextBox5.Name = "richTextBox5";
      this.richTextBox5.ReadOnly = true;
      this.richTextBox5.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox5.Size = new System.Drawing.Size(492, 160);
      this.richTextBox5.TabIndex = 109;
      this.richTextBox5.Text = resources.GetString("richTextBox5.Text");
      // 
      // frmHowToSyncAudioStream
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(783, 666);
      this.Controls.Add(this.richTextBox5);
      this.Controls.Add(this.richTextBox2);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.richTextBox3);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.cmdAutoSyncWindow);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cmdShowChordMap);
      this.Controls.Add(this.cmdShowTrackMap);
      this.Controls.Add(this.cmdShowPlayMap);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.label5);
      this.Name = "frmHowToSyncAudioStream";
      this.Text = "HowTo Sync to an Audio Stream";
      this.Load += new System.EventHandler(this.frmHowToSyncAudioStream_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdAutoSyncWindow;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button cmdShowChordMap;
    private System.Windows.Forms.Button cmdShowTrackMap;
    private System.Windows.Forms.Button cmdShowPlayMap;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.RichTextBox richTextBox3;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.RichTextBox richTextBox2;
    private System.Windows.Forms.RichTextBox richTextBox5;
  }
}