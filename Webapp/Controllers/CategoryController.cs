using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Webapp.data;
using Webapp.data.Repository.IRepository;
using Webapp.Models;

namespace Webapp.Controllers
{
    public class CategoryController : Controller
    {
        /* private readonly ApplicationDbContext _db;*/
        private readonly ICategoryRepository _db;
        /*public CategoryController(ApplicationDbContext db)*/

        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            /*IEnumerable<CategoryModel> categories = _db.Categories;*/
            IEnumerable<CategoryModel> categories = _db.GetAll();

            return View(categories);
        }
        [HttpGet]
        public IActionResult PostCategory()
        {
            return View();
        }
        public IActionResult PostCategory(CategoryModel categoryObj)
        {
            if (ModelState.IsValid)
            {
                if(categoryObj.CategoryName.ToLower() == "test")
                {
                    ModelState.AddModelError("Category","Test is not a valid category");
                    return View(categoryObj);
                }
                /*_db.Categories.Add(categoryObj);
                _db.SaveChanges();*/
                _db.Create(categoryObj);
                _db.save();
                TempData["success"] = "Category Added Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(categoryObj);
            }
        }
        [HttpGet]
        public IActionResult UpdateCategory( int id) 
        {
            /*CategoryModel categoryObj =_db.Categories.Find(id);*/
            CategoryModel categoryObj = _db.FirstOrDefault(u=>u.CategoryId==id);

            return View(categoryObj);
        }
        [HttpPost]
        public IActionResult UpdateCategory(CategoryModel categoryObj)
        {
            if (ModelState.IsValid)
            {
                /*_db.Categories.Update(categoryObj);
                _db.SaveChanges();*/
                _db.Update(categoryObj);
                _db.save();
                TempData["Success"] = "Category Updated successfully";
                return RedirectToAction("Index");

            }
            else
            {
                return View(categoryObj) ;
            }
        }
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            /*CategoryModel categoryObj = _db.Categories.Find(id);*/
            CategoryModel categoryObj = _db.FirstOrDefault(u=>u.CategoryId==id);
            return View(categoryObj);
        }
        [HttpPost]
        [ActionName("DeleteCategory")]
        public IActionResult Delete_Category(int id)
        {
            CategoryModel categoryObj = _db.FirstOrDefault(u => u.CategoryId == id);
            if (id == 0)
            {
                return NotFound();
                
            }
            else
            {
                _db.Delete(categoryObj);
                _db.save();
                TempData["Success"] = "Category Removed successfully";
                return RedirectToAction("Index");
            }
            
          
        }
    }
}
