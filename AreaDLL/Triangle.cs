using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaDLL
{
    public class Triangle : IFigure
    {
        double a;
        double b;
        double c;
        
        public Triangle (double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentException("Сторона треугольника не должна быть меньше или равна нулю");

            this.a = a;
            this.b = b;
            this.c = c;
        }

        public double AreaCalc()
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public bool IsRightTriangle()
        {
            return (c * c) == (a * a) + (b * b);
        }

        public double A
        {
            get
            {
                return a;
            }
        }

        public double B
        {
            get
            {
                return b;
            }
        }

        public double C
        {
            get
            {
                return c;
            }
        }
    }
}
