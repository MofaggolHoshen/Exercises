using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace VariousExcercises
{
    [TestClass]
    public class AsyncProgramming
    {

        public async Task GetLongTermAsync()
        {
            Debug.WriteLine("GetLongTermAsync enter.");

            await Task.Delay(20000);

            Debug.WriteLine("GetLongTermAsync, i am done.");

        }

        public async Task<string> GetTaskAsync()
        {
            Debug.WriteLine("GetTaskAsync entered.");

            await Task.Delay(20000);

            Debug.WriteLine("GetTaskAsync, i am returning.");

            return "mofaggol";
        }

        public async Task TestHeperMethod()
        {
            Debug.WriteLine("GetLongTermAsync started.");

            await GetLongTermAsync();

            Debug.WriteLine("GetLongTermAsync finished.");

            Debug.WriteLine("GetLongTermAsync started.");

            var result = await GetTaskAsync();

            Debug.WriteLine("GetLongTermAsync finished.");

        }

        [TestMethod]
        public async Task AsyncProgramTest01()
        {

            await TestHeperMethod();
        }
    }
}
