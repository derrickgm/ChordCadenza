using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ChordCadenza.Forms;

namespace ChordCadenza {
  internal class clsPicNoteMapSC : clsPicNoteMap {
    //* used by frmMultiMap - one trk only
    internal clsPicNoteMapSC(PictureBox pic) 
      : base(pic, null, false, P.frmSC.BarFont, false) {
      SetPicSize(null);
    }

    protected override int HFactor { get { return P.frmSC.HPixPerQI.NN; } }
    protected override int VFactor { get { return P.frmSC.VFactor; } }
    protected override int HDiv { get { return P.frmSC.HPixPerQI.DD; } }
    protected override int BarDiv { get { return 1; } }
    protected override bool OneOct { get { return false; } }
    protected override bool ShowKB { get { return false; } }
    protected override bool ShowBeats { get { return false; } }
    protected override clsFileStream FileStream { get { return P.F.FSTrackMap; } }

    protected override int Octaves {
      get {
        int octlo = 13, octhi = 0;
        foreach (clsTrks.T trk in P.frmSC.MapTrks) {
          if (FileStream.TrkMinPitch[trk] == 127 && FileStream.TrkMaxPitch[trk] == 0) return 3;
          octlo = Math.Min(octlo, FileStream.TrkMinPitch[trk] / 12);
          octhi = Math.Max(octhi, FileStream.TrkMaxPitch[trk] / 12);
        }
        return Math.Max(1, octhi - octlo + 1);
      }
    }

    protected override int GetPixPerNote(Forms.frmTrackMap frm) { //frm not used here
      return (P.frmSC.VFactor); 
    }

    protected override int GetTrkHeight(Forms.frmTrackMap frm) {  //frm not used here
      return P.frmSC.VFactor * Octaves * 12;
    }

    protected override void GetRanges(frmTrackMap frm) {   //frm null
      PixPerNoteInt = Math.Max(1, GetPixPerNote(frm));
      int octlo = 13, octhi = 0;
      MinPitchShow = 127;
      MaxPitchShow = 0;
      foreach (clsTrks.T trk in P.frmSC.MapTrks) {
        MinPitchShow = Math.Min(MinPitchShow, FileStream.TrkMinPitch[trk]);
        MaxPitchShow = Math.Max(MaxPitchShow, FileStream.TrkMaxPitch[trk]);
        octlo = Math.Min(octlo, FileStream.TrkMinPitch[trk] / 12);
        octhi = Math.Max(octhi, FileStream.TrkMaxPitch[trk] / 12);
      }
      MinC = Math.Min(120, octlo * 12);
    }

    protected override bool GetNoteMap(clsNoteMap notemap, int qtime, int notemod) {
      return ((clsNoteMapMidi)notemap)[qtime, notemod, true];   //all trks
    }

    protected override bool GetNoteMap(clsNoteMap notemap, int qtime, int note, bool mod) {
      return ((clsNoteMapMidi)notemap)[qtime, note, mod];   //all trks 
    }
  }

  internal class clsPicNoteMapMM : clsPicNoteMap {
    //* used by frmMultiMap - one trk only
    internal clsPicNoteMapMM(Forms.frmTrackMap multimap, PictureBox pic, clsTrks.T trk) 
      : base(pic, trk, false, multimap.BarFont, false) {
      frmMM = multimap;
      //FileName = multimap.FileName;
      //NoteMap = multimap.NoteMap;
      //MaxQTime = multimap.MaxQTime;
      //NumTrks = multimap.NumTrks;
      //QIdd = multimap.QIdd;
      //TicksPerQI = multimap.TicksPerQI;
      //FirstTrk = false;
      SetPicSize(multimap);
    }

    //internal bool FirstTrk;
    //internal override Font BarFont {
    //  get {
    //    if (!FirstTrk) return null;
    //    return ((Forms.frmMultiMap)FontFrm).BarFont;
    //  }
    //}
    protected const int MinOctRec = 2;
    protected const int MaxOctRec = 9;
    protected static readonly int MinPitchRec = MinOctRec * 12;
    protected static readonly int MaxPitchRec = MaxOctRec * 12;

    protected Forms.frmTrackMap frmMM;
    protected override int HFactor { get { return frmMM.HFactor; } }
    protected override int VFactor { get { return frmMM.VFactor; } }
    protected override int HDiv { get { return frmMM.HDiv; } }
    //protected override int BarDiv { get { return (int)frmMM.nudBars.Value; } }
    protected override int BarDiv { get { return 1; } }
    protected override bool OneOct { get { return frmMM.chkOneOctave.Checked; } }
    protected override bool ShowKB { get { return frmMM.chkShowKB.Checked; } }
    protected override bool ShowBeats { get { return frmMM.chkShowBeats.Checked; } }
    //protected override clsFileStream FileStream { get { return frmMM.FileStream; } }
    protected override clsFileStream FileStream { get { return P.F.FSTrackMap; } }
    protected override int Octaves {
      get {
        int octlo, octhi;
        if (frmMM.RecTrk == Trk) {
          octlo = MinOctRec;
          octhi = MaxOctRec;
        } else if (FileStream.TrkMinPitch[Trk] == 127 && FileStream.TrkMaxPitch[Trk] == 0) {
          return 3;
        } else {
          octlo = FileStream.TrkMinPitch[Trk] / 12;
          octhi = FileStream.TrkMaxPitch[Trk] / 12;
        }
        return octhi - octlo + 1;
      }
    }

    //protected override int GetPW(int qilo, int pwval, int qtime) {
    //  if (qtime == qilo) pwval = frmMM.FileStream.PitchBend[Trk].SearchGetValueQI(qtime);
    //  else pwval = frmMM.FileStream.PitchBend[Trk].SeqGetValueQI(qtime);
    //  return pwval;
    //}

    protected override int GetPixPerNote(Forms.frmTrackMap frm) { 
      return frm.GetPixPerNoteInt(Octaves);  //frmMultiMap
    }

    protected override int GetTrkHeight(Forms.frmTrackMap frm) {
      return frm.GetTrkHeight(Octaves);
    }

    protected override void GetRanges(frmTrackMap frm) {
      MinPitchShow = (frmMM.RecTrk == Trk) ? MinPitchRec : FileStream.TrkMinPitch[Trk];
      MaxPitchShow = (frmMM.RecTrk == Trk) ? MaxPitchRec : FileStream.TrkMaxPitch[Trk];
      PixPerNoteInt = Math.Max(1, frm.GetPixPerNoteInt(Octaves));
      int octlo, octhi;
      if (frmMM.RecTrk == Trk) {
        octlo = MinOctRec;
        octhi = MaxOctRec;
      } else {
        octlo = FileStream.TrkMinPitch[Trk] / 12;
        octhi = FileStream.TrkMaxPitch[Trk] / 12;
      }
      MinC = octlo * 12;
    }

    //protected override void GetMinMaxPitch(out int min, out int max) {
    //  min = (frmMM.RecTrk == Trk) ? MinPitchRec : FileStream.TrkMinPitch[Trk];
    //  max = (frmMM.RecTrk == Trk) ? MaxPitchRec : FileStream.TrkMaxPitch[Trk];
    //}

    //protected override void GetYMinC(Forms.frmTrackMap frm, out int pixpernote, out int minc) {
    //  pixpernote = Math.Max(1, frm.GetPixPerNoteInt(Octaves));
    //  int octlo, octhi;
    //  if (frmMM.RecTrk == Trk) {
    //    octlo = MinOctRec;
    //    octhi = MaxOctRec;
    //  } else {
    //    octlo = FileStream.TrkMinPitch[Trk] / 12;
    //    octhi = FileStream.TrkMaxPitch[Trk] / 12;
    //  }
    //  minc = octlo * 12;
    //}

    protected override bool GetNoteMap(clsNoteMap notemap, int qtime, int notemod) {
      return ((clsNoteMapMidi)notemap)[qtime, notemod, Trk];   //incl. pb notes, current trk
    }

    protected override bool GetNoteMap(clsNoteMap notemap, int qtime, int note, bool mod) {
      return ((clsNoteMapMidi)notemap)[qtime, note, Trk, mod];   //incl. pb notes, current trk
    }
  }

  internal class clsPicNoteMapNM : clsPicNoteMap {
    //* used by frmNoteMap - merge all selected trks
    internal clsPicNoteMapNM(Forms.IShowNoteMap ishownotemap, PictureBox pic, bool weighted) 
      : base(pic, null, true, ishownotemap.BarFont, weighted) {
      //* called from frmNoteMap using FileStream - merge all selected trks
      IShowNoteMap = ishownotemap;
      CFtxt = null;
      //FileStream = frmnm.CSVFileConv;
      //FileName = FileStream.FileName;
      //NoteMap = FileStream.NoteMap;
      //MaxQTime = P.F.MaxQTime;
      //NumTrks = FileStream.NumTrks;
      //QIdd = clsFileStream.QIdd;
      //TicksPerQI = P.F.TicksPerQI;
      SetPicSize(null);   //probably not necessary to do here
    }

    internal clsPicNoteMapNM(Forms.IShowNoteMap ishownotemap, clsCFPC cftxt, PictureBox pic)
      : base(pic, null, true, ishownotemap.BarFont, false) {
      //* called from frmNoteMap using CF (chordfile)
      IShowNoteMap = ishownotemap;
      CFtxt = cftxt;
      //FileName = cftxt.Txtfilename;
      //NoteMap = cftxt.NoteMap;
      //P.F.MaxQTime = maxqtime;
      //maxqtime.MaxQTime = cftxt.MaxQTime;
      //NumTrks = 1;
      //QIdd = clsFileStream.QIdd;
      //TicksPerQI = P.F.TicksPerQI;
      SetPicSize(null);   //probably not necessary to do here
    }

    //internal override Font BarFont {
    //  get {
    //    return ((Forms.frmNoteMap)FontFrm).BarFont;
    //  }
    //}

    protected override clsFileStream FileStream { get {return IShowNoteMap.FileStream; } }
    protected Forms.IShowNoteMap IShowNoteMap;
    protected override int HFactor { get { return  IShowNoteMap.HFactor; } }
    protected override int VFactor { get { return IShowNoteMap.VFactor; } }
    protected override int HDiv { get { return IShowNoteMap.HDiv; } }
    protected override int BarDiv { get { return 1; } }
    protected override bool OneOct {
      get { 
        if (CFtxt != null) return true;  //CF always one oct
        if (Weighted) return true;
        return (IShowNoteMap.OneOct);
      } 
    }
    protected override int Octaves { get { return IShowNoteMap.Octaves; } }
    protected override bool ShowKB { get { return IShowNoteMap.ShowKB; } }
    protected override bool ShowBeats { get { return IShowNoteMap.ShowBeats; } }

    //protected override int GetPW(int qilo, int pwval, int qtime) {
    //  return pwval;
    //}

    protected override int GetPixPerNote(Forms.frmTrackMap frm) { //frm not used here
      if (IShowNoteMap.OneOct) return 0;
      return IShowNoteMap.GetPixPerNoteInt() * (int)IShowNoteMap.nudHeightVal; 
    }

    protected override int GetTrkHeight(Forms.frmTrackMap frm) {  //frm not used here
      return IShowNoteMap.GetTrkHeight(PixPerNote);
    }

    protected override void GetRanges(frmTrackMap frm) {
      MinPitchShow = IShowNoteMap.MinC;  //may be lower than necessary
      MaxPitchShow = IShowNoteMap.MinC + IShowNoteMap.Octaves * 12;  //may be higher than necessary
      PixPerNoteInt = PixPerNote;
      MinC = IShowNoteMap.MinC;  //frmNoteMap
    }

    //protected override void GetMinMaxPitch(out int min, out int max) {
    //  min = IShowNoteMap.MinC;  //may be lower than necessary
    //  max = IShowNoteMap.MinC + IShowNoteMap.Octaves * 12;  //may be higher than necessary
    //}

    //protected override void GetYMinC(Forms.frmTrackMap frm, out int y, out int minc) {
    //  y = PixPerNote;
    //  minc = IShowNoteMap.MinC;  //frmNoteMap
    //}

    protected override bool GetNoteMap(clsNoteMap notemap, int qtime, int notemod) {
      if (Weighted) return notemap.IsF(qtime, notemod);
      //if (notemap is clsNoteMapMidi) return ((clsNoteMapMidi)notemap).GetMapPB(qtime, notemod);
      return notemap[qtime, notemod];
    }

    protected override bool GetNoteMap(clsNoteMap notemap, int qtime, int note, bool mod) {
      //* must be multiple octaves - top pic (Midi)
      return ((clsNoteMapMidi)notemap)[qtime, note, mod];  
    }
  }

  internal abstract class clsPicNoteMap {
    protected static Brush[] TrkBrushes = new Brush[] {
      Brushes.Black,
      Brushes.Red,
      Brushes.Green,
      Brushes.Cyan,
      Brushes.SkyBlue,
      Brushes.Turquoise,
      Brushes.Blue,
      Brushes.Azure,
      Brushes.LightGreen,
      Brushes.Pink,
      Brushes.Orange,
      Brushes.Maroon,
      Brushes.Magenta,
      Brushes.Beige,
      Brushes.Maroon,
      Brushes.RoyalBlue
    };
    internal PictureBox Pic;
    protected clsTrks.T Trk;  //multimap/frmSC only
    protected bool ShowBarNumbers = false;

    //protected Form FontFrm;
    internal Font BarFont;
    //internal Font BarFont = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);

    //protected clsNoteMap NoteMap;
    //protected int MaxQTime;
    //protected int NumTrks;
    //protected string FileName;
    //protected int QIdd;  //quantized to 1/QIdd note (e.g. 1/32 note)
    //internal int TicksPerQI;
    internal clsCFPC CFtxt;

    protected int Width;
    protected int Height;
    protected int BarDivMod;
    protected int PixPerNote;
    protected bool Weighted;
    protected int MinPitchShow, MaxPitchShow, PixPerNoteInt, MinC;

    protected abstract int HFactor { get; }
    protected abstract int VFactor { get; }
    protected abstract int HDiv { get; }
    protected abstract int BarDiv { get; }
    protected abstract bool OneOct { get; }
    protected abstract bool ShowKB { get; }
    protected abstract bool ShowBeats { get; }
    //protected abstract int GetPW(int qilo, int pwval, int qtime);
    protected abstract clsFileStream FileStream { get; }
    protected abstract int GetPixPerNote(Forms.frmTrackMap frm);
    protected abstract int GetTrkHeight(Forms.frmTrackMap frm);
    protected abstract int Octaves { get; }
    protected abstract void GetRanges(Forms.frmTrackMap frm);
    //protected abstract void GetMinMaxPitch(out int min, out int max);
    //protected abstract void GetYMinC(Forms.frmTrackMap frm, out int y, out int minc);
    protected abstract bool GetNoteMap(clsNoteMap notemap, int qtime, int notemod);
    protected abstract bool GetNoteMap(clsNoteMap notemap, int qtime, int note, bool mod);

    protected clsPicNoteMap(PictureBox pic, clsTrks.T trk, bool firsttrk, Font barfont, bool weighted) {
      Weighted = weighted;
      Pic = pic;
      Trk = trk;
      //BarFont = null;
      if (firsttrk) BarFont = barfont;
      //if (firsttrk) BarFont = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);
    }

    internal void PaintMap(clsNoteMap notemap, Forms.frmTrackMap frm, Graphics xgr, int pixlo, int pixhi, clsMTime mtime) {
      //* frm = frmMultiMap if multimap and showing multiple octaves
      //* bardiv=0: don't show bars
      //if (Pic.Name == "picNoteMapCF") Debugger.Break();
      //pixlo = 0;
      //pixhi = NoteMap.GetLengthQTime();
      //Brush brushkb = new SolidBrush(Color.FromArgb(200, 200, 200));
      Brush brushkb = P.ColorsNoteMap["Keyboard"].Br;
      int qilo = (pixlo * HDiv) / HFactor;
      int qihi = (pixhi * HDiv) / HFactor;
      if (qihi > P.F.MaxBBT.QI) qihi = P.F.MaxBBT.QI;
      //SetPicSize(frm);  //call moved to frmMultiMap & frmNoteMap
      //if (!Loc.IsEmpty) Pic.Location = Loc;
      Pen penline = new Pen(Color.Black);

      if (ShowBarNumbers) {
        int y = BarFont.Height;
        xgr.DrawLine(penline, 0, y, Width, y);   //start
      }
      //* draw keyboard
      if (ShowKB) {
        int hi = 12;
        if (!OneOct) {
          hi = Octaves * 12;
          //if (frm != null) hi = frm.Octaves[Trk] * 12;  //frmMultiMap
          //else hi = P.F.frmNoteMap.Octaves * 12;  //frmNoteMap
        }
        for (int o = 0; o < hi; o += 12) {
          int kbxlo = pixlo, kbxhi = pixhi;
          int[] offset = new int[] { 1, 3, 6, 8, 10 };
          xgr.FillRectangle(brushkb, kbxlo, GetY(o + offset[0], PixPerNote), kbxhi, PixPerNote);  //C#
          xgr.FillRectangle(brushkb, kbxlo, GetY(o + offset[1], PixPerNote), kbxhi, PixPerNote);  //E-
          //xgr.DrawLine(penline, kbxlo, GetY(o+4), kbxhi, GetY(4));   //E|F
          xgr.FillRectangle(brushkb, kbxlo, GetY(o + offset[2], PixPerNote), kbxhi, PixPerNote);  //F#
          xgr.FillRectangle(brushkb, kbxlo, GetY(o + offset[3], PixPerNote), kbxhi, PixPerNote);  //G#
          xgr.FillRectangle(brushkb, kbxlo, GetY(o + offset[4], PixPerNote), kbxhi, PixPerNote);  //B-
        }
      }

      //* draw notes
      //Color color = Color.Black;
      int pwval = 8192;
      //for (int qtime = 0; qtime < NoteMap.GetLengthQTime(); qtime++) {
      for (int qtime = qilo; qtime < qihi; qtime++) {
        //if (qtime >= notemap.GetLengthQTime()) break;
        if (qtime >= P.F.MaxBBT.QI) break;
        //pwval = GetPW(qilo, pwval, qtime);
        //if (frm != null) pwval = ((clsNoteMapMidi)notemap).PB[Trk][qtime];
        if (frm != null) pwval = ((clsNoteMapMidi)notemap).GetPB(Trk, qtime);
        //if (pwval != 8192) color = Color.SkyBlue; else color = Color.Black;
        DrawChord(notemap, xgr, frm, pwval, qtime, HDiv, PixPerNote);
      }

      //draw bars
      // height was GetY(-1, pixpernote);
      DrawBars(BarFont, xgr, Height, ShowBeats, HFactor, HDiv, BarDivMod, qilo, qihi, mtime);
    }

    internal Size SetPicSize(Forms.frmTrackMap frm) {
      PixPerNote = VFactor;
      if (!OneOct) PixPerNote = GetPixPerNote(frm);
      if (PixPerNote == 0) {
        PixPerNote = 1;
        Pic.BackColor = Color.LightGray;
      } else {
        Pic.BackColor = Color.White;
      }
      BarDivMod = BarDiv;
      if (ShowBeats && BarDiv != 1) BarDivMod = 1;
      ShowBarNumbers = (BarDiv > 0 && BarFont != null);
      if (OneOct) {
        Height = 12 * PixPerNote;
      } else {
        Height = GetTrkHeight(frm);
      }
      if (ShowBarNumbers) Height += BarFont.Height;
      Width = (P.F.MaxBBT.QI * HFactor) / HDiv;
      Size size = new Size(Width, Height);
      Pic.ClientSize = size;
      GetRanges(frm);
      //if (frm != null && Trk == frm.RecTrk) Debugger.Break();
      return size;
    }

    internal void DrawBars(Font barfont, Graphics xgr, int height, 
      bool showbeats, int hfactor, int hdiv, int bardiv, int qilo, int qihi, clsMTime mtime) {
      //return maxbarnum
      if (bardiv <= 0) return;
      Brush penbrush = new SolidBrush(Color.Black);
      Pen penbar = new Pen(penbrush, 2);
      Pen penbeat = new Pen(penbrush, 1);
      Pen pen;
      int barnum = -1;

      int qtimebardiv = -1;  //barnum of nearest previous displayable barnum
      int shownum = -1;
      string showstring = "";
      for (int beat = 0; ; beat++) {
        pen = penbeat;
        clsMTime.clsBBT bbt = new clsMTime.clsBBT(mtime, beat, true);
        int qtime = (bbt.Ticks * P.F.QIPerNote) /(4 * P.F.MTime.TicksPerQNote);
        if (qtime < qilo) continue;
        if (qtime > qihi) break;
        if (qtime > P.F.MaxBBT.QI) break;
        bool showline = true;
        barnum = bbt.Bar;
        if (bbt.BeatsRemBar == 0) {  //first beat
          if (barnum % bardiv == 0) {  //barnum display option 
            pen = penbar;
            qtimebardiv = qtime;
            shownum = barnum + 1;
          }
        } else {
          if (!showbeats) showline = false;
        }
        if (showline) xgr.DrawLine(pen, (qtime * hfactor) / hdiv, 0, (qtime * hfactor) / hdiv, height);

        SolidBrush fontbrush = new SolidBrush(Color.Black);  //default
        if (barfont != null && bbt.BeatsRemBar == 0 /*&& barnum % bardiv == 0*/) {
          //if (Pic.Name == "picNoteMapFile") {
          //  char sectname = P.F.CF.QSections.GetName(qtime);
          //  if (sectname != '*') {
          //    fontbrush = new SolidBrush(P.F.CF.QSections.GetColor(sectname));
          //  }
          //} else if (Pic.Name == "pic1" || Pic.Name == "pic2") {  //frmCompareNM
          //  char name = P.F.CF.QSections.FirstBarName(bbt);
          //  if (name != '*') showstring = name.ToString(); else showstring = "";
          //}
          if (qtimebardiv < 0) {
            int barnumdiv = (barnum / bardiv) * bardiv;
            clsMTime.clsBBT bbtbardiv = new clsMTime.clsBBT(mtime, barnumdiv, 0, 0);
            qtimebardiv = (bbtbardiv.Ticks * P.F.QIPerNote) /(4 * P.F.MTime.TicksPerQNote);
            if (shownum < 0) shownum = barnumdiv + 1;
          }

          string shownumstring = (showstring == "") ? shownum.ToString() : showstring;
          xgr.DrawString(shownumstring, barfont, fontbrush, (qtimebardiv * hfactor) / hdiv, 0);
        }
      }
    }

    private Brush BrushPW = new SolidBrush(Color.SkyBlue);  //pitch bend
    private Brush BrushNoPW = new SolidBrush(Color.Black);  //pitch bend

    private static class clsChBrush {
      //internal static Brush Pink { get { return P.ColorsNoteMap["Potential Chord"].Br; } }
      internal static Brush Old0 { get { return P.ColorsNoteMap["Common Chord"].Br; } }
      //internal static Brush Old1 { get { return P.ColorsNoteMap["Uncommon Chord"].Br; } }
      //internal static Brush NewNotChorded { get { return P.ColorsNoteMap["Not Chorded"].Br; } }
      //internal static Brush NewFail { get { return P.ColorsNoteMap["Chording Fail"].Br; } }
      //internal static Brush NewNoMatch { get { return P.ColorsNoteMap["Chording NoMatch"].Br; } }
      //internal static Brush NewMatch { get { return P.ColorsNoteMap["Chording Match"].Br; } }
      //internal static Brush NewDefault { get { return P.ColorsNoteMap["Chording Default"].Br; } }

      internal static Brush Green = new SolidBrush(Color.Green);  //matchchordfile option: in CF chord but not in this notemap
      internal static Brush Red = new SolidBrush(Color.Red);  //matchchordfile option: in CF chord and this notemap
      private static Brush OldX = new SolidBrush(Color.Black); //should not happen
      //internal static Brush Black = P.ColorsNoteMap["Default Chord"].Br;  //default (no chord/show "black" only)
      internal static Brush Black = new SolidBrush(Color.Black);  //default (no chord/show "black" only)

      /*
      internal static Brush Black = new SolidBrush(Color.Black);  //default (no chord/show "black" only)
      //internal static Brush Orange = new SolidBrush(Color.Orange);
      internal static Brush Pink = new SolidBrush(Color.Pink);  //potential chord (chweights)
      internal static Brush Green = new SolidBrush(Color.Green);  //matchchordfile option: in CF chord but not in this notemap
      internal static Brush Red = new SolidBrush(Color.Red);  //matchchordfile option: in CF chord and this notemap
 
      private static Brush Old0 = new SolidBrush(Color.Red);  //rank 0 - common chord
      private static Brush Old1 = new SolidBrush(Color.Yellow); //rank 1 - less common chord
      private static Brush OldX = new SolidBrush(Color.Black); //should not happen

      private static Brush NewNotChordinated = new SolidBrush(Color.Black);
      private static Brush NewFail = new SolidBrush(Color.Pink);
      private static Brush NewNoMatch = new SolidBrush(Color.LightBlue);
      private static Brush NewMatch = new SolidBrush(Color.Blue);
      private static Brush NewDefault = new SolidBrush(Color.Black);
      */
      //internal static Brush GetOldBrush(int rank) {
      //  switch (rank) {
      //    case 0:
      //      return Old0;
      //    case 1:
      //      return Old1;
      //    default:
      //      return OldX;
      //  }
      internal static Brush GetOldBrush() {
        return Old0;
      }

      //internal static Brush GetNewBrush(ChordAnalysis.eStatus status) {
      //  switch (status) {
      //    case ChordAnalysis.eStatus.NotChordinated: return NewNotChorded;
      //    case ChordAnalysis.eStatus.Fail: return NewFail;
      //    case ChordAnalysis.eStatus.Nomatch: return NewNoMatch;
      //    case ChordAnalysis.eStatus.Match: return NewMatch;
      //    default: return NewDefault;
      //  }
      //}
    }

    private void DrawChord(clsNoteMap notemap, Graphics xgr, Forms.frmTrackMap frm, int pwval, int qtime,
      int hdiv, int vfactor) {

      string name = Pic.Name;
      //int trk = Trk;
      int[] chweights = null;
      int maxchweight = 0;
      Brush brush;
      if (pwval != 8192) brush = BrushPW; else brush = BrushNoPW;
      int noteborder = (ShowKB) ? vfactor / 3 : 0;  //vertical pixels not coloured in (to allow black keyboard notes to be seen)

      if (Trk == null) {  //all channels (not multimap)
        //* get chord colours (int[12] chweights)
        bool[] boolmap;
        if (Weighted) boolmap = notemap.IsF(qtime); else boolmap = notemap[qtime];
        ChordDB.clsDesc desc = ChordDB.GetChord(boolmap);
        if (desc != null) {  //matches chord exactly
          //brush = clsChBrush.GetOldBrush(desc.Rank);
          brush = clsChBrush.GetOldBrush();
        } else {  //no match - show note weights
          chweights = ChordDB.GetChordWeights(boolmap);
          maxchweight = chweights.Max();
        }
      }

      int width = Math.Max(1, HFactor / hdiv);
      if (!OneOct) {  //frmMultiMap or notemap.midi
        //* calc note display limits
        //int minpitch, maxpitch;
        //GetMinMaxPitch(out minpitch, out maxpitch);
        //int y, minc;
        //GetYMinC(frm, out y, out minc);

        //* draw note if notemap[..] true
        for (int note = MinPitchShow; note <= MaxPitchShow; note++) {
          if (!GetNoteMap(notemap, qtime, note, false)) continue;
          int notemod = note.Mod12();
          if (Pic.Name == "picNoteMapMidi") {
            //brush = Brushes.Black;
            if (P.F.CF != null && P.F.CF.NoteMap[qtime, note.Mod12()]) {
              brush = P.ColorsNoteMap["Note Match"].Br;
            } else {
              brush = P.ColorsNoteMap["Note No Match"].Br;
            }
          } else if (Pic == P.frmSC.picNoteMap) {
            for (int i = 0; i < P.frmSC.MapTrks.Count; i++) {
              clsTrks.T trk = P.frmSC.MapTrks[i];
              int trknum = trk.TrkNum;
              if (trknum > 15) trknum %= 16;
              if (((clsNoteMapMidi)notemap)[qtime, note, trk, false]) {
                brush = TrkBrushes[i];
                xgr.FillRectangle(brush, (qtime * HFactor) / hdiv, GetY(note - MinC, PixPerNoteInt), width, vfactor);
              }
            }
            continue;
          } else {
            brush = GetNoteBrush(chweights, maxchweight, brush, notemod, notemap, qtime);
          }
          if (frm?.NoteMap?.Delete != null  //trackmap
          && Trk == frm.MouseTrk
          && frm.NoteMap.Delete[qtime, note]) brush = Brushes.Red;
          xgr.FillRectangle(brush, (qtime * HFactor) / hdiv, GetY(note - MinC, PixPerNoteInt), width, vfactor);
        }
      } else {  //oneoct - called from frm NoteMap (any) or frmMultiMap
        //* draw note if notemap[..] true
        for (int notemod = 0; notemod < 12; notemod++) {
          if (!GetNoteMap(notemap, qtime, notemod)) continue;
          if (Pic.Name == "picNoteMapMidi") {
            brush = Brushes.Black;
            if (P.F.CF != null && P.F.CF.NoteMap[qtime, notemod]) {
              brush = P.ColorsNoteMap["Note Match"].Br;
            } else {
              brush = P.ColorsNoteMap["Note No Match"].Br;
            }
          } else {
            brush = GetNoteBrush(chweights, maxchweight, brush, notemod, notemap, qtime);
          }
          xgr.FillRectangle(brush, (qtime * HFactor) / hdiv, GetY(notemod, vfactor) + noteborder, width, vfactor - 2 * noteborder);
        }
        if ((Pic.Name == "picNoteMapMidi" || Pic.Name == "picNoteMapQuant") 
          && (P.F.frmChordMapAdv != null && P.F.frmChordMapAdv.optChordMatch.Checked)) {
          //* check if pitchclass (pc) present in notemapcf, but not in pic
          for (int pc = 0; pc < 12; pc++) {
            if (P.F.CF.NoteMap[qtime, pc] && !GetNoteMap(notemap, qtime, pc)) {
              Brush gbrush = clsChBrush.Green;
              xgr.FillRectangle(gbrush, (qtime * HFactor) / hdiv, GetY(pc, vfactor) + noteborder, width, vfactor - 2 * noteborder);
            }
          }
        }
      }
    }

    private Brush GetNoteBrush(int[] chweights, int maxchweight, Brush brush, int notemod, clsNoteMap notemap, int qtime) {
      if (Pic.Name != "picNoteMapMidi" && Pic.Name != "picNoteMapQuant" && Pic.Name != "picNoteMapFile") {
        return brush;
      } else if (P.F.frmChordMapAdv != null && P.F.frmChordMapAdv.optChordBlack.Checked) {
        return clsChBrush.Black;
      } else if (Pic.Name != "picNoteMapFile" && (P.F.frmChordMapAdv != null && P.F.frmChordMapAdv.optChordMatch.Checked)) {
        return (P.F.CF.NoteMap[qtime, notemod]) ? clsChBrush.Red : clsChBrush.Black;
      //} else if (!P.F.frmNoteMapAdv.optChordBlack.Checked) {
      } else {
        if (chweights != null && chweights[notemod] > 0) {
          //* show weight -> black or red
          //if (chweights[notemod] == maxchweight) brush = clsChBrush.Pink;  //part of potential chord
          //else brush = clsChBrush.Black;  //not part of potential chord
          brush = clsChBrush.Black;  
        }
        return brush;
        //* show weight -> fromarg (green)
        //* int b = (chweights[note] * 255) / maxchweight;
        //* brush = new SolidBrush(Color.FromArgb(0, b, 0));  //shades of green
      } 
      //else {
      //  LogicError.Throw(eLogicError.X076);
      //  return clsChBrush.Black;
      //}
    }

    private int GetY(int note, int vfactor) {  //return top of note rectangle
      return (Pic.ClientSize.Height - (note + 1) * vfactor);
    }
  }
}
