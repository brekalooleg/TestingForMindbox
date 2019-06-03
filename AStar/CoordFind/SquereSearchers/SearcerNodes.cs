using System;
using System.Collections.Generic;
using System.Linq;

using AStar;
using AStar.Source;
using AStar.CoordFind;
using AStar.CoordFind.FunctionSet;
using AStar.CoordFind.SquareMatrix;
using AStar.CoordFind.SquereSearchers;

namespace AStar.CoordFind.SquereSearchers
{
    public class SearcherNodes : SearcherBase
    {
        internal SquareMatrixNodes square_matrix;


        public SearcherNodes(IEnumerable<IPoint> points, IAstarFunctionSet fun_set, int point_per_segment = 100) : base(points, fun_set, point_per_segment)
        {
            if (points == null || points.FirstOrDefault() == null)
                throw new ArgumentNullException(nameof(points));
        }


        public IPoint Find(IPoint xpoint)
        {
            if (xpoint == null)
                throw new ArgumentNullException(nameof(xpoint)); 

            if (square_matrix == null )
                square_matrix = new SquareMatrixNodes(points, point_per_segment);
            
            var result = Find(xpoint, square_matrix);
            return result.Point_To;
        }


        protected override DropOnEdge CheckSquere(int a, int b, SquareMatrixBase square_matrix, DropOnEdge curr_result)
        {
            var s_matrix = square_matrix as SquareMatrixNodes;

            if (s_matrix == null)
                throw new ArgumentException(nameof(square_matrix));

            if (a < 0 || a >= s_matrix.SizeX || b < 0 || b >= s_matrix.SizeY)
                return curr_result;

            double curDistance = curr_result.L;
            IPoint curPoint = curr_result.Point_To;

            if (s_matrix.Matrix[a, b] != null)
                foreach (var point in s_matrix.Matrix[a, b])
                {
                    double distance = fun_set.between_point(point, curr_result.Point_From);

                    if (distance < curDistance)
                    {
                        curDistance = distance;
                        curPoint = point;
                    }
                }

            return new DropOnEdge(curPoint, curr_result.Point_From, curDistance);
        }  
              
    }
}
