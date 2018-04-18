using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using ExtensionMethods;

namespace ChordCadenza.Forms {
  public partial class frmCfgChords : Form {
    public frmCfgChords() {
      InitializeComponent();
      GetContainerCtls(this);
      foreach (Control ctl in Controls) {
        if (ctl is Panel | ctl is GroupBox) {  //assume no other conainer in container
          GetContainerCtls(ctl);
        }
      }
      SaveContainer(this);  //-> default values (as set in form designer)
      Bypass_CtlEvent = true;
      LoadChordIniFile();
      Bypass_CtlEvent = false;
    }

    private void frmCfgChords_Load(object sender, EventArgs e) {
      //only called if P.Advanced set
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      indFrmLoaded = true;
      #if !ADVANCED
        grpFactors.Hide();
        chkDebug.Hide();
        panTotals.Hide();
      #endif
      //SaveContainer(this);
      lblHalfBarSegment.Text = ((int)trkSegmentHalfBar.Value).ToString();
      lblBeatSegment.Text = ((int)trkSegmentBeat.Value).ToString();
      frmStart.SetNudAndTag(nudTrimInnerDD);  //initial value
      frmStart.SetNudAndTag(nudTrimOuterDD);  //initial value
    }

    internal bool indFrmLoaded = false;  //set if this is ever loaded/shown
    private clsChordSegs ChordSegs;
    private int MaxChordCountBar = 0, MaxChordCountHalfBar = 0, MaxChordCountBeat = 0;
    internal bool MaxHBWeight = false;
    internal bool MinHBWeight = false;
    internal bool MaxBeatWeight = false;
    internal bool MinBeatWeight = false;
    private SortedList<string, Control> Containers = new SortedList<string, Control>();
    private bool Bypass_CtlEvent = false;

    private List<int> ValTrks;
    private List<int> ValNuds;
    private List<bool> ValChks;
    private List<bool> ValOpts;
    private int ITrk, INud, IChk, IOpt;

    private void LoadContainer(Control container) {
      if (container is Form) { ITrk = 0; INud = 0; IChk = 0; IOpt = 0; }
      foreach (Control ctl in container.Controls) {
        if (ctl is Panel) LoadContainer(ctl);
        else if (ctl is GroupBox) LoadContainer(ctl);
        else if (ctl is TrackBar) ((TrackBar)ctl).Value = ValTrks[ITrk++];
        else if (ctl is NumericUpDown) ((NumericUpDown)ctl).Value = ValNuds[INud++];
        else if (ctl is CheckBox) ((CheckBox)ctl).Checked = ValChks[IChk++];
        else if (ctl is RadioButton) ((RadioButton)ctl).Checked = ValOpts[IOpt++];
      }
    }

    private void SaveContainer(Control container) {
      if (container == this) {
        ValTrks = new List<int>();
        ValNuds = new List<int>();
        ValChks = new List<bool>();
        ValOpts = new List<bool>();
      }
      foreach (Control ctl in container.Controls) {
        if (ctl is Panel) SaveContainer(ctl);
        else if (ctl is GroupBox) SaveContainer(ctl);
        else if (ctl is TrackBar) ValTrks.Add(((TrackBar)ctl).Value);
        else if (ctl is NumericUpDown) ValNuds.Add((int)((NumericUpDown)ctl).Value);
        else if (ctl is CheckBox) ValChks.Add(((CheckBox)ctl).Checked);
        else if (ctl is RadioButton) ValOpts.Add(((RadioButton)ctl).Checked);
      }
    }

    internal void NewFile() {
      ChordSegs = null;
      //trkSegmentBeat.Value = DfltBeatWeight;
      //trkSegmentHalfBar.Value = DfltHBWeight;
      lblHalfBarTotal.Text = " ";
      lblHalfBarMax.Text = "";
      lblBeatTotal.Text = " ";
      lblBeatMax.Text = "";
    }

    private void chkChordAnalysis_CheckedChanged(object sender, EventArgs e) {
      //nudMaxChordType.Enabled = chkChordAnalysis.Checked;
      //lblMaxChordType.Enabled = chkChordAnalysis.Checked;
      //nudMaxChordTypeNoMatch.Enabled = chkChordAnalysis.Checked;
      //lblMaxChordTypeNoMatch.Enabled = chkChordAnalysis.Checked;
    }

    private void cmdZeroRKD_Click(object sender, EventArgs e) {
      trkRKD.Value = 0;
    }

    private void cmdZeroChromatic_Click(object sender, EventArgs e) {
      trkChromatic.Value = 0;
    }

    private void cmdZeroChordLength_Click(object sender, EventArgs e) {
      trkChordLength.Value = 0;
    }

    private void cmdZeroChordType_Click(object sender, EventArgs e) {
      trkChordType.Value = 0;
    }

    private void cmdZeroBass_Click(object sender, EventArgs e) {
      trkBass.Value = 0;
    }

    //private void cmdDfltRKD_Click(object sender, EventArgs e) {
    //  trkRKD.Value = DfltRKD;
    //}

    //private void cmdDfltChromatic_Click(object sender, EventArgs e) {
    //  trkChromatic.Value = DfltChromatic;
    //}

    //private void cmdDfltChordLength_Click(object sender, EventArgs e) {
    //  trkChordLength.Value = DfltChordLength;
    //}

    //private void cmdDfltChordType_Click(object sender, EventArgs e) {
    //  trkChordType.Value = DfltChordType;
    //}


    private void trkSegmentHalfBar_ValueChanged(object sender, EventArgs e) {
      int val = (int)trkSegmentHalfBar.Value;
      if (val == trkSegmentHalfBar.Minimum) lblHalfBarSegment.Text = "Min";
      else if (val == trkSegmentHalfBar.Maximum) lblHalfBarSegment.Text = "Max";
      else lblHalfBarSegment.Text = val.ToString();
      MaxHBWeight = (val == trkSegmentHalfBar.Maximum);
      MinHBWeight = (val == trkSegmentHalfBar.Minimum);
    }

    private void trkSegmentBeat_ValueChanged(object sender, EventArgs e) {
      int val = (int)trkSegmentBeat.Value;
      if (val == trkSegmentBeat.Minimum) lblBeatSegment.Text = "Min";
      else if (val == trkSegmentBeat.Maximum) lblBeatSegment.Text = "Max";
      else lblBeatSegment.Text = val.ToString();
      MaxBeatWeight = (val == trkSegmentBeat.Maximum);
      MinBeatWeight = (val == trkSegmentBeat.Minimum);
    }

    private void cmdShowTotals_Click(object sender, EventArgs e) {
      if (Bypass_CtlEvent) return;
      if (P.F == null || P.F.frmChordMap == null) return;
      int chordcountbar, chordcounthalfbar, chordcountbeat;
      //* instantiate ChordSegs first time only
      bool indselectalign = true;
      if (ChordSegs == null) {
        //ChordSegs = new clsChordSegs(eAlign.Auto, false);
        P.F.frmChordMap.ApplyFilter(eAlign.Auto, false, out ChordSegs);
        MaxChordCountBar = ChordSegs.ChordCountBar;
        MaxChordCountHalfBar = ChordSegs.ChordCountHalfBar;
        MaxChordCountBeat = ChordSegs.ChordCountBeat;
        indselectalign = false;
      }
      ChordSegs.GetChords(false, indselectalign, out chordcountbar, out chordcounthalfbar, out chordcountbeat);
      lblHalfBarMax.Text = MaxChordCountHalfBar.ToString();
      lblHalfBarTotal.Text = chordcounthalfbar.ToString();
      lblBeatMax.Text = MaxChordCountBeat.ToString();
      lblBeatTotal.Text = chordcountbeat.ToString();
    }

    private void cmdInitCounts_Click(object sender, EventArgs e) {
      if (Bypass_CtlEvent) return;
      //ChordSegs = new clsChordSegs(eAlign.Auto, false);
      P.F.frmChordMap.ApplyFilter(eAlign.Auto, false, out ChordSegs);
      lblHalfBarMax.Text = "";
      lblBeatMax.Text = "";
    }

    private void frmCfgChords_VisibleChanged(object sender, EventArgs e) {
      NewFile();
    }

    private void cmdZeroHB_Click(object sender, EventArgs e) {
      trkSegmentHalfBar.Value = 0;
      //lblHalfBarSegment.Text = "0";
    }

    private void cmdZeroBeats_Click(object sender, EventArgs e) {
      trkSegmentBeat.Value = 0;
      //lblBeatSegment.Text = "0";
    }

    //private void cmdDfltHB_Click(object sender, EventArgs e) {
    //  trkSegmentHalfBar.Value = DfltHBWeight;
    //  //lblHalfBarSegment.Text = DfltHBWeight.ToString();
    //}

    //private void cmdDfltBeats_Click(object sender, EventArgs e) {
    //  trkSegmentBeat.Value = DfltBeatWeight;
    //  //lblBeatSegment.Text = DfltBeatWeight.ToString();
    //}

    private void frmCfgChords_FormClosing(object sender, FormClosingEventArgs e) {
      e.Cancel = true;
      Hide();
    }

    private void cmdClose_Click(object sender, EventArgs e) {
      Hide();
    }

    //private void cmdSave_Click(object sender, EventArgs e) {
    //  string msg = SaveChordIniFile();
    //  if (msg != "") MessageBox.Show("ChordCfg ini file save failed: " + msg);
    //}

    private void nudTrimInner_ValueChanged(object sender, EventArgs e) {
      if (Bypass_CtlEvent) return;
      //if (optAlignQuantize.Checked) return;  //segments only (Bar, Bar/2, Beat, ...)
      int dd, val;
      if (sender == nudTrimInnerDD) {
        val = frmStart.SetNudExp2(nudTrimInnerDD);
        if (val >= 0) dd = val; else return;
      } else {
        dd = (int)nudTrimInnerDD.Value;
      }
      P.F.frmChordMap.TrimInnerQI = P.F.MTime.DD2DI((int)nudTrimInnerNN.Value, dd);
      //ApplyFilter();
      //Refresh();
    }

    private void cmdLoadDflts_Click(object sender, EventArgs e) {
      LoadContainer(this);
    }

    private void nudTrimOuter_ValueChanged(object sender, EventArgs e) {
      if (Bypass_CtlEvent) return;
      //if (optAlignQuantize.Checked) return;  //segments only (Bar, Bar/2, Beat, ...)
      int dd, val;
      if (sender == nudTrimOuterDD) {
        val = frmStart.SetNudExp2(nudTrimOuterDD);
        if (val >= 0) dd = val; else return;
      } else {
        dd = (int)nudTrimOuterDD.Value;
      }
      P.F.frmChordMap.TrimOuterQI = P.F.MTime.DD2DI((int)nudTrimOuterNN.Value, dd);
      //ApplyFilter();
      //Refresh();
    }

    private void optAlignTrim_CheckedChanged(object sender, EventArgs e) {
      if (Bypass_CtlEvent) return;
      RadioButton opt = (RadioButton)sender;
      if (!opt.Checked) return;
      SetoptAlignTrim();
      //ApplyFilter();
      //Refresh();
    }

    //private void cmdLoad_Click(object sender, EventArgs e) {
    //  LoadChordIniFile();
    //}

    private void cmdRanks_Click(object sender, EventArgs e) {
      if (P.frmChordRanks == null) P.frmChordRanks = new Forms.frmChordRanks();
      Utils.FormAct(P.frmChordRanks); 
    }

    internal void SetoptAlignTrim() {
      if (optAlignTrimBar.Checked) P.F.frmChordMap.AlignTrim = eAlign.Bar;
      else if (optAlignTrimHalfBar.Checked) P.F.frmChordMap.AlignTrim = eAlign.HalfBar;
      else if (optAlignTrimBeat.Checked) P.F.frmChordMap.AlignTrim = eAlign.Beat;
      else {
        LogicError.Throw(eLogicError.X042);
        P.F.frmChordMap.AlignTrim = eAlign.Beat;
      }
    }

    //private void cmdDfltSwing_Click(object sender, EventArgs e) {
    //  trkSwing.Value = 50;
    //}

    internal string SaveChordIniFile() {
      //if (P.IgnoreIni) return "";
      //StreamWriter sw;
      try {
        using (StreamWriter sw = new StreamWriter(Cfg.ChordCfgIniFilePath, false)) {  //overwrite or create (not append)
          SaveContainer(sw, this);
          foreach (Control ctl in Controls) {
            if (ctl is Panel | ctl is GroupBox) SaveContainer(sw, ctl);  //assume no other conainer in container
          }
        }
      }
      catch (Exception exc) {
        return "Save ChordCfg.ini failed: " + exc.Message;
      }

      //sw.Close();
      return "";
    }

    private void SaveContainer(StreamWriter sw, Control container) {
      foreach (Control ctl in container.Controls) {
        if (ctl is TrackBar) {
          TrackBar trk = (TrackBar)ctl;
          sw.WriteLine(trk.Name + " = " + trk.Value);  //int

        } else if (ctl is NumericUpDown) {
          NumericUpDown nud = (NumericUpDown)ctl;
          sw.WriteLine(nud.Name + " = " + (int)nud.Value);  //decimal -> int

        } else if (ctl is CheckBox) {
          CheckBox chk = (CheckBox)ctl;
          sw.WriteLine(chk.Name + " = " + chk.Checked);  //bool
        }
      }
    }

    private void GetContainerCtls(Control container) {
      //* update lst with controls in container 
      foreach (Control ctl in container.Controls) {
        if (ctl is TrackBar || ctl is NumericUpDown || ctl is CheckBox) {
          Containers.Add(ctl.Name, ctl);
        }
      }
    }

    private void LoadChordIniFile() {
      //* load config chords parameters (NOT chord definitions) - optional
      //if (P.IgnoreIni) return;  
      if (!File.Exists(Cfg.ChordCfgIniFilePath)) return;
      try {
        using (StreamReader sr = new StreamReader(Cfg.ChordCfgIniFilePath)) {
          //string path = Cfg.ChordCfgIniFilePath;
          //sr = File.OpenText(path);
          string line;
          char[] delimequality = new char[] { '=' };
          while ((line = sr.ReadLine()) != null) {
            line = line.Trim();
            string[] f = line.Split(delimequality);
            for (int i = 0; i < f.Length; i++) f[i] = f[i].Trim();
            string name = f[0];
            if (!Containers.ContainsKey(name)) {
#if DEBUG
              MessageBox.Show("Read ChordsCfg Ini File error: key <" + name + "> not found");
#endif
              return;
            }
            Control ctl = Containers[name];

            if (ctl is TrackBar) {
              TrackBar trk = (TrackBar)ctl;
              trk.Value = int.Parse(f[1]);

            } else if (ctl is NumericUpDown) {
              NumericUpDown nud = (NumericUpDown)ctl;
              nud.Value = int.Parse(f[1]);

            } else if (ctl is CheckBox) {
              CheckBox chk = (CheckBox)ctl;
              chk.Checked = bool.Parse(f[1]);
            }
            //*throw new TestException();
          }
        }
      }
      catch (Exception exc) {
        MessageBox.Show("Read error on CfgChord.ini file: " + exc.Message);
        return;
      }
    }

    //private void mnuSaveMidiFileAs_Click(object sender, EventArgs e) {
    //  clsSaveMidiFile.SaveMidiFileAs();
    //}
  }
}
