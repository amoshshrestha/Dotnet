using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utils;
using Webapp.data.Repository.IRepository;
using Webapp.Models;

namespace Webapp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticData.ROLE_ADMIN)]

    public class ProductController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Product.GetAll(includeProperties: "Category");
            return View(products);
        }
        [HttpGet]
        public IActionResult UpsertProduct(int? id)
        {
            ProductViewModel productviewmodel;
            IEnumerable<SelectListItem> categoryList = _db.Category.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryId.ToString()
                });
            if (id == 0 || id == null)
            {
                productviewmodel = new ProductViewModel()
                {

                    product = new Product(),
                    categoryList = categoryList
                };
            }
            else
            {
                productviewmodel = new ProductViewModel()
                {
                    product = _db.Product.FirstOrDefault(u => u.Id == id),
                    categoryList = categoryList
                };
            }
            return View(productviewmodel);
        }
        [HttpPost]
        public IActionResult UpsertProduct(ProductViewModel productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                    string filepath = Path.Combine(wwwRoot, @"Images\Products", filename);
                    if (!string.IsNullOrEmpty(productVM.product.ImageUrl))
                    {

                        var oldFilePath = Path.Combine(wwwRoot, productVM.product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    using (var filestream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    productVM.product.ImageUrl = @"\Images\Products\" + filename;

                }
                if (productVM.product.Id == 0)
                {
                    _db.Product.Create(productVM.product);
                    _db.save();
                    TempData["success"] = "Product Added Succesfully";
                }
                else
                {
                    _db.Product.Update(productVM.product);
                    _db.save();
                    TempData["success"] = "Product Updated Succesfully";

                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(productVM);
            }
        }
        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            /*CategoryModel categoryObj = _db.Categories.Find(id);*/
            Product productObj = _db.Product.FirstOrDefault(u => u.Id == id);
            return View(productObj);
        }
        [HttpPost]
        [ActionName("DeleteProduct")]
        public IActionResult Delete_Product(int id)
        {
            Product productObj = _db.Product.FirstOrDefault(u => u.Id == id);
            if (id == 0)
            {
                return NotFound();

            }
            else
            {
                _db.Product.Delete(productObj);
                _db.save();
                TempData["Success"] = "Product Removed successfully";
                return RedirectToAction("Index");
            }


        }
    }
}
