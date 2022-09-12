//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Text;
//using System.Threading;

//namespace TaskCancelationToken
//{
//    public class Parallel_Thread2
//    {
//        static void Main(string[] args)
//        {
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();

//            ThreadStart testThreadStart1 = new ThreadStart(new Parallel_Thread2().testThread1);
//            Thread testThread1 = new Thread(testThreadStart1);

//            ThreadStart testThreadStart2 = new ThreadStart(new Parallel_Thread2().testThread2);
//            Thread testThread2 = new Thread(testThreadStart2);

//            ThreadStart testThreadStart3 = new ThreadStart(new Parallel_Thread2().testThread3);
//            Thread testThread3 = new Thread(testThreadStart3);



//            testThread1.Start();
//            testThread2.Start();
//            testThread3.Start();

//            stopwatch.Stop();
//            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);

//            Console.ReadLine();
//        }

//        public void testThread1()
//        {
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();

//            //executing in thread
//            int count = 0;
//            while (count++ < 5000)
//            {
//                Console.WriteLine("Thread 1 Executed 1 " + count + " times");
//                stopwatch.Stop();
//                Console.WriteLine("Time elapsed for thread 1: {0}", stopwatch.Elapsed);
//                Thread.Sleep(5000);
//            }

//        }
//        public void testThread2()
//        {
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();
//            //executing in thread
//            int count = 0;
//            while (count++ < 5000)
//            {
//                Console.WriteLine("Thread 2 Executed " + count + " times");
//                stopwatch.Stop();
//                Console.WriteLine("Time elapsed for thread 2: {0}", stopwatch.Elapsed);
//                Thread.Sleep(605);
//            }

//        }
//        public void testThread3()
//        {
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();
//            //executing in thread
//            int count = 0;
//            while (count++ < 5000)
//            {
//                Console.WriteLine("Thread 3 Executed " + count + " times");
//                stopwatch.Stop();
//                Console.WriteLine("Time elapsed for thread 3: {0}", stopwatch.Elapsed);
//                Thread.Sleep(907);
//            }

//        }
//    }
//}
