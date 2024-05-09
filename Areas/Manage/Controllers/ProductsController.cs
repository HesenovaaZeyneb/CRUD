using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_1.DAL;
using mvc_1.Models;

namespace mvc_1.Areas.Manage.Controllers
{
	[Area("Manage")]
	public class ProductsController:Controller
	{
		AppDbContext _db;
        public ProductsController(AppDbContext dbcontext)
        {
            _db = dbcontext;
        }
        public IActionResult Index()
		{
			List<Product> products = _db.Products.Include(x=>x.Category).ToList();

			
			return View(products);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Product product)
		{
			if(!ModelState.IsValid)
			{
				return View();
			}
			_db.Products.Add(product);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Update(int Id)
		{
			var product=_db.Products.FirstOrDefault(x => x.Id == Id);
			if(product == null)
			{
				return RedirectToAction("Index");
			}
			return View(product);
		}
		[HttpPost]
		public IActionResult Update(Product product)
		{
			var oldProduct = _db.Products.FirstOrDefault(y => y.Id == product.Id);
			if(oldProduct == null)
			{
				return RedirectToAction("Index");
			}
			if(!ModelState.IsValid)
			{
				return View();
			}
			oldProduct.Name = product.Name;
			oldProduct.Price= product.Price;
			oldProduct.Description = product.Description;
			oldProduct.Category = product.Category;
			_db.SaveChanges();
			return RedirectToAction("Index");
			

		}
		public IActionResult Delete(int id) 
		{
			var product = _db.Products.FirstOrDefault(x => x.Id == id);
			if(product != null)
			{
				_db.Products.Remove(product);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}
		
	}
}
