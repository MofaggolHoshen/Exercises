using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;
using LinqCommon;

namespace PartitioningOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqExamples samples = new LinqExamples();
            
            samples.LinqTake01(); 
            samples.LinqTake02(); 

            samples.LinqSkip01(); 
            samples.LinqSkip02(); 

            samples.LinqTakeWhile01(); 
            samples.LinqTakeWhile02(); 

            samples.LinqSkipWhile01(); 
            samples.LinqSkipWhile02(); 
        }

        public class Order
        {
            public int OrderID { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal Total { get; set; }
        }
        
        class LinqExamples
        {
            public void LinqTake01()
            {
                int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

                var first3Numbers = numbers.Take(3);

                Console.WriteLine("First 3 numbers:");
                foreach (var n in first3Numbers)
                {
                    Console.WriteLine(n);
                }
            }
            
            public void LinqTake02()
            {
                List<Customer> customers = LinqHellper.GetCustomers();

                var first3WAOrders = (
                    from cust in customers
                    from order in cust.Orders
                    where cust.Region == "WA"
                    select new { cust.CustomerID, order.OrderID, order.OrderDate })
                    .Take(3);

                Console.WriteLine("First 3 orders in WA:");
                foreach (var order in first3WAOrders)
                {
                    Console.WriteLine($"Customer ID: {order.CustomerID} Order ID: {order.OrderID} Order Date: {order.OrderDate}");
                }
            }

            public void LinqSkip01()
            {
                int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

                var allButFirst4Numbers = numbers.Skip(4);

                Console.WriteLine("All but first 4 numbers:");
                foreach (var n in allButFirst4Numbers)
                {
                    Console.WriteLine(n);
                }
            }
            
            public void LinqSkip02()
            {
                List<Customer> customers = LinqHellper.GetCustomers();

                var waOrders =
                    from cust in customers
                    from order in cust.Orders
                    where cust.Region == "WA"
                    select new { cust.CustomerID, order.OrderID, order.OrderDate };

                var allButFirst2Orders = waOrders.Skip(2);

                Console.WriteLine("All but first 2 orders in WA:");
                foreach (var order in allButFirst2Orders)
                {
                    Console.WriteLine($"Customer ID: {order.CustomerID} Order ID: {order.OrderID} Order Date: {order.OrderDate}");
                }
            }
            
            public void LinqTakeWhile01()
            {
                int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

                var firstNumbersLessThan6 = numbers.TakeWhile(n => n < 6);

                Console.WriteLine("First numbers less than 6:");
                foreach (var num in firstNumbersLessThan6)
                {
                    Console.WriteLine(num);
                }
            }
            
            public void LinqTakeWhile02()
            {
                int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

                var firstSmallNumbers = numbers.TakeWhile((n, index) => n >= index);

                Console.WriteLine("First numbers not less than their position:");
                foreach (var n in firstSmallNumbers)
                {
                    Console.WriteLine(n);
                }
            }
            
            public void LinqSkipWhile01()
            {
                int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

                // In the lambda expression, 'n' is the input parameter that identifies each
                // element in the collection in succession. It is is inferred to be
                // of type int because numbers is an int array.
                var allButFirst3Numbers = numbers.SkipWhile(n => n % 3 != 0);

                Console.WriteLine("All elements starting from first element divisible by 3:");
                foreach (var n in allButFirst3Numbers)
                {
                    Console.WriteLine(n);
                }
            }
            
            public void LinqSkipWhile02()
            {
                int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

                var laterNumbers = numbers.SkipWhile((n, index) => n >= index);

                Console.WriteLine("All elements starting from first element less than its position:");
                foreach (var n in laterNumbers)
                {
                    Console.WriteLine(n);
                }
            }
        }
    }
}
