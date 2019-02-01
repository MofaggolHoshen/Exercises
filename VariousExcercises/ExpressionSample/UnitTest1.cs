using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace ExpressionSample
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        delegate int del(int i);
        [TestMethod]
        public  void LandaExpression01()
        {
            del myDelegate = x => x * x;
            int j = myDelegate(5); //j = 25  
        }

        [TestMethod]
        public void ExpessionTree()
        {
            Expression<del> myET = x => x * x;

        }
    }
}
