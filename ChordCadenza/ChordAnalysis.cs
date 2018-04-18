#undef DumpTopScores

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
  internal class ChordAnalysis {
    internal static SortedList<ushort, clsTemplate> USToTemplate;
    internal static SortedList<int, clsTemplate> FileSeqToTemplate;
    internal static SortedList<string, clsTemplate> NameToTemplate;  //key is lowercase, Template.Name is correct case

    //internal enum eStatus: sbyte { NotChordinated, Match, Nomatch, Fail };
    //internal static clsTemplate[] Templates;  //multiple mnames (synonyms)
    //internal static clsTemplate[] MainTemplates;  //main names (one per sequence)
    private List<clsSeg> MinSegs = new List<clsSeg>();
    private static int MaxType;  //1-4
    private static int MaxNotes;
    //private const int SortRange = 10;

#if (DEBUG && DumpTopScores)
    //private static bool FirstTime = true;
    private static Stream XStreamDump;
    private static StreamWriter XSWDump;
    private static Stream XStreamDumpTop;
    private static StreamWriter XSWDumpTop;
    private static string Fmt;
    private static bool indFlush = false;
#endif

    static ChordAnalysis() {  //static constructor
      string msg = LoadTemplates();
      if (msg.Length > 0) {
        MessageBox.Show("Application terminating - error loading Chords.dat file");
        Environment.Exit(1);
      }
    }

    internal static string PtrToDesc(sbyte notemapptr) {  //notemapptr > 0
      //* return standard name (not synonym)
      if (notemapptr == 0) return "?";
      return USToTemplate.Values[notemapptr - 1].Name;
    }

    internal static ushort BoolArrayToUShort(bool[] bools) {
      if (bools.Length != 12) return 0;  //should not happen
      return clsNoteMap.BoolArrayToUShort(bools);
    }

    internal static string GetName(bool[] pcs) {
      //* get main (default) name
      ushort us = BoolArrayToUShort(pcs);
      if (!USToTemplate.ContainsKey(us)) return "xxx";
      return USToTemplate[us].Name;
    }

    internal static List<string> GetSynonyms(clsTemplate template) {
      //* get all names that are associated with templete 
      List<string> syns = new List<string>(3);
      for (int i = 0; i < NameToTemplate.Count; i++) {
        clsTemplate t = NameToTemplate.Values[i];
        if (template == t) syns.Add(NameToTemplate.Keys[i]);
      }
      return syns;
    }

    internal static void SetParams(int maxnotes, int maxtype) {
      //* set params for subsequent GetChord() calls
      MaxType = maxtype;
      MaxNotes = maxnotes;
    }

    //internal static List<string> GetMatchingChordNames(bool[] inpc, int root) {
    //  //* return matching chord names
    //  int length = 0;
    //  List<string> names = new List<string>();
    //  foreach (bool tf in inpc) if (tf) length++;
    //  if (length < 3 || length > 4) return names;  //empty list - only 3, 4 note chords checked
    //  bool[] pc0 = new bool[12];  //pcs root C
    //  for (int i = 0; i < 12; i++) {
    //    if (inpc[i]) {
    //      int p = (i - root).Mod12();  //root C 
    //      pc0[p] = true;
    //    }
    //  }
    //  foreach (clsTemplate t in Templates) {
    //    if (t.PC.SequenceEqual(pc0)) names.Add(t.Name); 
    //  }
    //  return names;
    //}

    internal static clsScore GetTopChord(bool[] inpc, clsKeyTicks mkey, int maxtypenomatch, 
      clsMTime.clsBBT bbt, out string chordname, out List<int> chnotes) {
      int length = 0;
      foreach (bool tf in inpc) if (tf) length++;
      if (length < 2) {
        chordname = "";
        chnotes = null;
        return null;
      }
      List<clsScore> chlist = GetChords(inpc, mkey, bbt);
      for (int i = 0; i < chlist.Count; i++) {
        //clsTemplate t = ChordAnalysis.Templates[chlist[i].TIndex];
        clsTemplate t = ChordAnalysis.USToTemplate.Values[chlist[i].TIndex];
        if (chlist[i].Score == t.Length) {  //exact match (but may not be unique)
          chordname = t.Name;
          chnotes = chlist[i].ChNotes;
          return chlist[i];  
        }
        if (t.Rank > maxtypenomatch) continue;  //no match and uncommon chord
        chordname = t.Name;
        chnotes = chlist[i].ChNotes;
        return chlist[i];
      }
      chordname = "";
      chnotes = null;
      return null;
    }

    internal static clsScore GetTopChordSeg(int[] inpercent, int[] basspercent, clsKeyTicks mkey,
    clsMTime.clsSegment segment, out string chordname, out List<int> chnotes) {
      int length = 0;
      foreach (int percent in inpercent) if (percent > 0) length++;
      //if (length <= 2) {
      if (length < (int)P.frmStart.nudMinChordSize.Value) {  //should be 0 unless advanced...
        chordname = "";
        chnotes = null;
        return null;
      }
      List<clsScore> chlist = GetChordsSeg(inpercent, basspercent, mkey, segment);
      if (chlist.Count == 0) {
        chnotes = new List<int>();
        chordname = "";
        return null;
      }
      chnotes = chlist[0].ChNotes;  //highest score
      chordname = chlist[0].Desc;
      //chordname = ChordAnalysis.UTemplates[chlist[0].TIndex].Name;
      return chlist[0];
    }

    internal static List<clsScore> GetChordsSeg(int[] inpercent, int[] basspercent, clsKeyTicks mkey, clsMTime.clsSegment segment) {
      //* inpc = segment weights for each pitchclass (0 = not present, 100 = occupies whole segment
      //* calculate scores
      //* segment param only used by dumpscores 
      List<clsScore> scores = new List<clsScore>();
      bool[] prevpc = new bool[12];
      int adder = (int)P.frmCfgChords.nudAdder.Value;  //high value favours 3-note chords...
      int factor = (int)P.frmCfgChords.nudChordFactor.Value;  //high value favours notes present
      for (int t = 0; t < USToTemplate.Count; t++) {  //each possible chord (template)
        if (USToTemplate.Values[t].Rank > MaxType) continue;
        if (USToTemplate.Values[t].Length > MaxNotes) continue;
        if (USToTemplate.Values[t].PC.SequenceEqual(prevpc)) continue;
        for (int r = 0; r < 12; r++) {  //for each root
          int score = 0;
          for (int p = 0; p < 12; p++) {  //each pitchclass
            int pp = p - r;
            if (pp < 0) pp += 12;
            //* calc score for this t/r/p
            //* range: -50 to 50
            //if (Templates[t].PC[pp]) score += factor * inpc[p] - adder; else score += adder - inpc[p];
            if (USToTemplate.Values[t].PC[pp]) score += (factor * inpercent[p]) / 10 - adder; 
            else score += adder - inpercent[p];
          }
          //* calc template pc relative to current key
          bool[] temppc = new bool[12];
          for (int i = 0; i < 12; i++) {
            if (USToTemplate.Values[t].PC[i]) {
              int tempi = (i + r - mkey.KeyNote).Mod12();
              temppc[tempi] = true;
            }
          }
          clsScore newscore = new clsScore(score, t, r, mkey, temppc, basspercent);
          scores.Add(newscore);
        }
        prevpc = USToTemplate.Values[t].PC;
      }

      if (scores.Count == 0) return new List<clsScore>();

      scores.Sort(new SimpleScoreComparer());   //simple sort (score only)
      scores.Reverse();  //highest score first

#if (DEBUG && DumpTopScores)
      clsMTime.clsBBT bbt = new clsMTime.clsBBT(segment.SegQILo * P.F.TicksPerQI);
      DumpScores(XSWDump, -1, mkey, bbt, scores, "");
#endif

      //* create list of top scores, sorted by other criteria
      List<clsScore> topscores = new List<clsScore>(9);
      topscores.Add(scores[0]);  //top score
      for (int i = 1; i < scores.Count; i++) {  //start at second element
        if (scores[i].Score <= scores[0].Score - (int)P.frmCfgChords.nudScoreRange.Value) break; 
        topscores.Add(scores[i]);
      }

      //topscores.Sort(new SecondaryScoreComparer());  //secondary fields, then score
      topscores.Sort(new AggScoreComparer());  //aggregate score
      topscores.Reverse();

      if (P.frmCfgChords.chkRemoveDups.Checked) {
        topscores = RemoveDups(topscores);
      }

#if (DEBUG && DumpTopScores)
      DumpScores(XSWDumpTop, -1, mkey, bbt, topscores, "");
#endif
      return topscores;
    }

    private static List<clsScore> RemoveDups(List<clsScore> scores) {
      List<clsScore> ret = new List<clsScore>();
      for (int i = 0; i < scores.Count; i++) {
        for (int j = 0; j < scores.Count; j++) {
          if (j == i) { //only check previous (lower) scores
            ret.Add(scores[i]);
            break;
          }
          if (scores[i].PC.SequenceEqual(scores[j].PC)) break;
        }
      }
      return ret;
    }

//#if CheckDups
//    internal static clsScore CheckDups(List<clsScore> scores, clsScore newscore) {
//      bool[] newpc = newscore.PC;
//      for (int i = 0; i < scores.Count; i++) {  
//        bool[] pc = scores[i].PC;
//        if (pc.SequenceEqual(newpc)) return scores[i];
//      }
//      return null;
//    }
//#endif

    internal static List<clsScore> GetChords(bool[] inpc, clsKeyTicks mkey, clsMTime.clsBBT bbt) {
      //* return scores etc. of nearest matching chords
      //int[,] score = new int[Templates.Length, 12];   //[template,root]
      //int maxscore = int.MinValue;
      string dia = NoteName.Diatonic[mkey.MidiKey + 7, mkey.MajMin];
      //******************** should above line be KBTrans???
      List<clsScore> ret = new List<clsScore>();  
      bool[] dummy = new bool[12];

      //* calculate scores
      for (int t = 0; t < USToTemplate.Count; t++) {  //each possible chord (template)
        if (USToTemplate.Values[t].Rank > MaxType) continue;
        if (USToTemplate.Values[t].Length > MaxNotes) continue;
        for (int r = 0; r < 12; r++) {  //for each root
          int score = 0;
          for (int p = 0; p < 12; p++) {  //each pitchclass in inchord
            int pp = p - r;
            if (pp < 0) pp += 12;
            if (inpc[p] == USToTemplate.Values[t].PC[pp]) {
              if (inpc[p]) score++;  //hit
            } else {
              if (inpc[p]) score--;  //note present in inchord, but not in template
              else {   //note present in template, but not inchord
                score--;
                if (dia.Substring(p, 1) == "0") score--;  //missing template note not diatonic
              }
            }
          }

          ret.Add(new clsScore(score, t, r, mkey, dummy, new int[0]));  //dups not monitored
        }
      }
      ret.Sort();
      ret.Reverse();  //highest score first

#if (DEBUG && DumpTopScores)
      if (bbt != null) {
        //* print InChord/Key info
        string inchordstr = "";
        for (int i = 0; i < 12; i++) {
          if (inpc[i]) inchordstr += GetNoteName(i, mkey) + " ";
        }

        DumpScores(XSWDump, 10, mkey, bbt, ret, inchordstr);
      }
#endif

      return ret;
    }

#if (DEBUG && DumpTopScores)
    private static void DumpScores(StreamWriter xsw, int showcount, clsKey mkey, clsMTime.clsBBT bbt, 
      List<clsScore> ret, string inchordstr) {
      //int ShowCount = 9;
      if (showcount < 0) showcount = int.MaxValue;
      xsw.WriteLine("");  //delimit bbt entries
      if (bbt != null) xsw.WriteLine("BBT: " + bbt.ToString());
      xsw.WriteLine("InChord: " + inchordstr);
      xsw.WriteLine("Key: " + mkey.KeyStrLong);
      xsw.WriteLine("MaxType: " + MaxType);
      xsw.WriteLine("MaxNotes: " + MaxNotes);

      //* print header
      xsw.WriteLine(Fmt, "Agg", "Scr", "RKD", "Typ", "Len", "Chr", "TI", "RT", "Name", "Notes");
      if (indFlush) xsw.Flush();

      for (int i = 0; i < showcount && i < ret.Count; i++) {
        clsScore sc = ret[i];
        //foreach (clsScore sc in ret) {
        xsw.Write(Fmt,
          sc.Agg,
          sc.Score,
          sc.COFRootKeyDistance,
          //Templates[sc.TIndex].Rank,
          //Templates[sc.TIndex].Length,
          FileSeqToTemplate[sc.TIndex].Rank,
          FileSeqToTemplate[sc.TIndex].Length,
          sc.ChromaticNotes,
          sc.TIndex,
          GetNoteName(sc.Root, mkey),
          sc.Desc,
          "");

        for (int n = 0; n < 12; n++) {
          //if (Templates[sc.TIndex].PC[n]) {
          if (FileSeqToTemplate[sc.TIndex].PC[n]) {
            xsw.Write(GetNoteName(n + sc.Root, mkey) + " ");
          }
        }
        //if (sc.Dup) xsw.Write("***");
        xsw.WriteLine("");
      }
      if (indFlush) xsw.Flush();
    }
#endif

#if DEBUG
    internal static void CloseDumpChords() {
    #if DumpTopScores
      XSWDump.Close();
      XSWDumpTop.Close();
    #endif
    }
#endif

#if DEBUG
    internal static void OpenDumpChords(string name, bool indflush) {
      #if DumpTopScores
        indFlush = indflush;
        Fmt = "{0,5} {1,3} {2,3} {3,3} {4,3} {5,3} {6,3} {7,-3} {8,-6} {9}";
        //*     agg   score COF   type  len   chrom tindex root  chordname notes

        //* open files for output
        string dir = Path.GetDirectoryName(name);
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        XStreamDump = new FileStream(name + ".txt", FileMode.Create, FileAccess.Write);  //overwrite
        XSWDump = new StreamWriter(XStreamDump);
        XSWDump.WriteLine("All Chords, Sorted by Score Only, for file: " + P.F.Project.CHPPath);
        XStreamDumpTop = new FileStream(name + ".Top.txt", FileMode.Create, FileAccess.Write);  //overwrite
        XSWDumpTop = new StreamWriter(XStreamDumpTop);
        XSWDumpTop.WriteLine("Top Chords with Score Near to Top, Sorted by All Sortkeys, for file: " + P.F.Project.CHPPath);
      #endif
    }
#endif

    private static string GetNoteName(int note, clsKey mkey) {
      return NoteName.ToSharpFlat(NoteName.GetNoteName(mkey, note.Mod12()));
    }

    internal static string LoadTemplates() {
      if (!File.Exists(Cfg.ChordNamesDatFilePath)) {
        return ("Chord Names file not found");
      }
      List<string> lines = Utils.ReadLinesIgnoreComments(Cfg.ChordNamesDatFilePath);
      if (lines == null) {
        return "ChordNames File Load Error";
      }

      NameToTemplate = new SortedList<string, clsTemplate>();
      USToTemplate = new SortedList<ushort, clsTemplate>();
      FileSeqToTemplate = new SortedList<int, clsTemplate>();

      for (int i = 0; i < lines.Count; i++) {
        string line = lines[i];
        string[] ff = line.Split(new char[] { ',' });
        if (ff.Length < 4) {
          return ("ChordNames file  - invalid field count found at or around line " + i);
        }

        List<string> names = new List<string>();
        int rank = 0, j = 0;
        for (j = 0; j < ff.Length; j++) {
          if (int.TryParse(ff[j], out rank) && rank < 0) break;  //first negative int
          string name = ff[j].Trim();
          if (name != "*") names.Add(name);
        }
        rank = -rank;

        int note;
        List<int> chord = new List<int>(5);
        chord.Add(0);  //root always C
        for (++j; j < ff.Length; j++) {
          if (!int.TryParse(ff[j], out note)) {
            return ("ChordNames file  - invalid line found at or around line " + i);
          }
          chord.Add(note);
        }

        clsTemplate t = new clsTemplate(names[0], rank, chord.ToArray());
        FileSeqToTemplate.Add(i, t);
        ushort us = BoolArrayToUShort(t.PC);
        USToTemplate.Add(us, t);
        foreach (string name in names) NameToTemplate.Add(name.ToLower(), t);
      }
      string msg = LoadRanks();
      if (msg != "") MessageBox.Show(msg);
      return "";

      /*
      //Stopwatch SW = new Stopwatch();
      //SW.Start();
      foreach (clsTemplate t in MainTemplates) t.GetHash();
      //* NOTE *******************************************
      //* C6 = Am7; Cm6 = Am7b5; Csus = Fsus2
      //* NOTE *******************************************

      //Debug.WriteLine("ChordAnalysis: InitTemplates: InvSyms millisecs = " + SW.ElapsedMilliseconds);
      //for (int i = 0; i < MainTemplates.Length; i++) Debug.WriteLine(i + ": " + MainTemplates[i].Hash);
      //SW.Stop();

      for (int i = 0; i < MainTemplates.Length; i++) {
        for (int j = i + 1; j < MainTemplates.Length; j++) {
          if (MainTemplates[i].Hash > 0 && MainTemplates[i].Hash == MainTemplates[j].Hash) {
            MainTemplates[i].InvSyns.Add(MainTemplates[j]);
            MainTemplates[j].InvSyns.Add(MainTemplates[i]);
          }
        }
      }
      */

      //foreach (clsTemplate t in MainTemplates) {
      //  if (t.InvSyns.Count > 0) {
      //    Debug.Write("InvSyms for " + t.Name + ": ");
      //    foreach (clsTemplate tt in t.InvSyns) Debug.Write(tt.Name);
      //    Debug.WriteLine("");
      //  }
      //}

      //return "";
    }

    internal static string LoadRanks() {
      //* overwrite only if ranks file exists
      //* should only exist if ranks have been changed by frmChordRanks
      if (!File.Exists(Cfg.ChordNamesRankIniFilePath)) return "";
      List<string> lines = Utils.ReadLinesIgnoreComments(Cfg.ChordNamesRankIniFilePath);
      if (lines == null || lines.Count != 2 || lines[0].Length != 2 || lines[1].Length != FileSeqToTemplate.Count) {
        return "ChordNames Rank File Load Error";
      }
      Forms.frmChordRanks.MaxRankMatch = int.Parse(lines[0].Substring(0, 1));
      Forms.frmChordRanks.MaxRankNoMatch = int.Parse(lines[0].Substring(1, 1));
      for (int i = 0; i < lines[1].Length; i++) {
        int rank = int.Parse(lines[1].Substring(i, 1));
        FileSeqToTemplate.Values[i].Rank = rank;
      }
      return "";
    }

    internal static void SetRanksToDefault() {
      Forms.frmChordRanks.MaxRankMatch = Forms.frmChordRanks.MaxRankMatchDefault;
      Forms.frmChordRanks.MaxRankNoMatch = Forms.frmChordRanks.MaxRankNoMatchDefault;
      foreach (clsTemplate t in FileSeqToTemplate.Values) {
        t.Rank = t.DefaultRank;
      }
    }

    internal class clsTemplate {
      internal string Name;  //main (default) name 
      internal int Length;  //number of notes in the chord
      internal bool[] PC = new bool[12];  //pitch classes (PC[0] always true)
      internal int[] Chord;  //pitch classes (incl. root - always 0)
      internal int Rank;
      internal int DefaultRank;
      /* next line used to find synonyms on different root (eg C6 = Am7)
      List<clsTemplate> InvSyns = new List<clsTemplate>(4);
      internal int Hash = 0;
      */

      internal clsTemplate(string name, int rank, int[] chord) {
        Name = name;
        Rank = rank;
        DefaultRank = rank;
        Chord = chord;
        foreach (int pc in Chord) PC[pc] = true;
        Length = Chord.Length;
      }

      //internal clsTemplate(string name, int type, params int[] pcarray) {
      //  //* parameters = int,int,...,synonym,synonym,...
      //  List<int> pclist = new List<int>();  //pitchclasses
      //  foreach (int pc in pcarray) pclist.Add(pc);
      //  if (pclist.Count < 2) {  
      //    LogicError.Throw(eLogicError.X002);
      //    return;
      //  }
      //  NewTemplate(name, type, pclist.ToArray());
      //  string key = name;
      //  if (!Synonyms.ContainsKey(key)) Synonyms.Add(key.ToLower(), this);  
      //}

      //private void NewTemplate(string name, int type, int[] pcarray) {
      //  Name = name;
      //  if (type >= 0) {
      //    LogicError.Throw(eLogicError.X003);
      //    return;
      //  }
      //  Rank = -type;
      //  Length = pcarray.Length + 1;
      //  PC[0] = true;   //assumed
      //  for (int i = 0; i < pcarray.Length; i++) PC[pcarray[i].Mod12()] = true;
      //}

      public override string ToString() {
        return Name;
      }

      /*
      internal void GetHash() {
        //* return minval of 1*int1 + 3*int2 + primex*intx ... across all inversions
        //* only calculate for max 3 intervals (3 or 4 note chords)
        Chord = GetChord();
        int len = Chord.Length;
        if (len > 4) return;
        if (len < 3) LogicError.Throw(eLogicError.X109);   //should not happen

        //* calculate intervals
        int tot = 0;
        int[] ints = new int[len];
          for (int j = 0; j < len; j++) {
          if (j == len - 1) {
            ints[j] = 12 - tot;
          } else {
            ints[j] = Chord[j + 1] - Chord[j];
            tot += ints[j];
          }
        }

        //* calculate hashes - no need to use last interval
        foreach (int i in ints) {
          if (i > 9) {
            LogicError.Throw(eLogicError.X110);  //should not happen
          }
        }
        int[] hashes = new int[Chord.Length];  //one hash for each inversion
        if (len == 3) {  //3 note chord (2 intervals)
          hashes[0] = ints[0] + 10 * ints[1];  //root
          hashes[1] = ints[1] + 10 * ints[2];  //1st inversion
          hashes[2] = ints[2] + 10 * ints[0];  //2nd inversion
        } else if (len == 4) {
          hashes[0] = ints[0] + 10 * ints[1] + 100 * ints[2];  //root
          hashes[1] = ints[1] + 10 * ints[2] + 100 * ints[3];  //1st inversion
          hashes[2] = ints[2] + 10 * ints[3] + 100 * ints[0];  //2nd inversion
          hashes[3] = ints[3] + 10 * ints[0] + 100 * ints[1];  //3rd inversion
        } else {
          LogicError.Throw(eLogicError.X111);  //should not happen
        }

        //* get Max Hash
        Hash = hashes.Min();
      }
      */

      internal int[] GetChord() {
        //* return chord notes
        List<int> list = new List<int>(5);
        for (int i = 0; i < 12; i++) {
          if (PC[i]) list.Add(i);
        }
        return list.ToArray();
      }
    }

    internal class clsSeg {
      //internal int StartQI;
      //internal int EndQI;
      internal int[] OnCount = new int[12];  //pitch class
      internal int Score = int.MinValue;
      internal int Chord = -1;  //index to Templates

      internal void CalcScore() {
        List<clsTemplate> tlist = new List<clsTemplate>();
        int pos = 0;  //positive evidence
        int neg = 0;  //negative evidence
        int miss = 0;  //misses
        int score = 0;
        for (int chd = 0; chd < tlist.Count; chd++) {  //for each chord template
          clsTemplate template = tlist[chd];
          if (template.Name == "***") break;  //end stop
          foreach (int pc in OnCount) {
            if (pc > 0) {
              if (template.PC[pc]) pos++;
              else neg++;
            } else if (template.PC[pc]) miss++;
          }
          score = pos - neg - miss;
          if (score > Score) {
            Score = score;
            Chord = chd;
          }
        }
      }
    }

    public class SimpleScoreComparer : IComparer<clsScore> {
      public int Compare(clsScore x, clsScore y) {
        if (x.Score < y.Score) return -1;
        if (x.Score > y.Score) return 1;
        return 0;
      }
    }

    public class AggScoreComparer : IComparer<clsScore> {
      //* uses Template index to ensure no equalities
      public int Compare(clsScore x, clsScore y) {
        if (x.Agg < y.Agg) return -1;
        if (x.Agg > y.Agg) return 1;
        return (x.TIndex > y.TIndex) ? -1 : 1; 
        //return 0;
      }
    }

    internal class clsScore : IComparable<clsScore> {
      internal int Score;
      internal int TIndex;
      internal string Desc;
      internal int Root;
      internal int COFRootKeyDistance;
      internal int ChromaticNotes = 0;
      internal List<int> ChNotes = new List<int>();
      internal bool[] PC;
      internal int Agg;  //aggregate score

      internal clsScore(int score, int tindex, int root, clsKeyTicks key, bool[] pc, int[] basspercent) {
        PC = pc;
        Score = score;
        TIndex = tindex;
        Root = root;
        Desc = USToTemplate.Values[tindex].Name;
        COFRootKeyDistance = NoteName.IntervalToCOF[Math.Abs(Root - key.KeyNote)];
        for (int n = 0; n < 12; n++) {
          if (USToTemplate.Values[tindex].PC[n]) ChNotes.Add((n + root).Mod12());
        }
        bool[] scalenotes = (key.Scale == "major") ? NoteName.MajScaleNotes : NoteName.MinScaleNotesAll;
        int noterel;
        for (int i = 0; i < 12; i++) {
          if (pc[i]) {  //note present in template chord
            //noterel = (i - key.KBTrans_KeyNote + 12).Mod12();
            noterel = (i - key.KeyNote).Mod12();  
            if (!scalenotes[i]) ChromaticNotes++;
          }
        }
        CalcAgg(score, basspercent, root);
      }

      private void CalcAgg(int score, int[] basspercent, int root) {
        int score10 = score * 10;
        Agg = score10;  //approx -100 to 600
        Agg -= COFRootKeyDistance * P.frmCfgChords.trkRKD.Value;  //0-6 * 0-40
        Agg -= ChromaticNotes * P.frmCfgChords.trkChromatic.Value;  //0-3,4,5 * 0-40
        Agg -= USToTemplate.Values[TIndex].Length * P.frmCfgChords.trkChordLength.Value;
        Agg -= USToTemplate.Values[TIndex].Rank * P.frmCfgChords.trkChordType.Value;
        //Agg += basspercent[root] * P.frmCfgChords.trkBass.Value / 5;  //0-10 * 0-40
      }

      public bool LessThan(clsScore s) {  //simple - not full implementation
        return (CompareTo(s) == -1);
      }

      public bool GreaterThan(clsScore s) {   //simple - not full implementation
        return (CompareTo(s) == 1);
      }

      public int CompareTo(clsScore s) {
        //* favour high score
        if (Score < s.Score) return -1;
        if (Score > s.Score) return 1;

        //* favour low root-key distance (circle of fifths)
        if (COFRootKeyDistance > s.COFRootKeyDistance) return -1;  //on circle of fifths
        if (COFRootKeyDistance < s.COFRootKeyDistance) return 1;  //opposite to other comparisons

        //* favour low type
        if (USToTemplate.Values[TIndex].Rank > USToTemplate.Values[s.TIndex].Rank) return -1;
        if (USToTemplate.Values[TIndex].Rank < USToTemplate.Values[s.TIndex].Rank) return 1;

        //* favour low chromatic note count
        if (ChromaticNotes > s.ChromaticNotes) return -1;
        if (ChromaticNotes < s.ChromaticNotes) return 1;

        //* favour low note count
        if (USToTemplate.Values[TIndex].Length > USToTemplate.Values[s.TIndex].Length) return -1;
        if (USToTemplate.Values[TIndex].Length < USToTemplate.Values[s.TIndex].Length) return 1;

        //* arbitary
        if (TIndex < s.TIndex) return -1;
        if (TIndex > s.TIndex) return 1;

        //* arbitary
        if (Root < s.Root) return -1;
        if (Root > s.Root) return 1;
        return 0;  //should not happen
      }
    }
  }
}

    /*
                 Templates.Add(new clsTemplate("maj", 0, 4, 7));
                 Templates.Add(new clsTemplate("minor", 0, 3, 7));
                 Templates.Add(new clsTemplate("diminished", 0, 3, 6));
                 Templates.Add(new clsTemplate("-5", 0, 4, 6));
                 Templates.Add(new clsTemplate("aug", 0, 4, 8));
                 Templates.Add(new clsTemplate("sus4", 0, 5, 7));
                 Templates.Add(new clsTemplate("sus2", 0, 2, 7));
                 Templates.Add(new clsTemplate("maj7", 0, 4, 7, 11));
                 Templates.Add(new clsTemplate("maj7+5", 0, 4, 8, 11));
                 Templates.Add(new clsTemplate("dom", 0, 4, 7, 10));
                 Templates.Add(new clsTemplate("add9", 0, 4, 7, 14));
                 Templates.Add(new clsTemplate("1/2dim", 0, 3, 6, 10));
                 Templates.Add(new clsTemplate("dim7", 0, 3, 6, 9));
                 Templates.Add(new clsTemplate("min7", 0, 3, 7, 10));
                 Templates.Add(new clsTemplate("min/maj7", 0, 3, 7, 11));
                 Templates.Add(new clsTemplate("7+5", 0, 4, 8, 10));
                 Templates.Add(new clsTemplate("7-5", 0, 4, 6, 10));
                 Templates.Add(new clsTemplate("7sus4", 0, 5, 7, 10));
                 Templates.Add(new clsTemplate("maj6", 0, 4, 7, 9));
                 Templates.Add(new clsTemplate("minor6", 0, 3, 7, 9));
                 Templates.Add(new clsTemplate("madd9", 0, 3, 7, 14));
                 Templates.Add(new clsTemplate("m6/9", 0, 3, 7, 9, 14));
                 Templates.Add(new clsTemplate("6add9", 0, 4, 7, 9, 14));
                 Templates.Add(new clsTemplate("7/6", 0, 4, 7, 9, 10));
                 Templates.Add(new clsTemplate("9", 0, 4, 7, 10, 14));
                 Templates.Add(new clsTemplate("7/13", 0, 4, 7, 10, 21));
                 Templates.Add(new clsTemplate("9-5", 0, 4, 6, 10, 14));
                 Templates.Add(new clsTemplate("9+5", 0, 4, 8, 10, 14));
                 Templates.Add(new clsTemplate("min9", 0, 3, 7, 10, 14));
                 Templates.Add(new clsTemplate("7-9", 0, 4, 7, 10, 13));
                 Templates.Add(new clsTemplate("7+9", 0, 4, 7, 10, 15));
                 Templates.Add(new clsTemplate("maj9", 0, 4, 7, 11, 14));
                 Templates.Add(new clsTemplate("min/maj9", 0, 3, 7, 11, 14));
                 Templates.Add(new clsTemplate("9/6", 0, 4, 7, 9, 10, 14));
                 Templates.Add(new clsTemplate("maj11", 0, 4, 7, 11, 14, 17));
                 Templates.Add(new clsTemplate("9+11", 0, 4, 7, 10, 14, 18));
                 Templates.Add(new clsTemplate("11", 0, 4, 7, 10, 14, 17));
                 Templates.Add(new clsTemplate("11-9", 0, 4, 7, 10, 13, 17));
                 Templates.Add(new clsTemplate("aug11", 0, 4, 7, 10, 14, 18));
                 Templates.Add(new clsTemplate("min11", 0, 3, 7, 10, 14, 17));
                 Templates.Add(new clsTemplate("min13", 0, 3, 7, 10, 14, 17, 21));
                 Templates.Add(new clsTemplate("maj13", 0, 4, 7, 11, 14, 17, 21));
                 Templates.Add(new clsTemplate("13", 0, 4, 7, 10, 14, 17, 21));
                 Templates.Add(new clsTemplate("13-9", 0, 4, 7, 10, 13, 17, 21));
                 Templates.Add(new clsTemplate("13-9-6", 0, 4, 6, 10, 13, 17, 21));
                 Templates.Add(new clsTemplate("13-9+11", 0, 4, 7, 10, 13, 18, 21));
                 Templates.Add(new clsTemplate("13+11", 0, 4, 7, 10, 14, 18, 21));
                 Templates.Add(new clsTemplate("13b", 0, 4, 7, 10, 14, 17, 20]}]
    */
