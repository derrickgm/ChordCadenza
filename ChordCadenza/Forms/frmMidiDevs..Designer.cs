namespace ChordCadenza.Forms {
  partial class frmMidiDevs {
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
      this.lblSyncIn = new System.Windows.Forms.Label();
      this.lblcmbInKB = new System.Windows.Forms.Label();
      this.lblcmbOutKB = new System.Windows.Forms.Label();
      this.cmbInSync = new System.Windows.Forms.ComboBox();
      this.cmbInKB = new System.Windows.Forms.ComboBox();
      this.cmbOutKB = new System.Windows.Forms.ComboBox();
      this.cmdClose = new System.Windows.Forms.Button();
      this.cmbOutStream = new System.Windows.Forms.ComboBox();
      this.lblcmbOutStream = new System.Windows.Forms.Label();
      this.cmbFontStream = new System.Windows.Forms.ComboBox();
      this.cmbFontKB = new System.Windows.Forms.ComboBox();
      this.lblcmbFontStream = new System.Windows.Forms.Label();
      this.cmdFXStream = new System.Windows.Forms.Button();
      this.cmdFXKB = new System.Windows.Forms.Button();
      this.grpMidiIn = new System.Windows.Forms.GroupBox();
      this.cmdExecInKB = new System.Windows.Forms.Button();
      this.grpMidiOut = new System.Windows.Forms.GroupBox();
      this.cmdExecOutKB = new System.Windows.Forms.Button();
      this.cmdExecOutStream = new System.Windows.Forms.Button();
      this.grpTuningOutKB = new System.Windows.Forms.GroupBox();
      this.lblMidiOutKBFineTuning = new System.Windows.Forms.Label();
      this.lbltrkMidiOutKBFineTuning = new System.Windows.Forms.Label();
      this.trkMidiOutKBFineTuning = new System.Windows.Forms.TrackBar();
      this.grpTuningOutStream = new System.Windows.Forms.GroupBox();
      this.lblMidiStreamFineTuning = new System.Windows.Forms.Label();
      this.lbltrkMidiStreamFineTuning = new System.Windows.Forms.Label();
      this.trkMidiStreamFineTuning = new System.Windows.Forms.TrackBar();
      this.lblCurrentAudio = new System.Windows.Forms.Label();
      this.lblLitCurrent = new System.Windows.Forms.Label();
      this.grpOutKB = new System.Windows.Forms.GroupBox();
      this.lblcmbFontKB = new System.Windows.Forms.Label();
      this.grpOutStream = new System.Windows.Forms.GroupBox();
      this.lblSyncMsg = new System.Windows.Forms.Label();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.grpMidiIn.SuspendLayout();
      this.grpMidiOut.SuspendLayout();
      this.grpTuningOutKB.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trkMidiOutKBFineTuning)).BeginInit();
      this.grpTuningOutStream.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trkMidiStreamFineTuning)).BeginInit();
      this.grpOutKB.SuspendLayout();
      this.grpOutStream.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblSyncIn
      // 
      this.lblSyncIn.AutoSize = true;
      this.lblSyncIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblSyncIn.ForeColor = System.Drawing.Color.Red;
      this.lblSyncIn.Location = new System.Drawing.Point(557, 17);
      this.lblSyncIn.Name = "lblSyncIn";
      this.lblSyncIn.Size = new System.Drawing.Size(56, 16);
      this.lblSyncIn.TabIndex = 0;
      this.lblSyncIn.Text = "Sync In*";
      // 
      // lblcmbInKB
      // 
      this.lblcmbInKB.AutoSize = true;
      this.lblcmbInKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblcmbInKB.Location = new System.Drawing.Point(13, 21);
      this.lblcmbInKB.Name = "lblcmbInKB";
      this.lblcmbInKB.Size = new System.Drawing.Size(108, 16);
      this.lblcmbInKB.TabIndex = 1;
      this.lblcmbInKB.Text = "Midi Keyboard In";
      // 
      // lblcmbOutKB
      // 
      this.lblcmbOutKB.AutoSize = true;
      this.lblcmbOutKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblcmbOutKB.Location = new System.Drawing.Point(6, 18);
      this.lblcmbOutKB.Name = "lblcmbOutKB";
      this.lblcmbOutKB.Size = new System.Drawing.Size(118, 16);
      this.lblcmbOutKB.TabIndex = 2;
      this.lblcmbOutKB.Text = "Midi Keyboard Out";
      // 
      // cmbInSync
      // 
      this.cmbInSync.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbInSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmbInSync.FormattingEnabled = true;
      this.cmbInSync.Location = new System.Drawing.Point(683, 16);
      this.cmbInSync.Name = "cmbInSync";
      this.cmbInSync.Size = new System.Drawing.Size(226, 21);
      this.cmbInSync.TabIndex = 5;
      this.cmbInSync.SelectedIndexChanged += new System.EventHandler(this.cmbInSync_SelectedIndexChanged);
      // 
      // cmbInKB
      // 
      this.cmbInKB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbInKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmbInKB.FormattingEnabled = true;
      this.cmbInKB.Location = new System.Drawing.Point(139, 18);
      this.cmbInKB.Name = "cmbInKB";
      this.cmbInKB.Size = new System.Drawing.Size(226, 21);
      this.cmbInKB.TabIndex = 6;
      this.cmbInKB.SelectedIndexChanged += new System.EventHandler(this.cmbInKB_SelectedIndexChanged);
      // 
      // cmbOutKB
      // 
      this.cmbOutKB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbOutKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.cmbOutKB.FormattingEnabled = true;
      this.cmbOutKB.Location = new System.Drawing.Point(132, 15);
      this.cmbOutKB.Name = "cmbOutKB";
      this.cmbOutKB.Size = new System.Drawing.Size(226, 21);
      this.cmbOutKB.TabIndex = 7;
      this.cmbOutKB.SelectedIndexChanged += new System.EventHandler(this.cmbOutKB_SelectedIndexChanged);
      // 
      // cmdClose
      // 
      this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdClose.Location = new System.Drawing.Point(968, 334);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(75, 39);
      this.cmdClose.TabIndex = 12;
      this.cmdClose.Text = "Close Window";
      this.cmdClose.UseVisualStyleBackColor = true;
      this.cmdClose.Click += new System.EventHandler(this.cmdCancel_Click);
      // 
      // cmbOutStream
      // 
      this.cmbOutStream.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbOutStream.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.cmbOutStream.FormattingEnabled = true;
      this.cmbOutStream.Location = new System.Drawing.Point(131, 15);
      this.cmbOutStream.Name = "cmbOutStream";
      this.cmbOutStream.Size = new System.Drawing.Size(226, 21);
      this.cmbOutStream.TabIndex = 15;
      this.cmbOutStream.SelectedIndexChanged += new System.EventHandler(this.cmbOutStream_SelectedIndexChanged);
      // 
      // lblcmbOutStream
      // 
      this.lblcmbOutStream.AutoSize = true;
      this.lblcmbOutStream.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblcmbOutStream.Location = new System.Drawing.Point(5, 18);
      this.lblcmbOutStream.Name = "lblcmbOutStream";
      this.lblcmbOutStream.Size = new System.Drawing.Size(120, 16);
      this.lblcmbOutStream.TabIndex = 14;
      this.lblcmbOutStream.Text = "Sequencer Stream";
      // 
      // cmbFontStream
      // 
      this.cmbFontStream.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbFontStream.Enabled = false;
      this.cmbFontStream.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.cmbFontStream.FormattingEnabled = true;
      this.cmbFontStream.Location = new System.Drawing.Point(132, 59);
      this.cmbFontStream.Name = "cmbFontStream";
      this.cmbFontStream.Size = new System.Drawing.Size(226, 21);
      this.cmbFontStream.TabIndex = 17;
      this.cmbFontStream.SelectedIndexChanged += new System.EventHandler(this.cmbFontStream_SelectedIndexChanged);
      // 
      // cmbFontKB
      // 
      this.cmbFontKB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbFontKB.Enabled = false;
      this.cmbFontKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.cmbFontKB.FormattingEnabled = true;
      this.cmbFontKB.Location = new System.Drawing.Point(132, 60);
      this.cmbFontKB.Name = "cmbFontKB";
      this.cmbFontKB.Size = new System.Drawing.Size(226, 21);
      this.cmbFontKB.TabIndex = 18;
      this.cmbFontKB.SelectedIndexChanged += new System.EventHandler(this.cmbFontKB_SelectedIndexChanged);
      // 
      // lblcmbFontStream
      // 
      this.lblcmbFontStream.AutoSize = true;
      this.lblcmbFontStream.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblcmbFontStream.Location = new System.Drawing.Point(6, 59);
      this.lblcmbFontStream.Name = "lblcmbFontStream";
      this.lblcmbFontStream.Size = new System.Drawing.Size(73, 16);
      this.lblcmbFontStream.TabIndex = 19;
      this.lblcmbFontStream.Text = "SoundFont";
      // 
      // cmdFXStream
      // 
      this.cmdFXStream.Location = new System.Drawing.Point(320, 14);
      this.cmdFXStream.Name = "cmdFXStream";
      this.cmdFXStream.Size = new System.Drawing.Size(37, 33);
      this.cmdFXStream.TabIndex = 20;
      this.cmdFXStream.Text = "FX";
      this.cmdFXStream.UseVisualStyleBackColor = true;
      this.cmdFXStream.Click += new System.EventHandler(this.cmdFXStream_Click);
      // 
      // cmdFXKB
      // 
      this.cmdFXKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdFXKB.Location = new System.Drawing.Point(319, 12);
      this.cmdFXKB.Name = "cmdFXKB";
      this.cmdFXKB.Size = new System.Drawing.Size(37, 33);
      this.cmdFXKB.TabIndex = 21;
      this.cmdFXKB.Text = "FX";
      this.cmdFXKB.UseVisualStyleBackColor = true;
      this.cmdFXKB.Click += new System.EventHandler(this.cmdFXKB_Click);
      // 
      // grpMidiIn
      // 
      this.grpMidiIn.Controls.Add(this.cmdExecInKB);
      this.grpMidiIn.Controls.Add(this.cmbInKB);
      this.grpMidiIn.Controls.Add(this.cmbInSync);
      this.grpMidiIn.Controls.Add(this.lblcmbInKB);
      this.grpMidiIn.Controls.Add(this.lblSyncIn);
      this.grpMidiIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grpMidiIn.Location = new System.Drawing.Point(9, 35);
      this.grpMidiIn.Name = "grpMidiIn";
      this.grpMidiIn.Size = new System.Drawing.Size(1034, 75);
      this.grpMidiIn.TabIndex = 24;
      this.grpMidiIn.TabStop = false;
      this.grpMidiIn.Text = "Midi In";
      // 
      // cmdExecInKB
      // 
      this.cmdExecInKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdExecInKB.Location = new System.Drawing.Point(379, 16);
      this.cmdExecInKB.Name = "cmdExecInKB";
      this.cmdExecInKB.Size = new System.Drawing.Size(93, 39);
      this.cmdExecInKB.TabIndex = 34;
      this.cmdExecInKB.Text = "Disconnect";
      this.cmdExecInKB.UseVisualStyleBackColor = true;
      this.cmdExecInKB.Click += new System.EventHandler(this.cmdExecInKB_Click);
      // 
      // grpMidiOut
      // 
      this.grpMidiOut.Controls.Add(this.cmdExecOutKB);
      this.grpMidiOut.Controls.Add(this.cmdExecOutStream);
      this.grpMidiOut.Controls.Add(this.grpTuningOutKB);
      this.grpMidiOut.Controls.Add(this.grpTuningOutStream);
      this.grpMidiOut.Controls.Add(this.lblCurrentAudio);
      this.grpMidiOut.Controls.Add(this.lblLitCurrent);
      this.grpMidiOut.Controls.Add(this.grpOutKB);
      this.grpMidiOut.Controls.Add(this.grpOutStream);
      this.grpMidiOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grpMidiOut.Location = new System.Drawing.Point(7, 128);
      this.grpMidiOut.Name = "grpMidiOut";
      this.grpMidiOut.Size = new System.Drawing.Size(1036, 198);
      this.grpMidiOut.TabIndex = 25;
      this.grpMidiOut.TabStop = false;
      this.grpMidiOut.Text = "Midi Out";
      // 
      // cmdExecOutKB
      // 
      this.cmdExecOutKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdExecOutKB.Location = new System.Drawing.Point(930, 50);
      this.cmdExecOutKB.Name = "cmdExecOutKB";
      this.cmdExecOutKB.Size = new System.Drawing.Size(93, 39);
      this.cmdExecOutKB.TabIndex = 37;
      this.cmdExecOutKB.Text = "Disconnect";
      this.cmdExecOutKB.UseVisualStyleBackColor = true;
      this.cmdExecOutKB.Click += new System.EventHandler(this.cmdExecOutKB_Click);
      // 
      // cmdExecOutStream
      // 
      this.cmdExecOutStream.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdExecOutStream.Location = new System.Drawing.Point(382, 50);
      this.cmdExecOutStream.Name = "cmdExecOutStream";
      this.cmdExecOutStream.Size = new System.Drawing.Size(93, 39);
      this.cmdExecOutStream.TabIndex = 32;
      this.cmdExecOutStream.Text = "Disconnect";
      this.cmdExecOutStream.UseVisualStyleBackColor = true;
      this.cmdExecOutStream.Click += new System.EventHandler(this.cmdExecOutStream_Click);
      // 
      // grpTuningOutKB
      // 
      this.grpTuningOutKB.Controls.Add(this.lblMidiOutKBFineTuning);
      this.grpTuningOutKB.Controls.Add(this.lbltrkMidiOutKBFineTuning);
      this.grpTuningOutKB.Controls.Add(this.trkMidiOutKBFineTuning);
      this.grpTuningOutKB.Controls.Add(this.cmdFXKB);
      this.grpTuningOutKB.Location = new System.Drawing.Point(553, 132);
      this.grpTuningOutKB.Name = "grpTuningOutKB";
      this.grpTuningOutKB.Size = new System.Drawing.Size(371, 53);
      this.grpTuningOutKB.TabIndex = 36;
      this.grpTuningOutKB.TabStop = false;
      // 
      // lblMidiOutKBFineTuning
      // 
      this.lblMidiOutKBFineTuning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblMidiOutKBFineTuning.Location = new System.Drawing.Point(280, 19);
      this.lblMidiOutKBFineTuning.Name = "lblMidiOutKBFineTuning";
      this.lblMidiOutKBFineTuning.Size = new System.Drawing.Size(30, 19);
      this.lblMidiOutKBFineTuning.TabIndex = 34;
      this.lblMidiOutKBFineTuning.Text = "0";
      this.lblMidiOutKBFineTuning.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lbltrkMidiOutKBFineTuning
      // 
      this.lbltrkMidiOutKBFineTuning.AutoSize = true;
      this.lbltrkMidiOutKBFineTuning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbltrkMidiOutKBFineTuning.Location = new System.Drawing.Point(5, 15);
      this.lbltrkMidiOutKBFineTuning.Name = "lbltrkMidiOutKBFineTuning";
      this.lbltrkMidiOutKBFineTuning.Size = new System.Drawing.Size(49, 16);
      this.lbltrkMidiOutKBFineTuning.TabIndex = 28;
      this.lbltrkMidiOutKBFineTuning.Text = "Tuning";
      // 
      // trkMidiOutKBFineTuning
      // 
      this.trkMidiOutKBFineTuning.AutoSize = false;
      this.trkMidiOutKBFineTuning.LargeChange = 16;
      this.trkMidiOutKBFineTuning.Location = new System.Drawing.Point(55, 15);
      this.trkMidiOutKBFineTuning.Maximum = 63;
      this.trkMidiOutKBFineTuning.Minimum = -64;
      this.trkMidiOutKBFineTuning.Name = "trkMidiOutKBFineTuning";
      this.trkMidiOutKBFineTuning.Size = new System.Drawing.Size(219, 30);
      this.trkMidiOutKBFineTuning.TabIndex = 27;
      this.trkMidiOutKBFineTuning.TickFrequency = 64;
      this.trkMidiOutKBFineTuning.Scroll += new System.EventHandler(this.trkMidiOutKBFineTuning_Scroll);
      this.trkMidiOutKBFineTuning.ValueChanged += new System.EventHandler(this.trkMidiOutKBFineTuning_ValueChanged);
      // 
      // grpTuningOutStream
      // 
      this.grpTuningOutStream.Controls.Add(this.lblMidiStreamFineTuning);
      this.grpTuningOutStream.Controls.Add(this.lbltrkMidiStreamFineTuning);
      this.grpTuningOutStream.Controls.Add(this.cmdFXStream);
      this.grpTuningOutStream.Controls.Add(this.trkMidiStreamFineTuning);
      this.grpTuningOutStream.Location = new System.Drawing.Point(10, 132);
      this.grpTuningOutStream.Name = "grpTuningOutStream";
      this.grpTuningOutStream.Size = new System.Drawing.Size(366, 53);
      this.grpTuningOutStream.TabIndex = 35;
      this.grpTuningOutStream.TabStop = false;
      // 
      // lblMidiStreamFineTuning
      // 
      this.lblMidiStreamFineTuning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblMidiStreamFineTuning.Location = new System.Drawing.Point(280, 19);
      this.lblMidiStreamFineTuning.Name = "lblMidiStreamFineTuning";
      this.lblMidiStreamFineTuning.Size = new System.Drawing.Size(30, 19);
      this.lblMidiStreamFineTuning.TabIndex = 33;
      this.lblMidiStreamFineTuning.Text = "0";
      this.lblMidiStreamFineTuning.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lbltrkMidiStreamFineTuning
      // 
      this.lbltrkMidiStreamFineTuning.AutoSize = true;
      this.lbltrkMidiStreamFineTuning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbltrkMidiStreamFineTuning.Location = new System.Drawing.Point(5, 16);
      this.lbltrkMidiStreamFineTuning.Name = "lbltrkMidiStreamFineTuning";
      this.lbltrkMidiStreamFineTuning.Size = new System.Drawing.Size(49, 16);
      this.lbltrkMidiStreamFineTuning.TabIndex = 26;
      this.lbltrkMidiStreamFineTuning.Text = "Tuning";
      // 
      // trkMidiStreamFineTuning
      // 
      this.trkMidiStreamFineTuning.AutoSize = false;
      this.trkMidiStreamFineTuning.LargeChange = 16;
      this.trkMidiStreamFineTuning.Location = new System.Drawing.Point(55, 16);
      this.trkMidiStreamFineTuning.Maximum = 63;
      this.trkMidiStreamFineTuning.Minimum = -64;
      this.trkMidiStreamFineTuning.Name = "trkMidiStreamFineTuning";
      this.trkMidiStreamFineTuning.Size = new System.Drawing.Size(219, 30);
      this.trkMidiStreamFineTuning.TabIndex = 25;
      this.trkMidiStreamFineTuning.TickFrequency = 64;
      this.trkMidiStreamFineTuning.Scroll += new System.EventHandler(this.trkMidiStreamFineTuning_Scroll);
      this.trkMidiStreamFineTuning.ValueChanged += new System.EventHandler(this.trkMidiStreamFineTuning_ValueChanged);
      // 
      // lblCurrentAudio
      // 
      this.lblCurrentAudio.AutoSize = true;
      this.lblCurrentAudio.Location = new System.Drawing.Point(139, 18);
      this.lblCurrentAudio.Name = "lblCurrentAudio";
      this.lblCurrentAudio.Size = new System.Drawing.Size(29, 16);
      this.lblCurrentAudio.TabIndex = 30;
      this.lblCurrentAudio.Text = "???";
      // 
      // lblLitCurrent
      // 
      this.lblLitCurrent.AutoSize = true;
      this.lblLitCurrent.Location = new System.Drawing.Point(6, 18);
      this.lblLitCurrent.Name = "lblLitCurrent";
      this.lblLitCurrent.Size = new System.Drawing.Size(129, 16);
      this.lblLitCurrent.TabIndex = 29;
      this.lblLitCurrent.Text = "Current Audio Output";
      // 
      // grpOutKB
      // 
      this.grpOutKB.Controls.Add(this.lblcmbFontKB);
      this.grpOutKB.Controls.Add(this.cmbFontKB);
      this.grpOutKB.Controls.Add(this.cmbOutKB);
      this.grpOutKB.Controls.Add(this.lblcmbOutKB);
      this.grpOutKB.Location = new System.Drawing.Point(553, 37);
      this.grpOutKB.Name = "grpOutKB";
      this.grpOutKB.Size = new System.Drawing.Size(371, 89);
      this.grpOutKB.TabIndex = 28;
      this.grpOutKB.TabStop = false;
      // 
      // lblcmbFontKB
      // 
      this.lblcmbFontKB.AutoSize = true;
      this.lblcmbFontKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblcmbFontKB.Location = new System.Drawing.Point(6, 61);
      this.lblcmbFontKB.Name = "lblcmbFontKB";
      this.lblcmbFontKB.Size = new System.Drawing.Size(73, 16);
      this.lblcmbFontKB.TabIndex = 22;
      this.lblcmbFontKB.Text = "SoundFont";
      // 
      // grpOutStream
      // 
      this.grpOutStream.Controls.Add(this.lblcmbFontStream);
      this.grpOutStream.Controls.Add(this.cmbFontStream);
      this.grpOutStream.Controls.Add(this.cmbOutStream);
      this.grpOutStream.Controls.Add(this.lblcmbOutStream);
      this.grpOutStream.Location = new System.Drawing.Point(10, 37);
      this.grpOutStream.Name = "grpOutStream";
      this.grpOutStream.Size = new System.Drawing.Size(366, 89);
      this.grpOutStream.TabIndex = 27;
      this.grpOutStream.TabStop = false;
      // 
      // lblSyncMsg
      // 
      this.lblSyncMsg.AutoSize = true;
      this.lblSyncMsg.ForeColor = System.Drawing.Color.Red;
      this.lblSyncMsg.Location = new System.Drawing.Point(6, 352);
      this.lblSyncMsg.Name = "lblSyncMsg";
      this.lblSyncMsg.Size = new System.Drawing.Size(260, 13);
      this.lblSyncMsg.TabIndex = 26;
      this.lblSyncMsg.Text = "*Note: Set Sync In to \'None\' to enable Stream Playing";
      // 
      // cmdHelp
      // 
      this.cmdHelp.Location = new System.Drawing.Point(856, 334);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(75, 39);
      this.cmdHelp.TabIndex = 27;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // frmMidiDevs
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdClose;
      this.ClientSize = new System.Drawing.Size(1053, 382);
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.lblSyncMsg);
      this.Controls.Add(this.grpMidiOut);
      this.Controls.Add(this.grpMidiIn);
      this.Controls.Add(this.cmdClose);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmMidiDevs";
      this.Text = "Midi Devices - Chord Cadenza";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMidiDevs_FormClosed);
      this.Load += new System.EventHandler(this.frmMidiDevs_Load);
      this.grpMidiIn.ResumeLayout(false);
      this.grpMidiIn.PerformLayout();
      this.grpMidiOut.ResumeLayout(false);
      this.grpMidiOut.PerformLayout();
      this.grpTuningOutKB.ResumeLayout(false);
      this.grpTuningOutKB.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trkMidiOutKBFineTuning)).EndInit();
      this.grpTuningOutStream.ResumeLayout(false);
      this.grpTuningOutStream.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trkMidiStreamFineTuning)).EndInit();
      this.grpOutKB.ResumeLayout(false);
      this.grpOutKB.PerformLayout();
      this.grpOutStream.ResumeLayout(false);
      this.grpOutStream.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblSyncIn;
    private System.Windows.Forms.Label lblcmbInKB;
    private System.Windows.Forms.Label lblcmbOutKB;
    private System.Windows.Forms.ComboBox cmbInSync;
    private System.Windows.Forms.ComboBox cmbInKB;
    private System.Windows.Forms.Button cmdClose;
    private System.Windows.Forms.Label lblcmbOutStream;
    private System.Windows.Forms.Label lblcmbFontStream;
    private System.Windows.Forms.Button cmdFXStream;
    private System.Windows.Forms.Button cmdFXKB;
    private System.Windows.Forms.GroupBox grpMidiIn;
    private System.Windows.Forms.GroupBox grpMidiOut;
    private System.Windows.Forms.Label lblcmbFontKB;
    private System.Windows.Forms.Label lblSyncMsg;
    private System.Windows.Forms.Label lblCurrentAudio;
    private System.Windows.Forms.Label lblLitCurrent;
    private System.Windows.Forms.Label lbltrkMidiStreamFineTuning;
    private System.Windows.Forms.Label lbltrkMidiOutKBFineTuning;
    private System.Windows.Forms.ComboBox cmbFontStream;
    private System.Windows.Forms.ComboBox cmbFontKB;
    private System.Windows.Forms.ComboBox cmbOutKB;
    private System.Windows.Forms.ComboBox cmbOutStream;
    private System.Windows.Forms.GroupBox grpOutKB;
    private System.Windows.Forms.GroupBox grpOutStream;
    internal System.Windows.Forms.Button cmdHelp;
    internal System.Windows.Forms.TrackBar trkMidiStreamFineTuning;
    internal System.Windows.Forms.TrackBar trkMidiOutKBFineTuning;
    private System.Windows.Forms.Label lblMidiStreamFineTuning;
    private System.Windows.Forms.Label lblMidiOutKBFineTuning;
    private System.Windows.Forms.GroupBox grpTuningOutKB;
    private System.Windows.Forms.GroupBox grpTuningOutStream;
    internal System.Windows.Forms.Button cmdExecOutKB;
    internal System.Windows.Forms.Button cmdExecOutStream;
    internal System.Windows.Forms.Button cmdExecInKB;
  }
}