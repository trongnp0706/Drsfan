using Drsfan.DataAcess.Data;
using Drsfan.DataAcess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.DataAcess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DrsfanDbContext _db;
        public ICategoryRepo Category { get; private set; }
        public ICompanyRepo Company { get; private set; }
        public IProductRepo Product { get; private set; }
        public IShoppingCartRepo ShoppingCart { get; private set; }
        public IApplicationUserRepo ApplicationUser { get; private set; }
        public IOrderDetailRepo OrderDetail { get; private set; }
        public IOrderHeaderRepo OrderHeader { get; private set; }
        public UnitOfWork(DrsfanDbContext db)
        {
            _db = db;
            Category = new CategoryRepo(_db);
            Product = new ProductRepo(_db);
            Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepo(_db);
            ApplicationUser = new ApplicationUserRepo(_db);
            OrderDetail = new OrderDetailRepo(_db);
            OrderHeader = new OrderHeaderRepo(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
