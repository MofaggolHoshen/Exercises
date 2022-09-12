using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace TaskCancelationToken
{
    [TestClass]
    public class AsyncAwait
    {
        [TestMethod]
        public async Task AsyncProgramTest01()
        {
            Debug.Write(typeof(string).Assembly.ImageRuntimeVersion);
            await AsyncAwaitHelper.TestHeperMethod();
        }

    }

    public static class AsyncAwaitHelper
    {
        //Return type of async method must be void, Task or Task<T> but in C# 8 may be it will be possible 
        //public static async string ReturnStrWithoutTask()
        //{
        //    await Task.Delay(20000);

        //    return "Hi i am without task";
        //}

        public static async Task GetLongTermAsync()
        {
            Debug.WriteLine("GetLongTermAsync enter.");

            await Task.Delay(20000);

            Debug.WriteLine("GetLongTermAsync, i am done.");

        }

        public static async Task<string> GetTaskAsync()
        {
            Debug.WriteLine("GetTaskAsync entered.");

            await Task.Delay(20000);

            Debug.WriteLine("GetTaskAsync, i am returning.");

            return "mofaggol";
        }

        public static async Task TestHeperMethod()
        {
            Debug.WriteLine("GetLongTermAsync started.");

            await GetLongTermAsync();

            Debug.WriteLine("GetLongTermAsync finished.");

            Debug.WriteLine("GetLongTermAsync started.");

            var result = await GetTaskAsync();

            Debug.WriteLine("GetLongTermAsync finished.");

        }
    }
}
