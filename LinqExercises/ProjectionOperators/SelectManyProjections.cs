using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ProjectionOperators
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Address> Addresses { get; set; }
        //public List<string> Pets { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string Strasse { get; set; }
    }

    [TestClass]
    public class SelectManyProjections
    {

        Person[] people =
            {
                new Person
                {
                    Id = 1,
                    Name = "Mofaggol",
                    Addresses = new List<Address> {
                        new Address{
                            Id = 1,
                            Strasse = "Adam-Opel Str."
                        }
                    }
                },
                new Person
                {
                    Id = 2,
                    Name = "Mofaggol2",
                    Addresses = new List<Address> {
                        new Address{
                            Id = 2,
                            Strasse = "Adam-Opel Str1."
                        }
                    }
                },
                new Person
                {
                    Id = 3,
                    Name = "Mofaggol3",
                    Addresses = new List<Address> {
                        new Address{
                            Id = 3,
                            Strasse = "Adam-Opel Str3."
                        }
                    }
                }
            };


        [TestMethod]
        public void SelectManyEx1()
        {
            var bla = people.SelectMany(p => p.Addresses);
        }

        [TestMethod]
        public void SelectManyEx2()
        {
            var bla = people.SelectMany((p, index) => p.Addresses).ToList();
        }

        [TestMethod]
        public void SelectManyEx3()
        {
            var bla = people.SelectMany(p => p.Addresses, (p, address) => new { address });
        }

        [TestMethod]
        public void SelectManyEx4()
        {
            var bla = people.SelectMany((p,index)=> p.Addresses, (p, address)=> new { address} ).ToList();
        }

        

    }
}
