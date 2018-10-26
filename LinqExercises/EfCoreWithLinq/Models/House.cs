using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreWithLinq.Models
{
    public class House
    {
        public int Id { get; set; }
        public int Number { get; set; }
        [ForeignKey("Street")]
        public int StreetId { get; set; }
        public Street Street { get; set; }
    }
}