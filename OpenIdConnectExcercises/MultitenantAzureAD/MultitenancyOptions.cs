using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5
{
    public class MultitenancyOptions
    {
        public Collection<AppTenant> Tenants { get; set; }
    }
}
