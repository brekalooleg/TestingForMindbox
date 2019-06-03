using System;

namespace AStar
{
   
    /// <summary>
    /// Внутренний объект А* включаюзий реальный вес и эвритический вес для вершины
    /// </summary>
    class OpenListValue: IComparable<OpenListValue>
    {
        public double RealWeight;
        public double HeuristicWeight;
        public NodeInWork Node;

        public OpenListValue(NodeInWork Node, double RealWeight, double HeuristicWeight)
        {
            this.RealWeight = RealWeight;
            this.HeuristicWeight = HeuristicWeight;
            this.Node = Node;
        }

        public OpenListValue(NodeInWork Node)
        {
            RealWeight = new double();
            HeuristicWeight = new double();
            this.Node = Node;
        }

        public int CompareTo(OpenListValue other)
        {
            return Math.Sign(other.HeuristicWeight- HeuristicWeight);
        }

        public override string ToString()
        {
            //return $"{id} ({X}, {Y})";
            return $"ID: {Node.Id}, Heuristic: {HeuristicWeight}, Real: {RealWeight}";
        }

    }
}
