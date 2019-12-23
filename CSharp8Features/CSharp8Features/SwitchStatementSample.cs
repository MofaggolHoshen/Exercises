using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp8Features
{
    public static class SwitchStatementSample
    {
        public static string GetFullName(Person person) 
        {

           return  (person) switch
            {
                { MiddleName: { }, LastName: { } } => person.FirstName,
                {  MiddleName: { } } => $"{person.FirstName} {person.LastName}",
                _ => "Name not found!"
            };
        }
    }
}
