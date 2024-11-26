using Drsfan.DataAccess.Data;
using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManagement(string userId)
        {
            // Create a new RoleManagementVM object
            RoleManagementVM RoleVM = new RoleManagementVM()
            {
                // Get the ApplicationUser by userId and include the Company property
                ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId, includeProperties: "Company"),
                // Get all roles and convert them to SelectListItem
                RoleList = _roleManager.Roles.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                }),
                // Get all companies and convert them to SelectListItem
                CompanyList = _unitOfWork.Company.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            // Get the role of the ApplicationUser
            RoleVM.ApplicationUser.Role = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser.Get(u => u.Id == userId))
                    .GetAwaiter().GetResult().FirstOrDefault();
            return View(RoleVM);
        }

        [HttpPost]
        public IActionResult RoleManagement(RoleManagementVM roleManagementVM)
        {
            // Get the old role of the ApplicationUser
            string oldRole = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser.Get(u => u.Id == roleManagementVM.ApplicationUser.Id))
                    .GetAwaiter().GetResult().FirstOrDefault();

            // Get the ApplicationUser by Id
            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == roleManagementVM.ApplicationUser.Id);

            // Check if the role has been updated
            if (!(roleManagementVM.ApplicationUser.Role == oldRole))
            {
                // If the new role is Company, update the CompanyId
                if (roleManagementVM.ApplicationUser.Role == UserRoles.Company)
                {
                    applicationUser.CompanyId = roleManagementVM.ApplicationUser.CompanyId;
                }
                // If the old role was Company, set the CompanyId to null
                if (oldRole == UserRoles.Company)
                {
                    applicationUser.CompanyId = null;
                }
                // Update the ApplicationUser
                _unitOfWork.ApplicationUser.Update(applicationUser);
                _unitOfWork.Save();

                // Remove the old role and add the new role
                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagementVM.ApplicationUser.Role).GetAwaiter().GetResult();
            }
            else
            {
                // If the role is Company and the CompanyId has changed, update the CompanyId
                if (oldRole == UserRoles.Company && applicationUser.CompanyId != roleManagementVM.ApplicationUser.CompanyId)
                {
                    applicationUser.CompanyId = roleManagementVM.ApplicationUser.CompanyId;
                    _unitOfWork.ApplicationUser.Update(applicationUser);
                    _unitOfWork.Save();
                }
            }

            return RedirectToAction("Index");
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            // Lấy danh sách người dùng cùng thông tin công ty và vai trò
            List<ApplicationUser> userList = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company").ToList();

            foreach (var user in userList)
            {
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
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

            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == id);

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

            _unitOfWork.ApplicationUser.Update(user);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Status updated successfully" });
        }

        #endregion
    }
}