namespace ChordCadenza.Forms {
  partial class frmShowList {
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
      this.txtList = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // txtList
      // 
      this.txtList.BackColor = System.Drawing.SystemColors.Window;
      this.txtList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtList.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtList.Location = new System.Drawing.Point(0, 0);
      this.txtList.Multiline = true;
      this.txtList.Name = "txtList";
      this.txtList.ReadOnly = true;
      this.txtList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtList.Size = new System.Drawing.Size(714, 265);
      this.txtList.TabIndex = 0;
      // 
      // frmShowList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(714, 265);
      this.Controls.Add(this.txtList);
      this.Name = "frmShowList";
      this.Text = "Show List - Chord Cadenza";
      this.Load += new System.EventHandler(this.frmShowList_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtList;
  }
}