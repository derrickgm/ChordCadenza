using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

/*
 * notemap/ev <-> beatch <-> beatchrun <-> datagrid 
 * initial: 
 *   notemap/ev(all) -> beatch(all) -> beatchrun(all) -> datagrid(all) 
 * change notemap: 
 *   notemap/ev(qis) -> beatch(beat(s)) -> beatchrun(all) -> datagrid(beatchrun/oldbeatchhrun diffs) 
 * change datagrid:
 *   notemap/ev(qis for beatch/oldbeatch diffs) <- beatch(all) <- beatchrun(one beat) <- datagrid(one beat) 
 * entries
 *   (space) - same as prev beat
 *   null
 *   chord
 *   *** -> unrecognisable chord or multiple chords
 *   *** <- as chord/null, but no change to notemap
 * optional null insert
 *   insert null x bars after new chord (if no other chords present after new chord)
*/

namespace ExtensionMethods {
  public static class Extensions {
    public static void DoubleBuffered(this DataGridView dgv, bool setting) {
      Type dgvType = dgv.GetType();
      PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
            BindingFlags.Instance | BindingFlags.NonPublic);
      pi.SetValue(dgv, setting, null);
    }
  }
}

namespace ChordCadenza {
  internal class clsBeatChords {
    private class clsChord {
      internal string Txt;  //root and qual
      private int Offset;  //to qualifier

      internal string Root {
        get {
          if (Offset == 0) return null;
          return Txt.Substring(0, Offset);
        }
      }

      internal string Qualifier {
        get {
          if (Offset == 0) return null;
          return Txt.Substring(Offset);
        }
      }

      internal clsChord(string txt, int offset) {
        Txt = txt;
        Offset = offset;
      }

      internal static clsChord Validate(string txt) {
        if (string.IsNullOrWhiteSpace(txt)) return new clsChord("", 0);
        if (txt == "***" || txt == "null") return new clsChord(txt, 0);
        string qualifier;
        int pc = NoteName.GetPitchAndQualifier(txt, out qualifier);
        if (pc < 0) return null;  //invalid chord
        //string root = NoteName.ToSharpFlat(NoteName._Names[P.F.Keys[0].MidiKey + 7][pc]).TrimEnd();
        string root = NoteName.ToSharpFlat(NoteName.GetName(P.F.Keys[0], pc).TrimEnd());
        return new clsChord(root + qualifier, root.Length);
      }

      internal bool IsEquiv(clsChord chord) {
        if (Txt == chord.Txt && Offset == chord.Offset) return true;
        return false;
      }

      internal clsChord Copy() {
        return new clsChord(Txt, Offset);
      }

      public override string ToString() {  //for easier debugging
        return Txt;
      }
    }

    //internal clsBeatChords(Forms.frmNoteMap frm, DataGridView dgv) {
    internal DataGridViewCellValidatingEventHandler DGV_CellValidating_Handler;
    internal clsBeatChords(Forms.IFrmDGV frm, DataGridView dgv) {
      DGV = dgv;
      Frm = frm;
      DGV_CellValidating_Handler = new DataGridViewCellValidatingEventHandler(DGV_CellValidating);
      DGV.CellValidating += DGV_CellValidating_Handler;
      //DGV.CellValidating += new DataGridViewCellValidatingEventHandler(DGV_CellValidating);
    }

    private DataGridView DGV;
    //private Forms.frmNoteMap Frm;
    private Forms.IFrmDGV Frm;
    private clsChord[,] ChordsRaw;  //[bar, beat]
    private clsChord[,] ChordsRun;  //[bar, beat]
    internal clsMTime.clsBBT[] BarToBBT;  //[bar]
    private clsMTime.clsBBT[] BeatToBBT;  //[beat]
    private int NumBars;
    private int NumBeats;
    private int MaxNN;

    internal void Init() {
      //Stopwatch stopwatch = new Stopwatch();
      //stopwatch.Start();

      //clsMTime.clsBBT bbtmax = new clsMTime.clsBBT(P.F.MaxTicks);
      //bbtmax.RoundUpToBar();  //???
      //NumBars = bbtmax.Bar + 1;
      //bbtmax = new clsMTime.clsBBT(NumBars, 0, 0);
      //NumBeats = bbtmax.Beats;
      NumBars = P.F.MaxBBT.Bar;  //not + 1 ???
      //NumBeats = new clsMTime.clsBBT(NumBars, 0, 0).Beats;
      NumBeats = P.F.MaxBBT.Beats;  //not + 1 ???

      BarToBBT = new clsMTime.clsBBT[NumBars + 1];
      BeatToBBT = new clsMTime.clsBBT[NumBeats + 1];
      for (int bar = 0; bar <= NumBars; bar++) {  //end at one after numbars
        BarToBBT[bar] = new clsMTime.clsBBT(bar, 0, 0);
        MaxNN = Math.Max(MaxNN, BarToBBT[bar].TSig.NN);
      }
      for (int beat = 0; beat <= NumBeats; beat++) {  //end at one after numbeats
        BeatToBBT[beat] = new clsMTime.clsBBT(beat, true);
      }
      ChordsRaw = new clsChord[NumBars + 1, MaxNN];
      ChordsRun = new clsChord[NumBars + 1, MaxNN];

      //DoubleBuffered(true);
      while (DGV.Rows.Count > 0) DGV.Rows.RemoveAt(0);
      DGV.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular);
      //DGV.Width = Frm.panNoteMap.Width;
      DGV.Height = ((int)DGV.DefaultCellStyle.Font.GetHeight() + 10) * MaxNN + 10;
      DGV.ColumnCount = NumBars;
      SetColumnWidth();
      //DGV.Rows.Clear();
      for (int beat = 0; beat < MaxNN; beat++) {
        DataGridViewRow row = new DataGridViewRow();
        for (int bar = 0; bar < NumBars; bar++) {
          DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
          row.Cells.Add(cell);
          if (Frm is Forms.frmTrackMap) cell.ReadOnly = true;
          if (beat >= BarToBBT[bar].TSig.NN) {
            cell.ReadOnly = true;
            cell.Style.BackColor = Color.LightGray;
          }
        }
        DGV.Rows.Add(row);
      }

      ShowDGVCells();  //46ms!!! for 100 loops
      //Debug.WriteLine("init datagridview chords millisecs = " + stopwatch.ElapsedMilliseconds);
    }

    internal void SetColumnWidth() {
      int pixlo = 0, pixhi = 0;
      for (int bar = 0; bar < NumBars; bar++) {
        DataGridViewColumn col = DGV.Columns[bar];
        pixhi = Frm.TicksToPix(BarToBBT[bar + 1].Ticks);
        col.Width = pixhi - pixlo;
        pixlo = pixhi;
      }
    }

    private void PopulateDGV(int beatfrom, int beatto) {
      for (int b = beatfrom; b < beatto; b++) {
        clsMTime.clsBBT bbt = BeatToBBT[b];
        int bar = bbt.Bar;
        int beat = bbt.BeatsRemBar;
        DataGridViewCell cell = DGV.Rows[beat].Cells[bar];
        cell.Value = ChordsRun[bar, beat];
      }
    }

    internal void ShowDGVCells() {
      ShowDGVCells(0, NumBeats - 1);
    }

    private void ShowDGVCells(int beatfrom, int beatto) {
      EvsToChordsRaw(beatfrom, beatto); 
      ChordsRawToRun();
      PopulateDGV(beatfrom, beatto);
    }

    private clsChord[,] CopyChords(clsChord[,] chords) {
      //* create copy of ChordsRun or ChordsRaw
      clsChord[,] ret = new clsChord[chords.GetLength(0), chords.GetLength(1)];
      for (int i = 0; i < chords.GetLength(0); i++) {
        for (int j = 0; j < chords.GetLength(1); j++) {
          if (chords[i, j] == null) continue;  //bar with less than MaxNN beats
          ret[i, j] = chords[i, j].Copy();
        }
      }
      return ret;
    }

    private void DGV_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
      if (MidiPlay.Sync.indPlayActive != clsSync.ePlay.None) {
        //e.Cancel = true;
        return;
      }
      string txt = ((string)e.FormattedValue).ToLower();
      //Debug.WriteLine("DGV_CellValidating [" + e.ColumnIndex + ", " + e.RowIndex + "] - " + txt);
      DataGridViewCell cell = DGV.Rows[e.RowIndex].Cells[e.ColumnIndex];
      clsChord chord = clsChord.Validate(txt);
      if (cell.ReadOnly) {  //greater than tsig.nn for this bar (greyed out)
        //e.Cancel = true;
      } else if (chord == null) {  //invalid chord
        //cell.ErrorText = "@@@" + txt;
        e.Cancel = true;
      } else if (!chord.IsEquiv(ChordsRun[e.ColumnIndex, e.RowIndex])) {  //different chord
        //if (String.IsNullOrWhiteSpace(chord.Txt) && P.F.CF.QSections.FirstBeatName(bbt) != '*') {
        //* what are the next 3 lines doing??? 
        //*if (String.IsNullOrWhiteSpace(chord.Txt) && e.ColumnIndex == 0 && e.RowIndex == 0) {
        //*  chord = ChordsRaw[e.ColumnIndex, e.RowIndex];  //replace "" with raw chord  
        //*}
        ChordsRun[e.ColumnIndex, e.RowIndex] = chord;
        clsChord[,] chordsrawcopy = CopyChords(ChordsRaw);
        ChordsRunToRaw();
        ChordsRawToNoteMap(chordsrawcopy);
        cell.Value = chord.Txt;
        //CopySimBars(chordsrawcopy);
        Frm.SetNoteMapFileChanged(undoredo: true, indqi: true);  //no copysimbars - already done
      }
      DGV.RefreshEdit();
    }

    //private void CopySimBars(clsChord[,] chordsrawcopy) {
    //  //* copy simbars
    //  if (P.F.frmNoteMapAdv != null && !P.F.frmNoteMapAdv.chkSectionsCopy.Checked || P.F.CF.QSections.Count == 0) return;
    //  for (int bar = 0; bar < NumBars; bar++) {
    //    int nn = BarToBBT[bar].TSig.NN;
    //    //for (int beat = 0; beat < nn; beat++) {
    //    //  if (!ChordsRaw[bar, beat].IsEquiv(chordsrawcopy[bar, beat])) {
    //    //    Frm.CopySimBars(bar, bar);  //copy whole bar if any beat is different
    //    //    break;
    //    //  }
    //    //}
    //    for (int beat = 0; beat < nn; beat++) {
    //      if (!ChordsRaw[bar, beat].IsEquiv(chordsrawcopy[bar, beat])) {
    //        clsMTime.clsBBT bbt = new clsMTime.clsBBT(bar, beat, 0);
    //        int qilo = bbt.Ticks / P.F.TicksPerQI;
    //        bbt.NextBeat();
    //        int qihi = bbt.Ticks / P.F.TicksPerQI;
    //        P.F.CF.QSections.CopyNoteMap(qilo, qihi);
    //      }
    //    }
    //  }
    //}

    //* raw -> run: set first beat of section or gap to current chord
    //*             
    //* run -> raw: no propagation over section boundary

    //private void CheckSection(DataGridViewCellValidatingEventArgs e, clsChord chord) {
    //  //* stop new chord from going over a section boundary
    //  if (chord.Txt != "null") {
    //    int sectchangebar = P.F.CF.Sections.GetNextChange(e.ColumnIndex);
    //    if (sectchangebar < NumBars) {
    //      for (int bar = e.ColumnIndex; bar < NumBars; bar++) {
    //        clsMTime.clsBBT bbtbar = new clsMTime.clsBBT(bar, 0, 0);
    //        for (int beat = 0; beat < bbtbar.TSig.NN; beat++) {
    //          if (!String.IsNullOrWhiteSpace(ChordsRun[bar, beat].Txt)) break;
    //          if (bar == sectchangebar) {
    //            ChordsRun[bar, beat] = ChordsRaw[e.ColumnIndex, e.RowIndex].Copy();
    //            break;
    //          }
    //        }
    //      }
    //    }
    //  }
    //}

    //private void DGV_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
    //  Debug.WriteLine("DGV_CellEndEdit");
    //  // Clear the row error in case the user presses ESC.   
    //  DataGridViewCell cell = DGV.Rows[e.RowIndex].Cells[e.ColumnIndex];
    //  //DGV.Rows[e.RowIndex].ErrorText = String.Empty;
    //  cell.ErrorText = String.Empty;
    //}

    private void EvsToChordsRaw(int beatfrom, int beatto) {
      //* create ChordRun[bar, beat] from P.F.CF.Evs
      clsMTime.clsBBT bbtlo = new clsMTime.clsBBT(beatfrom, true);
      //clsMTime.clsBBT bbthi = new clsMTime.clsBBT(beatsto + 1, true);
      int firstevi = 0;
      if (beatfrom > 0) {
        firstevi = P.F.CF.Evs.BinarySearch(new clsCFPC.clsEvPC(bbtlo));
        if (firstevi < 0) firstevi = Math.Max(0, ~firstevi - 1);  //index before bbt
      }
      clsCFPC.clsEv ev;
      for (int i = firstevi; i <= P.F.CF.Evs.Count; i++) {
        if (i == P.F.CF.Evs.Count) {  //gap at end - pad with null ev
          if (P.F.CF.Evs.Count == 0) {
            ev = new clsCFPC.clsEvPC(P.F.CF, new clsMTime.clsBBT(0), new int[0]);  //pad after lastev
          } else {
            clsCFPC.clsEv evlast = P.F.CF.Evs[i - 1];
            if (evlast.OffTime >= P.F.MaxBBT.Ticks) break;
            ev = new clsCFPC.clsEvPC(P.F.CF, evlast.OffBBT, new int[0]);  //pad after lastev
          }
          //ev.OffBBT = new clsMTime.clsBBT(P.F.MaxTicks);
          ev.OffBBT = P.F.MaxBBT.BBTCopy;
        } else {
          ev = P.F.CF.Evs[i];
        }
        //* look at ALL beats (lines) contained in the ev
        if (ev.OnTime >= new clsMTime.clsBBT(beatto, true).Ticks) break;
        for (int beat = ev.OnBBT.Beats; beat < ev.OffBBT.Beats; beat++) {
          if (beat < beatfrom) continue;  //before start of beats range
          if (beat > beatto) break;  //after end of beats range
          clsMTime.clsBBT bbt = new clsMTime.clsBBT(beat, true);
          int ticks = bbt.Ticks;
          int nextticks = new clsMTime.clsBBT(beat + 1, true).Ticks;
          if (ticks >= ev.OnBBT.Ticks && nextticks <= ev.OffBBT.Ticks) {  //ev fills beat
            ChordsRaw[bbt.Bar, bbt.BeatsRemBar] = EvToTxt(ev);
          } else {
            ChordsRaw[bbt.Bar, bbt.BeatsRemBar] = new clsChord("***", 0);
          }
        }
      }

      //* set initial, end (or all) empty ChordsRaw to null
      for (int beat = 0; beat < NumBeats; beat++) {
        //if (beat == 270) Debugger.Break();  //temp
        clsMTime.clsBBT bbt = BeatToBBT[beat];
        if (ChordsRaw[bbt.Bar, bbt.BeatsRemBar] != null) continue;
        ChordsRaw[bbt.Bar, bbt.BeatsRemBar] = new clsChord("null", 0);
      }
    }

    private clsChord EvToTxt(clsCFPC.clsEv ev) {
      //* get chord txt from ev ("null", "***", chord)
      clsChord chord;
      if (ev.Notes.Length == 0) chord = new clsChord("null", 0);  //null chord
      else if (!ev.Root) {
        chord = new clsChord("***", 0);  //non-recognisable chord
      } else {
        clsKeyTicks keyticks = P.F.Keys[ev.OnTime];
        //int midikey = P.F.Keys[ev.OnTime].MidiKey;  //current key
        if (Frm.TransposeChordNamesVal != 0) {
          keyticks = keyticks.GetTransposeNames(Frm.TransposeChordNamesVal);
          //midikey = keyticks.MidiKey;
        }
        int rootpitch = Frm.ChordTranspose(ev.Notes[0].PC[eKBTrans.None]);
        string root = NoteName.ToSharpFlat(NoteName.GetNoteName(keyticks, rootpitch));
        root = root.Trim();
        chord = new clsChord(root + ev.ChordQualifier, root.Length);
      }
      return chord;
    }

    private void ChordsRawToRun() {
      //* convert ChordsRaw to ChordsRun
      string runningtxt = "#"; 
      for (int b = 0; b < BeatToBBT[NumBeats].Beats; b++) {  //BarToBBT includes bar after last (Numbars+1)
        clsMTime.clsBBT bbt = BeatToBBT[b];
        int bar = bbt.Bar;
        int beat = bbt.BeatsRemBar;
        clsChord chord = ChordsRaw[bar, beat]; 
        string txt = chord.Txt;
        if (txt == null || txt == "") {
          LogicError.Throw(eLogicError.X006, "txt = " + txt);  //invalid beat (> NN) or not raw
          ChordsRun[bar, beat] = new clsChord("", 0);
        }
        //if (txt != runningtxt || (beat == 0 && P.F.CF.Sections.Boundaries[bar])) {
        //if (txt != runningtxt || P.F.CF.QSections.FirstBeatName(bbt) != '*') {
        if (txt != runningtxt || (bar == 0 && beat == 0)) {
            ChordsRun[bar, beat] = chord.Copy();
        } else {
          ChordsRun[bar, beat] = new clsChord("", 0);
        }
        runningtxt = txt;
      }
    }

    private void ChordsRunToRaw() {
      //* convert ChordsRun to ChordsRaw
      clsChord runningchord = new clsChord("null", 0);
      for (int b = 0; b < NumBeats; b++) {
        clsMTime.clsBBT bbt = BeatToBBT[b];
        int bar = bbt.Bar;
        int beat = bbt.BeatsRemBar;
        clsChord chord = ChordsRun[bar, beat];
        if (chord != null && !string.IsNullOrWhiteSpace(chord.Txt)) runningchord = chord;
        ChordsRaw[bar, beat] = runningchord.Copy();
      }
    }

    private void ChordsRawToNoteMap(clsChord[,] copy) {
      //* update notemap with chord changes 
      for (int b = 0; b < NumBeats; b++) {
        clsMTime.clsBBT bbt = BeatToBBT[b];
        int bar = bbt.Bar;
        int beat = bbt.BeatsRemBar;
        clsChord chord = ChordsRaw[bar, beat];
        if (chord.Txt == "***") continue;
        if (copy[bar, beat].IsEquiv(chord)) continue;
        string txt = chord.Txt;

        //* transpose if necessary
        //int diff = (Frm.ChordTransposeNamesVal - Frm.ChordTransposeNotesVal).Mod12();
        int diff = Frm.TransposeChordNamesVal;
        if (diff != 0 && txt != "null") {
          string qualifier;
          int pc = NoteName.GetPitchAndQualifier(chord.Txt, out qualifier);
          if (pc < 0) {
            LogicError.Throw(eLogicError.X007, "pc = " + pc);
            pc = 0;
          }
          pc = Frm.ChordTransposeReverse(pc);
          txt = NoteName.PitchToKeyStr(pc, P.F.Keys[0].Scale).TrimEnd() + qualifier;
        }

        int qilo = bbt.Ticks / P.F.TicksPerQI;
        int qihi = new clsMTime.clsBBT(b + 1, true).Ticks / P.F.TicksPerQI;
        if (qihi > P.F.MaxBBT.QI) break;  //???
        string dottxt = (chord.Txt == "null") ? "null" : "." + txt;
        if (!P.F.CF.NoteMap.PropagateNoteMapBeat(bbt, dottxt, qilo, qihi)) {
          LogicError.Throw(eLogicError.X008);
          P.F.CF.NoteMap.PropagateNoteMapBeat(bbt, "null", qilo, qihi);
        }
      }

      //Frm.picNoteMapFile.Refresh();
      Frm.RefreshpicNoteMapFile();
    }
  }
}