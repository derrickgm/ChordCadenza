using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MPlay.Forms.HowTo {
  public partial class frmHowToKeySwitches : Form {
    public frmHowToKeySwitches() {
      InitializeComponent();
    }

    private void frmHowToKeySwitches_Load(object sender, EventArgs e) {
      FormBorderStyle = FormBorderStyle.FixedSingle;
      MaximizeBox = false;
      MinimizeBox = false;
      Forms.frmInitial.ApplyBoldAll(this);
    }

    private void cmdShowPlayMap_Click(object sender, EventArgs e) {
      P.frmSC.Activate();
    }

    private void cmdKBRanges_Click(object sender, EventArgs e) {
      P.frmSCOctaves = new Forms.frmSCOctaves(P.frmSC);
      P.frmSCOctaves.ShowDialog(this);
      P.frmSCOctaves = null;
    }

    private void button1_Click(object sender, EventArgs e) {
      P.frmStart.Show();
    }

    private void cmdSetKeySwitches_Click(object sender, EventArgs e) {
      P.frmSC.mnuCfgSwitch_Click(null, null);
    }
  }
}
