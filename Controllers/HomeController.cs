using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using mvc_1.DAL;
using mvc_1.Models;

namespace mvc_1.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _dbContext;
        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Product> products = _dbContext.Products.ToList();
            return View(products);
        }

        public IActionResult Detail(int id)
        {
            var product = _dbContext.Products.Include(x=>x.Category).FirstOrDefault(p => p.Id == id);
            ViewBag.Realed=_dbContext.Products.Where(x=>x.CategoryID==product.CategoryID&&x.Id!=product.Id).ToList();
            return View(product);
        }
    }
}
