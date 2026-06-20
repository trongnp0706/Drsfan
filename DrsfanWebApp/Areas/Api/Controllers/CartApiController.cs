using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
using Drsfan.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrsfanWebApp.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartFromDb = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product,Product.ProductImages").ToList();
            
            // Set price (assuming Product has Price or Price50, etc. For simplicity we use Price)
            foreach(var item in cartFromDb)
            {
                item.Price = item.Product.DiscountPrice > 0 ? item.Product.DiscountPrice : item.Product.ListPrice;
            }

            return Ok(new { success = true, data = cartFromDb });
        }

        [HttpPost("add")]
        public IActionResult AddToCart([FromBody] CartItemDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == model.ProductId);

            if (cartFromDb == null)
            {
                ShoppingCart cartObj = new ShoppingCart
                {
                    ApplicationUserId = userId,
                    ProductId = model.ProductId,
                    Count = model.Count
                };
                _unitOfWork.ShoppingCart.Add(cartObj);
            }
            else
            {
                cartFromDb.Count += model.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }

            _unitOfWork.Save();

            // Return cart count
            var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).ToList().Count;
            return Ok(new { success = true, message = "Thêm vào giỏ hàng thành công", cartCount = count });
        }

        [HttpDelete("remove/{cartId}")]
        public IActionResult RemoveFromCart(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if(cart != null)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
                _unitOfWork.Save();
                return Ok(new { success = true });
            }
            return NotFound();
        }
    }

    public class CartItemDto
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
