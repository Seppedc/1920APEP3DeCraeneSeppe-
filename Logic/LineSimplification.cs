using Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class LineSimplification
    {
        public LineSimplification()
        {

        }
        //deze code is overgenomen van : 
        //https://rosettacode.org/wiki/Ramer-Douglas-Peucker_line_simplification#C.23
        private double PerpendicularDistance(Point p, Point lineStart, Point lineEnd)
        {
            double dx = lineEnd.X - lineStart.X;
            double dy = lineEnd.Y - lineStart.Y;

            // Normalize
            double mag = Math.Sqrt(dx * dx + dy * dy);

            if (mag > 0.0)
            {
                dx /= mag;
                dy /= mag;
            }
            double pvx = p.X - lineStart.X;
            double pvy = p.Y - lineStart.Y;

            // Get dot product (project pv onto normalized direction)
            double pvdot = dx * pvx + dy * pvy;

            // Scale line direction vector and subtract it from pv
            double ax = pvx - pvdot * dx;
            double ay = pvy - pvdot * dy;

            return Math.Sqrt(ax * ax + ay * ay);
        }

        public List<Point> RamerDouglasPeucker(List<Point> pointList, double epsilon)
        {
            List<Point> output = new List<Point>();

            if (pointList.Count < 2)
            {
                throw new ArgumentOutOfRangeException("Not enough points to simplify");
            }

            // Find the point with the maximum distance from line between the start and end
            double dmax = 0.0;
            int index = 0;

            for (int i = 1; i < pointList.Count - 1; ++i)
            {
                double d = PerpendicularDistance(pointList[i], pointList[0], pointList[pointList.Count - 1]);
                if (d > dmax)
                {
                    index = i;
                    dmax = d;
                }
            }

            // If max distance is greater than epsilon, recursively simplify
            if (dmax > epsilon)
            {
                List<Point> firstLine = pointList.Take(index + 1).ToList();
                List<Point> lastLine = pointList.Skip(index).ToList();

                List<Point> recResults1 = RamerDouglasPeucker(firstLine, epsilon);
                List<Point> recResults2 = RamerDouglasPeucker(lastLine, epsilon);

                // build the result list
                output.AddRange(recResults1.Take(recResults1.Count - 1));
                output.AddRange(recResults2);
                if (output.Count < 2) throw new Exception("Problem assembling output");
            }
            else
            {
                // Just return start and end points
                output.Clear();
                output.Add(pointList[0]);
                output.Add(pointList[pointList.Count - 1]);
            }

            return output;
        }


    }
}