using AStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar.CoordFind.FunctionSet
{
    public class Astar3DFunctionSet : IAstarFunctionSet
    {
        public virtual double between_point(IPoint point, IPoint xpoint)
        {
            double Lat1 = point.X;
            double Lat2 = xpoint.X;
            double dLat = point.X - xpoint.X;
            double dLon = point.Y - xpoint.Y;

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(Lat1) * Math.Cos(Lat2) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            return 6371000 * 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        }

        public virtual double heuristic_between_point(IPoint point, IPoint xpoint)
        {
            return between_point(point, xpoint);
        }

        public virtual DropOnEdge heuristic_between_point_and_line(IPoint point, IRoad road)
        {

            DecardPoint A = new DecardPoint(road.Start),
                        B = new DecardPoint(road.Finish),
                        C = new DecardPoint(point);


            Vector RA = new Vector(A);
            Vector RB = new Vector(B);
            Vector RC = new Vector(C);

            Vector Norm_AB = RA * RB;

            Vector Norm_XC = Norm_AB * RC;

            Vector OX =   Norm_AB * Norm_XC;
            OX = OX.Normalized();

            DecardPoint X = new DecardPoint(OX);
            DecardPoint XNeg = new DecardPoint(-OX);

            double distX = X.DistanceTo(C);
            double distXNeg = XNeg.DistanceTo(C);

            double distA = A.DistanceTo(C); 
            double distB = B.DistanceTo(C); 


            double distRoad = A.DistanceTo(B);

            if (distX > distXNeg)
            {
                X = XNeg;
                distX = distXNeg;
            }
                
            if (distRoad < A.DistanceTo(X) || distRoad < B.DistanceTo(X))
                distX = Double.PositiveInfinity;

            if (distX < Math.Min(distA, distB))
            {
                var geoX = X.FormDecardToGeo();
                return new DropOnEdge(geoX, point, distX, road);
            }

            else
                if (distA < distB)
                return new DropOnEdge(road.Start, point, distA, road);
            else
                return new DropOnEdge(road.Finish, point, distB, road);
        }

        public Tuple<IPoint, IPoint> find_cross_line_and_circle(double phi, IPoint point, double r)
        {
            throw new NotImplementedException();
        }

        public Tuple<IPoint,IPoint> find_cross_line_and_edge(double phi, IRoad edge)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, int> min_max_index_x(Tuple<IPoint, IPoint> DownCross, Tuple<IPoint, IPoint> UpCross, double min_x, double delta)
        {
            throw new NotImplementedException();
        }
    }
}
