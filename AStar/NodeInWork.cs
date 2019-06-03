using System;
using System.Collections.Generic;

namespace AStar
{
    /// <summary>
    /// Внутренний объект алгоритма А* для вершины
    /// </summary>
    public class NodeInWork: IPoint
    {
        int id;
        internal double x;
        internal double y;
        internal List<EdgeInWork> edge;

        /// <summary>
        /// Координата Х вершины
        /// </summary>
        public double X
        {
            get
            {
                return x;
            }
        }

        /// <summary>
        /// Координата Y вершины
        /// </summary>
        public double Y
        {
            get
            {
                return y;
            }
        }

        /// <summary>
        /// ID вершины
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// Список вершин выходящих из данной вершины
        /// </summary>
        public IEnumerable<EdgeInWork> Edges
        {
            get
            {
                return edge;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Координата Х</param>
        /// <param name="y">Координата Y</param>
        /// <param name="id">ID</param>
        public NodeInWork(double x, double y,int id)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            edge = new List<EdgeInWork>();
        }

        public override string ToString()
        {
            return $"{id} ({X}, {Y})";
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
