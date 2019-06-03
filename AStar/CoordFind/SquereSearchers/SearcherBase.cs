using System;
using System.Collections.Generic;

using AStar.CoordFind.FunctionSet;
using AStar.CoordFind.SquareMatrix;


namespace AStar.CoordFind.SquereSearchers
{
    public abstract class SearcherBase
    {
        protected IEnumerable<IPoint> points;
        protected IAstarFunctionSet fun_set;
        protected int point_per_segment;

        protected SearcherBase(IEnumerable<IPoint> points, IAstarFunctionSet fun_set, int point_per_segment)
        {
            this.points = points;
            this.fun_set = fun_set;
            this.point_per_segment = point_per_segment;
        }

        protected abstract DropOnEdge CheckSquere(int a, int b, SquareMatrixBase squere_matrix, DropOnEdge result);

        public DropOnEdge Find(IPoint xpoint, SquareMatrixBase squere_matrix)
        {
            //Определение квадрата входной точки
            int a = Convert.ToInt32(Math.Truncate((xpoint.X - squere_matrix.MinX) / squere_matrix.Delta));
            int b = Convert.ToInt32(Math.Truncate((xpoint.Y - squere_matrix.MinY) / squere_matrix.Delta));

            //если входная точка выходит за пределы квадрата, встраиваем её в крайний квадрат
            if (a < 0)
                a = 0;
            if (a >= squere_matrix.SizeX)
                a = squere_matrix.SizeX - 1;

            if (b < 0)
                b = 0;
            if (b >= squere_matrix.SizeY)
                b = squere_matrix.SizeY - 1;

            int old_a = a;
            int old_b = b;

            DropOnEdge result = new DropOnEdge(xpoint);

            var cheked_set = new HashSet<IndexSquere>();

            result = CheckSquere(a, b, squere_matrix, result);

            cheked_set.Add(new IndexSquere(a, b));

            //вычисление координат сторон квадрата
            double x1 = squere_matrix.MinX + a * squere_matrix.Delta;
            double x2 = squere_matrix.MinX + (a + 1) * squere_matrix.Delta;
            double y1 = squere_matrix.MinY + b * squere_matrix.Delta;
            double y2 = squere_matrix.MinY + (b + 1) * squere_matrix.Delta;

            //Если радиус поиска искомой точки выходит за пределы квадрата или точка не была найденна, выход на поиск вокруг квадрата
            if ((result.Point_To == null) || (Math.Abs(x1 - xpoint.X) < result.L) || (Math.Abs(x2 - xpoint.X) < result.L)
                || (Math.Abs(y1 - xpoint.Y) < result.L) || (Math.Abs(y2 - xpoint.Y) < result.L))
            {
                //Условие выхода - 2 раза наличие значения искомой точки
                //Для случая выхода за пределы первого квадрата выходная переменная равна 1 и делается еще один лишний круг
                //Для случая не нахождения точки сразу
                double r = 0;
                do
                {
                    r += squere_matrix.Delta;

                    int check_from = Convert.ToInt32(Math.Truncate((xpoint.Y + r - squere_matrix.MinY) / squere_matrix.Delta));
                    int check_to = Convert.ToInt32(Math.Truncate((xpoint.Y - r - squere_matrix.MinY) / squere_matrix.Delta)) + 1;

                    int current_b = check_from;                    

                    Tuple<IPoint, IPoint> cross_circle;

                    while (current_b > old_b)
                    {
                        //Выше центра круга
                        cross_circle = fun_set.find_cross_line_and_circle(squere_matrix.MinY + squere_matrix.Delta * current_b, xpoint, r);

                        if (cross_circle.Item1 != null && cross_circle.Item2 != null)
                        {
                            int from_a = Convert.ToInt32(Math.Truncate((cross_circle.Item1.X - squere_matrix.MinX) / squere_matrix.Delta));
                            int to_a = Convert.ToInt32(Math.Truncate((cross_circle.Item2.X - squere_matrix.MinX) / squere_matrix.Delta));

                            for (int j = from_a; j <= to_a; j++)
                            {
                                var temp_hash = new IndexSquere(j, current_b);
                                if (!cheked_set.Contains(temp_hash))
                                {
                                    result = CheckSquere(j, current_b, squere_matrix, result);
                                    cheked_set.Add(temp_hash);
                                }
                            }
                        }

                        else if (cross_circle.Item1 != null)
                        {
                            int from_a = Convert.ToInt32(Math.Truncate((cross_circle.Item1.X - squere_matrix.MinX) / squere_matrix.Delta));
                            var temp_hash = new IndexSquere(from_a, current_b);
                            if (!cheked_set.Contains(temp_hash))
                            {
                                result = CheckSquere(from_a, current_b, squere_matrix, result);
                                cheked_set.Add(temp_hash);
                            }
                        }

                        current_b--;
                    }


                    //Обработка центра круга
                    cross_circle = fun_set.find_cross_line_and_circle(xpoint.Y, xpoint, r);
                    if (cross_circle.Item1 != null && cross_circle.Item2 != null)
                    {
                        int from_a = Convert.ToInt32(Math.Truncate((cross_circle.Item1.X - squere_matrix.MinX) / squere_matrix.Delta));
                        int to_a = Convert.ToInt32(Math.Truncate((cross_circle.Item2.X - squere_matrix.MinX) / squere_matrix.Delta));

                        for (int j = from_a; j <= to_a; j++)
                        {
                            var temp_hash = new IndexSquere(j, current_b);
                            if (!cheked_set.Contains(temp_hash))
                            {
                                result = CheckSquere(j, current_b, squere_matrix, result);
                                cheked_set.Add(temp_hash);
                            }
                        }
                    }


                    while (current_b >= check_to)
                    {
                        //Ниже центра круга
                        cross_circle = fun_set.find_cross_line_and_circle(squere_matrix.MinY + squere_matrix.Delta * current_b, xpoint, r);

                        if (cross_circle.Item1 != null && cross_circle.Item2 != null)
                        {
                            int from_a = Convert.ToInt32(Math.Truncate((cross_circle.Item1.X - squere_matrix.MinX) / squere_matrix.Delta));
                            int to_a = Convert.ToInt32(Math.Truncate((cross_circle.Item2.X - squere_matrix.MinX) / squere_matrix.Delta));

                            for (int j = from_a; j <= to_a; j++)
                            {
                                var temp_hash = new IndexSquere(j, current_b - 1);
                                if (!cheked_set.Contains(temp_hash))
                                {
                                    result = CheckSquere(j, current_b - 1, squere_matrix, result);
                                    cheked_set.Add(temp_hash);
                                }
                            }
                        }

                        else if (cross_circle.Item1 != null)
                        {
                            int from_a = Convert.ToInt32(Math.Truncate((cross_circle.Item1.X - squere_matrix.MinX) / squere_matrix.Delta));
                            var temp_hash = new IndexSquere(from_a, current_b - 1);
                            if (!cheked_set.Contains(temp_hash))
                            {
                                result = CheckSquere(from_a, current_b - 1, squere_matrix, result);
                                cheked_set.Add(temp_hash);
                            }
                        }
                        current_b--;
                    }
                } while (result.Point_To == null || result.L > r);
            }

            return result;
        }
    }
}