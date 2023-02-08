using eCommerceWebProj.Data;
using eCommerceWebProj.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceWebProj.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Display Order can't be the same as Name");
                // This here with Name, will be a specific error to the Name label
                ModelState.AddModelError("Name", "Display Order can't be the same as Name");
            }
            // in .NET Core we have this method to check validation
            if (ModelState.IsValid)
            {
            _db.Categories.Add(obj);
            _db.SaveChanges();
                TempData["success"] = "Category created successfully!";
            return RedirectToAction("Index");
            } 
            return View(obj);
        }


		// GET with Edit using ID
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) { 
            return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb== null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // POST with Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Display Order can't be the same as Name");
                // This here with Name, will be a specific error to the Name label
                //ModelState.AddModelError("Name", "Display Order can't be the same as Name");
            }
            // in .NET Core we have this method to check validation
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET using ID
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // POST with Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id); 
            if (obj == null)
            {
                return NotFound();
            }

                _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
            
        }
    }
}
