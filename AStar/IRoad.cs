namespace AStar
{
    /// <summary>
    /// Интерфейс ребра на графе включающий в себя координаты старта и финиша.
    /// Так же уровнение линейной функции для данного ребра 
    /// </summary>
    public interface IRoad
    {
        /// <summary>
        /// Конечная координата Х
        /// </summary>
        double X
        {
            get;
        }
        /// <summary>
        /// Конечная координата Y
        /// </summary>
        double Y
        {
            get;
        }

        /// <summary>
        /// Начальная координата Х
        /// </summary>
        double X0
        {
            get;
        }
        /// <summary>
        /// Начальная координата Y
        /// </summary>
        double Y0
        {
            get;
        }
        /// <summary>
        /// Коэффициент линейной функции вершины А
        /// </summary>
        double A
        {
            get;
        }
        /// <summary>
        /// Коэффициент линейной функции вершины B
        /// </summary>
        double B
        {
            get;
        }
        /// <summary>
        /// Коэффициент линейной функции вершины C
        /// </summary>
        double C
        {
            get;
        }

        /// <summary>
        /// Координата старта ребра
        /// </summary>
        IPoint Start
        {
            get;
        }

        /// <summary>
        /// Координата конца ребра
        /// </summary>
        IPoint Finish
        {
            get;
        }
    }
}