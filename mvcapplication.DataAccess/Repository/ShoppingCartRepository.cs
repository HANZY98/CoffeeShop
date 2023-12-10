
using Microsoft.EntityFrameworkCore;
using mvcapplication.DataAccess.Data;
using mvcapplication.DataAccess.Repository.IRepository;
using mvcapplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mvcapplication.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {

        private ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        

        public void Update(ShoppingCart obj)
        {
            _db.ShopCart.Update(obj);
        }
    }
}
