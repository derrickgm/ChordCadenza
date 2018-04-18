using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ChordCadenza {
  internal class clsLoadCSV {
    internal class clsTimeChord {
      internal int Ticks;
      internal clsChord Chord;
      internal clsTimeChord(int ticks, clsChord chord) {
        Ticks = ticks;
        Chord = chord;
      }
    }

    internal class clsChord {
      internal bool[] Notes = new bool[12];
      internal List<int> ChList;

      internal clsChord() {
        for (int i = 0; i < Notes.Length; i++) Notes[i] = false;
      }

      internal bool this[int i] {
        get {return Notes[i];}
        set {Notes[i] = value;}
      }

      internal bool SequenceEqual(clsChord ch) {
        if (ch == null) return false;
        return Notes.SequenceEqual(ch.Notes);
      }

      internal bool Contains(clsChord ch) {
        if (ch == null) return false;
        for (int i = 0; i < ch.Notes.Length; i++) {
          if (ch[i] && !Notes[i]) return false;
        }
        return true;
      }

      internal bool IsEmpty() {
        foreach (bool note in Notes) {
          if (note) return false;
        }
        return true;
      }

      private void Invert(List<int> chord) {
        chord.Add(chord[0]);
        chord.RemoveAt(0);
      }

      internal int Count() {
        int ret = 0;
        foreach (bool tf in Notes) {
          if (tf) ret++;
        }
        return ret;
      }

      private bool Check(int[] intervals, List<int> chord) {
        for (int i = 0; i < intervals.Length; i++) {
          int intv = chord[i + 1] - chord[i];
          if (intv < 0) intv += 12;
          if (intv != intervals[i]) return false;
        }
        return true;
      }

      internal string CreateChList(bool[] notes) {  //interactive
        Notes = notes;
        return CreateChList();
      }

      internal string CreateChList() {
        CreateUList();
        List<int> ul = ChList;
        for (int i = 0; i < NoteName.CommonChords.Count; i++) {  //for each chord name 
          if (ul.Count != NoteName.CommonChords[i].SimpleIntervals.Length + 1) continue;
          for (int j = 0; j < ul.Count; j++) {
            if (Check(NoteName.CommonChords[i].SimpleIntervals, ul)) return NoteName.CommonChords[i].Name;  //match
            Invert(ul);
          }
        }
        return "***";
      }

      private void CreateUList() {  
        //convert bool[] to unordered List<int> (ul) 
        ChList = new List<int>();
        for (int i = 0; i < Notes.Length; i++) {
          if (Notes[i]) ChList.Add(i);
        }
      }

      private void CreateUList(bool[] notes) {  //interactive
        Notes = notes;
        CreateUList();
      }
    }

    //internal clsNoteMap NoteMap;
    internal List<clsTimeChord> ChordList = new List<clsTimeChord>(); 
    //internal string FileName;
    //internal StreamWriter xsw;
    //internal MemoryStream xstream;
    //internal clsFileStream CSVFileSummary;
    //internal clsFileStream CSVFileConv;
    //internal clsFileStream CSVFileMultiMap;
    //internal clsFileStream CSVFileMultiPlay;
    //internal clsFileStream CSVFileChordTrk;
    //private bool Append = true;
    //internal string DefaultTxtFileName;
    //internal string TxtFileName = "";

    internal clsLoadCSV() {
      //P.F.MidiFilePath = filename;
      //CSVFileSummary = new clsFileStream(filename, true, null, false);
      //P.F.frmLoadCSV.Populateclb(CSVFileSummary);
    }

    //internal void ConvertCSV(bool[] trkselect) {
    //  if (trkselect == null) return;
    //  Append = OpenTextFile(out xstream, out xsw);
    //  if (xsw == null) return;  //txtfilename not set 
    //  CSVFileConv = new clsCSVFileConv(CSVFileSummary, trkselect);
    //  if (!Append) InitMemStream();  //new file/overwrite file
    //  P.F.frmNoteMap = new Forms.frmNoteMap(CSVFileConv);
    //  P.F.frmNoteMap.Show();
    //}

    //internal void InitMemStream(Forms.frmNoteMap frm, bool[] trkselect, clsCFtxt cftxt) {
    //  //* create memory stream and initialize with header (and params, keys, comments)
    //  if (!FileName.ToLower().EndsWith(".csv") && !FileName.ToLower().EndsWith(".mid")) {
    //    throw new AppException();
    //  }
    //  xstream = new MemoryStream();
    //  xsw = new StreamWriter(xstream);
    //  WriteNewComments(frm, trkselect);

    //  DefaultTxtFileName = FileName.Substring(0, FileName.Length - 4) + ".txt";
    //  if (!File.Exists(DefaultTxtFileName)) {
    //    P.F.LoadCSV.WriteHeader(xsw);  //incl. all tsigs 
    //    return;
    //  }

    //  if (cftxt == null) {
    //    try {
    //      cftxt = new clsCFtxt(DefaultTxtFileName);
    //    }
    //    catch (ChordFileException) {
    //      P.F.LoadCSV.WriteHeader(xsw);  //incl. all tsigs 
    //      return;
    //    }
    //  }

    //  foreach (string line in cftxt.Lines_Comments) {
    //    if (line.ToLower().StartsWith("*created ")) continue;
    //    else if (line.ToLower().StartsWith("*using ")) continue;
    //    xsw.WriteLine(line);
    //  }

    //  xsw.WriteLine(cftxt.Line_Header);
    //  foreach (string line in cftxt.Lines_TSigs) xsw.WriteLine(line);
    //  foreach (string line in cftxt.Lines_Params) xsw.WriteLine(line);
    //  foreach (string line in cftxt.Lines_Keys) xsw.WriteLine(line);
    //  //return -cftxt.Transpose_File;  //reverse the txt transpose 
    //}

    //private void WriteNewComments(Forms.frmNoteMap frm, bool[] trkselect) {
    //  string newline = "";
    //  WriteNewCreatedBy(frm, newline, trkselect);
    //  if (frm != null) {
    //    newline = "*using filters: ";
    //    newline += (int)frm.nudCloseGap.Value + "/";
    //    newline += (int)frm.nudFilter.Value + "/";
    //    newline += (int)frm.nudQuantize.Value;
    //    if (frm.chkMinWeight.Checked) newline += " MinWeight: " + frm.trkFilter.Value;
    //    else if (frm.chkMaxChord.Checked) newline += " MaxChord: " + (int)frm.nudMaxChord.Value;
    //    xsw.WriteLine(newline);
    //  }
    //}

    //private void WriteNewCreatedBy(Forms.frmNoteMap frm, string newline, bool[] trkselect) {
    //  if (frm == null) newline = "*created by 'NoteMap' on " + System.DateTime.Now;
    //  else newline = "*created by 'AddAll' on " + System.DateTime.Now;
    //  xsw.WriteLine(newline);
    //  newline = "*using tracks:";
    //  if (trkselect != null) {
    //    for (int ch = 0; ch < trkselect.Length; ch++) {
    //      if (trkselect[ch]) newline += " " + (ch + 1);
    //    }
    //  } else {
    //    for (int ch = 0; ch < P.F.CSVFileConv.TrkSelect.Length; ch++) {
    //      if (P.F.CSVFileConv.TrkSelect[ch]) newline += " " + (ch + 1);
    //    }
    //  }
    //  xsw.WriteLine(newline);
    //}

    //internal void CreateMultiMap(bool[] trkselect) {
    //  if (trkselect == null) return;
    //  //bool[] mutedtracks = new bool[trkselect.Length];
    //  //for (int i = 0; i < trkselect.Length; i++) mutedtracks[i] = !trkselect[i];
    //  if (P.F.frmNoteMap != null && !P.F.frmNoteMap.IsDisposed) P.F.frmNoteMap.Close();
    //  CSVFileMultiMap = new clsFileStream(FileName, false, 0, trkselect, false);
    //  P.F.CloseFrm(P.F.frmMultiMap);
    //  P.F.frmMultiMap = new Forms.frmMultiMap(CSVFileMultiMap, true, trkselect);
    //  P.F.frmMultiMap.Show();
    //}

    //internal void WriteChord(bool[] notes, string line, int? bass) {
    //  line = GetChordLine(notes, line, bass);
    //  xsw.WriteLine(line);
    //  xsw.Flush();
    //}

    //internal void WriteLine(string line) {
    //  xsw.WriteLine(line);
    //  xsw.Flush();
    //}

    //internal void WriteBasicComments() {
    //  xsw.WriteLine("*created on " + System.DateTime.Now);
    //  string line;
    //  line = "*using tracks";
    //  //for (int ch = 0; ch < 16; ch++) {
    //  for (int ch = 0; ch < CSVFileConv.TrkSelect.Length; ch++) {
    //    if (CSVFileConv.TrkSelect[ch]) line += " " + (ch + 1);
    //  }
    //  xsw.WriteLine(line);
    //}

    //internal void WriteHeader(StreamWriter xsw) {
    //  //write header
    //  clsMTime.clsBBT bbt0 = new clsMTime.clsBBT(0);
    //  xsw.WriteLine(bbt0.TSig.Txt
    //    + " " + P.F.MTime.TicksPerQNote  //was tickperbeat???
    //    + " " + P.F.CSVFileConv.Keys[0].KeyNoteStr + " " 
    //    + P.F.CSVFileConv.Keys[0].Scale);
    //  //* write 2nd and subsequent tsigs  
    //  clsCF.WriteBBTTSigs(xsw);
    //}

    //internal void SaveTextFile() {
    //  MemoryStream xstream = P.F.LoadCSV.xstream;
    //  StreamWriter xsw = P.F.LoadCSV.xsw;
    //  Stream filestream = new FileStream(TxtFileName, FileMode.Create, FileAccess.Write);
    //  xsw.Flush();
    //  xstream.Seek(0, SeekOrigin.Begin);
    //  xstream.CopyTo(filestream);
    //  filestream.Close();
    //}

    //internal void WriteLines(string[] lines) {
    //  Stream filestream = new FileStream(TxtFileName, FileMode.Create, FileAccess.Write);
    //  StreamWriter xsw = new StreamWriter(filestream);
    //  foreach (string line in lines) xsw.WriteLine(line);
    //  xsw.Close();
    //}
  }
}
