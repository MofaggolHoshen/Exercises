using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExcercises.ObjectOrientedSample.Entities
{
    public class StudentSubject
    {
        [ForeignKey("Student")]
        public int StudentId { get; private set; }
        public Student Student { get; private set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; private set; }
        public Subject Subject { get; private set; }

        private StudentSubject()
        {

        }

        public StudentSubject(int studentId, int subjectId)
        {
            StudentId = studentId;
            SubjectId = subjectId;
        }
    }
}
