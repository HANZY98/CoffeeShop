using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvcapplication.DataAccess.Repository;
using mvcapplication.DataAccess.Repository.IRepository;
using mvcapplication.Models;
using mvcapplication.Models.ViewModels;
using mvcapplication.Utility;
using Stripe;
using System.Runtime.ConstrainedExecution;

namespace mvcapplication.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM OrderVM { get; set; }

		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
        }



        [HttpPost]
        public IActionResult StartProcessing()
        {
            _unitOfWork.OrderHeaderRepository.UpdateStatus(OrderVM.OrderHeader.Id, SD.StatusInProcess);
            _unitOfWork.Save();
            TempData["Success"] = "Order Details Updated Successfully.";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });

        }

        [HttpPost]
        public IActionResult StartShipping()
        {
            var orderHeader = _unitOfWork.OrderHeaderRepository.Get(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeader.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            orderHeader.Carrier = OrderVM.OrderHeader.Carrier;  
            orderHeader.OrderStatus = SD.StatusShipped;
            orderHeader.ShippingDate = DateTime.Now;
            _unitOfWork.OrderHeaderRepository.Update(orderHeader);
            _unitOfWork.OrderHeaderRepository.UpdateStatus(OrderVM.OrderHeader.Id, SD.StatusShipped);
            _unitOfWork.Save();
            TempData["Success"] = "Order Details Updated Successfully.";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });

        }

        [HttpPost]
        public IActionResult CancelOrder()
        {
            var orderHeader = _unitOfWork.OrderHeaderRepository.Get(u => u.Id == OrderVM.OrderHeader.Id);

            if(orderHeader.PaymentStatus == SD.PaymentStatusApproved)
            {
                var options = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeader.PaymentIntentId
                };

                var service = new RefundService();
                Refund refund = service.Create(options);

                _unitOfWork.OrderHeaderRepository.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusRefunded);
            }
            else
            {
                _unitOfWork.OrderHeaderRepository.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusCancelled);
            }
            _unitOfWork.Save();
            TempData["Success"] = "Order Details Updated Successfully.";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });

        }

        

            [Authorize(Roles = SD.Role_Admin + "," + SD.Role_User_Empl)]
        public IActionResult Index()
		{
           
            List<OrderDetail> orderDetailList = _unitOfWork.OrderDetailRepository
         .GetAll(includeProperties: "OrderHeader")
         .ToList();

            // Group the OrderDetails by OrderHeaderId
            var groupedOrderDetails = orderDetailList.GroupBy(detail => detail.OrderHeaderId);

            // Create a list of OrderVM objects by mapping each group to an OrderVM
            List<OrderVM> orderVMList = groupedOrderDetails.Select(group =>
            {
                var orderHeader = group.First().OrderHeader; // Assuming there is always one OrderHeader for each group
                return new OrderVM
                {
                    OrderHeader = orderHeader,
                    OrderDetail = group
                };
            }).ToList();

            return View(orderVMList);
        }

        public IActionResult Details(int orderId) {
            OrderVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeaderRepository.Get(u=>u.Id==orderId, includeProperties:"ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetailRepository.GetAll(u=>u.OrderHeaderId ==orderId, includeProperties: "Product" )
            };
        
        return View(OrderVM);
        }

        [HttpPost]

        [Authorize(Roles = SD.Role_Admin+","+SD.Role_User_Empl)]
        public IActionResult UpdateOrderDetails()
        {
            var orderHeaderFromDb = _unitOfWork.OrderHeaderRepository.Get(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeaderFromDb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFromDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFromDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
            orderHeaderFromDb.City = OrderVM.OrderHeader.City;
            orderHeaderFromDb.PostCode = OrderVM.OrderHeader.PostCode;
            if (!string.IsNullOrEmpty(OrderVM.OrderHeader.Carrier))
            {
                orderHeaderFromDb.Carrier = OrderVM.OrderHeader.Carrier;
            }
            if (!string.IsNullOrEmpty(OrderVM.OrderHeader.TrackingNumber))
            {
                orderHeaderFromDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            }
            _unitOfWork.OrderHeaderRepository.Update(orderHeaderFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "Order Details Updated Successfully.";

            return RedirectToAction(nameof(Details), new {orderId = orderHeaderFromDb.Id } );
        }




    }
}
