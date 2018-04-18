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

namespace ChordCadenza {
  internal class clsSaveMidiFile {
    //private string FilePath;
    private clsFileStream FS;
    private clsMWriter MWriterFile;
    internal clsMWriter MWriterTrk;
    private byte RunningStatus = 0;  //usually >= 0x80
    private FileStream stFile;
    private MemoryStream stTrk;
    //private Forms.frmCfgChords FrmCfgChords;

    //private bool indChordRoot {
    //  get { return P.frmSaveMidiFileAs?.chkChordRoot.Checked ?? false; }
    //}

    //private int intChordChan {
    //  get { return (int)(P.frmSaveMidiFileAs?.nudChordChan.Value ?? 16) - 1; }
    //}

    //private bool indSaveChordLabels {
    //  get { return P.frmSaveMidiFileAs?.chkSaveChordLabels.Checked ?? false; }
    //}

    //private bool indSaveMutedTracks {
    //  get { return P.frmSaveMidiFileAs?.chkSaveMutedTrks.Checked ?? true; }
    //}

    //private string txtChordTrackTitle {
    //  get { return P.frmSaveMidiFileAs?.txtChordTrackTitle.Text ?? "Chords"; }
    //}

    internal clsSaveMidiFile(clsFileStream fs) {
      FS = fs;
      //FrmCfgChords = frmcfgchords;
    }

    //internal static void SaveMidiFile(string midifilepath) {
    //  Cursor.Current = Cursors.WaitCursor;
    //  clsSaveMidiFile savemidifile = new clsSaveMidiFile(P.F.FSTrackMap);
    //  bool res = savemidifile.Save(midifilepath);
    //  Cursor.Current = Cursors.Default;
    //  if (!res) MessageBox.Show("MidiFile not saved");
    //}

    internal string SaveAs() {
      try {
        string dir = Path.GetDirectoryName(P.F.Project.MidiPath);
        if (!Directory.Exists(dir)) throw new ChordFileException("Directory: " + dir + " not found");

        //* set up dialog
        SaveFileDialog sfdMidi = P.frmStart.sfd;
        sfdMidi.InitialDirectory = dir;
        string txtfilename = Path.GetFileName(P.F.Project.MidiPath);
        sfdMidi.FileName = txtfilename;
        sfdMidi.Filter = "MidiFiles|*.mid";
        sfdMidi.FilterIndex = 1;
        sfdMidi.RestoreDirectory = false;
        sfdMidi.Title = "Save MidiFile";

        //* run dialog
        if (sfdMidi.ShowDialog() != DialogResult.OK) return "Save MidiFile cancelled";

        //* process result
        return Save(sfdMidi.FileName);
      }
      catch (Exception exc) {
        return "Error saving MidiFile: " + exc.Message;
      }
    }

    internal string Save(string path) {
      //string msg = OpenFile(path);
      //if (msg != "") return msg;
      OpenFile(path);
      WriteHeader();
      WriteCondTrk();
      foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {
        if (GetindSaveTrack(trk)) WriteTrack(trk);
      }
      //if (P.frmSaveMidiFileAs.chkSaveRecTrk.Checked) WriteRecTrk();
      if (P.frmSaveMidiFileAs.chkSaveChordLabels.Checked || P.frmSaveMidiFileAs.chkSaveChordTrack.Checked) WriteChordTrack();
      MWriterFile.Close();
      FS.indSave = false;
      return "";
    }

    private void OpenFile(string path) {
      stFile = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
      BinaryWriter br = new BinaryWriter(stFile);
      MWriterFile = new clsMWriter(br);
      //return "";
    }

    internal void CheckStatus(byte status) {
      if (status != RunningStatus) {
        MWriterTrk.WriteByte(status);
        RunningStatus = status;
      }
    }

    internal void SystemStatus(byte status, int len) {  //cancels running status 
      MWriterTrk.WriteByte(status);  //0xf0, 0xf7
      MWriterTrk.WriteIntV(len);
      RunningStatus = 0;
    }

    internal void MetaStatus(byte type, byte len) {  //cancels running status 
      MWriterTrk.WriteByte(0xff);  
      MWriterTrk.WriteByte(type);
      MWriterTrk.WriteIntV(len);  //variable length len field
      RunningStatus = 0;
    }

    private bool GetindSaveTrack(clsTrks.T trk) {
      if (FS.OnCount[trk] == 0 && FS.Title[trk].Length == 0) return false;
      //if (trk == FS.NumTrks - 1 && FS.OnCount[trk] == 0) return false;  //last (recording) trk
      if (!P.frmSaveMidiFileAs.chkSaveMutedTrks.Checked && P.F.Mute[trk]) return false;
      //if (!P.frmSaveMidiFileAs.chkSaveEmptyTrks.Checked && FS.OnCount[trk] == 0) return false;
      return true;
    }

    private void WriteHeader() {
      int trkcnt = 0;
      if ((P.frmSaveMidiFileAs.chkSaveChordLabels.Checked || P.frmSaveMidiFileAs.chkSaveChordTrack.Checked)
      && P.F?.CF?.Evs != null && P.F.CF.Evs.Count > 0) trkcnt++;
      //if (P.frmSaveMidiFileAs.chkSaveRecTrk.Checked 
      //&& FS?.RecStrmNew != null && FS.RecStrmNew.Count > 0) trkcnt++;
      foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {
        if (GetindSaveTrack(trk)) trkcnt++;
      }
      MWriterFile.WriteString("MThd");
      MWriterFile.WriteUIntF(6, 4);  //length 6
      MWriterFile.WriteUIntF(1, 2);  //format 1
      MWriterFile.WriteUIntF(((uint)trkcnt + 1), 2);   //incl. cond trk 
      MWriterFile.WriteUIntF((uint)P.F.MTime.TicksPerQNote, 2);  
    }

    private void WriteCondTrk() {
      List<clsFileStream.clsEvStrm> listev = new List<clsFileStream.clsEvStrm>();
      using (stTrk = new MemoryStream()) {
        BinaryWriter br = new BinaryWriter(stTrk);
        MWriterTrk = new clsMWriter(br);

        //* get current conductor track 
        //* should not incl. keys or tsigs
        //* should not incl. tempos
        foreach (clsFileStream.clsEvStrm ev in FS.Strm) {
          if (ev.Trk == null && ev.indWrite) listev.Add(ev);
        }

        //* save keys
        foreach (clsKeyTicks key in P.F.Keys.Keys) {
          //sbyte midikey = (sbyte)key.MidiKey;
          byte[] data = new byte[] { (byte)key.MidiKey, (byte)key.MajMin };
          clsFileStream.clsEvStrm ev = new clsFileStream.clsEvMeta(0, key.Ticks, -1, 0x59, data);
          int index = listev.BinarySearch(ev);
          if (index < 0) index = ~index;
          listev.Insert(index, ev);
        }

        //* save time signatures
        foreach (clsMTime.clsTSigBB tsig in P.F.MTime.TSigs) {
          byte[] data = new byte[] { (byte)tsig.NN, (byte)tsig.DDMidi, (byte)tsig.MidiClocksPerMetClick, (byte)8 };
          clsFileStream.clsEvStrm ev = new clsFileStream.clsEvMeta(0, tsig.Tick, -1, 0x58, data);
          int index = listev.BinarySearch(ev);
          if (index < 0) index = ~index;
          listev.Insert(index, ev);
        }

        //* save tempos
        //clsFileStream.clsMap<int> tempomap = null;
        clsMap<int> tempomap = null;
        if (P.F?.FSTrackMap?.TempoMap != null) tempomap = P.F.FSTrackMap.TempoMap;
        else if (P.F?.FileStreamConv?.TempoMap != null) tempomap = P.F.FileStreamConv.TempoMap;
        if (tempomap != null) {
          //for (int i = 0; i < tempomap.Count; i++) {
          //  int ticks = tempomap.KeyByIndex(i);
          //  int tempo = tempomap.ValByIndex(i);
          foreach (KeyValuePair<int, int> pair in tempomap) {
            int ticks = pair.Key;
            int tempo = pair.Value;
            byte[] data = clsFileStream.ConvUIntF((uint)tempo, 3);
            clsFileStream.clsEvStrm ev = new clsFileStream.clsEvMeta(0, ticks, -1, 0x51, data);
            int index = listev.BinarySearch(ev);
            if (index < 0) index = ~index;
            listev.Insert(index, ev);
          }
        }

        //* write to trk stream
        int prevticks = 0;
        foreach (clsFileStream.clsEvStrm ev in listev) {
          if (ev is clsFileStream.clsEvBeat) continue;
          WriteStrmMsg(ref prevticks, ev);
        }
        WriteToFileStream(prevticks);
      }
    }

    private void WriteChordTrack() {
      //* contains labels and/or midinotes
      //* last track in midi file
      if (P.F?.CF?.Evs == null || P.F.CF.Evs.Count == 0) return;
      int prevticks = 0;
      using (stTrk = new MemoryStream()) {
        BinaryWriter br = new BinaryWriter(stTrk);
        MWriterTrk = new clsMWriter(br);
        string strtitle = (P.frmSaveMidiFileAs.txtChordTrackTitle.Text == "") ? 
          "Chords" : P.frmSaveMidiFileAs.txtChordTrackTitle.Text;
        byte[] bytestitle = Encoding.ASCII.GetBytes(strtitle);
        clsFileStream.clsEvTitle evtitle = new clsFileStream.clsEvTitle(0, 0, -1, bytestitle); //trk param not used
        evtitle.WriteTitle(this, 0);
        foreach (clsCF.clsEv evcf in P.F.CF.Evs) {
          if (P.frmSaveMidiFileAs.chkSaveChordLabels.Checked) {
            byte[] bytestext = Encoding.ASCII.GetBytes(evcf.ChordName(eKBTrans.None, kbtranskey: false));
            clsFileStream.clsEvStrm evstrm = new clsFileStream.clsEvMeta(0, evcf.OnTime, -1, 0x01, bytestext);
            WriteStrmMsg(ref prevticks, evstrm);
          }
          if (P.frmSaveMidiFileAs.chkSaveChordTrack.Checked) {
            int status = 0x90 | (int)(P.frmSaveMidiFileAs.nudChordChan.Value - 1);
            WriteChord(ref prevticks, evcf, status, evcf.OnTime, 80);  //ON evs of chord
            WriteChord(ref prevticks, evcf, status, evcf.OffTime, 0);  //OFF evs of chord
          }
        }
        WriteToFileStream(prevticks);
      }
    }

    private void WriteChord(ref int prevticks, clsCF.clsEv evcf, int status, int ticks, byte vel) {
      foreach (clsCF.clsEv.clsNote note in evcf.Notes) {  //write ON evs
        int pc = note.PC[eKBTrans.None] + 60;   //octave above middle C
        if (P.frmSaveMidiFileAs.chkChordRoot.Checked 
          && evcf.Root && evcf.Notes.Length > 1 
          && pc < evcf.Notes[0].PC[eKBTrans.None] + 60) pc += 12;
        clsFileStream.clsEvStrm evstrm = new clsFileStream.clsEvShort(0, ticks, -1, (byte)status, (byte)pc, vel);
        WriteStrmMsg(ref prevticks, evstrm);
      }
    }

    private void WriteToFileStream(int prevticks) {
      clsFileStream.clsEvMeta eveof = new clsFileStream.clsEvMeta(int.MaxValue, prevticks, -1, 0x2f, new byte[0]);
      eveof.WriteMsg(this, 0);
      stTrk.Flush();
      MWriterFile.WriteString("MTrk");
      MWriterFile.WriteUIntF((uint)stTrk.Length, 4);
      stTrk.WriteTo(stFile);  //write trkstream to filestream
    }

    private void WriteStrmMsg(ref int prevticks, clsFileStream.clsEvStrm evstrm) {
      if (evstrm is clsFileStream.clsEvBeat) return;
      int delta = evstrm.Ticks - prevticks;
      prevticks = evstrm.Ticks;
      evstrm = evstrm.Transpose();
      evstrm.WriteMsg(this, delta);
      //return delta;
    }

    private void WriteTrack(clsTrks.T trk) {
      int prevticks = 0;
      using (stTrk = new MemoryStream()) {
        BinaryWriter br = new BinaryWriter(stTrk);
        MWriterTrk = new clsMWriter(br);
        string strtitle = (P.F.frmTrackMap == null || P.F.frmTrackMap.txtTitles[trk] == null) ?
          FS.Title[trk] : P.F.frmTrackMap.txtTitles[trk].Text;
        byte[] bytestitle = Encoding.ASCII.GetBytes(strtitle);
        clsFileStream.clsEvTitle evtitle = new clsFileStream.clsEvTitle(0, 0, -1, bytestitle);  //trk param not used
        evtitle.WriteTitle(this, 0);

        //* write overriding controllers (vol, pan, patch)
        clsFileStream.clsEvShort evshort;
        bool indvol = false, indpan = false, indpatch = false; 
        if (P.F.Chan[trk] >= 0) {
          int chan = P.F.Chan[trk];
          byte vol = (byte)P.F.Vol[chan];
          if (vol <= 127) {
            indvol = true;
            evshort = new clsFileStream.clsEvShort(0, trk.TrkNum, (byte)(0xb0 | chan), 7, vol);
            WriteStrmMsg(ref prevticks, evshort);
          }
          byte pan = (byte)P.F.Pan[chan];
          if (pan <= 127) {
            indpan = true;
            evshort = new clsFileStream.clsEvShort(0, trk.TrkNum, (byte)(0xb0 | chan), 10, pan);
            WriteStrmMsg(ref prevticks, evshort);
          }
          byte patch = (byte)P.F.Patch[chan];
          if (patch <= 127) {
            indpatch = true;
            evshort = new clsFileStream.clsEvShort(0, 0, trk.TrkNum, (byte)(0xc0 | chan), patch);
            WriteStrmMsg(ref prevticks, evshort);
          }
        }

        foreach (clsFileStream.clsEvStrm ev in FS.Strm) {
          if (ev.Trk != trk) continue;
          if (ev is clsFileStream.clsEvBeat) continue;
          if (ev is clsFileStream.clsEvShort) {
            clsFileStream.clsEvShort evsh = (clsFileStream.clsEvShort)ev;
            int status = evsh.Status & 0xf0;
            if (indvol && status == 0xb0 && evsh.Msg == 7) continue;  //vol overriden
            if (indpan && status == 0xb0 && evsh.Msg == 10) continue;  //pan overriden
            if (indpatch && status == 0xc0) continue;  //patch overriden
          }
          WriteStrmMsg(ref prevticks, ev);
        }
        WriteToFileStream(prevticks);
      }
    }

    //private void WriteRecTrk() {  //new
    //  //* get channel
    //  //* find unused chan
    //  if (FS?.RecStrmNew == null || FS.RecStrmNew.Count == 0) return;

    //  //int recchan = FS.GetRecChan();

    //  int prevticks = 0;
    //  using (stTrk = new MemoryStream()) {
    //    BinaryWriter br = new BinaryWriter(stTrk);
    //    MWriterTrk = new clsMWriter(br);
    //    string strtitle = (P.frmSaveMidiFileAs.txtRecTrackTitle.Text == "") ?
    //      "Chord Cadenza" : P.frmSaveMidiFileAs.txtRecTrackTitle.Text;
    //    byte[] bytestitle = Encoding.ASCII.GetBytes(strtitle);
    //    clsFileStream.clsEvTitle evtitle = new clsFileStream.clsEvTitle(0, -1, bytestitle);  //trk param not used
    //    evtitle.WriteTitle(this, 0);

    //    foreach (clsFileStream.clsEvShort ev in FS.RecStrmNew) {
    //      //if ((ev.Status & 0x0f) == 0) {
    //      //  ev.Status = (byte)(ev.Status | recchan);  //chan0 -> recchan
    //      //} else {
    //      //  LogicError.Throw(eLogicError.X142);
    //      //}
    //      WriteStrmMsg(ref prevticks, ev);
    //    }
    //    WriteToFileStream(prevticks);
    //  }
    //}

    internal class clsMWriter {
      private BinaryWriter br;

      internal clsMWriter(BinaryWriter xbr) {
        br = xbr;
      }

      internal void WriteString(string str) {
        ASCIIEncoding xenc = new ASCIIEncoding();
        br.Write(xenc.GetBytes(str));
      }

      internal void WriteByte(byte b) {
        br.Write(b);
      }

      internal void WriteBytes(byte[] b) {
        br.Write(b);
      }

      internal void WriteUIntF(uint ui, int len) {
        //convert uint to 1-4 byte array and write
        byte[] xbytes = clsFileStream.ConvUIntF(ui, len);
        //if (len > 4 || len < 1) {
        //  throw new FatalException();
        //}
        //byte[] xbytes = new byte[len];
        //for (int i = len - 1; i >= 0; i--) {
        //  xbytes[i] = (byte)(ui & 0xff);
        //  ui >>= 8;
        //}
        br.Write(xbytes);
      }

      internal void WriteIntSByte(int i) {
        //convert signed int to signed byte and write
        br.Write((sbyte)i);
      }

      internal void WriteIntV(int zint) {
        //* convert integer to variable length number
        //* copied from C code in standard midi files 1.0 spec 
        int xbuffer = zint & 0x7f;
        while ((zint >>= 7) > 0) {
          xbuffer <<= 8;
          xbuffer |= 0x80;
          xbuffer += (zint & 0x7f);
        }
        while (true) {
          br.Write((byte)(xbuffer & 0xff));
          if ((xbuffer & 0x80) != 0) xbuffer >>= 8; else break;
        }
      }

      internal void Close() {
        br.Close();
      }
    }

  }
}