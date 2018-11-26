using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Xml.Linq;
using LinqCommon;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestrictionOperators
{
    /// <summary>
    /// Restriction Operatiors
    /// 1. Where
    /// Details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic3
    /// </summary>
    [TestClass]
    public class LinqExamples
    {
        [TestMethod]
        public void Restrict01()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var lowNums =
                from num in numbers
                where num < 5
                select num;

            Debug.WriteLine("Numbers < 5:");
            foreach (var x in lowNums)
            {
                Debug.WriteLine(x);
            }
        }

        [TestMethod]
        public void Restrict02()
        {
            List<Product> products = LinqHellper.GetProducts();

            var soldOutProducts =
                from prod in products
                where prod.UnitsInStock == 0
                select prod;

            Debug.WriteLine("Sold out products:");
            foreach (var product in soldOutProducts)
            {
                Debug.WriteLine("{0} is sold out!", product.ProductName);
            }
        }

        [TestMethod]
        public void Restrict03()
        {
            List<Product> products = LinqHellper.GetProducts();

            var expensiveInStockProducts =
                from prod in products
                where prod.UnitsInStock > 0 && prod.UnitPrice > 3.00M
                select prod;

            Debug.WriteLine("In-stock products that cost more than 3.00:");
            foreach (var product in expensiveInStockProducts)
            {
                Debug.WriteLine("{0} is in stock and costs more than 3.00.", product.ProductName);
            }
        }

        [TestMethod]
        public void Restrict04()
        {
            List<Customer> customers = LinqHellper.GetCustomers();

            var waCustomers =
                from cust in customers
                where cust.Region == "WA"
                select cust;

            Debug.WriteLine("Customers from Washington and their orders:");
            foreach (var customer in waCustomers)
            {
                Debug.WriteLine("Customer {0}: {1}", customer.CustomerID, customer.CompanyName);
                foreach (var order in customer.Orders)
                {
                    Debug.WriteLine("  Order {0}: {1}", order.OrderID, order.OrderDate);
                }
            }
        }

        /// <summary>
        /// This sample demonstrates an indexed where clause that returns digits whose name is shorter than their value.
        /// </summary>
        [TestMethod]
        public void Restrict05()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var shortDigits = digits.Where((digit, index) => digit.Length < index);

            Debug.WriteLine("Short digits:");
            foreach (var d in shortDigits)
            {
                Debug.WriteLine("The word {0} is shorter than its value.", d);
            }
        }

    }
}

