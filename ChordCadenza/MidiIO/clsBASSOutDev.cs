using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
//using Un4seen.Bass;
//using Un4seen.Bass.AddOn.Midi;
//using Un4seen.Bass.Misc;
//using Un4seen.BassAsio;
//using Un4seen.Bass.AddOn.Mix;
//using Un4seen.Bass.AddOn.Fx;
using ManagedBass;
using ManagedBass.Fx;
using ManagedBass.Midi;
using ManagedBass.Asio;
using ManagedBass.Mix;

using static ChordCadenza.clsBassOutMidi;  //CheckOK etc.

namespace ChordCadenza {
  internal abstract class clsBASSOutDev {
    static clsBASSOutDev() {
      //Debug.WriteLine("{0:X8} {1:X8}", BassFx.BASS_FX_GetVersion(), BassFx.BASSFXVERSION);
      //Debug.WriteLine("BASS version: {0:X8} {1:X8}", Bass. BassFx.BASS_FX_GetVersion(), BassFx.BASSFXVERSION);
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
    private static bool _Disconnected = false;  //set if disconnect requested
    internal static bool Disconnected {  //or no devs
      get {
        bool? devsexist = (indAsio) ? clsBASSOutDevAsio._DevsExist : clsBASSOutDevNonAsio._DevsExist;
        if (!devsexist.HasValue) return _Disconnected;
        if (!devsexist.Value) return true;
        return _Disconnected;
      }
      set {
        _Disconnected = value;
      }
    }

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
      Errors error = Bass.LastError;
      LogicError.Throw(eLogicError.X117);
      return 0;
    }

    protected static int CheckHandleHard(int handle) {
      if (handle != 0) return handle;
      Errors error = Bass.LastError;
      throw new AudioIOException(error.ToString());
    }

    //protected void FreeDevice(int devnum) {
    //  int tmp = Bass.BASS_GetDevice();  //may be set to new or temp (non-asio)
    //  Bass.BASS_SetDevice(devnum);  //output used by asio
    //  Bass.BASS_Free();  //using SetDevice device - can't use .BASS_STREAM_AUTOFREE 
    //  Bass.BASS_SetDevice(tmp);  //restore
    //}

    protected void FreeDevice(int devnum) {
      int tmp = Bass.CurrentDevice;  //may be set to new or temp (non-asio)
      Bass.CurrentDevice = devnum;  //output used by asio
      Bass.Free();  //using SetDevice device - can't use .BASS_STREAM_AUTOFREE 
      //Bass.CurrentDevice = tmp;  //restore
    }
  }

  //internal class TempBassAsioHandler {  //dummy class
  //  internal TempBassAsioHandler(int asiodevnum, int asiochannel, int mixerstream) { }
  //  internal float Volume;
  //  internal bool Start(int x, int y) { return false; }
  //  internal void Stop() { }
  //  internal void Dispose() { }
  //}

  internal class clsBASSOutDevAsio : clsBASSOutDev {
    //internal static int NumDevs;   //for asio mixer
    internal int MixerStream;
    //private TempBassAsioHandler _asio;
    //private int AsioChannel = 0;
    internal static int AsioDevNum = 0;  //0=ASIO4ALL, 1=XONAR ASIO
    internal static bool? _DevsExist = null;

    internal clsBASSOutDevAsio() {
      //* create mixer stream
      //* get asio infos
      AsioDeviceInfo asiodevinfo = new AsioDeviceInfo();
      BassAsio.GetDeviceInfo(AsioDevNum, out asiodevinfo);
      Debug.WriteLine("AsioDevInfo " + AsioDevNum + ": " + asiodevinfo.ToString());

      AsioInfo asioinfo;
      bool ok = BassAsio.GetInfo(out asioinfo);
      if (!ok || asioinfo.Name == "") Debug.WriteLine("No Asio Info");
      else Debug.WriteLine("AsioInfo " + ": " + asioinfo.ToString());

      //AsioChannelInfo chaninfo = BassAsio.ChannelGetInfo(false, AsioDevNum);
      //if (chaninfo.Name == null || chaninfo.Name == "") Debug.WriteLine("No Asio Channel Info");
      //else Debug.WriteLine("AsioChannelInfo " + ": " + chaninfo.ToString());

      //* init
      //* no update - not played via BASS
      //* init nosound device
      Bass.Configure(Configuration.UpdatePeriod, 0);
      CheckOKHard(Bass.Init(0, 48000, 0, P.frmSC.Handle));  //44100 ignored (dev0=nosound/decode)
      //CheckOKHard(Bass.SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, 0));
      //Bass.UpdatePeriod = 0;
      //* int mixer stream (no sound 2 channels=stereo) 
      //MixerStream = TestBass.BASS_Mixer_StreamCreate(44100, 2,
      //  BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_DECODE);
      MixerStream = CheckHandleHard(BassMix.CreateMixerStream(44100, 2,
        BassFlags.Decode));
      //* init asio stream on asiodevnum (optional)
      //TestBass.BASS_ASIO_Init(AsioDevNum, BASSASIOInit.BASS_ASIO_DEFAULT);  //(asiodev, flags)
      CheckOKHard(BassAsio.Init(AsioDevNum, AsioInitFlags.None));  //(asiodev, flags)
      Start();  //should be OK to start and add channels later
    }

    internal override void ConnectFile(clsMP3Bass mp3bass) {
      mp3bass.StreamHandle = CheckHandleHard(Bass.CreateStream(
        mp3bass.FilePath, 0, 0, BassFlags.Decode | BassFlags.Prescan));
      CheckOKHard(BassMix.MixerAddChannel(
        MixerStream, mp3bass.StreamHandle, BassFlags.Default));
      CheckOKHard(Bass.ChannelStop(mp3bass.StreamHandle));
    }

    internal override void ConnectMidiDev(clsBassOutMidi bassoutmidi) {
      bassoutmidi.MidiStream = CheckHandleHard(BassMidi.CreateStream(
        bassoutmidi.NumMidiChans, BassFlags.Decode | BassFlags.Float, 1));
      bassoutmidi.InitStream();
      CheckOKHard(BassMix.MixerAddChannel(
        MixerStream, bassoutmidi.MidiStream, BassFlags.Default));
    }

    internal override void DisconnectFile(clsMP3Bass mp3bass) {
      CheckOK(Bass.ChannelStop(mp3bass.StreamHandle));
      CheckOK(BassMix.MixerRemoveChannel(mp3bass.StreamHandle));
      CheckOK(Bass.StreamFree(mp3bass.StreamHandle));
      mp3bass.StreamHandle = 0;
    }

    internal override void DisconnectMidiDev(clsBassOutMidi bassoutmidi) {
      CheckOK(Bass.ChannelStop(bassoutmidi.MidiStream));
      CheckOK(BassMix.MixerRemoveChannel(bassoutmidi.MidiStream));
      CheckOK(Bass.StreamFree(bassoutmidi.MidiStream));
      bassoutmidi.MidiStream = 0;
    }

    internal static bool DevsExist {
      get {
        //return false;  //temp
        if (!_DevsExist.HasValue) {
          AsioDeviceInfo asiodevinfo = new AsioDeviceInfo();
          _DevsExist = BassAsio.GetDeviceInfo(0, out asiodevinfo);
        }
        return _DevsExist.Value;
      }
    }

    internal static string DevName {
      get {
        AsioDeviceInfo info = new AsioDeviceInfo();
        _DevsExist = BassAsio.GetDeviceInfo(AsioDevNum, out info);
        if (!_DevsExist.Value) return "*";
        //AsioDeviceInfo info = BassAsio.GetDeviceInfo(AsioDevNum);
        string name = (info.Name == null) ? "" : info.Name;
        if (name == "") return "*"; else return name;
      }
      set {
        if (value == "*") {  //default
          AsioDevNum = 0;  //no asio default, no asio nosound - use first real device (0)
          return;
        }
        AsioDeviceInfo devinfo;
        int devnum = 0;
        do {
          _DevsExist = BassAsio.GetDeviceInfo(devnum++, out devinfo);
          //devinfo = BassAsio.GetDeviceInfo(devnum++);
          if (!_DevsExist.Value || devinfo.Name == null) {
            AsioDevNum = 0;  //no default - set to first ASIO
            return;
          }
        } while (devinfo.Name != value);
        AsioDevNum = --devnum;
      }
    }

    internal override string GetDeviceDesc() {
      AsioDeviceInfo info;
      _DevsExist = BassAsio.GetDeviceInfo(AsioDevNum, out info);
      //return BassAsio.GetDeviceInfo(AsioDevNum).Name;
      return (_DevsExist.Value) ? info.Name : "";
    }

    //private void Start() {
    //  Debug.WriteLine("{0} {1} {2} {3}", "Start ASIO listener: ", AsioDevNum, AsioChannel, MixerStream);
    //  _asio = new TempBassAsioHandler(AsioDevNum, AsioChannel, MixerStream);  //(asiodev, asiochan, basschan) 
    //  _asio.Volume = (float)0.2;
    //  if (!_asio.Start(0, 0)) {  //(buflen, threads)
    //    _asio.Dispose();
    //    _asio = null;
    //    LogicError.Throw(eLogicError.X118);
    //  }
    //  Bass.ChannelPlay(MixerStream, false);
    //}

    //internal override void Close() {
    //  if (_asio != null) {
    //    _asio.Stop();
    //    _asio.Dispose();
    //    _asio = null;
    //  }
    //  //if (MixerStream != 0) clsBassOutMidi.CheckOK(Bass.BASS_StreamFree(MixerStream));
    //  BassAsio.CurrentDevice = AsioDevNum;
    //  BassAsio.Free();
    //  FreeDevice(0);
    //}

    private AsioProcedure dAsioProc;

    private int AsioProc(bool input, int chan, IntPtr buffer, int len, IntPtr user) {  
      int c = Bass.ChannelGetData(MixerStream, buffer, len);
      if (c == -1) c = 0;  //an error, no data (could pause the channel at this point) ???
      return c;
    }

    private void Start() {
      //Debug.WriteLine("{0} {1} {2} {3}", "Start ASIO handler: ", AsioDevNum, AsioChannel, MixerStream);
      //Bass.Configure(Configuration.UpdatePeriod, 0);
      BassAsio.CurrentDevice = AsioDevNum;
      AsioInfo asioinfo;
      BassAsio.GetInfo(out asioinfo);
      BassAsio.Rate = 44100;
      dAsioProc = new AsioProcedure(AsioProc);
      Cfg.AsioProcs.Add(dAsioProc);  //prevent Garbage Collection
      CheckOKHard(BassAsio.ChannelEnable(false, 0, dAsioProc));
      CheckOKHard(BassAsio.ChannelJoin(false, 1, 0));
      BassAsio.ChannelSetFormat(false, 0, AsioSampleFormat.Bit16);
      BassAsio.ChannelSetFormat(false, 1, AsioSampleFormat.Bit16);
      SetdB();
      CheckOKHard(BassAsio.Start(asioinfo.PreferredBufferLength));
      //CheckOK(Bass.ChannelPlay(MixerStream, false));  //can't play decode chan
    }

    internal static void SetdB() {
      double vol = Math.Pow(10, ((double)Cfg.AsiodB / (double)20));
      BassAsio.ChannelSetVolume(false, 0, vol);
      BassAsio.ChannelSetVolume(false, 1, vol);
    }

    internal override void Close() {
      Bass.ChannelStop(MixerStream);
      BassAsio.Stop();
      BassAsio.ChannelReset(false, 0, 
        AsioChannelResetFlags.Enable | AsioChannelResetFlags.Join);
      //BassAsio.CurrentDevice = AsioDevNum;
      BassAsio.Free();
      FreeDevice(0);
      Cfg.AsioProcs.Remove(dAsioProc);
    }
  }

  internal class clsBASSOutDevNonAsio : clsBASSOutDev {
    //internal const int DefaultAudioDevNum = -1;
    internal static int BufferSize = 0;  //millisecs
    internal static int UpdatePeriod = 20;
    internal static int BufferMargin = 10; 
    internal static int AudioDevNum = -1;
    //* -1=default, 0=nosound, 1=asus, 2=asus spdif, 3=realtek 
    internal static bool? _DevsExist = null;
    internal static DeviceInfo[] DevInfos;
    private static int DefaultDevice = 0;  //0 only used if there are no devices!
    internal static BassInfo BassInfo;

    static clsBASSOutDevNonAsio() {
#if NOAUDIODEVS
      DevInfos = null;
#else
      //DevInfos = Bass.BASS_GetDeviceInfos();
      DeviceInfo devinfo;
      List<DeviceInfo> listdevinfos = new List<DeviceInfo>(); ;
      for (int i = 0; Bass.GetDeviceInfo(i, out devinfo); i++) {
        if (devinfo.IsEnabled) listdevinfos.Add(devinfo);
      }
      DevInfos = listdevinfos.ToArray();

#endif
      if (DevInfos == null) DevInfos = new DeviceInfo[0];
      for (int i = 0; i < DevInfos.Length; i++) {
        if (DevInfos[i].IsDefault) {
          DefaultDevice = i;
          return;
        }
      }
    }

    internal clsBASSOutDevNonAsio() {
      Version bassversion = Bass.Version;
      Debug.WriteLine("Bass Version: " + bassversion); 
      BassInfo = new BassInfo();

#if NOAUDIODEVS
      ThrowAudioExc();  //temp Bass test
      //Disconnected = true;  //temp Bass test
#else
      CheckOKHard(Bass.Init(AudioDevNum, 44100, DeviceInitFlags.Latency, P.frmSC.Handle));  //44100 ignored
#endif

      AudioDevNum = (int)Bass.CurrentDevice;  //convert default device to actual device
      Bass.GetInfo(out BassInfo);
      SetConfig();
    }

    internal static void SetConfig() {
      if (UpdatePeriod < 5) {
        LogicError.Throw(eLogicError.X160);
        return;
      }
      //CheckOK(Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, UpdatePeriod));
      Bass.UpdatePeriod = UpdatePeriod;

      BassInfo info = new BassInfo();
      if (Bass.GetInfo(out info)) {
        BassInfo = info;
        if (P.frmConfigBass != null) {
          P.frmConfigBass.lblMinBuf.Text = BassInfo.MinBufferLength.ToString();
          P.frmConfigBass.lblLatency.Text = BassInfo.Latency.ToString();
        }
        if (BufferSize <= 10) {  //10 is min, but may be defaulted to by BASS
          int minbuf = (BassInfo.MinBufferLength > 0) ? BassInfo.MinBufferLength : 100;
          BufferSize = UpdatePeriod + minbuf + BufferMargin;
          if (P.frmConfigBass != null) {
            P.frmConfigBass.Bypass_Event = true;
            P.frmConfigBass.nudBuffer.Value = BufferSize;
            P.frmConfigBass.Bypass_Event = false;
          }
        }
      }
      //CheckOK(Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_BUFFER, BufferSize));
      Bass.PlaybackBufferLength = BufferSize;
    }

    internal override void ConnectFile(clsMP3Bass mp3bass) {
      //mp3bass.StreamHandle = CheckHandle(Bass.BASS_StreamCreateFile(
      //  mp3bass.FilePath, 0, 0, BASSFlag.BASS_STREAM_PRESCAN));
      mp3bass.StreamHandle = CheckHandle(Bass.CreateStream(
        mp3bass.FilePath, 0, 0, BassFlags.Prescan));
    }

    internal override void ConnectMidiDev(clsBassOutMidi bassoutmidi) {
      //mixed by windows/hardware
      bassoutmidi.MidiStream = CheckHandle(BassMidi.CreateStream(
        bassoutmidi.NumMidiChans, BassFlags.AutoFree, 1));  
      bassoutmidi.InitStream();  //soundfonts etc.
      CheckOK(Bass.ChannelPlay(bassoutmidi.MidiStream, false));
    }

    internal override void DisconnectFile(clsMP3Bass mp3bass) {
      CheckOK(Bass.ChannelStop(mp3bass.StreamHandle));
      Bass.StreamFree(mp3bass.StreamHandle);  //may be already be freed by channelstop
      mp3bass.StreamHandle = 0;
    }

    internal override void DisconnectMidiDev(clsBassOutMidi bassoutmidi) {
      CheckOK(Bass.ChannelStop(bassoutmidi.MidiStream));
      Bass.StreamFree(bassoutmidi.MidiStream);  //may be already be freed by channelstop
      bassoutmidi.MidiStream = 0;
    }

    internal static bool DevsExist {
      get {
        //return false;  //temp
        if (!_DevsExist.HasValue) {
          DeviceInfo devinfo;
          _DevsExist = Bass.GetDeviceInfo(1, out devinfo);  //1=first real device
        }
        return _DevsExist.Value;
      }
    }

    internal override string GetDeviceDesc() {
      DeviceInfo devinfo;
      _DevsExist = Bass.GetDeviceInfo(1, out devinfo);  //1=first real device
      if (_DevsExist.Value) Bass.GetDeviceInfo(AudioDevNum, out devinfo);
      return (_DevsExist.Value) ? devinfo.Name : "";
      //return Bass.GetDeviceInfo(AudioDevNum).Name;
    }

    internal override void Close() {
      FreeDevice(AudioDevNum);
    }

    internal static string DevName {
      get {
        DeviceInfo info;
        _DevsExist = Bass.GetDeviceInfo(1, out info);
        if (_DevsExist.Value) Bass.GetDeviceInfo(AudioDevNum, out info);
        Bass.GetDeviceInfo(AudioDevNum, out info);
        if (!_DevsExist.Value) return "*";
        //DeviceInfo info = Bass.GetDeviceInfo(AudioDevNum);
        string name = (info.Name == null) ? "" : info.Name;
        if (name == "") return "*"; else return name;
      }
      set {
        if (value == "*") {
          AudioDevNum = clsBASSOutDevNonAsio.DefaultDevice;  //-1 = system default devnum
          return;
        }
        DeviceInfo devinfo;
        int devnum = 0;
        do {
          bool exists = Bass.GetDeviceInfo(devnum++, out devinfo);  //1=first real device
          //devinfo = Bass.GetDeviceInfo(devnum++);
          if (!exists || devinfo.Name == null) { //if (devinfo == null) {
            AudioDevNum = clsBASSOutDevNonAsio.DefaultDevice;  //-1 = system default devnum
            return;
          }
        } while (devinfo.Name != value);
        AudioDevNum = --devnum;
      }
    }
  }
}