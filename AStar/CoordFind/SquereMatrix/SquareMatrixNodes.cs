using System;
using System.Collections.Generic;
using System.Linq;

namespace AStar.CoordFind.SquareMatrix
{    
    class SquareMatrixNodes : SquareMatrixBase
    {
        List<IPoint>[,] matrix;

        public SquareMatrixNodes(IEnumerable<IPoint> points, int point_per_segment)
        {
            if (points == null || points.FirstOrDefault() == null)
                throw new ArgumentNullException(nameof(points));

            int count = 0;

            foreach (var point in points)
            {
                count++;

                if (point.X > max_x)
                    max_x = point.X;

                if (point.X < min_x)
                    min_x = point.X;

                if (point.Y > max_y)
                    max_y = point.Y;

                if (point.Y < min_y)
                    min_y = point.Y;
            }

            if (count != 0)
            {

                int temp_size = Convert.ToInt32(Math.Sqrt(count / point_per_segment));

                if (temp_size == 0)
                    temp_size = 1;

                double delta_x = max_x - min_x;
                double delta_y = max_y - min_y;

                if (delta_x > delta_y)
                    delta = delta_x / temp_size;
                else
                    delta = delta_y / temp_size;

                size_x = Convert.ToInt32((max_x - min_x) / delta);
                size_y = Convert.ToInt32((max_y - min_y) / delta);

                if (size_x == 0)
                    size_x = 1;

                if (size_y == 0)
                    size_y = 1;

                matrix = new List<IPoint>[size_x, size_y];

                foreach (var point in points)
                {
                    int a = Convert.ToInt32(Math.Truncate((point.X - min_x) / delta));
                    int b = Convert.ToInt32(Math.Truncate((point.Y - min_y) / delta));

                    if (a == size_x)
                        a -= 1;
                    if (b == size_y)
                        b -= 1;

                    if (matrix[a, b] == null)
                        matrix[a, b] = new List<IPoint>();
                    matrix[a, b].Add(point);
                }
            }
        }

        public List<IPoint>[,] Matrix
        {
            get
            {
                return matrix;
            }
        }
    }
}
