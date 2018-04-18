namespace ChordCadenza.Forms {
  partial class dlgCHP {
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
      this.clbCHPList = new System.Windows.Forms.ListBox();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.cmdLoad = new System.Windows.Forms.Button();
      this.cmdDelete = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // clbCHPList
      // 
      this.clbCHPList.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.clbCHPList.FormattingEnabled = true;
      this.clbCHPList.HorizontalScrollbar = true;
      this.clbCHPList.ItemHeight = 15;
      this.clbCHPList.Location = new System.Drawing.Point(12, 11);
      this.clbCHPList.Name = "clbCHPList";
      this.clbCHPList.Size = new System.Drawing.Size(752, 199);
      this.clbCHPList.TabIndex = 1;
      this.clbCHPList.DoubleClick += new System.EventHandler(this.clbCHPList_DoubleClick);
      // 
      // cmdHelp
      // 
      this.cmdHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdHelp.Location = new System.Drawing.Point(12, 229);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(75, 33);
      this.cmdHelp.TabIndex = 44;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // cmdCancel
      // 
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdCancel.Location = new System.Drawing.Point(646, 229);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(118, 33);
      this.cmdCancel.TabIndex = 43;
      this.cmdCancel.Text = "Close Window";
      this.cmdCancel.UseVisualStyleBackColor = true;
      // 
      // cmdLoad
      // 
      this.cmdLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdLoad.Location = new System.Drawing.Point(522, 229);
      this.cmdLoad.Name = "cmdLoad";
      this.cmdLoad.Size = new System.Drawing.Size(118, 33);
      this.cmdLoad.TabIndex = 42;
      this.cmdLoad.Text = "???";
      this.cmdLoad.UseVisualStyleBackColor = true;
      this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
      // 
      // cmdDelete
      // 
      this.cmdDelete.Enabled = false;
      this.cmdDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdDelete.Location = new System.Drawing.Point(264, 229);
      this.cmdDelete.Name = "cmdDelete";
      this.cmdDelete.Size = new System.Drawing.Size(118, 33);
      this.cmdDelete.TabIndex = 45;
      this.cmdDelete.Text = "Delete File";
      this.cmdDelete.UseVisualStyleBackColor = true;
      this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
      // 
      // frmCHP
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(776, 274);
      this.ControlBox = false;
      this.Controls.Add(this.cmdDelete);
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.cmdLoad);
      this.Controls.Add(this.clbCHPList);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "frmCHP";
      this.Text = "???";
      this.Load += new System.EventHandler(this.frmCHP_Load);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.ListBox clbCHPList;
    private System.Windows.Forms.Button cmdHelp;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.Button cmdLoad;
    private System.Windows.Forms.Button cmdDelete;
  }
}