using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;


namespace IsKeyWord
{

    [TestClass]
    public class TypePattern
    {
        [TestMethod]
        public void TestMethod()
        {
            Object o = new Person("Mofaggols");
            ShowValue(o);

            o = new Dog("Alaskan Malamute");
            ShowValue(o);
        }

        public static void ShowValue(object o)
        {
            if (o is Person {Name: "Mofaggol" } p)
            {
                Debug.WriteLine(p.Name);
            }
            else if (o is Dog d)
            {
                Debug.WriteLine(d.Breed);
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public Person(string name)
        {
            Name = name;
        }
    }

    public class Dog
    {
        public string Breed { get; set; }

        public Dog(string breedName)
        {
            Breed = breedName;
        }
    }
    // The example displays the following output:
    //	Jane
    //	Alaskan Malamute
}
