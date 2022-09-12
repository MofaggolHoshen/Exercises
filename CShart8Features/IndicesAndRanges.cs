using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace CShart8Features
{
    [TestClass]
    public class IndicesAndRanges
    {
        string[] words = new string[]
        {
                            // index from start    index from end
                "The",      // 0                   ^9
                "quick",    // 1                   ^8
                "brown",    // 2                   ^7
                "fox",      // 3                   ^6
                "jumped",   // 4                   ^5
                "over",     // 5                   ^4
                "the",      // 6                   ^3
                "lazy",     // 7                   ^2
                "dog"       // 8                   ^1
        };              // 9 (or words.Length) ^0           // 9 (or words.Length) ^0

        [TestMethod]
        public void TestMethod1()
        {
            Debug.WriteLine($"The last word is {words[^1]}");
            // writes "dog"
            var quickBrownFox = words[0..2];
            var lazyDog = words[2..^0];
            var allWords = words[..]; // contains "The" through "dog".
            var firstPhrase = words[..4]; // contains "The" through "fox"
            var lastPhrase = words[6..]; // contains "the", "lazy" and "dog"
            Range phrase = 1..4;
            var text = words[phrase];


        }

        [TestMethod]
        public void MyMethod()
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            string str1 = "2,900.20";
            string str2 = "29,20";

            var s = decimal.Parse(str1);
            var s1 = decimal.Parse(str2);
        }
    }
}
