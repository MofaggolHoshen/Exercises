using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Domain
{
    public class Card
    {
        public int Id { get; set; }
        [Required]
        public int NumberOnCard { get; set; }
        [Required]
        public string CardHolderName { get; set; }
        [Required]
        public int ExpirationMonth { get; set; }
        [Required]
        public int ExpirationYear { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime ExpirationDate
        {
            get
            {
                int dateInMonth = DateTime.DaysInMonth(this.ExpirationYear, this.ExpirationMonth);

                return new DateTime(this.ExpirationYear, this.ExpirationMonth, dateInMonth);
            }
        }

    }
}
