using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
        public List<Customer> Customers { get; set; }
        public List<Discount> Discounts { get; set; }

        public Product AddProduct(Product product)
        {
            var newProduct = new Product(this, product);
            this.Products.Add(newProduct);

            return newProduct;
        }

        public Product Product(int id)
        {
            return this.Products.SingleOrDefault(i => i.Id == id);
        }

        public bool HasDiscount(DateTime date)
        {
            if (this.Discounts.Count > 0 && this.Discounts.Any(i => i.DateStarted <= date && i.DateEnded >= date))
                return true;
            else
                return false;
        }

        public double DiscountValueInPercentage(DateTime date)
        {
            if (this.HasDiscount(date))
            {
                var discountValue = (this.Discounts.SingleOrDefault(i => i.ShopId == this.Id && i.DateStarted <= date && i.DateEnded >= date).ValueInPercentage);

                return discountValue;
            }

            return 0;
        }
    }
}
