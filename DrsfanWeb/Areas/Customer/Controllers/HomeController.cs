using DrsfanBook.DataAcess.Repository.IRepository;
using DrsfanBook.Models;
using DrsfanBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;


namespace DrsfanBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            // Lấy thông tin đăng nhập của người dùng hiện tại
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            // Kiểm tra nếu người dùng đã đăng nhập
            if (claim != null)
            {
                // Lấy tổng số sản phẩm trong giỏ hàng của người dùng
                var totalItemsInCart = _unitOfWork.ShoppingCart
                    .GetAll(u => u.ApplicationUserId == claim.Value)
                    .Sum(c => c.Count); // Đếm tổng số lượng các sản phẩm

                // Cập nhật tổng số sản phẩm vào session
                HttpContext.Session.SetInt32(SD.SSShoppingCart, totalItemsInCart);
            }

            // Lấy danh sách sản phẩm để hiển thị
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(productList);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category"),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            // Kiểm tra xem giỏ hàng của người dùng đã có sản phẩm này chưa
            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId &&
            u.ProductId == shoppingCart.ProductId);

            if (cartFromDb != null)
            {
                // Nếu có sản phẩm trong giỏ hàng, cập nhật số lượng
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
                _unitOfWork.Save();
            }
            else
            {
                // Nếu không có, thêm mới sản phẩm vào giỏ
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
            }

            // Cập nhật số lượng giỏ hàng trong session
            var totalItemsInCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Sum(u => u.Count);
            HttpContext.Session.SetInt32(SD.SSShoppingCart, totalItemsInCart);

            TempData["success"] = "Update Cart successfully";

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
