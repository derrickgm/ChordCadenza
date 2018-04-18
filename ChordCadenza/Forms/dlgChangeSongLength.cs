using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChordCadenza.Forms {
  public partial class dlgChangeSongLength : Form {
    public dlgChangeSongLength() {
      InitializeComponent();
    }

    private void frmChangeSongLength_Load(object sender, EventArgs e) {
      //* get current length
      lblCurrentLen.Text = P.F.MaxBBT.MidiWholeNotes.ToString();
      lblCurrentBars.Text = (P.F.MaxBBT.Bar + 1).ToString();

      //* get last ev ticks/wholenotes
      int minlen = 10;
      clsCF.clsEv ev = null;
      for (int i = P.F.CF.Evs.Count - 1; i >= 0; i--) {
        ev = P.F.CF.Evs[i];
        if (ev != null) break;
      }
      if (ev == null) return;
      int rem;
      int wholenotes = Math.DivRem(ev.OffTime, P.F.MTime.TicksPerQNote * 4, out rem);
      if (rem > 0) wholenotes++;

      ////* find lowest wholenotes that fits
      //clsMaxBBT maxbbt = null;
      //while (maxbbt == null || maxbbt.Ticks > ev.OffTime) {
      //  maxbbt = new clsMaxBBT(wholenotes * P.F.MTime.TicksPerQNote * 4);
      //  wholenotes--;
      //}

      wholenotes = Math.Max(minlen, ++wholenotes);
      clsMaxBBT maxbbt = new clsMaxBBT(wholenotes * P.F.MTime.TicksPerQNote * 4);

      //* update controls
      lblMinLen.Text = wholenotes.ToString();
      lblMinBars.Text = (maxbbt.Bar + 1).ToString();
      nudNewLen.Minimum = wholenotes;
      nudNewLen.Value = Math.Max(wholenotes, P.F.MaxBBT.MidiWholeNotes);
    }

    private void cmdOK_Click(object sender, EventArgs e) {
      if (P.F?.CF?.Evs == null) return;
      if (P.F.Project.MidiExists) return;
      if (P.F.frmChordMap == null) return;
      using (new clsChordMapDis(P.F.frmChordMap)) {
        int ticks = (int)nudNewLen.Value * P.F.MTime.TicksPerQNote * 4;
        P.F.MaxBBT = new clsMaxBBT(ticks);
        P.F.CF.NoteMap.ReInitMapAndAtts();

        P.F.frmChordMap?.NewMTime();
        //P.F.frmChordMap?.RefreshDGV();
        //P.F.frmChordMap?.HResize();
        //P.frmSC.vScrollBar1.Maximum = P.F.MaxBBT.Bar;
        //P.frmSC.nudStartBar.Maximum = P.F.MaxBBT.Bar;
        //P.F.frmChordMap?.Refresh();
      }
      P.F.CF.indSave = true;
      P.F.CF.UndoRedoCF.Update();
      //P.F.frmChordMap?.BeatChords.ShowDGVCells();
      //Forms.frmStart.RefreshBBT(P.F.CurrentBBT);
    }

    private void nudNewLen_ValueChanged(object sender, EventArgs e) {
      clsMaxBBT maxbbt = new clsMaxBBT((int)nudNewLen.Value * P.F.MTime.TicksPerQNote * 4);
      lblNewBars.Text = (maxbbt.Bar + 1).ToString();
    }
  }
}
