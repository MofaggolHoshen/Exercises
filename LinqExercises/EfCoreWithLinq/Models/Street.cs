using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreWithLinq.Models
{
    public class Street
    {
        public int Id { get; set; }
        public List<House> Houses { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}