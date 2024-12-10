using Drsfan.DataAccess.Data;
using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
using Drsfan.Models;
using Drsfan.Models.ViewModels;
using Drsfan.Utility;
using Drsfan.Utility.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace DrsfanBookWeb.Areas.Customer.Controllers
{
    [Area("customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,
                includeProperties: "Product"),
                OrderHeader = new()
            };

            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            // Calculate total item count in the cart for the current user
            int totalItemCount = _unitOfWork.ShoppingCart
                .GetAll(u => u.ApplicationUserId == userId)
                .Sum(item => item.Count); // Sum the count of each product

            // Update session with the accurate total item count
            HttpContext.Session.SetInt32(Constants.CartSession, totalItemCount);

            IEnumerable<ProductImage> productImages = _unitOfWork.ProductImage.GetAll();

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Product.ProductImages = productImages.Where(p => p.ProductId == cart.ProductId).ToList();
                // Calculate price after discount or original price
                cart.Price = CalculateOrderTotal(cart);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(ShoppingCartVM);
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            // Initialize ShoppingCartVM object
            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,
                includeProperties: "Product"),
                OrderHeader = new()
            };
            // Get current user information
            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);



            // Calculate total order amount based on products in cart
            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = CalculateOrderTotal(cart);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPOST()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
                u => u.ApplicationUserId == userId, includeProperties: "Product");

            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = userId;

            // Get application user and populate the City field
            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = CalculateOrderTotal(cart);
                ShoppingCartVM.OrderHeader.OrderTotal += cart.Price * cart.Count;
            }

            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                ShoppingCartVM.OrderHeader.PaymentStatus = Payment.StatusPending;
                ShoppingCartVM.OrderHeader.OrderStatus = Status.Pending;
            }
            else
            {
                ShoppingCartVM.OrderHeader.PaymentStatus = Payment.StatusDelayedPayment;
                ShoppingCartVM.OrderHeader.OrderStatus = Status.Approved;
            }

            _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfWork.Save();

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }

            // Payment logic (unchanged)
            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                var domain = $"{Request.Scheme}://{Request.Host.Value}/";
                var options = new SessionCreateOptions
                {
                    SuccessUrl = $"{domain}customer/cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
                    CancelUrl = $"{domain}customer/cart/index",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                };

                foreach (var item in ShoppingCartVM.ShoppingCartList)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions { Name = item.Product.Name },
                            UnitAmount = (long)item.Price * 100,
                        },
                        Quantity = item.Count,
                    });
                }

                var service = new SessionService();
                Session session = service.Create(options);
                _unitOfWork.OrderHeader.UpdateStripePaymentID(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }

            return RedirectToAction(nameof(OrderConfirmation), new { id = ShoppingCartVM.OrderHeader.Id });
        }

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == id, includeProperties: "ApplicationUser");
            if (orderHeader.PaymentStatus != Payment.StatusDelayedPayment)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unitOfWork.OrderHeader.UpdateStripePaymentID(id, session.Id, session.PaymentIntentId);
                    _unitOfWork.OrderHeader.UpdateStatus(id, Status.Approved, Payment.StatusApproved);
                    _unitOfWork.Save();
                }
                HttpContext.Session.Clear();

            }
            _emailSender.SendEmailAsync(orderHeader.ApplicationUser.Email, $"Order {orderHeader.Id} Created", "Order has been submitted successfully");

            List<ShoppingCart> shoppingCartList = _unitOfWork.ShoppingCart.
                GetAll(u => u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();

            _unitOfWork.ShoppingCart.RemoveRange(shoppingCartList);
            _unitOfWork.Save();

            return View(id);
        }

        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId, tracked: true);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();

            // Get current user information
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            // Calculate total item count in the cart for the current user
            int totalItemCount = _unitOfWork.ShoppingCart
                .GetAll(u => u.ApplicationUserId == userId)
                .Sum(item => item.Count); // Sum the count of each product

            // Update session with the accurate total item count
            HttpContext.Session.SetInt32(Constants.CartSession, totalItemCount);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId, tracked: true);
            if (cartFromDb.Count <= 1)
            {
                //remove that from cart
                HttpContext.Session.SetInt32(Constants.CartSession,
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).ToList().Count - 1);
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
                _unitOfWork.Save();
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
                _unitOfWork.Save();
                // Get current user information
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                // Calculate total item count in the cart for the current user
                int totalItemCount = _unitOfWork.ShoppingCart
                    .GetAll(u => u.ApplicationUserId == userId)
                    .Sum(item => item.Count); // Sum the count of each product

                // Update session with the accurate total item count
                HttpContext.Session.SetInt32(Constants.CartSession, totalItemCount);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId, tracked: true);

            if (cartFromDb != null)
            {
                // Remove product from cart
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
                _unitOfWork.Save();

                // Recalculate total item count in the cart for the current user
                var userId = cartFromDb.ApplicationUserId;
                int totalItemCount = _unitOfWork.ShoppingCart
                    .GetAll(u => u.ApplicationUserId == userId)
                    .Sum(item => item.Count); // Sum the count of each product

                // Update session with the accurate total item count
                HttpContext.Session.SetInt32(Constants.CartSession, totalItemCount);
            }
            else
            {
                TempData["error"] = "Item not found.";
            }

            TempData["success"] = "Item removed successfully.";

            // Redirect to Index to display remaining products in the cart
            return RedirectToAction(nameof(Index));
        }

        private double CalculateOrderTotal(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Product.DiscountPrice < shoppingCart.Product.ListPrice)
            {
                // If there is a discount price, use DiscountPrice
                return shoppingCart.Product.DiscountPrice;
            }
            else
            {
                // If there is no discount, use ListPrice
                return shoppingCart.Product.ListPrice;
            }
        }

    }
}
