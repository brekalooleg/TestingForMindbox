using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar.CoordFind
{
    /// <summary>
    /// Объект-точка для географических координат
    /// </summary>
    public class GeoPoint : IPoint
    {
        double x;
        double y;

        /// <summary>
        /// Создание новой географической точки из координат в радианах
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="flag"></param>
        public GeoPoint(double x, double y, bool flag)
        {
            this.x = x;
            this.y = y;
        }
        
        /// <summary>
        /// Создание новой географической точки из координат в градусах
        /// </summary>
        /// <param name="x"> Координата Х в градусах</param>
        /// <param name="y"> Координата Y в градусах</param>
        public GeoPoint (double x, double y)
        {
            this.x = LatLonConvert.DegreeToRadian(x);
            this.y = LatLonConvert.DegreeToRadian(y);
        }
        public GeoPoint()
        {

        }

        /// <summary>
        /// Координата Х
        /// </summary>
        public double X
        {
            get
            {
                return x;
            }
        }

        /// <summary>
        /// Коориданата Y
        /// </summary>
        public double Y
        {
            get
            {
                return y;
            }
        }        

        public override string ToString()
        {
            return $"Rad [  {X}  ,  {Y}  ] \nDegree [  {LatLonConvert.RadianToDegree(X)}  ,  {LatLonConvert.RadianToDegree(Y)}  ]";
        }

        public bool Equals(IPoint other)
        {
            return other != null && other.X == x && other.Y == y;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() + y.GetHashCode();
        }
    }
}
