using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MPlay.Forms.HowTo {
  public partial class frmHowToSyncAudioSource : Form {
    public frmHowToSyncAudioSource() {
      InitializeComponent();
    }

    private void frmHowToSync_Load(object sender, EventArgs e) {
      FormBorderStyle = FormBorderStyle.FixedSingle;
      AutoScroll = false;
      MaximizeBox = false;
      MinimizeBox = false;
    }

    private void cmdSyncStream_Click(object sender, EventArgs e) {
      Forms.HowTo.frmHowToSyncAudioStream frm = new HowTo.frmHowToSyncAudioStream();
      frm.Show();
    }

    private void cmdSyncFile_Click(object sender, EventArgs e) {
      Forms.HowTo.frmHowToSyncAudioFile frm = new HowTo.frmHowToSyncAudioFile();
      frm.Show();
    }

    private void cmdPlayStream_Click(object sender, EventArgs e) {
      Forms.HowTo.frmHowToPlayAudio frm = new HowTo.frmHowToPlayAudio();
      frm.Show();
    }

    private void cmdPlayFile_Click(object sender, EventArgs e) {
      Forms.HowTo.frmHowToPlayAudio frm = new HowTo.frmHowToPlayAudio();
      frm.Show();
    }

    private void cmdSyncComp_Click(object sender, EventArgs e) {
      Forms.HowTo.frmCompAudioSync frm = new HowTo.frmCompAudioSync();
      frm.Show();
    }

    private void cmdLiveStream_Click(object sender, EventArgs e) {
      Forms.HowTo.frmHowToPlayLiveAudio frm = new HowTo.frmHowToPlayLiveAudio();
      frm.Show();
    }
  }
}
