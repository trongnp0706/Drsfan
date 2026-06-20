using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
using Microsoft.AspNetCore.Mvc;

namespace DrsfanWebApp.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var objCompanyList = _unitOfWork.Company.GetAll().ToList();
                return Ok(new { success = true, data = objCompanyList });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var company = _unitOfWork.Company.Get(u => u.Id == id);
                if (company == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy công ty" });
                }
                return Ok(new { success = true, data = company });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
