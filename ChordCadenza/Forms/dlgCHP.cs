using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ChordCadenza.Forms {
  public partial class dlgCHP : Form, IFormStream {
    //private string FileName;
    private string[] CHPPaths;
    internal string[] CHPDescs;
    private string[] CHPExts;
    internal string OK_Ext = "";
    private bool indLoad;  //else save as

    public dlgCHP(string[] chppaths, bool indload) {
      InitializeComponent();
      //FileName = nonchppath;  //not .chp file
      indLoad = indload;
      CHPPaths = chppaths;
      Array.Sort(CHPPaths);
      if (indload) {
        Text = "Load Project " + Path.GetFileNameWithoutExtension(chppaths[0] + " - Chord Cadenza");
        cmdLoad.Text = "Load Project";
      } else {  //save as
        Text = "Save ChordFile As " + P.F.Project.CHPPath + " - Chord Cadenza";
        cmdLoad.Text = "Save File";
      }  
      GetDescs();
    }

    public void FormStreamOnOff(bool on) {
      if (on) Close();
    }

    private void GetDescs() {
      CHPDescs = new string[CHPPaths.Length];
      CHPExts = new string[CHPPaths.Length];
      for (int k = 0; k < CHPPaths.Length; k++) {
        CHPExts[k] = Path.GetExtension(CHPPaths[k]);
      }
      int index = 0;
      try {
        for (; index < CHPPaths.Length; index++) {
          if (File.Exists(CHPPaths[index])) {
            using (StreamReader str = new StreamReader(CHPPaths[index])) {
              string line;
              while ((line = str.ReadLine()) != null) {
                if (!line.StartsWith("*")) break;  //old starting comments
              }
              string[] f = line.Split(new char[] { ' ' }, 5, StringSplitOptions.RemoveEmptyEntries);
              CHPDescs[index] = CHPExts[index];
              if (f.Length == 5) SetDesc(index, f[4]); else SetDesc(index, "*** No Description ***");
            }
          } else {  //file not present
            if (indLoad) {
              LogicError.Throw(eLogicError.X139);  //leave blank and continue
            }
            SetDesc(index, "*** File not present ***");
          }
        }
      }
      catch (Exception exc) {
        string msg = "Error reading ChordFile " + CHPPaths[index] + ": " + exc.Message;
        MessageBox.Show(msg);
        return;
      }
    }

    private void SetDesc(int index, string desc) {
      CHPDescs[index] = string.Format("{0,-6}: {1}", CHPExts[index], desc);
    }

    private void frmCHP_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      ToolTip tt;
      clsTT.LoadToolTips(this, out tt);
      if (!indLoad) {  //SaveCHP As
        string txt = "Shows a list of all ChordFile filename extensions (.chpN), where N = 0 - 9";
        txt += "\r\nSelected name will be used as the saved filename.";
        txt += "\r\nClick 'Save ChordFile As' Button, or doubleclick the selected filename, to save the ChordFile with the selected filename extension."; 
        tt.SetToolTip(clbCHPList, txt);
        txt = "Click to save the ChordFile with the selected filename extension.";
        tt.SetToolTip(cmdLoad, txt);
      }

      Init();
    }

    private void Init() {
      clbCHPList.Items.Clear();
      clbCHPList.Items.AddRange(CHPDescs);
      clbCHPList.SelectedIndex = 0;
      cmdDelete.Enabled = (CHPDescs.Length > 1);
    }

    private void cmdLoad_Click(object sender, EventArgs e) {
      OK_Ext = CHPExts[clbCHPList.SelectedIndex];
      if (!indLoad) {
        string path = CHPPaths[clbCHPList.SelectedIndex];
        if (File.Exists(path)) {
          string msg = "File " + path + " will be overwritten - click 'OK' to continue";
          if (MessageBox.Show(msg, MessageBoxButtons.OKCancel) == DialogResult.Cancel) {
            return;
          }
        }
      }
      DialogResult = DialogResult.OK;
    }

    private void cmdDelete_Click(object sender, EventArgs e) {
      int index = clbCHPList.SelectedIndex;
      string file = CHPPaths[index];
      if (!File.Exists(file)) {
        MessageBox.Show("File " + file + " not deleted: " + "File not present");
        return;
      }
      string msg = "Confirm deletion of file: " + file;
      if (MessageBox.Show(msg, MessageBoxButtons.YesNo) != DialogResult.Yes) return;
      //if (file == P.F.Project.CHPPath) {
      //  MessageBox.Show("File " + file + " not deleted: " + "File currently in use");
      //  return;
      //}
      try {
        File.Delete(file);
      }
      catch (Exception exc) {
        MessageBox.Show("File " + file + " not deleted: " + exc.Message);
        return;
      }
      if (indLoad) {
        CHPDescs = Utils.RemoveAt(CHPDescs, index);
        CHPPaths = Utils.RemoveAt(CHPPaths, index);
        CHPExts = Utils.RemoveAt(CHPExts, index);
      } else {
        SetDesc(index, "*** File not present ***"); 
      }
      Init();
    }

    private void clbCHPList_DoubleClick(object sender, EventArgs e) {
      cmdLoad_Click(null, null);
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Utils.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_LoadProject_Intro.htm");
    }
  }
}
