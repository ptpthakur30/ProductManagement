using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManagement.DataAccess.Interface;
using ProductManagement.Utility;

namespace ProductManagement.DataAccess.Repository
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {

        private readonly DataContext _db;
        internal DbSet<T> dbSet;

        public RepositoryAsync(DataContext db)
        {
            if (db == null)
                throw new Exception("Database not initialised");
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<T> GetAsync(int id)
        {
            T obj = await dbSet.FindAsync(id);
            if (obj == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, new { product = "Not Found" });
            return obj;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            IEnumerable<T> lst = await query.ToListAsync();
            if (lst == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, new { product = "Not Found" });
            return lst;
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            T entity = await query.FirstOrDefaultAsync();
            if(entity == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, new { product = "Not Found" });
            return entity;
        }

        public async Task RemoveAsync(int id)
        {
            T entity = await dbSet.FindAsync(id);
            if (entity == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, new { product = "Not Found" });
            Remove(entity);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}