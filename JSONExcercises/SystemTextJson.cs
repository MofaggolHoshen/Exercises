using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace JSONExcercises
{
    [TestClass]
    public class SystemTextJson
    {
        [TestMethod]
        public void MyMethod()
        {

            object obj = "123";

           var objStr = JsonSerializer.Serialize<object>(obj);
           var vv = JsonSerializer.Deserialize<object>(objStr);
        }
    }

    public class Branch
    {
        public int Id { get; set; }
        public object Vlaue { get; set; }
    }
}
