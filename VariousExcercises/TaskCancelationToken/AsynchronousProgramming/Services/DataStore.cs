using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskCancelationToken.AsynchronousProgramming.Services.Domain;

namespace TaskCancelationToken.AsynchronousProgramming.Services
{
    public class DataStore
    {
        public Dictionary<string, Company> Companies = new Dictionary<string, Company>();
        public static Dictionary<string, IEnumerable<StockPrice>> Stocks
            = new Dictionary<string, IEnumerable<StockPrice>>();

        public async Task<Dictionary<string, IEnumerable<StockPrice>>> LoadStocks()
        {
            if (Stocks.Any()) return Stocks;

            await LoadCompanies();

            var prices = await GetStockPrices();

            Stocks = prices
                .GroupBy(x => x.Ticker)
                .ToDictionary(x => x.Key, x => x.AsEnumerable());

            return Stocks;
        }

        public async Task LoadCompanies()
        {
            using (var stream = new StreamReader(File.OpenRead(@"C:\Users\m.hoshen\source\repos\Exercises\VariousExcercises\TaskCancelationToken\AsynchronousProgramming\Services\StockData\CompanyData.csv")))
            {
                await stream.ReadLineAsync();

                string line;
                while ((line = await stream.ReadLineAsync()) != null)
                {
                    #region Loading and Adding Company to In-Memory Dictionary

                    var segments = line.Split(',');

                    for (var i = 0; i < segments.Length; i++) segments[i] = segments[i].Trim('\'', '"');

                    var company = new Company
                    {
                        Symbol = segments[0],
                        CompanyName = segments[1],
                        Exchange = segments[2],
                        Industry = segments[3],
                        Website = segments[4],
                        Description = segments[5],
                        CEO = segments[6],
                        IssueType = segments[7],
                        Sector = segments[8]
                    };

                    if (!Companies.ContainsKey(segments[0]))
                    {
                        Companies.Add(segments[0], company);
                    }

                    #endregion
                }
            }
        }


        public async Task<IList<StockPrice>> GetStockPrices()
        {
            var prices = new List<StockPrice>();

            using (var stream =
                new StreamReader(File.OpenRead(@"C:\Users\m.hoshen\source\repos\Exercises\VariousExcercises\TaskCancelationToken\AsynchronousProgramming\Services\StockData\StockPrices_Small.csv")))
            {
                await stream.ReadLineAsync(); // Skip headers

                string line;
                while ((line = await stream.ReadLineAsync()) != null)
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

            return prices;
        }
    }
}
