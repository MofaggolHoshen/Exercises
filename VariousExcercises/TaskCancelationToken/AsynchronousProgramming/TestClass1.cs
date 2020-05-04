using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskCancelationToken.AsynchronousProgramming.Services;
using TaskCancelationToken.AsynchronousProgramming.Services.Domain;

namespace TaskCancelationToken.AsynchronousProgramming
{
    [TestClass]
    public class TestClass
    {
        [TestMethod]
        public async Task TestMethod()
        {
            var service = new StockService();

            var content = await service.GetEmployeesAsync();
        }
        [TestMethod]
        public void DisPatcherTest()
        {
            //var service = new WebClientService();
            // await service.GetSocksAsync();
        }

        [TestMethod]
        public async Task MyMethod()
        {
            var service = new StockService();

            string[] tickers = new string[] { "MSFT"};
            var tickerLoadingTasks = new List<Task<IEnumerable<StockPrice>>>();
            foreach (var ticker in tickers)
            {
                var loadTask = service.GetStockPricesFor(ticker, new CancellationTokenSource().Token);

                tickerLoadingTasks.Add(loadTask);
            }

            var timeOutTask = Task.Delay(2000);

            var allStocksLoadingTask = Task.WhenAll(tickerLoadingTasks);

            var complitatedTask = await Task.WhenAny(timeOutTask, allStocksLoadingTask);

            if(complitatedTask == timeOutTask)
            {
                // cancellationTokenSource.Cancel();
                // cancellationTokenSource = null;
                // We can throw exception
            }
            var result = allStocksLoadingTask.Result.SelectMany(i => i);

            //var sources = await allStocksLoadingTask;

            //var values = new ConcurrentBag<StockPrice>();
            //Parallel.ForEach(sources, stock =>
            //{
            //    values.Add()
            //});
            

        }

        [TestMethod]
        public async Task ThreadSave()
        {
            var service = new StockService();
            var stocks = new ConcurrentBag<StockPrice>();

            string[] tickers = new string[] { "MSFT" };
            var tickerLoadingTasks = new List<Task<IEnumerable<StockPrice>>>();
            foreach (var ticker in tickers)
            {
                var loadTask = service.GetStockPricesFor(ticker, new CancellationTokenSource().Token)
                    .ContinueWith(t=> {
                        foreach (var stock in t.Result.Take(5))
                            stocks.Add(stock);
                        return t.Result;
                    });

                tickerLoadingTasks.Add(loadTask);
            }

            var allStocksLoadingTask = Task.WhenAll(tickerLoadingTasks);

            await allStocksLoadingTask;
        }

        /// <summary>
        /// This only applies to traditional ASP.NET not ASP.NET Core
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ContinueConfigTest()
        {
            //var result = await ContinueConfig();
            var date = new DateTime(2020, 1, 23);

            var toTalDays = (DateTime.Now.Date > date.Date);

            var d = DateTime.Now.AddDays(-1).ToString("dddd, dd MMMM yyyy");

            await Task.CompletedTask;
        }


        public async Task<IEnumerable<StockPrice>> ContinueConfig()
        {
            var service = new StockService();

            var stocks = await service.GetStockPricesFor("MSFT", CancellationToken.None)
                // This only applies to traditional ASP.NET not ASP.NET Core
                .ConfigureAwait(false);

            return stocks.Take(5);
        }



    }
}
