using System.Data.SqlClient;

namespace AStar.Source
{

    /// <summary>
    /// Объект ридер графа из БД
    /// </summary>
    public class AStarDatabaseSource : IAstarSource
    {
        string connecrion_string;
        SqlConnection connection_for_nodes;
        SqlConnection connection_for_edges;
        SqlDataReader reader_node;
        SqlDataReader reader_edge;

        /// <summary>
        /// Создание объекта источника из БД
        /// </summary>
        /// <param name="connecrion_string">Строка соединения с БД</param>
        public AStarDatabaseSource(string connecrion_string)
        {
            this.connecrion_string = connecrion_string;
        }

        public void Dispose()
        {
            reader_node?.Dispose();
            reader_edge?.Dispose();
            connection_for_nodes?.Dispose();
            connection_for_edges?.Dispose();
        }
        
        /// <summary>
        /// Возврат ребра из БД, если ребра кончились возвращает Null
        /// </summary>
        /// <returns></returns>
        public EdgeFromSource ReadeEdge()
        {
            if (connection_for_edges == null)
            {
                connection_for_edges = new SqlConnection(connecrion_string);
                connection_for_edges.Open();

                var cmd = connection_for_edges.CreateCommand();
                cmd.CommandText = "SELECT ID_EDGE, NODE_IN, NODE_OUT, WEIGHT FROM EDGES";
                reader_edge = cmd.ExecuteReader();
            }

            if (reader_edge.Read())
            {
                return new EdgeFromSource() { Id = reader_edge.GetInt32(0), Node_in = reader_edge.GetInt32(1), Node_out = reader_edge.GetInt32(2), Weight = reader_edge.GetDouble(3) };
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
            if (connection_for_nodes == null)
            {
                connection_for_nodes = new SqlConnection(connecrion_string);
                connection_for_nodes.Open();

                var cmd = connection_for_nodes.CreateCommand();
                cmd.CommandText = "SELECT ID, X, Y FROM NODES";
                reader_node = cmd.ExecuteReader();
            }

            if (reader_node.Read())
                return new NodeFormSource() { id = reader_node.GetInt32(0), X = reader_node.GetDouble(1), Y = reader_node.GetDouble(2) };
            else
                return null;
        }
    }    
}
