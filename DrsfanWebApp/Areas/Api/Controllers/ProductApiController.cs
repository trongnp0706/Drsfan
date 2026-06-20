using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
using Microsoft.AspNetCore.Mvc;

namespace DrsfanWebApp.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category,ProductImages").ToList();
                return Ok(new { success = true, data = objProductList });
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
                var product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "Category,ProductImages");
                if (product == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy sản phẩm" });
                }
                return Ok(new { success = true, data = product });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
