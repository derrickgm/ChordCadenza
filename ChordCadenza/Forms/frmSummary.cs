using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ChordCadenza.Forms {
  internal partial class frmSummary : Form, IFormStream, IFormProjectName, ITT {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    public void FormStreamOnOff(bool on) { }
    internal frmSummary() {
      InitializeComponent();
      Forms.frmSC.ZZZSetPCKBEvs(this);
      //P.Forms.Add(this);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
      bool? ret = Forms.frmSC.StaticProcessCmdKey(ref msg, keyData);
      if (!ret.HasValue) return base.ProcessCmdKey(ref msg, keyData);
      return ret.Value;
    }

    internal clsFileStream CSVFileSummary;
    private List<Forms.frmShowList> ShowLists = new List<Forms.frmShowList>(10);

    internal void Populateclb(clsFileStream fsmm) {
      CSVFileSummary = fsmm;
      //int lasttrk = 1;
      clbTrk.Items.Clear();
      string fmt = "{0,2} {1,2} {2,5} {3,3} {4,3} {5,3} {6,3} {7,3} {8,-10} {9,6} {10,-24} {11,-40}";
      //txtTitle.Text = string.Format(fmt, "Tk", "Ch", "Count", "Min", "Max", "Min", "Max", "Rng", "Style", "Poly", "Patch", "Title");
      foreach (clsTrks.T trk in P.F.Chan.Next) {
        int chan = P.F.Chan[trk];
        string chantxt = "";
        string[] patchtxt = new string[] {""};
        if (chan >= 0) {
          chantxt = (chan + 1).ToString();
          if (chan == 9) {  // percussion
            patchtxt = new string[] {"*** channel 10 ***"};
          } else {
            clsFileStream fs = null;
            if (P.F.FSTrackMap != null) fs = P.F.FSTrackMap;
            if (fs != null) {
              patchtxt = fs.MidiCtlrs.GetPatchList(chan);
            }
          }
        }

        int diff = fsmm.TrkMaxPitch[trk] - fsmm.TrkMinPitch[trk];
        if (diff < 0) diff = 0;
        string poly = (fsmm.Poly[trk] < 0) ? "" : string.Format("{0:N2}", fsmm.Poly[trk]);
        string chord = (fsmm.ChordNeg[trk] < 0) ? "" : string.Format("{0:N2}", fsmm.ChordNeg[trk]);
        string txt = string.Format(fmt,
          trk.ToString(),
          chantxt,
          fsmm.OnCount[trk],
          (fsmm.OnCount[trk] == 0) ? "*" : NoteName.GetPitchStr(fsmm.TrkMinPitch[trk]),
          (fsmm.OnCount[trk] == 0) ? "*" : NoteName.GetPitchStr(fsmm.TrkMaxPitch[trk]),
          (fsmm.OnCount[trk] == 0) ? "*" : NoteName.GetPitchStrMidi(fsmm.TrkMinPitch[trk]),
          (fsmm.OnCount[trk] == 0) ? "*" : NoteName.GetPitchStrMidi(fsmm.TrkMaxPitch[trk]),
          diff,
          fsmm.TrkType[trk].ToString(),
          poly,
          patchtxt[0],
          fsmm.Title[trk].Trim(new char[] {'"'}));

        clbTrk.Items.Add(txt);
        for (int i = 1; i < patchtxt.Length; i++) {  //start at 2nd element
          clbTrk.Items.Add(string.Format(fmt,"","","","","", "", "","", "", "", patchtxt[i],""));
        }
      }
    }

    private void cmdClose_Click(object sender, EventArgs e) {
      Close();
    }

    private class clsBatchParams {
      internal clsBatchParams(clsKeysAlg.eProfile profile, clsKeysAlg.eAlg alg, int changepenalty) {
        Total = 0;
        Profile = profile;
        Alg = alg;
        ChangePenalty = changepenalty;
      }
      internal clsKeysAlg.eProfile Profile;
      internal clsKeysAlg.eAlg Alg;
      internal int ChangePenalty;
      internal float Total;
    }

    private void chkSyncTranspose_CheckedChanged(object sender, EventArgs e) {
      //lblTranspose.Enabled = !chkSyncTranspose.Checked;
      //nudTranspose.Enabled = !chkSyncTranspose.Checked;
    }

    private void frmLoadCSV_FormClosed(object sender, FormClosedEventArgs e) {
      foreach (Forms.frmShowList frm in ShowLists) frm.Close();
      P.F.frmSummary = null;
    }

    private void cmdShowTSigs_Click(object sender, EventArgs e) {
      NewFrmShowList(Forms.frmShowList.eList.TSigs);
    }

    private void NewFrmShowList(Forms.frmShowList.eList elist) {
      foreach (frmShowList f in ShowLists) {
        if (f.Type != elist) continue;
        if (!f.IsHandleCreated) continue;
        f.WindowState = FormWindowState.Normal;
        //f.Activate();
        Utils.FormAct(f);
        return;
      }
      frmShowList frm = new Forms.frmShowList(elist);
      ShowLists.Add(frm);
      Utils.FormAct(frm);
    }

    private void cmdShowKeys_Click(object sender, EventArgs e) {
      NewFrmShowList(Forms.frmShowList.eList.Keys);
    }

    //private void cmdAutoSync_Click(object sender, EventArgs e) {
    //  P.F.frmAutoSync = new frmAutoSync();
    //  P.F.frmAutoSync.Show();
    //}

    private void cmdChordList_Click(object sender, EventArgs e) {
      NewFrmShowList(Forms.frmShowList.eList.ChordList);
    }

    private void frmSummary_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      clsTT.LoadToolTips(this);
      if (P.F.FSTrackMap != null) P.F.frmSummary.Populateclb(P.F.FSTrackMap);
      SetFormTitle();
      #if !ADVANCED
        cmdShowCtlrDetails.Hide();
        cmdShowCtlrTots.Hide();
        cmdShowNoteMap.Hide();
        cmdShowStrm.Hide();
      #endif
    }

    public void SetFormTitle() {  //interface IFormProjectName
      SetFormTitle(P.F.Project);
    }

    public void SetFormTitle(clsProject project) {  //interface IFormProjectName
      Text = "Summary: " + project.MidiPath + " - Chord Cadenza";
    }

    private void cmdShowTempos_Click(object sender, EventArgs e) {
      NewFrmShowList(Forms.frmShowList.eList.Tempos);
    }

    private void cmdShowCtlrs_Click(object sender, EventArgs e) {
      NewFrmShowList(Forms.frmShowList.eList.CtlrTots);
    }

    private void cmdShowCtlrDetails_Click(object sender, EventArgs e) {
      NewFrmShowList(Forms.frmShowList.eList.CtlrDetails);
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Utils.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_Summary_Intro.htm");
    }

    private void cmdShowAttributes_Click(object sender, EventArgs e) {
      NewFrmShowList(Forms.frmShowList.eList.Attributes);
    }

    private void cmdShowStrm_Click(object sender, EventArgs e) {
      NewFrmShowList(Forms.frmShowList.eList.Strm);
    }

    private void cmdShowNoteMap_Click(object sender, EventArgs e) {
      NewFrmShowList(Forms.frmShowList.eList.NoteMap);
    }

    //private void cmdUpdateDesc_Click(object sender, EventArgs e) {
    //  if (P.F.CF != null) {
    //    Cursor.Current = Cursors.WaitCursor;
    //    P.F.CF.Description = txtCHPDesc.Text;
    //    string ret = P.frmSC.SaveChordFile();
    //    if (ret != "") MessageBox.Show("ChordFile not saved: " + ret);
    //    Cursor.Current = Cursors.Default;
    //  }
    //}

    //private void cmdShowSections_Click(object sender, EventArgs e) {
    //  NewFrmShowList(Forms.frmShowList.eList.Sections);
    //}

    //private void cmdDiff_Click(object sender, EventArgs e) {
    //  //if (P.F.CFActive == null || P.F.CFActive == P.F.CF) return;
    //  NewFrmShowList(Forms.frmShowList.eList.Diff);
    //}

    /*
    private void cmdBatch_Click(object sender, EventArgs e) {
      string indir = @"D:\1\Midi.Alg\Projects\";
      string outdir = @"D:\1\Midi.Alg\KeyStats\";
      clsKeysAlg.ErrorMargin = 1;  //match if adjacent key is the same
      string outname = "KeyAnalysis" + "_" + "Err" + clsKeysAlg.ErrorMargin;
      Stream xstream = new FileStream(outdir + outname + ".txt", FileMode.Create, FileAccess.Write);  //overwrite
      StreamWriter xsw = new StreamWriter(xstream);
      List<clsBatchParams> parms = new List<clsBatchParams>();

      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Default, clsKeysAlg.eAlg.Weighted, 0));
      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Default, clsKeysAlg.eAlg.Weighted, 40));
      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Default, clsKeysAlg.eAlg.Weighted, 60));
      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Default, clsKeysAlg.eAlg.Weighted, 70));
      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Default, clsKeysAlg.eAlg.Weighted, 80));

      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Jazz, clsKeysAlg.eAlg.Weighted, 0));
      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Jazz, clsKeysAlg.eAlg.Weighted, 40));
      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Jazz, clsKeysAlg.eAlg.Weighted, 60));
      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Jazz, clsKeysAlg.eAlg.Weighted, 70));
      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Jazz, clsKeysAlg.eAlg.Weighted, 80));

      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Special, clsKeysAlg.eAlg.Weighted, 0));
      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Special, clsKeysAlg.eAlg.Weighted, 40));
      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Special, clsKeysAlg.eAlg.Weighted, 60));
      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Special, clsKeysAlg.eAlg.Weighted, 70));
      parms.Add(new clsBatchParams(clsKeysAlg.eProfile.Special, clsKeysAlg.eAlg.Weighted, 80));

      string fmtpc = "{0,6:F1}";
      string fmtstr = "{0,6}";
      xsw.WriteLine("Error Margin: " + clsKeysAlg.ErrorMargin);

      xsw.Write("{0, -40}", "Algorithm");
      string desc = "???";
      foreach (clsBatchParams p in parms) {
        if (p.Alg == clsKeysAlg.eAlg.Flat) desc = "flat";
        else if (p.Alg == clsKeysAlg.eAlg.Weighted) desc = "wght";
        xsw.Write(fmtstr, desc);
      }
      xsw.WriteLine("");

      xsw.Write("{0, -40}", "Profile");
      desc = "???";
      foreach (clsBatchParams p in parms) {
        if (p.Profile == clsKeysAlg.eProfile.Default) desc = "dflt";
        else if (p.Profile == clsKeysAlg.eProfile.Jazz) desc = "jazz";
        else if (p.Profile == clsKeysAlg.eProfile.Special) desc = "spcle";
        xsw.Write(fmtstr, desc);
      }
      xsw.WriteLine("");

      xsw.Write("{0, -40}", "Change Penalty");
      foreach (clsBatchParams p in parms) xsw.Write(fmtpc, p.ChangePenalty);
      xsw.WriteLine("");
      xsw.WriteLine(new string('-', 40 + parms.Count * 6));  //6 is from fmtpc

      int filecnt = 0;
      foreach (string file in Directory.GetFiles(indir, "*.mid", SearchOption.AllDirectories)) {
        List<clsKeysAlg> keyslist = new List<clsKeysAlg>();
        clsFileStream csvfilesummary, csvfileconv;

        try {
          csvfilesummary = new clsFileStream(file, true, null, false);
          bool[] trkselect = new bool[CSVFileSummary.NumTrks];
          for (int trk = 0; trk < csvfilesummary.NumTrks; trk++) {
          //trkselect[trk] = csvfilesummary.TrkExists[trk];
          //if (trkselect[trk]) trkselect[trk] = (csvfilesummary.OnCount[trk] > 0);
            trkselect[trk] = (csvfilesummary.OnCount[trk] > 0);
          }
          csvfileconv = new clsFileStream(txtFileName.Text, false, trkselect, true);
        }
        catch (MidiFileException) {
          return;
        }

        clsKeysAlg.clsSegments segments = new clsKeysAlg.clsSegments(csvfileconv.NoteMap, csvfileconv);
        clsCF cf;
        try {
          cf = segments.LoadChordFile();
        }
        catch (ChordFileException) {
          cf = null;
        }
        //if (cf == null || !cf.KeysOK) {
        if (cf == null) {
          MessageBox.Show("File: " + file + " bypassed - no txt file!");
          continue;
        }
        clsKeysAlg.Segments = segments;

        string fname = Path.GetFileName(file);
        if (fname.Length > 40) fname = fname.Substring(0, 40);
        xsw.Write("{0,-40}", fname);
        for (int i = 0; i < parms.Count; i++) {
          clsBatchParams p = parms[i];
          float percent = new clsKeysAlg(p.Profile, p.Alg, p.ChangePenalty).GetPercent(cf);
          p.Total += percent;
          xsw.Write(fmtpc, percent);
        }
        xsw.WriteLine("");
        filecnt++;

      }
      xsw.Write("{0, -40}", "AVERAGES");
      foreach (clsBatchParams p in parms) xsw.Write(fmtpc, p.Total / filecnt);
      xsw.WriteLine("");
      xsw.Close();
      MessageBox.Show("Key Analysis Ended");
    }
    */
  }
}