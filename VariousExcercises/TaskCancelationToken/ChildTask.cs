using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCancelationToken
{
    [TestClass]
    public class ChildTask
    {
        /// <summary>
        /// Note that the parent task does not wait for the detached child task to finish.
        /// </summary>
        [TestMethod]
        public void CreatingDetachedChildTasks()
        {
            var outer = Task.Factory.StartNew(() =>
            {
                Debug.WriteLine("Outer task beginning.");

                var child = Task.Factory.StartNew(() =>
                {
                    Thread.SpinWait(5000000);
                    Debug.WriteLine("Detached task completed.");
                });

            });

            outer.Wait();
            Debug.WriteLine("Outer task completed.");
        }

        [TestMethod]
        public void CreatingChildTasks()
        {
            var parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent task beginning.");
                for (int ctr = 0; ctr < 10; ctr++)
                {
                    int taskNo = ctr;

                    Task.Factory.StartNew((x) =>
                    {
                        Thread.SpinWait(5000);
                        Debug.WriteLine("Attached child #{0} completed.", x);
                    }, taskNo, TaskCreationOptions.AttachedToParent);
                }
            });

            parent.Wait();

            Debug.WriteLine("Parent task completed.");
        }

        [TestMethod]
        public void WaitingForTasksToFinish()
        {

            Task[] tasks = new Task[3]
            {
               Task.Factory.StartNew(() => { Debug.WriteLine("Method1"); }),
               Task.Factory.StartNew(() =>{ Debug.WriteLine("Method2"); }),
               Task.Factory.StartNew(() => { Debug.WriteLine("Method3"); })
            };

            //Block until all tasks complete.
            Task.WaitAll(tasks);

            // Continue on this thread...
        }
    }
}
