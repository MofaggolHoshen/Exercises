using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EntityFrameworkExcercises.Entities
{
    public class Student
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Department { get; private set; }
        public string UniversityName { get; private set; }
        private HashSet<StudentSubject> _studentSubjects;
        public List<StudentSubject> StudentSubjects => _studentSubjects?.ToList();

        public Student()
        {

        }
        public Student(int id, string firstName, string lastName, string department, string university)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Department = department;
            UniversityName = university;
        }
    }
}
