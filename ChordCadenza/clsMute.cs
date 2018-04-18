using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChordCadenza {
  internal class clsMute {
    //* manage mutedtracks, solotracks, exclch10 for streamplay
    //private static readonly int NumTrks = 64;
    private clsTrks.Array<bool> MutedTrackFromStart;
    private clsTrks.Array<bool> MutedTracks;
    private long SoloTracks = 0;  //64-bit mask
    internal bool ExclCh10 = false;
    private clsTrks Trks;
    //* update Copy() if field added here

    internal clsMute() : this(P.F.Trks) {
    }

    internal clsMute(clsTrks trks) {
      Trks = trks;
      MutedTrackFromStart = new clsTrks.Array<bool>(Trks, false);
      MutedTracks = new clsTrks.Array<bool>(Trks, false);
    }

    internal bool this[clsTrks.T trk] {
      get {
        return MutedTracks[trk];
      }
      set {
        MutedTracks[trk] = value;
        MutedTrackFromStart[trk] = false;  //use StartPlay() to mute from start
      }
    }

    internal clsMute Copy() {
      clsMute mute = new clsMute();
      foreach (clsTrks.T trk in MutedTrackFromStart.Next) {
        mute.MutedTrackFromStart[trk] = MutedTrackFromStart[trk];
        mute.MutedTracks[trk] = MutedTracks[trk];
      }
      mute.SoloTracks = SoloTracks;
      mute.ExclCh10 = ExclCh10;
      mute.Trks = Trks;
      return mute;
    }

    internal void StartPlay() {
      foreach (clsTrks.T trk in MutedTrackFromStart.Next) {
        MutedTrackFromStart[trk] = MutedTracks[trk];
      }
    }

    internal bool IsSolo(clsTrks.T trk) {
      //* return true if trk is solo'ed (played)
      if (SoloTracks == 0) return false;
      long mask = 1L << trk.TrkNum;
      return ((SoloTracks & mask) != 0);
    }

    internal void SetSolo(clsTrks.T trk, bool val) {
      long mask = 1L << trk.TrkNum;
      if (val) SoloTracks |= mask;
      else SoloTracks &= ~mask;
    }

    internal bool MutedEv(clsTrks.T trk, int chan, bool on) {
      //* check if an (ON) event should play
      if (ExclCh10 && chan == 9) return true;  //exclch10 overrides solo and mute
      if (IsSolo(trk)) return false;  //solo always plays
      if (SoloTracks != 0 && on) return true;  //don't play ON if other trk(s) solo'ed
      if (on) return MutedTracks[trk];
      return MutedTrackFromStart[trk];  //OFF & others
    }
  }
}
