using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ChordCadenza.Forms {
  public partial class frmColoursNM : Forms.frmColours {
    public frmColoursNM(clsColors colors) {
      Colors = colors;
      InitializeComponent();
      //if (!P.Advanced) {
      //  panBlack0.Hide();
      //  panBlack1.Hide();
      //  panBlack2.Hide();
      //  panPink.Hide();
      //}
    }

    private new void trk_Scroll(object sender, EventArgs e) {
      base.trk_Scroll(sender, e);
    }

    private new void cmd_Click(object sender, EventArgs e) {
      base.cmd_Click(sender, e);
    }

    private void frmColoursNM_Load(object sender, EventArgs e) {
      clsTT.LoadToolTips(this);
    }
  }
}
