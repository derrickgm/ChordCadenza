using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MPlay.Forms.HowTo {
  public partial class frmHowToUpdateTSigs : Form {
    public frmHowToUpdateTSigs() {
      InitializeComponent();
    }

    private void frmHowToUpdateTSigs_Load(object sender, EventArgs e) {
      FormBorderStyle = FormBorderStyle.FixedSingle;
      MaximizeBox = false;
      MinimizeBox = false;
      Forms.frmInitial.ApplyBoldAll(this);
    }

    private void cmdLoadChordFile_Click(object sender, EventArgs e) {
      P.frmSC.LoadSongClick(Cfg.SamplesPath);
    }

    private void cmdShowChordMap_Click(object sender, EventArgs e) {
      //if (!P.F.MidiFileLoaded) {
      //  MessageBox.Show("MidiFile not loaded");
      //  return;
      //}
      P.frmSC.cmdChordMap_Click(null, null);
    }
  }
}
