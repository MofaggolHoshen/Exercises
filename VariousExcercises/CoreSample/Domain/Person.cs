using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiltersSample.Domain
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
}
