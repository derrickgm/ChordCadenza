namespace ChordCadenza.Forms {
  partial class dlgImport {
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
      this.cmdAudioFile = new System.Windows.Forms.Button();
      this.txtAudioFilePath = new System.Windows.Forms.TextBox();
      this.lblAudioFile = new System.Windows.Forms.Label();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.cmdOK = new System.Windows.Forms.Button();
      this.ofd = new System.Windows.Forms.OpenFileDialog();
      this.lblAudioFileMsg = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.lblMidiFileMsg = new System.Windows.Forms.Label();
      this.cmdMidiFile = new System.Windows.Forms.Button();
      this.txtMidiFilePath = new System.Windows.Forms.TextBox();
      this.lblMidiFile = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // cmdAudioFile
      // 
      this.cmdAudioFile.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdAudioFile.Location = new System.Drawing.Point(549, 95);
      this.cmdAudioFile.Name = "cmdAudioFile";
      this.cmdAudioFile.Size = new System.Drawing.Size(30, 21);
      this.cmdAudioFile.TabIndex = 60;
      this.cmdAudioFile.Text = "...";
      this.cmdAudioFile.UseVisualStyleBackColor = true;
      this.cmdAudioFile.Click += new System.EventHandler(this.cmdAudioFile_Click);
      // 
      // txtAudioFilePath
      // 
      this.txtAudioFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtAudioFilePath.Location = new System.Drawing.Point(15, 95);
      this.txtAudioFilePath.Name = "txtAudioFilePath";
      this.txtAudioFilePath.Size = new System.Drawing.Size(528, 22);
      this.txtAudioFilePath.TabIndex = 59;
      // 
      // lblAudioFile
      // 
      this.lblAudioFile.AutoSize = true;
      this.lblAudioFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblAudioFile.Location = new System.Drawing.Point(12, 76);
      this.lblAudioFile.Name = "lblAudioFile";
      this.lblAudioFile.Size = new System.Drawing.Size(157, 16);
      this.lblAudioFile.TabIndex = 58;
      this.lblAudioFile.Text = "Copy Audio File From";
      // 
      // cmdCancel
      // 
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdCancel.Location = new System.Drawing.Point(506, 209);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(75, 33);
      this.cmdCancel.TabIndex = 62;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      // 
      // cmdOK
      // 
      this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdOK.Location = new System.Drawing.Point(423, 209);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(75, 33);
      this.cmdOK.TabIndex = 61;
      this.cmdOK.Text = "Reload";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
      // 
      // lblAudioFileMsg
      // 
      this.lblAudioFileMsg.AutoSize = true;
      this.lblAudioFileMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblAudioFileMsg.Location = new System.Drawing.Point(171, 76);
      this.lblAudioFileMsg.Name = "lblAudioFileMsg";
      this.lblAudioFileMsg.Size = new System.Drawing.Size(174, 16);
      this.lblAudioFileMsg.TabIndex = 64;
      this.lblAudioFileMsg.Text = "(Leave Blank if not required)";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(410, 48);
      this.label1.TabIndex = 65;
      this.label1.Text = "From here, you can copy an audio or midi file into the project.\r\nAny file using t" +
    "he same extension (e.g. .mp3, .mid) will be overwritten.\r\nClick \"Reload\" to relo" +
    "ad the project and close this window.";
      // 
      // lblMidiFileMsg
      // 
      this.lblMidiFileMsg.AutoSize = true;
      this.lblMidiFileMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMidiFileMsg.Location = new System.Drawing.Point(171, 136);
      this.lblMidiFileMsg.Name = "lblMidiFileMsg";
      this.lblMidiFileMsg.Size = new System.Drawing.Size(174, 16);
      this.lblMidiFileMsg.TabIndex = 69;
      this.lblMidiFileMsg.Text = "(Leave Blank if not required)";
      // 
      // cmdMidiFile
      // 
      this.cmdMidiFile.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdMidiFile.Location = new System.Drawing.Point(549, 155);
      this.cmdMidiFile.Name = "cmdMidiFile";
      this.cmdMidiFile.Size = new System.Drawing.Size(30, 21);
      this.cmdMidiFile.TabIndex = 68;
      this.cmdMidiFile.Text = "...";
      this.cmdMidiFile.UseVisualStyleBackColor = true;
      this.cmdMidiFile.Click += new System.EventHandler(this.cmdMidiFile_Click);
      // 
      // txtMidiFilePath
      // 
      this.txtMidiFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtMidiFilePath.Location = new System.Drawing.Point(15, 155);
      this.txtMidiFilePath.Name = "txtMidiFilePath";
      this.txtMidiFilePath.Size = new System.Drawing.Size(528, 22);
      this.txtMidiFilePath.TabIndex = 67;
      // 
      // lblMidiFile
      // 
      this.lblMidiFile.AutoSize = true;
      this.lblMidiFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMidiFile.Location = new System.Drawing.Point(12, 136);
      this.lblMidiFile.Name = "lblMidiFile";
      this.lblMidiFile.Size = new System.Drawing.Size(146, 16);
      this.lblMidiFile.TabIndex = 66;
      this.lblMidiFile.Text = "Copy Midi File From";
      // 
      // dlgImport
      // 
      this.AcceptButton = this.cmdOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(597, 253);
      this.Controls.Add(this.lblMidiFileMsg);
      this.Controls.Add(this.cmdMidiFile);
      this.Controls.Add(this.txtMidiFilePath);
      this.Controls.Add(this.lblMidiFile);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lblAudioFileMsg);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.cmdOK);
      this.Controls.Add(this.cmdAudioFile);
      this.Controls.Add(this.txtAudioFilePath);
      this.Controls.Add(this.lblAudioFile);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "dlgImport";
      this.Text = "Import Files - Chord Cadenza";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdAudioFile;
    private System.Windows.Forms.TextBox txtAudioFilePath;
    private System.Windows.Forms.Label lblAudioFile;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.OpenFileDialog ofd;
    private System.Windows.Forms.Label lblAudioFileMsg;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblMidiFileMsg;
    private System.Windows.Forms.Button cmdMidiFile;
    private System.Windows.Forms.TextBox txtMidiFilePath;
    private System.Windows.Forms.Label lblMidiFile;
  }
}