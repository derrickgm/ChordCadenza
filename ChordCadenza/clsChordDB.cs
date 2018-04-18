using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChordCadenza {
  internal class ChordDB {

    static ChordDB() {
      foreach (ChordAnalysis.clsTemplate template in ChordAnalysis.USToTemplate.Values) {
        List<int> pitchlist = new List<int>();
        for (int pc = 0; pc < 12; pc++) {
          if (template.PC[pc]) pitchlist.Add(pc);
        }
        //int rank = (template.Rank < 3) ? 0 : 1; 
        NewChord(template.Name, pitchlist.ToArray());
      }

      //NewChord(0, "", 0, 4, 7);
      //NewChord(0, "m", 0, 3, 7);
      ////...
      //NewChord(0, "7", 0, 4, 7, 10);
      //NewChord(0, "m7", 0, 3, 7, 10);
      //NewChord(0, "dim", 0, 3, 6);
      //NewChord(0, "dim7", 0, 3, 6, 9);
      //NewChord(0, "aug", 0, 4, 8);
      //NewChord(0, "Maj7", 0, 4, 7, 11);
      //NewChord(0, "Maj9", 0, 4, 7, 11, 14);
      //NewChord(0, "6", 0, 4, 7, 9);
      //NewChord(1, "69", 0, 4, 7, 9, 14);
      //NewChord(0, "m6", 0, 3, 7, 9);
      //NewChord(1, "m9", 0, 3, 7, 10, 14);
      //NewChord(1, "m7b5", 0, 3, 6, 10);
      //NewChord(1, "mMaj7", 0, 3, 7, 11);
      //NewChord(1, "7sus4", 0, 5, 7, 10);
      //NewChord(1, "9", 0, 4, 7, 10, 14);
      //NewChord(1, "7b5", 0, 4, 6, 10);
      //NewChord(1, "sus4", 0, 5, 7);
      //NewChord(1, "7sus2", 0, 2, 7, 10);
      //NewChord(1, "13", 0, 4, 7, 10, 21);
      //NewChord(1, "7b9", 0, 4, 7, 10, 13);
      //NewChord(1, "sus", 0, 5, 7);
      //NewChord(1, "7sus", 0, 5, 7, 10, 14);
      // partial chords
      NewChord("7x5", 0, 4, 10);
      //NewChord(0, "7x3", 0, 10);

    }

    internal class clsDesc {
      internal int Root;
      internal string Qualifier;
      //internal int Rank;  //only used to get brush colour
      internal sbyte NoteMapPtr;  // -1 to -127
      internal clsDesc(int root, string qualifier, sbyte notemapptr) {
        Root = root;
        Qualifier = qualifier;
        //Rank = rank;
        NoteMapPtr = notemapptr;
      }
    }

    private static SortedList<int, clsDesc> ChList = new SortedList<int, clsDesc>(500);
    private static List<string> DescList = new List<string>();

    internal static string PtrToDesc(sbyte notemapptr) {  //notemapptr < 0
      if (notemapptr == 0) return "?";
      return DescList[-(notemapptr + 1)];
    }

    internal static sbyte DescToPtr(string desc) {  //return ptr (<0)
      for (int i = 0; i < DescList.Count; i++) {
        if (DescList[i].ToLower() == desc.ToLower()) return (sbyte)-(i + 1);
      }
      return 0;
    }

    private static void NewChord(string qualifier, params int[] pitches) {
      DescList.Add(qualifier);
      sbyte notemapptr = (sbyte)-DescList.Count;  //-1 to -127
      for (int r = 0; r < 12; r++) {
        int bitmap = 0;
        foreach (int p in pitches) bitmap |= (1 << ((p + r).Mod12()));
        if (ChList.ContainsKey(bitmap)) continue;
        clsDesc desc = new clsDesc(r, qualifier, notemapptr);
        ChList.Add(bitmap, desc);
      }
    }

    internal static clsDesc GetChord(bool[] notes) {
      //* look up chord from bool[12] of modded notes
      int bitmap = GetBitMap(notes);
      if (!ChList.ContainsKey(bitmap)) return null;
      return ChList[bitmap];
    }

    private static int GetBitMap(bool[] notes) {
      //* return bitmap from bool[]
      if (notes.Length != 12) {
        LogicError.Throw(eLogicError.X013);
      }
      int bitmap = 0;
      for (int i = 0; i < 12; i++) {
        if (notes[i]) bitmap |= 1 << i;
      }
      return bitmap;
    }

    internal static int[] GetChordWeights(bool[] inchord) {
      //* return weightings for inchord notes
      int inbitmap = GetBitMap(inchord);
      int[] ret = new int[12];
      foreach (KeyValuePair<int, clsDesc> db in ChList) {
        //* check if all notes in db entry are present in inchord
        if ((db.Key & inbitmap) == db.Key) {  
          int mask = 1;
          for (int i = 0; i < 12; i++) {
            if ((db.Key & mask) == mask) ret[i]++;
            mask = mask << 1;
          }
        }
      }
      return ret;
    }

    internal static void Test() {
      bool t = true, f = false;
      int[] weights = GetChordWeights(new bool[] { t, f, f, f, t, f, f, t, f, f, f, t }); 
      //*                                          0  1  2  3  4  5  6  7  8  9 10 11 
    }
  }
}