using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChordCadenza.Forms {
  public partial class dlgInsertBars : Form {
    internal dlgInsertBars(Forms.frmChordMap frm, int barlo, int barhi) {
      InitializeComponent();
      Frm = frm;
      BarLo = barlo;
      BarHi = barhi;
    }

    private Forms.frmChordMap Frm;
    private int BarLo, BarHi;
    internal clsMTime.clsTSig TSig;

    private void frmInsertBars_Load(object sender, EventArgs e) {
      lblMsg.Text = "Insert " + (BarHi - BarLo) + " bar(s) at bar " + (BarLo + 1);
      int tickslo = Frm.PixToTicks(Frm.CsrPixLo);
      TSig = P.F.MTime.GetTSig(tickslo);
      cmbTSigs.Items.Add(TSig.ToString());  //current tsig
      foreach (clsNNDD nndd in clsMTime.clsTSig.AllTSigs) {
        cmbTSigs.Items.Add(nndd.ToString());
      }
      cmbTSigs.SelectedIndex = 0;  //current tsig
    }

    private void cmbTSigs_SelectedIndexChanged(object sender, EventArgs e) {
      if (cmbTSigs.SelectedIndex > 0) {
        clsNNDD nndd = clsMTime.clsTSigBB.AllTSigs[cmbTSigs.SelectedIndex - 1];
        TSig = new clsMTime.clsTSig(P.F.MTime, nndd.NN, nndd.DD);
      }
    }
  }
}
