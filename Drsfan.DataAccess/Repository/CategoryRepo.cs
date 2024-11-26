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
    public class CategoryRepo : EntityBaseRepository<Category>, ICategoryRepo
    {
        private DrsfanDbContext _db;
        public CategoryRepo(DrsfanDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
