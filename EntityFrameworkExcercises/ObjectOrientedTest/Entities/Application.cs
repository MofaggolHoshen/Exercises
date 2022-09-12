using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOrientedTest.Entities
{
    internal class Application
    {
        public int Id { get; set; }
        public string JobOfferName { get; set; }
        public ICollection<Education> Educations { get; set; }
        public ICollection<Experience> Experiences { get; set; }

        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }

        internal void AddExperience(Experience experience)
        {
            this.Experiences.Add(new Experience(this, experience));
        }
    }
}
