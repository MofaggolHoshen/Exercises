﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqCommon
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
    }
}
