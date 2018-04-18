using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace ChordCadenza {
  internal enum eKBTrans { None, Add, Sub };

  internal abstract partial class clsCF {
    internal abstract class clsEv : IComparable<clsEv> {
      //internal bool Diff = false;
      internal bool Root = false;  //0-11 if root exists, else -1
      internal clsMTime.clsBBT OnBBT;
      internal clsMTime.clsBBT OffBBT;
      //internal clsEv AltEv;   //alternative chord
      internal clsPlay.clsChordEvTimed PlayChord;
      //internal int Index;  //ev seq
      //protected clsCF CF;
      internal string ChordQualifier = "";  //eg "dim", "m", "")
      //internal clsRiffs.eChordType RiffChordType;
      internal string BassNote = "";
      //internal float? Score = null;   //from ChordAnalysis (0: match, <0: nearest chord)

      internal abstract clsEv CopyEv();

      internal int OnTime {  //ticks
        get { return OnBBT.Ticks; }
      }

      protected clsEv(clsEv ev, clsMTime.clsBBT onbbt, clsMTime.clsBBT offbbt) {  //clone for different time
        Copy(ev);
        OnBBT = onbbt;
        OffBBT = offbbt;
      }

      protected clsEv(clsEv ev) {  //clone 
        Copy(ev);
      }

      protected void Copy(clsEv ev) {
        //CF = ev.CF;
        CopyCF(ev);
        //Notes = ev.Notes.ToArray();  //OnBBT or OFFBBT may be changed 
        CopyNotes(ev.Notes.ToArray());
        Root = ev.Root;
        ChordQualifier = ev.ChordQualifier;
        BassNote = ev.BassNote;
        OnBBT = ev.OnBBT.Copy();
        OffBBT = ev.OffBBT.Copy();
        PlayChord = ev.PlayChord;
        //AltEv = ev.AltEv;
      }

      protected clsEv(clsMTime.clsBBT onbbt) {  //used for binary search
        OnBBT = onbbt;
      }

      protected clsEv(clsMTime.clsBBT onbbt, clsMTime.clsBBT offbbt) {  //used for binary search
        OnBBT = onbbt;
        OffBBT = offbbt;
      }

      protected clsEv() {
        OnBBT = new clsMTime.clsBBT(0);
        OffBBT = new clsMTime.clsBBT(0);
      }

      public int CompareTo(clsEv ev) {  //use OnTicks as sorted key for BinarySearch
        if (OnTime < ev.OnTime) return -1;
        if (OnTime > ev.OnTime) return 1;
        return 0;   //equals 
      }

      //public string ToString(clsKey key) {
      //  if (Notes.Length == 0) return "null";
      //  int root = (Root) ? Notes[0].PC_KBTrans : -1;
      //  if (root < 0) return "???";
      //  return NoteName.ToSharpFlat(NoteName.GetNoteName(key.MidiKey, Notes[0].PC_KBTrans)).TrimEnd() + ChordQualifier;
      //}

      internal int OffTime { //ticks
        get {
          if (OffBBT == null) throw new ChordFileException(); //return -1;
          return OffBBT.Ticks;
        }
      }

      //internal void AddTicks(int ticks) {
      //  //* if indoff, set for off evs only (extend chord)
      //  //* this should only be used on all events from a point in time
      //  OnBBT = new clsMTime.clsBBT(OnTime + ticks);
      //  OffBBT = new clsMTime.clsBBT(OffTime + ticks);
      //  foreach (clsNote note in Notes) {
      //    note.OnBBT = new clsMTime.clsBBT(note.OnBBT.Ticks + ticks);
      //    note.OffBBT = new clsMTime.clsBBT(note.OffBBT.Ticks + ticks);
      //    if (note.OffBBT.Ticks >= P.F.MaxBBT.Ticks) {
      //      clsMTime.clsBBT bbt = new clsMTime.clsBBT(note.OffBBT.Ticks);
      //      P.F.MaxTicks = bbt.GetNextBar().Ticks;
      //    }
      //  }
      //}

      internal bool SubTicks(int ticks) {
        //* this should only be used on all events from a point in time
        //* called from frmChordMap.DeleteBar()
        if (OnTime < ticks) return false;
        OnBBT = new clsMTime.clsBBT(OnTime - ticks);
        OffBBT = new clsMTime.clsBBT(OffTime - ticks);
        foreach (clsNote note in Notes) {
          note.OnBBT = new clsMTime.clsBBT(note.OnBBT.Ticks - ticks);
          note.OffBBT = new clsMTime.clsBBT(note.OffBBT.Ticks - ticks);
        }
        return true;
      }

      internal bool[] GetBoolNotes(eKBTrans kbtrans = eKBTrans.None) {
        bool[] boolchord = new bool[12];
        foreach (clsNote n in Notes)
          //if (kbtrans) boolchord[n.PC_KBTrans] = true;
          //else boolchord[n.PC_NoKBTrans] = true;
          boolchord[n.PC[kbtrans]] = true;
        return boolchord;
      }

      internal bool[] GetBoolNotesTriad(eKBTrans kbtrans) {
        //* return bool[12] containing maj, min, dim, aug triad notes (or null)
        bool[] boolchord = new bool[12];
        if (Notes.Length < 3) return null;
        boolchord[Notes[0].PC[kbtrans]] = true;  //root 
        int count = 1;
        for (int i = 1; i < Notes.Length; i++) {
          int diff = (Notes[i].PC[kbtrans] - Notes[0].PC[kbtrans]).Mod12();
          //* check if mi3, ma3, dim5, p5, aug5 relative to root
          if (diff != 3 && diff != 4 && diff != 6 && diff != 7 && diff != 8) continue;
          boolchord[Notes[i].PC[kbtrans]] = true;
          count++;
        }
        return (count == 3) ? boolchord : null;
      }

      protected static List<int> ConvBoolArrayToListInt(int root, bool[] boolarray, out bool rootexists) {
        List<int> listint = new List<int>();
        rootexists = false;
        int notecnt = 0;
        for (int i = 0; i < 12; i++) if (boolarray[i]) notecnt++;
        if (notecnt == 0) return listint;
        for (int i = 0; ; i = (i + 1).Mod12()) {//loop
          if (boolarray[i] && i == root) rootexists = true;
          if (!rootexists && i == 11) root = -1;  //not returned
          if (boolarray[i] && (rootexists || root < 0)) {
            listint.Add(i);
            if (listint.Count == notecnt) break;
          }
        }
        return listint;
      }

      internal static List<int> ConvBoolArrayToListInt(bool[] boolarray) {
        List<int> listint = new List<int>();
        for (int i = 0; i < 12; i++) {
          if (boolarray[i]) listint.Add(i);
        }
        return listint;
      }

      //internal string ChordName(sNote[] notes) {  //eg C, Am, F#dim
      internal string ChordName(eKBTrans kbtranspc, bool kbtranskey) {  //eg C, Am, F#dim
        //* show in txt boxes
        if (Notes == null || Notes.Length == 0) return "null";
        if (Root) {
          //int pitch = (kbtrans) ? Notes[0].PC_KBTrans : Notes[0].PC_NoKBTrans;
          int pitch = Notes[0].PC[kbtranspc];
          //int midikey = P.F.Keys[OnTime].KBTrans_MidiKey;
          clsKey key = P.F.Keys[OnTime, kbtranskey];
          //return NoteName.ToSharpFlat(NoteName._Names[midikey + 7][pitch].TrimEnd()) + ChordQualifier;
          return NoteName.ToSharpFlat(NoteName.GetName(key, pitch).TrimEnd()) + ChordQualifier;
        } else {
          return "xxxx";
        }
      }

      internal string ChordNameRoman(clsKeyTicks key, eKBTrans kbtrans) {  //eg I, vi, #IVo
        if (Notes == null || Notes.Length == 0) return "null";
        if (Notes.Length < 3 || !Root) return "xxxx";
        //int rootpitch = (kbtrans) ? Notes[0].PC_KBTrans : Notes[0].PC_NoKBTrans;
        //string roman = NoteName.GetDegree(rootpitch, key.KBTrans_KeyNote);
        int rootpitch = Notes[0].PC[kbtrans];
        string roman = NoteName.GetDegree(rootpitch, key.KeyNote);
        return roman + ChordQualifier;
      }

      /*
      internal static void CreateScaleChord(clsCF cf, clsEv ev) {
        //Root = true;
        //* not used, but may be used in future
        //////Debug.WriteLine("if this is called, check if it should be updating P.F.CFTxt.Evs!!!");
        //////Debugger.Break();
        ev.Notes = new sNotePC[7];
        clsKeys keys = P.F.Keys;
        int ontime = ev.OnTime;
        int midikey = keys[ontime].MidiKey;  //key = 0-14  
        //int midikey = keys[ontime].MidiKey + 7;  //key = 0-14  
        int keynote = keys[ontime].KeyNote;  //keynote = 0-11  (C=0)
        int majmin = keys[ontime].MajMin;
        int[] scale = NoteName.GetScaleChord(midikey, keynote, majmin);
        for (int i = 0; i < 7; i++) {
          ev.Notes[i] = new sNote();
          ev.Notes[i].PC_NoKBTrans = scale[i];
          ev.Notes[i].OnBBT = ev.OnBBT;
          ev.Notes[i].OffBBT = ev.OffBBT;
          //NotePitchWeight[i].Note_NoKBTrans = NoteName.GetNoteName(midikey, scale[i]).TrimEnd();
        }
        //ChordQualifier = keys[ontime].Scale;
      }
      */

      //internal string GetNotesStr(sNote[] notes, bool align) {
      //internal string GetNotesStr() {
      //  //* show in txt boxes
      //  string ret = "";
      //  if (Notes == null) return ret;
      //  foreach (clsNote np in Notes) {
      //    string notestr = np.Note_KBTrans(P.F.Keys[OnTime].KBTrans_MidiKey + 7) + " ";
      //    //if (align && notestr.Length == 2) notestr += " ";
      //    ret += notestr;
      //    //ret += np.Note_KBTrans(CF.Keys[OnTime].KBTrans_MidiKey + 7) + " ";
      //  }
      //  ret = NoteName.ToSharpFlat(ret);
      //  return ret;
      //}

      internal bool Equiv(clsEv ev, bool dup) {
        if (ev == null) return false;
        bool indnull = (Notes == null || Notes.Length == 0);
        bool indnull2 = (ev.Notes == null || ev.Notes.Length == 0);
        if (indnull && !indnull2) return false;
        if (!indnull && indnull2) return false;
        if (indnull && indnull2) return true;
        //*!indnull && !indnull2
        if (Notes.Length != ev.Notes.Length) return false;
        if (OnTime != ev.OnTime || OffTime != ev.OffTime) return false;
        if (!dup) {  //default
          for (int i = 0; i < Notes.Length; i++) {
            if (Notes[i].OnBBT.Ticks != ev.Notes[i].OnBBT.Ticks) return false;
            if (Notes[i].OffBBT.Ticks != ev.Notes[i].OffBBT.Ticks) return false;
            if (Notes[i].PC[eKBTrans.None] != ev.Notes[i].PC[eKBTrans.None]) return false;
            if (ChordQualifier != ev.ChordQualifier) return false;
            if (Root != ev.Root) return false;
          }
          return true;
        } else {
          return CheckDup(ev);
        }
        //if (BassNote != ev.BassNote) return false;
        //if (PlayChord != ev.PlayChord) return false;
      }

      protected bool CheckDup(clsCFPC.clsEv ev2) {
        //* return true if duplicate
        bool[] bool1 = new bool[12];
        bool[] bool2 = new bool[12];
        foreach (clsCFPC.clsEv.clsNote s in Notes) bool1[s.PC[eKBTrans.None]] = true;
        foreach (clsCFPC.clsEv.clsNote s in ev2.Notes) bool2[s.PC[eKBTrans.None]] = true;
        return bool1.SequenceEqual(bool2);
      }

      internal clsNote SamePitch(clsNote note) {
        //* return copy of Note element with same pitch as note, or null 
        foreach (clsNote n in Notes) {
          if (n == null) continue;
          //if (n.PC_NoKBTrans == note.PC_NoKBTrans) return n.Copy();
          if (n.PC[eKBTrans.None] == note.PC[eKBTrans.None]) return n.Copy();
        }
        return null;
      }

      internal abstract clsEv New(clsMTime.clsBBT bbt, clsMTime.clsBBT offbbt);
      internal abstract clsNote[] Notes { get; }
      protected abstract void CopyNotes(clsNote[] notes);
      protected abstract void CopyCF(clsEv ev);

      internal abstract class clsNote {
        internal clsMTime.clsBBT OnBBT;  //allowing for contiguous notes
        internal clsMTime.clsBBT OffBBT;  //allowing for contiguous notes
        //protected int _PC_NoKBTrans;  //pitchclass

        internal abstract clsPC PC { get; }
        //internal abstract int PC_NoKBTrans { get; }
        //internal abstract int PC_KBTrans { get; }
        internal abstract sPitchPC PitchPC_KBTrans { get; }
        internal abstract clsNote Copy();

        //internal string Note_KBTrans(int key) {
        //  return NoteName.Names[key][PC_KBTrans].TrimEnd();
        //}

        internal class clsPC {
          private int PC;

          internal clsPC(int pc) {
            PC = pc;
          }

          internal int this[eKBTrans kbtrans] {
            get {
              int x = (kbtrans == eKBTrans.Add) ? P.frmSC.Play.TransposeKB : 0;
              x = (kbtrans == eKBTrans.Sub) ? -P.frmSC.Play.TransposeKB : x;
              return (PC + x).Mod12(); 
            }
          }
        }
      }

      internal struct sPitchPC {
        internal int PitchPC;
        internal bool indPC;

        internal sPitchPC(int pitchpc, bool indpc) {
          PitchPC = pitchpc;
          indPC = indpc;
        }
      }

      internal class clsNotePC : clsNote {
        private int _PC_NoKBTrans;  //pitchclass

        internal clsNotePC() { }

        internal clsNotePC(int pc) {
          _PC_NoKBTrans = pc;
          OnBBT = null;
          OffBBT = null;
        }

        internal override clsNote Copy() {
          clsNotePC ret = new clsNotePC(_PC_NoKBTrans);
          ret.OnBBT = OnBBT;
          ret.OffBBT = OffBBT;
          return ret;
        }

        internal override clsPC PC {
          get {
            return new clsPC(_PC_NoKBTrans);
          }
        }

        //internal override int PC_NoKBTrans {
        //  get {
        //    return _PC_NoKBTrans;
        //  }
        //}

        internal void Set_PC_NoKBTrans(int value) {
          _PC_NoKBTrans = value;
        }

        //internal override int PC_KBTrans {
        //  get {
        //    if (P.F == null || P.frmSC == null || P.frmSC.Play == null) return PC_NoKBTrans;
        //    return (PC_NoKBTrans + P.frmSC.Play.TransposeKB).Mod12();
        //  }
        //}

        internal override sPitchPC PitchPC_KBTrans {  //return pitchclass
          get {
            //return new sPitchPC(PC_KBTrans, indpc: true);
            return new sPitchPC(PC[eKBTrans.Add], indpc: true);
          }
        }
      }

      internal class clsNotePitch : clsNote {
        internal clsNotePitch() { }

        internal clsNotePitch(int pitch) {
          _Pitch_NoKBTrans = pitch;
          OnBBT = null;
          OffBBT = null;
        }

        internal class clsPitch {
          private int Pitch;

          internal clsPitch(int pitch) {
            Pitch = pitch;
          }

          internal int this[eKBTrans kbtrans] {
            get {
              if (P.frmSC.Play == null) return Pitch;
              int x = (kbtrans == eKBTrans.Add) ? P.frmSC.Play.TransposeKB : 0;
              x = (kbtrans == eKBTrans.Sub) ? -P.frmSC.Play.TransposeKB : x;
              return (Pitch + x);  //no Mod12()
            }
          }
        }

        private int _Pitch_NoKBTrans;   //unmodded (midifile source only)

        //internal int Pitch_NoKBTrans {
        //  get {
        //    return _Pitch_NoKBTrans;
        //  }
        //  set {
        //    _Pitch_NoKBTrans = value;
        //  }
        //}

        internal override clsPC PC {
          get {
            //return new clsPC(PC_NoKBTrans);
            return new clsPC(_Pitch_NoKBTrans.Mod12());
          }
        }

        //internal override int PC_NoKBTrans {
        //  get {
        //    return _Pitch_NoKBTrans.Mod12();
        //  }
        //}

        internal clsPitch Pitch {
          get {
            return new clsPitch(_Pitch_NoKBTrans);
          }
        }

        //internal override int PC_KBTrans {
        //  get {
        //    if (P.F == null || P.frmSC == null || P.frmSC.Play == null) return PC_NoKBTrans;
        //    return (PC_NoKBTrans + P.frmSC.Play.TransposeKB).Mod12();
        //  }
        //}

        //internal int Pitch_KBTrans {
        //  get {
        //    if (P.F == null || P.frmSC == null || P.frmSC.Play == null) return _Pitch_NoKBTrans;
        //    return _Pitch_NoKBTrans + P.frmSC.Play.TransposeKB;
        //  }
        //}

        internal override sPitchPC PitchPC_KBTrans {  //return actual pitch
          get {
            //return new sPitchPC(Pitch_KBTrans, false);
            return new sPitchPC(Pitch[eKBTrans.Add], false);
          }
        }

        internal override clsNote Copy() {
          clsNotePitch ret = new clsNotePitch(_Pitch_NoKBTrans);
          //if (ret.OnBBT != null) ret.OnBBT = OnBBT.Copy();
          //if (ret.OffBBT != null) ret.OffBBT = OffBBT.Copy();  
          ret.OnBBT = OnBBT;
          ret.OffBBT = OffBBT;  
          return ret;
        }
      }
    } //clsEv
  }
}