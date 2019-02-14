using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkExcercises.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string UniversityName { get; set; }
    }
}
