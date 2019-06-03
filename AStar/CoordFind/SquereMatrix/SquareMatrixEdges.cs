using System;
using System.Collections.Generic;
using AStar.CoordFind.FunctionSet;

namespace AStar.CoordFind.SquareMatrix
{
    class SquareMatrixEdges : SquareMatrixBase
    {
        List<IRoad>[,] matrix;

        public SquareMatrixEdges(IEnumerable<IPoint> points, IAstarFunctionSet fun_set, int point_per_segment)
        {
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

                matrix = new List<IRoad>[size_x, size_y];
                Tuple<Int32, Int32> sector = new Tuple<int, int>(0, 0);
                Tuple<IPoint, IPoint> DownCross, UpCross;

                foreach (NodeInWork point in points)
                {
                    foreach (var road in point.Edges)
                    {

                        int index_y = Convert.ToInt32(Math.Truncate((road.Node_out.Y - min_y) / delta));

                        if (index_y == SizeY)
                            index_y = SizeY-1;

                        int temp_y = index_y;

                        UpCross = fun_set.find_cross_line_and_edge(min_y + (delta * (temp_y + 1)), road);
                        DownCross = fun_set.find_cross_line_and_edge(min_y + (delta * temp_y), road); 

                        //Ребро не выходит на пределы горизонтальной линии индексации или параллельна медиане 
                        if (DownCross.Item1 == null || (road.Node_out.Y > DownCross.Item1.Y && road.Node_out.Y < UpCross.Item1.Y &&
                                                        road.Node_in.Y > DownCross.Item1.Y && road.Node_in.Y < UpCross.Item1.Y))

                            sector = fun_set.min_max_index_x(new Tuple<IPoint, IPoint>(road.Node_out, null), new Tuple<IPoint, IPoint>(road.Node_in, null), min_x, delta);


                        //Проверка горизонталей выше стартовой точки
                        else if (road.Node_out.Y < UpCross.Item1.Y && road.Node_in.Y > UpCross.Item1.Y)
                        {

                            do
                            {
                                if (temp_y == index_y)
                                    sector = fun_set.min_max_index_x(new Tuple<IPoint, IPoint>(road.Node_out, null), UpCross, min_x, delta);
                                else
                                    sector = fun_set.min_max_index_x(DownCross, UpCross, min_x, delta);

                                for (int i = sector.Item1; i <= sector.Item2 && i <= SizeX - 1; i++)
                                {
                                    if (matrix[i, temp_y] == null)
                                        matrix[i, temp_y] = new List<IRoad>();
                                    matrix[i, temp_y].Add(road);
                                }

                                temp_y++;

                                UpCross = fun_set.find_cross_line_and_edge(min_y + (delta * (temp_y + 1)), road);
                                DownCross = fun_set.find_cross_line_and_edge(min_y + (delta * temp_y), road);


                            } while (road.Node_in.Y > UpCross.Item1.Y);

                            sector = fun_set.min_max_index_x(DownCross, new Tuple<IPoint, IPoint>(road.Node_in, null), min_x, delta);


                        }

                        //Проверка горизонталей ниже стартовой точки
                        else if (road.Node_out.Y > DownCross.Item1.Y && road.Node_in.Y < DownCross.Item1.Y)
                        {

                            do
                            {
                                if (temp_y == index_y)
                                    sector = fun_set.min_max_index_x(DownCross, new Tuple<IPoint, IPoint>(road.Node_out, null), min_x, delta);
                                else
                                    sector = fun_set.min_max_index_x(DownCross, UpCross, min_x, delta);

                                for (int i = sector.Item1; i <= sector.Item2 && i <= SizeX - 1; i++)
                                {
                                    if (matrix[i, temp_y] == null)
                                        matrix[i, temp_y] = new List<IRoad>();
                                    matrix[i, temp_y].Add(road);
                                }

                                temp_y--;

                                UpCross = fun_set.find_cross_line_and_edge(min_y + (delta * (temp_y + 1)), road);
                                DownCross = fun_set.find_cross_line_and_edge(min_y + (delta * temp_y), road);


                            } while (road.Node_in.Y < DownCross.Item1.Y);

                            sector = fun_set.min_max_index_x(UpCross, new Tuple<IPoint, IPoint>(road.Node_in, null), min_x, delta);
                        }

                        for (int i = sector.Item1; i <= sector.Item2 && i<=SizeX-1; i++)
                        {
                            if (matrix[i, temp_y] == null)
                                matrix[i, temp_y] = new List<IRoad>();
                            matrix[i, temp_y].Add(road);
                        }


                    }
                }
            }
        }

        public List<IRoad>[,] Matrix
        {
            get
            {
                return matrix;
            }
        }
    }
}

