using System;
using System.Collections.Generic;
using System.Linq;
using AStar.CoordFind.FunctionSet;
using AStar.CoordFind.SquareMatrix;

namespace AStar.CoordFind.SquereSearchers
{
    public class SearcherEdges : SearcherBase
    {
        internal SquareMatrixEdges square_matrix;


        public SearcherEdges(IEnumerable<IPoint> points, IAstarFunctionSet fun_set, int point_per_segment = 100) : base(points, fun_set, point_per_segment)
        {
            if (points == null || points.FirstOrDefault() == null)
                throw new ArgumentNullException(nameof(points));
        }


        public DropOnEdge Find(IPoint xpoint)
        {
            if (xpoint == null)
                throw new ArgumentNullException(nameof(xpoint));

            if (square_matrix == null)
                square_matrix = new SquareMatrixEdges(points, fun_set, point_per_segment);

            return Find(xpoint, square_matrix);
        }


        protected override DropOnEdge CheckSquere(int a, int b, SquareMatrixBase square_matrix, DropOnEdge result)
        {
            var s_matrix = square_matrix as SquareMatrixEdges;

            if (s_matrix == null)
                throw new ArgumentException(nameof(square_matrix));

            if (a < 0 || a >= s_matrix.SizeX || b < 0 || b >= s_matrix.SizeY)
                return result;



            double curDistance = result.L;
            IRoad curRoad = result.Edge;
            IPoint curPointTo = result.Point_To;
            bool curDropNodeFlag = result.DropNodeFlag;

            double curA = 0, curB = 0, curC = 0;

            if (result.Edge != null)
            {
                curA = result.Edge.A;
                curB = result.Edge.B;
                curC = result.Edge.C;
            }

            if (s_matrix.Matrix[a, b] != null)
                foreach (var road in s_matrix.Matrix[a, b])
                {
                    DropOnEdge temp = fun_set.heuristic_between_point_and_line(result.Point_From, road);

                    if (temp.L <= curDistance && temp.Edge.Finish != temp.Point_To)
                    {
                        if (temp.IsRightTurn)
                        {
                            curDistance = temp.L;
                            curRoad = temp.Edge;
                            curPointTo = temp.Point_To;
                            curDropNodeFlag = temp.DropNodeFlag;
                        }

                        else

                        if (temp.L < curDistance)
                        {
                            curDistance = temp.L;
                            curRoad = temp.Edge;
                            curPointTo = temp.Point_To;
                            curDropNodeFlag = temp.DropNodeFlag;
                        }
                    }
                }

            return new DropOnEdge(curPointTo, result.Point_From, curDistance, curRoad, curDropNodeFlag);
        }

    }
}
