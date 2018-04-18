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
using Un4seen.Bass.AddOn.Fx;

namespace ChordCadenza.Forms {
  internal partial class frmFX : Form, IFormStream, ITT {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    public void FormStreamOnOff(bool on) { }

    internal frmFX(clsBassOutMidi.eOutType type) {
      InitializeComponent();
      Type = type;
      //P.Forms.Add(this);
    }

    private clsBassOutMidi.eOutType Type;
    private BASS_DX8_REVERB ReverbDefaults = new BASS_DX8_REVERB();
    private BASS_BFX_FREEVERB FreeverbDefaults = new BASS_BFX_FREEVERB();

    private clsBassOutMidi BassOut {
      get {
        iBassMidiOut outm = new clsBassMidiOutNull();
        if (Type == clsBassOutMidi.eOutType.KB) outm = MidiPlay.OutMKB;
        else if (Type == clsBassOutMidi.eOutType.Stream) outm = MidiPlay.OutMStream;
        else throw new FatalException();
        if (outm == null || !(outm is clsBassOutMidi)) return null;
        return (clsBassOutMidi)outm;
      }
    }

    //private BASS_DX8_REVERB CurrentReverb {
    //  get { return clsBassOut.Reverb[(int)Type]; }
    //}

    private BASS_BFX_FREEVERB CurrentFreeverb {
      get {
        if (BassOut == null) return null;
        return clsBassOutMidi.Freeverb[(int)Type]; 
      }
    }

    private struct ReverbMinMax {
      internal const float GainMin = -32;
      internal const float GainMax = 0;
      internal const float MixMin = -24;
      internal const float MixMax = 0;
      internal const float TimeMin = 0.001f;
      internal const float TimeMax = 3000f;
      internal const float HFRTMin = 0.001f;
      internal const float HFRTMax = 0.999f;
    }

    private struct FreeverbMinMax {
      internal const float DryMixMin = 0;
      internal const float DryMixMax = 1;
      internal const float WetMixMin = 0;
      internal const float WetMixMax = 3;
      internal const float RoomSizeMin = 0;
      internal const float RoomSizeMax = 1;
      internal const float DampMin = 0;
      internal const float DampMax = 1;
      internal const float WidthMin = 0;
      internal const float WidthMax = 1;
    }

    private void frmFX_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      clsTT.LoadToolTips(this);
      if (Type == clsBassOutMidi.eOutType.KB) Text = "KBOut FX - Chord Cadenza";
      else if (Type == clsBassOutMidi.eOutType.Stream) Text = "StreamOut FX - Chord Cadenza";
      else throw new FatalException();

      //LoadReverb(CurrentReverb);
      //chkEnableReverb.Checked = clsBassOut.indReverb[(int)Type];

      if (BassOut == null) {
        grpFreeverb.Enabled = false;
      } else {
        LoadFreeverb(CurrentFreeverb);
        chkEnableFreeverb.Checked = clsBassOutMidi.indFreeverb[(int)Type];
      }
    }

    //private void LoadReverb(BASS_DX8_REVERB reverb) {
    //  trkGain.Value = FXToTrk(reverb.fInGain, ReverbMinMax.GainMin, ReverbMinMax.GainMax);
    //  trkMix.Value = FXToTrk(reverb.fReverbMix, ReverbMinMax.MixMin, ReverbMinMax.MixMax);
    //  trkTime.Value = FXToTrk(reverb.fReverbTime, ReverbMinMax.TimeMin, ReverbMinMax.TimeMax);
    //  trkHFRT.Value = FXToTrk(reverb.fHighFreqRTRatio, ReverbMinMax.HFRTMin, ReverbMinMax.HFRTMax);
    //}

    private void LoadFreeverb(BASS_BFX_FREEVERB freeverb) {
      trkDryMix.Value = FXToTrk(freeverb.fDryMix, FreeverbMinMax.DryMixMin, FreeverbMinMax.DryMixMax);
      trkWetMix.Value = FXToTrk(freeverb.fWetMix, FreeverbMinMax.WetMixMin, FreeverbMinMax.WetMixMax);
      trkRoomSize.Value = FXToTrk(freeverb.fRoomSize, FreeverbMinMax.RoomSizeMin, FreeverbMinMax.RoomSizeMax);
      trkDamp.Value = FXToTrk(freeverb.fDamp, FreeverbMinMax.DampMin, FreeverbMinMax.DampMax);
      trkWidth.Value = FXToTrk(freeverb.fWidth, FreeverbMinMax.WidthMin, FreeverbMinMax.WidthMax);
    }

    private int FXToTrk(float fx, float min, float max) {
      //* convert FX val to trkbar val
      //* trkbar range: 0 - 100
      //* FX range: min to max
      if (fx == min) return 0;
      if (fx == max) return 100;
      float trk = (fx - min) * 100f / (max - min);
      return (int)(Math.Min(Math.Max(trk, 0), 100));
    }

    private float TrkToFX(int trk, float min, float max) {
      //* convert trkbar val to FX val
      //* trkbar range: 0 - 100
      //* FX range: min to max
      if (trk == 0) return min;
      if (trk == 100) return max;
      float fx = (min + (max - min) * (float)trk / 100f);
      return Math.Min(Math.Max(fx, min), max);
    }

    //private void cmdResetReverb_Click(object sender, EventArgs e) {
    //  LoadReverb(ReverbDefaults);
    //  CurrentReverb.fInGain = TrkToFX(trkGain.Value, ReverbMinMax.GainMin, ReverbMinMax.GainMax);
    //  CurrentReverb.fReverbMix = TrkToFX(trkMix.Value, ReverbMinMax.MixMin, ReverbMinMax.MixMax);
    //  CurrentReverb.fReverbTime = TrkToFX(trkTime.Value, ReverbMinMax.TimeMin, ReverbMinMax.TimeMax);
    //  CurrentReverb.fHighFreqRTRatio = TrkToFX(trkHFRT.Value, ReverbMinMax.HFRTMin, ReverbMinMax.HFRTMax);
    //  EnableReverb();
    //}

    private void cmdResetFreeverb_Click(object sender, EventArgs e) {
      if (CurrentFreeverb == null) return;
      LoadFreeverb(FreeverbDefaults);
      CurrentFreeverb.fDryMix = TrkToFX(trkDryMix.Value, FreeverbMinMax.DryMixMin, FreeverbMinMax.DryMixMax);
      CurrentFreeverb.fWetMix = TrkToFX(trkWetMix.Value, FreeverbMinMax.WetMixMin, FreeverbMinMax.WetMixMax);
      CurrentFreeverb.fRoomSize = TrkToFX(trkRoomSize.Value, FreeverbMinMax.RoomSizeMin, FreeverbMinMax.RoomSizeMax);
      CurrentFreeverb.fDamp = TrkToFX(trkDamp.Value, FreeverbMinMax.DampMin, FreeverbMinMax.DampMax);
      CurrentFreeverb.fWidth = TrkToFX(trkWidth.Value, FreeverbMinMax.WidthMin, FreeverbMinMax.WidthMax);
      //EnableFreeverb();
    }

    //private void trkGain_Scroll(object sender, EventArgs e) {
    //  CurrentReverb.fInGain = TrkToFX(trkGain.Value, ReverbMinMax.GainMin, ReverbMinMax.GainMax);
    //  EnableReverb();
    //}

    //private void trkMix_Scroll(object sender, EventArgs e) {
    //  CurrentReverb.fReverbMix = TrkToFX(trkMix.Value, ReverbMinMax.MixMin, ReverbMinMax.MixMax);
    //  EnableReverb();
    //}

    //private void trkTime_Scroll(object sender, EventArgs e) {
    //  CurrentReverb.fReverbTime = TrkToFX(trkTime.Value, ReverbMinMax.TimeMin, ReverbMinMax.TimeMax);
    //  EnableReverb();
    //}

    //private void trkHFRT_Scroll(object sender, EventArgs e) {
    //  CurrentReverb.fHighFreqRTRatio = TrkToFX(trkHFRT.Value, ReverbMinMax.HFRTMin, ReverbMinMax.HFRTMax);
    //  EnableReverb();
    //}

    private void trkDryMix_Scroll(object sender, EventArgs e) {
      CurrentFreeverb.fDryMix = TrkToFX(trkDryMix.Value, FreeverbMinMax.DryMixMin, FreeverbMinMax.DryMixMax);
      EnableFreeverb();
    }

    private void trkWetMix_Scroll(object sender, EventArgs e) {
      CurrentFreeverb.fWetMix = TrkToFX(trkWetMix.Value, FreeverbMinMax.WetMixMin, FreeverbMinMax.WetMixMax);
      EnableFreeverb();
    }

    private void trkRoomSize_Scroll(object sender, EventArgs e) {
      CurrentFreeverb.fRoomSize = TrkToFX(trkRoomSize.Value, FreeverbMinMax.RoomSizeMin, FreeverbMinMax.RoomSizeMax);
      EnableFreeverb();
    }

    private void trkDamp_Scroll(object sender, EventArgs e) {
      CurrentFreeverb.fDamp = TrkToFX(trkDamp.Value, FreeverbMinMax.DampMin, FreeverbMinMax.DampMax);
      EnableFreeverb();
    }

    private void trkWidth_Scroll(object sender, EventArgs e) {
      CurrentFreeverb.fWidth = TrkToFX(trkWidth.Value, FreeverbMinMax.WidthMin, FreeverbMinMax.WidthMax);
      EnableFreeverb();
    }

    //private void EnableReverb() {
    //  if (!chkEnableReverb.Checked) chkEnableReverb.Checked = true;
    //  else if (BassOut != null) BassOut.SetReverb();
    //}

    private void EnableFreeverb() {
      if (!chkEnableFreeverb.Checked) chkEnableFreeverb.Checked = true;
      else if (BassOut != null) BassOut.SetFreeverb();
    }

    //private void chkEnableReverb_CheckedChanged(object sender, EventArgs e) {
    //  clsBassOut.indReverb[(int)Type] = chkEnableReverb.Checked;
    //  if (BassOut != null) {
    //    if (chkEnableReverb.Checked) BassOut.SetReverb(); else BassOut.UnsetReverb();
    //  }
    //}

    private void chkEnableFreeverb_CheckedChanged(object sender, EventArgs e) {
      if (BassOut == null) return;
      clsBassOutMidi.indFreeverb[(int)Type] = chkEnableFreeverb.Checked;
      if (BassOut != null) {
        if (chkEnableFreeverb.Checked) BassOut.SetFreeverb(); else BassOut.UnsetFreeverb();
      }
    }

    private void frmFX_FormClosed(object sender, FormClosedEventArgs e) {
      if (BassOut != null) BassOut.frmFX = null;
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Help.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_FX.htm");
    }
  }
}
