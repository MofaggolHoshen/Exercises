using ObjectOriented.Entities;
using ObjectOriented.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOrientedTest
{
    [TestClass]
    public class AddOperationsTest
    {
        [TestMethod]
        public async Task AddNewEntityToExistingExtityTest()
        {
            ObjectOrientedDbContext context = new();
            ObjectOrientedService service = new(context);

            var applicant = await service.Init(applicantId: 1);
            var application = applicant.Applications.Single();

            var ex = new Experience()
            {
                Sector = "C#",
                companyName = "ABC GmbH"
            };

            application.AddExperience(ex);

           var result = await service.Persist();
        }

        [TestMethod]
        public void SomeTest()
        {
            var ex = new Experience()
            {
                Sector = "C#",
                companyName = "ABC GmbH"
            };

            var ex2 = new Experience(new Application() { Id = 1 }, ex);
            ex2.Id = 20;

            Assert.IsTrue(ex2.Id == ex.Id);
        }
    }
}
