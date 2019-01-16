using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace VariousExcercises
{
    [TestClass]
    public class Delegates
    {
        [TestMethod]
        public void Delegate01()
        {
            var ex = new Example()
            {
                Id = 1,
                Name = "Mofaggol Hoshen"
            };

            Extention.UpdateAnObjectUsingActionDelegate<Example>(ex, u =>
            {
                u.Address = "Adam-Opel Str.";
            });
        }

        [TestMethod]
        public void Delegate02()
        {

            var ex = new Example()
            {
                Id = 1,
                Name = "Mofaggol Hoshen"
            };

            UpdateAnObjectUsingActionDelegate(ex, excercise =>
            {
                excercise.Name = "Hosher, Mofaggol";

            });
        }

        public static void UpdateAnObjectUsingActionDelegate(Example ex, Action<Example> predicate)
        {
            predicate.Invoke(ex);

            var result = ex;
        }


        private static void NullValuePrintExample(Example from)
        {
            if ((from?.Address == "hello") || (from?.IsActive == true))
            {

                Debug.WriteLine(from?.Address);
            }
        }



        public static Example InjectFrom(Example from, Example To)
        {
            var example = new Example();

            var properties = from.GetType().GetProperties();

            foreach (var item in properties)
            {
                var name = item.Name;

                var value1 = To.GetType().GetProperty(name).GetValue(To);
                var value2 = from.GetType().GetProperty(name).GetValue(from);

                if (value1 != null)
                    example.GetType().GetProperty(name).SetValue(example, value1);
                else
                    example.GetType().GetProperty(name).SetValue(example, value2);
            }

            return example;
        }
    }

    public static class Extention
    {
        public static void UpdateAnObjectUsingActionDelegate<T>(this T ex, Action<T> predicate)
        {
            predicate.Invoke(ex);

            var result = ex;
        }
    }


    public class Example
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
    }

    public enum Ex
    {
        FirstName = 1,
        LastName = 2
    }
}
