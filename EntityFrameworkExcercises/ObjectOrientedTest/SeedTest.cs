using ObjectOrientedTest.Entities;
using ObjectOrientedTest.Services;

namespace ObjectOrientedTest
{
    [TestClass]
    public class SeedTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            ObjectOrientedDbContext context = new();

            context.Applicants.Add(new Applicant
            {
                Name = "Mofaggol Hoahen",
                Applications = new List<Application>()
               {
                    new Application
                   {
                     JobOfferName = "Software Developer",
                     Educations = new List<Education>()
                     {
                         new Education
                         {
                             DegreeName = "Bacholor in IT",
                             PassingYear = "2019"
                         }
                     }
                   }
               }
            });

            context.SaveChanges();
        }
    }
}