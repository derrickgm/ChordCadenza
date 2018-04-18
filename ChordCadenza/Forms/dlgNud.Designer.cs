namespace ChordCadenza.Forms {
  partial class dlgNud {
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
      this.lblPrompt = new System.Windows.Forms.Label();
      this.cmdOK = new System.Windows.Forms.Button();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.nud1 = new System.Windows.Forms.NumericUpDown();
      this.lblMsg = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.nud1)).BeginInit();
      this.SuspendLayout();
      // 
      // lblPrompt
      // 
      this.lblPrompt.AutoSize = true;
      this.lblPrompt.Location = new System.Drawing.Point(24, 21);
      this.lblPrompt.Name = "lblPrompt";
      this.lblPrompt.Size = new System.Drawing.Size(25, 13);
      this.lblPrompt.TabIndex = 0;
      this.lblPrompt.Text = "???";
      // 
      // cmdOK
      // 
      this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdOK.Location = new System.Drawing.Point(452, 172);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(62, 32);
      this.cmdOK.TabIndex = 1;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
      // 
      // cmdCancel
      // 
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Location = new System.Drawing.Point(520, 172);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(62, 32);
      this.cmdCancel.TabIndex = 2;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
      // 
      // nud1
      // 
      this.nud1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud1.Location = new System.Drawing.Point(27, 57);
      this.nud1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.nud1.Name = "nud1";
      this.nud1.Size = new System.Drawing.Size(52, 22);
      this.nud1.TabIndex = 4;
      this.nud1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.nud1.Validating += new System.ComponentModel.CancelEventHandler(this.nud1_Validating);
      // 
      // lblMsg
      // 
      this.lblMsg.AutoSize = true;
      this.lblMsg.Location = new System.Drawing.Point(24, 106);
      this.lblMsg.Name = "lblMsg";
      this.lblMsg.Size = new System.Drawing.Size(25, 13);
      this.lblMsg.TabIndex = 5;
      this.lblMsg.Text = "???";
      // 
      // dlgNud
      // 
      this.AcceptButton = this.cmdOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(588, 216);
      this.ControlBox = false;
      this.Controls.Add(this.lblMsg);
      this.Controls.Add(this.nud1);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.cmdOK);
      this.Controls.Add(this.lblPrompt);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "dlgNud";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Numeric Input - Chord Cadenza";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.frmDialogNud_Load);
      ((System.ComponentModel.ISupportInitialize)(this.nud1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button cmdOK;
    internal System.Windows.Forms.Label lblPrompt;
    internal System.Windows.Forms.NumericUpDown nud1;
    internal System.Windows.Forms.Button cmdCancel;
    internal System.Windows.Forms.Label lblMsg;
  }
}