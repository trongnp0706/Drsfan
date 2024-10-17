using DrsfanBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrsfanBook.DataAcess.Repository.IRepository
{
    public interface  ICompanyRepository : IRepository<Company>
    {
        void Update(Company obj);
    }
}
