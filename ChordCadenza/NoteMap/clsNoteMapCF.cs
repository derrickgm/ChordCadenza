#undef MemoryInfo

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ChordCadenza {
  internal class clsNoteMapCF : clsNoteMap {
    //internal clsNoteMapCF() {  //default
    //  if (P.F.MaxTicks <= 0) Debugger.Break();
    //  if (P.F.TicksPerQI <= 0) Debugger.Break();
    //  int len = P.F.MaxTicks / P.F.TicksPerQI;
    //  NewMap = new ushort[len];

    //}

    internal clsNoteMapCF(clsCF cf, clsOnOff onoff) {
      CF = cf;
#if MemoryInfo
      long before = GC.GetTotalMemory(true);
      Debug.WriteLine("clsNoteMapCF len = " + len);
      Debug.WriteLine("memory before = " + before + " bytes");
#endif
      _Map = new ushort[P.F.MaxBBT.MaxNoteMapQI];
      //Map = new clsMap(len, 12);
#if MemoryInfo
      long after = GC.GetTotalMemory(true);
      Debug.WriteLine("memory after = " + after + " bytes");
      Debug.WriteLine("diff = " + (after - before) + " bytes");
#endif
      _ChordAtt = new sChordAtt[P.F.MaxBBT.MaxNoteMapQI];
      for (int qi = 0; qi < P.F.MaxBBT.MaxNoteMapQI; qi++) {
        _ChordAtt[qi] = new sChordAtt(0);   //root -1 etc.
      }
      if (onoff != null) InitMap(onoff);
      //CF.UndoRedoCF = new clsCF.clsUndoRedo(this);
      //CF.UndoRedoCF.Active = true;
    }

    private sChordAtt[] _ChordAtt;
    private clsCF CF;
    //internal clsUndoRedo UndoRedo;
    private ushort[] _Map;

    //internal override bool QI_OOR(int qi) {
    //  return (qi >= P.F.MaxBBT.QI || qi >= NewMap.Length);
    //}

    private void InitMap(clsOnOff onoff) {
      for (int note = 0; note < 12; note++) {
        int cnt = 0;
        for (int qtime = 0; qtime < onoff.Length; qtime++) {
          cnt += onoff[qtime, note, new clsTrks.T(P.F.CFTrks, 0), true];  //trk0 (only one trk)
          if (cnt > 0) this[qtime, note] = true;
        }
      }
    }

    public override bool[] this[int qtime] {  //return array of modded notes 
      get {
        //if (QI_OOR(qtime)) return new bool[12];
        return UShortToBoolArray(_Map[qtime]);
      }
      set {
        //if (QI_OOR(qtime)) return;
        _Map[qtime] = BoolArrayToUShort(value);
      }
    }

    internal ushort GetMap(int qtime) {
      return _Map[qtime];
    }

    internal void SetMap(int qtime, ushort val) {
      _Map[qtime] = val;
    }

    internal void NewMapAndAtts() {
      _Map = new ushort[P.F.MaxBBT.MaxNoteMapQI];
      _ChordAtt = new sChordAtt[P.F.MaxBBT.MaxNoteMapQI];
    }

    internal void ReInitMapAndAtts() {
      ushort[] map = _Map;
      sChordAtt[] chordatt = _ChordAtt;
      NewMapAndAtts();
      for (int i = 0; i < _Map.Length && i < map.Length; i++) _Map[i] = map[i];
      for (int i = 0; i < _ChordAtt.Length && i < chordatt.Length; i++) _ChordAtt[i] = chordatt[i];
    }

    internal sChordAtt GetChordAtt(int q) {
      return _ChordAtt[q];
    }

    internal void SetChordAtt(int q, sChordAtt val) {
      _ChordAtt[q] = val;
    }

    //internal override void Realloc(int qlen) {
    //  if (qlen == _Map.Length) return;
    //  ushort[] newmap = new ushort[qlen];
    //  sChordAtt[] chordatt = new sChordAtt[qlen];
    //  for (int q = 0; q < qlen; q++) {
    //    if (q >= _Map.Length) {
    //      newmap[q] = 0;
    //      chordatt[q] = new sChordAtt(0);
    //    } else {
    //      newmap[q] = _Map[q];
    //      chordatt[q] = _ChordAtt[q];
    //    }
    //  }
    //  _ChordAtt = chordatt;
    //  _Map = newmap;
    //}

    public bool[] GetBoolChord(int qtime, eKBTrans kbtrans) {
      bool[] ch = this[qtime];
      if (kbtrans == eKBTrans.None) return ch;
      int x = (kbtrans == eKBTrans.Add) ? P.frmSC.Play.TransposeKB : -P.frmSC.Play.TransposeKB;
      bool[] chtrans = new bool[12];
      for (int pc = 0; pc < 12; pc++) {
        if (ch[pc]) chtrans[(pc + x).Mod12()] = true;
      }
      return chtrans;
    }

    public override bool this[int qtime, int note] {  //trk0 (should be same as all trks)
      get {
        //if (QI_OOR(qtime)) return false;
        return (_Map[qtime] & NoteMask12[note]) > 0;
      }
      set {
        //if (QI_OOR(qtime)) return;
        if (value) _Map[qtime] |= NoteMask12[note];
        else _Map[qtime] &= (ushort)~NoteMask12[note];
      }
    }

    //internal override int GetLengthQTime() {
    //  return NewMap.Length;
    //}

    internal void SetChordAndAtts(int q, bool[] chord) {
      //CF.UndoRedoCF.ActionQI(q);
      this[q] = chord;
      CalcChordAtts(q);
    }

    public override void NullifyChordinateStatus(int qi) {
      //if (qi >= P.F.MaxBBT.QI || qi >= _ChordAtt.Length) return;
      if (qi >= P.F.MaxBBT.QI) return;
      _ChordAtt[qi] = new sChordAtt(0);
    }

    internal void TransposeChord(int q, int val) {
      //CF.UndoRedoCF.ActionQI(q);
      //if (QI_OOR(q)) return;
      bool[] inchord = this[q];
      List<int> pclist = new List<int>(5);
      for (int i = 0; i < 12; i++) {
        if (inchord[i]) pclist.Add((i + val).Mod12());
      }
      bool[] outchord = new bool[12];
      foreach (int pc in pclist) outchord[pc] = true;
      this[q] = outchord;  
      int root = _ChordAtt[q].Root;
      if (root >= 0) {  //-1 = noroot
        root += val;
        _ChordAtt[q].Root = (sbyte)root.Mod12();
      }
    }

    internal void SetChordAndAtts(int q, int note, bool val) {
      //CF.UndoRedoCF.ActionQI(q);
      //if (QI_OOR(q)) return;
      this[q, note] = val;
      CalcChordAtts(q);
    }

    internal void SetChordAndAtts(int q, bool[] chord, sChordAtt att) {
      SetChordAndAtts(q, BoolArrayToUShort(chord), att);
    }

    internal void SetChordAndAtts(int q, ushort chord, sChordAtt att) {
      //CF.UndoRedoCF.ActionQI(q);
      //if (QI_OOR(q)) return;
      _Map[q] = chord;
      _ChordAtt[q] = att;
    }

    private ChordDB.clsDesc GetAndSetChordAtts_Desc = null;
    private bool[] GetAndSetChordAtts_BoolMap = new bool[12]; 
    private void CalcChordAtts(int qtime) {
      //* search for chord in ChordDB
      //if (QI_OOR(qtime)) return;
      bool[] map = this[qtime];
      ChordDB.clsDesc desc;
      if (map.SequenceEqual(GetAndSetChordAtts_BoolMap)) {
        desc = GetAndSetChordAtts_Desc;  //same as last call
      } else {
        desc = ChordDB.GetChord(this[qtime]);
        GetAndSetChordAtts_BoolMap = map;
        GetAndSetChordAtts_Desc = desc;
      }
      if (desc == null) _ChordAtt[qtime] = new sChordAtt(0);
      else _ChordAtt[qtime] = new sChordAtt((sbyte)desc.Root, desc.NoteMapPtr);
      //if (qtime == 9) qtime = qtime;  //temp debugging
    }

    internal void CopySeg(int srcqi, int destqi, int qilen, int transpose) {
      //* copy segment within NewMap (eg bar)
      if (Math.Abs(srcqi - destqi) < qilen) {    //no overlap allowed
        LogicError.Throw(eLogicError.X060);
        return;
      }
      //P.F.CF.UndoRedoCF.StartEvs();
      for (int i = 0; i < qilen; i++) {
        bool[] newchord = Transpose(this[srcqi + i], transpose);
        SetChordAndAtts(destqi + i, newchord);  //-> calc chordatts
      }
    }

    //internal void CopyQI(int srcqi, int destqi) {
    //  //* copy one qi - no transpose or undo/redo (used by frmNoteMap.ChSymbols...)
    //  SetChordAndAtts(destqi, NewMap[srcqi], ChordAtt[srcqi]);
    //}

    internal bool PropagateNoteMapBeat(clsMTime.clsBBT bbt, string txt, int qilo, int qihi) {
      clsCFPC.clsEv ev;  //temp ev
      try {
        ev = new clsCFPC.clsEvPC(P.F.CF, new string[] { "", txt });
      }
      catch (ChordFileException) { //invalid chord
        return false;
      }
      ev.OnBBT = bbt;  
      ev.OffBBT = new clsMTime.clsBBT(bbt.Beats + 1, true);  //one beat
      bool[] boolarray = new bool[12];
      foreach (clsCFPC.clsEv.clsNote note in ev.Notes) boolarray[note.PC[eKBTrans.None]] = true;
      for (int qi = qilo; qi < qihi; qi++) {  //was <=
        int root = (ev.Root) ? ev.Notes[0].PC[eKBTrans.None] : -1;
        sChordAtt att = new sChordAtt(
          (sbyte)root,
          clsNoteMap.DescToPtr(ev.ChordQualifier));
        SetChordAndAtts(qi, boolarray, att);
        //if (qi == 9) qi = qi;  //temp debugging
      }
      return true;
    }

    internal override bool IsF(int qi, int note) {
      return this[qi, note];
    }

    internal override bool[] IsF(int qi) {
      return (this[qi]);
    }

    internal void RemoveNotes(int qtime) {
      //if (QI_OOR(qtime)) return;
      if (qtime >= _Map.Length) return;
      //SetChord(qtime, new bool[12]); 
      //P.F.CF.UndoRedoCF.StartEvs();
      SetChordAndAtts(qtime, 0, new sChordAtt(0));
    }

    internal override void CalcKeys_CalcSegQI(int[] seg, int qi, ref bool indempty) {
      for (int note = 0; note < 12; note++) {
        if (this[qi, note]) {
          seg[note]++;
          indempty = false;
        }
      }
    }

    internal struct sChordAtt {
      internal sbyte Qualifier;
      //internal clsRiffs.eChordType RiffType;
      internal sbyte Root;
      //internal ChordAnalysis.eStatus ChordinateStatus;
      private sbyte Flags;   //0=unmarked; 1=marked

      internal sChordAtt(int dummy) {  //should be able to make parameterless in C#6 (VS2015)
        Qualifier = 0;
        Root = -1;
        //ChordinateStatus = ChordAnalysis.eStatus.NotChordinated;
        Flags = 0;
      }

      //internal sChordAtt(ChordAnalysis.eStatus status) {  
      //  Qualifier = 0;
      //  Root = -1;
      //  //ChordinateStatus = status;
      //  Flags = 0;
      //}

      internal sChordAtt(sbyte root, sbyte qualifier) {
        Qualifier = qualifier;
        Root = root;
        //ChordinateStatus = chordinatestatus;
        Flags = 0;
      }

      private bool GetFlag(int mask) {
        return (Flags & mask) == mask;
      }


      private void SetFlag(sbyte mask, bool value) {
        Flags = (sbyte)((value) ? Flags | mask : Flags & ~mask); 
      }

      internal bool ChordEquals(sChordAtt att) {
        //* should check notemap before using this
        if (Root < 0 && att.Qualifier == 0) return true; //both undefined 
        if (att.Root < 0 && Qualifier == 0) return true; //both undefined
        if (Root != att.Root) return false;
        if (Root >= 0 && Qualifier != att.Qualifier) return false;
        return true;
      }
    }

    //internal class clsUndoRedo {
    //  private class clsQI {
    //    internal clsQI(int qtime,  bool[] chord, sChordAtt att) {  //convert bool[] to ushort
    //      QTime = qtime;
    //      ChordAtt = att;
    //      Chord = chord;
    //    }

    //    internal clsQI(int qtime, ushort chord, sChordAtt att) {  //no chord conversion
    //      QTime = qtime;
    //      ChordAtt = att;
    //      UShort = chord;
    //    }

    //    internal int QTime;
    //    private sChordAtt ChordAtt;
    //    private ushort UShort;  //chord

    //    private bool[] Chord {
    //      get {
    //        return UShortToBoolArray(UShort);
    //      }
    //      set {
    //        UShort = BoolArrayToUShort(value);
    //      }
    //    }

    //    internal void StackPush(Stack<clsQI> stack) {
    //      clsNoteMapCF ncf = P.F.CF.NoteMap;
    //      stack.Push(new clsQI(QTime, ncf.NewMap[QTime], ncf.ChordAtt[QTime]));
    //    }

    //    internal void Update() {
    //      clsNoteMapCF ncf = P.F.CF.NoteMap;
    //      ncf.NewMap[QTime] = UShort;
    //      ncf.ChordAtt[QTime] = ChordAtt;
    //    }
    //  }

    //  //private class clsTSigs : clsEntry {
    //  //  internal clsTSigs(clsMTime mtime) {
    //  //    MTime = mtime;
    //  //  }

    //  //  private clsMTime MTime;

    //  //  internal override void StackPush(Stack<clsEntry> stack) {
    //  //    stack.Push(new clsTSigs(P.F.MTime));
    //  //  }

    //  //  internal override void Update() {
    //  //    P.F.MTime = MTime;
    //  //  }

    //  //  internal override void GetQIMinMaxUndo(ref int? minqi, ref int? maxqi) {
    //  //    minqi = -1;
    //  //    maxqi = -1;
    //  //  }

    //  //  internal override void GetQIMinMaxRedo(ref int? minqi, ref int? maxqi) {
    //  //    minqi = -1;
    //  //    maxqi = -1;
    //  //  }
    //  //}

    //  internal clsUndoRedo(clsNoteMapCF notemapcf) {
    //    UndoStack = new Stack<clsQI>();
    //    RedoStack = new Stack<clsQI>();
    //    NoteMapCF = notemapcf;
    //  }

    //  //* first ... last (LIFO)
    //  //* delimiter: qtime = -1
    //  //* undo stack format
    //  //*   data, ..., data, delimiter, data, ..., data, delimiter
    //  //* redo stack format
    //  //*   delimiter, data, ..., data, delimiter, data, ..., data
    //  private Stack<clsQI> UndoStack;
    //  private Stack<clsQI> RedoStack;
    //  private clsNoteMapCF NoteMapCF;
    //  internal bool Active = false;  //switch on after initial load

    //  internal void PushDelimiter() {
    //    if (Active) UndoStack.Push(null);
    //    SetCmdStateUndoRedo();
    //  }

    //  internal void PushQI(int qtime) {
    //    clsNoteMapCF ncf = P.F.CF.NoteMap;
    //    if (Active) UndoStack.Push(new clsQI(qtime, ncf.NewMap[qtime], ncf.ChordAtt[qtime]));
    //    SetCmdStateUndoRedo();
    //  }

    //  //internal void PushTSigs() {
    //  //  UndoStack.Push(new clsTSigs(P.F.MTime));
    //  //  SetCmdState();
    //  //}

    //  internal void Undo() {
    //    //int? minqi = null;
    //    //int? maxqi = null;
    //    if (!Active) return;
    //    if (UndoStack.Count == 0) return;
    //    clsQI x;
    //    x = UndoStack.Pop();
    //    if (x != null) {
    //      LogicError.Throw(eLogicError.X061);
    //      return;
    //    }
    //    RedoStack.Push(x);  //delimiter
    //    while (UndoStack.Count > 0) {
    //      x = UndoStack.Peek();
    //      if (x == null) break;  //delimiter
    //      x = UndoStack.Pop();
    //      //x.GetQIMinMaxUndo(ref minqi, ref maxqi);
    //      x.StackPush(RedoStack);
    //      //RedoStack.Push(new clsNote(x.QTime, x.Note, NoteMapCF.Map[x.QTime, x.Note]));
    //      //NoteMapCF.Map[x.QTime, x.Note] = x.Val;
    //      x.Update();
    //    }
    //    FinalizeUndoRedo();
    //    SetCmdStateUndoRedo();
    //  }

    //  private static void FinalizeUndoRedo() {
    //    P.F.frmChordMap.SetNoteMapFileChanged(true, false);
    //  }

    //  internal void Redo() {
    //    //int? minqi = null;
    //    //int? maxqi = null;
    //    if (!Active) return;
    //    while (RedoStack.Count > 0) {
    //      clsQI x = RedoStack.Pop();
    //      if (x == null) UndoStack.Push(x);
    //      else {
    //        x.StackPush(UndoStack);
    //        //x.GetQIMinMaxRedo(ref minqi, ref maxqi);
    //      }
    //      if (x == null) break;  //delimiter
    //      x.Update();
    //    }
    //    FinalizeUndoRedo();
    //    SetCmdStateUndoRedo();
    //  }

    //  internal void SetCmdStateUndoRedo() {
    //    if (P.F.frmChordMap != null) {
    //      P.F.frmChordMap.cmdUndo.Enabled = (Active && UndoStack.Count > 0);
    //      P.F.frmChordMap.cmdRedo.Enabled = (Active && RedoStack.Count > 0);
    //    }
    //  }
    //}
  }
}


