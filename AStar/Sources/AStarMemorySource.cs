using System.Collections.Generic;

namespace AStar.Source
{
    /// <summary>
    /// Объект ридер графа из памяти
    /// </summary>
    public class AStarMemorySource : IAstarSource
    {
        public IEnumerator<NodeFormSource> node_enum;
        public IEnumerator<EdgeFromSource> edge_enum;

        /// <summary>
        /// Создание источника на основании адресов двух энумерейбалов содержащих вершины и ребра
        /// </summary>
        /// <param name="nodes">Вершины</param>
        /// <param name="edges">Ребра</param>
        public AStarMemorySource(IEnumerable<NodeFormSource> nodes, IEnumerable<EdgeFromSource> edges)
        {
            node_enum = nodes.GetEnumerator();
            edge_enum = edges.GetEnumerator();
        }

        public void Dispose()
        {
            node_enum.Dispose();
            edge_enum.Dispose();
        }

        /// <summary>
        /// Возврат ребра из БД, если ребра кончились возвращает Null
        /// </summary>
        /// <returns></returns>
        public EdgeFromSource ReadeEdge()
        {
            if (edge_enum.MoveNext())
                return edge_enum.Current;
            else
                return null;
        }

        /// <summary>
        /// Возврат вершины из БД, если вершины кончились возвращает Null
        /// </summary>
        /// <returns></returns>
        public NodeFormSource ReadNode()
        {
            if (node_enum.MoveNext())
                return node_enum.Current;
            else
                return null;
        }
    }
}
