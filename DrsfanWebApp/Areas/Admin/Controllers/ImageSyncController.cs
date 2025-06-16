using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
using Drsfan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Drsfan.Utility.Static;

namespace DrsfanWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public class ImageSyncController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageSyncController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var syncResult = new List<string>();
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var productsPath = Path.Combine(wwwRootPath, "images", "products");

            // Get all product directories
            var productDirs = Directory.GetDirectories(productsPath);
            foreach (var dir in productDirs)
            {
                var productId = int.Parse(Path.GetFileName(dir).Split('-')[1]);
                var product = _unitOfWork.Product.Get(p => p.Id == productId);

                if (product == null)
                {
                    // Product doesn't exist in database, delete directory
                    Directory.Delete(dir, true);
                    syncResult.Add($"Deleted directory {dir} - Product {productId} not found in database");
                    continue;
                }

                // Get all images in directory
                var files = Directory.GetFiles(dir);
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var imageUrl = $@"\images\products\product-{productId}\{fileName}";
                    var image = _unitOfWork.ProductImage.Get(i => i.ImageUrl == imageUrl);

                    if (image == null)
                    {
                        // Image doesn't exist in database, add it
                        var productImage = new ProductImage
                        {
                            ImageUrl = imageUrl,
                            ProductId = productId
                        };
                        _unitOfWork.ProductImage.Add(productImage);
                        syncResult.Add($"Added image {fileName} to database for product {productId}");
                    }
                }

                // Get all images in database for this product
                var dbImages = _unitOfWork.ProductImage.GetAll(i => i.ProductId == productId);
                foreach (var dbImage in dbImages)
                {
                    var imagePath = Path.Combine(wwwRootPath, dbImage.ImageUrl.TrimStart('\\'));
                    if (!System.IO.File.Exists(imagePath))
                    {
                        // Image doesn't exist in file system, remove from database
                        _unitOfWork.ProductImage.Remove(dbImage);
                        syncResult.Add($"Removed image {dbImage.ImageUrl} from database - File not found");
                    }
                }
            }

            _unitOfWork.Save();
            ViewBag.SyncResult = syncResult;
            return View();
        }
    }
} 