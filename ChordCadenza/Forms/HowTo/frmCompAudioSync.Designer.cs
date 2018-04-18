namespace MPlay.Forms.HowTo {
  partial class frmCompAudioSync {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompAudioSync));
      this.textBox4 = new System.Windows.Forms.TextBox();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // textBox4
      // 
      this.textBox4.Location = new System.Drawing.Point(12, 12);
      this.textBox4.Multiline = true;
      this.textBox4.Name = "textBox4";
      this.textBox4.ReadOnly = true;
      this.textBox4.Size = new System.Drawing.Size(167, 423);
      this.textBox4.TabIndex = 69;
      this.textBox4.TabStop = false;
      this.textBox4.Text = resources.GetString("textBox4.Text");
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(176, 12);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(240, 423);
      this.textBox1.TabIndex = 70;
      this.textBox1.TabStop = false;
      this.textBox1.Text = resources.GetString("textBox1.Text");
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(412, 12);
      this.textBox2.Multiline = true;
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = true;
      this.textBox2.Size = new System.Drawing.Size(240, 423);
      this.textBox2.TabIndex = 71;
      this.textBox2.TabStop = false;
      this.textBox2.Text = resources.GetString("textBox2.Text");
      // 
      // textBox3
      // 
      this.textBox3.Location = new System.Drawing.Point(649, 12);
      this.textBox3.Multiline = true;
      this.textBox3.Name = "textBox3";
      this.textBox3.ReadOnly = true;
      this.textBox3.Size = new System.Drawing.Size(240, 423);
      this.textBox3.TabIndex = 72;
      this.textBox3.TabStop = false;
      this.textBox3.Text = resources.GetString("textBox3.Text");
      // 
      // frmCompAudioSync
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(958, 599);
      this.Controls.Add(this.textBox3);
      this.Controls.Add(this.textBox2);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.textBox4);
      this.Name = "frmCompAudioSync";
      this.Text = "Audio Sync Comparison Table";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox4;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.TextBox textBox3;
  }
}