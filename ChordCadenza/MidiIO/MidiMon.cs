using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace ChordCadenza {
  internal static class MidiMon {
    internal static bool NoMon = false;
    internal static int KeyCount = 0;
    private static int PitchCount = 0;
    private static bool[] Pitches = new bool[128];  //pitches sounding
    private static bool[] Keys = new bool[128];  //keys down
    private static bool SustainDown = false;
    private static bool Bypass = false;

    internal static void PlayPedal(bool down) {
      if (NoMon) return;
      //* KB only
      SustainDown = down;
    }

    internal static void MidiOutShortMsg(int status, int msg, int data) {
      if (NoMon) return;
      //* OutKB only
      if (Bypass) return;
      int stat = (status & 0xf0);
      if (stat != 0x80 && stat != 0x90) return;
      int chan = (status & 0x0f);
      if (chan != MidiPlay.KBOutChan) {
#if DEBUG
        MessageBox.Show("Wrong channel on MidiMon.MidiOutShortMsg");
        Debugger.Break();
#endif
        return;
      }
      if (stat == 0x90 && data > 0) {  //note ON 
        if (!Pitches[msg]) PitchCount++;
        Pitches[msg] = true;
      } else if (stat == 0x80 || (stat == 0x90 && data == 0)) {  //note OFF
        if (Pitches[msg]) PitchCount--;
        if (PitchCount < 0) {
          LogicError.Throw(eLogicError.X165);
          PitchCount = 0;
        }
        Pitches[msg] = false;
      }
    }

    internal static void Reset() {  //called by openmidi, allnotesoff
      if (NoMon) return;
      Pitches = new bool[128];
      Keys = new bool[128];
      PitchCount = 0;
      KeyCount = 0;
      //SustainDown = false;
    }

    internal static void CheckAllOff() {
      if (NoMon) return;
      if (SustainDown) return;
      if (KeyCount > 0) return;
      if (PitchCount > 0) { //should not happen!!!
#if DEBUG
        MessageBox.Show("Hanging Midi Pitch(es) found!");
        //Debugger.Break();
#endif
        Bypass = true;  //avoid recursive call
        int status = 0x80 | MidiPlay.KBOutChan;
        for (int p = 0; p < 128; p++) {  //switch off possible hanging pitches
          if (Pitches[p]) MidiPlay.OutMKB.SendShortMsg(status, p, 0);
        }
        Bypass = false;
        Reset();
      }
    }

    internal static void KeyDown(int kb) {
      if (NoMon) return;
      if (!Keys[kb]) KeyCount++;
      Keys[kb] = true;
    }

    internal static void KeyUp(int kb) {
      if (NoMon) return;
      if (Keys[kb]) KeyCount--;
        if (KeyCount < 0) {
          LogicError.Throw(eLogicError.X166);
          KeyCount = 0;
        }
      Keys[kb] = false;
    }

    internal static void Sustain(bool down) {
      if (NoMon) return;
      SustainDown = down;
    }

  }
}
