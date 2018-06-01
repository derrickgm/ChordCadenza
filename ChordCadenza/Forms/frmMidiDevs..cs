using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ChordCadenza.Forms {
  internal partial class frmMidiDevs : Form, ITT {
    //private static bool indFirstTime = true;
    //private static int SelectedIndexcmbInKB = -1;
    //private static int SelectedIndexcmbInSync = -1;
    //private static int SelectedIndexcmbOutStream = -1;
    //private static int SelectedIndexcmbOutKB = -1;

    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    public void FormStreamOnOff(bool on) {
      //if (on) Close();
      Enabled = !on;
    }

    internal frmMidiDevs() {
      InitializeComponent();
      Forms.frmSC.ZZZSetPCKBEvs(this);
      //P.Forms.Add(this);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
      bool? ret = Forms.frmSC.StaticProcessCmdKey(ref msg, keyData);
      if (!ret.HasValue) return base.ProcessCmdKey(ref msg, keyData);
      return ret.Value;
    }

    private void frmMidiDevs_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      clsTT.LoadToolTips(this);
      #if !ADVANCED
        lblSyncIn.Hide();
        cmbInSync.Hide();
        //lblMidiInSyncDisconnected.Hide();
        lblSyncMsg.Hide();
#endif

      CheckConnectStates();  //disable cmb connected to stop selectedindex being set

      try {
        clsMidiInDevs midiindevs = new clsMidiInDevs();

        PopulateCmbMidi(cmbOutStream, new clsMDevsOut(), (clsBassMidiInOut)MidiPlay.OutMStream);
        PopulateCmbMidi(cmbOutKB, new clsMDevsOut(), (clsBassMidiInOut)MidiPlay.OutMKB);
        PopulateCmbMidi(cmbInSync, new clsMDevsIn(), MidiPlay.MidiInSync);
        PopulateCmbMidi(cmbInKB, new clsMDevsIn(), MidiPlay.MidiInKB);

        SetSelection(cmbOutStream, Cfg.MidiOutStream);
        SetSelection(cmbOutKB, Cfg.MidiOutKB);
        SetSelection(cmbInSync, Cfg.MidiInSync);
        SetSelection(cmbInKB, Cfg.MidiInKB);

        trkMidiOutKBFineTuning.Value = Cfg.MidiOutKBFineTuning;
        trkMidiStreamFineTuning.Value = Cfg.MidiStreamFineTuning;
      }
      catch (MidiIOException) {
        return;
      }
      //cmdApply.Enabled = true;
      //SetCmdFX();
      //PopulateCmbFonts(cmbFontStream, MidiPlay.OutMStream);
      //PopulateCmbFonts(cmbFontKB, MidiPlay.OutMKB);
      //if (!P.Advanced) chkMicrosoft.Hide();
      //chkMicrosoft.Checked = MidiPlay.Sync.MicrosoftMidiStreaming;
      //HideShowControls();  //hide/show BASS stuff
      ShowCurrentConnections();
      trkMidiOutKBFineTuning.Value = Cfg.MidiOutKBFineTuning;
      trkMidiStreamFineTuning.Value = Cfg.MidiStreamFineTuning;

      //SetStates();
      //indFirstTime = false;
    }

    //private void SetStates() { 
    //  bool anyinact = grpOutStream.Enabled || grpOutKB.Enabled || cmbInKB.Enabled;
    //  bool anyact = !grpOutStream.Enabled || !grpOutKB.Enabled || !cmbInKB.Enabled;
    //  #if ADVANCED
    //    anyinact = anyinact || cmbInSync.Enabled;
    //    anyact = anyact || !cmbInSync.Enabled;
    //  #endif
    //  cmdApply.Enabled = anyinact;
    //  cmdDisconnect.Enabled = anyact;
    //  PlayableForms.CmdState_Set();
    //}

    private void PopulateCmbFonts(ComboBox cmb, string fontname, clsBassOutMidi.eType type) {
      cmb.Show();
      cmb.Items.Clear();
      cmb.Enabled = false;

      //* get file paths
      string[] files;
      string errmsg;
      string font = clsBassOutMidi.GetFont(fontname, out files, out errmsg, false, type);
      if (errmsg == "") {
        cmb.Enabled = true;
        cmb.Items.AddRange(files);
        cmb.SelectedItem = font;
      } else {
        cmb.Items.Add("*** " + errmsg + " ***");
        cmb.SelectedIndex = 0;
      }
    }

    private void PopulateCmbMidi(ComboBox cmb, clsMDevs devs, clsBassMidiInOut midi) {
      cmb.Items.Clear();
      string[] devnames;
      try {
        devnames = devs.GetDevs();
      }
      catch (MidiIOException) {
        return;
      }

      clsMidiInOut.eType type;
      if (cmb == cmbOutStream) type = clsMidiInOut.eType.OutStream;
      else if (cmb == cmbOutKB) type = clsMidiInOut.eType.OutKB;
      else if (cmb == cmbInKB) type = clsMidiInOut.eType.InKB;
      else type = clsMidiInOut.eType.InSync; 

      foreach (string name in devnames) {
        if (cmb == cmbOutKB && name == "BuiltIn Synth (Stream)") continue;
        if (cmb == cmbOutStream && name == "BuiltIn Synth (KB)") continue;
        cmb.Items.Add(name);
        if (midi == null) {
          if (MidiPlay.IsBuiltIn(name)) {
            cmb.SelectedItem = clsMidiInOut.GetMidiDevName(type);
          } else {
            //cmb.SelectedItem = "None";
            cmb.SelectedIndex = (cmb.Items.Count > 0) ? 0 : -1;  
          }
        } else if (name == midi.MidiDevName) {
          cmb.SelectedItem = name;
        }
      }
    }

    private void SetSelection(ComboBox cmb, string item) {
      //* set midi cmb selectedindex when not connected
      //if (!cmb.Enabled || indFirstTime) return;  //connected or default
      if (!cmb.Enabled) return;  //connected or default
      if (item != "" && item != "***" && item != "None") cmb.SelectedItem = item;
      if (cmb.SelectedIndex == -1) {
        cmb.SelectedIndex = (cmb.Items.Count > 0) ? 0 : -1;
      }
    }

    private void cmdCancel_Click(object sender, EventArgs e) {
      Close();
    }

    //private void cmdDisconnect_Click(object sender, EventArgs e) {
    //  using (new clsWaitCursor()) {
    //    MidiPlay.CloseAllMidi();
    //    ShowCurrentConnection();
    //  }
    //  SetStates();
    //}

    //private void cmdApply_Click(object sender, EventArgs e) {
    //  using (new clsWaitCursor()) {
    //    Apply();
    //    //PlayableForms.CmdState_Set();
    //  }
    //  SetStates();
    //}

    private void ConnectInKB() {
      //* assumes device already closed
      if (cmbInKB.SelectedIndex < 0) {
        MessageBox.Show("Error: InMidiKB device not selected");
        return;
      }
      string devnameinkb = GetSelectedDevName(cmbInKB);

      try {
        MidiPlay.OpenMidiDev(clsMidiInOut.eType.InKB, devnameinkb, false);
      }
      catch (AudioIOException exc) {
        MessageBox.Show("Audio IO Exception: " + exc.Message); 
        return;
      }
      catch (MidiIOException exc) {
        MessageBox.Show("Midi IO Exception: " + exc.Message); 
        return;
      }
      Cfg.MidiInKB = GetSelectedDevName(cmbInKB);
      PostApply();
    }

    private void ConnectOutKB() {
      //* assumes device already closed
      if (cmbOutKB.SelectedIndex < 0) {
        MessageBox.Show("Error: OutKB device not selected");
        return;
      }
      if (cmbFontKB.Enabled) Cfg.SoundFontKB = (string)cmbFontKB.SelectedItem;
      if (Cfg.SoundFontKB == null) Cfg.SoundFontKB = "";
      string devnameoutkb = GetSelectedDevName(cmbOutKB);

      try {
        MidiPlay.OpenMidiDev(clsMidiInOut.eType.OutKB, devnameoutkb, false);
      }
      catch (AudioIOException exc) {
        MessageBox.Show("Audio IO Exception: " + exc.Message); 
        return;
      }
      catch (MidiIOException exc) {
        MessageBox.Show("Midi IO Exception: " + exc.Message); 
        return;
      }

      if (P.BASSOutDev == null && devnameoutkb == "BuiltIn Synth (KB)") {
        MidiPlay.OpenAudioDevs();
      }
      if (MidiPlay.OutMKB != null) MidiPlay.OutMKB.SetFineTuning(trkMidiOutKBFineTuning.Value);
      Cfg.MidiOutKB = GetSelectedDevName(cmbOutKB);
      PostApply();
    }

    private void ConnectOutStream() {
      //* assumes device already closed
      if (cmbOutStream.SelectedIndex < 0) {
        MessageBox.Show("Error: OutStream device not selected");
        return;
      }
      if (cmbFontStream.Enabled) Cfg.SoundFontStream = (string)cmbFontStream.SelectedItem;
      if (Cfg.SoundFontStream == null) Cfg.SoundFontStream = "";
      string devnameoutstream = GetSelectedDevName(cmbOutStream);
      try {
        MidiPlay.OpenMidiDev(clsMidiInOut.eType.OutStream, devnameoutstream, false);
      }
      catch (AudioIOException exc) {
        MessageBox.Show("Audio IO Exception: " + exc.Message); 
        return;
      }
      catch (MidiIOException exc) {
        MessageBox.Show("Midi IO Exception: " + exc.Message); 
        return;
      }

      if (P.BASSOutDev == null && devnameoutstream == "BuiltIn Synth (Stream)") {
        MidiPlay.OpenAudioDevs();
      }
      if (MidiPlay.OutMStream != null) MidiPlay.OutMStream.SetFineTuning(trkMidiStreamFineTuning.Value);
      Cfg.MidiOutStream = GetSelectedDevName(cmbOutStream);
      PostApply();
    }

    private void DisconnectInKB() {
      MidiPlay.CloseMidi(clsMidiInOut.eType.InKB, false);
      P.frmSC.Refresh();  //PCKB or MidiIn
      ShowCurrentConnections();
    }

    private void DisconnectOutKB() {
      MidiPlay.CloseMidi(clsMidiInOut.eType.OutKB, false);
      ShowCurrentConnections();
    }

    private void DisconnectOutStream() {
      MidiPlay.CloseMidi(clsMidiInOut.eType.OutStream, false);
      ShowCurrentConnections();
    }

    //private void Apply() {

    //  //* check InSync/InKB devices
    //  if (cmbInKB.SelectedIndex < 0) {
    //    MessageBox.Show("Error: InMidiKB device not selected");
    //    return;
    //  }
    //  if (GetSelectedDevName(cmbInSync) == GetSelectedDevName(cmbInKB)
    //    && GetSelectedDevName(cmbInSync) != "None") {
    //    MessageBox.Show("Error: Sync InMidi device and Keyboard InMidi devices are the same");
    //    return;
    //  }

    //  //* check OutKBn devices
    //  if (cmbOutKB.SelectedIndex < 0) {
    //    MessageBox.Show("Error: OutKB device not selected");
    //    return;
    //  }

    //  //* close any open frmFX forms 
    //  if (MidiPlay.OutMStream != null && MidiPlay.OutMStream is clsBassOutMidi) {
    //    Forms.frmFX frmfx = ((clsBassOutMidi)MidiPlay.OutMStream).frmFX;
    //    if (frmfx != null) frmfx.Close();
    //  }
    //  if (MidiPlay.OutMKB != null && MidiPlay.OutMKB is clsBassOutMidi) {
    //    Forms.frmFX frmfx = ((clsBassOutMidi)MidiPlay.OutMKB).frmFX;
    //    if (frmfx != null) frmfx.Close();
    //  }

    //  MidiPlay.CloseAllMidi();

    //  if (cmbFontKB.Enabled) Cfg.SoundFontKB = (string)cmbFontKB.SelectedItem;
    //  if (Cfg.SoundFontKB == null) Cfg.SoundFontKB = "";
    //  if (cmbFontStream.Enabled) Cfg.SoundFontStream = (string)cmbFontStream.SelectedItem;
    //  if (Cfg.SoundFontStream == null) Cfg.SoundFontStream = "";

    //  try {
    //    string devnameinkb = GetSelectedDevName(cmbInKB);
    //    string devnameinsync = GetSelectedDevName(cmbInSync);
    //    string devnameoutstream = GetSelectedDevName(cmbOutStream);
    //    string devnameoutkb = GetSelectedDevName(cmbOutKB);
    //    MidiPlay.OpenMidiDevs(devnameinkb, devnameinsync, devnameoutstream, devnameoutkb, false);
    //    if (P.BASSOutDev == null) {
    //      if (devnameoutstream == "BuiltIn Synth (Stream)"
    //      || (devnameoutkb == "BuiltIn Synth (KB)")) MidiPlay.OpenAudioDevs();
    //    }
    //  }
    //  catch (AudioIOException exc) {
    //    MessageBox.Show("Audio IO Exception: " + exc.Message);
    //    return;
    //  }
    //  catch (MidiIOException exc) {
    //    MessageBox.Show("Midi IO Exception: " + exc.Message);
    //    return;
    //  }

    //  if (MidiPlay.OutMKB != null) MidiPlay.OutMKB.SetFineTuning(trkMidiOutKBFineTuning.Value);
    //  if (MidiPlay.OutMStream != null) MidiPlay.OutMStream.SetFineTuning(trkMidiStreamFineTuning.Value);

    //  Cfg.MidiInKB = GetSelectedDevName(cmbInKB);
    //  Cfg.MidiInSync = GetSelectedDevName(cmbInSync);
    //  Cfg.MidiOutKB = GetSelectedDevName(cmbOutKB);
    //  Cfg.MidiOutStream = GetSelectedDevName(cmbOutStream);

    //  PostApply();
    //}

    private static void PostApply() {
      if (P.F.FSTrackMap != null) P.F.SendInit();
      if (P.frmSC != null) P.frmSC.Refresh();  //Qwerty or solfa
      if (P.frmConfigBass != null) P.frmConfigBass.ShowCurrentConnection();
    }

    //private void SetCmdFX() {
    //  cmdFXKB.Visible = (MidiPlay.OutMKB is clsBassOut);
    //  cmdFXStream.Visible = (MidiPlay.OutMStream is clsBassOut);
    //}

    //private bool CheckDup(ComboBox cmb0, ComboBox cmb1) {
    //  //* return false if OK (no duplicate)
    //  string devname0 = GetSelectedDevName(cmb0);
    //  string devname1 = GetSelectedDevName(cmb1);
    //  if (devname0 == "" || devname1 == "") return false;
    //  if (devname0 == "None" || devname1 == "None") return false;
    //  if (devname0 != devname1) return false;
    //  MessageBox.Show("Error: Sync OutKB device: " + devname0 + " duplicated");
    //  return true;
    //}

    private string GetSelectedDevName(ComboBox cmb) {
      if (cmb.SelectedItem == null) return "";
      return (string)cmb.SelectedItem;
    }

    private void HideShowFontFXControls() {
      if (cmbOutStream.SelectedItem != null && MidiPlay.IsBuiltIn((string)cmbOutStream.SelectedItem)) { 
        lblcmbFontStream.Show();
        cmbFontStream.Show();
        cmdFXStream.Show();
        PopulateCmbFonts(cmbFontStream, Cfg.SoundFontStream, clsBassMidiInOut.eType.OutStream);
      } else {
        lblcmbFontStream.Hide();
        cmbFontStream.Hide();
        cmdFXStream.Hide();
      }

      if (cmbOutKB.SelectedItem != null && MidiPlay.IsBuiltIn((string)cmbOutKB.SelectedItem)) {
        lblcmbFontKB.Show();
        cmbFontKB.Show();
        cmdFXKB.Show();
        PopulateCmbFonts(cmbFontKB, Cfg.SoundFontKB, clsBassMidiInOut.eType.OutKB);
        //if (MidiPlay.OutMKB != null) trkMidiOutKBVol.Value = MidiPlay.OutMKB.GetStreamVol();
      } else {
        lblcmbFontKB.Hide();
        cmbFontKB.Hide();
        cmdFXKB.Hide();
      }
    }

    private void cmdFXStream_Click(object sender, EventArgs e) {
      if (MidiPlay.OutMStream is clsBassOutMidi) {
        clsBassOutMidi outm = (clsBassOutMidi)MidiPlay.OutMStream;
        if (outm.frmFX == null) outm.frmFX = new Forms.frmFX(clsBassOutMidi.eOutType.Stream);
        Utils.FormAct(outm.frmFX);
        //if (!outm.frmFX.Visible) outm.frmFX.Show();
      }
    }

    private void cmdFXKB_Click(object sender, EventArgs e) {
      if (MidiPlay.OutMKB is clsBassOutMidi) {
        clsBassOutMidi outm = (clsBassOutMidi)MidiPlay.OutMKB;
        if (outm.frmFX == null) outm.frmFX = new Forms.frmFX(clsBassOutMidi.eOutType.KB);
        if (!outm.frmFX.Visible) outm.frmFX.Show();
      }
    }

    //internal void trkMidiStreamVol_Scroll(object sender, EventArgs e) {
    //  if (MidiPlay.OutMStream != null) MidiPlay.OutMStream.SetStreamVol(trkMidiStreamVol.Value);
    //}

    //internal void trkMidiOutVol_Scroll(object sender, EventArgs e) {
    //  MidiPlay.OutMKB.SetStreamVol(trkMidiOutKBVol.Value);
    //}

    internal void trkMidiStreamFineTuning_Scroll(object sender, EventArgs e) {
      Cfg.MidiStreamFineTuning = trkMidiStreamFineTuning.Value;
      MidiPlay.OutMStream.SetFineTuning(trkMidiStreamFineTuning.Value);
    }

    internal void trkMidiStreamFineTuning_ValueChanged(object sender, EventArgs e) {
      lblMidiStreamFineTuning.Text = trkMidiStreamFineTuning.Value.ToString();
    }

    internal void trkMidiOutKBFineTuning_Scroll(object sender, EventArgs e) {
      Cfg.MidiOutKBFineTuning = trkMidiOutKBFineTuning.Value;
      MidiPlay.OutMKB.SetFineTuning(trkMidiOutKBFineTuning.Value);
    }

    internal void trkMidiOutKBFineTuning_ValueChanged(object sender, EventArgs e) {
      lblMidiOutKBFineTuning.Text = trkMidiOutKBFineTuning.Value.ToString();
    }

    internal void ShowCurrentConnections() {
      if (P.BASSOutDev == null) lblCurrentAudio.Text = "None";
      else lblCurrentAudio.Text = P.BASSOutDev.GetDeviceDesc();
      HideShowFontFXControls();
      CheckConnectStates();
      PlayableForms.CmdState_Set();
    }

    private void frmMidiDevs_FormClosed(object sender, FormClosedEventArgs e) {
      P.frmMidiDevs = null;
    }

    internal void CheckConnectStates() {
      if (MidiPlay.MidiInSync == null || !MidiPlay.MidiInSync.Opened()) {
        //lblMidiInSyncDisconnected.Text = "Disconnected";
        lblSyncIn.Enabled = true;
        cmbInSync.Enabled = true;
      } else {
        //lblMidiInSyncDisconnected.Text = "";
        lblSyncIn.Enabled = false;
        cmbInSync.Enabled = false;
      }

      if (MidiPlay.MidiInKB == null || !MidiPlay.MidiInKB.Opened()) {
        //lblMidiInKBDisconnected.Text = "Disconnected";
        cmdExecInKB.Text = "Connect";
        lblcmbInKB.Enabled = true;
        cmbInKB.Enabled = true;
      } else {
        //lblMidiInKBDisconnected.Text = "";
        cmdExecInKB.Text = "Disconnect";
        lblcmbInKB.Enabled = false;
        cmbInKB.Enabled = false;
      }

      if (MidiPlay.OutMStream == null || !MidiPlay.OutMStream.Opened()) {
        //lblOutStreamDisconnected.Text = "Disconnected";
        cmdExecOutStream.Text = "Connect";
        grpTuningOutStream.Enabled = false;  //can't send tuning to closed dev
        grpOutStream.Enabled = true;
      } else {
        //lblOutStreamDisconnected.Text = "";
        cmdExecOutStream.Text = "Disconnect";
        grpTuningOutStream.Enabled = true;  //BASS should be thread safe
        grpOutStream.Enabled = false;
      }

      if (MidiPlay.OutMKB == null || !MidiPlay.OutMKB.Opened()) {
        //lblOutKBDisconnected.Text = "Disconnected";
        cmdExecOutKB.Text = "Connect";
        grpTuningOutKB.Enabled = false;   //can't send tuning to closed dev
        grpOutKB.Enabled = true;
      } else {
        //lblOutKBDisconnected.Text = "";
        cmdExecOutKB.Text = "Disconnect";
        grpTuningOutKB.Enabled = true;  //BASS should be thread safe
        grpOutKB.Enabled = false;
      }

      /*
      if (MidiPlay.IsBuiltIn(clsBassMidiInOut.MidiDevNameOutStream)) {
        if (P.BASSOutDev == null) {
          //*lblOutStreamDisconnected.Text = "Audio Disconnected";
          grpOutStreamBass.Enabled = false;
        } else {
          //*lblOutStreamDisconnected.Text = "";
          grpOutStreamBass.Enabled = true;
        }
      } else {
        //*lblOutStreamDisconnected.Text = "";
      }

      if (MidiPlay.IsBuiltIn(clsBassMidiInOut.MidiDevNameOutKB)) {
        if (P.BASSOutDev == null) {
          //*lblOutKBDisconnected.Text = "Audio Disconnected";
          grpOutKBBass.Enabled = false;
        } else {
          //*lblOutKBDisconnected.Text = "";
          grpOutKBBass.Enabled = true;
        }
      } else {
        //*lblOutKBDisconnected.Text = "";
      }
      */
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Utils.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_MidiDevs_Intro.htm");
    }

    private void cmbOutStream_SelectedIndexChanged(object sender, EventArgs e) {
      Cfg.MidiOutStream = (string)cmbOutStream.SelectedItem;
      clsBassMidiInOut.MidiDevNameOutStream =  GetSelectedDevName(cmbOutStream);
      HideShowFontFXControls();
    }

    private void cmbOutKB_SelectedIndexChanged(object sender, EventArgs e) {
      Cfg.MidiOutKB = (string)cmbOutKB.SelectedItem;
      clsBassMidiInOut.MidiDevNameOutKB =  GetSelectedDevName(cmbOutKB);
      HideShowFontFXControls();
    }

    private void cmbInKB_SelectedIndexChanged(object sender, EventArgs e) {
      Cfg.MidiInKB = (string)cmbInKB.SelectedItem;
    }

    private void cmbInSync_SelectedIndexChanged(object sender, EventArgs e) {
      Cfg.MidiInSync = (string)cmbInSync.SelectedItem;
    }

    //private void frmMidiDevs_FormClosing(object sender, FormClosingEventArgs e) {
    //  Cfg.SelectedIndexcmbInKB = cmbInKB.SelectedIndex;
    //  Cfg.SelectedIndexcmbInSync = cmbInSync.SelectedIndex;
    //  Cfg.SelectedIndexcmbOutStream = cmbOutStream.SelectedIndex;
    //  Cfg.SelectedIndexcmbOutKB = cmbOutKB.SelectedIndex;
    //}

    private void cmdExecInKB_Click(object sender, EventArgs e) {
      using (new clsWaitCursor()) {
        if (cmdExecInKB.Text == "Connect") ConnectInKB();
        else DisconnectInKB();
      }
      ShowCurrentConnections();
    }

    private void cmdExecOutKB_Click(object sender, EventArgs e) {
      using (new clsWaitCursor()) {
        if (cmdExecOutKB.Text == "Connect") ConnectOutKB();
        else DisconnectOutKB();
      }
      ShowCurrentConnections();
    }

    private void cmdExecOutStream_Click(object sender, EventArgs e) {
      using (new clsWaitCursor()) {
        if (cmdExecOutStream.Text == "Connect") ConnectOutStream();
        else DisconnectOutStream();
      }
      ShowCurrentConnections();
    }

    private void cmbFontStream_SelectedIndexChanged(object sender, EventArgs e) {
      Cfg.SoundFontStream = (string)cmbFontStream.SelectedItem;
    }

    private void cmbFontKB_SelectedIndexChanged(object sender, EventArgs e) {
      Cfg.SoundFontKB = (string)cmbFontKB.SelectedItem;
    }
  }
}
