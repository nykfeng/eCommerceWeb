using eCommerceWebProj.DataAccess.Repository.IRepository;
using eCommerceWebProj.Models;
using eCommerceWebProj.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerceWebProj.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> objCoverTypeList = _unitOfWork.Product.GetAll();
            return View(objCoverTypeList);
        }


        // GET with Edit using ID
        // using one action method to handle both create and edit
        public IActionResult Upsert(int? id)
        {

            //Product product = new();
            //// SelectListItem is from ASP.Net MVC Rendering 
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
            //	u => new SelectListItem
            //	{
            //		Text = u.Name,
            //		Value = u.Id.ToString()
            //	}
            //);
            //IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
            //	u => new SelectListItem
            //	{
            //		Text = u.Name,
            //		Value = u.Id.ToString()
            //	}
            //);

            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }
                    ),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    })
            };


			if (id == null || id == 0)
            {
                // --- create product ---
                //// ViewBag
                //ViewBag.CategoryList = CategoryList;
                //// ViewData
                //ViewData["CoverTypeList"] = CoverTypeList;
                
                return View(productVM);
            } 
            else
            {
                // update product
            }


            return View(productVM);
        }

        // POST with Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType obj)
        {
            // in .NET Core we have this method to check validation
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
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
            var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDbFirst);
        }

        // POST with Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }

            _unitOfWork.CoverType.Remove(coverTypeFromDbFirst);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");

        }
    }
}
