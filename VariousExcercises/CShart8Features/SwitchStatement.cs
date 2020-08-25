using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CShart8Features
{
    [TestClass]
    public class SwitchStatement
    {
        [TestMethod]
        public void SwitchWithFunc()
        {
            int a = 10;
            int b = 20;
            string operation = "+";

            var result = operation switch
            {
                "+" => ((Func<int>)(() =>
                {
                    Log("addition");
                    return a + b;
                }))(),
                "-" => ((Func<int>)(() =>
                {
                    Log("subtraction");
                    return a - b;
                }))(),
                "/" => ((Func<int>)(() =>
                {
                    Log("division");
                    return a / b;
                }))(),
                _ => throw new NotSupportedException()
            };

        }

        public static T TypeExample<T>(IEnumerable<T> sequence) =>
                                                                    sequence switch
                                                                    {
                                                                        Array array => (T)array.GetValue(2),
                                                                        IList<T> list => list[2],
                                                                        IEnumerable<T> seq => seq.Skip(2).First(),
                                                                    };

        private void Log(string mgs) => Debug.WriteLine("Log: " + mgs);

        public void Method()
        {
        }
    }
}
