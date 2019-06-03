using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JoinOperators
{
    public class A
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<A> Bs { get; set; }
    }

    public class B
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    [TestClass]
    public class LeftOuterJoin
    {
        [TestMethod]
        public void MyMethod()
        {
            var list = new List<A>()
            {
                new A
            {
                Id = 1,
                Name = "Mofaggol",
                Bs = new List<A>()
            },
                new A
            {
                Id = 2,
                Name = "Mofaggol",
                Bs = new List<A>() { new A { Id = 1, Name = "bla" } }
            },
                 new A
            {
                Id = 1,
                Name = "Mofaggol",
                Bs = new List<A>() { new A { Id = 1, Name = "bla" } }
            }
        };

            var query = (from l in list
                         from ll in l.Bs
                         where l.Id == 1
                         select l).ToList();
        }

        [TestMethod]
        public void Linq105()
        {
            var list = new List<int>() { 1, 2, 3, 4 };
            var list2 = new List<int>();

            var q = (from c in list
                    join p in list2 on c equals p into ps
                    where ps != null || c != 0 
                    select c).ToList();



            
        }

        /*
         * 
         * public void Linq105()  
                { 
                    string[] categories = new string[]{   
                        "Beverages",  
                        "Condiments",   
                        "Vegetables",   
                        "Dairy Products",  
                        "Seafood" }; 
  
                    List<Product> products = GetProductList(); 
  
  
  
                    var q = 
                        from c in categories 
                        join p in products on c equals p.Category into ps 
                        from p in ps.DefaultIfEmpty() 
                        select new { Category = c, ProductName = p == null ? "(No products)" : p.ProductName }; 
  
                    foreach (var v in q) 
                    { 
                        Console.WriteLine(v.ProductName + ": " + v.Category); 
                    } 
                }
         * 
         * 
         */
    }
}
