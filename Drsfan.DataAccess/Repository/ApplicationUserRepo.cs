using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Drsfan.DataAcess.Data;
using Drsfan.DataAcess.Repository.IRepository;
using Drsfan.Models;

namespace Drsfan.DataAcess.Repository
{
    public class ApplicationUserRepo : Repository<ApplicationUser>, IApplicationUserRepo
    {
        private DrsfanDbContext _db;
        public ApplicationUserRepo(DrsfanDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
