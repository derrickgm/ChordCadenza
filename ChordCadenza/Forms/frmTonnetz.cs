/*
 * current chord: black cross
 * next chord: pink circle (bullseye)
 * right mouse (next chord)
 *  actions NOT reversed on mouse up
 *    click on node: add node
 *    double click on node: add node and remove least harmonic chord note
 *    click off node: new chord (maj/min)
 *    double click off node: new altenate chord (aug/dim)
 *    click ouside picturebox: wipe changes 
 * left mouse (this chord)
 *   same as right mouse
 * 
*/
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
  public partial class frmTonnetz : Form, IFormProjectName, ITT {
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

    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    internal clsPlay.clsChordEv PlayChord = null;
    //private int HalfSpaceSquared; 
    //private int Depth = 1;
    //private int Offset = 8;

    internal static int HalfSpace = 34;  //should be even
    //internal static bool ClickToEmerging = false;
    internal static int LookAhead = 4;
    internal static int CirclePenWidth = 1;
    internal static int Diameter = 58;

    //private Brush DiatonicFontBrush = new SolidBrush(Color.Black);
    //private Brush ChromaticFontBrush = new SolidBrush(Color.Black);
    //private Brush ActiveBrush = Brushes.LightGray;
    //private Brush NewActiveBrush = Brushes.Red;
    //private Brush InactiveBrush = Brushes.White;
    //private Color PlayColor = Color.Green; private Brush PlayBrush;
    //private Brush BackBrush;

    private int Radius;
    private int RadiusSquared;
    private int Space;
    private List<List<clsXYPC>> XYPC = new List<List<clsXYPC>>();  //[y][x]
    private Dictionary<int, clsXYPC> DictXYPC = new Dictionary<int, clsXYPC>(12);
    private Font DiatonicFont = new Font("Arial", 16, FontStyle.Bold);
    private Font ChromaticFont = new Font("Arial", 12, FontStyle.Regular);
    private delegate void delegRefresh();
    private delegRefresh dRefresh;
    private Pen CirclePen;
    private clsKeyTicks Key;
    private bool[] ScaleNotes = new bool[12];
    private bool[] ActiveBoolChordCF;  //[PC]: current playchord from NoteMap
    private bool[] ActiveBoolChordMouse = new bool[12];  //only set on MouseDown
    private bool[] NextBoolChordCF;
    //private bool[] EmergingBoolChord = new bool[12];
    private readonly bool[] NullBoolChord = new bool[12];
    //private readonly int BeatWidth = 4;  //bullseye
    private bool picPC_indMouseDown = false;

    public frmTonnetz(Form frm) {
      InitializeComponent();
      //HalfSpaceSquared = HalfSpace * HalfSpace;
      //Diameter = HalfSpace + 24;
      Radius = Diameter / 2;
      RadiusSquared = Radius * Radius;
      CirclePen = new Pen(Color.Black, CirclePenWidth);
      dRefresh = new delegRefresh(picPC.Refresh);
      //PlayBrush = new SolidBrush(PlayColor);
      //BackBrush = new SolidBrush(picPC.BackColor);
      //lblNewChord.ForeColor = PlayColor;
      //lblNewChordLit.ForeColor = PlayColor;

      //Show(frm);
      Utils.FormAct(this);
    }

    private void frmTonnetz_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      nudLookAhead.Value = LookAhead;
      SetFormTitle();
      Cfg.DictFormProps[Name].SetForm(this);
      if (P.F?.Keys != null) Key = P.F.Keys[0, kbtrans: true];
      CreateNodes();
      picPC.BackColor = P.ColorsTonnetz["Background"].Co;
      clsTT.LoadToolTips(this);
    }

    public void SetFormTitle() {  //interface IFormProjectName
      SetFormTitle(P.F.Project);
    }

    public void SetFormTitle(clsProject project) {  //interface IFormProjectName
      this.Text = "TONNETZ (Tone Diagram): " + project.PathAndName + " - Chord Cadenza";
    }

    internal void CreateNodes() {
      //* always start at C (fixed keyboard allows for modulation)
      //* change solfa during modulation or kbtrans
      //* XYPC.PC = displayed PC - same as frmSC
      Space = HalfSpace * 2;
      //int pc = (Key == null) ? 0 : Key.KeyNote;
      int pc = 0;  //C
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
      Key = (P.F.MTime == null) ? new clsKeyTicks(0, "major", 0) : P.F.Keys[P.F.CurrentBBT.Ticks, kbtrans: true];
      bool[] scalenotes = (Key.Major) ? NoteName.MajScaleNotes : NoteName.MinScaleNotesDown;
      ScaleNotes = new bool[12];
      for (int i = 0; i < 12; i++) {
        //ScaleNotes[(i + Key.KBTrans_KeyNote).Mod12()] = scalenotes[i];
        ScaleNotes[(i + Key.KeyNote).Mod12()] = scalenotes[i];
      }

      picPC.Refresh();
    }

    private void picPC_Paint(object sender, PaintEventArgs e) {
      Graphics xgr = e.Graphics;
      xgr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      SizeF size = new SizeF(Diameter, Diameter);
      //int oldkeynote = Key.KBTrans_KeyNote;
      //Key = (P.F.MTime == null) ? new clsKey(0, "major", 0) : P.F.Keys[P.F.CurrentBBT.Ticks];
      int oldkeynote = Key.KeyNote;
      Key = (P.F.MTime == null) ? new clsKeyTicks(0, "major", 0) : P.F.Keys[P.F.CurrentBBT.Ticks, kbtrans: true];
      int cfindex = P.F.CF.FindCFEv(P.F.CurrentBBT);  //this or prev

      //* recreate nodes if necessary
      //if (Key.KBTrans_KeyNote != oldkeynote) CreateNodes();
      if (Key.KeyNote != oldkeynote) CreateNodes();  //update solfa (and enharmonic notes?)

      //* show nodes
      ShowNodes(xgr, size);

      //* check if bullseyes should be shown
      if (LookAhead > 0 && P.F.CF != null) ShowBullsEyes(xgr, size);

      //* drawstring solfa or note
      ShowNodesTxt(xgr, size);

      //* show chords (name and qualifier)
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
      lblThisChord.Text = (P.frmSC.chkShowChordsRel.Checked) ? 
        ev.ChordNameRoman(Key, eKBTrans.None) : ev.ChordName(eKBTrans.Add, kbtranskey: true);

      index--;
      if (index >= 0) {
        ev = P.F.CF.Evs[index];
        lblNextChord.Text = (P.frmSC.chkShowChordsRel.Checked) ?
          ev.ChordNameRoman(Key, eKBTrans.None) : ev.ChordName(eKBTrans.Add, kbtranskey: true);
      }

      lblKey.Text = Key.ToString();
    }

    private void ShowNodesTxt(Graphics xgr, SizeF size) {
      StringFormat fmt = new StringFormat();
      fmt.Alignment = StringAlignment.Center;  //horizontal
      fmt.LineAlignment = StringAlignment.Center;  //vertical
      for (int y = 0; y < XYPC.Count; y++) {
        for (int x = 0; x < XYPC[y].Count; x++) {
          ShowNodeTxt(xgr, size, fmt, y, x);
        }
      }
    }

    private void ShowNodeTxt(Graphics xgr, SizeF size, StringFormat fmt, int y, int x) {
      clsXYPC xypc = XYPC[y][x];
      int pc = xypc.PC;
      int pcsf = (pc - P.frmSC.Play.TransposeKB).Mod12();
      //int pcshow = (P.frmSC.optShowSolfa.Checked) ? pcsf : pc;
      //* kbtrans applied to key by GetNoteNameOrSolfa
      string txt = NoteName.GetNoteNameOrSolfa(pc, P.F.Keys[P.F.CurrentBBT.Ticks, kbtrans: true]);
      Font font = (ScaleNotes[pc]) ? DiatonicFont : ChromaticFont;
      Brush fontbrush = (ScaleNotes[pc]) ? 
        P.ColorsTonnetz["Diatonic Note"].Br :
        P.ColorsTonnetz["Chromatic Note"].Br;
      PointF loc = new PointF(xypc.XPos - Radius, xypc.YPos - Radius);
      RectangleF rectf = new RectangleF(loc, size);
      //xgr.DrawRectangle(new Pen(Color.Green, 3), loc.X, loc.Y, size.Width, size.Height);
      xgr.DrawString(txt, font, fontbrush, rectf, fmt);
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

    /* temp comment out
    *** 
    private void ShowNode(Graphics xgr, int pc, PointF loc, SizeF size) {
      //* called from paint event
      GetActiveNotes();
      loc = new PointF(loc.X, loc.Y);
      RectangleF rectf = new RectangleF(loc, size);
      Brush brush = (ActiveBoolChordCF[pc]) ? P.ColorsTonnetz["Active Node"].Br : P.ColorsTonnetz["Inactive Node"].Br;
      if (ActiveBoolChordMouse[pc]) brush = P.ColorsTonnetz["Played Note"].Br;
      xgr.FillEllipse(brush, rectf);
      xgr.DrawEllipse(CirclePen, rectf);
    }
    ***
    */

    private void ShowNode(Graphics xgr, int pc, PointF loc, SizeF size) {
      //* called from paint event
      if (!GetActiveNotes()) return;
      loc = new PointF(loc.X, loc.Y);
      RectangleF rectf = new RectangleF(loc, size);
      xgr.DrawEllipse(CirclePen, rectf);

      if (ActiveBoolChordMouse[pc]) {  //fill circle
        xgr.FillEllipse(P.ColorsTonnetz["New Active Node"].Br, rectf);
      }

      if (ActiveBoolChordCF[pc]) {  //draw cross
        float midX = loc.X + size.Width / 2;
        float midY = loc.Y + size.Height / 2;
        Pen crosspen = new Pen(Color.Black, 8);
        xgr.DrawLine(crosspen, loc.X, midY, loc.X + size.Width, midY);  //horizontal line
        xgr.DrawLine(crosspen, midX, loc.Y, midX, loc.Y + size.Height);  //vertical line
      }

      //if (ActiveBoolChordCF[pc]) {  //fill circle
      //  xgr.FillEllipse(P.ColorsTonnetz["Active Node"].Br, rectf);
      //}

      Color innercolor = (NextBoolChordCF[pc]) ? Color.Green : picPC.BackColor;

      //* fill inner circle to stop cross from obliterating text
      float div4 = size.Width / 4;  //width = height 
      float div2 = size.Width / 2;  //width = height 
      RectangleF innerrectf = new RectangleF(loc.X + div4, loc.Y + div4, div2, div2);
      xgr.FillEllipse(new SolidBrush(innercolor), innerrectf);
    }

    private void ShowBullsEyes(Graphics xgr, SizeF size) {
      //EmergingBoolChord = new bool[12];
      //* show bullseye countdown using nudLookAhead
      //int lookahead = (int)nudLookAhead.Value;
      if (LookAhead == 0) return;
      clsMTime.clsBBT bbt = P.F.CurrentBBT.Copy();
      //int qi = bbt.Ticks / P.F.TicksPerQI;
      //bool[] currentchord = P.F.CF.NoteMap[qi];
      bool[] currentchord = ActiveBoolChordCF;

      for (int i = 0; i < LookAhead; i++) {
        bbt.NextBeat();
        if (bbt.Ticks >= P.F.MaxBBT.Ticks) break;
        int qi = bbt.Ticks / P.F.TicksPerQI;
        //bool[] newchord = P.F.CF.NoteMap[qi];
        bool[] newchord = P.F.CF.NoteMap.GetBoolChord(qi, eKBTrans.Add);
        if (newchord.SequenceEqual(currentchord)) continue;

        if (!picPC_indMouseDown) {
          int innerradius = (i == 0) ?
          Radius : ((2 * LookAhead - 1  - i) * Radius) / (2 * LookAhead);
          int innerdiameter = 2 * innerradius;
          SizeF innersize = new SizeF(innerdiameter, innerdiameter);
          for (int y = 0; y < XYPC.Count; y++) {
            for (int x = 0; x < XYPC[y].Count; x++) {
              clsXYPC xypc = XYPC[y][x];
              //* compare without kbtrans applied
              //int pc = (xypc.PC - P.frmSC.Play.TransposeKB).Mod12();
              PointF innerloc = new PointF(xypc.XPos - innerradius, xypc.YPos - innerradius);
              RectangleF innerrectf = new RectangleF(innerloc, innersize);
              if (currentchord[xypc.PC] && !newchord[xypc.PC]) {  //pc only in current chord
                xgr.FillEllipse(P.ColorsTonnetz["Old Active Node"].Br, innerrectf);
              } else if (!currentchord[xypc.PC] && newchord[xypc.PC]) {  //pc only in new chord
                xgr.FillEllipse(P.ColorsTonnetz["New Active Node"].Br, innerrectf);
              }
            }
          }
          break;
        }
      }
    }

    private bool GetActiveNotes() {
      //* called from ShowNode()
      //* kbtrans applied (midi output may be different - same as node PC)
      if (P.F.CF == null) return false;
      if (P.F.CF.Evs.Count == 0) return false;
      clsMTime.clsBBT bbt = P.F.CurrentBBT.Copy();

      ActiveBoolChordCF = new bool[12];
      int index = P.F.CF.FindCFEv(bbt);  //matching or previous (>= 0)
      clsCF.clsEv.clsNote[] notes;
      notes = P.F.CF.Evs[index].Notes;
      foreach (clsCF.clsEv.clsNote n in notes) ActiveBoolChordCF[n.PC[eKBTrans.Add]] = true;

      NextBoolChordCF = new bool[12];  
      if (index < P.F.CF.Evs.Count - 1) {
        notes = P.F.CF.Evs[index + 1].Notes;
        foreach (clsCF.clsEv.clsNote n in notes) NextBoolChordCF[n.PC[eKBTrans.Add]] = true;
      }
      return true;
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

    private void picPC_MouseDown(object sender, MouseEventArgs e) {
      picPC_indMouseDown = true;
      List<int> keylist = new List<int>();
      List<clsXYPC> valuelist = new List<clsXYPC>();

      //* get distance from any node where diffx and diffy <= Space
      int distancesquared;
      foreach (List<clsXYPC> list in XYPC) {
        foreach (clsXYPC xypc in list) {
          int diffx = Math.Abs(xypc.XPos - e.X);
          int diffy = Math.Abs(xypc.YPos - e.Y);
          if (diffx <= Space && diffy <= Space) {
            distancesquared = diffx * diffx + diffy * diffy;
            if (distancesquared < RadiusSquared) {  //inside node - add to chord
              NodeMouseDown(e, xypc);
              return;
            }
            keylist.Add(distancesquared);
            valuelist.Add(xypc);
          }
        }
      }

      //* sort nearest nodes by distance from mouse click
      if (keylist.Count < 3) return;  //too near edge?
      int[] keyarray = keylist.ToArray();  //distancesquared
      clsXYPC[] valuearray = valuelist.ToArray();
      Array.Sort(keyarray, valuearray);

      //* find nearest 3 nodes (triangle - maj, min chords)
      clsXYPC[] chxypc = new clsXYPC[3];
      for (int i = 0; i < 3; i++) {
        int pc = valuearray[i].PC;
        chxypc[i] = valuearray[i];
      }

      //* check if triangle and find apex
      int y0 = chxypc[0].YPos;  //nearest from mouseclick
      int y1 = chxypc[1].YPos;
      int y2 = chxypc[2].YPos;  //furthest from mouseclick
      int apex = -1;
      if (y0 == y1 && y2 != y0) apex = 2;
      else if (y0 == y2 && y1 != y0) apex = 1;
      else if (y1 == y2 && y0 != y1) apex = 0;
      else return;  //not triangle

      int[] chord = null;
      //if (e.Button == MouseButtons.Right) {  //dim, aug
      if (e.Clicks == 2) {  //dim, aug
                                               //* get nearest non-apex node
          int near = (apex == 0) ? 1 : 0;  //nearest index 
        List<int> chlist = new List<int>(4);
        chlist.Add(chxypc[apex].PC);
        chlist.Add(chxypc[near].PC);  //2 nodes on nearest diagonal
        int diff = (chlist[1] - chlist[0]).Mod12();
        if (diff > 6) diff = 12 - diff;
        if (diff != 3 && diff != 4) return;  //not dim or aug chord (should not happen)

        //* add notes to make dim or aug chord
        int pc = chlist[1];
        do {
          pc = (pc + diff).Mod12();
        } while (chlist.Contains(pc));
        chlist.Add(pc);
        if (diff == 3) {  //mi3 -> dim7 chord
          do {
            pc = (pc + diff).Mod12();
          } while (chlist.Contains(pc));
          chlist.Add(pc);
        }
        chord = chlist.ToArray();
      //} else if (e.Button == MouseButtons.Left) {  //maj, min
      } else if (e.Clicks == 1) {  //maj, min
        chord = new int[] { chxypc[0].PC, chxypc[1].PC, chxypc[2].PC };
      } else {
        //LogicError.Throw(eLogicError.X143);
        return;
      }

      if (chord != null) {
        for (int i = 0; i < chord.Length; i++) {
          int pc = chord[i];
          ActiveBoolChordMouse[pc] = true;  //kbtrans applied
        }
        SetPlayChord(chord);
      }
    }

    private void SetPlayChord(int[] chord) {  //chord without kbtrans applied
      if (chord == null) return;
      int[] chordkbtrans = new int[chord.Length];
      for (int i = 0; i < chord.Length; i++) {
        int pc = chord[i];
        int pckbtrans = (pc - P.frmSC.Play.TransposeKB).Mod12();
        chordkbtrans[i] = pckbtrans;
      }
      PlayChord = new clsPlay.clsChordEv(P.frmSC.Play, chordkbtrans, true);

      ChordDB.clsDesc desc = ChordDB.GetChord(ActiveBoolChordMouse);
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
      bool[] boolchord = (e.Button == MouseButtons.Left) ? ActiveBoolChordCF : NextBoolChordCF;
      List<int> chord = clsCF.clsEv.ConvBoolArrayToListInt(boolchord);
      ActiveBoolChordMouse = boolchord.ToArray();
      if (boolchord[xypc.PC]) {  //remove hit node from current chord 
        ActiveBoolChordMouse[xypc.PC] = false;
      } else {  //not active
        if (chord.Count > 3 || e.Clicks == 2) RemoveWeakestNode(xypc);
        ActiveBoolChordMouse[xypc.PC] = true;
      }
      int[] newchord = clsCF.clsEv.ConvBoolArrayToListInt(ActiveBoolChordMouse).ToArray();
      SetPlayChord(newchord);
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
        if (!ActiveBoolChordMouse[i]) continue;
        int diff = (i - xypc.PC).Mod12();
        if (diff > 6) diff = 12 - diff;
        if (diff == indiff) {  //first semitone
          ActiveBoolChordMouse[i] = false;
          return true;
        }
      }
      return false;
    }

    private bool RemoveNodeDia() {
      int[] dia = (Key.Major) ? NoteName.MajDia : NoteName.MinDia;
      //* remove least diatonic node
      for (int i = 11; i >= 0; i--) {  //start with least diatonic
        //int pc = (dia[i] + Key.KBTrans_KeyNote).Mod12();
        int pc = (dia[i] + Key.KeyNote).Mod12();
        if (ActiveBoolChordMouse[pc]) {
          ActiveBoolChordMouse[pc] = false;
          return true;
        }
      }
      return false;
    }

    private void picPC_MouseUp(object sender, MouseEventArgs e) {
      picPC_indMouseDown = false;
      PlayChord = null;
      ActiveBoolChordMouse = new bool[12];
      lblNewChord.Text = "null";
      picPC.Refresh();
    }

    private void nudLookAhead_ValueChanged(object sender, EventArgs e) {
      LookAhead = (int)nudLookAhead.Value;
    }

    //private void chkEmerging_CheckedChanged(object sender, EventArgs e) {
    //  ClickToEmerging = chkEmerging.Checked;
    //}

    private void cmdColours_Click(object sender, EventArgs e) {
      Utils.FormAct(P.ColorsTonnetz.FrmColours);  //show new colours as they are changed
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Utils.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_Tonnetz_Intro.htm");
    }

    private void frmTonnetz_FormClosing(object sender, FormClosingEventArgs e) {
      Cfg.DictFormProps[Name] = new clsFormProps(this);
    }
  }
}
