using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Tenant
    {
        [Key]
        public int Id { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public ICollection<NetworkUnit> NetworkUnits { get; set; } = new List<NetworkUnit>();
    }
}
