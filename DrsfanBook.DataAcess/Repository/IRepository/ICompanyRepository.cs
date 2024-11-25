using Drsfan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.DataAcess.EntityBaseRepository.IEntityBaseRepository
{
    public interface  ICompanyRepository : IEntityBaseRepository<Company>
    {
        void Update(Company obj);
    }
}
