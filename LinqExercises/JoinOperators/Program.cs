using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;
using LinqCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace JoinOperators
{
    /// <summary>
    /// Join Operators
    /// 1. Join
    /// 2. GroupJoin
    /// Details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic6
    /// </summary>
    [TestClass]
    public class LinqExamples
    {
        /// <summary>
        /// This sample shows how to perform a simple inner equijoin of two sequences to produce a flat result set that consists of each element in 
        /// suppliers that has a matching element in customers.
        /// </summary>
        [TestMethod]
        public void LinqJoin01()
        {

            List<Customer> customers = LinqHellper.GetCustomers();
            List<Supplier> suppliers = LinqHellper.GetSupplier();

            var custSupJoin =
                from sup in suppliers
                join cust in customers on sup.Country equals cust.Country
                select new { Country = sup.Country, SupplierName = sup.SupplierName, CustomerName = cust.CompanyName };

            foreach (var item in custSupJoin)
            {
                Debug.WriteLine("Country = {0}, Supplier = {1}, Customer = {2}", item.Country, item.SupplierName, item.CustomerName);
            }
        }

        /// <summary>
        /// A group join produces a hierarchical sequence. The following query is an inner join  that produces a sequence of objects, 
        /// each of which has a key and an inner sequence of all matching elements.
        /// </summary>
        [TestMethod]
        public void LinqJoin02()
        {


            List<Customer> customers = LinqHellper.GetCustomers();
            List<Supplier> suppliers = LinqHellper.GetSupplier();

            var custSupQuery =
                from sup in suppliers
                join cust in customers on sup.Country equals cust.Country into cs
                select new { Key = sup.Country, Items = cs };


            foreach (var item in custSupQuery)
            {
                Debug.WriteLine(item.Key + ":");
                foreach (var element in item.Items)
                {
                    Debug.WriteLine("   " + element.CompanyName);
                }
            }
        }

        /// <summary>
        /// The group join operator is more general than join, as this slightly more verbose version of the cross join sample shows.
        /// </summary>
        [TestMethod]
        public void LinqJoin03()
        {
            string[] categories = new string[]{
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood" };

            List<Product> products = LinqHellper.GetProducts();

            var prodByCategory =
                from cat in categories
                join prod in products on cat equals prod.Category into ps
                from p in ps
                select new { Category = cat, p.ProductName };

            foreach (var item in prodByCategory)
            {
                Debug.WriteLine(item.ProductName + ": " + item.Category);
            }
        }

        /// <summary>
        /// A left outer join produces a result set that includes all the left hand side elements at least once, 
        /// even if they don't match any right hand side elements.
        /// </summary>
        [TestMethod]
        public void LinqJoin04()
        {
            List<Customer> customers = LinqHellper.GetCustomers();
            List<Supplier> suppliers = LinqHellper.GetSupplier();

            var supplierCusts =
                from sup in suppliers
                join cust in customers on sup.Country equals cust.Country into cs
                from c in cs.DefaultIfEmpty()  // DefaultIfEmpty preserves left-hand elements that have no matches on the right side 
                    orderby sup.SupplierName
                select new
                {
                    Country = sup.Country,
                    CompanyName = c == null ? "(No customers)" : c.CompanyName,
                    SupplierName = sup.SupplierName
                };

            foreach (var item in supplierCusts)
            {
                Debug.WriteLine("{0} ({1}): {2}", item.SupplierName, item.Country, item.CompanyName);
            }
        }

        /// <summary>
        /// For each customer in the table of customers, this query returns all the suppliers from that same country,
        /// or else a string indicating that no suppliers from that country were found.
        /// </summary>
        [TestMethod]
        public void LinqJoin05()
        {

            List<Customer> customers = LinqHellper.GetCustomers();
            List<Supplier> suppliers = LinqHellper.GetSupplier();

            var custSuppliers =
                from cust in customers
                join sup in suppliers on cust.Country equals sup.Country into ss
                from s in ss.DefaultIfEmpty()
                orderby cust.CompanyName
                select new
                {
                    Country = cust.Country,
                    CompanyName = cust.CompanyName,
                    SupplierName = s == null ? "(No suppliers)" : s.SupplierName
                };

            foreach (var item in custSuppliers)
            {
                Debug.WriteLine("{0} ({1}): {2}", item.CompanyName, item.Country, item.SupplierName);
            }
        }

        /// <summary>
        /// For each supplier in the table of suppliers, this query returns all the customers from the same city and country, 
        /// or else a string indicating that no customers from that city/country were found. Note the use of anonymous types to encapsulate 
        /// the multiple key values.
        /// </summary>
        [TestMethod]
        public void LinqJoin06()
        {
            string[] names = { "C#", "VB", "F#" };
            var lengths = from name in names select name.Length;

            List<Customer> customers = LinqHellper.GetCustomers();
            List<Supplier> suppliers = LinqHellper.GetSupplier();

            var supplierCusts =
                from sup in suppliers
                join cust in customers on new { sup.City, sup.Country } equals new { cust.City, cust.Country } into cs
                from c in cs.DefaultIfEmpty() //Remove DefaultIfEmpty method call to make this an inner join
                    orderby sup.SupplierName
                select new
                {
                    Country = sup.Country,
                    City = sup.City,
                    SupplierName = sup.SupplierName,
                    CompanyName = c == null ? "(No customers)" : c.CompanyName
                };

            foreach (var item in supplierCusts)
            {
                Debug.WriteLine("{0} ({1}, {2}): {3}", item.SupplierName, item.City, item.Country, item.CompanyName);
            }
        }
    }
}

