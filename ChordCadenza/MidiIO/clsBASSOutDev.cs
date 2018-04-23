using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Midi;
using Un4seen.Bass.Misc;
using Un4seen.BassAsio;
using Un4seen.Bass.AddOn.Mix;
using Un4seen.Bass.AddOn.Fx;
using static ChordCadenza.clsBassOutMidi;  //CheckOK etc.

namespace ChordCadenza {
  internal abstract class clsBASSOutDev {
    static clsBASSOutDev() {
      //Debug.WriteLine("{0:X8} {1:X8}", BassFx.BASS_FX_GetVersion(), BassFx.BASSFXVERSION);
      Debug.WriteLine("BASS version: {0:X8} {1:X8}", BassFx.BASS_FX_GetVersion(), BassFx.BASSFXVERSION);
      //int versionA = BassFx.BASS_FX_GetVersion();
      //int versionB = BassFx.BASSFXVERSION;
    }

    internal abstract string GetDeviceDesc();
    internal abstract void Close();
    internal abstract void ConnectFile(clsMP3Bass mp3bass);
    internal abstract void ConnectMidiDev(clsBassOutMidi bassoutmidi);
    internal abstract void DisconnectFile(clsMP3Bass mp3bass);
    internal abstract void DisconnectMidiDev(clsBassOutMidi bassoutmidi);

    internal static bool indAsio = false;
    //internal const int NoAudioDev = -2;
    internal static bool Disconnected = false;  //set if disconnect requested
    //internal const int InvalidAudioDev = -3;

    internal static clsBASSOutDev New() {
      if (Disconnected) return null;
      try {
        if (indAsio) return new clsBASSOutDevAsio(); else return new clsBASSOutDevNonAsio();
      }
      catch (AudioIOException) {
        Disconnected = true;
        return null;
      }
    }

    internal static string[] GetBASSDevs() {
      //if (Disconnected) return new string[0];
      return new string[] { "BuiltIn Synth (KB)", "BuiltIn Synth (Stream)" };
    }

    //protected static void CheckOK(bool ok) { CheckOK(ok); }
    //protected static void CheckOKHard(bool ok) { CheckOKHard(ok); }
    //protected static void CheckOKSoft(bool ok) { CheckOKSoft(ok); }

    protected void ThrowAudioExc() {
      throw new AudioIOException("Testing Bass_Init Error");  //temp Bass test
    }

    protected static int CheckHandle(int handle) {
      if (handle != 0) return handle;
      BASSError error = Bass.BASS_ErrorGetCode();
      LogicError.Throw(eLogicError.X117);
      return 0;
    }

    protected static int CheckHandleHard(int handle) {
      if (handle != 0) return handle;
      BASSError error = Bass.BASS_ErrorGetCode();
      throw new AudioIOException(error.ToString());
    }

    protected void FreeDevice(int devnum) {
      int tmp = Bass.BASS_GetDevice();  //may be set to new or temp (non-asio)
      Bass.BASS_SetDevice(devnum);  //output used by asio
      Bass.BASS_Free();  //using SetDevice device - can't use .BASS_STREAM_AUTOFREE 
      Bass.BASS_SetDevice(tmp);  //restore
    }
  }

  internal class clsBASSOutDevAsio : clsBASSOutDev {
    //internal static int NumDevs;   //for asio mixer
    internal int MixerStream;
    private BassAsioHandler _asio;
    private int AsioChannel = 0;
    internal static int AsioDevNum = 0;  //0=ASIO4ALL, 1=XONAR ASIO
    private static bool? _DevsExist = null;

    internal clsBASSOutDevAsio() {
      //* create mixer stream
      //* get asio infos
      BASS_ASIO_DEVICEINFO asiodevinfo = new BASS_ASIO_DEVICEINFO();
      BassAsio.BASS_ASIO_GetDeviceInfo(AsioDevNum, asiodevinfo);
      Debug.WriteLine("AsioDevInfo " + AsioDevNum + ": " + asiodevinfo.ToString());

      BASS_ASIO_INFO asioinfo = BassAsio.BASS_ASIO_GetInfo();
      if (asioinfo == null) Debug.WriteLine("No Asio Info");
      else Debug.WriteLine("AsioInfo " + ": " + asioinfo.ToString());

      BASS_ASIO_CHANNELINFO chaninfo = BassAsio.BASS_ASIO_ChannelGetInfo(false, AsioDevNum);
      if (chaninfo == null) Debug.WriteLine("No Asio Channel Info");
      else Debug.WriteLine("AsioChannelInfo " + ": " + chaninfo.ToString());

      //* init
      //* no update - not played via BASS
      //* init nosound device
#if NOASIODEVS
      ThrowAudioExc();
#else
      CheckOKHard(Bass.BASS_Init(0, 48000, 0, P.frmSC.Handle));  //44100 ignored
#endif
      CheckOKHard(Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, 0));
      //* int mixer stream (no sound 2 channels=stereo) 
      //MixerStream = TestBass.BASS_Mixer_StreamCreate(44100, 2,
      //  BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_DECODE);
      MixerStream = CheckHandleHard(BassMix.BASS_Mixer_StreamCreate(44100, 2,
        BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_DECODE));
      //* init asio stream on asiodevnum (optional)
      //TestBass.BASS_ASIO_Init(AsioDevNum, BASSASIOInit.BASS_ASIO_DEFAULT);  //(asiodev, flags)
      CheckOKHard(BassAsio.BASS_ASIO_Init(AsioDevNum, BASSASIOInit.BASS_ASIO_DEFAULT));  //(asiodev, flags)
      Start();  //should be OK to start and add channels later
    }

    internal override void ConnectFile(clsMP3Bass mp3bass) {
      mp3bass.StreamHandle = CheckHandleHard(Bass.BASS_StreamCreateFile(
        mp3bass.FilePath, 0, 0, BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_STREAM_PRESCAN));
      CheckOKHard(BassMix.BASS_Mixer_StreamAddChannel(
        MixerStream, mp3bass.StreamHandle, BASSFlag.BASS_DEFAULT));
      CheckOKHard(Bass.BASS_ChannelStop(mp3bass.StreamHandle));
    }

    internal override void ConnectMidiDev(clsBassOutMidi bassoutmidi) {
      bassoutmidi.MidiStream = CheckHandleHard(BassMidi.BASS_MIDI_StreamCreate(
        bassoutmidi.NumMidiChans, BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_FLOAT, 1));
      bassoutmidi.InitStream();
      CheckOKHard(BassMix.BASS_Mixer_StreamAddChannel(
        MixerStream, bassoutmidi.MidiStream, BASSFlag.BASS_DEFAULT));
    }

    internal override void DisconnectFile(clsMP3Bass mp3bass) {
      CheckOK(Bass.BASS_ChannelStop(mp3bass.StreamHandle));
      CheckOK(BassMix.BASS_Mixer_ChannelRemove(mp3bass.StreamHandle));
      CheckOK(Bass.BASS_StreamFree(mp3bass.StreamHandle));
      mp3bass.StreamHandle = 0;
    }

    internal override void DisconnectMidiDev(clsBassOutMidi bassoutmidi) {
      CheckOK(Bass.BASS_ChannelStop(bassoutmidi.MidiStream));
      CheckOK(BassMix.BASS_Mixer_ChannelRemove(bassoutmidi.MidiStream));
      CheckOK(Bass.BASS_StreamFree(bassoutmidi.MidiStream));
      bassoutmidi.MidiStream = 0;
    }

    internal static bool DevsExist {
      get {
        //return false;  //temp
        if (!_DevsExist.HasValue) {
          BASS_ASIO_DEVICEINFO asiodevinfo = new BASS_ASIO_DEVICEINFO();
          _DevsExist = BassAsio.BASS_ASIO_GetDeviceInfo(0, asiodevinfo);
        }
        return _DevsExist.Value;
      }
    }

    internal static string DevName {
      get {
        BASS_ASIO_DEVICEINFO info = BassAsio.BASS_ASIO_GetDeviceInfo(AsioDevNum);
        string name = (info == null) ? "" : info.name;
        if (name == "") return "*"; else return name;
      }
      set {
        if (value == "*") {  //default
          AsioDevNum = 0;  //no asio default, no asio nosound - use first real device (0)
          return;
        }
        BASS_ASIO_DEVICEINFO devinfo;
        int devnum = 0;
        do {
          devinfo = BassAsio.BASS_ASIO_GetDeviceInfo(devnum++);
          if (devinfo == null) {
            AsioDevNum = 0;  //no default - set to first ASIO
            return;
          }
        } while (devinfo.name != value);
        AsioDevNum = --devnum;
      }
    }

    internal override string GetDeviceDesc() {
      return BassAsio.BASS_ASIO_GetDeviceInfo(AsioDevNum).name;
    }

    private void Start() {
      Debug.WriteLine("{0} {1} {2} {3}", "Start ASIO listener: ", AsioDevNum, AsioChannel, MixerStream);
      _asio = new BassAsioHandler(AsioDevNum, AsioChannel, MixerStream);  //(asiodev, asiochan, basschan) 
      _asio.Volume = (float)0.2;
      if (!_asio.Start(0, 0)) {  //(buflen, threads)
        _asio.Dispose();
        _asio = null;
        LogicError.Throw(eLogicError.X118);
      }
      Bass.BASS_ChannelPlay(MixerStream, false);
    }

    internal override void Close() {
      if (_asio != null) {
        _asio.Stop();
        _asio.Dispose();
        _asio = null;
      }
      //if (MixerStream != 0) clsBassOutMidi.CheckOK(Bass.BASS_StreamFree(MixerStream));
      BassAsio.BASS_ASIO_SetDevice(AsioDevNum);
      BassAsio.BASS_ASIO_Free();
      FreeDevice(0);
    }
  }

  internal class clsBASSOutDevNonAsio : clsBASSOutDev {
    //internal const int DefaultAudioDevNum = -1;
    internal static int BufferSize = 0;  //millisecs
    internal static int UpdatePeriod = 20;
    internal static int BufferMargin = 10; 
    internal static int AudioDevNum = -1;
    //* -1=default, 0=nosound, 1=asus, 2=asus spdif, 3=realtek 
    private static bool? _DevsExist = null;
    internal static BASS_DEVICEINFO[] DevInfos;
    private static int DefaultDevice = 0;  //0 only used if there are no devices!
    internal static BASS_INFO BassInfo;

    static clsBASSOutDevNonAsio() {
#if NOAUDIODEVS
      DevInfos = null;
#else
      DevInfos = Bass.BASS_GetDeviceInfos();
#endif
      if (DevInfos == null) DevInfos = new BASS_DEVICEINFO[0];
      for (int i = 0; i < DevInfos.Length; i++) {
        if (DevInfos[i].IsDefault) {
          DefaultDevice = i;
          return;
        }
      }
    }

    internal clsBASSOutDevNonAsio() {
      BassInfo = new BASS_INFO();

#if NOAUDIODEVS
      ThrowAudioExc();  //temp Bass test
      //Disconnected = true;  //temp Bass test
#else
      CheckOKHard(Bass.BASS_Init(AudioDevNum, 44100, BASSInit.BASS_DEVICE_LATENCY, P.frmSC.Handle));  //44100 ignored
#endif

      AudioDevNum = (int)Bass.BASS_GetDevice();  //convert default device to actual device
      CheckOK(Bass.BASS_GetInfo(BassInfo));
      SetConfig();
    }

    internal static void SetConfig() {
      if (UpdatePeriod < 5) {
        LogicError.Throw(eLogicError.X160);
        return;
      }
      CheckOK(Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, UpdatePeriod));

      BASS_INFO info = new BASS_INFO();
      if (Bass.BASS_GetInfo(info)) {
        BassInfo = info;
        if (P.frmConfigBass != null) {
          P.frmConfigBass.lblMinBuf.Text = BassInfo.minbuf.ToString();
          P.frmConfigBass.lblLatency.Text = BassInfo.latency.ToString();
        }
        if (BufferSize <= 10) {  //10 is min, but may be defaulted to by BASS
          int minbuf = (BassInfo.minbuf > 0) ? BassInfo.minbuf : 100;
          BufferSize = UpdatePeriod + minbuf + BufferMargin;
          if (P.frmConfigBass != null) {
            P.frmConfigBass.Bypass_Event = true;
            P.frmConfigBass.nudBuffer.Value = BufferSize;
            P.frmConfigBass.Bypass_Event = false;
          }
        }
      }
      CheckOK(Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_BUFFER, BufferSize));
    }

    internal override void ConnectFile(clsMP3Bass mp3bass) {
      mp3bass.StreamHandle = CheckHandle(Bass.BASS_StreamCreateFile(
        mp3bass.FilePath, 0, 0, BASSFlag.BASS_STREAM_PRESCAN));
    }

    internal override void ConnectMidiDev(clsBassOutMidi bassoutmidi) {
      //mixed by windows/hardware
      bassoutmidi.MidiStream = CheckHandle(BassMidi.BASS_MIDI_StreamCreate(
        bassoutmidi.NumMidiChans, BASSFlag.BASS_STREAM_AUTOFREE, 1));
      bassoutmidi.InitStream();  //soundfonts etc.
      CheckOK(Bass.BASS_ChannelPlay(bassoutmidi.MidiStream, false));
    }

    internal override void DisconnectFile(clsMP3Bass mp3bass) {
      CheckOK(Bass.BASS_ChannelStop(mp3bass.StreamHandle));
      Bass.BASS_StreamFree(mp3bass.StreamHandle);  //may be already be freed by channelstop
      mp3bass.StreamHandle = 0;
    }

    internal override void DisconnectMidiDev(clsBassOutMidi bassoutmidi) {
      CheckOK(Bass.BASS_ChannelStop(bassoutmidi.MidiStream));
      Bass.BASS_StreamFree(bassoutmidi.MidiStream);  //may be already be freed by channelstop
      bassoutmidi.MidiStream = 0;
    }

    internal static bool DevsExist {
      get {
        //return false;  //temp
        if (!_DevsExist.HasValue) {
          BASS_DEVICEINFO devinfo = new BASS_DEVICEINFO();
          _DevsExist = Bass.BASS_GetDeviceInfo(1, devinfo);  //1=first real device
        }
        return _DevsExist.Value;
      }
    }

    internal override string GetDeviceDesc() {
      return Bass.BASS_GetDeviceInfo(AudioDevNum).name;
    }

    internal override void Close() {
      FreeDevice(AudioDevNum);
    }

    internal static string DevName {
      get {
        BASS_DEVICEINFO info = Bass.BASS_GetDeviceInfo(AudioDevNum);
        string name = (info == null) ? "" : info.name;
        if (name == "") return "*"; else return name;
      }
      set {
        if (value == "*") {
          AudioDevNum = clsBASSOutDevNonAsio.DefaultDevice;  //-1 = system default devnum
          return;
        }
        BASS_DEVICEINFO devinfo;
        int devnum = 0;
        do {
          devinfo = Bass.BASS_GetDeviceInfo(devnum++);
          if (devinfo == null) {
            AudioDevNum = clsBASSOutDevNonAsio.DefaultDevice;  //-1 = system default devnum
            return;
          }
        } while (devinfo.name != value);
        AudioDevNum = --devnum;
      }
    }
  }
}