using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BestPractices.Models
{
    public class Child
    {
        public int Id { get; set; }
        public string ChildName { get; set; }

        #region Navigation Properties
        [ForeignKey("Parent")]
        public int ParentId { get; set; }
        public Parent Parent { get; set; }

        public Address Address { get; set; }
        #endregion
    }
}
