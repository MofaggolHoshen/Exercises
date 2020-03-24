using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EShop.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Email { get; set; }

        public List<Card> Cards { get; set; }
        public List<Order> Orders { get; set; }  = new List<Order>();
        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        [NotMapped]
        public Card Card { get; set; } = new Card();

        public void AddOrders(List<Product> products)
        {
            foreach (var product in products)
            {
                Orders.Add(new Order(this, product, this.Cards.FirstOrDefault()));
            }
        }
    }
}
