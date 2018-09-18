using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IsPrarentDeleteChiledDelete.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string State { get; set; }

        #region Navigation Properties
        [ForeignKey("Child")]
        public int ChildId { get; set; }
        public Child Child { get; set; }
        #endregion
    }
}
