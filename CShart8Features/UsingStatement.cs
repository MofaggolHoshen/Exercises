using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CShart8Features
{
    [TestClass]
    public class UsingStatementSample
    {
        /// <summary>
        /// After finishing using block Dispose method will call
        /// </summary>
        [TestMethod]
        public void OldSample()
        {

            using (var us1 = new UsingStatement1())
            {
                using (var us2 = new UsingStatement2())
                {
                    us2.PrintMessage();

                }

                us1.PrintMessage();
            }
        }

        /// <summary>
        /// End of method Dispose method will call
        /// </summary>
        [TestMethod]
        public void Sample()
        {

            using var us1 = new UsingStatement1();

            using var us2 = new UsingStatement2();
            us2.PrintMessage();
            us1.PrintMessage();

            Debug.WriteLine("Last");

            Debug.WriteLine("Last"); 
            Debug.WriteLine("Last"); 
            Debug.WriteLine("Last"); 
            Debug.WriteLine("Last"); 
            Debug.WriteLine("Last");

        }

        /// <summary>
        /// Dispose never call
        /// </summary>
        [TestMethod]
        public void Sample1()
        {

            var us1 = new UsingStatement1();
            var us2 = new UsingStatement2();
            us2.PrintMessage();
            us1.PrintMessage();

        }
    }

    public class UsingStatement1 : IDisposable
    {
        public void PrintMessage()
        {

            Debug.WriteLine("I am from UsingStatement1.");
        }

        public void Dispose()
        {

        }
    }

    public class UsingStatement2 : IDisposable
    {
        public void PrintMessage()
        {

            Debug.WriteLine("I am from UsingStatement2.");
        }

        public void Dispose()
        {

        }
    }
}
