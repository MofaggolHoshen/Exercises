using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Xml.Linq;
using LinqCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CustomSequenceOperators
{
    public static class CustomSequenceOperators
    {
        public static IEnumerable<T> Combine<T>(this IEnumerable<DataRow> first, IEnumerable<DataRow> second, System.Func<DataRow, DataRow, T> func)
        {
            using (IEnumerator<DataRow> e1 = first.GetEnumerator(), e2 = second.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext())
                {
                    yield return func(e1.Current, e2.Current);
                }
            }
        }
    }

    [TestClass]
    public class LinqExamples
    {
        [TestMethod]
        public void DataSetLinq98()
        {

            var numbersA = DataTableHelper.CreateTestDataset().Tables["NumbersA"].AsEnumerable();
            var numbersB = DataTableHelper.CreateTestDataset().Tables["NumbersB"].AsEnumerable();

            var dotProduct = numbersA.Combine(numbersB, (a, b) => a.Field<int>("number") * b.Field<int>("number"));

            Debug.WriteLine("Dot product: {0}", dotProduct);
        }
    }
}
