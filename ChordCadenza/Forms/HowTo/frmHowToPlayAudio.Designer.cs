namespace MPlay.Forms.HowTo {
  partial class frmHowToPlayAudio {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHowToPlayAudio));
      this.label1 = new System.Windows.Forms.Label();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.cmdHowToSwitchKey = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.cmdLiveAudio = new System.Windows.Forms.Button();
      this.cmdSyncAudioSource = new System.Windows.Forms.Button();
      this.richTextBox3 = new System.Windows.Forms.RichTextBox();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.richTextBox2 = new System.Windows.Forms.RichTextBox();
      this.richTextBox4 = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(75, 13);
      this.label1.TabIndex = 58;
      this.label1.Text = "Introduction";
      // 
      // textBox2
      // 
      this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox2.Location = new System.Drawing.Point(30, 38);
      this.textBox2.Multiline = true;
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = true;
      this.textBox2.Size = new System.Drawing.Size(552, 131);
      this.textBox2.TabIndex = 57;
      this.textBox2.TabStop = false;
      this.textBox2.Text = resources.GetString("textBox2.Text");
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 468);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(79, 13);
      this.label2.TabIndex = 59;
      this.label2.Text = "Load a Song";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(12, 601);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(94, 13);
      this.label3.TabIndex = 62;
      this.label3.Text = "Start Sync Play";
      // 
      // cmdHowToSwitchKey
      // 
      this.cmdHowToSwitchKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdHowToSwitchKey.Location = new System.Drawing.Point(593, 399);
      this.cmdHowToSwitchKey.Name = "cmdHowToSwitchKey";
      this.cmdHowToSwitchKey.Size = new System.Drawing.Size(220, 51);
      this.cmdHowToSwitchKey.TabIndex = 69;
      this.cmdHowToSwitchKey.Text = "HowTo Set Up\r\na SwitchKey";
      this.cmdHowToSwitchKey.UseVisualStyleBackColor = true;
      this.cmdHowToSwitchKey.Click += new System.EventHandler(this.cmdHowToSwitchKey_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(9, 374);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(157, 13);
      this.label4.TabIndex = 67;
      this.label4.Text = "Setup the Sync SwitchKey";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(12, 189);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(200, 13);
      this.label6.TabIndex = 73;
      this.label6.Text = "Choose a Synchronisation Method";
      // 
      // cmdLiveAudio
      // 
      this.cmdLiveAudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdLiveAudio.Location = new System.Drawing.Point(593, 281);
      this.cmdLiveAudio.Name = "cmdLiveAudio";
      this.cmdLiveAudio.Size = new System.Drawing.Size(220, 50);
      this.cmdLiveAudio.TabIndex = 76;
      this.cmdLiveAudio.Text = "HowTo Play Along\r\nto Live Audio";
      this.cmdLiveAudio.UseVisualStyleBackColor = true;
      this.cmdLiveAudio.Click += new System.EventHandler(this.cmdLiveAudio_Click);
      // 
      // cmdSyncAudioSource
      // 
      this.cmdSyncAudioSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdSyncAudioSource.Location = new System.Drawing.Point(593, 214);
      this.cmdSyncAudioSource.Name = "cmdSyncAudioSource";
      this.cmdSyncAudioSource.Size = new System.Drawing.Size(220, 50);
      this.cmdSyncAudioSource.TabIndex = 77;
      this.cmdSyncAudioSource.Text = "HowTo Sync\r\nto an Audio Source";
      this.cmdSyncAudioSource.UseVisualStyleBackColor = true;
      this.cmdSyncAudioSource.Click += new System.EventHandler(this.cmdSyncAudioSource_Click);
      // 
      // richTextBox3
      // 
      this.richTextBox3.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox3.Location = new System.Drawing.Point(30, 214);
      this.richTextBox3.Name = "richTextBox3";
      this.richTextBox3.ReadOnly = true;
      this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox3.Size = new System.Drawing.Size(552, 140);
      this.richTextBox3.TabIndex = 98;
      this.richTextBox3.Text = resources.GetString("richTextBox3.Text");
      // 
      // richTextBox1
      // 
      this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Location = new System.Drawing.Point(30, 402);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox1.Size = new System.Drawing.Size(552, 48);
      this.richTextBox1.TabIndex = 99;
      this.richTextBox1.Text = "Goto^PlayMap Menu > Configure > KeySwitches/Pedal.$\n\nSet the^Sync$dropdown box to" +
    " a SwitchKey or Pedal.";
      // 
      // richTextBox2
      // 
      this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox2.Location = new System.Drawing.Point(27, 493);
      this.richTextBox2.Name = "richTextBox2";
      this.richTextBox2.ReadOnly = true;
      this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox2.Size = new System.Drawing.Size(552, 91);
      this.richTextBox2.TabIndex = 100;
      this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
      // 
      // richTextBox4
      // 
      this.richTextBox4.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox4.Location = new System.Drawing.Point(27, 631);
      this.richTextBox4.Name = "richTextBox4";
      this.richTextBox4.ReadOnly = true;
      this.richTextBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox4.Size = new System.Drawing.Size(552, 185);
      this.richTextBox4.TabIndex = 101;
      this.richTextBox4.Text = resources.GetString("richTextBox4.Text");
      this.richTextBox4.TextChanged += new System.EventHandler(this.richTextBox4_TextChanged);
      // 
      // frmHowToPlayAudio
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(843, 696);
      this.Controls.Add(this.richTextBox4);
      this.Controls.Add(this.richTextBox2);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.richTextBox3);
      this.Controls.Add(this.cmdSyncAudioSource);
      this.Controls.Add(this.cmdLiveAudio);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.cmdHowToSwitchKey);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBox2);
      this.Name = "frmHowToPlayAudio";
      this.Text = "How To Play along with an Audio File, Audio Stream, or Live Audio";
      this.Load += new System.EventHandler(this.frmHowToPlayAudioFile_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button cmdHowToSwitchKey;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button cmdLiveAudio;
    private System.Windows.Forms.Button cmdSyncAudioSource;
    private System.Windows.Forms.RichTextBox richTextBox3;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.RichTextBox richTextBox2;
    private System.Windows.Forms.RichTextBox richTextBox4;
  }
}