using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskCancelationToken.AsynchronousProgramming.Services.Domain;
using System.Text.Json;
using System.Linq;

namespace TaskCancelationToken.AsynchronousProgramming.Services
{
    public class StockService
    {
        public async Task<string> GetEmployeesAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://dummy.restapiexample.com/api/v1/employees");
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task GetSocksAsync()
        {
            string[] UiThreadLine = null;

            UiThreadLine = await Task.Run(() =>
            {
                var line = File.ReadAllLines(@"C:\Users\m.hoshen\source\repos\Exercises\VariousExcercises\TaskCancelationToken\AsynchronousProgramming\Services\StockData\StockPrices_Small.csv");

                return line;
            });

        }

        public Task<IEnumerable<StockPrice>> GetStockPricesFor(string ticker,
            CancellationToken cancellationToken)
        {
            var stocks = new List<StockPrice> {
                new StockPrice { Ticker = "MSFT", Change = 0.5m, ChangePercent = 0.75m },
                new StockPrice { Ticker = "MSFT", Change = 0.2m, ChangePercent = 0.15m },
                new StockPrice { Ticker = "GOOGL", Change = 0.3m, ChangePercent = 0.25m },
                new StockPrice { Ticker = "GOOGL", Change = 0.5m, ChangePercent = 0.65m }
            };

            return Task.FromResult(stocks.Where(stock => stock.Ticker == ticker));
        }

        public void GetStocksAsync()
        {
            CancellationTokenSource source = null;// = new CancellationTokenSource();

            if(source != null)
            {
                source.Cancel();
                source = null;
            }

            source = new CancellationTokenSource();

            var token = source.Token;

            var task = Task.Run(() =>
            {
                if (!token.IsCancellationRequested)
                {
                    var line = File.ReadAllLines(@"C:\Users\m.hoshen\source\repos\Exercises\VariousExcercises\TaskCancelationToken\AsynchronousProgramming\Services\StockData\StockPrices_Small.csv");

                    return line;
                }
                return null;
            }, token);

            var task2 = task.ContinueWith(lien =>
             {
                 var l = lien.Result;
             }, token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Current);

            task2.ContinueWith(t =>
            {

            }, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
