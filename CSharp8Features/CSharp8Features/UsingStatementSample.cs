using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp8Features
{
    public static class UsingStatementSample
    {
        public static void OldSample()
        {

           using(var us1 = new UsingStatement1())
            {
                using(var us2 = new UsingStatement2())
                {
                    us2.PrintMessage();
                    us1.PrintMessage();

                    Console.WriteLine("End inner using statement");
                }

                Console.WriteLine("End outer using statement");
            }
        }
        public static void Sample()
        {

            using var us1 = new UsingStatement1();
            using var us2 = new UsingStatement2();
            us2.PrintMessage();
            us1.PrintMessage();
            Console.WriteLine("End inner using statement");

            Console.WriteLine("End outer using statement");
        }
    }

    public class UsingStatement1 : IDisposable
    {
        public void PrintMessage()
        {

            Console.WriteLine("I am from UsingStatement1.");
        }

        public void Dispose()
        {
            
        }
    }

    public class UsingStatement2 : IDisposable
    {
        public void PrintMessage()
        {

            Console.WriteLine("I am from UsingStatement2.");
        }

        public void Dispose()
        {
           
        }
    }
}
