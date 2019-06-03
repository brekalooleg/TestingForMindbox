using System;

namespace AStar.CoordFind
{
    /// <summary>
    /// Упрощенный класс-ребро
    /// </summary>
    class SimpleRoad : IRoad
    {

        double x;
        double y;

        double x0;
        double y0;


        public SimpleRoad(IPoint start, IPoint finish) {
            x0 = start.X;
            y0 = start.Y;

            x = finish.X;
            y = finish.Y;
        }

        public SimpleRoad (double x0, double y0, double x, double y)
        {
            this.x = x;
            this.y = y;

            this.x0 = x0;
            this.y0 = y0;
        }

        public double A
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public double B
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public double C
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IPoint Finish
        {
            get
            {
                return new SimplePoint(x,y);
            }
        }

        public IPoint Start
        {
            get
            {
                return new SimplePoint(x0,y0);
            }
        }

        public double X
        {
            get
            {
                return x;
            }
        }

        public double X0
        {
            get
            {
                return x0;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
        }

        public double Y0
        {
            get
            {
                return y0;
            }
        }
    }
}
