using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
using Drsfan.Utility;
using Drsfan.Utility.Static;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrsfanWebApp.Areas.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Lấy thông tin đăng nhập của người dùng hiện tại
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                if (HttpContext.Session.GetInt32(Constants.CartSession) == null)
                {
                    var cartItems = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value);
                    var totalQuantity = cartItems.Sum(item => item.Count);
                    HttpContext.Session.SetInt32(Constants.CartSession, totalQuantity);
                }

                return View(HttpContext.Session.GetInt32(Constants.CartSession));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
