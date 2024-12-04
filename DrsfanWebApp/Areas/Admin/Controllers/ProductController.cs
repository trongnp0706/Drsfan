using Drsfan.DataAccess.EntityBaseRepository.IEntityBaseRepository;
using Drsfan.Models;
using Drsfan.Models.ViewModels;
using Drsfan.Utility;
using Drsfan.Utility.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DrsfanBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> ojbProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return View(ojbProductList);
        }
        public IActionResult Upsert(int? id)
        {

            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()

            };
            if (id == null || id == 0)
            {
                //Create
                return View(productVM);
            }
            else
            {
                //Update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "ProductImages");
                return View(productVM);
            }

        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, List<IFormFile> files)
        {
            
            if (ModelState.IsValid)
            {
                // If the product ID is 0, add a new product
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    // Otherwise, update the existing product
                    _unitOfWork.Product.Update(productVM.Product);
                }

                // Get the root path of the web host environment
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        // Generate a unique file name using a GUID and the file extension
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        // Construct the product path using the product ID
                        string productPath = @"images\products\product-" + productVM.Product.Id;
                        // Combine the root path and product path to get the end path
                        string endPath = Path.Combine(wwwRootPath, productPath);

                        // Check if the directory exists, if not, create it
                        if (!Directory.Exists(endPath))
                        {
                            Directory.CreateDirectory(endPath);
                        }

                        // Create a file stream to save the file to the end path
                        using (var fileStream = new FileStream(Path.Combine(endPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        // Create a new ProductImage object and set its properties
                        ProductImage productImage = new ProductImage()
                        {
                            ImageUrl = @"\" + productPath + @"\" + fileName,
                            ProductId = productVM.Product.Id
                        };

                        // Initialize the ProductImages list if it is null
                        if (productVM.Product.ProductImages == null)
                        {
                            productVM.Product.ProductImages = new List<ProductImage>();
                        }
                        // Add the new product image to the ProductImages list
                        productVM.Product.ProductImages.Add(productImage);
                    }

                    // Update the product with the new images and save changes
                    _unitOfWork.Product.Update(productVM.Product);
                    _unitOfWork.Save();
                }

                // Save the product changes
                _unitOfWork.Save();
                // Set a success message in TempData
                TempData["success"] = "Product created/update successfully";
                // Redirect to the Index action
                return RedirectToAction("Index");
            }
            else
            {
                // If the model state is not valid, reload the category list and return the view
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }

        // Action method to delete an image
        public IActionResult DeleteImage(int imageId)
        {
            // Retrieve the image to be deleted using the imageId
            var imageToBeDeleted = _unitOfWork.ProductImage.Get(u => u.Id == imageId);
            // Get the productId associated with the image
            int productId = imageToBeDeleted.ProductId;

            // Check if the image exists
            if (imageToBeDeleted != null)
            {
                // Check if the image URL is not empty
                if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
                {
                    // Construct the full path of the image to be deleted
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageToBeDeleted.ImageUrl.TrimStart('\\'));
                    // Check if the file exists at the specified path
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        // Delete the file from the file system
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                // Remove the image record from the database
                _unitOfWork.ProductImage.Remove(imageToBeDeleted);
                _unitOfWork.Save();
                TempData["success"] = "Deleted Image Successfully";
            }
            return RedirectToAction(nameof(Upsert), new { id = productId });
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            // Construct the path to the product's image directory
            string productPath = @"images\products\product-" + id;
            string endPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);

            // Check if the directory exists
            if (Directory.Exists(endPath))
            {
                // Get all file paths in the directory
                string[] filePaths = Directory.GetFiles(endPath);

                // Delete each file in the directory
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }

                // Delete the directory itself
                Directory.Delete(endPath);
            }

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Product Successful" });
        }

        #endregion    }
    }
}
