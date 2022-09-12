using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCancelationToken
{
    [TestClass]
    public class TaskWithCulture
    {
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
            var t12 = Task.Run(formatDelegate);
                Debug.WriteLine(t12.Result);

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
    }
}
