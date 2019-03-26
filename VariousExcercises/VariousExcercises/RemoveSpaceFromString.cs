using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace VariousExcercises
{
    [TestClass]
    public class RemoveSpaceFromString
    {
        [TestMethod]
        public void RemoveSpace()
        {
            /*
             * Here space is not empty string but in web it is empty string
             */
            var str = " ";

            if(!String.IsNullOrEmpty(str.Trim()))
            {

            }
            
            Debug.WriteLine(str.Replace(" ",string.Empty,StringComparison.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void MyMethod()
        {
            List<Name> list = new List<Name>();

            list.Last().LastName = "Satish"; 


        }
    }

    public class Name
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
