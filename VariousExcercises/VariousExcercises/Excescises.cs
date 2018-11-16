using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VariousExcercises
{
    [TestClass]
    public class Excescises
    {
        [TestMethod]
        public void RemoveSpaceFronString()
        {
            var str = "MofaggolHoshen";

            Debug.WriteLine(str.Replace(" ",string.Empty,StringComparison.OrdinalIgnoreCase));
        }
    }
}
