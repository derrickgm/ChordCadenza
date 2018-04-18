using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace ChordCadenza {
  internal class clsPCKB {
    private Keys[] KB;  
    private List<Keys> KeysDown = new List<Keys>(6);
    private SortedList<Keys, byte> KBToPitch = new SortedList<Keys, byte>();
    internal string[] Chars;
    private delegate void delegRefresh();
    //private int Velocity = 80;  //1 - 127

    internal clsPCKB() {
      ////*KeyPressEventArgs KeyChars... 
      //Chars[0] = "ZSXDCVGBHNJM"; //1st 8ve
      //Chars[1] = "Q2W3ER5T6Y7U";  //2nd 8ve
      //Chars[3] = "I9O0P";  //3rd 8ve
      KB = new Keys[] {
        Keys.Z, Keys.S, Keys.X, Keys.D, Keys.C,  //C - E  (1st 8ve)
        Keys.V, Keys.G, Keys.B, Keys.H, Keys.N, Keys.J, Keys.M,  //F - Bb (1st 8ve)
        Keys.Q, Keys.D2, Keys.W, Keys.D3, Keys.E,  //C - E (2nd 8ve)
        Keys.R, Keys.D5, Keys.T, Keys.D6, Keys.Y, Keys.D7, Keys.U, Keys.I,  //F - C (2nd 8ve)
        Keys.D9, Keys.O, Keys.D0, Keys.P  //C# - E  (3rd 8ve)
      };

      KeysConverter kc = new KeysConverter();
      Chars = new string[KB.Length];
      for (int i = 0; i < KB.Length; i++) {
        Chars[i] = kc.ConvertToString(KB[i]);
        KBToPitch.Add(KB[i], (byte)i);
      }
    }

    internal void KeyUpDown(KeyEventArgs e, bool keydown) {
      MidiPlay.MidiInKB.Ticks = clsPlay.GetTicks();
      byte[] b = GetMidiIn(e, keydown);
      //Debug.WriteLine("clsPCKB: KeyUpDown: keydown = " + keydown + " b.Length = " + b.Length);
      if (b.Length != 3) return;  //len 0: inactive key
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

    private byte[] GetMidiIn(KeyEventArgs e, bool keydown) {
      //* return status, pitch, vel

      //* check if switchkey
      if (e.KeyCode == Keys.ControlKey) {  //pedal
        foreach (string action in Forms.frmSwitch.KeyToActionsPedal) {
          if (action != "") Forms.frmSwitch.Delegs[action](keydown);
        }
        return new byte[0];
      }

      //* update up/down status
      if (keydown) {
        if (KeysDown.Contains(e.KeyCode)) return new byte[0];  //repeated key
        KeysDown.Add(e.KeyCode);
      } else {
        KeysDown.Remove(e.KeyCode);
      }

      ////* check velocity keys
      //if (keydown && e.KeyCode == Keys.PageUp) {
      //  VelChange(16);
      //  return new byte[0];
      //}
      //if (keydown && e.KeyCode == Keys.PageDown) {
      //  VelChange(-16);
      //  return new byte[0];
      //}
      //if (keydown && e.KeyCode == Keys.Up) {
      //  VelChange(4);
      //  return new byte[0];
      //}
      //if (keydown && e.KeyCode == Keys.Down) {
      //  VelChange(-4);
      //  return new byte[0];
      //}

      if (!KBToPitch.ContainsKey(e.KeyCode)) return new byte[0];

      //* qwerty key mapping to midi key
      int chan = MidiPlay.KBOutChanRec;
      byte status = (byte)(0x90 | chan);  //ON 
      //byte pitch = (KBList[e.KeyCode] == 25) ? (byte)72 : (byte)(KBList[e.KeyCode] + 60);
      byte pitch = (byte)(KBToPitch[e.KeyCode] + 60);
      byte vel = (keydown) ? (byte)P.frmSC.nudKeyVel.Value : (byte)0;
      return new byte[] { status, pitch, vel };
    }

    //private void VelChange(int change) {
    //  if (Velocity == 1) Velocity = 0;  //to allow incr 4/16 to work consistently
    //  if (Velocity == 127) Velocity = 128;  //to allow incr 4/16 to work consistently
    //  Velocity += change;
    //  if (Velocity > 127) Velocity = 127;
    //  if (Velocity < 1) Velocity = 1;
    //}
  }
}