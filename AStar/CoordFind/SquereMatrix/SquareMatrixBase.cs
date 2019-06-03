using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar.CoordFind.SquareMatrix
{
    public abstract class SquareMatrixBase
    {
        protected double min_x = Double.PositiveInfinity;
        protected double max_x = Double.NegativeInfinity;
        protected double min_y = Double.PositiveInfinity;
        protected double max_y = Double.NegativeInfinity;
        protected double delta;
        protected int size_x;
        protected int size_y;

        protected SquareMatrixBase()
        {

        }

        public double MinX
        {
            get
            {
                return min_x;
            }
        }

        public double MaxX
        {
            get
            {
                return max_x;
            }
        }

        public double MinY
        {
            get
            {
                return min_y;
            }
        }

        public double MaxY
        {
            get
            {
                return max_y;
            }
        }

        public double Delta
        {
            get
            {
                return delta;
            }
        }

        public int SizeX
        {
            get
            {
                return size_x;
            }
        }

        public int SizeY
        {
            get
            {
                return size_y;
            }
        }        

    }
}
