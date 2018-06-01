namespace ChordCadenza.Forms {
  partial class frmPCKBCfg {
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPCKBCfg));
      this.cmdApply = new System.Windows.Forms.Button();
      this.cmdOK = new System.Windows.Forms.Button();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.panMain = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.lblTopOctave = new System.Windows.Forms.Label();
      this.lblMiddleOctave = new System.Windows.Forms.Label();
      this.lblBottomOctave = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.cmdDefault24 = new System.Windows.Forms.Button();
      this.cmdDefault44 = new System.Windows.Forms.Button();
      this.cmdClear = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.panMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmdApply
      // 
      this.cmdApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdApply.Location = new System.Drawing.Point(653, 400);
      this.cmdApply.Name = "cmdApply";
      this.cmdApply.Size = new System.Drawing.Size(67, 46);
      this.cmdApply.TabIndex = 8;
      this.cmdApply.TabStop = false;
      this.cmdApply.Text = "Apply";
      this.cmdApply.UseVisualStyleBackColor = true;
      this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
      // 
      // cmdOK
      // 
      this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdOK.Location = new System.Drawing.Point(726, 400);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(67, 46);
      this.cmdOK.TabIndex = 9;
      this.cmdOK.TabStop = false;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
      // 
      // cmdCancel
      // 
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdCancel.Location = new System.Drawing.Point(799, 400);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(67, 46);
      this.cmdCancel.TabIndex = 10;
      this.cmdCancel.TabStop = false;
      this.cmdCancel.Text = "Close";
      this.cmdCancel.UseVisualStyleBackColor = true;
      this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
      // 
      // panMain
      // 
      this.panMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panMain.Controls.Add(this.label2);
      this.panMain.Controls.Add(this.lblTopOctave);
      this.panMain.Controls.Add(this.lblMiddleOctave);
      this.panMain.Controls.Add(this.lblBottomOctave);
      this.panMain.Location = new System.Drawing.Point(10, 98);
      this.panMain.Name = "panMain";
      this.panMain.Size = new System.Drawing.Size(857, 291);
      this.panMain.TabIndex = 11;
      this.panMain.TabStop = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(615, 15);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(89, 16);
      this.label2.TabIndex = 15;
      this.label2.Text = "Top Octave";
      // 
      // lblTopOctave
      // 
      this.lblTopOctave.AutoSize = true;
      this.lblTopOctave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTopOctave.Location = new System.Drawing.Point(415, 15);
      this.lblTopOctave.Name = "lblTopOctave";
      this.lblTopOctave.Size = new System.Drawing.Size(87, 16);
      this.lblTopOctave.TabIndex = 14;
      this.lblTopOctave.Text = "3rd. Octave";
      // 
      // lblMiddleOctave
      // 
      this.lblMiddleOctave.AutoSize = true;
      this.lblMiddleOctave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMiddleOctave.Location = new System.Drawing.Point(215, 15);
      this.lblMiddleOctave.Name = "lblMiddleOctave";
      this.lblMiddleOctave.Size = new System.Drawing.Size(90, 16);
      this.lblMiddleOctave.TabIndex = 13;
      this.lblMiddleOctave.Text = "2nd. Octave";
      // 
      // lblBottomOctave
      // 
      this.lblBottomOctave.AutoSize = true;
      this.lblBottomOctave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblBottomOctave.Location = new System.Drawing.Point(15, 15);
      this.lblBottomOctave.Name = "lblBottomOctave";
      this.lblBottomOctave.Size = new System.Drawing.Size(109, 16);
      this.lblBottomOctave.TabIndex = 12;
      this.lblBottomOctave.Text = "Bottom Octave";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(28, 20);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(299, 65);
      this.label1.TabIndex = 12;
      this.label1.Text = resources.GetString("label1.Text");
      // 
      // cmdDefault24
      // 
      this.cmdDefault24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdDefault24.Location = new System.Drawing.Point(139, 400);
      this.cmdDefault24.Name = "cmdDefault24";
      this.cmdDefault24.Size = new System.Drawing.Size(121, 46);
      this.cmdDefault24.TabIndex = 13;
      this.cmdDefault24.TabStop = false;
      this.cmdDefault24.Text = "Set to default \r\n2 octaves, 4 rows\r\n";
      this.cmdDefault24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.toolTip1.SetToolTip(this.cmdDefault24, "This uses alphanumeric characters only.\r\nBlack and white notes are on the differe" +
        "nt rows.\r\nIt should work on most US/UK keyboards. ");
      this.cmdDefault24.UseVisualStyleBackColor = true;
      this.cmdDefault24.Click += new System.EventHandler(this.cmdDefault24_Click);
      // 
      // cmdDefault44
      // 
      this.cmdDefault44.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdDefault44.Location = new System.Drawing.Point(12, 400);
      this.cmdDefault44.Name = "cmdDefault44";
      this.cmdDefault44.Size = new System.Drawing.Size(121, 46);
      this.cmdDefault44.TabIndex = 14;
      this.cmdDefault44.TabStop = false;
      this.cmdDefault44.Text = "Set to default \r\n4 octaves, 4 rows";
      this.cmdDefault44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.toolTip1.SetToolTip(this.cmdDefault44, "This uses alphanumeric and special characters.\r\nBlack and white notes are present" +
        " on the same row.\r\nIt may need updating on some keyboards ");
      this.cmdDefault44.UseVisualStyleBackColor = true;
      this.cmdDefault44.Click += new System.EventHandler(this.cmdDefault44_Click);
      // 
      // cmdClear
      // 
      this.cmdClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdClear.Location = new System.Drawing.Point(266, 400);
      this.cmdClear.Name = "cmdClear";
      this.cmdClear.Size = new System.Drawing.Size(67, 46);
      this.cmdClear.TabIndex = 15;
      this.cmdClear.TabStop = false;
      this.cmdClear.Text = "Clear";
      this.cmdClear.UseVisualStyleBackColor = true;
      this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
      // 
      // frmPCKBCfg
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(877, 456);
      this.ControlBox = false;
      this.Controls.Add(this.cmdClear);
      this.Controls.Add(this.cmdDefault44);
      this.Controls.Add(this.cmdDefault24);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.panMain);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.cmdOK);
      this.Controls.Add(this.cmdApply);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmPCKBCfg";
      this.Text = "PC Keyboard Mapping - Chord Cadenza";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPCKBCfg_FormClosed);
      this.Load += new System.EventHandler(this.frmPCKBCfg_Load);
      this.panMain.ResumeLayout(false);
      this.panMain.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button cmdApply;
    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.Panel panMain;
    private System.Windows.Forms.Label lblTopOctave;
    private System.Windows.Forms.Label lblMiddleOctave;
    private System.Windows.Forms.Label lblBottomOctave;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button cmdDefault24;
    private System.Windows.Forms.Button cmdDefault44;
    private System.Windows.Forms.Button cmdClear;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ToolTip toolTip1;
  }
}