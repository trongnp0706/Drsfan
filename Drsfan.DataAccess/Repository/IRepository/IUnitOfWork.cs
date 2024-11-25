using Drsfan.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.DataAcess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepo Category { get; }
        IProductRepo Product { get; }
        IProductImageRepo ProductImage { get; }
        ICompanyRepo Company { get; }
        IShoppingCartRepo ShoppingCart { get; }
        IApplicationUserRepo ApplicationUser { get; }
        IOrderDetailRepo OrderDetail { get; }
        IOrderHeaderRepo OrderHeader { get; }
        void Save();
    }
}
