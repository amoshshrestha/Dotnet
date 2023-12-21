using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;
using Utils;
using Webapp.data.Repository.IRepository;
using Webapp.Models;

namespace Webapp.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles =StaticData.ROLE_CUSTOMER)]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _db;
        public ShoppingCartController(IUnitOfWork db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingViewModel shoppingCartVm = new()
            {
                shoppingCarts = _db.ShoppingCart.GetAll(u => u.UserID == userId,"Product").ToList(),
            };
            double total = 0;
            foreach(var item in shoppingCartVm.shoppingCarts)
            {
                total += item.Quantity * item.Product.Price;

            }
            return View(shoppingCartVm);
        }

        [HttpPost]
        public IActionResult AddToCart(ShoppingCart shoppingCart)
        {
            var claimsIdentity =(ClaimsIdentity)User.Identity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCart.UserID= userId;
            var shoppingCartFromDb = _db.ShoppingCart.FirstOrDefault(u => u.productID == shoppingCart.productID && u.UserID == userId);
            if (shoppingCartFromDb != null)
            {
                shoppingCartFromDb.Quantity += shoppingCart.Quantity;
                _db.save();
                TempData["success"] = "Quantity increased in Cart.";
            }
            else
            {

                _db.ShoppingCart.Create(shoppingCart);
                _db.save();
                TempData["success"] = "Item Added to Cart";
            }


            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Decrease(int productId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var shoppingCart= _db.ShoppingCart.FirstOrDefault(u=>u.UserID == userId&& u.productID==productId);
            if(shoppingCart.Quantity == 1) 
            {
                _db.ShoppingCart.Delete(shoppingCart);
                _db.save();
                TempData["success"] = "Items removed";

            }
            else
            {
                shoppingCart.Quantity--;
                _db.ShoppingCart.Update(shoppingCart);
                _db.save();
                TempData["success"] = "Quantity Decreased";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Increase(int productId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var shoppingCart = _db.ShoppingCart.FirstOrDefault(u => u.UserID == userId && u.productID == productId);
            if (shoppingCart.Quantity == shoppingCart.Product.Stock)
            {
                TempData["error"] = "Items Quantity reached maximum";
                

            }
            else
            {
                shoppingCart.Quantity++;
                /*_db.ShoppingCart.Update(shoppingCart);*/
                _db.save();
                TempData["success"] = "Quantity Increased";
            }
            return RedirectToAction("Index");
        }
    }
}
