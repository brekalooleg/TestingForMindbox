namespace AStar
{
    /// <summary>
    /// Внутрений объект для составления результирующего маршрута алгоритма А*
    /// </summary>
    public class RoadInfo
    {
        public NodeInWork prev;
        public EdgeInWork edge;
        public bool         close;

        
        public RoadInfo(NodeInWork prev, EdgeInWork edge)
        {
            this.prev = prev;
            this.edge = edge;
            this.close = false;
        }

        public RoadInfo()
        {
            this.close = false;
        }
    }
}