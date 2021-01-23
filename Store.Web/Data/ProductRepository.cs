﻿

namespace Store.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using Store.Web.Dados.Entities;
    using System.Linq;

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext context;

        public ProductRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return this.context.Products.Include(p => p.User);
        }
    }
}
