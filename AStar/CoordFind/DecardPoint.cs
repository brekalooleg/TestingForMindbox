using AStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar.CoordFind
{
    /// <summary>
    /// Точка в пространстве в декардовых координатах 
    /// </summary>
    public class DecardPoint
    {
        public double x;
        public double y;
        public double z;

        /// <summary>
        /// Coord.X - Широта в диапазоне от -1/2 Pi до 1/2 Pi в радианах
        /// Coord.Y - Долгота в диапазоне от -Pi до Pi в радианах
        /// </summary>
        /// <param name="coord"></param>
        
        public DecardPoint(IPoint coord)
        {
            double lat = coord.X + Math.PI / 2;
            double lon = coord.Y + Math.PI;

            x = Math.Cos(lon) * Math.Sin(lat);
            y = Math.Sin(lon) * Math.Sin(lat);
            z = Math.Cos(lat);
        }

        public DecardPoint(Vector v)
        {
            x = v.i;
            y = v.j;
            z = v.k;
        }

        public DecardPoint(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }


        public GeoPoint FormDecardToGeo() //работает только для точек на сфере R=1
        {
            double r = Math.Sqrt(x * x + y * y + z * z);
            double lat = Math.Acos(z / r) - Math.PI / 2;
            double lon;
            if (Math.Abs(Math.Abs(z) - 1) < 0.00000000000001)
                lon = 0;
            else
                lon = Math.Atan2(y, x) - Math.PI;

            if (lon <= -Math.PI)
                lon += 2 * Math.PI;

            return new GeoPoint(lat, lon, true);
        }

        public static DecardPoint operator +(DecardPoint a, Vector b)
        {
            return new DecardPoint(a.x + b.i, a.y + b.j, a.z + b.k);
        }

        public static DecardPoint operator -(DecardPoint a, Vector b)
        {
            return new DecardPoint(a.x - b.i, a.y - b.j, a.z - b.k);
        }

        public double DistanceTo(DecardPoint p)
        {
            double dx = p.x - x;
            double dy = p.y - y;
            double dz = p.z - z;

            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public override string ToString()
        {
            var a = this.FormDecardToGeo();
            return $" x = {x}, y = {y}, z = {z}||| lat = {LatLonConvert.RadianToDegree(a.X)}; lon = {LatLonConvert.RadianToDegree(a.Y)}";
        }
    }
}
