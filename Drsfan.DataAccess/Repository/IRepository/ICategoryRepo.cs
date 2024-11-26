using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drsfan.Models;

namespace Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository
{
    public interface ICategoryRepo : IEntityBaseRepository<Category>
    {
        
        void Update(Category obj);
    }
}
