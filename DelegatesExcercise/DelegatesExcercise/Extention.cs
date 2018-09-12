using System;
using System.Collections.Generic;
using System.Text;

namespace DelegatesExcercise
{
    public static class Extention
    {
        public static void FuncDelegateWithGenericType<T>(this T ex, Action<T> predicate)
        {
            predicate.Invoke(ex);

            var result = ex;
        }
    }
}
