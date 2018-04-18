using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using ExtensionMethods;

namespace ChordCadenza.Forms {
  public partial class frmChordMapAdv : Form {
    public frmChordMapAdv() {
      InitializeComponent();
    }

    private void nudMinLen_ValueChanged(object sender, EventArgs e) {
      int dd, val;
      if (sender == nudMinLenDD) {
        val = frmStart.SetNudExp2(nudMinLenDD);
        if (val >= 0) dd = val; else return;
      } else {
        dd = (int)nudMinLenDD.Value;
      }
      P.F.frmChordMap.MinLenTicks = P.F.MTime.DD2Ticks((int)nudMinLenNN.Value, dd);
      //ApplyFilter();
      //Refresh();
    }

    private void nudCloseGap_ValueChanged(object sender, EventArgs e) {
      int dd, val;
      if (sender == nudCloseGapDD) {
        val = frmStart.SetNudExp2(nudCloseGapDD);
        if (val >= 0) dd = val; else return;
      } else {
        dd = (int)nudCloseGapDD.Value;
      }
      P.F.frmChordMap.CloseGapTicks = P.F.MTime.DD2Ticks((int)nudCloseGapNN.Value, dd);
      //ApplyFilter();
      //Refresh();
    }

    private void nudFilter_ValueChanged(object sender, EventArgs e) {
      int dd, val;
      if (sender == nudFilterDD) {
        val = frmStart.SetNudExp2(nudFilterDD);
        if (val >= 0) dd = val; else return;
      } else {
        dd = (int)nudFilterDD.Value;
      }
      P.F.frmChordMap.NoteFilterTicks = P.F.MTime.DD2Ticks((int)nudFilterNN.Value, dd);
      //ApplyFilter();
      //Refresh();
    }

    private void nudQuantize_ValueChanged(object sender, EventArgs e) {
      if (!optAlignQuantize.Checked) return;
      int dd, val;
      if (sender == nudQuantizeDD) {
        val = frmStart.SetNudExp2(nudQuantizeDD);
        if (val >= 0) dd = val; else return;
      } else {
        dd = (int)nudQuantizeDD.Value;
      }
      P.F.frmChordMap.QuantizeTicks = P.F.MTime.DD2Ticks((int)nudQuantizeNN.Value, dd);
      //ApplyFilter();
      //Refresh();
    }

    private void optAlign_CheckedChanged(object sender, EventArgs e) {
      if (sender is RadioButton) {
        RadioButton opt = (RadioButton)sender;
        if (!opt.Checked) return;
      }
      if (optAlignQuantize.Checked) {
        P.F.frmChordMap.Align = eAlign.Interval;
        P.F.frmChordMap.QuantizeTicks = P.F.MTime.DD2Ticks((int)nudQuantizeNN.Value, (int)nudQuantizeDD.Value);
        //nudChordWidth.Value = Math.Max(1, QuantizeTicks / CSVFileConv.TicksPerQI);
      } else if (optAlignBar.Checked) P.F.frmChordMap.Align = eAlign.Bar;
      else if (optAlignHalfBar.Checked) P.F.frmChordMap.Align = eAlign.HalfBar;
      else if (optAlignBeat.Checked) P.F.frmChordMap.Align = eAlign.Beat;
      else {
        LogicError.Throw(eLogicError.X042);
        P.F.frmChordMap.Align = eAlign.Beat;
      }
      //*for (int i = 0; i < 100; i++) {  //1 second
      //ApplyFilter();
      //*}
      //Refresh();
    }

    private void frmNoteMapAdv_Load(object sender, EventArgs e) {
      frmStart.SetNudAndTag(nudCloseGapDD);  //initial value
      frmStart.SetNudAndTag(nudFilterDD);  //initial value
      frmStart.SetNudAndTag(nudMinLenDD);  //initial value
      frmStart.SetNudAndTag(nudQuantizeDD);  //initial value
    }

    private void frmNoteMapAdv_FormClosed(object sender, FormClosedEventArgs e) {
      P.F.frmChordMapAdv.Hide();
      P.F.frmChordMapAdv = null;
    }

    //private void cmdCopySection_Click(object sender, EventArgs e) {
    //  //use CsrPos and confirm (was CsrHi/Lo)
    //  if (P.F.frmNoteMap.CsrPos < 0) return;
    //  int qi = P.F.frmNoteMap.CsrPos / P.F.frmNoteMap.HPixPerQI;
    //  char name = P.F.CF.QSections.GetName(qi);
    //  if (name == '*') return;
    //  string msg = "Confirm copy of section " + name + " to all linked sections";
    //  if (MessageBox.Show(msg, "Confirm Section Copy", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
    //  P.F.CF.QSections.CopyNoteMap(name);
    //  P.F.frmNoteMap.SetNoteMapFileChanged(false, true, -1, -1);
    //}

    private void optChordMatch_CheckedChanged(object sender, EventArgs e) {
      P.F.frmChordMap.optChord_CheckedChanged();
    }
  }
}
