using Drsfan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository
{
    public interface IApplicationUserRepo : IEntityBaseRepository<ApplicationUser>
    {
        public void Update(ApplicationUser applicationUser);
    }
}
