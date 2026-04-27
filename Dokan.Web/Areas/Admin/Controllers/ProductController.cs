using Dokan.Models.Models.ProductCatalog;
using Dokan.Models.Repositories;
using Dokan.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dokan.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.Products.GetAllAsync(includeWords: "Category");
            return View(products);
        }

       


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var categoryList = categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            var brands = await _unitOfWork.Brands.GetAllAsync();
            var brandList = brands.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            ProductVM productVM = new ProductVM
            {
                Product = new Product(),
                CategoryList = categoryList,
                BrandList = brandList
            };

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVM productVM, IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                await UploadProductImages(productVM, files);

                await _unitOfWork.Products.AddAsync(productVM.Product!);
                await _unitOfWork.Complete();
                TempData["Create"] = "Product Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }

        private async Task UploadProductImages(ProductVM productVM, IFormFile[] files)
        {
            string RootPath = _webHostEnvironment.WebRootPath;
            var uploads = Path.Combine(RootPath, @"Images\Products");

            // Create directory if it doesn't exist
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            bool isFirstImage = true;

            if (files != null && files.Length > 0)
            {
                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        string filename = Guid.NewGuid().ToString();
                        var extension = Path.GetExtension(file.FileName);

                        using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        var productImage = new ProductImage
                        {
                            ImgUrl = @"Images\Products\" + filename + extension,
                            IsMain = isFirstImage,
                            ProductId = productVM.Product!.Id
                        };

                        productVM.Product.ProductImages.Add(productImage);
                        isFirstImage = false;
                    }
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _unitOfWork.Products.GetFristOrDefaultAsync(p => p.Id == id, includeWords: "ProductImages");
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var categoryList = categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            var brands = await _unitOfWork.Brands.GetAllAsync();
            var brandList = brands.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            ProductVM productVM = new ProductVM
            {
                Product = product,
                CategoryList = categoryList,
                BrandList = brandList
            };

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM productVM, IFormFile[] newFiles, string[] imagesToDelete)
        {
            if (ModelState.IsValid)
            {
                await EditImages(productVM, newFiles, imagesToDelete);
                await _unitOfWork.Products.UpdateProductAsync(productVM.Product!);
                await _unitOfWork.Complete();
                TempData["Edit"] = "Product Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }

        private async Task EditImages(ProductVM productVM, IFormFile[] newFiles, string[] imagesToDelete)
        {
            string RootPath = _webHostEnvironment.WebRootPath;
            var uploads = Path.Combine(RootPath, @"Images\Products");

            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            // Delete selected images
            if (imagesToDelete != null && imagesToDelete.Length > 0)
            {
                foreach (var imageId in imagesToDelete)
                {
                    if (Guid.TryParse(imageId, out var id))
                    {
                        var imageToDelete = productVM.Product!.ProductImages.FirstOrDefault(i => i.Id == id);
                        if (imageToDelete != null)
                        {
                            var filePath = Path.Combine(RootPath, imageToDelete.ImgUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                            productVM.Product.ProductImages.Remove(imageToDelete);
                        }
                    }
                }
            }

            // Add new images
            if (newFiles != null && newFiles.Length > 0)
            {
                foreach (var file in newFiles)
                {
                    if (file != null && file.Length > 0)
                    {
                        string filename = Guid.NewGuid().ToString();
                        var extension = Path.GetExtension(file.FileName);

                        using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        var productImage = new ProductImage
                        {
                            ImgUrl = @"Images\Products\" + filename + extension,
                            IsMain = !productVM.Product!.ProductImages.Any(i => i.IsMain),
                            ProductId = productVM.Product.Id
                        };

                        productVM.Product.ProductImages.Add(productImage);
                    }
                }
            }
        }



        [HttpGet]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _unitOfWork.Products.Remove(product);
            var RootPath = _webHostEnvironment.WebRootPath;

            // Delete all product images
            foreach (var image in product.ProductImages)
            {
                var oldImage = Path.Combine(RootPath, image.ImgUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }
            }

            await _unitOfWork.Complete();
            return Json(new { success = true, message = "Delete Successful" });
        }

        private async Task<Product?> GetProductByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }
            return await _unitOfWork.Products.GetFristOrDefaultAsync(p => p.Id == id);
        }
    }
}
