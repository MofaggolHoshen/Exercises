
using BloazorSample.Components;
using System;
using System.Collections.Generic;

namespace BloazorSample.Services
{
    public class ServiceClass
    {
        public List<ListItem> Get()
        {

          return  new List<ListItem>() { new ListItem { Value = 1 }, new ListItem { Value = 2 }, new ListItem { Value = 3 } };
        }
    }
}
