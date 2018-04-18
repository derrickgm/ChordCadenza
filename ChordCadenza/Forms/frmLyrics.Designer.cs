namespace ChordCadenza.Forms {
  partial class frmLyrics {
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
      this.cmdCancel = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.cmdUndo = new System.Windows.Forms.Button();
      this.cmdRedo = new System.Windows.Forms.Button();
      this.cmdPaste = new System.Windows.Forms.Button();
      this.cmdCut = new System.Windows.Forms.Button();
      this.cmdUpdate = new System.Windows.Forms.Button();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.cmdCopy = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.panLyrics = new ChordCadenza.PanelNoScrollOnFocus();
      this.rtbBars = new System.Windows.Forms.RichTextBox();
      this.rtbLines = new System.Windows.Forms.RichTextBox();
      this.panLyrics.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmdCancel
      // 
      this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdCancel.Location = new System.Drawing.Point(148, 465);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(130, 44);
      this.cmdCancel.TabIndex = 101;
      this.cmdCancel.Text = "Close Window";
      this.toolTip1.SetToolTip(this.cmdCancel, "Close this window.");
      this.cmdCancel.UseVisualStyleBackColor = true;
      this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
      // 
      // cmdUndo
      // 
      this.cmdUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdUndo.Enabled = false;
      this.cmdUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdUndo.Location = new System.Drawing.Point(216, 517);
      this.cmdUndo.Name = "cmdUndo";
      this.cmdUndo.Size = new System.Drawing.Size(62, 73);
      this.cmdUndo.TabIndex = 109;
      this.cmdUndo.Text = "Undo\r\nUpdate";
      this.toolTip1.SetToolTip(this.cmdUndo, "Undo last update.\r\n");
      this.cmdUndo.UseVisualStyleBackColor = true;
      this.cmdUndo.Click += new System.EventHandler(this.cmdUndo_Click);
      // 
      // cmdRedo
      // 
      this.cmdRedo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdRedo.Enabled = false;
      this.cmdRedo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdRedo.Location = new System.Drawing.Point(284, 517);
      this.cmdRedo.Name = "cmdRedo";
      this.cmdRedo.Size = new System.Drawing.Size(62, 73);
      this.cmdRedo.TabIndex = 110;
      this.cmdRedo.Text = "Redo\r\nUpdate";
      this.toolTip1.SetToolTip(this.cmdRedo, "Redo last undone update.\r\n");
      this.cmdRedo.UseVisualStyleBackColor = true;
      this.cmdRedo.Click += new System.EventHandler(this.cmdRedo_Click);
      // 
      // cmdPaste
      // 
      this.cmdPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdPaste.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdPaste.Location = new System.Drawing.Point(148, 517);
      this.cmdPaste.Name = "cmdPaste";
      this.cmdPaste.Size = new System.Drawing.Size(62, 73);
      this.cmdPaste.TabIndex = 115;
      this.cmdPaste.Text = "Paste";
      this.toolTip1.SetToolTip(this.cmdPaste, "Paste selected text from the Clipboard.");
      this.cmdPaste.UseVisualStyleBackColor = true;
      this.cmdPaste.Click += new System.EventHandler(this.cmdPaste_Click);
      // 
      // cmdCut
      // 
      this.cmdCut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdCut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdCut.Location = new System.Drawing.Point(12, 517);
      this.cmdCut.Name = "cmdCut";
      this.cmdCut.Size = new System.Drawing.Size(62, 73);
      this.cmdCut.TabIndex = 116;
      this.cmdCut.Text = "Cut";
      this.toolTip1.SetToolTip(this.cmdCut, "Cut selected text from the Clipboard.");
      this.cmdCut.UseVisualStyleBackColor = true;
      this.cmdCut.Click += new System.EventHandler(this.cmdCut_Click);
      // 
      // cmdUpdate
      // 
      this.cmdUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdUpdate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdUpdate.Location = new System.Drawing.Point(12, 465);
      this.cmdUpdate.Name = "cmdUpdate";
      this.cmdUpdate.Size = new System.Drawing.Size(130, 44);
      this.cmdUpdate.TabIndex = 117;
      this.cmdUpdate.Text = "Update";
      this.toolTip1.SetToolTip(this.cmdUpdate, "Update the lyrics on the Map windows");
      this.cmdUpdate.UseVisualStyleBackColor = true;
      this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
      // 
      // cmdHelp
      // 
      this.cmdHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdHelp.Location = new System.Drawing.Point(284, 465);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(62, 44);
      this.cmdHelp.TabIndex = 118;
      this.cmdHelp.Text = "Help";
      this.toolTip1.SetToolTip(this.cmdHelp, "Update the lyrics");
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // cmdCopy
      // 
      this.cmdCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdCopy.Location = new System.Drawing.Point(80, 517);
      this.cmdCopy.Name = "cmdCopy";
      this.cmdCopy.Size = new System.Drawing.Size(62, 73);
      this.cmdCopy.TabIndex = 119;
      this.cmdCopy.Text = "Copy";
      this.toolTip1.SetToolTip(this.cmdCopy, "Copy selected text from the Clipboard.");
      this.cmdCopy.UseVisualStyleBackColor = true;
      this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(61, 19);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(40, 13);
      this.label4.TabIndex = 103;
      this.label4.Text = "Lyrics";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(20, 19);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(26, 13);
      this.label5.TabIndex = 102;
      this.label5.Text = "Bar";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // panLyrics
      // 
      this.panLyrics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panLyrics.AutoScroll = true;
      this.panLyrics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panLyrics.Controls.Add(this.rtbBars);
      this.panLyrics.Controls.Add(this.rtbLines);
      this.panLyrics.Location = new System.Drawing.Point(12, 35);
      this.panLyrics.Name = "panLyrics";
      this.panLyrics.Size = new System.Drawing.Size(334, 424);
      this.panLyrics.TabIndex = 108;
      // 
      // rtbBars
      // 
      this.rtbBars.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.rtbBars.Location = new System.Drawing.Point(3, -1);
      this.rtbBars.Name = "rtbBars";
      this.rtbBars.ReadOnly = true;
      this.rtbBars.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.rtbBars.Size = new System.Drawing.Size(35, 283);
      this.rtbBars.TabIndex = 105;
      this.rtbBars.Text = "";
      this.rtbBars.WordWrap = false;
      // 
      // rtbLines
      // 
      this.rtbLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.rtbLines.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.rtbLines.DetectUrls = false;
      this.rtbLines.Location = new System.Drawing.Point(44, -1);
      this.rtbLines.Name = "rtbLines";
      this.rtbLines.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.rtbLines.Size = new System.Drawing.Size(269, 283);
      this.rtbLines.TabIndex = 104;
      this.rtbLines.Text = "";
      this.rtbLines.WordWrap = false;
      this.rtbLines.SelectionChanged += new System.EventHandler(this.rtbLines_SelectionChanged);
      this.rtbLines.TextChanged += new System.EventHandler(this.rtbLines_TextChanged);
      // 
      // frmLyrics
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(358, 600);
      this.Controls.Add(this.cmdCopy);
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.cmdUpdate);
      this.Controls.Add(this.cmdCut);
      this.Controls.Add(this.cmdPaste);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.cmdRedo);
      this.Controls.Add(this.cmdUndo);
      this.Controls.Add(this.panLyrics);
      this.Controls.Add(this.cmdCancel);
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimumSize = new System.Drawing.Size(300, 300);
      this.Name = "frmLyrics";
      this.Text = "Update Lyrics - Chord Cadenza";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLyrics_FormClosing);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLyrics_FormClosed);
      this.Load += new System.EventHandler(this.frmLyrics_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLyrics_KeyDown);
      this.panLyrics.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button cmdCancel;
    internal System.Windows.Forms.ToolTip toolTip1;
    //private System.Windows.Forms.Panel panLyrics;
    private PanelNoScrollOnFocus panLyrics;
    internal System.Windows.Forms.RichTextBox rtbLines;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button cmdUndo;
    private System.Windows.Forms.Button cmdRedo;
    internal System.Windows.Forms.RichTextBox rtbBars;
    private System.Windows.Forms.Button cmdPaste;
    private System.Windows.Forms.Button cmdCut;
    private System.Windows.Forms.Button cmdUpdate;
    private System.Windows.Forms.Button cmdHelp;
    private System.Windows.Forms.Button cmdCopy;
  }
}