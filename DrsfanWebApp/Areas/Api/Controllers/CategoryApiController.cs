using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
using Microsoft.AspNetCore.Mvc;

namespace DrsfanWebApp.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var objCategoryList = _unitOfWork.Category.GetAll().ToList();
                return Ok(new { success = true, data = objCategoryList });
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
                var category = _unitOfWork.Category.Get(u => u.Id == id);
                if (category == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy danh mục" });
                }
                return Ok(new { success = true, data = category });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
