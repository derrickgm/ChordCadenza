#undef MemoryInfo

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Numerics;

namespace ChordCadenza {
  internal class clsNoteMapMidi : clsNoteMap {
    internal class clsW: IQNEle {
      internal clsW(clsNoteMapMidi notemapmidi) {
        NoteMapMidi = notemapmidi;
      }

      public void NullifyChordinateStatus(int qi) { }
      private clsNoteMapMidi NoteMapMidi;

      public bool this[int q, int n] {
        get { return NoteMapMidi.IsF(q, n); }
        set { NoteMapMidi.SetF(q, n, value); }
      }

      public bool[] this[int q] {
        get {
          bool[] ret = new bool[12];  
          for (int n = 0; n < 12; n++) ret[n] = NoteMapMidi.IsF(q, n); 
          return ret; 
        }
      }
    }

    internal clsNoteMapMidi(clsOnOff onoff, clsTrks.Array<int> trkminpitch, clsTrks.Array<int> trkmaxpitch, clsFileStream filestream) {
      //NumTrks = numtrks;
      TrkMinPitch = trkminpitch;
      TrkMaxPitch = trkmaxpitch;
      int len = P.F.MaxBBT.MaxNoteMapQI;
      Filter = new byte[len, 12];
#if MemoryInfo
      long before = GC.GetTotalMemory(true);
#endif
      PropagatePW(filestream, len, null);
      _Map = new clsTrks.Array<ushort[]>(delegate () { return new ushort[len]; });
      //NewMap = clsTrks.Array<ushort[]>.NewClass(delegate () { return new ushort[onoff.Length]; });
      InitMap(onoff, null, filestream.ExclPB);
      _FullMap = new clsTrks.Array<BigInteger[]>(delegate () { return new BigInteger[len]; });   //BigInteger 128 bits
      //NewFullMap = clsTrks.Array<BigInteger[]>.NewClass(delegate () { return new BigInteger[onoff.Length]; });//BigInteger 128 bits
      InitFullMap(onoff, null, filestream.ExclPB);
#if MemoryInfo
      long after = GC.GetTotalMemory(true);
      long diff = after - before;
      TotalMem += diff;
      Debug.WriteLine("clsNoteMapMidi new diff = " + diff + " total diff = " + TotalMem + " bytes");
#endif
      InitFilter(0, Filter.GetLength(0) - 1);
      EleW = new clsW(this);
      CreateOnOffPairs();
    }

//    internal void UpdateTrk(clsOnOff onoff, clsTrks.T trk, int trkminpitch, int trkmaxpitch, clsFileStream filestream) {
//      //* onoff should contain updated track
//      //* maxqtime(onoff.Length) should be constant
//      //* if this doesn't work, use constructor instead
//      TrkMinPitch[trk] = trkminpitch;
//      TrkMaxPitch[trk] = trkmaxpitch;
//#if DEBUG
//      Stopwatch stopwatch = new Stopwatch();
//      stopwatch.Start();
//#endif
//      PropagatePWTrk(trk, filestream, onoff.Length); StrmMsg(stopwatch, "Elapsed after PW: ");
//      InitMap(onoff, trk, filestream.ExclPB);
//      InitFullMap(onoff, trk, filestream.ExclPB); StrmMsg(stopwatch, "Elapsed after InitMaps: ");
//      InitFilter(0, Filter.GetLength(0) - 1); StrmMsg(stopwatch, "Elapsed after InitFilter: ");
//      EleW = new clsW(this);
//      CreateOnOffPairs(); StrmMsg(stopwatch, "Elapsed after CreateOnOffPairs: ");
//    }

    private void StrmMsg(Stopwatch sw, string msg) {
#if DEBUG
      Debug.WriteLine("Elapsed " + msg + sw.ElapsedMilliseconds);
#endif
    }

    //internal override bool QI_OOR(int qi) {
    //  return (qi >= P.F.MaxBBT.QI || qi >= NewMap[new clsTrks.T(P.F.Trks, 0)].Length);
    //}

    //internal override void Realloc(int qlen) {
    //  int maplen = _Map[new clsTrks.T(P.F.Trks, 0)].Length;
    //  if (qlen == maplen) return;

    //  clsTrks.Array<ushort[]> newmap = new clsTrks.Array<ushort[]>(delegate () { return new ushort[qlen]; });
    //  clsTrks.Array<BigInteger[]> newfullmap = new clsTrks.Array<BigInteger[]>(delegate () { return new BigInteger[qlen]; });
    //  bool[,] trk_selected = new bool[qlen, 128];
    //  clsTrks.Array<short[]> pb = new clsTrks.Array<short[]>(delegate () {
    //    short[] pw = new short[qlen];
    //    for (int i = 0; i < qlen; i++) pw[i] = -1;
    //    return pw;
    //  });
    //  byte[,] filter = new byte[qlen, 12];

    //  for (int q = 0; q < qlen; q++) {
    //    if (q < maplen) {
    //      foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {
    //        newmap[trk][q] = _Map[trk][q];
    //        newfullmap[trk][q] = _FullMap[trk][q];
    //        pb[trk][q] = PB[trk][q];
    //      }
    //      for (int n = 0; n < 128; n++) trk_selected[q, n] = Trk_Selected[q, n];
    //      for (int n = 0; n < 12; n++) filter[q, n] = Filter[q, n];
    //    }
    //  }
    //}

    private void InitFullMap(clsOnOff onoff, clsTrks.T intrk, bool exclpb) {
      foreach (clsTrks.T trk in TrkMinPitch.Next) {  //create this[] from unmodded note (fullmap)
        if (intrk != null && intrk != trk) continue;
        for (int note = TrkMinPitch[trk]; note <= TrkMaxPitch[trk]; note++) {
          int cnt = 0;
          for (int qtime = 0; qtime < onoff.Length; qtime++) {
            cnt += onoff[qtime, note, trk, false];
            //if (cnt > 0) Debugger.Break();  //ON 
            this[qtime, note, trk, false] = (cnt > 0);   
          }
        }
        if (exclpb) {
          for (int qtime = 0; qtime < onoff.Length; qtime++) {
            for (int note = TrkMinPitch[trk]; note <= TrkMaxPitch[trk]; note++) {
              if (this[qtime, note, trk, false] && PB[trk][qtime] != 8192) {
                this[qtime, note, trk, false] = false;
              }
            }
          }
        }
      }
    }

    private void InitMap(clsOnOff onoff, clsTrks.T intrk, bool exclpb) {   //virtual channel (= track)
      foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {  //create this[] from modded note (map)
        if (intrk != null && intrk != trk) continue;
        for (int note = 0; note < 12; note++) {
          int cnt = 0;
          for (int qtime = 0; qtime < onoff.Length; qtime++) {
            cnt += onoff[qtime, note, trk, true];
            //if (cnt > 0) Debugger.Break(); //ON 
            this[qtime, note, trk] = (cnt > 0);  
          }
        }
        if (exclpb) {
          for (int qtime = 0; qtime < onoff.Length; qtime++) {
            for (int note = 0; note < 12; note++) {
              if (this[qtime, note, trk] && PB[trk][qtime] != 8192) {
                this[qtime, note, trk] = false;
              }
            }
          }
        }
      }
    }

    internal void PropagatePW(clsFileStream filestream, int len, clsTrks.T trk) {
      //* null trk = all trks
      //* initialize PB
      if (trk == null) {
        PB = new clsTrks.Array<short[]>(delegate () {
          short[] pw = new short[len];
          for (int i = 0; i < len; i++) pw[i] = -1;
          return pw;
        });
      } else {
        PB[trk] = new short[len];
        for (int i = 0; i < len; i++) PB[trk][i] = -1;
      }

      //* read stream
      clsFileStream.clsEvStrm[] strm = filestream.Strm;
      for (int i = 0; i < strm.Length; i++) {
        if (strm[i] is clsFileStream.clsEvShort) {
          clsFileStream.clsEvShort ev = ((clsFileStream.clsEvShort)strm[i]);
          if (trk != null && trk != ev.Trk) continue;
          if ((ev.Status & 0xe0) != 0xe0) continue;  //not pitchbend
          int val = ev.Msg + (ev.Data << 7);
          PB[ev.Trk][ev.QTime] = (short)val;  //channel not used - only track
        }
      }

      //* propagate PB
      foreach (clsTrks.T t in PB.Next) {
        if (trk != null && trk != t) continue;
        short pw = 8192;  //centre (default)
        for (int qi = 0; qi < len; qi++) {
          if (PB[t][qi] >= 0) pw = PB[t][qi];  //set current value 
          PB[t][qi] = pw;
        }
      }
    }

    internal void InitPWTrk(clsTrks.T trk) {
      for (int qi = 0; qi < PB[trk].Length; qi++) {
        PB[trk][qi] = 8192;
      }
    }

    //private void PropagatePWTrk(clsTrks.T trk, clsFileStream filestream, int onofflen) {
    //  //* initialize PB
    //  for (int qi = 0; qi < onofflen; qi++) {
    //    PB[trk][qi] = -1;
    //  }

    //  //* read stream
    //  clsFileStream.clsEvStrm[] strm = filestream.Strm;
    //  for (int i = 0; i < strm.Length; i++) {
    //    if (strm[i] is clsFileStream.clsEvShort) {
    //      clsFileStream.clsEvShort ev = ((clsFileStream.clsEvShort)strm[i]);
    //      if (ev.Trk != trk) continue;
    //      if ((ev.Status & 0xe0) != 0xe0) continue;  //not pitchbend
    //      int val = ev.Msg + (ev.Data << 7);
    //      PB[ev.Trk][ev.QTime] = (short)val;  //channel not used - only track
    //    }
    //  }

    //  //* propagate PB
    //  short pw = 8192;  //centre (default)
    //  for (int qi = 0; qi < onofflen; qi++) {
    //    if (PB[trk][qi] >= 0) pw = PB[trk][qi];  //set current value 
    //    PB[trk][qi] = pw;
    //  }
    //}

#if MemoryInfo
    private static long TotalMem = 0;
#endif
    //internal enum ePB { Incl, Excl };
    private clsTrks.Array<ushort[]> _Map;  //[qtime, trk] -> [trk][qtime]
    private clsTrks.Array<BigInteger[]> _FullMap;  //[qtime, trk] -> [trk][qtime]
    internal clsTrks.Array<short[]> PB;  //pitchbend [qime, trk] -> [trk][qtime]
    internal bool[,] Delete;  //only used as required  [qtime, note]
    //protected clsMap _Map;  //[qtime, note(0-11)]
    //private clsMap FullMap;  //[qtime, note(0-127)]
    internal clsW EleW;
    private byte[,] Filter;  //[qtime, note]
    //private int[,] TrksPerQI;  //[qtime, note].//...
    internal clsTrks.Array<int> TrkMinPitch;
    internal clsTrks.Array<int> TrkMaxPitch;
    //internal int NumTrks;
    //internal int FilterLen {
    //  get { return Filter.GetLength(0); }
    //}
    internal clsOnOffPairs[] OnOffPairs = new clsOnOffPairs[12];  //[note]

    //internal struct sOnOffPair {
    //  internal int OnQTime;
    //  internal int OffQTime;
    //  internal sOnOffPair(int onqtime) {
    //    OnQTime = onqtime;
    //    OffQTime = -1;
    //  }
    //}

    ////* not yet used or tested
    //internal List<sOnOffPair>[] OnOffPairs = new List<sOnOffPair>[12];    //[note][qi]
    ////* not yet used or tested

    internal class clsOnOffPairs {
      private List<int> OnList = new List<int>(500);
      private List<int> OffList = new List<int>(500);

      internal int IndexOnQ(int q) {
        //* return index of ON event at or before q
        //* return < 0 if q is before first event
        int index = OnList.BinarySearch(q);
        if (index < 0) index = ~index - 1;
        return index;
      }

      internal int IndexOffQ(int q) {
        //* return index of OFF event at or after q
        //* return < 0 if q is after last event
        int index = OffList.BinarySearch(q);

        if (index < 0) index = ~index;
        if (index == OffList.Count) index = -1; 
        return index;
      }

      internal void Add(int onq, int offq) {
        OnList.Add(onq);
        OffList.Add(offq);
      }

      internal int On(int index) {
        return OnList[index];
      }

      internal int Off(int index) {
        return OffList[index];
      }
    }

    internal int NoteCount(int qtime, clsTrks.T trk) {
      int cnt = 0;
      for (int note = 0; note < 12; note++) {
        if (this[qtime, note, trk]) cnt++;
      }
      return cnt;
    }

    internal bool this[int qtime, int note, clsTrks.T trk] {  //modded note for one trk
      get {
        return (_Map[trk][qtime] & NoteMask12[note]) > 0;
      }
      set {
        SetMap12(_Map, qtime, note, trk, value);
      }
    }

    internal bool this[int qtime, int note, clsTrks.T trk, bool mod] {
      //* note for one trk, mod/nomod
      get {
        if (mod) return this[qtime, note, trk];
        else return (_FullMap[trk][qtime] & NoteMask128[note]) > 0;
      }
      set {
        if (mod) this[qtime, note, trk] = value;
        else SetMap128(_FullMap, qtime, note, trk, value);
      }
    }

    //internal bool GetMapPB(int qtime, int note, int trk, bool mod) {
    //  //* note for one trk, excl. pitchbent notes
    //  if (PB[qtime, trk] != 8192) return false;  //pitchbent 
    //  return this[qtime, note, trk, mod];
    //}

    internal ushort[] Map_GetTrk(clsTrks.T trk) {  //called by frmTrackMap Undo/Redo
      return _Map[trk];
    }

    internal void Map_SetTrk(clsTrks.T trk, ushort[] val) {  //called by frmTrackMap Undo/Redo
      _Map[trk] = val;
    }

    internal BigInteger[] FullMap_GetTrk(clsTrks.T trk) {  //called by frmTrackMap Undo/Redo
      return _FullMap[trk];
    }

    internal void FullMap_SetTrk(clsTrks.T trk, BigInteger[] val) {  //called by frmTrackMap Undo/Redo
      _FullMap[trk] = val;
    }

    internal short GetPB(clsTrks.T trk, int qi) {
      return PB[trk][qi];
    }

    //internal void SetPB(clsTrks.T trk, int qi, short val) {
    //  PB[trk][qi] = val;
    //}

    internal void SyncPCFromPitches(int qtime, int pc, clsTrks.T trk) {
      //* set Map12 from Map128 pitch values
      //* use to synchronise Map12 from Map128 
      for (int n = pc; n < 128; n += 12) {
        if (this[qtime, n, trk, false]) {
          this[qtime, pc, trk, true] = true;
          return;
        }
      }
      this[qtime, pc, trk, true] = false;
    }

    internal bool FullMapEquals(int q0, int q1) {
      foreach (clsTrks.T trk in _FullMap.Next) {
        if (_FullMap[trk][q0] != _FullMap[trk][q1]) return false;
      }
      return true;
    }

    private void SetMap12(clsTrks.Array<ushort[]> map, int qtime, int note, clsTrks.T trk, bool value) {
      if (value) map[trk][qtime] |= NoteMask12[note];
      else map[trk][qtime] &= (ushort)~NoteMask12[note];
    }

    private void SetMap128(clsTrks.Array<BigInteger[]> map, int qtime, int note, clsTrks.T trk, bool value) {
      if (value) map[trk][qtime] |= NoteMask128[note];
      else map[trk][qtime] &= ~NoteMask128[note];
    }

    public override bool this[int qtime, int note] {  //all trks (modded)
      get {
        foreach (ushort[] x in _Map) {
          if ((x[qtime] & NoteMask12[note]) > 0) return true;
        }
        return false;
      }
      set {
        LogicError.Throw(eLogicError.X062);
      }
    }

    //internal bool GetMapPB(int qtime, int note) {  //all trks (modded)
    //  for (int trk = 0; trk < NumTrks; trk++) {
    //    if (GetMapPB(qtime, note, trk, true)) return true;
    //  }
    //  return false;
    //}

    public bool this[int qtime, int note, bool mod] {  //all trks
      get {
        if (mod) return this[qtime, note];
        foreach (BigInteger[] x in _FullMap) {
          if ((x[qtime] & NoteMask128[note]) > 0) return true;
        }
        return false;
      }
      set {
        LogicError.Throw(eLogicError.X063);
      }
    }

    //internal bool GetMapPB(int qtime, int note, bool mod) {  //all trks
    //  //*( return true if any trk has a note with no pitchbend
    //  for (int trk = 0; trk < NumTrks; trk++) {
    //    if (GetMapPB(qtime, note, trk, mod)) return true;
    //  }
    //  return false;
    //}

    //public override bool this[int qtime, int note] {  //all trks
    //  get {
    //    return (_Map[qtime, note] != 0);
    //  }
    //  set {
    //    throw new LogicException();
    //  }
    //}

    public override bool[] this[int qtime] {  //return bool[12] array for all trks
      get {
        ushort ushrt = 0;
        foreach (ushort[] s in _Map) {
          ushrt |= s[qtime];
        }
        return UShortToBoolArray(ushrt);
      }
      set {
        LogicError.Throw(eLogicError.X064);
      }
    }

    public bool[] this[int qtime, bool mod, bool noch9] {  //return bool[12 or 128] array for all trks
      get {
        if (mod) return this[qtime];
        BigInteger big = 0;
        foreach (clsTrks.T trk in P.F.Chan.Next) {
          if (noch9 && P.F.Chan[trk] == 9) continue;
          big |= _FullMap[trk][qtime];
        }
        return BigIntegerToBoolArray(big, 128);
      }
      set {
        LogicError.Throw(eLogicError.X065);
      }
    }

    public bool[] this[int qtime, bool mod, bool indtrk, bool noch9] {  
      //* return bool[12 or 128 or 12*numtrks or 128*numtrks] array
      //* indtrk=true: check each track individually
      get {
        if (!indtrk) return this[qtime, mod, noch9];
        if (mod) {  //mod and trk
          BigInteger big = 0;
          int size = 0;
          foreach (clsTrks.T trk in P.F.Chan.Next) {
            if (noch9 && P.F.Chan[trk] == 9) continue;
            if (trk.TrkNum > 0) big <<= 12;
            big |= _Map[trk][qtime];
            size += 12;
          }
          return BigIntegerToBoolArray(big, size);
        } else {  //unmodded and trk
          BigInteger big = 0;
          int size = 0;
          foreach (clsTrks.T trk in P.F.Chan.Next) {
            if (noch9 && P.F.Chan[trk] == 9) continue;
            if (trk.TrkNum > 0) big <<= 128; 
            big |= _FullMap[trk][qtime];
            size += 128;
          }
          return BigIntegerToBoolArray(big, size);
        }
      }
      set {
        LogicError.Throw(eLogicError.X066);
      }
    }

    //public override bool[] this[int qtime] {  //return array of modded notes 
    //  get {
    //    bool[] ret = new bool[12];
    //    for (int n = 0; n < 12; n++) ret[n] = (Map[qtime, n] != 0);  //any trk (trk0)
    //    return ret;
    //  }
    //  set {
    //    throw new LogicException();
    //  }
    //}

    internal bool Filled(clsTrks.T trk, int qtime) {
      //* return true if at least one pitch is ON 
      return _Map[trk][qtime] > 0;
      }

    internal void InitFilter(int qilo, int qihi) {
      for (int q = qilo; q <= qihi; q++) {
        for (int n = 0; n < Filter.GetLength(1); n++) SetF(q, n, this[q, n]);
      }
      //CalcWeights(0, 0, 0, Filter.GetLength(0) - 1);
    }

    //internal override int GetLengthQTime() {
    //  return NewMap[new clsTrks.T(P.F.Trks, 0)].Length;
    //}

    internal bool TrkEmpty(int qtime, clsTrks.T trk) {
      for (int n = 0; n < 12; n++) {
        if (this[qtime, n, trk]) return false;
      }
      return true;
    }

    internal override bool IsF(int qi, int note) {
      return Filter[qi, note] != 0;
    }

    internal override bool[] IsF(int qi) {
      bool[] ret = new bool[12];
      for (int n = 0; n < 12; n++) ret[n] = IsF(qi, n);
      return ret;
    }

    internal void SetF(int qi, int note, bool on) {
      Filter[qi, note] = (on) ? (byte)1 : (byte)0;
    }

    internal void ApplyTrim(eAlign aligntrim, int qilo, int qihi,
      int qiquant, int qiinner, int qiouter) {
      //* trim syncopated notes
      //* uses OnOffPair - apply before other filters
      bool pretrim = P.frmCfgChords.chkTrimPre.Checked;
      bool posttrim = P.frmCfgChords.chkTrimPost.Checked;
      if (qiinner == 0 || qiouter == 0) return;
      if (!pretrim && !posttrim) return;
      int ticksperqi = P.F.TicksPerQI;

      if (OnOffPairs == null) CreateOnOffPairs();
      for (int n = 0; n < 12; n++) {
        clsOnOffPairs pairs = OnOffPairs[n];

        if (pretrim) {
          clsMTime.clsSegment startseg = GetStartSeg(aligntrim, qilo, qihi, qiquant, ticksperqi);
          for (clsMTime.clsSegment segment = startseg; !segment.OOR; segment++) {
            if (qiinner > segment.SegQIWidth / 2) continue;  //'before' too large
            if (qiouter < segment.SegQIWidth / 8) continue;  //'after' too small

            //* check if trim required
            int indexoffq = pairs.IndexOffQ(segment.SegQILo); //first ON at or after SeqQILo
            if (indexoffq < 0) continue;  //after last event
            int offq = pairs.Off(indexoffq);
            if (offq > segment.SegQILo + qiinner) continue;
            int onq = pairs.On(indexoffq);
            if (onq > segment.SegQILo - qiouter) continue;
            //* trim
            //for (int q = offq; q >= segment.SegQILo; q--) SetF(q, n, false);
            for (int q = segment.SegQILo; q < segment.SegQILo + qiinner; q++) SetF(q, n, false);
          }
        }

        if (posttrim) {
          clsMTime.clsSegment startseg = GetStartSeg(aligntrim, qilo, qihi, qiquant, ticksperqi);
          for (clsMTime.clsSegment segment = startseg; !segment.OOR; segment++) {
            if (qiinner > segment.SegQIWidth / 2) continue;  //'before' too large
            if (qiouter < segment.SegQIWidth / 8) continue;  //'after' too small

            //* check if trim required
            int indexonq = pairs.IndexOnQ(segment.SegQIHi); //first ON at or before SeqQIHi
            if (indexonq < 0) continue;  //before first event
            int onq = pairs.On(indexonq);
            if (onq < segment.SegQIHi + 1 - qiinner) continue;
            int offq = pairs.Off(indexonq);
            if (offq < segment.SegQIHi + 1 + qiouter) continue;
            //* trim
            //for (int q = onq; q <= segment.SegQIHi; q++) SetF(q, n, false)
            for (int q = segment.SegQIHi; q > segment.SegQIHi - qiouter; q--) SetF(q, n, false); 
          }
        }
      }
    }

    internal void ApplyMinLenFilter(int qicnt, int qilo, int qihi) {
      if (qicnt == 0) return;
      for (int n = 0; n < 12; n++) {
        int oncnt = 0;
        for (int qi = qilo; qi <= qihi; qi++) {
          if (IsF(qi, n)) oncnt++;  //ON ev
          else {  //OFF ev
            if (oncnt > 0 && oncnt <= qicnt) {  //expand note to min length
              for (; qi < qihi; qi++) {
                if (IsF(qi, n) || oncnt > qicnt) {
                  qi--;
                  break;
                }
                SetF(qi, n, true);
                oncnt++;
              }
            }
            oncnt = 0;
          }
        }
      }
    }

    internal void ApplyGapFilter(int qicnt, int qilo, int qihi) {
      if (qicnt == 0) return;
      for (int n = 0; n < 12; n++) {
        int offcnt = 0;
        for (int qi = qilo; qi <= qihi; qi++) {
          if (!IsF(qi, n)) offcnt++;  //OFF ev
          else {  //ON ev
            if (offcnt > 0 && offcnt <= qicnt) {  //close the gap
              for (int qii = qi - 1; qii >= qilo; qii--) {
                if (IsF(qii, n)) break;
                SetF(qii, n, true);
              }
            }
            offcnt = 0;
          }
        }
      }
    }

    internal void ApplyNoteFilter(int qicnt, int qilo, int qihi) {  //remove short notes
      if (qicnt == 0) return;
      for (int n = 0; n < 12; n++) {
        int oncnt = 0;
        for (int qi = qilo; qi <= qihi; qi++) {
          if (IsF(qi, n)) oncnt++;  //ON ev
          else {  //OFF ev
            if (oncnt > 0 && oncnt <= qicnt) {  //remove pitchclass
              for (int qii = qi - 1; qii >= qilo; qii--) {
                if (!IsF(qii, n)) break;
                SetF(qii, n, false);
              }
            }
            oncnt = 0;
          }
        }
      }
    }

    internal void FillSegments(eAlign align, int qilo, int qihi, int fillpercent, int qiwidth) {
      //* fill or empty segments (bars/halfbars/beats) by modded note
      if (align == eAlign.Interval && qiwidth == 0) return;
      int ticksperqi = P.F.TicksPerQI;

      for (int n = 0; n < 12; n++) {
        clsMTime.clsSegment startseg = GetStartSeg(align, qilo, qihi, qiwidth, ticksperqi);

        //* quantize
        for (clsMTime.clsSegment segment = startseg; !segment.OOR; segment++) {
          int totqiall = segment.SegQIWidth;
          int totqion = 0;
          int segqilo = segment.SegQILo;
          int segqihi = segment.SegQIHi;
          for (int qi = segqilo; qi <= segqihi; qi++) if (IsF(qi, n)) totqion++;
          int percent = (totqion * 100) / totqiall;
          bool fill = false;
          if (totqion > 0 && percent >= fillpercent) fill = true;  //fill segment (else clear)
          for (int qi = segqilo; qi <= segqihi; qi++) SetF(qi, n, fill);  //fill or clear segment
        }
      }
    }

    internal int[] GetFillPercent(clsFileStream filestream, clsMTime.clsSegment segment, bool indbass) {
      //* return occupancy as a percent for each modded note in segment
      int offsetqi = 0;
      bool offsetpre = false;
      int swing = (int)P.frmCfgChords.trkSwing.Value - 50;
      if (segment is clsMTime.clsSegHalfBar && swing != 0) {
        offsetqi = swing * segment.SegQIWidth / 50;
        if (segment.SegBBTLo.BeatsRemBar > 0) offsetpre = true; 
      }
      int[] ret = new int[12];
      int maxpercent = 0;
      for (int n = 0; n < 12; n++) {
        int totqiall = 0;
        int totqion = 0;
        int segqilo = segment.SegQILo;
        int segqihi = segment.SegQIHi;
        if (offsetpre) segqilo += offsetqi; else segqihi += offsetqi;  
        if (indbass || P.frmCfgChords.chkWeightedScores.Checked) {
          foreach (clsTrks.T trk in P.F.Chan.Next) {
            bool select = (indbass) ? P.F.FSTrackMap.TrkType[trk] == clsFileStream.eTrkType.Bass : filestream.TrkSelect[trk];
            if (select && P.F.Chan[trk] != 9) {  //not percussion
              totqiall += segment.SegQIWidth;  //qi * num selectedtrks
              for (int qi = segqilo; qi <= segqihi; qi++) {
                //* may be trimmed - OK
                //* may be quantized - not recommended, but should still work
                if (IsF(qi, n) && this[qi, n, trk]) totqion++;
              }
              //if (indbass) trk = trk + 1 - 1;
            }
          }
        } else {  //unweighted
          totqiall = segment.SegQIWidth;
          for (int qi = segqilo; qi <= segqihi; qi++) if (IsF(qi, n)) totqion++;
        }
        int percent = (totqiall == 0) ? 0 : (totqion * 100) / totqiall;
        maxpercent = Math.Max(maxpercent, percent);
        ret[n] = percent;
      }

      if (!indbass && maxpercent > 0) {
        for (int i = 0; i < 12; i++) {
          if (ret[i] > 0) ret[i] = (ret[i] * 100) / maxpercent;
        }
      }
      return ret;
    }

    internal static clsMTime.clsSegment GetStartSeg(eAlign align, int qilo, int qihi, int qiwidth, int ticksperqi) {
      clsMTime.clsSegment startseg;

      switch (align) {
        case eAlign.Bar:
          startseg = new clsMTime.clsSegBar(ticksperqi, qilo, qihi);
          break;
        case eAlign.Beat:
          startseg = new clsMTime.clsSegBeat(ticksperqi, qilo, qihi);
          break;
        case eAlign.HalfBar:
          startseg = new clsMTime.clsSegHalfBar(ticksperqi, qilo, qihi);
          break;
        case eAlign.Interval:
          startseg = new clsMTime.clsSegInterval(ticksperqi, qilo, qihi, qiwidth);
          break;
        default:
          LogicError.Throw(eLogicError.X067);
          startseg = new clsMTime.clsSegBeat(ticksperqi, qilo, qihi);
          break;
      }
      return startseg;
    }

    internal void CreateOnOffPairs() {
      //* create onoff pairs for all tracks aggregated from pitch classes
      bool thismapq, prevmapq;
      int onq = -1;
      for (int n = 0; n < 12; n++) {  //modded note
        prevmapq = false;
        OnOffPairs[n] = new clsOnOffPairs();
        //for (int q = 0; q < this.GetLengthQTime(); q++) {
        for (int q = 0; q < P.F.MaxBBT.QI; q++) {
          //thismapq = this[q, n];
          thismapq = this[q, n];  
          if (thismapq && !prevmapq) {   //ON
            onq = q;
          } else if (!thismapq && prevmapq) {  //OFF
            if (onq < 0) {
              LogicError.Throw(eLogicError.X078);
              continue;
            }
            OnOffPairs[n].Add(onq, q);
            onq = -1;  
          }
          prevmapq = thismapq;
        }
      }
    }

    internal override void CalcKeys_CalcSegQI(int[] seg, int qi, ref bool indempty) {
      for (int note = 0; note < 12; note++) {
        foreach (clsTrks.T trk in P.F.Chan.Next) {
          int chan = P.F.Chan[trk];
          if (chan == 9) continue;  //not percussion
          if (this[qi, note, trk]) {
            seg[note]++;
            indempty = false;
          }
        }
      }
    }
  }
}