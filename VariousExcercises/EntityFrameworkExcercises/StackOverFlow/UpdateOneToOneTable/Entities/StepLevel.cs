using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkExcercises.StackOverFlow.UpdateOneToOneTable.Entities
{
    public class StepLevel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Step  Step { get; set; }

        private StepLevel()
        {
        }

        public StepLevel(string name, Step steps)
        {
            Name = name;
            Step = steps;
        }
    }
}
