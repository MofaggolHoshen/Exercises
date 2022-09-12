using System;
using System.Collections.Generic;
using System.Text;

namespace EqualityTest
{
    public struct Food
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public Food(string name, int num)
        {
            this.Name = name;
            this.Number = num;
        }

    }
}
