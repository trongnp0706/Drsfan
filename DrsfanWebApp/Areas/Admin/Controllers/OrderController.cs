using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
using Drsfan.Models;
using Drsfan.Models.ViewModels;
using Drsfan.Utility;
using Drsfan.Utility.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Diagnostics;
using System.Security.Claims;

namespace DrsfanBookWeb.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class OrderController : Controller
    {


        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM OrderVM { get; set; }
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int orderId)
        {
            OrderVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            };

            return View(OrderVM);
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Staff)]
        public IActionResult UpdateOrderDetail()
        {
            // Retrieve the order header from the database using the order ID from the view model
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);

            // If the order header is not found, set an error message and redirect to the index page
            if (orderHeaderFromDb == null)
            {
                TempData["Error"] = "Order not found.";
                return RedirectToAction(nameof(Index));
            }

            // Update the order header properties with the values from the view model
            UpdateOrderHeaderProperties(orderHeaderFromDb, OrderVM.OrderHeader);

            // Save the updated order header to the database
            _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "Order Details Updated Successfully.";
            return RedirectToAction(nameof(Details), new { orderId = orderHeaderFromDb.Id });
        }

        private void UpdateOrderHeaderProperties(OrderHeader orderHeaderFromDb, OrderHeader orderHeaderFromVm)
        {
            orderHeaderFromDb.Name = orderHeaderFromVm.Name;
            orderHeaderFromDb.PhoneNumber = orderHeaderFromVm.PhoneNumber;
            orderHeaderFromDb.StreetAddress = orderHeaderFromVm.StreetAddress;
            orderHeaderFromDb.City = orderHeaderFromVm.City;
            orderHeaderFromDb.State = orderHeaderFromVm.State;
            orderHeaderFromDb.PostalCode = orderHeaderFromVm.PostalCode;

            if (!string.IsNullOrEmpty(orderHeaderFromVm.Carrier))
            {
                orderHeaderFromDb.Carrier = orderHeaderFromVm.Carrier;
            }

            if (!string.IsNullOrEmpty(orderHeaderFromVm.TrackingNumber))
            {
                orderHeaderFromDb.TrackingNumber = orderHeaderFromVm.TrackingNumber;
            }
        }


        [HttpPost]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Staff)]
        public IActionResult StartProcessing()
        {
            _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id, Status.InProcess);
            _unitOfWork.Save();
            TempData["Success"] = "Order is now being processed.";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }

		[HttpPost]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Staff)]
        public IActionResult ShipOrder()
        {
            var orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);

            if (orderHeader == null)
            {
                TempData["Error"] = "Order not found.";
                return RedirectToAction(nameof(Index));
            }

            orderHeader.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            orderHeader.Carrier = OrderVM.OrderHeader.Carrier;
            orderHeader.OrderStatus = Status.Shipped;
            orderHeader.ShippingDate = DateTime.Now;

            if (orderHeader.PaymentStatus == Payment.StatusDelayedPayment)
            {
                orderHeader.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
            }

            _unitOfWork.OrderHeader.Update(orderHeader);
            _unitOfWork.Save();
            TempData["Success"] = "Order Shipped Successfully.";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }

		[HttpPost]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Staff)]
        public IActionResult CancelOrder()
        {
            var orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);

            if (orderHeader == null)
            {
                TempData["Error"] = "Order not found.";
                return RedirectToAction(nameof(Index));
            }

            if (orderHeader.PaymentStatus == Payment.StatusApproved)
            {
                var options = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeader.PaymentIntentId
                };

                var service = new RefundService();
                Refund refund = service.Create(options);

                _unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, Status.Cancelled, Status.Refunded);
            }
            else
            {
                _unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, Status.Cancelled, Status.Cancelled);
            }

            _unitOfWork.Save();
            TempData["Success"] = "Order Cancelled Successfully.";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }



        [ActionName("Details")]
        [HttpPost]
        public IActionResult Details_PAY_NOW()
        {
            OrderVM.OrderHeader = _unitOfWork.OrderHeader
                .Get(u => u.Id == OrderVM.OrderHeader.Id, includeProperties: "ApplicationUser");
            OrderVM.OrderDetail = _unitOfWork.OrderDetail
                .GetAll(u => u.OrderHeaderId == OrderVM.OrderHeader.Id, includeProperties: "Product");

            // Stripe logic
            var domain = $"{Request.Scheme}://{Request.Host.Value}/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{domain}admin/order/PaymentConfirmation?orderHeaderId={OrderVM.OrderHeader.Id}",
                CancelUrl = $"{domain}admin/order/details?orderId={OrderVM.OrderHeader.Id}",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };

            foreach (var item in OrderVM.OrderDetail)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100), // $20.50 => 2050
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name
                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);
            _unitOfWork.OrderHeader.UpdateStripePaymentID(OrderVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult PaymentConfirmation(int orderHeaderId)
        {

            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderHeaderId);
            if (orderHeader.PaymentStatus == Payment.StatusDelayedPayment)
            {
                //this is an order by company

                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unitOfWork.OrderHeader.UpdateStripePaymentID(orderHeaderId, session.Id, session.PaymentIntentId);
                    _unitOfWork.OrderHeader.UpdateStatus(orderHeaderId, orderHeader.OrderStatus, Payment.StatusApproved);
                    _unitOfWork.Save();
                }

            }

            return View(orderHeaderId);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(string status)
        {
            // Declare a variable to hold the order headers
            IEnumerable<OrderHeader> objOrderHeaders;

            // Check if the user is an Admin or Staff
            if (User.IsInRole(UserRoles.Admin) || User.IsInRole(UserRoles.Staff))
            {
                // If the user is an Admin or Staff, get all order headers including the ApplicationUser property
                objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            }
            else
            {
                // If the user is not an Admin or Staff, get the user ID from the claims
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                // Get all order headers for the current user including the ApplicationUser property
                objOrderHeaders = _unitOfWork.OrderHeader
                    .GetAll(u => u.ApplicationUserId == userId, includeProperties: "ApplicationUser");
            }

            // Filter the order headers based on the status parameter
            switch (status)
            {
                case "pending":
                    objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == Payment.StatusDelayedPayment);
                    break;
                case "inprocess":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == Status.InProcess);
                    break;
                case "completed":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == Status.Shipped);
                    break;
                case "approved":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == Status.Approved);
                    break;
                default:
                    break;
            }

            // Return the filtered order headers as JSON
            return Json(new { data = objOrderHeaders });
        }


        #endregion
    }
}
