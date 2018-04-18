namespace ChordCadenza.Forms {
  partial class frmInitial {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInitial));
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.cmdMidiDevs = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.cmdAudioDevs = new System.Windows.Forms.Button();
      this.cmdKBRanges = new System.Windows.Forms.Button();
      this.textBox4 = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.cmdClose = new System.Windows.Forms.Button();
      this.cmdCloseFinal = new System.Windows.Forms.Button();
      this.cmdSaveSettings = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.cmdCopyHTML = new System.Windows.Forms.Button();
      this.cmdIntro = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox1.Location = new System.Drawing.Point(45, 65);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(460, 93);
      this.textBox1.TabIndex = 0;
      this.textBox1.TabStop = false;
      this.textBox1.Text = resources.GetString("textBox1.Text");
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(42, 294);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(80, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Midi Devices";
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(45, 317);
      this.textBox2.Multiline = true;
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = true;
      this.textBox2.Size = new System.Drawing.Size(460, 156);
      this.textBox2.TabIndex = 2;
      this.textBox2.TabStop = false;
      this.textBox2.Text = resources.GetString("textBox2.Text");
      // 
      // cmdMidiDevs
      // 
      this.cmdMidiDevs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdMidiDevs.Location = new System.Drawing.Point(545, 380);
      this.cmdMidiDevs.Name = "cmdMidiDevs";
      this.cmdMidiDevs.Size = new System.Drawing.Size(220, 44);
      this.cmdMidiDevs.TabIndex = 3;
      this.cmdMidiDevs.Text = "Configure Midi Devices\r\nand SoundFonts";
      this.cmdMidiDevs.UseVisualStyleBackColor = true;
      this.cmdMidiDevs.Click += new System.EventHandler(this.cmdMidiDevs_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(43, 177);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(81, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Audio Output";
      // 
      // textBox3
      // 
      this.textBox3.Location = new System.Drawing.Point(46, 201);
      this.textBox3.Multiline = true;
      this.textBox3.Name = "textBox3";
      this.textBox3.ReadOnly = true;
      this.textBox3.Size = new System.Drawing.Size(460, 74);
      this.textBox3.TabIndex = 5;
      this.textBox3.TabStop = false;
      this.textBox3.Text = resources.GetString("textBox3.Text");
      // 
      // cmdAudioDevs
      // 
      this.cmdAudioDevs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdAudioDevs.Location = new System.Drawing.Point(546, 222);
      this.cmdAudioDevs.Name = "cmdAudioDevs";
      this.cmdAudioDevs.Size = new System.Drawing.Size(220, 30);
      this.cmdAudioDevs.TabIndex = 6;
      this.cmdAudioDevs.Text = "Configure Audio Devices";
      this.cmdAudioDevs.UseVisualStyleBackColor = true;
      this.cmdAudioDevs.Click += new System.EventHandler(this.cmdAudioDevs_Click);
      // 
      // cmdKBRanges
      // 
      this.cmdKBRanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdKBRanges.Location = new System.Drawing.Point(546, 525);
      this.cmdKBRanges.Name = "cmdKBRanges";
      this.cmdKBRanges.Size = new System.Drawing.Size(220, 30);
      this.cmdKBRanges.TabIndex = 10;
      this.cmdKBRanges.Text = "Configure Keyboard Ranges";
      this.cmdKBRanges.UseVisualStyleBackColor = true;
      this.cmdKBRanges.Click += new System.EventHandler(this.cmdKBRanges_Click);
      // 
      // textBox4
      // 
      this.textBox4.Location = new System.Drawing.Point(46, 514);
      this.textBox4.Multiline = true;
      this.textBox4.Name = "textBox4";
      this.textBox4.ReadOnly = true;
      this.textBox4.Size = new System.Drawing.Size(460, 51);
      this.textBox4.TabIndex = 9;
      this.textBox4.TabStop = false;
      this.textBox4.Text = "This is where you configure the midi pitch ranges that your midi keyboard can tra" +
    "nsmit.\r\n\r\nThis is used to set up any switchkeys that you may want to use.";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(43, 489);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(134, 13);
      this.label3.TabIndex = 8;
      this.label3.Text = "Midi Keyboard Ranges";
      // 
      // cmdClose
      // 
      this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdClose.Location = new System.Drawing.Point(545, 65);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(220, 30);
      this.cmdClose.TabIndex = 0;
      this.cmdClose.Text = "Close Window";
      this.cmdClose.UseVisualStyleBackColor = true;
      this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
      // 
      // cmdCloseFinal
      // 
      this.cmdCloseFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdCloseFinal.Location = new System.Drawing.Point(545, 114);
      this.cmdCloseFinal.Name = "cmdCloseFinal";
      this.cmdCloseFinal.Size = new System.Drawing.Size(220, 44);
      this.cmdCloseFinal.TabIndex = 12;
      this.cmdCloseFinal.Text = "Close Window and \r\nDon\'t Show Again";
      this.cmdCloseFinal.UseVisualStyleBackColor = true;
      this.cmdCloseFinal.Click += new System.EventHandler(this.cmdCloseFinal_Click);
      // 
      // cmdSaveSettings
      // 
      this.cmdSaveSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdSaveSettings.Location = new System.Drawing.Point(347, 586);
      this.cmdSaveSettings.Name = "cmdSaveSettings";
      this.cmdSaveSettings.Size = new System.Drawing.Size(158, 30);
      this.cmdSaveSettings.TabIndex = 13;
      this.cmdSaveSettings.Text = "Save Settings";
      this.cmdSaveSettings.UseVisualStyleBackColor = true;
      this.cmdSaveSettings.Click += new System.EventHandler(this.cmdSaveSettings_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(43, 34);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(168, 13);
      this.label4.TabIndex = 92;
      this.label4.Text = "Welcome to Chord Cadenza.";
      // 
      // cmdCopyHTML
      // 
      this.cmdCopyHTML.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdCopyHTML.ForeColor = System.Drawing.Color.Red;
      this.cmdCopyHTML.Location = new System.Drawing.Point(545, 277);
      this.cmdCopyHTML.Name = "cmdCopyHTML";
      this.cmdCopyHTML.Size = new System.Drawing.Size(220, 30);
      this.cmdCopyHTML.TabIndex = 93;
      this.cmdCopyHTML.Text = "Copy HTML Files";
      this.cmdCopyHTML.UseVisualStyleBackColor = true;
      // 
      // cmdIntro
      // 
      this.cmdIntro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdIntro.Location = new System.Drawing.Point(45, 586);
      this.cmdIntro.Name = "cmdIntro";
      this.cmdIntro.Size = new System.Drawing.Size(116, 30);
      this.cmdIntro.TabIndex = 94;
      this.cmdIntro.Text = "Introduction";
      this.cmdIntro.UseVisualStyleBackColor = true;
      this.cmdIntro.Click += new System.EventHandler(this.cmdIntro_Click);
      // 
      // frmInitial
      // 
      this.AcceptButton = this.cmdClose;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdClose;
      this.ClientSize = new System.Drawing.Size(777, 626);
      this.Controls.Add(this.cmdIntro);
      this.Controls.Add(this.cmdCopyHTML);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.cmdSaveSettings);
      this.Controls.Add(this.cmdCloseFinal);
      this.Controls.Add(this.cmdClose);
      this.Controls.Add(this.cmdKBRanges);
      this.Controls.Add(this.textBox4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.cmdAudioDevs);
      this.Controls.Add(this.textBox3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cmdMidiDevs);
      this.Controls.Add(this.textBox2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmInitial";
      this.Text = "Initial Configuration - Chord Cadenza";
      this.Load += new System.EventHandler(this.frmInitial_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Button cmdMidiDevs;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.Button cmdAudioDevs;
    private System.Windows.Forms.Button cmdKBRanges;
    private System.Windows.Forms.TextBox textBox4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button cmdClose;
    private System.Windows.Forms.Button cmdCloseFinal;
    private System.Windows.Forms.Button cmdSaveSettings;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button cmdCopyHTML;
    private System.Windows.Forms.Button cmdIntro;
  }
}