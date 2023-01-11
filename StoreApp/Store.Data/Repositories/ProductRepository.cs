using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Repositories;
using Store.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Repositories
{
    public class ProductRepository :Repository<Product>, IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context):base(context)
        {
            _context = context;
        }
       
    }
}
