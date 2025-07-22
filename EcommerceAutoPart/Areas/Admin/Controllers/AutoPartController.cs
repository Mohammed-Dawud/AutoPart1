using EcommerceAutoPart.DataAccess.Data;
using EcommerceAutoPart.DataAccess.Repository;
using EcommerceAutoPart.DataAccess.Repository.IRepository;
using EcommerceAutoPart.Models;
using EcommerceAutoPart.Models.ViewModels;
using EcommerceAutoPart.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace EcommerceAutoPart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AutoPartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AutoPartController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<AutoPart> objAutoPartList = _unitOfWork.AutoPart.GetAll(includeProperties: "Car").ToList();

            return View(objAutoPartList);
        }
        public IActionResult Upsert(int? id)
        {

            AutoPartVM autoPartVM = new()
            {
                CarList = _unitOfWork.Car.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CarName,
                    Value = u.Id.ToString(),
                }),
                AutoPart = new AutoPart()

            };
            if (id == null || id == 0)
            {
                //create
                return View(autoPartVM);

            }
            else
            {
                //update
                autoPartVM.AutoPart = _unitOfWork.AutoPart.Get(u => u.Id == id);
                return View(autoPartVM);
            }

        }
        [HttpPost]
        public IActionResult Upsert(AutoPartVM autoPartVM, IFormFile? file)
        {


            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string autoPartPath = Path.Combine(wwwRootPath, @"images\autoPart");
                    if (!string.IsNullOrEmpty(autoPartVM.AutoPart!.AutoPartPhoto))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, autoPartVM.AutoPart.AutoPartPhoto.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(autoPartPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                    }
                    autoPartVM.AutoPart!.AutoPartPhoto = @"\images\autoPart\" + fileName;
                }

                if (autoPartVM.AutoPart!.Id == 0)
                {
                    _unitOfWork.AutoPart.Add(autoPartVM.AutoPart);
                }
                else
                {
                    _unitOfWork.AutoPart.Update(autoPartVM.AutoPart);
                }

                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");

            }
            else
            {
                autoPartVM.CarList = _unitOfWork.Car.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CarName,
                    Value = u.Id.ToString(),
                });
                return View(autoPartVM);

            }

        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<AutoPart> objAutoPartList = _unitOfWork.AutoPart.GetAll(includeProperties: "Car").ToList();
            return Json(new { data = objAutoPartList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var autoPartToDeleted = _unitOfWork.AutoPart.Get(u => u.Id == id);
            if (autoPartToDeleted == null)
            {
                return Json(new { success = false, message = "error while deleting" });
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, autoPartToDeleted.AutoPartPhoto!.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.AutoPart.Remove(autoPartToDeleted);
            _unitOfWork.Save();
            return Json(new { success = false, message = "Delete Successful" });
        }
        #endregion
    }
}
