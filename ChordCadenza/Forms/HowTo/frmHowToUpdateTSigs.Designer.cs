namespace MPlay.Forms.HowTo {
  partial class frmHowToUpdateTSigs {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHowToUpdateTSigs));
      this.cmdShowChordMap = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.richTextBox2 = new System.Windows.Forms.RichTextBox();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.richTextBox3 = new System.Windows.Forms.RichTextBox();
      this.richTextBox4 = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // cmdShowChordMap
      // 
      this.cmdShowChordMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdShowChordMap.Location = new System.Drawing.Point(523, 162);
      this.cmdShowChordMap.Name = "cmdShowChordMap";
      this.cmdShowChordMap.Size = new System.Drawing.Size(220, 30);
      this.cmdShowChordMap.TabIndex = 69;
      this.cmdShowChordMap.Text = "Show ChordMap";
      this.cmdShowChordMap.UseVisualStyleBackColor = true;
      this.cmdShowChordMap.Click += new System.EventHandler(this.cmdShowChordMap_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 136);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(166, 13);
      this.label2.TabIndex = 68;
      this.label2.Text = "Open the ChordMap window";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 217);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(194, 13);
      this.label1.TabIndex = 70;
      this.label1.Text = "Activate Time Signature Updates";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(12, 324);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(143, 13);
      this.label4.TabIndex = 72;
      this.label4.Text = "Update Time Signatures";
      // 
      // textBox3
      // 
      this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox3.Location = new System.Drawing.Point(27, 502);
      this.textBox3.Multiline = true;
      this.textBox3.Name = "textBox3";
      this.textBox3.ReadOnly = true;
      this.textBox3.Size = new System.Drawing.Size(480, 67);
      this.textBox3.TabIndex = 75;
      this.textBox3.TabStop = false;
      this.textBox3.Text = "In order for the new time signatures to come into effect, you will have to save a" +
    "nd reload the ChordFile.\r\n\r\nThe program will conitinue to use the old time signa" +
    "tures until the ChordFile is reloaded.";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(12, 473);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(184, 13);
      this.label5.TabIndex = 74;
      this.label5.Text = "Save and Reload the ChordFile";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(12, 16);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(75, 13);
      this.label3.TabIndex = 77;
      this.label3.Text = "Introduction";
      // 
      // richTextBox2
      // 
      this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox2.Location = new System.Drawing.Point(30, 44);
      this.richTextBox2.Name = "richTextBox2";
      this.richTextBox2.ReadOnly = true;
      this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox2.Size = new System.Drawing.Size(480, 75);
      this.richTextBox2.TabIndex = 102;
      this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
      // 
      // richTextBox1
      // 
      this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Location = new System.Drawing.Point(30, 162);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox1.Size = new System.Drawing.Size(480, 44);
      this.richTextBox1.TabIndex = 103;
      this.richTextBox1.Text = "If you haven\'t loaded a ChordFile, a window should be displayed to create a new C" +
    "hordFile with a maximum number of bars. Click^OK$to proceed.";
      // 
      // richTextBox3
      // 
      this.richTextBox3.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox3.Location = new System.Drawing.Point(30, 245);
      this.richTextBox3.Name = "richTextBox3";
      this.richTextBox3.ReadOnly = true;
      this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox3.Size = new System.Drawing.Size(480, 63);
      this.richTextBox3.TabIndex = 104;
      this.richTextBox3.Text = resources.GetString("richTextBox3.Text");
      // 
      // richTextBox4
      // 
      this.richTextBox4.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox4.Location = new System.Drawing.Point(30, 351);
      this.richTextBox4.Name = "richTextBox4";
      this.richTextBox4.ReadOnly = true;
      this.richTextBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox4.Size = new System.Drawing.Size(480, 107);
      this.richTextBox4.TabIndex = 105;
      this.richTextBox4.Text = resources.GetString("richTextBox4.Text");
      // 
      // frmHowToUpdateTSigs
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(755, 584);
      this.Controls.Add(this.richTextBox4);
      this.Controls.Add(this.richTextBox3);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.richTextBox2);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.textBox3);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cmdShowChordMap);
      this.Controls.Add(this.label2);
      this.Name = "frmHowToUpdateTSigs";
      this.Text = "How To Update Time Signatures";
      this.Load += new System.EventHandler(this.frmHowToUpdateTSigs_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button cmdShowChordMap;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.RichTextBox richTextBox2;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.RichTextBox richTextBox3;
    private System.Windows.Forms.RichTextBox richTextBox4;
  }
}