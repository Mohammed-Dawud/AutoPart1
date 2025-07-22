using EcommerceAutoPart.DataAccess.Data;
using EcommerceAutoPart.DataAccess.Repository;
using EcommerceAutoPart.DataAccess.Repository.IRepository;
using EcommerceAutoPart.Models;
using EcommerceAutoPart.Models.ViewModels;
using EcommerceAutoPart.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;


namespace EcommerceAutoPart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BrandController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Brand> objBrandList = _unitOfWork.Brand.GetAll().ToList();

            return View(objBrandList);
        }

        public IActionResult Upsert(int? id)
        {
            BrandVM brandVM = new BrandVM
            {
                Brand = new Brand()
            };

            if (id == null || id == 0)
            {
                // Create
                return View(brandVM);
            }
            else
            {
                // Update
                brandVM.Brand = _unitOfWork.Brand.Get(u => u.Id == id);
                return View(brandVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(BrandVM brandVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string brandPath = Path.Combine(wwwRootPath, @"images\brand");

                    if (!string.IsNullOrEmpty(brandVM.Brand!.BrandPhoto))
                    {
                        // Delete old image
                        var oldImagePath = Path.Combine(wwwRootPath, brandVM.Brand.BrandPhoto.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(brandPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    brandVM.Brand!.BrandPhoto = @"\images\brand\" + fileName;
                }

                if (brandVM.Brand!.Id == 0)
                {
                    _unitOfWork.Brand.Add(brandVM.Brand);
                }
                else
                {
                    _unitOfWork.Brand.Update(brandVM.Brand);
                }

                _unitOfWork.Save();
                TempData["success"] = "Brand created successfully";
                return RedirectToAction("Index");
            }

            return View(brandVM);
        }
   

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Brand> objBrandList = _unitOfWork.Brand.GetAll().ToList();
            return Json(new { data = objBrandList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var brandToDeleted = _unitOfWork.Brand.Get(u => u.Id == id);
            if (brandToDeleted == null)
            {
                return Json(new { success = false, message = "error while deleting" });
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, brandToDeleted.BrandPhoto!.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Brand.Remove(brandToDeleted);
            _unitOfWork.Save();
            return Json(new { success = false, message = "Delete Successful" });
        }
        #endregion
    }
}
