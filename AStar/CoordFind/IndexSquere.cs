using System;

namespace AStar.CoordFind
{
    /// <summary>
    /// Хэшируемый объект на основе индесов квадратов в поиске
    /// </summary>
    public class IndexSquere: IEquatable <IndexSquere>
    {
        int a;
        int b;

        public IndexSquere(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        public bool Equals(IndexSquere other)
        {
            return other != null && a == other.a && b == other.b;
        }

        public override int GetHashCode()
        {
            return a.GetHashCode() + b.GetHashCode();
        }
    }
}
