namespace ChordCadenza.Forms {
  partial class frmPCKBIn {
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
      this.lblLastAction = new System.Windows.Forms.Label();
      this.trkVel = new System.Windows.Forms.TrackBar();
      ((System.ComponentModel.ISupportInitialize)(this.trkVel)).BeginInit();
      this.SuspendLayout();
      // 
      // lblLastAction
      // 
      this.lblLastAction.BackColor = System.Drawing.SystemColors.Control;
      this.lblLastAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblLastAction.Location = new System.Drawing.Point(9, 8);
      this.lblLastAction.Name = "lblLastAction";
      this.lblLastAction.Size = new System.Drawing.Size(14, 27);
      this.lblLastAction.TabIndex = 0;
      // 
      // trkVel
      // 
      this.trkVel.AutoSize = false;
      this.trkVel.LargeChange = 16;
      this.trkVel.Location = new System.Drawing.Point(29, 9);
      this.trkVel.Maximum = 127;
      this.trkVel.Minimum = 1;
      this.trkVel.Name = "trkVel";
      this.trkVel.Size = new System.Drawing.Size(141, 26);
      this.trkVel.SmallChange = 4;
      this.trkVel.TabIndex = 1;
      this.trkVel.TickFrequency = 16;
      this.trkVel.Value = 1;
      this.trkVel.Scroll += new System.EventHandler(this.trkVel_Scroll);
      this.trkVel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trkVel_KeyDown);
      this.trkVel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.trkVel_KeyUp);
      this.trkVel.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.trkVel_PreviewKeyDown);
      // 
      // frmPCKBIn
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(172, 42);
      this.Controls.Add(this.trkVel);
      this.Controls.Add(this.lblLastAction);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Location = new System.Drawing.Point(-200, -100);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmPCKBIn";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "PCKB Velocity";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPCKBIn_FormClosed);
      this.Load += new System.EventHandler(this.frmPCKBIn_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trkVel_KeyDown);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.trkVel_KeyUp);
      ((System.ComponentModel.ISupportInitialize)(this.trkVel)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblLastAction;
    internal System.Windows.Forms.TrackBar trkVel;
  }
}