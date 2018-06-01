#undef DebugHeader
#undef DebugEvents

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace ChordCadenza {
  public partial class clsFileStream {
    internal class clsPlay {
      //internal clsPlay(clsFileStream filestream, clsMute mute) {
      //  FileStream = filestream;
      //  Mute = mute;
      //  ResetAllCtlrsTimer.AutoReset = false;
      //  //ResetAllCtlrsTimer.Elapsed += ResetAllCtlrsTimer_Elapsed;
      //}

      //[DllImport("winmm.dll")]
      //private static extern int midiOutReset
      //  (IntPtr hMidiOut);

      //[DllImport("winmm.dll")]
      //private static extern int midiOutShortMsg
      //  (IntPtr hMidiOut, int dwMsg);

      //internal int SendDirect(int status, int msg, int data) {
      //  int xword = (((data << 8) | msg) << 8) | status;
      //  int ret = midiOutShortMsg(hmi, xword);  
      //  //if (P.F != null && P.F.FileStreamMM != null) {
      //  //  P.F.FileStreamMM.RecordEv(MidiPlay.MidiInKB.Ticks.Value, (byte)status, (byte)msg, (byte)data);  //filtered by channel 
      //  //}
      //  //Debug.WriteLine("MidiOut SendDirect " + zmsg + " " + zpitch + " " + zvel);
      //  return ret;
      //}

      //internal int SendDirect(byte[] b) {
      //  if (b.Length != 3) {
      //    LogicError.Throw(eLogicError.X053);
      //    return 0;
      //  }
      //  return SendDirect(b[0], b[1], b[2]);
      //}

      //[DllImport("winmm.dll")]
      //private static extern int midiOutSetVolume
      //  (IntPtr hMidiOut, int dwMsg);

      //private clsFileStream FileStream;
      //private clsMidiOutStream MidiStream;
      //private clsMidiOutStream MidiKB;
      //private IntPtr hmi;
      //private int NextIndex;
      //private int NextTicks;
      //private int LastTicksWritten = 0;
      //List<byte> Bytes;
      //private List<IntPtr> PtrMidiHdr = new List<IntPtr>();
      //private List<IntPtr> PtrMidiHdrData = new List<IntPtr>();
      //internal LinkedList<sBuffer> Buffer = new LinkedList<sBuffer>();
      //internal int ChunksPerQNote;
      //internal int ChunksPerBeat;
      //private int TicksPerChunk;
      //private byte[] StreamID = new byte[4];  //assume 0
      internal bool indStop = false;
      //internal bool MuteChanged = false;  //true if mute/solo changed during play

      //private clsMute Mute;

      //private clsMTime.clsBBT StartBBT;
      //private clsMTime.clsBBT NextBBT;

      private System.Timers.Timer ResetAllCtlrsTimer = new System.Timers.Timer(100);

      internal static int Find(clsFileStream fs, clsFileStream.clsEvStrm[] strm, int ticks) {
        //* return index of ev on or after ticks
        if (ticks == 0) return 0;
        //int index = Array.BinarySearch<clsEvStrm>(strm, new clsEvShort(ticks, 0, 0, 0, 0));
        int index = Array.BinarySearch<clsEvStrm>(strm, new clsEvBeat(new clsMTime.clsBBT(ticks)));
        if (index < 0) index = ~index;  //if not found, get index after ticks
        else {  //if found, locate first ev with ticks
          while (strm[index].Ticks == ticks) {
            if (--index < 0) break;
          }
          index++;
        }
        return index;
      }

      internal void ResetAllCtlrs() {
        ResetAllCtlrsStatic(null);
      }

      internal void AllNotesOff(bool indrec) {
        AllNotesOffStatic(null);
      }

      internal static void ResetAllCtlrsStatic(iBassMidiOut bassout) {
        //* used by clsPlay or clsBASSOut
        //Debug.WriteLine("Reset All Controllers at " + DateTime.Now);  //tmp debugging
        //* don't send if recording (should only be at start)
        //if (P.F != null && P.F.FileStreamMM != null && P.F.FileStreamMM.indRec) return; 
        if (bassout == null) return;
        for (byte ch = 0; ch < 16; ch++) {
          byte status = (byte)(0xb0 | ch);
          OutShortMsg(bassout, status, 0x79, 0);  //reset all controllers
          //* pan, vol (& others?) appear not to be reset with resetallcontrollers in sonar/cubase
          OutShortMsg(bassout, status, 0x0a, 64);  //pan centre 
          OutShortMsg(bassout, status, 0x07, 100);  //vol 100
          OutShortMsg(bassout, status, 0x40, 0);  //sustain off
          OutShortMsg(bassout, status, 101, 0);  //set pitchbend range MSB
          OutShortMsg(bassout, status, 100, 0);  //set pitchbend range LSB
          OutShortMsg(bassout, status, 6, 2);  //data entry MSB +-2 semitones
          OutShortMsg(bassout, status, 38, 0);  //data entry LSB 0
          OutShortMsg(bassout, status, 100, 127);  //reset RPN LSB
          OutShortMsg(bassout, status, 101, 127);  //reset RPN MSB
          OutShortMsg(bassout, status, 98, 127);  //reset NRPN LSB
          OutShortMsg(bassout, status, 99, 127);  //reset NRPN MSB
          OutShortMsg(bassout, status, 0x7b, 0);  //all notes off
        }
      }

      internal static void AllNotesOffStatic(iBassMidiOut bassout) {
        if (bassout == null) return;
        for (byte ch = 0; ch < 16; ch++) {
          byte status = (byte)(0xb0 | ch);
          OutShortMsg(bassout, status, 0x7b, 0);  //all notes off
          OutShortMsg(bassout, status, 0x40, 0);  //sustain off
        }
      }

      private static void OutShortMsg(iBassMidiOut bassout, byte status, byte msg, byte data) {
        if (bassout == null) return;
        bassout.SendShortMsg(status, msg, data);
      }

      //internal int SetVolume(byte vol) {
      //  //* set output volume MSB of both channels to vol
      //  int msg = (vol << 28) | (vol << 12);
      //  int rtn = midiOutSetVolume(hmi, msg);
      //  return rtn;
      //}
 
      internal static int FactorMidiTempo(clsFileStream  filestream, int miditempo) {
        if (P.frmStart.TempoFactor == 0) P.frmStart.TempoFactor = 1;
        if (P.frmStart.TempoFactor != 1) {
          miditempo = (int)(miditempo / P.frmStart.TempoFactor); 
          miditempo = Math.Min(Math.Max(miditempo, filestream.MidiTempoMin), filestream.MidiTempoMax);
        }
        return miditempo;
      }

      //internal void Stop() {
      //  if (MidiStream == null) return;
      //  indStop = true;
      //}

      internal static bool BypassEv(clsFileStream filestream, clsMute mute, clsEvStrm ev) {
        //* return true to bypass
        if (ev is clsFileStream.clsEvBeat) return false;  //retain beats
        if (ev.Trk == null) return false;  //retain conductor trk 
        if (ev is clsEvTempo) return false;  //retain meta msgs (tempo)
        if (ev is clsEvSystem) return true;  //bypass system/meta
        if (ev is clsEvMeta) return true;  //bypass system/meta
        clsEvShort evshort = (clsEvShort)ev;
        byte status = evshort.Status;
        int statusprefix = status & 0xf0;
        if (statusprefix >= 0xf0) return false;
        bool on = (statusprefix == 0x90 && evshort.Data > 0);  //all others evs = OFF
        int chan = status & 0x0f;
        if (mute.MutedEv(ev.Trk, chan, on)) return true;  
        if (statusprefix != 0x90 && statusprefix != 0x80) return filestream.MidiCtlrs.Bypass(evshort);
        return false;
      }
    }
  }
}
