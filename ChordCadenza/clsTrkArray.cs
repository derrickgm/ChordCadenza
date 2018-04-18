using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace ChordCadenza {
  internal class clsTrks {
    internal const int NumTrksMax = 64;
    private int _NumTrks = -1;
    internal int NumTrks { get { return _NumTrks; } }
    internal Array<int> Ints;  //always default values - only used for foreach...

    internal clsTrks() {
      Ints = new Array<int>(this);
    }

    internal bool SetNumTrks(int num) {
      if (_NumTrks >= 0 && _NumTrks != num) return false;  //already set
      if (num > NumTrksMax) return false;  //too many trks 
      _NumTrks = num;
      return true;
    }

    internal T AddTrack() {
      if (_NumTrks >= NumTrksMax) return null;
      _NumTrks++;
      return new T(this, _NumTrks - 1);  //last track number
    }

    internal T SubTrack() {
      if (_NumTrks <= 0) return null;
      _NumTrks--;
      return new T(this, _NumTrks - 1);  //last track number
    }

    internal class T {
      private clsTrks Trks;
      internal int TrkNum;

      internal T(clsTrks trks, int trk) {
        Trks = trks;
        if (trk >= trks._NumTrks || trk < 0) {
          LogicError.Throw(eLogicError.X144);
          //TrkNum = 0;
        }
        TrkNum = trk;
      }

      public override string ToString() {
        return (TrkNum + 1).ToString();  //display starts at 1
      }

      internal T Copy() {
        return new T(Trks, TrkNum);
      }

      public override bool Equals(System.Object obj) {
        if (obj == null) return false;
        T a = obj as T;
        if ((object)a == null) return false;  //not able to cast as clsTrks.T
        return TrkNum == a.TrkNum;
      }

      public override int GetHashCode() {
        return TrkNum.GetHashCode();
      }

      public static bool operator ==(T a, T b) {
        if ((object)a == null && (object)b == null) return true;
        if ((object)a == null || (object)b == null) return false;
        return a.TrkNum == b.TrkNum;
      }

      public static bool operator !=(T a, T b) {
        return !(a == b);
      }

      internal static bool TrkOOR(uint trk) {
        return trk >= NumTrksMax;
      }
    }

    internal class Array<T> : IEnumerable<T> {
      private clsTrks Trks;
      internal delegate U Del<U>();
      private T[] Arr = new T[NumTrksMax];

      internal Array() : this(P.F.Trks) {
      }

      internal Array(clsTrks trks) {
        Trks = trks;
      }

      internal Array(T val) : this(P.F.Trks, val) {  //structs only
      }

      internal Array(clsTrks trks, T val) : this(trks) {  //structs only
        for (int trk = 0; trk < NumTrksMax; trk++) Arr[trk] = val;
      }

      internal Array(Del<T> initializer) : this(P.F.Trks, initializer) {
      }

      internal Array(clsTrks trks, Del<T> initializer) : this(trks) {
        for (int trk = 0; trk < NumTrksMax; trk++) Arr[trk] = initializer();
      }

      internal T this[clsTrks.T trk] {
        get { return Arr[trk.TrkNum]; }
        set { Arr[trk.TrkNum] = value; }
      }

      public IEnumerator<T> GetEnumerator() {
        for (int trk = 0; trk < Trks._NumTrks; trk++) {
          yield return Arr[trk];
        }
      }

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
        return GetEnumerator();
      }

      internal IEnumerable<clsTrks.T> Next {
        get {
          for (int trk = 0; trk < Trks._NumTrks; trk++) {
            yield return new clsTrks.T(Trks, trk);
          }
        }
      }
    }
  }
}