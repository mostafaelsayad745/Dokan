using Dokan.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dokan.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _unitOfWork.Products.GetFristOrDefaultAsync(p => p.Id == id , includeWords:"Category,Brand,ProductImages");
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
