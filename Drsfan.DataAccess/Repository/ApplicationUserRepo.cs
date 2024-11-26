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
    public class ApplicationUserRepo : EntityBaseRepository<ApplicationUser>, IApplicationUserRepo
    {
        private DrsfanDbContext _db;
        public ApplicationUserRepo(DrsfanDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser applicationUser)
        {
            _db.ApplicationUsers.Update(applicationUser);
        }
    }
}
