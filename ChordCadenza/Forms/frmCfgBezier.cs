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

namespace ChordCadenza.Forms {
  public partial class frmCfgBezier : Form, ITT {
    private ToolTip _TT;
    public ToolTip TT {
      get { return _TT; }
      set { _TT = value; }
    }

    //internal static List<clsTT.sTTcmd> TTcmds = new List<clsTT.sTTcmd>();

    private Brush BlueBrush = new SolidBrush(Color.Blue);
    private Brush RedBrush = new SolidBrush(Color.Red);
    private Brush BlackBrush = new SolidBrush(Color.Black);
    private Pen BluePen;
    private Pen RedPen;
    private Pen BlackPen;
    private int Seq;
    private bool indVel;
    private Point P0, P3;  //boundaries of vel curve (used by Paint event)
    private Point P1, P2;  //boundaries of bezier line (used by Paint event)
    private int ActiveCtlPoint = -1;
    private const int HitRadius = 20;
    private const int HitDiameter = 40;
    private readonly int HitRadiusSquared;
    //private readonly int HitDiameter;
    private bool MyMouseDown = false;
    private delegate void delegRefresh();
    private delegRefresh dRefresh;
    private int LastKBVelIn = -1;
    private int LastKBVelOut = -1;
    private const int CrossLen = 20;

    internal const int Dflt1X = 123;
    internal const int Dflt1Y = 123;
    internal const int Dflt2X = 4;
    internal const int Dflt2Y = 4;

    public frmCfgBezier(int seq, bool indvel) {
      InitializeComponent();
      //P.Forms.Add(this);
      Seq = seq;
      indVel = indvel;
      HitRadiusSquared = HitRadius * HitRadius;
      //HitDiameter = 2 * HitRadius;
      dRefresh = new delegRefresh(pic.Refresh);
      SetPoints();
    }

    private void SetPoints() {
      int cfg1x = Cfg.Bezier1X[Seq];
      int cfg1y = Cfg.Bezier1Y[Seq];
      int cfg2x = Cfg.Bezier2X[Seq];
      int cfg2y = Cfg.Bezier2Y[Seq];
      //CfgName = Cfg.BezierName[Seq];
      P0 = new Point(VelToSplineX(0), VelToSplineY(0));
      P1 = new Point(VelToSplineX(cfg1x), VelToSplineY(cfg1y));
      P2 = new Point(VelToSplineX(cfg2x), VelToSplineY(cfg2y));
      P3 = new Point(VelToSplineX(127), VelToSplineY(127));
    }

    private void frmNewCfgBezier_Load(object sender, EventArgs e) {
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      clsTT.LoadToolTips(this);
      BluePen = new Pen(Color.Blue);
      RedPen = new Pen(Color.Red);
      BlackPen = new Pen(Color.Black);
      string txt = "Configure Bezier Curve for ";
      if (indVel) txt += "Keyboard Velocity"; else txt += "Aftertouch";
      Text = txt;
    }

    private void pic_Paint(object sender, PaintEventArgs e) {
      //* pic size 452 x 452
      //* kbvel 100 - 352 (scale: 100 + 127 * 2)

      //* draw bezier
      Graphics xgr = e.Graphics;
      xgr.DrawLine(RedPen, P1, P2);  //line between control points
      int showradius = HitRadius / 2;
      int showdiameter = HitDiameter / 2;
      xgr.FillEllipse(RedBrush, P1.X - showradius, P1.Y - showradius, showdiameter, showdiameter);
      xgr.FillEllipse(RedBrush, P2.X - showradius, P2.Y - showradius, showdiameter, showdiameter);
      xgr.DrawBezier(BlackPen, P0, P1, P2, P3);

      //* draw current vel/atouch
      if (LastKBVelIn > 0 && LastKBVelOut > 0) {
        int x = VelToSplineX(LastKBVelIn);
        int y = VelToSplineY(LastKBVelOut);
        xgr.DrawLine(BluePen, x, y + CrossLen, x, y - CrossLen);
        xgr.DrawLine(BluePen, x + CrossLen, y, x - CrossLen, y);
      }
    }

    internal int VelToSplineX(int vel) {
      //return 100 + 2 * vel;
      return ((pic.Width * vel) / 127);
    }

    internal int VelToSplineY(int vel) {
      //return 354 - 2 * vel;
      return ((127 - vel) * pic.Height) / 127;
    }

    internal int SplineToVelX(int spline) {
      //return (spline - 100) / 2;
      return (127 * spline) / pic.Width;
    }

    internal int SplineToVelY(int spline) {
      //return (354 - spline) / 2;
      return 127 - (127 * spline) / pic.Height;
    }

    private int GetNearestCtlPoint(Point p) {
      //* get nearest control point (1 or 2) to point p
      //* return -1 if too far away

      //* get modulus squared for P1 and P2
      int smod1 = GetModSquared(p, P1);
      int smod2 = GetModSquared(p, P2);

      //* return smallest mod, if less than min val
      int smodmin = Math.Min(smod1, smod2);
      if (smodmin > HitRadiusSquared) return -1;
      return (smodmin == smod1) ? 1 : 2; 
    }

    private int GetModSquared(Point pa, Point pb) {
      int sx = GetLenSquared(pa.X, pb.X);  //x len squared
      int sy = GetLenSquared(pa.Y, pb.Y);  //y len squared
      int smod = sx + sy;  //modulus squared
      return smod;
    }

    private int GetLenSquared(int pa, int pb) {
      int sx = (pa - pb) * (pa - pb);  //len squared
      return sx;
    }

    internal void InvokeRefresh() {
      BeginInvoke(dRefresh);
    }

    internal void Monitor(int inval, int outval, bool indvel) {
      //if (indVel && Seq != Cfg.BezierVel) return;
      if (indVel != indvel) return;
      LastKBVelIn = inval;
      LastKBVelOut = outval;
      P.frmCfgBezier.InvokeRefresh();
    }

    private void CalcCfg() { 
      //* calculate Cfg.Bezier1/2 from control points P1, P2 
      Cfg.Bezier1X[Seq] = SplineToVelX(P1.X);
      Cfg.Bezier1Y[Seq] = SplineToVelY(P1.Y);
      Cfg.Bezier2X[Seq] = SplineToVelX(P2.X);
      Cfg.Bezier2Y[Seq] = SplineToVelY(P2.Y);
      clsMidiInKB.Beziers[Seq].CalcVelocities(Seq);
    }

    private void pic_MouseDown(object sender, MouseEventArgs e) {
      MyMouseDown = true;
      ActiveCtlPoint = GetNearestCtlPoint(new Point(e.X, e.Y));
      //Debug.WriteLine("Nearest Bezier Control Point = " + p);
    }

    private void pic_MouseUp(object sender, MouseEventArgs e) {
      MyMouseDown = false;
      CalcCfg();
    }

    private void pic_MouseMove(object sender, MouseEventArgs e) {
      if (!MyMouseDown || ActiveCtlPoint < 0) return;
      if (e.X < 0 || e.X > pic.Width || e.Y < 0 || e.Y > pic.Height) return;
      Point pt = new Point(e.X, e.Y);
      if (ActiveCtlPoint == 1) P1 = pt; else P2 = pt;
      pic.Refresh();
    }

    private void cmdHelp_Click(object sender, EventArgs e) {
      Help.ShowHelp(this, Cfg.HelpFilePath, HelpNavigator.Topic, "Form_ConfigBezier_Intro.htm");
    }

    private void frmCfgBezier_FormClosed(object sender, FormClosedEventArgs e) {
      P.frmCfgBezier = null;
    }

    private void cmdLoadDflts_Click(object sender, EventArgs e) {
      SetCfgDefaults();
      P1 = new Point(VelToSplineX(Dflt1X), VelToSplineY(Dflt1Y));
      P2 = new Point(VelToSplineX(Dflt2X), VelToSplineY(Dflt2Y));
      clsMidiInKB.Beziers[Seq].CalcVelocities(Seq);
      pic.Refresh();
    }

    internal static void SetCfgDefaults() {
      for (int i = 0; i < 2; i++) {
        Cfg.Bezier1X[i] = Dflt1X;
        Cfg.Bezier1Y[i] = Dflt1Y;
        Cfg.Bezier2X[i] = Dflt2X;
        Cfg.Bezier2Y[i] = Dflt2Y;
      }
    }

    //private void cmdRestore_Click(object sender, EventArgs e) {
    //  Cfg.BezierName[Seq] = CfgName;
    //  txtName.Text = CfgName;
    //  P.frmStart.PopulatecmbBezier();
    //  Cfg.Bezier1X[Seq] = Cfg1X;
    //  Cfg.Bezier1Y[Seq] = Cfg1Y;
    //  Cfg.Bezier2X[Seq] = Cfg2X;
    //  Cfg.Bezier2Y[Seq] = Cfg2Y;
    //  P1 = new Point(VelToSplineX(Cfg1X), VelToSplineY(Cfg1Y));
    //  P2 = new Point(VelToSplineX(Cfg2X), VelToSplineY(Cfg2Y));
    //  MidiPlay.MidiInKB.Beziers[Seq].CalcVelocities(Seq);
    //  pic.Refresh();
    //}

    //private void cmdSave_Click(object sender, EventArgs e) {
    //  //Cfg.BezierName[Seq] = txtName.Text;
    //  //P.frmStart.PopulatecmbBezier();
    //  StreamWriter xsw;
    //  Stream xstream;
    //  try {
    //    xstream = new FileStream(Cfg.BezierFilePath, FileMode.Create, FileAccess.Write);  //overwrite
    //    xsw = new StreamWriter(xstream);
    //  }
    //  catch (Exception) {
    //    return;
    //  }
    //  try {
    //    for (int i = 0; i < Cfg.BezierName.Length; i++) {
    //      xsw.WriteLine(
    //        Cfg.Bezier1X[i] + ", "
    //        + Cfg.Bezier1Y[i] + ", "
    //        + Cfg.Bezier2X[i] + ", "
    //        + Cfg.Bezier2Y[i]);
    //    }
    //    //xsw.WriteLine(Cfg.BezierVel + ", " + Cfg.BezierATouch);
    //  }
    //  catch (Exception) {
    //    xsw.Close();
    //    return;
    //  }
    //  xsw.Close();
    //  MessageBox.Show("Bezier File Saved");
    //}

    private void cmdClose_Click(object sender, EventArgs e) {
      Close();
    }
  }
}
