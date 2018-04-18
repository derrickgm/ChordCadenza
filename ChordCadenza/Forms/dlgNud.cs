using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChordCadenza.Forms {
  public partial class dlgNud : Form {
    public dlgNud() {
      InitializeComponent();
    }

    private void cmdOK_Click(object sender, EventArgs e) {
      Close();
    }

    private void cmdCancel_Click(object sender, EventArgs e) {
      Close();
    }

    private void frmDialogNud_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
    }

    private void nud1_Validating(object sender, CancelEventArgs e) {
      //if (nud1.Value > nud1.Maximum)
    }
  }
}
