using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace ChordCadenza.Forms {
  public partial class dlgSaveProjectAs : Form, ITT, IFormStream {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    public dlgSaveProjectAs() {
      InitializeComponent();
    }

    private void frmSaveAs_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      clsTT.LoadToolTips(this);
      txtProjectLocation.Text = P.F.Project.ProjectPathXName;
      txtProjectName.Text = P.F.Project.Name;
      chkAddNameToLocation.Checked = P.F.Project.NameAsSubdir;
    }

    public void FormStreamOnOff(bool on) {
      if (on) Close();
    }

    private void cmdProjectLocation_Click(object sender, EventArgs e) {
      dlgNewProject.FBDLocation(fbd, txtProjectLocation);
    }

    private void cmdOK_Click(object sender, EventArgs e) {
      if (txtProjectLocation.Text == P.F.Project.ProjectPathXName && txtProjectName.Text == P.F.Project.Name) {
        P.F.SaveProject(P.F.Project);
        DialogResult = DialogResult.OK;  //close form
        return;
      }

      //* create project dir
      string fulldir = dlgNewProject.CreateProjectDir(txtProjectLocation, txtProjectName, chkAddNameToLocation);
      if (fulldir == "") return;  //do not close form

      clsProject oldproject = P.F.Project;
      P.F.Project = new clsProject(P.F.Project, fulldir, txtProjectName.Text);
      bool ok = P.F.SaveProject(oldproject);
      //if (!ok) MessageBox.Show("Error saving project");

      DialogResult = DialogResult.OK;  //close form
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Utils.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_SaveAs_Intro.htm");
    }
  }
}
