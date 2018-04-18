using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ChordCadenza {
  internal class clsLoadMidiFile {
    //load midifile to csv file
    //internal string FileName;
    protected uint NumMidiFileTrks;  //incl. conductor trk
    //internal int NumTrks;  //excl. conductor trk
    protected clsMReader MReaderFile;
    protected clsMReader MReaderTrk;
    internal int Format;
    private bool TPQNConv = false;
    private int TPQNMult = 1;
    private int TPQNDiv = 1;
    private int MaxMidiTicks = 0;

    internal clsLoadMidiFile(clsFileStream filestream) {  //readheader only
      //P.F.MidiFilePath = filename;
      P.frmSC.cmdMultiMap.Enabled = true;
      if (P.F.frmChordMap != null) P.F.frmChordMap.cmdMultiMap.Enabled = true;
      FileStream = filestream;
      clsTrks.Array<List<clsFileStream.clsOO>> oo;
      if (!OpenFile() || !ReadHeader(true, out oo)) {
        MessageBox.Show("MidiFile Read Error");
        throw new MidiFileException();
      }
    }

    internal clsLoadMidiFile(clsFileStream filestream, 
    string filename, clsTrks.Array<bool> trkselect, bool excl10, bool firsttime) {
      bool condtrkempty = P.F.CondTrkEmpty;
      //P.F.Project.MidiPath = filename;
      P.frmSC.cmdMultiMap.Enabled = true;
      if (P.F.frmChordMap != null) P.F.frmChordMap.cmdMultiMap.Enabled = true;
      FileStream = filestream;
      //FileStream.Summary = summary;
      //FileStream.Transpose = transpose;
      if (trkselect != null) FileStream.TrkSelect = trkselect;
      Excl10 = excl10;
      if (!ReadFile(firsttime)) {
        if (condtrkempty && !P.F.CondTrkEmpty) {  //non-empty conductor track found
          condtrkempty = false;
          if (!ReadFile(firsttime)) ReadError();
        } else {
          ReadError();
        }
      }
    }

    private static void ReadError() {
        MessageBox.Show("MidiFile Read Error");
        throw new MidiFileException();
    }

    protected bool OpenFile() {
      try {
        if (P.F.MidiFileBuffer == null) {  //read file from disk
          if (!File.Exists(P.F.Project.MidiPath)) {
            MessageBox.Show("Midifile " + P.F.Project.MidiPath + " not found");
            return false;
          }
          FileStream stfile = new FileStream(P.F.Project.MidiPath, FileMode.Open, FileAccess.Read, FileShare.Read);
          //BinaryReader brfile = new BinaryReader(stfile);
          int len = (int)stfile.Length;
          P.F.MidiFileBuffer = new byte[len];
          stfile.Read(P.F.MidiFileBuffer, 0, len);
        }
        MemoryStream stmem = new MemoryStream(P.F.MidiFileBuffer, false);
        BinaryReader brmem = new BinaryReader(stmem);
        MReaderFile = new clsMReader(brmem, stmem);
        return true;
      }
      catch (Exception exc) {
        MessageBox.Show("Error loading Midifile: " + exc.Message);
        return false;
      }
      //catch (FileNotFoundException ex) {
      //  MessageBox.Show("Midifile Open Error: " + ex.Message);
      //  return false;
      //}
      //catch (DirectoryNotFoundException ex) {
      //  MessageBox.Show("Midifile Open Error: " + ex.Message);
      //  return false;
      //}
    }

    protected bool ReadHeader(bool firsttime, out clsTrks.Array<List<clsFileStream.clsOO>> oo) {
      oo = null;
      string head = MReaderFile.ReadStr(4);
      if (head != "MThd") {
        MessageBox.Show("Invalid Midi Header: " + head);
        return false;
      }
      uint len = MReaderFile.ReadUIntF(4);
      if (len != 6) {
        MessageBox.Show("Invalid Midi Header Length: " + len);
        return false;
      }
      Format = (int)MReaderFile.ReadUIntF(2);
      if (Format != 1 && Format != 0) {
        MessageBox.Show("Unsupported Midi File Format: " + Format);
        return false;
      }
      if (Format == 0) {
        //if (FileStream.Summary) MessageBox.Show("Format 0 file - creating 16 tracks");
        P.F.CondTrkEmpty = false;
      }

      //if (P.F.NumNewTrks < 0) P.F.NumNewTrks = (int)P.frmStart.nudAddedTrks.Value;  //for recording
      //int numnewtrks = (int)P.frmStart.nudAddedTrks.Value;
      //int numnewtrks = 0;
      NumMidiFileTrks = MReaderFile.ReadUIntF(2);  //includes conductor trk, excludes header
      P.MMSW?.WriteLine("    File Format: " + Format);
      P.MMSW?.WriteLine("    NumMidiFileTrks = " + NumMidiFileTrks);
      if (clsTrks.T.TrkOOR(NumMidiFileTrks)) {
        string msg = "Number of Midi Tracks (" + NumMidiFileTrks + ") exceeds limit of " + clsTrks.NumTrksMax;
        MessageBox.Show(msg);
        return false;
      }
      bool trksok = false;
      if (P.F.CondTrkEmpty) {
        //NumTrks = (int)(NumMidiTrks);  //excludes header and conductor trk, incl. empty recording trk
        trksok = P.F.Trks.SetNumTrks((int)(NumMidiFileTrks - 1));  //excludes header and conductor trk, incl. empty recording trk
      } else {
        if (Format == 0) trksok = P.F.Trks.SetNumTrks(16);  //one track per channel
        //else NumTrks = (int)NumMidiTrks + 1;  //excludes header, incl. conductor trk with channel data & empty recording trk
        else trksok = P.F.Trks.SetNumTrks((int)NumMidiFileTrks);  //excludes header, incl. conductor trk with channel data & empty recording trk
        if (!trksok) {
          LogicError.Throw(eLogicError.X146);
          return false;
        }
      }

      oo = new clsTrks.Array<List<clsFileStream.clsOO>>(delegate() { return new List<clsFileStream.clsOO>(500); });  //[trk][seq]
      //oo = clsTrks.Array<List<clsFileStream.clsOO>>.NewClass(delegate () { return new List<clsFileStream.clsOO>(500); });

      //foreach (clsTrks.T trk in oo.Next) oo[trk] = new List<clsFileStream.clsOO>(500);

      //* new method
      int tpqnmidifile = (int)MReaderFile.ReadUIntF(2);
      if (tpqnmidifile >= 0x8000) {
        MessageBox.Show("Unsupported time-code-based time in midi header");
        return false;
      }
      if (firsttime) {
        P.frmStart.TraceLoad("initialize mtime 4/4 from midi header");
        P.F.MTime = new clsMTime(tpqnmidifile, 4, 4);  //tsig 4/4 default
        P.F.BarPaneBBTLo = new clsMTime.clsBBT(0);
        P.F.BarPaneBBTHi = new clsMTime.clsBBT(0);
        //GetTicksPerQI();  //default (if no tsig in midi file)
      } else P.frmStart.TraceLoad("bypass mtime from midi header");
      if (P.F.CurrentBBT == null) P.F.CurrentBBT = new clsMTime.clsBBT(0);
      // else P.F.CurrentBBT = new clsMTime.clsBBT(P.F.CurrentBBT.Ticks);  //using current P.F.MTime (4/4)
      return true;
    }

    protected void InitTrack() {
      string head = MReaderFile.ReadStr(4);
      if (head != "MTrk") {
        MessageBox.Show("Unexpected Track Header: " + head);
        throw new MidiFileException();
      }
      int trklen = (int)MReaderFile.ReadUIntF(4);
      Stream sttrk = new MemoryStream(trklen); 
      byte[] trkbytes = MReaderFile.ReadBytes(trklen);
      if (trkbytes.Length < trklen) {
        MessageBox.Show("Invalid trk length");
        throw new MidiFileException();
      }
      sttrk.Write(trkbytes, 0, trklen);
      sttrk.Seek(0, SeekOrigin.Begin);
      BinaryReader brtrk = new BinaryReader(sttrk);
      MReaderTrk = new clsMReader(brtrk, sttrk);
    }

    protected class clsMReader {
      protected BinaryReader br;
      protected Stream st;

      internal clsMReader(BinaryReader xbr, Stream xst) {
        br = xbr;
        st = xst;
      }

      internal void Locate(int offset) {
        st.Seek(offset, SeekOrigin.Current); 
      }

      internal string ReadStr(int zlen) {
        byte[] xbytes = new byte[zlen];
        br.Read(xbytes, 0, zlen);
        ASCIIEncoding xenc = new ASCIIEncoding();
        return xenc.GetString(xbytes);
      }

      internal uint ReadUIntF(int zlen) {  //read 1-4 byte array to uint from binaryreader
        if (zlen > 4 || zlen < 1) throw new MidiFileException();
        byte[] xbytes = new byte[zlen];
        br.Read(xbytes, 0, zlen);
        uint xint = 0;
        for (int xi = 0; xi < xbytes.Length; xi++) {
          xint <<= 8;
          xint += xbytes[xi];
        }
        return xint;
      }

      internal int ReadIntSByte() {  
        //read signed byte and convert to integer
        return br.ReadSByte();
      }

      internal int ReadIntV() {
        //* convert variable length number to integer (deltatime)
        //* converted from C code in standard midi files 1.0 spec
        //* return -1 if end of stream
        byte xbyte;
        try {
          int xint = br.ReadByte();
          if (xint < 0x80) return xint;
          xint &= 0x7f;
          do {
            xbyte = br.ReadByte();
            xint = (xint << 7) + (xbyte & 0x7f);
          } while (xbyte >= 0x80);
          return xint;
        }
        catch (EndOfStreamException) {
          return -1;
        }
      }

      internal byte ReadByte() {
        return br.ReadByte();
      }

      internal byte[] ReadBytes(int len) {
        return br.ReadBytes(len);
      }

      internal void Close() {
        br.Close();
      }
    }

    internal clsFileStream FileStream;
    private readonly bool Excl10;

    internal bool ReadFile(bool firsttime) {
      if (!OpenFile()) return false;
      clsTrks.Array<List<clsFileStream.clsOO>> oo;
      if (!ReadHeader(firsttime, out oo)) return false;  
      FileStream.OnCount = new clsTrks.Array<int>(0);
      FileStream.OnCountX10 = new clsTrks.Array<int>(0);
      FileStream.ChanOnCount = new clsTrks.Array<int[]>(delegate() { return new int[16]; });
      FileStream.ChanAllCount = new clsTrks.Array<int[]>(delegate() { return new int[16]; });
      FileStream.TrkMaxPitch = new clsTrks.Array<int>(0);
      FileStream.TrkMinPitch = new clsTrks.Array<int>(127);
      FileStream.indPitchBend = new clsTrks.Array<bool>(false);
      FileStream.TrkType = new clsTrks.Array<clsFileStream.eTrkType>(clsFileStream.eTrkType.NoStyle);
      FileStream.ChordNeg = new clsTrks.Array<float>(-1f);
      FileStream.Poly = new clsTrks.Array<float>(-1f);
      //FileStream.PitchBend = new clsPitchBend[NumTrks];
      //FileStream.Title = clsTrks.Array<string>.NewClass(delegate () { return ""; });
      FileStream.Title = new clsTrks.Array<string>("");

      if (FileStream.TrkSelect == null) {
        FileStream.TrkSelect = new clsTrks.Array<bool>(true);
      }
      P.F._MidiKeys = new clsKeysTicks(0, "major");
      //FileStream.MidiFileKeys = new clsKeys(0, "major");  

      //* NumMidiTrks excludes header, includes conductor trk
      if (P.F.CondTrkEmpty) {  //not format 0
        for (int midifiletrk = 0; midifiletrk < NumMidiFileTrks; midifiletrk++) {
          if (!ReadTrack(midifiletrk - 1, firsttime, oo)) {
            P.F.CondTrkEmpty = false;
            FileStream.TrkSelect = null;  //force reinit with new NumTrks
            Debug.WriteLine("Channel events found in conductor track - re-reading midi file");
            return false;
          }
        }
      } else {
        for (int midifiletrk = 0; midifiletrk < NumMidiFileTrks; midifiletrk++) {
          if (!ReadTrack(midifiletrk, firsttime, oo)) {
            throw new MidiFileException();
          }
        }
      }

      //* check for empty trk containing midi events
      //* and for non-empty track that doesn't set chan/pan/vol
      //* (SONAR appears to put channel pan & vol on synth/audio trk)
      //if (FileStream.Summary) {
      //if (firsttime) {
      foreach (clsTrks.T trk in FileStream.OnCount.Next) {
        if (FileStream.OnCount[trk] > 0) {
          FileStream.SetChan(trk);
        } else {
          if (P.MMSW != null) FileStream.CheckChan(trk);
        }
      }
      //}

      MReaderFile.Close();
      //if (FileStream.Keys.Keys.Count == 0) FileStream.Keys.Add(0, "major", 0);
      //if (!FileStream.Summary) FileStream.CreateData(NumTrks);
      FileStream.CreateData(oo, MaxMidiTicks);
      return true;
    }

    protected bool ReadTrack(int midifiletrk, bool indtsigs, clsTrks.Array<List<clsFileStream.clsOO>> oo) {  //trk0 = first non-conductor trk
      //* return false if midi trk data in trk0 (eg midi ON, OFF, patch, ...)
      InitTrack();
      //if (trk >= 0) FileStream.OO[trk] = new List<clsFileStream.clsOO>();
      clsTrks.T trk = (midifiletrk >= 0) ? new clsTrks.T(P.F.Trks, midifiletrk) : null;
      FileStream.StrmLL.ResetCurrentLLN();
      int delta = MReaderTrk.ReadIntV();
      int ticks = 0;
      byte status = 0;  //usually >= 0x80
      int chan = -1;
      int seq = -1;
      //bool sustainon = false;  //set only only if P.frmStart.chkMidiFileSustain.Checked
      //bool[] sustained = new bool[128];  //set only only if P.frmStart.chkMidiFileSustain.Checked
      while (delta >= 0) {  //not EOF
        seq++;
        ticks += delta;
        if (TPQNConv) ticks = (ticks * TPQNMult) / TPQNDiv;   //should normally not need rounding
        byte b = MReaderTrk.ReadByte();
        if (b >= 0x80) {  //new status
          status = b;
          chan = -1;
          if (status < 0xf0) {
            chan = status & 0x0f;
            b = MReaderTrk.ReadByte();  //not sysex or metaev
          }
          if (Format == 0) {
            midifiletrk = chan;
            if (chan < 0) trk = new clsTrks.T(P.F.Trks, 0);
            else trk = new clsTrks.T(P.F.Trks, midifiletrk);
          }
        }
        switch (status & 0xf0) {
          case 0x90:  //ON
          case 0x80:  //OFF
            //* 2 data bytes
            if (midifiletrk < 0) return false;
            //int chan = status & 0x0f;
            //byte pitch = (byte)(b + FileStream.Transpose);
            byte pitch = b;
            byte vel = MReaderTrk.ReadByte();
            bool on = (status & 0xf0) == 0x90 && (vel > 0);

            //* throw new TestException();

            if (on) FileStream.UpdateChanOnTotals(trk, chan, pitch);
            FileStream.ChanAllCount[trk][chan]++;
            FileStream.StrmLL.InsertShortEv(seq, ticks, midifiletrk, status, b, vel);
            if (!Excl10 || chan != 9) {
              oo[new clsTrks.T(P.F.Trks, midifiletrk)].Add(new clsFileStream.clsOO(
              ticks, on, chan, pitch, vel, P.F.TicksPerQI));
            }
            break;
          case 0xa0:  //poly key pressure
          case 0xb0:  //control change
          case 0xe0:  //pitch wheel
            //* 2 data bytes
            //if (trk < 0) return true;
            if (midifiletrk < 0) {  //ignore
              MReaderTrk.ReadByte();
              break;
            }
            byte c = MReaderTrk.ReadByte();
            if (FileStream.Title[trk].StartsWith("Cakewalk TTS-1")) break;  //kludge to get around Sonar bug
            if ((status & 0xf0) == 0xe0) {  //pitchbend
              if (b != 0 || c != 64) FileStream.indPitchBend[trk] = true;  //not middle value
            }
            //else if (P.frmStart.chkMidiFileSustain.Checked && (!Excl10 || chan != 9)) {
            //  if ((status & 0xf0) == 0xb0 && b == 64) {  //sustain 
            //    sustainon = (c > 64);
            //    if (!sustainon) { //sustainon -> sustainoff
            //      for (int i = 0; i < 128; i++) {
            //        if (sustained[i]) {
            //          FileStream.OO[trk].Add(new clsFileStream.clsOO(ticks, false, chan, i, 0, P.F.TicksPerQI));
            //          sustained[i] = false;
            //        }
            //      }
            //    }
            //  }
            //}
            FileStream.StrmLL.InsertShortEv(seq, ticks, midifiletrk, status, b, c);
            FileStream.ChanAllCount[trk][status & 0x0f]++;
            break;
          case 0xc0:  //program change (patch)
            //* 1 data byte
            //if (trk < 0) return false;
            if (midifiletrk < 0) break;  //ignore
            if (FileStream.Title[trk].StartsWith("Cakewalk TTS-1")) break;  //kludge to get around Sonar bug
            //if (!FileStream.Summary) FileStream.InsertShortEv(ticks, trk, status, b);
            FileStream.StrmLL.InsertShortEv(seq, ticks, midifiletrk, status, b);
            FileStream.ChanAllCount[trk][status & 0x0f]++;
            break;
          case 0xd0:  //channel pressure
            //if (trk < 0) return false;
            if (midifiletrk < 0) break;
            //* 1 data byte
            //if (!FileStream.Summary && FileStream.TrkSelect[trk]) {
            //if (!FileStream.Summary) {
              FileStream.StrmLL.InsertShortEv(seq, ticks, midifiletrk, status, b);
            //}
            FileStream.ChanAllCount[trk][status & 0x0f]++;
            break;
          case 0xf0:  //system common messages
            int len;
            switch (status) {
              case 0xf0:  //sysex start
              case 0xf7:  //sysex end
                len = MReaderTrk.ReadIntV();
                byte[] sysdata = MReaderTrk.ReadBytes(len);
                FileStream.StrmLL.InsertSystemEv(seq, ticks, midifiletrk, status, sysdata);
                status = 0;  //sysex cancels running status
                break;
              case 0xff:  //meta event
                status = 0;  //metaev cancels running status
                byte type = MReaderTrk.ReadByte();
                len = MReaderTrk.ReadIntV();
                byte[] metadata;
                switch (type) {
                  case 0x03:  //title
                    metadata = MReaderTrk.ReadBytes(len);
                    string title = Encoding.Default.GetString(metadata, 0, metadata.Length);
                    if (midifiletrk == -1) FileStream.ProjectTitle = title;
                    else FileStream.Title[trk] = title;
                    FileStream.StrmLL.InsertTitleEv(seq, ticks, midifiletrk, metadata);
                    break;
                  //case 0x21:  //device (port) number
                  //  if (len != 1) throw new MidiFileException();
                  //  int pp = (int)MReaderTrk.ReadUIntF(1);
                  //  if (pp != 0) Debug.WriteLine("MidiPortNumber: " + pp + " found - ignored");
                  //  break;
                  //case 0x09:  //device (port) name
                  //  //MReaderTrk.ReadBytes(len);  //ignore metaev  
                  //  break;
                  case 0x51:  //tempo
                    ////if (!IsCondTrk(midifiletrk)) {
                    ////  MReaderTrk.ReadBytes(len);  //ignore 
                    ////  Debug.WriteLine("Invalid tempo found on track " + midifiletrk + " - ignored");
                    ////  break;
                    ////}
                    if (len != 3) throw new MidiFileException();
                    int data = (int)MReaderTrk.ReadUIntF(3);
                    FileStream.TempoMap.Add(ticks, data);
                    FileStream.StrmLL.InsertTempoEv(seq, ticks, data);
                    break;
                  case 0x58:  //tsig
                    ////if (!IsCondTrk(midifiletrk)) {
                    ////  MReaderTrk.ReadBytes(len);  //ignore 
                    ////  Debug.WriteLine("Invalid time signature found on track " + midifiletrk + " - ignored");
                    ////  break;
                    ////}
                    if (len != 4) throw new MidiFileException();
                    int nn = (int)MReaderTrk.ReadUIntF(1);
                    int dd = (int)MReaderTrk.ReadUIntF(1);
                    MReaderTrk.ReadUIntF(1);  //bb - not used
                    MReaderTrk.ReadUIntF(1);  //cc - not used
                    if (indtsigs) {
                      int tsigdd = (int)Math.Pow(2, dd);
                      clsMTime.clsBBT bbt = new clsMTime.clsBBT(ticks);
                      P.frmStart.TraceLoad("add tsig " + nn + "/" + tsigdd + " ticks " + ticks + " from midifile");
                      //P.F.MTime.UpdateTSigsTicks(nn, tsigdd, ticks);
                      if (ticks == 0) P.F.MTime.AddTSigFirst(nn, tsigdd);
                      else P.F.MTime.AddTSig(nn, tsigdd, ticks, adj: true);
                    } else P.frmStart.TraceLoad("bypass tsig from midifile at ticks " + ticks);
                    break;
                  case 0x59:  //key sig
                    //if (!IsCondTrk(midifiletrk)) {
                    //  MReaderTrk.ReadBytes(len);  //ignore 
                    //  MessageBox.Show("Invalid key signature found on track " + midifiletrk + " - ignored");
                    //  break;
                    //}
                    if (len != 2) throw new MidiFileException(); 
                    int midikey = MReaderTrk.ReadIntSByte();
                    midikey = Math.Min(Math.Max(midikey, -7), 7);
                    string scale = MajMin(MReaderTrk.ReadUIntF(1));
                    P.frmStart.TraceLoad("add midifile key to _MidiKeys at ticks " + ticks);
                    P.F._MidiKeys.Add(midikey, scale, ticks);
                    break;
                  case 0x01:  //text
                    metadata = MReaderTrk.ReadBytes(len);  //other metaev  
                    FileStream.StrmLL.InsertMetaEv(seq, ticks, midifiletrk, type, metadata);
                    if (IsCondTrk(midifiletrk)) {
                      string text = Encoding.Default.GetString(metadata, 0, metadata.Length);
                      if (ticks == 0) {  //general text about the midifile?
                        FileStream.Text00.Add(text);
                      }
                    }
                    break;
                  case 0x2f:  //end of track
                    MaxMidiTicks = Math.Max(MaxMidiTicks, ticks);
                    break;  //don't put in stream!!!
                  default:
                    metadata = MReaderTrk.ReadBytes(len);  //other metaev  
                    FileStream.StrmLL.InsertMetaEv(seq, ticks, midifiletrk, type, metadata);
                    break;
                }
                break;
              default:
                status = 0;  //probably
                throw new MidiFileException();  //not sysex or metaev
            }
            break;
          default:
            break;
        }
        delta = MReaderTrk.ReadIntV();
        if (delta < 0) break;
      }
      MReaderTrk.Close();
      //if (P.F.MTime.TSigsSrc == clsMTime.eSource.None) P.F.MTime.TSigsSrc = clsMTime.eSource.Midi;
      return true;
    }

    private bool IsCondTrk(int trk) {
      //return true; 
      return (Format == 0 || trk < 0);  //was <return true;> before 8/12/17
    }

    //internal static void GetTicksPerQI() {
    //  if (((P.F.MTime.TicksPerQNote * 4) % clsFileStream.QIdd) != 0) {
    //    throw new MidiFileException("Resolution (QIDD) of 1/" + clsFileStream.QIdd + " not supported - try using a lower QIDD");
    //  }
    //  P.F.TicksPerQI = (P.F.MTime.TicksPerQNote * 4) / clsFileStream.QIdd;
    //}

    protected string MajMin(uint val) {
      if (val == 0) return "major";
      else if (val == 1) return "minor";
      else throw new MidiFileException(); 
    }
  }
}
