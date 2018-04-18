namespace ChordCadenza.Forms {
  partial class frmConsole {
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
      this.txt1 = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // txt1
      // 
      this.txt1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txt1.Location = new System.Drawing.Point(0, 0);
      this.txt1.Multiline = true;
      this.txt1.Name = "txt1";
      this.txt1.ReadOnly = true;
      this.txt1.Size = new System.Drawing.Size(709, 448);
      this.txt1.TabIndex = 0;
      // 
      // frmConsole
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(709, 448);
      this.Controls.Add(this.txt1);
      this.Name = "frmConsole";
      this.Text = "frmConsole";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txt1;
  }
}