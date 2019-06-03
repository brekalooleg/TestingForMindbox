using System;

namespace AStar.CoordFind
{
    /// <summary>
    /// Конверктер из радиан в градусы и наоборот
    /// </summary>
    public static class LatLonConvert
    {
        /// <summary>
        /// Перевод из градусов в радианы
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        /// <summary>
        /// Перевод из радиан в градусы
        /// </summary>
        /// <param name="radian"></param>
        /// <returns></returns>
        public static double RadianToDegree(double radian)
        {
            return radian * 180.0 / Math.PI;
        }
    }
}
