using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Reflection;
using System.Linq;

using Un4seen.Bass;
using Un4seen.Bass.AddOn.Midi;
using Un4seen.Bass.Misc;
using Un4seen.BassAsio;
using Un4seen.Bass.AddOn.Mix;

namespace ChordCadenza {
  //internal static class Env {
  //  internal static string temp = Application.ExecutablePath + "\\..\\..\\..\\tmp";
  //  static Env() {
  //    if (!Directory.Exists(temp)) Directory.CreateDirectory(Env.temp);
  //  }
  //}

  internal class LLStack<T> {
    //* stack with limit on number of entries allowed
    private LinkedList<T> LL = new LinkedList<T>();
    private readonly int Capacity;

    internal LLStack(int capacity) {
      Capacity = capacity;
    }

    internal void Push(T val) {
      LL.AddLast(val);
      if (LL.Count > Capacity) LL.RemoveFirst();
    }

    internal T Pop() {
      T val = LL.Last.Value;
      LL.RemoveLast();
      return val;
    }

    internal T Peek() {
      return LL.Last.Value;
    }

    internal int Count {
      get {
        return LL.Count;
      }
    }
  }

  public class clsWaitCursor : IDisposable {
    public clsWaitCursor() {
      Cursor.Current = Cursors.WaitCursor;
    }
    public void Dispose() {
      Cursor.Current = Cursors.Default;
    }
  }

  public class clsLoadingProject : IDisposable {
    public clsLoadingProject() {
      clsF.clsindSave.Loading = true;
    }
    public void Dispose() {
      clsF.clsindSave.Loading = false;
      clsF.clsindSave.SetCmdsEna(false);
      clsAudioSync.SetPlayAudioText(P.F?.AudioSync);
      P.F.StartBar = 0;
      if (P.F.MTime == null) P.F.SetEmpty(clsCF.DefaultSongLength);
      P.F.CurrentBBT = new clsMTime.clsBBT(0);
      P.frmSC.Play?.NewReset();
      Forms.frmStart.RefreshBBT(P.F.CurrentBBT);
      P.frmSC.ResizeForm();
      P.frmSC.Refresh();
    }
  }

  public class clsChordMapDis : IDisposable {
    private Forms.frmChordMap Frm;
    //////private clsMTime.clsBBT BBT;
    private int AutoScrollX;

    internal clsChordMapDis(Forms.frmChordMap frm) {
      Frm = frm;
      AutoScrollX = Frm.panNoteMap.AutoScrollPosition.X;
      Frm.NoScroll = true;
      Frm.Bypass_DGV = true;
      /////BBT = new clsMTime.clsBBT(Frm.PixToBars(Frm.CsrPixPos), 0, 0);
      //Frm.NoRefreshDGV = true;
    }

    public void Dispose() {
      Frm.Bypass_DGV = false;
      Frm.NoScroll = false;
      //* restore scroll position
      //* Frm.panNoteMap.HorizontalScroll.Maximum is always bigger than actual maximum
      //* setting autoscrollposition above max seems to set it to max???
      int scrollmax = Frm.panNoteMap.HorizontalScroll.Maximum;
      Frm.panNoteMap.AutoScrollPosition = new Point(-AutoScrollX, 0);
      Frm.DGV.HorizontalScrollingOffset = -AutoScrollX;
      Frm.dgvLyrics.HorizontalScrollingOffset = -AutoScrollX;
      Frm.CsrPixLo = Frm.AlignCsrToBar(Frm.CsrPixLo);
      Frm.CsrPixHi = Frm.AlignCsrToBar(Frm.CsrPixHi);
      P.F.CurrentBBT.RoundToBar();
      Frm.Refresh();
      //Frm.NoRefreshDGV = false;
    }
  }

  public static class MessageBox {
    //private static bool indCached = false;
    //private static string Cache = "";

    public static void Show(string text) {  //no dialogresult
      //if (indCached) {
      //  Cache += text + "\r\n";
      //} else {
        System.Windows.Forms.MessageBox.Show(text, "Chord Cadenza");
      //}
    }

    public static DialogResult Show(string text, MessageBoxButtons buttons) {  
      return System.Windows.Forms.MessageBox.Show(text, "Chord Cadenza", buttons);
    }

    public static DialogResult Show(string text, MessageBoxButtons buttons, MessageBoxIcon icon) {
      return System.Windows.Forms.MessageBox.Show(text, "Chord Cadenza", buttons, icon);
    }

    //public static void CacheOn() {
    //  indCached = true;
    //  Cache = "";
    //}

    //public static void CacheOff() {
    //  indCached = false;
    //  if (Cache != "") System.Windows.Forms.MessageBox.Show(Cache, "Chord Cadenza");
    //  Cache = "";
    //}
  }

  public class PanelNoScrollOnFocus : System.Windows.Forms.Panel {
    protected override System.Drawing.Point ScrollToControl(System.Windows.Forms.Control activeControl) {
      // Returning the current location prevents the panel from
      // scrolling to the active control when the panel loses and regains focus
      return this.DisplayRectangle.Location;
    }
  }

  /*
  internal static class TestBass {
    private static void WriteLine(string format, params object[] args) {
      //***Debug.WriteLine(format, args);
    }

    private static int CheckHandle(int handle) {
      return clsBassOutMidi.CheckHandle(handle);
    }

    private static void CheckOK(bool ok) {
      clsBassOutMidi.CheckOK(ok);
    }

    internal static int BASS_MIDI_StreamCreate(int numchans, BASSFlag flags, int freq) {
      int ret = CheckHandle(BassMidi.BASS_MIDI_StreamCreate(numchans, flags, freq));
      WriteLine("{0} {1} {2} {3} -> {4}", "BASS_MIDI_StreamCreate: ", numchans, flags, freq, ret);
      return ret;
    }

    internal static int BASS_Mixer_StreamCreate(int freq, int numchans, BASSFlag flags) {
      int ret = CheckHandle(BassMix.BASS_Mixer_StreamCreate(freq, numchans, flags));
      WriteLine("{0} {1} {2} {3} -> {4}", "BASS_Mixer_StreamCreate: ", freq, numchans, flags, ret);
      return ret;
    }

    internal static void BASS_Mixer_StreamAddChannel(int handle, int channel, BASSFlag flags) {
      CheckOK(BassMix.BASS_Mixer_StreamAddChannel(handle, channel, flags));
      WriteLine("{0} {1} {2} {3}", "BASS_Mixer_StreamAddChannel:", handle, channel, flags);
    }

    internal static void SetAtrrAndFont(int midistream) {
      WriteLine("{0} {1}", "SettAttrAndFont:", midistream);
    }

    internal static void BASS_ChannelPlay(int handle, bool restart) {
      CheckOK(Bass.BASS_ChannelPlay(handle, restart));
      WriteLine("{0} {1} {2}", "BASS_ChannelPlay:", handle, restart);
    }

    internal static void BASS_ASIO_Init(int device, BASSASIOInit flags) {
      CheckOK(BassAsio.BASS_ASIO_Init(device, flags));
      WriteLine("{0} {1} {2}", "BASS_ASIO_Init:", device, flags);
    }
  }
  */

  internal class IntPair {
    internal int Int0;
    internal int Int1;
    internal IntPair(int int0, int int1) {
      Int0 = int0;
      Int1 = int1;
    }
  }

  internal class IntDiv {
    private int _NN;
    private int _DD;

    internal int NN { get { return _NN; } }
    internal int DD { get { return _DD; } }

    internal IntDiv(int nn, int dd) {
      if (nn != 1 && dd != 1) throw new FatalException();
      if (nn < 1 || dd < 1) throw new FatalException();
      _NN = nn;
      _DD = dd;
    }

    public override string ToString() {
      return NN.ToString() + ":" + DD.ToString();
    }

    public static int operator /(int x, IntDiv y) {
      if (y._DD == 1) return x / y._NN; else return (x * y._DD);
    }

    public static int operator *(IntDiv x, int y) {
      return y * x;
    }

    public static int operator *(int x, IntDiv y) {
      if (y._DD == 1) return x * y._NN; else return (x / y._DD);
    }

    public static IntDiv operator ++(IntDiv x) {
      if (x._DD == 1) x._NN += 1; else x._DD -= 1;
      return x;
    }

    public static IntDiv operator --(IntDiv x) {
      if (x._NN == 1) x._DD++; else x._NN--;
      return x;
    }

    public static bool operator <(IntDiv x, int y) {
      if (x.NN < y) return true;
      if (x.NN == y && x.DD > y) return true;
      return false;
    }

    public static bool operator >(IntDiv x, int y) {
      if (x.NN > y) return true;
      if (x.NN == y && x.DD < y) return true;
      return false;
    }
  }

  internal class clsFormProps {
    private string FormName;
    internal bool IsMaximized;
    internal Rectangle Rect;

    internal clsFormProps(Form frm) {
      Init(frm);
    }

    private void Init(Form frm) {
      FormName = frm.Name; 
      switch (frm.WindowState) {
        case FormWindowState.Normal:
          IsMaximized = false;
          Rect.Location = frm.Location;
          Rect.Size = frm.Size;
          break;
        case FormWindowState.Maximized:
          IsMaximized = true;
          Rect = frm.RestoreBounds;
          break;
        case FormWindowState.Minimized:
          IsMaximized = false;
          Rect = frm.RestoreBounds;
          break;
      }
    }

    private clsFormProps(string f1) {
      //* f1 = name, maximized, X, Y, width, height
      string[] ff = f1.Split(new char[] { ',' });
      FormName = ff[0].Trim();
      IsMaximized = bool.Parse(ff[1]);
      Rect = new Rectangle(
        int.Parse(ff[2]), int.Parse(ff[3]), int.Parse(ff[4]), int.Parse(ff[5]));
    }

    public override string ToString() {
      foreach (Form frm in Application.OpenForms) {
        if (frm.Name == FormName) {
          Init(frm);  //get current loc and size
          break;
        }
      }

      return FormName  
        + ", " + IsMaximized.ToString()
        + ", " + Rect.Left.ToString()
        + ", " + Rect.Top.ToString()
        + ", " + Rect.Width.ToString()
        + ", " + Rect.Height.ToString();
    }

    internal static clsFormProps Parse(string f1) {
      //* used for parsing f[1] in Cfg
      return new clsFormProps(f1);
    }

    internal void SetForm(Form frm) {
      //* set form size and location
      if (frm == null) return;
      frm.WindowState = FormWindowState.Normal;
      frm.Location = Rect.Location;
      frm.Size = Rect.Size;
      if (IsMaximized) frm.WindowState = FormWindowState.Maximized;
    }
  }

  internal static class Utils {
    internal static string GetFileVersion() {
      //* D:\D2\Dev\CS.Express\ChordCadenza\ChordCadenza\bin\Release X64\ChordCadenza.exe
      //* D:\GoogleDrive\Expression BU\ChordCadenza 01.11.00\ChordCadenza\bin\Release X64\ChordCadenza.exe
      string path = Application.ExecutablePath;
      if (!path.StartsWith(@"D:\GoogleDrive\Expression BU\ChordCadenza ")) return "???";
      string[] dirs = path.Split(new char[] { '\\' });
      if (dirs.Length != 8) return "???";
      if (dirs[4].Length != 21) return "???";
      return dirs[4].Substring(14);
    }

    internal static void FormAct(Form frm) {
      if (frm == null) return;
      if (frm.Name == "frmSC") return;
      frm.Show();
      if (frm.WindowState == FormWindowState.Minimized) frm.WindowState = FormWindowState.Normal;
      frm.Activate();
      //* check for test form
      //* (frmSC is checked in frmSC_Load)
    }

    internal static Color? BackColor = null;
    //internal static Color? GetBackColor() {
    //  string[] args = System.Environment.GetCommandLineArgs();
    //  foreach (string arg in args) {
    //    string a = arg.ToLower();
    //    if (a.StartsWith("backcolor.")) {
    //      string c = a.Substring(10).ToLower();
    //      Color color = (c == "default") ? Color.Black : Color.FromName(c);
    //      if (color == Color.Black) return null; 
    //      return color;
    //    }
    //  }
    //  return null;
    //}

    internal static Color SetBackColor(System.Threading.Mutex mtx, Color color) {
      if (BackColor != null) return BackColor.Value;
      else if (mtx == null) return Color.Cyan;
      else return color;
    }

    internal static T[] RemoveAt<T>(T[] arr, int index) where T:IEnumerable {
      List<T> lst = arr.ToList();
      lst.RemoveAt(index);
      return lst.ToArray();
    }

    internal enum eFormType { frmStart, frmShowChords, frmSC, frmLoadCSV, frmChordMap, frmSections, frmTrackMap, frmCalcKeys };
    internal static clsFormProps[] FormsProps = new clsFormProps[5];

    private static eFormType GetFormType(Form frm) {
      if (frm is Forms.frmSC) return eFormType.frmSC;
      else if (frm is Forms.frmSummary) return eFormType.frmLoadCSV;
      else if (frm is Forms.frmChordMap) return eFormType.frmChordMap;
     // else if (frm is Forms.frmCompare) return eFormType.frmSections;
      else if (frm is Forms.frmTrackMap) return eFormType.frmTrackMap;
      else if (frm is Forms.frmCalcKeys) return eFormType.frmCalcKeys;
      else if (frm is Forms.frmStart) return eFormType.frmStart;
      else {  //invalid
        throw new FatalException();
      }
    }

    internal delegate void delSaveFile(StreamWriter xsw);
    internal static string SaveFile(string filepath, delSaveFile savesub) {
      if (filepath =="") return "SaveFile error: Invalid filepath)";

      //* return any error msg
      try {
        using (MemoryStream st = new MemoryStream(3000)) {   //() initial capacity in bytes
          using (StreamWriter xsw = new StreamWriter(st)) {  //memory stream
            savesub(xsw);
            //*throw new TestException();
            xsw.Flush();
            using (FileStream stfile = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.Write)) {
              st.WriteTo(stfile);
            }
          }
        }
      }
      catch (Exception exc) {
        MessageBox.Show("Error saving file " + filepath + ". " + exc.Message);
        return exc.Message;
      }
      return "";
    }

    //internal delegate List<string> delLoadFileLines(StreamReader xsr);
    //internal static string LoadFile(string filepath, delLoadFileLines loadsub, out List<string> lines) {
    //  //* return any error msg
    //  lines = null;
    //  try {
    //    using (StreamReader xsr = new StreamReader(filepath)) {  
    //      lines = loadsub(xsr);
    //    }
    //  }
    //  catch (Exception exc) {
    //    MessageBox.Show("Error loading file " + filepath + ". " + exc.Message);
    //    return exc.Message;
    //  }
    //  if (lines == null) lines = new List<string>(0);
    //  return "";
    //}

    //internal static string LoadFile(string filepath, delLoadFileLines loadsub) {
    //  List<string> lines;
    //  return LoadFile(filepath, loadsub, out lines);
    //}

    //internal static void FormToProps(Form frm) {
    //  //* save form properties in FormProps[]
    //  if (frm == null || !frm.IsHandleCreated) return;
    //  if (frm.WindowState == FormWindowState.Normal) {  //save location and size
    //    FormsProps[(int)GetFormType(frm)] = new clsFormProps(frm.Location, frm.Size);
    //  }
    //}

    //internal static void PropsToForm(Form frm) {
    //  //* load form properties from FormProps[] to open form 
    //  if (frm == null || !frm.IsHandleCreated) return;
    //  if (frm.WindowState != FormWindowState.Normal) return;
    //  clsFormProps props = FormsProps[(int)GetFormType(frm)];
    //  if (props == null) return;  //null if first time or form props not saved 
    //  Rectangle frmrect = new Rectangle(props.Loc, new Size(1, 1));  //1 x 1 rectangle
    //  bool ok = false;
    //  foreach (Screen screen in Screen.AllScreens) {
    //    if (screen.WorkingArea.Contains(frmrect)) {
    //      ok = true;
    //      break;
    //    }
    //  }
    //  if (!ok) return;
    //  frm.Location = props.Loc;
    //  frm.Size = props.Size;
    //}

    internal static List<string> ReadLines(string filename) {
      List<string> lines = new List<string>();
      try {
        using (StreamReader str = new StreamReader(filename)) {
          string line;
          while ((line = str.ReadLine()) != null) {
            lines.Add(line);
          }
        }
      }
      catch (Exception ex) {
        MessageBox.Show("Exception reading " + filename + ": " + ex.Message);
        return null;
      }
      return lines;
    }

    internal static List<string> ReadLinesIgnoreComments(string filename) {  //ignore comments
      //* return null on error
      List<string> comments;
      return ReadLinesWithComments(filename, out comments);
    }

    internal static List<string> ReadLinesWithComments(string filename, out List<string> comments) {
      //* read lines with *comments (position of comments not retained)
      //* return null on error
      List<string> lines = new List<string>();
      comments = new List<string>();
      try {
        using (StreamReader str = new StreamReader(filename)) {
          string line;
          while ((line = str.ReadLine()) != null) {
            line = line.Trim();
            if (line == "#EOF") break;
            if (line == "") continue;
            if (line.StartsWith("*")) comments.Add(line); else lines.Add(line);
            //* if (!filename.ToLower().EndsWith(".dat")) throw new TestException(); 
          }
        }
      }
      catch (Exception ex) {
        MessageBox.Show("Exception reading " + filename + ": " + ex.Message);
        return null;
      }
      return lines;
    }

    static internal string BoolTF(bool zbool) {  //return "T" or "F"
      if (zbool) return "T"; else return "F";
    }

    static internal string BoolYesNo(bool zbool) {  //return "Yes" or "No"
      if (zbool) return "Yes"; else return "No";
    }

    static internal string BoolStar(bool zbool) {  //return "*" or " "
      if (zbool) return "*"; else return " ";
    }

    static internal bool StringYesNo(string zstr) {
      if (zstr.ToLower().Trim() == "yes") return true;
      return false;
    }

    static internal string FixedHex(byte zint, int zlen) {
      //*			slow and dirty way of formatting hex with leading zeros
      string xout;
      xout = Conversion.Hex(zint);
      if (xout.Length < zlen) {
        xout = new string(System.Convert.ToChar("0"), zlen - xout.Length) + xout;
      }
      return xout;
    }

    static internal string FixedHex(int zint, int zlen) {
      //*			slow and dirty way of formatting hex with leading zeros
      string xout;
      xout = Conversion.Hex(zint);
      if (xout.Length < zlen) {
        xout = new string(System.Convert.ToChar("0"), zlen - xout.Length) + xout;
      }
      return xout;
    }

    static internal float GetMean(int[] array) {
      int tot = 0;
      foreach (int val in array) tot += val;
      return tot / array.Length;
    }


    internal static int GetMaxIndex(int[] array) {
      int maxval;
      return GetMaxIndex(array, out maxval);
    }

    internal static int GetMaxIndex(int[] array, out int maxval) {
      //* return index of max value
      int maxkey = -1;
      maxval = -1;
      for (int i = 0; i < array.Length; i++) {
        if (array[i] > maxval) {
          maxkey = i;
          maxval = array[i];
        }
      }
      return maxkey;
    }

    internal static void Throw(Exception exc) {
      //* call to retain ref and identify what needs removing before publishing
      throw exc;
    }

    internal static void ThrowAppExc() {
      //* call to retain ref and identify what needs removing before publishing
      throw new ApplicationException("Test Error");
    }
  }

  public static class clsMod12 {
    //* number 2 1 0 -1 -2
    //* mod    2 1 0 11 10 
    private static int[] Pos = new int[128];  //0 to 127
    private static int[] Neg = new int[128];  //-1 to -128

    static clsMod12() {
      for (int i = 0; i < 128; i++) {
        Pos[i] = i % 12;
        int x = (-i % 12) + 12;
        Neg[i] = (x == 12) ? 0 : x;
      }
    }

    public static int Mod12(this int i) {
      //if (i > 127) i = 127; else if (i < -128) i = -128;
      while (i > 127) i -= 12;
      while (i < -128) i += 12;
      return (i < 0) ? Neg[-i] : Pos[i];
    }
  }

  public static class clsMod7 {
    //* number 2 1 0 -1 -2
    //* mod    2 1 0  6  5 
    private static int[] Pos = new int[64];  //0 to 63
    private static int[] Neg = new int[64];  //-1 to -64

    static clsMod7() {
      for (int i = 0; i < 64; i++) {
        Pos[i] = i % 7;
        int x = (-i % 7) + 7;
        Neg[i] = (x == 7) ? 0 : x;
      }
    }

    public static int Mod7(this int i) {
      //if (i > 127) i = 127; else if (i < -128) i = -128;
      while (i > 63) i -= 7;
      while (i < -64) i += 7;
      return (i < 0) ? Neg[-i] : Pos[i];
    }
  }

  //public static class clsMod7 {
  //  //* slower than Mod12, but simpler
  //  public static int Mod7(this int i) {
  //    while (i < 0) i += 7;
  //    return i % 7;
  //  }
  //}
}
