using System;

namespace DelegatesExcercise
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine(Ex.FirstName);
            //Console.WriteLine(Ex.LastName.ToString());

            //Console.WriteLine("Hello World!");

            //Example from = null;
            //NullValuePrintExample(from);

            var ex = new Example()
            {
                Id = 1,
                Name = "Mofaggol Hoshen"
            };

            Extention.FuncDelegateWithGenericType<Example>(ex, u =>
            {
                u.Address = "Adam-Opel Str.";
            });

        }

        private static void NullValuePrintExample(Example from)
        {
            if ((from?.Address == "hello") || (from?.IsActive == true))
            {

                Console.WriteLine(from?.Address);
            }
        }

        public static void FuncDelegate(Example ex, Action<Example> predicate)
        {
            predicate.Invoke(ex);

            var result = ex;
        }
        public static Example InjectFrom(Example from, Example To)
        {
            var example = new Example();

            var properties = from.GetType().GetProperties();

            foreach (var item in properties)
            {
                var name = item.Name;

                var value1 = To.GetType().GetProperty(name).GetValue(To);
                var value2 = from.GetType().GetProperty(name).GetValue(from);

                if (value1 != null)
                    example.GetType().GetProperty(name).SetValue(example, value1);
                else
                    example.GetType().GetProperty(name).SetValue(example, value2);
            }

            return example;
        }
    }

    public class Example
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
    }

    public enum Ex
    {
        FirstName = 1,
        LastName = 2
    }
}
