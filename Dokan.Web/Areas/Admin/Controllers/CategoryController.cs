using Dokan.DataAccess.Data;
using Dokan.Models.Models;
using Dokan.Models.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Dokan.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Categories.GetAllAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Categories.AddAsync(category);
                await _unitOfWork.Complete();
                TempData["Create"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }


        // Edit 

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return NotFound();
            }
            var CategoryFromDb = await _unitOfWork.Categories.GetFristOrDefaultAsync(c => c.Id == Id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Categories.UpdateCategoryAsync(category);
                await _unitOfWork.Complete();
                TempData["Edit"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return NotFound();
            }
            var CategoryFromDb = await _unitOfWork.Categories.GetFristOrDefaultAsync(c => c.Id == Id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(Guid Id)
        {
            var categoryFromDb = await _unitOfWork.Categories.GetFristOrDefaultAsync(c => c.Id == Id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Categories.Remove(categoryFromDb);
            await _unitOfWork.Complete();
            TempData["Delete"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}
