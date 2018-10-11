using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MutitenantMSAL.Models
{
    public class Tenant
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public ICollection<NetworkUnit> NetworkUnits { get; set; } = new List<NetworkUnit>();
    }
}
