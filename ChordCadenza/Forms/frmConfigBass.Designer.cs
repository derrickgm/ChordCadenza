namespace ChordCadenza.Forms {
  partial class frmConfigBass {
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
      this.nudBuffer = new System.Windows.Forms.NumericUpDown();
      this.nudBufferLit = new System.Windows.Forms.Label();
      this.nudBufferMS = new System.Windows.Forms.Label();
      this.lblAudioOutPut = new System.Windows.Forms.Label();
      this.cmbAudioOutput = new System.Windows.Forms.ComboBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.lblMinBufMS = new System.Windows.Forms.Label();
      this.lblLatencyMS = new System.Windows.Forms.Label();
      this.lblMinBuf = new System.Windows.Forms.Label();
      this.lblMinBufLit = new System.Windows.Forms.Label();
      this.lblLatency = new System.Windows.Forms.Label();
      this.lblLatencyLit = new System.Windows.Forms.Label();
      this.cmdClose = new System.Windows.Forms.Button();
      this.nudUpdatePeriodLit = new System.Windows.Forms.Label();
      this.nudUpdatePeriod = new System.Windows.Forms.NumericUpDown();
      this.nudUpdatePeriodMS = new System.Windows.Forms.Label();
      this.cmdApply = new System.Windows.Forms.Button();
      this.panNonAsio = new System.Windows.Forms.Panel();
      this.grpSetParams = new System.Windows.Forms.GroupBox();
      this.cmdCalcBuffer = new System.Windows.Forms.Button();
      this.chkAsio = new System.Windows.Forms.CheckBox();
      this.cmdDisconnect = new System.Windows.Forms.Button();
      this.lblLitCurrent = new System.Windows.Forms.Label();
      this.lblCurrent = new System.Windows.Forms.Label();
      this.cmdAsioPanel = new System.Windows.Forms.Button();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cmdLatencyKBZero = new System.Windows.Forms.Button();
      this.cmdLatencyMidiPlayZero = new System.Windows.Forms.Button();
      this.cmdLatencyKB = new System.Windows.Forms.Button();
      this.cmdLatencyMidiPlay = new System.Windows.Forms.Button();
      this.lblLatencyKBMS = new System.Windows.Forms.Label();
      this.lblLatencyKB = new System.Windows.Forms.Label();
      this.nudLatencyKB = new System.Windows.Forms.NumericUpDown();
      this.lblLatencyMidiPlayMS = new System.Windows.Forms.Label();
      this.lblLatencyMidiPlay = new System.Windows.Forms.Label();
      this.nudLatencyMidiPlay = new System.Windows.Forms.NumericUpDown();
      this.panConnectDisconnect = new System.Windows.Forms.Panel();
      this.cmdConnectAll = new System.Windows.Forms.Button();
      this.trkAsiodB = new System.Windows.Forms.TrackBar();
      this.lblAsiodB = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.nudBuffer)).BeginInit();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudUpdatePeriod)).BeginInit();
      this.panNonAsio.SuspendLayout();
      this.grpSetParams.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudLatencyKB)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudLatencyMidiPlay)).BeginInit();
      this.panConnectDisconnect.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trkAsiodB)).BeginInit();
      this.SuspendLayout();
      // 
      // nudBuffer
      // 
      this.nudBuffer.Location = new System.Drawing.Point(95, 23);
      this.nudBuffer.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.nudBuffer.Minimum = new decimal(new int[] {
            11,
            0,
            0,
            0});
      this.nudBuffer.Name = "nudBuffer";
      this.nudBuffer.Size = new System.Drawing.Size(52, 20);
      this.nudBuffer.TabIndex = 2;
      this.nudBuffer.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.nudBuffer.ValueChanged += new System.EventHandler(this.nudBuffer_ValueChanged);
      // 
      // nudBufferLit
      // 
      this.nudBufferLit.AutoSize = true;
      this.nudBufferLit.Location = new System.Drawing.Point(13, 25);
      this.nudBufferLit.Name = "nudBufferLit";
      this.nudBufferLit.Size = new System.Drawing.Size(58, 13);
      this.nudBufferLit.TabIndex = 3;
      this.nudBufferLit.Text = "Buffer Size";
      // 
      // nudBufferMS
      // 
      this.nudBufferMS.AutoSize = true;
      this.nudBufferMS.Location = new System.Drawing.Point(153, 25);
      this.nudBufferMS.Name = "nudBufferMS";
      this.nudBufferMS.Size = new System.Drawing.Size(20, 13);
      this.nudBufferMS.TabIndex = 4;
      this.nudBufferMS.Text = "ms";
      // 
      // lblAudioOutPut
      // 
      this.lblAudioOutPut.AutoSize = true;
      this.lblAudioOutPut.Location = new System.Drawing.Point(24, 52);
      this.lblAudioOutPut.Name = "lblAudioOutPut";
      this.lblAudioOutPut.Size = new System.Drawing.Size(69, 13);
      this.lblAudioOutPut.TabIndex = 6;
      this.lblAudioOutPut.Text = "Audio Output";
      // 
      // cmbAudioOutput
      // 
      this.cmbAudioOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbAudioOutput.Enabled = false;
      this.cmbAudioOutput.FormattingEnabled = true;
      this.cmbAudioOutput.Location = new System.Drawing.Point(112, 49);
      this.cmbAudioOutput.Name = "cmbAudioOutput";
      this.cmbAudioOutput.Size = new System.Drawing.Size(651, 21);
      this.cmbAudioOutput.TabIndex = 5;
      this.cmbAudioOutput.SelectedIndexChanged += new System.EventHandler(this.cmbAudioOutput_SelectedIndexChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.lblMinBufMS);
      this.groupBox1.Controls.Add(this.lblLatencyMS);
      this.groupBox1.Controls.Add(this.lblMinBuf);
      this.groupBox1.Controls.Add(this.lblMinBufLit);
      this.groupBox1.Controls.Add(this.lblLatency);
      this.groupBox1.Controls.Add(this.lblLatencyLit);
      this.groupBox1.Location = new System.Drawing.Point(4, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(146, 49);
      this.groupBox1.TabIndex = 7;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Device Characteristics";
      // 
      // lblMinBufMS
      // 
      this.lblMinBufMS.AutoSize = true;
      this.lblMinBufMS.Location = new System.Drawing.Point(120, 29);
      this.lblMinBufMS.Name = "lblMinBufMS";
      this.lblMinBufMS.Size = new System.Drawing.Size(20, 13);
      this.lblMinBufMS.TabIndex = 6;
      this.lblMinBufMS.Text = "ms";
      // 
      // lblLatencyMS
      // 
      this.lblLatencyMS.AutoSize = true;
      this.lblLatencyMS.Location = new System.Drawing.Point(120, 16);
      this.lblLatencyMS.Name = "lblLatencyMS";
      this.lblLatencyMS.Size = new System.Drawing.Size(20, 13);
      this.lblLatencyMS.TabIndex = 5;
      this.lblLatencyMS.Text = "ms";
      // 
      // lblMinBuf
      // 
      this.lblMinBuf.AutoSize = true;
      this.lblMinBuf.Location = new System.Drawing.Point(82, 29);
      this.lblMinBuf.Name = "lblMinBuf";
      this.lblMinBuf.Size = new System.Drawing.Size(25, 13);
      this.lblMinBuf.TabIndex = 3;
      this.lblMinBuf.Text = "???";
      // 
      // lblMinBufLit
      // 
      this.lblMinBufLit.AutoSize = true;
      this.lblMinBufLit.Location = new System.Drawing.Point(6, 29);
      this.lblMinBufLit.Name = "lblMinBufLit";
      this.lblMinBufLit.Size = new System.Drawing.Size(55, 13);
      this.lblMinBufLit.TabIndex = 2;
      this.lblMinBufLit.Text = "Min Buffer";
      // 
      // lblLatency
      // 
      this.lblLatency.AutoSize = true;
      this.lblLatency.Location = new System.Drawing.Point(82, 16);
      this.lblLatency.Name = "lblLatency";
      this.lblLatency.Size = new System.Drawing.Size(25, 13);
      this.lblLatency.TabIndex = 1;
      this.lblLatency.Text = "???";
      // 
      // lblLatencyLit
      // 
      this.lblLatencyLit.AutoSize = true;
      this.lblLatencyLit.Location = new System.Drawing.Point(6, 16);
      this.lblLatencyLit.Name = "lblLatencyLit";
      this.lblLatencyLit.Size = new System.Drawing.Size(45, 13);
      this.lblLatencyLit.TabIndex = 0;
      this.lblLatencyLit.Text = "Latency";
      // 
      // cmdClose
      // 
      this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdClose.Location = new System.Drawing.Point(711, 224);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(52, 33);
      this.cmdClose.TabIndex = 9;
      this.cmdClose.Text = "Close";
      this.cmdClose.UseVisualStyleBackColor = true;
      this.cmdClose.Click += new System.EventHandler(this.cmdCancel_Click);
      // 
      // nudUpdatePeriodLit
      // 
      this.nudUpdatePeriodLit.AutoSize = true;
      this.nudUpdatePeriodLit.Location = new System.Drawing.Point(13, 48);
      this.nudUpdatePeriodLit.Name = "nudUpdatePeriodLit";
      this.nudUpdatePeriodLit.Size = new System.Drawing.Size(75, 13);
      this.nudUpdatePeriodLit.TabIndex = 11;
      this.nudUpdatePeriodLit.Text = "Update Period";
      // 
      // nudUpdatePeriod
      // 
      this.nudUpdatePeriod.Location = new System.Drawing.Point(95, 46);
      this.nudUpdatePeriod.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
      this.nudUpdatePeriod.Name = "nudUpdatePeriod";
      this.nudUpdatePeriod.Size = new System.Drawing.Size(52, 20);
      this.nudUpdatePeriod.TabIndex = 10;
      this.nudUpdatePeriod.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
      this.nudUpdatePeriod.ValueChanged += new System.EventHandler(this.nudUpdatePeriod_ValueChanged);
      // 
      // nudUpdatePeriodMS
      // 
      this.nudUpdatePeriodMS.AutoSize = true;
      this.nudUpdatePeriodMS.Location = new System.Drawing.Point(153, 48);
      this.nudUpdatePeriodMS.Name = "nudUpdatePeriodMS";
      this.nudUpdatePeriodMS.Size = new System.Drawing.Size(20, 13);
      this.nudUpdatePeriodMS.TabIndex = 12;
      this.nudUpdatePeriodMS.Text = "ms";
      // 
      // cmdApply
      // 
      this.cmdApply.Location = new System.Drawing.Point(161, 3);
      this.cmdApply.Name = "cmdApply";
      this.cmdApply.Size = new System.Drawing.Size(73, 66);
      this.cmdApply.TabIndex = 17;
      this.cmdApply.Text = "Connect\r\nAudio\r\nOnly";
      this.cmdApply.UseVisualStyleBackColor = true;
      this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
      // 
      // panNonAsio
      // 
      this.panNonAsio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panNonAsio.Controls.Add(this.grpSetParams);
      this.panNonAsio.Controls.Add(this.groupBox1);
      this.panNonAsio.Location = new System.Drawing.Point(27, 111);
      this.panNonAsio.Name = "panNonAsio";
      this.panNonAsio.Size = new System.Drawing.Size(355, 159);
      this.panNonAsio.TabIndex = 18;
      // 
      // grpSetParams
      // 
      this.grpSetParams.Controls.Add(this.cmdCalcBuffer);
      this.grpSetParams.Controls.Add(this.nudUpdatePeriodMS);
      this.grpSetParams.Controls.Add(this.nudBufferLit);
      this.grpSetParams.Controls.Add(this.nudUpdatePeriodLit);
      this.grpSetParams.Controls.Add(this.nudBuffer);
      this.grpSetParams.Controls.Add(this.nudUpdatePeriod);
      this.grpSetParams.Controls.Add(this.nudBufferMS);
      this.grpSetParams.Location = new System.Drawing.Point(3, 67);
      this.grpSetParams.Name = "grpSetParams";
      this.grpSetParams.Size = new System.Drawing.Size(334, 78);
      this.grpSetParams.TabIndex = 23;
      this.grpSetParams.TabStop = false;
      this.grpSetParams.Text = "Set Device Variable";
      // 
      // cmdCalcBuffer
      // 
      this.cmdCalcBuffer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdCalcBuffer.Location = new System.Drawing.Point(179, 22);
      this.cmdCalcBuffer.Name = "cmdCalcBuffer";
      this.cmdCalcBuffer.Size = new System.Drawing.Size(141, 22);
      this.cmdCalcBuffer.TabIndex = 24;
      this.cmdCalcBuffer.Text = "Calc From Update Period";
      this.cmdCalcBuffer.UseVisualStyleBackColor = true;
      this.cmdCalcBuffer.Click += new System.EventHandler(this.cmdCalcBuffer_Click);
      // 
      // chkAsio
      // 
      this.chkAsio.AutoSize = true;
      this.chkAsio.Enabled = false;
      this.chkAsio.Location = new System.Drawing.Point(27, 84);
      this.chkAsio.Name = "chkAsio";
      this.chkAsio.Size = new System.Drawing.Size(51, 17);
      this.chkAsio.TabIndex = 19;
      this.chkAsio.Text = "ASIO";
      this.chkAsio.UseVisualStyleBackColor = true;
      this.chkAsio.CheckedChanged += new System.EventHandler(this.chkAsio_CheckedChanged);
      // 
      // cmdDisconnect
      // 
      this.cmdDisconnect.Location = new System.Drawing.Point(3, 3);
      this.cmdDisconnect.Name = "cmdDisconnect";
      this.cmdDisconnect.Size = new System.Drawing.Size(73, 66);
      this.cmdDisconnect.TabIndex = 20;
      this.cmdDisconnect.Text = "Disconnect\r\nAudio &&\r\nBuiltIn\r\nMidiOut";
      this.cmdDisconnect.UseVisualStyleBackColor = true;
      this.cmdDisconnect.Click += new System.EventHandler(this.cmdDisconnect_Click);
      // 
      // lblLitCurrent
      // 
      this.lblLitCurrent.AutoSize = true;
      this.lblLitCurrent.Location = new System.Drawing.Point(24, 19);
      this.lblLitCurrent.Name = "lblLitCurrent";
      this.lblLitCurrent.Size = new System.Drawing.Size(98, 13);
      this.lblLitCurrent.TabIndex = 21;
      this.lblLitCurrent.Text = "Current Connection";
      // 
      // lblCurrent
      // 
      this.lblCurrent.AutoSize = true;
      this.lblCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrent.Location = new System.Drawing.Point(128, 19);
      this.lblCurrent.Name = "lblCurrent";
      this.lblCurrent.Size = new System.Drawing.Size(28, 13);
      this.lblCurrent.TabIndex = 22;
      this.lblCurrent.Text = "???";
      // 
      // cmdAsioPanel
      // 
      this.cmdAsioPanel.Location = new System.Drawing.Point(240, 3);
      this.cmdAsioPanel.Name = "cmdAsioPanel";
      this.cmdAsioPanel.Size = new System.Drawing.Size(73, 66);
      this.cmdAsioPanel.TabIndex = 23;
      this.cmdAsioPanel.Text = "Show\r\nASIO\r\nPanel";
      this.cmdAsioPanel.UseVisualStyleBackColor = true;
      this.cmdAsioPanel.Click += new System.EventHandler(this.cmdAsioPanel_Click);
      // 
      // cmdHelp
      // 
      this.cmdHelp.Location = new System.Drawing.Point(711, 191);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(52, 32);
      this.cmdHelp.TabIndex = 24;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel1.Controls.Add(this.cmdLatencyKBZero);
      this.panel1.Controls.Add(this.cmdLatencyMidiPlayZero);
      this.panel1.Controls.Add(this.cmdLatencyKB);
      this.panel1.Controls.Add(this.cmdLatencyMidiPlay);
      this.panel1.Controls.Add(this.lblLatencyKBMS);
      this.panel1.Controls.Add(this.lblLatencyKB);
      this.panel1.Controls.Add(this.nudLatencyKB);
      this.panel1.Controls.Add(this.lblLatencyMidiPlayMS);
      this.panel1.Controls.Add(this.lblLatencyMidiPlay);
      this.panel1.Controls.Add(this.nudLatencyMidiPlay);
      this.panel1.Location = new System.Drawing.Point(390, 111);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(361, 74);
      this.panel1.TabIndex = 25;
      // 
      // cmdLatencyKBZero
      // 
      this.cmdLatencyKBZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdLatencyKBZero.Location = new System.Drawing.Point(281, 40);
      this.cmdLatencyKBZero.Name = "cmdLatencyKBZero";
      this.cmdLatencyKBZero.Size = new System.Drawing.Size(70, 22);
      this.cmdLatencyKBZero.TabIndex = 25;
      this.cmdLatencyKBZero.Text = "Switch Off";
      this.cmdLatencyKBZero.UseVisualStyleBackColor = true;
      this.cmdLatencyKBZero.Click += new System.EventHandler(this.cmdLatencyKBZero_Click);
      // 
      // cmdLatencyMidiPlayZero
      // 
      this.cmdLatencyMidiPlayZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdLatencyMidiPlayZero.Location = new System.Drawing.Point(281, 8);
      this.cmdLatencyMidiPlayZero.Name = "cmdLatencyMidiPlayZero";
      this.cmdLatencyMidiPlayZero.Size = new System.Drawing.Size(70, 22);
      this.cmdLatencyMidiPlayZero.TabIndex = 24;
      this.cmdLatencyMidiPlayZero.Text = "Switch Off";
      this.cmdLatencyMidiPlayZero.UseVisualStyleBackColor = true;
      this.cmdLatencyMidiPlayZero.Click += new System.EventHandler(this.cmdLatencyMidiPlayZero_Click);
      // 
      // cmdLatencyKB
      // 
      this.cmdLatencyKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdLatencyKB.Location = new System.Drawing.Point(177, 40);
      this.cmdLatencyKB.Name = "cmdLatencyKB";
      this.cmdLatencyKB.Size = new System.Drawing.Size(98, 22);
      this.cmdLatencyKB.TabIndex = 23;
      this.cmdLatencyKB.Text = "Set From Device";
      this.cmdLatencyKB.UseVisualStyleBackColor = true;
      this.cmdLatencyKB.Click += new System.EventHandler(this.cmdLatencyKB_Click);
      // 
      // cmdLatencyMidiPlay
      // 
      this.cmdLatencyMidiPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdLatencyMidiPlay.Location = new System.Drawing.Point(177, 8);
      this.cmdLatencyMidiPlay.Name = "cmdLatencyMidiPlay";
      this.cmdLatencyMidiPlay.Size = new System.Drawing.Size(98, 22);
      this.cmdLatencyMidiPlay.TabIndex = 22;
      this.cmdLatencyMidiPlay.Text = "Set From Device";
      this.cmdLatencyMidiPlay.UseVisualStyleBackColor = true;
      this.cmdLatencyMidiPlay.Click += new System.EventHandler(this.cmdLatencyMidiPlay_Click);
      // 
      // lblLatencyKBMS
      // 
      this.lblLatencyKBMS.AutoSize = true;
      this.lblLatencyKBMS.Location = new System.Drawing.Point(146, 43);
      this.lblLatencyKBMS.Name = "lblLatencyKBMS";
      this.lblLatencyKBMS.Size = new System.Drawing.Size(20, 13);
      this.lblLatencyKBMS.TabIndex = 10;
      this.lblLatencyKBMS.Text = "ms";
      // 
      // lblLatencyKB
      // 
      this.lblLatencyKB.AutoSize = true;
      this.lblLatencyKB.Location = new System.Drawing.Point(4, 44);
      this.lblLatencyKB.Name = "lblLatencyKB";
      this.lblLatencyKB.Size = new System.Drawing.Size(82, 13);
      this.lblLatencyKB.TabIndex = 9;
      this.lblLatencyKB.Text = "Keyboard Delay";
      // 
      // nudLatencyKB
      // 
      this.nudLatencyKB.Location = new System.Drawing.Point(88, 41);
      this.nudLatencyKB.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
      this.nudLatencyKB.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147483648});
      this.nudLatencyKB.Name = "nudLatencyKB";
      this.nudLatencyKB.Size = new System.Drawing.Size(52, 20);
      this.nudLatencyKB.TabIndex = 8;
      this.nudLatencyKB.ValueChanged += new System.EventHandler(this.nudLatencyKB_ValueChanged);
      // 
      // lblLatencyMidiPlayMS
      // 
      this.lblLatencyMidiPlayMS.AutoSize = true;
      this.lblLatencyMidiPlayMS.Location = new System.Drawing.Point(146, 12);
      this.lblLatencyMidiPlayMS.Name = "lblLatencyMidiPlayMS";
      this.lblLatencyMidiPlayMS.Size = new System.Drawing.Size(20, 13);
      this.lblLatencyMidiPlayMS.TabIndex = 6;
      this.lblLatencyMidiPlayMS.Text = "ms";
      // 
      // lblLatencyMidiPlay
      // 
      this.lblLatencyMidiPlay.AutoSize = true;
      this.lblLatencyMidiPlay.Location = new System.Drawing.Point(4, 13);
      this.lblLatencyMidiPlay.Name = "lblLatencyMidiPlay";
      this.lblLatencyMidiPlay.Size = new System.Drawing.Size(76, 13);
      this.lblLatencyMidiPlay.TabIndex = 4;
      this.lblLatencyMidiPlay.Text = "MidiPlay Delay";
      // 
      // nudLatencyMidiPlay
      // 
      this.nudLatencyMidiPlay.Location = new System.Drawing.Point(88, 10);
      this.nudLatencyMidiPlay.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
      this.nudLatencyMidiPlay.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147483648});
      this.nudLatencyMidiPlay.Name = "nudLatencyMidiPlay";
      this.nudLatencyMidiPlay.Size = new System.Drawing.Size(52, 20);
      this.nudLatencyMidiPlay.TabIndex = 3;
      this.nudLatencyMidiPlay.ValueChanged += new System.EventHandler(this.nudLatencyMidiPlay_ValueChanged);
      // 
      // panConnectDisconnect
      // 
      this.panConnectDisconnect.Controls.Add(this.cmdConnectAll);
      this.panConnectDisconnect.Controls.Add(this.cmdApply);
      this.panConnectDisconnect.Controls.Add(this.cmdDisconnect);
      this.panConnectDisconnect.Controls.Add(this.cmdAsioPanel);
      this.panConnectDisconnect.Location = new System.Drawing.Point(388, 188);
      this.panConnectDisconnect.Name = "panConnectDisconnect";
      this.panConnectDisconnect.Size = new System.Drawing.Size(317, 82);
      this.panConnectDisconnect.TabIndex = 26;
      // 
      // cmdConnectAll
      // 
      this.cmdConnectAll.Location = new System.Drawing.Point(82, 3);
      this.cmdConnectAll.Name = "cmdConnectAll";
      this.cmdConnectAll.Size = new System.Drawing.Size(73, 66);
      this.cmdConnectAll.TabIndex = 21;
      this.cmdConnectAll.Text = "Connect\r\nAudio && \r\nBuilitIn\r\nMidiOut";
      this.cmdConnectAll.UseVisualStyleBackColor = true;
      this.cmdConnectAll.Click += new System.EventHandler(this.cmdConnectAll_Click);
      // 
      // trkAsiodB
      // 
      this.trkAsiodB.AutoSize = false;
      this.trkAsiodB.Location = new System.Drawing.Point(190, 77);
      this.trkAsiodB.Maximum = 20;
      this.trkAsiodB.Minimum = -20;
      this.trkAsiodB.Name = "trkAsiodB";
      this.trkAsiodB.Size = new System.Drawing.Size(329, 27);
      this.trkAsiodB.TabIndex = 27;
      this.trkAsiodB.TickFrequency = 20;
      this.trkAsiodB.Scroll += new System.EventHandler(this.trkAsiodB_Scroll);
      // 
      // lblAsiodB
      // 
      this.lblAsiodB.AutoSize = true;
      this.lblAsiodB.Location = new System.Drawing.Point(110, 85);
      this.lblAsiodB.Name = "lblAsiodB";
      this.lblAsiodB.Size = new System.Drawing.Size(77, 13);
      this.lblAsiodB.TabIndex = 28;
      this.lblAsiodB.Text = "ASIO dB Level";
      // 
      // frmConfigBass
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdClose;
      this.ClientSize = new System.Drawing.Size(774, 280);
      this.Controls.Add(this.lblAsiodB);
      this.Controls.Add(this.trkAsiodB);
      this.Controls.Add(this.panConnectDisconnect);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.lblCurrent);
      this.Controls.Add(this.lblLitCurrent);
      this.Controls.Add(this.chkAsio);
      this.Controls.Add(this.panNonAsio);
      this.Controls.Add(this.cmdClose);
      this.Controls.Add(this.lblAudioOutPut);
      this.Controls.Add(this.cmbAudioOutput);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmConfigBass";
      this.Text = "Configure Audio - Chord Cadenza";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmConfigBass_FormClosed);
      this.Load += new System.EventHandler(this.frmConfigBASS_Load);
      ((System.ComponentModel.ISupportInitialize)(this.nudBuffer)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudUpdatePeriod)).EndInit();
      this.panNonAsio.ResumeLayout(false);
      this.grpSetParams.ResumeLayout(false);
      this.grpSetParams.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudLatencyKB)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudLatencyMidiPlay)).EndInit();
      this.panConnectDisconnect.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.trkAsiodB)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label nudBufferLit;
    private System.Windows.Forms.Label nudBufferMS;
    private System.Windows.Forms.Label lblAudioOutPut;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label lblLatencyLit;
    private System.Windows.Forms.Button cmdClose;
    private System.Windows.Forms.Label lblMinBufLit;
    private System.Windows.Forms.Label nudUpdatePeriodLit;
    private System.Windows.Forms.Label nudUpdatePeriodMS;
    private System.Windows.Forms.Button cmdApply;
    internal System.Windows.Forms.NumericUpDown nudBuffer;
    internal System.Windows.Forms.Label lblLatency;
    internal System.Windows.Forms.Label lblMinBuf;
    internal System.Windows.Forms.NumericUpDown nudUpdatePeriod;
    internal System.Windows.Forms.Panel panNonAsio;
    private System.Windows.Forms.Label lblLitCurrent;
    private System.Windows.Forms.Label lblCurrent;
    private System.Windows.Forms.Button cmdAsioPanel;
    private System.Windows.Forms.Label lblMinBufMS;
    private System.Windows.Forms.Label lblLatencyMS;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label lblLatencyMidiPlayMS;
    private System.Windows.Forms.Label lblLatencyMidiPlay;
    internal System.Windows.Forms.NumericUpDown nudLatencyMidiPlay;
    private System.Windows.Forms.Label lblLatencyKBMS;
    private System.Windows.Forms.Label lblLatencyKB;
    internal System.Windows.Forms.NumericUpDown nudLatencyKB;
    private System.Windows.Forms.Button cmdLatencyKB;
    private System.Windows.Forms.Button cmdLatencyMidiPlay;
    private System.Windows.Forms.Button cmdCalcBuffer;
    internal System.Windows.Forms.GroupBox grpSetParams;
    private System.Windows.Forms.Button cmdLatencyKBZero;
    private System.Windows.Forms.Button cmdLatencyMidiPlayZero;
    private System.Windows.Forms.Panel panConnectDisconnect;
    internal System.Windows.Forms.Button cmdConnectAll;
    internal System.Windows.Forms.Button cmdDisconnect;
    internal System.Windows.Forms.CheckBox chkAsio;
    internal System.Windows.Forms.Button cmdHelp;
    private System.Windows.Forms.TrackBar trkAsiodB;
    private System.Windows.Forms.Label lblAsiodB;
    internal System.Windows.Forms.ComboBox cmbAudioOutput;
  }
}