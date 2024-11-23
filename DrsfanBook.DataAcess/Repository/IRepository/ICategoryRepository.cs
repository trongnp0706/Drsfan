using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drsfan.Models;

namespace Drsfan.DataAcess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        
        void Update(Category obj);
    }
}
