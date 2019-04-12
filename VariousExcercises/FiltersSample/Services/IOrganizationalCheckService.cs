using FiltersSample.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiltersSample.Services
{
    public class OrganizationalCheckService : IOrganizationalCheckService
    {
        public bool IsNameValid(Person person)
        {
            return person.FirstName.ToLower() == "mofaggol";
        }
    }
    public interface IOrganizationalCheckService
    {
        bool IsNameValid(Person person);
    }
}
