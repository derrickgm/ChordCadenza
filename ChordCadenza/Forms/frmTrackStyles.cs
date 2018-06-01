using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChordCadenza.Forms {
  public partial class frmTrackStyles : Form {
    public frmTrackStyles() {
      InitializeComponent();
      Forms.frmSC.ZZZSetPCKBEvs(this);
      Chks = new CheckBox[4, NumStyles];  //[action, style]
      Counts = new int[NumStyles];  //[style]
    }

    private readonly int NumStyles = Enum.GetNames(typeof(clsFileStream.eTrkType)).Length;
    private CheckBox[,] Chks;  //[action, style]
    internal enum eAction : int { Select, Mute, Collapse, Hide };
    private int[] Counts;

    private void frmTrackStyles_Load(object sender, EventArgs e) {
      foreach (Control ctl in panChks.Controls) {
        CheckBox chk = (CheckBox)ctl;
        int action;
        int style = Math.DivRem(chk.TabIndex, 4, out action);
        Chks[action, style] = chk;
      }
      SetChks();
      UpdateCounts();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
      bool? ret = Forms.frmSC.StaticProcessCmdKey(ref msg, keyData);
      if (!ret.HasValue) return base.ProcessCmdKey(ref msg, keyData);
      return ret.Value;
    }

    internal void UpdateCounts() {
      if (P.F.frmTrackMap == null) return;
      for (int i = 0; i < Counts.Length; i++) Counts[i] = 0;
      foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {
        Counts[(int)P.F.FSTrackMap.TrkType[trk]]++;
      }
      foreach (Control ctl in panCounts.Controls) {
        Label lbl = (Label)ctl;
        lbl.Text = Counts[lbl.TabIndex].ToString();
      }
    }

    private void chk_Click(object sender, EventArgs e) {
      if (P.F?.frmTrackMap == null) return;
      CheckBox chk = (CheckBox)sender;
      if (chk.CheckState == CheckState.Indeterminate) chk.CheckState = CheckState.Unchecked;
      int action;
      int style = Math.DivRem(chk.TabIndex, 4, out action);
      try {
        P.F.frmTrackMap.indLocatePics = false;
        foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {
          if ((int)P.F.FSTrackMap.TrkType[trk] != style) continue;
          switch (action) {
            case (int)eAction.Select:
              if (P.F.frmTrackMap.Chks[trk] == null) break;
              P.F.frmTrackMap.Bypass_Event = true;
              P.F.frmTrackMap.Chks[trk].Checked = chk.Checked;
              P.F.frmTrackMap.Bypass_Event = false;
              break;
            case (int)eAction.Mute:
              if (P.F.frmTrackMap.chkMutes[trk] == null) break;
              P.F.frmTrackMap.chkMutes[trk].Checked = chk.Checked;
              break;
            case (int)eAction.Collapse:
              if (P.F.frmTrackMap.chkCollapses[trk] == null) break;
              P.F.frmTrackMap.chkCollapses[trk].Checked = chk.Checked;
              break;
            case (int)eAction.Hide:
              P.F.frmTrackMap.TrkShow[trk] = !chk.Checked;
              Chks[(int)eAction.Collapse, style].Enabled = !chk.Checked;
              break;
          }
          P.frmSC.chkShowTracks_CheckedChanged(null, null);
        }
      }
      finally {
        if (action == (int)eAction.Hide) {
          P.F.frmTrackMap.chkShowCheckedChanged(false, false);
        } else {
          P.F.frmTrackMap.LocatePics();
        }

        P.F.frmTrackMap.indLocatePics = true;
      }
    }

    internal void SetChks() {
      if (P.F?.frmTrackMap == null) return;
      foreach (CheckBox chk in Chks) {
        chk.Enabled = false;
        chk.CheckState = CheckState.Unchecked;
      }
      foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {
        CheckBox chk = P.F.frmTrackMap.Chks[trk];
        if (chk == null) continue;
        SetChk(trk, chk.Checked, eAction.Select);
        SetChk(trk, P.F.Mute[trk], eAction.Mute);
        if (P.F.frmTrackMap.TrkShow[trk]) {
          SetChk(trk, P.F.frmTrackMap.chkCollapses[trk].Checked, eAction.Collapse);
        }
        SetChk(trk, !P.F.frmTrackMap.TrkShow[trk], eAction.Hide);
      }
    }

    private void SetChk(clsTrks.T trk, bool arg, eAction action) {
      if (P.F.FSTrackMap == null) return;
      CheckBox chk = Chks[(int)action, (int)P.F.FSTrackMap.TrkType[trk]];
      if (!chk.Enabled) {
        chk.Enabled = true;
        chk.Checked = arg;
      } else {
        if (chk.CheckState != CheckState.Indeterminate) {
          if (arg != chk.Checked) chk.CheckState = CheckState.Indeterminate;
        }
      }
    }

    private void frmTrackStyles_FormClosed(object sender, FormClosedEventArgs e) {
      P.F.frmTrackStyles = null;
    }
  }
}
