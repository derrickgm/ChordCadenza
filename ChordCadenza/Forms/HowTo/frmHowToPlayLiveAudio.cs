using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MPlay.Forms.HowTo {
  public partial class frmHowToPlayLiveAudio : Form {
    public frmHowToPlayLiveAudio() {
      InitializeComponent();
    }

    private void cmdConfig_Click(object sender, EventArgs e) {
      P.frmStart.Show();
      P.frmStart.Activate();
    }

    private void cmdShowPlayMap_Click(object sender, EventArgs e) {
      P.frmSC.Activate();
    }

    private void frmHowToPlayLiveAudio_Load(object sender, EventArgs e) {
      FormBorderStyle = FormBorderStyle.FixedSingle;
      MaximizeBox = false;
      MinimizeBox = false;
      Forms.frmInitial.ApplyBoldAll(this);
    }

    //private void cmdLoadSong_Click(object sender, EventArgs e) {
    //  P.frmSC.LoadSongClick(Cfg.SamplesPathCHP_MID);
    //}
  }
}
