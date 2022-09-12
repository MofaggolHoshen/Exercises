using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOrientedTest.Entities
{
    internal class Experience
    {
        public int Id { get; set; }
        public string Sector { get; set; }
        public string companyName { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public Experience()
        {

        }

        public Experience(Application application, Experience experience)
        {
            this.ApplicationId = application.Id;
            this.Application = application;
            this.Sector = experience.Sector;
            this.companyName = experience.companyName;
        }
    }
}
