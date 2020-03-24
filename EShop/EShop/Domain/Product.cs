using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public double Price { get; set; }
        public int Quantities { get; set; }
        
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        public Shop  Shop { get; set; }

        public List<ProductDiscount> Discounts { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public int Quantity { get; set; }

        [NotMapped]
        public string ImageBase64String
        {
            get
            {
                if (this.Image is { })
                    return String.Format($"data:image/jpeg;base64,{Convert.ToBase64String(this.Image)}");
                else
                    return string.Empty;
            }
        }

        public Product()
        {

        }

        public Product(Shop shop, Product product)
        {
            this.Title = product.Title;

            using var mstr = new MemoryStream();
            product.ImageFile.CopyTo(mstr);

            this.Image = mstr.ToArray();

            this.Price = product.Price;
            this.Quantities = product.Quantities;
            this.Description = product.Description;
            this.Shop = product.Shop;
            this.DateCreated = DateTime.Now;
            this.DateUpdated = DateTime.Now;
        }

        public void Set(Product product)
        {
            if (!this.Title.Equals(product.Title))
                this.Title = product.Title;

            if (product.ImageFile != null)
            {
                using var mstr = new MemoryStream();
                product.ImageFile.CopyTo(mstr);

                this.Image = mstr.ToArray();
            }

            if(!this.Price.Equals(product.Price))
            this.Price = product.Price;

            if (!this.Quantities.Equals(product.Quantities))
                this.Quantities = product.Quantities;

            if (!this.Description.Equals(product.Description))
                this.Description = product.Description;

            this.DateUpdated = DateTime.Now;
        }

        public bool IsInStock
        {
            get
            {
                return this.Quantities > this.Orders.Sum(i=> i.Quantity);
            }
        }

        public int QuantitiesInSock { 
            get 
            { 
                return this.Quantities - this.Orders.Sum(i => i.Quantity);
            } 
        }

        public double PriceWithDiscount(DateTime date)
        {
            return this.Price - this.DiscountPrice(date);
        }

        private bool myDiscount(DateTime date)
        {
            if (this.Discounts.Count > 0 && this.Discounts.Any(i => i.DateStarted <= date && i.DateEnded >= date))
                return true;
            else
                return false;
        }

        public bool HasDiscount(DateTime date)
        {
            if (this.myDiscount(date) || this.Shop.HasDiscount(date))
                return true;
            else
                return false;
        }

        public double DiscountPrice(DateTime date)
        {
            double totalDiscountInPercentage = 0;

            //Shop
                totalDiscountInPercentage += this.Shop.DiscountValueInPercentage(date);

            // Product
            if(this.myDiscount(date))
                totalDiscountInPercentage += (this.Discounts.SingleOrDefault(i => i.ProductId == this.Id && i.DateStarted <= date && i.DateEnded >= date).ValueInPercentage);


            return this.Price * totalDiscountInPercentage;
        }
    }
}
