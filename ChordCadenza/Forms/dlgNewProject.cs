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
  public partial class dlgNewProject : Form, ITT, IFormStream {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    internal static readonly string[] MidiFilter = new string[] {".mid" };
    internal static readonly string[] AudioFilter = new string[] {
      ".mp1", ".mp2", ".mp3", ".wav", ".riff", ".aiff", ".ogg" };
    internal static readonly string[] ProjectFilter = new string[] {
      ".chp", ".chp0", ".chp1", ".chp2", ".chp3", ".chp4", ".chp5", ".chp6", ".chp7", ".chp8", ".chp9",
      ".mid", ".chl", ".chtc" }; 
    internal static readonly string[] AllFilter;
    private static readonly string MidiFilterMsg = "";
    private static readonly string AudioFilterMsg = "";

    static dlgNewProject() {
      List<string> lst = AudioFilter.ToList();
      lst.AddRange(ProjectFilter);
      AllFilter = lst.ToArray();

      foreach (string f in MidiFilter) MidiFilterMsg += f + ", ";
      foreach (string f in AudioFilter) AudioFilterMsg += f + ", ";
      MidiFilterMsg = MidiFilterMsg.Substring(0, MidiFilterMsg.Length - 2);
      AudioFilterMsg = AudioFilterMsg.Substring(0, AudioFilterMsg.Length - 2);
    }

    public dlgNewProject() {
      InitializeComponent();
    }

    private void frmNewProject_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      clsTT.LoadToolTips(this);
      txtProjectLocation.Text = Cfg.ProjectDir;
    }

    public void FormStreamOnOff(bool on) {
      if (on) Close();
    }

    private void cmdMidiPath_Click(object sender, EventArgs e) {
      GetMidiAudioPath(ofd, txtMidiFilePath, Cfg.MidiFilesPath, "MidiFiles|*.mid", "Midi File");
    }

    private void cmdAudioPath_Click(object sender, EventArgs e) {
      GetMidiAudioPath(ofd, txtAudioFilePath, Cfg.AudioFilesPath,
        "AudioFiles|*.mp1;*.mp2;*.mp3;*.wav;*.riff;*.aiff;*.ogg",
        "Audio File");
    }

    internal static void GetMidiAudioPath(OpenFileDialog ofd, TextBox txtbox, string dfltpath, string filter, string title) {
      string filepath = "";
      if (txtbox.Text.Length > 0) {
        try {
          filepath = Path.GetDirectoryName(txtbox.Text);  //null if root dir
        }
        catch {
          filepath = "";
        }
      }
      if (filepath == "") filepath = dfltpath;
      filepath = Forms.frmStart.GetFileNameFromOFD(ofd, filter, title, filepath);
      if (filepath != "***") txtbox.Text = filepath;
    }

    private void cmdProjectLocation_Click(object sender, EventArgs e) {
      FBDLocation(fbd, txtProjectLocation);
    }

    internal static void FBDLocation(FolderBrowserDialog fbd, TextBox txtbox) {
      fbd.RootFolder = Environment.SpecialFolder.MyComputer;

      if (txtbox.Text != "" && Directory.Exists(txtbox.Text)) {
        fbd.SelectedPath = txtbox.Text;
      } else if (Directory.Exists(Cfg.ProjectDir)) {
        fbd.SelectedPath = Cfg.ProjectDir;  //...\ChordCadenza Projects
      } else {
        fbd.SelectedPath = Cfg.UserMusicPath;  //should always exist
      }

      if (fbd.ShowDialog() == DialogResult.OK) {
        txtbox.Text = fbd.SelectedPath;
      }
    }

    private void txtProjectName_TextChanged(object sender, EventArgs e) {
      cmdOK.Enabled = (txtProjectName.Text.Length > 0 && txtProjectLocation.Text.Length > 0);
    }

    private void cmdUseMidi_Click(object sender, EventArgs e) {
      GetUseMidiAudio(txtMidiFilePath.Text, MidiFilter, MidiFilterMsg);
    }

    private void cmdUseAudio_Click(object sender, EventArgs e) {
      GetUseMidiAudio(txtAudioFilePath.Text, AudioFilter, AudioFilterMsg);
    }

    private void GetUseMidiAudio(string path, string[] filter, string filtermsg) {
      if (path == "") return;
      if (!File.Exists(path)) {
        MessageBox.Show("File " + path + " does not exist"); 
        return;
      }
      if (!filter.Contains(Path.GetExtension(path).ToLower())) {
        MessageBox.Show("File " + path + " does not end with extension: " + filtermsg);
        return;
      }
      txtProjectName.Text = Path.GetFileNameWithoutExtension(path);
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Utils.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_NewProject_Intro.htm");
    }

    private void cmdOK_Click(object sender, EventArgs e) {
      try {
        if (!P.F.CloseCancellableForms()) return;

        //* check if midifile and audiofile exists
        if (!CheckFileToCopy(txtMidiFilePath.Text)) return;
        if (!CheckFileToCopy(txtAudioFilePath.Text)) return;

        //* create project dir
        string fulldir = CreateProjectDir(txtProjectLocation, txtProjectName, chkAddNameToLocation);
        if (fulldir == "") return;

        //* copy files
        string mididest = CopyFile(txtMidiFilePath.Text, fulldir, txtProjectName.Text);
        if (mididest == "***") return;  //copy failed
        string audiodest = CopyFile(txtAudioFilePath.Text, fulldir, txtProjectName.Text);
        if (audiodest == "***") return;  //copy failed

        if (!clsF.NewF(ref P.F)) return;  //should not happen - already checked earlier

        //* error checking complete
        P.F.Project = new clsProject(fulldir, txtProjectName.Text, true);

        //* load midifile
        if (mididest != "") {
          //P.F.Project.MidiExists = true;
          if (!P.frmStart.LoadMidiFile()) return;
          PlayableForms.CmdState_Stopped();
        } else {
          clsF.InitNullMidiFile(clsCF.DefaultSongLength, 192, 4, 4);  
        }

        //* load audiofile 
        if (audiodest != "") {
          P.F.AudioSync = clsAudioSync.New();
          PlayableForms.CmdState_Stopped();
          if (mididest == "") {
            //P.F.ProjectPathAndName = fulldir + "\\" + txtProjectName.Text;
            //P.frmSC.cmbPlayStyle_SelectedIndexChanged(null, null);
          }
        }

      //* set up and save chordfile
      {
        P.F.CF = new clsCFPC(0);
        if (P.frmSC != null) P.frmSC.NewEmpty();
        string msg = P.frmSC.SaveChordFile();
        if (msg != "") {
          MessageBox.Show("Error saving ChordFile: " + msg);
        } else {
          //MessageBox.Show("Null ChordFile (" + P.F.MaxBBT.Bar + " Bars): " + P.F.Project.CHPPath + " created");
          P.frmSC.UpdateRecentProjects();
        }
      }
        DialogResult = DialogResult.OK;
    }
      catch {
        return;
      }
    }

    internal static string CreateProjectDir(
      TextBox txtprojectlocation, TextBox txtprojectname, CheckBox chkaddnametolocation) {
      //* check if location + name is valid
      if (txtprojectname.Text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0) {
        MessageBox.Show("Project Name " + txtprojectname.Text + " is invalid");
        return "";
      }

      string fulldir;
      string dir = txtprojectlocation.Text;
      if (chkaddnametolocation.Checked) dir += "\\" + txtprojectname.Text;
      try {
        fulldir = Path.GetFullPath(dir);  //validate
      }
      catch {
        MessageBox.Show("Project Path " + dir + " is invalid");
        return "";
      }

      //* create project dir if it doesn't already exist
      if (Directory.Exists(fulldir)) {
        string msg = "Project Directory " + fulldir + " already exists";
        msg += "\r\nYou will need to delete it before creating a new project there.";
        MessageBox.Show(msg);
        return "";
      //if (Directory.Exists(fulldir)) {
      //  string[] dirs = Directory.GetFiles(fulldir, txtprojectname.Text + ".*");
      //  foreach (string d in dirs) {
      //    if (AllFilter.Contains(Path.GetExtension(d).ToLower())) {
      //      string msg = "Project Directory " + fulldir + " already exists with at least one project file.";
      //      msg += "\r\nDo you want to overwrite files in this directory?";
      //      DialogResult res = MessageBox.Show(msg, MessageBoxButtons.YesNo);
      //      if (res != DialogResult.Yes) return "";
      //      break;
      //    }
      //  }
      } else {
        try {
          Directory.CreateDirectory(fulldir);  //create parentdirs if necessary (fulldir may already exist)
        }
        catch (Exception exc) {
          MessageBox.Show("New Project folder not created: " + exc.Message);
          return "";
        }
      }
      return fulldir;
    }

    private bool CheckFileToCopy(string txt) {
      //* check if midifile or audio exists
      //* return false if invalid
      if (txt.Length == 0) return true;
      if (!File.Exists(txt)) {
        MessageBox.Show(txt + " not found or invalid");
        return false;
      }
      return true;
    }

    private static string CopyFile(string src, string destprojectdir, string destprojectname) {
      //* copy midi or audio file
      //* return dest, or "***" if failed, or "" if no file copied
      if (src.Length == 0) return "";
      string dest = "***";  //* eg ...\Project1\Project1.mid
      try {
        dest = destprojectdir + "\\" + destprojectname + Path.GetExtension(src);
        File.Copy(src, dest);  //overwrite not allowed
      }
      catch (Exception exc) {
        MessageBox.Show("File " + src + " Copy/Load failed: " + exc.Message);
        return "***";
      }
      return dest;
    }
  }
}
