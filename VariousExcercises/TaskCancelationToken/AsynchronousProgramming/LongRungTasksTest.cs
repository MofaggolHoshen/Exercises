using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TaskCancelationToken.AsynchronousProgramming.Services;
using TaskCancelationToken.AsynchronousProgramming.Services.Domain;

namespace TaskCancelationToken.AsynchronousProgramming
{
    [TestClass]
    public class LongRungTasksTest
    {
        [TestMethod]
        public async Task GetStock()
        {
            var dataStore = new DataStore();

            var result = await dataStore.GetStockPrices();
        }

        [TestMethod]
        public async Task ParallelProcessingUsingTPL()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            #region Task.WhenAll
            var taskList = new List<Task<List<StockPrice>>>()
            {
                LongRunningTask(1),
                LongRunningTask(2),
                LongRunningTask(3),
                LongRunningTask(4)
            };

            var tks = Task.WhenAll(taskList);

            await tks;
            #endregion

            #region Parallel.ForEach
            Parallel.Invoke(() => LongRunningTask(1),
                            () => LongRunningTask(2),
                            () => LongRunningTask(3),
                            () => LongRunningTask(4));
            #endregion

            stopwatch.Stop();

            Debug.WriteLine($"All task finished in {stopwatch.ElapsedMilliseconds} milisecs.");
        }

        public Task<List<StockPrice>> LongRunningTask(int testNumber)
        {
            return Task.Run(() =>
            {
                Debug.WriteLine($"Thread {testNumber} started.");
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                var prices = new List<StockPrice>();

                using (var stream =
                    new StreamReader(File.OpenRead(@"C:\Users\m.hoshen\source\repos\Exercises\VariousExcercises\TaskCancelationToken\AsynchronousProgramming\Services\StockData\StockPrices_Small.csv")))
                {
                    stream.ReadLine(); // Skip headers

                    string line;
                    while ((line = stream.ReadLine()) != null)
                    {
                        var segments = line.Split(',');

                        for (var i = 0; i < segments.Length; i++) segments[i] = segments[i].Trim('\'', '"');
                        var price = new StockPrice
                        {
                            Ticker = segments[0],
                            TradeDate = DateTime.ParseExact(segments[1], "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                            Volume = Convert.ToInt32(segments[6], CultureInfo.InvariantCulture),
                            Change = Convert.ToDecimal(segments[7], CultureInfo.InvariantCulture),
                            ChangePercent = Convert.ToDecimal(segments[8], CultureInfo.InvariantCulture),
                        };
                        prices.Add(price);
                    }
                }

                stopwatch.Stop();

                Debug.WriteLine($"Thread {testNumber} finished in {stopwatch.ElapsedMilliseconds} milisecs.");

                return prices;
            });
        }
    }
}
