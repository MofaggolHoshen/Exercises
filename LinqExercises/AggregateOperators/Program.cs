using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using LinqCommon;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AggregateOperators
{
    [TestClass]
    public class LinqExamples
    {
        [TestMethod]
        //This sample uses Count to get the number of unique prime factors of 300.
        public void Distinct01()
        {
            int[] primeFactorsOf300 = { 2, 2, 3, 5, 5 };

            //Won't take duplicate 
            int uniqueFactors = primeFactorsOf300.Distinct().Count();

            Debug.WriteLine("There are {0} unique prime factors of 300.", uniqueFactors);
        }

        [TestMethod]
        public void Count01()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            //This sample uses Count to get the number of odd ints in the array.
            int oddNumbers = numbers.Count(n => n % 2 == 1);

            Debug.WriteLine("There are {0} odd numbers in the list.", oddNumbers);
        }

        [TestMethod]
        //This sample uses Count to return a list of customers and how many orders each has.
        public void Projection01()
        {
            List<Customer> customers = LinqHellper.GetCustomers();

            var orderCounts =
                from cust in customers
                select new { cust.CustomerID, OrderCount = cust.Orders.Count() };

            foreach (var item in orderCounts)
            {
                Debug.WriteLine("Custormet Id:" + item.CustomerID + " Order Count:" + item.OrderCount);
            }
        }

        [TestMethod]
        //This sample uses Count to return a list of categories and how many products each has.
        public void Projection02()
        {
            List<Product> products = LinqHellper.GetProducts();

            var categoryCounts =
                    from prod in products
                    group prod by prod.Category into prodGroup
                    select new { Category = prodGroup.Key, ProductCount = prodGroup.Count() };

            foreach (var item in categoryCounts)
            {
                Debug.WriteLine("Category: " + item.Category + " Product Count: " + item.ProductCount);
            }
        }

        [TestMethod]
        //This sample uses Sum to get the total units in stock for each product category.
        public void Projection03()
        {
            List<Product> products = LinqHellper.GetProducts();

            var categories =
                from prod in products
                group prod by prod.Category into prodGroup
                select new { Category = prodGroup.Key, TotalUnitsInStock = prodGroup.Sum(p => p.UnitsInStock) };

            foreach (var item in categories)
            {
                Debug.WriteLine("Category: " + item.Category + " TotalUnitsInStock: " + item.TotalUnitsInStock);
            }
        }

        [TestMethod]
        //This sample uses Sum to add all the numbers in an array.
        public void Sum01()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            double numSum = numbers.Sum();

            Debug.WriteLine("The sum of the numbers is {0}.", numSum);
        }

        [TestMethod]
        //This sample uses Sum to get the total number of characters of all words in the array.
        public void Sum02()
        {
            string[] words = { "cherry", "apple", "blueberry" };

            double totalChars = words.Sum(w => w.Length);

            Debug.WriteLine("There are a total of {0} characters in these words.", totalChars);
        }

        [TestMethod]
        //This sample uses Min to get the lowest number in an array.
        public void Min01()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int minNum = numbers.Min();

            Debug.WriteLine("The minimum number is {0}.", minNum);
        }

        [TestMethod]
        //This sample uses Min to get the length of the shortest word in an array.
        public void Min02()
        {
            string[] words = { "cherry", "apple", "blueberry" };

            int shortestWord = words.Min(w => w.Length);

            Debug.WriteLine("The shortest word is {0} characters long.", shortestWord);
        }

        [TestMethod]
        //This sample uses Min to get the cheapest price among each category's products.
        public void GroupAndProjection01()
        {
            List<Product> products = LinqHellper.GetProducts();

            var categories =
                from prod in products
                group prod by prod.Category into prodGroup
                select new { Category = prodGroup.Key, CheapestPrice = prodGroup.Min(p => p.UnitPrice) };

            foreach (var item in categories)
            {
                Debug.WriteLine("Category: " + item.Category + " CheapestPrice: " + item.CheapestPrice);
            }
        }

        [TestMethod]
        //This sample uses Min to get the products with the lowest price in each category.
        public void Let01()
        {
            List<Product> products = LinqHellper.GetProducts();

            var categories =
                from prod in products
                group prod by prod.Category into prodGroup
                let minPrice = prodGroup.Min(p => p.UnitPrice)
                select new { Category = prodGroup.Key, CheapestProducts = prodGroup.Where(p => p.UnitPrice == minPrice) };

            foreach (var item in categories)
            {
                Debug.WriteLine("Category: " + item.Category + " CheapestProducts: " + item.CheapestProducts);
            }
        }

        [TestMethod]
        //This sample uses Max to get the highest number in an array. Note that the method returns a single value.
        public void Max01()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int maxNum = numbers.Max();

            Debug.WriteLine("The maximum number is {0}.", maxNum);
        }

        [TestMethod]
        //This sample uses Max to get the length of the longest word in an array.
        public void Max02()
        {
            string[] words = { "cherry", "apple", "blueberry" };

            int longestLength = words.Max(w => w.Length);

            Debug.WriteLine("The longest word is {0} characters long.", longestLength);
        }

        [TestMethod]
        //This sample uses Max to get the most expensive price among each category's products.
        public void GroupAndMax01()
        {
            var producs = LinqHellper.GetProducts();
            var categories =
                from prod in producs
                group prod by prod.Category into prodGroup
                select new { Category = prodGroup.Key, MostExpensivePrice = prodGroup.Max(p => p.UnitPrice) };


        }

        [TestMethod]
        //This sample uses Max to get the products with the most expensive price in each category.
        public void Let02()
        {
            var producs = LinqHellper.GetProducts();
            var categories =
                    from prod in producs
                    group prod by prod.Category into prodGroup
                    let maxPrice = prodGroup.Max(p => p.UnitPrice)
                    select new { Category = prodGroup.Key, MostExpensiveProducts = prodGroup.Where(p => p.UnitPrice == maxPrice) };

            foreach (var item in categories)
            {
                Debug.WriteLine("Category: " + item.Category + " MostExpensiveProduct: " + item.MostExpensiveProducts);
            }
        }

        [TestMethod]
        //This sample uses Average to get the average of all numbers in an array.
        public void Average01()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            double averageNum = numbers.Average();

            Debug.WriteLine("The average number is {0}.", averageNum);
        }

        [TestMethod]
        //This sample uses Average to get the average length of the words in the array.
        public void Average02()
        {
            string[] words = { "cherry", "apple", "blueberry" };

            double averageLength = words.Average(w => w.Length);

            Debug.WriteLine("The average word length is {0} characters.", averageLength);
        }

        [TestMethod]
        //This sample uses Average to get the average price of each category's products.
        public void GroupAndAverage()
        {
            List<Product> products = LinqHellper.GetProducts();

            var categories =
                from prod in products
                group prod by prod.Category into prodGroup
                select new { Category = prodGroup.Key, AveragePrice = prodGroup.Average(p => p.UnitPrice) };

            foreach (var item in categories)
            {
                Debug.WriteLine("Category: " + item.Category + " AvaragePrice: " + item.AveragePrice);
            }
        }

        [TestMethod]
        //This sample uses Aggregate to create a running product on the array that calculates the total product of all elements.
        public void Aggregate01()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

            //Frist value will be in runningProduct and second value will be in nextFactor, 
            //after multiplied store in runningProduct and nextFactor will take 3rd value again multiplied with stored value in runningProduct and so on
            double product = doubles.Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor);

            Debug.WriteLine("Total product of all numbers: {0}", product);
        }

        [TestMethod]
        //This sample uses Aggregate to create a running account balance that subtracts each withdrawal from the initial balance of 100, 
        //as long as the balance never drops below 0.
        public void Aggregate02()
        {
            double startBalance = 100.0;

            int[] attemptedWithdrawals = { 20, 10, 40, 50, 10, 70, 30 };

            //Here balance is startBalance and nextWithdrawal, go one by one from attemptedWithdrawals array values 
            double endBalance = attemptedWithdrawals.Aggregate(startBalance, (balance, nextWithdrawal) =>((nextWithdrawal <= balance) ? (balance - nextWithdrawal) : balance));

            Debug.WriteLine("Ending balance: {0}", endBalance);
        }

        [TestMethod]
        public void Aggregate03()
        {
            string[] fruits = { "apple", "mango", "orange", "passionfruit", "grape" };

            // Determine whether any string in the array is longer than "banana".
            string longestName = fruits.Aggregate("banana", (longest, next) => next.Length > longest.Length ? next : longest,
                                // Return the final result as an upper case string.
                                fruit => fruit.ToUpper());

            Debug.WriteLine("The fruit with the longest name is {0}.", longestName);

        }

        [TestMethod]
        public void Aggregate04()
        {

            int[] ints = { 4, 8, 8, 3, 9, 0, 7, 8, 2 };

            // Count the even numbers in the array, using a seed value of 0.
            int numEven = ints.Aggregate(0, (total, next) => next % 2 == 0 ? total + 1 : total);

            Debug.WriteLine("The number of even integers is: {0}", numEven);
        }
    }
}

