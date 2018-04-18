using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Drawing;

namespace ChordCadenza {
  internal class clsKey {
    internal string KeyNoteStr;
    internal string Scale;
    internal int KeyNote;   //pitch of keynote (eg C=0) (0-11)
    internal int MidiKey;  //-7 - +7 (eg Cmajor = Aminor = 0)

    internal bool IsEquiv(clsKeyTicks key) {  //should be called in preference to Equals(obj)
      if (key == null) return false;
      if (MidiKey == key.MidiKey && Scale == key.Scale) return true;
      return false;
    }

    internal bool Major { get { return Scale == "major"; } }
    internal bool Minor { get { return Scale == "minor"; } }

    internal int MajMin {  //0=major; 1=minor
      get {
        if (Scale == "minor") return 1; else return 0;
      }
    }

    internal string KeyNoteStr_ToSharpFlat {
      get { return NoteName.ToSharpFlat(KeyNoteStr); }
    }

    protected int GetTransposeKB() {
      if (P.F == null || P.frmSC == null) return 0;
      if (P.frmSC.Play == null) return 0;
      return P.frmSC.Play.TransposeKB;
    }

    internal string KeyStrShort {
      get {
        if (Scale == "major") return KeyNoteStr_ToSharpFlat;
        return KeyNoteStr_ToSharpFlat + "m";
      }
    }

    internal string KeyStrLong {
      get {
        if (Scale == "major") return KeyNoteStr_ToSharpFlat + " Major";
        return KeyNoteStr_ToSharpFlat + " Minor";
      }
    }

    public override string ToString() {
      return KeyStrLong;
    }
  }

  internal class clsKeyTicks : clsKey {
    internal int Ticks;
    internal bool? TransposeNamesSharp = null;  //#/b/null for transposed names keys (not these keys!)

    //internal List<string> Desc = new List<string>;
    //private clsMTime MTime;

    internal clsKeyTicks(int keynote, string scale, int ticks, bool indkeynote) {
      if (!indkeynote) {
        LogicError.Throw(eLogicError.X014);
      }
      Init(scale, ticks);
      KeyNote = keynote;
      MidiKey = NoteName.PitchToMidiKey(KeyNote, scale, null);
      KeyNoteStr = NoteName.MidiKeyToKeyStr(MidiKey, Scale);
    }

    internal clsKeyTicks(string keynotestr, string scale, int ticks) {
      Init(scale, ticks);
      KeyNoteStr = keynotestr;
      KeyNote = NoteName.GetPitch(KeyNoteStr);
      MidiKey = NoteName.GetMidiKey(KeyNoteStr, Scale);
    }

    internal clsKeyTicks(int midikey, string scale, int ticks) {
      Init(scale, ticks);
      MidiKey = midikey;
      KeyNoteStr = NoteName.MidiKeyToKeyStr(midikey, Scale);
      KeyNote = NoteName.MidiKeyToPitch(midikey, scale);
    }

    internal clsKeyTicks(clsKeyTicks key) {  //clone
      TransKB(key, false);
    }

    internal clsKeyTicks(clsKey key, int ticks) : this(key.MidiKey, key.Scale, ticks) {
    }

    internal clsKeyTicks(clsKeyTicks key, bool kbtrans) {  //clone and transpose
      TransKB(key, kbtrans);
    }

    private void TransKB(clsKeyTicks key, bool kbtrans) {
      TransposeNamesSharp = key.TransposeNamesSharp;
      Scale = key.Scale;
      Ticks = key.Ticks;
      if (kbtrans) {
        int trans = GetTransposeKB();
        KeyNote = (trans == 0) ? key.KeyNote : (key.KeyNote + trans).Mod12(); 
        MidiKey = (trans == 0) ? key.MidiKey : NoteName.PitchToMidiKey(KeyNote, Scale, null);
        //KeyNoteStr = (trans == 0) ? key.KeyNoteStr : NoteName._Names[MidiKey + 7][KeyNote];
        KeyNoteStr = (trans == 0) ? key.KeyNoteStr : NoteName.GetName(this, KeyNote);
      } else {
        KeyNoteStr = key.KeyNoteStr;
        KeyNote = key.KeyNote;
        MidiKey = key.MidiKey;
      }
    }

    private void Init(string scale, int ticks) {
      Scale = scale.ToLower();
      Ticks = ticks;
    }

    internal clsMTime.clsBBT BBT {
      get {
        return new clsMTime.clsBBT(Ticks);
      }
    }

    internal clsKeyTicks GetTranspose(int t) {
      //* indsharp: true{#), false{b}, null(middle) 
      if (t == 0) return this;
      int keynote = (KeyNote + t).Mod12();
      int midikey = NoteName.PitchToMidiKey(keynote, Scale, null);
      return new clsKeyTicks(midikey, Scale, Ticks);
    }

    internal clsKeyTicks GetTransposeNames(int t) {
      //* indsharp: true{#), false{b}, null(middle) 
      if (t == 0) return this;
      int keynote = (KeyNote + t).Mod12();
      int midikey = NoteName.PitchToMidiKey(keynote, Scale, TransposeNamesSharp);
      return new clsKeyTicks(midikey, Scale, Ticks);
    }

    internal string[] List {
      get {
        return new string[] {
          (BBT.Bar+1).ToString(),
          (BBT.BeatsRemBar+1).ToString(),
          (BBT.TicksRemBeat+1).ToString(),  
          NoteName.ToSharpFlat(KeyNoteStr) + " " + Scale
        };
      }
    }
  }

  //internal class clsUndoRedoKeys {
  //  internal Stack<clsKeysTicks> UndoStack = new Stack<clsKeysTicks>();
  //  internal Stack<clsKeysTicks> RedoStack = new Stack<clsKeysTicks>();

  //  internal void Update() {
  //    UndoStack.Push(new clsKeysTicks(P.F.Keys));
  //    P.F.CF.indSave = true;
  //  }

  //  internal void Undo() {
  //    RedoStack.Push(P.F.Keys);
  //    if (UndoStack.Peek().indCF) P.F._CFKeys = UndoStack.Pop();
  //    else P.F._MidiKeys = UndoStack.Pop();
  //    P.F.Keys.Finish();
  //    P.F.CF.indSave = true;
  //  }

  //  internal void Redo() {
  //    UndoStack.Push(P.F.Keys);
  //    if (RedoStack.Peek().indCF) P.F._CFKeys = RedoStack.Pop();
  //    else P.F._MidiKeys = RedoStack.Pop();
  //    P.F.Keys.Finish();
  //    P.F.CF.indSave = true;
  //  }
  //}

  //internal class clsKeys : IEnumerable<clsKey> {
  internal class clsKeysTicks {
    //  public IEnumerator<clsKey> GetEnumerator() {
    //  for (int i = 0; i < Keys.Count; i++) {
    //    yield return Keys[i];
    //  }
    //}

    //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
    //  return GetEnumerator();
    //}

    //internal clsKeysTicks(clsKeysTicks keys) {  //clone
    //  foreach (clsKeyTicks key in keys.Keys) Keys.Add(key);
    //}

    internal clsKeysTicks(clsKeyTicks key) {
      Keys.Add(key);
    }

    internal clsKeysTicks(List<clsKeyTicks> keys) {
      Keys = keys;
      
    }

    internal clsKeysTicks(string keynotestr, string scale) {
      Add(keynotestr, scale, 0);
    }

    internal clsKeysTicks(int midikey, string scale) {
      Add(midikey, scale, 0);
    }

    internal clsKeysTicks(clsKeysTicks keys) {  //copy
      //Keys = new List<clsKeyTicks>();
      //foreach (clsKeyTicks key in keys.Keys) Keys.Add(new clsKeyTicks(key));  //copy
      Keys = keys.Keys.ToList();
    }

    //internal clsKeys(clsMTimeNew mtime) {
    //  MTime = mtime;
    //}

    //****** fields (update copy constructor if changed)
    internal List<clsKeyTicks> Keys = new List<clsKeyTicks>();

    //internal bool indCF;  //true if sourced from _CFKeys, else sourced from _MidiKeys 

    internal clsKeyTicks GetKey(int index, bool kbtrans = false) {
      if (!kbtrans) return Keys[index];
      return new clsKeyTicks(Keys[index], kbtrans);
    }

    internal void TransposeKey(int index, int val) {
      Keys[index] = Keys[index].GetTranspose(val);
    }

    internal void Add(string keynotestr, string scale, int ticks) {
      //* called by chordfile
      Keys.Add(new clsKeyTicks(keynotestr, scale, ticks));
      Finish();
    }

    internal void Add(int midikey, string scale, int ticks) {
      //* called by loadmidifile
      if (Keys.Count > 0) {
        clsKeyTicks prevkey = Keys[Keys.Count - 1];
        //if (ticks < prevkey.Ticks) throw new FatalException();
        if (ticks < prevkey.Ticks) {
          LogicError.Throw(eLogicError.X081);
          return;
        }
        clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);  //should have (partial) mtime created by now 
        if (bbt.TicksRemBeat != 0) {
          LogicError.Throw(eLogicError.X104, "key ticks not aligned to bar");
          clsMTime.clsBBT bbtbar = new ChordCadenza.clsMTime.clsBBT(bbt.Bar, 0, 0);
          ticks = bbtbar.Ticks;
        }
        if (ticks == prevkey.Ticks) {
          //* replace key, but retain ticks
          Keys.RemoveAt(Keys.Count - 1);
          Keys.Add(new clsKeyTicks(midikey, scale, prevkey.Ticks));
          return;
        }
      }
      Keys.Add(new clsKeyTicks(midikey, scale, ticks));
      Finish();
    }

    internal void Insert(clsKeyTicks key) {   //replace if ticks are equal
      int index = GetIndex(key.Ticks);
      if (Keys[index].Ticks == key.Ticks) Keys.RemoveAt(index); else index++;
      Keys.Insert(index, key);
      Finish();
    }

    internal void Change(int index, clsKeyTicks key) {
      Keys[index] = key;
      Finish();
    }

    internal void RemoveAt(int index) {
      Keys.RemoveAt(index);
      Finish();
    }

    internal void Finish() {
      //P.F?.CF?.TransposeMidi(0);
      RemoveDups();
      if (P.F?.CF != null) P.F.CF.SyncEvsToKeys();  
    }

    //internal void Truncate(int ticks) {  //remove keys after ticks
    //  int index = GetIndex(ticks);
    //  if (index >= Keys.Count - 1) return;  //last key in keys
    //  Keys.RemoveRange(index + 1, Keys.Count - index - 1); 
    //}

    private void RemoveDups() {
      //remove any keys which are equal and adjacent
      for (int i = 1; i < Keys.Count; i++) {  //start at second key
        if (Keys[i].MidiKey == Keys[i - 1].MidiKey && Keys[i].Scale == Keys[i - 1].Scale) {
          Keys.RemoveAt(i--);
        }
      }
    }

    internal clsKeyTicks this[int ticks] {
      get {
        return Keys[GetIndex(ticks)];
      }
    }

    internal clsKeyTicks this[int ticks, bool kbtrans] {
      get {
        clsKeyTicks key = Keys[GetIndex(ticks)];
        if (key == null) return null;
        return new clsKeyTicks(key, kbtrans);
      }
    }

    internal int GetIndex(int ticks) {
      int i = 1;
      for (; i < Keys.Count; i++) if (Keys[i].Ticks > ticks) break;
      return i - 1;
    }

    internal clsKeyTicks GetPrev(int ticks, bool kbtrans = false) {
      //if (ModKey != null) return null;  //always modkey
      int i = 1;
      for (; i < Keys.Count; i++) if (GetKey(i, kbtrans).Ticks > ticks) break;
      if (i > 1) return GetKey(i - 2, kbtrans); else return null;
    }

    internal clsKeyTicks GetNext(int ticks, bool kbtrans = false) {
      //if (ModKey != null) return null;  //always modkey
      int i = 1;
      for (; i < Keys.Count; i++) if (GetKey(i, kbtrans).Ticks > ticks) break;
      if (i < Keys.Count) return GetKey(i, kbtrans); else return null;
    }

    internal clsKeyTicks[] GetChanges(int tickslo, int tickshi) {
      //return key changes (if any) between tickslo and tickshi
      //does not include initial key
      //if (ModKey != null) return new clsKey[0];  //no changes if ModKey set
      List<clsKeyTicks> list = new List<clsKeyTicks>();
      for (int i = 1; i < Keys.Count; i++) {
        if (Keys[i].Ticks > tickshi) break;
        if (Keys[i].Ticks >= tickslo) list.Add(Keys[i]);
      }
      return list.ToArray();
    }
  }
}