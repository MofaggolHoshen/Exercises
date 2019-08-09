using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskCancelationToken
{
    // https://www.youtube.com/watch?v=J0mcYVxJEl0
    // Time: 23:50
    public class AsyncAwaitBestPractice
    {
        #region Don't User .Wait()
        /*
        * Never ever use .Wait() because it will say thread 1 you have to wait and start new thread  
        */
        public async Task ExecuteRefreshCommand()
        {
            // Never ever use .Wait() because it will say thread 1 you have to wait and start new thread, use await.
            // IsExist().Wait();
            // beret way to do this 
            // IsExist().GetAwaiter().GetResult();
            // It will throw our exception with our stack race 
            // If we don't want to block UI thread (or thread 1) use await

            await IsExist();

        }

        private async Task<bool> IsExist()
        {
            await Task.Delay(10);

            return true;
        }

        #endregion Don't User .Wait()

        #region One function depend on other function result
        /*
        * One function is depend on other function result. 
        */
        public async Task<List<Client>> GetClients()
        {
            var clients = new List<Client>();

            // var baIds = await getBusinessActivityIds();
            // If here only await is used, Thread 1 => Thread 2 => Synchronization Context => Thread 1
            // but thread may busy for other job, thread will be waiting, so we need to tall i don't care next which Thread will take care of this.
            // I don't use .ConfigureAwait(false) while i am interacting with UI
            var baIds = await getBusinessActivityIds().ConfigureAwait(false);

            foreach (var baid in baIds)
            {
                var client = await getClient(baid).ConfigureAwait(false);

                clients.Add(client);
            }

            return clients;
        }

        private async Task<List<int>> getBusinessActivityIds()
        {
            await Task.Delay(10);

            return new List<int>();
        }

        private async Task<Client> getClient(int id)
        {
            await Task.Delay(100);

            return new Client()
            {
                Id = 1,
                Name = "Mofaggol Hoshen"
            };
        }

        #endregion One function depend on other function result
    }

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
