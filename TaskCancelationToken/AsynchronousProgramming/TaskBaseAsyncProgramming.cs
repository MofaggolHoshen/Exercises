using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime;

namespace TaskCancelationToken.AsynchronousProgramming
{
    [TestClass]
    public class TaskBaseAsyncProgramming
    {
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
                },
                                                      new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks });
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

        /// <summary>
        /// If the Result property is accessed before the computation finishes, 
        /// the property blocks the calling thread until the value is available.
        /// </summary>
        [TestMethod]
        public void TaskResult()
        {
            Task<Double>[] taskArray = { Task<Double>.Factory.StartNew(() => DoComputation(1.0)),
                                     Task<Double>.Factory.StartNew(() => DoComputation(100.0)),
                                     Task<Double>.Factory.StartNew(() => DoComputation(1000.0)) };

            var results = new Double[taskArray.Length];
            Double sum = 0;

            for (int i = 0; i < taskArray.Length; i++)
            {
                results[i] = taskArray[i].Result;
                Debug.WriteLine("{0:N1} {1}", results[i],
                                  i == taskArray.Length - 1 ? "= " : "+ ");
                sum += results[i];
            }
            Debug.WriteLine("{0:N1}", sum);
        }

        private Double DoComputation(Double start)
        {
            Double sum = 0;
            for (var value = start; value <= start + 10; value += .1)
                sum += value;

            return sum;
        }

        [TestMethod]
        public void TaskCulture()
        {
            decimal[] values = { 163025412.32m, 18905365.59m };
            string formatString = "C2";
            Func<String> formatDelegate = () => {
                string output = String.Format("Formatting using the {0} culture on thread {1}.\n",
                                              CultureInfo.CurrentCulture.Name,
                                              Thread.CurrentThread.ManagedThreadId);
                foreach (var value in values)
                    output += String.Format("{0}   ", value.ToString(formatString));

                output += Environment.NewLine;
                return output;
            };

            Debug.WriteLine("The example is running on thread {0}",
                              Thread.CurrentThread.ManagedThreadId);
            // Make the current culture different from the system culture.
            Debug.WriteLine("The current culture is {0}",
                              CultureInfo.CurrentCulture.Name);
            if (CultureInfo.CurrentCulture.Name == "fr-FR")
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            else
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");

            Debug.WriteLine("Changed the current culture to {0}.\n",
                              CultureInfo.CurrentCulture.Name);

            // Execute the delegate synchronously.
            Debug.WriteLine("Executing the delegate synchronously:");
            Debug.WriteLine(formatDelegate());

            // Call an async delegate to format the values using one format string.
            Debug.WriteLine("Executing a task asynchronously:");
            var t1 = Task.Run(formatDelegate);
            Debug.WriteLine(t1.Result);

            Debug.WriteLine("Executing a task synchronously:");
            var t2 = new Task<String>(formatDelegate);
            t2.RunSynchronously();
            Debug.WriteLine(t2.Result);
        }

        [TestMethod]
        public void CreatingChildTasks()
        {
            var parent = Task.Factory.StartNew(() => {
                Debug.WriteLine("Parent task beginning.");
                for (int ctr = 0; ctr < 10; ctr++)
                {
                    int taskNo = ctr;
                    Task.Factory.StartNew((x) => {
                        Thread.SpinWait(5000000);
                        Debug.WriteLine("Attached child #{0} completed.",
                                          x);
                    },
                                          taskNo, TaskCreationOptions.AttachedToParent); // Attach child task with parent task 
                }
            });

            parent.Wait();
            Debug.WriteLine("Parent task completed.");
        }

        [TestMethod]
        public void CopySample()
        {
            CustomData custom = new CustomData {
              CreationTime = 20,
              Name = 20,
              ThreadNum = 20,
              ProName = "Mofaggol"
            };

            CustomData custom1 = new CustomData(custom);

            CustomData custom2 = custom.ShallowCopy();

            var b = ReferenceEquals(custom, custom1);

            custom1.ProName = "Hoshen";
            custom1.Name = 10;

        }

    }

    class CustomData
    {
        public long CreationTime;
        public int Name;
        public int ThreadNum;
        public string ProName { get; set; }

        public CustomData ShallowCopy()
        {

            return (CustomData)this.MemberwiseClone();
        }

        public CustomData()
        {

        }

        public CustomData(CustomData data)
        {
            this.CreationTime = data.CreationTime;
            this.Name = data.Name;
            this.ThreadNum = data.ThreadNum;
            this.ProName = data.ProName;
        }
    }
}
