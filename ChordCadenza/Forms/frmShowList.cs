using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ChordCadenza.Forms {
  internal partial class frmShowList : Form, IFormStream {
    public void FormStreamOnOff(bool on) { }
    internal frmShowList(eList type) {
      Type = type;
      InitializeComponent();
      Forms.frmSC.ZZZSetPCKBEvs(this);
    }

    internal frmShowList(string[] msgs) {
      Type = eList.Msgs;
      Msgs = msgs;
      InitializeComponent();
      Forms.frmSC.ZZZSetPCKBEvs(this);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
      bool? ret = Forms.frmSC.StaticProcessCmdKey(ref msg, keyData);
      if (!ret.HasValue) return base.ProcessCmdKey(ref msg, keyData);
      return ret.Value;
    }

    internal enum eList { Msgs, TSigs, Keys, Tempos, CtlrTots, CtlrDetails,
      Debug, ChordList, Attributes, Strm, NoteMap };
    internal eList Type;
    private string[] Msgs;

    private void frmShowList_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      //P.Forms.Add(this);
      ShowList();
    }

    internal void ShowList() {
      if (Type == eList.Msgs) {
        txtList.Lines = Msgs;
        return;
      }

      string[] lines = null;
      clsFileStream fs = null;
      if (P.F.FSTrackMap != null) fs = P.F.FSTrackMap;
      else if (P.F.FileStreamConv != null) fs = P.F.FileStreamConv;

      //* get first selected track, or null
      if (Type == eList.Strm) {
        clsTrks.T trk = null;
        clsFileStream.clsEvStrm[] strm = P.F.FSTrackMap.Strm;
        if (strm == null) return;
        clsTrks.Array<bool> trkselect = P.F.frmTrackMap?.GetSelectedTrks();
        if (trkselect != null) {
          foreach (clsTrks.T t in trkselect.Next) {
            if (trkselect[t]) {
              trk = t;
              break;
            }
          }
        }
        Text = "Show Strm for ";
        Text += (trk == null) ? "all tracks" : "track " + trk.ToString();

        //* display headings
        List<string> list = new List<string>(110);
        string fmt = "{0,6} {1,3} {2,6} {3,6} {4}";
        list.Add((string.Format(fmt, "Seq", "Trk", "Ticks", "DITime", "Data")));

        //* show data from CurrentBBT
        int strmpos = clsFileStream.clsPlay.Find(P.F.FSTrackMap, strm, P.F.CurrentBBT.Ticks);
        int cnt = 0;
        for (int i = strmpos; i < strm.Length; i++) {
          clsFileStream.clsEvStrm s = strm[i];
          if (trk != null && s.Trk != null && trk != s.Trk) continue;
          string t = (s.Trk == null) ? "*" : s.Trk.ToString(); 
          list.Add(String.Format(fmt, i, t, s.Ticks, s.QTime, s.ToString()));
          if (++cnt > 100) break;
        }
        lines = list.ToArray();

      } else if (Type == eList.Attributes) {
        if (P.F.MTime == null) return;
        Text = "Show Project and System Attributes";
        List<string> lineslist = new List<string>();
        //lineslist.Add("Version = " + P.Version);
        lineslist.Add("64-bit Operating System = " + Environment.Is64BitOperatingSystem);
        lineslist.Add("64-bit Process = " + Environment.Is64BitProcess);
        lineslist.Add("Debug Mode = " + Debugger.IsAttached);
        //#if APPDATAPATH
        //  lineslist.Add("AppDataPath = true");
        //#else
        //  lineslist.Add("AppDataPath = false");
        //#endif
        #if ADVANCED
          lineslist.Add("Advanced = true");
        #else
          lineslist.Add("Advanced = false");
        #endif
        lineslist.Add("TicksPerQuarterNote = " + P.F.MTime.TicksPerQNote);
        lineslist.Add("TicksPerQI = " + P.F.TicksPerQI);
        lineslist.Add("QIPerNote = " + P.F.QIPerNote);
        if (P.F.FSTrackMap?.Text00.Count > 0) {
          lineslist.Add("");
          lineslist.Add("Initial text events on MidiFile conductor track");
          lineslist.Add("-----------------------------------------------");
          foreach (string text in P.F.FSTrackMap?.Text00) {
            lineslist.Add(text);
          }
        }
        lines = lineslist.ToArray();

      } else if (Type == eList.TSigs) {
        if (P.F.MTime?.TSigs == null) return;
        Text = "Show Time Signatures";
        string fmt = "{0,3} {1,2}/{2,-2}";
        //* bar nn / dd
        lines = new string[P.F.MTime.TSigs.Length + 2];
        //lines[0] = "Source: Midi";
        lines[0] = string.Format(fmt, "Bar", "NN", "DD");
        for (int i = 0; i < P.F.MTime.TSigs.Length; i++) {
          clsMTime.clsTSigBB tsig = P.F.MTime.TSigs[i];
          lines[i + 1] = String.Format(fmt, tsig.Bar+1, tsig.NN, tsig.DD);
        }

      } else if (Type == eList.Keys) {
        clsKeysTicks keys = P.F.Keys;
        if (keys == null) return;
        Text = "Show Keys";
        string fmt = "{0,3} {1,4} {2,5} {3,-5}";
        //* bar beat pitch scale
        lines = new string[keys.Keys.Count + 1];  
        lines[0] = string.Format(fmt, "Bar", "Beat", "Pitch", "Scale");
        for (int i = 0; i < keys.Keys.Count; i++) {
          clsKeyTicks key = keys.Keys[i];
          lines[i + 1] = String.Format(fmt, key.BBT.Bar+1, key.BBT.BeatsRemBar+1, key.KeyNoteStr_ToSharpFlat, key.Scale);
        }

      } else if (Type == eList.Tempos) {
        if (fs == null || fs.TempoMap == null) return;
        Text = "Show Tempos";
        string fmt = "{0,3} {1,4} {2,5} {3,9} {4,10}";  
        //* bar beat tempo
        lines = new string[Math.Max(2, fs.TempoMap.Count + 1)];  
        lines[0] = string.Format(fmt, "Bar", "Beat", "Tempo", "MidiTempo", "MSecsPerQI");
        int miditempo, bpm, msecsperqi;
        if (fs.TempoMap.Count == 0) {
          //miditempo = fs.TempoMap[0];  //default
          miditempo = fs.TempoMap.GetFirstValue();  //default
          msecsperqi = (P.F.TicksPerQI * miditempo) / (P.F.MTime.TicksPerQNote * 1000);
          //msecsperpi = (P.F.TicksPerPI * miditempo) / (P.F.MTime.TicksPerQNote * 1000);
          int dd = P.F.MTime.GetTSig(0).DD;
          bpm = clsAudioSync.DivRound(dd * 60000000, 4 * miditempo);
          lines[1] = String.Format(fmt, 1, 1, bpm, miditempo, msecsperqi);
          lines[1] += " (default)";
        } else {
          //for (int i = 0; i < fs.TempoMap.Count; i++) {
          //  int ticks = fs.TempoMap.KeyByIndex(i);
          //  miditempo = fs.TempoMap.ValByIndex(i);
          int linenum = 1;
          foreach (KeyValuePair<int, int> pair in fs.TempoMap) {
            int ticks = pair.Key;
            miditempo = pair.Value;
            clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
            msecsperqi = (P.F.TicksPerQI * miditempo) / (P.F.MTime.TicksPerQNote * 1000);
            int dd = P.F.MTime.GetTSig(ticks).DD;
            bpm = clsAudioSync.DivRound(dd * 60000000, 4 * miditempo);
            //lines[i + 1] = String.Format(fmt, bbt.Bar + 1, bbt.BeatsRemBar + 1, bpm, miditempo, msecsperdi);
            lines[linenum++] = String.Format(fmt, bbt.Bar + 1, bbt.BeatsRemBar + 1, bpm, miditempo, msecsperqi);
          }
        }

      } else if (Type == eList.CtlrTots) {
        if (fs == null || fs.MidiCtlrs == null) return;  //or P.F.MultiMap or P.F.MultiPlay???
        Text = "Show Controller Totals";
        string fmt = "{0,4} {1,4} {2,4} {3,6} {4}";
        //string fmt = "{0,4} {1,4} {2,4} {3,4} {4,6} {5}";
        //* trk port chan ctlr total description
        List<string> l = new List<string>();
        l.Add(string.Format(fmt, " Trk", "Chan", "Ctlr", "Total", "Description"));
        for (int ctlr = 0; ctlr < fs.MidiCtlrs.DataLast.GetLength(0); ctlr++) {
          for (int pc = 0; pc < 16; pc++) {
            if (fs.MidiCtlrs.DataLast[ctlr, pc] == null) continue;
            int tot = fs.MidiCtlrs.DataLast[ctlr, pc].Count;
            string trk = "*";
            string desc = "???";
            if (ctlr == clsMidiCtlrs.PatchCtlrNum) desc = "Patch";
            else desc = MidiCtlrList.Desc[ctlr];
            l.Add(String.Format(fmt, trk, (pc + 1), ctlr, tot, desc));
          }
        }   
        lines = l.ToArray();

      } else if (Type == eList.CtlrDetails) {
        if (fs == null || fs.MidiCtlrs == null) return;  //or P.F.MultiMap or P.F.MultiPlay???
        Text = "Show Controller Details";
        string fmt = "{0,4} {1,4} {2,4} {3,6} {4,9} {5,4} {6}";
        //string fmt = "{0,4} {1,4} {2,4} {3,4} {4,6} {5,9} {6,4} {7}";
        //* trk port chan ctlr ticks value description
        List<string> l = new List<string>();
        l.Add(string.Format(fmt, " Trk", "Chan", "Ctlr", "Ticks", "BBT", " Val", "Description"));
        for (int pc = 0; pc < 16; pc++) {
          string trk = "*";
          for (int ctlr = 0; ctlr < fs.MidiCtlrs.DataLast.GetLength(0); ctlr++) {
            string desc = "???";
            //clsMidiCtlrs.clsMap<int> map = fs.MidiCtlrs.DataLast[ctlr, pc];
            clsMap<int> map = fs.MidiCtlrs.DataLast[ctlr, pc];
            if (map == null) continue;
            foreach (KeyValuePair<int, int> pair in map) {
              int ticks = pair.Key;
              clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
              int val = pair.Value;
              if (ctlr == clsMidiCtlrs.PatchCtlrNum) desc = "Patch: " + GeneralMidiList.Desc[val];
              else desc = MidiCtlrList.Desc[ctlr];
              l.Add(String.Format(fmt, trk, (pc + 1), ctlr, ticks, bbt.ToString(), val, desc));
            }
          }
        }
        lines = l.ToArray();

      } else if (Type == eList.Debug) {
        Text = "Show Debug Info";
        List<string> l = new List<string>();
        l.Add("Current Ticks = " + P.F.CurrentBBT.Ticks);
        if (P.F != null) {
          if (P.F.MTime != null) l.Add("P.F.MTime.TicksPerQNote = " + P.F.MTime.TicksPerQNote);
          l.Add("P.F.TicksPerDI = " + P.F.TicksPerQI);
          //l.Add("P.F.TicksPerPI = " + P.F.TicksPerPI);
          l.Add("P.F.MaxTicks = " + P.F.MaxBBT.Ticks);
          l.Add("P.F.MaxDITime = " + P.F.MaxBBT.QI);
          //l.Add("P.F.MaxPITime = " + P.F.MaxPITime);
        }
        lines = l.ToArray();

      } else if (Type == eList.ChordList) {  //standard chords
        Text = "Show Chords";
        string fmt = "{0,-8} {1,-8} {2,-8} {3,-4} {4,-24}";
        lines = new string[ChordAnalysis.USToTemplate.Values.Count + 1];
        lines[0] = string.Format(fmt, "Name", "Name", "Name", "Rank", "Notes");
        for (int i = 0; i < ChordAnalysis.FileSeqToTemplate.Values.Count; i++) {
          ChordAnalysis.clsTemplate t = ChordAnalysis.FileSeqToTemplate.Values[i];
          string notes = "";  //root note
          for (int j = 0; j < 12; j++) {
            if (t.PC[j]) notes += NoteName.ToSharpFlat(NoteName.GetNoteName(0, 0, j)) + ' ';
          }
          List<string> names = ChordAnalysis.GetSynonyms(t);
          string name0 = names[0];
          string name1 = (names.Count > 1) ? names[1] : "";
          string name2 = (names.Count > 2) ? names[2] : "";
          lines[i + 1] = String.Format(fmt, name0, name1, name2, t.Rank, notes);
        }

      } else {
         LogicError.Throw(eLogicError.X048);
         lines = new string[] {"Invalid Show Type"};
      }
      txtList.Lines = lines;
      txtList.Select(0, 0);

    }  //method

  }  //class
}  //namespace
