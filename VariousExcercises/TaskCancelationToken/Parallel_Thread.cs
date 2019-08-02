using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCancelationToken
{
    public class Parallel_Thread
    {
        private long m_FinishCounter = 0;


        /// <summary>
        /// Starts all job in parallel.
        /// </summary>
        /// <param name="threads"></param>
        /// <param name="func"></param>
        public void StartMultithreadedNative(int threads, Action<object> func)
        {
            for (int i = 0; i < threads; i++)
            {
                var t = new Thread(new ParameterizedThreadStart(func));
                t.Name = i.ToString();
                t.Start(new Action<string>(Stop));
            }

            while (true)
            {
                if (Interlocked.Read(ref m_FinishCounter) == threads)
                    break;
                else
                    Thread.Sleep(500);
            }
        }


        public void StartWithTpl(int threads, Action<object> func)
        {
            List<Task> tList = new List<Task>();

            for (int i = 0; i < threads; i++)
            {
                var t = new Task(func, new Action<string>(Stop));
                tList.Add(t);
            }

            foreach (var t in tList)
            {
                t.Start();
            }

            Task.WaitAll(tList.ToArray());
        }

        private void Stop(string threadName)
        {
            Interlocked.Increment(ref m_FinishCounter);
        }
    }
}
