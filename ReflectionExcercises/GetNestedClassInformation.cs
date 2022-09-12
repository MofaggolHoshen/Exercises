using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionExcercises
{
    [TestClass]
    public class GetNestedClassInformation
    {
        [TestMethod]
        public void getNestedClassProperty()
        {
            var person = new Person()
            {
                FirstName = "Mofaggol",
                LastName = "Hoshen",
                Address = new Address()
                {
                    HouseNo = 24,
                    Road = "Adam-Opel Strasse."
                }
            };

            var address = person.GetType().GetProperty("Address").GetValue(person);
            var road = address.GetType().GetProperty("Road").GetValue(address);

        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public int HouseNo { get; set; }
        public string Road { get; set; }
    }
}
