using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOriented.Entities
{
    public class Applicant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}
