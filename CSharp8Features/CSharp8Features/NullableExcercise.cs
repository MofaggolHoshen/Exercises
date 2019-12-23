using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp8Features
{
    public static class NullableExcercise
    {
        public static Person GetPerson()
        {

            return new Person("Mofaggol", "Hoshen");
        }
    }


    public class Person
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public Person(string fn, string ln)
        {
            FirstName = fn;
            LastName = ln;
        }
    }
}
