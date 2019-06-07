using EntityFrameworkExcercises.ObjectOrientedSample.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkExcercises.ObjectOrientedSample
{
    public class ObjectOrientedDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public ObjectOrientedDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=EfExcercise;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             *  modelBuilder.Entity<NetworkUnitRelatedNeworkUnit>()
                .HasOne(middletable => middletable.ThisNetworkUnit)
                .WithMany(rel1 => rel1.ChildNetworkUnitLinks)
                .HasForeignKey(middletable => middletable.ChildNetworkUnitId);
            modelBuilder.Entity<NetworkUnitRelatedNeworkUnit>()
                .HasOne(middletable => middletable.ChildNetworkUnit)
                .WithMany(rel2 => rel2.ParentNetworkUnitLinks)
                .HasForeignKey(middletable => middletable.ChildNetworkUnitId);
             */
            modelBuilder.Entity<StudentSubject>()
               .HasKey(middletable => new { middletable.StudentId, middletable.SubjectId });
            modelBuilder.Entity<StudentSubject>()
                        .HasOne(middle => middle.Subject)
                        .WithMany(r => r.StudentSubjects)
                        .HasForeignKey(k => k.SubjectId);
            modelBuilder.Entity<StudentSubject>()
                        .HasOne(middle => middle.Student)
                        .WithMany(r => r.StudentSubjects)
                        .HasForeignKey(k => k.StudentId);

            modelBuilder.Entity<Student>().HasData(new Student(1,"Mofaggol","Hoshen", "Information Technology","FH Frankfurnt"));
            modelBuilder.Entity<Student>().HasData(new Student(2, "Mofaggol-2","Hoshen-2", "Information Technology", "FH Frankfurnt" ));

            modelBuilder.Entity<Subject>().HasData(new Subject(1, "Computer Scientce", true));
            modelBuilder.Entity<Subject>().HasData(new Subject(2, "Networking", true));
            modelBuilder.Entity<Subject>().HasData(new Subject(3, "Math", false));

            modelBuilder.Entity<StudentSubject>().HasData(new StudentSubject(1, 1));
            modelBuilder.Entity<StudentSubject>().HasData(new StudentSubject(1, 2));
            base.OnModelCreating(modelBuilder);
        }
    }
}
