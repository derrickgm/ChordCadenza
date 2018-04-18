using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ChordCadenza.Forms {
  public partial class frmTonnetz : Form {
    private class clsXYPC {
      internal int XPos;  //centre of circle
      internal int YPos;  //centre of circle
      internal int X;
      internal int Y;
      internal int PC;

      internal clsXYPC(int x, int y, int xpos, int ypos, int pc) {
        X = x;
        Y = y;
        XPos = xpos;
        YPos = ypos;
        PC = pc;
      } 
    }

    internal clsPlay.clsChordEv PlayChord = null;
    private int HalfSpace = 50;  //should be even
    //private int HalfSpaceSquared; 
    private int Depth = 1;
    private int Offset = 8;
    private int CirclePenWidth = 1;
    private int Radius;
    private int RadiusSquared;
    private int Space;
    private int Diameter;
    private List<List<clsXYPC>> XYPC = new List<List<clsXYPC>>();  //[y][x]
    private Dictionary<int, clsXYPC> DictXYPC = new Dictionary<int, clsXYPC>(12);
    private Font DiatonicFont = new Font("Arial", 16, FontStyle.Bold);
    private Font ChromaticFont = new Font("Arial", 12, FontStyle.Regular);
    private Brush DiatonicFontBrush = new SolidBrush(Color.Black);
    private Brush ChromaticFontBrush = new SolidBrush(Color.Red);
    private delegate void delegRefresh();
    private delegRefresh dRefresh;
    private Pen CirclePen;
    private clsKey Key;
    private bool[] ScaleNotes = new bool[12];
    private Brush ActiveBrush = Brushes.LightGray;
    private Brush InactiveBrush = Brushes.White;
    private Color PlayColor = Color.Green;
    private Brush PlayBrush;
    private Brush BullsEyeBrush = Brushes.LightGray; 
    private bool[][] Active;  //[depth][PC]: current playchord from NoteMap
    private bool[] ActiveBoolChord = new bool[12];  //only set on MouseDown outside nodes
    private readonly bool[] NullBoolChord = new bool[12];
    //private readonly int BeatWidth = 4;  //bullseye

    public frmTonnetz(Form frm) {
      InitializeComponent();
      //HalfSpaceSquared = HalfSpace * HalfSpace;
      Diameter = HalfSpace;
      Radius = Diameter / 2;
      RadiusSquared = Radius * Radius;
      CirclePen = new Pen(Color.Black, CirclePenWidth);
      dRefresh = new delegRefresh(picPC.Refresh);
      PlayBrush = new SolidBrush(PlayColor);
      lblNewChord.ForeColor = PlayColor;
      lblNewChordLit.ForeColor = PlayColor;

      //Show(frm);
      Show();
    }

    private void frmTonnetz_Load(object sender, EventArgs e) {
      CreateNodes();
    }

    internal void CreateNodes() {
      Space = HalfSpace * 2;
      int pc = 0;
      int y = 0;
      int ypos = HalfSpace;
      XYPC.Clear();
      DictXYPC.Clear();


      //* initialize PC and DictPC
      int xpos = HalfSpace;
      while (ypos < picPC.Height - HalfSpace) {
        List<clsXYPC> listx = new List<clsXYPC>();
        int x = 0;
        while (xpos < picPC.Width - HalfSpace) {
          clsXYPC xypc = new clsXYPC(x, y, xpos, ypos, pc);
          listx.Add(xypc);
          x++;
          xpos += Space;
          pc = (pc + 7).Mod12();  //circle of fifths
          if (!DictXYPC.ContainsKey(pc)) DictXYPC.Add(pc, xypc); 
        }
        XYPC.Add(listx);
        x = 0;
        if (y % 2 == 0) {
          xpos = Space;
          pc = (listx[0].PC + 3).Mod12();  //minor sixth
        } else {
          xpos = HalfSpace;  //starting position
          pc = (listx[0].PC + 8).Mod12();  //minor third
        }
        y++;
        ypos += Space;
      }

      //* set key
      Key = (P.F.MTime == null) ? new clsKey(0, "major", 0) : P.F.Keys[P.F.CurrentBBT.Ticks];
      bool[] scalenotes = (Key.Major) ? NoteName.MajScaleNotes : NoteName.MinScaleNotesDown;
      ScaleNotes = new bool[12];
      for (int i = 0; i < 12; i++) {
        ScaleNotes[(i + Key.KBTrans_KeyNote).Mod12()] = scalenotes[i];
      }

      picPC.Refresh();
    }

    private void picPC_Paint(object sender, PaintEventArgs e) {
      Graphics xgr = e.Graphics;
      xgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      SizeF size = new SizeF(Diameter, Diameter);
      Key = (P.F.MTime == null) ? new clsKey(0, "major", 0) : P.F.Keys[P.F.CurrentBBT.Ticks];

      //* show nodes
      ShowNodes(xgr, size);

      //* check if bullseyes should be shown
      if (nudLookAhead.Value > 0 && P.F.CF != null) ShowBullsEyes(xgr, size);

      //* drawstring solfa or note
      ShowNames(xgr, size);

      //* show chords
      ShowChords();
    }

    private void ShowChords() {
      //* show thischord and next chord
      //* newchord showed in MouseDown event
      if (P.F.CF?.Evs == null || P.F.CF.Evs.Count == 0) {
        lblThisChord.Text = "null";
        lblNextChord.Text = "null";
        return;
      }

      int index = P.F.CF.FindCFEv(P.F.CurrentBBT);  //this or prev
      clsCF.clsEv ev = P.F.CF.Evs[index];
      lblThisChord.Text = (P.frmStart.chkShowChordsRel.Checked) ? 
        ev.ChordNameRoman(Key) : ev.ChordName();

      index--;
      if (index >= 0) {
        ev = P.F.CF.Evs[index];
        lblNextChord.Text = (P.frmStart.chkShowChordsRel.Checked) ?
          ev.ChordNameRoman(Key) : ev.ChordName();
      }

      lblKey.Text = Key.ToString();
    }

    private void ShowNames(Graphics xgr, SizeF size) {
      StringFormat fmt = new StringFormat();
      fmt.Alignment = StringAlignment.Center;  //horizontal
      fmt.LineAlignment = StringAlignment.Center;  //vertical
      for (int y = 0; y < XYPC.Count; y++) {
        for (int x = 0; x < XYPC[y].Count; x++) {
          ShowName(xgr, size, fmt, y, x);
        }
      }
    }

    private void ShowName(Graphics xgr, SizeF size, StringFormat fmt, int y, int x) {
      int pc = XYPC[y][x].PC;
      string sf = NoteName.GetNoteNameOrSolfa(pc, P.F.Keys[P.F.CurrentBBT.Ticks]);
      Font font = (ScaleNotes[pc]) ? DiatonicFont : ChromaticFont;
      Brush fontbrush = (ScaleNotes[pc]) ? DiatonicFontBrush : ChromaticFontBrush;
      clsXYPC xypc = XYPC[y][x];
      PointF loc = new PointF(xypc.XPos - Radius, xypc.YPos - Radius);
      RectangleF rectf = new RectangleF(loc, size);
      //xgr.DrawRectangle(new Pen(Color.Green, 3), loc.X, loc.Y, size.Width, size.Height);
      xgr.DrawString(sf, font, fontbrush, rectf, fmt);
    }

    private void ShowNodes(Graphics xgr, SizeF size) {
      for (int y = 0; y < XYPC.Count; y++) {
        for (int x = 0; x < XYPC[y].Count; x++) {
          clsXYPC xypc = XYPC[y][x];
          PointF loc = new PointF(xypc.XPos - Radius, xypc.YPos - Radius);
          ShowNode(xgr, xypc.PC, loc, size);
        }
      }
    }

    private void ShowNode(Graphics xgr, int pc, PointF loc, SizeF size) {
      //* called from paint event
      GetActiveNotes();
      Brush brush = InactiveBrush;
      for (int d = Depth - 1; d >= 0; d--) {  //d==0 is current
        int off = Offset * d;
        loc = new PointF(loc.X - off, loc.Y - off); 
        RectangleF rectf = new RectangleF(loc, size);
        brush = (Active[d][pc]) ? ActiveBrush : InactiveBrush;
        if (ActiveBoolChord[pc]) brush = PlayBrush;  //temp?
        xgr.FillEllipse(brush, rectf);
        xgr.DrawEllipse(CirclePen, rectf);
      }
    }

    private void ShowBullsEyes(Graphics xgr, SizeF size) {
      //* show bullseye countdown using nudLookAhead
      clsMTime.clsBBT bbt = P.F.CurrentBBT.Copy();
      int qi = bbt.Ticks / P.F.TicksPerQI;
      bool[] currentchord = P.F.CF.NoteMap[qi];
      int lookahead = (int)nudLookAhead.Value;
      for (int i = 0; i < lookahead; i++) {
        bbt.NextBeat();
        if (bbt.Ticks >= P.F.MaxTicks) break;
        qi = bbt.Ticks / P.F.TicksPerQI;
        bool[] chord = P.F.CF.NoteMap[qi];
        if (chord.SequenceEqual(currentchord)) continue;
        if (chord.SequenceEqual(NullBoolChord)) continue;

        //* draw bullseyes
        int outerradius = Radius + lookahead * (int)nudBeatWidth.Value;
        int outerdiameter = 2 * outerradius;
        for (int y = 0; y < XYPC.Count; y++) {
          for (int x = 0; x < XYPC[y].Count; x++) {
            clsXYPC xypc = XYPC[y][x];
            if (!chord[xypc.PC]) continue;  //only show if in new chord

            int innerradius = Radius + i * (int)nudBeatWidth.Value;
            int innerdiameter = innerradius * 2;

            PointF outerloc = new PointF(xypc.XPos - outerradius, xypc.YPos - outerradius);
            SizeF outersize = new SizeF(outerdiameter, outerdiameter);
            RectangleF outerrectf = new RectangleF(outerloc, outersize);

            PointF innerloc = new PointF(xypc.XPos - innerradius, xypc.YPos - innerradius);
            SizeF innersize = new SizeF(innerdiameter, innerdiameter);
            RectangleF innerrectf = new RectangleF(innerloc, innersize);

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(innerrectf);
            Region region = new Region(path);
            xgr.ExcludeClip(region);
            xgr.FillEllipse(BullsEyeBrush, outerrectf);
            xgr.ResetClip();
          }
        }
        break;
      }
    }

    private void GetActiveNotes() {
      //* called from ShowNode()
      Active = new bool[Depth][];
      if (P.F.CF == null) return;
      clsMTime.clsBBT bbt = P.F.CurrentBBT.Copy();
      //for (int i = 0; i < (int)nudLookAhead.Value; i++) bbt.NextBeat(); 
      for (int d = 0; d < Depth; d++) {
        int qi = bbt.Ticks / P.F.TicksPerQI;
        Active[d] = P.F.CF.NoteMap[qi];
        bbt.NextBeat();
        if (bbt.Ticks >= P.F.MaxTicks) break;
      }
    }

    private void frmTonnetz_FormClosed(object sender, FormClosedEventArgs e) {
      P.F.frmTonnetz = null;
    }

    internal void NewCurrentBBT() {
      //* called from any thread
      BeginInvoke(dRefresh);
    }

    private void picPC_Resize(object sender, EventArgs e) {
      CreateNodes();
    }

    private void cmdThicker_Click(object sender, EventArgs e) {
      CirclePenWidth++;
      if (CirclePenWidth > Radius) CirclePenWidth = Radius;
      CirclePen = new Pen(Color.Black, CirclePenWidth);
      picPC.Refresh();
    }

    private void cmdThinner_Click(object sender, EventArgs e) {
      CirclePenWidth--;
      if (CirclePenWidth < 1) CirclePenWidth = 1;
      CirclePen = new Pen(Color.Black, CirclePenWidth);
      picPC.Refresh();
    }

    private void cmdSpaceMore_Click(object sender, EventArgs e) {
      HalfSpace++;
      //HalfSpaceSquared = HalfSpace * HalfSpace;
      CreateNodes();
    }

    private void cmdSpaceLess_Click(object sender, EventArgs e) {
      HalfSpace--;
      if (HalfSpace < 1) HalfSpace = 1;
      //HalfSpaceSquared = HalfSpace * HalfSpace;
      CreateNodes();
    }

    private void cmdDiameterMore_Click(object sender, EventArgs e) {
      Diameter += 2;
      Radius = Diameter / 2;
      RadiusSquared = Radius * Radius;
      CreateNodes();
    }

    private void cmdDiameterLess_Click(object sender, EventArgs e) {
      Diameter -= 2;
      if (Diameter < 4) Diameter = 4;
      Radius = Diameter / 2;
      RadiusSquared = Radius * Radius;
      CreateNodes();
    }

    private void cmdDepthMore_Click(object sender, EventArgs e) {
      Depth++;
      CreateNodes();
    }

    private void cmdDepthLess_Click(object sender, EventArgs e) {
      Depth--;
      if (Depth < 1) Depth = 1;
      CreateNodes();
    }

    private void cmdOffsetMore_Click(object sender, EventArgs e) {
      Offset++;
      CreateNodes();
    }

    private void cmdOffsetLess_Click(object sender, EventArgs e) {
      Offset--;
      if (Offset < 1) Offset = 1;
      CreateNodes();
    }

    private void picPC_MouseDown(object sender, MouseEventArgs e) {
      //* get distance from any node where diffx and diffy <= 
      List<double> keylist = new List<double>();
      List<clsXYPC> valuelist = new List<clsXYPC>();

      //* get list of possible nodes
      foreach (List<clsXYPC> list in XYPC) {
        foreach (clsXYPC xypc in list) {
          int diffx = Math.Abs(xypc.XPos - e.X);
          int diffy = Math.Abs(xypc.YPos - e.Y);
          if (diffx <= Space && diffy <= Space) {
            double diagsquared = (double)(diffx * diffx + diffy * diffy);
            if (diagsquared < RadiusSquared) {  //inside node - add to chord
              NodeMouseDown(e, xypc);
              return;
            }
            keylist.Add(diagsquared);
            valuelist.Add(xypc);
          }
        }
      }

      if (keylist.Count < 3) return;  //too near edge?
      double[] keyarray = keylist.ToArray();
      clsXYPC[] valuearray = valuelist.ToArray();
      Array.Sort(keyarray, valuearray);
      int[] chord = new int[3];  //triad
      for (int i = 0; i < 3; i++) {
        int pc = valuearray[i].PC;
        chord[i] = pc;
        ActiveBoolChord[pc] = true;  //temp?
      }

      ShowPlayChord(chord);
    }

    private void ShowPlayChord(int[] chord) {
      PlayChord = new clsPlay.clsChordEv(P.frmSC.Play, chord, true);
      //string txt = clsManChords.ShowChordText(chord, Key);
      //lblNewChord.Text = (txt == "") ? "null" : txt;
      ChordDB.clsDesc desc = ChordDB.GetChord(ActiveBoolChord);
      if (desc == null) {
        lblNewChord.Text = "xxx";
      } else {
        string name = NoteName.GetNoteNameOrRoman(desc.Root, Key);
        lblNewChord.Text = name + desc.Qualifier;
      }
      picPC.Refresh();
    }

    private void NodeMouseDown(MouseEventArgs e, clsXYPC xypc) {
      //* inactive:
      //*   left: add node (max 4), else replace
      //*   right: replace node
      //*   replace node that would make semitone or tone, else use COF
      //* active
      //*   remove node from chord
      //*
      //* Active[0][] = current chord (from NoteMap)
      //* ActiveBoolChord[] = new chord
      List<int> chord = clsCF.clsEv.ConvBoolArrayToListInt(Active[0]);
      ActiveBoolChord = Active[0].ToArray();
      if (Active[0][xypc.PC]) {  //remove hit node from current chord 
        ActiveBoolChord[xypc.PC] = false;
      } else {  //not active
        if (chord.Count > 3 || e.Button == MouseButtons.Right) RemoveWeakestNode(xypc);
        ActiveBoolChord[xypc.PC] = true;
      }
      int[] newchord = clsCF.clsEv.ConvBoolArrayToListInt(ActiveBoolChord).ToArray();
      ShowPlayChord(newchord);
    }

    private void RemoveWeakestNode(clsXYPC xypc) {
      //* remove from ActiveBoolChord
      if (RemoveNodeDiff(xypc, 1)) return;
      if (RemoveNodeDiff(xypc, 2)) return;
      RemoveNodeDia();
    }

    private bool RemoveNodeDiff(clsXYPC xypc, int indiff) {
      //* remove first node that where diff <= indiff
      for (int i = 0; i < 12; i++) {
        if (!ActiveBoolChord[i]) continue;
        int diff = (i - xypc.PC).Mod12();
        if (diff == indiff) {  //first semitone
          ActiveBoolChord[i] = false;
          return true;
        }
      }
      return false;
    }

    private bool RemoveNodeDia() {
      int[] dia = (Key.Major) ? NoteName.MajDia : NoteName.MinDia;
      //* remove least diatonic node
      for (int i = 11; i >= 0; i--) {  //start with least diatonic
        int pc = (dia[i] + Key.KBTrans_KeyNote).Mod12();
        if (ActiveBoolChord[pc]) {
          ActiveBoolChord[pc] = false;
          return true;
        }
      }
      return false;
    }

    private void picPC_MouseUp(object sender, MouseEventArgs e) {
      PlayChord = null;
      ActiveBoolChord = new bool[12];
      lblNewChord.Text = "null";
      picPC.Refresh();
    }
  }
}
