using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;
using LinqCommon;

namespace OrderingOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqExamples samples = new LinqExamples();

            samples.LinqOrderBy01();
            samples.LinqOrderBy02();
            samples.Linq30();
            samples.LinqOrderBy03();
            samples.LinqOrderByDescending04();
            samples.LinqOrderByDescending05();
            samples.LinqOrderByDecending06();
            samples.LinqOrderBy06();
            samples.LinqOrderByThenBy07();
            samples.LinqOderBy08();
            samples.LinqOrderBy09();
            samples.LinqOrderBy10();

        }

        class LinqExamples
        {
            public void LinqOrderBy01()
            {
                string[] words = { "cherry", "apple", "blueberry" };

                var sortedWords =
                    from word in words
                    orderby word
                    select word;

                Console.WriteLine("The sorted list of words:");
                foreach (var w in sortedWords)
                {
                    Console.WriteLine(w);
                }
            }

            /// <summary>
            /// "This sample uses orderby to sort a list of words by length."
            /// </summary>
            public void LinqOrderBy02()
            {
                string[] words = { "cherry", "apple", "blueberry" };

                var sortedWords =
                    from word in words
                    orderby word.Length
                    select word;

                Console.WriteLine("The sorted list of words (by length):");
                foreach (var w in sortedWords)
                {
                    Console.WriteLine(w);
                }
            }

            /// <summary>
            /// 
            /// </summary>
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

            public void LinqOrderBy03()
            {
                string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

                var sortedWords = words.OrderBy(a => a, new CaseInsensitiveComparer());
            }

            public void LinqOrderByDescending04()
            {
                double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

                var sortedDoubles =
                    from d in doubles
                    orderby d descending
                    select d;

                Console.WriteLine("The doubles from highest to lowest:");
                foreach (var d in sortedDoubles)
                {
                    Console.WriteLine(d);
                }
            }


            public void LinqOrderByDescending05()
            {
                List<Product> products = LinqHellper.GetProducts();

                var sortedProducts =
                    from prod in products
                    orderby prod.UnitsInStock descending
                    select prod;
            }

            public void LinqOrderByDecending06()
            {
                string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

                var sortedWords = words.OrderByDescending(a => a, new CaseInsensitiveComparer());

            }

            public void LinqOrderBy06()
            {
                string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                var sortedDigits =
                    from digit in digits
                    orderby digit.Length, digit
                    select digit;

                Console.WriteLine("Sorted digits:");
                foreach (var d in sortedDigits)
                {
                    Console.WriteLine(d);
                }
            }


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

            public void LinqOderBy08()
            {
                List<Product> products = LinqHellper.GetProducts();

                var sortedProducts =
                    from prod in products
                    orderby prod.Category, prod.UnitPrice descending
                    select prod;

            }

            public void LinqOrderBy09()
            {
                string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

                var sortedWords =
                    words.OrderBy(a => a.Length)
                         .ThenByDescending(a => a, new CaseInsensitiveComparer());
            }

            public void LinqOrderBy10()
            {
                string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                var reversedIDigits = (
                    from digit in digits
                    where digit[1] == 'i'
                    select digit)
                    .Reverse();

                Console.WriteLine("A backwards list of the digits with a second character of 'i':");
                foreach (var d in reversedIDigits)
                {
                    Console.WriteLine(d);
                }
            }
        }
    }
}
