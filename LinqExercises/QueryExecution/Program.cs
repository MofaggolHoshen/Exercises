using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QueryExecution
{
    [TestClass]
    public class LinqExamples
    {
        /// <summary>
        /// The following sample shows how query execution is deferred until the query is enumerated at a foreach statement.
        /// </summary>
        [TestMethod]
        public void DeferredExecution()
        {

            // Queries are not executed until you enumerate over them.
            int[] numbers = new int[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int i = 0;
            var simpleQuery =
                from num in numbers
                select ++i;

            // The local variable 'i' is not incremented
            // until the query is executed in the foreach loop.
            Debug.WriteLine("The current value of i is {0}", i); //i is still zero

            foreach (var item in simpleQuery)
            {
                Debug.WriteLine("v = {0}, i = {1}", item, i); // now i is incremented          
            }
        }

        /// <summary>
        /// The following sample shows how queries can be executed immediately, and their results stored in memory, with methods such as ToList.
        /// </summary>
        [TestMethod]
        public void ImmediateExecution()
        {

            // Methods like ToList(), Max(), and Count() cause the query to be
            // executed immediately.            
            int[] numbers = new int[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int i = 0;
            var immediateQuery = (
                from num in numbers
                select ++i)
                .ToList();

            Debug.WriteLine("The current value of i is {0}", i); //i has been incremented

            foreach (var item in immediateQuery)
            {
                Debug.WriteLine("v = {0}, i = {1}", item, i);
            }
        }

        /// <summary>
        /// The following sample shows how, because of deferred execution, queries can be used again after data changes and will then operate on the new data.
        /// </summary>
        [TestMethod]
        public void DefferedExecutionChangedAfterDataChanged()
        {

            // Deferred execution lets us define a query once
            // and then reuse it later in various ways.
            int[] numbers = new int[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var lowNumbers =
                            from num in numbers
                            where num <= 3
                            select num;

            Debug.WriteLine("First run numbers <= 3:");
            foreach (int n in lowNumbers)
            {
                Debug.WriteLine(n);
            }

            // Query the original query.
            var lowEvenNumbers =
                                from num in lowNumbers
                                where num % 2 == 0
                                select num;

            Debug.WriteLine("Run lowEvenNumbers query:");
            foreach (int n in lowEvenNumbers)
            {
                Debug.WriteLine(n);
            }

            // Modify the source data.
            for (int i = 0; i < 10; i++)
            {
                numbers[i] = -numbers[i];
            }

            // During this second run, the same query object,
            // lowNumbers, will be iterating over the new state
            // of numbers[], producing different results:
            Debug.WriteLine("Second run numbers <= 3:");
            foreach (int n in lowNumbers)
            {
                Debug.WriteLine(n);
            }
        }
    }
}

