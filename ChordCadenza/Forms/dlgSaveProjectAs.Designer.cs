namespace ChordCadenza.Forms {
  partial class dlgSaveProjectAs {
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
      this.cmdCancel = new System.Windows.Forms.Button();
      this.cmdOK = new System.Windows.Forms.Button();
      this.lblProjectNameMsg = new System.Windows.Forms.Label();
      this.txtProjectName = new System.Windows.Forms.TextBox();
      this.lblProjectName = new System.Windows.Forms.Label();
      this.txtProjectLocation = new System.Windows.Forms.TextBox();
      this.lblProjectLocation = new System.Windows.Forms.Label();
      this.cmdProjectLocation = new System.Windows.Forms.Button();
      this.chkAddNameToLocation = new System.Windows.Forms.CheckBox();
      this.fbd = new System.Windows.Forms.FolderBrowserDialog();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.ofd = new System.Windows.Forms.OpenFileDialog();
      this.SuspendLayout();
      // 
      // cmdCancel
      // 
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdCancel.Location = new System.Drawing.Point(506, 192);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(75, 33);
      this.cmdCancel.TabIndex = 49;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      // 
      // cmdOK
      // 
      this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdOK.Location = new System.Drawing.Point(423, 192);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(75, 33);
      this.cmdOK.TabIndex = 48;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
      // 
      // lblProjectNameMsg
      // 
      this.lblProjectNameMsg.AutoSize = true;
      this.lblProjectNameMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblProjectNameMsg.Location = new System.Drawing.Point(120, 119);
      this.lblProjectNameMsg.Name = "lblProjectNameMsg";
      this.lblProjectNameMsg.Size = new System.Drawing.Size(134, 16);
      this.lblProjectNameMsg.TabIndex = 47;
      this.lblProjectNameMsg.Text = "(FileNames Qualifier)";
      // 
      // txtProjectName
      // 
      this.txtProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtProjectName.Location = new System.Drawing.Point(15, 138);
      this.txtProjectName.Name = "txtProjectName";
      this.txtProjectName.Size = new System.Drawing.Size(528, 22);
      this.txtProjectName.TabIndex = 46;
      // 
      // lblProjectName
      // 
      this.lblProjectName.AutoSize = true;
      this.lblProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblProjectName.Location = new System.Drawing.Point(12, 119);
      this.lblProjectName.Name = "lblProjectName";
      this.lblProjectName.Size = new System.Drawing.Size(102, 16);
      this.lblProjectName.TabIndex = 45;
      this.lblProjectName.Text = "Project Name";
      // 
      // txtProjectLocation
      // 
      this.txtProjectLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtProjectLocation.Location = new System.Drawing.Point(15, 54);
      this.txtProjectLocation.Name = "txtProjectLocation";
      this.txtProjectLocation.Size = new System.Drawing.Size(528, 22);
      this.txtProjectLocation.TabIndex = 43;
      // 
      // lblProjectLocation
      // 
      this.lblProjectLocation.AutoSize = true;
      this.lblProjectLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblProjectLocation.Location = new System.Drawing.Point(12, 35);
      this.lblProjectLocation.Name = "lblProjectLocation";
      this.lblProjectLocation.Size = new System.Drawing.Size(120, 16);
      this.lblProjectLocation.TabIndex = 42;
      this.lblProjectLocation.Text = "Project Location";
      // 
      // cmdProjectLocation
      // 
      this.cmdProjectLocation.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdProjectLocation.Location = new System.Drawing.Point(549, 53);
      this.cmdProjectLocation.Name = "cmdProjectLocation";
      this.cmdProjectLocation.Size = new System.Drawing.Size(30, 21);
      this.cmdProjectLocation.TabIndex = 41;
      this.cmdProjectLocation.Text = "...";
      this.cmdProjectLocation.UseVisualStyleBackColor = true;
      this.cmdProjectLocation.Click += new System.EventHandler(this.cmdProjectLocation_Click);
      // 
      // chkAddNameToLocation
      // 
      this.chkAddNameToLocation.AutoSize = true;
      this.chkAddNameToLocation.Checked = true;
      this.chkAddNameToLocation.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkAddNameToLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chkAddNameToLocation.Location = new System.Drawing.Point(15, 76);
      this.chkAddNameToLocation.Name = "chkAddNameToLocation";
      this.chkAddNameToLocation.Size = new System.Drawing.Size(239, 20);
      this.chkAddNameToLocation.TabIndex = 50;
      this.chkAddNameToLocation.Text = "Add Name Subdirectory to Location";
      this.chkAddNameToLocation.UseVisualStyleBackColor = true;
      // 
      // cmdHelp
      // 
      this.cmdHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdHelp.Location = new System.Drawing.Point(15, 192);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(75, 33);
      this.cmdHelp.TabIndex = 53;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // dlgSaveProjectAs
      // 
      this.AcceptButton = this.cmdOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(607, 239);
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.chkAddNameToLocation);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.cmdOK);
      this.Controls.Add(this.lblProjectNameMsg);
      this.Controls.Add(this.txtProjectName);
      this.Controls.Add(this.lblProjectName);
      this.Controls.Add(this.txtProjectLocation);
      this.Controls.Add(this.lblProjectLocation);
      this.Controls.Add(this.cmdProjectLocation);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "dlgSaveProjectAs";
      this.Text = "Save Project As - Chord Cadenza";
      this.Load += new System.EventHandler(this.frmSaveAs_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.Label lblProjectNameMsg;
    private System.Windows.Forms.TextBox txtProjectName;
    private System.Windows.Forms.Label lblProjectName;
    private System.Windows.Forms.TextBox txtProjectLocation;
    private System.Windows.Forms.Label lblProjectLocation;
    private System.Windows.Forms.Button cmdProjectLocation;
    private System.Windows.Forms.CheckBox chkAddNameToLocation;
    private System.Windows.Forms.FolderBrowserDialog fbd;
    private System.Windows.Forms.Button cmdHelp;
    private System.Windows.Forms.OpenFileDialog ofd;
  }
}