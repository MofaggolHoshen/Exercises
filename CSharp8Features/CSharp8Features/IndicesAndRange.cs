using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp8Features
{
    public static class IndicesAndRange
    {
        public static int[] myArray = { 1, 2, 3, 4, 5, 6, 7 };

        public static int GetValueByIndex(Index index)
        {
            return myArray[index]; // ^2, second last
        }

        public static int[] GetValueByRange(Range range)
        {
            return myArray[range]; // ^2, second last
        }

    }
}
