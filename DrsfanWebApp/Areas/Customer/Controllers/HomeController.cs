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

        // Action method to display the list of products
        public IActionResult Index(string searchString, string category)
        {
            // Get all categories
            var categories = _unitOfWork.Category.GetAll();
            // Get all products with related Category and ProductImages
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,ProductImages");

            // Filter products by search string
            if (!string.IsNullOrEmpty(searchString))
            {
                productList = productList.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                                     p.Brand.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                                     p.ModelNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            // Filter products by category
            if (!string.IsNullOrEmpty(category))
            {
                productList = productList.Where(p => p.Category.Name.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            // Create a ViewModel to pass data to the view
            var viewModel = new ProductListVM
            {
                Products = productList,
                Categories = categories
            };

            return View(viewModel);
        }

        // Action method to display product details
        public IActionResult Details(int productId)
        {
            // Create a new ShoppingCart object with the selected product
            var cart = new ShoppingCart
            {
                Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category,ProductImages"),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }

        // Action method to handle adding product to the shopping cart
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            // Get the current user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = userId;

            // Check if the product is already in the user's shopping cart
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == shoppingCart.ProductId);

            if (cartFromDb != null)
            {
                // If the product is already in the cart, update the quantity
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            else
            {
                // If the product is not in the cart, add it to the cart
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }
            _unitOfWork.Save();

            // Update the total number of items in the cart session
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