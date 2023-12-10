using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcapplication.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IUsersRepository UsersRepository { get; }

        IProductsRepository ProductsRepository { get; }
        IShoppingCartRepository ShoppingCartRepository { get; }
        IApplicationUserRepository ApplicationUserRepository { get; }
        IOrderDetail OrderDetailRepository { get; }
        IOrderHeaderRepository OrderHeaderRepository { get; }
        void Save();
    }
}
