using Store.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Data
{
    public class Country : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
