using DrsfanBook.DataAcess.Repository.IRepository;
using DrsfanBook.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrsfanBookWeb.Areas.ViewComponents
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
                if (HttpContext.Session.GetInt32(SD.SSShoppingCart) == null)
                {
                    HttpContext.Session.SetInt32(SD.SSShoppingCart,
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
                }

                return View(HttpContext.Session.GetInt32(SD.SSShoppingCart));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
