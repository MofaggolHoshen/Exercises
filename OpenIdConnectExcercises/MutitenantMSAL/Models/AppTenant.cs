using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutitenantMSAL.Models
{
    public class AppTenant
    {
        public string Name { get; set; }
        public string[] Hostnames { get; set; }
        
        public string ConnectionString { get; set; }
        
        public string TenantId { get; set; }

        public string Domain { get; set; }
        
    }
}