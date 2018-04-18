namespace ChordCadenza.Forms {
  partial class frmSummary {
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
      this.cmdClose = new System.Windows.Forms.Button();
      this.cmdShowTSigs = new System.Windows.Forms.Button();
      this.cmdShowKeys = new System.Windows.Forms.Button();
      this.clbTrk = new System.Windows.Forms.ListBox();
      this.cmdShowTempos = new System.Windows.Forms.Button();
      this.cmdShowCtlrTots = new System.Windows.Forms.Button();
      this.cmdShowCtlrDetails = new System.Windows.Forms.Button();
      this.cmdChordList = new System.Windows.Forms.Button();
      this.cmdHelp = new System.Windows.Forms.Button();
      this.lblTk = new System.Windows.Forms.Label();
      this.lblCh = new System.Windows.Forms.Label();
      this.lblCount = new System.Windows.Forms.Label();
      this.lblMinChar = new System.Windows.Forms.Label();
      this.lblMaxChar = new System.Windows.Forms.Label();
      this.lblMaxNum = new System.Windows.Forms.Label();
      this.lblMinNum = new System.Windows.Forms.Label();
      this.lblStyle = new System.Windows.Forms.Label();
      this.lblRng = new System.Windows.Forms.Label();
      this.lblPoly = new System.Windows.Forms.Label();
      this.lblPatch = new System.Windows.Forms.Label();
      this.lblTitle = new System.Windows.Forms.Label();
      this.cmdShowAttributes = new System.Windows.Forms.Button();
      this.cmdShowNoteMap = new System.Windows.Forms.Button();
      this.cmdShowStrm = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // cmdClose
      // 
      this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdClose.Location = new System.Drawing.Point(631, 447);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(70, 24);
      this.cmdClose.TabIndex = 3;
      this.cmdClose.Text = "Close";
      this.cmdClose.UseVisualStyleBackColor = true;
      this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
      // 
      // cmdShowTSigs
      // 
      this.cmdShowTSigs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdShowTSigs.Location = new System.Drawing.Point(88, 422);
      this.cmdShowTSigs.Name = "cmdShowTSigs";
      this.cmdShowTSigs.Size = new System.Drawing.Size(70, 50);
      this.cmdShowTSigs.TabIndex = 26;
      this.cmdShowTSigs.Text = "Time\r\nSignatures";
      this.cmdShowTSigs.UseVisualStyleBackColor = true;
      this.cmdShowTSigs.Click += new System.EventHandler(this.cmdShowTSigs_Click);
      // 
      // cmdShowKeys
      // 
      this.cmdShowKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdShowKeys.Location = new System.Drawing.Point(240, 422);
      this.cmdShowKeys.Name = "cmdShowKeys";
      this.cmdShowKeys.Size = new System.Drawing.Size(70, 50);
      this.cmdShowKeys.TabIndex = 28;
      this.cmdShowKeys.Text = "Keys";
      this.cmdShowKeys.UseVisualStyleBackColor = true;
      this.cmdShowKeys.Click += new System.EventHandler(this.cmdShowKeys_Click);
      // 
      // clbTrk
      // 
      this.clbTrk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.clbTrk.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.clbTrk.FormattingEnabled = true;
      this.clbTrk.Location = new System.Drawing.Point(12, 27);
      this.clbTrk.Name = "clbTrk";
      this.clbTrk.Size = new System.Drawing.Size(689, 381);
      this.clbTrk.TabIndex = 29;
      // 
      // cmdShowTempos
      // 
      this.cmdShowTempos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdShowTempos.Location = new System.Drawing.Point(164, 422);
      this.cmdShowTempos.Name = "cmdShowTempos";
      this.cmdShowTempos.Size = new System.Drawing.Size(70, 50);
      this.cmdShowTempos.TabIndex = 30;
      this.cmdShowTempos.Text = "Tempos";
      this.cmdShowTempos.UseVisualStyleBackColor = true;
      this.cmdShowTempos.Click += new System.EventHandler(this.cmdShowTempos_Click);
      // 
      // cmdShowCtlrTots
      // 
      this.cmdShowCtlrTots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdShowCtlrTots.ForeColor = System.Drawing.Color.Red;
      this.cmdShowCtlrTots.Location = new System.Drawing.Point(534, 421);
      this.cmdShowCtlrTots.Name = "cmdShowCtlrTots";
      this.cmdShowCtlrTots.Size = new System.Drawing.Size(91, 25);
      this.cmdShowCtlrTots.TabIndex = 31;
      this.cmdShowCtlrTots.Text = "Midi Ctlr Totals";
      this.cmdShowCtlrTots.UseVisualStyleBackColor = true;
      this.cmdShowCtlrTots.Click += new System.EventHandler(this.cmdShowCtlrs_Click);
      // 
      // cmdShowCtlrDetails
      // 
      this.cmdShowCtlrDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdShowCtlrDetails.ForeColor = System.Drawing.Color.Red;
      this.cmdShowCtlrDetails.Location = new System.Drawing.Point(534, 447);
      this.cmdShowCtlrDetails.Name = "cmdShowCtlrDetails";
      this.cmdShowCtlrDetails.Size = new System.Drawing.Size(91, 25);
      this.cmdShowCtlrDetails.TabIndex = 32;
      this.cmdShowCtlrDetails.Text = "Midi Ctlr Details";
      this.cmdShowCtlrDetails.UseVisualStyleBackColor = true;
      this.cmdShowCtlrDetails.Click += new System.EventHandler(this.cmdShowCtlrDetails_Click);
      // 
      // cmdChordList
      // 
      this.cmdChordList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdChordList.Location = new System.Drawing.Point(12, 422);
      this.cmdChordList.Name = "cmdChordList";
      this.cmdChordList.Size = new System.Drawing.Size(70, 50);
      this.cmdChordList.TabIndex = 35;
      this.cmdChordList.Text = "Chord\r\nNames";
      this.cmdChordList.UseVisualStyleBackColor = true;
      this.cmdChordList.Click += new System.EventHandler(this.cmdChordList_Click);
      // 
      // cmdHelp
      // 
      this.cmdHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdHelp.Location = new System.Drawing.Point(631, 421);
      this.cmdHelp.Name = "cmdHelp";
      this.cmdHelp.Size = new System.Drawing.Size(70, 24);
      this.cmdHelp.TabIndex = 36;
      this.cmdHelp.Text = "Help";
      this.cmdHelp.UseVisualStyleBackColor = true;
      this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
      // 
      // lblTk
      // 
      this.lblTk.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTk.Location = new System.Drawing.Point(12, 7);
      this.lblTk.Name = "lblTk";
      this.lblTk.Size = new System.Drawing.Size(21, 16);
      this.lblTk.TabIndex = 38;
      this.lblTk.Text = "Tk";
      // 
      // lblCh
      // 
      this.lblCh.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCh.Location = new System.Drawing.Point(30, 7);
      this.lblCh.Name = "lblCh";
      this.lblCh.Size = new System.Drawing.Size(21, 16);
      this.lblCh.TabIndex = 39;
      this.lblCh.Text = "Ch";
      // 
      // lblCount
      // 
      this.lblCount.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCount.Location = new System.Drawing.Point(49, 7);
      this.lblCount.Name = "lblCount";
      this.lblCount.Size = new System.Drawing.Size(37, 16);
      this.lblCount.TabIndex = 40;
      this.lblCount.Text = "Count";
      // 
      // lblMinChar
      // 
      this.lblMinChar.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMinChar.Location = new System.Drawing.Point(85, 7);
      this.lblMinChar.Name = "lblMinChar";
      this.lblMinChar.Size = new System.Drawing.Size(25, 16);
      this.lblMinChar.TabIndex = 41;
      this.lblMinChar.Text = "Min";
      // 
      // lblMaxChar
      // 
      this.lblMaxChar.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMaxChar.Location = new System.Drawing.Point(109, 7);
      this.lblMaxChar.Name = "lblMaxChar";
      this.lblMaxChar.Size = new System.Drawing.Size(25, 16);
      this.lblMaxChar.TabIndex = 42;
      this.lblMaxChar.Text = "Max";
      // 
      // lblMaxNum
      // 
      this.lblMaxNum.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMaxNum.Location = new System.Drawing.Point(156, 7);
      this.lblMaxNum.Name = "lblMaxNum";
      this.lblMaxNum.Size = new System.Drawing.Size(25, 16);
      this.lblMaxNum.TabIndex = 44;
      this.lblMaxNum.Text = "Max";
      // 
      // lblMinNum
      // 
      this.lblMinNum.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMinNum.Location = new System.Drawing.Point(133, 7);
      this.lblMinNum.Name = "lblMinNum";
      this.lblMinNum.Size = new System.Drawing.Size(25, 16);
      this.lblMinNum.TabIndex = 43;
      this.lblMinNum.Text = "Min";
      // 
      // lblStyle
      // 
      this.lblStyle.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblStyle.Location = new System.Drawing.Point(205, 7);
      this.lblStyle.Name = "lblStyle";
      this.lblStyle.Size = new System.Drawing.Size(37, 16);
      this.lblStyle.TabIndex = 46;
      this.lblStyle.Text = "Style";
      // 
      // lblRng
      // 
      this.lblRng.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblRng.Location = new System.Drawing.Point(180, 7);
      this.lblRng.Name = "lblRng";
      this.lblRng.Size = new System.Drawing.Size(25, 16);
      this.lblRng.TabIndex = 45;
      this.lblRng.Text = "Rng";
      // 
      // lblPoly
      // 
      this.lblPoly.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPoly.Location = new System.Drawing.Point(282, 7);
      this.lblPoly.Name = "lblPoly";
      this.lblPoly.Size = new System.Drawing.Size(31, 16);
      this.lblPoly.TabIndex = 47;
      this.lblPoly.Text = "Poly";
      // 
      // lblPatch
      // 
      this.lblPatch.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPatch.Location = new System.Drawing.Point(312, 7);
      this.lblPatch.Name = "lblPatch";
      this.lblPatch.Size = new System.Drawing.Size(37, 16);
      this.lblPatch.TabIndex = 48;
      this.lblPatch.Text = "Patch";
      // 
      // lblTitle
      // 
      this.lblTitle.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTitle.Location = new System.Drawing.Point(462, 7);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new System.Drawing.Size(37, 16);
      this.lblTitle.TabIndex = 49;
      this.lblTitle.Text = "Title";
      // 
      // cmdShowAttributes
      // 
      this.cmdShowAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdShowAttributes.Location = new System.Drawing.Point(316, 422);
      this.cmdShowAttributes.Name = "cmdShowAttributes";
      this.cmdShowAttributes.Size = new System.Drawing.Size(70, 50);
      this.cmdShowAttributes.TabIndex = 50;
      this.cmdShowAttributes.Text = "Attributes\r\nAnd Text";
      this.cmdShowAttributes.UseVisualStyleBackColor = true;
      this.cmdShowAttributes.Click += new System.EventHandler(this.cmdShowAttributes_Click);
      // 
      // cmdShowNoteMap
      // 
      this.cmdShowNoteMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdShowNoteMap.ForeColor = System.Drawing.Color.Red;
      this.cmdShowNoteMap.Location = new System.Drawing.Point(463, 447);
      this.cmdShowNoteMap.Name = "cmdShowNoteMap";
      this.cmdShowNoteMap.Size = new System.Drawing.Size(65, 25);
      this.cmdShowNoteMap.TabIndex = 52;
      this.cmdShowNoteMap.Text = "NoteMap";
      this.cmdShowNoteMap.UseVisualStyleBackColor = true;
      this.cmdShowNoteMap.Click += new System.EventHandler(this.cmdShowNoteMap_Click);
      // 
      // cmdShowStrm
      // 
      this.cmdShowStrm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdShowStrm.ForeColor = System.Drawing.Color.Red;
      this.cmdShowStrm.Location = new System.Drawing.Point(463, 421);
      this.cmdShowStrm.Name = "cmdShowStrm";
      this.cmdShowStrm.Size = new System.Drawing.Size(65, 25);
      this.cmdShowStrm.TabIndex = 51;
      this.cmdShowStrm.Text = "Strm";
      this.cmdShowStrm.UseVisualStyleBackColor = true;
      this.cmdShowStrm.Click += new System.EventHandler(this.cmdShowStrm_Click);
      // 
      // frmSummary
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdClose;
      this.ClientSize = new System.Drawing.Size(713, 480);
      this.Controls.Add(this.cmdShowNoteMap);
      this.Controls.Add(this.cmdShowStrm);
      this.Controls.Add(this.cmdShowAttributes);
      this.Controls.Add(this.lblTitle);
      this.Controls.Add(this.lblPatch);
      this.Controls.Add(this.lblPoly);
      this.Controls.Add(this.lblStyle);
      this.Controls.Add(this.lblRng);
      this.Controls.Add(this.lblMaxNum);
      this.Controls.Add(this.lblMinNum);
      this.Controls.Add(this.lblMaxChar);
      this.Controls.Add(this.lblMinChar);
      this.Controls.Add(this.lblCount);
      this.Controls.Add(this.lblCh);
      this.Controls.Add(this.lblTk);
      this.Controls.Add(this.cmdShowKeys);
      this.Controls.Add(this.cmdHelp);
      this.Controls.Add(this.cmdChordList);
      this.Controls.Add(this.cmdShowCtlrDetails);
      this.Controls.Add(this.cmdShowCtlrTots);
      this.Controls.Add(this.cmdShowTempos);
      this.Controls.Add(this.clbTrk);
      this.Controls.Add(this.cmdShowTSigs);
      this.Controls.Add(this.cmdClose);
      this.Name = "frmSummary";
      this.Text = "Midi File Summary - Chord Cadenza";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLoadCSV_FormClosed);
      this.Load += new System.EventHandler(this.frmSummary_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button cmdClose;
    private System.Windows.Forms.Button cmdShowTSigs;
    private System.Windows.Forms.Button cmdShowKeys;
    private System.Windows.Forms.ListBox clbTrk;
    private System.Windows.Forms.Button cmdShowTempos;
    private System.Windows.Forms.Button cmdShowCtlrTots;
    private System.Windows.Forms.Button cmdShowCtlrDetails;
    private System.Windows.Forms.Button cmdChordList;
    private System.Windows.Forms.Button cmdHelp;
    private System.Windows.Forms.Label lblTk;
    private System.Windows.Forms.Label lblCh;
    private System.Windows.Forms.Label lblCount;
    private System.Windows.Forms.Label lblMinChar;
    private System.Windows.Forms.Label lblMaxChar;
    private System.Windows.Forms.Label lblMaxNum;
    private System.Windows.Forms.Label lblMinNum;
    private System.Windows.Forms.Label lblStyle;
    private System.Windows.Forms.Label lblRng;
    private System.Windows.Forms.Label lblPoly;
    private System.Windows.Forms.Label lblPatch;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Button cmdShowAttributes;
    private System.Windows.Forms.Button cmdShowNoteMap;
    private System.Windows.Forms.Button cmdShowStrm;
  }
}