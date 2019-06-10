using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkExcercises.StackOverFlow.Entities.UpdateOneToOneTable
{
    public class Step
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Step()
        {

        }

        public Step(string name)
        {
            Name = name;
        }
    }
}
