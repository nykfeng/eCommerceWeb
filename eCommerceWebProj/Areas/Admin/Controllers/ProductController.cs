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
        // So to access the wwwroot folder
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
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
                // render the product's data
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id== id);
				return View(productVM);
			}


           
        }

        // POST with Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            // in .NET Core we have this method to check validation
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        Console.WriteLine("oldImagePath: "+ oldImagePath);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                if (obj.Product.Id == 0)
                {
					_unitOfWork.Product.Add(obj.Product);
				}
                else
                {
					_unitOfWork.Product.Update(obj.Product);
				}
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully!";
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

		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return Json(new { data = productList });
		}
		#endregion

	}
}
