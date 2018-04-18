
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass;
//using Un4seen.Bass.AddOn.Midi;
using Un4seen.Bass.Misc;
using Un4seen.BassAsio;

namespace ChordCadenza.Forms {
  internal partial class frmConfigBass : Form, IFormStream, ITT {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    public void FormStreamOnOff(bool on) {
      //cmdApply.Enabled = !on;
      //cmdDisconnect.Enabled = !on;
      panConnectDisconnect.Enabled = !on;
    }

    internal frmConfigBass() {
      //P.Forms.Add(this);
      InitializeComponent();
    }

    //private BASS_DEVICEINFO[] DeviceInfos;
    //protected string MidiOutKBNames = "";
    //protected string MidiOutStreamName = "";
    //protected string MidiOutKBNames {
    //  get {
    //    if (MidiPlay.OutMKB is clsBassOutMidi) return ((clsMidiOut)MidiPlay.MidiOutKB).MidiDevName;
    //    else return "";
    //  }
    //}

    //protected string MidiOutStreamName {
    //  get {
    //    if (MidiPlay.OutMStream is clsBassOutMidi) return ((clsMidiOut)MidiPlay.MidiOutStream).MidiDevName;
    //    else return "";
    //  }
    //}

    //internal int DevIndex;
    private clsInterface Interface;
    //internal static string DevLog = "DEV LOG\r\n1\r\n2\r\n3\r\n4\r\n5\r\n6";
    internal static string DevLog = "DevLog Start " + DateTime.Now;
    internal bool Bypass_Event = false;

    private void frmConfigBASS_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      clsTT.LoadToolTips(this);

      //* get midi device info
      SetStates();
      if (P.BASSOutDev == null) {
        chkAsio.Checked = clsBASSOutDev.indAsio;
      } else {
        chkAsio.Checked = (P.BASSOutDev is clsBASSOutDevAsio);
      }

      SetInterface();
      PopulatecmbAudioDevs();
      ShowCurrentConnection();

      nudLatencyKB.Value = Cfg.LatencyKB;
      nudLatencyMidiPlay.Value = Cfg.LatencyMidiPlay;

      //* show devlog
      //txtDevLog.Text = DevLog;
      //Task task1 = new Task(new Action(ScrollDevLog));
      //task1.Start();
    }

    private void SetStates() {
      chkAsio.Enabled = (P.BASSOutDev == null);
      cmbAudioOutput.Enabled = (P.BASSOutDev == null);
      if (!clsBASSOutDevAsio.DevsExist || !clsBASSOutDevNonAsio.DevsExist) chkAsio.Enabled = false;
      grpSetParams.Enabled = (P.BASSOutDev == null);
      cmdApply.Enabled = (P.BASSOutDev == null);
      cmdConnectAll.Enabled = (P.BASSOutDev == null);
      cmdDisconnect.Enabled = (P.BASSOutDev != null);
    }

    //private void ScrollDevLog() {
    //  this.Invoke(new MethodInvoker(delegate ()
    //  {
    //    txtDevLog.SelectionStart = txtDevLog.Text.Length;
    //    txtDevLog.ScrollToCaret();
    //  }));
    //}

    //private int cnt = 1;
    //private void cmdUpdateDevLog_Click(object sender, EventArgs e) {
    //  DevLogAddLine("Line " + cnt++);
    //}

    //internal static void DevLogAddLine(string txt) {
    //  DevLog += "\r\n" + txt;
    //  if (P.frmConfigBass != null) P.frmConfigBass.txtDevLog.AppendText("\r\n" + txt);

    //}

    private void SetInterface() {
      if (chkAsio.Checked) Interface = new clsAsio(this); else Interface = new clsNonAsio(this);
    }

    internal void ShowCurrentConnection() {
      if (P.BASSOutDev == null) {
        lblCurrent.Text = "None";
        return;
      }
      lblCurrent.Text = P.BASSOutDev.GetDeviceDesc();
      Interface.ShowCurrentConnectionSub();
    }

    private void PopulatecmbAudioDevs() {
      //* list all audio devices
      //* excl.dev 0 (no sound)
      //* dev -1 = default device
      cmbAudioOutput.Items.Clear();
      Interface.PopulatecmbAudioDevs(cmbAudioOutput);
    }

    private void cmbAudioOutput_SelectedIndexChanged(object sender, EventArgs e) {
      if (!cmbAudioOutput.Enabled) return;
      using (new clsWaitCursor()) {
        Interface.GetBassNonAsioInfo();
      }
    }

    internal static void CloseAllStreams() {
      if (P.F != null && P.F.AudioSync != null && P.F.AudioSync.MP3Player != null) {
        if (P.F.AudioSync.MP3Player is clsMP3Bass) {
          P.F.AudioSync.MP3Player.Free();  //should also stop play
        }
      }

      MidiPlay.CloseAllMidi(true);

      //if (MidiPlay.OutMStream is clsBassOutMidi) {
      //  MidiPlay.OutMStream.Close();
      //  MidiPlay.OutMStream = new clsBassMidiOutNull();
      //}

      //if (MidiPlay.OutMKB is clsBassOutMidi) {  
      //  MidiPlay.OutMKB.Close();  //MidiPlay.OutMKB may be null!!!
      //  MidiPlay.OutMKB = new clsBassMidiOutNull();
      //}

      if (P.BASSOutDev != null) {
        P.BASSOutDev.Close();
        P.BASSOutDev = null;
      }
    }

    internal void OpenAllStreams() {  //called from Apply()
      try {
        MidiPlay.OpenAudioDevs();
        if (P.F != null && P.F.AudioSync != null && P.F.AudioSync.MP3Player != null) {
          if (P.F.AudioSync.MP3Player is clsMP3Bass) {
            P.BASSOutDev.ConnectFile((clsMP3Bass)P.F.AudioSync.MP3Player);
          }
        }
        //MidiPlay.OpenMidiDevs(true);  //non-audio already open 
      }
      catch (AudioIOException exc) {
        MessageBox.Show("Audio IO Exception: " + exc.Message); 
        return;
      }
      catch (MidiIOException exc) {
        MessageBox.Show("Midi IO Exception: " + exc.Message); 
        return;
      }
    }

    //private int GetNumBASSDevs() {
    //  int cnt = 0;
    //  if (MidiOutStreamName.StartsWith("BuiltIn")) cnt++;
    //  if (MidiOutKBNames.StartsWith("BuiltIn")) cnt++;
    //  return cnt;
    //}

    private void cmdCancel_Click(object sender, EventArgs e) {
      Close();
    }

    private void cmdOK_Click(object sender, EventArgs e) {
      cmdApply_Click(sender, e);
      Close();
    }

    private void cmdApply_Click(object sender, EventArgs e) {
      using (new clsWaitCursor()) {
        clsBASSOutDev.Disconnected = false;
        CloseAllStreams();
        Interface.Apply();
        OpenAllStreams();
        SetStates();
        ShowCurrentConnection();
        PlayableForms.CmdState_Set();
      }
    }

    private void cmdConnectAll_Click(object sender, EventArgs e) {
      using (new clsWaitCursor()) {
        clsBASSOutDev.Disconnected = false;
        CloseAllStreams();
        Interface.Apply();
        OpenAllStreams();

        try {
          MidiPlay.OpenMidiDevs(bassonly: true); 
        }
        catch (AudioIOException exc) {
          MessageBox.Show("Audio IO Exception: " + exc.Message); 
          return;
        }
        catch (MidiIOException exc) {
          MessageBox.Show("Midi IO Exception: " + exc.Message); 
          return;
        }

        SetStates();
        ShowCurrentConnection();
        PlayableForms.CmdState_Set();
      }
    }

    private void cmdDisconnect_Click(object sender, EventArgs e) {
      using (new clsWaitCursor()) {
        clsBASSOutDev.Disconnected = true;
        CloseAllStreams();
        //Interface = null;
        SetStates();
        ShowCurrentConnection();
        if (P.frmMidiDevs != null) P.frmMidiDevs.ShowCurrentConnections();
        PlayableForms.CmdState_Set();
      }
    }

    private void chkAsio_CheckedChanged(object sender, EventArgs e) {
      if (!chkAsio.Enabled) return;
      cmdDisconnect_Click(null, null);
      SetInterface();
      PopulatecmbAudioDevs();
    }

    private void cmdAsioPanel_Click(object sender, EventArgs e) {
      Interface.ShowAsioPanel();
    }

    private abstract class clsInterface {
      protected clsInterface(frmConfigBass frm) {
        Frm = frm;
      }

      protected frmConfigBass Frm;
      internal abstract void PopulatecmbAudioDevs(ComboBox cmb);
      internal abstract void GetBassNonAsioInfo();
      internal abstract void Apply();
      internal virtual void ShowAsioPanel() { }
      internal virtual void ShowCurrentConnectionSub() { }
    }

    private class clsAsio : clsInterface {
      internal clsAsio(frmConfigBass frm)
        : base(frm) {
#if NOASIODEVS
        DevInfos = null;
#else
        DevInfos = BassAsio.BASS_ASIO_GetDeviceInfos();
#endif
        if (DevInfos == null) DevInfos = new BASS_ASIO_DEVICEINFO[0];
        Frm.panNonAsio.Visible = false;
        Frm.panNonAsio.Enabled = false;
        if (DevInfos.Length > 0) {
          Frm.cmdAsioPanel.Visible = true;
          Frm.cmdAsioPanel.Enabled = true;
        } else {
          Frm.cmdAsioPanel.Visible = false;
          Frm.cmdAsioPanel.Enabled = false;
          Frm.cmdApply.Enabled = false;
          Frm.cmdConnectAll.Enabled = false;
        }
      }

      private BASS_ASIO_DEVICEINFO[] DevInfos;

      internal override void ShowAsioPanel() {
        int tmp = -1;  //device to init temporarily
        int sel = Frm.cmbAudioOutput.SelectedIndex;
        int dev = BassAsio.BASS_ASIO_GetDevice();
        if (sel >= 0) {
          if (sel != dev) {
            tmp = sel;
            BassAsio.BASS_ASIO_Init(tmp, BASSASIOInit.BASS_ASIO_DEFAULT);  //(asiodev, flags)
          }
          clsBassOutMidi.CheckOK(BassAsio.BASS_ASIO_ControlPanel());
        }
        if (tmp >= 0) BassAsio.BASS_ASIO_Free();
      }

      internal override void PopulatecmbAudioDevs(ComboBox cmb) {
        if (DevInfos.Length == 0 || clsBASSOutDevAsio.AsioDevNum < 0) {  //0 = first real device
          cmb.SelectedIndex = -1;
        } else {
          for (int n = 0; n < DevInfos.Length; n++) {
            cmb.Items.Add(DevInfos[n]);
          }
          cmb.SelectedIndex = clsBASSOutDevAsio.AsioDevNum;
        }
      }

      internal override void GetBassNonAsioInfo() { }

      internal override void Apply() {
        clsBASSOutDev.indAsio = true;
        clsBASSOutDevAsio.AsioDevNum = Frm.cmbAudioOutput.SelectedIndex;
      }
    }

    private class clsNonAsio : clsInterface {
      internal clsNonAsio(frmConfigBass frm)
        : base(frm) {
        //DevInfos = Bass.BASS_GetDeviceInfos();
        Frm.panNonAsio.Visible = true;
        Frm.panNonAsio.Enabled = true;
        Frm.cmdAsioPanel.Visible = false;
        Frm.cmdAsioPanel.Enabled = false;
      }


      internal override void PopulatecmbAudioDevs(ComboBox cmb) {
        for (int n = 1; n < clsBASSOutDevNonAsio.DevInfos.Length; n++) {  //ignore dev 0 - nosound
          cmb.Items.Add(clsBASSOutDevNonAsio.DevInfos[n]);
        }
        int index = clsBASSOutDevNonAsio.AudioDevNum - 1;  //dfltdev: index = -2
        if (index < cmb.Items.Count && index >= 0) cmb.SelectedIndex = clsBASSOutDevNonAsio.AudioDevNum - 1;
        else cmb.SelectedIndex = -1;  //no selection
      }

      internal override void GetBassNonAsioInfo() {
        bool ok = true;
        bool temp = true;
        Bass.BASS_SetDevice(Frm.cmbAudioOutput.SelectedIndex + 1);

#if NOAUDIODEVS
        temp = false; 
        ok = false;  
        clsBASSOutDev.Disconnected = true; 
#else
        if (!Bass.BASS_Init(Frm.cmbAudioOutput.SelectedIndex + 1, 44100, BASSInit.BASS_DEVICE_LATENCY, P.frmSC.Handle)) {
          temp = false;
          BASSError error = Bass.BASS_ErrorGetCode();
          if (error != BASSError.BASS_ERROR_ALREADY) {
            ok = false;
            clsBASSOutDev.Disconnected = true;
          }
        }
#endif

        if (ok) clsBASSOutDevNonAsio.SetConfig();
        if (temp) Bass.BASS_Free();  //close if used in this method only
      }

      //internal override void GetBassNonAsioInfo() {
      //  //* get BASS_INFO for cmbAudioOutPut.Selected device
      //  //* if live device, just get the info
      //  //* if other device, initialize, getinfo, free
      //  //* only applies to non-ASIO
      //  Bass.BASS_SetDevice(Frm.cmbAudioOutput.SelectedIndex + 1);
      //  bool ok = true;
      //  bool temp = true;  //if dev used in this method only
      //  if (!Bass.BASS_Init(Frm.cmbAudioOutput.SelectedIndex + 1, 44100, BASSInit.BASS_DEVICE_LATENCY, P.frmSC.Handle)) {
      //    temp = false;
      //    BASSError error = Bass.BASS_ErrorGetCode();
      //    if (error != BASSError.BASS_ERROR_ALREADY) ok = false;
      //  }
      //  Frm.lblLatency.Text = "???";
      //  Frm.lblMinBuf.Text = "???";
      //  if (ok) {
      //    BASS_INFO info = new BASS_INFO();
      //    if (Bass.BASS_GetInfo(info)) clsBASSOutDevNonAsio.BassInfo = info;
      //    if (clsBASSOutDevNonAsio.BassInfo.latency > 0) {
      //      Frm.lblLatency.Text = clsBASSOutDevNonAsio.BassInfo.latency.ToString();
      //    }
      //    if (clsBASSOutDevNonAsio.BassInfo.minbuf > 0) {
      //      Frm.lblMinBuf.Text = clsBASSOutDevNonAsio.BassInfo.minbuf.ToString();
      //      Frm.nudBuffer.Value = clsBASSOutDevNonAsio.UpdatePeriod + clsBASSOutDevNonAsio.BassInfo.minbuf + clsBASSOutDevNonAsio.BufferMargin;
      //    }
      //  }

      //  //* set audio values
      //  if (clsBASSOutDevNonAsio.UpdatePeriod > 0) {
      //    Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, clsBASSOutDevNonAsio.UpdatePeriod);
      //  }
      //  if (clsBASSOutDevNonAsio.BufferSize > 0) {
      //    Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_DEV_BUFFER, clsBASSOutDevNonAsio.BufferSize);
      //  }

      //  //* get audio config values
      //  int updateperiod = Bass.BASS_GetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD);
      //  Frm.lblUpdate.Text = updateperiod.ToString(); ;
      //  if (temp) {
      //    int buffer = Bass.BASS_GetConfig(BASSConfig.BASS_CONFIG_BUFFER);
      //    Frm.lblBuffer.Text = (buffer > 0) ? buffer.ToString() : "???";
      //  } else {
      //    ShowCurrentConnectionSub();
      //  }
      //  if (temp) Bass.BASS_Free();  //close if used in this method only
      //}

      internal override void Apply() {
        clsBASSOutDevNonAsio.UpdatePeriod = (int)Frm.nudUpdatePeriod.Value;
        clsBASSOutDevNonAsio.BufferSize = (int)Frm.nudBuffer.Value;
        clsBASSOutDevNonAsio.SetConfig(); 
        clsBASSOutDev.indAsio = false;
        if (Frm.cmbAudioOutput.SelectedIndex >= 0) {
          clsBASSOutDevNonAsio.AudioDevNum = Frm.cmbAudioOutput.SelectedIndex + 1;
        }
      }

      internal override void ShowCurrentConnectionSub() {
        try {
          Frm.Bypass_Event = true;

          int buffer = Bass.BASS_GetConfig(BASSConfig.BASS_CONFIG_BUFFER);
          if (buffer >= Frm.nudBuffer.Minimum && buffer <= Frm.nudBuffer.Maximum) {
            Frm.nudBuffer.Value = buffer;
          }

          int updateperiod = Bass.BASS_GetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD);
          if (updateperiod >= Frm.nudBuffer.Minimum && updateperiod <= Frm.nudBuffer.Maximum) {
            Frm.nudUpdatePeriod.Value = updateperiod;
          }

          BASS_INFO info = new BASS_INFO();
          if (Bass.BASS_GetInfo(info)) clsBASSOutDevNonAsio.BassInfo = info;
          if (clsBASSOutDevNonAsio.BassInfo.latency > 0) {
            Frm.lblLatency.Text = clsBASSOutDevNonAsio.BassInfo.latency.ToString();
          }
          if (clsBASSOutDevNonAsio.BassInfo.minbuf > 0) {
            Frm.lblMinBuf.Text = clsBASSOutDevNonAsio.BassInfo.minbuf.ToString();
          }
        }
        finally {
          Frm.Bypass_Event = false;
        }
      }
    }

    private void frmConfigBass_FormClosed(object sender, FormClosedEventArgs e) {
      P.frmConfigBass = null;
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      HelpNavigator navigator = HelpNavigator.Topic;
      Help.ShowHelp(this, Cfg.HelpFilePath, navigator, "Form_ConfigBass_Intro.htm");
    }

    private void nudLatencyMidiPlay_ValueChanged(object sender, EventArgs e) {
      Cfg.LatencyMidiPlay = (int)nudLatencyMidiPlay.Value;
    }

    private void nudLatencyKB_ValueChanged(object sender, EventArgs e) {
      Cfg.LatencyKB = (int)nudLatencyKB.Value;
    }

    private void cmdLatencyMidiPlay_Click(object sender, EventArgs e) {
      if (clsBASSOutDev.Disconnected) return;
      SetLatencyFromDevice(nudLatencyMidiPlay);
    }

    private void SetLatencyFromDevice(NumericUpDown nud) {
      if (clsBASSOutDev.indAsio) {
        double rate = BassAsio.BASS_ASIO_GetRate();
        int latency = BassAsio.BASS_ASIO_GetLatency(false);
        int ms = (int)(latency * 1000 / rate);
        if (ms <= 5) ms = 0;  //not worth using timer delay
        nud.Value = ms;
      } else {  //non-ASIO
        BASS_INFO info = new BASS_INFO();
        if (Bass.BASS_GetInfo(info)) clsBASSOutDevNonAsio.BassInfo = info;
        if (clsBASSOutDevNonAsio.BassInfo.latency > 0) nud.Value = clsBASSOutDevNonAsio.BassInfo.latency;
      }
    }

    private void cmdLatencyKB_Click(object sender, EventArgs e) {
      if (clsBASSOutDev.Disconnected) return;
      SetLatencyFromDevice(nudLatencyKB);
    }

    private void cmdCalcBuffer_Click(object sender, EventArgs e) {
      BASS_INFO info = new BASS_INFO();
      if (Bass.BASS_GetInfo(info)) clsBASSOutDevNonAsio.BassInfo = info;
      if (clsBASSOutDevNonAsio.BassInfo.minbuf > 0) {
        nudBuffer.Value = (int)nudUpdatePeriod.Value + clsBASSOutDevNonAsio.BassInfo.minbuf + clsBASSOutDevNonAsio.BufferMargin;
      }
    }

    private void nudBuffer_ValueChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      clsBASSOutDevNonAsio.BufferSize = (int)nudBuffer.Value;
      clsBASSOutDevNonAsio.SetConfig();
    }

    private void nudUpdatePeriod_ValueChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      clsBASSOutDevNonAsio.UpdatePeriod = (int)nudUpdatePeriod.Value;
      clsBASSOutDevNonAsio.SetConfig();
    }

    private void cmdLatencyMidiPlayZero_Click(object sender, EventArgs e) {
      nudLatencyMidiPlay.Value = 0;
    }

    private void cmdLatencyKBZero_Click(object sender, EventArgs e) {
      nudLatencyKB.Value = 0;
    }
  }
}