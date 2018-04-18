using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

namespace ChordCadenza {
  internal class clsPlayKeyboard : clsPlay {
    //internal int _TransposeKB = 0;

    //internal int? ShowBadChordIndex = null;  //clsCF.clsEv index of chord to show
    //private delegate void delegShowChord(clsCF.clsEv ev);
    //private delegShowChord dShowChord;

    internal clsPlayKeyboard()
      : base() { //initial creation
      //dShowChord = new MPlay.clsPlayKeyboard.delegShowChord(ShowChord);
      P.frmSC.PlayMode = Forms.frmSC.ePlayMode.KB;
      if (P.frmSC.chkAutoCapitalize.Checked) Forms.frmSC.CapitalizeRootsStatic = false;
      P.frmSC.chkShowTracks.Enabled = true;
      InitPlayEvents();
      CF.SyncEvsToKeys();
      //CheckSyncs();
      P.frmSC.ShowRanges();   //P.frmSC.InvokeShowPlayMode();
    }

    internal clsPlayKeyboard(int lastwhitekeyon, int lastwhitepitchon, clsPlay oldplay)
      : base(oldplay) { //switch
      //frmSC = oldplay.frmSC;
      //CF = frmSC.CF;
      //dShowChord = new MPlay.clsPlayKeyboard.delegShowChord(ShowChord);
      P.frmSC.PlayMode = Forms.frmSC.ePlayMode.KB;
      LastKeyOn = lastwhitekeyon;
      LastPitchOn = lastwhitepitchon;
      LastPitchOnTime = null;
      if (P.frmSC.chkAutoCapitalize.Checked) Forms.frmSC.CapitalizeRootsStatic = false;
      P.frmSC.chkShowTracks.Enabled = true;
      InitPlayEvents();
      CF.SyncEvsToKeys();
      //CheckSyncs();
      P.frmSC.ShowRanges();   //P.frmSC.InvokeShowPlayMode();
    }

    private DateTime? LastPitchOnTime = null;  //only set by 
    internal static bool PlayNearestChordNote = false;
    //internal clsManChords ManChordsZZZ;
    //internal static bool PlayChordNotes = false;

    //protected override clsChordEv MidiOn_ThisPlayChord {
    //  get {
    //    if (ManChords != null) return ManChords.PlayChord;
    //    return _MidiOn_ThisPlayChord;
    //  }
    //  set {
    //    _MidiOn_ThisPlayChord = value;
    //  }
    //}

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

    internal override void ShowPicChord(clsCFPC.clsEv ev, Graphics xgr, bool roundup) {
      ShowPicChordStatic(ev, xgr, roundup);
    }

    internal static void ShowPicChordStatic(clsCFPC.clsEv ev, Graphics xgr, bool roundup) {
      //******** show unmodded (Pitch_NoKBTrans/KBTrans) if not null
      //******** still need to set Pitch_NoKBTrans
      if (ev.Notes == null || ev.Notes.Length == 0) return;

      //if (KBScale) {  //show chord notes on white keys
      //  if (ev == null || ev.PlayChord == null || ev.PlayChord.Chord == null) return;
      //  clsKey key = P.F.Keys[ev.PlayChord.OnTime];
      //  foreach (int cmajpc in ChordScales.CMajorToPC) {
      //    int? sfp = ChordScales.GetNearestDiaPitch(~cmajpc, key, ev.PlayChord);
      //    if (sfp.HasValue && ev.PlayChord.Chord.Contains(sfp.Value)) {
      //      frmSC.ShowChordNoteRiff(xgr, ev, cmajpc, sfp.Value);
      //    }
      //  }
      //  return;
      //}

      bool first = true;
      int rootpitch = -1;
      foreach (clsCFPC.clsEv.clsNote npw in ev.Notes) {
        bool dom = false;
        if (first && ev.Root) {  //root note
          //rootpitch = npw.PC_NoKBTrans;
          rootpitch = npw.PC[eKBTrans.None];
        } else if (rootpitch >= 0) {
          //int interval = npw.PC_NoKBTrans - rootpitch + 12;
          int interval = npw.PC[eKBTrans.None] - rootpitch + 12;
          if (interval.Mod12() == 7) dom = true;
        }
        clsCFPC.clsEv.sPitchPC pitchpc_kbtrans = npw.PitchPC_KBTrans;
        P.frmSC.ShowChordNote(xgr, pitchpc_kbtrans,
          ev.OnTime, npw.OnBBT.Ticks, ev.OffTime, first && ev.Root, dom, true, roundup);
        first = false;
      }
      if (P.frmSC.Play != null && ev.Notes.Length > 0 && ev.PlayChord == null) {  //support for KBChord
        int[] chord = new int[ev.Notes.Length];
        int i = 0;
        foreach (clsCFPC.clsEv.clsNote notepitchweight in ev.Notes) {
          //chord[i++] = notepitchweight.PC_KBTrans;
          chord[i++] = notepitchweight.PC[eKBTrans.Add];
        }
        ev.PlayChord = new clsChordEvTimed(P.frmSC.Play, ev, chord);
      }
    }

    internal override void ShowPicBottom(Graphics xgr) {
      clsKeyTicks key = P.F.Keys[P.F.CurrentBBT.Ticks, kbtrans: true];
      for (int pitch = frmSC.ShowLowPitch; pitch <= frmSC.ShowHighPitch; pitch++) {
        //if (NoteName.Diatonic[key.MidiKey + 7, key.MajMin].Substring(pitch.Mod12(), 1) == "1") {
        //  frmSC.ShowBottomNote(xgr, pitch);
          if (NoteName.Diatonic[key.MidiKey + 7, key.MajMin].Substring(pitch.Mod12(), 1) == "1") {
            frmSC.ShowBottomNote(xgr, pitch);
          }
        }
    }

    //protected override void Switch(clsPlay play) {
    //  frmSC.ResizeForm();
    //}

    //internal override int TransposeKB { get { return (int)frmSC.nudTransposeKB.Value; } }
    internal override int TransposeKB {
      get {
        if (P.F != null && P.frmSC != null) return (int)P.frmSC.nudTransposeKB.Value;
        return 0;
        //return (int)P.frmStart.nudTransposeKB.Value; 
      }
    }

    internal override int LastActiveKeyOn {
      get { return LastKeyOn; }
    }

    internal override int LastActivePitchOn {
      get { return LastPitchOn; }
    }

    protected override void ResetSub() {
      LastKeyOn = -1;
      LastPitchOn = -1;
      LastPitchOnTime = null;
    }

    //protected override int? MidiOnSub() {
    //  LastKeyOn = B[1];
    //  if (PlayNearestChordNote) {
    //    MidiOn_ThisPlayChord = GetPlayChord(out MidiOn_NextPlayChord);
    //    if (MidiOn_ThisPlayChord == null) {
    //      LastPitchOn = B[1];
    //    } else {
    //      int kb = B[1] - TransposeKB;
    //      int pitch = MidiOn_ThisPlayChord.GetNearestPitch(kb);
    //      if (pitch < 0) return B[1];
    //      pitch += TransposeKB;
    //      if (P.frmStart.chkKBChordMatch.Checked) {
    //        while (KBPitchList.GetPitch(false, pitch).Count > 0) {  //look for next highest if pitch already playing
    //          pitch = MidiOn_ThisPlayChord.GetNextUp(pitch);
    //          if (pitch < 0) return null;
    //        }
    //      }
    //      LastPitchOn = pitch;
    //    }
    //  } else {
    //    LastPitchOn = B[1];
    //  }
    //  LastPitchOn -= 12;  //kludge to get KBMode/ChordMode pitch consistency!!!
    //  frmSC.BeginInvoke(new delegResizeForm(frmSC.ResizeForm));
    //  return LastPitchOn;
    //}

    protected bool PlayMult() {
      return P.frmStart.chkConstantChordPlay.Checked
        && PlayNearestChordNote
        && P.frmStart.chkKBChordMatch.Checked;
        //&& P.ChordsSets.ActiveSet == null;
    }

    //internal override void SetOctPitch(int val) {
    //  if (P.ChordsSets.ActiveSet == null) SetOctTransposeKBPitchDefault(val);
    //  else {
    //    int diff = (val > OctTransposeKBPitch) ? 4 : -4;
    //    for (int i = 0; i < MidiOn_PrevChord.Length; i++) {
    //      MidiOn_PrevChord[i] = Math.Max(MidiOn_PrevChord[i] + diff, 0);  //major third
    //    }
    //  }
    //}

    //internal override void OctDecrPitch() {
    //  if (P.ChordsSets.ActiveSet == null) OctDecrDefault();
    //  else {
    //    for (int i = 0; i < MidiOn_PrevChord.Length; i++) {
    //      MidiOn_PrevChord[i] = Math.Max(MidiOn_PrevChord[i] - 4, 0);  //major third
    //    }
    //  }
    //}

    //internal override void OctIncrPitch() {
    //  if (P.ChordsSets.ActiveSet == null) OctIncrDefault();
    //  else {
    //    for (int i = 0; i < MidiOn_PrevChord.Length; i++) {
    //      MidiOn_PrevChord[i] = Math.Min(MidiOn_PrevChord[i] + 4, 127);  //major third
    //    }
    //  }
    //}

    //protected override void MidiOff() {
      //if (ManChordsZZZ == null || !ManChordsZZZ.MidiOff(B[1])) base.MidiOff();
    //}

    protected override void MidiOnSub() {
      //int ticks = (ticksv.HasValue) ? ticksv.Value : 0;
      int? pitch = B[1];
      if (PlayNearestChordNote) {  //keyswitch/pedal
        List<int> chlist = new List<int>(6);
        int loopcnt = (PlayMult()) ? (int)P.frmStart.nudNotesPerChordPlay.Value : 1;
        for (int i = 0; i < loopcnt; i++) {  //loopcnt > 1 for PlayKeyboard
          int? ret = MidiOnSub_KBChord(pitch.Value);
          if (!ret.HasValue || ret.Value < 0) break;
          MidiOn_Play(new int[] { ret.Value });
        }
        return;
      } else {
        LastPitchOn = pitch.Value - TransposeKB + TransposeKBPitch;
        LastPitchOnTime = null;
      }
      LastKeyOn = B[1];
      //frmSC.BeginInvoke(new delegResizeForm(frmSC.ResizeForm));
      MidiOn_Play(new int[] { LastPitchOn });   //return new int[] { LastPitchOn };
    }

    private void ShowChord(clsCF.clsEv ev) {
      //* used to display chord after playing wrong chordnote
      Graphics xgr = P.frmSC.picChords.CreateGraphics();
      ShowPicChord(ev, xgr, true);
    }

    private int? MidiOnSub_KBChord(int inpitch) {
      //* B[1] = key pressed
      //* pitch = pitch calculated by KBScale, or B[1]

      if (MidiOn_ThisPlayChord == null) {
        LastPitchOn = inpitch - TransposeKB + TransposeKBPitch;
        LastPitchOnTime = null;
      } else {
        int pitch = MidiOn_ThisPlayChord.GetNearestPitch(inpitch);
        if (pitch < 0) {
          LastPitchOn = inpitch - TransposeKB + TransposeKBPitch;
          return LastPitchOn;  //return for MidiOnSub
        }
        if (P.frmStart.chkKBChordMatch.Checked) {
          bool up = (B[1] >= LastKeyOn
            || !LastPitchOnTime.HasValue
            || (DateTime.Now - LastPitchOnTime.Value).TotalMilliseconds < 100);
          while (KBPitchList.GetPitch(false, pitch + TransposeKBPitch).Count > 0) {  //look for next highest if pitch already playing
            if (up) {
              pitch = MidiOn_ThisPlayChord.GetNextUp(pitch);  //chord or ascending arpeggio
            } else {
              pitch = MidiOn_ThisPlayChord.GetNextDown(pitch);  //descending arpeggio
            }
            if (pitch < 0) return null;  //return for MidiOnSub
          }
        }
        pitch += TransposeKBPitch;
        LastPitchOn = pitch;
        LastPitchOnTime = DateTime.Now;
      }
      return LastPitchOn;  
    }

    //private int[] MidiOn_PrevChord = new int[] { 48, 64, 67, 72 };  //assumed for now
    //private int[] MidiOn_ChordSets(long ticks) {
    //  int kb = B[1] - TransposeKB;
    //  List<int> chord = new List<int>();
    //  //int octs = kb / 12;  //not rounded
    //  //if (P.ChordsSets.ActiveSet != null) {
    //  clsKey key = P.F.Keys[(int)ticks];
    //  int scale = 0;
    //  if (key.Scale == "minor") scale = (int)P.frmStart.MinorKeyType;

    //  //bool up = (clsPlay.KBHi - kb <= 12);

    //  int root = (kb - key.KeyNote).Mod12();  //relative to current key
    //  chord.AddRange(P.ChordsSets.ActiveSet.Lines[root].PCLists[scale]);  //excl. bass
    //  if (P.ChordsSets.GetChordType() == clsChordSets.eChordType.Ma6) {
    //    chord.Add((root + 9).Mod12());  //Ma6 = 9 semitones
    //  } else if (P.ChordsSets.GetChordType() == clsChordSets.eChordType.Mi7) {
    //    chord.Add((root + 10).Mod12());  //Mi7 = 10 semitones
    //  }

    //  int bass = (root + key.KeyNote /*+ TransposeKB*/ + TransposeKBPitch).Mod12() + (4 * 12);

    //  //* transpose all chord notes to get correct key
    //  for (int i = 0; i < chord.Count; i++) {
    //    chord[i] = (chord[i] + key.KeyNote /*+ TransposeKB*/ + TransposeKBPitch).Mod12();
    //  }

    //  //* calculate prev high chord note 
    //  int prevhinote = MidiOn_PrevChord[MidiOn_PrevChord.Length - 1];

    //  //* shift chord notes to correct register
    //  for (int i = 0; i < chord.Count; i++) {
    //    //* oct transpose non-bass notes to highest note below or equal to prevchord note (down or same)        
    //    for (int oct = 0; oct < 16; oct++) {
    //      if (chord[i] + 12 > prevhinote) break;
    //      chord[i] += 12;
    //    }
    //  }

    //  //* check if new chord is going higher or lower
    //  int diff = 0;
    //  chord.Sort();
    //  for (int i = 0; i < chord.Count; i++) diff += chord[i] - MidiOn_PrevChord[i];

    //  //* change to up/down if necessary
    //  /*
    //  if (MidiOn_Up.HasValue) {
    //    if (diff > 0 && !MidiOn_Up.Value) {
    //      chord[chord.Count - 1] -= 12;  //hinote
    //      chord.Sort();
    //    } else if (diff < 0 && MidiOn_Up.Value) {
    //      chord[0] += 12;  //lonote
    //      chord.Sort();
    //    }
    //  }
    //  */

    //  LastPitchOn = chord[0];
    //  LastPitchOnTime = DateTime.Now;
    //  LastKeyOn = B[1];
    //  frmSC.BeginInvoke(new delegResizeForm(frmSC.ResizeForm));

    //  /*
    //  string msg = "Bass: ";
    //  msg += bass;
    //  msg += " Chord: ";
    //  foreach (int c in chord) msg += (c + " ");
    //  Debug.WriteLine(msg);
    //  */

    //  /*
    //  if (MidiOn_Up.HasValue) MidiOn_PrevChord = chord.ToArray();  //excl. bass
    //  */
    //  ///////////////////chord.Add(bass);  
    //  return chord.ToArray();  //bass at end
    //}

    //protected override void SustainPedalSub(byte val) { }

    //protected override void MidiOffSub() {
    //}

    internal override void ShowCurrentChord() {  //form thread
      //if (P.ChordsSets.ActiveSet == null) return;  //use with chordsets only
      return;

      /*
        //* show current chord (root and qualifier)
      bool[] pcs = new bool[12];
      lock (clsPlay.KBPitchList) {
        foreach (clsPlay.clsKBPitch pitch in KBPitchList.GetUnsustained()) {
          pcs[(pitch.Pitch + TransposeKB - TransposeKBPitch).Mod12()] = true;
        }
        bool[] kbs = KBPitchList.GetKBDownMod();
        int ticks = (MidiPlay.MidiInKB.Ticks.HasValue) ? (int)MidiPlay.MidiInKB.Ticks.Value : 0;
        int midikey = P.F.Keys[ticks].KBTrans_MidiKey;
        int keynote = P.F.Keys[ticks].KBTrans_KeyNote;
        List<string> lsttxt = new List<string>(4);
        int kbcount = 0;
        bool[] scalechordnotes = (P.F.Keys[ticks].Scale == "major") ? NoteName.MajChordNotes : NoteName.MinChordNotes;
        for (int kb = 0; kb < 12; kb++) {
          if (!kbs[kb]) continue;
          int rootpc = kb;
          List<string> names = ChordAnalysis.GetMatchingChordNames(pcs, rootpc);
          string qualifier = (names.Count == 0) ? "xxx" : names[0];
          if (P.frmStart.chkShowChordsRel.Checked) {
            lsttxt.Add(NoteName.GetDegree(rootpc, keynote) + qualifier);
          } else {
            lsttxt.Add(NoteName.ToSharpFlat(NoteName.Names[midikey + 7][rootpc].TrimEnd()) + qualifier);
          }
          if (++kbcount > 1) break;  //max 2 chords displayed

          //* check if chord is diatonic
          for (int pc = 0; pc < 12; pc++) {
            if (pcs[pc] && !scalechordnotes[(pc - keynote).Mod12()]) {
              lsttxt[lsttxt.Count - 1] += "*";  //not diatonic
              break;
            }
          }
        }

        P.frmSC.txtChordBottom.Lines = lsttxt.ToArray();
      }
      */

    }
  }
}