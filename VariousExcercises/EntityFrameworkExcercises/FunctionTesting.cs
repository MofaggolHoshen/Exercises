using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExcercises
{
    [TestClass]
    public class FunctionTesting
    {
        [TestMethod]
        public void CallMethodOnFly()
        {
            using (var context = new MyDbContext())
            {
                foreach (var student in context.Students
                                      .Include(c => c.StudentSubjects)
                                        .ThenInclude(k => k.Subject))
                {
                    var firstName = student.FirstName;
                }             
            }
        }

    }
}
