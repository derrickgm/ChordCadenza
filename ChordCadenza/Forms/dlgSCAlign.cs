using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChordCadenza.Forms {
  public partial class dlgSCAlign : Form, ITT, IFormStream {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    public dlgSCAlign() {
      InitializeComponent();
    }

    public void FormStreamOnOff(bool on) {
      if (on) Close();
    }

    private void cmdCancel_Click(object sender, EventArgs e) {
      Close();
    }

    private void frmSCAlign_FormClosed(object sender, FormClosedEventArgs e) {
      P.frmSCAlign = null;
    }

    private void frmSCAlign_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
    }
  }
}
