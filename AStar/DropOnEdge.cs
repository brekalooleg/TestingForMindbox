using System;

namespace AStar
{
    /// <summary>
    /// Объект включающий точку в пространстве, ребро, и место падения пенпендикуляра от точки к ребру
    /// </summary>
    public class DropOnEdge : IPoint
    {
        IPoint pointFrom;
        IPoint pointTo;

        bool dropNode = false;

        double l;

        IRoad edge;

        /// <summary>
        /// Возвращает True, если конечная точка ребра на плоскости находится по правую сторону от точки
        /// </summary>
        public bool IsRightTurn
        {
            get
            {
                bool orient_flag = false;
                bool orient_road = false;
                   
                if (Edge.Y0 > Edge.Y)
                    orient_road = true;
                
                else if (Edge.Y0 < Edge.Y)                
                    orient_road = false; 
                
                else if (Edge.X0 < Edge.X) 
                    orient_road = true; 
                               
                else                
                    orient_road = false;

                ///////////////////////////////

                if (X_From > X)
                    orient_flag = false;

                else if (X_From < X)
                    orient_flag = true;

                else if (Y_From > Y)
                    orient_flag = false;

                else orient_flag = true;


                ///////////////////////////////

                if (orient_flag)
                    return orient_road;

                else return !orient_road;
            }
        }
        
        public DropOnEdge(IPoint pointTo, IPoint pointFrom, double l, IRoad edge)
        {
            this.pointFrom = pointFrom;
            this.pointTo = pointTo;

            this.l = l;

            this.edge = edge;
        }

        public DropOnEdge(IPoint pointTo, IPoint pointFrom, double l, IRoad edge, bool flag) : this(pointTo, pointFrom, l, edge)
        {
            dropNode = flag;
        }

        public DropOnEdge(IPoint pointTo, IPoint pointFrom, double l)
        {
            this.pointFrom = pointFrom;
            this.pointTo = pointTo;

            this.l = l;

            this.edge = null;            
        }

        public DropOnEdge(IPoint pointFrom)
        {
            this.pointFrom = pointFrom;
            this.pointTo = null;
            this.l = Double.PositiveInfinity;
            this.edge = null;

        }

        /// <summary>
        /// Координата Х точки начала пенпендикуляра к ребру
        /// </summary>
        public double X_From
        {
            get
            {
                return pointFrom.X;
            }
        }
        /// <summary>
        /// Координата Y точки начала пенпендикуляра к ребру
        /// </summary>
        public double Y_From
        {
            get
            {
                return pointFrom.Y;
            }
        }
        /// <summary>
        /// Минимальное растояние от точки до ребра, длинна пенпендикуляра к нему
        /// </summary>
        public double L
        {
            get
            {
                return l;
            }
        }
        /// <summary>
        /// Координата Х точки падения пенпендикуляра к ребру, ближайшая искомая точка
        /// </summary>
        public double X
        {
            get
            {
                return pointTo.X;
            }
        }
        /// <summary>
        /// Координата Y точки падения пенпендикуляра к ребру, ближайшая искомая точка
        /// </summary>
        public double Y
        {
            get
            {
                return pointTo.Y;
            }
        }
        /// <summary>
        /// Ссылка на ребро
        /// </summary>
        public IRoad Edge
        {
            get
            {
                return edge;
            }
        }

        /// <summary>
        /// Точка начала пенпендикуляра к ребру, точка от которой искалось минимальное расстояние
        /// </summary>
        public IPoint Point_From
        {
            get
            {
                return pointFrom;
            }
        }

        /// <summary>
        /// Флаг является ли ближайшей точкой от точке в пространстве один из концов ребра
        /// </summary>
        public bool DropNodeFlag
        {
            get
            {
                return dropNode;
            }
        }

        /// <summary>
        /// Вершина ребра, которая может быть использована как начальная для маршрута от этого ребра
        /// </summary>
        public IPoint StartPath
        {
            get
            {
                if (dropNode)                
                    return Point_To;                
                else
                    return Edge.Finish;
            }
        }

        /// <summary>
        /// Вершина ребра, которая может быть использована как конечная для маршрута от этого ребра
        /// </summary>
        public IPoint FinishPath
        {
            get
            {
                if (dropNode)
                    return Point_To;
                else
                    return Edge.Start;
            }
        }

        /// <summary>
        /// Точка падения пенпендикуляра к ребру, искомая минимальная точка для минимального расстояния
        /// </summary>
        public IPoint Point_To
        {
            get
            {
                return pointTo;
            }
        }

        public override string ToString()
        {
            
            return $"Edge [ {Edge.X0} , {Edge.Y0} ] -> [ {Edge.X} , {Edge.Y} ], Point [ {X} , {Y} ], L= {L} ";
        }

        public bool Equals(IPoint other)
        {
            return other != null && other.X == X && other.Y == Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + X.GetHashCode();
        }
    }
}
