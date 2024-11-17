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
    public class CompanyRepository : Repository<Company>, ICompanyRepo
    {
        private DrsfanDbContext _db;
        public CompanyRepository(DrsfanDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
