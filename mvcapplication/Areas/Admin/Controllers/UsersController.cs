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
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(IUnitOfWork UOW)
        {
            _unitOfWork = UOW;
        }

        public IActionResult Index()
        {
            List<Users> usersList = _unitOfWork.UsersRepository.GetAll().ToList();
            return View(usersList);
        }


    }
}
