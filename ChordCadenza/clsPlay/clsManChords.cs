#define Trc

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChordCadenza {
  internal class clsManChords {
    private static readonly int?[] PCToCMajor = new int?[] { 0, null, 1, null, 2, 3, null, 4, null, 5, null, 6 };
    private delegate void delegShowChord(int[] chord, clsKeyTicks key);

    //private List<int> Chord = new List<int>(4);
    private List<int> KeysDown = new List<int>(4);  //sorted
    private clsKeyTicks Key = new clsKeyTicks("C", "major", 0);
    internal clsPlay.clsChordEv PlayChord;
    private readonly int[] CMajorChord = new int[] { 0, 4, 7 };  //C major chord
    //private int? LastChordTime;
    //private clsPlay.clsChordEv LastPlayChord;
    //private clsPlay.clsChordEv LastStablePlayChord;
    private Stopwatch SW = new Stopwatch();
    private static System.Timers.Timer OffTimer = new System.Timers.Timer(100);  //100 msecs
    private bool PlayActual;

    internal clsManChords(clsKeyTicks key, bool playactual) {
      Key = key;
      PlayActual = playactual;
      CreatePlayChord(CMajorChord);
      SW.Start();
      OffTimer.AutoReset = false;
      OffTimer.Elapsed += OffTimerEvent; 
    }

    internal bool MidiOff(int kb) {
      //* return true if processed here
      if (kb >= Forms.frmSC.valPlayLoC + 24) return false;   //not manchord key
      lock (OffTimer) {
        int kbrel = kb - Forms.frmSC.valPlayLoC;
        KeysDown.Remove(kbrel);
        if (KeysDown.Count == 0) {
          OffTimer.Stop();
          CreatePlayChord();
          Trace();
        } else {
          OffTimer.Start();  //cancels any existing timer
        }
        //int[] chord = GetChord();
        //CreatePlayChord(chord);
      }
      ShowChord(PlayChord, Key);
      return true;
    }

    private static void OffTimerEvent(Object source, System.Timers.ElapsedEventArgs e) {
      clsManChords manchords;
      lock (OffTimer) {
        if (P.frmSC.Play != null
        && P.frmSC.Play.ManChords != null) {
          manchords = P.frmSC.Play.ManChords;
          manchords.CreatePlayChord();
          manchords.Trace();
        } else {
          manchords = null;
        }
      }
      if (manchords != null) clsManChords.ShowChord(manchords.PlayChord, manchords.Key);
    }

    //private void CheckTime() {
    //  int time = (int)SW.ElapsedMilliseconds;
    //  if (LastChordTime.HasValue && time - LastChordTime.Value > 200) {
    //    LastStablePlayChord = PlayChord;
    //  }
    //  LastChordTime = time;
    //}

    internal bool MidiOn(int kb) {
      //* return true if valid manchord key processed here
      int[] chord;
      if (kb >= Forms.frmSC.valPlayLoC + 24) {  //not manchord key
#if Trc
        if (!LastLineChord) Debug.WriteLine("Time: " + SW.ElapsedMilliseconds + " Play Chord: ");
        LastLineChord = true;
#endif
        return false;  
      }
      lock (OffTimer) {
        int kbrel = kb - Forms.frmSC.valPlayLoC;
        if (KeysDown.Contains(kbrel)) return true;  //should not happen
        KeysDown.Add(kbrel);  //0 to 23 -> -11 to 34
        KeysDown.Sort();
        chord = GetChord();
        CreatePlayChord(chord);
        //CheckTime();
        Trace();
      }
      ShowChord(PlayChord, Key);
      return true;
    }

#if Trc
    private bool LastLineChord = false;
#endif

    private void Trace() {
#if Trc
      string stime = "Time: " + SW.ElapsedMilliseconds;
      string s0 = "KeysDown: ";
      foreach (int k in KeysDown) s0 += k + " ";
      string s1 = " Chord: ";
      foreach (int c in PlayChord.Chord) s1 += c + " ";
      Debug.WriteLine("{0,8} {1,-20} {2,-20}", stime, s0, s1);
#endif
    }

    private void CreatePlayChord() {
      int[] chord = GetChord();
      CreatePlayChord(chord);
      //CheckTime();
    }

    private void CreatePlayChord(int[] chord) {
      //clsCF.clsEv ev = new clsCF.clsEvPC(null, new clsMTime.clsBBT(0), chord);
      //PlayChord = new clsPlay.clsChordEvTimed(P.frmSC.Play, ev, chord);
      PlayChord = new ChordCadenza.clsPlay.clsChordEv(P.frmSC.Play, chord, true);
    }

    private int[] GetChord() {
      //* return manchord from KeysDown;
      if (KeysDown.Count == 0) {
        return (PlayChord == null) ? CMajorChord : PlayChord.Chord;
      }

      //if (KeysDown[0] >= 12) {  //root not in bottom octave
      //  KeysDown[0] = KeysDown[0].Mod12();
      //  for (int i = 1; i < KeysDown.Count; i++) {
      //    if (KeysDown[i] == KeysDown[0]) KeysDown.RemoveAt(i);
      //  }
      //}

      if (KeysDown.Count == 1) return TransposeChord(GetDefaultChord(KeysDown[0].Mod12()));
      List<int> chord = new List<int>(4);

      if (PlayActual) {
        for (int i = 0; i < KeysDown.Count; i++) {
          int pcrelmod = KeysDown[i].Mod12();
          if (chord.Contains(pcrelmod)) continue;
          chord.Add(pcrelmod);
        }
        return TransposeChord(chord);

      } else {
        int dia0 = (PCToCMajor[KeysDown[0].Mod12()].HasValue) ?
          PCToCMajor[KeysDown[0].Mod12()].Value : PCToCMajor[KeysDown[0].Mod12() - 1].Value;  //black root - use preceding white 
        int dia1, diadiff1;
        if (!PCToCMajor[KeysDown[1].Mod12()].HasValue) {  //1st. modifier blacknote (sus/sus2)
          int modifier = GetBlackModifier(KeysDown[0].Mod12(), KeysDown[1]);
          if (modifier == 0) {
            return CMajorChord;
          } else if (modifier == 1) {  //sus2 chord
            chord.Add((KeysDown[0].Mod12()));
            chord.Add((KeysDown[0] + 2).Mod12());
            chord.Add((KeysDown[0] + 7).Mod12());
          } else if (modifier == 2) {  //sus(4) chord
            chord.Add((KeysDown[0].Mod12()));
            chord.Add((KeysDown[0] + 5).Mod12());
            chord.Add((KeysDown[0] + 7).Mod12());
          }
          return TransposeChord(chord);
        }

        dia1 = PCToCMajor[KeysDown[1].Mod12()].Value;
        if (KeysDown[1] > 11) dia1 += 7;
        diadiff1 = dia1 - dia0;

        chord.Add((KeysDown[0]).Mod12());
        if (diadiff1 == 1) {  //minor chord
          chord.Add((KeysDown[0] + 3).Mod12());
          chord.Add((KeysDown[0] + 7).Mod12());
        } else if (diadiff1 == 2) {  //major chord
          chord.Add((KeysDown[0] + 4).Mod12());
          chord.Add((KeysDown[0] + 7).Mod12());
        } else if (diadiff1 == 3) {  //dim chord
          chord.Add((KeysDown[0] + 3).Mod12());
          chord.Add((KeysDown[0] + 6).Mod12());
        } else if (diadiff1 == 4) {  //aug chord
          chord.Add((KeysDown[0] + 4).Mod12());
          chord.Add((KeysDown[0] + 8).Mod12());
        } else {
          return CMajorChord;
        }
        if (KeysDown.Count == 2) return TransposeChord(chord);

        if (!PCToCMajor[KeysDown[2].Mod12()].HasValue) return CMajorChord;  //2nd. modifier blacknote

        int dia2 = PCToCMajor[KeysDown[2].Mod12()].Value;
        if (KeysDown[2] > 11) dia2 += 7;
        int diadiff2 = dia2 - dia1;

        if (diadiff2 == 1 && diadiff1 != 4) {  //6 or dim7 (not aug) 
          chord.Add((KeysDown[0] + 9).Mod12());
        } else if (diadiff2 == 2 && (diadiff1 == 1 || diadiff1 == 2)) {  //7 (maj or min) 
          chord.Add((KeysDown[0] + 10).Mod12());
        } else if (diadiff2 == 3 && diadiff1 == 2) {  //maj7 (maj) 
          chord.Add((KeysDown[0] + 11).Mod12());
        } else {
          return CMajorChord;
        }
        return TransposeChord(chord);
      }
    }

    private int[] TransposeChord(List<int> chord) {
      if (!(P.frmSC.Play is clsPlayKeyboard)) return chord.ToArray();
      int[] ret = new int[chord.Count];
      for (int i = 0; i < chord.Count; i++) {
        ret[i] = (chord[i] - P.frmSC.Play.TransposeKB).Mod12();
      }
      return ret;
    }

    private int GetBlackModifier(int kbrel, int kbrelmodifier) {
      //* return 0 (no modifier), 1 (first modifier), 2 (2nd. modifier) 
      int kbrelmod = kbrel.Mod12();
      int kbrelmodifiermod = kbrelmodifier.Mod12();
      if (kbrelmod == 0 || kbrelmod == 10 || kbrelmod == 11) {
        if (kbrelmodifiermod == 1) return 1;
        if (kbrelmodifiermod == 3) return 2;
      } else if (kbrelmod == 1 || kbrelmod == 2) {
        if (kbrelmodifiermod == 3) return 1;
        if (kbrelmodifiermod == 6) return 2;
      } else if (kbrelmod == 3 || kbrelmod == 4 || kbrelmod == 5) {
        if (kbrelmodifiermod == 6) return 1;
        if (kbrelmodifiermod == 8) return 2;
      } else if (kbrelmod == 6 || kbrelmod == 7) {
        if (kbrelmodifiermod == 8) return 1;
        if (kbrelmodifiermod == 10) return 2;
      } else if (kbrelmod == 8 || kbrelmod == 9) {
        if (kbrelmodifiermod == 10) return 1;
        if (kbrelmodifiermod == 1) return 2;
      }
      return 0;
    }

    private List<int> GetDefaultChord(int root) {
      //* major chord
      List<int> chord = new List<int>(3);
      chord.Add(root);
      chord.Add((root + 4).Mod12());
      chord.Add((root + 7).Mod12());
      return chord;
    }

    internal static void ShowChord(clsPlay.clsChordEv playchord, clsKeyTicks key) {
      if (key == null) key = new ChordCadenza.clsKeyTicks(0, "major", 0);
      int[] tchord;
      if (playchord == null || playchord.Chord == null) {
        tchord = null;
      } else {
        tchord = playchord.Chord.ToArray();
        for (int i = 0; i < tchord.Length; i++) {
          tchord[i] = (tchord[i] + P.frmSC.Play.TransposeKB).Mod12();
        }
      }
      P.frmSC.BeginInvoke(new delegShowChord(ShowChordForm), tchord, key);
    }

    private static void ShowChordForm(int[] chord, clsKeyTicks key) {
      P.frmSC.txtChordBottom.Text = ShowChordText(chord, key);
    }

    internal static string ShowChordText(int[] chord, clsKeyTicks key) {
      //* form thread
      //* show current playchord (root and qualifier)
      if (chord == null || chord.Length < 3) {
        //P.frmSC.txtChordBottom.Text = "";
        return "";
      }
      bool[] pcs = new bool[12];
      foreach (int c in chord) pcs[c] = true;
      int rootpc = chord[0];
      //List<string> names = ChordAnalysis.GetMatchingChordNames(pcs, rootpc);
      //string qualifier = (names.Count == 0) ? "xxx" : names[0];
      string qualifier = ChordAnalysis.GetName(pcs);
      //string txt = NoteName.ToSharpFlat(NoteName._Names[key.MidiKey + 7][rootpc].TrimEnd()) + qualifier;
      string txt = NoteName.ToSharpFlat(NoteName.GetName(key, rootpc).TrimEnd()) + qualifier;
      return txt;
    }
  }
}