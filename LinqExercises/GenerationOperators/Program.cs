using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace GenerationOperators
{
    /// <summary>
    /// Generation Operators
    /// 1. Range
    /// 2. Repeat
    /// 3. Empty
    /// More details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic14
    /// </summary>
    [TestClass]
    public class LinqExamples
    {
        //This sample uses Range to generate a sequence of numbers from 100 to 149 that is used to find which numbers in that range are odd and even.
        [TestMethod]
        public void Range01()
        {
            var numbers =
                from n in Enumerable.Range(100, 50)
                select new { Number = n, OddEven = n % 2 == 1 ? "odd" : "even" };

            foreach (var n in numbers)
            {
                Debug.WriteLine("The number {0} is {1}.", n.Number, n.OddEven);
            }
        }

        //This sample uses Repeat to generate a sequence that contains the number 7 ten times.
        [TestMethod]
        public void Repeat01()
        {
            var numbers = Enumerable.Repeat(7, 10);

            foreach (var n in numbers)
            {
                Debug.WriteLine(n);
            }
        }
    }
}
