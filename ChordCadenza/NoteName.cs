using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace ChordCadenza {
  internal class NoteName {
    internal class clsStandardChord {
      internal int[] Pitches;
      internal int[] ModPitches;
      internal int[] CompoundIntervals;
      internal int[] SimpleIntervals;
      internal string Name;

      internal clsStandardChord (string name, params int[] pitches) { 
        Name = name;
        Pitches = pitches;
        ModPitches = new int[pitches.Length];
        CompoundIntervals = new int[Pitches.Length - 1];
        SimpleIntervals = new int[Pitches.Length - 1];
        for (int i = 0; i < CompoundIntervals.Length; i++) {
          CompoundIntervals[i] = Pitches[i + 1] - Pitches[i];
        }
        for (int i = 0; i < Pitches.Length; i++) {
          ModPitches[i] = Pitches[i].Mod12();
        }
        Array.Sort(ModPitches);
        for (int i = 0; i < SimpleIntervals.Length; i++) {
          SimpleIntervals[i] = ModPitches[i + 1] - ModPitches[i];
        }
      }
    }

    internal static List<clsStandardChord> CommonChords = new List<clsStandardChord>();
//    internal static string[] ChordNames = new string[] 
//        { "", "m", "7", "m7", "dim", "dim7", "aug", "maj7", "m6", "7sus4", 
//        "9", "7b5", "7x5", "7x3", "sus4", "7sus2", "major", "minor", "7#5", "#5" };
//    internal static string[] ChordSymbols = new string[]
//       { "I", "i", "I7", "i7", "Io", "Io7", "I+", "IM7", "i6", "I7sus4",
//         "I9", "I7b5", "I7x5", "I7x3", "Isus4", "I7sus2", "Imajor", "Iminor", "I7#5", "I#5" };
//    internal static int[][] Intervals = new int[NoteName.ChordNames.Length][];  

    private const bool t = true, f = false;
    internal static readonly string[] strNote = new string[] { "c", "b+", "c+", "d-", "d", "d", "d+", "e-", "e", "f-", "f", "e+", "f+", "g-", "g", "g", "g+", "a-", "a", "a", "a+", "b-", "b", "c-" };
    //internal static readonly string[][] Names = new string[15][];  //[majkey][note]
    internal static readonly string[,,] _Names = new string[2, 15, 12];  //[majmin][key][note]
    internal static readonly string[,] Diatonic  = new string[15, 4];  //major & minor (diatonic note on keyboard, starting at C)
    internal static readonly Color[] DiaColor = new Color[] { Color.White, Color.Red, Color.Blue, Color.Green };

    internal static readonly int KeysStart = 2;
    internal static readonly int KeysSharpStart = 3;  //may not be used or tested
    internal static readonly int KeysFlatStart = 0;   //may not be used or tested

    internal static readonly string[] MajKeyNames = new string[] { "C", "Db", "D", "Eb", "E", "F", "F#", "G", "Ab", "A", "Bb", "B" };
  //internal static readonly string[] MinKeyNames = new string[] { "C", "C#", "D", "Eb", "E", "F", "F#", "G", "G#", "A", "Bb", "B" };
    internal static readonly string[] MinKeyNames = new string[] { "C", "Db", "D", "Eb", "E", "F", "F#", "G", "G#", "A", "Bb", "B" };

    internal static readonly string[] MajKeys = new string[] { "C-", "G-", "D-", "A-", "E-", "B-", "F", "C", "G", "D", "A", "E", "B", "F+", "C+" };
    internal static readonly int[] MajKeysPitch = new int[] {11, 6, 1, 8, 3, 10, 5, 0, 7, 2, 9, 4, 11, 6, 1 };

    internal static readonly string[] MinKeys = new string[] { "A-", "E-", "B-", "F", "C", "G", "D", "A", "E", "B", "F+", "C+", "G+", "D+", "A+" };
    internal static readonly int[] MinKeysPitch = new int[] { 8, 3, 10, 5, 0, 7, 2, 9, 4, 11, 6, 1, 8, 3, 10 };

    internal static readonly bool[] MajScaleNotes = new bool[] { t, f, t, f, t, t, f, t, f, t, f, t };
    //*                                                          d     r     m  f     s     l     t
    internal static readonly bool[] MinScaleNotesAll = new bool[] { t, f, t, t, f, t, f, t, t, t, t, t };
    //* all variants of minor scale                                 d     r  m-    f     s  l- l  t- t
    internal static readonly bool[] MinScaleNotesHarmonic = new bool[] { t, f, t, t, f, t, f, t, t, f, f, t };
    //* harmonic minor scale                                             d     r  m-    f     s  l-       t
    internal static readonly bool[] MinScaleNotesDown = new bool[] { t, f, t, t, f, t, f, t, t, f, t, f };
    //* melodic minor scale down                                     d     r  m-    f     s  l-    t- 
    internal static readonly bool[] MinScaleNotesUp = new bool[] { t, f, t, t, f, t, f, t, f, t, f, t };
    //* melodic minor scale up                                     d     r  m-    f     s     l     t

    internal static readonly string Solfa = "d r-r m-m f f+s l-l t-t ";
    internal static readonly string SolfaMaj = "drmfsltd";
    internal static readonly string[] Degree = new string[] { "I", "bII", "II", "bIII", "III", "IV", "#IV", "V", "bVI", "VI", "bVII", "VII" };

    internal static readonly int[] MajorPitches = new int[] { 0, 2, 4, 5, 7, 9, 11 };
    internal static readonly int[] HarmonicMinorPitches = new int[] { 0, 2, 3, 5, 7, 8, 11 };
    internal static readonly int[] MelUpMinorPitches = new int[] { 0, 2, 3, 5, 7, 9, 11 };
    internal static readonly int[] MelDownMinorPitches = new int[] { 0, 2, 3, 5, 7, 8, 10 };

    internal static readonly int[] IntervalToCOF = new int[] { 0, 5, 2, 3, 4, 1, 6, 1, 4, 3, 2, 5 };
    internal static readonly int[] COF = new int[] {0, 7, 2, 9, 4, 11, 6, 1, 8, 3, 10, 5 };
    internal static readonly int[] MajDia = new int[] { 0, 7, 5, 4, 9, 2, 11, 8, 10, 3, 1, 6 };
    //*                                                 d  s  f  m  l  r  t   l- t-  m- r- f+
    internal static readonly int[] MinDia = new int[] { 0, 7, 5, 3, 8, 2, 10, 9, 11, 4, 1, 6 };
    //*                                                 d  s  f  m- l- r  t-  l  t   m  r- f+

    static NoteName() {
      CommonChords.Add(new clsStandardChord("Major", 0, 2, 4, 5, 7, 9, 11));  //major scale
      CommonChords.Add(new clsStandardChord("Minor", 0, 2, 3, 5, 7, 8, 11));  //harmonic minor scale

      foreach (ChordAnalysis.clsTemplate template in ChordAnalysis.USToTemplate.Values) {
        List<int> pitchlist = new List<int>();
        for (int pc = 0; pc < 12; pc++) {
          if (template.PC[pc]) pitchlist.Add(pc);
        }
        CommonChords.Add(new clsStandardChord(template.Name, pitchlist.ToArray()));
      }

      //CommonChords.Add(new clsStandardChord("", 0, 4, 7));
      //CommonChords.Add(new clsStandardChord("m", 0, 3, 7));
      //CommonChords.Add(new clsStandardChord("7", 0, 4, 7, 10));
      //CommonChords.Add(new clsStandardChord("m7", 0, 3, 7, 10));
      //CommonChords.Add(new clsStandardChord("dim", 0, 3, 6));
      //CommonChords.Add(new clsStandardChord("dim7", 0, 3, 6, 9));
      //CommonChords.Add(new clsStandardChord("aug", 0, 4, 8));
      //CommonChords.Add(new clsStandardChord("Maj7", 0, 4, 7, 11));
      //CommonChords.Add(new clsStandardChord("Maj9", 0, 4, 7, 11, 14));
      //CommonChords.Add(new clsStandardChord("6", 0, 4, 7, 9));
      //CommonChords.Add(new clsStandardChord("69", 0, 4, 7, 9, 14));
      //CommonChords.Add(new clsStandardChord("m6", 0, 3, 7, 9));
      //CommonChords.Add(new clsStandardChord("m9", 0, 3, 7, 10, 14));
      //CommonChords.Add(new clsStandardChord("m7b5", 0, 3, 6, 10));
      //CommonChords.Add(new clsStandardChord("mMaj7", 0, 3, 7, 11));
      //CommonChords.Add(new clsStandardChord("7sus4", 0, 5, 7, 10));
      //CommonChords.Add(new clsStandardChord("9", 0, 4, 7, 10, 14));
      //CommonChords.Add(new clsStandardChord("7b5", 0, 4, 6, 10));
      //CommonChords.Add(new clsStandardChord("sus4", 0, 5, 7));
      //CommonChords.Add(new clsStandardChord("7sus2", 0, 2, 7, 10));
      //CommonChords.Add(new clsStandardChord("13", 0, 4, 7, 10, 21));
      //CommonChords.Add(new clsStandardChord("7b9", 0, 4, 7, 10, 13));
      //CommonChords.Add(new clsStandardChord("sus", 0, 5, 7));
      //CommonChords.Add(new clsStandardChord("7sus", 0, 5, 7, 10, 14));

      // partial chords
      CommonChords.Add(new clsStandardChord("7x5", 0, 4, 10));
      CommonChords.Add(new clsStandardChord("7x3", 0, 10));

      //*                C D EF G A B      major scale (drmfsltd)
      Diatonic[0, 0] = "010110101011";  //C- Major (B- E- A- D- G- C- F-)
      Diatonic[1, 0] = "010101101011";  //G- Major (B- E- A- D- G- C-)
      Diatonic[2, 0] = "110101101010";  //D- Major (B- E- A- D- G-)
      Diatonic[3, 0] = "110101011010";  //A- Major (B- E- A- D-)
      Diatonic[4, 0] = "101101011010";  //E- Major (B- E- A-)
      Diatonic[5, 0] = "101101010110";  //B- Major (B- E-)
      Diatonic[6, 0] = "101011010110";  //F  Major (B-)
      Diatonic[7, 0] = "101011010101";  //C  Major (no sharps or flats)
      Diatonic[8, 0] = "101010110101";  //G  Major (F+)
      Diatonic[9, 0] = "011010110101";  //D  Major (F+ C+)
      Diatonic[10, 0] = "011010101101";  //A  Major (F+ C+ G+)
      Diatonic[11, 0] = "010110101101";  //E  Major (F+ C+ G+ D+)
      Diatonic[12, 0] = "010110101011";  //B  Major (F+ C+ G+ D+ A+)
      Diatonic[13, 0] = "010101101011";  //F+ Major (F+ C+ G+ D+ A+ E+)
      Diatonic[14, 0] = "110101101010";  //C+ Major (F+ C+ G+ D+ A+ E+ B+)

      //                 C D EF G A B      harmonic minor scale  (drm-fsl-td)
      Diatonic[0, 1] = "010110011011";  //A- Minor (B- E- A- D- G- C- F-)
      Diatonic[1, 1] = "001101101011";  //E- Minor (B- E- A- D- G- C-)
      Diatonic[2, 1] = "110101100110";  //B- Minor (B- E- A- D- G-)
      Diatonic[3, 1] = "110011011010";  //F  Minor (B- E- A- D-)
      Diatonic[4, 1] = "101101011001";  //C  Minor (B- E- A-)
      Diatonic[5, 1] = "101100110110";  //G  Minor (B- E-)
      Diatonic[6, 1] = "011011010110";  //D  Minor (B-)
      Diatonic[7, 1] = "101011001101";  //A  Minor (no sharps or flats)
      Diatonic[8, 1] = "100110110101";  //E  Minor (F+)
      Diatonic[9, 1] = "011010110011";  //B  Minor (F+ C+)
      Diatonic[10, 1] = "011001101101";  //F+ Minor (F+ C+ G+)
      Diatonic[11, 1] = "110110101100";  //C+ Minor (F+ C+ G+ D+)
      Diatonic[12, 1] = "010110011011";  //G+ Minor (F+ C+ G+ D+ A+)
      Diatonic[13, 1] = "001101101011";  //D+ Minor (F+ C+ G+ D+ A+ E+)
      Diatonic[14, 1] = "110101100110";  //A+ Minor (F+ C+ G+ D+ A+ E+ B+)

      //                 C D EF G A B      melodic minor down scale  (drm-fsl-t-d)
      Diatonic[0, 2] = "010110101011";  //A- Minor (B- E- A- D- G- C- F-)
      Diatonic[1, 2] = "010101101011";  //E- Minor (B- E- A- D- G- C-)
      Diatonic[2, 2] = "110101101010";  //B- Minor (B- E- A- D- G-)
      Diatonic[3, 2] = "110101011010";  //F  Minor (B- E- A- D-)
      Diatonic[4, 2] = "101101011010";  //C  Minor (B- E- A-)
      Diatonic[5, 2] = "101101010110";  //G  Minor (B- E-)
      Diatonic[6, 2] = "101011010110";  //D  Minor (B-)
      Diatonic[7, 2] = "101011010101";  //A  Minor (no sharps or flats)
      Diatonic[8, 2] = "101010110101";  //E  Minor (F+)
      Diatonic[9, 2] = "011010110101";  //B  Minor (F+ C+)
      Diatonic[10, 2] = "011010101101";  //F+ Minor (F+ C+ G+)
      Diatonic[11, 2] = "010110101101";  //C+ Minor (F+ C+ G+ D+)
      Diatonic[12, 2] = "010110101011";  //G+ Minor (F+ C+ G+ D+ A+)
      Diatonic[13, 2] = "010101101011";  //D+ Minor (F+ C+ G+ D+ A+ E+)
      Diatonic[14, 2] = "110101101010";  //A+ Minor (F+ C+ G+ D+ A+ E+ B+)

      //                 C D EF G A B      melodic minor up scale  (drm-fsltd)
      Diatonic[0, 3] = "010101011011";  //A- Minor (B- E- A- D- G- C- F-)
      Diatonic[1, 3] = "101101101010";  //E- Minor (B- E- A- D- G- C-)
      Diatonic[2, 3] = "110101010110";  //B- Minor (B- E- A- D- G-)
      Diatonic[3, 3] = "101011011010";  //F  Minor (B- E- A- D-)
      Diatonic[4, 3] = "101101010101";  //C  Minor (B- E- A-)
      Diatonic[5, 3] = "101010110110";  //G  Minor (B- E-)
      Diatonic[6, 3] = "011011010101";  //D  Minor (B-)
      Diatonic[7, 3] = "101010101101";  //A  Minor (no sharps or flats)
      Diatonic[8, 3] = "010110110101";  //E  Minor (F+)
      Diatonic[9, 3] = "011010101011";  //B  Minor (F+ C+)
      Diatonic[10, 3] = "010101101101";  //F+ Minor (F+ C+ G+)
      Diatonic[11, 3] = "110110101010";  //C+ Minor (F+ C+ G+ D+)
      Diatonic[12, 3] = "010101011011";  //G+ Minor (F+ C+ G+ D+ A+)
      Diatonic[13, 3] = "101101101010";  //D+ Minor (F+ C+ G+ D+ A+ E+)
      Diatonic[14, 3] = "110101010110";  //A+ Minor (F+ C+ G+ D+ A+ E+ B+)

      SetNames(0,  0, "C ", "D-", "D ", "E-", "F-", "F ", "G-", "G ", "A-", "A ", "B-", "C-" ); //Cb major
      SetNames(0,  1, "C ", "D-", "D ", "E-", "F-", "F ", "G-", "G ", "A-", "A ", "B-", "C-" ); //Gb major
      SetNames(0,  2, "C ", "D-", "D ", "E-", "F-", "F ", "G-", "G ", "A-", "A ", "B-", "B " ); //Db major
      SetNames(0,  3, "C ", "D-", "D ", "E-", "F-", "F ", "G-", "G ", "A-", "A ", "B-", "B " ); //Ab major
      SetNames(0,  4, "C ", "D-", "D ", "E-", "F-", "F ", "G-", "G ", "A-", "A ", "B-", "B " ); //Eb major
      SetNames(0,  5, "C ", "D-", "D ", "E-", "E ", "F ", "F+", "G ", "A-", "A ", "B-", "B " ); //Bb major
      SetNames(0,  6, "C ", "D-", "D ", "E-", "E ", "F ", "F+", "G ", "A-", "A ", "B-", "B " ); //F major
      SetNames(0,  7, "C ", "C+", "D ", "E-", "E ", "F ", "F+", "G ", "G+", "A ", "B-", "B " ); //C major
      SetNames(0,  8, "C ", "C+", "D ", "E-", "E ", "F ", "F+", "G ", "G+", "A ", "B-", "B " ); //G major
      SetNames(0,  9, "C ", "C+", "D ", "D+", "E ", "F ", "F+", "G ", "G+", "A ", "B-", "B " ); //D major
      SetNames(0, 10, "C ", "C+", "D ", "D+", "E ", "F ", "F+", "G ", "G+", "A ", "B-", "B " ); //A major
      SetNames(0, 11, "C ", "C+", "D ", "D+", "E ", "F ", "F+", "G ", "G+", "A ", "A+", "B " ); //E major
      SetNames(0, 12, "C ", "C+", "D ", "D+", "E ", "F ", "F+", "G ", "G+", "A ", "A+", "B " ); //B major
      SetNames(0, 13, "C ", "C+", "D ", "D+", "E ", "F ", "F+", "G ", "G+", "A ", "A+", "B " ); //F# major
      SetNames(0, 14, "B+", "C+", "D ", "D+", "E ", "F ", "F+", "G ", "G+", "A ", "A+", "B " ); //C# major

      SetNames(1,  0, "C ", "D-", "D ", "E-", "F-", "F ", "F+", "G ", "A-", "A ", "B-", "C-" ); //Ab minor
      SetNames(1,  1, "C ", "C+", "D ", "E-", "F-", "F ", "G-", "G ", "A-", "A ", "B-", "C-" ); //Eb minor
      SetNames(1,  2, "C ", "D-", "D ", "E-", "F-", "F ", "G-", "G ", "G+", "A ", "B-", "B " ); //Bb minor
      SetNames(1,  3, "C ", "D-", "D ", "D+", "E ", "F ", "G-", "G ", "A-", "A ", "B-", "B " ); //F minor
      SetNames(1,  4, "C ", "D-", "D ", "E-", "F-", "F ", "G-", "G ", "A-", "A ", "A+", "B " ); //C minor
      SetNames(1,  5, "C ", "D-", "D ", "E-", "E ", "F ", "F+", "G ", "A-", "A ", "B-", "B " ); //G minor
      SetNames(1,  6, "C ", "C+", "D ", "E-", "E ", "F ", "F+", "G ", "A-", "A ", "B-", "B " ); //D minor
      SetNames(1,  7, "C ", "C+", "D ", "E-", "E ", "F ", "F+", "G ", "G+", "A ", "B-", "B " ); //A minor
      SetNames(1,  8, "C ", "C+", "D ", "D+", "E ", "F ", "F+", "G ", "G+", "A ", "B-", "B " ); //E minor
      SetNames(1,  9, "C ", "C+", "D ", "D+", "E ", "F ", "F+", "G ", "G+", "A ", "A+", "B " ); //B minor
      SetNames(1, 10, "C ", "C+", "D ", "D+", "E ", "F ", "F+", "G ", "G+", "A ", "B-", "B " ); //F# minor
      SetNames(1, 11, "C ", "C+", "D ", "D+", "E ", "F ", "F+", "G ", "G+", "A ", "A+", "B " ); //C# minor
      SetNames(1, 12, "C ", "C+", "D ", "D+", "E ", "F ", "F+", "G ", "G+", "A ", "A+", "B " ); //G# minor
      SetNames(1, 13, "C ", "C+", "D ", "D+", "E ", "F ", "F+", "G ", "G+", "A ", "A+", "B " ); //D# minor
      SetNames(1, 14, "B+", "C+", "D ", "D+", "E ", "F ", "F+", "G ", "G+", "A ", "A+", "B " ); //A# minor
    }

    private static void SetNames(int majmin, int midikey, params string[] names) {
      //* majmin 0=maj, 1=min
      //* midikey = 0-14 (midikey + 7)
      for (int i = 0; i < 12; i++) {
        _Names[majmin, midikey, i] = names[i];
      }
    }

    internal static string GetName(clsKey key, int note) {
      note = note.Mod12();
      return _Names[key.MajMin, key.MidiKey + 7, note]; 
    }


    internal static int GetPitch(string note) {
      for (int n = 0; n < strNote.Length; n++) {  //locate note in strNote
        if (strNote[n] == note.ToLower()) return n / 2;  //0-11
      }
      return -1;  //note not found
    }

    internal static int GetPitchAndQualifier(string chordname, out string qualifier) {
      qualifier = "";
      if (chordname == "") return -1;  //no recognisable chord
      string notename;
      int notelen = 1;
      if (chordname.Length == 1) {
        notename = chordname.Substring(0, 1);
      } else {
        notename = NoteName.ToPlusMinus(chordname.Substring(0, 2));
        notelen = 2;
        if (!notename.EndsWith("+") && !notename.EndsWith("-")) {
          notelen = 1;
          notename = chordname.Substring(0, 1);
        }
      }
      int index = Array.IndexOf(strNote, notename.ToLower());
      if (index < 0) return -1;
      if (notelen == chordname.Length) qualifier = ""; else {
        string qual = chordname.Substring(notelen).ToLower();
        if (!ChordAnalysis.NameToTemplate.ContainsKey(qual)) return -1;
        qualifier = ChordAnalysis.NameToTemplate[qual].Name;  //get correct case
      }
      return index / 2;
    }

    internal static string GetNoteName(int majmin, int midikey, int note) {  //midikey = -7 to +7
      return _Names[majmin, midikey + 7, note];
    }

    internal static string GetNoteName(clsKey key, int note) {  //midikey = -7 to +7
      return _Names[key.MajMin, key.MidiKey + 7, note];
    }

    internal static string MidiKeyToKeyStr(int midikey, string majmin) {  //key = -7 - +7
      majmin = majmin.ToLower();
      if (majmin == "major") return MajKeys[midikey + 7]; else return MinKeys[midikey + 7];
    }

    internal static string PitchToKeyStr(int pitch, string majmin) {  //pitch = 0 - 11
      //* sharp: null(default), true(favour sharp), false (favour flat)
      majmin = majmin.ToLower();

      if (majmin == "major") {
        for (int i = KeysStart; i <= KeysStart + 11; i++) {
          if (MajKeysPitch[i] == pitch) return MajKeys[i];
        }
        new NoteNameWarning("Major Key (to KeyStr): " + pitch + " not found");
        return "";
      } else {
        for (int i = KeysStart; i <= KeysStart + 11; i++) {
          if (MinKeysPitch[i] == pitch) return MinKeys[i];
        }
        new NoteNameWarning("Minor Key (to KeyStr): " + pitch + " not found");
        return "";
      }
    }

    internal static int PitchToMidiKey(int pitch, string majmin, bool? indsharp) {  //pitch = 0-11 -> key = 0-14 
      majmin = majmin.ToLower();
      int start = KeysStart;  //no preference for sharp or flat
      if (indsharp.HasValue) {
        start = (indsharp.Value) ? 3 : 0;  //start at A or C- in MajKeysPitch
      }

      if (majmin == "major") {
        for (int i = start; i <= start + 11; i++) {
          if (MajKeysPitch[i] == pitch) return i - 7;
        }
        new NoteNameWarning("Major Key (to MidiKey): " + pitch + " not found");
        return int.MinValue;
      } else {
        for (int i = start; i <= start + 11; i++) {
          if (MinKeysPitch[i] == pitch) return i - 7;
        }
        new NoteNameWarning("Minor Key (to MidiKey): " + pitch + " not found");
        return int.MinValue;
      }
    }

    internal static int GetMidiKey(string key, string majmin) { 
      //key = 0 - 14, excluding remote keys with simpler enharmonic
      //returns int.MinValue if key is invalid
      majmin = majmin.ToLower();
      key = key.ToUpper();
      int ret;
      if (majmin == "major") ret = Array.IndexOf(MajKeys, key);
      else ret = Array.IndexOf(MinKeys, key);
      if (ret < 0) return int.MinValue; else return ret - 7;
    }

    internal static int MidiKeyToPitch(int midikey, string scale) {
      scale = scale.ToLower();
      if (scale == "major") return MajKeysPitch[midikey + 7];
      else if (scale == "minor") return MinKeysPitch[midikey + 7];
      else new NoteNameWarning("Scale: " + scale + " not found");
      return int.MinValue;
    }

    internal static string GetNoteNameOrSolfa(int pitch, clsKeyTicks key) {
      if (P.frmSC.optShowSolfa.Checked) {
        //int keynote = key.KBTrans_KeyNote;
        int keynote = key.KeyNote;
        int offset = Math.Abs(((pitch - keynote).Mod12())) * 2;
        return Solfa.Substring(offset, 2).TrimEnd();
      } else if (P.frmSC.optShowNoteName.Checked) {
        return ToSharpFlat(GetNoteName(key, pitch.Mod12()));
      } else {
        return "*";
      }
    }

    internal static string GetNoteNameOrRoman(int pitch, clsKeyTicks key) {
      if (P.frmSC.chkShowChordsRel.Checked) {
        //return GetDegree(pitch, key.KBTrans_KeyNote);
        return GetDegree(pitch, key.KeyNote);
      } else {
        return NoteName.ToSharpFlat(NoteName._Names[key.MajMin, key.MidiKey + 7, pitch].TrimEnd());
      }
    }

    internal static string GetDegree(int? pitch, int keynote) {
      if (pitch == null) return "x";
      int index = ((int)pitch - keynote).Mod12();
      return Degree[index];
    }

    internal static string ToPlusMinus(string txt) {
      //* replace 2nd b/# with -/+
      if (txt.Length < 2) {
        LogicError.Throw(eLogicError.X069);
        return txt;
      }
      if (txt.Substring(1, 1) == "b") {
        string suffix = (txt.Length > 2) ? txt.Substring(2) : "";
        txt = txt.Substring(0, 1) + "-" + suffix;
      }
      if (txt.Substring(1, 1) == "#") {
        string suffix = (txt.Length > 2) ? txt.Substring(2) : "";
        txt = txt.Substring(0, 1) + "+" + suffix;
      }
      //txt = txt.Replace('b', '-');
      //txt = txt.Replace('#', '+');
      return txt;
    }

    internal static string ToSharpFlat(string txt) {
      txt = txt.Replace('-', 'b');
      txt = txt.Replace('+', '#');
      return txt;
    }

    internal static string[] ToSharpFlat(string[] txt) {
      string[] ret = new string[txt.Length];
      for (int i = 0; i < txt.Length; i++) ret[i] = ToSharpFlat(txt[i]);
      return ret;
    }

    internal static int[] GetScaleChord(clsKeyTicks key) {
      //return 7 notes of scale for key, starting at doh
      int[] ret = new int[7];
      string diatonic = Diatonic[key.MidiKey + 7, key.MajMin];
      int idia = key.KeyNote - 1;
      for (int i = 0; i < 7; i++) {
        string val;
        do {  //get next "1" in Diatonic[key, majmin]
          if (++idia == 12) idia = 0;
          val = diatonic.Substring(idia, 1);
        } while (val == "0");
        ret[i] = idia;
      }
      return ret;
    }

    internal static string GetPitchStr(int midipitch) {
      if (midipitch < 0 || midipitch > 127) return "*";
      return NoteName.MajKeyNames[midipitch.Mod12()] + (midipitch / 12 - 1);
    }

    internal static string GetPitchStrMidi(int midipitch) {
      if (midipitch < 0 || midipitch > 127) return "*";
      return midipitch.ToString();
    }

    internal static string MidiToNoteNameAndOctave(int note) {  //e.g. 61 -> C#4)
      string notename = ToSharpFlat(GetNoteName(0, 0, note.Mod12()).TrimEnd());
      int oct = note / 12 - 1;
      return notename + oct;
    }

    internal static int NoteNameAndOctaveToMidi(string name) {  //e.g. C#4 -> 61)
      string notename = name.Substring(0, name.Length - 1);
      if (notename.Length == 1) notename += " ";
      int pc = GetPitch(ToPlusMinus(notename.ToLower()).TrimEnd());
      int oct = int.Parse(name.Substring(name.Length - 1, 1));
      return pc + (oct + 1) * 12;
    }
  }
}
