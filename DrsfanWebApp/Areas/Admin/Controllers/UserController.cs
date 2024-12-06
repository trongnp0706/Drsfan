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
            // Get user information and related company
            var applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId, includeProperties: "Company");
            // Get list of roles
            var roleList = _roleManager.Roles.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Name
            });
            // Get list of companies
            var companyList = _unitOfWork.Company.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            // Create ViewModel for role management
            var roleVM = new RoleManagementVM
            {
                ApplicationUser = applicationUser,
                RoleList = roleList,
                CompanyList = companyList
            };

            // Get current role of the user
            roleVM.ApplicationUser.Role = _userManager.GetRolesAsync(applicationUser).GetAwaiter().GetResult().FirstOrDefault();
            return View(roleVM);
        }

        [HttpPost]
        public IActionResult RoleManagement(RoleManagementVM roleManagementVM)
        {
            // Get user information from ViewModel
            var applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == roleManagementVM.ApplicationUser.Id);
            // Get old role of the user
            var oldRole = _userManager.GetRolesAsync(applicationUser).GetAwaiter().GetResult().FirstOrDefault();

            // If new role is different from old role
            if (roleManagementVM.ApplicationUser.Role != oldRole)
            {
                // If new role is Company, update CompanyId
                if (roleManagementVM.ApplicationUser.Role == UserRoles.Company)
                {
                    applicationUser.CompanyId = roleManagementVM.ApplicationUser.CompanyId;
                }
                // If old role is Company, remove CompanyId
                if (oldRole == UserRoles.Company)
                {
                    applicationUser.CompanyId = null;
                }

                // Update user information
                _unitOfWork.ApplicationUser.Update(applicationUser);
                _unitOfWork.Save();

                // Remove old role and add new role
                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagementVM.ApplicationUser.Role).GetAwaiter().GetResult();
            }
            // If old role is Company and CompanyId changes, update CompanyId
            else if (oldRole == UserRoles.Company && applicationUser.CompanyId != roleManagementVM.ApplicationUser.CompanyId)
            {
                applicationUser.CompanyId = roleManagementVM.ApplicationUser.CompanyId;
                _unitOfWork.ApplicationUser.Update(applicationUser);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            // Get list of all users and related companies
            var userList = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company").ToList();

            // Get role of each user and update company information if needed
            foreach (var user in userList)
            {
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
                if (user.Company == null)
                {
                    user.Company = new Company { Name = "" };
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

            // Get user information by ID
            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == id);

            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            // Update lock/unlock status of the user
            user.LockoutEnd = user.LockoutEnd != null && user.LockoutEnd > DateTime.Now
                ? DateTime.Now
                : DateTime.Now.AddYears(100);

            _unitOfWork.ApplicationUser.Update(user);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Status updated successfully" });
        }

        #endregion
    }
}