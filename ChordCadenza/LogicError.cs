using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace ChordCadenza {
  internal enum eStopError : int {
    Y000 = 0,
    Y001,
    Y002,
    Y003
  }

  internal enum eLogicError : int {
    X999 = 0,

    X166,
    X165,
    X164,
    X163,
    X162,
    X161,
    X160,
    X159,
    X158,
    X157,
    X156,
    X155,
    X154,
    X153,
    X152,
    X151,
    X150,
    X149,
    X148,
    X147,
    X146,
    X145,
    X144,
    X143,
    X142,
    X141,
    X140,
    X139,
    X138,
    X137,
    X136,
    X135,
    X134,
    X133,
    X132,
    X131,
    X130,
    X129,
    X128,
    X127,
    X126,
    X125,
    X124,
    X123,
    X122,
    X121,
    X120,
    X119,
    X118,
    X117,
    X116,
    X115,
    X114,
    X113,
    X112,
    X111,
    X110,
    X109,
    X108,
    X107,
    X106,
    X105,
    X104,
    X103,
    X102,
    X101,
    X100,

    X000,
    X001, 
    X002, 
    X003, 
    X004,
    X005,
    X006,
    X007,
    X008,
    X009,
    X010,
    X011,
    X012,
    X013,
    X014,
    X015,
    X016,
    X017,
    X018,
    X019,
    X020,
    X021,
    X022,
    X023,
    X024,
    X025,
    X026,
    X027,
    X028,
    X029,
    X030,
    X031,
    X032,
    X033,
    X034,
    X035,
    X036,
    X037,
    X038,
    X039,
    X040,
    X041,
    X042,
    X043,
    X044,
    X045,
    X046,
    X047,
    X048,
    X049,
    X050,
    X051,
    X052,
    X053,
    X054,
    X055,
    X056,
    X057,
    X058,
    X059,
    X060,
    X061,
    X062,
    X063,
    X064,
    X065,
    X066,
    X067,
    X068,
    X069,
    X070,
    X071,
    X072,
    X073,
    X074,
    X075,
    X076,
    X077,
    X078,
    X079,
    X080,
    X081,
    X082,
    X083,
    X084,
    X085,
    X086,
    X087,
    X088,
    X089,
    X090,
    X091,
    X092,
    X093,
    X094,
    X095,
    X096,
    X097,
    X098,
    X099
  };

  internal class LogicError {
    private static int[] Counts = new int[200];  //0 - 199

    internal static void Throw(eLogicError e) {
#if DEBUG
      //throw new LogicException((e).ToString());
      Debugger.Break();
#else
      //Debug.WriteLine(DateTime.Now + " Logic Exception (not thrown): " + e);
      Counts[(int)e]++;
#endif
    }

    internal static void Throw(eStopError e) {
#if DEBUG   //ThrowStopException
      throw new LogicException((e).ToString());
#else
      //Debug.WriteLine("Stop Exception (not thrown): " + e);
      Counts[(int)e]++;
#endif
    }

    internal static void Throw(eLogicError e, string msg) {
#if DEBUG
      Debug.WriteLine(DateTime.Now + " Logic Exception (not thrown): " + e + " : " + msg);
      Counts[(int)e]++;
      //throw new LogicException(e + " : " + msg);
#else
      Counts[(int)e]++;
#endif
    }

    internal static void Throw(eStopError e, string msg) {
#if DEBUG  //ThrowStopException
      Debugger.Break();
#else
      Counts[(int)e]++;
#endif
    }

    internal static void ShowTotals() {
      string msg = "LogicError Counts:\n";
      bool show = false;
      for (int i = 0; i < Counts.Length; i++) {
        if (Counts[i] > 0) {
          show = true;
          msg += "LogicError " + (eLogicError)i + " Count = " + Counts[i] + "\n";  
        }
      }
      if (show) MessageBox.Show(msg);
    }
  }
}