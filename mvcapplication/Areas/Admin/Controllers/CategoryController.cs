using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvcapplication.DataAccess.Data;
using mvcapplication.DataAccess.Repository;
using mvcapplication.DataAccess.Repository.IRepository;
using mvcapplication.Models;
using mvcapplication.Utility;

namespace mvcapplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork UOW)
        {
            _unitOfWork = UOW;
        }

        public IActionResult Index()
        {
            List<Category> categoryList = _unitOfWork.CategoryRepository.GetAll().ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index"); // Redirect to the desired action after successful submission


            }

            return View();



        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _unitOfWork.CategoryRepository.Get(u => u.Category_Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category cat)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Update(cat);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View();



        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _unitOfWork.CategoryRepository.Get(u => u.Category_Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Delete(Category cat)
        {
            Category? obj = _unitOfWork.CategoryRepository.Get(u => u.Category_Id == cat.Category_Id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.CategoryRepository.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");





        }
    }
}
