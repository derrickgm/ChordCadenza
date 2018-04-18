namespace ChordCadenza.Forms {
  partial class dlgNewProject {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgNewProject));
      this.txtAudioFilePath = new System.Windows.Forms.TextBox();
      this.cmdAudioPath = new System.Windows.Forms.Button();
      this.txtProjectLocation = new System.Windows.Forms.TextBox();
      this.txtMidiFilePath = new System.Windows.Forms.TextBox();
      this.lblProjectLocation = new System.Windows.Forms.Label();
      this.cmdProjectLocation = new System.Windows.Forms.Button();
      this.cmdMidiPath = new System.Windows.Forms.Button();
      this.lblCopyMidiFile = new System.Windows.Forms.Label();
      this.lblCopyAudioFile = new System.Windows.Forms.Label();
      this.lblCopyMidiFileMsg = new System.Windows.Forms.Label();
      this.lblCopyAudioFileMsg = new System.Windows.Forms.Label();
      this.lblProjectNameMsg = new System.Windows.Forms.Label();
      this.txtProjectName = new System.Windows.Forms.TextBox();
      this.lblProjectName = new System.Windows.Forms.Label();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.cmdOK = new System.Windows.Forms.Button();
      this.ofd = new System.Windows.Forms.OpenFileDialog();
      this.fbd = new System.Windows.Forms.FolderBrowserDialog();
      this.cmdUseMidi = new System.Windows.Forms.Button();
      this.cmdUseAudio = new System.Windows.Forms.Button();
      this.chkAddNameToLocation = new System.Windows.Forms.CheckBox();
      this.lblIntro = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // txtAudioFilePath
      // 
      this.txtAudioFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtAudioFilePath.Location = new System.Drawing.Point(15, 182);
      this.txtAudioFilePath.Name = "txtAudioFilePath";
      this.txtAudioFilePath.Size = new System.Drawing.Size(528, 22);
      this.txtAudioFilePath.TabIndex = 30;
      // 
      // cmdAudioPath
      // 
      this.cmdAudioPath.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdAudioPath.Location = new System.Drawing.Point(549, 181);
      this.cmdAudioPath.Name = "cmdAudioPath";
      this.cmdAudioPath.Size = new System.Drawing.Size(30, 21);
      this.cmdAudioPath.TabIndex = 29;
      this.cmdAudioPath.Text = "...";
      this.cmdAudioPath.UseVisualStyleBackColor = true;
      this.cmdAudioPath.Click += new System.EventHandler(this.cmdAudioPath_Click);
      // 
      // txtProjectLocation
      // 
      this.txtProjectLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtProjectLocation.Location = new System.Drawing.Point(15, 249);
      this.txtProjectLocation.Name = "txtProjectLocation";
      this.txtProjectLocation.Size = new System.Drawing.Size(528, 22);
      this.txtProjectLocation.TabIndex = 27;
      // 
      // txtMidiFilePath
      // 
      this.txtMidiFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtMidiFilePath.Location = new System.Drawing.Point(15, 117);
      this.txtMidiFilePath.Name = "txtMidiFilePath";
      this.txtMidiFilePath.Size = new System.Drawing.Size(528, 22);
      this.txtMidiFilePath.TabIndex = 26;
      // 
      // lblProjectLocation
      // 
      this.lblProjectLocation.AutoSize = true;
      this.lblProjectLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblProjectLocation.Location = new System.Drawing.Point(12, 230);
      this.lblProjectLocation.Name = "lblProjectLocation";
      this.lblProjectLocation.Size = new System.Drawing.Size(120, 16);
      this.lblProjectLocation.TabIndex = 25;
      this.lblProjectLocation.Text = "Project Location";
      // 
      // cmdProjectLocation
      // 
      this.cmdProjectLocation.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdProjectLocation.Location = new System.Drawing.Point(549, 248);
      this.cmdProjectLocation.Name = "cmdProjectLocation";
      this.cmdProjectLocation.Size = new System.Drawing.Size(30, 21);
      this.cmdProjectLocation.TabIndex = 24;
      this.cmdProjectLocation.Text = "...";
      this.cmdProjectLocation.UseVisualStyleBackColor = true;
      this.cmdProjectLocation.Click += new System.EventHandler(this.cmdProjectLocation_Click);
      // 
      // cmdMidiPath
      // 
      this.cmdMidiPath.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdMidiPath.Location = new System.Drawing.Point(549, 116);
      this.cmdMidiPath.Name = "cmdMidiPath";
      this.cmdMidiPath.Size = new System.Drawing.Size(30, 21);
      this.cmdMidiPath.TabIndex = 23;
      this.cmdMidiPath.Text = "...";
      this.cmdMidiPath.UseVisualStyleBackColor = true;
      this.cmdMidiPath.Click += new System.EventHandler(this.cmdMidiPath_Click);
      // 
      // lblCopyMidiFile
      // 
      this.lblCopyMidiFile.AutoSize = true;
      this.lblCopyMidiFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCopyMidiFile.Location = new System.Drawing.Point(12, 98);
      this.lblCopyMidiFile.Name = "lblCopyMidiFile";
      this.lblCopyMidiFile.Size = new System.Drawing.Size(142, 16);
      this.lblCopyMidiFile.TabIndex = 32;
      this.lblCopyMidiFile.Text = "Copy MidiFile From";
      // 
      // lblCopyAudioFile
      // 
      this.lblCopyAudioFile.AutoSize = true;
      this.lblCopyAudioFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCopyAudioFile.Location = new System.Drawing.Point(12, 163);
      this.lblCopyAudioFile.Name = "lblCopyAudioFile";
      this.lblCopyAudioFile.Size = new System.Drawing.Size(134, 16);
      this.lblCopyAudioFile.TabIndex = 33;
      this.lblCopyAudioFile.Text = "Copy AudioFile From";
      // 
      // lblCopyMidiFileMsg
      // 
      this.lblCopyMidiFileMsg.AutoSize = true;
      this.lblCopyMidiFileMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCopyMidiFileMsg.Location = new System.Drawing.Point(160, 98);
      this.lblCopyMidiFileMsg.Name = "lblCopyMidiFileMsg";
      this.lblCopyMidiFileMsg.Size = new System.Drawing.Size(133, 16);
      this.lblCopyMidiFileMsg.TabIndex = 34;
      this.lblCopyMidiFileMsg.Text = "(No Copy if Blank)";
      // 
      // lblCopyAudioFileMsg
      // 
      this.lblCopyAudioFileMsg.AutoSize = true;
      this.lblCopyAudioFileMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCopyAudioFileMsg.Location = new System.Drawing.Point(150, 163);
      this.lblCopyAudioFileMsg.Name = "lblCopyAudioFileMsg";
      this.lblCopyAudioFileMsg.Size = new System.Drawing.Size(115, 16);
      this.lblCopyAudioFileMsg.TabIndex = 35;
      this.lblCopyAudioFileMsg.Text = "(No Copy if Blank)";
      // 
      // lblProjectNameMsg
      // 
      this.lblProjectNameMsg.AutoSize = true;
      this.lblProjectNameMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblProjectNameMsg.Location = new System.Drawing.Point(120, 325);
      this.lblProjectNameMsg.Name = "lblProjectNameMsg";
      this.lblProjectNameMsg.Size = new System.Drawing.Size(155, 16);
      this.lblProjectNameMsg.TabIndex = 38;
      this.lblProjectNameMsg.Text = "(FileNames Qualifier)";
      // 
      // txtProjectName
      // 
      this.txtProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtProjectName.Location = new System.Drawing.Point(15, 344);
      this.txtProjectName.Name = "txtProjectName";
      this.txtProjectName.Size = new System.Drawing.Size(528, 22);
      this.txtProjectName.TabIndex = 37;
      this.txtProjectName.TextChanged += new System.EventHandler(this.txtProjectName_TextChanged);
      // 
      // lblProjectName
      // 
      this.lblProjectName.AutoSize = true;
      this.lblProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblProjectName.Location = new System.Drawing.Point(12, 325);
      this.lblProjectName.Name = "lblProjectName";
      this.lblProjectName.Size = new System.Drawing.Size(102, 16);
      this.lblProjectName.TabIndex = 36;
      this.lblProjectName.Text = "Project Name";
      // 
      // cmdHelp
      // 
      this.cmdHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdHelp.Location = new System.Drawing.Point(15, 447);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(75, 33);
      this.cmdHelp.TabIndex = 41;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // cmdCancel
      // 
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdCancel.Location = new System.Drawing.Point(504, 447);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(75, 33);
      this.cmdCancel.TabIndex = 40;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      // 
      // cmdOK
      // 
      this.cmdOK.Enabled = false;
      this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdOK.Location = new System.Drawing.Point(423, 447);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(75, 33);
      this.cmdOK.TabIndex = 39;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
      // 
      // cmdUseMidi
      // 
      this.cmdUseMidi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdUseMidi.Location = new System.Drawing.Point(15, 370);
      this.cmdUseMidi.Name = "cmdUseMidi";
      this.cmdUseMidi.Size = new System.Drawing.Size(186, 33);
      this.cmdUseMidi.TabIndex = 42;
      this.cmdUseMidi.Text = "Use MidiFile Qualifier";
      this.cmdUseMidi.UseVisualStyleBackColor = true;
      this.cmdUseMidi.Click += new System.EventHandler(this.cmdUseMidi_Click);
      // 
      // cmdUseAudio
      // 
      this.cmdUseAudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdUseAudio.Location = new System.Drawing.Point(207, 370);
      this.cmdUseAudio.Name = "cmdUseAudio";
      this.cmdUseAudio.Size = new System.Drawing.Size(186, 33);
      this.cmdUseAudio.TabIndex = 43;
      this.cmdUseAudio.Text = "Use AudioFile Qualifier";
      this.cmdUseAudio.UseVisualStyleBackColor = true;
      this.cmdUseAudio.Click += new System.EventHandler(this.cmdUseAudio_Click);
      // 
      // chkAddNameToLocation
      // 
      this.chkAddNameToLocation.AutoSize = true;
      this.chkAddNameToLocation.Checked = true;
      this.chkAddNameToLocation.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkAddNameToLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chkAddNameToLocation.Location = new System.Drawing.Point(15, 271);
      this.chkAddNameToLocation.Name = "chkAddNameToLocation";
      this.chkAddNameToLocation.Size = new System.Drawing.Size(239, 20);
      this.chkAddNameToLocation.TabIndex = 44;
      this.chkAddNameToLocation.Text = "Add Name Subdirectory to Location";
      this.chkAddNameToLocation.UseVisualStyleBackColor = true;
      // 
      // lblIntro
      // 
      this.lblIntro.AutoSize = true;
      this.lblIntro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblIntro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblIntro.Location = new System.Drawing.Point(15, 9);
      this.lblIntro.Name = "lblIntro";
      this.lblIntro.Size = new System.Drawing.Size(426, 66);
      this.lblIntro.TabIndex = 45;
      this.lblIntro.Text = resources.GetString("lblIntro.Text");
      // 
      // dlgNewProject
      // 
      this.AcceptButton = this.cmdOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(594, 491);
      this.Controls.Add(this.lblIntro);
      this.Controls.Add(this.chkAddNameToLocation);
      this.Controls.Add(this.cmdUseAudio);
      this.Controls.Add(this.cmdUseMidi);
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.cmdOK);
      this.Controls.Add(this.lblProjectNameMsg);
      this.Controls.Add(this.txtProjectName);
      this.Controls.Add(this.lblProjectName);
      this.Controls.Add(this.lblCopyAudioFileMsg);
      this.Controls.Add(this.lblCopyMidiFileMsg);
      this.Controls.Add(this.lblCopyAudioFile);
      this.Controls.Add(this.lblCopyMidiFile);
      this.Controls.Add(this.txtAudioFilePath);
      this.Controls.Add(this.cmdAudioPath);
      this.Controls.Add(this.txtProjectLocation);
      this.Controls.Add(this.txtMidiFilePath);
      this.Controls.Add(this.lblProjectLocation);
      this.Controls.Add(this.cmdProjectLocation);
      this.Controls.Add(this.cmdMidiPath);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "dlgNewProject";
      this.Text = "Create and Load New Project - Chord Cadenza";
      this.Load += new System.EventHandler(this.frmNewProject_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TextBox txtAudioFilePath;
    private System.Windows.Forms.Button cmdAudioPath;
    private System.Windows.Forms.TextBox txtProjectLocation;
    private System.Windows.Forms.TextBox txtMidiFilePath;
    private System.Windows.Forms.Label lblProjectLocation;
    private System.Windows.Forms.Button cmdProjectLocation;
    private System.Windows.Forms.Button cmdMidiPath;
    private System.Windows.Forms.Label lblCopyMidiFile;
    private System.Windows.Forms.Label lblCopyAudioFile;
    private System.Windows.Forms.Label lblCopyMidiFileMsg;
    private System.Windows.Forms.Label lblCopyAudioFileMsg;
    private System.Windows.Forms.Label lblProjectNameMsg;
    private System.Windows.Forms.TextBox txtProjectName;
    private System.Windows.Forms.Label lblProjectName;
    private System.Windows.Forms.Button cmdHelp;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.OpenFileDialog ofd;
    private System.Windows.Forms.FolderBrowserDialog fbd;
    private System.Windows.Forms.Button cmdUseMidi;
    private System.Windows.Forms.Button cmdUseAudio;
    private System.Windows.Forms.CheckBox chkAddNameToLocation;
    private System.Windows.Forms.Label lblIntro;
  }
}