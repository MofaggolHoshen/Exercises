using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EntityFrameworkExcercises.StackOverFlow.UpdateOneToOneTable.Entities;

namespace EntityFrameworkExcercises.StackOverFlow.UpdateOneToOneTable.Tests
{
    [TestClass]
    public class UpdateOneToOneTests
    {
        [TestMethod]
        public void Insert()
        {
            using (var context = new StackOverFlowDbContext())
            {
                var table = new StepLevel("First Level", new Step("First Level - 1"));

                context.Add(table);

                context.SaveChanges();

                var results = context.StepLevels.ToList();
            }
        }

        [TestMethod]
        public void Update()
        {
           var result =  UpdateStepAssignedToLevelAsync(new Step() { Id = 1, Name = "Modified" }, 1);
        }

        public bool UpdateStepAssignedToLevelAsync(Step step, int levelId)
        {
            using(var context = new StackOverFlowDbContext())
            {
                var item = context.StepLevels
                                  .Include(sl => sl.Step)
                                  .FirstOrDefault(x => x.Id == step.Id && x.Id == levelId);
                if (item == null)
                {
                    return false;
                }

                // Updating Name property
                item.Step.Name = step.Name;

                // Other properties can be upadated

                var rows = context.SaveChanges();
                return rows > 0;
            }
            
        }

        [TestMethod]
        public void Update1()
        {
            var step = new Step() { Id = 1, Name = "Modified-1" };

            using (var context = new StackOverFlowDbContext())
            {
                var item = context.StepLevels
                                  .Include(sl => sl.Step)
                                  .FirstOrDefault(x => x.Id == step.Id && x.Id == 1);

                context.Entry(item.Step).CurrentValues.SetValues(step);

                var rows = context.SaveChanges();
            }
        }
    }
}
