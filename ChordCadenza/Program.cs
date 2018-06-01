using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace ChordCadenza {
  public class clsProject {
    //* no need for LoadMidi button
    //* Load Project button and DragDrop should do the same thing
    //* load project from any valid file, but will always create <name>.chp on save
    //* Save Project in same location
    //* Save Project As using SFD, default: Cfg.ProjectDir\<name>
    //* save new .chp - no prompts (use .mid, CMajor, 4/4, 200 bars)
    //* if ChordMap opened, new .chp: bars/key/(tsig) prompt
    //* initial screen; new project from .mid ...

    internal clsProject() { }  //no project

    internal clsProject(string projectpath, string name, bool indnew) {  //new project (.chp0)
      ProjectPath = projectpath;
      Name = name;
      CHPExt = ".chp0";
      SetFrmTitles();
      P.frmSC.cmdSaveProjectAs.Enabled = true;
      P.frmSC.mnuSaveProjectAs.Enabled = true;
      P.frmSC.mnuReloadProject.Enabled = true;
      P.frmSC.mnuImport.Enabled = true;
    }

    internal clsProject(clsProject project, string projectpath, string name) {  //clone (Save As...)
      //* filename = any file, including full path
      ProjectPath = projectpath;
      Name = name;
      MidiExists = project.MidiExists;
      //LyricsExists = project.LyricsExists;
      AudioExt = project.AudioExt;
      //AudioSyncExt = project.AudioSyncExt;
      CHPExt = project.CHPExt;
      SetFrmTitles();
      P.frmSC.cmdSaveProjectAs.Enabled = true;
      P.frmSC.mnuSaveProjectAs.Enabled = true;
      P.frmSC.mnuReloadProject.Enabled = true;
      P.frmSC.mnuImport.Enabled = true;
    }

    internal clsProject(string filename, string chpext) {
      //* filename = any file, including full path
      ProjectPath = Path.GetDirectoryName(filename);
      Name = Path.GetFileNameWithoutExtension(filename);
      CHPExt = chpext;
      SetFrmTitles();
      P.frmSC.cmdSaveProjectAs.Enabled = true;
      P.frmSC.mnuSaveProjectAs.Enabled = true;
      P.frmSC.mnuReloadProject.Enabled = true;
      P.frmSC.mnuImport.Enabled = true;
    }

    private void SetFrmTitles() {
      foreach (Form frm in Application.OpenForms) {
        if (frm is IFormProjectName) ((IFormProjectName)frm).SetFormTitle(this);
      }
    }

    internal string ProjectPath = "";  //eg "D:\D1\Sonar\Projects\#test\Godfather"
    internal string Name = "";  //eg "Godfather"
    //* XXXExists=true if loaded or created new - not necessarily saved
    //* CHP must always exist

    private bool _MidiExists = false;
    internal bool MidiExists {
      get { return _MidiExists; }
      set {
        P.frmSC.mnuSaveMidiFileAs.Enabled = value;
        _MidiExists = value;
        P.frmStart.lblMidLoad.Text = (_MidiExists) ? ".mid" : "";
      }
    }

    private bool _AudioSyncExists = false;
    internal bool AudioSyncExists {
      get { return _AudioSyncExists; }
      set {
        _AudioSyncExists = value;
        P.frmStart.lblChtLoad.Text = (_AudioSyncExists) ? ".chtc" : "";
      }
    }

    private bool _LyricsExists = false;
    internal bool LyricsExists {
      get { return _LyricsExists; }
      set {
        _LyricsExists = value;
        P.frmStart.lblLyrLoad.Text = (_LyricsExists) ? ".chl" : "";
      }
    }

    private string _AudioExt = "";  //.mp3 etc.
    internal string AudioExt {  //.mp3 etc.
      get {
        return _AudioExt;
      }
      set {
        _AudioExt = value;
        P.frmStart.lblAudLoad.Text = _AudioExt;
      }
    }

    private string _CHPExt = ""; 
    internal string CHPExt {  //chp0 to chp9
      get {
        return _CHPExt;
      }
      set {
        _CHPExt = value;
        if (P.F?.frmChordMap != null) P.F.frmChordMap.SetFormTitle();
        P.frmSC.SetFormTitle();
        P.frmStart.lblChpLoad.Text = _CHPExt;
      }
    }

    internal string PathAndName {  //eg "D:\D1\Sonar\Projects\#test\Godfather\Godfather"
      get {
        if (ProjectPath == "" || Name == "") return "";
        return ProjectPath + '\\' + Name;
      }
    }

    //* paths where to save to (whether saved already or not) 
    internal string MidiPath {  //eg "D:\D1\Sonar\Projects\#test\Godfather\Godfather.mid"
      get {
        if (PathAndName == "") return "";
        return PathAndName + ".mid";
      }
    }

    internal string CHPPath {  //eg "D:\D1\Sonar\Projects\#test\Godfather\Godfather.chp"
      get {
        if (PathAndName == "") return "";
        if (CHPExt == "") return "";
        return PathAndName + CHPExt;
      }
    }

    internal string LyricsPath {  //eg "D:\D1\Sonar\Projects\#test\Godfather\Godfather.chl"
      get {
        if (PathAndName == "") return "";
        return PathAndName + ".chl";
      }
    }

    internal string AudioPath {  //eg "D:\D1\Sonar\Projects\#test\Godfather\Godfather.mp3"
      get {
        if (AudioExt == "") return "";
        //if (AudioSyncExt == ".chtx") return "";
        if (PathAndName == "") return "";
        return PathAndName + AudioExt;
      }
    }

    internal string AudioSyncPath {  //eg "D:\D1\Sonar\Projects\#test\Godfather\Godfather.chtc"
      get {
        //if (AudioSyncExt == "") return "";
        if (PathAndName == "") return "";
        return PathAndName + ".chtc";
      }
    }

    internal bool NameAsSubdir {
      get {
        if (Name == "") return false;
        return ProjectPath.EndsWith('\\' + Name);
      }
    }

    internal string ProjectPathXName {
      get {
        if (NameAsSubdir) return ProjectPath.Substring(0, ProjectPath.Length - Name.Length - 1);
        return ProjectPath;
      }
    }
  }

  sealed class Program {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    //internal static ApplicationContext AppContext;

    [STAThreadAttribute]
    static internal void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      //**************testing start
      //clsTrks.Array<string> Title = new clsTrks.Array<string>("***");
      //foreach (clsTrks.T t in Title.Next) {
      //  Debug.WriteLine(t.ToString() + ": " + Title[t]);
      //}
      //Debugger.Break();
      //**************testing end

//#if !DEBUG
//      //* allow debugger to show unhandled exceptions
      Application.Run(P.frmSC = new Forms.frmSC());
//#else
      ////*show all unhandled exceptions + innerexceptions
      //try {
      //  Application.Run(P.frmSC = new Forms.frmSC());
      //}
      //catch (Exception exc) {
      //  string msg = exc.Message;
      //  while (exc.InnerException != null) {
      //    exc = exc.InnerException;
      //    msg += "\r\n" + exc.Message;
      //  }
      //  MessageBox.Show("Exception: " + msg);
      //  Environment.Exit(1);
      //}
//#endif

      //Application.Run(P.frmBCL = new Forms.frmBCL());  //temp
      //AppContext = new clsContext();
      //Application.Run(AppContext);
    }
  }

  internal interface IFormStream {
    void FormStreamOnOff(bool on);  //enable/disable controls (not StartPlay/StopPlay etc.)
  }

  internal interface IFormProjectName {
    void SetFormTitle();
    void SetFormTitle(clsProject project);
  }

  internal interface ITT {  //form with ToolTips
    ToolTip TT { get; set; }
  }

  internal static class P {
    //internal static string Version = "01.11.00";
    internal static Forms.frmSC frmSC_Temp;  //used during Cfg initialisation
    internal static Forms.frmSC frmSC;
    internal static clsPCKB PCKB;
    internal static Forms.frmPCKBCfg frmPCKB;
    internal static Forms.frmPCKBIn frmPCKBIn;

    #if ADVANCED
      internal static Forms.frmConsole frmConsole;
    #endif

    //#if (DEBUG && ADVANCED)
    //    internal static bool Advanced = true;
    //#else
    //    internal static bool Advanced = false;
    //#endif

    //#if IGNOREINI
    //    internal static bool IgnoreIni = true;
    //#else
    //    internal static bool IgnoreIni = false;
    //#endif

    //#if DEBUG
    //    internal static bool Debug = true;
    //#else
    //    internal static bool Debug = false;
    //#endif

    internal static clsBASSOutDev BASSOutDev;
    internal static StreamWriter MMSW;  //multimidifile load (advanced)

    internal static Forms.frmCfgBezier frmCfgBezier;
    internal static Forms.frmStart frmStart;
    internal static Forms.frmMidiDevs frmMidiDevs;
    //internal static Forms.frmMod frmMod;
    internal static Forms.frmConfigBass frmConfigBass;
    internal static Forms.frmCfgChords frmCfgChords;
    internal static Forms.frmChordRanks frmChordRanks;
    internal static Forms.frmSwitch frmSwitch;
    //internal static Forms.frmControls frmControls;
    internal static clsColorsShowChords ColorsShowChords;
    internal static clsColorsNoteMap ColorsNoteMap;
    internal static clsColorsTonnetz ColorsTonnetz;
    internal static clsF F;
    //internal static clsChordSets ChordsSets;
    internal static Forms.frmSCOctaves frmSCOctaves;
    internal static Forms.dlgSCAlign frmSCAlign;
    internal static Forms.frmInitial frmInitial;
    internal static Forms.dlgSaveMidiFileAs frmSaveMidiFileAs;
    internal static Forms.frmManChordSync frmManChordSync;

    internal static bool CloseFrm(Form frm) {
      if (frm != null && frm.IsHandleCreated) {
        if (frm == P.frmSC) LogicError.Throw(eLogicError.X099); 
        else if (frm == P.frmStart) LogicError.Throw(eLogicError.X099);
        else frm.Close();
      }
      return (frm == null || !frm.IsHandleCreated);
    }
  }

  internal static class MidiPlay {
    //internal static clsMInOut MidiOutKB;
    //internal static clsMInOut MidiOutStream;
    internal static iBassMidiOut OutMKB = new clsBassMidiOutNull();
    internal static iBassMidiOut OutMStream = new clsBassMidiOutNull();
    internal static iBassMidiOut OutMRec {
      get {
        return (P.F?.frmTrackMap?.RecTrk == null) ? OutMKB : OutMStream;
      }
    }
    internal static clsMidiInKB MidiInKB;
    internal static clsMidiInSync MidiInSync;
    //internal static int KBInChan = -1;  //default omni
    internal static int KBOutChan = 0; 
    internal static int KBOutChanRec {
      get {
        if (P.F?.frmTrackMap?.RecTrk != null) return P.F.FSTrackMap.RecChan;
        return KBOutChan;
      }
    }  
    //internal static int KBOutChanAutoRiff = 1;   
    internal static clsSync Sync;

    internal static void CloseAllMidi(bool bassonly) {
      CloseMidi(null, bassonly);
    }

    internal static void CloseMidi(clsMidiInOut.eType? type, bool bassonly) {
      //* type=null: open all (in and out)
      try {
        if (!bassonly && (type == null || type.Value == clsMidiInOut.eType.InSync)) {
          if (MidiInSync != null) {
            MidiInSync.Close();
            MidiInSync.EndCallback();
            MidiInSync = null;
          }
          //clsBassMidiInOut.MidiDevNameInSync = "None";
        }

        if (!bassonly && (type == null || type.Value == clsMidiInOut.eType.InKB)) {
          if (MidiInKB != null) {
            MidiInKB.Close();
            MidiInKB.EndCallback();
            MidiInKB = null;
          }
          //clsBassMidiInOut.MidiDevNameInKB = "None";
        }

        if (type == null || type.Value == clsMidiInOut.eType.OutKB) {
          string outkb = clsBassMidiInOut.MidiDevNameOutKB;
          if (IsBuiltIn(outkb) || !bassonly) {
            if (OutMKB != null) {
              if (OutMKB == OutMStream) {
                OutMKB = new clsBassMidiOutNull();
                ((clsBassMidiInOut)OutMStream).Type = clsMidiInOut.eType.OutStream;
              } else {
                OutMKB.Close();
                OutMKB.EndCallback();
                OutMKB = new clsBassMidiOutNull();
              }
            }
            //clsBassMidiInOut.MidiDevNameOutKB = "None";
          }
        }

        if (type == null || type.Value == clsMidiInOut.eType.OutStream) {
          string outstream = clsBassMidiInOut.MidiDevNameOutStream;
          if (IsBuiltIn(outstream) || !bassonly) {
            if (OutMStream != null) {
              if (OutMStream == OutMKB) {
                OutMStream = new clsBassMidiOutNull();
                ((clsBassMidiInOut)OutMKB).Type = clsMidiInOut.eType.OutKB;
              } else {
                OutMStream.Close();  //may already be closed (if OutMStream WAS same as OutMKB before closing OutMKB
                OutMStream.EndCallback();
                OutMStream = new clsBassMidiOutNull();
              }
            }
            //clsBassMidiInOut.MidiDevNameOutStream = "None";
          }
        }
      }
      catch (AudioIOException) {  //continue executing
      }
      catch (MidiIOException) {  //continue executing
      }
    }

    internal static void CloseAllBass() {
      if (P.BASSOutDev != null) {
        P.BASSOutDev.Close();
        P.BASSOutDev = null;
      }
    }

    internal static bool OpenAudioDevs() {
      P.BASSOutDev = clsBASSOutDev.New();
      return (P.BASSOutDev != null);
    }

    private static void OpenMidiDev(clsMidiInOut.eType? type, bool bassonly) {
      //* type=null: open all (in and out)
      if (!bassonly && (type == null || type.Value == clsMidiInOut.eType.InSync)) OpenMidiInSync();
      if (!bassonly && (type == null || type.Value == clsMidiInOut.eType.InKB)) OpenMidiInKB();
      if (type == null || type.Value == clsMidiInOut.eType.OutStream) OpenMidiOutStream(bassonly);
      if (type == null || type.Value == clsMidiInOut.eType.OutKB) OpenMidiOutKB(bassonly);
      PostOpenMidi();
    }

    internal static void OpenMidiDev(clsMidiInOut.eType type, string name, bool bassonly) {
      if (!bassonly && type == clsMidiInOut.eType.InSync) OpenMidiInSync(name, true);
      if (!bassonly && type == clsMidiInOut.eType.InKB) OpenMidiInKB(name, true);
      if (type == clsMidiInOut.eType.OutStream) OpenMidiOutStream(name, true, bassonly);
      if (type == clsMidiInOut.eType.OutKB) OpenMidiOutKB(name, true, bassonly);
      PostOpenMidi();
    }

    internal static void OpenMidiDevs(string inkb, string insync, string outstream, string outkb,
      bool midiinkbconnected, 
      bool midiinsyncconnected, 
      bool midioutkbconnected, 
      bool midioutstreamconnected) {
        OpenMidiInKB(inkb, midiinkbconnected);
        OpenMidiInSync(insync, midiinsyncconnected);
        OpenMidiOutKB(outkb, midioutkbconnected, bassonly: false);
        OpenMidiOutStream(outstream, midioutstreamconnected, bassonly: false);
        PostOpenMidi();
    }

    internal static void OpenMidiDevs(bool bassonly) {
      OpenMidiDev(null, bassonly);
    }

    private static void OpenMidiInSync(string insync, bool act) {
      if (insync == "" || insync == "None") return;
      clsMidiIn.MidiDevNameInSync = insync;
      if (act) OpenMidiInSync();
    }

    private static void OpenMidiInSync() {
      MidiInSync = null;
      string insync = clsMidiIn.MidiDevNameInSync;
      clsMDevsIn midiindevs = new clsMDevsIn();
      if (insync == "***") {  //default
        if (midiindevs.Devs.Length > 0) insync = midiindevs.Devs[0]; else insync = "None";
        clsMidiIn.MidiDevNameInSync = insync;
      } else {
        if (!midiindevs.Devs.Contains(insync)) return;
      }  
      try {
        if (insync != "") {
          MidiInSync = new clsMidiInSync(midiindevs.Devs, insync);
          if (MidiInSync.Handle == IntPtr.Zero) MidiInSync = null;
        }
        Sync.indPlayActive = (MidiInSync != null) ?  //disable play if sync active
          clsSync.ePlay.SyncEnabled :
          clsSync.ePlay.None;
      }
      catch (MidiIOException exc) {
        MessageBox.Show("MidiIOException opening MidiInSync: " + exc.Message);
      }
      PlayableForms.CmdState_Set();
    }

    private static void OpenMidiInKB(string inkb, bool act) {
      if (inkb == "" || inkb == "None") return;
      clsMidiIn.MidiDevNameInKB = inkb;
      if (act) {
        OpenMidiInKB();
      } else {
        if (P.PCKB == null) P.PCKB = clsPCKB.NewPCKB(); 
      }
    }

    private static void OpenMidiInKB() {
      MidiInKB = null;  
      try {
        string inkb = clsMidiIn.MidiDevNameInKB;
        clsMDevsIn midiindevs = new clsMDevsIn();
        if (inkb == "***") {  //default
          if (midiindevs.Devs.Length > 0) inkb = midiindevs.Devs[0]; else inkb = "None";
          clsMidiIn.MidiDevNameInKB = inkb;
        } else {
          if (!midiindevs.Devs.Contains(inkb)) return;
        }
        if (inkb != "") {
          MidiInKB = new clsMidiInKB(midiindevs.Devs, inkb);
          if (MidiInKB.Handle == IntPtr.Zero) MidiInKB = null;
        }
      }
      catch (MidiIOException exc) {
        MessageBox.Show("MidiIOException opening MidiInKB: " + exc.Message);
      }
      finally {
        if (MidiInKB != null) {
          clsPCKB.NullifyPCKB();
        } else {
          if (P.PCKB == null) P.PCKB = clsPCKB.NewPCKB();
        }
      }
    }

    private static void OpenMidiOutStream(string outstream, bool act, bool bassonly) {
      if (!IsBuiltIn(outstream) && bassonly) return;
      clsBassMidiInOut.MidiDevNameOutStream = outstream;
      if (act) OpenMidiOutStream(bassonly);
    }

    private static void OpenMidiOutStream(bool bassonly) {
      OutMStream = null;
      string outstream = clsBassMidiInOut.MidiDevNameOutStream;
      if (!IsBuiltIn(outstream) && bassonly) return;
      if (outstream == "" || outstream == "None") {
        OutMStream = new clsBassMidiOutNull();
        return;
      }
      clsMDevsOut midioutdevs = new clsMDevsOut();
      if (outstream == "***") {  //default
        if (midioutdevs.Devs.Length > 0) outstream = midioutdevs.Devs[0]; else outstream = "None";
        clsMidiIn.MidiDevNameOutStream = outstream;
      } else {
        if (!midioutdevs.Devs.Contains(outstream) && !IsBuiltIn(outstream)) return;
      }  
      try {
        if (OutMKB != null && !(OutMKB is clsBassMidiOutNull)
        && clsBassMidiInOut.MidiDevNameOutKB == outstream) {
          OutMStream = OutMKB;
          ((clsBassMidiInOut)OutMStream).Type = clsMidiInOut.eType.OutKBStream;  //shared
        } else {
          string namestrm = outstream;
          if (IsBuiltIn(namestrm)) {
            if (!clsBASSOutDev.Disconnected && P.BASSOutDev != null) {
              OutMStream = new clsBassOutMidi(16, namestrm, Cfg.SoundFontStream, clsBassOutMidi.eType.OutStream);
            } else {
              MessageBox.Show("Cannot connect BuiltIn Midi device - No audio device connected");
            }
          } else {
            OutMStream = new clsMidiOut(midioutdevs.Devs, namestrm, clsMidiInOut.eType.OutStream);
          }
        }
      }
      catch (MidiIOException exc) {
        MessageBox.Show("MidiIOException opening MidiOutStream: " + exc.Message);
      }
    }

    private static void OpenMidiOutKB(string outkb, bool act, bool bassonly) {
      if (!IsBuiltIn(outkb) && bassonly) return;
      clsBassMidiInOut.MidiDevNameOutKB = outkb;
      if (act) OpenMidiOutKB(bassonly);
    }

    private static void OpenMidiOutKB(bool bassonly) {
      OutMKB = null;
      string outkb = clsBassMidiInOut.MidiDevNameOutKB;
      if (!IsBuiltIn(outkb) && bassonly) return;
      if (outkb == "" || outkb == "None") {
        OutMKB = new clsBassMidiOutNull();
        return;
      }
      clsMDevsOut midioutdevs = new clsMDevsOut();
      if (outkb == "***") {  //default
        if (midioutdevs.Devs.Length > 0) outkb = midioutdevs.Devs[0]; else outkb = "None";
        clsMidiIn.MidiDevNameOutKB = outkb;
      } else {
        if (!midioutdevs.Devs.Contains(outkb) && !IsBuiltIn(outkb)) return;
      }  
      try {
        if (OutMStream != null && !(OutMStream is clsBassMidiOutNull)
        && clsBassMidiInOut.MidiDevNameOutStream == outkb) {
          OutMKB = OutMStream;
          ((clsBassMidiInOut)OutMKB).Type = clsMidiInOut.eType.OutKBStream;  //shared
        } else {
          if (IsBuiltIn(outkb)) {
            if (!clsBASSOutDev.Disconnected && P.BASSOutDev != null) {
              OutMKB = new clsBassOutMidi(16, outkb, Cfg.SoundFontKB, clsBassOutMidi.eType.OutKB);
            } else {
              MessageBox.Show("Cannot connect BuiltIn Midi device - No audio device connected");
            }
          } else {
            OutMKB = new clsMidiOut(midioutdevs.Devs, outkb, clsMidiInOut.eType.OutKB);
          }
        }
        MidiMon.Reset();
      }
      catch (MidiIOException exc) {
        MessageBox.Show("MidiIOException opening MidiOutKB: " + exc.Message);
      }
    }

    //internal static void OpenMidiDevs(string inkb, string insync, string outstream, string outkb, bool bassonly) {
    //  //*	open midi devices
    //  clsMidiIn.MidiDevNameInKB = inkb;
    //  clsMidiIn.MidiDevNameInSync = insync;
    //  clsBassMidiInOut.MidiDevNameOutStream = outstream;
    //  clsBassMidiInOut.MidiDevNameOutKB = outkb;

    //  OpenMidiDevs(bassonly);
    //}

    //internal static void OpenMidiDevs(bool bassonly) {
    //  string inkb = clsMidiIn.MidiDevNameInKB;
    //  string insync = clsMidiIn.MidiDevNameInSync;
    //  string outstream = clsBassMidiInOut.MidiDevNameOutStream;
    //  string outkb = clsBassMidiInOut.MidiDevNameOutKB;

    //  clsMDevsIn midiindevs = new clsMDevsIn();
    //  clsMDevsOut midioutdevs = new clsMDevsOut();

    //  if (insync.Length > 0 && insync == inkb && insync != "None") {
    //    throw new MidiIOException("MidiInSync and MidiInKB cannot be the same device");
    //  }

    //  //* open MidiInSync
    //  try {
    //    if (insync != "" && !bassonly) {
    //      MidiInSync = new clsMidiInSync(midiindevs.Devs, insync);
    //      if (MidiInSync.Handle == IntPtr.Zero) MidiInSync = null;
    //    }
    //    if (MidiInSync != null) {  //disable play if sync active
    //      Sync.indPlayActive = clsSync.ePlay.SyncEnabled;
    //      //foreach (IPlayable pf in PlayableForms.Active) pf.StreamPlayDisable();
    //    } else {
    //      Sync.indPlayActive = clsSync.ePlay.None;
    //      //foreach (IPlayable pf in PlayableForms.Active) pf.StreamPlayOff();
    //    }
    //  }
    //  catch (MidiIOException exc) {
    //    MessageBox.Show("MidiIOException opening MidiInSync: " + exc.Message);
    //  }
    //  PlayableForms.CmdState_Set();

    //  //* open MidiInKB
    //  try {
    //    if (inkb != "" && !bassonly) {
    //      MidiInKB = new clsMidiInKB(midiindevs.Devs, inkb);
    //      if (MidiInKB.Handle == IntPtr.Zero) {
    //        //MessageBox.Show("Warning: MidiInKB handle not set");
    //        MidiInSync = null;
    //        //throw new MidiIOException("MidiInKB handle not set");
    //      }
    //    }
    //  }
    //  catch (MidiIOException exc) {
    //    MessageBox.Show("MidiIOException opening MidiInKB: " + exc.Message);
    //  }

    //  //* open MidiOutStream
    //  try {
    //    Forms.frmConsole.WriteLine("OpenMidiDevs: outstream = <" + outstream + ">");
    //    if (outstream != "") {
    //      string namestrm = outstream;
    //      //if (namestrm.StartsWith("BuiltIn")) {
    //      if (IsBuiltIn(namestrm)) {
    //        Forms.frmConsole.WriteLine("OpenMidiDevs: clsBASSOutDev.Disconnected = " + clsBASSOutDev.Disconnected
    //          + " BASSOutDev == null: " + (P.BASSOutDev == null));
    //        if (!clsBASSOutDev.Disconnected && P.BASSOutDev != null) {
    //          OutMStream = new clsBassOutMidi(16, namestrm, Cfg.SoundFontStream, clsBassOutMidi.eType.Stream);
    //        }
    //      } else if (!bassonly) {
    //        OutMStream = new clsMidiOut(midioutdevs.Devs, namestrm, clsMidiInOut.eType.OutStream);
    //      }
    //      //if (MidiOutStream != null) OutMStream = (iOutM)MidiOutStream;
    //    }
    //  }
    //  catch (MidiIOException exc) {
    //    MessageBox.Show("MidiIOException opening MidiOutStream: " + exc.Message);
    //  }

    //  //* open MidiOutKB
    //  //if (outkbs.Count == 0) outkbs.Add("");  //must have one midioutkb
    //  try {
    //    if (OutMStream != null && outstream == outkb) OutMKB = OutMStream;
    //    else {
    //      if (outkb != "") {
    //        if (IsBuiltIn(outkb)) {
    //          if (!clsBASSOutDev.Disconnected && P.BASSOutDev != null) {
    //            OutMKB = new clsBassOutMidi(16, outkb, Cfg.SoundFontKB, clsBassOutMidi.eType.KB);
    //          }
    //        } else if (!bassonly) {
    //          OutMKB = new clsMidiOut(midioutdevs.Devs, outkb, clsMidiInOut.eType.OutKB);
    //        }
    //      }
    //    }
    //  }
    //  catch (MidiIOException exc) {
    //    MessageBox.Show("MidiIOException opening MidiOutKB: " + exc.Message);
    //  }

    //  PostOpenMidi();
    //}

    private static void PostOpenMidi() {
      if (P.frmMidiDevs != null) P.frmMidiDevs.ShowCurrentConnections();

      if (P.frmSC != null) P.frmSC.panTrkStream.Enabled = false;
      if (OutMStream != null) {
        if (P.frmSC != null) {
          P.frmSC.panTrkStream.Enabled = (OutMStream is clsBassOutMidi);
          OutMStream.SetStreamVol(P.frmSC.trkStreamVol.Value);
          OutMStream.SetStreamPan(P.frmSC.trkStreamPan.Value);
        } else {
          OutMStream.SetStreamVol(80);
          OutMStream.SetStreamPan(0);
        }
        OutMStream.SetFineTuning(Cfg.MidiStreamFineTuning);
      }

      if (P.frmSC != null) P.frmSC.panTrkKB.Enabled = false;
      if (OutMKB != null) {
        if (P.frmSC != null) {
          P.frmSC.panTrkKB.Enabled = (OutMKB is clsBassOutMidi);
          OutMKB.SetStreamVol(P.frmSC.trkKBVol.Value);
          OutMKB.SetStreamPan(P.frmSC.trkKBPan.Value);
        } else {
          OutMKB.SetStreamVol(80);
          OutMKB.SetStreamPan(0);
        }
        OutMKB.SetFineTuning(Cfg.MidiOutKBFineTuning);
      }

      if (P.frmSC != null) P.frmSC.cmbKBChanPatch_SelectedIndexChanged(null, null);
    }

    internal static bool IsBuiltIn(string name) {
      return (name == "BuiltIn Synth (Stream)" || name == "BuiltIn Synth (KB)"); 
    }
  }

  internal class clsF {  //filestream or chordfile (or both)
    internal clsTrks Trks;
    internal clsTrks CFTrks;
    //internal const int DefaultSongLength = 100;
    private delegate void delegSetCmdsEna(bool ena);
    //internal bool indDGVReverse = false;

    internal static void NewEmpty(int bars) {
      if (P.F != null) P.F.CloseAllFormsUnconditional();
      P.F = new clsF();
      P.F.SetEmpty(bars);
    }

    internal static void NewEmpty() {
      if (P.F != null) P.F.CloseAllFormsUnconditional();
      P.F = new clsF();
      //P.frmStart.nudMaxBarsNoMidiFile.Value = DefaultMaxBars;
      P.F.SetEmpty(clsCF.DefaultSongLength);
    }

    internal void SetEmpty(int wholenotes) {
      InitNullMidiFile(wholenotes, 192, 4, 4);
      P.F.CF = new clsCFPC(0);
      if (P.frmSC != null) P.frmSC.NewEmpty();
    }

    internal class clsindSave {
      internal clsindSave() {
        Ind = false; //set P.frmSC.cmd...
      }

      //internal clsindSave(bool ind) {
      //  Ind = ind;
      //}

      private bool _Ind = false;
      internal static bool Loading = true;  //used to stop _Ind being set during load project

      internal bool Ind {
        get {
          return _Ind;
        }
        set {
          if (Loading) return;
          if (_Ind == value) return;
          _Ind = value;
          bool ena = (P.F != null && P.F.SaveProject(null, true, false));  //this only checks if anything needs saving!
          if (P.frmSC != null && P.frmSC.cmdSaveProject.Enabled != ena) {
            P.frmSC.Invoke(new delegSetCmdsEna(SetCmdsEna), ena);
            //SetCmdsEna(ena);
          }
        }
      }

      internal static void SetCmdsEna(bool ena) {
        if (P.frmSC != null) {
          P.frmSC.cmdSaveProject.Enabled = ena;
          P.frmSC.mnuSaveProject.Enabled = ena;
        }
        if (P.F?.frmTrackMap != null) P.F.frmTrackMap.cmdSaveProject.Enabled = ena;
        if (P.F?.frmChordMap != null) P.F.frmChordMap.cmdSaveProject.Enabled = ena;
      }
    }

    internal bool SaveProject(clsProject oldproject) {
      return SaveProject(oldproject, false, false);
    }

    internal bool SaveProject(clsProject oldproject, bool nomsg, bool indcancel) {
      //* oldproject = null
      //*   check required
      //*     nomsg=false (default): save changed files
      //*     nomsg=true: return (files to save > 0)
      //* oldproject not null
      //*   nomsg always false
      //*   copy files to new location
      //*   
      if (P.frmSC == null) return true;
      string msg = "";
      try {
        //* save chordfile
        if (CF?.Evs != null && CF.indSave) {
          if (oldproject == null) {
            msg += "\r\n" + Project.CHPPath;
          } else {
            msg = P.frmSC.SaveChordFile();
            if (msg != "") {
              MessageBox.Show("Error saving ChordFile: " + msg);
              //return false;
            }
          }
        }

        //* save audiosync file
        if (AudioSync != null && AudioSync.indSave) {
          if (oldproject == null) {
            msg += "\r\n" + Project.AudioSyncPath;
          } else {
            msg = AudioSync.SaveFile();
            if (msg != "") {
              MessageBox.Show("Error saving AudioSync File: " + msg);
              //return false;
            }
          }
        }

        //* save lyrics file
        if (Lyrics != null && Lyrics.indSave) {
          if (oldproject == null) {
            msg += "\r\n" + Project.LyricsPath;
          } else {
            msg = Lyrics.SaveFile();
            if (msg != "") {
              MessageBox.Show("Error saving Lyrics File: " + msg);
              //return false;
            }
          }
        }

        //* save midi file
        if (FSTrackMap != null && FSTrackMap.indSave) {
          if (oldproject == null) {
            msg += "\r\n" + Project.MidiPath;
          } else {
            clsSaveMidiFile savemidifile = new clsSaveMidiFile(FSTrackMap);
            msg = savemidifile.Save(Project.MidiPath);
            if (msg != "") {
              MessageBox.Show("Error saving Midi File: " + msg);
              //return false;
            }
          }
        }

        if (oldproject == null) {  //check required
          if (nomsg) {  //check only: return (filestosave > 0)
            return (msg != "");
          }
          //* save files
          if (msg != "") {  //at least one file to save
            msg = "Save Changes to the following files?" + msg;
            MessageBoxButtons buttons = (indcancel) ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo;
            DialogResult res = MessageBox.Show(msg, buttons);
            switch (res) {
              case DialogResult.Yes:
                P.frmSC.mnuSaveProject_Click(null, null);
                break;
              case DialogResult.No:
                break;
              case DialogResult.Cancel:
                return false;
            }
          }
          return true;  //dialog not cancelled
        }

        //* copy unchanged files to new location
        if (oldproject.PathAndName != Project.PathAndName) {
          //* copy all files in dir starting with project name
          string[] files = Directory.GetFiles(oldproject.ProjectPath, oldproject.Name + ".*");
          foreach (string f in files) {
            string dest = Project.ProjectPath + "\\" + Project.Name + Path.GetExtension(f);
            if (!File.Exists(dest)) File.Copy(f, dest);
          }

          ////* copy chordfile(s)
          //if (File.Exists(oldproject.CHPPath) && !File.Exists(Project.CHPPath)) {
          //  File.Copy(oldproject.CHPPath, Project.CHPPath, true);  //overwrite
          //}

          ////* copy audiosyncfile
          //if (File.Exists(oldproject.AudioSyncPath) && !File.Exists(Project.AudioSyncPath)) {
          //  File.Copy(oldproject.AudioSyncPath, Project.AudioSyncPath, true);  //overwrite
          //}

          ////* copy lyrics file
          //if (File.Exists(oldproject.LyricsPath) && !File.Exists(Project.LyricsPath)) {
          //  File.Copy(oldproject.LyricsPath, Project.LyricsPath, true);  //overwrite
          //}

          ////* copy midifile
          //if (File.Exists(oldproject.MidiPath) && !File.Exists(Project.MidiPath)) {
          //  File.Copy(oldproject.MidiPath, Project.MidiPath, true);  //overwrite
          //}

          ////* copy audiofile
          //if (File.Exists(oldproject.AudioPath)) {
          //  File.Copy(oldproject.AudioPath, Project.AudioPath, true);  //overwrite
          //}
        }
      }
      catch (Exception exc) {
        MessageBox.Show("Error saving or accessing Project file.\r\n" + exc.Message);
        return false;
      }
      P.frmSC.UpdateRecentProjects();
      return true;
    }

    //internal bool CheckSaves() {
    //  //* check if any files need saving before destroying F
    //  //* return false if cancelled
    //  string msg = "";
    //  if (Project.CHPPath != "" && CF != null && CF.indSave.Ind) {
    //    msg += "\r\n" + Project.CHPPath;
    //  }
    //  if (Project.AudioSyncPath != "" && AutoSync != null && AutoSync.indSave.Ind) {
    //    msg += "\r\n" + Project.AudioSyncPath;
    //  }
    //  if (Project.LyricsPath != "" && Lyrics != null && Lyrics.indSave.Ind) {
    //    msg += "\r\n" + Project.LyricsPath;
    //  }
    //  .//midifile

    //  if (msg != "") {
    //    msg = "Save Changes to the following files?" + msg;
    //    DialogResult res = MessageBox.Show(msg, "Chord Cadenza", MessageBoxButtons.YesNoCancel);
    //    switch (res) {
    //      case DialogResult.Yes:
    //        P.frmSC.mnuSaveProject_Click(null, null);
    //        break;
    //      case DialogResult.No:
    //        break;
    //      case DialogResult.Cancel:
    //        return false;
    //    }
    //  }
    //  return true;
    //}

    internal static void InitNullMidiFile(int wholenotes, int tpqn, int nn, int dd) {
      //int tpqn = 192;        if (!bars.HasValue) bars = 200; 
      P.F.MTime = new clsMTime(tpqn, nn, dd);
      P.F._MidiKeys = new clsKeysTicks(0, "major");
      //P.F.MaxTicks = tpqn * 4 * bars.Value;
      //P.F._MaxMidiTicks = bars * 4 * tpqn;
      P.F.MaxBBT = new clsMaxBBT(wholenotes * 4 * tpqn);
      P.F.CurrentBBT = new clsMTime.clsBBT(0);
      //P.frmStart.nudMaxBarsNoMidiFile.Value = bars;
      //clsCF.SongLength = wholenotes;
      if (P.frmSC != null) P.frmSC.vScrollBar1.Maximum = P.F.MaxBBT.Bar;
      //clsLoadMidiFile.GetTicksPerQI();
    }

    //internal clsUndoRedoKeys UndoRedoKeys = new clsUndoRedoKeys();
    internal clsKeysTicks Keys {
      get {
        if (_CFKeys != null) return _CFKeys;
        if (_MidiKeys != null) return _MidiKeys;
        //return null;
        return _DefaultKeys;
      }
      //set {
      //  if (_CFKeys != null) _CFKeys = value;
      //  else if (_MidiKeys != null) _MidiKeys = value;
      //  LogicError.Throw(eLogicError.X154);
      //  return;
      //}
    }

    internal void CopyDefaultKeys() {
      if (_CFKeys == null) _CFKeys = _DefaultKeys;
    }

    //internal clsKeys KeysAlt;
    internal clsKeysTicks _MidiKeys;
    internal clsKeysTicks _CFKeys;
    internal clsKeysTicks _DefaultKeys = new clsKeysTicks("C", "major");

    internal clsMaxBBT MaxBBT;
    //internal int _MaxMidiTicks;  //from midifile EOTracks
    //internal int _MaxNoteMapTicks;  //allowing for tsig change expansion
    //internal int _MaxNoteMapQI;  //allowing for tsig change expansion

    //internal int MaxTicks {
    //  //* should be on a bar boundary (len = maxticks + 1)
    //  //* setlastev etc
    //  //* CF header (new - may not be required)
    //  //* newsections numbars
    //  get { return _MaxTicks; }
    //  set {
    //    _MaxTicks = value;
    //  }
    //}
    //private int _MaxTicks = -1;

    internal void SetKeys(clsKeysTicks val) {
      if (_CFKeys != null) _CFKeys = val; else _DefaultKeys = val;
      //if (val.indCF) _CFKeys = val; else _MidiKeys = val;
    }

    internal void GetTicksPerQI(clsMTime mtime) {
      //* called only during new P.F / new MTime
      if ((mtime.TicksPerQNote % 8) != 0) {
        throw new MidiFileException("Midi Header: TicksPerQuarterNote = "
          + mtime.TicksPerQNote % 8 + " not supported");
      }

      if (Cfg.TPDI == 0) {
        QIPerNote = Cfg.DIdd;  //16, 32, 64
        while (((mtime.TicksPerQNote * 4) % QIPerNote) != 0) QIPerNote /= 2;
        TicksPerQI = (mtime.TicksPerQNote * 4) / QIPerNote;
      } else {
        TicksPerQI = Cfg.TPDI;
        //* QIs per sixteenth note = ticks per sixteenth note / ticks per QI : no remainder
        while ((mtime.TicksPerQNote / 4) % TicksPerQI != 0) TicksPerQI -= 1;
        QIPerNote = (mtime.TicksPerQNote * 4) / TicksPerQI;
      }
      HalfTicksPerQI = TicksPerQI / 2;
      Debug.WriteLine("DIPerNote = " + this.QIPerNote + "; TicksPerDI = " + this.TicksPerQI);
    }

    internal int TicksPerQI;
    internal int QIPerNote;  //quantized to 1/QIdd note (e.g. 1/32 note) 
    internal int HalfTicksPerQI;
    //internal int TicksPerPI;
    //internal int PIPerNote;  //quantized to 1/QIdd note (e.g. 1/32 note) 
    //internal int HalfTicksPerPI;
    //internal int NumNewTrks = -1;  //recording
    internal bool LoadWarningShown = false;

    //internal int MaxQTime {
    //  //* notemap len
    //  //* notemap.chordatt len
    //  //* onoff len
    //  get {
    //    int rem;
    //    int q = Math.DivRem(MaxTicks, TicksPerQI, out rem);
    //    if (rem > 0) q++;  //round up
    //    return q;
    //  }
    //}

    //internal int MaxPITime {
    //  get {
    //    int rem;
    //    int q = Math.DivRem(MaxTicks, TicksPerPI, out rem);
    //    if (rem > 0) q++;  //round up
    //    return q;
    //  }
    //}

    //internal clsProject Project {
    //  get {
    //    return _Project;
    //  }
    //  set {
    //    if (P.F?.AutoSync?.MP3Player is clsMP3Bass) {
    //      P.F.AutoSync.MP3Player.CloseStream();
    //    }
    //    _Project = value;
    //  }
    //}
    internal clsProject Project = new clsProject();

    //internal string ProjectPathAndName = "";  //e.g. D:\D1\Sonar\Projects\#test\godfather\godfather

    //private string _CHPFilePath = "";
    //internal string CHPFilePath {
    //  get {
    //    if (_CHPFilePath != "") return _CHPFilePath;
    //    if (ProjectPathAndName == "") return "";
    //    return ProjectPathAndName + ".chp";
    //  }
    //  set {
    //    _CHPFilePath = value;  //set by frmNoteMap Save As...
    //  }
    //}

    //private string _MidiPath = "";
    //internal string MidiPath {
    //  get {
    //    if (ProjectPathAndName == "") return _MidiPath;
    //    return ProjectPathAndName + ".mid";
    //  }
    //  set {
    //    _MidiPath = value;
    //  }
    //}

    internal bool MidiFileLoaded {
      get {
        return P.F.Project.MidiExists;
        //return (FSTrackMap != null);
      }
    }

    internal bool CloseFormsUnconditional = false;
    //internal bool CloseFormsAll = false;
    internal byte[] MidiFileBuffer;
    internal clsCFPC CF;
    internal clsCFPitch CFNotes;

    internal Forms.frmSummary frmSummary;
    internal Forms.frmAutoSync frmAutoSync;
    internal Forms.frmChordMap frmChordMap;
    internal Forms.frmChordMapAdv frmChordMapAdv;
    internal Forms.frmTrackMap frmTrackMap;
    internal Forms.frmCalcKeys frmCalcKeys;
    internal clsLyrics Lyrics = new clsLyrics();
    internal Forms.frmLyrics frmLyrics;
    //internal Forms.frmCmdChords frmCmdChords;
    internal Forms.frmTonnetz frmTonnetz;
    internal Forms.frmTrackStyles frmTrackStyles;

    internal clsLoadCSV LoadCSV;
    internal clsFileStream FileStreamConv;
    internal clsFileStream FSTrackMap;
    internal clsWaitPlay WaitPlay;
    private clsAudioSync _AudioSync;
    internal clsAudioSync AudioSync {
      get {
        return _AudioSync;
      }
      set {
        //if (value == null) P.frmSC.lblTopSync.Text = "Sync: None";
        _AudioSync = value;
        P.frmSC.mnuShowAudioSyncWindow.Enabled = (value != null);
      }
    }
    internal clsMute Mute;

    internal clsTrks.Array<int> Chan;
    internal clsTrks.Array<bool> Added;

    //* new arrays
    internal int[] Vol;  //[portchan]
    internal int[] Pan;  //[portchan]
    internal int[] Patch;  //[portchan]

    internal clsMTime MTime;
    //internal clsMTime NewMTime = null;
    internal clsMTime.clsBBT BarPaneBBTLo;
    internal clsMTime.clsBBT BarPaneBBTHi;
    private clsMTime.clsBBT _CurrentBBT;
    //private delegate void delegSetScrollVal(int val);
    private delegate void delegSetScrollBarBBT(clsMTime.clsBBT bbt);
    internal clsMTime.clsBBT CurrentBBT {
      get {
        return _CurrentBBT;
      }
      set {
        if (P.frmSC?.Play != null) {
          if (CurrentBBT == null || value.Ticks != _CurrentBBT.Ticks) {
            P.frmSC.Play.indReloc = true;
          }
        }
        _CurrentBBT = value;
        if (P.frmSC != null) {
          if (!P.frmSC.ScrollEvActive) {
            P.frmSC.BeginInvoke(new delegSetScrollBarBBT(P.frmSC.SetScrollBarBBT), _CurrentBBT);
          }
        }
        frmTonnetz?.NewCurrentBBT();
      }
    }

    internal int StartBar {
      get {
        if (P.frmSC == null) return 0;
        return P.frmSC.StartBar;
      }
      set {
        //clsPlay.SetBeatChord_FirstTime = true;
        if (P.frmSC == null) return;
        P.frmSC.nudStartBar.Value = Math.Max(0, Math.Min(P.frmSC.nudStartBar.Maximum, value + 1));
        //P.frmSC.Play?.Reset();  //should fix problems with missing MidiOff evs
      }
    }

    internal bool CondTrkEmpty = true;  //assume conductor trk does not contain channel events
    //internal bool indCalcKeys = false;  //true if frmCalcKeys has been shown

    //internal clsF(string songpath) : this() {
    //  ProjectPathAndName = (songpath == null) ? "" : songpath;
    //}

    internal bool CloseCancellableForms() {
      //* check form closures that may be cancelled by the user
      //CloseFormsAll = true;
      if (!P.CloseFrm(P.F.frmAutoSync)) return false;
      if (!P.CloseFrm(P.F.frmChordMap)) return false;
      if (!P.CloseFrm(P.F.frmLyrics)) return false;
      //P.CloseFrm(P.F.frmTrackMap);
      if (!P.CloseFrm(P.F.frmTrackMap)) return false;
      //CloseFormsAll = false;
      return true;
    }

    internal static bool NewF(ref clsF f) {
      bool ret = true;
      if (P.F != null) {
        ret = P.F.CloseCancellableForms();
        if (P.F.AudioSync?.MP3Player is clsMP3Bass) {
          P.F.AudioSync.MP3Player.CloseStream();
        }
      }
      if (ret) f = new clsF();
      return ret;
    }

    private clsF() {
      if (P.F != null) CloseAllFormsUnconditional();
      //if (P.frmCfgChords != null && P.frmCfgChords.IsHandleCreated) P.frmCfgChords.Hide();

      Trks = new clsTrks();
      StartBar = 0;
      //clsFileStream.QIdd = Cfg.QIdd;
      Mute = new clsMute(Trks);
      Chan = new clsTrks.Array<int>(Trks, -1);
      Added = new clsTrks.Array<bool>(Trks, false);
      Vol = new int[16];
      Pan = new int[16];
      Patch = new int[16];
      for (int pc = 0; pc < 16; pc++) {
        Vol[pc] = -1;
        Pan[pc] = -1;
        Patch[pc] = -1;
      }
      clsPlay.Sustain = clsPlay.clsSustain.New(null);
      if (P.frmSC != null) P.frmSC.NewEmpty();
      P.frmStart.NewFilesDisplay();
    }

    private void CloseAllFormsUnconditional() {
      CloseFormsUnconditional = true;
      P.CloseFrm(P.F.frmAutoSync);
      P.CloseFrm(P.F.frmChordMap);
      P.CloseFrm(P.F.frmTrackMap);
      P.CloseFrm(P.F.frmChordMapAdv);
      P.CloseFrm(P.F.frmSummary);
      P.CloseFrm(P.F.frmCalcKeys);
      P.CloseFrm(P.F.frmLyrics);
      //P.CloseFrm(P.F.frmCmdChords);
      P.CloseFrm(P.F.frmTonnetz);
      CloseFormsUnconditional = false;
    }

    internal void SendInit() {
      //P.frmStart.trkMidiStream_Scroll(P.frmStart.trkMidiStream, EventArgs.Empty);
      //P.frmStart.trkMidiOut_Scroll(P.frmStart.trkMidiOut, EventArgs.Empty);
      foreach (clsTrks.T trk in P.F.Trks.Ints.Next) {
        SendVol(trk);
        SendPan(trk);
        SendPatch(trk);
      }
    }

    internal void SendVol(clsTrks.T trk) {
      int chan = P.F.Chan[trk];
      if (chan < 0) return;
      int val = P.F.Vol[chan];
      if (MidiPlay.OutMStream != null && chan >= 0 && val >= 0) {
        MidiPlay.OutMStream.SendShortMsg(0xb0 | chan, 7, val);  //ctlr 7 volume 
      }
    }

    internal void SendPan(clsTrks.T trk) {
      int chan = P.F.Chan[trk];
      if (chan < 0) return;
      int val = P.F.Pan[chan];
      if (MidiPlay.OutMStream != null && chan >= 0 && val >= 0) {
        MidiPlay.OutMStream.SendShortMsg(0xb0 | chan, 10, val);  //ctlr 10 pan
      }
    }

    internal void SendPatch(clsTrks.T trk) {
      int chan = P.F.Chan[trk];
      if (chan == 9) return;  //percussion
      if (chan < 0) return;
      int val = P.F.Patch[chan];
      //FileStream.Play.SendDirect(0xc0 | chan, val, 0);  //status 0xc0   
      if (MidiPlay.OutMStream != null && chan >= 0 && val >= 0) {
        MidiPlay.OutMStream.SendShortMsg(0xc0 | chan, val, 0);  //status 0xc0 
      }
    }

    internal void Panic() {
      //* all notes off, stop stream play

      //if (P.F.AudioSync != null) P.F.AudioSync.Stop();
      MidiPlay.Sync.Stop();

      clsPlay.clsSustain.PlayPedalStatic(false);
      MidiPlay.OutMKB.AllNotesOff();
      if (MidiPlay.OutMStream != null) MidiPlay.OutMStream.AllNotesOff();

      //foreach (clsMidiOutStream m in MidiPlay.MidiOutStreams) {
      //  if (m != null) m.AllNotesOff();
      //}
      //MidiPlay.Sync.Stop();
      //MidiPlay.Sync.indPlayActive = clsSync.ePlay.None;
      PlayableForms.CmdState_Stopped();
      //foreach (IPlayable pf in PlayableForms.Active) pf.StreamPlayOff();
      if (P.frmSC?.Play != null) P.frmSC.Play.Reset();
      if (P.F != null && P.F.WaitPlay != null) P.F.WaitPlay.ResetAllCtlrs();
    }

  }
  internal class clsNNDD {
    private int _Ticks;

    private int _NN;
    internal int NN {
      get {
        return _NN;
      }
      set {
        _NN = value;
        CalcTicks();
      }
    }

    private int _DD;
    internal int DD {
      get {
        return _DD;
      }
      set {
        _DD = value;
        CalcTicks();
      } 
    } 

    internal int Ticks { get { return _Ticks; } }

    internal clsNNDD(int nn, int dd) {
      _NN = nn;
      _DD = dd;
    }

    internal clsNNDD(NumericUpDown nudNN, NumericUpDown nudDD) {
      _NN = (int)nudNN.Value;
      _DD = (int)nudDD.Value;
    }

    internal clsNNDD Copy() {
      return new clsNNDD(NN, DD);
    }

    private void CalcTicks() {
      if (P.F == null || P.F.MTime == null) return;
      _Ticks = (_NN * P.F.MTime.TicksPerQNote * 4) / DD;
    }

    public override string ToString() {
      return NN.ToString() + "/" + DD.ToString();
    }

    internal static clsNNDD GetNNDD(string txt) {
      //* get NNDD from ToString() text
      string[] f = txt.Split(new char[] { '/' });
      return new clsNNDD(int.Parse(f[0]), int.Parse(f[1]));
    }

    internal bool IsEquiv(clsNNDD nndd) {
      if (nndd == null) return false;
      return (nndd.NN == NN && nndd.DD == DD);
    }
  }
}
