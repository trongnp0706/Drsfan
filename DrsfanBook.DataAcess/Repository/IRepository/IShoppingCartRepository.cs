using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drsfan.Models;

namespace Drsfan.DataAcess.EntityBaseRepository.IEntityBaseRepository
{
    public interface IShoppingCartRepository : IEntityBaseRepository<ShoppingCart>
    {
        
        void Update(ShoppingCart obj);
    }
}
