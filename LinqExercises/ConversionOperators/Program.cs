using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace ConversionOperators
{
    [TestClass]
    public class LinqExamples
    {
        [TestMethod]
        public void ToArray01()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

            var sortedDoubles =
                from d in doubles
                orderby d descending
                select d;

            var doublesArray = sortedDoubles.ToArray();
            
            for (int d = 0; d < doublesArray.Length; d += 2)
            {
                Debug.WriteLine(doublesArray[d]);
            }
        }

        [TestMethod]
        public void ToList01()
        {
            string[] words = { "cherry", "apple", "blueberry" };

            var sortedWords =
                from w in words
                orderby w
                select w;

            var wordList = sortedWords.ToList();
            
            foreach (var w in wordList)
            {
                Debug.WriteLine(w);
            }
        }

        [TestMethod]
        public void ToDictionary01()
        {
            var scoreRecords = new[] { new {Name = "Alice", Score = 50},
                                        new {Name = "Bob"  , Score = 40},
                                        new {Name = "Cathy", Score = 45}
                                    };

            var scoreRecordss = (new[] { new {Name = "Alice", Score = 50, Add = "h"},
                                        new {Name = "Bob"  , Score = 40, Add = "h"},
                                        new {Name = "Cathy", Score = 45, Add = "h"}
                                    }).ToDictionary(s=> s.Name);


            var scoreRecordsDict = scoreRecords.ToDictionary(sr => sr.Name);
            
            Debug.WriteLine("Bob's score: {0}", scoreRecordsDict["Bob"]);
        }
        
        [TestMethod]
        public void Linq57()
        {
            object[] numbers = { null, 1.0, "two", 3, "four", 5, "six", 7.0 };

            //Filters the elements of an System.Collections.IEnumerable based on a specified type.
            var doubles = numbers.OfType<double>();
            
            foreach (var d in doubles)
            {
                Debug.WriteLine(d);
            }
        }
    }

}
