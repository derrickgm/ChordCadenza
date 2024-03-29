﻿using System;
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
  public partial class frmHowToPlayMelody : Form {
    public frmHowToPlayMelody() {
      InitializeComponent();
    }

    private void frmHowToPlayMelody_Load(object sender, EventArgs e) {
      FormBorderStyle = FormBorderStyle.FixedSingle;
      MaximizeBox = false;
      MinimizeBox = false;
      Forms.frmInitial.ApplyBoldAll(this);
    }

    private void cmdLoadSong_Click(object sender, EventArgs e) {
      P.frmSC.LoadSongClick(Cfg.SamplesPathCHP_MID);
    }

    private void cmdShowPlayMap_Click(object sender, EventArgs e) {
      P.frmSC.Activate();
    }

    private void cmdKeyboardMode_Click(object sender, EventArgs e) {
      if (!P.F.MidiFileLoaded) {
        MessageBox.Show("MidiFile not loaded");
        return;
      }
      if (P.frmSC.PlayMode == frmSC.ePlayMode.Chords) P.frmSC.SwitchPlayMode();
    }

    private void cmdChordMode_Click(object sender, EventArgs e) {
      if (!P.F.MidiFileLoaded) {
        MessageBox.Show("MidiFile not loaded");
        return;
      }
      if (P.frmSC.PlayMode == frmSC.ePlayMode.KB) P.frmSC.SwitchPlayMode();
    }
  }
}
