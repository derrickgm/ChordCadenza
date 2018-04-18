using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace ChordCadenza {
  internal class clsMap<T> :IEnumerable<KeyValuePair<int, T>> where T: struct {
    // assumes keys are added in order
    private List<int> Keys = new List<int>();
    private List<T> Values = new List<T>();
    private List<KeyValuePair<int, T>> KeyValues = new List<KeyValuePair<int, T>>();
    private T Dflt;

    internal clsMap(T dflt) {
      Dflt = dflt;
    }

    internal void Add(int ticks, T val) {
      //if (val == null) {
      //  LogicError.Throw(eLogicError.X052);
      //  return;
      //}
      if (Keys.Count > 0) {
        if (Keys[Count - 1] == ticks) return;  //use first key only
        if (Keys[Count - 1] > ticks) {   //assumed infrequent!
          int index = Keys.BinarySearch(ticks);
          if (index >= 0) return;  //key already exists - use first key only
          index = ~index;  //index after ticks
          Keys.Insert(index, ticks);
          Values.Insert(index, val);
          LogicError.Throw(eLogicError.X157);  //not in order - warning (for now)
          return;
        }
      }
      Keys.Add(ticks);
      Values.Add(val);
    }

    internal int Count { get { return Keys.Count; } }

    internal T this[int ticks] {
      get {
        int index = Keys.BinarySearch(ticks);
        if (index < 0) index = ~index - 1;  //previous element if no match
        if (index < 0) return Dflt;  //before first value
        return Values[index];
      }
    }

    internal T GetFirstValue() {
      return Values[0];
    }

    public IEnumerator<KeyValuePair<int, T>> GetEnumerator() {
      for (int i = 0; i < Keys.Count; i++) {
        yield return new KeyValuePair<int, T>(Keys[i], Values[i]);
      }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }
  }

}