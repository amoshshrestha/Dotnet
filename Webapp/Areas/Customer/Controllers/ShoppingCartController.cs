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
            ShoppingViewModel shoppingCart = new()
            {
                shoppingCarts = _db.ShoppingCart.GetAll(u => u.UserID == userId)
            };
            return View(shoppingCart);
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
    }
}
