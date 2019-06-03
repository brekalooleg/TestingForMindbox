using AStar.Source;
using System;
using System.Collections.Generic;

namespace AStar
{
    /// <summary>
    /// Граф храняший словарь из индесов-вершин. Каждая вершина хранит исходящие из неё ребра
    /// Требует предварительной загрузки графа из любого из источника, но один раз
    /// </summary>
    public class Graph
    {
        Dictionary<int, NodeInWork> nodes;

        public Graph()
        {

        }

        /// <summary>
        /// Загрузка вершин и ребер из разного рода источников
        /// Может быть выполненно только один раз для такого объекта
        /// </summary>
        /// <param name="source">Объект-источник содержищий граф</param>
        public  void Load(IAstarSource source)
        {

            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (nodes == null)
            {
                nodes = new Dictionary<int, NodeInWork>();

                bool cont = true;
                while (cont)
                {
                    NodeFormSource info = source.ReadNode();
                    if (info != null)
                        nodes.Add(info.id, new NodeInWork(info.X, info.Y, info.id));
                    else
                        cont = false;
                }
                cont = true;
                while (cont)
                {
                    EdgeFromSource info = source.ReadeEdge();
                    if (info != null)
                        nodes[info.Node_out].edge.Add(new EdgeInWork(info.Id, nodes[info.Node_out], nodes[info.Node_in], info.Weight));
                    else
                        cont = false;
                }

                source.Dispose();
                source = null;
            }
            else
                throw new InvalidOperationException("Graph can be load once");
        }

        /// <summary>
        /// Возвращает набор вершин вместе с их ребрами
        /// </summary>
        public IEnumerable<NodeInWork> Nodes
        {
            get
            {
                return nodes.Values;
            }
        }

        public NodeInWork this[int id]
        {
            get
            {
                return nodes[id];
            }
        }

        public bool TryGetValue(int id, out NodeInWork value)
        {
            return nodes.TryGetValue(id, out value);
        }
    }
}
