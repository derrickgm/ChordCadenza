using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ChordCadenza {
  internal class clsPCKB {
    internal static Keys[] KB = new Keys[48];  //current
    internal static Keys[] KB24 = new Keys[48];  //2 octs 4 rows
    internal static Keys[] KB44 = new Keys[48];  //4 octs 4 rows
    private List<Keys> KeysDown = new List<Keys>(6);
    private SortedList<Keys, byte> KBToPitch;
    private string[] Descs;
    internal string[] Chars;
    private static Dictionary<string, string> DescToChars = new Dictionary<string, string>();
    private static Dictionary<string, string> CharsToChars = new Dictionary<string, string>();
    private int? MouseKB = null; //only one kb active at any one time

    private delegate void delegRefresh();
    private delegate void delegSetNoteName(int note);
    private delegate void delegClosePCKBIn();
    private delegSetNoteName dSetNoteName;

    [DllImport("user32.dll")]
    static extern bool GetKeyboardState(byte[] lpKeyState);

    [DllImport("user32.dll")]
    static extern uint MapVirtualKey(uint uCode, uint uMapType);

    [DllImport("user32.dll")]
    static extern IntPtr GetKeyboardLayout(uint idThread);

    [DllImport("user32.dll")]
    static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

    static clsPCKB() {
      InitKB24();
      InitKB44();
      KB = KB24.ToArray();

      string filename = Cfg.PCKBIniFilePath;
      if (!Cfg.NoIni) {
        //* Read ini file 
        List<string> lines = Utils.ReadLinesIgnoreComments(filename);
        if (lines != null) {
          int linenum;
          for (linenum = 0; linenum < lines.Count; linenum++) {
            KB[linenum] = (Keys)Enum.Parse(typeof(Keys), lines[linenum]);
          }
          Debug.WriteLine(Cfg.PCKBIniFilePath + " File loaded");
        }
      }

      InitDescToChars();
      InitCharsToChars();
    }

    internal static clsPCKB NewPCKB() {
      if (P.frmStart.chkDisablePCKB.Checked) return null;
      return new clsPCKB();
    }

    internal clsPCKB() {
      InitData();

      if (P.frmSCOctaves != null) {
        P.frmSCOctaves.lblPlayLoC.Enabled = false;
        P.frmSCOctaves.cmbPlayLoC.Enabled = false;
      }

      P.frmSC.nudOctTransposeKB.Enabled = false;
      P.frmSC.lblnudOctTransposeKB.Enabled = false;
      P.frmSC.nudOctTransposeDisplay.Enabled = false;
      P.frmSC.lblnudOctTransposeDisplay.Enabled = false;
    }

    internal static string GetChars(Keys key) {
      string chars = _GetChars(key);
      if (!CharsToChars.ContainsKey(chars)) return chars;
      return CharsToChars[chars];
    }

    private static string _GetChars(Keys key) {
      string desc = key.ToString();
      if (DescToChars.ContainsKey(desc)) return DescToChars[desc];
      if (desc.Length < 4) return desc;
      string uni = KeycodeToUnicode(key);
      if (uni.Length > 0) return uni;
      return (desc.Length > 3) ? desc.Substring(0, 3) : desc;
    }

    private static string KeycodeToUnicode(Keys key) {
      //Debug.WriteLine("KeycodeToUnicode called");
      byte[] keyboardState = new byte[255];
      bool keyboardStateStatus = GetKeyboardState(keyboardState);

      if (!keyboardStateStatus) return "";

      uint virtualKeyCode = (uint)key;
      uint scanCode = MapVirtualKey(virtualKeyCode, 0);
      IntPtr inputLocaleIdentifier = GetKeyboardLayout(0);

      StringBuilder result = new StringBuilder();
      ToUnicodeEx(virtualKeyCode, scanCode, keyboardState, result, (int)5, (uint)0, inputLocaleIdentifier);

      return result.ToString();
    }

    internal static void NullifyPCKB() {
      if (P.frmPCKBIn != null) {
        P.frmPCKBIn.Invoke(new delegClosePCKBIn(P.frmPCKBIn.Close));
      }
      P.PCKB = null;
      if (P.frmSCOctaves != null) {
        P.frmSCOctaves.lblPlayLoC.Enabled = true;
        P.frmSCOctaves.cmbPlayLoC.Enabled = true;
      }
      P.frmSC.nudOctTransposeKB.Enabled = true;
      P.frmSC.lblnudOctTransposeKB.Enabled = true;
      P.frmSC.nudOctTransposeDisplay.Enabled = true;
      P.frmSC.lblnudOctTransposeDisplay.Enabled = true;
    }

    internal void InitData() {
      KeysConverter kc = new KeysConverter();
      Descs = new string[KB.Length];
      Chars = new string[KB.Length];
      KBToPitch = new SortedList<Keys, byte>();
      for (int i = 0; i < KB.Length; i++) {
        if (KB[i] == Keys.None) continue;
        Descs[i] = kc.ConvertToString(KB[i]);
        Chars[i] = GetChars(KB[i]);
        if (!KBToPitch.ContainsKey(KB[i])) KBToPitch.Add(KB[i], (byte)i);
      }
    }

    internal static string SaveIniFile() {
      return Utils.SaveFile(Cfg.PCKBIniFilePath, SaveFileSub);
    }

    private static void SaveFileSub(StreamWriter xsw) {
      foreach (Keys k in KB) {
        xsw.WriteLine(k.ToString());
      }
    }

    internal static Keys GetKB(int oct, int pc) {
      if (oct < 0 || oct > 3) return Keys.None;
      if (pc < 0 || pc > 11) return Keys.None;
      return KB[oct * 12 + pc];
    }

    internal void MouseUp() {
      if (!MouseKB.HasValue) return;
      MouseUpDown(MouseKB.Value, false);
    }

    internal void MouseDown(int kb) {
      MouseUpDown(kb, true);
    }

    private void MouseUpDown(int kb, bool keydown) {
      //* check status
      if (keydown) {
        if (MouseKB.HasValue) {
          if (MouseKB.Value == kb) return;  //already on
          MouseUp(); 
        }
        MouseKB = kb;
      } else {
        MouseKB = null;
      }

      //* play midi pitch
      byte[] b = new byte[3];
      b[0] = (byte)((keydown) ? 0x90 : 0x80);
      b[0] |= (byte)MidiPlay.KBOutChanRec;
      b[1] = (byte)kb;
      b[2] = (keydown) ? (byte)Cfg.PCKBVel : (byte)0;

      if (P.frmSC.Play != null) {
        P.frmSC.Play.InMidi(b);
      } else {
        MidiPlay.OutMRec.SendShortMsg(b[0], b[1], b[2]);
        lock (clsPlay.KBPitchList) {
          if (keydown) {
            clsPlay.KBPitchList.Add(b[1], b[1]);  //ON
            P.frmSC.BeginInvoke(new delegRefresh(P.frmSC.picBottom.Refresh));
            P.frmSC.BeginInvoke(new delegRefresh(P.frmSC.picChords.Refresh));
          } else {
            clsPlay.KBPitchList_MidiOff(b);   //OFF
          }
        }
      }
    }

    //internal static void ExecSwitch(int keyswitch, bool down) {
    //  //* same as clsMidiInKB.ExecSwitch, but without BeginInvoke...
    //  foreach (string action in Forms.frmSwitch.KeyToActions[keyswitch]) {
    //    if (action != "") {
    //      Forms.frmSwitch.Delegs[action](down);
    //      if (action == "Sustain") {
    //        clsMidiInKB.SetChkSwitch(P.frmSC.chkSwitchSustain, down);
    //      } else if (action == "KB Chord") {
    //        clsMidiInKB.SetChkSwitch(P.frmSC.chkSwitchKBChord, down);
    //      }
    //    }
    //  }
    //}

    internal void KeyUpDown(Keys keyData, bool keydown) {
      //MidiPlay.MidiInKB.Ticks = clsPlay.GetTicks();

      if (keyData.HasFlag(Keys.Shift)) keyData = (keyData & ~Keys.Shift);
      if (keyData.HasFlag(Keys.Alt)) keyData = (keyData & ~Keys.Alt);
      if (keyData.HasFlag(Keys.Control)) keyData = (keyData & ~Keys.Control);

      //* check switchkey
      if (P.frmSC.Play != null) {
        if (keyData == Keys.Space) {
          Forms.frmSwitch.ExecSwitch(12, keydown);  //pedal
          return;
        } else if (keyData >= Keys.F1 && keyData <= Keys.F12) {  //C-B 
          Forms.frmSwitch.ExecSwitch(keyData - Keys.F1, keydown);
          return;
        }
      }

      //* play midi pitch
      byte[] b = GetMidiIn(keyData, keydown);
      //Debug.WriteLine("clsPCKB: KeyUpDown: keydown = " + keydown + " b.Length = " + b.Length);
      if (b.Length != 3) return;  //len 0: inactive or repeated key
      if (P.frmSC.Play != null) {
        P.frmSC.Play.InMidi(b);
      } else {
        MidiPlay.OutMRec.SendShortMsg(b[0], b[1], b[2]);
        lock (clsPlay.KBPitchList) {
          if (keydown) {
            clsPlay.KBPitchList.Add(b[1], b[1]);  //ON
            P.frmSC.BeginInvoke(new delegRefresh(P.frmSC.picBottom.Refresh));
            P.frmSC.BeginInvoke(new delegRefresh(P.frmSC.picChords.Refresh));
          } else {
            clsPlay.KBPitchList_MidiOff(b);   //OFF
          }
        }
      }

      ////* activate frmPCKBIn (for next kb input)
      //if (P.frmPCKBIn != null && frm != null && frm != P.frmPCKBIn) {
      //  P.frmPCKBIn.Invoke(dAct, P.frmPCKBIn);
      //}

      //* set kb range using untransposed kb notes
      if (P.frmSCOctaves != null) { //kb ranges
        dSetNoteName = new delegSetNoteName(P.frmSCOctaves.SetNoteName);
        P.frmSCOctaves.BeginInvoke(dSetNoteName, b[1]);
      }
    }

    private byte[] GetMidiIn(Keys keyData, bool keydown) {
      //* return status, pitch, vel

      if (!KBToPitch.ContainsKey(keyData)) return new byte[0];

      //* update up/down status
      if (keydown) {
        if (KeysDown.Contains(keyData)) return new byte[0];  //repeated key
        KeysDown.Add(keyData);
      } else {
        KeysDown.Remove(keyData);
      }

      //* qwerty key mapping to midi key
      int chan = MidiPlay.KBOutChanRec;
      byte status = (byte)(0x90 | chan);  //ON 
      //byte pitch = (byte)(KBToPitch[e.KeyCode] + 60);
      byte pitch = (byte)(KBToPitch[keyData] + Forms.frmSC.valShowLowCDflt);
      byte vel = (keydown) ? (byte)Cfg.PCKBVel : (byte)0;
      return new byte[] { status, pitch, vel };
    }

    private static void InitKB24() {  //2+ octaves 4 rows alphanumeric
      KB24 = new Keys[48];
      for (int i = 0; i < 48; i++) KB24[i] = Keys.None;

      KB24[00] = Keys.Z;
      KB24[01] = Keys.S;
      KB24[02] = Keys.X;
      KB24[03] = Keys.D;
      KB24[04] = Keys.C;
      KB24[05] = Keys.V;
      KB24[06] = Keys.G;
      KB24[07] = Keys.B;
      KB24[08] = Keys.H;
      KB24[09] = Keys.N;
      KB24[10] = Keys.J;
      KB24[11] = Keys.M;

      KB24[12] = Keys.Q;
      KB24[13] = Keys.D2;
      KB24[14] = Keys.W;
      KB24[15] = Keys.D3;
      KB24[16] = Keys.E;
      KB24[17] = Keys.R;
      KB24[18] = Keys.D5;
      KB24[19] = Keys.T;
      KB24[20] = Keys.D6;
      KB24[21] = Keys.Y;
      KB24[22] = Keys.D7;
      KB24[23] = Keys.U;
      KB24[24] = Keys.I;
      KB24[25] = Keys.D9;
      KB24[26] = Keys.O;
      KB24[27] = Keys.D0;
      KB24[28] = Keys.P;
    }

    private static void InitKB44() {  //4 octaves 4 rows all chars
      KB44 = new Keys[48];
      for (int i = 0; i < 48; i++) KB44[i] = Keys.None;

      KB44[00] = Keys.ShiftKey;
      KB44[01] = Keys.Oem5;
      KB44[02] = Keys.Z;
      KB44[03] = Keys.X;
      KB44[04] = Keys.C;
      KB44[05] = Keys.V;
      KB44[06] = Keys.B;
      KB44[07] = Keys.N;
      KB44[08] = Keys.M;
      KB44[09] = Keys.Oemcomma;
      KB44[10] = Keys.OemPeriod;
      KB44[11] = Keys.OemQuestion;

      KB44[12] = Keys.Capital;
      KB44[13] = Keys.A;
      KB44[14] = Keys.S;
      KB44[15] = Keys.D;
      KB44[16] = Keys.F;
      KB44[17] = Keys.G;
      KB44[18] = Keys.H;
      KB44[19] = Keys.J;
      KB44[20] = Keys.K;
      KB44[21] = Keys.L;
      KB44[22] = Keys.Oem1;
      KB44[23] = Keys.Oemtilde;

      KB44[24] = Keys.Tab;
      KB44[25] = Keys.Q;
      KB44[26] = Keys.W;
      KB44[27] = Keys.E;
      KB44[28] = Keys.R;
      KB44[29] = Keys.T;
      KB44[30] = Keys.Y;
      KB44[31] = Keys.U;
      KB44[32] = Keys.I;
      KB44[33] = Keys.O;
      KB44[34] = Keys.P;
      KB44[35] = Keys.Menu;

      KB44[36] = Keys.Oem8;
      KB44[37] = Keys.D1;
      KB44[38] = Keys.D2;
      KB44[39] = Keys.D3;
      KB44[40] = Keys.D4;
      KB44[41] = Keys.D5;
      KB44[42] = Keys.D6;
      KB44[43] = Keys.D7;
      KB44[44] = Keys.D8;
      KB44[45] = Keys.D9;
      KB44[46] = Keys.D0;
      KB44[47] = Keys.OemMinus;
    }

    private static void InitDescToChars() {
      DescToChars.Add("Back", "Bk");
      DescToChars.Add("Clear", "Clr");
      DescToChars.Add("ControlKey", "Ctl");
      DescToChars.Add("Decimal", "Dec");
      DescToChars.Add("Down", "Dwn");
      DescToChars.Add("Enter", "Ret");
      DescToChars.Add("Escape", "Esc");
      DescToChars.Add("Help", "Hlp");
      DescToChars.Add("LineFeed", "LF");
      DescToChars.Add("Menu", "Alt");
      DescToChars.Add("Next", "PgD");
      DescToChars.Add("None", "");
      DescToChars.Add("PageDown", "PgD");
      DescToChars.Add("PageUp", "PgU");
      DescToChars.Add("RControlKey", "Ctl");
      DescToChars.Add("Return", "Ret");
      DescToChars.Add("Tab", "Tab");
      DescToChars.Add("D0", "0");
      DescToChars.Add("D1", "1");
      DescToChars.Add("D2", "2");
      DescToChars.Add("D3", "3");
      DescToChars.Add("D4", "4");
      DescToChars.Add("D5", "5");
      DescToChars.Add("D6", "6");
      DescToChars.Add("D7", "7");
      DescToChars.Add("D8", "8");
      DescToChars.Add("D9", "9");
      DescToChars.Add("NumPad0", "0");
      DescToChars.Add("NumPad1", "1");
      DescToChars.Add("NumPad2", "2");
      DescToChars.Add("NumPad3", "3");
      DescToChars.Add("NumPad4", "4");
      DescToChars.Add("NumPad5", "5");
      DescToChars.Add("NumPad6", "6");
      DescToChars.Add("NumPad7", "7");
      DescToChars.Add("NumPad8", "8");
      DescToChars.Add("NumPad9", "9");
    }

    private static void InitCharsToChars() {
      CharsToChars.Add(".", "...");
      CharsToChars.Add(",", ",,,");
      CharsToChars.Add("'", "'''");
      CharsToChars.Add(";", ";;;");
      CharsToChars.Add(":", ":::");
      CharsToChars.Add("~", "~~~");
      CharsToChars.Add("`", "```");
      CharsToChars.Add("-", "---");
      CharsToChars.Add("_", "___");
      CharsToChars.Add("\"", "\"\"\"");
    }
  }
}