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

namespace ChordCadenza.Forms {
  public partial class frmSwitch : Form, ITT {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    //internal static bool indLoaded = false;  //set if this has ever been loaded/shown
    //internal static bool FileLoaded = false;  //ini file loaded
    internal delegate void delegAction(bool down);
    private delegate void delegSwitchSyncopation();
    private delegate void delegVoid();
    //private delegate void delegShowChord(int[] chord);
    internal static SortedList<string, delegAction> Delegs = new SortedList<string, delegAction>();
    internal static SortedList<string, string> ActionToKey = new SortedList<string, string>();
    //internal static List<delegAction>[] KeyToActions = new List<delegAction>[13];   //[key][actionseq]
    internal static List<string>[] KeyToActions = new List<string>[13];   //[key][actionseq]
    //internal static bool frmSCSwitch = false;

    internal static void InitStatic() {
      InitDelegs();
      LoadData();
    }

    public frmSwitch() {
      InitializeComponent();
      #if !ADVANCED
        cmbAutoChords.Hide();
        lblAutoChords.Hide();

        cmbManChords.Hide();
        lblManChords.Hide();
      #endif
    }

    internal static List<string> KeyToActionsPedal {
      get { return KeyToActions[12]; }
    }

    private static void InitDelegs() {
      Delegs["KB Chord"] = delegate (bool down) {
        clsPlayKeyboard.PlayNearestChordNote = down;
      };
      ActionToKey.Add("KB Chord", "");

      Delegs["Sustain"] = delegate (bool down) {
        //if (P.frmStart.chkPlaySustain.Checked) return;
        clsPlay.clsSustain.PlayPedalStatic(down);
      };
      ActionToKey.Add("Sustain", "");

      //Delegs["Mode"] = delegate(bool down) {
      //  if (P.F == null || P.frmSC?.Play == null) return;
      //  if (!frmSCSwitch && !down) return;
      //  P.frmSC.SwitchPlayMode();
      //  P.frmSC.BeginInvoke(new clsPlay.delegResizeForm(P.frmSC.ResizeForm));
      //};
      //ActionToKey.Add("Mode", "");

      Delegs["Auto Chords"] = delegate (bool down) {
        //if (!(P.frmSC.Play is clsPlayKeyboard)) return;
        NewManChords(down, false);
      };
      ActionToKey.Add("Auto Chords", "");

      Delegs["Man Chords"] = delegate (bool down) {
        //if (!clsPlay.IsPlayKeyboard) return;
        //if (!(P.frmSC.Play is clsPlayKeyboard)) return;
        NewManChords(down, true);
      };
      ActionToKey.Add("Man Chords", "");

      Delegs["Sync"] = delegate (bool down) {
        if (down && P.F.AudioSync != null) P.F.AudioSync.MP3Player.SwitchKey();
      };
      ActionToKey.Add("Sync", "");

      Delegs["Prev Beat"] = delegate (bool down) {
        if (P.frmSC.Play != null) P.frmSC.Play.PrevSwitch(clsPlay.eSwitchInterval.Beat, down);
      };
      ActionToKey.Add("Prev Beat", "");

      Delegs["Next Beat"] = delegate (bool down) {
        if (P.frmSC.Play != null) P.frmSC.Play.NextSwitch(clsPlay.eSwitchInterval.Beat, down);
      };
      ActionToKey.Add("Next Beat", "");

      Delegs["Prev Bar"] = delegate (bool down) {
        if (P.frmSC.Play != null) P.frmSC.Play.PrevSwitch(clsPlay.eSwitchInterval.Bar, down);
      };
      ActionToKey.Add("Prev Bar", "");

      Delegs["Next Bar"] = delegate (bool down) {
        if (P.frmSC.Play != null) P.frmSC.Play.NextSwitch(clsPlay.eSwitchInterval.Bar, down);
      };
      ActionToKey.Add("Next Bar", "");

      Delegs["Prev Chord"] = delegate (bool down) {
        if (P.frmSC.Play != null) P.frmSC.Play.PrevSwitch(clsPlay.eSwitchInterval.Chord, down);
      };
      ActionToKey.Add("Prev Chord", "");

      Delegs["Next Chord"] = delegate (bool down) {
        if (P.frmSC.Play != null) P.frmSC.Play.NextSwitch(clsPlay.eSwitchInterval.Chord, down);
      };
      ActionToKey.Add("Next Chord", "");
    }

    private static void NewManChords(bool down, bool playactual) {
      if (down) {
        clsKeyTicks key = new clsKeyTicks("C", "major", 0);  //temp
        P.frmSC.Play.ManChords = new ChordCadenza.clsManChords(key, playactual);
      } else {
        if (P.frmSC.Play.ManChords != null) {
          clsManChords.ShowChord(null, null);
          P.frmSC.Play.ManChords = null;
        }
      }
    }

    private void frmSwitch_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      clsTT.LoadToolTips(this);
      //indLoaded = true;
      PopulateCmbSwitchKeys(grpSwitchKeys);
    }

    private static void LoadData() {
      //* populate 
      string filename = Cfg.SwitchIniFilePath;
      if (Cfg.NoIni) {
        SetDefaults();
      } else {
        //* Read ini file
        List<string> lines = Utils.ReadLinesIgnoreComments(filename);
        if (lines == null) {
          SetDefaults();
        } else {
          char[] delimeq = new char[] { '=' };
          int linenum;
          for (linenum = 0; linenum < lines.Count; linenum++) {
            string[] f = lines[linenum].Split(delimeq, 2);
            InitAction(f[0].Trim(), f[1].Trim());
          }
        Debug.WriteLine(Cfg.SwitchIniFilePath + " File loaded");
        }
      }
      CreateKeyToActions();
      //FileLoaded = true;
    }

    private static void SetDefaults() {
      InitAction("Sustain", "Pedal");
      InitAction("KB Chord", "Pedal");
      InitAction("Next Beat", "G");
      InitAction("Next Bar", "A");
      InitAction("Next Chord", "B");
      InitAction("Sync", "D");
    }

    private static void InitAction(string actiondesc, string key) {
      if (actiondesc.Length == 0 
      || !Delegs.ContainsKey(actiondesc)
      || !ActionToKey.ContainsKey(actiondesc))  {
#if DEBUG
        MessageBox.Show("Invalid Switch Action <" + actiondesc + "> found in Switch.ini");
#endif
        return;
      }
      if (key == "None") key = "";
      ActionToKey[actiondesc] = key;
    }

    private void PopulateCmbSwitchKeys(GroupBox grp) {
      foreach (Control ctl in grp.Controls) {
        if (ctl is ComboBox) {
          ComboBox cmb = (ComboBox)ctl;
          cmb.Items.Add("None");
          cmb.Items.AddRange(NoteName.MajKeyNames);
          cmb.Items.Add("Pedal");
          string actiondesc = (string)cmb.Tag;
          if (actiondesc.Length > 0) cmb.SelectedItem = ActionToKey[actiondesc];  
        }
      }
    }

    //private void SetChordSetLabels() {
    //  int cnt = 0;
    //  foreach (Control ctl in grpChordSet.Controls) {
    //    if (ctl is Label) {
    //      Label lbl = (Label)ctl;
    //      lbl.Text = P.ChordsSets[cnt++].Desc;
    //      if (cnt >= grpChordSet.Controls.Count) break;
    //      if (cnt >= P.ChordsSets.Length) break;
    //    }
    //  }
    //}

    private void RefreshCmbSwitchKeys() {
      foreach (Control ctl in grpSwitchKeys.Controls) {
        if (ctl is ComboBox) {
          ComboBox cmb = (ComboBox)ctl;
          string actiondesc = (string)cmb.Tag;
          if (actiondesc.Length > 0) {
            string key = ActionToKey[actiondesc];
            if (key == "") cmb.SelectedIndex = -1;  //no selection
            else cmb.SelectedItem = key;  //may be "None"
          }
        }
      }
    }

    private void cmbSustains_SelectedIndexChanged(object sender, EventArgs e) {
      clsPlay.clsSustain.PlayPedalStatic(false);  //switch off any existing (old) sustain
      cmb_SelectedIndexChanged(sender, e);
    }

    private void cmb_SelectedIndexChanged(object sender, EventArgs e) {
      ComboBox cmb = (ComboBox)sender;
      string txt = (string)cmb.Tag;
      if (cmb.SelectedIndex == -1 || (string)cmb.SelectedItem == "None") {
        ActionToKey[txt] = "";
      } else {
        ActionToKey[txt] = (string)cmb.SelectedItem;
      }
      CreateKeyToActions();
      //SetFrmSCChkBoxes();
    }

    //internal static void SetFrmSCChkBoxes() {
    //  if (P.F == null || P.frmSC == null) return;
    //  for (int key = 0; key <= 12; key++) {
    //    P.frmSC.SetChkSwitch(key, KeyToActions[key]);
    //  }
    //}

    internal static void SetAction(string action, string key) {
      //* called from frmStart (change style)
      clsPlay.clsSustain.PlayPedalStatic(false);  //switch off any existing (old) sustain
      if (key == "None") {
        ActionToKey[action] = "";
      } else {
        ActionToKey[action] = key;
      }
      CreateKeyToActions();
      if (P.frmSwitch != null) P.frmSwitch.RefreshCmbSwitchKeys();
    }

    private static void CreateKeyToActions() {
      //* (re)create KeyToActions from ActionsToKey
      for (int i = 0; i < KeyToActions.Length; i++) {
        KeyToActions[i] = new List<string>(2);
      }
      foreach (KeyValuePair<string, string> pair in ActionToKey) {
        if (pair.Value.Length == 0) continue;
        int key = (pair.Value == "Pedal") ? 12 : Array.IndexOf(NoteName.MajKeyNames, pair.Value);
        if (key < 0) {  //string not found
          LogicError.Throw(eLogicError.X072);
          key = 12;  //pedal
        }
        KeyToActions[key].Add(pair.Key);
      }
      //SetFrmSCChkBoxes();
    }

    private void cmdClose_Click(object sender, EventArgs e) {
      Close();
    }

    private void cmdSave_Click(object sender, EventArgs e) {
      string msg = SaveSwitchIniFile();
      //if (msg == "") MessageBox.Show("Switch File Saved");
      //else MessageBox.Show("Save Switch.ini failed: " + msg);
    }

    internal static string SaveSwitchIniFile() {
      //if (P.IgnoreIni) return "";
      return Utils.SaveFile(Cfg.SwitchIniFilePath, SaveFileSub);
    }

    private static void SaveFileSub(StreamWriter xsw) {
      foreach (KeyValuePair<string, string> pair in ActionToKey) {
        string val = (pair.Value == "") ? "None" : pair.Value;
        xsw.WriteLine(pair.Key + " = " + val);
        //* throw new TestException();
      }
    }

    private void frmSwitch_FormClosed(object sender, FormClosedEventArgs e) {
      P.frmSwitch = null;
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Help.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_SwitchKeys_Intro.htm");
    }

    //private void cmdLoad_Click(object sender, EventArgs e) {
    //  LoadData();
    //  foreach (Control ctl in grpSwitchKeys.Controls) {
    //    if (ctl is ComboBox) {
    //      ComboBox cmb = (ComboBox)ctl;
    //      string actiondesc = (string)cmb.Tag;
    //      if (actiondesc.Length > 0) cmb.SelectedItem = ActionToKey[actiondesc];
    //    }
    //  }
    //}

    //internal static bool indKBScale {
    //  get { return (ActionToKey["KBScale"] != ""); } 
    //}
  }
}
