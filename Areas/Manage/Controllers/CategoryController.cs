using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using mvc_1.DAL;
using mvc_1.Models;

namespace mvc_1.Areas.Manage.Controllers
{   
    [Area("Manage")]
    public class CategoryController:Controller
    {
		
		AppDbContext _context;
        public CategoryController(AppDbContext dbcontext)
        {
            _context = dbcontext;
            
        }
        public IActionResult Index() 
        {
            List<Category>categories=_context.Categories.Include(x=>x.Products).ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var category=_context.Categories.FirstOrDefault(x=>x.Id == id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(Category category) 
        {
            var oldcategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (oldcategory == null)
            {
                return RedirectToAction("Index");
            }
            if(!ModelState.IsValid)
            {
                return View();
            }
            oldcategory.Name = category.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
