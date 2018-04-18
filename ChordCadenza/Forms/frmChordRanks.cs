using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace ChordCadenza.Forms {
  public partial class frmChordRanks : Form {
    private bool Bypass_Event = true;
    private RadioButton[,] Opts;  //[rank, fseq]
    private RadioButton[] OptsMatch;  //[rank - 1]
    private RadioButton[] OptsNoMatch;  //[rank - 1]

    internal const int MaxRankMatchDefault = 2;
    internal const int MaxRankNoMatchDefault = 2;
    internal static int MaxRankMatch = MaxRankMatchDefault;   //1-4
    internal static int MaxRankNoMatch = MaxRankNoMatchDefault;  //1-4

    public frmChordRanks() {
      InitializeComponent();

      OptsMatch = new RadioButton[] { optMatch1, optMatch2, optMatch3, optMatch4 };
      OptsNoMatch = new RadioButton[] { optNoMatch1, optNoMatch2, optNoMatch3, optNoMatch4 };
      for (int r = 0; r < 4; r++) {
        OptsMatch[r].Tag = r + 1;
        OptsNoMatch[r].Tag = r + 1;
      }
      SetMaxRanks();

      Opts = new RadioButton[4, ChordAnalysis.FileSeqToTemplate.Count];
      int fseq = 0;
      foreach (ChordAnalysis.clsTemplate t in ChordAnalysis.FileSeqToTemplate.Values) {
        //* set up pans
        Panel pan = new Panel();
        pan.Size = new Size(panChords.ClientSize.Width, 19);
        pan.Location = new Point(0, 71 + fseq * 18);
        //pan.BorderStyle = BorderStyle.FixedSingle;  //temp
        panChords.Controls.Add(pan);

        //* set up opts
        for (int i = 0; i < 4; i++) {
          RadioButton opt = new RadioButton();
          opt.Size = new Size(14, 13);
          opt.Location = new Point(3 + i * 17, 3);
          opt.Checked = (t.Rank == (i + 1));
          Opts[i, fseq] = opt;
          string tagstr = (i + 1).ToString() + fseq.ToString();
          opt.Tag = tagstr;
          opt.Click += opt_Click;
          pan.Controls.Add(opt);
        }

        //* set up lblchords
        Label lblchord = new Label();
        lblchord.Font = new Font("Consolas", 8);
        lblchord.Location = new Point(72, 3);
        lblchord.AutoSize = false;
        lblchord.Size = new Size(50, 13);
        lblchord.Text = t.ToString();
        pan.Controls.Add(lblchord);
        //lblchord.BorderStyle = BorderStyle.FixedSingle;  //temp

        //* set up lblnotes
        Label lblnotes = new Label();
        lblnotes.Font = new Font("Consolas", 8);
        lblnotes.Location = new Point(135, 3);
        lblnotes.AutoSize = false;
        lblnotes.Size = new Size(115, 13);
        string notes = "";  //root note
        for (int j = 0; j < 12; j++) {
          if (t.PC[j]) notes += NoteName.ToSharpFlat(NoteName.GetNoteName(0, 0, j)) + ' ';
        }
        lblnotes.Text = notes;
        pan.Controls.Add(lblnotes);
        //lblnotes.BorderStyle = BorderStyle.FixedSingle;  //temp
        fseq++;
      }
      Bypass_Event = false;
    }

    private void frmChordRanks_Load(object sender, EventArgs e) {
    }

    private void SetMaxRanks() {
      for (int r = 0; r < 4; r++) {
        //if (r < P.frmCfgChords.MaxRankNoMatch) OptsMatch[r].Enabled = false;
        //if (r > P.frmCfgChords.MaxRankMatch) OptsNoMatch[r].Enabled = false;
        OptsMatch[r].Enabled = ((r + 1) >= MaxRankNoMatch);
        OptsNoMatch[r].Enabled = ((r + 1) <= MaxRankMatch);
      }
      OptsMatch[MaxRankMatch - 1].Checked = true;
      OptsNoMatch[MaxRankNoMatch - 1].Checked = true;
    }

    private void opt_Click(object sender, EventArgs e) {
      if (Bypass_Event) return;
      RadioButton opt = (RadioButton)sender;
      string tagstr = (string)opt.Tag;
      int rank = int.Parse(tagstr.Substring(0, 1));
      int fseq = int.Parse(tagstr.Substring(1, tagstr.Length - 1));
      ChordAnalysis.clsTemplate t = ChordAnalysis.FileSeqToTemplate.Values[fseq];
      t.Rank = rank;
    }

    internal static string Save() {
      try {
        using (StreamWriter sw = new StreamWriter(Cfg.ChordNamesRankIniFilePath, false)) {  //overwrite or create (not append)
          sw.Write(MaxRankMatch.ToString());
          sw.WriteLine(MaxRankNoMatch.ToString());
          foreach (ChordAnalysis.clsTemplate t in ChordAnalysis.FileSeqToTemplate.Values) {
            sw.Write(t.Rank.ToString());
          }
        }
      }
      catch (Exception exc) {
        return "Save Chords Ranks File failed: " + exc.Message;
      }
      return "";
    }

    //private void cmdLoad_Click(object sender, EventArgs e) {
    //  if (File.Exists(Cfg.ChordNamesRankIniFilePath)) ChordAnalysis.LoadRanks();
    //  else ChordAnalysis.SetRanksToDefault();
    //  UpdateOpts();
    //}

    private void cmdLoadDflts_Click(object sender, EventArgs e) {
      ChordAnalysis.SetRanksToDefault();
      UpdateOpts();
    }

    private void UpdateOpts() {
      Bypass_Event = true;
      SetMaxRanks();
      for (int i = 0; i < ChordAnalysis.FileSeqToTemplate.Count; i++) {
        ChordAnalysis.clsTemplate t = ChordAnalysis.FileSeqToTemplate.Values[i];
        Opts[t.Rank - 1, ChordAnalysis.FileSeqToTemplate.Keys[i]].Checked = true;
      }
      Bypass_Event = false;
    }

    private void cmdClose_Click(object sender, EventArgs e) {
      Close();
    }

    private void frmChordRanks_FormClosed(object sender, FormClosedEventArgs e) {
      P.frmChordRanks = null;
    }

    private void optMatch_Click(object sender, EventArgs e) {
      RadioButton opt = (RadioButton)sender;
      MaxRankMatch = (int)opt.Tag;
      SetMaxRanks();
    }

    private void optNoMatch_Click(object sender, EventArgs e) {
      RadioButton opt = (RadioButton)sender;
      MaxRankNoMatch = (int)opt.Tag;
      SetMaxRanks();
    }
  }
}
