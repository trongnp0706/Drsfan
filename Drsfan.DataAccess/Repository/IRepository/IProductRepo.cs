﻿using Drsfan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.DataAcess.Repository.IRepository
{
    
   public interface IProductRepo :  IRepository<Product>
    {
        void Update(Product obj);
    }
}
