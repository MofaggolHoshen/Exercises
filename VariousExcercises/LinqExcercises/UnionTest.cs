using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LinqExcercises
{
    [TestClass]
    public class UnionTest
    {
        [TestMethod]
        public void ComplexList()
        {
            var list =new List<A>(){ new A()
            {
                Id = 2,
                Code = 3,
                Bs = new List<B>() { new B() {
                    Id = 2,
                    Code = 5,
                    Cs = new List<C>(){
                        new C()
                        {
                            Id = 8,
                            Code = 8
                        }
                    }
                }
                }
            } };

            var list2 = new List<A>(){
                //new A()
            //{
            //    Id = 2,
            //    Code = 3,
            //    Bs = new List<B>() { new B() {
            //        Id = 2,
            //        Code = 5,
            //        Cs = new List<C>(){
            //            new C()
            //            {
            //                Id = 8,
            //                Code = 8
            //            }
            //        }
            //    }
            //    }
            //}
            };

            var uValue = list.Union(list2).ToList();

        }
    }
}
