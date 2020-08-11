using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VariousExcercises
{
    [TestClass]
    public class Delegates
    {
        public class CountryInfo
        {
            public int Id { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public int Value { get; set; }
        }
        public enum Count
        {
            One,
            Two,
            Three
        }

        [TestMethod]
        public void TestMethos()
        {
            string path = string.Empty;

            var bla1 = path?.Count();
        }

        [TestMethod]
        public void MyMethod()
        {
            List<CountryInfo> table = new List<CountryInfo>()
            {
                new CountryInfo
                {
                    Id = 1,
                    Country = "England",
                    City = "London",
                    Value = 10
                },
                 new CountryInfo
                {
                    Id = 2,
                    Country = "Germany",
                    City = "Munich",
                    Value = 17
                },
                  new CountryInfo
                {
                    Id = 3,
                    Country = "China",
                    City = "Beijing",
                    Value = 8
                },
                   new CountryInfo
                {
                    Id = 4,
                    Country = "Japan",
                    City = "Tokyo",
                    Value = 20
                },
                    new CountryInfo
                {
                    Id = 5,
                    Country = "Russia",
                    City = "Moscow",
                    Value = 10
                },
                     new CountryInfo
                {
                    Id = 6,
                    Country = "England",
                    City = "Liverpool",
                    Value = 11
                },
                      new CountryInfo
                {
                    Id = 7,
                    Country = "Russia",
                    City = "Saint Petersburg",
                    Value = 12
                },
                       new CountryInfo
                {
                    Id = 8,
                    Country = "Japan",
                    City = "Nagoya",
                    Value = 13
                },
                        new CountryInfo
                {
                    Id = 9,
                    Country = "England",
                    City = "Manchester",
                    Value = 9
                },
                         new CountryInfo
                {
                    Id = 10,
                    Country = "Germany",
                    City = "Hamburg",
                    Value = 18
                }
            };

            //var info3 = table.GroupBy(i => i.Country, j => j.Id); //.ToList()


            //var info4 = table.GroupBy(i => i.Country, (key, value) => new { 
            //    Country = key, 
            //    Ids = value.Select(j => j.Id) //.ToList()
            //}); //.ToList()

            //var into1 = table.GroupBy(i => i.Country, j => j, (key, values) =>
            //{
            //    // You can project how you want 
            //    return new { 
            //        Country = key, 
            //        Ids = values.Select(i => i.Id) //.ToList()
            //    };
            //}); //.ToList()

            //var info2 = table.GroupBy(i => i.Country).Select(i => new { 
            //    Country = i.Key, 
            //    Ids = i.Select(j => j.Id) // .ToList()
            //}); //.ToList()


            var total1 = table.Where(i => i.Country == "Japan").Sum(i => i.Value);

        }

        private async Task<bool> isFileExist(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var restponse = await client.GetAsync(url);

                return restponse.StatusCode == System.Net.HttpStatusCode.OK;
            }
        }
        public Action Action { get; set; }
        [TestMethod]
        public void MyMethod1()
        {
            Action = GetMethod;

            Action.Invoke();

            Action.Invoke();

        }

        public void GetMethod()
        {

        }

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
