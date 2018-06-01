using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace ChordCadenza {
  internal abstract partial class clsPlay {
    internal abstract class clsSustain {
      protected clsSustain(clsSustain oldsustain) {
        if (oldsustain != null) PedalDown = oldsustain.PedalDown;
      }

      protected bool PedalDown = false;

      internal abstract bool MidiOff(clsPlay.clsKBPitch kbp);
      internal abstract void MidiOn();
      internal abstract bool MidiOnDup(int pitch);
      internal abstract void PlayPedal(bool on);  //invoked during sustain change (e.g. pedal action)

      internal static clsSustain New(clsSustain oldsustain) {
        if (oldsustain == null) PlayPedalStatic(false);  //switch off any old sustain

        if (P.F == null || P.frmSC == null) return new clsSustainNormal(oldsustain);
        if (P.frmSC.optSustainNormal.Checked) return new clsSustainNormal(oldsustain);
        //else if (P.frmSC.optSustainCarryOver.Checked) return new clsSustainCarryOver(oldsustain);
        else if (P.frmSC.optSustainReplay.Checked) return new clsSustainReplay(oldsustain);
        else if (P.frmSC.optSustainSendCtlr.Checked) return new clsSustainSendCtlr(oldsustain);
        else {
          LogicError.Throw(eLogicError.X071);
          return new clsSustainNormal(oldsustain);
        }
      }

      internal static void PlayPedalStatic(bool down) {
        if (clsPlay.PlayExists()) {
          Sustain.PlayPedal(down);
        } else {
          int data = (down) ? 127 : 0;
          if (MidiPlay.OutMRec != null) {
            MidiPlay.OutMRec.SendShortMsg((byte)(0xb0 | MidiPlay.KBOutChanRec), 0x40, data);
          }
          if (MidiPlay.OutMStream != null) {
            for (int ch = 0; ch < 16; ch++) {
              //MidiPlay.OutMStream.SendShortMsg((byte)(0xb0 | ch), 121, 0);  //reset all ctlrs
              MidiPlay.OutMStream.SendShortMsg((byte)(0xb0 | ch), 0x40, data);  //sustain off
            }
          }
        }
      }

      private static clsPlay.clsSustain Sustain {
        get { return clsPlay.Sustain; }
      }

      protected void CheckAllNotesOff() {
        //* switch off any hanging midi pitches (just in case!)
        if (P.frmStart.chkAllNotesOffAfterSustain.Checked
        && P.frmSC.Play != null
        && clsPlay.KBPitchList.Count == 0) {
          MidiPlay.OutMRec.SendShortMsg((byte)(0xb0 | MidiPlay.KBOutChanRec), 0x7b, 0);
        }
      }
    }

    internal class clsSustainNormal : clsSustain {
      internal clsSustainNormal(clsSustain oldsustain) : base(oldsustain) { }

      internal override void PlayPedal(bool on) {
        PedalDown = on;
        int data = (on) ? 127 : 0;
        MidiPlay.OutMRec.SendShortMsg((byte)(0xb0 | MidiPlay.KBOutChanRec), 0x40, data);
        CheckAllNotesOff();
      }

      internal override void MidiOn() { }

      internal override bool MidiOff(clsPlay.clsKBPitch kbp) {
        return false;  //no action
      }

      internal override bool MidiOnDup(int pitch) {  //no action 
        return false;
      }
    }

    internal class clsSustainReplay : clsSustain {
      /*
       * midion && pedaldown: sustainedoff: switchoff sustained notes if no kb down
       * midiondup: noplay
       * midioff: sustain if pedaldown
       * pedal: set pedaldown
       * pedalup: sustainedoff: switchoff sustained notes if no kb down
      */
      internal clsSustainReplay(clsSustain oldsustain) : base(oldsustain) { }

      internal override bool MidiOff(clsPlay.clsKBPitch kbp) {
        if (PedalDown) {  //defer it
          kbp.Sustained = true;  
          return true;  //sustained
        }
        return false;  //unsustained
      }

      internal override void MidiOn() {
        if (PedalDown) SustainedOff();
      }

      internal override bool MidiOnDup(int pitch) {
        return false;  //no action
      }
      
      internal override void PlayPedal(bool on) {
        PedalDown = on;
        if (!on) SustainedOff();
        CheckAllNotesOff();
      }


      private void SustainedOff() {
        lock (clsPlay.KBPitchList) {
          if (clsPlay.KBPitchList.GetSustained().Count == 0) return;
          List<clsPlay.clsKBPitch> kbplist = clsPlay.KBPitchList.GetSustained();
          foreach (clsPlay.clsKBPitch kbp in kbplist) {
            clsPlay.KBPitchList.Remove(kbp);
            if (clsPlay.KBPitchList.GetPitch(false, kbp.Pitch).Count == 0) {
              clsPlay.SendDirect(0x80 | MidiPlay.KBOutChanRec, kbp.Pitch, 0);  //OFF
            }
          }
        }
      }
    }

    internal class clsSustainSendCtlr : clsSustain {
      private static object TimerLock = new object();
      private System.Timers.Timer SustainTimer;

      internal clsSustainSendCtlr(clsSustain oldsustain) : base(oldsustain) {
        SustainTimer = new System.Timers.Timer((int)P.frmStart.nudTimerSustain.Value);
        SustainTimer.AutoReset = false;
        SustainTimer.Elapsed += OnSustainTimer;
      }

      internal override void PlayPedal(bool on) {
        PedalDown = on;
        if (on) clsPlay.SendDirect(0xb0 | MidiPlay.KBOutChanRec, 0x40, 127);  //pedal down
        else SustainedOff();
        CheckAllNotesOff();
      }

      internal override void MidiOn() {
        if (PedalDown) SustainedOff();
      }

      internal override bool MidiOff(clsKBPitch kbp) {  //no action
        return false;
      }

      internal override bool MidiOnDup(int pitch) {
        return false;  //no action
      }
      
      private void OnSustainTimer(Object source, System.Timers.ElapsedEventArgs e) {
        lock (TimerLock) {
          if (PedalDown) {
            clsPlay.SendDirect(0xb0 | MidiPlay.KBOutChanRec, 0x40, 127);  //pedal down
          }
        }
      }

      private void SustainedOff() {
        if (SustainTimer.Enabled) return;
        lock (TimerLock) {
          SustainTimer.Start();
          clsPlay.SendDirect(0xb0 | MidiPlay.KBOutChanRec, 0x40, 0);  //pedal up
        }
      }
    }
  }
}

