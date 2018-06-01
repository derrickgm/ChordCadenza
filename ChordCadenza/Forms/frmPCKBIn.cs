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

namespace ChordCadenza.Forms {
  public partial class frmPCKBIn : Form {
    internal bool Bypass_Event = false;
    private delegate void delegSetPCKBVel(int val);
    private delegSetPCKBVel dSetPCKBVel;

    public frmPCKBIn() {
      InitializeComponent();
#if DEBUG
      ShowInTaskbar = true;
#endif
      //Forms.frmSC.SetPCKBEvs(this);
      dSetPCKBVel = new delegSetPCKBVel(SetPCKBVel);
      trkVel.Value = Cfg.PCKBVel;
    }

    private void frmPCKBIn_Load(object sender, EventArgs e) {
      //Location = new Point(0, 0);
    }

    private void frmPCKBIn_FormClosed(object sender, FormClosedEventArgs e) {
      P.frmPCKBIn = null;
    }

    //private int TempCount = 0;
    private void trkVel_KeyDown(object sender, KeyEventArgs e) {
      if (P.PCKB == null) return;
      //if (Control.ModifierKeys != Keys.None) return;
      //if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.Control || e.KeyCode == Keys.Alt) return;
      e.Handled = true;
      P.PCKB.KeyUpDown(e.KeyCode, true);
      lblLastAction.BackColor = Color.Green;
      //Debug.WriteLine("frmPCKBIn: KeyDown " + ++TempCount);
    }

    private void trkVel_KeyUp(object sender, KeyEventArgs e) {
      if (P.PCKB == null) return;
      //if (Control.ModifierKeys != Keys.None) return;
      //if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.Control || e.KeyCode == Keys.Alt) return;
      e.Handled = true;
      P.PCKB.KeyUpDown(e.KeyCode, false);
      if (P.frmSC.Play == null || MidiMon.NoMon || MidiMon.KeyCount == 0) {
        lblLastAction.BackColor = SystemColors.Control;
      }
    }

    private void trkVel_Scroll(object sender, EventArgs e) {
      TrackBar trk = (TrackBar)sender;
      Cfg.PCKBVel = trk.Value;
      P.frmSC.BeginInvoke(dSetPCKBVel, trk.Value);
    }

    private void SetPCKBVel(int val) {
      P.frmSC.trkPCKBVel.Value = val;
    }

    private void trkVel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
      //if (Forms.frmSC.KeyUpDownFilter(e)) return;
      e.IsInputKey = true;
    }
  }
}
