using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class NetworkUnit
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string LocalId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string City { get; set; }

        public bool IsActive { get; set; }

        public int TenantId { get; set; }

        public Tenant Tenant { get; set; }


    }
}
