using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChordCadenza.Forms.Colours {
  public partial class frmColoursTonnetz : Forms.frmColours {
    public frmColoursTonnetz(clsColors colors) {
      Colors = colors;
      InitializeComponent();
    }

    private new void cmd_Click(object sender, EventArgs e) {
      base.cmd_Click(sender, e);
    }

    private void cmdBackground_Click(object sender, EventArgs e) {
      base.cmd_Click(sender, e);
      if (P.F?.frmTonnetz != null) {
        P.F.frmTonnetz.picPC.BackColor = panMain.BackColor;
      }
    }

    private void frmColoursTonnetz_Load(object sender, EventArgs e) {
      clsTT.LoadToolTips(this);
    }
  }
}
