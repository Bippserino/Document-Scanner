using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentScanner
{
    public static class Utility
    {
        public static bool IsRectangle(VectorOfPoint approxContour)
        {
            Point[] pts = approxContour.ToArray();
            LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

            for (int j = 0; j < edges.Length; j++)
            {
                double angle = Math.Abs(
                    edges[(j + 1) % edges.Length].GetExteriorAngleDegree(edges[j]));
                if (angle < 35 || angle > 145)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
