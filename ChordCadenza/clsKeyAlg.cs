using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ChordCadenza {
  internal class clsKeysAlg {
    internal class clsSegments {
    internal List<int[]> Segs = new List<int[]>();  //number of qi's note%12 present
    //internal clsFileStream CSVFileConv;
    internal bool indEmpty = true;

    internal clsSegments(clsNoteMap notemap) {
      //CSVFileConv = csvfileconv;
      //int barnum = 1;  //tmp debugging
      clsMTime.clsSegment startbar = new clsMTime.clsSegBar(P.F.TicksPerQI, 0, P.F.MaxBBT.QI);
      for (clsMTime.clsSegment bar = startbar; !bar.OOR; bar++) {
        //Debug.WriteLine("Bar: " + barnum++
        //  + " TicksLo: " + bar.SegQILo * P.F.TicksPerQI
        //  + " TicksHi: " + bar.SegQIHi * P.F.TicksPerQI);  //temp debugging
        int[] seg = new int[12];
        for (int qi = bar.SegQILo; qi <= bar.SegQIHi; qi++) {
          notemap.CalcKeys_CalcSegQI(seg, qi, ref indEmpty);
          //CalcSegQI(notemap, seg, qi, indEmpty);
        }
        Segs.Add(seg);
      }
    }

      //private void CalcSegQI(clsNoteMapMidi notemap, int[] seg, int qi, bool indempty) {
      //  for (int note = 0; note < 12; note++) {
      //    foreach (clsTrks.T trk in P.F.Chan.Next) {
      //      int chan = P.F.Chan[trk];
      //      if (chan == 9) continue;  //not percussion
      //      if (notemap[qi, note, trk]) {
      //        seg[note]++;
      //        indempty = false;
      //      }
      //    }
      //  }
      //}

      //internal clsSegments(clsNoteMap notemap, clsFileStream csvfileconv) {
      //  int size = clsFileStream.QIdd;  //number of QIs per segment
      //  CSVFileConv = csvfileconv;

      //  int[] seg = new int[12]; 
      //  for (int qtime = 0; qtime < notemap.GetLengthQTime(); qtime++) {
      //    if (qtime % size == 0) {
      //      //if (qtime > 0) Segs.Add(seg);  //add previous segment
      //      Segs.Add(seg);  //add previous segment
      //      seg = new int[12];
      //    }
      //    for (int note = 0; note < 12; note++) {
      //      for (int chan = 0; chan < 16; chan++) { 
      //        if (chan == 9) continue;  //not percussion
      //        if (notemap[qtime, note, chan]) {
      //          seg[note]++;
      //          //break;  //count only once for each qi
      //        }
      //      }
      //    }
      //  }
      //  Segs.Add(seg);
      //}

      //  internal clsCF LoadChordFile() {
      //    string txtfilename = P.F.MidiFilePath.Substring(0, P.F.MidiFilePath.Length - 4) + ".chp";
      //    if (!File.Exists(txtfilename)) return null;
      //    return new clsCF(txtfilename, true);
      //  }
    }

    internal enum eProfile { Default, Jazz, Special };

    //* default profile
    internal readonly static int[] ProfileMajDefault = new int[] { 10, 4, 7, 4, 9, 8, 4, 9, 4, 7, 3, 8 };
    internal readonly static int[] ProfileMinDefault = new int[] { 10, 4, 7, 9, 4, 8, 4, 9, 7, 4, 3, 8 };
    //***                                                         d     r     m  f     s     l     t

    //*blues/jazz
    internal readonly static int[] ProfileMajJazz = new int[] { 10, 4, 7, 3, 9, 8, 4, 9, 4, 7, 5, 5 };
    internal readonly static int[] ProfileMinJazz = new int[] { 10, 4, 7, 9, 3, 8, 4, 9, 7, 4, 5, 5 };
    //***                                                      d     r     m  f     s     l     t

    //*special
    internal readonly static int[] ProfileMajSpecial = new int[] { 12, 4, 7, 4, 12, 8, 4, 11, 4, 7, 3, 8 };
    internal readonly static int[] ProfileMinSpecial = new int[] { 12, 4, 7, 12, 4, 8, 4, 11, 7, 4, 3, 8 };
    //***                                                         d     r      m  f     s      l     t

    internal int[] ProfileMaj;
    internal int[] ProfileMin;
    internal float ProfileMajMean;
    internal float ProfileMinMean;
    internal float[] ProfileMajDiff = new float[12];
    internal float[] ProfileMinDiff = new float[12];
    internal float DD2Maj;  //denominator - second sum
    internal float DD2Min;  //denominator - second sum

    internal enum eAlg { Flat, Weighted };
    internal List<clsKeyTicks> Keys = new List<clsKeyTicks>();
    internal List<int[]> Score = new List<int[]>();  //[seg][key]
    internal int ChangePenalty;

    internal static eAlg Alg = eAlg.Weighted;
    //internal static clsMTime MTime;
#if DEBUG
    internal static bool Trace = false;
#else
    internal static bool Trace = false;
#endif
    internal static clsSegments Segments;
    internal static int ErrorMargin = 0;
    //internal static Forms.frmCalcKeys frmCalcKeys;

    internal clsKeysAlg(eProfile profile, eAlg alg, int changepenalty) {
      if (profile == eProfile.Default) {
        ProfileMaj = ProfileMajDefault;
        ProfileMin = ProfileMinDefault;
      } else if (profile == eProfile.Jazz) {
        ProfileMaj = ProfileMajJazz;
        ProfileMin = ProfileMinJazz;
      } else if (profile == eProfile.Special) {
        ProfileMaj = ProfileMajSpecial;
        ProfileMin = ProfileMinSpecial;
      }
      ProfileMajMean = Utils.GetMean(ProfileMaj);
      ProfileMinMean = Utils.GetMean(ProfileMin);
      ProfileMajDiff = new float[12];
      ProfileMinDiff = new float[12];
      DD2Maj = 0;  //denominator - second sum
      DD2Min = 0;  //denominator - second sum
      for (int i = 0; i < 12; i++) {
        ProfileMajDiff[i] = ProfileMaj[i] - ProfileMajMean;
        ProfileMinDiff[i] = ProfileMin[i] - ProfileMinMean;
        DD2Maj += ProfileMajDiff[i] * ProfileMajDiff[i];
        DD2Min += ProfileMinDiff[i] * ProfileMinDiff[i];
      }

      Alg = alg;
      ChangePenalty = changepenalty;
      // calculate keys from segments of midi file
      // assumes segment = 1 bar
      int[] keynotes = new int[Segments.Segs.Count];

      //* calculate unrationalized scores 
      for (int s = 0; s < Segments.Segs.Count; s++) {
        int[] seg = Segments.Segs[s];
        int[] score = new int[24];  //0-11:major keys  12-23:minor keys
        for (int key = 0; key < 12; key++) {
          for (int note = 0; note < 12; note++) {
            int degree = (note - key).Mod12();
            if (Alg == eAlg.Flat) {
              if (seg[note] > 0) {
                score[key + 00] += ProfileMaj[degree];
                score[key + 12] += ProfileMin[degree];
              }
            } else if (Alg == eAlg.Weighted) {
              score[key + 00] += seg[note] * ProfileMaj[degree];
              score[key + 12] += seg[note] * ProfileMin[degree];
            }
          }
        }

        int maxscore;
        int maxkey = Utils.GetMaxIndex(score, out maxscore);

        if (ChangePenalty == 0) {
          //* use unrationalized scores
          if (maxscore > 0) {
            keynotes[s] = maxkey;  //one key per segment
            Keys.Add(GetKey(s, keynotes[s]));
          } else {
            Keys.Add(null);  //empty segment
          }
          Score.Add(score);
        } else {  //non-zero change penalty
          CalcRMS(seg, score);
          Score.Add(score.ToArray());
        }
      }  //next segment

      if (ChangePenalty != 0) {
        //* find initial empty segments
        int ss, kk;
        for (ss = 0; ss < Score.Count; ss++) {
          for (kk = 0; kk < 24; kk++) {
            if (Score[ss][kk] != 0) break;  //not empty
          }
          if (kk < 24) break;  //not empty
          //LogicError.Throw(eLogicError.X082);  //empty 
          //return;
        }  //ss = first non-empty segment
        if (ss == Score.Count) {
          LogicError.Throw(eLogicError.X082);
          return;
        }

        //* apply change penalty
        if (ChangePenalty < 0) ChangePenalty = 0;  //for testing this code with zero change penalty 
        int[] agg = new int[24];
        int[] maxkey = new int[24];
        int[] maxagg = new int[24];
        int[] maxaggprev = new int[24];
        List<int[]> segkeys = new List<int[]>();  //[seg][key]
        for (int s = 0; s < Score.Count; s++) segkeys.Add(null);
        int k;
        for (k = 0; k < 24; k++) {
          maxkey[k] = k;
          maxaggprev[k] = Score[ss][k];  //first (non-empty) seg
        }
        segkeys[ss] = maxkey;
        for (int s = ss + 1; s < Score.Count; s++) {  //for each segment
          maxkey = new int[24];
          for (int j = 0; j < 24; j++) {
            maxagg[j] = int.MinValue;
            maxkey[j] = int.MinValue;
          }
          for (k = 0; k < 24; k++) {   //for each aggregate score
            for (int j = 0; j < 24; j++) {  //for each key for this segment
              agg[j] = maxaggprev[k] + Score[s][j];
              if (k != j) agg[j] -= ChangePenalty;
              if (agg[j] > maxagg[j]) {
                maxagg[j] = agg[j];
                maxkey[j] = k;
              }
            }
          }
          maxaggprev = maxagg.ToArray();
          segkeys[s] = maxkey;
        }  //next segment
        for (int s = 0; s < Score.Count; s++) {
          Keys.Add(null);
        }
        int maxkeyback = Utils.GetMaxIndex(maxagg);  //get max agg for last segment 
        for (int s = Score.Count - 1; s >= ss; s--) {
          maxkeyback = segkeys[s][maxkeyback];
          Keys[s] = GetKey(s, maxkeyback);
        }
#if DEBUG
        if (Trace) {
          WriteTraceFile("Score", Score, "{0, 4}", false);
          WriteTraceFile("Key", segkeys, "{0, -4}", true);
        }
#endif
      } else {  //changepenalty = 0
#if DEBUG
        if (Trace) WriteTraceFile("Score", Score, "{0, 6}", false);
#endif
      }
    }

    internal float GetPercent(clsCFPC cf) {
      //* return percentage of keys that are the same
      int samecnt = 0, validcnt = 0;
      for (int seg = 0; seg < Keys.Count; seg++ ) {
        bool? result = Compare(cf, seg);
        if (result.HasValue) {
          validcnt++;
          if (result.Value) samecnt++;
        }
      }
      return (samecnt * 100) / validcnt;
    }

    internal bool? Compare(clsCFPC cf, int seg) {
      //* compare key from this to key from txtkey - return true if the same
      clsKeyTicks algkey = Keys[seg];
      if (algkey == null) return null;
      //algkey = algkey.Transpose(-cf.Transpose_File);
      int seglo = seg - ErrorMargin;
      int seghi = seg + ErrorMargin;
      for (int s = seglo; s <= seghi; s++) {
        if (s < 0 || s >= Keys.Count) continue;
        int ticks = new clsMTime.clsBBT(s, 0, 0).Ticks;
        //int ticks = cf.MTime.GetTicks(s);
        clsKeyTicks txtkey = P.F.Keys[ticks];
        if (algkey.IsEquiv(txtkey)) return true;
      }
      return false;
    }

    private void CalcRMS(int[] seg, int[] score) {
      //* rationalize scores (based on root mean square correlation)
      float[] scorefloat = new float[24];
      float meanseg = Utils.GetMean(seg);

      for (int key = 0; key < 12; key++) {
        float nn = 0;  //numerator
        float dd1 = 0;  //denominator - first sum
        float[] segdiff = new float[12];
        for (int note = 0; note < 12; note++) segdiff[note] = seg[note] - meanseg;

        //* calc major rms terms
        for (int note = 0; note < 12; note++) {
          int degree = (note - key).Mod12();
          nn += segdiff[note] * ProfileMajDiff[degree];
          dd1 += segdiff[note] * segdiff[note];
        }
        if (dd1 == 0) scorefloat[key + 00] = 0;
        else scorefloat[key + 00] = (float)(nn / Math.Sqrt(dd1 * DD2Maj));

        //* calc minor rms terms
        nn = 0; dd1 = 0;
        for (int note = 0; note < 12; note++) {
          int degree = (note - key).Mod12();
          nn += segdiff[note] * ProfileMinDiff[degree];
          dd1 += segdiff[note] * segdiff[note];
        }
        if (dd1 == 0) scorefloat[key + 12] = 0;
        else scorefloat[key + 12] = (float)(nn / Math.Sqrt(dd1 * DD2Min));

      }

      for (int i = 0; i < 24; i++) score[i] = (int)(scorefloat[i] * 100);
    }

#if DEBUG
    private void WriteTraceFile(string name, List<int[]> table, string fmt, bool indkey) {
      string filename = name + "_" + Alg.ToString() + "(" + ChangePenalty + ").txt";
      Stream xstream = new FileStream(@"D:\0\Sonar\Trace\" + filename, FileMode.Create, FileAccess.Write);  //overwrite
      using (StreamWriter xsw = new StreamWriter(xstream)) {
        string line = "    ";
        for (int s = 0; s < table.Count; s++) line += string.Format(fmt, s + 1);
        xsw.WriteLine(line);
        for (int kk = 0; kk < 24; kk++) {
          clsKeyTicks key = GetKey(0, kk);
          line = key.KeyStrShort;
          line = string.Format("{0, -4}", line);
          string ent;
          for (int s = 0; s < table.Count; s++) {
            if (table[s] == null) {
              ent = "***";
            } else {
              if (indkey) {
                clsKeyTicks entkey = GetKey(s, table[s][kk]);
                ent = entkey.KeyStrShort;
              } else {
                ent = table[s][kk].ToString();
              }
            }
            line += string.Format(fmt, ent);
          }
          xsw.WriteLine(line);
        }
      }
      //xsw.Close();
    }
#endif

    private clsKeyTicks GetKey(int s, int keynote) {
      string scale = "major";
      if (keynote > 11) {
        scale = "minor";
        keynote -= 12;
      }
      int midikey = NoteName.PitchToMidiKey(keynote, scale, null);
      int ticks = new clsMTime.clsBBT(s, 0, 0).Ticks;
      //int ticks = mtime.TicksPerBeat * mtime.TSigNN * s;
      clsKeyTicks key = new clsKeyTicks(midikey, scale, ticks);
      //if (frmCalcKeys.optKeysText.Checked) return key.Transpose(-frmCalcKeys.CF.Transpose_File);
      return key;
    }
  }
}
