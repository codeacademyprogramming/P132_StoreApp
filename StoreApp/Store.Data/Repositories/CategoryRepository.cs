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
    public class CategoryRepository :Repository<Category> ,ICategoryRepository
    {
        private readonly StoreDbContext _context;

        public CategoryRepository(StoreDbContext context):base(context)
        {

        }
    }
}
