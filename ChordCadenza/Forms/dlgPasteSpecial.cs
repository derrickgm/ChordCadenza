using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChordCadenza.Forms {
  public partial class dlgPasteSpecial : Form {
    //private static bool static_ReplaceRetain;
    //private static bool static_ReplaceNullify;
    //private static bool static_RemoveInsert;

    public dlgPasteSpecial(int qilo, int qihi) {
      InitializeComponent();
      //static_ReplaceRetain = optReplaceRetain.Checked;
      //static_ReplaceNullify = optReplaceNullify.Checked;
      //static_RemoveInsert = optRemoveInsert.Checked;
      clsMTime.clsBBT bbtlo = new clsMTime.clsBBT(qilo * P.F.TicksPerQI);
      clsMTime.clsBBT bbthi = new clsMTime.clsBBT(qihi * P.F.TicksPerQI);
      string txtlo = (bbtlo.TicksRemBar == 0) ? (bbtlo.Bar + 1).ToString() : "*"; 
      string txthi = (bbthi.TicksRemBar == 0) ? bbthi.Bar.ToString() : "*"; 
      lblCopy_Buff.Text = "Copy Buffer Bars: " + txtlo + " - " + txthi;
    }

    //private void optRemoveInsert_VisibleChanged(object sender, EventArgs e) {
    //  if (Visible) {
    //    optReplaceNullify.Checked = static_ReplaceNullify;
    //    optReplaceRetain.Checked = static_ReplaceRetain;
    //    optRemoveInsert.Checked = static_RemoveInsert;
    //  } else {
    //    static_ReplaceRetain = optReplaceRetain.Checked;
    //    static_ReplaceNullify = optReplaceNullify.Checked;
    //    static_RemoveInsert = optRemoveInsert.Checked;
    //  }
    //}
  }
}
