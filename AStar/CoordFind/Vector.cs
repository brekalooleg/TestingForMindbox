using System;

namespace AStar.CoordFind
{
    /// <summary>
    /// Вектор в декардовой системе координат
    /// </summary>
    public class Vector
    {
        public double i;
        public double j;
        public double k;

        public Vector(DecardPoint A, DecardPoint B)
        {
            i = B.x - A.x;
            j = B.y - A.y;
            k = B.z - A.z;
        }

        public Vector(double i, double j, double k)
        {
            this.i = i;
            this.j = j;
            this.k = k;
        }

        public Vector(DecardPoint A)
        {
            i = A.x;
            j = A.y;
            k = A.z;
        }

        public static Vector operator * (Vector a, Vector b)
        {
            return new Vector((a.j * b.k) - (a.k * b.j), (a.k * b.i) - (a.i * b.k), (a.i * b.j) - (a.j * b.i));
        }

        public static Vector operator - (Vector a)
        {
            return new Vector(-a.i, -a.j, -a.k);
        }

        public double Length()
        {
            return Math.Sqrt(i * i + j * j + k * k);
        }

        public Vector Normalized()
        {
            var len = Length();
            return new Vector(i / len, j / len, k / len);
        }
    }
}
