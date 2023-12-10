using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using mvcapplication.DataAccess.Migrations;
using mvcapplication.DataAccess.Repository;
using mvcapplication.DataAccess.Repository.IRepository;
using mvcapplication.Models;
using mvcapplication.Models.ViewModels;
using mvcapplication.Utility;
using Stripe.Checkout;
using System.Diagnostics;
using System.Security.Claims;

namespace mvcapplication.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public ShopController(ILogger<ShopController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != null)
            {
                HttpContext.Session.SetInt32(SD.SessionCart,
                   _unitOfWork.ShoppingCartRepository.GetAll(u => u.ApplicationUserId == userId).Count());
            }
                IEnumerable<Product> productList = _unitOfWork.ProductsRepository.GetAll(includeProperties:"Category");
            return View(productList);
            
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCartRepository.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new()
            };

            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUserRepository.Get(u => u.Id == userId);

            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.FirstLineOfAddress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.PostCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostCode;


            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = ShoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
            }
            return View(ShoppingCartVM);  
        }

        [HttpPost]
        [ActionName("Summary")]
		public IActionResult SummaryPOST()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCartRepository.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product");

            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
			ShoppingCartVM.OrderHeader.ApplicationUserId = userId;


			ApplicationUser applicationUser = _unitOfWork.ApplicationUserRepository.Get(u => u.Id == userId);

			


			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				cart.Price = ShoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
			}

            ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
			ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;

            _unitOfWork.OrderHeaderRepository.Add(ShoppingCartVM.OrderHeader);
            _unitOfWork.Save();

            foreach(var cart in ShoppingCartVM.ShoppingCartList)
            {
                OrderDetail orderDetail = new()
                {
                    Product_Id = cart.Product_Id,
                    OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count

                };
                _unitOfWork.OrderDetailRepository.Add(orderDetail);
                _unitOfWork.Save();
            }
           if(applicationUser.Id != null) { 
            var domain = "https://localhost:7248/";
			var options = new SessionCreateOptions
			{
				SuccessUrl = domain+$"customer/shop/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
                CancelUrl = domain+$"customer/shop/cart",
				LineItems = new List<SessionLineItemOptions>(),
				Mode = "payment",
			};

            foreach(var item in ShoppingCartVM.ShoppingCartList)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Product.Price*100),
                        Currency ="gbp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Title
                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
			}

			var service = new SessionService();
			Session session = service.Create(options);
            _unitOfWork.OrderHeaderRepository.UpdateStripePaymentId(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
			}



			return RedirectToAction(nameof(OrderConfirmation), new { id =ShoppingCartVM.OrderHeader.Id });
		}

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeaderRepository.Get(u=> u.Id == id, includeProperties: "ApplicationUser");
            if (orderHeader.PaymentStatus != null)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);

                if(session.PaymentStatus.ToLower()== "paid")
                {
					_unitOfWork.OrderHeaderRepository.UpdateStripePaymentId(id, session.Id, session.PaymentIntentId);
                    _unitOfWork.OrderHeaderRepository.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                    _unitOfWork.Save();
				}
            }
            HttpContext.Session.Clear();

            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCartRepository.GetAll(u=>u.ApplicationUserId==orderHeader.ApplicationUserId).ToList();
            _unitOfWork.ShoppingCartRepository.RemoveRange(shoppingCarts);
			_unitOfWork.Save();


			return View(id);
        }

        [Authorize]
        public IActionResult Cart()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCartRepository.GetAll(u=>u.ApplicationUserId==userId, includeProperties: "Product"),
                OrderHeader=new()
            };

            foreach(var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = ShoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
            }

            
            return View(ShoppingCartVM);
        }

        


        public IActionResult Details(int id)
        {
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.ProductsRepository.Get(u => u.Product_Id == id, includeProperties: "Category"),
            Count = 1,
            Product_Id = id
            };
            
            
            return View(cart);
            
        }

        
        public IActionResult Remove(int id)
        {
            var cartFromDb = _unitOfWork.ShoppingCartRepository.Get(u => u.Id == id, tracked:true);


            HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCartRepository.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() - 1);
            _unitOfWork.ShoppingCartRepository.Remove(cartFromDb);
            
            _unitOfWork.Save();
            


            return RedirectToAction(nameof(Cart));

        }

        public IActionResult Plus(int id)
        {
            var cartFromDb = _unitOfWork.ShoppingCartRepository.Get(u => u.Id == id);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCartRepository.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Cart));
        }



        public IActionResult Minus(int id)
        {
            
            var cartFromDb = _unitOfWork.ShoppingCartRepository.Get(u => u.Id == id, tracked: true);
            if (cartFromDb.Count <= 1){
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCartRepository.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() - 1);
                _unitOfWork.ShoppingCartRepository.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCartRepository.Update(cartFromDb);
            }
                
            
            _unitOfWork.Save();
            return RedirectToAction(nameof(Cart));
           
        }


        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shopCart)
        {
           var claimsIdentity = (ClaimsIdentity)User.Identity;
           var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shopCart.ApplicationUserId = userId;

            ShoppingCart cartfromDB = _unitOfWork.ShoppingCartRepository.Get(u => u.ApplicationUserId == userId && u.Product_Id==shopCart.Product_Id
            );

            if(cartfromDB != null)
            {
                cartfromDB.Count += shopCart.Count;
                _unitOfWork.ShoppingCartRepository.Update(cartfromDB);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.ShoppingCartRepository.Add(shopCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart, 
                    _unitOfWork.ShoppingCartRepository.GetAll(u => u.ApplicationUserId == userId ).Count());
            }

            
            


            return RedirectToAction(nameof(Index));

        }

      
    public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}