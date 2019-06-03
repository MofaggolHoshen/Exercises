using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace VariousExcercises
{
    [TestClass]
    public class LinqOperation
    {
        [TestMethod]
        public void MyMethod()
        {
            var list = new List<int>() { 1, 2, 3, 4 };
            var list2 = new List<int>();

            var list3 = (from l in list
                         where list2.Count() == 0
                         from ll in list2
                         where  l == ll
                        select ll).ToList();

            

        }
    }
}
