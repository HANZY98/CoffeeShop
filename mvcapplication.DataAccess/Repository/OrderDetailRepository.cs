
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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetail

    {

        private ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        

        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
