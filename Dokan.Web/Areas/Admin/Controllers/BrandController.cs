using Dokan.DataAccess.Data;
using Dokan.Models.Models.ProductCatalog;
using Dokan.Models.Repositories;
using Dokan.Models.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Dokan.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandController(IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Brands.GetAllAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BrandDetails(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return NotFound();
            }
            var BrandFromDb = await _unitOfWork.Brands.GetFristOrDefaultAsync(c => c.Id == Id);
            if (BrandFromDb == null)
            {
                return NotFound();
            }
            return View(BrandFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brand brand, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                await UploadBrandLogo(brand, file);

                await _unitOfWork.Brands.AddAsync(brand);
                await _unitOfWork.Complete();
                TempData["Create"] = "Brand Created Successfully";
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        private async Task UploadBrandLogo(Brand brand, IFormFile file)
        {
            string RootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString();
                var uploads = Path.Combine(RootPath, @"Images\Brands");
                var extension = Path.GetExtension(file.FileName);

                // Create directory if it doesn't exist
                Directory.CreateDirectory(uploads);

                using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                brand.LogoUrl = @"Images\Brands\" + filename + extension;
            }
        }


        // Edit 

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return NotFound();
            }
            var BrandFromDb = await _unitOfWork.Brands.GetFristOrDefaultAsync(c => c.Id == Id);
            if (BrandFromDb == null)
            {
                return NotFound();
            }
            return View(BrandFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Brand brand , IFormFile file)
        {
            if (ModelState.IsValid)
            {
                await EditBrandLogo(brand, file);
                await _unitOfWork.Brands.UpdateBrandAsync(brand);
                await _unitOfWork.Complete();
                TempData["Edit"] = "Brand Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        private async Task EditBrandLogo(Brand brand, IFormFile file)
        {
            string RootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString();
                var uploads = Path.Combine(RootPath, @"Images\Brands");
                var extension = Path.GetExtension(file.FileName);

                if (brand.LogoUrl != null)
                {
                    var oldImage = Path.Combine(RootPath, brand.LogoUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                brand.LogoUrl = @"Images\Brands\" + filename + extension;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            var brand = await GetBrandByIdAsync(id);
            if (brand == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _unitOfWork.Brands.Remove(brand);
            var RootPath = _webHostEnvironment.WebRootPath;
           
                var oldImage = Path.Combine(RootPath, brand.LogoUrl!.TrimStart('\\'));
                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }
          
            await _unitOfWork.Complete();
            return Json(new { success = true, message = "Delete Successful" });
        }

        private async Task<Brand?> GetBrandByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }
            return await _unitOfWork.Brands.GetFristOrDefaultAsync(p => p.Id == id);
        }

    }
}
