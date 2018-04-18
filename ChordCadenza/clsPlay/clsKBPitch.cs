using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

namespace ChordCadenza {
  internal abstract partial class clsPlay {
    internal class clsKBPitch {
      private Lst List;
      private int? _KB;  //null = sustained pitch
      internal int Pitch;
      internal bool TimerActive;

      internal clsKBPitch(Lst kbplist, int? kb, int pitch) {
        List = kbplist;
        _KB = kb;
        Pitch = pitch;
        TimerActive = false;
      }

      internal int? KB {
        get {
          return _KB;
        }
        //set {
        //  if (value.HasValue && List.ContainsKB(value.Value)) throw new LogicException();
        //  _KB = value;  //switch sustained off
        //}
      }

      internal bool Sustained {
        get {
          return !_KB.HasValue;
        }
        set {
          if (!value) {
            LogicError.Throw(eLogicError.X030);  //no kb
          }
          _KB = null;
        }
      }

      internal class Lst {
        private List<clsKBPitch> List;   //<kb, pitch> 
        internal Object Lock = new Object();

        internal Lst() {
          List = new List<clsKBPitch>(12);
        }

        //internal clsKBPitch this[int i] {
        //  get { return List[i]; }
        //  set { List[i] = value; }
        //}

        //public IEnumerator<clsKBPitch> GetEnumerator() {
        //  for (int i = 0; i < List.Count; i++) {
        //    yield return List[i];
        //  }
        //}

        //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
        //  return GetEnumerator();
        //}

        internal bool ContainsKB(int kb) {
          foreach (clsKBPitch kbp in List) {  
            if (!kbp._KB.HasValue) continue;
            if (kbp._KB == kb) return true;
          }
          return false;
        }

        internal void Add(int kb, int pitch) {
          //if (ContainsKB(kb)) {  //kb already present
          //  clsKBPitch kbp = GetKB(kb);   //get kb from kbpitchlist
          //  if (kbp.Pitch != pitch) throw new LogicException();  //pitches don't match
          //  return;  //no action required (incl. TimerActive)
          //}
          List<clsKBPitch> lst = GetKBPitches(kb);
          foreach (clsKBPitch kbp in lst) {
            if (kbp.Pitch == pitch) return;  //already there
          }
          List.Add(new clsKBPitch(this, kb, pitch));
        }

        //internal void Add(clsKBPitch kbp) {
        //  if (kbp._KB.HasValue && ContainsKB(kbp._KB.Value)) throw new LogicException();
        //  List.Add(kbp);
        //}

        internal int Count {
          get { return List.Count; }
        }

        internal bool[] GetKBDownMod() {
          bool[] ret = new bool[12];
          foreach (clsKBPitch kbp in List) {
            if (kbp.KB.HasValue) ret[kbp.KB.Value.Mod12()] = true;
          }
          return ret;
        }

        internal List<clsKBPitch> GetKBPitches(int kb) {
          List<clsKBPitch> lst = new List<clsKBPitch>(6);
          foreach (clsKBPitch kbp in List) { 
            if (kbp._KB == kb) lst.Add(kbp);
          }
          return lst;
        }

        internal List<clsKBPitch> GetPitch(bool? sustained, int pitch) {
          //* return all entries associated with pitch
          List<clsKBPitch> ret = new List<clsKBPitch>(12);
          foreach (clsKBPitch kbp in List) {  
            if (sustained.HasValue) {
              if (sustained.Value == kbp._KB.HasValue) continue;
            }
            if (kbp.Pitch == pitch) ret.Add(kbp);
          }
          return ret;
        }

        internal List<clsKBPitch> GetSustained() {
          //* return all sustained entries 
          List<clsKBPitch> ret = new List<clsKBPitch>(12);
          foreach (clsKBPitch kbp in List) {  
            if (kbp._KB.HasValue) continue;
            ret.Add(kbp);
          }
          return ret;
        }

        internal List<clsKBPitch> GetUnsustained() {
          //* return all unsustained entries 
          List<clsKBPitch> ret = new List<clsKBPitch>(12);
          foreach (clsKBPitch kbp in List) {  
            if (kbp == null || !kbp._KB.HasValue) continue;
            ret.Add(kbp);
          }
          return ret;
        }

        internal void Remove(clsKBPitch kbp) {
          List.Remove(kbp);
        }
      }
    }
  }
}


