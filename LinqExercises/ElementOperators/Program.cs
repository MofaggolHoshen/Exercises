using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;
using LinqCommon;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElementOperators
{
    /// <summary>
    /// Element Operators
    /// 1. First
    /// 2. FirstOrDefault
    /// 3. Last
    /// 4. LastOrDefault
    /// 5. Single
    /// 6. SingleOrDefault
    /// 7. ElementAt
    /// 8. ElementAtOrDefault
    /// 9. DefaultIfEmpty : The DefaultIfEmpty operator supplies a default element for an empty sequence.
    /// Details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic13
    /// </summary>
    [TestClass]
    public class LinqExamples
        {
            //This sample uses First to return the first matching element as a Product, instead of as a sequence containing a Product.
            [TestMethod]
            public void ElementOperators01()
            {
                List<Product> products =LinqHellper.GetProducts();

                Product product12 = (
                    from prod in products
                    where prod.ProductID == 12
                    select prod)
                    .First();
            
            }

            //This sample uses First to find the first element in the array that starts with 'o'.
            [TestMethod]
            public void ElementOperators02()
            {
                string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                string startsWithO = strings.First(s => s[0] == 'o');

                Debug.WriteLine("A string starting with 'o': {0}", startsWithO);
            }

            //This sample uses FirstOrDefault to try to return the first element of the sequence, unless there are no elements, in which case the default value for that type 
            //is returned. FirstOrDefault is useful for creating outer joins.
            [TestMethod]
            public void ElementOperators03()
            {
                int[] numbers = { };

                int firstNumOrDefault = numbers.FirstOrDefault();

                Debug.WriteLine(firstNumOrDefault);
            }

            //This sample uses FirstOrDefault to return the first product whose ProductID is 789 
             //as a single Product object, unless there is no match, in which case null is returned.
             [TestMethod]
            public void ElementOperators04()
            {
                List<Product> products =LinqHellper.GetProducts();

                Product product789 = products.FirstOrDefault(p => p.ProductID == 789);

                Debug.WriteLine("Product 789 exists: {0}", product789 != null);
            }

            //This sample uses ElementAt to retrieve the second number greater than 5 from an array.
            [TestMethod]
            public void ElementOperators05()
            {
                int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

                int fourthLowNum = (
                    from num in numbers
                    where num > 5
                    select num)
                    .ElementAt(1);  // second number is index 1 because sequences use 0-based indexing

                Debug.WriteLine("Second number > 5: {0}", fourthLowNum);
            }
        
        }
    }
