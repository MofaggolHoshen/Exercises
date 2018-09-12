using System;

namespace EnumExcercise
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintEnumValue(ActionTypeEnum.Inactive);
            PrintEnumPropertyName(2);

            Console.WriteLine("Program Exit");
            Console.Read();
        }

        private static void PrintEnumPropertyName(int v)
        {

        }

        private static void PrintEnumValue(ActionTypeEnum actionType)
        {
            int value = (int) actionType;

            Console.WriteLine($"Enum value: {value}");
        }

        
    }

    public enum ActionTypeEnum
    {
        Create = 1,
        Active = 2,
        Inactive = 3
    }
}
