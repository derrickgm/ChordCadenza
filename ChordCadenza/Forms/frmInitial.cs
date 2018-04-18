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
  public partial class frmInitial : Form, ITT {
    //internal static List<clsTT.sTTcmd> TTcmds = new List<clsTT.sTTcmd>();

    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    public frmInitial() {
      InitializeComponent();
      #if ADVANCED
        cmdCopyHTML.Click += cmdCopyHTML_Click;
      #endif
    }

    private void frmInitial_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      #if !ADVANCED
        cmdCopyHTML.Hide();
        //cmdClearTestForm.Hide();
      #endif
      clsTT.LoadToolTips(this);
    }

    private void cmdClose_Click(object sender, EventArgs e) {
      Close();
    }

    private void cmdCloseFinal_Click(object sender, EventArgs e) {
      Close();
      string path = Cfg.InitialScreenDatFilePath;
      try {
        File.Delete(path);
      }
      catch { }  //ignore any errors
    }

    private void cmdMidiDevs_Click(object sender, EventArgs e) {
      if (P.frmMidiDevs == null || !P.frmMidiDevs.IsHandleCreated) P.frmMidiDevs = new frmMidiDevs();
      Utils.FormAct(P.frmMidiDevs);
    }

    private void cmdKBRanges_Click(object sender, EventArgs e) {
      if (P.frmSCOctaves == null) P.frmSCOctaves = new Forms.frmSCOctaves(P.frmSC);
      Utils.FormAct(P.frmSCOctaves);
      P.frmSC.SetEndBBTRefresh();
      P.F.Panic();
    }

    private void cmdAudioDevs_Click(object sender, EventArgs e) {
      if (P.frmConfigBass == null) P.frmConfigBass = new frmConfigBass();
      Utils.FormAct(P.frmConfigBass);
    }

    private void cmdSaveSettings_Click(object sender, EventArgs e) {
      Forms.frmStart.SaveAllIni();
      //string msg = Cfg.WriteIniFile();
    }

    //internal static void ApplyBoldAll(Form frm) {
    //  foreach (RichTextBox rtf in frm.Controls.OfType<RichTextBox>()) {
    //    ApplyBold(rtf);
    //  }
    //}

    //private static void ApplyBold(RichTextBox rtf) {
    //  Font selectionfont = new Font(rtf.Font, FontStyle.Bold);
    //  List<int> selstart = new List<int>(16);
    //  List<int> selend = new List<int>(16);

    //  for (int i = 0; i < rtf.Text.Length; i++) {
    //    char c = rtf.Text[i];
    //    if (c == '^') selstart.Add(i);
    //    else if (c == '$') selend.Add(i);
    //  }

    //  if (selstart.Count != selend.Count) {
    //    LogicError.Throw(eLogicError.X130);
    //    return;
    //  }

    //  rtf.Text = rtf.Text.Replace('^', ' ');
    //  rtf.Text = rtf.Text.Replace('$', ' ');

    //  for (int i = 0; i < selstart.Count; i++) {
    //    rtf.SelectionStart = selstart[i];
    //    rtf.SelectionLength = selend[i] - selstart[i];
    //    rtf.SelectionFont = selectionfont;
    //  }
    //}

#if ADVANCED
    private void cmdCopyHTML_Click(object sender, EventArgs e) {
      using (new clsWaitCursor()) {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        clsTT.CopyHTMLFiles();
        sw.Stop();
        Debug.WriteLine("Time for CopyHTML = " + sw.ElapsedMilliseconds + " ms");
        CompileHelp();
      }
    }
#endif

    private void CompileHelp() {
      int ExitCode;
      ProcessStartInfo ProcessInfo;
      Process Process;

      //ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + @"C:\C2\Scripts\compilehelp.bat");
      ProcessInfo = new ProcessStartInfo("cmd.exe", "/c" + Cfg.CfgPath + @"\MainHelp\compilehelp.bat");
      ProcessInfo.CreateNoWindow = false;
      ProcessInfo.UseShellExecute = true;

      Process = Process.Start(ProcessInfo);
      Process.WaitForExit();

      ExitCode = Process.ExitCode;
      Process.Close();

      //MessageBox.Show("ExitCode: " + ExitCode.ToString());
    }

    private void cmdIntro_Click(object sender, EventArgs e) {
      Help.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.TableOfContents);
    }

    private void cmdNewProject_Click(object sender, EventArgs e) {
      //if (!P.F.CloseCancellableForms()) return;
      if (P.F != null && !P.F.SaveProject(null)) return;  //check and save

      //* show new project dialog
      dlgNewProject frm = new dlgNewProject();
      DialogResult result = frm.ShowDialog(this);
      if (result == DialogResult.Cancel) return;

      P.frmSC.Refresh();
    }

    //private void cmdTest_Click(object sender, EventArgs e) {
    //  Point scroll = this.AutoScrollPosition;
    //  this.AutoScrollPosition = new Point(0, 200);
    //  Debug.WriteLine("autoscroll: " + this.AutoScrollPosition);
    //}
  }
}
