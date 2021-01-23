using Store.Web.Dados.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Data
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable GetAllWithUsers();

    }
}
