
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
    public class ProductsRepository : Repository<Product>, IProductsRepository 
    {

        private ApplicationDbContext _db;
        public ProductsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Product_Id == obj.Product_Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Description = obj.Description;
                objFromDb.Category_Id = obj.Category_Id;
                objFromDb.Author = obj.Author;
                if(obj.ImageURL != null)
                {
                    objFromDb.ImageURL = obj.ImageURL;
                }
                



            }
        }
    }
}
