using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime DatePurchased { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Card")]
        public int CardId { get; set; }
        public Card Card { get; set; }

        public Order(Customer customer, Product product, Card card)
        {
            this.DatePurchased = DateTime.Now;
            this.Quantity = product.Quantity;

            this.Product = product;
            this.Card = card;
            this.Customer = customer;

        }

        public Order()
        {

        }
    }
}
