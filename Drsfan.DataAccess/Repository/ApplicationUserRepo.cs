using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Drsfan.DataAcess.Data;
using Drsfan.DataAcess.EntityBaseRepository.IEntityBaseRepository;
using Drsfan.Models;

namespace Drsfan.DataAcess.EntityBaseRepository
{
    public class ApplicationUserRepo : EntityBaseRepository<ApplicationUser>, IApplicationUserRepo
    {
        private DrsfanDbContext _db;
        public ApplicationUserRepo(DrsfanDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
