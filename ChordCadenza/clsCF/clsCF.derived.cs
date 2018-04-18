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
  internal class clsCFPitch : clsCF {
    //internal clsCFNotes(string filename) : base(filename) { }
    internal clsCFPitch(int dummy) : base(0) { }
    internal int MinPitch = 0;
    internal int MaxPitch = 127;

    internal void CreateEvsFromMidi(clsFileStream filestream) {
      //* create evs from notemapmidi
      NoChords = true;
      Evs = new List<clsEv>();
      for (int q = 0; q < P.F.MaxBBT.QI; q++) {
        clsNoteMapMidi notemapmidi = filestream.NoteMap;
        //if (q > 0 && notemapmidi[q] == notemapmidi[q - 1]) continue;
        if (q > 0 && notemapmidi.FullMapEquals(q, q - 1)) continue;
        clsMTime.clsBBT bbt = new clsMTime.clsBBT(q * P.F.TicksPerQI);
        //clsEv ev = new clsEv(this, bbt, notemapmidi[q], null);
        clsEvPitch ev = new clsEvPitch(this, bbt, notemapmidi[q, false, true]);  //unmodded
        ev.ChordQualifier = "";
        if (ev.Notes.Length == 0 && P.frmStart.chkIgnoreNullChords.Checked) continue;  //null chord
        if (Evs.Count > 0 && Evs[Evs.Count - 1].Notes.Length == 0) {  //previous ev was null
          if (bbt.Ticks - Evs[Evs.Count - 1].OnTime <= Forms.frmSC.SyncopationDefault.Ticks) Evs.RemoveAt(Evs.Count - 1);
        }
        if (Evs.Count > 0) Evs[Evs.Count - 1].OffBBT = bbt;  //set offtime for previous line
        Evs.Add(ev);
      }
      if (Evs.Count > 0) SetLastEv();
      SyncEvsToKeys();
      CalcRange();
    }


    private void CalcRange() {
      int min = 127, max = 0;
      foreach (clsEv ev in Evs) {
        if (ev.Notes == null) continue;
        foreach (clsEv.clsNotePitch note in ev.Notes) {
          //if (note.Pitch_NoKBTrans < min) min = note.Pitch_NoKBTrans;
          //if (note.Pitch_NoKBTrans > max) max = note.Pitch_NoKBTrans;
          if (note.Pitch[eKBTrans.None] < min) min = note.Pitch[eKBTrans.None];
          if (note.Pitch[eKBTrans.None] > max) max = note.Pitch[eKBTrans.None];
        }
      }
      if (min != 127 && max != 0) {
        MinPitch = min;
        MaxPitch = max;
      }
    }
  }

  internal partial class clsCFPC : clsCF {
    internal clsCFPC() {
      //Syncopation = P.frmStart.Syncopation.Copy();
      //BeatMarginDD = 2;
      //* maxqtime taken from midi file
      //P.F.CHPFilePath = filename;
      Lines = Utils.ReadLinesWithComments(P.F.Project.CHPPath, out Lines_Comments);
      if (Lines == null) {
        throw new ChordFileException("ChordFile empty or load failure");
      }
      if (ReadHeader() == "") return;
      ReadTSigs();
      ReadParams();
      ReadKeys();
      if (P.frmSC?.Play != null) P.frmSC.Play.Reset();   //clear fields
      //if (P.frmSC != null && P.F.AutoSync == null) P.frmSC.mnuCreateSyncFile.Enabled = true;
    }

    internal clsCFPC(int dummy) : base(0) { }

    internal void CreateEvs() {
      //* create evs from notemapcf
      Evs = new List<clsEv>();
      //for (int q = 0; q < P.F.CF.NoteMap.GetLengthQTime(); q++) {
      for (int q = 0; q < P.F.MaxBBT.QI; q++) {
        if (q > 0 && NoteMap.GetMap(q) == NoteMap.GetMap(q - 1) //null != unrecognised chord
          && NoteMap.GetChordAtt(q).ChordEquals(NoteMap.GetChordAtt(q - 1))) continue;
        clsMTime.clsBBT bbt = new clsMTime.clsBBT(q * P.F.TicksPerQI);
        clsNoteMapCF ncf = (clsNoteMapCF)NoteMap;
        clsEv ev = new clsEvPC(this, bbt, ncf[q], ncf.GetChordAtt(q).Root);
        ev.ChordQualifier = clsNoteMap.PtrToDesc(ncf.GetChordAtt(q).Qualifier);
        if (ev.Notes.Length == 0 && P.frmStart.chkIgnoreNullChords.Checked) continue;  //null chord
        //if (ncf.ChordAtt[q].Marked > 0) ev.Mark = true;
        if (Evs.Count > 0 && Evs[Evs.Count - 1].Notes.Length == 0) {  //previous ev was null
          if (bbt.Ticks - Evs[Evs.Count - 1].OnTime <= Forms.frmSC.SyncopationDefault.Ticks) Evs.RemoveAt(Evs.Count - 1);
        }
        if (Evs.Count > 0) Evs[Evs.Count - 1].OffBBT = bbt;  //set offtime for previous line
        Evs.Add(ev);
      }
      if (Evs.Count > 0) SetLastEv();
      SyncEvsToKeys();
      //if (P.frmSC != null && P.F.AutoSync == null) P.frmSC.mnuCreateSyncFile.Enabled = true;
    }

    internal ChordAnalysis.clsScore ChordinateEv(ref clsEvPC ev) {  //return score
      //* assumes ChordAnalysis OpenDumpChords/SetParams already executed
      if (ev.Notes == null) return null;
      bool[] chord = new bool[12];
      clsMTime.clsBBT onbbt = ev.OnBBT;
      clsMTime.clsBBT offbbt = ev.OffBBT;
      List<int> chnotes;
      string qualifier;
      for (int i = 0; i < ev.Notes.Length; i++) chord[ev.Notes[i].PC[eKBTrans.None]] = true;
      //ChordAnalysis.clsScore score = ChordAnalysis.GetTopChord(chord, P.F.Keys[onbbt.Ticks],
      //  (int)P.frmCfgChords.nudMaxChordTypeNoMatch.Value, onbbt, 
      //  out qualifier, out chnotes);
      ChordAnalysis.clsScore score = ChordAnalysis.GetTopChord(chord, P.F.Keys[onbbt.Ticks],
        Forms.frmChordRanks.MaxRankNoMatch, onbbt, out qualifier, out chnotes);
      if (score != null) {
        bool[] t = new bool[12];
        for (int n = 0; n < chnotes.Count; n++) t[chnotes[n]] = true;
        ev = new clsEvPC(this, onbbt, t, chnotes);
        ev.ChordQualifier = qualifier;
        ev.OffBBT = offbbt;
        ev.Root = true;
        return score;
      }
      return null;
    }

    protected List<string> ReadNotes() {
      List<string> ret = new List<string>();
      if (Lnum >= Lines.Count) return ret;
      if (Lines[Lnum].Trim() == "Start Chords") Lnum++;  //not used here - skip
      for (int prevonticks = int.MinValue; Lnum < Lines.Count; Lnum++) {
        string[] ll;  //line delimited by spaces
        clsMTime.clsBBT bbt;
        string line = ReadBBTLine(prevonticks, out ll, out bbt);
        if (line.Length > 0) ret.Add(line);  //"" if > P.F.MaxBBT.Bar
        if (ll == null) continue;  //offset or (null and chkIgnoreNullChords.Checked)

        //* check for "end" line
        if (ll[1] == "end" || bbt.Bar > P.F.MaxBBT.Bar) {
          break;
          //if (Evs.Count > 0) {
          //  int endticks = Math.Min(P.F.MaxBBT.Ticks, bbt.Ticks);
          //  Evs[Evs.Count - 1].OffBBT = new clsMTime.clsBBT(endticks);
          //}
          //return ret;  //ignore anything after "end" line
        }

        //* read note(s)
        clsEvPC ev;
        try {
          ev = new clsEvPC(this, bbt, ll);
        }
        catch (ChordFileException) {  //should be ChordFileException
          ev = new clsEvPC(this, bbt, new string[] { "", "null" });
        }

        if (ev.Notes.Length == 0 && P.frmStart.chkIgnoreNullChords.Checked) continue;  //null chord

        if (Evs.Count > 0 && Evs[Evs.Count - 1].Notes.Length == 0) {  //previous ev was null
          if (bbt.Ticks - Evs[Evs.Count - 1].OnTime <= Forms.frmSC.SyncopationDefault.Ticks) Evs.RemoveAt(Evs.Count - 1);
        }
        prevonticks = bbt.Ticks;

        if (Evs.Count > 0) Evs[Evs.Count - 1].OffBBT = bbt;  //set offtime for previous line
        if (Evs.Count > 0 && Evs[Evs.Count - 1].OnTime == bbt.Ticks) {  //same time - alternative chord
          continue;
          //clsEv altev = new clsEv(this, ll);
          //Evs[Evs.Count - 1].AltEv = altev;
        } else {
          Evs.Add(ev);
        }
      }

      //* set lastev offtime (if no "end" line)
      SetLastEv();
      return ret;
    }

    internal void PostInit() {
      //if (frmSC != null) frmSC.SetStartBar();

      Lines_Notes = ReadNotes();
      //if (frmSC != null) frmSC.NewPlay();
      P.frmSC.Play = clsPlay.NewPlay(frmSC.PlayMode);
      SyncEvsToKeys();
      CreateNoteMap();
    }

    internal void SetNoteMapFileChanged() {
      SetNoteMapFileChanged(undoredo: true, indqi: true);
    }

    internal void SetNoteMapFileChanged(bool undoredo, bool indqi) {
      indSave = true;
      if (indqi) {  //notemap -> evs
        CreateEvs();
      } else {  //evs -> notemap
        SyncEvsToKeys();
        CreateNoteMap();
      }
      P.F.frmChordMap?.RefreshDGV();
      if (undoredo && P.F.frmChordMap != null) P.F.CF?.UndoRedoCF?.Update();  //Undo/Redo cmds on frmChordMap
      if (P.frmSC != null) {
        P.frmSC.ReInitPlayMode();  //to allow for transposition, ...
        P.frmSC.Refresh();
      }
      if (P.F.frmTonnetz != null) P.F.frmTonnetz.Refresh();
      P.F.frmChordMap?.Refresh();
    }
  }

  internal abstract partial class clsCF {
    internal class clsEvPC : clsEv {
      internal override clsEv New(clsMTime.clsBBT bbt, clsMTime.clsBBT offbbt) {
        return new clsEvPC(this, bbt, offbbt);
      }

      private clsNotePC[] _Notes;

      internal override clsNote[] Notes {
        get {
          return _Notes;
        }
      }

      internal override clsEv CopyEv() {
        return new clsEvPC(this);
      }

      protected override void CopyNotes(clsNote[] notes) {
        _Notes = new clsNotePC[notes.Length];
        for (int i = 0; i < notes.Length; i++) _Notes[i] = (clsNotePC)notes[i].Copy(); 
        //_Notes = (sNotePC[])notes.ToArray();
      }

      protected override void CopyCF(clsEv ev) {
        CF = ((clsEvPC)ev).CF;
      }

      private clsCFPC CF;

      internal clsEvPC(clsMTime.clsBBT onbbt, clsMTime.clsBBT offbbt) : base(onbbt, offbbt) {
        //* null evpc 
        _Notes = new clsNotePC[0];
      }

      internal clsEvPC(clsEvPC evpc) : base(evpc) { }  //clone

      internal clsEvPC(clsEvPC evpc, clsMTime.clsBBT onbbt, clsMTime.clsBBT offbbt)
        : base(evpc, onbbt, offbbt) { }  //clone for different time

      internal clsEvPC(clsCFPC cf, clsMTime.clsBBT onbbt, string[] ll)
        : this(cf, ll) {  //read .chp lines
        OnBBT = onbbt;
        int ontime = onbbt.Ticks;
        if (ontime < 0) throw new ChordFileException();
      }

      internal clsEvPC(clsCFPC cf, string[] ll) {  //null bbt, or called by other constructor
        //bars. beats, twelfths start at 0
        CF = cf;
        //Index = index;

        if (ll[1] == "null") {  //no chord - use scale (7 note chord)
          _Notes = new clsNotePC[0];
          return;  //show nothing
        }

        List<clsNotePC> listnpw = new List<clsNotePC>();
        int chordpitch = -1;
        int notecnt = 0;
        for (int i = 1; i < ll.Length; i++) {  //ll = space delimited line, incl bbt
          string l = ll[i];
          if (l.StartsWith("..")) {  //bassnote
            BassNote = l.TrimStart(new char[] { '.' });  //not yet used...
          } else if (l.StartsWith(".")) {  //chordname
            string name = l.TrimStart(new char[] { '.' });
            chordpitch = NoteName.GetPitchAndQualifier(name, out ChordQualifier);
            if (chordpitch < 0) throw new ChordFileException();  //should be ChordFileException
          //} else if (l.StartsWith(":")) {  //chordname (marked)
          //  string name = l.TrimStart(new char[] { ':' });
          //  chordpitch = NoteName.GetPitchAndQualifier(name, out ChordQualifier);
          //  if (chordpitch < 0) throw new ChordFileException();  //should be ChordFileException
          //  Mark = true;
          } else {  //note
            if (++notecnt > (int)P.frmStart.nudMaxChordSize.Value) continue;
            clsNotePC npw = new clsNotePC();
            int pitch = NoteName.GetPitch(l);
            if (pitch < 0) {  //not a valid note - assume chord+qualifier
              ll[i--] = "." + l;
              continue;
            }
            npw.Set_PC_NoKBTrans(NoteName.GetPitch(l));
            listnpw.Add(npw);
          }
        }

        if (chordpitch >= 0) {  //chordname present, with or without notes 
          string qual = ChordQualifier.ToLower();
          ChordAnalysis.clsTemplate t = ChordAnalysis.NameToTemplate[qual];
          ChordQualifier = t.Name;  //change to correct case and spelling (if necessary)
          if (listnpw.Count == 0) {  //get notes from ChordAnalysis
            listnpw.Add(new clsNotePC(chordpitch));  //root
            for (int p = 1; p < 12; p++) {  //start at 1 - root already added
              if (t.PC[p]) listnpw.Add(new clsNotePC((chordpitch + p).Mod12()));
            }
            //* if (listnpw.Count == 0) throw new ChordFileException();  //chord qualifier not found
            //* temp frig - need to get user exceptions working with the debugger
            if (listnpw.Count == 0) throw new ChordFileException();
          }
        }

        if (listnpw.Count < P.frmStart.nudMinChordSize.Value) {   //should be 0 unless advanced...
          _Notes = new clsNotePC[0];  //null chord
        } else {
          _Notes = listnpw.ToArray();
          if (chordpitch == Notes[0].PC[eKBTrans.None]) Root = true;
        }
      }

      internal clsEvPC(clsCFPC chordfile, clsMTime.clsBBT onbbt, bool[] chord, List<int> chnotes) {  //create from notemap etc.
        //* chnotes optional
        CF = chordfile;
        OnBBT = onbbt;
        if (chnotes == null || chnotes.Count == 0) {
          chnotes = new List<int>();
          Root = false;
          for (int i = 0; i < 12; i++) if (chord[i]) chnotes.Add(i);
        } else {
          Root = true;
        }
        List<clsNotePC> listnpw = new List<clsNotePC>();
        foreach (int n in chnotes) listnpw.Add(new clsNotePC(n));
        _Notes = listnpw.ToArray();
      }

      internal clsEvPC(clsCFPC chordfile, clsMTime.clsBBT onbbt, bool[] chord, int root) {  //create from notemapCF
        CF = chordfile;
        OnBBT = onbbt;
        List<int> notelist = ConvBoolArrayToListInt(root, chord, out Root);
        List<clsNotePC> listnpw = new List<clsNotePC>();
        foreach (int n in notelist) listnpw.Add(new clsNotePC(n));
        _Notes = listnpw.ToArray();
      }

      internal clsEvPC(clsCFPC chordfile, clsMTime.clsBBT onbbt, int[] chord) {  //BIAB, ManChords
        CF = chordfile;
        OnBBT = onbbt;
        Root = true;
        List<clsNotePC> listnpw = new List<clsNotePC>();
        for (int i = 0; i < chord.Length; i++) {
          clsNotePC npw = new clsNotePC();
          //npw.PC_NoKBTrans = chord[i].Mod12();
          npw.Set_PC_NoKBTrans(chord[i]);
          listnpw.Add(npw);
        }
        _Notes = listnpw.ToArray();
      }

      internal clsEvPC(clsMTime.clsBBT onbbt) : base(onbbt) { }

      internal clsEvPC() : base() { }
    }

    internal class clsEvPitch : clsEv {
      internal override clsEv New(clsMTime.clsBBT bbt, clsMTime.clsBBT offbbt) {
        return new clsEvPitch(this, bbt, offbbt);
      }

      internal clsEvPitch(clsEvPitch evpitch) : base(evpitch) { }  //clone

      internal clsEvPitch(clsEvPitch evpitch, clsMTime.clsBBT onbbt, clsMTime.clsBBT offbbt)
        : base(evpitch, onbbt, offbbt) { }  //clone for different time

      internal clsEvPitch(clsCFPitch chordfile, clsMTime.clsBBT onbbt, bool[] chord) {  //bool[128] unmodded
        //* called from CreateEvsFromMidi()
        CFNotes = chordfile;
        OnBBT = onbbt;
        Root = false;
        List<int> chnotes = new List<int>();
        for (int i = 0; i < 128; i++) if (chord[i]) chnotes.Add(i);
        List<clsNotePitch> listnpw = new List<clsNotePitch>();
        foreach (int n in chnotes) listnpw.Add(new clsNotePitch(n));
        _Notes = listnpw.ToArray();
      }

      internal clsEvPitch(clsMTime.clsBBT onbbt) : base(onbbt) { }

      internal clsEvPitch() : base() { }

      private clsNotePitch[] _Notes;

      internal override clsEv CopyEv() {
        return new clsEvPitch(this);
      }

      internal override clsNote[] Notes {
        get {
          return _Notes;
        }
      }

      protected override void CopyNotes(clsNote[] notes) {
        _Notes = new clsNotePitch[notes.Length];
        for (int i = 0; i < notes.Length; i++) _Notes[i] = (clsNotePitch)notes[i].Copy();
        //_Notes = (sNotePitch[])notes.ToArray();
      }

      protected override void CopyCF(clsEv ev) {
        CFNotes = ((clsEvPitch)ev).CFNotes;
      }

      private clsCFPitch CFNotes;
    } //clsEvPitch
  } //clsCF
}