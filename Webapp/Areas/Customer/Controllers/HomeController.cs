using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Webapp.data.Repository.IRepository;
using Webapp.Models;

namespace Webapp.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _db;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork db)
        {
            _logger = logger;
            _db= db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products =_db.Product.GetAll(includeProperties:"Category");
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}