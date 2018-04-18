using System;
using System.Drawing;

namespace ChordCadenza{
  internal class Bezier {
    /*
    Code to generate a cubic Bezier curve

    cp is a 4 element array where:
    cp[0] is the starting point, or P0 in the above diagram
    cp[1] is the first control point, or P1 in the above diagram
    cp[2] is the second control point, or P2 in the above diagram
    cp[3] is the end point, or P3 in the above diagram
    t is the parameter value, 0 <= t <= 1
    */

    private float ax, bx, cx;
    private float ay, by, cy;
    internal byte[] KBtoMidiVel = new byte[128];

    private void CalcCoefficients(int seq) {
      /* calculate the polynomial coefficients */
      PointF[] cp = new PointF[] {
          new PointF(0, 0),
          new PointF(Cfg.Bezier1X[seq], Cfg.Bezier1Y[seq]),
          new PointF(Cfg.Bezier2X[seq], Cfg.Bezier2Y[seq]),
          new PointF(127, 127)
        };

      cx = 3.0f * (cp[1].X - cp[0].X);
      bx = 3.0f * (cp[2].X - cp[1].X) - cx;
      ax = cp[3].X - cp[0].X - cx - bx;
          
      cy = 3.0f * (cp[1].Y - cp[0].Y);
      by = 3.0f * (cp[2].Y - cp[1].Y) - cy;
      ay = cp[3].Y - cp[0].Y - cy - by;
    }

    private PointF CalcPoint(PointF cp0, float t )
    {
      PointF result = new PointF();
          
      /* calculate the curve point at parameter value t */
          
      float tSquared = t * t;
      float tCubed = tSquared * t;
          
      result.X = (ax * tCubed) + (bx * tSquared) + (cx * t) + cp0.X;
      result.Y = (ay * tCubed) + (by * tSquared) + (cy * t) + cp0.Y;
          
      return result;
    }

    /*
     ComputeBezier fills an array of Point2D structs with the curve   
     points generated from the control points cp. Caller must 
     allocate sufficient memory for the result, which is 
     <sizeof(Point2D) numberOfPoints>
    */

    internal Point[] CalcCurve(int seq, int znumpoints) {
      //* calculate bezier curve of kb/midi vels
      //* return integer points for znumpoints values of bezier 't' parameter
      Point[] curve = new Point[znumpoints];
      float dt = 1.0f / (znumpoints - 1);
      CalcCoefficients(seq);
      PointF cp = new PointF(0, 0);
      for (int i = 0; i < znumpoints; i++) curve[i] = Point.Round(CalcPoint(cp, i*dt));
      return curve;
    }

    internal bool CalcVelocities(int seq) {
      //* calculate all integer points 0..127 on the curve
      //* return false if any kbvel results in more than one midivel (ie curve changes direction)
      //* seq = bezier number (0, 1, 2)
      int xkbvel = 0;
      int xnumpoints = 500;
      byte[] xkbtomidivel = new byte[128];
      Point[] xcurve = CalcCurve(seq, xnumpoints);
      for (int xt = 1; xt < xnumpoints; xt++) {
        if (xcurve[xt].X < xcurve[xt - 1].X) {
          new BezierWarning("keyboard curve inconsistent - correction attempted");
          xcurve[xt - 1].X = xcurve[xt].X;
          //return false;
        }
        if (xcurve[xt].Y < xcurve[xt - 1].Y) {
          new BezierWarning("keyboard curve inconsistent - correction attempted");
          xcurve[xt - 1].Y = xcurve[xt].Y;
          //return false;
        }
      }
      for (int xi = 0; xi < xcurve.Length; xi++) {   //for all (500) 't' points on curve
        for (; xkbvel <= xcurve[xi].X; xkbvel++) {   //fill in 'missing' kbvel integers
          xkbtomidivel[xkbvel] = (byte)Math.Min(127, Math.Max(0, xcurve[xi].Y));  //xkbvel: 0-127 ; xi: 0-500
        }
      }
      //* set any high cutoff kbvel to same as last kbvel on curve 
      for (; xkbvel <= 127; xkbvel++) {
        xkbtomidivel[xkbvel] = xkbtomidivel[Math.Max(1, xkbvel) - 1];
      }
      xkbtomidivel[0] = 0;  //OFF velocity must be 0
      KBtoMidiVel = xkbtomidivel;
      return true;
    }
  }
}
