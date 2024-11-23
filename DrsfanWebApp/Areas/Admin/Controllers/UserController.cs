using Drsfan.DataAcess.Data;
using Drsfan.Models;
using Drsfan.Utility.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrsfanWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public class UserController : Controller
    {
        private readonly DrsfanDbContext _db;
        public UserController(DrsfanDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            // Lấy danh sách người dùng cùng thông tin công ty và vai trò
            List<ApplicationUser> userList = _db.ApplicationUsers.Include(u => u.Company).ToList();

            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in userList)
            {
                var roleId = _db.UserRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
                if (user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
            }
            return Json(new { data = userList });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "Invalid User ID" });
            }

            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            // Kiểm tra trạng thái Lock/Unlock
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                // User đang bị khóa, mở khóa họ
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                // Khóa user đến 100 năm sau
                user.LockoutEnd = DateTime.Now.AddYears(100);
            }

            _db.SaveChanges();
            return Json(new { success = true, message = "Status updated successfully" });
        }

        #endregion
    }
}
