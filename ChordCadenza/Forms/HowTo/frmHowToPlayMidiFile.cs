using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace MPlay.Forms.HowTo {
  public partial class frmHowToPlayMidiFile : Form {
    public frmHowToPlayMidiFile() {
      InitializeComponent();
    }

    private void frmHowToPlayMidiFile_Load(object sender, EventArgs e) {
      FormBorderStyle = FormBorderStyle.FixedSingle;
      MaximizeBox = false;
      MinimizeBox = false;
      Forms.frmInitial.ApplyBoldAll(this);
    }

    private void cmdShowTrackMap_Click(object sender, EventArgs e) {
      P.frmSC.cmdMultiMap_Click(null, null);
    }

    private void cmdShowPlayMap_Click(object sender, EventArgs e) {
      P.frmSC.Activate();
    }

    private void cmdLoadSong_Click(object sender, EventArgs e) {
      P.frmSC.LoadSongClick(Cfg.SamplesPathCHP_MID);
    }

    private void cmdPlayMelody_Click(object sender, EventArgs e) {
      (new Forms.HowTo.frmHowToPlayMelody()).Show();
    }

    private void cmdPlayChords_Click(object sender, EventArgs e) {
      (new Forms.HowTo.frmHowToPlayChords()).Show();
    }
  }
}
