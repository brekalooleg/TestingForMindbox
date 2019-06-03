using System;

namespace AStar
{
    /// <summary>
    /// Внутренний класс внутри графа для Ребра
    /// </summary>
    public class EdgeInWork: IRoad
    {
        public int id;
        NodeInWork node_out;
        NodeInWork node_in;
        public double weight;

        double a = Double.NaN;
        double b = Double.NaN;
        double c = Double.NaN;

        /// <summary>
        /// Координата Х конца ребра 
        /// </summary>
        public double X
        {
            get
            {
                return node_in.X;
            }
        }

        /// <summary>
        /// Координата Y конца ребра 
        /// </summary>
        public double Y
        {
            get
            {
                return node_in.Y;
            }
        }

        /// <summary>
        /// Координата Х старта ребра 
        /// </summary>
        public double X0
        {
            get
            {
                return node_out.X;
            }
        }

        /// <summary>
        /// Координата Y старта ребра 
        /// </summary>
        public double Y0
        {
            get
            {
                return node_out.Y;
            }
        }

        /// <summary>
        /// Коэффициент А линейного уравнения ребра 
        /// </summary>
        public double A
        {
            get
            {
                if (Double.IsNaN(a))
                {
                    a = node_out.Y - node_in.Y;
                    return a;
                }
                else
                    return a;
                    
            }
        }

        /// <summary>
        /// Коэффициент B линейного уравнения ребра 
        /// </summary>
        public double B
        {
            get
            {
                if (Double.IsNaN(b))
                {
                    b = node_in.X - node_out.X;
                    return b;
                }
                else
                    return b;
            }
        }

        /// <summary>
        /// Коэффициент C линейного уравнения ребра 
        /// </summary>
        public double C
        {
            get
            {
                if (Double.IsNaN(c))
                {
                    c = node_out.X * node_in.Y - node_in.X * node_out.Y;
                    return c;
                }
                else
                    return c;
            }
        }

        /// <summary>
        /// Ссылка на конечную вершину ребра
        /// </summary>
        public NodeInWork Node_in
        {
            get
            {
                return node_in;
            }
        }

        /// <summary>
        /// Ссылка на начальную вершину ребра
        /// </summary>
        public NodeInWork Node_out
        {
            get
            {
                return node_out;
            }
        }

        /// <summary>
        /// Ссылка на начальную вершину ребра
        /// </summary>
        public IPoint Start
        {
            get
            {
                return node_out;
            }
        }

        /// <summary>
        /// Ссылка на конечную вершину ребра
        /// </summary>
        public IPoint Finish
        {
            get
            {
                return node_in;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ID ребра</param>
        /// <param name="node_out">Ссылка на старт</param>
        /// <param name="node_in">Ссылка на конец</param>
        /// <param name="weight">Вес ребра</param>
        public EdgeInWork(int id, NodeInWork node_out, NodeInWork node_in, double weight)
        {
            this.id = id;
            this.node_out = node_out;
            this.node_in = node_in;
            this.weight = weight;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ID ребра</param>
        public EdgeInWork(int id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            //return $"{id} ({X}, {Y})";
            return $"Edge [{node_out.Id}] -> [{node_in.Id}]";
        }
    }
}
