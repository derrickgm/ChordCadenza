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
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Midi;
using Un4seen.Bass.AddOn.Mix;


namespace ChordCadenza.Forms {
  public partial class frmAutoSync : Form, ITT, IFormStream {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    internal frmAutoSync(clsAudioSync autosync) {
      //P.Forms.Add(this);
      AudioSync = autosync;
      InitializeComponent();
    }

    public void FormStreamOnOff(bool on) {  //IFormStream
      panCmds.Enabled = !on;
      //panPos.Enabled = !on;
      chkStartRecPos.Enabled = !on;
      if (chkStartRecPos.Checked) trkPos.Enabled = !on;
    }

    private enum eCmd {
      SelRemovePlay, SelRemoveRecord, SelCopy,
      Undo, Redo,
      Merge, Copy,
      ResetRecord, ResetAll,
      InsertHole, RemoveCloseHole,
      Interpolate,
      Offset
    }

    private delegate void delAction(eCmd cmd);
    private clsAudioSync AudioSync;

    private clsAudioSync.clsElapsed.clsList Play {
      get { return AudioSync.Elapsed.Play; }
      set { AudioSync.Elapsed.Play = value; }
    }

    private clsAudioSync.clsElapsed.clsList Record {
      get { return AudioSync.Elapsed.Record; }
      set { AudioSync.Elapsed.Record = value; }
    }

    private void frmAutoSync_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      clsTT.LoadToolTips(this);
      ShowList();
      if (P.F != null && AudioSync != null) {
        AudioSync.SetUndoRedoDisplay();
        //if (AutoSync.MP3Player is clsMP3Bass) {
          clsMP3Bass mp3bass = (clsMP3Bass)AudioSync.MP3Player;
          double lensecs = mp3bass.GetDurationSeconds();
          trkPos.Maximum = (int)lensecs;
          lblLenTime.Text = GetMinSecs(lensecs);
          lblLenBytes.Text = mp3bass.GetDurationBytes().ToString("N0");
          //trkVol.Value = mp3bass.Vol;
          //trkPan.Value = mp3bass.Pan;
          //panPos.Enabled = true;
          //grpStartTime.Enabled = false;
        //} else {  //MP3Ext
        //  panPos.Enabled = false;
        //  grpStartTime.Enabled = true;
        //}
        //clsMTime.clsBBT bbt = new clsMTime.clsBBT(P.F.MaxTicks);
        cmdShow_Click(null, null);
      }
      #if !ADVANCED
        mnuEdit.DropDownItems.Remove(mnuPlay);
        mnuSelected.DropDownItems.Remove(mnuInterpolatePlay);
        mnuSelected.DropDownItems.Remove(mnuInterpolateRecord);
      #endif
    }

    internal void ShowList() {
      int topindex = clbEla.TopIndex;

      clbEla.Items.Clear();
      string[] lines;
      string[] titles;  //1 or 2 lines
      if (P.F == null || AudioSync == null) {
        titles = new string[] { "*** NO AUTOSYNC ***" };
        lines = new string[0];
      } else {
        lines = AudioSync.ShowList(out titles);
        txtTitle.Lines = titles;
        clbEla.Items.AddRange(lines);
      }
      clbEla.SelectedIndex = -1;

      clbEla.TopIndex = topindex;
    }

    internal void cmdShow_Click(object sender, EventArgs e) {
      //int topindex = clbEla.TopIndex;
      ShowList();
      //clbEla.TopIndex = topindex;
    }

    private void Merge_Click(object sender, EventArgs e) {
      Action(delegate (eCmd xcmd) {
        AudioSync.MergeElapsed();
      }, eCmd.Merge);
    }

    private void cmdMoveActive_Click(object sender, EventArgs e) {
      Action(delegate (eCmd xcmd) {
        AudioSync.MoveActiveToFileElapsed();
      }, eCmd.Copy);
    }

    private void cmdSave_Click(object sender, EventArgs e) {
      if (clsAudioSync.ActiveStatic) return;
      P.F.AudioSync.SaveFile();
    }

    private void cmdReset_Click(object sender, EventArgs e) {
      if (clsAudioSync.ActiveStatic) return;
      Action(delegate (eCmd xcmd) {
        if (MessageBox.Show("Confirm Reset (Clear Play and Record Tracks)", MessageBoxButtons.YesNo)
          == DialogResult.No) return;
        AudioSync.Reset();
      }, eCmd.ResetAll);
    }

    private void cmdResetActive_Click(object sender, EventArgs e) {
      Action(delegate (eCmd xcmd) {
        AudioSync.ResetActive();
      }, eCmd.ResetRecord);
    }

    private void frmAutoSync_FormClosed(object sender, FormClosedEventArgs e) {
      P.F.frmAutoSync = null;
    }

    private void trkPos_Scroll(object sender, EventArgs e) {
      if (P.F == null || AudioSync == null) return;
      AudioSync.MP3Player.SetPosSeconds(trkPos.Value);
      UpdatePos((double)trkPos.Value);
    }

    internal static string GetMinSecs(double secs) {
      int mins = (int)secs / 60;
      double remsecs = secs - mins * 60;  //eg 12.456789
      string ret = string.Format("{0:D2}:{1:00.00}", mins, remsecs); 
      return ret;
    }

    //private System.Timers.Timer Timer = new System.Timers.Timer(1000);
    internal void StartPlay() {  //called from MP3Bass.StartSync()
      timer1.Start();
      //Timer.AutoReset = true;
      //Timer.Elapsed += PosTimer_Elapsed;
      //Timer.Start();
    }

    internal void StopPlay() {
      timer1.Stop();
    }

    private void timer1_Tick(object sender, EventArgs e) {
      UpdateCurrentPos();
    }


    //private void PosTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
    //  UpdateCurrentPos();
    //}

    internal void UpdateCurrentPos() {
      if (P.F == null || AudioSync == null) return;
      lblPosBytes.Text = AudioSync.MP3Player.GetPosBytes().ToString("N0");
      double time = AudioSync.MP3Player.GetPosSeconds();
      lblPosTime.Text = GetMinSecs(time);
      if (trkPos.Enabled) trkPos.Value = (int)time;
    }

    internal void UpdatePos(double secs) {
      if (P.F == null || AudioSync == null) return;
      if (!lblPosBytes.Enabled || !lblPosTime.Enabled) return;
      long bytes = AudioSync.MP3Player.GetSeconds2Units(secs);
      lblPosTime.Text = GetMinSecs(secs);
      lblPosBytes.Text = bytes.ToString("N0");
      trkPos.Value = (int)secs;
    }

    internal void UpdatePos(long bytes) {
      if (P.F == null || AudioSync == null) return;
      if (!lblPosBytes.Enabled || !lblPosTime.Enabled) return;
      double secs = AudioSync.MP3Player.GetUnits2Seconds(bytes);
      if (secs >= 0) {
        lblPosTime.Text = GetMinSecs(secs);
        lblPosBytes.Text = bytes.ToString("N0");
        trkPos.Value = (int)secs;
      }
    }

    //private void trkVol_Scroll(object sender, EventArgs e) {
    //  if (P.F == null || AutoSync == null) return;
    //  if (!trkVol.Enabled) return;
    //  AutoSync.MP3Player.Vol = trkVol.Value;
    //}

    //private void trkPan_Scroll(object sender, EventArgs e) {
    //  if (P.F == null || AutoSync == null) return;
    //  if (!trkPan.Enabled) return;
    //  AutoSync.MP3Player.Pan = trkPan.Value;
    //}

    private void cmdCopySelected_Click(object sender, EventArgs e) {
      if (P.F == null || AudioSync == null) return;
      if (clsAudioSync.StaticMP3Playing) return;
      //int topindex = clbEla.TopIndex;
      clsAudioSync.clsElapsed elapsed = AudioSync.Elapsed.Copy();
      foreach (int beat in clbEla.SelectedIndices) {
        if (beat >= elapsed.Record.Count) continue;
        elapsed.Play[beat] = Math.Max(0, elapsed.Record[beat]);
      }
      int b = elapsed.ValidatePlay(prompt: false);
      if (b >= 0) {
        MessageBox.Show("Copy failed - invalid output at beat " + (b + 1));
      } else {
        AudioSync.UpdateUndo();
        AudioSync.Elapsed = elapsed;
        //AutoSync.indSave = true;
      }
      ShowList();
      //clbEla.TopIndex = topindex;
      PlayableForms.CmdState_Stopped();  //possible Play.IsEmpty changed
    }

    private void Action(delAction action, eCmd cmd) {  //cmd note used?
      //if (P.F == null || AutoSync == null) return;
      if (clsAudioSync.StaticMP3Playing) return;
      //int topindex = clbEla.TopIndex;
      if (cmd != eCmd.Undo && cmd != eCmd.Redo && cmd != eCmd.Merge) {
        AudioSync.UpdateUndo();
      }
      action(cmd);
      //AutoSync.indSave = true;
      ShowList();
      //clbEla.TopIndex = topindex;
      PlayableForms.CmdState_Stopped();  //possible Play.IsEmpty changed
      cmdSave.Enabled = (P.F.AudioSync != null && !P.F.AudioSync.IsEmpty());
      //P.frmSC.mnuSaveAutoSyncFile.Enabled = cmdSave.Enabled;
      //* update Elapsed.Play display in bars panes
      if (P.F?.frmTrackMap != null) P.F.frmTrackMap.picBars.Refresh();
      if (P.F?.frmChordMap != null) P.F.frmChordMap.picBars.Refresh();
      clsAudioSync.SetPlayAudioText(P.F?.AudioSync);
    }

    private void mnuSave_Click(object sender, EventArgs e) {
      if (clsAudioSync.ActiveStatic) return;
      P.F.AudioSync.SaveFile();
    }

    private void mnuUndo_Click(object sender, EventArgs e) {
      Action(delegate (eCmd xcmd) {
        AudioSync.Undo();
      }, eCmd.Undo);
    }

    private void mnuRedo_Click(object sender, EventArgs e) {
      Action(delegate (eCmd  xcmd) {
        AudioSync.Redo();
      }, eCmd.Redo);
    }

    private void mnuSelRemoveRecord_Click(object sender, EventArgs e) {
      Action(delegate (eCmd xcmd) {
        foreach (int beat in clbEla.SelectedIndices) {
          if (beat >= Record.Count) continue;
          Record[beat] = 0;
        }
      }, eCmd.SelRemoveRecord);
    }

    private void mnuSelRemovePlay_Click(object sender, EventArgs e) {
      Action(delegate (eCmd xcmd) {
        foreach (int beat in clbEla.SelectedIndices) {
          if (beat >= Play.Count) continue;
          //Debug.WriteLine("remove beat(0): " + beat);
          Play[beat] = 0;
        }
      }, eCmd.SelRemovePlay);
    }

    private void mnuSelCopy_Click(object sender, EventArgs e) {
      //if (P.F == null || AutoSync == null) return;
      if (clsAudioSync.StaticMP3Playing) return;
      if (clbEla.SelectedIndices.Count == 0) return;
      //int topindex = clbEla.TopIndex;
      clsAudioSync.clsElapsed elapsed = AudioSync.Elapsed.Copy();
      foreach (int beat in clbEla.SelectedIndices) {
        if (beat >= elapsed.Record.Count) continue;
        elapsed.Play[beat] = Math.Max(0, elapsed.Record[beat]);
      }
      int b = elapsed.ValidatePlay(prompt: false);
      if (b >= 0) {
        MessageBox.Show("Copy failed - invalid output at beat " + (b + 1));
      } else {
        AudioSync.UpdateUndo();
        AudioSync.Elapsed = elapsed;
        //AutoSync.indSave = true;
      }
      ShowList();
      //clbEla.TopIndex = topindex;
      PlayableForms.CmdState_Stopped();  //possible Play.IsEmpty changed
      clsAudioSync.SetPlayAudioText(P.F?.AudioSync);
    }

    private void mnuInsertHole_Click(object sender, EventArgs e) {
      if (clbEla.SelectedIndices.Count == 0) return;
      Action(delegate (eCmd xcmd) {
        int ilo = int.MaxValue;  
        foreach (int i in clbEla.SelectedIndices) ilo = Math.Min(ilo, i);
        int ihi = ilo + clbEla.SelectedIndices.Count - 1;
        for (int i = Play.Count - 1; i >= ilo; i--) {
          AudioSync.Elapsed.SetPlay(i + clbEla.SelectedIndices.Count, Play[i]);
          if (i <= ihi) Play[i] = 0;
        }
      }, eCmd.InsertHole);
    }

    private void mnuRemoveCloseHoleNew_Click(object sender, EventArgs e) {
      if (clbEla.SelectedIndices.Count == 0) return;
      Action(delegate (eCmd xcmd) {
        int ilo = int.MaxValue;
        foreach (int i in clbEla.SelectedIndices) ilo = Math.Min(ilo, i);
        int ihi = ilo + clbEla.SelectedIndices.Count - 1;
        for (int i = ilo; i < Play.Count - clbEla.SelectedIndices.Count; i++) {
          AudioSync.Elapsed.Play[i] = Play[i + clbEla.SelectedIndices.Count];
        }
        for (int i = Play.Count - clbEla.SelectedIndices.Count; i < Play.Count; i++) {
          Play[i] = 0;
        }
      }, eCmd.InsertHole);
    }

    private void cmdUndo_Click(object sender, EventArgs e) {
      Action(delegate (eCmd xcmd) {
        AudioSync.Undo();
      }, eCmd.Undo);
    }

    private void cmdRedo_Click(object sender, EventArgs e) {
      Action(delegate (eCmd xcmd) {
        AudioSync.Redo();
      }, eCmd.Redo);
    }

    private void cmdClose_Click(object sender, EventArgs e) {
      Close();
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Help.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_AudioSync_Intro.htm");
    }

    private void frmAutoSync_FormClosing(object sender, FormClosingEventArgs e) {
      if (P.F.CloseFormsUnconditional) return; 
      
      ////* check if Elapsed.Record copied to Elapsed.Play 
      //if (!AutoSync.Elapsed.RecordIsEmpty) {
      //  string msg = "Audio Record Track not moved/merged/cleared - clear it and continue?";
      //  if (MessageBox.Show(this, msg, "Audio Data Warning", MessageBoxButtons.YesNo) == DialogResult.No) {
      //    e.Cancel = true;
      //    return;
      //  }
      //  AutoSync.Elapsed.ResetRecord();
      //}
    }

    private void frmAutoSync_Activated(object sender, EventArgs e) {
      cmdShow_Click(null, null);
    }

    //private void nudMinsSecs_ValueChanged(object sender, EventArgs e) {
    //  //if (!(AutoSync.MP3Player is clsMP3Ext)) return;
    //  clsMP3Ext mp3ext = (clsMP3Ext)AutoSync.MP3Player;
    //  mp3ext.StartMSecs = ((int)nudMins.Value * 60 + (int)nudSecs.Value) * 1000;
    //  if (mp3ext.StartMSecs == 0) mp3ext.StartMSecs = 1000;   //0 will probably cause problems
    //  mp3ext.RecalcTimes();
    //}

    private void mnuInterpolatePlay_Click(object sender, EventArgs e) {
      List<int> indices = Interpolate(Play);
      //* restore selection after MessageBox.Show()
      foreach (int index in indices) clbEla.SelectedIndex = index;
    }

    private void mnuInterpolateRecord_Click(object sender, EventArgs e) {
      List<int> indices = Interpolate(Record);
      //* restore selection after MessageBox.Show()
      foreach (int index in indices) clbEla.SelectedIndex = index;
    }

    private List<int> Interpolate(clsAudioSync.clsElapsed.clsList list) {
      List<int> indices = new List<int>();
      foreach (int index in clbEla.SelectedIndices) indices.Add(index);
      indices.Sort();
      if (clbEla.SelectedIndices.Count < 3) {
        MessageBox.Show("Selected Index Count is too low");
        return indices;
      }
      for (int i = 1; i < indices.Count; i++) {
        if (indices[i] != indices[i - 1] + 1) {
          MessageBox.Show("Selection is not contiguous");
          return indices;
        } 
      }
      int min = indices[0];
      int max = indices[indices.Count - 1];
      if (list.Count <= max) {
        MessageBox.Show("Selection invalid");
        return indices;
      }

      if (max < list.Count - 1) max++;  //need to look at beat after last to calc diff 
      long minela = list[min];
      long maxela = list[max];
      if (minela <= 0 || maxela <= 0) {
        MessageBox.Show("Selection Boundary is zero or negative");
        return indices;
      }
      float diff = (float)(maxela - minela) / (max - min);

      Action(delegate (eCmd xcmd) {
        float running = list[min];
        for (int i = min + 1; i < max; i++) {
          running += diff;
          list[i] = (long)running;
        } 
      }, eCmd.Interpolate);
      return indices;
    }

    private void frmAutoSync_KeyDown(object sender, KeyEventArgs e) {
      if (e.Control) {
        switch (e.KeyCode) {
          case Keys.Y:
            if (!cmdRedo.Enabled) return;
            cmdRedo_Click(null, null);
            break;
          case Keys.Z:
            if (!cmdUndo.Enabled) return;
            cmdUndo_Click(null, null);
            break;
        }
      }
    }

    private void cmdOffsetTimes_Click(object sender, EventArgs e) {
      dlgNud frm = new dlgNud();
      frm.lblPrompt.Text = "Enter offset in milliseconds to apply to the Play and Record Tracks";
      frm.lblMsg.Text = "A positive offset will display beat positions later.";
      frm.lblMsg.Text = "Any events that are moved before the start or after the end of the song will be deleted.";
      frm.lblMsg.Text += "\r\n(Removed events can be recovered with the Undo button.)";
      frm.lblMsg.Text += "\r\nThe range of valid values is -2000 to 2000";
      frm.nud1.Minimum = -2000;
      frm.nud1.Maximum = 2000;
      frm.nud1.Increment = 10;
      frm.nud1.Value = 0;
      DialogResult res = frm.ShowDialog();  
      if (res == DialogResult.Cancel) return;
      if (frm.nud1.Value == 0) return;

      Action(delegate (eCmd xcmd) {
        AudioSync.Elapsed.OffsetTimes((int)frm.nud1.Value);
      }, eCmd.Offset);
    }

    private void cmdOffsetBeats_Click(object sender, EventArgs e) {
      dlgNud frm = new dlgNud();
      frm.lblPrompt.Text = "Enter number of beats to offset the Play and Record Tracks by";
      frm.lblMsg.Text = "Any events that are moved before the start or after the end of the song will be deleted.";
      frm.lblMsg.Text += "\r\n(Removed events can be recovered with the Undo button.)";
      frm.lblMsg.Text += "\r\nThe range of valid values is -100 to 100";
      frm.nud1.Minimum = -100;
      frm.nud1.Maximum = 100;
      frm.nud1.Value = 0;
      DialogResult res = frm.ShowDialog();
      if (res == DialogResult.Cancel) return;
      if (frm.nud1.Value == 0) return;

      Action(delegate (eCmd xcmd) {
        AudioSync.Elapsed.OffsetBeats((int)frm.nud1.Value);
      }, eCmd.Offset);
    }
  }
}
