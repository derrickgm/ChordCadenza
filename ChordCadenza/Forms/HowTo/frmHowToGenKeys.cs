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
  public partial class frmHowToGenKeys : Form {
    public frmHowToGenKeys() {
      InitializeComponent();
    }

    private void frmHowToGenKeys_Load(object sender, EventArgs e) {
      FormBorderStyle = FormBorderStyle.FixedSingle;
      AutoScroll = false;
      MaximizeBox = false;
      MinimizeBox = false;
      Forms.frmInitial.ApplyBoldAll(this);
    }

    private void cmdLoadMidiFile_Click(object sender, EventArgs e) {
      P.frmSC.LoadSongClick(Cfg.SamplesPathMID);
    }

    //private void cmdShowTrackMap_Click(object sender, EventArgs e) {
    //  P.frmSC.cmdMultiMap_Click(null, null);
    //}

    //private void cmdSelectAllTrks_Click(object sender, EventArgs e) {
    //  if (!CheckTrackMap()) return;
    //  P.F.frmMultiMap.cmdCheckAll_Click(null, null);
    //}

    //private void cmdShowCalcKeys_Click(object sender, EventArgs e) {
    //  if (!CheckTrackMap()) return; 
    //  P.F.frmMultiMap.cmdCalcKeys_Click(null, null);
    //}

    //private static bool CheckTrackMap() {
    //  if (!P.F.MidiFileLoaded) {
    //    MessageBox.Show("MidiFile not loaded");
    //    return false;
    //  }
    //  if (P.F == null || P.F.frmMultiMap == null) {
    //    P.frmSC.cmdMultiMap_Click(null, null);
    //  }
    //  P.F.frmMultiMap.Activate();
    //  return true;
    //}

    //private void cmdCalcKeysApplyAndClose_Click(object sender, EventArgs e) {

    //}
  }
}
