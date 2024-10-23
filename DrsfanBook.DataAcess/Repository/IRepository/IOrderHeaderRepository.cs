using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrsfanBook.Models;

namespace DrsfanBook.DataAcess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        
        void Update(OrderHeader obj);
    }
}
