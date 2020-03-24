﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Domain
{
    public class ProductDiscount
    {
        public int Id { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateEnded { get; set; }
        public double ValueInPercentage { get; set; }
        public string Comment { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
