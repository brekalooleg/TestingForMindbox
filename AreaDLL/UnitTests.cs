using NUnit.Framework;
using AreaDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaDLL
{

    [TestFixture]
    public class AreaDLLTesting
    {

        [Test]
        public void Test01Correct_TriagleAreaCalc()
        {
            var a = new Triangle(3, 4, 5);
            Assert.AreEqual(6, a.AreaCalc());
        }

        [Test]
        public void Test02Correct_CircleAreaCalc()
        {
            var a = new Circle(4);
            Assert.AreEqual(50.25, a.AreaCalc(), 0.1);
        }

        [Test]
        public void Test03_AreaCalcWithInterface()
        {
            IFigure a = new Triangle(3, 4, 5);
            Assert.AreEqual(6, a.AreaCalc());

            a = new Circle(4);
            Assert.AreEqual(50.25, a.AreaCalc(), 0.1);
        }

        [Test]
        public void Test04_TriagleWrongInput()
        {
            Assert.Throws<ArgumentException>(() => new Triangle(-1, 1, 2));
        }

        [Test]
        public void Test05_CircleWrongInput()
        {
            Assert.Throws<ArgumentException>(() => new Circle(0));
        }

        [Test]
        public void Test06_RightTriangle()
        {
            var a = new Triangle(3, 4, 5);
            Assert.IsTrue(a.IsRightTriangle());

            a = new Triangle(5, 5, 5);
            Assert.IsFalse(a.IsRightTriangle());
        }
    }
}
