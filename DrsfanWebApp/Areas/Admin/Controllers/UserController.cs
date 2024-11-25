using Drsfan.DataAcess.Data;
using Drsfan.Models;
using Drsfan.Models.ViewModels;
using Drsfan.Utility.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DrsfanWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public class UserController : Controller
    {
        private readonly DrsfanDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(DrsfanDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RoleManagement(string userId)
        {
            // Đợi kết quả trả về từ FirstOrDefaultAsync()
            string RoleID = (await _db.UserRoles.FirstOrDefaultAsync(u => u.UserId == userId))?.RoleId;

            // Đợi kết quả trả về từ FirstOrDefaultAsync()
            IdentityRole? role = await _db.Roles.FirstOrDefaultAsync(u => u.Id == RoleID);

            RoleManagementVM RoleVM = new RoleManagementVM()
            {
                ApplicationUser = await _db.ApplicationUsers.Include(u => u.Company).FirstOrDefaultAsync(u => u.Id == userId),
                RoleList = await _db.Roles.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                }).ToListAsync(),
                CompanyList = await _db.Companies.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }).ToListAsync(),
            };

            // Gán Role nếu role không phải null
            RoleVM.ApplicationUser.Role = role?.Name;
            return View(RoleVM);
        }

        [HttpPost]
        public async Task<IActionResult> RoleManagement(RoleManagementVM roleManagementVM)
        {
            string RoleID = (await _db.UserRoles.FirstOrDefaultAsync(u => u.UserId == roleManagementVM.ApplicationUser.Id))?.RoleId;
            string oldRole = (await _db.Roles.FirstOrDefaultAsync(u => u.Id == RoleID))?.Name;

            if (!(roleManagementVM.ApplicationUser.Role == oldRole))
            {
                // a role was change
                ApplicationUser applicationUser = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == roleManagementVM.ApplicationUser.Id);

                if (roleManagementVM.ApplicationUser.Role == UserRoles.Company)
                {
                    applicationUser.CompanyId = roleManagementVM.ApplicationUser.CompanyId;
                }
                if (oldRole == UserRoles.Company)
                {
                    applicationUser.CompanyId = null;
                }
                await _db.SaveChangesAsync();

                await _userManager.RemoveFromRoleAsync(applicationUser, oldRole);
                await _userManager.AddToRoleAsync(applicationUser, roleManagementVM.ApplicationUser.Role);
            }

            return RedirectToAction("Index");
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
