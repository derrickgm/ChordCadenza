using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using Un4seen.Bass.AddOn.Fx;
using System.ComponentModel;
using System.Windows.Forms;

namespace ChordCadenza {
  internal static class Cfg { //simple replacement for input params
    internal static int LatencyMidiPlay = 0;  //from frmConfigBass nud
    internal static int LatencyKB = 0;  //from frmConfigBass nud
    internal static bool NoIni = false;
    internal static bool Ind;  //called during initialisation
    //internal static Forms.frmSC frmSC;
    //internal static bool ShowInitialForm = true;
    internal static string MidiInKB = "";
    internal static string MidiInSync = "";
    internal static string MidiOutKB = "";
    internal static string MidiOutStream = "";
    internal static int SelectedIndexcmbInKB = -1;
    internal static int SelectedIndexcmbInSync = -1;
    internal static int SelectedIndexcmbOutStream = -1;
    internal static int SelectedIndexcmbOutKB = -1;
    internal static int MidiOutKBFineTuning = 0;
    internal static int MidiStreamFineTuning = 0;
    internal static int MidiCHPTuningAdj = 0;

#if !APPDATAPATH
    internal static readonly string BasePath;
#endif
    internal static readonly string AppPath;
    internal static readonly string AppDataPath;
    internal static readonly string UserMusicPath;
    internal static readonly string CfgPath;
    internal static string ProjectDir; //overridden by .ini setting
    internal static string MidiFilesPath;
    internal static string AudioFilesPath;
    //internal static readonly string SamplesPath;
    //internal static string BezierFilePath;
    internal static string SwitchIniFilePath;
    internal static string FrmSCColoursIniFilePath;
    internal static string FrmNMColoursIniFilePath;
    internal static string FrmTonnetzColoursIniFilePath;
    internal static string SoundFontsPath;
    internal static string ChordNamesDatFilePath;
    internal static string GeneralMidiDatFilePath;
    internal static string ChordNamesRankIniFilePath;
    internal static string InitialScreenDatFilePath;
    internal static string RecentProjectsLines;
    internal static string MainIniFilePath;
    internal static string ChordCfgIniFilePath;
    //internal static string ChordSetsFilePath;
    internal static string DebugPath;
    internal static string HelpFilePath;
    internal static string ToolTipsFilePath;
    internal static string LicenseFilePath;

    internal static string SoundFontStream = "";
    internal static string SoundFontKB = "";
    internal static int DIdd = 32;
    internal static int TPDI = 0;
    internal static Forms.frmChordMap.eSnapTo frmChordMap_SnapTo = Forms.frmChordMap.eSnapTo.Bar;

    internal static string[] BezierName = new string[2];
    internal static int[] Bezier1X = new int[2];
    internal static int[] Bezier1Y = new int[2];
    internal static int[] Bezier2X = new int[2];
    internal static int[] Bezier2Y = new int[2];
    internal const int BezierVel = 0;
    internal const int BezierATouch = 1;
    internal static Point frmShowChordsLoc = new Point(0, 0);
    internal static Size frmShowChordsSize = new Size(1226, 618);  //from VS Designer

    internal static int TrkType_SparsePercent = 10;
    internal static float TrkType_PolyChord = 1.50f;
    internal static float TrkType_ChordNotes = 1.5f;
    internal static int TrkType_MaxPitchBass = 48;
    internal static int TrkType_MaxPitchBassPatched = 60;

    internal static bool LoadMMInitial = false;
    internal static bool ShowPercussionInitial = false;
    //internal static bool HideStartInitial = false;

    internal static bool[] Patches_SustainCarryOver = new bool[128];
    internal static bool[] Patches_SustainReplay = new bool[128];

    internal static int OctTransposeKBPitch = 0;
    private static bool NoAsioDevName = true;
    private static bool NoNonAsioDevName = true;
    internal static int KBPatchSelectedIndex = 0;  //only used on program start
    internal static int KBOutChan = 0;  //only used on program start
    //internal static Forms.frmSC.ePlayMode InitialMode = Forms.frmSC.ePlayMode.KB;

    internal static Dictionary<string, clsFormProps> DictFormProps = new Dictionary<string, clsFormProps>(16);

    static Cfg() {

      AppPath = Application.StartupPath;  //D:\D2\Dev\CS.Express\ChordCadenza\ChordCadenza\bin\Debug
      AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

//#if !APPDATAPATH
//      string basepath = "";  //exclude last dir
//      string[] dirs = AppPath.Split(new string[] { "\\" }, StringSplitOptions.None);
//      for (int i = 0; i < dirs.Length - 1; i++) {  //exclude last dir (Debug, Release, Debug X64, or Release X64)
//        basepath += dirs[i] + "\\";
//      }
//      CfgPath = basepath + "Cfg";   //D:\D2\Dev\CS.Express\ChordCadenza\ChordCadenza\bin\ + Cfg
//      UserMusicPath = @"D:\D1\Sonar\Songs";
//      SoundFontsPath = @"C:\SoundFonts";
//#else
//      CfgPath = AppDataPath + "\\ChordCadenza";  //C:\Users\Derrick\AppData\Roaming\ChordCadenza
//      UserMusicPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
//      SoundFontsPath = UserMusicPath + @"\SoundFonts";
//#endif

      #if !APPDATAPATH
        BasePath = ""; 
        string[] dirs = AppPath.Split(new string[] { "\\" }, StringSplitOptions.None);
        //* exclude last 2 dirs (bin\Debug, bin\Release, bin\Debug X64, bin\Release X64)
        for (int i = 0; i < dirs.Length - 2; i++) {  
          BasePath += dirs[i] + "\\";
        }
        CfgPath = BasePath + "Cfg";   //D:\D2\Dev\CS.Express\ChordCadenza\ChordCadenza\ + Cfg
        UserMusicPath = @"D:\D1\Sonar\Songs";
        SoundFontsPath = @"C:\SoundFonts";
      #else
        CfgPath = AppDataPath + "\\ChordCadenza";  //C:\Users\Derrick\AppData\Roaming\ChordCadenza
        UserMusicPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        SoundFontsPath = UserMusicPath + @"\SoundFonts";
      #endif

      ProjectDir = UserMusicPath + @"\ChordCadenza Projects"; //overridden by .ini setting
      //MidiFilesPath = UserMusicPath + @"\Midi Files"; //overridden by .ini setting
      //AudioFilesPath = UserMusicPath + @"\Audio Files"; //overridden by .ini setting
      MidiFilesPath = UserMusicPath; //overridden by .ini setting
      AudioFilesPath = UserMusicPath; //overridden by .ini setting
      //DebugPath = CfgPath + @"\DumpChords";
      DebugPath = Path.GetTempPath() + @"\DumpChords";
      HelpFilePath = CfgPath + "\\ChordCadenza.chm";
      ToolTipsFilePath = CfgPath + "\\ToolTips.html";
      LicenseFilePath = CfgPath + "\\License.txt";
      ChordNamesDatFilePath = CfgPath + @"\Chords.dat";  //always present
      GeneralMidiDatFilePath = CfgPath + @"\GeneralMidi.dat";  //always present
      ChordNamesRankIniFilePath = CfgPath + @"\Chords.Ranks.ini";  
      InitialScreenDatFilePath = CfgPath + @"\InitialScreen.dat";  //present only if initial screen wanted
      RecentProjectsLines = CfgPath + @"\RecentProjects.ini";
      MainIniFilePath = CfgPath + @"\ChordCadenza.ini";
      SwitchIniFilePath = CfgPath + @"\Switch.ini";
      FrmSCColoursIniFilePath = CfgPath + @"\FrmSCColours.ini";
      FrmNMColoursIniFilePath = CfgPath + @"\FrmNMColours.ini";
      FrmTonnetzColoursIniFilePath = CfgPath + @"\FrmTonnetzColours.ini";
      ChordCfgIniFilePath = CfgPath + @"\ChordCfg.ini";
      //SamplesPath = CfgPath + @"\Music Files\Samples\HowTo";  

      BezierName[0] = "Velocity";
      BezierName[1] = "Aftertouch";
      Forms.frmCfgBezier.SetCfgDefaults();

      ReadIni();
      PostReadIni();

      //if (ret != "") {
      //  throw new CfgFileException(CfgPath + @"\ChordCadenza.Ini" + " file not found");
      //  //System.Windows.Forms.MessageBox.Show("Ini file IO error: "
      //  //  + "\n" + ret);
      //  //Environment.Exit(1);
      //}

      P.ColorsShowChords = new clsColorsShowChords();
      P.ColorsNoteMap = new clsColorsNoteMap();
      P.ColorsTonnetz = new clsColorsTonnetz();

      Patches_SustainCarryOver[44] = true;  //tremolo strings
      Patches_SustainCarryOver[48] = true;  //string ensemble 1
      Patches_SustainCarryOver[49] = true;  //string ensemble 2
      Patches_SustainCarryOver[50] = true;  //synthstrings1
      Patches_SustainCarryOver[51] = true;  //synthstrings2
      Patches_SustainCarryOver[52] = true;  //choir aahs
      Patches_SustainCarryOver[53] = true;  //voice oohs
    }

    /* not yet used
    private static bool CreateDirectory(string dir) {
      if (Directory.Exists(dir)) return true;
      try {
        Directory.CreateDirectory(dir);
      }
      catch (Exception exc) {
        MessageBox.Show("Create Folder " + dir + " failed: " + exc.Message);
        return false;
      }
      return true;
    }
    */

    private static void PostReadIni() {
      //* throw new ApplicationException("Just testing");
      if (MidiInKB == "") MidiInKB = "***";  //default

      if (NoAsioDevName) clsBASSOutDevAsio.DevName = "*";  //assume this was meant
      if (NoNonAsioDevName) clsBASSOutDevNonAsio.DevName = "*";  //assume this was meant

      if (!clsBASSOutDevNonAsio.DevsExist) clsBASSOutDev.indAsio = true;
      if (!clsBASSOutDevAsio.DevsExist) clsBASSOutDev.indAsio = false;

      //* out default: try using BuiltIn Synth with soundfont
      if (MidiOutKB == "" || MidiOutStream == "") {
        try {
          Forms.frmConsole.WriteLine("PostReadIni: Pre: MidiOutKB = <" + MidiOutKB + "> MidiOutStream = <" + MidiOutStream + ">");
          string[] soundfontfiles = Directory.GetFiles(SoundFontsPath, "*.sf2");
          if (soundfontfiles.Length > 0) {
            if (MidiOutKB == "") {
              if (SoundFontKB == "") SoundFontKB = Path.GetFileNameWithoutExtension(soundfontfiles[0]);
              Forms.frmConsole.WriteLine("PostReadIni: SoundFontKB =  " + SoundFontKB);
              if (DirContainsSoundFont(soundfontfiles, SoundFontKB)) MidiOutKB = "BuiltIn Synth (KB)";

            }
            if (MidiOutStream == "") {
              if (SoundFontStream == "") SoundFontStream = Path.GetFileNameWithoutExtension(soundfontfiles[0]);
              Forms.frmConsole.WriteLine("PostReadIni: SoundFontStream =  " + SoundFontStream);
              if (DirContainsSoundFont(soundfontfiles, SoundFontStream)) MidiOutStream = "BuiltIn Synth (Stream)";
            }
          Forms.frmConsole.WriteLine("PostReadIni: Post: MidiOutKB = <" + MidiOutKB + "> MidiOutStream = <" + MidiOutStream + ">");
          }
        }
        catch {}  //no soundfonts - skip over
      }

      for (int i = 0; i < clsBassOutMidi.Freeverb.Length; i++) {
        if (clsBassOutMidi.Freeverb[i] == null) ReadFX("False, 0, 0.99, 0.5, 0.5, 1", i);
      }

      //* out default if no soundfont: try using first midi out device
      if (MidiOutKB == "") MidiOutKB = "***";
      if (MidiOutStream == "") MidiOutStream = "***";

      if (!DictFormProps.ContainsKey("frmSC"))
        DictFormProps.Add("frmSC", clsFormProps.Parse("frmSC, False, 25, 25, 1390, 650"));
      if (!DictFormProps.ContainsKey("frmStart"))
        DictFormProps.Add("frmStart", clsFormProps.Parse("frmStart, False, 25, 25, 820, 250"));
      if (!DictFormProps.ContainsKey("frmTonnetz"))
        DictFormProps.Add("frmTonnetz", clsFormProps.Parse("frmTonnetz, False, 25, 25, 876, 390"));
      if (!DictFormProps.ContainsKey("frmAutoSync"))
        DictFormProps.Add("frmAutoSync", clsFormProps.Parse("frmAutoSync, False, 25, 25, 840, 663"));
      if (!DictFormProps.ContainsKey("frmChordMap"))
        DictFormProps.Add("frmChordMap", clsFormProps.Parse("frmChordMap, False, 25, 25, 1287, 922"));
      if (!DictFormProps.ContainsKey("frmTrackMap"))
        DictFormProps.Add("frmTrackMap", clsFormProps.Parse("frmTrackMap, False, 25, 25, 1443, 780"));

      //clsPlay.SetManChordSyncOpts();
    }

    //internal static void ReadIniLine(ref string var, string instr, string msg) {
    //  string s = instr;
    //  if (P.Advanced && s != var) {
    //    MessageBox.Show("Warning: Cfg: " + msg + " Inconsistent");
    //  }
    //  var = s;
    //}

    //internal static void ReadIniLine(ref bool var, string instr, string msg) {
    //  bool b = bool.Parse(instr);
    //  if (P.Advanced && b != var) {
    //    MessageBox.Show("Warning: Cfg: " + msg + " Inconsistent");
    //  }
    //  var = b;
    //}

    //internal static void ReadIniLine(CheckBox chk, string instr) {
    //  bool b = bool.Parse(instr);
    //  if (P.Advanced && b != chk.Checked) {
    //    MessageBox.Show("Warning: Cfg: " + chk.Name + " Inconsistent");
    //  }
    //  chk.Checked = b;
    //}

    //internal static void ReadIniLine(ref int var, string instr, string msg) {
    //  int i = int.Parse(instr);
    //  if (P.Advanced && i != var) {
    //    MessageBox.Show("Warning: Cfg: " + msg + " Inconsistent");
    //  }
    //  var = i;
    //}

    //internal static void ReadIniLine(ref float var, string instr, string msg) {
    //  float f = float.Parse(instr);
    //  if (P.Advanced && f != var) {
    //    MessageBox.Show("Warning: Cfg: " + msg + " Inconsistent");
    //  }
    //  var = f;
    //}

    //internal static void ReadIniLine(NumericUpDown nud, string instr) {
    //  int i = int.Parse(instr);
    //  if (P.Advanced && i != (int)nud.Value) {
    //    MessageBox.Show("Warning: Cfg: " + nud.Name + " Inconsistent");
    //  }
    //  nud.Value = i;
    //}

    //private static string ReadIni() {
    //  if (P.IgnoreIni) return "";
    //  return Utils.LoadFile(MainIniFilePath, LoadFileSub);
    //}

    private static void ReadIni() {
      if (!File.Exists(MainIniFilePath)
      && !File.Exists(FrmNMColoursIniFilePath)
      && !File.Exists(FrmSCColoursIniFilePath)
      //&& !File.Exists(FrmTonnetzColoursIniFilePath)
      && !File.Exists(SwitchIniFilePath)) {
        Debug.WriteLine("No Ini Files - Default values will be used");
        NoIni = true;
        return;
      }
      //if (P.IgnoreIni) return;
      List<string> lines = Utils.ReadLines(MainIniFilePath);
      if (lines == null) return;
      char[] delimcomma = new char[] { ',' };
      char[] delimequality = new char[] { '=' };
      string[] f = new string[] { "???" };
      try {
        foreach (string l in lines) {
          char[] backslash = new char[] { '\\' };
          string line = l.Trim();
          if (line.StartsWith("*")) continue;  //comment
          if (line.Length == 0) continue;
          f = line.Split(delimequality, 2);  //max 2 substrings (1 delimiter)
          for (int i = 0; i < f.Length; i++) f[i] = f[i].Trim();
          switch (f[0]) {
            case "MidiInKB":
              MidiInKB = f[1];
              break;
            case "MidiInSync":
              MidiInSync = f[1];
              break;
            case "MidiOutKB":
              MidiOutKB = f[1];
              break;
            case "MidiOutStream":
              MidiOutStream = f[1];
              break;
            case "SelectedIndexcmbInKB":
              SelectedIndexcmbInKB = int.Parse(f[1]);
              break;
            case "SelectedIndexcmbInSync":
              SelectedIndexcmbInSync = int.Parse(f[1]);
              break;
            case "SelectedIndexcmbOutKB":
              SelectedIndexcmbOutKB = int.Parse(f[1]);
              break;
            case "SelectedIndexcmbOutStream":
              SelectedIndexcmbOutStream = int.Parse(f[1]);
              break;
            case "KBOutChan":
              //MidiPlay.KBOutChan = int.Parse(f[1]) - 1; break;  //conv 1-16 to 0-15
              Cfg.KBOutChan = int.Parse(f[1]); break;
            case "ProjectPath":
              if (f[1] == "*" || f[1] == "") break;
              ProjectDir = f[1].TrimEnd(backslash); break;
            case "MidiFilesPath":
              if (f[1] == "*" || f[1] == "") break;
              MidiFilesPath = f[1].TrimEnd(backslash); break;
            case "AudioFilesPath":
              if (f[1] == "*" || f[1] == "") break;
              AudioFilesPath =  f[1].TrimEnd(backslash); break;
            case "SoundFontsPath":
              if (f[1] == "*" || f[1] == "") break;
              SoundFontsPath = f[1].TrimEnd(backslash); break;
            case "AudioDisconnected":
              //ReadIniLine(ref clsBASSOutDev.Disconnected, f[1], "AudioDisconnected");
              clsBASSOutDev.Disconnected = bool.Parse(f[1]);
              break;
            case "indAsio":
              clsBASSOutDev.indAsio = Utils.StringYesNo(f[1]); break;
            case "AsioDevName":
              if (f[1].Length > 0) NoAsioDevName = false;
              clsBASSOutDevAsio.DevName = f[1]; break;
            case "NonAsioDevName":
              if (f[1].Length > 0) NoNonAsioDevName = false;
              clsBASSOutDevNonAsio.DevName = f[1]; break;
            case "NonAsioBuffer":
              clsBASSOutDevNonAsio.BufferSize = int.Parse(f[1]); break;
            case "NonAsioUpdatePeriod":
              clsBASSOutDevNonAsio.UpdatePeriod = int.Parse(f[1]); break;
            case "SoundFontStream":
              SoundFontStream = f[1]; break;
            case "SoundFontKB":
              SoundFontKB = f[1]; break;
            case "DIdd":
              DIdd = int.Parse(f[1]);
              break;
            case "TPDI":
              TPDI = int.Parse(f[1]);
              break;
            //case "PIdd":
            //  PIdd = int.Parse(f[1]);
            //  break;
            //case "TPPI":
            //  TPPI = int.Parse(f[1]);
            //  break;
            //case "frmShowChordsProps":
            //  ReadFormProps(f, Utils.eFormType.frmSC);
            //  break;
            //case "frmStartProps":
            //  ReadFormProps(f, Utils.eFormType.frmStart);
            //  break;
            case "frmSCProps":
              DictFormProps.Add("frmSC", clsFormProps.Parse(f[1]));
              break;
            case "frmStartProps":
              DictFormProps.Add("frmStart", clsFormProps.Parse(f[1]));
              break;
            case "frmTonnetzProps":
              DictFormProps.Add("frmTonnetz", clsFormProps.Parse(f[1]));
              break;
            case "frmAutoSyncProps":
              DictFormProps.Add("frmAutoSync", clsFormProps.Parse(f[1]));
              break;
            case "frmChordMapProps":
              DictFormProps.Add("frmChordMap", clsFormProps.Parse(f[1]));
              break;
            case "frmTrackMapProps":
              DictFormProps.Add("frmTrackMap", clsFormProps.Parse(f[1]));
              break;
            case "Ranges":
              Forms.frmSC.ReadSettings(f);
              break;
            case "TrkType_SparsePercent":
              TrkType_SparsePercent = int.Parse(f[1]);
              break;
            case "TrkType_MaxPitchBass":
              TrkType_MaxPitchBass = int.Parse(f[1]);
              break;
            case "TrkType_MaxPitchBassPatched":
              TrkType_MaxPitchBassPatched = int.Parse(f[1]);
              break;
            case "TrkType_PolyChord":
              TrkType_PolyChord = float.Parse(f[1]);
              break;
            case "TrkType_ChordNotes":
              TrkType_ChordNotes = float.Parse(f[1]);
              break;
            case "LoadMM":
              LoadMMInitial = bool.Parse(f[1]);
              break;
            case "ShowPercussion":
              ShowPercussionInitial = bool.Parse(f[1]);
              break;
            case "ShowRelChords":
              P.frmSC_Temp.chkShowChordsRel.Checked = bool.Parse(f[1]);
              break;
            case "AllNotesOffAfterSustain":
              P.frmStart.chkAllNotesOffAfterSustain.Checked = bool.Parse(f[1]);
              break;
            case "DelaySustainOff":
              P.frmStart.chkDelaySustain.Checked = bool.Parse(f[1]);
              break;
            case "IgnoreNullChords":
              P.frmStart.chkIgnoreNullChords.Checked = bool.Parse(f[1]);
              break;
            case "KBChordMatch":
              P.frmStart.chkKBChordMatch.Checked = bool.Parse(f[1]);
              break;
            //case "ChunksPerQNote":
            //  //*throw new TestException();
            //  int chunks = int.Parse(f[1]);
            //  Forms.frmStart.SetNudAndTag(P.frmStart.nudChunksPerQNote, chunks);
            //  break;
            //case "MinChordSize":
            //  P.frmStart.nudMinChordSize.Value = int.Parse(f[1]);
            //  break;
            //case "MaxChordSize":
            //  P.frmStart.nudMaxChordSize.Value = int.Parse(f[1]);
            //  break;
            case "TimerSustain":
              P.frmStart.nudTimerSustain.Value = int.Parse(f[1]);
              break;
            case "StreamResolution":
              P.frmStart.nudPlayResolution.Value = int.Parse(f[1]);
              break;
            case "NotesPerChordDisplay":
              P.frmStart.nudNotesPerChordDisplay.Value = int.Parse(f[1]);
              break;
            case "NotesPerChordPlay":
              P.frmStart.nudNotesPerChordPlay.Value = int.Parse(f[1]);
              break;
            case "ConstantChordDisplay":
              P.frmStart.chkConstantChordDisplay.Checked = bool.Parse(f[1]);
              break;
            case "ConstantChordPlay":
              P.frmStart.chkConstantChordPlay.Checked = bool.Parse(f[1]);
              break;
            case "Syncopation":  //Syncopation, NN, DD
              {
                string[] strsync = f[1].Split(new char[] { ',' });
                //int nn = int.Parse(strsync[0]);
                //int dd = int.Parse(strsync[1]);
                int nn = (int)P.frmStart.nudSyncopationNN.Value;
                int dd = (int)P.frmStart.nudSyncopationDD.Value;
                nn = int.Parse(strsync[0]);
                dd = int.Parse(strsync[1]);
                P.frmStart.Syncopation = new clsNNDD(nn, dd);  //should also be set by nud event
                P.frmStart.nudSyncopationNN.Value = nn;
                Forms.frmStart.SetNudAndTag(P.frmStart.nudSyncopationDD, dd);
                break;
              }
            case "AutoCapitalize":
              //bool autocap = bool.Parse(f[1]);
              //ReadIniLine(ref Forms.frmSC.AutoCapitalizeStatic, f[1], "AutoCapitalize");
              Forms.frmSC.AutoCapitalizeStatic = bool.Parse(f[1]);
              Forms.frmSC.CapitalizeRootsStatic = !Forms.frmSC.AutoCapitalizeStatic;
              break;
            case "CapitalizeRoots":
              Forms.frmSC.CapitalizeRootsStatic = true;
              Forms.frmSC.CapitalizeRootsStatic = bool.Parse(f[1]);
              break;
            case "SustainAuto":
              P.frmSC_Temp.chkSustainAuto.Checked = bool.Parse(f[1]);
              break;
            case "SustainAction":
              P.frmSC_Temp.optSustainNormal.Checked = false;
              P.frmSC_Temp.optSustainCarryOver.Checked = false;
              P.frmSC_Temp.optSustainReplay.Checked = false;
              P.frmSC_Temp.optSustainSendCtlr.Checked = false;
              switch (f[1]) {
                case "Replay":
                  P.frmSC_Temp.optSustainReplay.Checked = true;
                  break;
                case "CarryOver":
                  P.frmSC_Temp.optSustainCarryOver.Checked = true;
                  break;
                case "SendCtlr":
                  P.frmSC_Temp.optSustainSendCtlr.Checked = true;
                  break;
                case "Normal":
                  P.frmSC_Temp.optSustainNormal.Checked = true;
                  break;
                default:
                  P.frmSC_Temp.optSustainNormal.Checked = true;
                  break;
              }
              break;
            case "FXKB":
              ReadFX(f[1], (int)clsBassOutMidi.eOutType.KB);
              break;
            case "FXStream":
              ReadFX(f[1], (int)clsBassOutMidi.eOutType.Stream);
              break;
            //case "frmMMBarLabels":
            //  Forms.frmTrackMap.BarLabels = int.Parse(f[1]);
            //  break;
            //case "frmNMSnapTo":
            //  Forms.frmChordMap.SnapTo = f[1];
            //  break;
            //case "frmNMSnapNote":
            //  Forms.frmChordMap.SnapNote = bool.Parse(f[1]);
            //  break;
            //case "frmNMSnapTime":
            //  Forms.frmChordMap.SnapTime = bool.Parse(f[1]);
            //  break;
            case "frmNMSnapTo":
              frmChordMap_SnapTo = (Forms.frmChordMap.eSnapTo)Enum.Parse(typeof(Forms.frmChordMap.eSnapTo), f[1]);
              break;
            case "frmNMShowBeats":
              Forms.frmChordMap.CfgShowBeats = bool.Parse(f[1]);
              break;
            case "frmNMShowKB":
              Forms.frmChordMap.CfgShowKB = bool.Parse(f[1]);
              break;
            case "frmNMOneOctave":
              Forms.frmChordMap.CfgOneOctave = bool.Parse(f[1]);
              break;
            case "frmNMVPixPerNote":
              Forms.frmChordMap.CfgVPixPerNote = int.Parse(f[1]);
              break;
            case "frmSCSolfaFont":
              TypeConverter convsf = TypeDescriptor.GetConverter(typeof(Font));
              Forms.frmSC.MainFont = (Font)convsf.ConvertFromString(f[1]);
              break;
            case "frmSCChordFont":
              TypeConverter convchord = TypeDescriptor.GetConverter(typeof(Font));
              Forms.frmSC.ChordFont = (Font)convchord.ConvertFromString(f[1]);
              break;
            case "frmSCChordColor":
              Forms.frmSC.ChordColor = Color.FromArgb(Convert.ToInt32(f[1].Trim(), 16));
              break;
            //case "frmSCSolfaFontReduction":
            //  Forms.frmSC.valFontReduction = int.Parse(f[1]);
            //  break;
            case "AutoRecChan":
              P.frmStart.chkAutoRecChan.Checked = bool.Parse(f[1]);
              break;
            //case "PlaySustain":
            //  P.frmStart.chkPlaySustain.Checked = bool.Parse(f[1]);
            //  break;
            //case "PlayKBChord":
            //  P.frmStart.chkPlayKBChord.Checked = bool.Parse(f[1]);
            //  break;
            case "PCKB":
              P.frmStart.chkPCKB.Checked = bool.Parse(f[1]);
              break;
            //case "AutoSyncMerge":
            //  P.frmStart.chkAutoSyncMerge.Checked = bool.Parse(f[1]);
            //  break;
            //case "SyncAudioPlaySync":
            //  P.frmStart.chkSyncAudioPlaySync.Checked = bool.Parse(f[1]);
            //  break;
            case "OctTransposeKBPitch":
              OctTransposeKBPitch = int.Parse(f[1]);
              break;
            //case "NextEvent":
            //  if (f[1] == "Chord") P.frmStart.optLiveNextChord.Checked = true;
            //  else P.frmStart.optLiveNextBeat.Checked = true;
            //  break;
            case "BezierVelocity":
              string[] bezvel = f[1].Split(new char[] { ',' });
              Bezier1X[0] = int.Parse(bezvel[0]);
              Bezier1Y[0] = int.Parse(bezvel[1]);
              Bezier2X[0] = int.Parse(bezvel[2]);
              Bezier2Y[0] = int.Parse(bezvel[3]);
              break;
            case "BezierAftertouch":
              string[] bezatouch = f[1].Split(new char[] { ',' });
              Bezier1X[1] = int.Parse(bezatouch[0]);
              Bezier1Y[1] = int.Parse(bezatouch[1]);
              Bezier2X[1] = int.Parse(bezatouch[2]);
              Bezier2Y[1] = int.Parse(bezatouch[3]);
              break;
            case "ShowNote":
              P.frmSC_Temp.optShowNoteName.Checked = false;
              P.frmSC_Temp.optShowSolfa.Checked = false;
              P.frmSC_Temp.optShowNone.Checked = false;
              P.frmSC_Temp.optShowPCKBChar.Checked = false;
              if (f[1] == "Solfa") P.frmSC_Temp.optShowSolfa.Checked = true; 
              else if (f[1] == "NoteName") P.frmSC_Temp.optShowNoteName.Checked = true;  
              else if (f[1] == "KBName") P.frmSC_Temp.optShowPCKBChar.Checked = true;  
              else P.frmSC_Temp.optShowNone.Checked = true;  
              break;
            case "RunChordNotes":
              P.frmSC_Temp.chkRunChordNotes.Checked = bool.Parse(f[1]);  //FO
              break;
            case "SaveMutedTrks":
              P.frmSaveMidiFileAs.chkSaveMutedTrks.Checked = bool.Parse(f[1]);
              break;
            //case "SaveEmptyTrks":
            //  P.frmSaveMidiFileAs.chkSaveEmptyTrks.Checked = bool.Parse(f[1]);
            //  break;
            case "SaveChordLabels":
              P.frmSaveMidiFileAs.chkSaveChordLabels.Checked = bool.Parse(f[1]);
              break;
            case "SaveChordTrack":
              P.frmSaveMidiFileAs.chkSaveChordTrack.Checked = bool.Parse(f[1]);
              break;
            //case "SaveRecTrack":
            //  P.frmSaveMidiFileAs.chkSaveRecTrk.Checked = bool.Parse(f[1]);
            //  break;
            case "SaveMidiFileChordRoot":
              P.frmSaveMidiFileAs.chkChordRoot.Checked = bool.Parse(f[1]);
              break;
            case "SaveMidiFileChordChan":
              P.frmSaveMidiFileAs.nudChordChan.Value = int.Parse(f[1]);
              break;
            case "ToolTipsActive":
              P.frmStart.chkTTActive.Checked = bool.Parse(f[1]);
              break;
            case "VolAudio":
              P.frmSC_Temp.trkAudioVol.Value = int.Parse(f[1]);
              break;
            case "PanAudio":
              P.frmSC_Temp.trkAudioPan.Value = int.Parse(f[1]);
              break;
            case "VolKB":
              P.frmSC_Temp.trkKBVol.Value = int.Parse(f[1]);
              break;
            case "PanKB":
              P.frmSC_Temp.trkKBPan.Value = int.Parse(f[1]);
              break;
            case "VolStream":
              P.frmSC_Temp.trkStreamVol.Value = int.Parse(f[1]);
              break;
            case "PanStream":
              P.frmSC_Temp.trkStreamPan.Value = int.Parse(f[1]);
              break;
            case "VolKBChan":
              P.frmSC_Temp.trkKBChanVol.Value = int.Parse(f[1]);
              break;
            case "PanKBChan":
              P.frmSC_Temp.trkKBChanPan.Value = int.Parse(f[1]);
              break;
            case "MidiOutKBFineTuning":
              Cfg.MidiOutKBFineTuning = int.Parse(f[1]);
              break;
            case "MidiStreamFineTuning":
              Cfg.MidiStreamFineTuning = int.Parse(f[1]);
              break;
            case "KBPatch":
              Cfg.KBPatchSelectedIndex = int.Parse(f[1]); //0 - 128 (0="None")
              break;
            case "TonnetzHalfSpace":
              Forms.frmTonnetz.HalfSpace = int.Parse(f[1]);
              break;
            //case "TonnetzEmerging":
            //  Forms.frmTonnetz.ClickToEmerging = bool.Parse(f[1]);
            //  break;
            case "TonnetzLookAhead":
              Forms.frmTonnetz.LookAhead = int.Parse(f[1]);
              break;
            case "TonnetzCirclePenWidth":
              Forms.frmTonnetz.CirclePenWidth = int.Parse(f[1]);
              break;
            case "TonnetzDiameter":
              Forms.frmTonnetz.Diameter = int.Parse(f[1]);
              break;
            case "ExitPrompt":
              P.frmStart.chkExitPrompt.Checked = bool.Parse(f[1]);
              break;
            case "NoSync":
              P.frmStart.chkNoAudioSync.Checked = bool.Parse(f[1]);
              break;
            case "DefaultSongLength":
              P.frmStart.nudDefaultSongLength.Value = int.Parse(f[1]);
              break;
            case "UndoRedoCapacity":
              P.frmStart.nudUndoRedoCapacity.Value = int.Parse(f[1]);
              break;
            case "LatencyMidiPlay":
              LatencyMidiPlay = int.Parse(f[1]);
              break;
            case "LatencyKB":
              LatencyKB = int.Parse(f[1]);
              break;
            case "frmSCScroll":
              P.frmSC_Temp.chkScroll.Checked = bool.Parse(f[1]);
              break;
            //case "BlackNext":
            //  P.frmStart.chkNextBlack.Checked = bool.Parse(f[1]);
            //  break;
            //case "Sync_Disable":
            //  Forms.frmManChordSync.Disabled = bool.Parse(f[1]);
            //  break;
            //case "Sync_NoSkipAfterReloc":
            //  clsPlay.NoSkipAfterReloc = bool.Parse(f[1]);
            //  break;
            case "Sync_indNextBlack":
              clsPlay.indNextBlack = bool.Parse(f[1]);
              break;
            case "Sync_indNextWhite":
              clsPlay.indNextWhite = bool.Parse(f[1]);
              break;
            case "Sync_SingleBlackAction":
              clsPlay.SingleBlackAction = 
                (clsPlay.eManSyncAction)Enum.Parse(typeof(clsPlay.eManSyncAction), f[1]);
              break;
            case "Sync_SingleWhiteAction":
              clsPlay.SingleWhiteAction = 
                (clsPlay.eManSyncAction)Enum.Parse(typeof(clsPlay.eManSyncAction), f[1]);
              break;
            //case "PlayMode":
            //  InitialMode = (Forms.frmSC.ePlayMode)Enum.Parse(typeof(Forms.frmSC.ePlayMode), f[1]);
            //  break;
            case "SCShowBeats":
              P.frmSC_Temp.Bypass_Event = true;
              P.frmSC_Temp.chkShowBeats.Checked = bool.Parse(f[1]);
              P.frmSC_Temp.Bypass_Event = false;
              break;
            case "SCShowChordNames":
              P.frmSC_Temp.Bypass_Event = true;
              P.frmSC_Temp.chkShowChordNames.Checked = bool.Parse(f[1]);
              P.frmSC_Temp.Bypass_Event = false;
              break;
            case "SCShowChordNotes":
              P.frmSC_Temp.Bypass_Event = true;
              P.frmSC_Temp.chkShowChordNotes.Checked = bool.Parse(f[1]);
              P.frmSC_Temp.cmbFirstNote.Enabled = P.frmSC_Temp.chkShowChordNotes.Checked;
              P.frmSC_Temp.Bypass_Event = false;
              break;
            case "SCShowTracks":
              P.frmSC_Temp.Bypass_Event = true;
              P.frmSC_Temp.chkShowTracks.Checked = bool.Parse(f[1]);
              P.frmSC_Temp.Bypass_Event = false;
              break;
            case "SCShowChords":
              P.frmSC_Temp.Bypass_Event = true;
              P.frmSC_Temp.chkShowChords.Checked = bool.Parse(f[1]);
              P.frmSC_Temp.Bypass_Event = false;
              break;
            case "SCFirstNote":
              P.frmSC_Temp.Bypass_Event = true;
              P.frmSC_Temp.cmbFirstNote.SelectedItem = f[1];
              P.frmSC_Temp.Bypass_Event = false;
              break;
            default:
              if (Forms.frmTrackMap.IniToStatic(f)) break;
#if DEBUG
              throw new CfgFileException("Parameter " + f[0] + " unrecognised");
#else
              break;
#endif
          }  //switch
        }
        Debug.WriteLine(MainIniFilePath + " File loaded");
      }
      catch (Exception exc) {
        MessageBox.Show(exc.Message + " whilst reading Ini parameter: " + f[0]);
      }
    }

    private static void ReadFX(string f, int type) {
      //* FreeverbKB|FreeverbStream = true|false, drymix, wetmix, roomsize, damp, width
      string[] ff = f.Split(new char[] { ',' });
      clsBassOutMidi.indFreeverb[type] = bool.Parse(ff[0]);
      float drymix = float.Parse(ff[1]);
      float wetmix = float.Parse(ff[2]);
      float roomsize = float.Parse(ff[3]);
      float damp = float.Parse(ff[4]);
      float width = float.Parse(ff[5]);
      clsBassOutMidi.Freeverb[type] = new BASS_BFX_FREEVERB(drymix, wetmix, roomsize, damp, width, 0);
    }

    private static bool DirContainsSoundFont(string[] files, string font) {
      foreach (string path in files) {
        if (Path.GetFileNameWithoutExtension(path) == font) return true;
      }
      return false;
    }

    //private static void ReadFormProps(string[] f, Utils.eFormType formtype) {
    //  string[] ff = f[1].Split(new char[] { ',' });
    //  Point loc = new Point(int.Parse(ff[0]), int.Parse(ff[1]));
    //  Size size = new Size(int.Parse(ff[2]), int.Parse(ff[3]));
    //  Utils.FormsProps[(int)formtype] = new Utils.clsFormProps(loc, size);
    //}

    internal static string WriteIniFile() {
      return Utils.SaveFile(MainIniFilePath, SaveFileSub);
    }

    private static void WriteLine(StreamWriter sw, string txt) {
      sw.WriteLine(txt);
    }

    private static void WriteMidiDev(StreamWriter sw, clsBassMidiInOut dev, string key, string devname) {
      if (dev == null || dev is clsBassMidiOutNull || !dev.Opened()) WriteLine(sw, key + "None");
      else WriteLine(sw, key + devname);
    }

    private static void SaveFileSub(StreamWriter sw) {
      WriteMidiDev(sw, MidiPlay.MidiInKB, "MidiInKB = ", MidiInKB);
      WriteMidiDev(sw, MidiPlay.MidiInSync, "MidiInSync = ", MidiInSync);
      WriteMidiDev(sw, (clsBassMidiInOut)MidiPlay.OutMKB, "MidiOutKB = ", MidiOutKB);
      WriteMidiDev(sw, (clsBassMidiInOut)MidiPlay.OutMStream, "MidiOutStream = ", MidiOutStream);

      if (MidiPlay.MidiInKB == null || !MidiPlay.MidiInKB.Opened()) {
        WriteLine(sw, "SelectedIndexcmbInKB = " + SelectedIndexcmbInKB);
      }
      if (MidiPlay.MidiInSync == null || !MidiPlay.MidiInSync.Opened()) {
        WriteLine(sw, "SelectedIndexcmbInSync = " + SelectedIndexcmbInSync);
      }
      if (MidiPlay.OutMKB == null || !MidiPlay.OutMKB.Opened()) {
        WriteLine(sw, "SelectedIndexcmbOutKB = " + SelectedIndexcmbOutKB);
      }
      if (MidiPlay.OutMStream == null || !MidiPlay.OutMStream.Opened()) {
        WriteLine(sw, "SelectedIndexcmbOutStream = " + SelectedIndexcmbOutStream);
      }

      WriteLine(sw, "KBOutChan = " + MidiPlay.KBOutChan); 
      WriteLine(sw, "ProjectPath = " + ProjectDir);
      WriteLine(sw, "MidiFilesPath = " + MidiFilesPath);
      WriteLine(sw, "AudioFilesPath = " + AudioFilesPath);
      WriteLine(sw, "SoundFontsPath = " + SoundFontsPath);
      WriteLine(sw, "AudioDisconnected = " + clsBASSOutDev.Disconnected);
      WriteLine(sw, "indAsio = " + Utils.BoolYesNo(clsBASSOutDev.indAsio));
      WriteLine(sw, "AsioDevName = " + clsBASSOutDevAsio.DevName);
      WriteLine(sw, "NonAsioDevName = " + clsBASSOutDevNonAsio.DevName);
      WriteLine(sw, "NonAsioBuffer = " + clsBASSOutDevNonAsio.BufferSize);
      WriteLine(sw, "NonAsioUpdatePeriod = " + clsBASSOutDevNonAsio.UpdatePeriod);
      WriteLine(sw, "SoundFontStream = " + SoundFontStream);
      WriteLine(sw, "SoundFontKB = " + SoundFontKB);
      WriteLine(sw, "DIdd = " + DIdd);
      WriteLine(sw, "TPDI = " + TPDI);
      //WriteLine(sw, "PIdd = " + PIdd);
      //WriteLine(sw, "TPPI = " + TPPI);
      WriteLine(sw, "TrkType_SparsePercent = " + TrkType_SparsePercent);
      WriteLine(sw, "TrkType_MaxPitchBass = " + TrkType_MaxPitchBass);
      WriteLine(sw, "TrkType_MaxPitchBassPatched = " + TrkType_MaxPitchBassPatched);
      WriteLine(sw, "TrkType_PolyChord = " + TrkType_PolyChord);
      WriteLine(sw, "TrkType_ChordNotes = " + TrkType_ChordNotes);
      WriteLine(sw, "LoadMM = " + P.frmStart.chkLoadMM.Checked);
      //WriteLine(sw, "ShowPercussion = " + P.frmStart.chkShowPercussion.Checked);
      //WriteLine(sw, "HideStart = " + P.frmStart.chkHideStart.Checked);

      //WriteLine(sw, "LoadMidiFilePlayer = " + P.frmStart.chkLoadMidiFilePlayer.Checked);
      WriteLine(sw, "ShowRelChords = " + P.frmSC.chkShowChordsRel.Checked);
      WriteLine(sw, "DelaySustainOff = " + P.frmStart.chkDelaySustain.Checked);
      WriteLine(sw, "IgnoreNullChords = " + P.frmStart.chkIgnoreNullChords.Checked);
      WriteLine(sw, "KBChordMatch = " + P.frmStart.chkKBChordMatch.Checked);
      //WriteLine(sw, "ChunksPerQNote = " + (int)P.frmStart.nudChunksPerQNote.Value);
      //WriteLine(sw, "MinChordSize = " + (int)P.frmStart.nudMinChordSize.Value);
      //WriteLine(sw, "MaxChordSize = " + (int)P.frmStart.nudMaxChordSize.Value);
      WriteLine(sw, "TimerSustain = " + (int)P.frmStart.nudTimerSustain.Value);
      WriteLine(sw, "StreamResolution = " + (int)P.frmStart.nudPlayResolution.Value);
      //WriteLine(sw, "MP3Resolution = " + (int)P.frmStart.nudMP3Res.Value);
      //WriteLine(sw, "ExtResolution = " + (int)P.frmStart.nudExtRes.Value);
      WriteLine(sw, "NotesPerChordDisplay = " + (int)P.frmStart.nudNotesPerChordDisplay.Value);
      WriteLine(sw, "NotesPerChordPlay = " + (int)P.frmStart.nudNotesPerChordPlay.Value);
      WriteLine(sw, "ConstantChordDisplay = " + P.frmStart.chkConstantChordDisplay.Checked);
      WriteLine(sw, "ConstantChordPlay = " + P.frmStart.chkConstantChordPlay.Checked);
      WriteLine(sw, "Syncopation = " + P.frmStart.Syncopation.NN + ", " + P.frmStart.Syncopation.DD);
      //WriteLine(sw, "EnhancedSyncopation = " + clsCF.AlternateSyncopation.NN + ", " + clsCF.AlternateSyncopation.DD);
      //WriteLine(sw, "SyncoOnce = " + P.frmStart.chkSyncoOnce.Checked);
      //WriteLine(sw, "SaveMutedTrks = " + P.frmStart.chkSaveMutedTrks.Checked);
      //WriteLine(sw, "SaveEmptyTrks = " + P.frmStart.chkSaveEmptyTrks.Checked);
      //WriteLine(sw, "AddedTrks = " + (int)P.frmStart.nudAddedTrks.Value);
      //WriteLine(sw, "frmMMShowBeats = " + Forms.frmMultiMap.Static_ShowBeats);
      //WriteLine(sw, "frmMMShowKB = " + Forms.frmMultiMap.Static_ShowKB);
      //WriteLine(sw, "frmMMShowOneOct = " + Forms.frmMultiMap.Static_OneOct);
      //WriteLine(sw, "frmMMBarLabels = " + Forms.frmTrackMap.BarLabels);
      //WriteLine(sw, "frmNMSnapTo = " + Forms.frmChordMap.SnapTo);
      //WriteLine(sw, "frmNMSnapNote = " + Forms.frmChordMap.SnapNote);
      //WriteLine(sw, "frmNMSnapTime = " + Forms.frmChordMap.SnapTime);
      WriteLine(sw, "frmNMSnapTo = " + frmChordMap_SnapTo);
      WriteLine(sw, "frmNMShowBeats = " + Forms.frmChordMap.CfgShowBeats);
      WriteLine(sw, "frmNMShowKB = " + Forms.frmChordMap.CfgShowKB);
      WriteLine(sw, "frmNMOneOctave = " + Forms.frmChordMap.CfgOneOctave);
      WriteLine(sw, "frmNMVPixPerNote = " + Forms.frmChordMap.CfgVPixPerNote);
      //WriteLine(sw, "SaveChordLabels = " + P.frmStart.chkSaveChordLabels.Checked);
      //WriteLine(sw, "SaveChordTrack = " + P.frmStart.chkSaveChordTrack.Checked);
      WriteLine(sw, "AutoRecChan = " + P.frmStart.chkAutoRecChan.Checked);
      //WriteLine(sw, "PlaySustain = " + P.frmStart.chkPlaySustain.Checked);
      WriteLine(sw, "AllNotesOffAfterSustain = " + P.frmStart.chkAllNotesOffAfterSustain.Checked);
      //WriteLine(sw, "PlayKBChord = " + P.frmStart.chkPlayKBChord.Checked);
      WriteLine(sw, "PCKB = " + P.frmStart.chkPCKB.Checked);
      //WriteLine(sw, "SyncPlayRestore = " + P.frmStart.chkSyncPlayRestore.Checked);
      //WriteLine(sw, "AutoSyncMerge = " + P.frmStart.chkAutoSyncMerge.Checked);
      //WriteLine(sw, "SyncAudioPlaySync = " + P.frmStart.chkSyncAudioPlaySync.Checked);
      WriteLine(sw, "RunChordNotes = " + P.frmSC.chkRunChordNotes.Checked);
      //WriteLine(sw, "ShowChordNotes = " + P.frmSC.chkShowChordNotes.Checked);
      WriteLine(sw, "OctTransposeKBPitch = " + P.frmSC.OctTransposeKBPitch / 12);
      WriteLine(sw, "SaveMutedTrks = " + P.frmSaveMidiFileAs.chkSaveMutedTrks.Checked);
      //WriteLine(sw, "SaveEmptyTrks = " + P.frmSaveMidiFileAs.chkSaveEmptyTrks.Checked);
      WriteLine(sw, "SaveChordLabels = " + P.frmSaveMidiFileAs.chkSaveChordLabels.Checked);
      WriteLine(sw, "SaveChordTrack = " + P.frmSaveMidiFileAs.chkSaveChordTrack.Checked);
      //WriteLine(sw, "SaveRecTrack = " + P.frmSaveMidiFileAs.chkSaveRecTrk.Checked);
      WriteLine(sw, "SaveMidiFileChordRoot = " + P.frmSaveMidiFileAs.chkChordRoot.Checked);
      WriteLine(sw, "SaveMidiFileChordChan = " + P.frmSaveMidiFileAs.nudChordChan.Value);
      WriteLine(sw, "ToolTipsActive = " + P.frmStart.chkTTActive.Checked);
      //string nextev = (P.frmStart.optLiveNextBeat.Checked) ? "Beat" : "Chord";
      //WriteLine(sw, "NextEvent = " + nextev);
      TypeConverter convsf = TypeDescriptor.GetConverter(typeof(Font));
      string fontstringsf = convsf.ConvertToString(Forms.frmSC.MainFont);
      WriteLine(sw, "frmSCSolfaFont = " + fontstringsf);
      //WriteLine(sw, "frmSCSolfaFontReduction = " + Forms.frmSC.valFontReduction);
      TypeConverter convchord = TypeDescriptor.GetConverter(typeof(Font));
      string fontstringchord = convchord.ConvertToString(Forms.frmSC.ChordFont);
      WriteLine(sw, "frmSCChordFont = " + fontstringchord);
      WriteLine(sw, "frmSCChordColor = " + string.Format("{0:X8}", Forms.frmSC.ChordColor.ToArgb()));
      WriteLine(sw, "AutoCapitalize = " + Forms.frmSC.AutoCapitalizeStatic);
      if (!Forms.frmSC.AutoCapitalizeStatic) {
        WriteLine(sw, "CapitalizeRoots = " + Forms.frmSC.CapitalizeRootsStatic);
      }
      //WriteLine(sw, "SustainAuto = " + Forms.frmStart.SustainAutoStatic);
      WriteLine(sw, "SustainAuto = " + P.frmSC_Temp.chkSustainAuto.Checked);

      WriteLine(sw, "VolAudio = " + P.frmSC.trkAudioVol.Value);
      WriteLine(sw, "PanAudio = " + P.frmSC.trkAudioPan.Value);
      WriteLine(sw, "VolKB = " + P.frmSC.trkKBVol.Value);
      WriteLine(sw, "PanKB = " + P.frmSC.trkKBPan.Value);
      WriteLine(sw, "VolStream = " + P.frmSC.trkStreamVol.Value);
      WriteLine(sw, "PanStream = " + P.frmSC.trkStreamPan.Value);
      WriteLine(sw, "VolKBChan = " + P.frmSC.trkKBChanVol.Value);
      WriteLine(sw, "PanKBChan = " + P.frmSC.trkKBChanPan.Value);
      WriteLine(sw, "MidiOutKBFineTuning = " + Cfg.MidiOutKBFineTuning);
      WriteLine(sw, "MidiStreamFineTuning = " + Cfg.MidiStreamFineTuning);
      WriteLine(sw, "KBPatch = " + P.frmSC.cmbKBChanPatch.SelectedIndex);
      //WriteLine(sw, "PlayMode = " + P.frmSC.PlayMode);
       
      WriteLine(sw, "TonnetzHalfSpace = " + Forms.frmTonnetz.HalfSpace);
      //WriteLine(sw, "TonnetzEmerging = " + Forms.frmTonnetz.ClickToEmerging);
      WriteLine(sw, "TonnetzLookAhead = " + Forms.frmTonnetz.LookAhead);
      WriteLine(sw, "TonnetzCirclePenWidth = " + Forms.frmTonnetz.CirclePenWidth);
      WriteLine(sw, "TonnetzDiameter = " + Forms.frmTonnetz.Diameter);
      WriteLine(sw, "ExitPrompt = " + P.frmStart.chkExitPrompt.Checked);
      WriteLine(sw, "NoSync = " + P.frmStart.chkNoAudioSync.Checked);
      WriteLine(sw, "DefaultSongLength = " + (int)P.frmStart.nudDefaultSongLength.Value);
      WriteLine(sw, "UndoRedoCapacity = " + (int)P.frmStart.nudUndoRedoCapacity.Value);
      WriteLine(sw, "LatencyMidiPlay = " + LatencyMidiPlay);
      WriteLine(sw, "LatencyKB = " + LatencyKB);
      WriteLine(sw, "SCShowBeats = " + P.frmSC_Temp.chkShowBeats.Checked);
      WriteLine(sw, "SCShowChordNames = " + P.frmSC_Temp.chkShowChordNames.Checked);
      WriteLine(sw, "SCShowChordNotes = " + P.frmSC_Temp.chkShowChordNotes.Checked);
      WriteLine(sw, "SCShowTracks = " + P.frmSC_Temp.chkShowTracks.Checked);
      WriteLine(sw, "SCShowChords = " + P.frmSC_Temp.chkShowChords.Checked);
      WriteLine(sw, "SCFirstNote = " + P.frmSC_Temp.cmbFirstNote.SelectedItem);
      WriteLine(sw, "frmSCScroll = " + P.frmSC_Temp.chkScroll.Checked);
      //WriteLine(sw, "BlackNext = " + P.frmStart.chkNextBlack.Checked);

      //WriteLine(sw, "Sync_Disable = " + Forms.frmManChordSync.Disabled);
      //WriteLine(sw, "Sync_NoSkipAfterReloc = " + clsPlay.NoSkipAfterReloc);
      WriteLine(sw, "Sync_indNextBlack = " + clsPlay.indNextBlack);
      WriteLine(sw, "Sync_indNextWhite = " + clsPlay.indNextWhite);
      WriteLine(sw, "Sync_SingleBlackAction = " + clsPlay.SingleBlackAction);
      WriteLine(sw, "Sync_SingleWhiteAction = " + clsPlay.SingleWhiteAction);

      if (P.frmSC_Temp.optSustainReplay.Checked) WriteLine(sw, "SustainAction = Replay");
      else if (P.frmSC_Temp.optSustainCarryOver.Checked) WriteLine(sw, "SustainAction = CarryOver");
      else if (P.frmSC_Temp.optSustainSendCtlr.Checked) WriteLine(sw, "SustainAction = SendCtlr");
      else if (P.frmSC_Temp.optSustainNormal.Checked) WriteLine(sw, "SustainAction = Normal");

      if (P.frmSC.optShowSolfa.Checked) WriteLine(sw, "ShowNote = Solfa");
      else if (P.frmSC.optShowNoteName.Checked) WriteLine(sw, "ShowNote = NoteName");
      else if (P.frmSC.optShowPCKBChar.Checked) WriteLine(sw, "ShowNote = KBName");
      else WriteLine(sw, "ShowNote = None");

      WriteFX(sw, clsBassOutMidi.eOutType.KB);
      WriteFX(sw, clsBassOutMidi.eOutType.Stream);

      WriteFrmProps(sw, "frmSC");
      WriteFrmProps(sw, "frmStart");
      WriteFrmProps(sw, "frmTonnetz");
      WriteFrmProps(sw, "frmAutoSync");
      WriteFrmProps(sw, "frmChordMap");
      WriteFrmProps(sw, "frmTrackMap");

      //if (P.F != null) {
      //  if (P.frmSC != null) Utils.FormToProps(P.frmSC);
      //  Utils.clsFormProps propsfrmsc = Utils.FormsProps[(int)Utils.eFormType.frmSC];
      //  WriteLine(sw, "frmShowChordsProps = "
      //    + propsfrmsc.Loc.X + ", "
      //    + propsfrmsc.Loc.Y + ", "
      //    + propsfrmsc.Size.Width + ", "
      //    + propsfrmsc.Size.Height);
      //}

      //Utils.FormToProps(P.frmStart);
      //Utils.clsFormProps propsfrmstart = Utils.FormsProps[(int)Utils.eFormType.frmStart];
      //WriteLine(sw, "frmStartProps = "
      //  + propsfrmstart.Loc.X + ", "
      //  + propsfrmstart.Loc.Y + ", "
      //  + propsfrmstart.Size.Width + ", "
      //  + propsfrmstart.Size.Height);

      WriteLine(sw, "Ranges = "
        + Forms.frmSC.valOctavesDflt + ", "
        + Forms.frmSC.valShowLowCDflt + ", "
        + Forms.frmSC.valPlayLoC + ", "
        + Forms.frmSC.valPlayHiC + ", "
        + Forms.frmSC.valBeatHeight);
        //+ Forms.frmSC.indSolfa.ToString() + ", "
        //+ Forms.frmSC.indBeats.ToString());

      WriteLine(sw, "BezierVelocity = "
        + Bezier1X[0] + ", "
        + Bezier1Y[0] + ", "
        + Bezier2X[0] + ", "
        + Bezier2Y[0]);

      WriteLine(sw, "BezierAftertouch = "
        + Bezier1X[1] + ", "
        + Bezier1Y[1] + ", "
        + Bezier2X[1] + ", "
        + Bezier2Y[1]);

      Forms.frmTrackMap.StaticToIni(sw);
      //Forms.frmChordMap.StaticToIni(sw);
    }

    private static void WriteFrmProps(StreamWriter sw, string frm) {
      sw.WriteLine(frm + "Props = " + DictFormProps[frm].ToString());
    }

    private static void WriteFX(StreamWriter sw, clsBassOutMidi.eOutType type) {
      int i = (int)type;
      if (type == clsBassOutMidi.eOutType.KB) sw.Write("FXKB = ");
      else sw.Write("FXStream = ");
      BASS_BFX_FREEVERB fx = clsBassOutMidi.Freeverb[i];
      sw.Write(clsBassOutMidi.indFreeverb[i] + ", ");
      sw.Write(fx.fDryMix + ", ");
      sw.Write(fx.fWetMix + ", ");
      sw.Write(fx.fRoomSize + ", ");
      sw.Write(fx.fDamp + ", ");
      sw.WriteLine(fx.fWidth);
    }
  }
}
