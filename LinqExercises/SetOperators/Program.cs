using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;
using LinqCommon;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SetOperators
{
    [TestClass]
    public class LinqExamples
    {
        /// <summary>
        /// The Distinct operator eliminates duplicate elements from a sequence.
        /// </summary>
        [TestMethod]
        public void LinqDistinct01()
        {
            int[] list = { 2, 2, 3, 5, 5 };

            var uniqueFactors = list.Distinct();

            Debug.WriteLine("Unique values:");
            foreach (var f in uniqueFactors)
            {
                Debug.WriteLine(f);
            }
        }

        /// <summary>
        /// The Distinct operator eliminates duplicate elements from a sequence. 
        /// This sample uses Distinct to find the unique Category names.
        /// </summary>
        [TestMethod]
        public void LinqDistinct02()
        {
            List<Product> products = LinqHellper.GetProducts();

            var categoryNames = (
                from prod in products
                select prod.Category)
                .Distinct();

            Debug.WriteLine("Category names:");
            foreach (var n in categoryNames)
            {
                Debug.WriteLine(n);
            }
        }

        /// <summary>
        /// The Union operator produces the set union of two sequences.
        /// The Union operator allocates and returns an enumerable object that captures the arguments passed to the operator. An ArgumentNullException is thrown if any argument is null.
        /// </summary>
        [TestMethod]
        public void LinqUnion01()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var uniqueNumbers = numbersA.Union(numbersB);

            Debug.WriteLine("Unique numbers from two arrays:");
            foreach (var n in uniqueNumbers)
            {
                Debug.WriteLine(n);
            }
        }

        /// <summary>
        /// The Union operator produces the set union of two sequences.
        /// This sample uses the Union method to create one sequence that contains the unique first letter 
        /// from both product and customer names. Union is only available through method syntax.
        /// The Union operator allocates and returns an enumerable object that captures the arguments passed to the operator. An ArgumentNullException is thrown if any argument is null.
        /// </summary>
        [TestMethod]
        public void LinqUnion02()
        {
            List<Product> products = LinqHellper.GetProducts();
            List<Customer> customers = LinqHellper.GetCustomers();

            var productFirstChars =
                from prod in products
                select prod.ProductName[0];
            var customerFirstChars =
                from cust in customers
                select cust.CompanyName[0];

            var uniqueFirstChars = productFirstChars.Union(customerFirstChars);

            Debug.WriteLine("Unique first letters from Product names and Customer names:");
            foreach (var ch in uniqueFirstChars)
            {
                Debug.WriteLine(ch);
            }
        }

        /// <summary>
        /// The Intersect operator produces the set intersection of two sequences.
        /// The Intersect operator allocates and returns an enumerable object that captures the arguments passed to the operator. An ArgumentNullException is thrown if any argument is null.
        /// </summary>
        [TestMethod]
        public void LinqIntersect01()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var commonNumbers = numbersA.Intersect(numbersB);

            Debug.WriteLine("Common numbers shared by both arrays:");
            foreach (var n in commonNumbers)
            {
                Debug.WriteLine(n);
            }
        }

        [TestMethod]
        public void LinqIntersect02()
        {
            List<Product> products = LinqHellper.GetProducts();
            List<Customer> customers = LinqHellper.GetCustomers();

            var productFirstChars =
                from prod in products
                select prod.ProductName[0];
            var customerFirstChars =
                from cust in customers
                select cust.CompanyName[0];

            var commonFirstChars = productFirstChars.Intersect(customerFirstChars);

            Debug.WriteLine("Common first letters from Product names and Customer names:");
            foreach (var ch in commonFirstChars)
            {
                Debug.WriteLine(ch);
            }
        }

        /// <summary>
        /// The Except operator produces the set difference between two sequences.
        /// The Except operator allocates and returns an enumerable object that captures the arguments passed to the operator. An ArgumentNullException is thrown if any argument is null.
        /// </summary>
        public void LinqExcept01()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            IEnumerable<int> aOnlyNumbers = numbersA.Except(numbersB);

            Debug.WriteLine("Numbers in first array but not in second array:");
            foreach (var n in aOnlyNumbers)
            {
                Debug.WriteLine(n);
            }
        }

        /// <summary>
        /// The Except operator produces the set difference between two sequences.
        /// The Except operator allocates and returns an enumerable object that captures the arguments passed to the operator. An ArgumentNullException is thrown if any argument is null.
        /// </summary>
        public void LinqExcept02()
        {
            List<Product> products = LinqHellper.GetProducts();
            List<Customer> customers = LinqHellper.GetCustomers();

            var productFirstChars =
                from prod in products
                select prod.ProductName[0];
            var customerFirstChars =
                from cust in customers
                select cust.CompanyName[0];

            var productOnlyFirstChars = productFirstChars.Except(customerFirstChars);

            Debug.WriteLine("First letters from Product names, but not from Customer names:");
            foreach (var ch in productOnlyFirstChars)
            {
                Debug.WriteLine(ch);
            }
        }
    }
}

