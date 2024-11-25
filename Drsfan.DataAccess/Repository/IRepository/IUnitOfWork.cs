using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.DataAcess.EntityBaseRepository.IEntityBaseRepository
{
    // Interface for Unit of Work pattern
    public interface IUnitOfWork
    {
        // Repository for each entity model
        ICategoryRepo Category { get; }
        IProductRepo Product { get; }
        IProductImageRepo ProductImage { get; }
        ICompanyRepo Company { get; }
        IShoppingCartRepo ShoppingCart { get; }
        IApplicationUserRepo ApplicationUser { get; }
        IOrderDetailRepo OrderDetail { get; }
        IOrderHeaderRepo OrderHeader { get; }

        // Method to save changes to the database
        void Save();
    }
}
