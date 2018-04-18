namespace MPlay.Forms.HowTo {
  partial class frmHowToPlayLiveAudio {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHowToPlayLiveAudio));
      this.label1 = new System.Windows.Forms.Label();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.cmdShowPlayMap = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmdConfig = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.richTextBox3 = new System.Windows.Forms.RichTextBox();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 18);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(75, 13);
      this.label1.TabIndex = 60;
      this.label1.Text = "Introduction";
      // 
      // textBox2
      // 
      this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox2.Location = new System.Drawing.Point(30, 34);
      this.textBox2.Multiline = true;
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = true;
      this.textBox2.Size = new System.Drawing.Size(712, 63);
      this.textBox2.TabIndex = 59;
      this.textBox2.TabStop = false;
      this.textBox2.Text = resources.GetString("textBox2.Text");
      // 
      // cmdShowPlayMap
      // 
      this.cmdShowPlayMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdShowPlayMap.Location = new System.Drawing.Point(579, 143);
      this.cmdShowPlayMap.Name = "cmdShowPlayMap";
      this.cmdShowPlayMap.Size = new System.Drawing.Size(220, 30);
      this.cmdShowPlayMap.TabIndex = 72;
      this.cmdShowPlayMap.Text = "Show PlayMap";
      this.cmdShowPlayMap.UseVisualStyleBackColor = true;
      this.cmdShowPlayMap.Click += new System.EventHandler(this.cmdShowPlayMap_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(12, 118);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(131, 13);
      this.label4.TabIndex = 70;
      this.label4.Text = "Setup the SwitchKeys";
      // 
      // textBox1
      // 
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Location = new System.Drawing.Point(30, 442);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(535, 216);
      this.textBox1.TabIndex = 74;
      this.textBox1.TabStop = false;
      this.textBox1.Text = resources.GetString("textBox1.Text");
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 414);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(156, 13);
      this.label2.TabIndex = 73;
      this.label2.Text = "Load a ChordFile and Play";
      // 
      // cmdConfig
      // 
      this.cmdConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdConfig.Location = new System.Drawing.Point(579, 344);
      this.cmdConfig.Name = "cmdConfig";
      this.cmdConfig.Size = new System.Drawing.Size(220, 30);
      this.cmdConfig.TabIndex = 78;
      this.cmdConfig.Text = "Show Config Window";
      this.cmdConfig.UseVisualStyleBackColor = true;
      this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(12, 224);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(131, 13);
      this.label3.TabIndex = 76;
      this.label3.Text = "Setup the Sync Event";
      // 
      // richTextBox3
      // 
      this.richTextBox3.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox3.Location = new System.Drawing.Point(30, 143);
      this.richTextBox3.Name = "richTextBox3";
      this.richTextBox3.ReadOnly = true;
      this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox3.Size = new System.Drawing.Size(535, 62);
      this.richTextBox3.TabIndex = 103;
      this.richTextBox3.Text = "Go to^PlayMap Menu > Configure > KeySwitches/Pedal.$\n\nSet the^NextEvent$ dropdown" +
    " box to a SwitchKey or Pedal.\n\n ";
      // 
      // richTextBox1
      // 
      this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Location = new System.Drawing.Point(30, 256);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox1.Size = new System.Drawing.Size(535, 129);
      this.richTextBox1.TabIndex = 104;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // frmHowToPlayLiveAudio
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(814, 669);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.richTextBox3);
      this.Controls.Add(this.cmdConfig);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cmdShowPlayMap);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBox2);
      this.Name = "frmHowToPlayLiveAudio";
      this.Text = "How To Play along with Live Audio";
      this.Load += new System.EventHandler(this.frmHowToPlayLiveAudio_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Button cmdShowPlayMap;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button cmdConfig;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.RichTextBox richTextBox3;
    private System.Windows.Forms.RichTextBox richTextBox1;
  }
}