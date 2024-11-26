﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drsfan.Models;

namespace Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository
{
    public interface IOrderDetailRepo : IEntityBaseRepository<OrderDetail>
    {
        
        void Update(OrderDetail obj);
    }
}
