using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Numerics;

namespace ChordCadenza {
  internal enum eAlign { None, Beat, HalfBar, Bar, Interval, Auto };

  internal class clsChordInfo {
    internal eAlign Align;
    internal ChordAnalysis.clsScore Score;
    internal clsMTime.clsSegBarBeat Segment;
    internal string ChordName;
    internal List<int> ChNotes;

    internal clsChordInfo(eAlign align) {
      if (Align == eAlign.Auto) LogicError.Throw(eLogicError.X084);
      Align = align;
    }

    internal bool Equiv(clsChordInfo ci) {
      if (ci == null) return false;
      if (ChNotes == null) return (ci.ChNotes == null);
      if (ci.ChNotes == null) return (ChNotes == null);
      return (ChordName == ci.ChordName && ChNotes.SequenceEqual(ci.ChNotes));
    }

    internal string ScoreToString() {
      if (Score == null) return "null";
      return Score.Score.ToString();
    }

    internal int? NullableScore {
      get {
        if (Score == null) return null;
        return Score.Score;
      }
    }
  }

  internal class clsChordSegs {
    internal clsChordSegs(clsFileStream filestream, Forms.frmChordMap frmnotemap, eAlign align, bool setatts) {
      if (align == eAlign.None) return;
      FS = filestream;
      frmNoteMap = frmnotemap;
      Align = align;
      ApplyChords(setatts);
    }

    internal void GetChords(bool chordatts,  bool indselectalign, out int bars, out int halfbars, out int beats) {
      //* create chords using Align.Auto, and return counts
      //* notemap not updated
      bars = 0; halfbars = 0; beats = 0;
      if (ListCIArr == null || ListCIArr.Length < 3) {
        GetScores();
        SelectAlign();
      } else {
        if (indselectalign) SelectAlign();
      }
      foreach (clsChordInfo ci in ListCI) {
        if (ci.Align == eAlign.Bar) bars++;
        else if (ci.Align == eAlign.HalfBar) halfbars++;
        else if (ci.Align == eAlign.Beat) beats++;
        else LogicError.Throw(eLogicError.X085);
      }
      if (chordatts) SetChordAtts();
    }

    internal int ChordCountBar { get { return GetChordCountAll(ListCIBar); } }
    internal int ChordCountHalfBar { get { return GetChordCountAll(ListCIHalfBar); } }
    internal int ChordCountBeat { get { return GetChordCountAll(ListCIBeat); } }
    private bool DebugChords { get { return P.frmCfgChords.chkDebug.Checked; } }
    private clsFileStream FS;
    private Forms.frmChordMap frmNoteMap;

    internal int GetChordCountAll(List<clsChordInfo> listci) {
      int count = 0;
      foreach (clsChordInfo ci in listci) {
        if (ci.NullableScore.HasValue) count++;
      }
      return count;
      //return listci.Count;
      //* get total chord count for Align.Auto invoked with bar, halfbar, or beat option 
      /*
      clsChordInfo ciprev = null;
      int count = 0;
      foreach (clsChordInfo ci in listci) {
        if (ciprev == null) count++;
        if (!ci.Equiv(ciprev)) count++; 
        ciprev = ci;
      }
      return count;
      */
    }

    internal int GetChordCountSeg(List<clsChordInfo> listci, eAlign align) {
      //* get chord counts for a segment type (bar/halfbar/beat)
      int count = 0;
      foreach (clsChordInfo ci in listci) {
        if (ci.Align == align) count++;
      }
      return count;
    }

    private eAlign Align;
    internal List<clsChordInfo>[] ListCIArr = null;
    private List<clsChordInfo> ListCIBar, ListCIHalfBar, ListCIBeat, ListCI;
    //internal int CIHalfBarCount = 0;
    //internal int CIBeatCount = 0;
    //internal int BeatCount = 0;
    private List<clsBarSubHB> Bars;
    private List<clsCompScores> MinScores;  //unweighted [bar]
    private List<clsWeightedScores> WeightedScores;  //combinations

    internal void ApplyChords(bool setchordatts) {
      //* set up align arrays
      GetScores();

      //* select align
      SelectAlign();

      //* set chords attributes
      if (setchordatts) SetChordAtts();
    }

    private void GetScores() {
      eAlign[] arralign = new eAlign[] { Align };
      ListCIArr = new List<clsChordInfo>[1];  //chordlist for bar only
      if (Align == eAlign.Auto) {  //try bar, half-bar, beat
        arralign = new eAlign[] { eAlign.Bar, eAlign.HalfBar, eAlign.Beat };
        ListCIArr = new List<clsChordInfo>[3];  //chordlists for bar, halfbar, beat
      }
      for (int i = 0; i < arralign.Length; i++) {
        ListCIArr[i] = Forms.frmChordMap.GetChordScores(FS, frmNoteMap, -1, -1, Cfg.DebugPath + @"\DumpChords[" + arralign[i] + "]", arralign[i]);
      }
      if (DebugChords) DebugScores();
    }

    private void DebugScores() {
      if (ListCIArr.Length < 3) return;  //not align.auto
      int hbindex = 0;
      int beatindex = 0;
      if (DebugChords) {
        for (int bar = 0; bar < ListCIArr[0].Count; bar++) {  //each bar
          DebugBarScores(ref hbindex, ref beatindex, bar);
        }
      }
      return;
    }

    private void DebugBarScores(ref int hbindex, ref int beatindex, int bar) {
      clsChordInfo ci = ListCIArr[0][bar];
      Debug.Write("Bar " + new clsMTime.clsBBT(ci.Segment.SegQILo * P.F.TicksPerQI).Bar);
      if (ci.Score == null) {
        Debug.WriteLine(": ***No Notes***"); 
      } else {
        Debug.Write(": " + ci.ScoreToString() + " ( ");

        while (hbindex < ListCIArr[1].Count && ListCIArr[1][hbindex].Segment.SegQIHi <= ci.Segment.SegQIHi) {
          Debug.Write(ListCIArr[1][hbindex++].ScoreToString() + " ");
        }
        Debug.Write(") [ ");

        while (beatindex < ListCIArr[2].Count && ListCIArr[2][beatindex].Segment.SegQIHi <= ci.Segment.SegQIHi) {
          Debug.Write(ListCIArr[2][beatindex++].ScoreToString() + " ");
        }
        Debug.WriteLine("]");
      }
    }

    internal class clsBarSubHB {  //bar
      internal clsChordInfo CIBar;
      internal List<clsHBSubBeat> CIHalfBars;  
      internal clsBarSubHB(clsChordInfo ci) {
        CIBar = ci;
        CIHalfBars = new List<clsHBSubBeat>(4);
      }
    }

    internal class clsHBSubBeat {  //halfbar
      internal clsChordInfo CIHalfBar;
      internal List<clsChordInfo> CIBeats;  //beats
      internal clsHBSubBeat(clsChordInfo ci) {
        CIHalfBar = ci;
        CIBeats = new List<clsChordInfo>(4);
      }
    }

    internal class clsCompScores {  //unweighted
      internal int? Bar;
      internal int? HB1;
      internal int? HB2;
      internal int? Beats1 = null;
      internal int? Beats2 = null;

      internal clsCompScores(clsBarSubHB bar) {
        Bar = bar.CIBar.NullableScore;
        HB1 = bar.CIHalfBars[0].CIHalfBar.NullableScore;
        //HB2 = bar.CIHalfBars[1].CIHalfBar.NullableScore;
        HB2 = GetRemHalfBarsAvg(bar);  //allowing for odd tsig
        Beats1 = GetBeatsAvg(bar.CIHalfBars[0].CIBeats);
        if (bar.CIHalfBars.Count > 1) Beats2 = GetBeatsAvg(bar.CIHalfBars[1].CIBeats);
      }

      private static int? GetBeatsAvg(List<clsChordInfo> cilist) {
        //* return average beat score
        int tot = 0;
        foreach (clsChordInfo ci in cilist) {
          if (!ci.NullableScore.HasValue) return null;
          tot += ci.NullableScore.Value;
        }
        if (tot == 0) return null; else return tot / cilist.Count;
      }

      private static int? GetBeatsMin(List<clsChordInfo> cilist) {
        //* return minimum beat score 
        int beats = int.MaxValue;
        foreach (clsChordInfo ci in cilist) {
          if (!ci.NullableScore.HasValue) return null;
          beats = Math.Min(beats, ci.NullableScore.Value);
        }
        if (beats == int.MaxValue) return null; else return beats;
      }

      private int? GetRemHalfBarsMin(clsBarSubHB bar) {
        //* get min halfbar score for 2nd. and subsequent halfbars (esp. odd tsigs)
        int minval = int.MaxValue;
        for (int i = 1; i < bar.CIHalfBars.Count; i++) {
          int? nscore = bar.CIHalfBars[i].CIHalfBar.NullableScore;
          if (!nscore.HasValue) return null;
          minval = Math.Min(minval, nscore.Value);
        }
        return minval;
      }

      private int? GetRemHalfBarsAvg(clsBarSubHB bar) {
        //* get avg halfbar score for 2nd. and subsequent halfbars (esp. odd tsigs)
        int tot = 0;
        for (int i = 1; i < bar.CIHalfBars.Count; i++) {  //start at 2nd
          int? nscore = bar.CIHalfBars[i].CIHalfBar.NullableScore;
          if (!nscore.HasValue) return null;
          tot += nscore.Value;
        }
        return tot / bar.CIHalfBars.Count - 1;
      }
    }

    internal class clsWeightedScores {
      internal clsWeightedScores(clsChordSegs segs, clsCompScores min) {
        Segs = segs;
        Forms.frmCfgChords frm = P.frmCfgChords;

        int avgweight = ((int)frm.trkSegmentHalfBar.Value + frm.trkSegmentBeat.Value) / 2;
        int hbweight = (int)frm.trkSegmentHalfBar.Value;
        int beatweight = (int)frm.trkSegmentBeat.Value;

        if (frm.MaxHBWeight) hbweight = int.MaxValue;
        if (frm.MinHBWeight) hbweight = int.MinValue;
        if (frm.MaxBeatWeight) beatweight = int.MaxValue;
        if (frm.MinBeatWeight) beatweight = int.MinValue;

        //* don't select HB_Beats or Beats_HB if either is minimum
        if (frm.MinBeatWeight || frm.MinHBWeight) avgweight = int.MinValue;

        //* don't select HB_Beats or Beats_HB if either is maximum
        if (frm.MaxBeatWeight || frm.MaxHBWeight) avgweight = int.MinValue;

        Combos[0] = new clsCombo(eCombo.Bar, SelectAuto_Bar);
        Combos[1] = new clsCombo(eCombo.HB_HB, SelectAuto_HB_HB);
        Combos[2] = new clsCombo(eCombo.HB_Beats, SelectAuto_HB_Beats);
        Combos[3] = new clsCombo(eCombo.Beats_HB, SelectAuto_Beats_HB);
        Combos[4] = new clsCombo(eCombo.Beats_Beats, SelectAuto_Beats_Beats);

        Combos[0].WeightedScore = min.Bar;
        Update(1, hbweight, min.HB1, min.HB2);
        Update(2, avgweight, min.HB1, min.Beats2);
        Update(3, avgweight, min.Beats1, min.HB2);
        Update(4, beatweight, min.Beats1, min.Beats2);
        SetMaxIndex();
      }

      private clsChordSegs Segs;
      internal clsCombo[] Combos = new clsCombo[5]; 
      internal clsCombo MaxCombo;

      internal void SetMaxIndex() {  //return index of maximum weighted score
        int maxscore = int.MinValue;
        int index = 0;
        for (int i = 0; i < Combos.Length; i++) {  
          int? score = Combos[i].WeightedScore;
          if (score == null) continue;
          if (score.Value > maxscore) {
            maxscore = score.Value;
            index = i;
          }
        }
        MaxCombo = Combos[index];
      }

      internal void Update(int index, int weight, int? score1, int? score2) {
        if (score1.HasValue && score2.HasValue) {
          if (weight == int.MaxValue) Combos[index].WeightedScore = int.MaxValue;
          else if (weight == int.MinValue) Combos[index].WeightedScore = int.MinValue;
          else Combos[index].WeightedScore = Math.Min(score1.Value, score2.Value) + weight;
        }
      }

      private clsChordInfo[] SelectAuto_Bar(int bar) {
        return new clsChordInfo[] { Segs.Bars[bar].CIBar };
      }  

      private clsChordInfo[] SelectAuto_HB_HB(int bar) {
        List<clsChordInfo> listci = new List<clsChordInfo>(4);
        foreach (clsHBSubBeat hb in Segs.Bars[bar].CIHalfBars) listci.Add(hb.CIHalfBar);  //hb
        return listci.ToArray();
      }  

      private clsChordInfo[] SelectAuto_HB_Beats(int bar) {
        List<clsChordInfo> listci = new List<clsChordInfo>(4);
        bool firsttime = true;
        foreach (clsHBSubBeat hb in Segs.Bars[bar].CIHalfBars) {
          if (firsttime) listci.Add(hb.CIHalfBar);  //hb
          else listci.AddRange(hb.CIBeats);  //beats
          firsttime = false;
        }
        return listci.ToArray();
      }  

      private clsChordInfo[] SelectAuto_Beats_HB(int bar) {
        List<clsChordInfo> listci = new List<clsChordInfo>(4);
        bool firsttime = true;
        foreach (clsHBSubBeat hb in Segs.Bars[bar].CIHalfBars) {
          if (!firsttime) listci.Add(hb.CIHalfBar);  //hb
          else listci.AddRange(hb.CIBeats);  //beats
          firsttime = false;
        }
        return listci.ToArray();
      }  

      private clsChordInfo[] SelectAuto_Beats_Beats(int bar) {
        List<clsChordInfo> listci = new List<clsChordInfo>(4);
        foreach (clsHBSubBeat hb in Segs.Bars[bar].CIHalfBars) listci.AddRange(hb.CIBeats);  //beats
        return listci.ToArray();
      }  
    }

    internal delegate clsChordInfo[] delSelectAuto(int bar);
    internal enum eCombo { Bar, HB_HB, HB_Beats, Beats_HB, Beats_Beats }; 

    internal class clsCombo {
      internal eCombo Combo;
      internal delSelectAuto SelectAuto;
      internal int? WeightedScore;

      internal clsCombo(eCombo combo, delSelectAuto selectauto) {
        Combo = combo;
        SelectAuto = selectauto;
      }
    }

    private void SelectAlign() {
      //* create listci from ListCIArr[0/1]
      ListCI = new List<clsChordInfo>(250);  //final chord list
      if (Align == eAlign.Auto) {
        //* create listci... from listciarr[0/1/2]
        ListCIBar = ListCIArr[0];
        ListCIHalfBar = ListCIArr[1];
        ListCIBeat = ListCIArr[2];
        Bars = new List<clsBarSubHB>(250);

        //* get bar/halfbar/beat structure
        GetStructure();

        //* calculate minimum unweighted scores
        MinScores = new List<clsCompScores>(250);
        for (int b = 0; b < Bars.Count; b++) {
          clsBarSubHB bar = Bars[b];
          clsCompScores min = new clsCompScores(bar);
          MinScores.Add(min);
          if (MinScores.Count != b + 1) LogicError.Throw(eLogicError.X086);
        }

        //* calculate weighted bar score combinations
        CalcWeightedScores();

        int hbindex = 0, beatindex = 0;
        for (int bar = 0; bar < Bars.Count; bar++) {
          clsWeightedScores w = WeightedScores[bar];
          clsChordInfo[] cibar = w.MaxCombo.SelectAuto(bar);
          if (DebugChords) {
            DebugBarScores(ref hbindex, ref beatindex, bar);
            DebugComboScore(bar, cibar);
          }
          ListCI.AddRange(cibar);
        }
      } else {
        ListCI = ListCIArr[0];
      }
    }

    private void DebugComboScore(int bar, clsChordInfo[] cibar) {
      if (ListCIArr.Length < 3) return;  //not align.auto
      Debug.Write("Combos " + bar + ": ");
      foreach (clsCombo c in WeightedScores[bar].Combos) {
        Debug.Write("(" + c.Combo + ": " + c.WeightedScore + ") ");
      }
      Debug.WriteLine("");

      Debug.Write("SelectedCombo " + bar +": ");
      foreach (clsChordInfo ci in cibar) {
        Debug.Write(ci.Align + "(" + ci.ScoreToString() + ") "); 
      }
      Debug.WriteLine("");
    }

    private void GetStructure() {
      int j = 0, k = 0;  //index to halfbar/beat segments list
      for (int i = 0; i < ListCIBar.Count; i++) {  
        clsBarSubHB bar = new clsBarSubHB(ListCIBar[i]);
        for (; j < ListCIHalfBar.Count; j++) {
          if (ListCIHalfBar[j].Segment.SegQILo > ListCIBar[i].Segment.SegQIHi) break;
          clsHBSubBeat halfbar = new clsHBSubBeat(ListCIHalfBar[j]);
          bar.CIHalfBars.Add(halfbar);
          for (; k < ListCIBeat.Count; k++) {
            if (ListCIBeat[k].Segment.SegQILo > ListCIHalfBar[j].Segment.SegQIHi) break;
            halfbar.CIBeats.Add(ListCIBeat[k]);
          }
        }
        Bars.Add(bar);
      }
   }

    internal void CalcWeightedScores() {
      WeightedScores = new List<clsWeightedScores>(250);
      foreach (clsCompScores min in MinScores) {
        clsWeightedScores w = new clsWeightedScores(this, min);
        WeightedScores.Add(w);
      }
    }

    //private clsChordInfo[] SelectAuto(clsChordInfo cibar, 
    //  List<clsChordInfo> listcihalfbarinbar,
    //  List<List<clsChordInfo>> listlistcibeatinhalfbar) {
    //  clsChordInfo[] cibararr = new clsChordInfo[] { cibar };  //one element

    //  //* debug scores
    //  Debug.Write("Bar: " + new clsMTime.clsBBT(cibar.Segment.SegQILo * P.F.TicksPerQI).Bar);
    //  Debug.Write(" scores: " + cibar.ScoreToString() + " ");
    //  foreach (clsChordInfo ci in listcihalfbarinbar) {
    //    Debug.Write(ci.ScoreToString() + " ");
    //    foreach (clsChordInfo ci in listcibeatinbar) Debug.Write(ci.ScoreToString() + " ");
    //    Debug.WriteLine("");
    //  }

    //  //* check if halfbar or beat chords are same as bar chord
    //  bool diff = false;
    //  foreach (clsChordInfo ci in listcihalfbarinbar) {
    //    if (!ci.Equiv(cibar)) { diff = true; break; }
    //  }
    //  if (!diff) {
    //    foreach (clsChordInfo ci in listcibeatinbar) {
    //      if (!ci.Equiv(cibar)) { diff = true; break; }
    //    }
    //  }
    //  if (!diff) return cibararr;  //all listcihalfbarinbar and listbeatinbar same as cibar

    //  //* check scores
    //  if (cibar.Score == null) return cibararr;
    //  foreach (clsChordInfo ci in listcihalfbarinbar) {
    //    if (ci.Score == null) return cibararr;
    //    if (ci.Score.Score - cibar.Score.Score < (int)P.frmCfgChords.trkSegmentHalfBar.Value) {
    //      HalfBarCount++;
    //      return cibararr;
    //    }
    //  }
    //  return listcihalfbarinbar.ToArray();
    //}

    private void SetChordAtts() {
      foreach (clsChordInfo ci in ListCI) {
        bool[] chord = new bool[12];
        clsNoteMapCF.sChordAtt att;
        if (ci.Score == null) {
          att = new clsNoteMapCF.sChordAtt(0);  //null chord
        } else {
          for (int n = 0; n < ci.ChNotes.Count; n++) chord[ci.ChNotes[n]] = true;
          int root = ci.Score.Root;
          sbyte qualifier = (sbyte)(ci.Score.TIndex + 1);
          att = new clsNoteMapCF.sChordAtt((sbyte)root, qualifier);
        }
        for (int q = ci.Segment.SegQILo; q <= ci.Segment.SegQIHi; q++) {
          P.F.CF.NoteMap.SetChordAndAtts(q, chord, att);
        }
        //} else {  //null score
        //  //* leave alone?
        //}
      }
    }
  }
}