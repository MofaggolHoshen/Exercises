using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace EqualityTest
{
    [TestClass]
    public class EqualityStaticMethods
    {
        [TestMethod]
        public void StaticsMethod1Test()
        {
            var food = new Food("Banana", 4);
            var food1 = new Food("Banana", 5);
            var food2 = new Food("Apple", 39);

            var isObjectEqual = object.Equals(food, food1);
            var isEqual = food.Equals(food1);

            var b = "Hello";
            var n = "Hello";
            var i = object.Equals(b,n);
        }

        [TestMethod]
        public void FoodEquals()
        {
            Food banana = new Food("banana", 2);
            Food banana2 = new Food("banana", 2);
            Food chocolate = new Food("chocolate", 5);

            // behaviour for non-null
            Debug.WriteLine(banana.Equals(chocolate));
            Debug.WriteLine(banana.Equals(banana2));

            // behaviour for nulls
            Debug.WriteLine(banana.Equals(null));
            Debug.WriteLine(object.Equals(banana, null));
            Debug.WriteLine(object.Equals(null, banana));
            Debug.WriteLine(object.Equals(null, null));

            Debug.WriteLine(Equals(banana2, banana));
            
        }

        [TestMethod]
        public void StringEquals()
        {
            string str = "Click ne now!";
            string strCopy = str;

            Debug.WriteLine(ReferenceEquals(str, strCopy));
            Debug.WriteLine(str.Equals(strCopy));
            Debug.WriteLine(str == strCopy);
        }
    }
}
