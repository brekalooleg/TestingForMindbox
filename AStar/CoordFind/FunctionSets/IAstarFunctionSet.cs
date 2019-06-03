using AStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar.CoordFind.FunctionSet
{
    public interface IAstarFunctionSet
    {
        /// <summary>
        /// Эвристическая функция алгоритма АStar. Возвращает эвристический вес расстояния между точками point и xpoint
        /// </summary>
        /// <param name="point"></param>
        /// <param name="xpoint"></param>
        /// <returns></returns>
        double heuristic_between_point(IPoint point, IPoint xpoint);

        /// <summary>
        /// Возвращает реальный вес расстояния между точками point и xpoint
        /// </summary>
        /// <param name="point"></param>
        /// <param name="xpoint"></param>
        /// <returns></returns>
        double between_point(IPoint point, IPoint xpoint);

        /// <summary>
        /// Возвращает реальный вес расстояния между точкой point и отрезком road
        /// </summary>
        /// <param name="point"></param>
        /// <param name="road"></param>
        /// <returns></returns>
        DropOnEdge heuristic_between_point_and_line(IPoint point, IRoad road);

        /// <summary>
        /// Возвращает 0-2 точки пересекающие горизонтальной медианы Y=Phi и круга с центром Point и радиусом R
        /// </summary>
        /// <param name="phi"> Координаты медианы  которая пересекает круг, Y = Phi</param>
        /// <param name="point"> Точка центра круга</param>
        /// <param name="r"> Радиус круга</param>
        /// <returns>Возвращает 0-2 точки пересечения медиа. 0 - при отсутствии пересечения, 1 - касание медианы к кругу, 2 - полное пересечение круга</returns>
        Tuple<IPoint, IPoint> find_cross_line_and_circle(double phi, IPoint point, double r);

        /// <summary>
        /// Возвращает 1-2 точки пересечения ребра и медианы Y=Phi
        /// </summary>
        /// <param name="phi"> Координаты медианы Y=Phi, которую пересекает ребро</param>
        /// <param name="edge"> Ребро, для которого ищется точка пересечения</param>
        /// <returns></returns>
        Tuple<IPoint,IPoint> find_cross_line_and_edge(double phi, IRoad edge);

        Tuple<Int32, Int32> min_max_index_x(Tuple<IPoint, IPoint> DownCross, Tuple<IPoint, IPoint> UpCross, double min_x, double delta);
    }
}
