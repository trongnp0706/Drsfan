using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
using Drsfan.Models;
using Drsfan.Models.ViewModels;
using Drsfan.Utility;
using Drsfan.Utility.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DrsfanWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var companyList = _unitOfWork.Company.GetAll().ToList();
            return View(companyList);
        }

        // Action method to display the create/update form
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                // Create
                return View(new Company());
            }
            else
            {
                // Update
                var company = _unitOfWork.Company.Get(u => u.Id == id);
                return View(company);
            }
        }

        // Action method to handle the form submission for create/update
        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);
                    TempData["success"] = "Company created successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                    TempData["success"] = "Company updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        #region API CALLS

        // API call to get all companies
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = companyList });
        }

        // API call to delete a company
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var company = _unitOfWork.Company.Get(u => u.Id == id);
            if (company == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(company);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
