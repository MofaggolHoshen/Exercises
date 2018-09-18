using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestPractices.Models
{
    public class Parent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        #region Navigation Properties
        public ICollection<Child> Children { get; set; }
        #endregion
    }
}
