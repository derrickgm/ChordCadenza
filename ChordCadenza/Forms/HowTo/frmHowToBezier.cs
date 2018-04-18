using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MPlay.Forms.HowTo {
  public partial class frmHowToBezier : Form {
    public frmHowToBezier() {
      InitializeComponent();
    }

    private void frmHowToBezier_Load(object sender, EventArgs e) {
      FormBorderStyle = FormBorderStyle.FixedSingle;
      AutoScroll = false;
      MaximizeBox = false;
      MinimizeBox = false;
      Forms.frmInitial.ApplyBoldAll(this);
    }

    private void cmdShowChordMap_Click(object sender, EventArgs e) {
      P.frmSC.Activate();
    }
  }
}
