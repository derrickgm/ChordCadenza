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
  public partial class frmLyrics : Form {
    private const int TopMargin = 0; //26;
    private const int BottomMargin = 0; //26;
    private const int TopOffset = 0; //52;
    private const int BottomOffset = 0; // 26;
    private readonly int PixelsPerBar;
    private int NumBars { get { return P.F.MaxBBT.Bar; } }
    //private readonly clsMTime.clsBBT BBTMax;
    private Random Rand = new Random();
    //private Stack<clsUndoRedo> UndoStack = new Stack<clsUndoRedo>();
    //private Stack<clsUndoRedo> RedoStack = new Stack<clsUndoRedo>();
    private LLStack<clsUndoRedo> UndoStack;
    private LLStack<clsUndoRedo> RedoStack;
    private int PrevStartPos = -1;
    private int PrevEndPos = -1;
    //private bool UndoRedoCmd = false;  //true if in button event
    //private clsUndoRedo UndoBuffer;
    //private string[] SavedText;
    //private int? panLyrics_AutoScrollPosY;
    private bool ByPassTextChangedEvent = false;
    private int UndoRedoPos = 0;

    //* set if text changed here but not in P.F.Lyrics - reset by UpdateText()
    internal bool indTextChanged = false; 

    public frmLyrics() {
      InitializeComponent();
      toolTip1.Active = P.frmStart.chkTTActive.Checked;
      int capacity = (int)P.frmStart.nudUndoRedoCapacity.Value;
      UndoStack = new LLStack<clsUndoRedo>(capacity);
      RedoStack = new LLStack<clsUndoRedo>(capacity);
      StringBuilder sb = new StringBuilder("1", P.F.MaxBBT.Bar);
      for (int b = 2; b <= NumBars; b++) {
        sb.Append("\r\n" + b.ToString());  //bar 2 onwards
      }
      rtbBars.Text = sb.ToString();

      //* calc pixels per bar
      int pos1 = rtbBars.GetPositionFromCharIndex(0).Y;  //1st line
      int pos2 = rtbBars.GetPositionFromCharIndex(2).Y;  //2nd line
      PixelsPerBar = pos2 - pos1;

      rtbBars.Height = 4 + PixelsPerBar * NumBars;
      rtbLines.Height = rtbBars.Height;
    }

    private void frmLyrics_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      ByPassTextChangedEvent = true;
      try {
        if (P.F.Lyrics.LyricsExist) {
          rtbLines.Lines = P.F.Lyrics.Text;
        } else {  //setup null lines and show lyrics on all forms
          string[] textarr = new string[P.F.MaxBBT.Bar];
          for (int b = 0; b < P.F.MaxBBT.Bar; b++) textarr[b] = "\r\n"; //allows cursor down on textbox

          rtbLines.Lines = textarr;
          //ByPassTextChangedEvent = false;

          P.F.Lyrics.Text = clsLyrics.TrimTextArray(rtbLines.Lines);

          if (P.F.frmChordMap != null) {
            P.F.Lyrics.InitDGV(P.F.frmChordMap);
            P.F.frmChordMap.dgvLyrics.Show();
            P.F.frmChordMap.lblLyricsLit.Show();
            P.F.frmChordMap.VResize();
          }
          if (P.F.frmTrackMap != null) {
            P.F.frmTrackMap.LoadDGVLyrics();
          }
          if (P.frmSC.mnuMap.Checked) {
            if (P.F.Lyrics.LyricsExist) {
              P.frmSC.dgvLyrics.Visible = true;
              P.F.Lyrics.InitDGV(P.frmSC);
            } else {
              P.frmSC.dgvLyrics.Hide();
            }
          }
        }
        rtbLines.Select();
        rtbLines.SelectionStart = 0;
        rtbLines.SelectionLength = 0;
      }
      finally {
        ByPassTextChangedEvent = false;
      }
    }

    private void cmdCancel_Click(object sender, EventArgs e) {
      Close();
    }

    //private bool SaveFile() {
    //  string msg = P.F.Lyrics.SaveFile();
    //  if (msg != "") {
    //    MessageBox.Show("Error saving Lyrics File: " + msg);
    //    return false;
    //  }
    //  ResetUndoRedo();
    //  return true;
    //}

    internal void UpdateText() {
      P.F.Lyrics.Text = clsLyrics.TrimTextArray(rtbLines.Lines);
      UpdateMaps();
      RefreshBBT();
      indTextChanged = false;
      P.F.Lyrics.indSave = true;
    }

    private void RefreshBBT() {
      P.frmSC.Play?.NewReset();
      int bar = rtbLines.GetLineFromCharIndex(rtbLines.SelectionStart);
      Forms.frmStart.RefreshBBT(new clsMTime.clsBBT(bar, 0, 0));
    }

    private static void UpdateMaps() {
      if (P.F.frmTrackMap != null) {
        P.F.Lyrics.ShowDGVCells(P.F.frmTrackMap.dgvLyrics);
        P.F.frmTrackMap.dgvLyrics.Show();
        P.F.frmTrackMap.lblLyricsLit.Show();
      }
      if (P.F.frmChordMap != null) {
        P.F.Lyrics.ShowDGVCells(P.F.frmChordMap.dgvLyrics);
        P.F.frmChordMap.dgvLyrics.Show();
        P.F.frmChordMap.lblLyricsLit.Show();
      }
      if (P.frmSC.mnuMap.Checked) {
        P.F.Lyrics.ShowDGVCells(P.frmSC.dgvLyrics);
        P.frmSC.dgvLyrics.Show();
      }
    }

    private void CheckScroll(int startpos) {
      ////test bug
      //int?[] array = new int?[9];
      //int y = array[0].Value;

      //* AutoScrollPosition - set positive, get negative 
      if (startpos > panLyrics.ClientRectangle.Height - panLyrics.AutoScrollPosition.Y - PixelsPerBar) {
        int newpos = Math.Max(0, startpos - panLyrics.ClientRectangle.Height + PixelsPerBar * 5);
        panLyrics.AutoScrollPosition = new Point(0, newpos);
      } else if (startpos < -panLyrics.AutoScrollPosition.Y + TopMargin) {
        panLyrics.AutoScrollPosition = new Point(0, startpos - PixelsPerBar * 5);
      }
    }

    //private void ZZZ_cmdUndo_Click(object sender, EventArgs e) {
    //  //UndoRedoCmd = true;
    //  if (UndoStack.Count > 0) {
    //    RedoStack.Push(new clsUndoRedo(rtbLines));
    //    clsUndoRedo last = UndoStack.Pop();
    //    rtbLines.Lines = last.Lines;
    //    rtbLines.SelectionStart = last.Pos;
    //    //UndoBuffer = last;
    //    SetStateUndoRedo();
    //  }
    //  UndoRedoCmd = false;
    //  UpdateText();
    //}

    private void cmdUndo_Click(object sender, EventArgs e) {
      if (UndoStack.Count > 0) {
        RedoStack.Push(new clsUndoRedo(rtbLines));  //live
        clsUndoRedo last = UndoStack.Pop();
        rtbLines.Lines = last.Lines;
        rtbLines.SelectionStart = last.Pos;
        UpdateText();
        SetStateUndoRedo();
        indTextChanged = false;
      }
    }

    private void cmdRedo_Click(object sender, EventArgs e) {
      if (RedoStack.Count > 0) {
        UndoStack.Push(new clsUndoRedo(rtbLines));  //live
        clsUndoRedo last = RedoStack.Pop();
        rtbLines.Lines = last.Lines;
        rtbLines.SelectionStart = last.Pos;
        UpdateText();
        SetStateUndoRedo();
        indTextChanged = false;
      }
    }

    //private void ZZZ_cmdRedo_Click(object sender, EventArgs e) {
    //  UndoRedoCmd = true;
    //  if (RedoStack.Count > 0) {
    //    UndoStack.Push(new clsUndoRedo(rtbLines));
    //    clsUndoRedo last = RedoStack.Pop();
    //    rtbLines.Lines = last.Lines;
    //    rtbLines.SelectionStart = last.Pos;
    //    SetStateUndoRedo();
    //  }
    //  UndoRedoCmd = false;
    //  UpdateText();
    //}

    private void SetStateUndoRedo() {
      cmdUndo.Enabled = (UndoStack.Count > 0);
      cmdRedo.Enabled = (RedoStack.Count > 0);
      cmdUndo.Text = "Undo\r\nUpdate\r\n(" + UndoStack.Count + ")";
      cmdRedo.Text = "Redo\r\nUpdate\r\n(" + RedoStack.Count + ")";
    }

    //private void ResetUndoRedo() {
    //  UndoStack.Clear();
    //  RedoStack.Clear();
    //  SetStateUndoRedo();
    //}

    private void frmLyrics_FormClosed(object sender, FormClosedEventArgs e) {
      P.F.frmLyrics = null;
    }

    private void cmdPaste_Click(object sender, EventArgs e) {
      //if (Clipboard.ContainsText()) {
      if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true) {
        rtbLines.Paste();
      }
      //UpdateText();
    }

    private void cmdCut_Click(object sender, EventArgs e) {
      rtbLines.Cut();
    }

    private void cmdCopy_Click(object sender, EventArgs e) {
      rtbLines.Copy();
    }

    private class clsUndoRedo {
      internal string[] Lines;
      internal int Pos;  //selection start

      internal clsUndoRedo(RichTextBox rtb) {
        Lines = rtb.Lines.ToArray();  //clone
        Pos = rtb.SelectionStart;
      }

      internal clsUndoRedo(string[] lines, int pos) {
        Lines = lines;  
        Pos = pos;
      }
    }

    private void rtbLines_TextChanged(object sender, EventArgs e) {
      if (ByPassTextChangedEvent) return;
      //if (UndoRedoCmd) return;  //called from cmdUndo or cmdRedo
      //P.F.Lyrics.indSave = true;
      //UndoBuffer = new clsUndoRedo(rtbLines);
      //SetStateUndoRedo();
      indTextChanged = true;
      //UpdateText();
    }

    private void rtbLines_SelectionChanged(object sender, EventArgs e) {
      //* get start and end vertical positions
      int startpos = rtbLines.GetPositionFromCharIndex(rtbLines.SelectionStart).Y;
      int endpos = rtbLines.GetPositionFromCharIndex(rtbLines.SelectionStart + rtbLines.SelectionLength).Y;

      ////* check undo
      //if (startpos != PrevStartPos) {
      //  if (!UndoRedoCmd && UndoBuffer != null) {
      //    UndoStack.Push(UndoBuffer);
      //    UndoBuffer = null;
      //  }
      //}

      //* check scroll
      if (startpos != PrevStartPos) CheckScroll(startpos);
      else if (endpos != PrevEndPos) CheckScroll(endpos);

      //* set prev variables
      PrevStartPos = startpos;
      PrevEndPos = endpos;
    }

    private void frmLyrics_FormClosing(object sender, FormClosingEventArgs e) {
      if (indTextChanged) {
        DialogResult res = MessageBox.Show("Lyrics text has changed - update Lyrics?", MessageBoxButtons.YesNoCancel);
        if (res == DialogResult.Yes) UpdateText();
        else if (res == DialogResult.Cancel) {
          e.Cancel = true;
          return;
        }
      }
      if (P.F.CloseFormsUnconditional) return;
    }

    //private void UpdateCaret(int bar) {
    //  rtbLines.SelectionLength = 0;
    //  int index = rtbLines.GetFirstCharIndexFromLine(bar);
    //  if (index < 0) {  //OOR (beyond end)
    //    rtbLines.SelectionStart = Math.Max(0, rtbLines.TextLength - 1);
    //  } else {
    //    rtbLines.SelectionStart = rtbLines.GetFirstCharIndexFromLine(bar);
    //  }
    //}

    //private void frmLyrics_Activated(object sender, EventArgs e) {
    //  UpdateCaret(P.F.CurrentBBT.Bar);
    //}

    //private void frmLyrics_Activated(object sender, EventArgs e) {
    //  if (panLyrics_AutoScrollPosY != null) panLyrics.AutoScrollPosition = new Point(0, -panLyrics_AutoScrollPosY.Value);
    //}

    //private void frmLyrics_Deactivate(object sender, EventArgs e) {
    //  panLyrics_AutoScrollPosY = panLyrics.AutoScrollPosition.Y;
    //}

    private void frmLyrics_KeyDown(object sender, KeyEventArgs e) {
      if (e.Control) {
        switch (e.KeyCode) {
          //case Keys.C:
          //  cmdCopy.PerformClick();
          //  break;
          //case Keys.V:
          //  cmdPaste.PerformClick();
          //  break;
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

    private void cmdUpdate_Click(object sender, EventArgs e) {
      UndoStack.Push(new clsUndoRedo(P.F.Lyrics.Text, UndoRedoPos));  //before change
      UndoRedoPos = rtbLines.SelectionStart;  //ready for next update
      UpdateText();  //update P.F.Lyrics etc.
      SetStateUndoRedo();
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Help.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_Lyrics.htm");
    }
  }
}
