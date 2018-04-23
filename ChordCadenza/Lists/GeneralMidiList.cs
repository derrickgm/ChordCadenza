using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChordCadenza {
  internal static class GeneralMidiList {
    internal static string[] Desc = new string[128];

    static GeneralMidiList() {
      List<string> lines = Utils.ReadLines(Cfg.GeneralMidiDatFilePath);
      char[] delimspace = new char[] { ' ' };
      for (int i = 0; i < 128; i++) {
        string[] f = lines[i].Split(delimspace, 2);
        if (int.Parse(f[0]) != i + 1) LogicError.Throw(eLogicError.X050);
        Desc[i] = f[1];
      }
    }
  }
}
