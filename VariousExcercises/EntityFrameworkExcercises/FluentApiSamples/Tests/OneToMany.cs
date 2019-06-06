using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkExcercises.FluentApiSamples.Entities;

namespace EntityFrameworkExcercises.FluentApiSamples.Tests
{
    [TestClass]
    public class OneToManyTest
    {
        [TestMethod]
        public void GetOneToManyData()
        {
            using(var context = new OneToManyDbContext())
            {
                var tents = context.Tenants
                                   .Include(t=> t.Currencies)
                                   .Include(t=> t.BaseCurrency)
                                   .ToList();
            }
        }

        [TestMethod]
        public void UpdateRelatedData()
        {
            using (var context = new OneToManyDbContext())
            {
                var tenat = context.Tenants
                                   .Include(t => t.Currencies)
                                   .Include(t => t.BaseCurrency)
                                   .First();

                // https://docs.microsoft.com/en-us/ef/core/saving/related-data
                tenat.BaseCurrency = context.Currencies.Single(i => i.Id == 2);

                context.SaveChanges();
            }
        }
    }
}
