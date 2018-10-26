using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VariousExcercises
{
    [TestClass]
    public class AndOperator
    {
        [TestMethod]
        public void And01()
        {
            // Each method displays a message and returns a Boolean value. 
            // Method1 returns false and Method2 returns true. When & is used,
            // both methods are called. 
            Debug.WriteLine("Regular AND:");
            if (Method1() & Method2())
                Debug.WriteLine("Both methods returned true.");
            else
                Debug.WriteLine("At least one of the methods returned false.");

            // When && is used, after Method1 returns false, Method2 is 
            // not called.
            Debug.WriteLine("\nShort-circuit AND:");
            if (Method1() && Method2())
                Debug.WriteLine("Both methods returned true.");
            else
                Debug.WriteLine("At least one of the methods returned false.");
        }

        private bool Method1()
        {
            Debug.WriteLine("Method1 called.");
            return true;
        }

        private bool Method2()
        {
            Debug.WriteLine("Method2 called.");
            return false;
        } 
    }
}
