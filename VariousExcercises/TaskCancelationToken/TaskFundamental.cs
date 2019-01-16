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
    public class TaskFundamental
    {
        [TestMethod]
        public void CreatingAndRunningTasksImplicitly()
        {

            Parallel.Invoke(() => { Debug.WriteLine("I am first method"); }, () => { Debug.WriteLine("I am second method"); });
        }

        [TestMethod]
        public void CreatingAndRunningTasksExplicitly()
        {

            Thread.CurrentThread.Name = "Main";

            // Create a task and supply a user delegate by using a lambda expression. 
            Task taskA = new Task(() => Debug.WriteLine("Hello from taskA."));
            // Start the task.
            taskA.Start();

            // Output a message from the calling thread.
            Debug.WriteLine("Hello from thread '{0}'.",
                              Thread.CurrentThread.Name);
            taskA.Wait();
        }

        [TestMethod]
        public void TaskFactoryStartNew()
        {

            Task[] taskArray = new Task[10];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((Object obj) =>
                {
                    CustomData data = obj as CustomData;
                    if (data == null)
                        return;

                    data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                }, new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks });
            }
            Task.WaitAll(taskArray);
            foreach (var task in taskArray)
            {
                var data = task.AsyncState as CustomData;
                if (data != null)
                    Debug.WriteLine("Task #{0} created at {1}, ran on thread #{2}.",
                                      data.Name, data.CreationTime, data.ThreadNum);
            }
        }

        [TestMethod]
        public void ReturnValueFromTask()
        {
            var result = Task<CustomData>.Factory.StartNew(() =>
            {
                return new CustomData
                {
                    Name = 1,
                    CreationTime = DateTime.Now.Ticks,
                    ThreadNum = 2
                };
            });
        }
    }

    public class CustomData
    {
        public int Name { get; set; }
        public long CreationTime { get; set; }
        public int ThreadNum { get; set; }
    }
}
