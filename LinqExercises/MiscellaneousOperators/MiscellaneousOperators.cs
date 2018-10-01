using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LinqCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class MiscellaneousOperators
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        /// <summary>
        /// This sample uses Concat to create one sequence that contains each array's values, one after the other.
        /// </summary>
        [TestMethod]
        public void LinqConcat01()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var allNumbers = numbersA.Concat(numbersB);

            Debug.WriteLine("All numbers from both arrays:");
            foreach (var n in allNumbers)
            {
                Debug.WriteLine(n);
            }
        }

        /// <summary>
        /// This sample uses Concat to create one sequence that contains the names of all customers and products, including any duplicates.
        /// </summary>
        public void LinqConcat02()
        {
            List<Customer> customers = LinqHellper.GetCustomers();
            List<Product> products = LinqHellper.GetProducts();

            var customerNames =
                from cust in customers
                select cust.CompanyName;
            var productNames =
                from prod in products
                select prod.ProductName;

            var allNames = customerNames.Concat(productNames);

            Debug.WriteLine("Customer and product names:");
            foreach (var n in allNames)
            {
                Debug.WriteLine(n);
            }
        }

        /// <summary>
        /// This sample uses SequenceEquals to see if two sequences match on all elements in the same order.
        /// </summary>
        public void LinqSequenceEqual01()
        {
            var wordsA = new string[] { "cherry", "apple", "blueberry" };
            var wordsB = new string[] { "cherry", "apple", "blueberry" };

            bool match = wordsA.SequenceEqual(wordsB);

            Debug.WriteLine("The sequences match: {0}", match);
        }


        public void LinqSequenceEqual02()
        {
            var wordsA = new string[] { "cherry", "apple", "blueberry" };
            var wordsB = new string[] { "apple", "blueberry", "cherry" };

            bool match = wordsA.SequenceEqual(wordsB);

            Debug.WriteLine("The sequences match: {0}", match);
        }
    }
}
