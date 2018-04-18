using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Numerics;

namespace ChordCadenza {
  internal interface IQNEle {
    bool this[int q, int n] { get; set; }
    bool[] this[int q] { get; }
    void NullifyChordinateStatus(int qi);
  }

  internal abstract class clsNoteMap : IQNEle {
    //* contains list of current notes for each time interval
    //[FlagsAttribute] internal enum eFilter : short { None = 0, Q = 1, W = 2, QW = 3 };
    //* Q = true if note present after gap/notes/quantize filter
    //* W = true if note present after chord weight filter
    static clsNoteMap() {
      CreateNoteMasks();
    }

    internal abstract void CalcKeys_CalcSegQI(int[] seg, int qi, ref bool indempty);

    public abstract bool this[int qtime, int note] { get; set; }  //all trks
    public abstract bool[] this[int qtime] { get; set; }  //return array[12] of modded notes (all trks)

    internal abstract bool IsF(int qi, int note);

    internal abstract bool[] IsF(int qi);

    //internal abstract int GetLengthQTime();
    public virtual void NullifyChordinateStatus(int qi) { }
    //internal abstract bool QI_OOR(int qi);
    //internal abstract void Realloc(int qlen);

    protected static ushort[] NoteMask12 = new ushort[12];
    protected static BigInteger[] NoteMask128 = new BigInteger[128];

    protected static void CreateNoteMasks() {
      ushort mask12 = 1;
      for (int i = 0; i < 12; i++) {
        NoteMask12[i] = mask12;
        mask12 <<= 1;
      }
      BigInteger mask128 = 1;
      for (int i = 0; i < 128; i++) {
        NoteMask128[i] = mask128;
        mask128 <<= 1;
      }
    }


    protected static bool[] BigIntegerToBoolArray(BigInteger big, int size) {
      //* convert biginteger to bool[128]
      bool[] bools = new bool[size];
      if (big == 0) return bools;
      BigInteger mask = 1;
      for (int i = 0; i < size; i++) {
        bools[i] = (big & mask) > 0;
        mask <<= 1;
      }
      return bools;
    }

    internal static bool[] UShortToBoolArray(ushort ushrt) {
      //* convert ushort to bool[12]
      bool[] bools = new bool[12];
      if (ushrt == 0) return bools;
      ushort mask = 1;
      for (int i = 0; i < 12; i++) {
        bools[i] = (ushrt & mask) > 0;
        mask <<= 1;
      }
      return bools;
    }

    internal static ushort BoolArrayToUShort(bool[] bools) {
      //* convert bool[12] to ushort
      ushort mask = 1;
      ushort int16 = 0;
      for (int i = 0; i < 12; i++) {
        if (bools[i]) int16 |= mask;
        mask <<= 1;
      }
      return int16;
    }

    internal static string PtrToDesc(sbyte ptr) {
      if (ptr == 0) return "";
      if (ptr > 0) return ChordAnalysis.PtrToDesc(ptr);
      return ChordDB.PtrToDesc(ptr);
    }

    internal static sbyte DescToPtr(string desc) {
      return ChordDB.DescToPtr(desc);
    }

    internal static bool[] Transpose(bool[] src, int trans) {
      //* transpose bool[12] chord
      trans = (trans).Mod12();
      bool[] dest = new bool[12];
      if (src.SequenceEqual(dest)) return src;  //all false 
      for (int i = trans, j = 0; j < 12; i++, j++) {
        if (i == 12) i = 0;
        dest[i] = src[j];
      }
      return dest;
    }

    private class clsWChord {
      internal clsMTime.clsBBT BBT;
      internal bool[] Chord;

      internal clsWChord(int qi, bool[] chord) {
        clsFileStream csvfileconv = P.F.frmChordMap.CSVFileConv;
        BBT = new clsMTime.clsBBT(qi * P.F.TicksPerQI);
        Chord = chord;
      }

      internal clsWChord(clsMTime.clsBBT bbt, bool[] chord) {
        clsFileStream csvfileconv = P.F.frmChordMap.CSVFileConv;
        BBT = bbt;
        Chord = chord;
      }

      internal bool IsEmpty() {
        foreach (bool b in Chord) {
          if (b) return false;
        }
        return true;
      }

      internal bool IsSubChord(clsWChord chord) {
        //* return true this is a subchord of chord
        for (int i = 0; i < 12; i++) {
          if (Chord[i] && !chord.Chord[i]) return false;
        }
        return true;
      }
    }
  }
}
