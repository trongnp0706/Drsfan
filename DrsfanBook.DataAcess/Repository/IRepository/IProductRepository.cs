using Drsfan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.DataAcess.EntityBaseRepository.IEntityBaseRepository
{
    
   public interface IProductRepository :  IEntityBaseRepository<Product>
    {
        void Update(Product obj);
    }
}
