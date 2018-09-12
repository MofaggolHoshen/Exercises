using System;
using System.Linq;
using EfEntityRelationship.Models;
using Microsoft.EntityFrameworkCore;

namespace EfEntityRelationship.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseAssignment>()
                   .HasKey(c => new { c.CourseID, c.InstructorID });

            modelBuilder.Entity<CourseAssignment>()
                .HasOne(ca => ca.Course)
                .WithMany(c => c.CourseAssignments)
                .HasForeignKey(ca => ca.CourseID);

            modelBuilder.Entity<CourseAssignment>()
                .HasOne(ca => ca.Instructor)
                .WithMany(c => c.CourseAssignments)
                .HasForeignKey(ca => ca.InstructorID);

            //base.OnModelCreating(modelBuilder);

            //var students = new Student[]
            //{
            //    new Student { FirstMidName = "Carson",   LastName = "Alexander",
            //        EnrollmentDate = DateTime.Parse("2010-09-01") },
            //    new Student { FirstMidName = "Meredith", LastName = "Alonso",
            //        EnrollmentDate = DateTime.Parse("2012-09-01") },
            //    new Student { FirstMidName = "Arturo",   LastName = "Anand",
            //        EnrollmentDate = DateTime.Parse("2013-09-01") },
            //    new Student { FirstMidName = "Gytis",    LastName = "Barzdukas",
            //        EnrollmentDate = DateTime.Parse("2012-09-01") },
            //    new Student { FirstMidName = "Yan",      LastName = "Li",
            //        EnrollmentDate = DateTime.Parse("2012-09-01") },
            //    new Student { FirstMidName = "Peggy",    LastName = "Justice",
            //        EnrollmentDate = DateTime.Parse("2011-09-01") },
            //    new Student { FirstMidName = "Laura",    LastName = "Norman",
            //        EnrollmentDate = DateTime.Parse("2013-09-01") },
            //    new Student { FirstMidName = "Nino",     LastName = "Olivetto",
            //        EnrollmentDate = DateTime.Parse("2005-09-01") }
            //};

            //modelBuilder.Entity<Student>().HasData(students);

            //var instructors = new Instructor[]
            //{
            //    new Instructor { FirstMidName = "Kim",     LastName = "Abercrombie",
            //        HireDate = DateTime.Parse("1995-03-11") },
            //    new Instructor { FirstMidName = "Fadi",    LastName = "Fakhouri",
            //        HireDate = DateTime.Parse("2002-07-06") },
            //    new Instructor { FirstMidName = "Roger",   LastName = "Harui",
            //        HireDate = DateTime.Parse("1998-07-01") },
            //    new Instructor { FirstMidName = "Candace", LastName = "Kapoor",
            //        HireDate = DateTime.Parse("2001-01-15") },
            //    new Instructor { FirstMidName = "Roger",   LastName = "Zheng",
            //        HireDate = DateTime.Parse("2004-02-12") }
            //};

            //modelBuilder.Entity<Instructor>().HasData(instructors);

            //var departments = new Department[]
            //{
            //    new Department { Name = "English",     Budget = 350000,
            //        StartDate = DateTime.Parse("2007-09-01"),
            //        InstructorID  = instructors.Single( i => i.LastName == "Abercrombie").ID },
            //    new Department { Name = "Mathematics", Budget = 100000,
            //        StartDate = DateTime.Parse("2007-09-01"),
            //        InstructorID  = instructors.Single( i => i.LastName == "Fakhouri").ID },
            //    new Department { Name = "Engineering", Budget = 350000,
            //        StartDate = DateTime.Parse("2007-09-01"),
            //        InstructorID  = instructors.Single( i => i.LastName == "Harui").ID },
            //    new Department { Name = "Economics",   Budget = 100000,
            //        StartDate = DateTime.Parse("2007-09-01"),
            //        InstructorID  = instructors.Single( i => i.LastName == "Kapoor").ID }
            //};

            //modelBuilder.Entity<Department>().HasData(departments);

            //var courses = new Course[]
            //{
            //    new Course {CourseID = 1050, Title = "Chemistry",      Credits = 3,
            //        DepartmentID = departments.Single( s => s.Name == "Engineering").DepartmentID
            //    },
            //    new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3,
            //        DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID
            //    },
            //    new Course {CourseID = 4041, Title = "Macroeconomics", Credits = 3,
            //        DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID
            //    },
            //    new Course {CourseID = 1045, Title = "Calculus",       Credits = 4,
            //        DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
            //    },
            //    new Course {CourseID = 3141, Title = "Trigonometry",   Credits = 4,
            //        DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
            //    },
            //    new Course {CourseID = 2021, Title = "Composition",    Credits = 3,
            //        DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
            //    },
            //    new Course {CourseID = 2042, Title = "Literature",     Credits = 4,
            //        DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
            //    },
            //};

            //modelBuilder.Entity<Course>().HasData(courses);

            //var officeAssignments = new OfficeAssignment[]
            //{
            //    new OfficeAssignment {
            //        InstructorID = instructors.Single( i => i.LastName == "Fakhouri").ID,
            //        Location = "Smith 17" },
            //    new OfficeAssignment {
            //        InstructorID = instructors.Single( i => i.LastName == "Harui").ID,
            //        Location = "Gowan 27" },
            //    new OfficeAssignment {
            //        InstructorID = instructors.Single( i => i.LastName == "Kapoor").ID,
            //        Location = "Thompson 304" },
            //};

            //modelBuilder.Entity<OfficeAssignment>().HasData(officeAssignments);

            //var courseInstructors = new CourseAssignment[]
            //{
            //    new CourseAssignment {
            //        CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
            //        InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID
            //        },
            //    new CourseAssignment {
            //        CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
            //        InstructorID = instructors.Single(i => i.LastName == "Harui").ID
            //        },
            //    new CourseAssignment {
            //        CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
            //        InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
            //        },
            //    new CourseAssignment {
            //        CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
            //        InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
            //        },
            //    new CourseAssignment {
            //        CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
            //        InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID
            //        },
            //    new CourseAssignment {
            //        CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
            //        InstructorID = instructors.Single(i => i.LastName == "Harui").ID
            //        },
            //    new CourseAssignment {
            //        CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
            //        InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
            //        },
            //    new CourseAssignment {
            //        CourseID = courses.Single(c => c.Title == "Literature" ).CourseID,
            //        InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
            //        },
            //};

            //modelBuilder.Entity<CourseAssignment>().HasData(courseInstructors);

            //var enrollments = new Enrollment[]
            //{
            //    new Enrollment {
            //        StudentID = students.Single(s => s.LastName == "Alexander").ID,
            //        CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
            //        Grade = Grade.A
            //    },
            //        new Enrollment {
            //        StudentID = students.Single(s => s.LastName == "Alexander").ID,
            //        CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
            //        Grade = Grade.C
            //        },
            //        new Enrollment {
            //        StudentID = students.Single(s => s.LastName == "Alexander").ID,
            //        CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
            //        Grade = Grade.B
            //        },
            //        new Enrollment {
            //            StudentID = students.Single(s => s.LastName == "Alonso").ID,
            //        CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
            //        Grade = Grade.B
            //        },
            //        new Enrollment {
            //            StudentID = students.Single(s => s.LastName == "Alonso").ID,
            //        CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
            //        Grade = Grade.B
            //        },
            //        new Enrollment {
            //        StudentID = students.Single(s => s.LastName == "Alonso").ID,
            //        CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
            //        Grade = Grade.B
            //        },
            //        new Enrollment {
            //        StudentID = students.Single(s => s.LastName == "Anand").ID,
            //        CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
            //        },
            //        new Enrollment {
            //        StudentID = students.Single(s => s.LastName == "Anand").ID,
            //        CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
            //        Grade = Grade.B
            //        },
            //    new Enrollment {
            //        StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
            //        CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
            //        Grade = Grade.B
            //        },
            //        new Enrollment {
            //        StudentID = students.Single(s => s.LastName == "Li").ID,
            //        CourseID = courses.Single(c => c.Title == "Composition").CourseID,
            //        Grade = Grade.B
            //        },
            //        new Enrollment {
            //        StudentID = students.Single(s => s.LastName == "Justice").ID,
            //        CourseID = courses.Single(c => c.Title == "Literature").CourseID,
            //        Grade = Grade.B
            //        }
            //};

            //modelBuilder.Entity<Enrollment>().HasData(enrollments);


            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");
            modelBuilder.Entity<Person>().ToTable("Person");
        }
    }
}