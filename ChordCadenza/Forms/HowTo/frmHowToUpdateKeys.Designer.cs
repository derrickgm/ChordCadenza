namespace MPlay.Forms.HowTo {
  partial class frmHowToUpdateKeys {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHowToUpdateKeys));
      this.label3 = new System.Windows.Forms.Label();
      this.cmdLoadSong = new System.Windows.Forms.Button();
      this.cmdShowChordMap = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.richTextBox2 = new System.Windows.Forms.RichTextBox();
      this.richTextBox3 = new System.Windows.Forms.RichTextBox();
      this.richTextBox4 = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(12, 16);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(75, 13);
      this.label3.TabIndex = 63;
      this.label3.Text = "Introduction";
      // 
      // cmdLoadSong
      // 
      this.cmdLoadSong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdLoadSong.Location = new System.Drawing.Point(517, 39);
      this.cmdLoadSong.Name = "cmdLoadSong";
      this.cmdLoadSong.Size = new System.Drawing.Size(220, 30);
      this.cmdLoadSong.TabIndex = 64;
      this.cmdLoadSong.Text = "Load Song";
      this.cmdLoadSong.UseVisualStyleBackColor = true;
      this.cmdLoadSong.Click += new System.EventHandler(this.cmdLoadSong_Click);
      // 
      // cmdShowChordMap
      // 
      this.cmdShowChordMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdShowChordMap.Location = new System.Drawing.Point(517, 197);
      this.cmdShowChordMap.Name = "cmdShowChordMap";
      this.cmdShowChordMap.Size = new System.Drawing.Size(220, 30);
      this.cmdShowChordMap.TabIndex = 66;
      this.cmdShowChordMap.Text = "Show ChordMap";
      this.cmdShowChordMap.UseVisualStyleBackColor = true;
      this.cmdShowChordMap.Click += new System.EventHandler(this.cmdShowChordMap_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 172);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(166, 13);
      this.label2.TabIndex = 65;
      this.label2.Text = "Open the ChordMap window";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 275);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(79, 13);
      this.label1.TabIndex = 67;
      this.label1.Text = "Update Keys";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(12, 395);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(67, 13);
      this.label4.TabIndex = 69;
      this.label4.Text = "Save Keys";
      // 
      // richTextBox1
      // 
      this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Location = new System.Drawing.Point(30, 39);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox1.Size = new System.Drawing.Size(480, 97);
      this.richTextBox1.TabIndex = 100;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // richTextBox2
      // 
      this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox2.Location = new System.Drawing.Point(30, 206);
      this.richTextBox2.Name = "richTextBox2";
      this.richTextBox2.ReadOnly = true;
      this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox2.Size = new System.Drawing.Size(480, 42);
      this.richTextBox2.TabIndex = 101;
      this.richTextBox2.Text = "If you haven\'t loaded a Song containing a ChordFile, a window should be displayed" +
    " to create a new ChordFile with a maximum number of bars. Click^OK$to proceed.";
      // 
      // richTextBox3
      // 
      this.richTextBox3.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox3.Location = new System.Drawing.Point(30, 307);
      this.richTextBox3.Name = "richTextBox3";
      this.richTextBox3.ReadOnly = true;
      this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox3.Size = new System.Drawing.Size(480, 85);
      this.richTextBox3.TabIndex = 102;
      this.richTextBox3.Text = resources.GetString("richTextBox3.Text");
      // 
      // richTextBox4
      // 
      this.richTextBox4.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox4.Location = new System.Drawing.Point(30, 424);
      this.richTextBox4.Name = "richTextBox4";
      this.richTextBox4.ReadOnly = true;
      this.richTextBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox4.Size = new System.Drawing.Size(480, 41);
      this.richTextBox4.TabIndex = 103;
      this.richTextBox4.Text = "Keys will be saved when the ChordFile is saved using the^Save$or^Save As$button i" +
    "n the^ChordMap.$";
      // 
      // frmHowToUpdateKeys
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(749, 472);
      this.Controls.Add(this.richTextBox4);
      this.Controls.Add(this.richTextBox3);
      this.Controls.Add(this.richTextBox2);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cmdShowChordMap);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cmdLoadSong);
      this.Controls.Add(this.label3);
      this.Name = "frmHowToUpdateKeys";
      this.Text = "How To Update Keys and Modulate";
      this.Load += new System.EventHandler(this.frmHowToUpdateKeys_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button cmdLoadSong;
    private System.Windows.Forms.Button cmdShowChordMap;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.RichTextBox richTextBox2;
    private System.Windows.Forms.RichTextBox richTextBox3;
    private System.Windows.Forms.RichTextBox richTextBox4;
  }
}