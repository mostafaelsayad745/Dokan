using Dokan.Models.Models;
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
            ProductVM productVM = new ProductVM
            {
                Product = new Product(),
                CategoryList = categoryList
            };

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVM productVM, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                await UploadProductImage(productVM, file);

                await _unitOfWork.Products.AddAsync(productVM.Product!);
                await _unitOfWork.Complete();
                TempData["Create"] = "Product Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }

        private async Task UploadProductImage(ProductVM productVM, IFormFile file)
        {
            // upload image file to Products images folder
            string RootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString();
                var uploads = Path.Combine(RootPath, @"Images\Products");
                var extension = Path.GetExtension(file.FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                productVM.Product!.Img = @"Images\Products\" + filename + extension;

            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await GetProductByIdAsync(id);
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var categoryList = categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ProductVM productVM = new ProductVM
            {
                Product = product,
                CategoryList = categoryList
            };

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM productVM , IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                await EditImage(productVM, file);
                await _unitOfWork.Products.UpdateProductAsync(productVM.Product!);
                await _unitOfWork.Complete();
                TempData["Edit"] = "Product Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }

        private async Task EditImage(ProductVM productVM, IFormFile? file)
        {
            string RootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString();
                var uploads = Path.Combine(RootPath, @"Images\Products");
                var extension = Path.GetExtension(file.FileName);

                // delete old image file if exists
                if (productVM.Product.Img != null)
                {
                    var oldImage = Path.Combine(RootPath, productVM.Product.Img.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                productVM.Product!.Img = @"Images\Products\" + filename + extension;

            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _unitOfWork.Products.Remove(product);
            var RootPath = _webHostEnvironment.WebRootPath;
            var oldImage = Path.Combine(RootPath, product.Img.TrimStart('\\'));
            if (System.IO.File.Exists(oldImage))
            {
                System.IO.File.Delete(oldImage);
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
