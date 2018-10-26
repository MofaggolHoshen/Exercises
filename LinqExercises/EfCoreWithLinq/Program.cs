using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System;
using EfCoreWithLinq.Models;
using System.Linq;

namespace EfCoreWithLinq
{
    [TestClass]
    public class Program
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dbContext = new DatabaseContext();          
        }

        public People GetPeople()
        {

            var people = new People()
            {
                Id = 1,
                Name = "a",
                Addresses = new System.Collections.Generic.List<Address>()
                {
                    new Address()
                    {
                        Id = 1,
                        Streets = new System.Collections.Generic.List<Street>()
                        {
                            new Street()
                            {
                                Id = 1,
                                Houses = new System.Collections.Generic.List<House>()
                                {
                                    new House()
                                    {
                                        Id = 2,
                                        Number = 2
                                    },
                                    new House()
                                    {
                                        Id = 3,
                                        Number = 3
                                    },
                                    new House()
                                    {
                                        Id = 4,
                                        Number = 4
                                    },
                                    new House()
                                    {
                                        Id = 5,
                                        Number = 5
                                    }
                                }
                            }
                        }
                    }
                }
            };

            return people;
        }
    }
    
}
