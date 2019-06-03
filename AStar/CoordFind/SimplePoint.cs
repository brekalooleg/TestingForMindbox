

namespace AStar.CoordFind
{
    /// <summary>
    /// Упрощенный класс-точка
    /// </summary>
    public class SimplePoint : IPoint
    {
        double x;
        double y;
        
        public SimplePoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get
            {
                return x;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
        }

        public bool Equals(IPoint other)
        {
            return other != null && other.X == x && other.Y == y;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() + y.GetHashCode();
        }
        public override string ToString()
        {
            return $"x={x}, y={y}";
        }
    }
}
