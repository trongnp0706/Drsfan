using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drsfan.Models;

namespace Drsfan.DataAcess.EntityBaseRepository.IEntityBaseRepository
{
    public interface IOrderDetailRepository : IEntityBaseRepository<OrderDetail>
    {
        
        void Update(OrderDetail obj);
    }
}
