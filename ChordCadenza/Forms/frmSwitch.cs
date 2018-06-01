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

    internal delegate void delegAction(bool down);
    private delegate void delegSwitchSyncopation();
    private delegate void delegVoid();

    private delegate void delegSetPCKBVel(int val);
    private static delegSetPCKBVel dSetPCKBVelfrmSC;
    private static delegSetPCKBVel dSetPCKBVelfrmPCKBIn;

    internal delegate void delegSetChkSwitch(CheckBox chk, bool down);
    internal static delegSetChkSwitch dSetChkSwitch;

    internal static SortedList<string, delegAction> Delegs = new SortedList<string, delegAction>();
    internal static SortedList<string, bool> KeyDowns = new SortedList<string, bool>();
    internal static SortedList<string, string> ActionToKey = new SortedList<string, string>();
    internal static List<string>[] KeyToActions = new List<string>[13];   //[key][actionseq]
    internal static string[] CmbItems;
    private static string Fmt = "{0,-5}     {1,-3}";

    internal static void InitStatic() {
      CmbItems = new string[13];
      NoteName.MajKeyNames.CopyTo(CmbItems, 0);
      for (int i = 0; i < 12; i++) {
        CmbItems[i] = string.Format(Fmt, NoteName.MajKeyNames[i], "F" + (i + 1));
        CmbItems[i] = CmbItems[i].TrimEnd();
      }
      CmbItems[12] = string.Format(Fmt, "Pedal", "Space");
      //CmbItems[0] = "C/Shift";
      //CmbItems[2] = "D/Alt";
      //CmbItems[12] = "Pedal/Ctrl"; 
      InitDelegs();
      InitKeyDowns();
      LoadData();
      dSetPCKBVelfrmSC = new delegSetPCKBVel(SetPCKBVelfrmSC);
      dSetPCKBVelfrmPCKBIn = new delegSetPCKBVel(SetPCKBVelfrmPCKBIn);
      dSetChkSwitch = new delegSetChkSwitch(SetChkSwitch);
    }

    public frmSwitch() {
      InitializeComponent();
      Forms.frmSC.ZZZSetPCKBEvs(this);
      #if !ADVANCED
        cmbAutoChords.Hide();
        lblAutoChords.Hide();

        cmbManChords.Hide();
        lblManChords.Hide();
      #endif
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
      bool? ret = Forms.frmSC.StaticProcessCmdKey(ref msg, keyData);
      if (!ret.HasValue) return base.ProcessCmdKey(ref msg, keyData);
      return ret.Value;
    }

    internal static List<string> KeyToActionsPedal {
      get { return KeyToActions[12]; }
    }

    private static void InitKeyDowns() {
      foreach (KeyValuePair<string, delegAction> pair in Delegs) {
        KeyDowns.Add(pair.Key, false);  //assume keyup initially 
      }
    }

    internal static void ExecSwitch(int keyswitch, bool down) {
      foreach (string action in KeyToActions[keyswitch]) {
        if (action != "") {
          if (CheckRepeatedState(action, down)) continue;
          Delegs[action](down);
          if (action == "Sustain") {
            P.frmSC.BeginInvoke(dSetChkSwitch, P.frmSC.chkSwitchSustain, down);
          } else if (action == "KB Chord") {
            P.frmSC.BeginInvoke(dSetChkSwitch, P.frmSC.chkSwitchKBChord, down);
          }
        }
      }
    }

    internal static bool CheckRepeatedState(string action, bool down) {
      //* return true if unwanted repeated key
      bool repeat = (P.PCKB != null 
        && down 
        && !action.StartsWith("PCKB Vel") 
        && KeyDowns[action]);
      KeyDowns[action] = down;
      return repeat;  //not repeated key on PCKB (except velocity key)
    }

    internal static void SetChkSwitch(CheckBox chk, bool down) {
      P.frmSC.Bypass_Event = true;
      chk.Checked = down;
      P.frmSC.Bypass_Event = false;
    }

    private static void InitDelegs() {
      Delegs["KB Chord"] = delegate (bool down) {
        clsPlayKeyboard.PlayNearestChordNote = down;
      };
      ActionToKey.Add("KB Chord", "");

      Delegs["Sustain"] = delegate (bool down) {
        //if (P.frmStart.chkPlaySustain.Checked) return;
        clsPlay.clsSustain.PlayPedalStatic(down);
        MidiMon.Sustain(down);
        if (!down) MidiMon.CheckAllOff();
      };
      ActionToKey.Add("Sustain", "");

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

      Delegs["PCKB Vel Down"] = delegate (bool down) {
        if (down) SetPCKBVelUpDown(-4);
      };
      ActionToKey.Add("PCKB Vel Down", "");

      Delegs["PCKB Vel Up"] = delegate (bool down) {
        if (down) SetPCKBVelUpDown(+4);
      };
      ActionToKey.Add("PCKB Vel Up", "");
    }

    private static void SetPCKBVelUpDown(int incr) {
      if (P.PCKB == null) return;  //only if PCKB switch key (function key)
      Cfg.PCKBVel += incr;
      //* set range 1 - 127 (0 may be confused with OFF ev)
      Cfg.PCKBVel  = Math.Min(127, Math.Max(1, Cfg.PCKBVel)); 
      P.frmSC.BeginInvoke(dSetPCKBVelfrmSC, Cfg.PCKBVel);
      P.frmPCKBIn?.BeginInvoke(dSetPCKBVelfrmPCKBIn, Cfg.PCKBVel);
    }

    private static void SetPCKBVelfrmSC(int val) {
      P.frmSC.trkPCKBVel.Value = val;
    }

    private static void SetPCKBVelfrmPCKBIn(int val) {
      P.frmPCKBIn.trkVel.Value = val;
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
      SetDefaults();
      string filename = Cfg.SwitchIniFilePath;
      if (!Cfg.NoIni) {
        //* Read ini file
        List<string> lines = Utils.ReadLinesIgnoreComments(filename);
        if (lines != null) {
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
      InitAction("Sustain", string.Format(Fmt, "Pedal", "Space"));
      InitAction("KB Chord", string.Format(Fmt, "Pedal", "Space"));
      InitAction("Next Beat", string.Format(Fmt, "G", "F8"));
      InitAction("Next Bar", string.Format(Fmt, "A", "F10"));
      InitAction("Next Chord", string.Format(Fmt, "B", "F12"));
      InitAction("Sync", string.Format(Fmt, "D", "F3"));
      InitAction("PCKB Vel Down", string.Format(Fmt, "A", "F10"));
      InitAction("PCKB Vel Up", string.Format(Fmt, "Bb", "F11"));
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
      ActionToKey[actiondesc] = key.TrimEnd();
    }

    private void PopulateCmbSwitchKeys(GroupBox grp) {
      foreach (Control ctl in grp.Controls) {
        if (ctl is ComboBox) {
          ComboBox cmb = (ComboBox)ctl;
          cmb.Items.Add("None");
          cmb.Items.AddRange(CmbItems);
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
        //int key = (pair.Value == "Pedal") ? 12 : Array.IndexOf(NoteName.MajKeyNames, pair.Value);
        int key = Array.IndexOf(CmbItems, pair.Value);
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

    //private void cmdSave_Click(object sender, EventArgs e) {
    //  string msg = SaveIniFile();
    //  //if (msg == "") MessageBox.Show("Switch File Saved");
    //  //else MessageBox.Show("Save Switch.ini failed: " + msg);
    //}

    internal static string SaveIniFile() {
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
      Utils.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_SwitchKeys_Intro.htm");
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
