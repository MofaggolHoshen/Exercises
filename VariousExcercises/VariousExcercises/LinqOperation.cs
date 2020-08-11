using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Globalization;

namespace VariousExcercises
{
    [TestClass]
    public class LinqOperation
    {
        [TestMethod]
        public void MyMethod()
        {
            var person = new Person(1, "Hoshen");

            var b = person.IsValid;

            string er;
            var bv = person.ValidErrorMgs.TryGetValue(nameof(Person.Name), out er);

            var errorMgs = person.ValidErrorMgs[nameof(Person.Name)];

            person.Name = "Mofaggol Hoshen";

            var bv1 = person.ValidErrorMgs.TryGetValue(nameof(Person.Name), out er);

        }
        public class AppSettings
        {
            public int NameLength { get; set; } = 10;
        }
        public class Person
        {
            public int Id { get; set; }
            private string _name;

            public string Name
            {
                get
                {
                    //if (string.IsNullOrEmpty(_name))
                    //    _validErrorMgs.Add(nameof(this.Name), "Value is not assigned");

                    return _name;
                }
                set
                {
                    if (string.IsNullOrEmpty(value) || value.Length < 10)
                        _validErrorMgs.Add(nameof(this.Name), "Name should not be less than 10 characters.");
                    else
                        _validErrorMgs.Remove(nameof(this.Name));

                    _name = value;
                }
            }

            //If we have multiple validation error message we can use array instead of string 
            private Dictionary<string, string> _validErrorMgs = new Dictionary<string, string>();

            public Dictionary<string, string> ValidErrorMgs
            {
                get { 
                    return _validErrorMgs; 
                }
            }

            public bool IsValid
            {
                get
                {
                    return ValidErrorMgs.Count == 0;
                }
            }

            public Person(int id, string name)
            {
                this.Id = id;
                this.Name = name;
            }

        }
    }
}
