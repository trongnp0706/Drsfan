using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Drsfan.DataAccess.Data;
using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
using Drsfan.Models;

namespace Drsfan.DataAccess.EntityBaseRepository
{
    public class OrderDetailRepo : EntityBaseRepository<OrderDetail>, IOrderDetailRepo
    {
        private DrsfanDbContext _db;
        public OrderDetailRepo(DrsfanDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
