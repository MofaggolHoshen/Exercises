using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCancelationToken.AsynchronousProgramming
{
    [TestClass]
    public class TaskParallelLibraryTest
    {
        /// <summary>
        /// Note that the addition is performed by calling the Interlocked.Add so that the addition is performed as an atomic operation. 
        /// Otherwise, multiple tasks could try to update the totalSize variable simultaneously.
        /// </summary>
        [TestMethod]
        public void ParallelForExample1()
        {
            long totalSize = 0;

            String[] files = Directory.GetFiles(@"C:\Users\m.hoshen\Pictures\Camera Roll\");

            Parallel.For(0, files.Length, index =>
            {
                FileInfo fileInfo = new FileInfo(files[index]);
                long size = fileInfo.Length;
                Interlocked.Add(ref totalSize, size);
            });

            Debug.WriteLine("{0:N0} files, {1:N0} bytes", files.Length, totalSize);
        }

        public void ParallelForExample2()
        {
            // Set up matrices. Use small values to better view
            // result matrix. Increase the counts to see greater
            // speedup in the parallel loop vs. the sequential loop.
            int colCount = 180;
            int rowCount = 2000;
            int colCount2 = 270;
            double[,] m1 = InitializeMatrix(rowCount, colCount);
            double[,] m2 = InitializeMatrix(colCount, colCount2);
            double[,] result = new double[rowCount, colCount2];

            MultiplyMatricesParallel(m1, m2, result);


        }
        private void MultiplyMatricesParallel(double[,] matA, double[,] matB, double[,] result)
        {
            int matACols = matA.GetLength(1);
            int matBCols = matB.GetLength(1);
            int matARows = matA.GetLength(0);

            // A basic matrix multiplication.
            // Parallelize the outer loop to partition the source array by rows.
            Parallel.For(0, matARows, i =>
            {
                for (int j = 0; j < matBCols; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < matACols; k++)
                    {
                        temp += matA[i, k] * matB[k, j];
                    }
                    result[i, j] = temp;
                }
            }); // Parallel.For
        }
        private double[,] InitializeMatrix(int rows, int cols)
        {
            double[,] matrix = new double[rows, cols];

            Random r = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = r.Next(100);
                }
            }
            return matrix;
        }
        /// <summary>
        /// For(Int32, Int32, Action<Int32,ParallelLoopState>) 
        /// </summary>
        [TestMethod]
        public void ParallelForExample3()
        {
            var rnd = new Random();
            int breakIndex = 2; //rnd.Next(1, 11);

            Debug.WriteLine($"Will call Break at iteration {breakIndex}\n");

            var result = Parallel.For(1, 101, (i, state) =>
            {
                Debug.WriteLine($"Beginning iteration {i}");
                int delay;
                lock (rnd)
                    delay = rnd.Next(1, 1001);
                Thread.Sleep(delay);

                Debug.WriteLine($"ShouldExitCurrentIteration: {state.ShouldExitCurrentIteration}");
                Debug.WriteLine($"LowestBreakIteration: {state.LowestBreakIteration}");

                // ShouldExitCurrentIteration is true after Break() called.
                if (state.ShouldExitCurrentIteration)
                {
                    if (state.LowestBreakIteration < i)
                        return;
                }

                if (i == breakIndex)
                {
                    Debug.WriteLine($"Break in iteration {i}");
                    state.Break();
                }

                Debug.WriteLine($"Completed iteration {i}");
            });

            if (result.LowestBreakIteration.HasValue)
                Debug.WriteLine($"\nLowest Break Iteration: {result.LowestBreakIteration}");
            else
                Debug.WriteLine($"\nNo lowest break iteration.");
        }

        /// <summary>
        /// For(Int32, Int32, ParallelOptions, Action<Int32>) 
        /// </summary>
        public void ParallelForExample4()
        {

            CancellationTokenSource cancellationSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions();
            options.CancellationToken = cancellationSource.Token;

            //Parallel.For(0, 10, options, i =>
            //{
            //    // Simulate a cancellation of the loop when i=2
            //    if (i == 2)
            //    {
            //        cancellationSource.Cancel();
            //    }
            //});

            try
            {
                ParallelLoopResult loopResult = Parallel.For(
                        0,
                        10,
                        options,
                        (i, loopState) =>
                        {
                            Debug.WriteLine("Start Thread={0}, i={1}", Thread.CurrentThread.ManagedThreadId, i);

                            // Simulate a cancellation of the loop when i=2
                            if (i == 2)
                            {
                                cancellationSource.Cancel();
                            }

                            // Simulates a long execution
                            for (int j = 0; j < 10; j++)
                            {
                                Thread.Sleep(1 * 200);

                                // check to see whether or not to continue
                                if (loopState.ShouldExitCurrentIteration) return;
                            }

                            Debug.WriteLine("Finish Thread={0}, i={1}", Thread.CurrentThread.ManagedThreadId, i);
                        }
                    );

                if (loopResult.IsCompleted)
                {
                    Debug.WriteLine("All iterations completed successfully. THIS WAS NOT EXPECTED.");
                }
            }
            // No exception is expected in this example, but if one is still thrown from a task,
            // it will be wrapped in AggregateException and propagated to the main thread.
            catch (AggregateException e)
            {
                Debug.WriteLine("Parallel.For has thrown an AggregateException. THIS WAS NOT EXPECTED.\n{0}", e);
            }
            // Catching the cancellation exception
            catch (OperationCanceledException e)
            {
                Debug.WriteLine("An iteration has triggered a cancellation. THIS WAS EXPECTED.\n{0}", e.ToString());
            }
            finally
            {
                cancellationSource.Dispose();
            }
        }

        [TestMethod]
        public void ThreadLocalForWithOptions()
        {
            // The number of parallel iterations to perform.
            const int N = 10;

            // The result of all thread-local computations.
            int result = 0;

            // This example limits the degree of parallelism to four.
            // You might limit the degree of parallelism when your algorithm
            // does not scale beyond a certain number of cores or when you 
            // enforce a particular quality of service in your application.

            Parallel.For(0, N, new ParallelOptions { MaxDegreeOfParallelism = 4 },
               // Initialize the local states for each task
               () =>
               {
                   // We can do some calculation which will pass to each task.
                   return 0;
               },
               // Accumulate the thread-local computations in the loop body
               (index, loop, localState) =>
               {
                   Debug.WriteLine($"Local State: {localState}");

                   for (long i = 0; i < 900000000; i++) ;

                   return localState;
               },
               // Combine all local states
               localState =>
               {
                   Interlocked.Add(ref result, localState);

                   Debug.WriteLine($"Local State: {localState} finished.");
               }
            );

            // Print the actual and expected results.
            Debug.WriteLine("Actual result: {0}. Expected 100.", result);
        }

        private static int Compute(int n)
        {
            for (int i = 0; i < 10000; i++) ;
            return 1;
        }

        [TestMethod]
        public void ParallelForWithParallelOptions()
        {
            long result = 0;
            // MaxDegreeOfParallelism, How many task run parallel
            Parallel.For(0, 10, new ParallelOptions { MaxDegreeOfParallelism = 2 }, index =>
               {
                   Debug.WriteLine($"Task: {index}");

                   long k = 0;
                   for (long i = 0; i < 90000000000000; i++)
                   {
                       k = i;
                   }
                   Interlocked.Add(ref result, k);
               });

            Debug.WriteLine($"Total Count: {result}");
        }
        [TestMethod]
        public void ParallelForeachExample1()
        {
            var files = Directory.GetFiles(@"C:\Users\m.hoshen\Pictures\Camera Roll\Images");
            string newDir = @"C:\Users\m.hoshen\Pictures\Camera Roll\Images\Modified";
            Directory.CreateDirectory(newDir);

            Parallel.ForEach(files, file =>
            {
                var fileName = Path.GetFileName(file);

                Bitmap bitmap = new Bitmap(file);
                bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);

                bitmap.Save(Path.Combine(newDir, fileName));
            });

        }
    }
}
