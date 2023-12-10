
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
    public class UsersRepository : Repository<Users>, IUsersRepository 
    {

        private ApplicationDbContext _db;
        public UsersRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        

        public void Update(Users obj)
        {
            _db.Users.Update(obj);
        }
    }
}
