using System;

namespace AStar
{
    /// <summary>
    /// Интерфейс точки на плоскоси включающий её координаты 
    /// </summary>
    public interface IPoint: IEquatable<IPoint>
    {
        /// <summary>
        /// Координата Х
        /// </summary>
        double X
        {
            get;
        }

        /// <summary>
        /// Координата Y
        /// </summary>
        double Y
        {
            get;
        }
    }
}
