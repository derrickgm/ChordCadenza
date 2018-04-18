using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using ExtensionMethods;

namespace ChordCadenza {
  internal class clsLyrics {
    internal string[] Text = null;
    private int NumBars { get { return P.F.MaxBBT.Bar; } }
    internal clsMTime.clsBBT[] BarToBBT;  //[bar]
    private clsF.clsindSave _indSave = new clsF.clsindSave();

    internal clsLyrics() {
    }

    internal bool indSave {
      get { return _indSave.Ind; }
      set { _indSave.Ind = value; }
    }

    internal bool LyricsExist { get { return Text != null; } }

    internal bool LoadLyricsFile(string chlfilename) {
      if (!File.Exists(chlfilename)) return false;
      List<string> list = Utils.ReadLines(chlfilename);
      if (list == null) return false;
      list = TrimTextList(list);
      Text = list.ToArray();
      return true;
    }

    internal static List<string> TrimTextList(List<string> inlist) {
      List<string> outlist = inlist.ToList();
      for (int i = 0; i < outlist.Count; i++) outlist[i].Trim();
      for (int i = outlist.Count - 1; i >= 0; i--) {  //remove trailing null lines
        if (outlist[i].Length > 0) break;
        outlist.RemoveAt(i);
      }
      return outlist;
    }

    internal static string[] TrimTextArray(string[] inarray) {
      return TrimTextList(inarray.ToList()).ToArray();
    }

    internal void InitDGV(Forms.IFrmDGV frm) {
      //if (NumBars == 0) NumBars = new clsMTime.clsBBT(P.F.MaxTicks).Bar + 1;
      if (frm == null) return;
      int vmult = 6;
      if (frm == P.frmSC) vmult = 4;  //use less space
      DataGridView dgv = frm.Prop_dgvLyrics;
      dgv.DoubleBuffered(true);
      //clsMTime.clsBBT bbtmax = new clsMTime.clsBBT(P.F.MaxTicks);
      //NumBars = bbtmax.Bar + 1;
      //bbtmax = new clsMTime.clsBBT(NumBars, 0, 0);
      BarToBBT = new clsMTime.clsBBT[NumBars + 1];
      for (int bar = 0; bar <= NumBars; bar++) {  //end at one after numbars
        BarToBBT[bar] = new clsMTime.clsBBT(bar, 0, 0);
      }
      while (dgv.Rows.Count > 0) dgv.Rows.RemoveAt(0);
      dgv.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular);
      //dgv.Width = frm.panNoteMap.Width;
      dgv.Height = ((int)dgv.DefaultCellStyle.Font.GetHeight()) * vmult + 10;  //lines
      dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;  //to avoid error on reinit
      dgv.ColumnCount = NumBars;
      SetColumnWidth(frm);
      //dgv.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
      DataGridViewRow row = new DataGridViewRow();
      row.Height = dgv.Height;
      for (int bar = 0; bar < NumBars; bar++) {
        DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
        row.Cells.Add(cell);
      }
      dgv.Rows.Add(row);  //this will reset P.F.CurrentBBT
      ShowDGVCells(dgv);
    }

    internal void SetColumnWidth(Forms.IFrmDGV frm) {
      if (!LyricsExist) return;
      DataGridView dgv = frm.Prop_dgvLyrics;
      int pixlo = 0, pixhi = 0;
      for (int bar = 0; bar < NumBars; bar++) {
        DataGridViewColumn col = dgv.Columns[bar];
        col.SortMode = DataGridViewColumnSortMode.NotSortable;
        col.Selected = true;
        pixhi = frm.TicksToPix(BarToBBT[bar + 1].Ticks);
        col.Width = pixhi - pixlo;
        pixlo = pixhi;
      }
    }

    internal void ShowDGVCells(DataGridView dgv) {
      for (int bar = 0; bar < NumBars; bar++) {
        clsMTime.clsBBT bbt = new clsMTime.clsBBT(bar, 0, 0);
        DataGridViewCell cell = dgv.Rows[0].Cells[bar];
        cell.Value = GetBarText(bar);
      }
    }

    internal void SelectBar(Forms.IFrmDGV frm, int bar) {  //not called?
      DataGridView dgv = frm.Prop_dgvLyrics;
      if (!dgv.Visible) return;
      DataGridViewColumn col = dgv.Columns[bar];
      col.Selected = true;
    }

    private string GetBarText(int bar) {
      if (Text == null || bar >= Text.Length) return "";
      return Text[bar];
    }

    //internal void CheckSaveFile(FormClosingEventArgs e) {
    //  //string[] text = clsLyrics.TrimTextArray(rtbLines.Lines);
    //  //if (!text.SequenceEqual(P.F.Lyrics.Text)) {
    //  if (indSave) {
    //    DialogResult res = MessageBox.Show("Lyrics have unsaved changes - Save?",
    //      "Unsaved Changes Warning",
    //      MessageBoxButtons.YesNoCancel);
    //    switch (res) {
    //      case DialogResult.No:
    //        //indSave = false;
    //        e.Cancel = false;
    //        break;
    //      case DialogResult.Yes:
    //        if (!SaveFile()) {  //error - msg should be displayed by SaveFile()
    //          e.Cancel = true;
    //          return;
    //        }
    //        e.Cancel = false;
    //        break;
    //      case DialogResult.Cancel:
    //        e.Cancel = true;
    //        return;
    //    }
    //  }
    //}

    internal string SaveFile() {
      try {
        string msg = Utils.SaveFile(P.F.Project.LyricsPath, SaveFileSub);
        if (msg != "") return msg;
      }
      catch (Exception exc) {
        //MessageBox.Show("Error saving Lyrics File: " + exc.Message);
        return exc.Message;
      }

      //UpdateText();
      indSave = false;
      return "";
    }

    private void SaveFileSub(StreamWriter xsw) {
      //string[] array = clsLyrics.TrimTextArray(rtbLines.Lines);
      //foreach (string s in array) xsw.WriteLine(s);
      foreach (string s in Text) xsw.WriteLine(s);
    }


  }
}