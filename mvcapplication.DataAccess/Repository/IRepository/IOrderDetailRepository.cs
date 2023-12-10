using mvcapplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mvcapplication.DataAccess.Repository.IRepository
{
    public interface IOrderDetail : IRepository<OrderDetail>
    {

        void Update(OrderDetail obj);
        
        

        
    }
}
