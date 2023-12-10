using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvcapplication.DataAccess.Data;
using mvcapplication.DataAccess.Repository;
using mvcapplication.DataAccess.Repository.IRepository;
using mvcapplication.Models;
using mvcapplication.Models.ViewModels;
using mvcapplication.Utility;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;

namespace mvcapplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment; 
        public ProductsController(IUnitOfWork UOW, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = UOW;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> productList = _unitOfWork.ProductsRepository.GetAll(includeProperties:"Category").ToList();
            
            return View(productList);
        }

        public IActionResult Upsert(int? id)
        {


            ProductVM productVM = new ProductVM
            {
                Product = new Product(),
                CategoryList = _unitOfWork.CategoryRepository.GetAll()
         .Select(u => new SelectListItem { Text = u.Name, Value = u.Category_Id.ToString() })
            };
            if(id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.ProductsRepository.Get(u => u.Product_Id == id);
                return View(productVM);
            }
            

            
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM pro, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"img\products");
                    if (!string.IsNullOrEmpty(pro.Product.ImageURL))
                    {
                        //delete the old image#
                        var oldImage = Path.Combine(wwwRootPath, pro.Product.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, filename),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    pro.Product.ImageURL = @"\img\products\" + filename;
                }

                if (pro.Product.Product_Id == 0)
                {
                    _unitOfWork.ProductsRepository.Add(pro.Product);
                }
                else
                {
                    _unitOfWork.ProductsRepository.Update(pro.Product);
                }
                
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index"); // Redirect to the desired action after successful submission


            }

            return View();



        }

       

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product productFromDb = _unitOfWork.ProductsRepository.Get(u => u.Product_Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult Delete(Product prod)
        {
            Product? obj = _unitOfWork.ProductsRepository.Get(u => u.Product_Id == prod.Product_Id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductsRepository.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");





        }
    }
}
