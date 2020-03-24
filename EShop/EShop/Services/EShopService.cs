using EShop.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Services
{
    public class EShopService
    {
        private readonly EShopDbContext _eShopDbContext;

        public EShopService(EShopDbContext dbContext)
        {
            this._eShopDbContext = dbContext;
        }

        public Shop InitShop()
        {
            var shop = _eShopDbContext.Shops
                                      .Include(i=> i.Discounts)
                                      .Include(i => i.Products)
                                        .ThenInclude(i=> i.Orders)
                                      .Include(i => i.Products)
                                        .ThenInclude(i => i.Discounts)
                                      .FirstOrDefault();

            return shop;
        }

        public Customer InitCustomer()
        {
            var customer = _eShopDbContext.Customers
                                           .Include(i=> i.Cards)
                                          .FirstOrDefault();

            return customer;
        }

        public int Persists()
        {
            return _eShopDbContext.SaveChanges();
        }
    }
}
