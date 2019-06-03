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
        //private HashSet<StudentSubject> _studentSubjects;
        public ICollection<StudentSubject> StudentSubjects { get; private set; }

        public Student()
        {
            StudentSubjects = new List<StudentSubject>();
        }

        public Student(string firstName, string lastName, string department, string university)
        {
            FirstName = firstName;
            LastName = lastName;
            Department = department;
            UniversityName = university;
        }

        /// <summary>
        /// For sead method 
        /// </summary>
        public Student(int id, string firstName, string lastName, string department, string university) 
                : this( firstName,  lastName,  department,  university)
        {
            Id = id;
        }


        public void SetFirstName(string firstName)
        {

            this.FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {

            this.LastName = lastName;
        }

        public void SetDepartment(string department)
        {
            this.Department = department;
        }
    }
}
