using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.DataAcess.EntityBaseRepository.IEntityBaseRepository
{
    // Interface for a generic repository that provides basic CRUD operations
    public interface IEntityBaseRepository<T> where T : class
    {
        // Retrieves all entities that match the specified filter and includes related properties if specified
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string? includeProperties = null);

        // Retrieves a single entity that matches the specified filter and includes related properties if specified
        // Optionally tracks the entity in the context
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);

        // Adds a new entity to the repository
        void Add(T entity);

        // Removes an entity from the repository
        void Remove(T entity);

        // Removes a range of entities from the repository
        void RemoveRange(IEnumerable<T> entities);
    }
}
