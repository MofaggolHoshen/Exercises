using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;
using LinqCommon;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PartitioningOperators
{

    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
    }
    [TestClass]
    public class LinqExamples
    {
        [TestMethod]
        public void LinqTake01()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var first3Numbers = numbers.Take(3);

            Debug.WriteLine("First 3 numbers:");
            foreach (var n in first3Numbers)
            {
                Debug.WriteLine(n);
            }
        }

        [TestMethod]
        public void LinqTake02()
        {
            List<Customer> customers = LinqHellper.GetCustomers();

            var first3WAOrders = (
                from cust in customers
                from order in cust.Orders
                where cust.Region == "WA"
                select new { cust.CustomerID, order.OrderID, order.OrderDate })
                .Take(3);

            Debug.WriteLine("First 3 orders in WA:");
            foreach (var order in first3WAOrders)
            {
                Debug.WriteLine($"Customer ID: {order.CustomerID} Order ID: {order.OrderID} Order Date: {order.OrderDate}");
            }
        }

        [TestMethod]
        public void LinqSkip01()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var allButFirst4Numbers = numbers.Skip(4);

            Debug.WriteLine("All but first 4 numbers:");
            foreach (var n in allButFirst4Numbers)
            {
                Debug.WriteLine(n);
            }
        }

        [TestMethod]
        public void LinqSkip02()
        {
            List<Customer> customers = LinqHellper.GetCustomers();

            var waOrders =
                from cust in customers
                from order in cust.Orders
                where cust.Region == "WA"
                select new { cust.CustomerID, order.OrderID, order.OrderDate };

            var allButFirst2Orders = waOrders.Skip(2);

            Debug.WriteLine("All but first 2 orders in WA:");
            foreach (var order in allButFirst2Orders)
            {
                Debug.WriteLine($"Customer ID: {order.CustomerID} Order ID: {order.OrderID} Order Date: {order.OrderDate}");
            }
        }

        [TestMethod]
        public void LinqTakeWhile01()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var firstNumbersLessThan6 = numbers.TakeWhile(n => n < 6);

            Debug.WriteLine("First numbers less than 6:");
            foreach (var num in firstNumbersLessThan6)
            {
                Debug.WriteLine(num);
            }
        }

        [TestMethod]
        public void LinqTakeWhile02()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var firstSmallNumbers = numbers.TakeWhile((n, index) => n >= index);

            Debug.WriteLine("First numbers not less than their position:");
            foreach (var n in firstSmallNumbers)
            {
                Debug.WriteLine(n);
            }
        }

        [TestMethod]
        public void LinqSkipWhile01()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            // In the lambda expression, 'n' is the input parameter that identifies each
            // element in the collection in succession. It is is inferred to be
            // of type int because numbers is an int array.
            var allButFirst3Numbers = numbers.SkipWhile(n => n % 3 != 0);

            Debug.WriteLine("All elements starting from first element divisible by 3:");
            foreach (var n in allButFirst3Numbers)
            {
                Debug.WriteLine(n);
            }
        }

        [TestMethod]
        public void LinqSkipWhile02()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var laterNumbers = numbers.SkipWhile((n, index) => n >= index);

            Debug.WriteLine("All elements starting from first element less than its position:");
            foreach (var n in laterNumbers)
            {
                Debug.WriteLine(n);
            }
        }
    }
}

