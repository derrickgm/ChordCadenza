using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChordCadenza.Forms {
  public partial class dlgSaveMidiFileAs : Form, ITT, IFormStream {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    public dlgSaveMidiFileAs() {
      InitializeComponent();
    }

    private void frmSaveMidiFileAs_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      clsTT.LoadToolTips(this);
    }

    public void FormStreamOnOff(bool on) {
      if (on) Close();
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Help.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_SaveMidiFileAs_Intro.htm");
    }
  }
}
