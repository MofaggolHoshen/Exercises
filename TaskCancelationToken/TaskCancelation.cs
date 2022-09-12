using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCancelationToken
{
    [TestClass]
    public class TaskCancelation
    {
        [TestMethod]
        public void TaskCancellation()
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;

            var task = Task.Factory.StartNew(() =>
            {

                // Were we already canceled?
                ct.ThrowIfCancellationRequested();  // It will throw exception while token has canceled 

                bool moreToDo = true;
                while (moreToDo)
                {
                    // Poll on this property if you have to do
                    // other cleanup before throwing.
                    if (ct.IsCancellationRequested)
                    {
                        // Clean up here, then...
                        ct.ThrowIfCancellationRequested();
                    }

                }
            }, tokenSource2.Token); // Pass same token to StartNew.

            tokenSource2.Cancel();
            

            // Just continue on this thread, or Wait/WaitAll with try-catch:
            try
            {
                task.Wait();
            }
            catch (AggregateException e)
            {
                foreach (var v in e.InnerExceptions)
                    Debug.WriteLine(e.Message + " " + v.Message);
            }
            finally
            {
                tokenSource2.Dispose();
            }
        }

        [TestMethod]
        public void CancelTaskAndItsChildren()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            // Store references to the tasks so that we can wait on them and  
            // observe their status after cancellation. 
            Task t;
            var tasks = new ConcurrentBag<Task>();

            Debug.WriteLine("Press any key to begin tasks...");
            //Debug.ReadKey(true);
            Debug.WriteLine("To terminate the example, press 'c' to cancel and exit...");
            //Debug.WriteLine();
            
            // Request cancellation of a single task when the token source is canceled. 
            // Pass the token to the user delegate, and also to the task so it can  
            // handle the exception correctly.
            t = Task.Factory.StartNew(() => DoSomeWork(1, token), token);
            Debug.WriteLine("Task {0} executing", t.Id);
            tasks.Add(t);

            // Request cancellation of a task and its children. Note the token is passed 
            // to (1) the user delegate and (2) as the second argument to StartNew, so  
            // that the task instance can correctly handle the OperationCanceledException.
            t = Task.Factory.StartNew(() => {
                // Create some cancelable child tasks.  
                Task tc;
                for (int i = 3; i <= 10; i++)
                {
                    // For each child task, pass the same token 
                    // to each user delegate and to StartNew.
                    tc = Task.Factory.StartNew(iteration => DoSomeWork((int)iteration, token), i, token);
                    Debug.WriteLine("Task {0} executing", tc.Id);
                    tasks.Add(tc);
                    // Pass the same token again to do work on the parent task.  
                    // All will be signaled by the call to tokenSource.Cancel below.
                    DoSomeWork(2, token);
                }
            },
                                        token);

            Debug.WriteLine("Task {0} executing", t.Id);
            tasks.Add(t);

            // Request cancellation from the UI thread. 
            char ch = 'c';// Debug.ReadKey().KeyChar;

            if (ch == 'c' || ch == 'C')
            {
                tokenSource.Cancel();
                Debug.WriteLine("\nTask cancellation requested.");

                // Optional: Observe the change in the Status property on the task. 
                // It is not necessary to wait on tasks that have canceled. However, 
                // if you do wait, you must enclose the call in a try-catch block to 
                // catch the TaskCanceledExceptions that are thrown. If you do  
                // not wait, no exception is thrown if the token that was passed to the  
                // StartNew method is the same token that requested the cancellation. 
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException e)
            {
                Debug.WriteLine("\nAggregateException thrown with the following inner exceptions:");
                // Display information about each exception. 
                foreach (var v in e.InnerExceptions)
                {
                    if (v is TaskCanceledException)
                        Debug.WriteLine("   TaskCanceledException: Task {0}",
                                          ((TaskCanceledException)v).Task.Id);
                    else
                        Debug.WriteLine("   Exception: {0}", v.GetType().Name);
                }
            }
            finally
            {
                tokenSource.Dispose();
            }

            // Display status of all tasks. 
            foreach (var task in tasks)
                Debug.WriteLine("Task {0} status is now {1}", task.Id, task.Status);
        }

        private void DoSomeWork(int taskNum, CancellationToken ct)
        {
            // Was cancellation already requested? 
            if (ct.IsCancellationRequested == true)
            {
                Debug.WriteLine("Task {0} was cancelled before it got started.",
                                  taskNum);
                ct.ThrowIfCancellationRequested();
            }

            int maxIterations = 100;

            // NOTE!!! A "TaskCanceledException was unhandled 
            // by user code" error will be raised here if "Just My Code" 
            // is enabled on your computer. On Express editions JMC is 
            // enabled and cannot be disabled. The exception is benign. 
            // Just press F5 to continue executing your code. 
            for (int i = 0; i <= maxIterations; i++)
            {
                // Do a bit of work. Not too much. 
                var sw = new SpinWait();
                for (int j = 0; j <= 100; j++)
                    sw.SpinOnce();

                if (ct.IsCancellationRequested)
                {
                    Debug.WriteLine("Task {0} cancelled", taskNum);
                    ct.ThrowIfCancellationRequested();
                }
            }
        }
    }
}
