using System;
using System.Collections.Generic;
using System.Linq;

namespace EqualityTest
{
    class MyClass : SampleClass
    {
        public override void MyVMethod()
        {

        }
    }
    public class SampleClass
    {
        public virtual void MyVMethod()
        {

            throw new NotImplementedException();
        }
        public void MyMethod(object arg)
        {
            throw new NotImplementedException();
        }

        public void MyMethod()
        {

            throw new NotImplementedException();
        }

        public void MyMethod(object arg1, object arg2)
        {

            throw new NotImplementedException();
        }

        public void LinqToForEach()
        {
            var greetings = new List<string>() { "hi", "yo", "hello", "howdy" };

            var shortGreeting =
                from greet in greetings
                where greet.Length < 3
                select greet;
        }
    }
}
