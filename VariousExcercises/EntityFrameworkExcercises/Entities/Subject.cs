using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameworkExcercises.Entities
{
    public class Subject
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public bool IsMain { get; private set; }

        private HashSet<StudentSubject> _studentSubjects;

        public IEnumerable<StudentSubject> StudentSubjects => _studentSubjects?.ToList();

        public Subject(int id, string name, bool isMain)
        {
            Id = id;
            Name = name;
            IsMain = isMain;
        }
    }
}
