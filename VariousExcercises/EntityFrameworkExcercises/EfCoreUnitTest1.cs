using EntityFrameworkExcercises.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace EntityFrameworkExcercises
{
    [TestClass]
    public class EfCoreUnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var version = Environment.Version;

            using (var context = new MyDbContext())
            {
                //var st = from student in context.Students
                //         where student.Id == 1
                //         select new Student()
                //         {
                //             Id = student.Id,
                //             LastName = "Hoshen"
                //         };

                //st.First(i => i.Id == 1).LastName = "Hoshen";

                var stu = context.Students.ToList().Find(i=> i.Id == 2);
                //stu.LastName = "hoshen hoshen";
                //context.UpdateRange(st);

                context.SaveChanges();
            }
        }

        [TestMethod]
        public void Find_Linq()
        {

            using (var context = new MyDbContext())
            {
                var student = context.Students.Find(2);
                var stu = context.Students.ToList().Find(i => i.Id == 2);
            }
        }
    }
}
