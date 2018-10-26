using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VariousExcercises
{
    [TestClass]
    public class ReadingExcelFile
    {
        [TestMethod]
        public void ReadExcel()
        {
            var lines = File.ReadAllLines(@"C:\Users\hoshen.m\source\repos\Exercises\VariousExcercises\VariousExcercises\Files\Book1.xlsx");
        }
    }
}
