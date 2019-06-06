using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFrameworkExcercises.FluentApiSamples.Entities.OneToMany
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //[ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public Currency(int id, string name, int tenantId)
        {
            Id = id;
            Name = name;
            TenantId = tenantId;
        }
    }
}
