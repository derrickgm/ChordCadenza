using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace ChordCadenza {
  internal abstract partial class clsCF {
    internal class clsUndoRedoCF {
      private LLStack<clsItem> UndoStack;
      private LLStack<clsItem> RedoStack;
      private clsItem Item;
      private Forms.frmChordMap FrmChordMap;

      internal clsUndoRedoCF(Forms.frmChordMap frmchordmap) {
        FrmChordMap = frmchordmap;
        int capacity = (int)P.frmStart.nudUndoRedoCapacity.Value;
        UndoStack = new LLStack<clsItem>(capacity);
        RedoStack = new LLStack<clsItem>(capacity);
        CopyLive();
      }

      internal void CopyLive() {  //called by Update() and constructor
        Item = new clsItem();
      }

      private void MakeLive(clsMTime.clsBBT bbt) {  //called by Undo/Redo
        //* return true if tsigs changed
        //Debug.WriteLine("Undo/Redo: AutoScrollPosition = " + FrmChordMap.panNoteMap.AutoScrollPosition);
        //FrmChordMap.CsrPixHi = FrmChordMap.CsrPixLo;  //selected area length zero
        int SelectedAreaTicksLo = FrmChordMap.PixToTicks(FrmChordMap.CsrPixLo);
        int SelectedAreaTicksHi = FrmChordMap.PixToTicks(FrmChordMap.CsrPixHi);
        using (new clsChordMapDis(FrmChordMap)) {  
          P.F.CF.Evs = new List<clsEv>();
          foreach (clsEv ev in Item.Evs) {
            P.F.CF.Evs.Add(ev.CopyEv());
          }
          P.F.SetKeys(new clsKeysTicks(Item.Keys));

          bool newmtime = false;
          if (!Item.TSigsEquivLive()) {
            P.F.MTime.TSigs = new clsMTime.clsTSigBB[Item.TSigs.Length];
            for (int i = 0; i < Item.TSigs.Length; i++) {
              P.F.MTime.TSigs[i] = Item.TSigs[i];
            }
            newmtime = true;
          }

          if (Item.SongLength != P.F.MaxBBT.MidiWholeNotes) {
            P.F.MaxBBT = new clsMaxBBT(Item.SongLength * P.F.MTime.TicksPerQNote * 4);
            P.F.CF.NoteMap.ReInitMapAndAtts();
            newmtime = true;
          }

          //FrmChordMap.Bypass_Event = true;
          //FrmChordMap.nudTransposeChordNotes.Value = Item.ChordNotesTrans;
          //FrmChordMap.Bypass_Event = false;

          if (newmtime) FrmChordMap.NewMTime();
          FrmChordMap.BeatChords.ShowDGVCells();
          FrmChordMap.SetNoteMapFileChanged(undoredo: false, indqi: false); //ReInitPlayMode -> new PlayChord 

          //FrmChordMap.CsrPixHi = FrmChordMap.GetPixNextBar(FrmChordMap.CsrPixLo);

          clsMTime.clsBBT bbtlo = new clsMTime.clsBBT(SelectedAreaTicksLo);
          bbtlo.RoundDownToBar();
          FrmChordMap.CsrPixLo = FrmChordMap.TicksToPix(bbtlo.Ticks);

          clsMTime.clsBBT bbthi = new clsMTime.clsBBT(SelectedAreaTicksHi);
          bbthi.RoundUpToBar();
          FrmChordMap.CsrPixHi = FrmChordMap.TicksToPix(bbthi.Ticks);

          SetCmdStateUndoRedo();
        }
          //FrmChordMap.Bypass_DGV = false;
          //FrmChordMap.NoScroll = false;
          //Debug.WriteLine("Undo/Redo: AutoScrollPosition = " + FrmChordMap.panNoteMap.AutoScrollPosition);

          //FrmChordMap.panNoteMap.Show();
          //FrmChordMap.picBars.Show();
          //FrmChordMap.DGV.Show();
          //FrmChordMap.dgvLyrics.Show();
          //FrmChordMap.picMargins.Show();
        //FrmChordMap.panNoteMap.AutoScrollPosition = new Point(-autoscrollx, 0);
        //FrmChordMap.Refresh();
        //FrmChordMap.RefreshDGV();
        //FrmChordMap.DGV.Refresh();
        //ScrollEventArgs e = new ScrollEventArgs(ScrollEventType.SmallIncrement, FrmChordMap.dgvLyrics.HorizontalScrollingOffset);
        //FrmChordMap.panNoteMap_Scroll(null, e); 
        //int autoscrollx = FrmChordMap.panNoteMap.AutoScrollPosition.X;
        //FrmChordMap.panNoteMap.AutoScrollPosition = new Point(-autoscrollx, FrmChordMap.panNoteMap.AutoScrollPosition.Y);
      }

      internal void Update() {  //called from SetNoteMapFileChanged()
        //* called after P.F.CF.Evs changed
        if (Item == null) {  //should be set before first change
          LogicError.Throw(eLogicError.X149);
          return;
        }
        if (Item.IsEquivLive()) return;
        Item.CurrentBBTAfter = P.F.CurrentBBT.Copy();
        UndoStack.Push(Item);  //state before change
        CopyLive();  //state after change
        SetCmdStateUndoRedo();
        Debug.WriteLine("clsUndoRedoEvs: Update ended");
      }

      internal void Undo() {
        RedoStack.Push(Item);  //state before change
        Item = UndoStack.Pop();
        MakeLive(Item.CurrentBBTAfter);
        //P.F.CurrentBBT = Item.CurrentBBTAfter;
        //FinalizeUndoRedo();
      }

      internal void Redo() {
        UndoStack.Push(Item);
        Item = RedoStack.Pop();
        MakeLive(Item.CurrentBBTBefore);
        //P.F.CurrentBBT = Item.CurrentBBTBefore;
        //FinalizeUndoRedo();
      }

      internal void SetCmdStateUndoRedo() {
        if (FrmChordMap != null) {
          FrmChordMap.cmdUndo.Enabled = (UndoStack.Count > 0);
          FrmChordMap.cmdRedo.Enabled = (RedoStack.Count > 0);
          FrmChordMap.cmdUndo.Text = (UndoStack.Count > 0) ? 
            "Undo\r\n(" + UndoStack.Count + ")" : "Undo";
          FrmChordMap.cmdRedo.Text = (RedoStack.Count > 0) ? 
            "Redo\r\n(" + RedoStack.Count + ")" : "Redo";
        }
      }

      private class clsItem {
        internal List<clsEv> Evs;
        internal clsMTime.clsBBT CurrentBBTBefore;
        internal clsMTime.clsBBT CurrentBBTAfter;
        internal clsKeysTicks Keys = null;
        internal clsMTime.clsTSigBB[] TSigs = null;
        internal int SongLength;  //wholenotes
        //internal int ChordNotesTrans = 0;

        internal clsItem() {
          //* copy evs and bbt (to isolate from live)  
          Evs = new List<clsEv>();
          foreach (clsEv ev in P.F.CF.Evs) Evs.Add(ev.CopyEv());

          TSigs = new clsMTime.clsTSigBB[P.F.MTime.TSigs.Length];
          for (int i = 0; i < P.F.MTime.TSigs.Length; i++) {
            TSigs[i] = P.F.MTime.TSigs[i].Copy();
          }

          Keys = new clsKeysTicks(P.F.Keys);

          SongLength = P.F.MaxBBT.MidiWholeNotes;

          //if (P.F.frmChordMap != null) ChordNotesTrans = P.F.frmChordMap.ChordTransposeNotesVal;

          CurrentBBTBefore = P.F.CurrentBBT.Copy();
          CurrentBBTAfter = P.F.CurrentBBT.Copy();
        }

        internal bool IsEquivLive() {
          //* return true if Evs/TSigs/Keys same as live
          if (!KeysEquivLive()) return false;
          if (!TSigsEquivLive()) return false;
          if (!EvsEquivLive()) return false;
          if (SongLength != P.F.MaxBBT.MidiWholeNotes) return false;
          return true;
        }

        internal bool KeysEquivLive() {
          if (Keys.Keys != null && Keys.Keys.Count != P.F.Keys.Keys.Count) return false;
          for (int i = 0; i < Keys.Keys.Count; i++) {
            if (!Keys.Keys[i].IsEquiv(P.F.Keys.Keys[i])) return false;
          }
          return true;
        }

        internal bool TSigsEquivLive() {
          if (TSigs != null && TSigs.Length != P.F.MTime.TSigs.Length) return false;
          for (int i = 0; i < TSigs.Length; i++) {
            if (!TSigs[i].IsEquiv(P.F.MTime.TSigs[i])) return false;
          }
          return true;
        }

        internal bool EvsEquivLive() {
          if (Evs.Count != P.F.CF.Evs.Count) return false;
          for (int i = 0; i < Evs.Count; i++) {
            if (!Evs[i].Equiv(P.F.CF.Evs[i], false)) return false;
          }
          return true;
        }
      }
    }
  }
}