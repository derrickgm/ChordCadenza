using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace ChordCadenza {
  public abstract class clsColors {
    internal clsColors() {
      InitFrmColours();
      clsData data;
      foreach (Panel mainpan in FrmColours.Controls.OfType<Panel>()) {

        //* get background
        data = new clsData(this, FrmColours);
        //data.Cmd = (Button)GetControl(mainpan, "background");
        data.Cmd = GetCmd(mainpan);
        data.Ctl = mainpan;
        data.DefaultCo = mainpan.BackColor;
        //data.Function = (eFunction)Enum.Parse(typeof(eFunction), (string)mainpan.Tag);  //xxxbackground
        data.Name = data.Cmd.Text;  //Xxx Background
        CList.Add(data.Name, data);

        //* get foregrounds
        foreach (Panel pan in mainpan.Controls.OfType<Panel>()) {
          data = new clsData(this, FrmColours);
          //data.Function = (eFunction)Enum.Parse(typeof(eFunction), (string)pan.Tag);
          data.Name = GetCmd(pan).Text;
          data.Cmd = (Button)GetControl(pan, "cmd");
          data.Ctl = GetControl(pan, "pic");
          data.PicTrans = (PictureBox)GetControl(pan, "pictrans");
          data.TrkBar = (TrackBar)GetControl(pan, "trk");
          TrackBar trk = (TrackBar)GetControl(pan, "trk");
          int alpha = (trk != null) ? trk.Value : 255;
          Color color = ((PictureBox)GetControl(pan, "pic")).BackColor;
          data.DefaultCo = Color.FromArgb(alpha, color);
          CList.Add(data.Name, data);
        }
      }
      LoadFile();
    }

    //internal enum eFunction {
    //  MainBackground,
    //  BottomBackground,
    //  BlackKeyboard,
    //  PlayedNote,
    //  ChordNote,
    //  RootNote,
    //  Solfa,
    //  Scale,
    //  FlashOK,
    //  FlashFail
    //}

    internal bool FileLoaded = false;
    internal static readonly char[] DelimEq = new char[] { '=' };
    internal SortedList<string, clsData> CList = new SortedList<string, clsData>(32);
    protected abstract void Refresh();
    internal abstract Form FrmColours { get; }
    protected abstract void InitFrmColours();
    protected virtual bool LoadSub(List<string> lines, int linenum) {
      return false;
    }
    protected virtual void SaveSub(StreamWriter xsw) { }
    protected abstract string FilePath { get; }

    internal clsData this[string txt] {
      get { return CList[txt]; }
    }

    internal Button GetCmd(Panel pan) {
      //* get first (or only) cmd from panel
      foreach (Button cmd in pan.Controls.OfType<Button>()) return cmd;
      return null;
    }

    //internal static eFunction ParseFunction(string txt) {
    //  return (eFunction)Enum.Parse(typeof(eFunction), txt);
    //}

    internal void SetDefaults() {
      foreach (KeyValuePair<string, clsData> pair in CList) {
        pair.Value.Co = pair.Value.DefaultCo;
      }
      Refresh();
    }

    private Control GetControl(Panel pan, string tag) {
      foreach (Control ctl in pan.Controls) {
        if ((string)ctl.Tag == tag) return ctl;
      }
      return null;
    }

    internal string SaveFile() {
      //if (P.IgnoreIni) return "";
      return Utils.SaveFile(FilePath, SaveFileSub);
    }

    private void SaveFileSub(StreamWriter xsw) {
      foreach (KeyValuePair<string, clsData> pair in CList) {
        xsw.WriteLine(pair.Value.OutLine());
      }
      //* throw new TestException();
      SaveSub(xsw);
    }

    internal void LoadFile() {
      SetDefaults();
      //if (P.IgnoreIni) return false;
      if (Cfg.NoIni) return;
      List<string> lines = Utils.ReadLinesIgnoreComments(FilePath);
      if (lines == null) return;
      char[] delimeq = new char[] { '=' };
      int linenum;
      for (linenum = 0; linenum < lines.Count; linenum++) {
        if (!InLine(lines[linenum])) {
          Refresh();
          if (!LoadSub(lines, linenum)) {
            #if DEBUG
              MessageBox.Show(FrmColours.Text +  ": Colour file line <" + lines[linenum] + "> not recognised");
            #endif
            return;
          }
          Debug.WriteLine(FilePath + " File loaded");
          return;
        }
      }
      Refresh();
      FileLoaded = true;
    }

    internal bool InLine(string line) {
      string[] f = line.Split(DelimEq, 2);
      //eFunction key;
      //if (!Enum.TryParse<eFunction>(f[0].Trim(), out key)) return false;
      string key = f[0].Trim();
      if (!CList.ContainsKey(key)) return false;
      clsData data = CList[key];
      data.Co = ParseColor(f[1]);  //also updates brush
      return true;
    }

    protected static Color ParseColor(string f1) {
      return Color.FromArgb(Convert.ToInt32(f1.Trim(), 16));
    }

    protected static string FormatColor(Color color) {
      return string.Format("{0:X8}", color.ToArgb());
    }

    internal class clsData {
      private clsColors Colors;
      private Form FrmColours;  //frmShowChordsColours
      internal string Name;  //also in CList key
      private Color _Co;  //incl. alpha
      private Brush _Br;
      internal Button Cmd;
      internal Control Ctl;  //was pic or pan
      internal PictureBox PicTrans;  //colour + alpha
      internal TrackBar TrkBar;  //opacity
      internal Color DefaultCo;  //incl. alpha

      internal clsData(clsColors colors, Form frm) {  //frmShowChordsColors
        Colors = colors;
        FrmColours = frm;
      }

      internal Color Co {
        get { return _Co; }
        set {
          _Co = value;
          _Br = new SolidBrush(value);
          Ctl.BackColor = Color.FromArgb(255, _Co);
          if (PicTrans != null) PicTrans.BackColor = _Co;
          if (TrkBar != null) TrkBar.Value = _Co.A;
        }
      }

      internal Brush Br {
        get { return _Br; }
      }

      internal void SetAlpha(int val) {
        Co = Color.FromArgb(val, Co);
        Colors.Refresh();
      }

      internal void SetColorDialog(ColorDialog dialog) {
        dialog.AnyColor = true;
        dialog.Color = Co;
        if (dialog.ShowDialog(FrmColours) == DialogResult.OK) {
          SetColorRGB(dialog.Color);
        }
        Colors.Refresh();
      }

      private void SetColorRGB(Color newcolor) {
        Co = Color.FromArgb(Co.A, newcolor);
      }

      internal string OutLine() {
        return Name + " = " + string.Format("{0:X8}", Co.ToArgb());
      }
    }
  }

  public class clsColorsShowChords : clsColors {
    internal override Form FrmColours {
      get { return FrmColoursSC; }
    }

    private Forms.frmColoursSC FrmColoursSC;

    protected override string FilePath {
      get { return Cfg.FrmSCColoursIniFilePath; }
    }

    protected override void InitFrmColours() {
      if (FrmColoursSC == null) FrmColoursSC = new Forms.frmColoursSC(this);
    }

    internal Color MainBackgroundColor { get { return CList["Main Background"].Co; } }
    internal Brush MainBackgroundBrush { get { return CList["Main Background"].Br; } }

    internal Color BottomBackgroundColor { get { return CList["Bottom Background"].Co; } }
    internal Brush BottomBackgroundBrush { get { return CList["Bottom Background"].Br; } }

    internal Color BlackKeyboardColor { get { return CList["Black Key"].Co; } }
    internal Brush BlackKeyboardBrush { get { return CList["Black Key"].Br; } }

    internal Color PlayedNoteColor { get { return CList["Played Note"].Co; } }
    internal Brush PlayedNoteBrush { get { return CList["Played Note"].Br; } }

    internal Color CurrentBackgroundColor { get { return CList["Current Background"].Co; } }
    internal Brush CurrentBackgroundBrush { get { return CList["Current Background"].Br; } }

    internal Color ChordNoteColor { get { return CList["Chord Note"].Co; } }
    internal Brush ChordNoteBrush { get { return CList["Chord Note"].Br; } }

    internal Color RootNoteColor { get { return CList["Root Note"].Co; } }
    internal Brush RootNoteBrush { get { return CList["Root Note"].Br; } }

    internal Color DominantNoteColor { get { return CList["Dominant Note"].Co; } }
    internal Brush DominantNoteBrush { get { return CList["Dominant Note"].Br; } }

    internal Color SolfaColor { get { return CList["Note"].Co; } }
    internal Brush SolfaBrush { get { return CList["Note"].Br; } }

    //internal Color BottomSolfaColor { get { return CList["Bottom Solfa"].Co; } }
    //internal Brush BottomSolfaBrush { get { return CList["Bottom Solfa"].Br; } }

    internal Color ScaleColor { get { return CList["Scale"].Co; } }
    internal Brush ScaleBrush { get { return CList["Scale"].Br; } }

    internal Color TrackNoteColor { get { return CList["Track Note"].Co; } }
    internal Brush TrackNoteBrush { get { return CList["Track Note"].Br; } }

    protected override void Refresh() {
      if (P.F != null && P.frmSC != null) {
        P.frmSC.picChords.BackColor = MainBackgroundColor;
        P.frmSC.picBottom.BackColor = BottomBackgroundColor;
        P.frmSC.picChords.Refresh();
        P.frmSC.picBottom.Refresh();
      }
    }

    internal bool ShowPlayedNote() {
      return ((Forms.frmColoursSC)FrmColours).chkBottom.Checked;
      //if (pos == 1) return ((Forms.frmColoursSC)FrmColours).chkBottom_1.Checked;
      //if (pos == 2) return ((Forms.frmColoursSC)FrmColours).chkBottom_2.Checked;
    }

    protected override bool LoadSub(List<string> lines, int linenum) {  //read played note display switches
      string[] f = lines[linenum].Split(DelimEq, 2);
      if (f[0].Trim() != "ShowPlayedNote") return false;
      //string[] ff = f[1].Split(new char[] { ',' });
      //FrmColoursSC.chkBottom.Checked = bool.Parse(ff[0]);
      //FrmColoursSC.chkBottom_1.Checked = bool.Parse(ff[1]);
      //FrmColoursSC.chkBottom_2.Checked = bool.Parse(ff[2]);
      FrmColoursSC.chkBottom.Checked = bool.Parse(f[1]);
      return true;
    }

    protected override void SaveSub(StreamWriter xsw) {
      string txt = "ShowPlayedNote = ";
      //txt += FrmColoursSC.chkBottom.Checked.ToString() + ", ";
      //txt += FrmColoursSC.chkBottom_1.Checked.ToString() + ", ";
      //txt += FrmColoursSC.chkBottom_2.Checked.ToString();
      txt += FrmColoursSC.chkBottom.Checked.ToString();
      xsw.WriteLine(txt);
    }
  }

  public class clsColorsNoteMap : clsColors {
    internal override Form FrmColours {
      get { return FrmColoursNM; }
    }

    private Forms.frmColoursNM FrmColoursNM;

    protected override string FilePath {
      get { return Cfg.FrmNMColoursIniFilePath; }
    }

    protected override void InitFrmColours() {
      if (FrmColoursNM == null) FrmColoursNM = new Forms.frmColoursNM(this);
    }

    protected override void Refresh() {
      if (P.F != null && P.F.frmChordMap != null) {
        P.F.frmChordMap.picNoteMapFile.BackColor = CList["Background"].Co;
        P.F.frmChordMap.picNoteMapMidi.BackColor = CList["Background"].Co;
        P.F.frmChordMap.picNoteMapQuant.BackColor = CList["Background"].Co;
        P.F.frmChordMap.picNoteMapMidi.Refresh();
        P.F.frmChordMap.picNoteMapQuant.Refresh();
        P.F.frmChordMap.picNoteMapFile.Refresh();
      }
    }
  }

  public class clsColorsTonnetz : clsColors {
    internal override Form FrmColours {
      get { return FrmColoursTonnetz; }
    }

    private Forms.Colours.frmColoursTonnetz FrmColoursTonnetz;

    protected override string FilePath {
      get { return Cfg.FrmTonnetzColoursIniFilePath; }
    }

    protected override void InitFrmColours() {
      if (FrmColoursTonnetz == null) FrmColoursTonnetz = new Forms.Colours.frmColoursTonnetz(this);
    }

    protected override void Refresh() {
      if (P.F != null && P.F.frmTonnetz != null) {
        P.F.frmTonnetz.Refresh();
      }
    }
  }
}