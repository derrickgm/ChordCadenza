#undef Debug

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

namespace ChordCadenza {
  internal abstract partial class clsPlay {
    internal class clsChordEvTimed : clsChordEv {
      internal int OnTime;

      internal clsChordEvTimed(clsPlay play, clsCFPC.clsEv ev, int[] chord) : base(play, chord, ev.Root) {
        OnTime = ev.OnTime;
      }
    }

    internal class clsChordEv{
      internal clsChordEv(clsPlay play, int[] chord, bool rootexists) {
        Chord = chord;  //CF.Ev[i].PlayChord
        RootExists = rootexists;
        if (RootExists) {
          ChordPCAbs = new bool[12];
          ChordPCRel = new bool[12];
          foreach (int c in Chord) {
            ChordPCAbs[c] = true;
            ChordPCRel[(c - Chord[0]).Mod12()] = true;
          }
        }
        Play = play;
        int kb = KBMid;
        int pitch = PitchMid;
        Align(pitch, kb);
        Status = eStatus.Play;
      }

      //private clsChordEv(clsCFPC.clsEv ev, int[] chord) {  //was clsChordEvBase
      //  //Index = evindex;  //CF.Evs[] index
      //}

      //internal clsChordEv(clsChordEv chordev, clsCF.clsEv ev) : base(ev, chordev.Chord) {
      //  //* clone (used by clsRiff.clsAuto) - messy!!!
      //  Play = chordev.Play;
      //}

      internal int KBLo { get { return Play.KBLo; } }
      internal int KBMid { get { return Play.KBMid; } }
      internal int KBHi { get { return Play.KBHi; } }
      protected int PitchMid { get { return clsPlay.PitchMid; } }
      protected int[] KBMap;
      internal int[] KBOct;
      private int[, ,] Select = new int[2, 4, 4]  //showlen-3, chordlen-1, chindex 
      {{{1,0,0,9}, {1,0,1,9}, {1,1,1,9}, {1,1,1,9}},   //showlen 3, chordlen 1,2,3,4+
       {{1,0,0,0}, {1,0,1,0}, {1,1,1,0}, {1,1,1,1}}};  //showlen 4, chordlen 1,2,3,4+

      internal enum eStatus { Initial, Preplay, Play };
      internal eStatus Status = eStatus.Initial;
      private enum eDir { Null, UpSoft, DownSoft, UpHard, DownHard };
      private delegate void delegShowText(string text);
      private clsPlay Play;
      /* private delegShowText dShowText; */

      internal int[] Chord;  //actual notes
      internal bool[] ChordPCAbs;  //pitch class
      internal bool[] ChordPCRel;  //relative to Root of this chord
      //internal int OffTime;
      internal bool RootExists = false;
      //internal int Index;

      protected void InitKBMap() {
        KBMap = new int[128];
        KBOct = new int[128];
        for (int i = 0; i < KBMap.Length; i++) {
          KBMap[i] = -1;
          KBOct[i] = -1;
        }
      }

      internal string GetChordTxt() {
        //* return displayable chord notes (eg C-Eb-G)
        //* assume first note is root
        string txt = "";
        foreach (int note in Chord) txt += NoteName.MajKeyNames[note] + "-";
        txt = txt.TrimEnd(new char[] { '-' });
        return txt;
      }

      protected void CalcOct() {
        int seq = -1;
        int oct = 0;
        for (int i = KBLo; i < KBHi; i++) {  //was <= KBHi
          if (++seq >= 12) {
            seq = 0;
            oct++;
          }
          KBOct[i] = oct;
        }
      }

      internal IEnumerable<int> Map {
        get {
          for (int i = KBLo; i < KBHi; i++) {  //was <= KBHi
            yield return KBMap[i];
          }
        }
      }

      internal int this[int kbnote] {
        get {
          kbnote = Math.Max(Math.Min(kbnote, 127), 0);
          return KBMap[kbnote];
        }
      }

      internal static int GetBlackDiff(int blackkey, int whitekey) {
        // calculate number of black keys between whitekey and blackkey 
        // <0: blackkey lower
        // >0: blackkey higher
        int dir = 1;
        if (blackkey < whitekey) dir = -1;
        if (!clsMidiInKB.IsBlackKey(blackkey) || clsMidiInKB.IsBlackKey(whitekey)) {
          LogicError.Throw(eLogicError.X027);
          return dir;
        }
        int cnt = 1;
        for (int i = whitekey; i != blackkey; i += dir) {
          if (clsMidiInKB.IsBlackKey(i)) cnt++;
        }
        return (cnt * dir);
      }

      private int[] GetShowChord() {
        //* return chord of constant length from Chord[]
        if (!P.frmStart.chkConstantChordDisplay.Checked) return Chord;
        int showlen = (int)P.frmStart.nudNotesPerChordDisplay.Value;
        if (showlen < 3) return Chord;
        if (showlen > 4) return Chord;
        int[] ret = new int[showlen];
        int chindex = 0;
        for (int i = 0; i < showlen; i++) {
          if (chindex >= showlen) throw new FatalException();
          int i1 = Math.Min(Select.GetLength(1) - 1, Chord.Length - 1);
          bool select = (Select[showlen - 3, i1, i] == 1);
          if (select) ret[i] = Chord[chindex++]; else ret[i] = -1;
        }
        return ret;
      }

      internal void Align(int pitch, int kbpos) {  
        InitKBMap();
        int octx12;
        int chindex = GetNearest(pitch, out octx12);

        int[] showchord = GetShowChord();

        //* create KBMap[] from kbpos upwards
        AlignUp(showchord, kbpos, 0, chindex, octx12);

        //* create KBMap[] from kbpos downwards
        AlignDown(showchord, kbpos, 0, chindex, octx12);
      }

      private void AlignUp(int[] showchord, int kbpos, int kbpitch, int chindex, int octx12) {
        int note;
        int prevnote = -1;
        for (int i = kbpos; i <= KBHi; i++) {
          if (clsMidiInKB.IsBlackKey(i)) i++;
          if (chindex >= showchord.Length) chindex = 0;
          note = showchord[chindex];
          chindex++;
          if (note < 0) continue;
          if (note < prevnote) octx12 += 12;
          KBMap[i] = note + kbpitch + octx12;
          prevnote = note;
        }
      }

      private void AlignDown(int[] showchord, int kbpos, int kbpitch, int chindex, int octx12) {
        int note;
        int prevnote = 999;
        for (int i = kbpos; i >= KBLo; i--) {  //override kbpos - should be the same 
          if (clsMidiInKB.IsBlackKey(i)) i--;
          if (chindex >= showchord.Length) chindex = 0;
          if (chindex < 0) chindex = showchord.Length - 1;
          note = showchord[chindex];
          chindex--;
          if (note < 0) continue;
          if (note > prevnote) octx12 -= 12;
          KBMap[i] = note + kbpitch + octx12;
          prevnote = note;
        }
      }

      internal int GetNearest(int pitch, out int octx12) {  //called by Align()
        //* return index of Chord.ChList containing note nearest to pitch, and octave
        //* 
        //Stopwatch stopwatch = new Stopwatch();
        //Debug.WriteLine("GetNearest start: " + DateTime.Now);
        int pitchclass = pitch.Mod12();
        int min = 99;
        int i = 0;
        int ret = 0;
        for (; i < Chord.Length; i++) {
          int note = Chord[i];
          int diff = Math.Abs(note - pitchclass);
          if (diff == 0) {
            ret = i;
            break;
          }
          if (diff > 6) diff = Math.Abs(diff - 12);
          if (diff < min) {
            min = diff;
            ret = i;
          }
        }
        int cp = Chord[ret];
        while (Math.Abs(cp - pitch) > 6) cp += 12;
        octx12 = (cp / 12) * 12;
        //Debug.WriteLine("GetNearest elapsed: "
        //  + stopwatch.ElapsedMilliseconds + " msecs" + " " +
        //  + stopwatch.ElapsedTicks + " ticks");
        return ret;
      }

      internal int GetNearestPitch(int pitch) {
        if (Chord.Length == 0) return -1;
        for (int d = 0; d < 7; d++) {
          int p = pitch + d;
          int pc = p.Mod12();
          if (Chord.Contains(pc)) return p;
          if (d > 0) {
            p = pitch - d;
            //pc = (p < 0) ? p + 12 : p.Mod12();
            pc = p.Mod12();
            if (Chord.Contains(pc)) return p;
          }
        }
        return -1;  //should not happen
      }

      internal int GetNextUp(int pitch) {
        //* get next higher chordnote from pitch
        for (int p = pitch + 1; p < 128; p++) {
          int pc = p.Mod12();
          if (Chord.Contains(pc)) return p;
        }
        return -1;  //no higher note available
      }

      internal int GetNextDown(int pitch) {
        //* get next lower chordnote from pitch
        for (int p = pitch - 1; p >= 0; p--) {
          int pc = p.Mod12();
          if (Chord.Contains(pc)) return p;
        }
        return -1;  //no lower note available
      }

      /*
      private void ShowText(string text) {
        P.F.frmShowChords.lblTest.Text = text;
        P.F.frmShowChords.lblTest.Refresh();
      }
      */

      /* private static int MapNext_TextCount = 0;  //for testing */
      internal void MapNext(clsChordEvTimed nextplaychord, int pitch, int kb, int callcount) {
        //* create kbmap from this ((previous) playchord)
        //* select nearest notes
        //* pitch and key used by GetDir to calculate mapping for hypothetical chord (size = chordsize)
        if (nextplaychord.Status == eStatus.Play) return;
        /* bool initial = false;  //temp (testing) */
        if (pitch < 0 || kb < 0) {  //initial (called from paint event)
          if (nextplaychord.Status != eStatus.Initial) return;  //initial - already done 
          pitch = PitchMid;
          kb = KBMid;
          /* initial = true;  //temp testing */
        }
        eDir dir = GetDir(pitch, kb);

#if Debug
      string text = "";
      if (dir == eDir.Null) text = "Null";
      else if (dir == eDir.DownSoft) text = "down";
      else if (dir == eDir.UpSoft) text = "up";
      else if (dir == eDir.DownHard) text = "DOWN";
      else if (dir == eDir.UpHard) text = "UP";
      if (!initial) P.F.frmShowChords.BeginInvoke(dShowText, text + ++MapNext_TextCount);
#endif

        nextplaychord.InitKBMap();
        int[] nextnote = new int[12];  //nextnote[thischordnote%12] = nextchordnote%12
        for (int i = 0; i < 12; i++) nextnote[i] = -99;

        for (int i = 0; i < Chord.Length; i++) {  //for each note in this 3,4 note chord
          nextnote[Chord[i]] = MapNote(nextplaychord, dir, i);
        }

        //* calculate KBMap from nextnote[]
        for (int k = KBLo; k <= KBHi; k++) {
          nextplaychord.KBMap[k] = SetKBMap(nextnote, k);
        }
        nextplaychord.Status = eStatus.Preplay;
      }

      private int SetKBMap(int[] nextnote, int k) {
        if (KBMap[k] < 0) return -1;  //black note
        int prevn = -1;
        int q = KBMap[k] / 12;   //octave
        int m = KBMap[k].Mod12();   //note 0-11
        int n = nextnote[m] + 12 * q;
        if (n - KBMap[k] > +6) n -= 12;
        if (n - KBMap[k] < -6) n += 12;
        if (n < prevn) {  //not sure if this can happen!
          LogicError.Throw(eLogicError.X028);
          return n;
        }
        prevn = n;
        return n;
      }

      private int MapNote(clsChordEvTimed nextplaychord, eDir dir, int i) {
        int mindiffpos = 99, mindiffposj = -1;
        int mindiffneg = 99, mindiffnegj = -1;
        int diff = -999;

        int j;
        //* calculate min interval (diff) up and down (pos/neg) for each note of next chord
        for (j = 0; j < nextplaychord.Chord.Length; j++) {  //for each note in next 3,4 note chord
          diff = GetDiff(Chord[i], nextplaychord.Chord[j]);  //positive if 2nd param highest
          if (diff == 0) {  //ignore dir - always choose if same note
            //nextnote[Chord[i]] = nextplaychord.Chord[j];
            break;
          } else if (diff > 0 && mindiffpos > diff) {  //next is up
            mindiffpos = diff;
            mindiffposj = j;
          } else if (diff < 0 && mindiffneg > -diff) {  //next is down
            mindiffneg = -diff;
            mindiffnegj = j;
          }
        }

        //* determine up true/false based on mindiffneg/pos and required direction (Dir) 
        bool up = true;  //true if mindiffpos is to be used (else mindiffneg)
        if (diff != 0) {
          switch (dir) {
            case eDir.Null:  //choose min interval
              up = (mindiffneg >= mindiffpos);
              break;
            case eDir.UpSoft:  //use smallest interval - up if equal
              up = (mindiffneg >= mindiffpos);
              break;
            case eDir.DownSoft:  //use smallest interval - down if equal
              up = (mindiffneg > mindiffpos);
              break;
            case eDir.UpHard:  //always go up (if not same note)
              up = true;
              break;
            case eDir.DownHard:  //always go down (if not same note)
              up = false;
              break;
            default:
              LogicError.Throw(eLogicError.X029);
              up = true;
              break;
          }
        }

        //* calculate mindiffj 
        int mindiffj;
        if (diff == 0) mindiffj = j;
        else if (mindiffposj == -1) mindiffj = mindiffnegj;
        else if (mindiffnegj == -1) mindiffj = mindiffposj;
        else if (up) mindiffj = mindiffposj;
        else mindiffj = mindiffnegj;

        return nextplaychord.Chord[mindiffj];
      }

      private int GetDiff(int p1, int p2) {
        int diff = p2 - p1;
        if (diff > 6) diff -= 12;
        else if (diff < -6) diff += 12;
        return diff;
      }

      private eDir GetDir(int pitch, int kb) {
        //calculate direction to move towards, based on hypothetical chord 
        int chordsize = (int)P.frmStart.nudNotesPerChordDisplay.Value;  //hypothetical chord
        int pitchlo = pitch - (kb - KBLo) * 7 / chordsize;  //lowest pitch on keyboard
        int pitchhi = pitch + (KBHi - kb) * 7 / chordsize;  //highest pitch on keyboard
        int lo = (int)Play.frmSC.valShowLowC;  //P.frmStart.nudChordLo.Value;
        int hi = Forms.frmSC.valPlayHiC;  //P.frmStart.nudChordHi.Value;
        if (lo >= hi) return eDir.Null;  //invalid
        if (pitchlo < lo && pitchhi < hi) return eDir.UpHard;    //lowest pitch on keyboard OOR; highest OK
        if (pitchlo > lo && pitchhi > hi) return eDir.DownHard;  //highest pitch on keyboard OOR; lowest OK
        if (lo - pitchlo > pitchhi - hi) return eDir.UpSoft;
        return eDir.DownSoft;
      }

      internal bool IsRoot(int pitch) {
        if (!RootExists) return false;
        return ((pitch.Mod12()) == Chord[0]);
      }

      internal bool IsDominant(int pitch) {
        if (!RootExists) return false;
        int diff = (pitch - Chord[0]).Mod12();  //pitchclass
        return (diff == 7);  //dominant interval from root
      }
    }
  }
}
