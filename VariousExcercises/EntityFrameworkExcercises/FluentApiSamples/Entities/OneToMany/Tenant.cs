using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkExcercises.FluentApiSamples.Entities.OneToMany
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Currency BaseCurrency { get; set; }
        public List<Currency> Currencies { get; set; }

        public Tenant(int id, string name)
        {
            Id = id;
            Name = name;
            //BaseCurrencyId = baseCurrencyId;
        }
    }
}
