using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsKeyWord
{
    [TestClass]
    public class ConstantPattern
    {
        [TestMethod]
        public void TestMethod()
        {
            var d1 = new Dice();
            ShowValue(d1);
        }

        private static void ShowValue(object o)
        {
            const int HIGH_ROLL = 6;

            if (o is Dice d && d.Roll() is HIGH_ROLL)
                Debug.WriteLine($"The value is {HIGH_ROLL}!");
            else
                Debug.WriteLine($"The dice roll is not a {HIGH_ROLL}!");
        }
    }
    public class Dice
    {
        Random rnd = new Random();
        public Dice()
        {
        }
        public int Roll()
        {
            return rnd.Next(1, 7);
        }
    }

    // The example displays output like the following:
    //      The value is 6!
}
