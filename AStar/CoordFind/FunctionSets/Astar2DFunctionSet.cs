using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AStar;

namespace AStar.CoordFind.FunctionSet
{
    public class Astar2DFunctionSet : IAstarFunctionSet
    {
        public virtual double between_point(IPoint point, IPoint xpoint)
        {
            return Math.Sqrt((xpoint.X - point.X) * (xpoint.X - point.X) + (xpoint.Y - point.Y) * (xpoint.Y - point.Y));
        }

        public virtual DropOnEdge heuristic_between_point_and_line(IPoint point, IRoad line)
        {
            double x = (line.B * (line.B * point.X - line.A * point.Y) - line.A * line.C) /
                (line.A * line.A + line.B * line.B);

            double y = (line.A * (-1 * line.B * point.X + line.A * point.Y) - line.B * line.C) /
                (line.A * line.A + line.B * line.B);

            double l = 0;

            if (x < Math.Min(line.X, line.X0) || x > Math.Max(line.X, line.X0) ||
                y < Math.Min(line.Y, line.Y0) || y > Math.Max(line.Y, line.Y0))
            {

                var lstart = between_point(point, line.Start);
                var lfinish = between_point(point, line.Finish);
                if (lstart < lfinish)
                {
                    x = line.Start.X;
                    y = line.Start.Y;
                    l = lstart;
                }
                else
                {
                    x = line.Finish.X;
                    y = line.Finish.Y;
                    l = lfinish;
                }

                return new DropOnEdge(new SimplePoint(x, y), point, l, line, true);
            }
            else
            {
                l = Math.Abs(line.A * point.X + line.B * point.Y + line.C) /
                        Math.Sqrt(line.A * line.A + line.B * line.B);
                return new DropOnEdge(new SimplePoint(x, y), point, l, line);
            }            
        }

        public virtual double heuristic_between_point(IPoint point, IPoint xpoint)
        {
            return between_point(point, xpoint);
        }

        public virtual Tuple<IPoint, IPoint> find_cross_line_and_circle(double phi, IPoint point, double r)
        {
            double d = -phi * phi + 2 * phi * point.Y - point.Y * point.Y + r * r;

            if (d < 0)
                return new Tuple<IPoint, IPoint>(null, null);
            else if (d == 0)
                return new Tuple<IPoint, IPoint>(new SimplePoint(point.X, phi), null);
            else
                return new Tuple<IPoint, IPoint>(new SimplePoint(point.X - Math.Sqrt(d), phi), new SimplePoint(point.X + Math.Sqrt(d), phi));
        }

        public Tuple<IPoint, IPoint> find_cross_line_and_edge(double phi, IRoad edge)
        {
            var result = new List<IPoint>();

            //x = Ax+By+C=0;
            //y = Phi;
            //x = -B * phi - C / edge.A;

            if (edge.A == 0)
                return new Tuple<IPoint, IPoint>(null, null);
            else
                return new Tuple<IPoint, IPoint>(new SimplePoint(((edge.B * phi) + edge.C) / -edge.A, phi), null);
        }

        public Tuple<int, int> min_max_index_x(Tuple<IPoint, IPoint> DownCross, Tuple<IPoint, IPoint> UpCross, double index_min_x, double delta)
        {
            double min_x = Math.Min(DownCross.Item1.X, UpCross.Item1.X);
            double max_x = Math.Max(DownCross.Item1.X, UpCross.Item1.X);

            int FromX = Convert.ToInt32(Math.Truncate((min_x - index_min_x) / delta));
            int ToX = Convert.ToInt32(Math.Truncate((max_x - index_min_x) / delta));

            return new Tuple<Int32, Int32>(FromX, ToX);
        }
    }
}
