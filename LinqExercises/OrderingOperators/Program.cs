﻿using System;
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

            //Comment or uncomment the method calls below to run or not

              samples.LinqOrderBy();

            samples.Linq29();

            samples.Linq30();


            samples.Linq31();


            samples.Linq32();


            samples.Linq33();


            samples.Linq34();


            samples.Linq35();


            samples.Linq36();




            samples.Linq37();


            samples.Linq38();



            samples.Linq39();

        }
        
        class LinqExamples
        {
            public void LinqOrderBy()
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

            [Category("Ordering Operators")]
            [Description("This sample uses orderby to sort a list of words by length.")]
            public void Linq29()
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

            [Category("Ordering Operators")]
            [Description("This sample uses orderby to sort a list of products by name. " +
                        "Use the \"descending\" keyword at the end of the clause to perform a reverse ordering.")]
            public void Linq30()
            {
                List<Product> products = LinqHellper.GetProducts();

                var sortedProducts =
                    from prod in products
                    orderby prod.ProductName
                    select prod;

                //ObjectDumper.Write(sortedProducts);
            }

            // Custom comparer for use with ordering operators
            public class CaseInsensitiveComparer : IComparer<string>
            {
                public int Compare(string x, string y)
                {
                    return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
                }
            }

            [Category("Ordering Operators")]
            [Description("This sample uses an OrderBy clause with a custom comparer to " +
                         "do a case-insensitive sort of the words in an array.")]
            public void Linq31()
            {
                string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

                var sortedWords = words.OrderBy(a => a, new CaseInsensitiveComparer());

                //ObjectDumper.Write(sortedWords);
            }

            [Category("Ordering Operators")]
            [Description("This sample uses orderby and descending to sort a list of " +
                         "doubles from highest to lowest.")]
            public void Linq32()
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

            [Category("Ordering Operators")]
            [Description("This sample uses orderby to sort a list of products by units in stock " +
                         "from highest to lowest.")]
            public void Linq33()
            {
                List<Product> products = LinqHellper.GetProducts();

                var sortedProducts =
                    from prod in products
                    orderby prod.UnitsInStock descending
                    select prod;

                //ObjectDumper.Write(sortedProducts);
            }

            [Category("Ordering Operators")]
            [Description("This sample uses method syntax to call OrderByDescending because it " +
                        " enables you to use a custom comparer.")]
            public void Linq34()
            {
                string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

                var sortedWords = words.OrderByDescending(a => a, new CaseInsensitiveComparer());

                //ObjectDumper.Write(sortedWords);
            }

            [Category("Ordering Operators")]
            [Description("This sample uses a compound orderby to sort a list of digits, " +
                         "first by length of their name, and then alphabetically by the name itself.")]
            public void Linq35()
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

            [Category("Ordering Operators")]
            [Description("The first query in this sample uses method syntax to call OrderBy and ThenBy with a custom comparer to " +
                         "sort first by word length and then by a case-insensitive sort of the words in an array. " +
                         "The second two queries show another way to perform the same task.")]
            public void Linq36()
            {
                string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

                var sortedWords =
                    words.OrderBy(a => a.Length)
                         .ThenBy(a => a, new CaseInsensitiveComparer());

                // Another way. TODO is this use of ThenBy correct? It seems to work on this sample array.
                var sortedWords2 =
                    from word in words
                    orderby word.Length
                    select word;

                var sortedWords3 = sortedWords2.ThenBy(a => a, new CaseInsensitiveComparer());

                //ObjectDumper.Write(sortedWords);

                //ObjectDumper.Write(sortedWords3);
            }

            [Category("Ordering Operators")]
            [Description("This sample uses a compound orderby to sort a list of products, " +
                         "first by category, and then by unit price, from highest to lowest.")]
            public void Linq37()
            {
                List<Product> products = LinqHellper.GetProducts();

                var sortedProducts =
                    from prod in products
                    orderby prod.Category, prod.UnitPrice descending
                    select prod;

                //ObjectDumper.Write(sortedProducts);
            }

            [Category("Ordering Operators")]
            [Description("This sample uses an OrderBy and a ThenBy clause with a custom comparer to " +
                         "sort first by word length and then by a case-insensitive descending sort " +
                         "of the words in an array.")]
            public void Linq38()
            {
                string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

                var sortedWords =
                    words.OrderBy(a => a.Length)
                         .ThenByDescending(a => a, new CaseInsensitiveComparer());

                //ObjectDumper.Write(sortedWords);
            }

            [Category("Ordering Operators")]
            [Description("This sample uses Reverse to create a list of all digits in the array whose " +
                         "second letter is 'i' that is reversed from the order in the original array.")]
            public void Linq39()
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
