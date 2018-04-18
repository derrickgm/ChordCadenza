using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChordCadenza {
  internal class clsOnOff {
    //contains one event for each midi ON or OFF
    internal class clsEv {
      internal clsTrks.T Trk;
      internal int Note;
      internal int Count;
      internal clsEv(int note, clsTrks.T trk, int count) {
        Trk = trk;
        Note = note;
        Count = count;
      }
    }

    internal clsOnOff(int len) {
      Evs = new List<clsEv>[len];
    }

    private List<clsEv>[] Evs;
    //private int NumTrks;

    internal int this[int qi, int note, clsTrks.T trk, bool mod] {  //return/set count 
      get {
        int modval = 128;
        if (mod) modval = 12;
        int evcount = 0;
        if (Evs[qi] == null) return 0;
        foreach (clsEv ev in Evs[qi]) {
          if (trk != ev.Trk || note != (ev.Note % modval)) continue;
          evcount += ev.Count;
        }
        return evcount;
      }
      set {
        //if (qi == 84) Debugger.Break();  //tmp
        if (mod) {
          LogicError.Throw(eLogicError.X068);
        }
        if (Evs[qi] == null) Evs[qi] = new List<clsEv>();
        List<clsEv> evs = Evs[qi];
        for (int i = 0; i < evs.Count; i++) {
          clsEv ev = evs[i];
          if (note != ev.Note || trk != ev.Trk) continue;
          ev.Count = value;
          return;
        }
        evs.Add(new clsEv(note, trk, value));
      }
    }

    internal int this[int qi, int note] {  //all chans, mod=true
      get {
        int ret = 0;
        foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {
          ret += this[qi, note, trk, true];
        }
        return ret;
      }
    }

    internal bool this[int qi] {  //return true if any event is present
      get {
        return (Evs[qi] != null);
      }
    }

    internal int Length {
      get {
        return Evs.Length;
      }
    }
  }
}