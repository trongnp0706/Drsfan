using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
using Drsfan.Models;
using Drsfan.Models.ViewModels;
using Drsfan.Utility;
using Drsfan.Utility.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Index(string searchString, string category)
        {
            // Lấy danh sách danh mục sản phẩm
            IEnumerable<Category> categories = _unitOfWork.Category.GetAll();

            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,ProductImages");

            // Lọc sản phẩm theo tên và danh mục
            if (!string.IsNullOrEmpty(searchString))
            {
                productList = productList.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                                     p.Brand.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                                     p.ModelNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(category))
            {
                productList = productList.Where(p => p.Category.Name.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            // Sử dụng ViewModel để truyền dữ liệu
            var viewModel = new ProductVM
            {
                Products = productList,
                Categories = categories
            };

            return View(viewModel);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category,ProductImages"),
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
            HttpContext.Session.SetInt32(Constants.CartSession, totalItemsInCart);

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