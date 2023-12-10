using mvcapplication.DataAccess.Data;
using mvcapplication.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcapplication.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationDbContext _db;
        public ICategoryRepository CategoryRepository { get; private set; }
        public IUsersRepository UsersRepository { get; private set; }
        public IProductsRepository ProductsRepository { get; private set; }
        public IShoppingCartRepository ShoppingCartRepository { get; private set; }
        public IApplicationUserRepository ApplicationUserRepository { get; private set; }
        public IOrderHeaderRepository OrderHeaderRepository { get; private set; }

        public IOrderDetail OrderDetailRepository { get; private set; }
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            ApplicationUserRepository = new ApplicationUserRepository(_db);
            ShoppingCartRepository = new ShoppingCartRepository(_db);
            CategoryRepository = new CategoryRepository(_db);
            UsersRepository = new UsersRepository(_db);
            ProductsRepository = new ProductsRepository(_db);
            OrderHeaderRepository = new OrderHeaderRepository(_db);
            OrderDetailRepository = new OrderDetailRepository(_db);
        }
        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
