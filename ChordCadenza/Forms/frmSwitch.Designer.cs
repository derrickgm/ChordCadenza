namespace ChordCadenza.Forms {
  partial class frmSwitch {
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
      this.grpSwitchKeys = new System.Windows.Forms.GroupBox();
      this.lblcmbNextBar = new System.Windows.Forms.Label();
      this.cmbNextBar = new System.Windows.Forms.ComboBox();
      this.lblcmbNextChord = new System.Windows.Forms.Label();
      this.cmbNextChord = new System.Windows.Forms.ComboBox();
      this.lblcmbNextBeat = new System.Windows.Forms.Label();
      this.cmbNextBeat = new System.Windows.Forms.ComboBox();
      this.lblcmbAutoSync = new System.Windows.Forms.Label();
      this.cmbAutoSync = new System.Windows.Forms.ComboBox();
      this.lblManChords = new System.Windows.Forms.Label();
      this.cmbManChords = new System.Windows.Forms.ComboBox();
      this.lblAutoChords = new System.Windows.Forms.Label();
      this.cmbAutoChords = new System.Windows.Forms.ComboBox();
      this.lblcmbSustain = new System.Windows.Forms.Label();
      this.cmbSustain = new System.Windows.Forms.ComboBox();
      this.lblcmbKBChord = new System.Windows.Forms.Label();
      this.cmbKBChord = new System.Windows.Forms.ComboBox();
      this.cmdClose = new System.Windows.Forms.Button();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.lblcmbPrevChord = new System.Windows.Forms.Label();
      this.cmbPrevChord = new System.Windows.Forms.ComboBox();
      this.lblcmbPrevBar = new System.Windows.Forms.Label();
      this.cmbPrevBar = new System.Windows.Forms.ComboBox();
      this.lblcmbPrevBeat = new System.Windows.Forms.Label();
      this.cmbPrevBeat = new System.Windows.Forms.ComboBox();
      this.grpSwitchKeys.SuspendLayout();
      this.SuspendLayout();
      // 
      // grpSwitchKeys
      // 
      this.grpSwitchKeys.Controls.Add(this.lblcmbPrevBeat);
      this.grpSwitchKeys.Controls.Add(this.cmbPrevBeat);
      this.grpSwitchKeys.Controls.Add(this.lblcmbPrevBar);
      this.grpSwitchKeys.Controls.Add(this.cmbPrevBar);
      this.grpSwitchKeys.Controls.Add(this.lblcmbPrevChord);
      this.grpSwitchKeys.Controls.Add(this.cmbPrevChord);
      this.grpSwitchKeys.Controls.Add(this.lblcmbNextBar);
      this.grpSwitchKeys.Controls.Add(this.cmbNextBar);
      this.grpSwitchKeys.Controls.Add(this.lblcmbNextChord);
      this.grpSwitchKeys.Controls.Add(this.cmbNextChord);
      this.grpSwitchKeys.Controls.Add(this.lblcmbNextBeat);
      this.grpSwitchKeys.Controls.Add(this.cmbNextBeat);
      this.grpSwitchKeys.Controls.Add(this.lblcmbAutoSync);
      this.grpSwitchKeys.Controls.Add(this.cmbAutoSync);
      this.grpSwitchKeys.Controls.Add(this.lblManChords);
      this.grpSwitchKeys.Controls.Add(this.cmbManChords);
      this.grpSwitchKeys.Controls.Add(this.lblAutoChords);
      this.grpSwitchKeys.Controls.Add(this.cmbAutoChords);
      this.grpSwitchKeys.Controls.Add(this.lblcmbSustain);
      this.grpSwitchKeys.Controls.Add(this.cmbSustain);
      this.grpSwitchKeys.Controls.Add(this.lblcmbKBChord);
      this.grpSwitchKeys.Controls.Add(this.cmbKBChord);
      this.grpSwitchKeys.Location = new System.Drawing.Point(12, 15);
      this.grpSwitchKeys.Name = "grpSwitchKeys";
      this.grpSwitchKeys.Size = new System.Drawing.Size(247, 306);
      this.grpSwitchKeys.TabIndex = 71;
      this.grpSwitchKeys.TabStop = false;
      // 
      // lblcmbNextBar
      // 
      this.lblcmbNextBar.AutoSize = true;
      this.lblcmbNextBar.Location = new System.Drawing.Point(109, 177);
      this.lblcmbNextBar.Name = "lblcmbNextBar";
      this.lblcmbNextBar.Size = new System.Drawing.Size(48, 13);
      this.lblcmbNextBar.TabIndex = 141;
      this.lblcmbNextBar.Text = "Next Bar";
      // 
      // cmbNextBar
      // 
      this.cmbNextBar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbNextBar.FormattingEnabled = true;
      this.cmbNextBar.Location = new System.Drawing.Point(23, 172);
      this.cmbNextBar.MaxDropDownItems = 13;
      this.cmbNextBar.Name = "cmbNextBar";
      this.cmbNextBar.Size = new System.Drawing.Size(80, 21);
      this.cmbNextBar.TabIndex = 140;
      this.cmbNextBar.Tag = "Next Bar";
      this.cmbNextBar.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
      // 
      // lblcmbNextChord
      // 
      this.lblcmbNextChord.AutoSize = true;
      this.lblcmbNextChord.Location = new System.Drawing.Point(109, 227);
      this.lblcmbNextChord.Name = "lblcmbNextChord";
      this.lblcmbNextChord.Size = new System.Drawing.Size(60, 13);
      this.lblcmbNextChord.TabIndex = 139;
      this.lblcmbNextChord.Text = "Next Chord";
      // 
      // cmbNextChord
      // 
      this.cmbNextChord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbNextChord.FormattingEnabled = true;
      this.cmbNextChord.Location = new System.Drawing.Point(23, 222);
      this.cmbNextChord.MaxDropDownItems = 13;
      this.cmbNextChord.Name = "cmbNextChord";
      this.cmbNextChord.Size = new System.Drawing.Size(80, 21);
      this.cmbNextChord.TabIndex = 138;
      this.cmbNextChord.Tag = "Next Chord";
      this.cmbNextChord.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
      // 
      // lblcmbNextBeat
      // 
      this.lblcmbNextBeat.AutoSize = true;
      this.lblcmbNextBeat.Location = new System.Drawing.Point(109, 127);
      this.lblcmbNextBeat.Name = "lblcmbNextBeat";
      this.lblcmbNextBeat.Size = new System.Drawing.Size(54, 13);
      this.lblcmbNextBeat.TabIndex = 137;
      this.lblcmbNextBeat.Text = "Next Beat";
      // 
      // cmbNextBeat
      // 
      this.cmbNextBeat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbNextBeat.FormattingEnabled = true;
      this.cmbNextBeat.Location = new System.Drawing.Point(23, 122);
      this.cmbNextBeat.MaxDropDownItems = 13;
      this.cmbNextBeat.Name = "cmbNextBeat";
      this.cmbNextBeat.Size = new System.Drawing.Size(80, 21);
      this.cmbNextBeat.TabIndex = 136;
      this.cmbNextBeat.Tag = "Next Beat";
      this.cmbNextBeat.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
      // 
      // lblcmbAutoSync
      // 
      this.lblcmbAutoSync.AutoSize = true;
      this.lblcmbAutoSync.Location = new System.Drawing.Point(109, 77);
      this.lblcmbAutoSync.Name = "lblcmbAutoSync";
      this.lblcmbAutoSync.Size = new System.Drawing.Size(31, 13);
      this.lblcmbAutoSync.TabIndex = 135;
      this.lblcmbAutoSync.Text = "Sync";
      // 
      // cmbAutoSync
      // 
      this.cmbAutoSync.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbAutoSync.FormattingEnabled = true;
      this.cmbAutoSync.Location = new System.Drawing.Point(23, 72);
      this.cmbAutoSync.MaxDropDownItems = 13;
      this.cmbAutoSync.Name = "cmbAutoSync";
      this.cmbAutoSync.Size = new System.Drawing.Size(80, 21);
      this.cmbAutoSync.TabIndex = 134;
      this.cmbAutoSync.Tag = "Sync";
      this.cmbAutoSync.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
      // 
      // lblManChords
      // 
      this.lblManChords.AutoSize = true;
      this.lblManChords.ForeColor = System.Drawing.Color.Red;
      this.lblManChords.Location = new System.Drawing.Point(109, 276);
      this.lblManChords.Name = "lblManChords";
      this.lblManChords.Size = new System.Drawing.Size(64, 13);
      this.lblManChords.TabIndex = 129;
      this.lblManChords.Text = "Man Chords";
      // 
      // cmbManChords
      // 
      this.cmbManChords.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbManChords.FormattingEnabled = true;
      this.cmbManChords.Location = new System.Drawing.Point(23, 272);
      this.cmbManChords.MaxDropDownItems = 13;
      this.cmbManChords.Name = "cmbManChords";
      this.cmbManChords.Size = new System.Drawing.Size(80, 21);
      this.cmbManChords.TabIndex = 128;
      this.cmbManChords.Tag = "Man Chords";
      this.cmbManChords.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
      // 
      // lblAutoChords
      // 
      this.lblAutoChords.AutoSize = true;
      this.lblAutoChords.ForeColor = System.Drawing.Color.Red;
      this.lblAutoChords.Location = new System.Drawing.Point(109, 251);
      this.lblAutoChords.Name = "lblAutoChords";
      this.lblAutoChords.Size = new System.Drawing.Size(65, 13);
      this.lblAutoChords.TabIndex = 127;
      this.lblAutoChords.Text = "Auto Chords";
      // 
      // cmbAutoChords
      // 
      this.cmbAutoChords.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbAutoChords.FormattingEnabled = true;
      this.cmbAutoChords.Location = new System.Drawing.Point(23, 247);
      this.cmbAutoChords.MaxDropDownItems = 13;
      this.cmbAutoChords.Name = "cmbAutoChords";
      this.cmbAutoChords.Size = new System.Drawing.Size(80, 21);
      this.cmbAutoChords.TabIndex = 126;
      this.cmbAutoChords.Tag = "Auto Chords";
      this.cmbAutoChords.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
      // 
      // lblcmbSustain
      // 
      this.lblcmbSustain.AutoSize = true;
      this.lblcmbSustain.Location = new System.Drawing.Point(109, 50);
      this.lblcmbSustain.Name = "lblcmbSustain";
      this.lblcmbSustain.Size = new System.Drawing.Size(42, 13);
      this.lblcmbSustain.TabIndex = 85;
      this.lblcmbSustain.Text = "Sustain";
      // 
      // cmbSustain
      // 
      this.cmbSustain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbSustain.FormattingEnabled = true;
      this.cmbSustain.Location = new System.Drawing.Point(23, 47);
      this.cmbSustain.MaxDropDownItems = 13;
      this.cmbSustain.Name = "cmbSustain";
      this.cmbSustain.Size = new System.Drawing.Size(80, 21);
      this.cmbSustain.TabIndex = 84;
      this.cmbSustain.Tag = "Sustain";
      this.cmbSustain.SelectedIndexChanged += new System.EventHandler(this.cmbSustains_SelectedIndexChanged);
      // 
      // lblcmbKBChord
      // 
      this.lblcmbKBChord.AutoSize = true;
      this.lblcmbKBChord.Location = new System.Drawing.Point(109, 25);
      this.lblcmbKBChord.Name = "lblcmbKBChord";
      this.lblcmbKBChord.Size = new System.Drawing.Size(52, 13);
      this.lblcmbKBChord.TabIndex = 79;
      this.lblcmbKBChord.Text = "KB Chord";
      // 
      // cmbKBChord
      // 
      this.cmbKBChord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbKBChord.FormattingEnabled = true;
      this.cmbKBChord.Location = new System.Drawing.Point(23, 22);
      this.cmbKBChord.MaxDropDownItems = 13;
      this.cmbKBChord.Name = "cmbKBChord";
      this.cmbKBChord.Size = new System.Drawing.Size(80, 21);
      this.cmbKBChord.TabIndex = 78;
      this.cmbKBChord.Tag = "KB Chord";
      this.cmbKBChord.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
      // 
      // cmdClose
      // 
      this.cmdClose.Location = new System.Drawing.Point(209, 327);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(50, 24);
      this.cmdClose.TabIndex = 72;
      this.cmdClose.Text = "Close";
      this.cmdClose.UseVisualStyleBackColor = true;
      this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
      // 
      // cmdHelp
      // 
      this.cmdHelp.Location = new System.Drawing.Point(153, 327);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(50, 24);
      this.cmdHelp.TabIndex = 73;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // lblcmbPrevChord
      // 
      this.lblcmbPrevChord.AutoSize = true;
      this.lblcmbPrevChord.Location = new System.Drawing.Point(109, 202);
      this.lblcmbPrevChord.Name = "lblcmbPrevChord";
      this.lblcmbPrevChord.Size = new System.Drawing.Size(60, 13);
      this.lblcmbPrevChord.TabIndex = 143;
      this.lblcmbPrevChord.Text = "Prev Chord";
      // 
      // cmbPrevChord
      // 
      this.cmbPrevChord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbPrevChord.FormattingEnabled = true;
      this.cmbPrevChord.Location = new System.Drawing.Point(23, 197);
      this.cmbPrevChord.MaxDropDownItems = 13;
      this.cmbPrevChord.Name = "cmbPrevChord";
      this.cmbPrevChord.Size = new System.Drawing.Size(80, 21);
      this.cmbPrevChord.TabIndex = 142;
      this.cmbPrevChord.Tag = "Prev Chord";
      this.cmbPrevChord.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
      // 
      // lblcmbPrevBar
      // 
      this.lblcmbPrevBar.AutoSize = true;
      this.lblcmbPrevBar.Location = new System.Drawing.Point(109, 152);
      this.lblcmbPrevBar.Name = "lblcmbPrevBar";
      this.lblcmbPrevBar.Size = new System.Drawing.Size(48, 13);
      this.lblcmbPrevBar.TabIndex = 145;
      this.lblcmbPrevBar.Text = "Prev Bar";
      // 
      // cmbPrevBar
      // 
      this.cmbPrevBar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbPrevBar.FormattingEnabled = true;
      this.cmbPrevBar.Location = new System.Drawing.Point(23, 147);
      this.cmbPrevBar.MaxDropDownItems = 13;
      this.cmbPrevBar.Name = "cmbPrevBar";
      this.cmbPrevBar.Size = new System.Drawing.Size(80, 21);
      this.cmbPrevBar.TabIndex = 144;
      this.cmbPrevBar.Tag = "Prev Bar";
      this.cmbPrevBar.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
      // 
      // lblcmbPrevBeat
      // 
      this.lblcmbPrevBeat.AutoSize = true;
      this.lblcmbPrevBeat.Location = new System.Drawing.Point(109, 102);
      this.lblcmbPrevBeat.Name = "lblcmbPrevBeat";
      this.lblcmbPrevBeat.Size = new System.Drawing.Size(54, 13);
      this.lblcmbPrevBeat.TabIndex = 147;
      this.lblcmbPrevBeat.Text = "Prev Beat";
      // 
      // cmbPrevBeat
      // 
      this.cmbPrevBeat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbPrevBeat.FormattingEnabled = true;
      this.cmbPrevBeat.Location = new System.Drawing.Point(23, 97);
      this.cmbPrevBeat.MaxDropDownItems = 13;
      this.cmbPrevBeat.Name = "cmbPrevBeat";
      this.cmbPrevBeat.Size = new System.Drawing.Size(80, 21);
      this.cmbPrevBeat.TabIndex = 146;
      this.cmbPrevBeat.Tag = "Prev Beat";
      this.cmbPrevBeat.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
      // 
      // frmSwitch
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(271, 358);
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.cmdClose);
      this.Controls.Add(this.grpSwitchKeys);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmSwitch";
      this.Text = "SwitchKeys - Chord Cadenza";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSwitch_FormClosed);
      this.Load += new System.EventHandler(this.frmSwitch_Load);
      this.grpSwitchKeys.ResumeLayout(false);
      this.grpSwitchKeys.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox grpSwitchKeys;
    private System.Windows.Forms.Label lblcmbSustain;
    private System.Windows.Forms.ComboBox cmbSustain;
    private System.Windows.Forms.Label lblcmbKBChord;
    private System.Windows.Forms.ComboBox cmbKBChord;
    private System.Windows.Forms.Button cmdClose;
    private System.Windows.Forms.Label lblAutoChords;
    private System.Windows.Forms.ComboBox cmbAutoChords;
    private System.Windows.Forms.Label lblManChords;
    private System.Windows.Forms.ComboBox cmbManChords;
    private System.Windows.Forms.Label lblcmbAutoSync;
    private System.Windows.Forms.ComboBox cmbAutoSync;
    private System.Windows.Forms.Label lblcmbNextBeat;
    private System.Windows.Forms.ComboBox cmbNextBeat;
    private System.Windows.Forms.Button cmdHelp;
    private System.Windows.Forms.Label lblcmbNextChord;
    private System.Windows.Forms.ComboBox cmbNextChord;
    private System.Windows.Forms.Label lblcmbNextBar;
    private System.Windows.Forms.ComboBox cmbNextBar;
    private System.Windows.Forms.Label lblcmbPrevBeat;
    private System.Windows.Forms.ComboBox cmbPrevBeat;
    private System.Windows.Forms.Label lblcmbPrevBar;
    private System.Windows.Forms.ComboBox cmbPrevBar;
    private System.Windows.Forms.Label lblcmbPrevChord;
    private System.Windows.Forms.ComboBox cmbPrevChord;
  }
}