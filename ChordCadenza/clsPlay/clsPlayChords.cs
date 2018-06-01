using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

namespace ChordCadenza {
  internal class clsPlayChords : clsPlay {
    //* initial creation
    internal clsPlayChords()
      : base() {  //initial
      P.frmSC.PlayMode = Forms.frmSC.ePlayMode.Chords;
      if (P.frmSC.chkAutoCapitalize.Checked) Forms.frmSC.CapitalizeRootsStatic = true;
      P.frmSC.chkShowTracks.Enabled = false;
      InitAlign();
      //frmSC.nudTransposeKB.Enabled = true;
      CF.SyncEvsToKeys();
      //CheckSyncs();
      P.frmSC.ShowRanges();   //P.frmSC.InvokeShowPlayMode();
    }

    internal clsPlayChords(int lastwhitekeyon, int lastwhitepitchon, clsPlay oldplay)
      : base(oldplay) {  //switch
      //* switch
      //frmSC = oldplay.frmSC;
      //CF = frmSC.CF;
      P.frmSC.PlayMode = Forms.frmSC.ePlayMode.Chords;
      LastWhiteKeyOn = lastwhitekeyon;
      LastWhitePitchOn = lastwhitepitchon;
      if (P.frmSC.chkAutoCapitalize.Checked) Forms.frmSC.CapitalizeRootsStatic = true;
      P.frmSC.chkShowTracks.Enabled = false;
      InitAlign();
      //frmSC.nudTransposeKB.Enabled = true;
      CF.SyncEvsToKeys();
      //CheckSyncs();
      P.frmSC.ShowRanges();   //P.frmSC.InvokeShowPlayMode();
    }

    private void InitPlayEvents() {
      for (int i = 0; i < CF.Evs.Count; i++) {  //for each chord
        clsCFPC.clsEv ev = CF.Evs[i];
        if (ev.Notes.Length == 0) continue;
        List<int> e = new List<int>();  //notes in Cf.Evs[i]
        //foreach (clsCFPC.clsEv.clsNote n in ev.Notes) e.Add(n.PC_NoKBTrans);
        foreach (clsCFPC.clsEv.clsNote n in ev.Notes) e.Add(n.PC[eKBTrans.None]);
        ev.PlayChord = new clsChordEvTimed(this, ev, e.ToArray());
      }
    }

    private void InitAlign() {
      //  if (!CF.indAlignTimer) {
      //    AlignTimer = null;
      InitPlayEvents();
      //  } else {
      //    AlignTimer = new System.Timers.Timer(Math.Max(1, (int)P.frmStart.nudTimerChordPlay.Value));
      //    AlignTimer.AutoReset = false;
      //    AlignTimer.Elapsed += this.OnAlignTimer;
      //  }
    }

    //private void InitPlayEvents() {
    //  for (int i = 0; i < CF.Evs.Count; i++) {  //for each chord
    //    clsCF.clsEv ev = CF.Evs[i];
    //    if (ev.Notes.Length == 0) continue;
    //    List<int> e = new List<int>();  //notes in Cf.Evs[i]
    //    foreach (clsCF.clsEv.sNote n in ev.Notes) e.Add(n.Pitch_NoKBTrans);
    //    ev.PlayChord = new clsChordEv(ev, i, e.ToArray(), -1, -1);
    //  }
    //}

    //internal static System.Timers.Timer AlignTimer;
    //private List<int> KBList;
    //private clsChordEv LastChordPlayed;

    //internal int LastChordPlayBlackPitch = -1;
    //internal int LastChordPlayBlackKey = -1;
    //internal int LastChordPlayBlackDir = 0;

    private int LastWhiteKeyOn = -1;  //white key (clsPlayChord)
    private int LastWhitePitchOn = -1;  //white key (clsPlayChord)

    internal override int LastActiveKeyOn {
      get { return LastWhiteKeyOn; }
    }

    internal override int LastActivePitchOn {
      get { return LastWhitePitchOn; }
    }

    protected override void ResetSub() {
      LastWhiteKeyOn = -1;
      LastWhitePitchOn = -1;
    }

    //protected override void Switch(clsPlay play) {
    //  CF.ChordPlaySwitch();
    //  frmSC.ResizeForm();
    //}

    //internal void StopAlignTimer() {
    //  if (AlignTimer != null) AlignTimer.Stop();
    //}

    internal override int TransposeKB { get { return 0; } }

    //protected override bool PlayMult() {
    //  return false;
    //  //return P.frmStart.chkConstantChordPlay.Checked;
    //}

    protected override void MidiOnSub() {
      //if (!ticksv.HasValue) return;
      //int ticks = ticksv.Value;
      int chan = (B[0] & 0x0f);
      List<clsCFPC.clsEv> evs = CF.Evs;

      if (MidiOn_ThisPlayChord == null) return;
      int pitch = MidiOn_ThisPlayChord[B[1]];

      if (P.frmStart.indConstantChordPlay) {  //play multiple chord notes
        List<int> pp = new List<int>();
        for (int kb = B[1]; kb < 128; kb++) {
          pitch = MidiOn_ThisPlayChord[kb];
          if (pitch >= 0) {  //should not happen
            if (pp.Count == 0) {
              LastWhitePitchOn = pitch;
              LastWhiteKeyOn = kb;
            }
            pitch += -TransposeKB + TransposeKBPitch - 12;
            pp.Add(pitch);
            if (pp.Count >= P.frmStart.nudNotesPerChordPlay.Value) break;
          }
        }
        MidiOn_Play(pp.ToArray());   //return pp.ToArray();
        return;
      } else {  //white key
        LastWhitePitchOn = pitch;
        LastWhiteKeyOn = B[1];
        pitch += -TransposeKB + TransposeKBPitch - 12;
        if (pitch < 0) return;
      }
      MidiOn_Play(new int[] { pitch });  //return new int[] { pitch };
    }

    internal override void ShowPicChord(clsCFPC.clsEv ev, Graphics xgr, bool roundup) {
      //show graphical chord notes 
      if (ev.PlayChord == null) {
        if (ev.Notes.Length == 0) {
          frmSC.ShowNullChord(xgr, ev.OnTime);
          return;
        }
        int[] chord = new int[ev.Notes.Length];
        int i = 0;
        foreach (clsCFPC.clsEv.clsNote notepitchweight in ev.Notes) {
          //chord[i++] = notepitchweight.PC_KBTrans;
          chord[i++] = notepitchweight.PC[eKBTrans.Add];
        }
        ev.PlayChord = new clsChordEvTimed(this, ev, chord);
      }
      frmSC.ShowChord(xgr, (clsChordEvTimed)ev.PlayChord, true);
    }

    internal override void ShowPicBottom(Graphics xgr) {
      if (P.PCKB != null) {
        for (int p = KBLo; p <= KBHi; p++) {
          if (clsMidiInKB.IsBlackKey(p)) continue;
          frmSC.ShowBottomNote(xgr, p);
        }
      }
    }

    internal override void ShowCurrentChord() {
      P.frmSC.txtChordBottom.Text = "";
    }
  }
}
