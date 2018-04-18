using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MPlay.Forms.HowTo {
  public partial class frmHowToPlayAudio : Form {
    public frmHowToPlayAudio() {
      InitializeComponent();
    }

    private void frmHowToPlayAudioFile_Load(object sender, EventArgs e) {
      FormBorderStyle = FormBorderStyle.FixedSingle;
      MaximizeBox = false;
      MinimizeBox = false;
      Forms.frmInitial.ApplyBoldAll(this);
    }

    private void cmdHowToSwitchKey_Click(object sender, EventArgs e) {
      Forms.HowTo.frmHowToKeySwitches frm = new HowTo.frmHowToKeySwitches();
      frm.Show();
    }

    //private void cmdLoadSong_Click(object sender, EventArgs e) {
    //  P.frmSC.LoadSongClick(Cfg.SamplesPathCHP_MID);
    //  P.frmSC.cmdChordMap_Click(null, null);
    //}

    private void cmdLiveAudio_Click(object sender, EventArgs e) {
      Forms.HowTo.frmHowToPlayLiveAudio frm = new HowTo.frmHowToPlayLiveAudio();
      frm.Show();
    }

    private void cmdSyncAudioSource_Click(object sender, EventArgs e) {
      Forms.HowTo.frmHowToSyncAudioSource frm = new HowTo.frmHowToSyncAudioSource();
      frm.Show();
    }

    private void richTextBox4_TextChanged(object sender, EventArgs e) {

    }

    //private void cmdSync_Click(object sender, EventArgs e) {
    //  Forms.HowTo.frmHowToSyncAudioFile frm = new HowTo.frmHowToSyncAudioFile();
    //  frm.Show();
    //}
  }
}
