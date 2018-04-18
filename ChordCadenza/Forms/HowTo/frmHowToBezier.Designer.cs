namespace MPlay.Forms.HowTo {
  partial class frmHowToBezier {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHowToBezier));
      this.label3 = new System.Windows.Forms.Label();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.pictureBox3 = new System.Windows.Forms.PictureBox();
      this.label4 = new System.Windows.Forms.Label();
      this.cmdShowChordMap = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.richTextBox2 = new System.Windows.Forms.RichTextBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
      this.SuspendLayout();
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(12, 18);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(75, 13);
      this.label3.TabIndex = 65;
      this.label3.Text = "Introduction";
      // 
      // textBox1
      // 
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Location = new System.Drawing.Point(30, 41);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(733, 60);
      this.textBox1.TabIndex = 64;
      this.textBox1.TabStop = false;
      this.textBox1.Text = resources.GetString("textBox1.Text");
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 123);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(177, 26);
      this.label1.TabIndex = 68;
      this.label1.Text = "Default Velocity Profile\r\nIn (Keyboard) = Out (Midi Out)";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(335, 123);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(154, 26);
      this.label2.TabIndex = 69;
      this.label2.Text = "Velocity Profile with more \r\nsensitivity in centre";
      // 
      // pictureBox2
      // 
      this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
      this.pictureBox2.Location = new System.Drawing.Point(335, 158);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(300, 287);
      this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox2.TabIndex = 71;
      this.pictureBox2.TabStop = false;
      // 
      // pictureBox3
      // 
      this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
      this.pictureBox3.Location = new System.Drawing.Point(12, 158);
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.Size = new System.Drawing.Size(300, 287);
      this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox3.TabIndex = 70;
      this.pictureBox3.TabStop = false;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(12, 472);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(189, 13);
      this.label4.TabIndex = 73;
      this.label4.Text = "Drawing a Velocity Bezier Curve";
      // 
      // cmdShowChordMap
      // 
      this.cmdShowChordMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdShowChordMap.Location = new System.Drawing.Point(546, 495);
      this.cmdShowChordMap.Name = "cmdShowChordMap";
      this.cmdShowChordMap.Size = new System.Drawing.Size(220, 30);
      this.cmdShowChordMap.TabIndex = 74;
      this.cmdShowChordMap.Text = "Show PlayMap";
      this.cmdShowChordMap.UseVisualStyleBackColor = true;
      this.cmdShowChordMap.Click += new System.EventHandler(this.cmdShowChordMap_Click);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(12, 657);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(210, 13);
      this.label5.TabIndex = 76;
      this.label5.Text = "Drawing an Aftertouch Bezier Curve";
      // 
      // richTextBox1
      // 
      this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Location = new System.Drawing.Point(30, 495);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox1.Size = new System.Drawing.Size(508, 133);
      this.richTextBox1.TabIndex = 93;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // richTextBox2
      // 
      this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
      this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox2.Location = new System.Drawing.Point(27, 682);
      this.richTextBox2.Name = "richTextBox2";
      this.richTextBox2.ReadOnly = true;
      this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.richTextBox2.Size = new System.Drawing.Size(508, 40);
      this.richTextBox2.TabIndex = 94;
      this.richTextBox2.Text = "If your keyboard has Aftertouch, you can create an Aftertouh profile using^PlayMa" +
    "p Menu > Configure > Bezier > Aftertouch,$using the same method.";
      // 
      // frmHowToBezier
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(778, 745);
      this.Controls.Add(this.richTextBox2);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.cmdShowChordMap);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.pictureBox2);
      this.Controls.Add(this.pictureBox3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.textBox1);
      this.Name = "frmHowToBezier";
      this.Text = "How To Create a Velocity Profile using a Bezier Curve";
      this.Load += new System.EventHandler(this.frmHowToBezier_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.PictureBox pictureBox3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button cmdShowChordMap;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.RichTextBox richTextBox2;
  }
}