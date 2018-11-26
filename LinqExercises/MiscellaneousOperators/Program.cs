using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LinqCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EqualityOperator
{
    /// <summary>
    /// Equality Operator
    /// 1. SequenceEqual: The SequenceEqual operator checks whether two sequences are equal.
    /// Details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic12
    /// </summary>
    [TestClass]
    public class MiscellaneousOperators
    {
       
        /// <summary>
        /// This sample uses SequenceEquals to see if two sequences match on all elements in the same order.
        /// </summary>
        public void LinqSequenceEqual01()
        {
            var wordsA = new string[] { "cherry", "apple", "blueberry" };
            var wordsB = new string[] { "cherry", "apple", "blueberry" };

            bool match = wordsA.SequenceEqual(wordsB);

            Debug.WriteLine("The sequences match: {0}", match);
        }


        public void LinqSequenceEqual02()
        {
            var wordsA = new string[] { "cherry", "apple", "blueberry" };
            var wordsB = new string[] { "apple", "blueberry", "cherry" };

            bool match = wordsA.SequenceEqual(wordsB);

            Debug.WriteLine("The sequences match: {0}", match);
        }
    }
}
