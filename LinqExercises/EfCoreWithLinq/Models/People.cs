using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreWithLinq.Models
{
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
