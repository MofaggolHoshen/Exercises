using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MutitenantMSAL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [ForeignKey("Tenant")]
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
