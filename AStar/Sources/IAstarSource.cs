using System;

namespace AStar.Source
{
    public interface IAstarSource: IDisposable
    {
        /// <summary>
        /// Функция читает очередную ноду из источника, если ноды прочитаны - возвращается null
        /// </summary>
        /// <returns></returns>
        NodeFormSource ReadNode();

        /// <summary>
        /// Функция читает очередное ребро из источника, если ребра прочитаны - возвращается null
        /// </summary>
        /// <returns></returns>
        EdgeFromSource ReadeEdge();
    }
}
