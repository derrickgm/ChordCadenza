using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace ChordCadenza {
  internal class clsWaitPlay {
    private delegate void delStartPlay();
    private delStartPlay dStartPlay;
    private Thread PlayThread;

    private delegate void delegUpdateTempo(int tempo, int ticks);
    private delegUpdateTempo dUpdateTempo;

    private delegate void delegStreamEnd();
    private delegStreamEnd dStreamEnd;

    private clsFileStream FileStream;
    private iBassMidiOut BASSOut = new clsBassMidiOutNull();
    private clsMute Mute;
    private clsFileStream.clsEvStrm[] Strm { get { return FileStream.Strm; } }
    private System.Timers.Timer ResetAllCtlrsTimer = new System.Timers.Timer(100);
    private int MidiTempo = -1;
    private int FactoredMidiTempo = -1;
    private float TempoFactor = -1;
    private int StrmPos = 0;  //current position in Strm 
    private Stopwatch SW = new Stopwatch();
    private int TicksStart = 0;
    private bool indStop = false;
    private int LastElapsedMilliseconds = 0;
    private int Resolution;
    private int HalfResolution;

    internal clsWaitPlay(clsFileStream filestream, iBassMidiOut bassout, clsMute mute) {
      Resolution = (int)P.frmStart.nudPlayResolution.Value;
      HalfResolution = Resolution / 2;  
      dStartPlay = new delStartPlay(StartPlay);
      PlayThread = new Thread(new ThreadStart(StartPlay));
      PlayThread.Priority = ThreadPriority.AboveNormal;
      PlayThread.Name = "Play Thread";
      //Debug.WriteLine("clsWaitPlay: ThreadId = " + PlayThread.ManagedThreadId);
      FileStream = filestream;
      BASSOut = bassout;
      Mute = mute;
      dUpdateTempo = new delegUpdateTempo(P.frmStart.UpdateLblTempo);
      ResetAllCtlrsTimer.AutoReset = false;
      ResetAllCtlrsTimer.Elapsed += ResetAllCtlrsTimer_Elapsed;
    }

    internal void Start() {
      PlayThread.Start();
    }

    private void StartPlay() {
      clsMTime.clsBBT bbt = P.F.CurrentBBT;
      if (bbt.TicksRemBeat > 0) {
        LogicError.Throw(eLogicError.X020);   //should not happen?
        bbt.NextBeat();
      }

      if (P.F.frmTrackMap != null && P.F.frmTrackMap.RecTrk != null) {
        P.F.frmTrackMap.Do.UpdateToBuffer(P.F.frmTrackMap.RecTrk);
      }

      //* initialize
      StrmPos = clsFileStream.clsPlay.Find(FileStream, Strm, bbt.Ticks);
      FileStream.MidiCtlrs.SendCtlrs(bbt.Ticks);

      //* set initial tempo and interval and set periodic timer
      MidiTempo  = FileStream.TempoMap[bbt.Ticks];

      //* initialize recording stream 
      if (P.F.frmTrackMap?.RecTrk != null) {
        FileStream.RecStrmNew = new List<clsFileStream.clsEvShort>(500);
      }

      ResetSW(bbt);
      List<byte> bytes;
      clsMTime.clsBBT bbtbeat;

      ////int loopcnt = 0;
      while (true) {
        ////loopcnt++;
        int diff = 0;
        lock (Strm) {
          //if (!ReadStreamChord(out bytes, out bbtbeat)) break;  //StrmPos++ (multiple) -> next ev
          ReadStreamChord(out bytes, out bbtbeat);  //StrmPos++ (multiple) -> next ev
          if (indStop) return;
          SendEvents(bytes);
          LastElapsedMilliseconds = (int)SW.ElapsedMilliseconds - HalfResolution;
          if (StrmPos >= Strm.Length) break;
          if (Strm[StrmPos].Ticks > P.F.MaxBBT.Ticks) break;
          if (bbtbeat != null) {
            MidiPlay.Sync.CallBack(clsSync.eMidiMsgType.StreamBASS, null, FileStream, bbtbeat.Copy());
          }
          //if (bbtbeat != null) Debug.WriteLine("clsWaitPlay: bbtbeat = " + bbtbeat.ToString());
          int ticks = Strm[StrmPos].Ticks;  //next ev
          int msecs = ((ticks - TicksStart) * GetMicroSecsPerTick()) / 1000;
          diff = msecs - LastElapsedMilliseconds;
          if (TempoFactor != P.frmStart.TempoFactor) {
            ResetSW(new clsMTime.clsBBT(Strm[StrmPos].Ticks));
            TicksStart = ticks - ((diff * 1000) / GetMicroSecsPerTick());
            //Debug.WriteLine("clsWaitPlay: PIStart = " + PIStart);
          }
          //Debug.WriteLine("clsWaitPlay: diff = " + diff);
        }
        if (diff >= (int)P.frmStart.nudPlayResolution.Value) Thread.Sleep(diff);
      }
      //Debug.WriteLine("clsWaitPlay: loopcnt = " + loopcnt);
      indStop = false;
      MidiPlay.Sync.indPause = false;
      FinalizeStop();
      MidiPlay.Sync.CallBack(clsSync.eMidiMsgType.StreamEnded, FileStream);
    }

    private void ReadStreamChord(out List<byte> bytes, out clsMTime.clsBBT bbtbeat) {
      //* read Strm until pitime changes
      clsFileStream.clsEvStrm ev = Strm[StrmPos];
      int ticks = ev.Ticks;
      bytes = new List<byte>(36);
      bbtbeat = null;
      //Debug.WriteLine("clsQIPlay.ReadStrm: QIThis = " + QIThis);
      while (ev.Ticks == ticks) {
        if (!clsFileStream.clsPlay.BypassEv(FileStream, Mute, ev)) {  //muted evs etc.
          //* process evtempo/evshort msgs for current ticks/qi
          if (ev is clsFileStream.clsEvBeat) {
            bbtbeat = ((clsFileStream.clsEvBeat)ev).BBT;
          } else if (ev is clsFileStream.clsEvTempo) {
            MidiTempo = ((clsFileStream.clsEvTempo)ev).MidiTempo;
            TempoFactor = -1;  //force tmepo recalc
          } else if (ev is clsFileStream.clsEvShort) {
            ev = ev.Transpose();
            clsFileStream.clsEvShort evshort = (clsFileStream.clsEvShort)ev;
            if (bytes == null) bytes = new List<byte>(36);
            bytes.Add(0);  //deltatime - always zero (ignored by BASS_MIDI_StreamEvents)
            bytes.Add(evshort.Status);
            bytes.Add(evshort.Msg);
            int statuspre = (evshort.Status & 0xf0);
            if (statuspre != 0xc0 && statuspre != 0xd0) bytes.Add(evshort.Data);
          }
        }
        if (++StrmPos >= Strm.Length) return;  //return false;
        ev = Strm[StrmPos];
      }
      //return (ev.PITime <= P.F.MaxPITime);
    }

    private void ResetSW(clsMTime.clsBBT bbt) {
      TempoFactor = P.frmStart.TempoFactor;
      FactoredMidiTempo = clsFileStream.clsPlay.FactorMidiTempo(FileStream, MidiTempo);
      P.frmSC.BeginInvoke(dUpdateTempo, FactoredMidiTempo, bbt.Ticks);
      SW.Restart();
      //PIStart = bbt.Ticks / P.F.TicksPerPI;
      TicksStart = bbt.Ticks;
    }

    internal int? GetTicks() {
      lock (Strm) {
        //* get variables 
        if (StrmPos >= Strm.Length) return null;
        if (StrmPos < 1) return 0;
        int msecsdiff = (int)SW.ElapsedMilliseconds - HalfResolution - LastElapsedMilliseconds;
        if (msecsdiff > 0) {
          int ticksdiff = (1000 * msecsdiff) / GetMicroSecsPerTick();
          //Debug.WriteLine("clsWaitPlay: GetTicks: TicksDiff = " + (pidiff / P.F.TicksPerPI));
          return Strm[StrmPos - 1].Ticks + ticksdiff;
        }
        return Strm[StrmPos - 1].Ticks;
      }
    }

    private void ResetAllCtlrsTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
      ResetAllCtlrs();
    }

    internal void ResetAllCtlrs() {
      clsFileStream.clsPlay.ResetAllCtlrsStatic(BASSOut);
    }

    private int GetMicroSecsPerTick() {
      //* calculate timer interval length in msecs
      //* miditempo is microsecs per midi quarternote   
      return FactoredMidiTempo / P.F.MTime.TicksPerQNote;
    }

    private void SendEvents(List<byte> byteslist) {
      if (byteslist != null && byteslist.Count > 0) {
        BASSOut.SendEvents(byteslist.ToArray());
        byteslist = new List<byte>(36);
      }
    }

    internal void Stop() {  //called from Form thread
      MidiPlay.Sync.RefreshBBTTimer.Stop();
      MidiPlay.Sync.BBTQueue.Clear();
      FinalizeStop();
      if (!MidiPlay.Sync.indPause) {
        P.frmSC.Play?.NewReset();
        Forms.frmStart.RefreshBBT(new clsMTime.clsBBT(P.F.StartBar, 0, 0));
        MidiPlay.Sync.indPause = true;
      }
    }

    private void FinalizeStop() {
      indStop = true;
      clsFileStream.clsPlay.AllNotesOffStatic(BASSOut);
      dStreamEnd = new delegStreamEnd(P.frmStart.StreamPlayOffAll);
      P.frmSC.Invoke(dStreamEnd);
      if (P.F.frmTrackMap?.RecTrk != null) FileStream.MergeRecStrm();
      P.frmPCKBIn?.Invoke(new clsSync.delegClosefrmPCKBIn(P.frmPCKBIn.Close));
    }
  }
}
