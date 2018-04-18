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
  internal partial class frmCalcKeys : Form, IFormStream, IFormProjectName, ITT {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    //public bool HideOrShowForm { get { return true; } }
    public void FormStreamOnOff(bool on) {
      if (on) Close(); 
    }

    private void frmCalcKeys_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      clsTT.LoadToolTips(this);
      #if !ADVANCED
        grpProfile.Hide();
        grpMinorKeyType.Hide();
      #endif
    }

    //private clsFileStream CSVFileSummary;
    private clsTrks.Array<bool> TrkSelect;
    private List<clsKeysAlg> KeysList;
    private List<string> KeysLabels;
    internal clsCFPC CF = null;
    private clsKeysAlg.clsSegments Segments;
    private string FileName;
    //internal static bool FormShown = false;
    //private bool indTrace = false;

    internal frmCalcKeys(clsTrks.Array<bool> trkselect) {
      //* load and show frmCalcKeys
      InitializeComponent();
      chkChordFile.Checked = false;
      FileName = P.F.Project.MidiPath;
      TrkSelect = trkselect;
      lblTracksSelected.Text = Forms.frmChordMap.UpdateTrksSelected(TrkSelect);
      //P.F.indCalcKeys = true;
      SetFormTitle();
      //////cmdDebugSegs.Hide();  //show if debugging needed
      //////chkTrace.Hide();  //show if debugging needed
      string msg = Calculate(null, null); 
      if (msg != "") {
        MessageBox.Show("Calc Keys failed: " + msg);
        return;  //don't show form
      }
      //if (showdialog) ShowDialog(); else Utils.FormAct(this);
      ShowDialog();
      EnableApply(true);
      WindowState = FormWindowState.Normal;
    }

    //internal frmCalcKeys(clsFileStream fs, clsTrks.Array<bool> trkselect, int inertia) {
    //  //*  don't show the form - just calculate
    //  InitializeComponent();
    //  chkChordFile.Checked = false;
    //  TrkSelect = trkselect;
    //  lblTracksSelected.Text = Forms.frmChordMap.UpdateTrksSelected(TrkSelect);
    //  P.F.indCalcKeys = true;
    //  string msg = Calculate(fs, inertia);
    //  if (msg != "") {
    //    MessageBox.Show("CalcKeys failed: " + msg);
    //    return;
    //  }
    //  Apply();
    //}

    internal frmCalcKeys() {  //use ChordFile
      //* load and show frmCalcKeys
      InitializeComponent();
      chkChordFile.Checked = true;
      lblTracksSelected.Text = "";
      //P.F.indCalcKeys = false;
      SetFormTitle();
      string msg = Calculate(null, null);
      if (msg != "") {
        MessageBox.Show("Calc Keys failed: " + msg);
        return;  //don't show form
      }
      //ShowDialog();
      EnableApply(true);
      WindowState = FormWindowState.Normal;
      //Activate();
      Utils.FormAct(this);
    }

    public void SetFormTitle() {  //interface IFormProjectName
      SetFormTitle(P.F.Project);
    }

    public void SetFormTitle(clsProject project) {  //interface IFormProjectName

      Text = "Calculate Keys: " + FileName + " - Chord Cadenza";
    }

    private void cmdCalculate_Click(object sender, EventArgs e) {
      string msg = Calculate(null, null);
      if (msg != "") {
        MessageBox.Show("CalcKeys failed: " + msg);
        EnableApply(false);
      }
      EnableApply(true);
    }

    private void EnableApply(bool ena) {
      //cmdApply0.Enabled = ena;
      //cmdApply1.Enabled = ena;
      //cmdApply2.Enabled = ena;
      //cmdApply3.Enabled = ena;
      //cmdApply4.Enabled = ena;
      //cmdApply5.Enabled = ena;
      cmdApply.Enabled = ena;
      cmdOK.Enabled = ena;
    }

    private void CalcMinorScores() {
      //* calculate minor key scores for different minor key types
      //* use currently selected inertia(penalty), alg, profile 

      //* calculate totals of each pitchclass for segements(bars) in a minor key
      int[] pctots = new int[12];  //pitchclass totals
      for (int i = 0; i < 12; i++) pctots[i] = 0;
      int index = (KeysList.Count > 1) ? GetInertia() : 0;
      if (KeysList.Count == 0) {
        LogicError.Throw(eStopError.Y003);  //unable to reproduce this, but it did happen!
        return;
      }
      clsKeysAlg keysalg = KeysList[index];
      clsKeysAlg.clsSegments segs = clsKeysAlg.Segments;
      for (int segnum = 0; segnum < keysalg.Keys.Count; segnum++) {
        clsKeyTicks key = keysalg.Keys[segnum];
        if (key != null && key.Scale == "minor") {
          if (segnum != key.BBT.Bar) {
            LogicError.Throw(eLogicError.X094);
            break;
          }
          for (int pc = 0; pc < 12; pc++) pctots[pc] += segs.Segs[segnum][pc];
        }
      }

      //* calculate totals of l/l- t/t- combinations
      int totharmonic = pctots[8] + pctots[11];  //l- t
      int totmelup = pctots[9] + pctots[11];  //l t
      int totmeldown = pctots[8] + pctots[10];  //l- t-
      int totspecial = pctots[9] + pctots[10];  //l t-

      lblTotHarmonic.Text = totharmonic.ToString();
      lblTotMelUp.Text = totmelup.ToString();
      lblTotMelDown.Text = totmeldown.ToString();
      lblTotSpecial.Text = totspecial.ToString();

      eMinorKeyType maxtottype = eMinorKeyType.Harmonic;
      int maxtot = totharmonic;

      if (totmelup > maxtot) {
        maxtottype = eMinorKeyType.MelodicUp;
        maxtot = totmelup;
      }

      if (totmeldown > maxtot) {
        maxtottype = eMinorKeyType.MelodicDown;
        maxtot = totmeldown;
      }

      if (totspecial > maxtot) {
        maxtottype = eMinorKeyType.Special;
        maxtot = totspecial;
      }

      FontStyle fontstyle;

      fontstyle = (maxtottype == eMinorKeyType.Harmonic) ? FontStyle.Bold : FontStyle.Regular;
      lblTotHarmonic.Font = new Font(lblTotHarmonic.Font, fontstyle);

      fontstyle = (maxtottype == eMinorKeyType.MelodicUp) ? FontStyle.Bold : FontStyle.Regular;
      lblTotMelUp.Font = new Font(lblTotMelUp.Font, fontstyle);

      fontstyle = (maxtottype == eMinorKeyType.MelodicDown) ? FontStyle.Bold : FontStyle.Regular;
      lblTotMelDown.Font = new Font(lblTotMelDown.Font, fontstyle);

      fontstyle = (maxtottype == eMinorKeyType.Special) ? FontStyle.Bold : FontStyle.Regular;
      lblTotSpecial.Font = new Font(lblTotSpecial.Font, fontstyle);

      if (chkUseHighestMinorKeyType.Checked) P.frmStart.MinorKeyType = maxtottype;
    }


    private string Calculate(clsFileStream fs, int? inertia) {
      KeysList = new List<clsKeysAlg>();
      KeysLabels = new List<string>();
      clsKeysAlg.eAlg alg = clsKeysAlg.eAlg.Weighted;
      if (optAlgFlat.Checked) alg = clsKeysAlg.eAlg.Flat;
      //clsFileStream csvfileconv;
      clsNoteMap notemap;

      if (chkChordFile.Checked) {
        if (P.F.CF?.Evs == null || P.F.CF.Evs.Count == 0) return "Empty ChordFile"; 
        notemap = P.F.CF.NoteMap;
      } else {
        if (fs != null) notemap = fs.NoteMap;
        else {
          try {
            notemap = (new clsFileStream(FileName, TrkSelect, true, false, true)).NoteMap;
          }
          catch (MidiFileException exc) {
            return "MidiFileException: " + exc.Message;
          }
        }
      }

      clsKeysAlg.eProfile profile = clsKeysAlg.eProfile.Default;
      if (optProfileJazz.Checked) profile = clsKeysAlg.eProfile.Jazz;
      else if (optProfileSpecial.Checked) profile = clsKeysAlg.eProfile.Special;
      Segments = new clsKeysAlg.clsSegments(notemap);
      if (Segments.indEmpty) return "Empty Tracks";
      CF = (chkLoadTxt.Checked && P.F.CF != null) ? CF = P.F.CF : null;
      KeysLabels.Add("Txt");  //leave in, even if text not present
      clsKeysAlg.Alg = alg;
      //clsKeysAlg.Trace = indTrace;
      clsKeysAlg.Segments = Segments;

      if (inertia.HasValue) {
        AddToKeysList(alg, profile, inertia.Value);
      } else {
        AddToKeysList(alg, profile, 0);
        AddToKeysList(alg, profile, 30);
        AddToKeysList(alg, profile, 50);
        AddToKeysList(alg, profile, 70);
        AddToKeysList(alg, profile, 90);
        AddToKeysList(alg, profile, 110);
      }

      CalcMinorScores();

      PopulatelvMod(true);
      //cmdDebugSegs.Enabled = true;
      return "";
    }

    private void AddToKeysList(clsKeysAlg.eAlg alg, clsKeysAlg.eProfile profile, int penalty) {
      clsKeysAlg keysalg = new clsKeysAlg(profile, alg, penalty);
      KeysList.Add(keysalg);
      KeysLabels.Add(penalty.ToString());
    }

    private void PopulatelvMod(bool headers) {
      //* create table of key strings
      string[,] txt = new string[KeysList[0].Keys.Count, KeysList.Count + 1];
      string[,] txtx = new string[KeysList[0].Keys.Count, KeysList.Count + 1];
      for (int seg = 0; seg < KeysList[0].Keys.Count; seg++) {
        if (CF != null) {
          txt[seg, 0] = GetFileKey(seg).KeyStrShort;
          txtx[seg, 0] = GetFileKey(seg).KeyStrShort;
        }
        for (int i = 0; i < KeysList.Count; i++) {  //for each algorithm type 
          clsKeyTicks key = KeysList[i].Keys[seg];
          if (key != null) {
            if (seg != key.BBT.Bar) LogicError.Throw(eLogicError.X034);
            txt[seg, i + 1] = key.KeyStrShort;
            txtx[seg, i + 1] = key.KeyStrShort;
          }
        }
      }

      //* replace repeated keys with "*"
      for (int i = 0; i < KeysList.Count + 1; i++) {  //txtfile and each algorithm type
        //for (int seg = 1; seg < KeysList[0].Keys.Count - 1; seg++) {
        //  if (txtx[seg, i] == txtx[seg - 1, i] && txtx[seg, i] == txtx[seg + 1, i]) {
        //    txt[seg, i] = "*";
        //  }
        //}
        for (int seg = 1; seg < KeysList[0].Keys.Count; seg++) {
          if (txtx[seg, i] == txtx[seg - 1, i]) {
            txt[seg, i] = "*";
          }
        }
      }

      //* load lvmod
      lvMod.BeginUpdate();
      lvMod.Clear();
      if (headers) {
        lvMod.Columns.Add("Bar", 30);
        foreach (string lbl in KeysLabels) {
          lvMod.Columns.Add(lbl, 40);
        }
      }
      for (int seg = 0; seg < KeysList[0].Keys.Count; seg++) {
        List<string> lvstr = new List<string>();
        lvstr.Add((seg + 1).ToString());  //bar num
        lvstr.Add(txt[seg, 0]);  //txt file
        for (int i = 0; i < KeysList.Count; i++) {  //for each algorithm type
          lvstr.Add(txt[seg, i + 1]);
        }
        lvMod.Items.Add(new ListViewItem(lvstr.ToArray()));  //add segment
      }
      lvMod.EndUpdate();
    }

    //  for (int seg = 0; seg < KeysList[0].Keys.Count; seg++) {
    //    clsKey txtkey = null, key;
    //    string star = "";
    //    List<string> lvstr = new List<string>();
    //    string line = "";
    //    lvstr.Add((seg + 1).ToString());
    //    if (CF != null) {
    //      txtkey = GetFileKey(seg);
    //      lvstr.Add(txtkey.KeyStrShort);
    //      if (indTrace) {
    //        line += string.Format("{0, 4} {1, -4}", seg + 1, txtkey.KeyStrShort);
    //      }
    //    }
    //    for (int i = 0; i < KeysList.Count; i++) {  //for each algorithm type
    //      key = KeysList[i].Keys[seg];
    //      if (key == null) {
    //        lvstr.Add("");
    //      } else {
    //        if (seg != key.BBT.Bar) {
    //          LogicError.Throw(eLogicError.X034);
    //          lvstr.Add("");
    //        }
    //        if (CF != null) {
    //          if (!txtkey.Equiv(key)) star = " *"; else star = "";
    //        }
    //        lvstr.Add(key.KeyStrShort + star);
    //      }
    //      if (indTrace) {
    //        line += string.Format("{0, -4}", key.KeyStrShort + star);
    //      }
    //    }
    //    lvMod.Items.Add(new ListViewItem(lvstr.ToArray()));  //add segment
    //  }
    //  lvMod.EndUpdate();

    private clsKeyTicks GetFileKey(int seg) {
      clsKeyTicks txtkey;
      int ticks = (new clsMTime.clsBBT(seg, 0, 0)).Ticks;
      txtkey = P.F.Keys[ticks];
      return txtkey;
    }

    private void cmdApply_Click(object sender, EventArgs e) {
      Apply();
    }

    private void Apply() {
      if (KeysList == null) return;
      //if (P.F == null || P.F.CF == null) {
      //  MessageBox.Show("Apply failed - showchords/text file not loaded");
      //  return;
      //}
      if (P.F == null) {
        MessageBox.Show("Apply failed - no midifile or chordfile loaded");
        return;
      }

      //P.F.UndoRedoKeys.Update();

      clsKeysTicks keys = null;
      int index = (KeysList.Count > 1) ? GetInertia() : 0;
      clsKeyTicks prevkey = null;
      foreach (clsKeyTicks key in KeysList[index].Keys) {
        if (key == null) continue;
        if (key.IsEquiv(prevkey)) continue;  //same key, different BBT
        clsKeyTicks newkey = new clsKeyTicks(key);
        //if (P.F.CFTxt.Transpose_File != 0) newkey = newkey.Transpose(-P.F.CFTxt.Transpose_File);
        if (keys == null) {
          newkey.Ticks = 0;  //first key
          keys = new clsKeysTicks(newkey);
        } else {
          keys.Keys.Add(newkey);
        }
        prevkey = key;
      }
      //P.F.KeysAlt = keys;
      //SwitchKeys();
      P.F._CFKeys = keys;

      //P.F.CF.SyncEvsToKeys();
      //P.F.CF.UndoRedoCF.Update();
      //P.F.CF.TransposeMidi(0);  //to force enharmonic changes (spelling) 
      P.F.CF?.SetNoteMapFileChanged();

      if (P.frmSC != null) P.frmSC.Refresh();
      if (P.F.frmChordMap != null) P.F.frmChordMap.RefreshAll();
    }

    private int GetInertia() {
      if (optPenalty0.Checked) return 0;
      if (optPenalty30.Checked) return 1;
      if (optPenalty50.Checked) return 2;
      if (optPenalty70.Checked) return 3;
      if (optPenalty90.Checked) return 4;
      if (optPenalty110.Checked) return 5;
      LogicError.Throw(eLogicError.X083);
      return 0;
    }

    //internal static void SwitchKeys() {
    //  if (P.F.Keys == null || P.F.KeysAlt == null) return;
    //  clsKeys keys = P.F._CFKeys;
    //  P.F._CFKeys = P.F.KeysAlt;
    //  P.F.KeysAlt = keys;
    //  //indKeysCalc = !indKeysCalc;
    //  P.F.CF.SyncEvsToKeys();
    //  if (P.frmSC != null) P.frmSC.Refresh();
    //  if (P.F.frmNoteMap != null) P.F.frmNoteMap.ShowKeys();
    //}

    //private void cmdLoadChordFile_Click(object sender, EventArgs e) {
    //  string csvname = FileName;
    //  string filename = csvname.Substring(0, csvname.Length - 4) + ".chp";
    //  if (!File.Exists(filename)) return;
    //  //frmStart.CloseMidiWindows();
    //  P.CloseFrm(P.F.frmShowChords);
    //  P.F.frmShowChords = new frmShowChords(frmStart.frmShowChordsSettings);
    //  if (!P.F.frmShowChords.InitShowChordsTxt(filename)) {
    //    P.F.frmShowChords = null;
    //    return;
    //  }
    //  P.F.frmShowChords.cmdPlayCtl.Enabled = true;
    //  P.F.frmShowChords.cmdPlayCtlNoPerc.Enabled = true;
    //  P.F.frmShowChords.cmdStartPlay.Enabled = true;
    //}

    private void cmdDebug_Click(object sender, EventArgs e) {  
      //* not nornmally in use
      int seglo = 0; 
      int seghi = Segments.Segs.Count; 
      int[] tot = new int[12];
      for (int s = seglo; s < seghi; s++) {
        int[] seg = Segments.Segs[s];
        for (int pmidi = 0; pmidi < 12; pmidi++) {
          int p = pmidi;
          //if (CF != null && optKeysText.Checked) p = (pmidi - CF.Transpose_File + 12).Mod12();
          tot[p] += seg[pmidi];  //absolute pitch
        }
      }
      PrintSegs(seglo, seghi, tot);
    }

    private void PrintSegs(int seglo, int seghi, int[] tot) {
      int maxtot = tot.Max();
      Debug.WriteLine("SegCount for " + FileName
        + " From Seg:" + (seglo + 1)
        + " To Seg:" + (seghi + 1));
      string msg = "Abs: ";
      for (int p = 0; p < 12; p++) {
        tot[p] = (tot[p] * 100) / maxtot;
        msg += NoteName.ToSharpFlat(NoteName.GetNoteName(0, 0, p)) + ": " + tot[p] + "  ";
      }
      Debug.WriteLine(msg);
      if (CF != null) {  //print rel
        clsKeyTicks txtkey = GetFileKey(seglo);  //assume seglo is key for this range 
        msg = NoteName.ToSharpFlat(txtkey.KeyStrShort) + ": ";
        for (int p = 0; p < 12; p++) {
          int i = (p + txtkey.KeyNote).Mod12();
          msg += NoteName.Solfa.Substring(p * 2, 2) + ": " + tot[i] + "  ";
        }
        Debug.WriteLine(msg);
      }
    }

    private void frmCalcKeys_FormClosed(object sender, FormClosedEventArgs e) {
      P.F.frmCalcKeys = null;
    }

    private void cmdOK_Click(object sender, EventArgs e) {
      Apply();
      Close();
    }

    //private void cmdClose_Click(object sender, EventArgs e) {
    //  Close();
    //}

    private void optPenalty_CheckedChanged(object sender, EventArgs e) {
      RadioButton opt = (RadioButton)sender;
      if (opt.Checked) CalcMinorScores();  //no need to call if switched off
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Help.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_CalcKeys_Intro.htm");
    }

    private void chkChordFile_CheckedChanged(object sender, EventArgs e) {
      lblTracksSelected.Text = (chkChordFile.Checked) ? "" : Forms.frmChordMap.UpdateTrksSelected(TrkSelect);
    }
  }
}
