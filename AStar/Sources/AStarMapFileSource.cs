using System;
using System.IO;

namespace AStar.Source
{

    /// <summary>
    /// Объект ридер графа из Файла
    /// </summary>
    public class AStarMapFileSource : IAstarSource
    {        
        string nodes_file;
        string edges_file;
        int edges_count = 0;
        StreamReader reader_node;
        StreamReader reader_edge;

        /// <summary>
        /// Создание источника на основании адресов двух файлов вершин и ребер
        /// </summary>
        /// <param name="nodes_file">Адрес файла вершин</param>
        /// <param name="edges_file">Адрес файла ребер</param>
        public AStarMapFileSource(string nodes_file, string edges_file)
        {
            this.nodes_file = nodes_file;
            this.edges_file = edges_file;
        }

        public void Dispose()
        {            
            reader_node?.Dispose();
            reader_edge?.Dispose();
        }

        /// <summary>
        /// Возврат ребра из БД, если ребра кончились возвращает Null
        /// </summary>
        /// <returns></returns>
        public EdgeFromSource ReadeEdge()
        {
            if (reader_edge == null)            
                reader_edge = new StreamReader(edges_file);

            string line;
            if ((line = reader_edge.ReadLine()) != null)
            {                
                string[] edge_line = line.Split(' ');
                return new EdgeFromSource() { Id = ++edges_count, Node_out = Convert.ToInt32(edge_line[1]), Node_in = Convert.ToInt32(edge_line[2]), Weight = Convert.ToInt32(edge_line[3]) };
            }
            else
                return null;
        }

        /// <summary>
        /// Возврат вершины из БД, если вершины кончились возвращает Null
        /// </summary>
        /// <returns></returns>
        public NodeFormSource ReadNode()
        {
            if (reader_node == null)
            {
                reader_node = new StreamReader(nodes_file);
            }
            string line;
            if ((line = reader_node.ReadLine()) != null)
            {
                string[] node_line = line.Split(' ');
                return new NodeFormSource() { id = Convert.ToInt32(node_line[1]), X = Convert.ToInt32(node_line[2]), Y = Convert.ToInt32(node_line[3]) };
            }
            else
                return null;
        }
    }
}