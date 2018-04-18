using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChordCadenza {
  internal class clsMTime {
    internal int Gen = 0;
    private delegate int delegTSig(int index);
    internal int TicksPerQNote;  //from midi file header
    internal clsTSigBB[] TSigs = new clsTSigBB[0];
    internal readonly int MidiClocksPerQNote = 24;

    internal clsMTime(int ticksperqnote, int nn, int dd) {
      TicksPerQNote = ticksperqnote;
      AddTSigFirst(nn, dd);  //default
      P.F.GetTicksPerQI(this);
    }

    internal clsMTime(clsMTime mtime) {  //copy
      TicksPerQNote = mtime.TicksPerQNote;
      TSigs = mtime.TSigs.ToArray();  //copy
    }

    internal bool IsEquiv(clsMTime mtime) {
      if (TicksPerQNote != mtime.TicksPerQNote) return false;;
      if (!TSigs.SequenceEqual(mtime.TSigs)) return false;
      return true;
    }

    //private clsTSig FindTSig(delegTSig dtsig, int arg, out int index) {
    //  for (index = 0; index < TSigs.Length; index++) {
    //    if (dtsig(index) > arg) break;
    //  }
    //  if (index < 1) index = 1;  //throw new LogicException();  //trying to get BBT before first tsig
    //  return TSigs[--index];  //current tsig (may be at start)
    //}

    internal clsTSigBB FindTSigTick(int ticks, out int index) {
      for (index = 0; index < TSigs.Length; index++) {
        if (TSigs[index].Tick > ticks) break;
      }
      if (index < 1) index = 1;  //throw new LogicException();  //trying to get BBT before first tsig
      return TSigs[--index];  //current tsig (may be at start)
    }

    internal clsTSigBB FindTSigBeat(int beats, out int index) {
      for (index = 0; index < TSigs.Length; index++) {
        if (TSigs[index].Beat > beats) break;
      }
      if (index < 1) index = 1;  //throw new LogicException();  //trying to get BBT before first tsig
      return TSigs[--index];  //current tsig (may be at start)
    }

    internal clsTSigBB FindTSigBar(int bars, out int index) {
      for (index = 0; index < TSigs.Length; index++) {
        if (TSigs[index].Bar > bars) break;
      }
      if (index < 1) index = 1;  //throw new LogicException();  //trying to get BBT before first tsig
      return TSigs[--index];  //current tsig (may be at start)
    }

    internal int DD2Ticks(int nn, int dd) {  //convert nn/dd notes to ticks
      if (dd == 0) return 0;
      return (TicksPerQNote * 4 * nn) / dd;
    }

    internal int DD2DI(int nn, int dd) {  //convert nn/dd notes to qi
      return DD2Ticks(nn, dd) / P.F.TicksPerQI;
    }

    internal int BeatsToTicks(int beats) {
      clsBBT bbt = new clsBBT(beats, true);
      return bbt.Ticks;
    }

    internal int TicksToBeats(int ticks) {
      clsBBT bbt = new clsBBT(ticks);
      return bbt.Beats;
    }

    internal clsTSigBB GetTSig(int ticks) {  
      int i;
      //return FindTSig(delegate(int index) { return TSigs[index].Tick; }, ticks, out i);
      return FindTSigTick(ticks, out i);
    }

    internal void AddTSig(int nn, int dd, int ticks, bool adj) {
      //* add tsig to end of TSigs[] without allowing a filler 
      clsTSigBB tsigprev = null;
      if (TSigs != null && TSigs.Length > 0) tsigprev = TSigs[TSigs.Length - 1];
      clsTSigBB tsig;
      try {
        tsig = new clsTSigBB(this, nn, dd, ticks, tsigprev, adj);
        if (tsig.Tick == 0) TSigs = null;  //must be first (after adj adjustment)
      }
      catch (TSigException) {
        return;
      }
      AppendTSig(tsig);
    }

    internal void AddTSigFirst(int nn, int dd) {
      //* add first tsig to TSigs[]
      TSigs = null; 
      clsTSigBB tsig = new clsTSigBB(this, nn, dd, 0, null, adj: false);
      AppendTSig(tsig);
    }

    //internal void RemoveTSig(clsBBT bbtlo, clsBBT bbthi) {
    //  //* Validate
    //  if (bbtlo.Bar == bbthi.Bar) return;
    //  if (bbtlo.TicksRemBar != 0) throw new TSigException();
    //  if (bbthi != null && bbthi.TicksRemBar != 0) throw new TSigException();

    //  //* create tsig list up to selected area
    //  List<clsTSigBB> list = new List<clsTSigBB>();
    //  clsTSigBB tsigprev = null;
    //  int index = 0;
    //  for (; index < TSigs.Length; index++) {
    //    clsTSigBB tsig = TSigs[index];
    //    if (tsig.Tick >= bbtlo.Ticks) break;
    //    list.Add(tsig);
    //    tsigprev = tsig;
    //  }

    //  //* add tsigs after selected area
    //  int ticksdiff = bbthi.Ticks - bbtlo.Ticks;
    //  for (; index < TSigs.Length; index++) {
    //    clsTSigBB tsig = TSigs[index];
    //    int ticksnext = (index < TSigs.Length - 1) ? TSigs[index + 1].Tick : P.F.MaxBBT.Ticks;
    //    if (ticksnext <= bbthi.Ticks) continue;
    //    int ticksnew = Math.Max(bbtlo.Ticks, tsig.Tick - ticksdiff);
    //    tsig = new clsTSigBB(this, tsig, tsigprev, ticksnew);
    //    list.Add(tsig);
    //    tsigprev = tsig;
    //  }

    //  //* make live
    //  TSigs = list.ToArray();
    //  CheckTSigs();
    //}

    internal void ChangeTSig(int nn, int dd, clsBBT bbtlo, clsBBT bbthi) {
      //* Validate
      if (bbthi == null) bbthi = P.F.MaxBBT.BBTCopy;
      if (bbtlo.Bar == bbthi.Bar) return;
      if (bbtlo.TicksRemBar != 0) throw new TSigException();
      if (bbthi != null && bbthi.TicksRemBar != 0) throw new TSigException();

      //* create tsig list up to bbtlo
      List<clsTSigBB> list = new List<clsTSigBB>();
      clsTSigBB tsigprev = null;
      foreach (clsTSigBB tsig in TSigs) {
        if (tsig.Tick >= bbtlo.Ticks) break;
        list.Add(tsig);
        tsigprev = tsig;
      }

      //* add new tsig
      clsTSigBB tsignew;
      try {
        tsignew = new clsTSigBB(this, nn, dd, bbtlo.Ticks, tsigprev, adj: false);
      }
      catch (TSigException) {
        return;
      }
      list.Add(tsignew);

      //* terminate or continue
      bool truncate = bbthi != null && bbthi.Bar == P.F.MaxBBT.Bar;
      if (bbthi == null || truncate) {  //truncate
        TSigs = list.ToArray();
      } else {  //insert
        //* set up newmtime
        clsMTime newmtime = new clsMTime(TicksPerQNote, TSigs[00].NN, TSigs[0].DD);
        newmtime.TSigs = list.ToArray();  //up to and incl. new tsig
        newmtime.CheckTSigs();  //=>TSigException (should not happen)

        //* filler bar required?
        clsTSig tsigfiller = null;
        clsMTime.clsBBT bbttest = new clsBBT(newmtime, bbthi.Ticks);
        if (bbttest.TicksRemBar > 0) {  //need a filler bar
          tsigfiller = GetFillerTSig(newmtime, bbttest.TicksRemBar);
          if (tsigfiller == null) throw new TSigException();
          if (tsigfiller != null) {
            tsigprev = (list.Count > 0) ? list[list.Count - 1] : null;
            list.Add(new clsTSigBB(tsigfiller, tsigprev, bbthi.Ticks - tsigfiller.TicksPerBar));
            newmtime.TSigs = list.ToArray();
          }
        }

        //* add tsigs after selected area
        int index;
        clsTSigBB tsighi = FindTSigTick(bbthi.Ticks, out index);
        clsTSigBB newtsighi = new clsTSigBB(newmtime, tsighi, newmtime.TSigs[newmtime.TSigs.Length - 1], bbthi.Ticks);
        list.Add(newtsighi);
        for (int i = index + 1; i < TSigs.Length; i++) {
          clsTSigBB tsignewmtime = new clsTSigBB(newmtime, TSigs[i], list[list.Count - 1], TSigs[i].Tick);
          list.Add(tsignewmtime);
        } 

        //* make live
        newmtime.TSigs = list.ToArray();
        TSigs = newmtime.TSigs;
      }
      CheckTSigs();  //=>TSigException (should not happen)
    }

    private void AppendTSig(clsTSigBB tsig) {
      if (TSigs == null) TSigs = new clsTSigBB[0];
      List<clsTSigBB> list = TSigs.ToList();
      list.Add(tsig);
      TSigs = list.ToArray();
    }

    private clsTSig GetFillerTSig(clsMTime mtime, int ticks) {
      int rem;
      int nn = Math.DivRem(ticks * 8, TicksPerQNote, out rem); //num 32nd notes
      if (rem > 0) return null;
      int dd = 32;
      for (; dd >= 2; dd /= 2) {
        //if (nn == 2) break;
        if (nn % 2 != 0) break;  //odd number
        nn /= 2;
      }
      if (nn == 1 && dd == 2) { nn = 2; dd = 4; }  //1/2 -> 2/4
      else if (nn == 1 && dd == 1) { nn = 2; dd = 2; }  //1/1 -> 2/2
      //* may not match next tsig (e.g. 2/2 then 4/4, 3/4 then 6/8)
      return new clsTSig(mtime, nn, dd);
    }

    private void CheckTSigs() {
      //* check indexes and ticks
      if (TSigs[0].Tick != 0) throw new TSigException();
      for (int i = 0; i < TSigs.Length; i++) {
        //if (TSigs[i].Index != i) throw new TSigException();
        if (i > 0 && TSigs[i].Tick <= TSigs[i - 1].Tick) throw new TSigException();
      }

      //* remove duplicates
      List<clsTSigBB> oldlist = TSigs.ToList();
      List<clsTSigBB> newlist = new List<clsTSigBB>();
      for (int i = 0; i < oldlist.Count; i++) {
        if (i > 0 && oldlist[i].NN == oldlist[i - 1].NN && oldlist[i].DD == oldlist[i - 1].DD) continue;
        newlist.Add(oldlist[i]);
        //newlist[newlist.Count - 1].Index = newlist.Count - 1;
      }
      TSigs = newlist.ToArray();
    }

    internal abstract class clsSegment {
      internal bool OOR = false;

      protected int TicksPerQI;
      protected int SegsQILo;  //overall
      protected int SegsQIHi;  //overall
      internal int SegQILo;
      internal int SegQIHi { get { return SegQILo + SegQIWidth - 1; } }
      internal clsBBT SegBBTLo;

      protected clsSegment() { }

      protected void Init(int qilo, int qihi) {
        SegsQILo = RoundUpQI(qilo);
        SegsQIHi = RoundDownQI(qihi) - 1;
        SegQILo = SegsQILo;
        //if (SegQILo < 0) Debugger.Break();  //temp
        SegBBTLo = new clsBBT(SegsQILo * TicksPerQI);
      }

      public static clsSegment operator ++(clsSegment segment) {
        segment.Incr();
        return segment;
      }

      protected void Incr() {
        SegQILo += SegQIWidth;
        SegBBTLo = new clsBBT(SegQILo);
        if (SegQIHi > SegsQIHi) OOR = true; else OOR = false;
      }

      internal abstract int SegQIWidth { get; }
      protected abstract int RoundUpQI(int qi);
      protected abstract int RoundDownQI(int qi);
    }

    internal abstract class clsSegBarBeat : clsSegment {
      internal clsSegBarBeat(int ticksperqi, int qilo, int qihi) : base() {
        //* calculate first segment hi/lo 
        TicksPerQI = ticksperqi;


        Init(qilo, qihi);  //-> SegsQILo, SegsQIHi 
        ////if (qilo != SegsQILo) Debugger.Break();
      }

      protected abstract void NextSeg(clsBBT bbt);
      protected abstract void RoundUp(clsBBT bbt);
      protected abstract void RoundDown(clsBBT bbt);

      internal abstract clsSegBarBeat Copy();

      protected int NextSegQI() {
        int qi = SegQILo;
        clsBBT bbt = (new clsBBT(qi * TicksPerQI));
        NextSeg(bbt);
        if (bbt.Ticks % TicksPerQI != 0) {
          LogicError.Throw(eLogicError.X026);
        }
        return bbt.Ticks / TicksPerQI;
      }

      internal override int SegQIWidth {
        get { return NextSegQI() - SegQILo; }
      }

      protected override int RoundUpQI(int qi) {
        //* round up segslo to start global at start of complete segment
        clsBBT bbt = (new clsBBT(qi * TicksPerQI));
        RoundUp(bbt);
        if (bbt.Ticks % TicksPerQI != 0) {
          LogicError.Throw(eLogicError.X016);
        }
        return bbt.Ticks / TicksPerQI;
      }

      protected override int RoundDownQI(int qi) {
        //* round down segshi to end global with a complete segment
        clsBBT bbt = (new clsBBT(qi * TicksPerQI));
        RoundDown(bbt);
        if (bbt.Ticks % TicksPerQI != 0) {
          LogicError.Throw(eLogicError.X017);
        }
        return bbt.Ticks / TicksPerQI;
      }
    }

    internal class clsSegBar : clsSegBarBeat {
      internal clsSegBar(int ticksperqi, int qilo, int qihi) : base(ticksperqi, qilo, qihi) { }

      protected override void NextSeg(clsBBT bbt) { bbt.NextBar(); }
      protected override void RoundUp(clsBBT bbt) { bbt.RoundUpToBar(); }
      protected override void RoundDown(clsBBT bbt) { bbt.RoundDownToBar(); }

      internal override clsSegBarBeat Copy() {
        return new clsSegBar(TicksPerQI, SegQILo, SegQIHi);
      }
    }

    internal class clsSegBeat : clsSegBarBeat {
      internal clsSegBeat(int ticksperqi, int qilo, int qihi)
        : base(ticksperqi, qilo, qihi) { }

      protected override void NextSeg(clsBBT bbt) { bbt.NextBeat(); }
      protected override void RoundUp(clsBBT bbt) { bbt.RoundUpToBeat(); }
      protected override void RoundDown(clsBBT bbt) { bbt.RoundDownToBeat(); }

      internal override clsSegBarBeat Copy() {
        return new clsSegBeat(TicksPerQI, SegQILo, SegQIHi);
      }
    }

    internal class clsSegHalfBar : clsSegBarBeat {
      internal clsSegHalfBar(int ticksperqi, int qilo, int qihi)
        : base(ticksperqi, qilo, qihi) { }

      protected override void NextSeg(clsBBT bbt) { bbt.NextHalfBar(); }
      protected override void RoundUp(clsBBT bbt) { bbt.RoundUpToHalfBar(); }
      protected override void RoundDown(clsBBT bbt) { bbt.RoundDownToHalfBar(); }

      internal override clsSegBarBeat Copy() {
        return new clsSegHalfBar(TicksPerQI, SegQILo, SegQIHi);
      }
    }

    internal class clsSegInterval : clsSegment {
      internal clsSegInterval(int ticksperqi, int qilo, int qihi, int width) {
        if (width <= 0) throw new FatalException();
        //* calculate first segment hi/lo 
        TicksPerQI = ticksperqi;
        _SegQIWidth = width;

        //SegQILo = qilo;
        //Init(qilo, qihi);
        Init(qilo, qihi);  //-> SegsQILo, SegsQIHi 
        //SegQILo = SegsQILo;  //local first = global first (rounded up)
      }

      private int _SegQIWidth;

      internal override int SegQIWidth {
        get { return _SegQIWidth; }
      }

      protected override int RoundUpQI(int qi) {
        //* round up segslo to start global at start of complete segment
        int count = qi / SegQIWidth;
        int ret = count * SegQIWidth;
        if (qi % SegQIWidth != 0) ret++;
        return ret;
      }

      protected override int RoundDownQI(int qi) {
        //* round down segshi to end global with a complete segment
        int count = qi / SegQIWidth;
        return count * SegQIWidth;
      }
    }

    internal class clsBBT {
      private int Gen = 0;
      internal readonly clsMTime MTime;
      private clsTSigBB _TSig;
      private int _TSigIndex = -1;
      private int _Bar;  //base0
      private int _BeatsRemBar;  //base0 in bar 
      private int _Twelfth;  //base0 in beat - twelfth of a beat
      private int _Ticks;  //base0 - absolute
      private int _TicksRemBar;  //base0
      private int _TicksRemBeat;  //base0
      private int _TicksRemTwelfth;  //base0
      private int _Beats;  //base0: absolute

      private void CheckGen() {
        //if (Gen != MTime.Gen) {
        //  Debug.WriteLine("clsMTime.clsBBT.CheckGen: Gen = " + Gen + " MTime.Gen = " + MTime.Gen);
        //}
        if (Gen != MTime.Gen) NewBBT(Ticks);
      }

      internal clsTSigBB TSig {
        get {
          CheckGen();
          return _TSig;
        }
        set {
          _TSig = value;
        }
      }

      internal int Bar {
        get {
          CheckGen();
          return _Bar;
        }
        private set {
          _Bar = value;
        }
      }

      internal int BeatsRemBar {  //base0 in bar 
        get {
          CheckGen();
          return _BeatsRemBar;
        }
        private set {
          _BeatsRemBar = value;
        }
      }

      internal int Twelfth {  //base0 in beat - twelfth of a beat
        get {
          CheckGen();
          return _Twelfth;
        }
        private set {
          _Twelfth = value;
        }
      }

      internal int Ticks {  //base0 - absolute
        get {
          return _Ticks;
        }
        private set {
          _Ticks = value;
        }
      }

      internal int TicksRemBar {  //base0
        get {
          CheckGen();
          return _TicksRemBar;
        }
        private set {
          _TicksRemBar = value;
        }
      }

      internal int TicksRemBeat {  //base0
        get {
          CheckGen();
          return _TicksRemBeat;
        }
        private set {
          _TicksRemBeat = value;
        }
      }

      internal int TicksRemTwelfth {  //base0
        get {
          CheckGen();
          return _TicksRemTwelfth;
        }
        private set {
          _TicksRemTwelfth = value;
        }
      }

      internal int Beats {  //base0
        get {
          CheckGen();
          return _Beats;
        }
        private set {
          _Beats = value;
        }
      }

      internal clsBBT(int ticks) {
        MTime = P.F.MTime;
        NewBBT(ticks);
      }

      internal clsBBT(clsMTime mtime, int ticks) {
        MTime = mtime;
        NewBBT(ticks);
      }

      internal clsBBT(int beats, bool indbeats) {
        if (!indbeats) {
          LogicError.Throw(eLogicError.X018);
        }
        MTime = P.F.MTime;
        NewBBTBeat(beats);
      }

      internal clsBBT(clsMTime mtime, int beats, bool indbeats) {
        if (!indbeats) {
          LogicError.Throw(eLogicError.X019);
        }
        MTime = mtime;
        NewBBTBeat(beats);
      }

      private void NewBBT(int ticks) {
        Gen = MTime.Gen;
        if (ticks == 0 && MTime.TSigs.Length == 0) {
          TSig = new clsTSigBB(MTime, 4, 4, 0, null, adj: false);
          _TSigIndex = 0;
          Bar = 0;
          TicksRemBar = 0;
          BeatsRemBar = 0;
          TicksRemBeat = 0;
          Twelfth = 0;
          TicksRemTwelfth = 0;
          Beats = 0;
          Ticks = 0;
        } else {
          //TSig = MTime.FindTSig(delegate (int index) { return MTime.TSigs[index].Tick; }, ticks, out _TSigIndex);
          TSig = MTime.FindTSigTick(ticks, out _TSigIndex);
          int delta = ticks - TSig.Tick;
          int ticksrembar;
          Bar = Math.DivRem(delta, TSig.TicksPerBar, out ticksrembar);
          TicksRemBar = ticksrembar;
          Bar += TSig.Bar;
          int ticksrembeat;
          BeatsRemBar = Math.DivRem(TicksRemBar, TSig.TicksPerBeat, out ticksrembeat);
          TicksRemBeat = ticksrembeat;
          int ticksremtwelfth;
          Twelfth = Math.DivRem(TicksRemBeat * 12, TSig.TicksPerBeat, out ticksremtwelfth);
          TicksRemTwelfth = ticksremtwelfth;
          Beats = TSig.Beat + delta / TSig.TicksPerBeat;
          Ticks = ticks;
        }
      }

      internal clsBBT(int bar, int beatrembars, int twelfth) {
        MTime = P.F.MTime;
        NewBBT(bar, beatrembars, twelfth);
      }

      internal clsBBT(clsMTime mtime, int bar, int beatrembars, int twelfth) {
        MTime = mtime;
        NewBBT(bar, beatrembars, twelfth);
      }

      private void NewBBTBeat(int beats) {
        if (beats == 0) {
          NewBBT(0);
          return;
        }
        Gen = MTime.Gen;
        Beats = beats;
        TicksRemBeat = 0;
        TicksRemTwelfth = 0;
        Twelfth = 0;
        //TSig = MTime.FindTSig(delegate (int index) { return MTime.TSigs[index].Beat; }, beats, out _TSigIndex);
        TSig = MTime.FindTSigBeat(beats, out _TSigIndex);
        int delta = beats - TSig.Beat;
        Ticks = TSig.Tick + delta * TSig.TicksPerBeat;
        int beatsrembar;
        Bar = Math.DivRem(delta, TSig.NN, out beatsrembar);
        BeatsRemBar = beatsrembar;
        Bar += TSig.Bar;
        TicksRemBar = BeatsRemBar * TSig.TicksPerBeat;
      }

      private void NewBBT(int bar, int beatsrembar, int twelfth) {
        if (bar == 0 && beatsrembar == 0 && twelfth == 0) {
          NewBBT(0);
          return;
        }
        Gen = MTime.Gen;
        Bar = bar;
        BeatsRemBar = beatsrembar;
        Twelfth = twelfth;
        //TSig = MTime.FindTSig(delegate (int index) { return MTime.TSigs[index].Bar; }, bar, out _TSigIndex);
        TSig = MTime.FindTSigBar(bar, out _TSigIndex);
        int delta = bar - TSig.Bar;
        TicksRemBeat = (twelfth * TSig.TicksPerBeat) / 12;
        TicksRemBar = (beatsrembar * TSig.TicksPerBeat) + TicksRemBeat;
        Ticks = TSig.Tick + delta * TSig.TicksPerBar + TicksRemBar;
        TicksRemTwelfth = 0;
        Beats = TSig.Beat + delta * TSig.NN + beatsrembar;
      }

      internal void AddTicks(int ticks) {  //move forward 
        NewBBT(Ticks + ticks);
      }

      internal clsBBT Copy() {
        return new clsBBT(MTime, Ticks);
      }

      internal int TicksPerBeat {
        get {
          return (MTime.TicksPerQNote * 4) / TSig.DD;
        }
      }

      internal int TicksPerBar {
        get {
          return (MTime.TicksPerQNote * 4 * TSig.NN) / TSig.DD;
        }
      }

      internal void LocateMidiBeat(int midibeats) {
        //* midibeat = 1/16 note - used by SPP (Project Position Pointer)
        int ticks = (midibeats * MTime.TicksPerQNote) / 4;
        NewBBT(ticks);
        //Debug.WriteLine("locate: bars = " + bars + " beats = " + beats);
      }

      internal int GetNearestBar() {  
        int ticksperbar = (MTime.TicksPerQNote * TSig.NN * 4) / TSig.DD;
        if (TicksRemBar > ticksperbar / 2) return Bar + 1;
        return Bar;
      }

      internal void Round(eAlign align) {
        switch (align) {
          case eAlign.Bar:
            RoundToBar();
            return;
          case eAlign.HalfBar:
            RoundToHalfBar();
            return;
          case eAlign.Beat:
            RoundToBeat();
            return;
          default:
            LogicError.Throw(eLogicError.X021);
            RoundToBeat();
            return;
        }
      }

      internal void RoundDown(eAlign align) {
        switch (align) {
          case eAlign.Bar:
            RoundDownToBar();
            return;
          case eAlign.HalfBar:
            RoundDownToHalfBar();
            return;
          case eAlign.Beat:
            RoundDownToBeat();
            return;
          default:
            LogicError.Throw(eLogicError.X022);
            RoundDownToBeat();
            return;
        }
      }

      internal void RoundUp(eAlign align) {
        switch (align) {
          case eAlign.Bar:
            RoundUpToBar();
            return;
          case eAlign.HalfBar:
            RoundUpToHalfBar();
            return;
          case eAlign.Beat:
            RoundUpToBeat();
            return;
          default:
            LogicError.Throw(eLogicError.X023);
            RoundUpToBeat();
            return;
        }
      }

      //*********************************************************************

      internal void RoundToBar() {
        int beatsrembar = BeatsRemBar;
        int bar = Bar;
        int twelfth = Twelfth;
        if (TSig.NN % 2 == 1) {  //odd tsig NN
          if ((beatsrembar > TSig.NN / 2) || (beatsrembar == TSig.NN / 2 && twelfth >= 6)) bar++;
        } else if (beatsrembar >= TSig.NN / 2) bar++;
        NewBBT(bar, 0, 0);
      }

      internal clsBBT GetRoundDownToBar() {  //return newbbt - does not change this
        clsBBT bbt = Copy();
        bbt.RoundDownToBar();
        return bbt;
      }

      internal void RoundDownToBar() {
        int bar = Bar;
        NewBBT(bar, 0, 0);
      }

      internal clsBBT GetRoundUpToBar() {  //return newbbt - does not change this
        clsBBT bbt = Copy();
        bbt.RoundUpToBar();
        return bbt;
      }

      internal void RoundUpToBar() {  //may go OOR
        if (BeatsRemBar > 0 || Twelfth > 0) NewBBT(Bar + 1, 0, 0);
      }

      internal clsBBT GetNextBar() {  //return newbbt - does not change this
        clsBBT bbt = Copy();
        bbt.NextBar();
        return bbt;
      }

      internal void NextBar() {  //may go OOR
        NewBBT(Bar + 1, 0, 0);
      }

      //*********************************************************************

      internal void RoundToBeat() {
        int beats = Beats;
        int twelfth = Twelfth;
        if (twelfth >= 6) beats++;   //round up
        NewBBTBeat(beats);
      }

      internal clsBBT GetRoundDownToBeat() {  //return newbbt - does not change this
        clsBBT bbt = Copy();
        bbt.RoundDownToBeat();
        return bbt;
      }

      internal void RoundDownToBeat() {
        int bar = Bar;
        int beatsrembar = BeatsRemBar;
        NewBBT(bar, beatsrembar, 0);
      }

      internal clsBBT GetRoundUpToBeat() {  //return newbbt - does not change this
        clsBBT bbt = Copy();
        bbt.RoundUpToBeat();
        return bbt;
      }

      internal void RoundUpToBeat() {
        if (TicksRemBeat == 0) return;  //don't round up if on the beat (27/3/16)
        if (BeatsRemBar >= TSig.NN - 1) NewBBT(Bar + 1, 0, 0);
        else NewBBT(Bar, BeatsRemBar + 1, 0);
      }

      internal clsBBT GetRoundUpToBeatAlt() {  //return newbbt - does not change this
        clsBBT bbt = Copy();
        bbt.RoundUpToBeatAlt();
        return bbt;
      }

      internal void RoundUpToBeatAlt() {
        if (TicksRemBeat == 0) return;  //don't round up if on the beat
        if (BeatsRemBar >= TSig.NN - 1) NewBBT(Bar + 1, 0, 0);
        else NewBBT(Bar, BeatsRemBar + 1, 0);
      }

      internal clsBBT GetNextBeat() {  //return newbbt - does not change this
        clsBBT bbt = Copy();
        bbt.NextBeat();
        return bbt;
      }

      internal void NextBeat() {
        CheckGen();
        if (++BeatsRemBar >= TSig.NN) {
          Bar++;
          BeatsRemBar = 0;
        }
        NewBBT(Bar, BeatsRemBar, 0);
      }

      //*********************************************************************

      internal void PrevBar() {
        int bar = (TicksRemBar == 0) ? Bar - 1 : Bar;
        NewBBT(bar, 0, 0);
      }

      internal clsBBT GetPrevBar() {
        int bar = (TicksRemBar == 0) ? Bar - 1 : Bar;
        return new clsMTime.clsBBT(MTime, bar, 0, 0);
      }

      internal void PrevBeat() {
        int beat = (TicksRemBeat == 0) ? Beats - 1 : Beats;
        NewBBTBeat(beat);
      }

      internal clsBBT GetPrevBeat() {
        int beat = (TicksRemBeat == 0) ? Beats - 1 : Beats;
        return new clsMTime.clsBBT(MTime, beat, true);
      }

      //*********************************************************************

      internal void RoundToHalfBar() {
        if (TSig.NN % 2 == 1) {  //odd tsig (eg 3/4)
          //RoundToBar();
          RoundToBeat();
          return;
        }
        int beatsrembar = BeatsRemBar;
        int bar = Bar;
        int twelfthsrembar = (BeatsRemBar * 12) + Twelfth;
        int twelfthsperbar = TSig.NN * 12;
        if (twelfthsrembar < twelfthsperbar / 4) NewBBT(bar, 0, 0);  //round down to bar
        else if (twelfthsrembar < 3 * twelfthsperbar / 4) NewBBT(bar, TSig.NN / 2, 0);  //round to halfbar
        else NewBBT(++bar, 0, 0);  //round up to bar
      }

      internal void RoundDownToHalfBar() {
        if (TSig.NN % 2 == 1) {  //odd tsig (eg 3/4)
          //RoundDownToBar();
          RoundDownToBeat();
          return;
        }
        int bar = Bar;
        int beatsrembar = BeatsRemBar;
        if (BeatsRemBar < TSig.NN / 2) NewBBT(Bar, 0, 0);
        else NewBBT(Bar, BeatsRemBar / 2, 0);
      }

      internal void RoundUpToHalfBar() {
        if (TSig.NN % 2 == 1) {  //odd tsig (eg 3/4)
          //RoundUpToBar();
          RoundUpToBeat();
          return;
        }
        //if (Twelfth == 0 && (TSig.NN == 0 || TSig.NN == 2)) return;
        //NextHalfBar();
        if (TicksRemBar == 0) return;
        clsBBT bbthb = new clsBBT(Bar, TSig.NN / 2, 0);  //on halfbar
        if (TicksRemBar > bbthb.TicksRemBar) NewBBT(Bar + 1, 0, 0);  //in second halfbar
        else if (TicksRemBar < bbthb.TicksRemBar) NewBBT(Bar, TSig.NN / 2, 0);  //in first halfbar  
        //* else on halfbar - no change
      }

      internal void PrevHalfBar() {
        if (TSig.NN % 2 == 1) {  //odd tsig (eg 3/4)
          PrevBeat();
          return;
        }
        int barneg1 = Math.Max(0, Bar - 1);
        if (BeatsRemBar == 0) NewBBT(barneg1, TSig.NN / 2, 0);
        else if (BeatsRemBar <= TSig.NN / 2) NewBBT(Bar, 0, 0);
        else NewBBT(Bar, TSig.NN / 2, 0);
      }

      internal void NextHalfBar() {
        if (TSig.NN % 2 == 1) {  //odd tsig (eg 3/4)
          NextBeat();
          return;
        }
        if (BeatsRemBar < TSig.NN / 2) NewBBT(Bar, TSig.NN / 2, 0);
        else NewBBT(Bar + 1, 0, 0);
      }

      internal clsBBT GetPrevHalfBar() {
        clsBBT bbt = Copy();
        bbt.PrevHalfBar();
        return bbt;
      }

      internal clsBBT GetNextHalfBar() {
        clsBBT bbt = Copy();
        bbt.NextHalfBar();
        return bbt;
      }

      //*********************************************************************

      public override string ToString() {
        return (Bar + 1) + "." + (BeatsRemBar + 1) + "." + (Twelfth + 1);
      }

      public string ToStringBase0() {
        return Bar + "." + BeatsRemBar + "." + Twelfth;
      }
    }

    internal class clsTSig {
      //* time signature without reference to start bar/beat
      internal clsMTime MTime;
      internal int NN;
      internal int DD;
      internal int DDMidi;  //exponent of 2
      internal int MM;  //metronome clicks per bar
      internal string Txt;
      internal string TxtExt;
      internal int TicksPerBar;
      internal int TicksPerBeat;
      internal int MidiClocksPerBeat;
      internal int MidiClocksPerBar;
      internal int MidiClocksPerMetClick;  //midifile 'cc' param
      internal static clsNNDD[] CommonTSigs;
      internal static List<clsNNDD> AllTSigs = new List<clsNNDD>();

      static clsTSig() {
        int len = 9;
        CommonTSigs = new clsNNDD[len];
        CommonTSigs[0] = new clsNNDD(4, 4);
        CommonTSigs[1] = new clsNNDD(2, 2);
        CommonTSigs[2] = new clsNNDD(2, 4);
        CommonTSigs[3] = new clsNNDD(3, 4);
        CommonTSigs[4] = new clsNNDD(3, 8);
        CommonTSigs[5] = new clsNNDD(6, 8);
        CommonTSigs[6] = new clsNNDD(9, 8);
        CommonTSigs[7] = new clsNNDD(12, 8);
        CommonTSigs[8] = new clsNNDD(5, 4);

        for (int d = 2; d <= 32; d *= 2) {
          for (int n = 2; n <= 16; n++) {
            if (d == 2 && n > 8) continue;  //don't want very big bars - MaxBBT problems?
            AllTSigs.Add(new clsNNDD(n, d));
          }
        }
      }
      
      internal clsTSig(clsMTime mtime, int nn, int dd) {
        MTime = mtime;
        NN = nn;
        DD = dd;
        DDMidi = (int)Math.Log(DD, 2);
        Txt = new clsNNDD(NN, DD).ToString();
        TxtExt = NN.ToString() + '/' + DD.ToString() + " (" + MM.ToString() + ")";
        switch (NN) {
          case 6:
            MM = 2; break;
          case 9:
            MM = 3; break;
          case 12:
            MM = 4; break;
          default:
            MM = NN;
            break;
        }
        //Tick = tick;
        TicksPerBeat = (4 * mtime.TicksPerQNote) / dd;
        TicksPerBar = TicksPerBeat * nn;
        MidiClocksPerBeat = 96 / DD;  //24 midiclocks per quarternote
        MidiClocksPerBar = (96 * NN) / DD;
        MidiClocksPerMetClick = (96 * NN) / (DD * MM);
      }

      internal clsTSig(clsTSigBB tsig) : this(tsig.MTime, tsig.NN, tsig.DD) {
        //* create new tsig from tsigbb
      }

      internal bool IsEquiv(clsTSig x) {
        if (x == null) return false;
        return (NN == x.NN && DD == x.DD);
      }

      public override string ToString() {
        return Txt;
      }
    }

    internal class clsTSigBB : clsTSig {
      //internal int Index;
      internal int Bar;  //base0
      internal int Beat;  //base0: absolute
      internal int Tick = 0;  //base0 absolute
      //internal clsTSig Filler;

      internal clsTSigBB(clsMTime mtime, int nn, int dd, int tick, clsTSigBB tsigprev, bool adj)
        : base(mtime, nn, dd) {
        //* tsignext is next tsig before any filler insert, or null
        Tick = tick;
        if (tsigprev == null) {
          //Index = 0;
          Beat = 0;
          Bar = 0;
        } else {
          //Index = tsigprev.Index + 1;
          int rem;
          int prevbars = Math.DivRem(Tick - tsigprev.Tick, tsigprev.TicksPerBar, out rem);
          if (rem != 0) {
            if (adj) Tick -= rem; else throw new TSigException();
          }
          Bar = tsigprev.Bar + prevbars;
          Beat = tsigprev.Beat + prevbars * tsigprev.NN;
        }
      }

      internal clsTSigBB(clsTSig tsigbase, clsTSigBB tsigprev, int tick) :
        this(tsigbase.MTime, tsigbase.NN, tsigbase.DD, tick, tsigprev, adj: false) {
      }

      internal clsTSigBB(clsMTime mtime, clsTSig tsigbase, clsTSigBB tsigprev, int tick) :
        this(mtime, tsigbase.NN, tsigbase.DD, tick, tsigprev, adj: false) {
      }

      internal clsTSigBB(clsTSigBB tsigbb) : base(tsigbb) {  //clone
        //Index = tsigbb.Index;
        Bar = tsigbb.Bar;
        Beat = tsigbb.Beat;
        Tick = tsigbb.Tick;
      }

      internal clsTSigBB Copy() {
        return new clsTSigBB(this);
      }

      internal bool IsEquiv(clsTSigBB x) {
        if (!base.IsEquiv(x)) return false;
        if (Tick != x.Tick) return false;
        return true;
      }

      internal bool IsEquivNNDD(clsTSig x) {
        return base.IsEquiv(x);
      }

      //private clsNNDD GetNNDD(int ticks) {
      //  //* get a valid (filler) tsig with exact number of ticks, or null
      //  if (ticks < 1) return null;
      //  int rem;
      //  int num4ths = Math.DivRem(ticks, MTime.TicksPerQNote, out rem);
      //  if (rem == 0) return new clsNNDD(num4ths, 4);
      //  int num8ths = Math.DivRem(ticks * 2, MTime.TicksPerQNote, out rem);
      //  if (rem == 0) return new clsNNDD(num4ths, 8);
      //  int num16ths = Math.DivRem(ticks * 4, MTime.TicksPerQNote, out rem);
      //  if (rem == 0) return new clsNNDD(num4ths, 16);
      //  return null;
      //}
    }
  }
}
