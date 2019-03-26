using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReflectionExcercises
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetPropertyName()
        {
            PersonInformaion personInformaion = new PersonInformaion() {
                FirstName = "Mofaggol",
                LastName = "Hoshen"
            };

            var name = personInformaion.GetType().GetProperty("FirstName");
        }

        [TestMethod]
        public void GetPropertyNameAndVlaue()
        {
            PersonInformaion personInformaion = new PersonInformaion()
            {
                FirstName = "Mofaggol",
                LastName = "Hoshen"
            };

            var name = personInformaion.GetType().GetProperty("FirstName").GetValue(personInformaion);
        }
    }

    public class PersonInformaion
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
