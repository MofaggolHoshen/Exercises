using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOriented.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public string JobOfferName { get; set; }
        public ICollection<Education> Educations { get; set; }
        public ICollection<Experience> Experiences { get; set; }

        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }

        public void AddExperience(Experience experience)
        {
            experience.Application = this;

            Experiences.Add(experience);
        }
    }
}
