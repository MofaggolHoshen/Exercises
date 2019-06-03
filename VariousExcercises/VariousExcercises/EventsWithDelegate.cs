using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VariousExcercises
{
    [TestClass]
    public class EventsWithDelegate
    {
        public event Action<int> MyFirstIntEvent;

        [TestMethod]
        public void EventExample()
        {
            var myClass = new MyClass();

            MyFirstIntEvent += myClass.MyMethod;

            myClass.MyMethod(10);
        }

        [TestMethod]
        public void MyMethod()
        {
            var myClass = new MyClass1();

            if (true)
                myClass.MyMethod1();
            else
                myClass.MyMethod();

            myClass.FireEvents(EventEnum.Second);
        }

        [TestMethod]
        public void Main1()
        {
            Number myNumber = new Number(100000);
            myNumber.PrintMoney();
            myNumber.PrintNumber();
        }
    }

    public class MyClass
    {
        public event Action<int> myEvent;

        public void MyMethod(int val)
        {
            int k = val;
        }
    }

    public class MyClass1
    {
        public event Action<EventEnum> myEvent;
        public event Action<EventEnum> myEvent2;

        public void MyMethod()
        {
            myEvent += MyClass1_myEvent;
        }

        public void MyMethod1()
        {
            myEvent2 += MyClass1_myEvent2;
        }

        private void MyClass1_myEvent2(EventEnum obj)
        {
            int k = (int)obj;
        }

        private void MyClass1_myEvent(EventEnum obj)
        {
            int k = (int)obj;
        }


        public void FireEvents(EventEnum obj)
        {
            if (myEvent != null && EventEnum.First == obj)
            {
                myEvent(obj);
            }
            if (myEvent2 != null && EventEnum.Second == obj)
            {
                myEvent2(obj);
            }
        }

    }

    public enum EventEnum
    {
        First,
        Second
    }

    //public class PrintHelper
    //{
    //    // declare delegate 
    //    public delegate void BeforePrint();

    //    //declare event of type delegate
    //    public event BeforePrint beforePrintEvent;

    //    public PrintHelper()
    //    {

    //    }

    //    public void PrintNumber(int num)
    //    {
    //        //call delegate method before going to print
    //        if (beforePrintEvent != null)
    //            beforePrintEvent();

    //        Debug.WriteLine("Number: {0,-12:N0}", num);
    //    }

    //    public void PrintDecimal(int dec)
    //    {
    //        if (beforePrintEvent != null)
    //            beforePrintEvent();

    //        Debug.WriteLine("Decimal: {0:G}", dec);
    //    }

    //    public void PrintMoney(int money)
    //    {
    //        if (beforePrintEvent != null)
    //            beforePrintEvent();

    //        Debug.WriteLine("Money: {0:C}", money);
    //    }

    //    public void PrintTemperature(int num)
    //    {
    //        if (beforePrintEvent != null)
    //            beforePrintEvent();

    //        Debug.WriteLine("Temperature: {0,4:N1} F", num);
    //    }
    //    public void PrintHexadecimal(int dec)
    //    {
    //        if (beforePrintEvent != null)
    //            beforePrintEvent();

    //        Debug.WriteLine("Hexadecimal: {0:X}", dec);
    //    }
    //}	

    class Number
    {
        private PrintHelper _printHelper;

        public Number(int val)
        {
            _value = val;

            _printHelper = new PrintHelper();
            //subscribe to beforePrintEvent event
            _printHelper.beforePrintEvent += printHelper_beforePrintEvent;
        }
        //beforePrintevent handler
        void printHelper_beforePrintEvent()
        {
            Debug.WriteLine("BeforPrintEventHandler: PrintHelper is going to print a value");
        }

        private int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public void PrintMoney()
        {
            _printHelper.PrintMoney(_value);
        }

        public void PrintNumber()
        {
            _printHelper.PrintNumber(_value);
        }
    }

    public class PrintHelper
    {
        // declare delegate 
        public delegate void BeforePrint();

        //declare event of type delegate
        public event BeforePrint beforePrintEvent;

        public PrintHelper()
        {

        }

        public void PrintNumber(int num)
        {
            //call delegate method before going to print
            if (beforePrintEvent != null)
                beforePrintEvent();

            Debug.WriteLine("Number: {0,-12:N0}", num);
        }

        public void PrintDecimal(int dec)
        {
            if (beforePrintEvent != null)
                beforePrintEvent();

            Debug.WriteLine("Decimal: {0:G}", dec);
        }

        public void PrintMoney(int money)
        {
            if (beforePrintEvent != null)
                beforePrintEvent();

            Debug.WriteLine("Money: {0:C}", money);
        }

        public void PrintTemperature(int num)
        {
            if (beforePrintEvent != null)
                beforePrintEvent();

            Debug.WriteLine("Temperature: {0,4:N1} F", num);
        }
        public void PrintHexadecimal(int dec)
        {
            if (beforePrintEvent != null)
                beforePrintEvent();

            Debug.WriteLine("Hexadecimal: {0:X}", dec);
        }
    }
}
