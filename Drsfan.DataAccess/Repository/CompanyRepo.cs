﻿using System;
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
    public class CompanyRepository : EntityBaseRepository<Company>, ICompanyRepo
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
