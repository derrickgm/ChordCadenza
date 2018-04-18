using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Midi;
using Un4seen.Bass.Misc;
using Un4seen.BassAsio;
using Un4seen.Bass.AddOn.Mix;
using Un4seen.Bass.AddOn.Fx;
using System.IO;
using System.Windows.Forms;

namespace ChordCadenza {
  internal class clsBassMidiOutNull : clsBassMidiInOut, iBassMidiOut {
    public void SendShortMsg(int zmsg, int zpitch, int zvel) { }
    //public void SendShortMsgAndRecord(int zmsg, int zpitch, int zvel) { }
    public void AllNotesOff() { }
    public override void Close() { }
    public void EndCallback() { }
    public int GetTicks() { return 0; }  //only used by clsBASSFile
    public int GetStreamVol() { return 0; }
    public void SetStreamVol(int val) { }
    public int GetStreamPan() { return 0; }
    public void SetStreamPan(int val) { }
    public void SetFineTuning(int val) { }
    public void SendEvents(byte[] bytes) { }
    public override bool Opened() { return false; }
    internal override string MidiDevName {
      get { return "None"; }
      //set { }
    }
  }

  internal interface iBassMidiOut {
    //* midi or BASS output
    void SendShortMsg(int zmsg, int zpitch, int zvel);
    //void SendShortMsgAndRecord(int zmsg, int zpitch, int zvel);
    void AllNotesOff();
    //void ResetAllControllers();
    //void Open();
    void Close();
    void EndCallback();
    int GetTicks();  //only used by clsBASSFile
    int GetStreamVol();
    void SetStreamVol(int val);
    int GetStreamPan();
    void SetStreamPan(int val);
    void SetFineTuning(int val);
    void SendEvents(byte[] bytes);
    bool Opened();
  }

  internal class clsBassOutMidi : clsBassMidiInOut, iBassMidiOut {
    internal clsBassOutMidi(int numchans, string mididevname, string font, eType type) {
      //MidiDevName = mididevname;
      //if (type == eType.OutKB) MidiDevNameOutKB = mididevname;
      //else MidiDevNameOutStream = mididevname;

      Type = type;
      NumMidiChans = numchans;
      //Font = font;  //--> call file dialog if necessary
      Font = GetFont(font, type);  //--> call file dialog if necessary
      if (type == eType.OutKB) Cfg.SoundFontKB = Font; else Cfg.SoundFontStream = Font;
      P.BASSOutDev.ConnectMidiDev(this);
    }

    internal enum eOutType : int {KB = 0, Stream = 1};
    internal eOutType OutType {
      get {
        if (IsOutKB) return eOutType.KB; else return eOutType.Stream;
      }
    }
    
    internal override string MidiDevName {
      get {
        if (Type == eType.OutKB) return MidiDevNameOutKB;
        else return MidiDevNameOutStream;
      }
      //set {
      //  if (Type == eType.OutKB) MidiDevNameOutKB = value;
      //  else MidiDevNameOutStream = value;
      //}
    }
    public override void Close() { //iOutM
      if (P.BASSOutDev != null) P.BASSOutDev.DisconnectMidiDev(this);
    }

    public void EndCallback() {  //iOutM  (used for keeping callback alive???
    }

    internal int MidiStream;
    internal int NumMidiChans;
    //private string _Font = "";

    internal static BASS_BFX_FREEVERB[] Freeverb = new BASS_BFX_FREEVERB[2];  //[kb/stream] 
    internal static bool[] indFreeverb = new bool[2];  //[kb/stream]
    private int FreeverbHandle;
    internal Forms.frmFX frmFX;

    //internal string Font {
    //  get {
    //    return _Font;
    //  }
    //  set {
    //    _Font = GetFont(value);
    //  }
    //}
    internal string Font = "";

    //internal static clsBassOutMidi New(int numchans, string mididevname, string font, eType type) {
    //  if (clsBASSOutDev.indAsio) return new clsBassOutMidiAsio(numchans, mididevname, font, type);
    //  else return new clsBassOutMidiNonAsio(numchans, mididevname, font, type);
    //}

    private static string GetFont(string name, eType type) {
      string[] files; string errmsg;
      string font = GetFont(name, out files, out errmsg, true, type);
      if (errmsg != "") System.Windows.Forms.MessageBox.Show("Soundfont Error: " + errmsg);
      return font;
    }

    internal static string GetFont(string name, out string[] files, out string errmsg, bool ofd, eType type) {
      //* return valid fontname using name
      errmsg = "";
      files = new string[0];
      string[] paths;
      try {
        if (!Directory.Exists(Cfg.SoundFontsPath)) return GetFontAndPath(ofd, type);
        paths = Directory.GetFiles(Cfg.SoundFontsPath, "*.sf2", SearchOption.TopDirectoryOnly);
      }
      catch (Exception exc) {
        errmsg = "SoundFont File Error " + exc.Message;
        return "";
      }
      if (paths.Length == 0) {
        return GetFontAndPath(ofd, type);
      }
      files = new string[paths.Length];
      for (int i = 0; i < paths.Length; i++) {
        files[i] = Path.GetFileName(paths[i]);
      }
      Array.Sort(files);

      if (name == "") {
        if (Cfg.SoundFontKB != "") return Cfg.SoundFontKB;
        if (Cfg.SoundFontStream != "") return Cfg.SoundFontStream;
      }

      //* select Cfg file name
      foreach (string f in files) {
        if (f == name) return name;
      }

      return files[0];
    }

    private static string GetFontAndPath(bool ofd, eType type) {
      if (!ofd) return "";  //don't show ofd
      string msg = "No SoundFont file found for " + type + " output - Show file dialog?";
      if (MessageBox.Show(msg, MessageBoxButtons.YesNo) == DialogResult.No) return "";
      string pathandfilename = P.frmStart.GetSoundFontsFileName();
      if (pathandfilename == "***") return "";
      Cfg.SoundFontsPath = Path.GetDirectoryName(pathandfilename);
      return Path.GetFileName(pathandfilename);
    }

    internal void InitStream() {
      //TestBass.SetAtrrAndFont(MidiStream);
      CheckOK(Bass.BASS_ChannelSetAttribute(MidiStream, BASSAttribute.BASS_ATTRIB_MIDI_CPU, 75));
      CheckOK(Bass.BASS_ChannelSetAttribute(MidiStream, BASSAttribute.BASS_ATTRIB_VOL, (float)0.5));
      CheckOK(Bass.BASS_ChannelSetAttribute(MidiStream, BASSAttribute.BASS_ATTRIB_PAN, 0));
      if (Font != "") {
        int fonthandle = CheckHandleHard(BassMidi.BASS_MIDI_FontInit(Cfg.SoundFontsPath + "\\" + Font, 0));
        //int fonthandle = CheckHandle(BassMidi.BASS_MIDI_FontInit(@"D:\0\Sonar\SoundFonts\FluidR3 GM2-2.SF2", 0));
        BASS_MIDI_FONT font = new BASS_MIDI_FONT(fonthandle, -1, 0);
        BASS_MIDI_FONT[] fontarr = new BASS_MIDI_FONT[] { font };
        CheckOKHard(BassMidi.BASS_MIDI_StreamSetFonts(0, fontarr, 1));
        CheckOKHard(BassMidi.BASS_MIDI_StreamSetFonts(MidiStream, fontarr, 1));
        if (indFreeverb[FXTypeSeq]) SetFreeverb();  //****SetReverb();
        //InitStreamSub();
      }
    }

    internal void SetReverb() {
      return;
      //* reverb params
      //* gain -96 - 0 (0) dB
      //* mix -96 - 0 (0) dB
      //* time 0.001 - 3000 (1000) ms
      //* hfrt 0.001 - 0.999 (0.001) ratio
      //BASS_DX8_REVERB reverb = new BASS_DX8_REVERB(0f, 0f, 1000f, 0.001f);
      //if (ReverbHandle == 0) ReverbHandle = CheckHandle(Bass.BASS_ChannelSetFX(MidiStream, BASSFXType.BASS_FX_DX8_REVERB, 0));
      //CheckOK(Bass.BASS_FXSetParameters(ReverbHandle, Reverb[(int)Type]));
    }

    internal void UnsetReverb() {
      return;
      //if (ReverbHandle != 0) CheckOK(Bass.BASS_ChannelRemoveFX(MidiStream, ReverbHandle));
      //ReverbHandle = 0;
    }

    //private int SetFreeverbCount = 0;  //for debugging
    internal void SetFreeverb() {
      //* freeverb params
      //* fDryMix 0 - 1 (0) 
      //* fWetMix 0 - 3 (1)
      //* fRoomSize 0 - 1 (0.5)
      //* fDamp 0 - 1 (0.5)
      //* fWidth 0 - 1 (1)
      //* Mode (0)
      //Debug.WriteLine("SetFreeverb Count = " + ++SetFreeverbCount
      //   + " Handle = " + FreeverbHandle 
      //   + " Params: " + Freeverb[(int)Type].fDryMix
      //   + ", " + Freeverb[(int)Type].fWetMix
      //   + ", " + Freeverb[(int)Type].fRoomSize
      //   + ", " + Freeverb[(int)Type].fDamp
      //   + ", " + Freeverb[(int)Type].fWidth);  //for debugging
      //BASS_BFX_FREEVERB freeverb = new BASS_BFX_FREEVERB(0f, 1f, 0.5f, 0.5f, 1f, 0);
      try {
        if (FreeverbHandle == 0) {
          int fxgetversion = BassFx.BASS_FX_GetVersion();  //full (e.g. 02040b01)
          int bassgetversion = Bass.BASS_GetVersion();  //full (e.g. 02040c01)
          int fxversion = BassFx.BASSFXVERSION;  //high order bytes (e.g. 0204)
          Debug.WriteLine("{0:X8} {1:X8} {2:X8}", fxgetversion, bassgetversion, fxversion);
          if ((bassgetversion & 0xFFFF0000) != (fxgetversion & 0xFFFF0000)) {
            MessageBox.Show("FX version incompatible");
            return;
          }
          FreeverbHandle = CheckHandleHard(Bass.BASS_ChannelSetFX(MidiStream, BASSFXType.BASS_FX_BFX_FREEVERB, 1));
        }
        CheckOK(Bass.BASS_FXSetParameters(FreeverbHandle, Freeverb[FXTypeSeq]));
      }
      catch (AudioIOException exc) {
        MessageBox.Show("AudioIO Exception: " + exc.Message);
      }
    }

    internal void UnsetFreeverb() {
      if (FreeverbHandle != 0) CheckOK(Bass.BASS_ChannelRemoveFX(MidiStream, FreeverbHandle));
      FreeverbHandle = 0;
    }

    internal static void CheckOK(bool ok) {
      if (ok) return;
      BASSError error = Bass.BASS_ErrorGetCode();
      #if DEBUG
        ////////throw new AudioIOException(error.ToString());
        Debug.WriteLine("Bass Error Code: " + error);
      #else
        Debug.WriteLine("Bass Error Code: " + error);
      #endif
    }

    internal static void CheckOKHard(bool ok) {
      if (ok) return;
      BASSError error = Bass.BASS_ErrorGetCode();
      throw new AudioIOException(error.ToString());
    }

    internal static void CheckOKSoft(bool ok) {
      if (ok) return;
      BASSError error = Bass.BASS_ErrorGetCode();
      Debug.WriteLine("BASS error code: " + error);
    }

    internal static int CheckHandleHard(int handle) {
      //return handle;
      if (handle != 0) return handle;
      BASSError error = Bass.BASS_ErrorGetCode();
      throw new AudioIOException(error.ToString());
    }

    //private static string GetHandleDesc(int handle) {
    //  return (handle == 0) ? "OK" : "Fail";
    //}

    public override bool Opened() {
      return MidiStream != 0;
    }

    public void SetStreamVol(int val) {  //0.0...1.0; 0...100
      float fl = (val == 100) ? 1f : (float)val / (float)100;
      Bass.BASS_ChannelSetAttribute(MidiStream, BASSAttribute.BASS_ATTRIB_VOL, fl);
    }

    public void SetStreamPan(int val) {  //-1.0...1.0; -50..50
      float fl;
      if (val == 50) fl = 1f;
      if (val == -50) fl = -1f;
      fl = (float)val / (float)50;
      Bass.BASS_ChannelSetAttribute(MidiStream, BASSAttribute.BASS_ATTRIB_PAN, fl);
    }

    public void SetFineTuning(int val) {
      if (P.frmMidiDevs != null) {
        //* setting value should not raise scroll event
        if (IsOutKB) P.frmMidiDevs.trkMidiOutKBFineTuning.Value = val;
        if (IsOutStream) P.frmMidiDevs.trkMidiStreamFineTuning.Value = val;
      }
      int datamidi = (byte)(val + 64);  //-64.0.63 -> 0.64.127
      int databass = (datamidi == 127) ? 16383 : datamidi * 128;  //0.64.127 -> 0.8192.16383
      for (int ch = 0; ch < 16; ch++) {
        BassMidi.BASS_MIDI_StreamEvent(MidiStream, ch, BASSMIDIEvent.MIDI_EVENT_FINETUNE, databass);
      }
    }

    public int GetStreamVol() {  //0.0...1.0; 0...100
      float f1 = 0;
      if (!Bass.BASS_ChannelGetAttribute(MidiStream, BASSAttribute.BASS_ATTRIB_VOL, ref f1)) return 0;
      return (f1 > 0.99f) ? 100 : (int)(f1 * 100f);  //avoid clipping on float/int conversion
    }

    public int GetStreamPan() {  //-1.0...1.0; -50..50
      float f1 = 0;
      if (!Bass.BASS_ChannelGetAttribute(MidiStream, BASSAttribute.BASS_ATTRIB_PAN, ref f1)) return 0;
      if (f1 > 0.99f) return 50;
      if (f1 < -0.99f) return -50;
      return (int)(f1 * 50f);  //avoid clipping on float/int conversion
    }

    public void SendEvents(byte[] b) {
      BassMidi.BASS_MIDI_StreamEvents(MidiStream,
        BASSMIDIEventMode.BASS_MIDI_EVENTS_RAW | BASSMIDIEventMode.BASS_MIDI_EVENTS_NORSTATUS,
        0, b);
    }

    //public void SendShortMsgAndRecord(int status, int msg, int data) {
    //  SendShortMsg(status, msg, data);
    //  if (P.F.FSTrackMap != null && MidiPlay.MidiInKB != null) {
    //    P.F.FSTrackMap.RecordEv(MidiPlay.MidiInKB.Ticks.Value, (byte)status, (byte)msg, (byte)data);
    //  }
    //}

    public void SendShortMsg(int status, int msg, int data) {  //interface iOutM
      //* not from QIPlay
      SendEvents(new byte[] { (byte)status, (byte)msg, (byte)data });
      //Debug.WriteLine("SendEvents: " + status + ' ' + msg + ' ' + data);
      if (P.F.frmTrackMap != null && P.F.FSTrackMap != null && MidiPlay.Sync.indPlayActive == clsSync.ePlay.MidiStream) {
        int chan = status & 0x0f;
        long? ticks = clsPlay.GetTicks();
        if (ticks.HasValue && chan == P.F.FSTrackMap.RecChan) {
          if (P.F.FSTrackMap.RecStrmNew != null && P.F.FSTrackMap.RecStrmNew.Count == 0) {
            P.F.FSTrackMap.InitRecStrm(ticks.Value);
          }
          P.F.FSTrackMap.RecordEvNM(ticks.Value, (byte)status, (byte)msg, (byte)data);
          //Debug.WriteLine("clsBASSOutMidi: RecordTicks = " + ticks.Value);
        }
      }
    }

    public void AllNotesOff() {
      for (int ch = 0; ch < 16; ch++) {
        //for (int n = 0; n < 128; n++) {
        //  BassMidi.BASS_MIDI_StreamEvent(MidiStream, ch, BASSMIDIEvent.MIDI_EVENT_NOTE, n);
        //}
        BassMidi.BASS_MIDI_StreamEvent(MidiStream, ch, BASSMIDIEvent.MIDI_EVENT_NOTESOFF, 0);
        BassMidi.BASS_MIDI_StreamEvent(MidiStream, ch, BASSMIDIEvent.MIDI_EVENT_SUSTAIN, 0);
      }
    }

    public virtual int GetTicks() { return -1; }  //only used by clsBASSOutFile
  }

  //internal class clsBASSOutFile : clsBASSOut {  //used for streaming a midi file
  //  internal clsBASSOutFile() {
  //    NumChans = 16;
  //  }

  //  internal override int GetTicks() {
  //    throw new NotImplementedException();
  //  }

  //  internal bool Start(string filename) {
  //    Stream = CheckHandle(BassMidi.BASS_MIDI_StreamCreateFile(
  //      filename, 0, 0,
  //      BASSFlag.BASS_STREAM_AUTOFREE, 1));
  //    MySync = new SYNCPROC(SyncProc);
  //    int synchandle = CheckHandle(Bass.BASS_ChannelSetSync(Stream,
  //      BASSSync.BASS_SYNC_MIDI_TICK,
  //      0, MySync, IntPtr.Zero));
  //    InitStream();
  //    return (Stream != 0 && synchandle != 0);
  //    //CheckOK(Bass.BASS_ChannelPause(Stream));
  //  }

  //  private SYNCPROC MySync;

  //  internal void SyncProc(int handle, int channel, int data, IntPtr user) {
  //    if (user == IntPtr.Zero) {  //start sync

  //    } else {
  //      throw new NotImplementedException();
  //    }
  //  }
 
  //  //private BASS_FILEPROCS UserProcs;
  //  //internal void TestStartStreaming() {
  //  //  UserProcs = new BASS_FILEPROCS(new FILECLOSEPROC(ProcUserClose),
  //  //  null, 
  //  //  new FILEREADPROC(ProcUserRead),
  //  //  null);
  //  //  int teststream = CheckHandle(BassMidi.BASS_MIDI_StreamCreateFileUser(
  //  //    BASSStreamSystem.STREAMFILE_NOBUFFER,
  //  //    0, UserProcs, IntPtr.Zero, 0));
  //  //}

  //  //private int ProcUserRead(IntPtr buffer, int length, IntPtr user) {
  //  //  byte[] data = 
  //  //  return 0;
  //  //}

  //  private void ProcUserClose(IntPtr user) {
  //  }
  //}
}