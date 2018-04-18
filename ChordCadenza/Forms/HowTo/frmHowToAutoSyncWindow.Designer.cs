namespace MPlay.Forms.HowTo {
  partial class frmHowToAutoSyncWindow {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHowToAutoSyncWindow));
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.textBox5 = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Location = new System.Drawing.Point(30, 50);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(582, 132);
      this.textBox1.TabIndex = 61;
      this.textBox1.TabStop = false;
      this.textBox1.Text = resources.GetString("textBox1.Text");
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(12, 24);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(75, 13);
      this.label5.TabIndex = 60;
      this.label5.Text = "Introduction";
      // 
      // textBox5
      // 
      this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox5.Location = new System.Drawing.Point(30, 222);
      this.textBox5.Multiline = true;
      this.textBox5.Name = "textBox5";
      this.textBox5.ReadOnly = true;
      this.textBox5.Size = new System.Drawing.Size(492, 215);
      this.textBox5.TabIndex = 67;
      this.textBox5.TabStop = false;
      this.textBox5.Text = resources.GetString("textBox5.Text");
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(12, 196);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(157, 13);
      this.label4.TabIndex = 66;
      this.label4.Text = "Managing AudioSync Data";
      // 
      // frmHowToAutoSyncWindow
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(613, 601);
      this.Controls.Add(this.textBox5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.label5);
      this.Name = "frmHowToAutoSyncWindow";
      this.Text = "How To Use the AudioSync Window";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox textBox5;
    private System.Windows.Forms.Label label4;
  }
}