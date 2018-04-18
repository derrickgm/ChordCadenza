using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChordCadenza.Forms {
  public partial class frmManChordSync : Form, ITT {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    internal static bool Bypass_Event = false;
    //internal static bool Disabled = false;
    //internal static clsPlay.eManSyncAction SingleBlackAction = clsPlay.eManSyncAction.Play;
    //internal static clsPlay.eManSyncAction SingleWhiteAction = clsPlay.eManSyncAction.Play;
    //internal static bool NoSkipAfterReloc = true;
    //internal static bool indNextBlack = false;
    //internal static bool indNextWhite = false;

    public frmManChordSync() {
      InitializeComponent();

      //chkDisable.CheckedChanged += Control_Changed;

      optSingleBlackPrev.CheckedChanged += Control_Changed;
      optSingleBlackNext.CheckedChanged += Control_Changed;
      optSingleBlackPlay.CheckedChanged += Control_Changed;

      optSingleWhitePrev.CheckedChanged += Control_Changed;
      optSingleWhiteNext.CheckedChanged += Control_Changed;
      optSingleWhitePlay.CheckedChanged += Control_Changed;

      chkNextBlack.CheckedChanged += Control_Changed;
      chkNextWhite.CheckedChanged += Control_Changed;
      chkNoSkipAfterReloc.CheckedChanged += Control_Changed;

      Show();
    }

    private void frmManChordSync_Load(object sender, EventArgs e) {
      Bypass_Event = true;

      clsTT.LoadToolTips(this);

      #if !ADVANCED
        chkNoSkipAfterReloc.Hide();
      #endif

      if (clsPlay.SingleBlackAction == clsPlay.eManSyncAction.Prev) optSingleBlackPrev.Checked = true;
      else if (clsPlay.SingleBlackAction == clsPlay.eManSyncAction.Next) optSingleBlackNext.Checked = true;
      else optSingleBlackPlay.Checked = true;

      if (clsPlay.SingleWhiteAction == clsPlay.eManSyncAction.Prev) optSingleWhitePrev.Checked = true;
      else if (clsPlay.SingleWhiteAction == clsPlay.eManSyncAction.Next) optSingleWhiteNext.Checked = true;
      else optSingleWhitePlay.Checked = true;

      chkNextBlack.Checked = clsPlay.indNextBlack;
      chkNextWhite.Checked = clsPlay.indNextWhite;
      chkNoSkipAfterReloc.Checked = clsPlay.NoSkipAfterReloc;

      Bypass_Event = false;
    }

    private void Control_Changed(object sender, EventArgs e) {
      if (Bypass_Event) return;

      //Disabled = chkDisable.Checked;
      //grpAll.Enabled = !Disabled;

      if (optSingleBlackPrev.Checked) clsPlay.SingleBlackAction = clsPlay.eManSyncAction.Prev;
      else if (optSingleBlackNext.Checked) clsPlay.SingleBlackAction = clsPlay.eManSyncAction.Next;
      else clsPlay.SingleBlackAction = clsPlay.eManSyncAction.Play;

      if (optSingleWhitePrev.Checked) clsPlay.SingleWhiteAction = clsPlay.eManSyncAction.Prev;
      else if (optSingleWhiteNext.Checked) clsPlay.SingleWhiteAction = clsPlay.eManSyncAction.Next;
      else clsPlay.SingleWhiteAction = clsPlay.eManSyncAction.Play;

      clsPlay.indNextBlack = chkNextBlack.Checked;
      clsPlay.indNextWhite = chkNextWhite.Checked;
      clsPlay.NoSkipAfterReloc = chkNoSkipAfterReloc.Checked;

      //clsPlay.SetManChordSyncOpts();
    }

    private void frmManChordSync_FormClosed(object sender, FormClosedEventArgs e) {
      P.frmManChordSync = null;
    }

    private void cmdClose_Click(object sender, EventArgs e) {
      Close();
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Help.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_ManChordSync_Intro.htm");
    }
  }
}
