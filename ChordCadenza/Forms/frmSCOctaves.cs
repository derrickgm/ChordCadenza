using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChordCadenza.Forms {
  public partial class frmSCOctaves : Form, ITT, IFormStream {
    private bool indChanged = false;
    private bool Bypass_Event = true;

    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    internal frmSCOctaves(Forms.frmSC frmsc) {
      InitializeComponent();
      Forms.frmSC.ZZZSetPCKBEvs(this);
      frmSC = frmsc;
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
      bool? ret = Forms.frmSC.StaticProcessCmdKey(ref msg, keyData);
      if (!ret.HasValue) return base.ProcessCmdKey(ref msg, keyData);
      return ret.Value;
    }

    public void FormStreamOnOff(bool on) {
      ////if (on) Close();
    }

    //private static string[] CArray = new string[9];  //-1 to 7
    private Forms.frmSC frmSC;

    static frmSCOctaves() {
      //for (int i = 0; i < 8; i++) CArray[i] = "C" + i + "  (" + ((i + 2) * 12 + ")");  //C0-C7 (24-108) 
      //for (int i = 0; i < 9; i++) CArray[i] = "C" + (i - 1) + "  (" + ((i + 1) * 12 + ")");  //C-1,C0-C7 (12-108) 
      //for (int i = 0; i < 9; i++) CArray[i] = "C" + (i) + "  (" + ((i + 1) * 12 + ")");  //C0-C7 (12-108) (C4=60=middleC)
    }

    private void frmOctaves_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
#if !ADVANCED
        lblPlayHiC.Hide();
        cmbPlayHiC.Hide();
#endif

      if (P.PCKB != null) cmbPlayLoC.Enabled = false;
      if (P.PCKB != null) lblPlayLoC.Enabled = false;

      PopulateCmb(cmbLowShowC, Forms.frmSC.valShowLowCDflt, 1, 8);
      PopulateCmb(cmbPlayLoC, Forms.frmSC.valPlayLoC, 1, 8);
      PopulateCmb(cmbPlayHiC, Forms.frmSC.valPlayHiC, 2, 9);
      Bypass_Event = false;
    }

    private void PopulateCmb(ComboBox cmb, int val, int octlo, int octhi) {
      for (int oct = octlo; oct < octhi; oct++) {  //C1 - C8 (24 - 108)
        int note = (oct + 1) * 12;  //midi
        string item = NoteName.MidiToNoteNameAndOctave(note);
        item += " (" + note + ")";
        cmb.Items.Add(item);
      }
      string notename = NoteName.MidiToNoteNameAndOctave(val);  //e.g. 60 -> C4
      SelectCmbItem(cmb, notename);
    }

    private void SelectCmbItem(ComboBox cmb, string notename) {  //e.g. notename = C4
      foreach (object obj in cmb.Items) {
        string item = (string)obj;
        string[] f = item.Split(new char[] { ' ' });
        if (notename != f[0]) continue;
        cmb.SelectedItem = obj;
        break;
      }
    }

    //private void cmdOK_Click(object sender, EventArgs e) {
    //  Apply();
    //  Close();
    //}

    private void Apply() {
      int playloc = GetSelectedMidi(cmbPlayLoC);
      int showloc = GetSelectedMidi(cmbLowShowC);
      int playhic = GetSelectedMidi(cmbPlayHiC);
      frmSC.SetRanges(playloc, showloc, playhic);

      SelectCmbItem(cmbLowShowC, NoteName.MidiToNoteNameAndOctave(Forms.frmSC.valShowLowCDflt));
      SelectCmbItem(cmbPlayLoC, NoteName.MidiToNoteNameAndOctave(Forms.frmSC.valPlayLoC));
      SelectCmbItem(cmbPlayHiC, NoteName.MidiToNoteNameAndOctave(Forms.frmSC.valPlayHiC));
    }

    //private void cmdCancel_Click(object sender, EventArgs e) {
    //  Close();
    //  P.F.Panic();
    //}

    //private void cmdSetPlayLoC_Click(object sender, EventArgs e) {
    //  if (lblNoteName.Text == "???") return;
    //  SelectCmbItem(cmbPlayLoC, lblNoteName.Text);
    //}

    //private void cmdSetLowShowC_Click(object sender, EventArgs e) {
    //  if (lblNoteName.Text == "???") return;
    //  SelectCmbItem(cmbLowShowC, lblNoteName.Text);
    //}

    private int GetSelectedMidi(ComboBox cmb) {
      string[] f = ((string)cmb.SelectedItem).Split(new char[] { ' ' });
      return NoteName.NoteNameAndOctaveToMidi(f[0]); 
    }

    private void cmdClose_Click(object sender, EventArgs e) {
      if (indChanged) {
        if (MessageBox.Show("Apply changes?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
          cmdApply_Click(null, null);
        }
      }
      indChanged = false;
      Close();
    }

    private void frmSCOctaves_FormClosed(object sender, FormClosedEventArgs e) {
      P.frmSCOctaves = null;
    }

    private void cmdApply_Click(object sender, EventArgs e) {
      Apply();
      P.F.Panic();  //switch off any hanging notes
      indChanged = false;
    }

    private void cmb_SelectedIndexChanged(object sender, EventArgs e) {
      if (Bypass_Event) return;
      indChanged = true;
    }

    internal void SetNoteName(int note) {
      string txt = NoteName.MidiToNoteNameAndOctave(note);
      txt += " (" + note + ")";
      P.frmSCOctaves.lblNoteName.Text = txt;
    }
  }
}
