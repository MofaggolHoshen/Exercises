using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Reflection;

namespace TaskCancelationToken
{
    [TestClass]
    public class UnitTest1
    {
        Action action;

        void Mymethod()
        {
            Debug.WriteLine("From Delegate !!!!");
        }

        [TestMethod]
        public void TestMethod1()
        {
            DateTime? date;
            date = DateTime.Now;

            var core = Type.GetTypeCode(date.GetType());
        }

        public object GetType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
