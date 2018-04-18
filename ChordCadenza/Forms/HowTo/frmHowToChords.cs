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
  public partial class frmHowToChords : Form {
    public frmHowToChords() {
      InitializeComponent();
    }

    private void frmHowToChords_Load(object sender, EventArgs e) {
      FormBorderStyle = FormBorderStyle.FixedSingle;
      AutoScroll = false;
      MaximizeBox = false;
      MinimizeBox = false;
      Forms.frmInitial.ApplyBoldAll(this);
    }

    private void cmdLoadSong_Click(object sender, EventArgs e) {
      P.frmSC.LoadSongClick(Cfg.SamplesPathEmptyCHP_MID);
      P.frmSC.cmdChordMap_Click(null, null);
     }

    private void cmdShowChordMap_Click(object sender, EventArgs e) {
      if (!P.F.MidiFileLoaded) {
        MessageBox.Show("MidiFile not loaded");
        return;
      }
      P.frmSC.cmdChordMap_Click(null, null);
    }

    private void cmdShowPlayMap_Click(object sender, EventArgs e) {
      P.frmSC.Activate();
    }
  }
}
