using Microsoft.VisualStudio.TestTools.UnitTesting;
using IsPrarentDeleteChiledDelete.Data;
using Microsoft.EntityFrameworkCore;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var db = getDb();

           var result =  db.Children
                .Include(i => i.Parent)
                .ToListAsync().Result;
        }

        private DatabaseContext getDb()
        {
            var option = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer(connectionString: "Data Source=localhost;Initial Catalog=EfExcercise;Integrated Security=True")
                .Options;
            return new DatabaseContext(option);
        
        }
    }
}
