using System.Collections.Generic;
using AStar.CoordFind;

namespace AStar
{
    /// <summary>
    /// Объект результат работы поиска пути между двумя точками на плоскости к рёбрам
    /// Включает в себя координаты на плоскости, падения пенпендикуляра, коорданаты концов маршута старта-фишиша и сам маршрут 
    /// </summary>
    public class ResultRoute
    {
        Path path;

        DropOnEdge startDropOnEdge;

        DropOnEdge finishDropOnEdge;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="route">Построенный маршрут</param>
        /// <param name="start_mitm">Стартовая точка на плоскости</param>
        /// <param name="finish_mitm">Конечная точка на плоскости</param>
        public ResultRoute(Path path,
               DropOnEdge start_DropOnEdge,
               DropOnEdge finish_mitm)
        {
            this.path = path;
            this.startDropOnEdge = start_DropOnEdge;
            this.finishDropOnEdge = finish_mitm;
        }

        /// <summary>
        /// Ссылка на начало маршрута для постоения
        /// </summary>
        public IPoint StartRoute
        {
            get
            {   
                if (startDropOnEdge.DropNodeFlag)                
                    return startDropOnEdge.Point_To;                
                 else 
                     return startDropOnEdge.Edge.Finish;
            }
        }

        /// <summary>
        /// Ссылка на конец маршрута для постоения
        /// </summary>
        public IPoint FinishRoute
        {
            get
            {
                if (finishDropOnEdge.DropNodeFlag)
                    return finishDropOnEdge.Point_To;
                else
                    return finishDropOnEdge.Edge.Start;
            }
        }

        /// <summary>
        /// Маршрут между начальным и конечным ребром
        /// </summary>
        public Path Path
        {
            get
            {
                return path;
            }
        }

        /// <summary>
        /// Стартовая точка на плоскости и её пенпендикуляр
        /// </summary>
        public DropOnEdge Start
        {
            get
            {
                return startDropOnEdge;
            }
        }

        /// <summary>
        /// Конечная точка на плоскости и её пенпендикуляр
        /// </summary>
        public DropOnEdge Finish
        {
            get
            {
                return finishDropOnEdge;
            }
        }
    }
}