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
  public partial class frmColours : Form, ITT {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    public frmColours() {
      //Colors = colors;
      InitializeComponent();
    }

    protected clsColors Colors;
    //internal bool indLoaded = false;  //set if this has ever been loaded/shown

    //private void frmShowChordsColours_Load(object sender, EventArgs e) {
    //  //LoadFrm();
    //}

    private void cmdClose_Click(object sender, EventArgs e) {
      Hide();
    }

    private void cmdSetDefaults_Click(object sender, EventArgs e) {
      Colors.SetDefaults();
    }

    private void cmdSave_Click(object sender, EventArgs e) {
      string msg = Colors.SaveFile();
      //if (msg == "") MessageBox.Show("Colours File Saved");
      //else MessageBox.Show("Save Colors File failed: " + msg);
    }

    //private void cmdLoad_Click(object sender, EventArgs e) {
    //  if (Colors.LoadFile()) MessageBox.Show("Colours File Loaded");
    //}

    protected void trk_Scroll(object sender, EventArgs e) {
      TrackBar trk = (TrackBar)sender;
      Panel pan = (Panel)trk.Parent;
      //P.ColorsShowChords[(string)pan.Tag].SetAlpha(trk.Value);
      string key = Colors.GetCmd(pan).Text;
      Colors[key].SetAlpha(trk.Value);
    }

    protected void cmd_Click(object sender, EventArgs e) {
      //foreach (Control ctl in panBlackKeyboard.Controls) {  //test
      //  Debug.WriteLine("ctl = " + ctl.Name);    //test
      //}
      Button button = (Button)sender;
      Panel pan = (Panel)button.Parent;
      string key = Colors.GetCmd(pan).Text;
      Colors[key].SetColorDialog(colorDialog1);
    }

    private void frmColours_Load(object sender, EventArgs e) {
      //indLoaded = true;
    }
  }
}
