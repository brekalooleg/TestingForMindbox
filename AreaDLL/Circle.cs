using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaDLL
{
    class Circle : IFigure
    {
        double r;

        public Circle(double r)
        {
            if (r <= 0 )
                throw new ArgumentException("Радиус не может быть меньше или равен нулю");

            this.r = r;
        }

        public double AreaCalc()
        {
            return Math.PI * r * r;
        }

        public double R
        {
            get
            {
                return r;
            }
        }
    }
}
