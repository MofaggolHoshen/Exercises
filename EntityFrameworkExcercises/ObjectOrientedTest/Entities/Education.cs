using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOrientedTest.Entities
{
    internal class Education
    {
        public int Id { get; set; }
        public string DegreeName { get; set; }
        public string PassingYear { get; set; }
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
