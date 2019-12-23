using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp8Features
{
    public static class LocalFunction
    {
        public static IEnumerable<int> Counter(int start, int end)
        {

            return localCountr(start, end);

            IEnumerable<int> localCountr(int start, int end)
            {
                for (int i = 0; i < end; i++)
                {
                    yield return i;
                }
            }
        }


    }
}
