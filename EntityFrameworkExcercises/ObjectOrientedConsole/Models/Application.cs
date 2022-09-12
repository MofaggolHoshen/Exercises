using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOrientedConsole.Models
{
    internal class Application
    {
        public int Id { get; set; }
        public string JobOfferName { get; set; }
        public ICollection<Education> Educations { get; set; }
    }
}
