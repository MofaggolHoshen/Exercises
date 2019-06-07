using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameworkExcercises.ObjectOrientedSample.Entities
{
    public class Subject
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public bool IsMain { get; private set; }

        private HashSet<StudentSubject> _studentSubjects;

        public List<StudentSubject> StudentSubjects { get; private set; }

        private Subject()
        {
            StudentSubjects = new List<StudentSubject>();
        }
        public Subject(int id, string name, bool isMain)
        {
            Id = id;
            Name = name;
            IsMain = isMain;
        }
    }
}
