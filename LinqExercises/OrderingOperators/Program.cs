using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;
using LinqCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace OrderingOperators
{
    /// <summary>
    /// 1. OrderBy and ThenBy : Operators in the OrderBy/ThenBy family of operators order a sequence according to one or more keys.
    /// Details: https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic8
    /// </summary>

    [TestClass]
    public class LinqExamples
    {
        [TestMethod]
        public void LinqOrderBy01()
        {
            string[] words = { "cherry", "apple", "blueberry" };

            var sortedWords =
                from word in words
                orderby word
                select word;

            Debug.WriteLine("The sorted list of words:");
            foreach (var w in sortedWords)
            {
                Debug.WriteLine(w);
            }
        }

        /// <summary>
        /// "This sample uses orderby to sort a list of words by length."
        /// </summary>
        [TestMethod]
        public void LinqOrderBy02()
        {
            string[] words = { "cherry", "apple", "blueberry" };

            var sortedWords =
                from word in words
                orderby word.Length
                select word;

            Debug.WriteLine("The sorted list of words (by length):");
            foreach (var w in sortedWords)
            {
                Debug.WriteLine(w);
            }
        }

        [TestMethod]
        public void Linq30()
        {
            List<Product> products = LinqHellper.GetProducts();

            var sortedProducts =
                from prod in products
                orderby prod.ProductName
                select prod;
        }

        // Custom comparer for use with ordering operators

        public class CaseInsensitiveComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
            }
        }
        [TestMethod]
        public void LinqOrderBy03()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedWords = words.OrderBy(a => a, new CaseInsensitiveComparer());
        }
        [TestMethod]
        public void LinqOrderByDescending04()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

            var sortedDoubles =
                from d in doubles
                orderby d descending
                select d;

            Debug.WriteLine("The doubles from highest to lowest:");
            foreach (var d in sortedDoubles)
            {
                Debug.WriteLine(d);
            }
        }

        [TestMethod]
        public void LinqOrderByDescending05()
        {
            List<Product> products = LinqHellper.GetProducts();

            var sortedProducts =
                from prod in products
                orderby prod.UnitsInStock descending
                select prod;
        }

        [TestMethod]
        public void LinqOrderByDecending06()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedWords = words.OrderByDescending(a => a, new CaseInsensitiveComparer());

        }

        [TestMethod]
        public void LinqOrderBy06()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var sortedDigits =
                from digit in digits
                orderby digit.Length, digit
                select digit;

            Debug.WriteLine("Sorted digits:");
            foreach (var d in sortedDigits)
            {
                Debug.WriteLine(d);
            }
        }

        [TestMethod]
        public void LinqOrderByThenBy07()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedWords =
                words.OrderBy(a => a.Length)
                     .ThenBy(a => a, new CaseInsensitiveComparer());

            var sortedWords2 =
                from word in words
                orderby word.Length
                select word;

            var sortedWords3 = sortedWords2.ThenBy(a => a, new CaseInsensitiveComparer());
        }

        [TestMethod]
        public void LinqOderBy08()
        {
            List<Product> products = LinqHellper.GetProducts();

            var sortedProducts =
                from prod in products
                orderby prod.Category, prod.UnitPrice descending
                select prod;

        }

        [TestMethod]
        public void LinqOrderBy09()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedWords =
                words.OrderBy(a => a.Length)
                     .ThenByDescending(a => a, new CaseInsensitiveComparer());
        }

        [TestMethod]
        public void LinqOrderBy10()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var reversedIDigits = (
                from digit in digits
                where digit[1] == 'i'
                select digit)
                .Reverse();

            Debug.WriteLine("A backwards list of the digits with a second character of 'i':");
            foreach (var d in reversedIDigits)
            {
                Debug.WriteLine(d);
            }
        }
    }
}
