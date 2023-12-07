using Microsoft.AspNetCore.Mvc;
using Webapp.data;
using Webapp.Models;

namespace Webapp.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<CategoryModel> categories = _db.Categories;
            return View(categories);
        }
    }
}
