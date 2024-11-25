using Drsfan.DataAccess.Repository.IRepository;
using Drsfan.DataAcess.Data;
using Drsfan.DataAcess.Repository;
using Drsfan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.DataAccess.Repository
{
    public class ProductImageRepo : Repository<ProductImage>, IProductImageRepo
    {
        private DrsfanDbContext _db;
        public ProductImageRepo(DrsfanDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ProductImage obj)
        {
            _db.ProductImages.Update(obj);
        }
    }
    
}
