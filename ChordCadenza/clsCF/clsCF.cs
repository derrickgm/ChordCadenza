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
  internal abstract partial class clsCF {
    internal clsUndoRedoCF UndoRedoCF;
    internal clsNoteMapCF NoteMap;
    //internal int BeatMarginDD;
    internal int LeadInBars = 0;
    internal List<clsEv> Evs = new List<clsEv>();
    //protected List<string> SectionGroupsIn = new List<string>();
    internal bool NoChords = false;  //true if created directly from midifile track(s)

    protected readonly char[] Space = new char[] { ' ' };
    protected readonly char[] Dot = new char[] { '.' };
    protected readonly char[] Slash = new char[] { '/' };
    protected readonly char[] Comma = new char[] { ',' };

    protected int Lnum;  //line number
    protected List<string> Lines;
    internal List<string> Lines_Comments = new List<string>();
    //internal string Line_Header;
    //internal List<string> Lines_TSigs;
    //internal List<string> Lines_Params;
    //internal List<string> Lines_Keys;
    internal List<string> Lines_Notes = new List<string>();
    private clsF.clsindSave _indSave = new clsF.clsindSave();
    protected Forms.frmSC frmSC { get { return P.frmSC; } }
    internal static clsNNDD Syncopation;
    internal string Description = "";
    private int NoMidi_TPQN = 192;
    //internal static int SongLength;
    internal static int DefaultSongLength { get { return (int)P.frmStart.nudDefaultSongLength.Value; } }
    //internal static clsNNDD AlternateSyncopation;

    //static clsCF() {
    //  SongLength = DefaultSongLength;
    //}

    internal clsCF() {  //null (no txt file yet)
      Forms.frmChordMap.StaticTransposeChordNamesVal = 0;
      Syncopation = new clsNNDD(1, 8);  
      SetSyncopationNN(P.frmStart.Syncopation.NN);
      SetSyncopationDD(P.frmStart.Syncopation.DD);
    }

    internal clsCF(int dummy) : this() {  //null (no txt file yet)
      Forms.frmChordMap.StaticTransposeChordNamesVal = 0;
      //NoteMap = new clsNoteMapCF(this, null, P.F.MaxBBT.QI);
      NoteMap = new clsNoteMapCF(this, null);
      P.F.CurrentBBT = new clsMTime.clsBBT(0);
      //if (setkeys) P.F._CFKeys = new clsKeysTicks(P.F._MidiKeys);  //clone
      //UndoRedoCF = new clsDo();
    }

    internal bool indSave {
      get { return _indSave.Ind; }
      set { _indSave.Ind = value; }
    }

    internal static bool IsEmpty() {
      if (P.F == null || P.F.CF == null) return true;
      if (P.F.CF.Evs.Count == 0) return true;
      return false;
    }

    internal void TransposeNoteMap(int val) {
      for (int qi = 0; qi < P.F.MaxBBT.QI; qi++) {  //do everything - not just selected area
        NoteMap.TransposeChord(qi, val);
      }
    }

    internal float GetChordNotesMissing(clsFileStream fs, clsTrks.T trk) {
      //* return aggregate notes missing per tick
      int all_missing = 0;
      int totticks = 0;

      foreach (clsCFPC.clsEv ev in P.F.CF.Evs) {
        if (ev == null || ev.Notes == null || ev.Notes.Length == 0) continue; 

        //* find notes present in notemapmidi[trk] between ev.OnTime and ev.OFfTime
        bool[] nmpresent = new bool[12];
        bool empty = true;
        for (int n = 0; n < 12; n++) {
          for (int q = ev.OnTime / P.F.TicksPerQI; q < ev.OffTime / P.F.TicksPerQI; q++) {
            if (fs.NoteMap[q, n, trk]) {
              nmpresent[n] = true;
              empty = false;
              break;
            }
          }
        }
        if (empty) continue;

        //* get notes present in ev(chord) but not notemampmidi[trk] 
        int nm_missing = 0;
        foreach (clsEv.clsNote n in ev.Notes) {
          //if (!nmpresent[n.PC_NoKBTrans]) nm_missing++;
          if (!nmpresent[n.PC[eKBTrans.None]]) nm_missing++;
        }

        //* get notes present in notemapmidi[trk] but not ev(chord)
        int ev_missing = 0;
        for (int n = 0; n < 12; n++) {
          if (nmpresent[n] && !ev.GetBoolNotes(eKBTrans.None)[n]) ev_missing++;
        }

        //* add to all_missing aggregate and totticks
        all_missing += (nm_missing + ev_missing) * (ev.OffTime - ev.OnTime);
        totticks += ev.OffTime - ev.OnTime;
      }
      return (float)all_missing / totticks;
    }

    //internal void DelimitUndo() {
    //  UndoRedoCF.PushDelimiter();
    //}

    internal void UndoMap() {
      UndoRedoCF.Undo();
    }

    internal void RedoMap() {
      UndoRedoCF.Redo();
    }

    internal void CreateNoteMap() {
      //* create NoteMap from Evs
      if (P.F.MaxBBT == null) {
        LogicError.Throw(eLogicError.X012);  //should be set from load midifile
      }
      List<clsFileStream.clsOO> oolist = new List<clsFileStream.clsOO>();
      int minpitch = int.MaxValue;
      int maxpitch = int.MinValue;

      foreach (clsEv ev in Evs) {  //ON notes in the chord
        foreach (clsEv.clsNote note in ev.Notes) {
          clsFileStream.clsOO oo = new clsFileStream.clsOO(
            ev.OnTime, true, 0, note.PC[eKBTrans.None], 80, P.F.TicksPerQI);
          oolist.Add(oo);
        }

        //int maxticks = maxqtime * TicksPerQI;
        foreach (clsEv.clsNote note in ev.Notes) {  //OFF notes in the chord
          int offtime = ev.OffTime;
          if (offtime > P.F.MaxBBT.Ticks) {
            if (ev.OnTime < P.F.MaxBBT.Ticks) offtime = P.F.MaxBBT.Ticks;  //CFtxt last offtime is assumed
            else {
              LogicError.Throw(eLogicError.X009);  //clsCFtxt longer than midi file
              offtime = P.F.MaxBBT.Ticks;
            }
          }
          clsFileStream.clsOO oo = new clsFileStream.clsOO(
            offtime, false, 0, note.PC[eKBTrans.None], 0, P.F.TicksPerQI);
          oolist.Add(oo);
          minpitch = Math.Min(minpitch, note.PC[eKBTrans.None]);
          maxpitch = Math.Max(maxpitch, note.PC[eKBTrans.None]);
        }
      }

      //MaxQTime = maxqtime;  //from midi file

      P.F.CFTrks = new clsTrks();
      P.F.CFTrks.SetNumTrks(1);
      clsTrks.Array<List<clsFileStream.clsOO>> trkarray = new clsTrks.Array<List<clsFileStream.clsOO>>();
      trkarray[new clsTrks.T(P.F.CFTrks, 0)] = oolist;
      clsOnOff onoff = clsFileStream.CreateOnOff1(trkarray, false);
      //NoteMap = new clsNoteMapCF(this, onoff, P.F.MaxBBT.QI + 1);
      NoteMap = new clsNoteMapCF(this, onoff);

      if (Evs.Count > 0) {
        clsEv ev = Evs[0];
        int evi = -1;
        clsNoteMapCF.sChordAtt att = new clsNoteMapCF.sChordAtt(0);  //default
        //for (int q = 0; q < Evs[0].OnTime; q++) NoteMap.ChordAtt[q] = att;
        for (int q = 0;  q <= P.F.MaxBBT.QI; q++) {
          int ticks = q * P.F.TicksPerQI;
          if (ticks >= ev.OffTime || ticks == Evs[0].OnTime) {
            if (++evi >= Evs.Count) att = new clsNoteMapCF.sChordAtt(0);  //no more evs
            else {
              ev = Evs[evi];
              int root = (ev.Root) ? ev.Notes[0].PC[eKBTrans.None] : -1;
              sbyte ptr = clsNoteMap.DescToPtr(ev.ChordQualifier);
              //ChordAnalysis.eStatus status = ChordAnalysis.eStatus.NotChordinated;
              att = new clsNoteMapCF.sChordAtt((sbyte)root, ptr);
              //if (q == 9) q = q;  //temp debugging
            }
          }
          NoteMap.SetChordAtt(q, att);
          //NoteMap.Marked[q] = (ev.Mark) ? (sbyte)1 : (sbyte)0;
        }
      }
    }

    internal int FindCFEv(clsMTime.clsBBT bbt) {
      //* return matching index, or previous index
      int index = Evs.BinarySearch(new clsEvPC(bbt));
      index = (index < 0) ? ~index - 1 : index;  //binarysearch not found
      index = (index < 0) ? 0 : index;  //index must be >= 0
      return index;
    }

    protected string ReadHeader() {
      //* format: <tsig> X|<ticksperqnote>[,<numbars>] <keynote> major|minor Description
      if (Lines.Count == 0) throw new ChordFileException();
      string[] f = Lines[0].Split(Space, 5); 

      //* read initial tsig (f[0])
      string[] s = f[0].Split(Slash, 2);  //nn/dd
      int tsignn = int.Parse(s[0]);
      int tsigdd = int.Parse(s[1]);
      P.frmStart.TraceLoad("initial tsig from chpfile " + tsignn + "/" + tsigdd);

      //* read comma separated parms 
      string[] parms = f[1].Split(Comma);
      int numwholenotes = clsCF.DefaultSongLength;
      //* get tpqn - 120 is the most common, but not divisible by 64 (qidd)
      int tpqn;
      if (int.TryParse(parms[0], out tpqn)) NoMidi_TPQN = tpqn;  //192 if not int
      if (parms.Length > 1) int.TryParse(parms[1], out numwholenotes);  //-1 if not int

      //* initnullmidifile or addtsigs
      if (P.F.MTime == null) {  //no .mid file
        clsF.InitNullMidiFile(numwholenotes, NoMidi_TPQN, tsignn, tsigdd);
        P.frmSC.cmdMultiMap.Enabled = false;
        if (P.F.frmChordMap != null) P.F.frmChordMap.cmdMultiMap.Enabled = false;
      } else {
        P.F.MTime.AddTSigFirst(tsignn, tsigdd); 
      }

      //* read keys (f[2] f[3])
      if (f[2] != "X") {
        P.F._CFKeys = new clsKeysTicks(f[2], f[3]);  //override existing value (if any)
      }

      //* read description
      if (f.Length == 5) Description = f[4];

      Lnum = 1;  //next line
      return Lines[0];
    }

    internal List<string> ReadParams() {
      List<string> ret = new List<string>();

      //* set default tempo factor
      if (P.frmSC != null) {
        P.frmSC.trkTempo.Value = 0;
        P.frmSC.trkTempo_Scroll(null, null);
      }

      //* initialize maptrk
      P.frmSC.MapTrks = new List<clsTrks.T>();

      //P.Riffs.InitRiffs();  //set live riffs to default (static) values
      for (; Lnum < Lines.Count; Lnum++) {   //skip over first line (header)
        string[] f = Lines[Lnum].Split(Space);
        string f0 = f[0].ToLower();
        if (f.Length == 2 && f0 == "syncopationnn") SetSyncopationNN(int.Parse(f[1]));
        else if (f.Length == 2 && f0 == "syncopationdd") SetSyncopationDD(int.Parse(f[1]));
        else if (f.Length == 2 && f0 == "ccmap") { }  //ReadCCMap(f);
        else if (f.Length == 2 && f0 == "transpose") ReadTranspose(f);  //obsolete
        else if (f.Length == 2 && f0 == "pitchwheelinterval") continue;  //obsolete
        else if (f.Length == 2 && f0 == "leadinbars") continue;  //obsolete
        else if (f.Length >= 2 && f0 == "mutedtracks") ReadMutedTracks(f);
        else if (f.Length >= 2 && f0 == "vol") ReadCtlr(f, P.F.Vol, 0, 127);
        else if (f.Length >= 2 && f0 == "pan") ReadCtlr(f, P.F.Pan, 0, 127);
        else if (f.Length >= 2 && f0 == "patch") ReadCtlr(f, P.F.Patch, 0, 127);
        else if (f.Length >= 2 && f0 == "chan") ReadChans(f);
        //else if (f.Length >= 2 && f0 == "select") ReadSelect(f);
        else if (f.Length >= 2 && f0 == "beatmargindd") continue;  //obsolete
        else if (f.Length >= 2 && f0 == "trackchans") continue;  //obsolete
        else if (f.Length >= 2 && f0 == "trackports") continue;  //obsolete
        else if (f.Length >= 2 && f0 == "trackvols") continue;  //obsolete
        else if (f.Length >= 2 && f0 == "trackpans") continue;  //obsolete
        else if (f.Length >= 2 && f0 == "trackpatches") continue;  //obsolete
        else if (f.Length == 2 && f0 == "tempofactor") ReadTempoFactor(f);
        //else if (f.Length == 2 && f0 == "sectionsgroup") SectionGroupsIn.Add(f[1]);
        else if (f.Length == 2 && f0 == "minorkeytype") ReadMinorKeyType(f[1]);
        else if (f.Length == 2 && f0 == "transkb") ReadTransKB(f[1]);
        else if (f.Length == 2 && f0 == "transkbpitch") ReadTransKBPitch(f[1]);
        else if (f.Length == 2 && f0 == "transstream") ReadTransStream(f[1]);
        else if (f.Length == 2 && f0 == "transposechordnamesval") ReadTransposeChordNamesVal(f[1]);
        //else if (f.Length == 2 && f0 == "barpanebarlo") ReadBarPaneBarLo(f[1]);
        //else if (f.Length == 2 && f0 == "barpanebarhi") ReadBarPaneBarHi(f[1]);
        else if (f.Length >= 2 && f0 == "maptrk") ReadMapTrks(f);
        else if (f.Length >= 2 && f0 == "firsttransnamessharp") ReadFirstTransNamesSharp(f[1]);
        //xsw.WriteLine("MinorKeyType " + P.frmStart.MinorKeyType.ToString());
        else break;
        ret.Add(Lines[Lnum]);
      }
      //P.frmStart.nudTimerBeatMargin.Value = P.F.MTime.TicksPerQNote / BeatMarginDD;
      if (P.F.frmTrackMap != null) {
        P.F.frmTrackMap.UpdateControls();
      }
      if (P.F.Project.MidiExists && P.frmSC.MapTrks.Count == 0) P.frmSC.MapTrks.Add(new clsTrks.T(P.F.Trks, 0));
      return ret;
    }

    protected void ReadMapTrks(string[] f) {
      P.frmSC.Bypass_Event = true;
      for (int i = 1; i < f.Length; i++) {
        string ff = f[i];
        int trk = int.Parse(ff);
        if (trk >= P.F.Trks.NumTrks) continue;
        P.frmSC.MapTrks.Add(new clsTrks.T(P.F.Trks, trk));
      }
      P.frmSC.Bypass_Event = false;
    }

    //protected void ReadBarPaneBarLo(string f1) {
    //  P.F.BarPaneBBTLo = new clsMTime.clsBBT(int.Parse(f1), 0, 0);
    //}

    //protected void ReadBarPaneBarHi(string f1) {
    //  P.F.BarPaneBBTHi = new clsMTime.clsBBT(int.Parse(f1), 0, 0);
    //}

    protected void ReadTransKB(string f1) {
      P.frmSC.nudTransposeKB.Value = int.Parse(f1);
    }

    protected void ReadTransKBPitch(string f1) {
      P.frmSC.nudTransposeKBPitch.Value = int.Parse(f1);
    }

    protected void ReadTransStream(string f1) {
      P.frmSC.nudTransposeStreamPitch.Value = int.Parse(f1);
    }

    protected void ReadTransposeChordNamesVal(string f1) {
      int val = int.Parse(f1);
      if (P.F.frmChordMap != null) P.F.frmChordMap.nudTransposeChordNames.Value = val;  //-> static
      else Forms.frmChordMap.StaticTransposeChordNamesVal = val;
    }

    protected void ReadFirstTransNamesSharp(string f1) {
      P.F._CFKeys[0].TransposeNamesSharp = bool.Parse(f1);
    }

    protected void SetSyncopationNN(int nn) {
      Syncopation.NN = nn;
      if (P.frmSC != null) {
        P.frmSC.nudSyncopationNN.Value = nn;
        //if (frmSC.SyncopationCurrent != null) frmSC.SyncopationCurrent.NN = Syncopation.NN;  //new
      }
    }

    protected void SetSyncopationDD(int dd) {  //called from ReadParams
      Syncopation.DD = dd;
      if (P.frmSC != null) {
        P.frmSC.Bypass_Event = true;
        //P.frmSC.nudSyncopationDD.Value = dd;
        Forms.frmStart.SetNudAndTag(P.frmSC.nudSyncopationDD, dd);
        P.frmSC.Bypass_Event = false;
        //if (frmSC.SyncopationCurrent != null) frmSC.SyncopationCurrent.DD = Syncopation.DD;  //new
      }
    }

    protected void ReadMinorKeyType(string f1) {
      P.frmStart.MinorKeyType = (eMinorKeyType)Enum.Parse(typeof(eMinorKeyType), f1);
    }

    protected void ReadTempoFactor(string[] f) {
      P.frmStart.TempoFactor = float.Parse(f[1]);
      if (P.frmStart.TempoFactor == 0) P.frmStart.TempoFactor = 1;
      //P.frmStart.indTempoChanged = true;
      //P.frmStart.trkTempo.Value = int.Parse(f[1]);
      //P.frmStart.trkTempo_Scroll(null, null);
    }

    protected void ReadMutedTracks(string[] f) {
      for (int i = 1; i < f.Length; i++) {  //skip over keyword
        clsTrks.T trk = new clsTrks.T(P.F.Trks, int.Parse(f[i]) - 1);  //convert trk starting at 1 to starting at 0
        P.F.Mute[trk] = true;
      }
    }

    protected void ReadCtlr(string[] f, int[] array, int lo, int hi) {
      for (int i = 1; i < f.Length; i++) {  //skip over keyword
        int index = i - 1;
        if (index >= array.Length) break;
        int val = int.Parse(f[i]);
        if (val < lo || val > hi) val = -1;
        array[index] = val;
      }
    }

    protected void ReadCtlrTrkInt(string[] f, clsTrks.Array<int> array, int lo, int hi) {
      for (int i = 1; i < f.Length; i++) {  //skip over keyword
        int index = i - 1;
        if (index >= P.F.Trks.NumTrks) break;
        int val = int.Parse(f[i]);
        if (val < lo || val > hi) val = -1;
        clsTrks.T trk = new clsTrks.T(P.F.Trks, index);
        array[trk] = val;
      }
    }

    protected void ReadCtlrTrkBool(string[] f, clsTrks.Array<bool> array) {
      for (int i = 1; i < f.Length; i++) {  //skip over keyword
        int index = i - 1;
        if (index >= P.F.Trks.NumTrks) break;
        bool val = bool.Parse(f[i]);
        clsTrks.T trk = new clsTrks.T(P.F.Trks, index);
        array[trk] = val;
      }
    }

    private void ReadChans(string[] f) {
      ReadCtlrTrkInt(f, P.F.Chan, 0, 15);
    }

    private void ReadSelect(string[] f) {
      if (P.F.frmTrackMap == null) return;
      clsTrks.Array<bool> array = new clsTrks.Array<bool>(false);
      ReadCtlrTrkBool(f, array);
      P.F.frmTrackMap.SetSelectedTrks(array);
    }

    protected void ReadTranspose(string[] f) {  //defunct
      int transpose = int.Parse(f[1]);
      if (transpose.Mod12() != 0) MessageBox.Show("Warning: Obsolete Transpose line found: " + Lines[Lnum]);
    }

    protected List<string> ReadTSigs() {
      List<string> ret = new List<string>();
      for (int prevonticks = int.MinValue; Lnum < Lines.Count; Lnum++) {
        string[] ll;
        clsMTime.clsBBT bbt;
        string line = ReadBBTLine(prevonticks, out ll, out bbt);
        if (line == "") break;

        //* format: tsig nn/dd
        if (ll.Length == 3 && ll[1].ToLower() == "tsig") {
          string[] tsigtxt = ll[2].Split(new char[] { '/' });
          int nn = int.Parse(tsigtxt[0]);
          int dd = int.Parse(tsigtxt[1]);
          P.frmStart.TraceLoad("add tsig " + nn + "/" + dd + " bar " + bbt.Bar + " from CHPfile");
          //P.F.MTime.UpdateTSigsBar(nn, dd, bbt.Bar);
          P.F.MTime.AddTSig(nn, dd, bbt.Ticks, adj: false);
          ret.Add(line);
          continue;
        } else break;
      }
      if (P.F.MaxBBT != null) P.F.MaxBBT.InitBBT();  //no midifile - MaxBars in chp header
      return ret;
    }

    protected List<string> ReadKeys() {
      List<string> ret = new List<string>();
      for (int prevonticks = int.MinValue; Lnum < Lines.Count; Lnum++) {
        string[] ll;
        clsMTime.clsBBT bbt;
        string line = ReadBBTLine(prevonticks, out ll, out bbt);
        if (ll == null) return ret;

        //* format: key <note> major|minor
        if (ll.Length >= 4 && ll[1].ToLower() == "key") {
          P.frmStart.TraceLoad("add key to CFKeys at ticks " + bbt.Ticks);
          P.F._CFKeys.Add(ll[2], ll[3], bbt.Ticks);
          if (ll.Length == 5) {  //indtransposenamessharp (optional)
            P.F._CFKeys[bbt.Ticks].TransposeNamesSharp = bool.Parse(ll[4]);
          }
          ret.Add(line);
        } else break;
      }
      return ret;
    }

    protected void SetLastEv() {
      //* set default last offtime 
      if (Evs.Count == 0) return; 
      clsEv evlast = Evs[Evs.Count - 1];
      if (evlast.Notes.Length == 0) {  //null chord
        Evs.RemoveAt(Evs.Count - 1);  //don't need null chord at end
        if (Evs.Count > 1 && Evs[Evs.Count - 2].OffBBT == null) {
          LogicError.Throw(eLogicError.X010);
          Evs[Evs.Count - 2].OffBBT = Evs[Evs.Count - 2].OnBBT.Copy();
        }
      } else {
        if (evlast.OffBBT == null || evlast.OffTime <= 0) {  //not already set
          clsMTime.clsBBT onbbt = new clsMTime.clsBBT(evlast.OnTime);
          if (P.F.MaxBBT.Ticks < onbbt.Ticks) {
            LogicError.Throw(eLogicError.X011);  //after maxticks
            throw new FatalException();
          }
          else if (P.F.MaxBBT.Ticks > onbbt.Ticks) {  //set last offtime
            //clsMTime.clsBBT offbbt = new clsMTime.clsBBT(onbbt.Bar + 1, 0, 0);  //round up to bar
            //int endticks = Math.Min(P.F.MaxTicks, offbbt.Ticks);
            //Evs[Evs.Count - 1].OffBBT = new clsMTime.clsBBT(endticks);
            //Evs[Evs.Count - 1].OffBBT = new clsMTime.clsBBT(P.F.MaxBBT.Ticks);
            //evlast.OffBBT = onbbt.GetNextBar();  //this messes up DGV editor
            evlast.OffBBT = P.F.MaxBBT.BBTCopy;
            if (evlast.OffBBT.Ticks > P.F.MaxBBT.Ticks) evlast.OffBBT = P.F.MaxBBT.BBTCopy;
          } else {  //equals - remove ev with same on/off time
            Evs.RemoveAt(Evs.Count - 1);
          }
        }
      }
    }

    protected string ReadBBTLine(int prevonticks, out string[] ll, out clsMTime.clsBBT bbt) {
      string l;
      l = Lines[Lnum];
      ll = l.Split(Space, StringSplitOptions.RemoveEmptyEntries);  //on (len) chan 6 notes chord

      //read bar.beats
      int bars = int.MinValue;
      int beats = int.MinValue;
      int twelfths = 1;
      int ticks;   //0=ontime ; (1=lentime)

      string[] bb = ll[0].Split(Dot, 3);  //bar.beat[.twelfths] (on or len)
      if (!int.TryParse(bb[0], out bars)) {
        bbt = null;
        return "";
      }

      //bars = OffsetBars + int.Parse(bb[0]);
      bars = int.Parse(bb[0]);
      beats = int.Parse(bb[1]);
      if (bb.Length > 2) twelfths = int.Parse(bb[2]);
      bars--;
      beats--;
      twelfths--;
      bbt = new clsMTime.clsBBT(bars, beats, twelfths);
      if (P.F.MaxBBT != null && bbt.Bar >= P.F.MaxBBT.Bar) return "";  //not checked for tsigs, keys
      //bbt = MTime.GetBBT(bars, beats, twelfths);
      //ticks = Bars2Ticks(bars, beats, twelfths);
      ticks = bbt.Ticks;
      if (ticks < prevonticks) throw new ChordFileException();
      //if (ticks == prevonticks) Debugger.Break();  //tmp debugging
      prevonticks = ticks;
      return l;
    }

    internal void ChordPlaySwitch() {
      for (int i = 0; i < Evs.Count; i++) Evs[i].PlayChord = null;
    }

    internal string SaveTxtFile() {
      indSave = false;
      return Utils.SaveFile(P.F.Project.CHPPath, SaveFileSub);
    }

    private void SaveFileSub(StreamWriter xsw) {
      clsMTime mtime = P.F.MTime;

      //* write comments (all at start)
      foreach (string line in Lines_Comments) xsw.WriteLine(line);

      //* write header
      int wholenotes = P.F.MaxBBT.MidiWholeNotes;
      string f1 = (P.F.Project.MidiExists) ?
        P.F.MTime.TicksPerQNote.ToString() + ',' + wholenotes.ToString() :
        NoMidi_TPQN.ToString().ToString() + ',' + wholenotes.ToString();
      //P.F.MTime.TicksPerQNote.ToString() + ',' + (P.F.MaxBBT.Bar + 2) :
      //NoMidi_TPQN.ToString() + ',' + P.frmStart.nudMaxBarsNoMidiFile.Value.ToString();
      //P.F.MTime.TicksPerQNote.ToString() + ',' + (P.F.MaxBBT.Bar + 2) :
      //NoMidi_TPQN.ToString() + ',' + SongLength.ToString();
      if (P.F.frmChordMap != null) Description = P.F.frmChordMap.txtCHPDesc.Text;

      string headerkey = "X X "; 
      if (P.F._CFKeys != null) headerkey = P.F.Keys[0].KeyNoteStr + " " + P.F.Keys[0].Scale + " "; 
      xsw.WriteLine(mtime.TSigs[0].Txt + " " + f1 + " " + headerkey + Description);  

      //* write tsigs
      WriteBBTTSigs(xsw, mtime);

      //* write params
      xsw.WriteLine("MinorKeyType " + P.frmStart.MinorKeyType.ToString());
      xsw.WriteLine("SyncopationNN " + Syncopation.NN);
      xsw.WriteLine("SyncopationDD " + Syncopation.DD);
      xsw.WriteLine("TempoFactor " + P.frmStart.TempoFactor);
      if (P.frmSC.nudTransposeKB.Value != 0)
        xsw.WriteLine("TransKB " + (int)P.frmSC.nudTransposeKB.Value);
      if (P.frmSC.nudTransposeKBPitch.Value != 0)
        xsw.WriteLine("TransKBPitch " + (int)P.frmSC.nudTransposeKBPitch.Value);
      if (P.frmSC.nudTransposeStreamPitch.Value != 0)
        xsw.WriteLine("TransStream " + (int)P.frmSC.nudTransposeStreamPitch.Value);
      if (Forms.frmChordMap.StaticTransposeChordNamesVal != 0) {
        xsw.WriteLine("TransposeChordNamesVal " + Forms.frmChordMap.StaticTransposeChordNamesVal);
      }
      //xsw.WriteLine("BarPaneBarLo " + P.F.BarPaneBBTLo.Bar);
      //xsw.WriteLine("BarPaneBarHi " + P.F.BarPaneBBTHi.Bar);
      WriteCtlrLineIntArray(xsw, "Vol", P.F.Vol);
      WriteCtlrLineIntArray(xsw, "Pan", P.F.Pan);
      WriteCtlrLineIntArray(xsw, "Patch", P.F.Patch);
      WriteCtlrLineIntTrk(xsw, "Chan", P.F.Chan);
      WriteCtlrTrks(xsw, "MapTrk", P.frmSC.MapTrks);
      if (Forms.frmChordMap.StaticTransposeChordNamesVal != 0 && P.F._CFKeys[0].TransposeNamesSharp.HasValue) {
        xsw.WriteLine("FirstTransNamesSharp " + P.F._CFKeys[0].TransposeNamesSharp.Value);
      }
      //if (P.F.frmTrackMap != null) WriteCtlrLineBoolTrk(xsw, "Select", P.F.frmTrackMap.GetSelectedTrks());

      //* throw new ApplicationException("Test Error");

      //* write muted tracks param
      string txt = "";
      foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {
        if (P.F.Mute[trk]) txt += " " + trk.ToString();   //convert trk base 0 to base 1
      }
      if (txt != "") xsw.WriteLine("MutedTracks" + txt);

      //* write keys (if any)
      for (int i = 1; i < P.F.Keys.Keys.Count; i++) {  //start at second key
        clsKeyTicks key = P.F.Keys.Keys[i];
        clsMTime.clsBBT bbtkey = key.BBT;
        bbtkey = GetBBT(mtime, bbtkey);
        bbtkey.RoundToBar();
        if (bbtkey.Bar >= P.F.MaxBBT.Bar) break;
        xsw.Write(bbtkey.ToString() + " key " + key.KeyNoteStr + " " + key.Scale);
        if (Forms.frmChordMap.StaticTransposeChordNamesVal != 0 && key.TransposeNamesSharp.HasValue) {
          xsw.WriteLine(" " + key.TransposeNamesSharp.Value);
        } else {
          xsw.WriteLine("");
        }
      }

      //* write notes (if any)
      if (Evs.Count > 0) xsw.WriteLine("Start Chords");
      for (int i = 0; i < Evs.Count; i++) {
        WriteNoteLine(xsw, Evs[i], mtime);
      }

      //* write last null if required (to set offbbt of last chord)
      if (Evs.Count > 0 && Evs[Evs.Count - 1].Notes.Length > 0) {  //not null
        clsMTime.clsBBT lastoffbbt = Evs[Evs.Count - 1].OffBBT;
        if (lastoffbbt.Bar < P.F.MaxBBT.Bar - 2) {
          clsEv evnull = new clsEvPC(lastoffbbt.Copy(), lastoffbbt.GetNextBar());  //null EvPC one bar long
          WriteNoteLine(xsw, evnull, mtime);
        }
      }
      //if (Evs.Count > 0) {
      //  clsMTime.clsBBT endbbt = Evs[Evs.Count - 1].OffBBT;
      //  endbbt = GetBBT(mtime, endbbt);
      //  xsw.WriteLine(endbbt.ToString() + " end");
      //}
    }

    protected static clsMTime.clsBBT GetBBT(clsMTime mtime, clsMTime.clsBBT bbt) {
      if (mtime != P.F.MTime) {
        bbt = new clsMTime.clsBBT(mtime, bbt.Ticks);
      }
      return bbt;
    }

    internal static void WriteBBTTSigs(StreamWriter xsw, clsMTime mtime) {
      for (int i = 1; i < mtime.TSigs.Length; i++) {  //start at 2nd index
        if (mtime.TSigs[i].Tick >= P.F.MaxBBT.Ticks) break;
        xsw.Write(new clsMTime.clsBBT(mtime, mtime.TSigs[i].Tick).ToString());
        xsw.WriteLine(" tsig " + mtime.TSigs[i].Txt);
      }
    }

    protected void WriteNoteLine(StreamWriter xsw, clsEv ev, clsMTime mtime) {
      clsMTime.clsBBT onbbt = (mtime == P.F.MTime) ? ev.OnBBT : new clsMTime.clsBBT(mtime, ev.OnBBT.Ticks); 
      string line = onbbt.ToString() + " ";
      bool[] chord = new bool[12];
      List<int> chnotes = new List<int>();
      foreach (clsEv.clsNote n in ev.Notes) {
        chord[n.PC[eKBTrans.None]] = true;
        chnotes.Add(n.PC[eKBTrans.None]);
      }
      char delim = '.';
      line = GetChordLine(P.F.Keys, chord, line, null, delim, ev, chnotes);
      xsw.WriteLine(line);
    }

    internal static string GetChordLine(clsKeysTicks keys, bool[] notes, string line, int? bass,
      char delim, clsEv ev, List<int> chnotes) {
      if ( ev.Notes.Length == 0) {
        line += "null";
        //if (delim == ':') line += " " + delim;
      } else {
        line = line.PadRight(8);
        clsLoadCSV.clsChord chord = new clsLoadCSV.clsChord();
        string chordqual = ev.ChordQualifier;
        if (chordqual == "") chordqual = chord.CreateChList(notes);
        if (chnotes == null) chnotes = chord.ChList;
        foreach (int note in chnotes) line += " " + NoteName.GetNoteName(keys[0], note);
        if (chordqual != "***") {  //valid chord
          line = line.PadRight(24);
          line += " " + delim + NoteName.GetNoteName(keys[0], chnotes[0]).Trim() + chordqual;
        } else {  //no chord qualifier
          if (delim == ':') {
            line = line.PadRight(24);
            line += " " + delim;
          }
        }
      }
      return line;
    }

    protected void WriteCtlrLineIntArray(StreamWriter xsw, string desc, int[] array) {
      int lastpos = -1;
      for (int i = 0; i < array.Length; i++) if (array[i] >= 0) lastpos = i;
      if (lastpos < 0) return;
      xsw.Write(desc);
      for (int i = 0; i <= lastpos; i++) xsw.Write(" " + array[i]);
      xsw.WriteLine("");
    }

    protected void WriteCtlrLineIntTrk(StreamWriter xsw, string desc, clsTrks.Array<int> array) {
      clsTrks.T lastpos = null;
      foreach (clsTrks.T trk in P.F.Trks.Ints.Next) if (array[trk] >= 0) lastpos = trk;
      if (lastpos == null) return;
      xsw.Write(desc);
      foreach (clsTrks.T trk in P.F.Trks.Ints.Next) xsw.Write(" " + array[trk]);
      xsw.WriteLine("");
    }

    protected void WriteCtlrTrks(StreamWriter xsw, string desc, List<clsTrks.T> list) {
      if (list == null || list.Count == 0) return;
      xsw.Write(desc);
      foreach (clsTrks.T trk in list) {
        xsw.Write(" " + trk.TrkNum);
      }
      xsw.WriteLine("");
    }

    protected void WriteCtlrLineBoolTrk(StreamWriter xsw, string desc, clsTrks.Array<bool> array) {
      bool? prevtrk = null;
      bool indwrite = false;
      foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {
        if (prevtrk.HasValue && prevtrk.Value != array[trk]) {
          indwrite = true;
          break;
        }
        prevtrk = array[trk];
      }
      if (!indwrite) return;  //all true or all false
      xsw.Write(desc);
      foreach (clsTrks.T trk in P.F.Trks.Ints.Next) xsw.Write(" " + array[trk]);
      xsw.WriteLine("");
    }

    internal void SyncEvsToKeys() {
      CheckOnOffTimes();
      RemoveDupEvs();
      CheckOnOffTimes();

      if (P.F.Keys.Keys.Count >= 2) {  //one key must be at start
        int keyindex = 1;
        clsKeyTicks key = P.F.Keys.Keys[1];  //2nd key
        for (int i = 0; i < Evs.Count; i++) {
          clsCFPC.clsEv ev = Evs[i];
          if (ev.OffBBT.Ticks < key.BBT.Ticks) continue;  //not yet reached next key
          if (ev.OnBBT.Ticks < key.BBT.Ticks && ev.OffBBT.Ticks > key.BBT.Ticks) SplitEv(i++, key.BBT);
          i--;  //re-examine for next key, unless split
          if (++keyindex >= P.F.Keys.Keys.Count) break;
          key = P.F.Keys.Keys[keyindex];  //next key  
        }
      }

      CheckOnOffTimes();
      MergeDemergeNotes(-1);
      CheckNoteOnOffTimes();
    }

    protected void CheckOnOffTimes() {
#if DEBUG
      clsCFPC.clsEv prevev = null;
      for (int i = 0; i < Evs.Count; i++) {
        clsCFPC.clsEv ev = Evs[i];
        if (ev.OnTime > ev.OffTime) LogicError.Throw(eLogicError.X999);  //throw new ChordFileException();  //exception thrown
        if (i > 0) {
          if (ev.OnTime != prevev.OffTime) LogicError.Throw(eLogicError.X999);  //throw new ChordFileException();
          if (ev.OnTime < prevev.OnTime) LogicError.Throw(eLogicError.X999);  //throw new ChordFileException();
        }
        prevev = ev;
      }
#endif
    }

    protected void CheckNoteOnOffTimes() {
#if DEBUG
      for (int i = 0; i < Evs.Count; i++) {
        clsCFPC.clsEv ev = Evs[i];
        for (int j = 0; j < ev.Notes.Length; j++) {
          clsEv.clsNote note = ev.Notes[j];
          if (note.OnBBT == null) throw new ChordFileException();
          if (note.OffBBT == null) throw new ChordFileException();
          if (note.OnBBT.Ticks > ev.OnBBT.Ticks) throw new ChordFileException();
          if (note.OffBBT.Ticks < ev.OffBBT.Ticks) throw new ChordFileException();
          //if (note.OnBBT.Ticks >= note.OffBBT.Ticks) throw new ChordFileException();
        }
      }
#endif
    }


    internal void MergeDemergeNotes(int index) {
      //* index: Evs[index]
      //* -1: all evs
      //* this changes note on/off times (not chord on/off times)
      if (Evs.Count == 0) return;
      if (Evs.Count == 1) {
        for (int i = 0; i < Evs[0].Notes.Length; i++) {
          Evs[0].Notes[i].OnBBT = Evs[0].OnBBT;
          Evs[0].Notes[i].OffBBT = Evs[0].OffBBT;
        }
        return;
      }

      int hi, lo;
      if (index < 0) {
        lo = 0;
        hi = Evs.Count - 1;
      } else {
        lo = index;
        hi = index;
      }

      if (Forms.frmSC.CapitalizeRootsStatic) {  //demerge
        //* treat each note of chord as separate event
        //* called when capitalize roots set on
        for (int i = lo; i <= hi; i++) {
          clsEv ev = Evs[i];
          for (int j = 0; j < ev.Notes.Length; j++) {
            ev.Notes[j].OnBBT = ev.OnBBT;
            ev.Notes[j].OffBBT = ev.OffBBT;
          }
        }
        return;
      }

      //* calculate ev[].NoteOn/NoteOff times 
      //* (start/end of note, allowing for same note on contiguous evs)

      //* set sNote.OnBBT
      {
        //clsEv prevev = Evs[0];
        //for (int j = 0; j < prevev.Notes.Length; j++) prevev.Notes[j].OnBBT = Evs[0].OnBBT;
        for (int i = lo; i <= hi; i++) {
          clsEv prevev;
          if (i == 0) {
            prevev = Evs[0];
            for (int j = 0; j < prevev.Notes.Length; j++) prevev.Notes[j].OnBBT = Evs[0].OnBBT;
          } else {
            prevev = Evs[i - 1];
          }
          clsEv ev = Evs[i];
          if (i > 0 && ev.OnTime != prevev.OffTime) throw new ChordFileException();  //continue;  //not contiguous with previous ev (should not happen)
          for (int j = 0; j < ev.Notes.Length; j++) {
            clsEv.clsNote prevnote = prevev.SamePitch(ev.Notes[j]);
            ev.Notes[j].OnBBT = ev.OnBBT;
            if (prevnote != null && ev.OnBBT.Ticks != P.F.Keys[ev.OnBBT.Ticks].Ticks) {
              ev.Notes[j].OnBBT = prevnote.OnBBT;
            }
          }
          //prevev = ev;
        }
      }

      //* set sNote.OffBBT
      {
        //clsEv nextev = Evs[Evs.Count - 1];
        //for (int j = 0; j < nextev.Notes.Length; j++) nextev.Notes[j].OffBBT = Evs[Evs.Count - 1].OffBBT;
        for (int i = hi; i >= lo; i--) {
          clsEv nextev;
          if (i == Evs.Count - 1) {
            nextev = Evs[Evs.Count - 1];
            for (int j = 0; j < nextev.Notes.Length; j++) nextev.Notes[j].OffBBT = Evs[Evs.Count - 1].OffBBT;
          } else {
            nextev = Evs[i + 1];
          }
          clsEv ev = Evs[i];
          if (i < Evs.Count - 1 && ev.OffTime != nextev.OnTime) throw new ChordFileException(); //continue;  //not contiguous with previous ev (should not happen)
          for (int j = 0; j < ev.Notes.Length; j++) {
            if (ev.Notes[j].OnBBT.Ticks > ev.OnBBT.Ticks) throw new ChordFileException();  //tmp debugging
            clsEv.clsNote nextnote = nextev.SamePitch(ev.Notes[j]);
            ev.Notes[j].OffBBT = ev.OffBBT;
            if (nextnote != null && ev.OffBBT.Ticks != P.F.Keys[ev.OffBBT.Ticks].Ticks) {
              ev.Notes[j].OffBBT = nextnote.OffBBT;
            }
          }
          //nextev = ev;
        }
      }
    }

    protected void CheckIt() {
#if DEBUG
      for (int i = 0; i < Evs.Count; i++) {
        clsEv ev = Evs[i];
        for (int j = 0; j < ev.Notes.Length; j++) {
          if (ev.Notes[j].OnBBT.Ticks > ev.OnBBT.Ticks) throw new ChordFileException();
        }
      }
#endif
    }

    protected void CheckOnBBT(int i) {
#if DEBUG
      clsEv ev = Evs[i];
      for (int j = 0; j < ev.Notes.Length; j++) {
        if (ev.Notes[j].OnBBT.Ticks > ev.OnBBT.Ticks) throw new ChordFileException();
      }
#endif
    }

    protected void RemoveDupEvs() {
      for (int i = 0; i < Evs.Count - 1; i++) {
        if (Evs[i].Equiv(Evs[i + 1], false)) {
          //if ((AltEvs[i] == null && AltEvs[i + 1] == null) || (AltEvs[i] != null && AltEvs[i].Equiv(AltEvs[i + 1]))) {
          Evs[i].OffBBT = Evs[i + 1].OffBBT;
          Evs.RemoveAt(i + 1);
          //}
        }
      }
    }

    protected void SplitEv(int index, clsMTime.clsBBT bbt) {
      //* split ev into 2 at bbt in Evs
      clsEv ev = Evs[index];
      //clsEv newev = new clsEv(ev, bbt, ev.OffBBT);
      clsEv newev = ev.New(bbt, ev.OffBBT);
      ev.OffBBT = bbt;
      Evs.Insert(++index, newev);
    }

    protected void InsertEv(clsEv ev) {
      MessageBox.Show("InsertEv: not yet used or tested!");
      int index = Evs.BinarySearch(new clsEvPC(ev.OnBBT));
      if (index < 0) index = ~index;  //next largest
      int i;
      for (i = index; i < Evs.Count; i++) {
        if (Evs[i].OffBBT.Ticks > ev.OffBBT.Ticks) break;
        Evs.RemoveAt(i--);
      }
      if (++i < Evs.Count) Evs[i].OnBBT = ev.OffBBT.Copy();
      Evs.Insert(index, ev);
    }

  }
}