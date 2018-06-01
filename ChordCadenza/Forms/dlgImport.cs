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
  public partial class dlgImport : Form {
    public dlgImport() {
      InitializeComponent();
    }

    private bool CheckAudioCopy() {
      //* return true if nocopy or copy ok
      //* return false if copy fail or cancel
      if (txtAudioFilePath.Text.Length > 0) {
        if (!File.Exists(txtAudioFilePath.Text)) {
          MessageBox.Show(txtAudioFilePath.Text + " not found or invalid");
          return false;  //no action
        }
        if (P.F.Project.AudioExt != "" && P.F.Project.AudioExt == Path.GetExtension(txtAudioFilePath.Text)) {
          string msg = P.F.Project.AudioPath + " will be overwritten - continue?";
          if (MessageBox.Show(msg, MessageBoxButtons.YesNo) == DialogResult.No) return false;  //no action
        }
        string outfile = P.F.Project.PathAndName + Path.GetExtension(txtAudioFilePath.Text);
        File.Copy(txtAudioFilePath.Text, outfile, overwrite: true);
      }
      return true;
    }

    private bool CheckMidiCopy() {
      //* return true if nocopy or copy ok
      //* return false if copy fail or cancel
      if (txtMidiFilePath.Text.Length > 0) {
        if (!File.Exists(txtMidiFilePath.Text)) {
          MessageBox.Show(txtMidiFilePath.Text + " not found or invalid");
          return false;  //no action
        }
        if (P.F.Project.MidiExists) {
          string msg = P.F.Project.MidiPath + " will be overwritten - continue?";
          if (MessageBox.Show(msg, MessageBoxButtons.YesNo) == DialogResult.No) return false;  //no action
        }
        string outfile = P.F.Project.MidiPath;
        File.Copy(txtMidiFilePath.Text, outfile, overwrite: true);
      }
      return true;
    }

    private void cmdAudioFile_Click(object sender, EventArgs e) {
      dlgNewProject.GetMidiAudioPath(ofd, txtAudioFilePath, Cfg.AudioFilesPath,
        "AudioFiles|*.mp1;*.mp2;*.mp3;*.wav;*.riff;*.aiff;*.ogg",
        "Audio File");
    }

    private void cmdMidiFile_Click(object sender, EventArgs e) {
      dlgNewProject.GetMidiAudioPath(ofd, txtMidiFilePath, Cfg.MidiFilesPath,
        "MidiFiles|*.mid", "Midi File");
    }

    private void cmdOK_Click(object sender, EventArgs e) {
      if (!CheckAudioCopy()) return;  //no action
      if (!CheckMidiCopy()) return;  //no action
      if (!P.frmStart.LoadProject(P.F.Project.CHPPath, false)) {
        MessageBox.Show("Load Project failed - no valid file found");
        return;  //no action
      }
      DialogResult = DialogResult.OK;
    }
  }
}
