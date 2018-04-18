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
  internal class clsMaxBBT {
    //* this should be big enough to accommodate all structures, with room to spare... 
    internal clsMaxBBT(int maxmiditicks) {
      int rem;
      MidiWholeNotes = Math.DivRem(maxmiditicks, P.F.MTime.TicksPerQNote * 4, out rem);
      if (rem > 0) MidiWholeNotes++;
      MaxNoteMapTicks = (MidiWholeNotes + 8) * P.F.MTime.TicksPerQNote * 4;
      MaxNoteMapQI = MaxNoteMapTicks / P.F.TicksPerQI;
      InitBBT();
    }

    internal void InitBBT() {  //was ReInit()
      BBT = new clsMTime.clsBBT(MidiWholeNotes * P.F.MTime.TicksPerQNote * 4);
      BBT = new clsMTime.clsBBT(BBT.Bar + 2, 0, 0);
    }

    private clsMTime.clsBBT BBT;
    internal readonly int MidiWholeNotes;
    internal readonly int MaxNoteMapTicks;
    internal readonly int MaxNoteMapQI;
  
    //internal int MaxMidiTicks { get { return _MaxMidiTicks; } }
    internal int Ticks { get { return BBT.Ticks; } }
    internal int Bar { get { return BBT.Bar; } }  //may change after tsig change 
    internal int Beats { get { return BBT.Beats; } }
    internal int QI { get { return BBT.Ticks / P.F.TicksPerQI; } }
    internal clsMTime.clsBBT BBTCopy { get { return BBT.Copy(); } }
  }

  internal class clsBars {
    internal const int NumBarsMax = 200;
    private int _NumBars = -1;
    internal int NumBars { get { return _NumBars; } }

    internal bool SetNumBars(int num) {
      if (num < 1 || num > NumBarsMax) return false;
      _NumBars = num;
      return true;
    }
  }
}
