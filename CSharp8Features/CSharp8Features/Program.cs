using System;
using System.Threading.Tasks;

namespace CSharp8Features
{
    class Program
    {
        static void Main(string[] args)
        {


            //foreach (var item in LocalFunction.Counter(1,5))
            //{
            //    Console.WriteLine(item);
            //}

            var kk = SwitchStatementSample.GetFullName(new Person("Mofaggol","Hoshen"));

            Console.ReadLine();
        }
    }
}
