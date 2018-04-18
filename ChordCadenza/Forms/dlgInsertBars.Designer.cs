namespace ChordCadenza.Forms {
  partial class dlgInsertBars {
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
      this.lblMsg = new System.Windows.Forms.Label();
      this.cmbTSigs = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmdOK = new System.Windows.Forms.Button();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lblMsg
      // 
      this.lblMsg.AutoSize = true;
      this.lblMsg.Location = new System.Drawing.Point(12, 18);
      this.lblMsg.Name = "lblMsg";
      this.lblMsg.Size = new System.Drawing.Size(134, 13);
      this.lblMsg.TabIndex = 0;
      this.lblMsg.Text = "Insert ??? bar(s) at bar ???";
      // 
      // cmbTSigs
      // 
      this.cmbTSigs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbTSigs.FormattingEnabled = true;
      this.cmbTSigs.Location = new System.Drawing.Point(27, 63);
      this.cmbTSigs.Name = "cmbTSigs";
      this.cmbTSigs.Size = new System.Drawing.Size(70, 21);
      this.cmbTSigs.TabIndex = 1;
      this.cmbTSigs.SelectedIndexChanged += new System.EventHandler(this.cmbTSigs_SelectedIndexChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 47);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(106, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Enter Time Signature";
      // 
      // cmdOK
      // 
      this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdOK.Location = new System.Drawing.Point(123, 134);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(50, 50);
      this.cmdOK.TabIndex = 3;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      // 
      // cmdCancel
      // 
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Location = new System.Drawing.Point(177, 134);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(50, 50);
      this.cmdCancel.TabIndex = 4;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      // 
      // frmInsertBars
      // 
      this.AcceptButton = this.cmdOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(233, 191);
      this.ControlBox = false;
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.cmdOK);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cmbTSigs);
      this.Controls.Add(this.lblMsg);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmInsertBars";
      this.Text = "Insert Bars";
      this.Load += new System.EventHandler(this.frmInsertBars_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblMsg;
    private System.Windows.Forms.ComboBox cmbTSigs;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.Button cmdCancel;
  }
}