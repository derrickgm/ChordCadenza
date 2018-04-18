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
        //Play = play;
        if (oldsustain != null) PedalDown = oldsustain.PedalDown; 
      }

      //protected clsPlay Play;
      protected bool Delay = false;
      protected bool PedalDown = false;

      internal virtual bool MidiOff(clsPlay.clsKBPitch kbp) { return false; }  //no action
      internal virtual void MidiOn() { }  
  
      internal virtual bool MidiOnDup(int pitch) { return false; }  //no action  
      internal abstract void PlayPedal(bool on);  //invoked during sustain change (e.g. pedal action)

      internal static clsSustain New(clsSustain oldsustain) {
        if (oldsustain == null) PlayPedalStatic(false);  //switch off any old sustain

        if (P.F == null || P.frmSC == null) return new clsSustainNormal(oldsustain); 
        if (P.frmSC.optSustainNormal.Checked) return new clsSustainNormal(oldsustain);
        else if (P.frmSC.optSustainCarryOver.Checked) return new clsSustainCarryOver(oldsustain);
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

      internal static void PlaySustainOnOffStatic() {
        if (clsPlay.PlayExists()) {
          Sustain.PlayPedal(!Sustain.PedalDown);
        } else {
          MidiPlay.OutMRec.SendShortMsg((byte)(0xb0 | MidiPlay.KBOutChanRec), 0x40, 0);  //sustain off
        }
      }

      //internal static bool PlayExists() {
      //  return (P.F != null && P.F.frmShowChords != null && P.F.frmShowChords.Play != null);
      //}

      private static clsPlay.clsSustain Sustain {
        get { return clsPlay.Sustain; }
      }

      protected void CheckAllNotesOff() {
        //* switch off any hanging midi pitches (just in case!)
        if (P.frmStart.chkAllNotesOffAfterSustain.Checked 
        && P.frmSC.Play != null 
        && clsPlay.KBPitchList.Count == 0) {
          MidiPlay.OutMRec.SendShortMsg((byte)(0xb0 | MidiPlay.KBOutChanRec), 0x7b, 0);
         //Debug.WriteLine("clsPlay: clsSustain: AllNotesOff");
        }
      }
    }

    //internal class clsSustainNone : clsSustain {
    //  internal clsSustainNone(clsPlay play) : base(play) { }
    //}

    internal class clsSustainNormal : clsSustain {
      internal clsSustainNormal(clsSustain oldsustain) : base(oldsustain) { }

      internal override void PlayPedal(bool on) {
        PedalDown = on;
        if (P.frmStart.chkDelaySustain.Checked && !on) {
          Delay = true;
        } else {
          int data = (on) ? 127 : 0;
          MidiPlay.OutMRec.SendShortMsg((byte)(0xb0 | MidiPlay.KBOutChanRec), 0x40, data);
        }
        //CheckAllNotesOff();
      }

      internal override void MidiOn() {
        if (Delay) MidiPlay.OutMRec.SendShortMsg((byte)(0xb0 | MidiPlay.KBOutChanRec), 0x40, 0);  //sustain off
        Delay = false;
      }
    }

    internal abstract class clsSustainEmu : clsSustain {
      protected clsSustainEmu(clsSustain oldsustain)
        : base(oldsustain) {
        SustainTimer = new System.Timers.Timer(Math.Max(1, (int)P.frmStart.nudTimerSustain.Value));
        SustainTimer.AutoReset = false;
        SustainTimer.Elapsed += OnSustainTimer;
      }

      protected static object TimerLock = new object();
      protected System.Timers.Timer SustainTimer;
      protected abstract void OnSustainTimer(Object source, System.Timers.ElapsedEventArgs e);
      internal abstract void SustainedOff();  //switch sustain off (e.g. midion)

      internal override void PlayPedal(bool on) {
        PedalDown = on;
        if (P.frmStart.chkDelaySustain.Checked && !on) {
          Delay = true;
        } else {
          if (!on) SustainedOff();
        }
        CheckAllNotesOff();
      }

      protected virtual void SustainedOffAction() {
        lock (clsPlay.KBPitchList) {
          List<clsPlay.clsKBPitch> kbplist = null;
          kbplist = clsPlay.KBPitchList.GetSustained();
          foreach (clsPlay.clsKBPitch kbp in kbplist) {
            if (kbp.TimerActive) {  //midi ON after start of timer period
              kbp.TimerActive = false;
              continue;
            }
            clsPlay.KBPitchList.Remove(kbp);
            if (clsPlay.KBPitchList.GetPitch(false, kbp.Pitch).Count == 0) {
              clsPlay.SendDirect(0x80 | MidiPlay.KBOutChanRec, kbp.Pitch, 0);  //OFF
            }
          }
        }
        //if (kbplist != null && kbplist.Count > 0) {
        //  P.F.frmShowChords.BeginInvoke(new clsPlay.delegResizeForm(P.F.frmShowChords.ResizeForm));
        //}
      }

      internal override bool MidiOff(clsPlay.clsKBPitch kbp) {
        if (PedalDown) {  //defer it
          kbp.Sustained = true;
          if (SustainTimer.Enabled) kbp.TimerActive = true;  //timer should not switch this off
          //P.frmSC.Play.MidiOffSub();  //can't do in timer callback - no KB on sustained note
          return true;
        }
        return false;
      }

      internal override void MidiOn() {
        if (PedalDown || Delay) SustainedOff();
        Delay = false;
      }
    }

    internal class clsSustainCarryOver : clsSustainEmu {
      internal clsSustainCarryOver(clsSustain oldsustain) : base(oldsustain) { }

      protected override void OnSustainTimer(Object source, System.Timers.ElapsedEventArgs e) {
        lock (TimerLock) {
          SustainedOffAction();
        }
      }

      protected override void SustainedOffAction() {
        base.SustainedOffAction();
      }

      internal override void SustainedOff() {
        if (SustainTimer.Enabled) return;
        lock (clsPlay.KBPitchList) {
          if (clsPlay.KBPitchList.GetSustained().Count == 0) return;
        }
        SustainTimer.Start();
      }

      internal override bool MidiOnDup(int pitch) {
        if (PedalDown) {
          lock (clsPlay.KBPitchList) {
            List<clsKBPitch> kbpl = KBPitchList.GetPitch(null, pitch);
            for (int i = 0; i < kbpl.Count; i++) {
              clsKBPitch kbp = kbpl[i];
              //* if pitch is sustained, associate with this kb
              //* else add new kb for this pitch
              if (kbp.Sustained) clsPlay.KBPitchList.Remove(kbp);
              ///////////KBPitchList.Add(B[1], pitch);
            }
          }
          return true;
        }
        return false;
      }
    }

    internal class clsSustainReplay : clsSustainEmu {
      internal clsSustainReplay(clsSustain oldsustain) : base(oldsustain) { }

      protected override void OnSustainTimer(Object source, System.Timers.ElapsedEventArgs e) {
        lock (TimerLock) {
          SustainedOffAction();
        }
      }

      internal override void SustainedOff() {
        if (SustainTimer.Enabled) return;
        lock (clsPlay.KBPitchList) {
          if (clsPlay.KBPitchList.GetSustained().Count == 0) return;
          SustainedOffAction();  //already locked
        }
      }
    }

    internal class clsSustainSendCtlr : clsSustainEmu {
      internal clsSustainSendCtlr(clsSustain oldsustain) : base(oldsustain) { }

      protected override void OnSustainTimer(Object source, System.Timers.ElapsedEventArgs e) {
        lock (TimerLock) {
          if (PedalDown) {
            clsPlay.SendDirect(0xb0 | MidiPlay.KBOutChanRec, 0x40, 127);  //pedal down
          }
        }
      }

      protected override void SustainedOffAction() {
        clsPlay.SendDirect(0xb0 | MidiPlay.KBOutChanRec, 0x40, 0);  //pedal up
      }

      internal override bool MidiOff(clsKBPitch kbp) {  //no action
        return false;
      }

      internal override void SustainedOff() {
        if (SustainTimer.Enabled) return;
        SustainTimer.Start();
        SustainedOffAction();
      }
    }
  }
}