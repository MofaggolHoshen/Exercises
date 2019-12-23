using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8Features
{
    public static class AsyncEnumarableSample
    {
        public static async IAsyncEnumerable<int> GeneralSequence()
        {
            for (int i = 0; i < 50; i++)
            {
                if (i % 10 == 0)
                    await Task.Delay(2000);

                yield return i;
            }
        }
    }
}
