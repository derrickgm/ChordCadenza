using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChordCadenza.Forms {
  public partial class frmTest : Form {
    private Color ClickColor = Color.Green;

    private frmConfigBass frmConfigBass;
    private frmMidiDevs frmMidiDevs;

    public frmTest() {
      InitializeComponent();
    }

    private void DoCmdClick(Button cmdhere, Button cmdthere) {
      cmdthere.PerformClick();
      cmdhere.BackColor = ClickColor;
    }

    private void DoCheck(Button cmd, CheckBox chk, bool val) {
      chk.Checked = val;
      cmd.BackColor = ClickColor;
    }

    private void cmdShowFrmAudio_Click(object sender, EventArgs e) {
      frmConfigBass = new frmConfigBass();
      frmConfigBass.Show();
      ((Button)sender).BackColor = ClickColor;
    }

    private void cmdShowFrmMidiDevs_Click(object sender, EventArgs e) {
      frmMidiDevs = new Forms.frmMidiDevs();
      frmMidiDevs.Show();
      ((Button)sender).BackColor = ClickColor;
    }

    private void cmdAudioDisconnect_Click(object sender, EventArgs e) {
      DoCmdClick((Button)sender,  frmConfigBass.cmdDisconnect);
    }

    private void cmdAudioConnectAll_Click(object sender, EventArgs e) {
      DoCmdClick((Button)sender, frmConfigBass.cmdConnectAll);
    }

    private void cmdAudioHelp_Click(object sender, EventArgs e) {
      DoCmdClick((Button)sender, frmConfigBass.cmdHelp);
    }

    private void cmdUncheckAsio_Click(object sender, EventArgs e) {
      DoCheck((Button)sender, frmConfigBass.chkAsio, false);
    }

    private void cmdCheckAsio_Click(object sender, EventArgs e) {
      DoCheck((Button)sender, frmConfigBass.chkAsio, true);
    }

    private void cmdLoadTestProject_Click(object sender, EventArgs e) {
      P.frmStart.LoadProject("", true);
      ((Button)sender).BackColor = ClickColor;
    }

    private void button11_Click(object sender, EventArgs e) {

    }
    /*
    autosync
    calckeys
   *bezier
    chordgen
   *cfg chordgen
    chord ranks
    config audio
    config midi
   *FX
   *lyrics
   *edit lyrics
   *manchordsync
    switchkeys
    summary
    trackmap
   *track styles
   *chordmap transpose
    playmap transpose
   *chordmap cut/copy/paste
   *change tsigs
   *change keysigs 
    edit chordnames (DGV)



  */
  }
}
