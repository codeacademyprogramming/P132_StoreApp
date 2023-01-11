using Microsoft.EntityFrameworkCore;
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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly StoreDbContext _context;

        public Repository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (includes != null) { foreach (var reff in includes) query = query.Include(reff); }

            return query.Where(exp);
        }


        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (includes != null) { foreach (var reff in includes) query = query.Include(reff); }

            return await query.FirstOrDefaultAsync(exp);
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (includes != null) { foreach (var reff in includes) query = query.Include(reff); }

            return await query.AnyAsync(exp);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
