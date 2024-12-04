using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository
{

    public interface IEntityBaseRepository<T> where T : class
    {
        // Retrieves all entities that match the specified filter and includes related properties if specified
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string? includeProperties = null);

        
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);

    
        void Add(T entity);

      
        void Remove(T entity);

        
        void RemoveRange(IEnumerable<T> entities);
    }
}
