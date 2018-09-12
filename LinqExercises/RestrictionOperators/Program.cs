using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Xml.Linq;
using LinqCommon;

namespace RestrictionOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqExamples samples = new LinqExamples();

            //samples.Restrict01();

            samples.Restrict02();

            //samples.Restrict03();

            //samples.Restrict04();

            //samples.Restrict05();

            Console.ReadKey();
        }

        public class LinqExamples
        {
            public void Restrict01()
            {
                int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

                var lowNums =
                    from num in numbers
                    where num < 5
                    select num;

                Console.WriteLine("Numbers < 5:");
                foreach (var x in lowNums)
                {
                    Console.WriteLine(x);
                }
            }
            

            public void Restrict02()
            {
                List<Product> products = LinqHellper.GetProducts();

                var soldOutProducts =
                    from prod in products
                    where prod.UnitsInStock == 0
                    select prod;

                Console.WriteLine("Sold out products:");
                foreach (var product in soldOutProducts)
                {
                    Console.WriteLine("{0} is sold out!", product.ProductName);
                }
            }
            
            public void Restrict03()
            {
                List<Product> products = LinqHellper.GetProducts();

                var expensiveInStockProducts =
                    from prod in products
                    where prod.UnitsInStock > 0 && prod.UnitPrice > 3.00M
                    select prod;

                Console.WriteLine("In-stock products that cost more than 3.00:");
                foreach (var product in expensiveInStockProducts)
                {
                    Console.WriteLine("{0} is in stock and costs more than 3.00.", product.ProductName);
                }
            }
            
            public void Restrict04()
            {
                List<Customer> customers = LinqHellper.GetCustomers();

                var waCustomers =
                    from cust in customers
                    where cust.Region == "WA"
                    select cust;

                Console.WriteLine("Customers from Washington and their orders:");
                foreach (var customer in waCustomers)
                {
                    Console.WriteLine("Customer {0}: {1}", customer.CustomerID, customer.CompanyName);
                    foreach (var order in customer.Orders)
                    {
                        Console.WriteLine("  Order {0}: {1}", order.OrderID, order.OrderDate);
                    }
                }
            }

           /// <summary>
           /// This sample demonstrates an indexed where clause that returns digits whose name is shorter than their value.
           /// </summary>
            public void Restrict05()
            {
                string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                var shortDigits = digits.Where((digit, index) => digit.Length < index);

                Console.WriteLine("Short digits:");
                foreach (var d in shortDigits)
                {
                    Console.WriteLine("The word {0} is shorter than its value.", d);
                }
            }
            
        }
    }
}
