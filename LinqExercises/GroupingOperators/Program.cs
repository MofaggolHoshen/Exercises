using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinqCommon;
using System.Diagnostics;

namespace GroupingOperators
{
    [TestClass]
    public class LinqExamples
    {
        private DataSet testDS;

        public LinqExamples()
        {
            testDS = DataTableHelper.CreateTestDataset();
        }

        /// <summary>
        /// This sample uses group by to partition a list of numbers by their remainder when divided by 5.
        /// </summary>
        [TestMethod]
        public void Group01()
        {

            var numbers = testDS.Tables["Numbers"].AsEnumerable();

            var numberGroups =
                from n in numbers
                group n by n.Field<int>("number") % 5 into g
                select new { Remainder = g.Key, Numbers = g };

            foreach (var g in numberGroups)
            {
                Debug.WriteLine("Numbers with a remainder of {0} when divided by 5:", g.Remainder);
                foreach (var n in g.Numbers)
                {
                    Debug.WriteLine(n.Field<int>("number"));
                }
            }
        }

        /// <summary>
        /// This sample uses group by to partition a list of words by their first letter.
        /// </summary>
        [TestMethod]
        public void Group02()
        {

            var words4 = testDS.Tables["Words4"].AsEnumerable();

            var wordGroups =
                from w in words4
                group w by w.Field<string>("word")[0] into g
                select new { FirstLetter = g.Key, Words = g };

            foreach (var g in wordGroups)
            {
                Debug.WriteLine("Words that start with the letter '{0}':", g.FirstLetter);
                foreach (var w in g.Words)
                {
                    Debug.WriteLine(w.Field<string>("word"));
                }
            }
        }

        [TestMethod]
        public void Group03()
        {

            var products = testDS.Tables["Products"].AsEnumerable();

            var productGroups =
                from p in products
                group p by p.Field<string>("Category") into g
                select new { Category = g.Key, Products = g };

            foreach (var g in productGroups)
            {
                Debug.WriteLine("Category: {0}", g.Category);
                foreach (var w in g.Products)
                {
                    Debug.WriteLine("\t" + w.Field<string>("ProductName"));
                }
            }
        }

        [TestMethod]
        public void Group04()
        {

            var customers = testDS.Tables["Customers"].AsEnumerable();

            var customerOrderGroups =
                from c in customers
                select
                    new
                    {
                        CompanyName = c.Field<string>("CompanyName"),
                        YearGroups =
                            from o in c.GetChildRows("CustomersOrders")
                            group o by o.Field<DateTime>("OrderDate").Year into yg
                            select
                                new
                                {
                                    Year = yg.Key,
                                    MonthGroups =
                                        from o in yg
                                        group o by o.Field<DateTime>("OrderDate").Month into mg
                                        select new { Month = mg.Key, Orders = mg }
                                }
                    };

            foreach (var cog in customerOrderGroups)
            {
                Debug.WriteLine("CompanyName= {0}", cog.CompanyName);
                foreach (var yg in cog.YearGroups)
                {
                    Debug.WriteLine("\t Year= {0}", yg.Year);
                    foreach (var mg in yg.MonthGroups)
                    {
                        Debug.WriteLine("\t\t Month= {0}", mg.Month);
                        foreach (var order in mg.Orders)
                        {
                            Debug.WriteLine("\t\t\t OrderID= {0} ", order.Field<int>("OrderID"));
                            Debug.WriteLine("\t\t\t OrderDate= {0} ", order.Field<DateTime>("OrderDate"));
                        }
                    }
                }
            }
        }

        private class AnagramEqualityComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return getCanonicalString(x) == getCanonicalString(y);
            }

            public int GetHashCode(string obj)
            {
                return getCanonicalString(obj).GetHashCode();
            }

            private string getCanonicalString(string word)
            {
                char[] wordChars = word.ToCharArray();
                Array.Sort<char>(wordChars);
                return new string(wordChars);
            }
        }

        [TestMethod]
        public void Group05()
        {

            var anagrams = testDS.Tables["Anagrams"].AsEnumerable();

            var orderGroups = anagrams.GroupBy(w => w.Field<string>("anagram").Trim(), new AnagramEqualityComparer());

            foreach (var g in orderGroups)
            {
                Debug.WriteLine("Key: {0}", g.Key);
                foreach (var w in g)
                {
                    Debug.WriteLine("\t" + w.Field<string>("anagram"));
                }
            }
        }

        [TestMethod]
        public void Group06()
        {

            var anagrams = testDS.Tables["Anagrams"].AsEnumerable();

            var orderGroups = anagrams.GroupBy(
                w => w.Field<string>("anagram").Trim(),
                a => a.Field<string>("anagram").ToUpper(),
                new AnagramEqualityComparer()
                );

            foreach (var g in orderGroups)
            {
                Debug.WriteLine("Key: {0}", g.Key);
                foreach (var w in g)
                {
                    Debug.WriteLine("\t" + w);
                }
            }
        }
    }
}
