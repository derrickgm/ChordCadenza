namespace MPlay.Forms.HowTo {
  partial class frmHowToGenKeys {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHowToGenKeys));
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.cmdLoadMidiFile = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.richTextBox2 = new System.Windows.Forms.RichTextBox();
      this.richTextBox3 = new System.Windows.Forms.RichTextBox();
      this.richTextBox4 = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 66);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(166, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Open the TrackMap window";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 177);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(342, 13);
      this.label2.TabIndex = 6;
      this.label2.Text = "Choose which tracks you want to use to calculate the keys";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(12, 285);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(258, 13);
      this.label3.TabIndex = 8;
      this.label3.Text = "Hit the \"Calc Keys\" button on the TrackMap";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(9, 437);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(268, 13);
      this.label4.TabIndex = 12;
      this.label4.Text = "Hit the \"Apply\" button on the CalcKey window";
      // 
      // cmdLoadMidiFile
      // 
      this.cmdLoadMidiFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdLoadMidiFile.Location = new System.Drawing.Point(496, 9);
      this.cmdLoadMidiFile.Name = "cmdLoadMidiFile";
      this.cmdLoadMidiFile.Size = new System.Drawing.Size(220, 30);
      this.cmdLoadMidiFile.TabIndex = 15;
      this.cmdLoadMidiFile.Text = "Load MidiFile";
      this.cmdLoadMidiFile.UseVisualStyleBackColor = true;
      this.cmdLoadMidiFile.Click += new System.EventHandler(this.cmdLoadMidiFile_Click);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(12, 18);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(269, 13);
      this.label5.TabIndex = 14;
      this.label5.Text = "If you haven\'t already done so, load a MidiFile";
      // 
      // richTextBox1
      // 
      this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Location = new System.Drawing.Point(30, 92);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox1.Size = new System.Drawing.Size(460, 62);
      this.richTextBox1.TabIndex = 93;
      this.richTextBox1.Text = "If there is no corresponding chord file in the directory containing the midi file" +
    ", this should open the^TrackMap$window.\n\nOtherwise, click the^TrackMap$button on" +
    " the^PlayMap$window.";
      // 
      // richTextBox2
      // 
      this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox2.Location = new System.Drawing.Point(27, 205);
      this.richTextBox2.Name = "richTextBox2";
      this.richTextBox2.ReadOnly = true;
      this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox2.Size = new System.Drawing.Size(460, 62);
      this.richTextBox2.TabIndex = 94;
      this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
      // 
      // richTextBox3
      // 
      this.richTextBox3.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox3.Location = new System.Drawing.Point(27, 317);
      this.richTextBox3.Name = "richTextBox3";
      this.richTextBox3.ReadOnly = true;
      this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox3.Size = new System.Drawing.Size(460, 117);
      this.richTextBox3.TabIndex = 95;
      this.richTextBox3.Text = resources.GetString("richTextBox3.Text");
      // 
      // richTextBox4
      // 
      this.richTextBox4.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox4.Location = new System.Drawing.Point(27, 465);
      this.richTextBox4.Name = "richTextBox4";
      this.richTextBox4.ReadOnly = true;
      this.richTextBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox4.Size = new System.Drawing.Size(460, 31);
      this.richTextBox4.TabIndex = 96;
      this.richTextBox4.Text = "Hit the^Apply and Close$button to generate the keys and close the window.";
      // 
      // frmHowToGenKeys
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(728, 628);
      this.Controls.Add(this.richTextBox4);
      this.Controls.Add(this.richTextBox3);
      this.Controls.Add(this.richTextBox2);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.cmdLoadMidiFile);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmHowToGenKeys";
      this.Text = "How To Generate Keys from a MidiFile";
      this.Load += new System.EventHandler(this.frmHowToGenKeys_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button cmdLoadMidiFile;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.RichTextBox richTextBox2;
    private System.Windows.Forms.RichTextBox richTextBox3;
    private System.Windows.Forms.RichTextBox richTextBox4;
  }
}