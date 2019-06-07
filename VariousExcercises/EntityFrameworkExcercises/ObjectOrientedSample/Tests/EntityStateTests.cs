using EntityFrameworkExcercises.ObjectOrientedSample.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;

namespace EntityFrameworkExcercises.ObjectOrientedSample.Tests
{
    // https://docs.microsoft.com/en-us/ef/ef6/saving/change-tracking/entity-state
    
    [TestClass]
    public class EntityStateTests
    {
        #region Entity State in EntityFramework Core
        /// <summary>
        /// Another way to add a new entity to the context is to change its state to Added. For example:
        /// </summary>
        [TestMethod]
        public void Added()
        {
            using(var context = new ObjectOrientedDbContext())
            {
                var student = new Student("Shatish", "Samuddrala", "GroupWare", "India");
                context.Entry(student).State = EntityState.Added;

                context.SaveChanges();

                var st = context.Students.Where(i => i.FirstName == "Shatish").ToList();
            }
        }

        [TestMethod]
        public void UnChnaged()
        {

            using (var context = new ObjectOrientedDbContext())
            {
                Student student = context.Students.Single(i => i.Id == 1);
                student.SetFirstName("Mofaggol");
                context.Attach(student);

                context.Entry(student).State = EntityState.Unchanged;
                

                var row = context.SaveChanges();

                var st = context.Students.Single(i => i.Id == 1);
            }
        }
        #endregion

        #region Change Tracker in Entity Framework Core
        /*
         * https://www.entityframeworktutorial.net/efcore/changetracker-in-ef-core.aspx
         * 
         *  The DbContext in Entity Framework Core includes the ChangeTracker class in Microsoft.EntityFrameworkCore.ChangeTracking namespace
         *  which is responsible of tracking the state of each entity retrieved using the same DbContext instance. It is not intended to use it 
         *  directly in your application code because it may change in future versions. However, you can use some methods for tracking purpose.
         *  The ChangeTracker class in Entity Framework Core starts tracking of all the entities as soon as it is retrieved using DbContext, 
         *  until they go out of its scope. EF keeps track of all the changes applied to all the entities and their properties, so that it can 
         *  build and execute appropriate DML statements to the underlying data source.
         *  An entity at any point of time has one of the following states which are represented by the enum Microsoft.EntityFrameworkCore.EntityState in EF Core.
         *
         *  Added
         *  Modified
         *  Deleted
         *  Unchanged
         *  Detached
         *  
         *  We can have Entity class name, properties and so on
         */
        [TestMethod]
        public void ChnageTracker()
        {
            using (var context = new ObjectOrientedDbContext())
            {
                // retrieve entity 
                var student = context.Students.First();
                student.SetLastName("Hoshen1");

                DisplayStates(context.ChangeTracker.Entries());
            }
        }

        private static void DisplayStates(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                Debug.WriteLine($"Entity: {entry.Entity.GetType().Name}," +
                                $"State: { entry.State.ToString()}");
            }
        }

        #endregion

        #region Working with Disconnected Entity Graph in Entity Framework Core
        /*
         * https://www.entityframeworktutorial.net/efcore/working-with-disconnected-entity-graph-ef-core.aspx
         *  Entity Framework Core provides the following different methods, which not only attach an entity to a context, 
         *  but also change the EntityState of each entity in a disconnected entity graph:
         *
         *  Attach() : The DbContext.Attach() and DbSet.Attach() methods attach the specified disconnected entity graph and start tracking it. 
         *             They return an instance of EntityEntry, which is used to assign the appropriate EntityState. 
         *  Entry()
         *  Add()
         *  Update()
         *  Remove()
         *
         * 
         */
        #endregion

        #region ChangeTracker.TrackGraph()
        /*
         *  The ChangeTracker.TrackGraph() method was introduced in Entity Framework Core to track the entire entity graph and set custom entity states to each entity in a graph.
         *  Signature: public virtual void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback)
         *  The ChangeTracker.TrackGraph() method begins tracking an entity and any entities that are reachable by traversing it's navigation properties. 
         *  The specified callback is called for each discovered entity and an appropriate EntityState must be set for each entity. The callback function 
         *  allows us to implement a custom logic to set the appropriate state. If no state is set, the entity remains untracked.
         *  
         *  The following example demonstrates the TrackGraph method.
         *  
         *      var student = new Student() { //Root entity (with key value)
         *      StudentId = 1,
         *      Name = "Bill",
         *      Address = new StudentAddress()  //Child entity (with key value)
         *      {
         *          StudentAddressId = 1,
         *          City = "Seattle",
         *          Country = "USA"
         *      },
         *      StudentCourses = new List<StudentCourse>() {
         *              new StudentCourse(){  Course = new Course(){ CourseName="Machine Language" } },//Child entity (empty key)
         *              new StudentCourse(){  Course = new Course(){  CourseId=2 } } //Child entity (with key value)
         *          }
         *      };
         *         
         *  var context = new SchoolContext();
         *              
         *  context.ChangeTracker.TrackGraph(student, e => {
         *                                                  if (e.Entry.IsKeySet)
         *                                                  {
         *                                                      e.Entry.State = EntityState.Unchanged;
         *                                                  }
         *                                                  else
         *                                                  {
         *                                                      e.Entry.State = EntityState.Added;
         *                                                  }
         *                                              });
         *  
         *  foreach (var entry in context.ChangeTracker.Entries())
         *  {
         *      Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, 
         *                          State: {entry.State.ToString()} ");
         *  }
         */
        #endregion
    }
}
