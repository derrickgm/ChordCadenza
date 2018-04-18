namespace MPlay.Forms.HowTo {
  partial class frmHowToInsertChords {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHowToInsertChords));
      this.cmdLoadSong = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.cmdShowChordMap = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.richTextBox2 = new System.Windows.Forms.RichTextBox();
      this.richTextBox3 = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // cmdLoadSong
      // 
      this.cmdLoadSong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdLoadSong.Location = new System.Drawing.Point(547, 38);
      this.cmdLoadSong.Name = "cmdLoadSong";
      this.cmdLoadSong.Size = new System.Drawing.Size(220, 30);
      this.cmdLoadSong.TabIndex = 52;
      this.cmdLoadSong.Text = "Load Song";
      this.cmdLoadSong.UseVisualStyleBackColor = true;
      this.cmdLoadSong.Click += new System.EventHandler(this.cmdLoadSong_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(9, 324);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(73, 13);
      this.label1.TabIndex = 56;
      this.label1.Text = "Set the Key";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 145);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(166, 13);
      this.label2.TabIndex = 57;
      this.label2.Text = "Open the ChordMap window";
      // 
      // cmdShowChordMap
      // 
      this.cmdShowChordMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdShowChordMap.Location = new System.Drawing.Point(545, 172);
      this.cmdShowChordMap.Name = "cmdShowChordMap";
      this.cmdShowChordMap.Size = new System.Drawing.Size(220, 30);
      this.cmdShowChordMap.TabIndex = 59;
      this.cmdShowChordMap.Text = "Show ChordMap";
      this.cmdShowChordMap.UseVisualStyleBackColor = true;
      this.cmdShowChordMap.Click += new System.EventHandler(this.cmdShowChordMap_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(12, 15);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(75, 13);
      this.label3.TabIndex = 61;
      this.label3.Text = "Introduction";
      // 
      // textBox1
      // 
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Location = new System.Drawing.Point(30, 38);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(500, 80);
      this.textBox1.TabIndex = 60;
      this.textBox1.TabStop = false;
      this.textBox1.Text = resources.GetString("textBox1.Text");
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(9, 558);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(114, 13);
      this.label4.TabIndex = 63;
      this.label4.Text = "Type in the Chords";
      // 
      // textBox3
      // 
      this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox3.Location = new System.Drawing.Point(27, 585);
      this.textBox3.Multiline = true;
      this.textBox3.Name = "textBox3";
      this.textBox3.ReadOnly = true;
      this.textBox3.Size = new System.Drawing.Size(738, 62);
      this.textBox3.TabIndex = 62;
      this.textBox3.TabStop = false;
      this.textBox3.Text = resources.GetString("textBox3.Text");
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(9, 665);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(101, 13);
      this.label6.TabIndex = 65;
      this.label6.Text = "Save the Chords";
      // 
      // richTextBox1
      // 
      this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Location = new System.Drawing.Point(27, 172);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox1.Size = new System.Drawing.Size(498, 126);
      this.richTextBox1.TabIndex = 94;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // richTextBox2
      // 
      this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox2.Location = new System.Drawing.Point(27, 353);
      this.richTextBox2.Name = "richTextBox2";
      this.richTextBox2.ReadOnly = true;
      this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox2.Size = new System.Drawing.Size(713, 193);
      this.richTextBox2.TabIndex = 95;
      this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
      // 
      // richTextBox3
      // 
      this.richTextBox3.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox3.Location = new System.Drawing.Point(27, 695);
      this.richTextBox3.Name = "richTextBox3";
      this.richTextBox3.ReadOnly = true;
      this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox3.Size = new System.Drawing.Size(713, 71);
      this.richTextBox3.TabIndex = 97;
      this.richTextBox3.Text = resources.GetString("richTextBox3.Text");
      // 
      // frmHowToInsertChords
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(811, 625);
      this.Controls.Add(this.richTextBox3);
      this.Controls.Add(this.richTextBox2);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.textBox3);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.cmdShowChordMap);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cmdLoadSong);
      this.Name = "frmHowToInsertChords";
      this.Text = "How To Insert Keys and Chords from an External Source";
      this.Load += new System.EventHandler(this.frmHowToInsertChords_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdLoadSong;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button cmdShowChordMap;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.RichTextBox richTextBox2;
    private System.Windows.Forms.RichTextBox richTextBox3;
  }
}