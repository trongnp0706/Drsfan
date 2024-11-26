using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
using Drsfan.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Drsfan.DataAccess.EntityBaseRepository
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class
    {
        private readonly DrsfanDbContext _db; // Database context
        internal DbSet<T> dbSet; // DbSet for the entity
        public EntityBaseRepository(DrsfanDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>(); // Initialize DbSet
                                  //_db.Categories == dbset
            _db.Products.Include(u => u.Category).Include(u => u.CategoryId); // Include related entities
        }

        public void Add(T entity)
        {
            dbSet.Add(entity); // Add entity to DbSet
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet; // Return tracked entities
            }
            else
            {
                query = dbSet.AsNoTracking(); // Return untracked entities
            }

            query = query.Where(filter); // Apply filter
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty); // Include related entities
                }
            }
            return query.FirstOrDefault(); // Return the first entity or default
        }

        //Category,CoverType
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter); // Apply filter
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty); // Include related entities
                }
            }
            return query.ToList(); // Return list of entities
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity); // Remove entity from DbSet
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity); // Remove range of entities from DbSet
        }
    }
}
