using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ChordCadenza {
  internal class BezierWarning {
    internal BezierWarning(string msg) {
      Debug.WriteLine("Bezier Warning: " + msg);
    }
  }

  internal class NoteNameWarning {
    internal NoteNameWarning(string msg) {
      Debug.WriteLine("NoteName Warning: " + msg);
    }
  }

  internal class TestException : Exception {
    internal TestException() {}
  }

  internal class AppException : Exception {
    internal AppException() { }
  }

  internal class IniParameterException : Exception {
    internal IniParameterException(string msg) : base(msg) {
    }
  }

  internal class NotYetCodedException : Exception {
    internal NotYetCodedException() { }
  }

  internal class MidiChordFileException : Exception {
  }

  internal class TSigException : Exception {
  }

  internal class MidiFileWarning {
    internal MidiFileWarning(string msg) {
      Debug.WriteLine("MidiFile Warning: " + msg);
    }
  }

  internal class MidiFileException : MidiChordFileException {
    internal MidiFileException() { }

    internal MidiFileException(string msg) {
      P.MMSW?.WriteLine("*** EXCEPTION: Midi File Read Error");
      Debug.WriteLine(msg);
      if (P.MMSW == null) MessageBox.Show(msg);
    }
  }

  internal class ChordFileException : MidiChordFileException {
    internal ChordFileException() { }

    internal ChordFileException(string msg) {
      Debug.WriteLine("Chord File Error: " + msg);
    }
  }

  internal class CfgFileException : Exception {
    internal CfgFileException(string msg) {
      MessageBox.Show("Cfg File Exception: " + msg);
      Environment.Exit(1);
    }
  }

  internal class FatalException : Exception {
    internal FatalException() {}
  }

  internal class LogicException : Exception {
    internal LogicException() { }
    internal LogicException(string msg) {
      Debug.WriteLine("Logic Exception: " + msg);
    }
  }

  internal class DebugException : Exception {
    internal DebugException() { }
  }

  internal class OnOffWarning {
    internal OnOffWarning(string msg) {
      Debug.WriteLine("OnOff Warning: " + msg);
    }
  }

  internal class OnOffException : Exception {  //On/Off inconsistent
    internal OnOffException() { }
  }

  internal class XMMTimerException : Exception {  //multimedia timer exception
    internal XMMTimerException() { }
  }

  internal class MidiIOWarning {
    internal MidiIOWarning(int ret, string dev) {
      Debug.WriteLine("Midi IO Warning return code: " + ret + " on device: " + dev);
    }
  }

  internal class MidiIOException : Exception {
    internal MidiIOException(int ret, string dev) {
      Debug.WriteLine("Midi IO Exception return code: " + ret + " on device: " + dev);
    }
    internal MidiIOException(string msg) {
      Debug.WriteLine("Midi IO Exception: " + msg);
    }
    internal MidiIOException() {
      Debug.WriteLine("Midi IO Exception");
    }
  }

  internal class AudioIOException : Exception {
    internal AudioIOException(int ret, string dev) {
      string txt = "Audio IO Exception return code: " + ret + " on device: " + dev;
      Debug.WriteLine(txt);
      MessageBox.Show("Audio IO Exception");
    }
    internal AudioIOException(string msg) {
      string txt = "Audio IO Exception: " + msg;
      Debug.WriteLine(txt);
      MessageBox.Show("Audio IO Exception");
    }
  }
}