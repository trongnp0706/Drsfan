
using Drsfan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository
{
    public interface IProductImageRepo : IEntityBaseRepository<ProductImage>
    {
        void Update(ProductImage obj);
    }
}
