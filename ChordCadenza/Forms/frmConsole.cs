using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChordCadenza.Forms {
  public partial class frmConsole : Form {
    public frmConsole() {
      InitializeComponent();
    }

    internal static void WriteLine(string txt) {
      #if ADVANCED
        P.frmConsole.txt1.Text += txt + "\r\n";
      #endif
    }
  }
}
