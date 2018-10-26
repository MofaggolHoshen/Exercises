using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreWithLinq.Models
{
    public class Address
    {
        public int Id { get; set; }
        public List<Street> Streets { get; set; }
        [ForeignKey("People")]
        public int Peopleid { get; set; }
        public People People { get; set; }
    }
}