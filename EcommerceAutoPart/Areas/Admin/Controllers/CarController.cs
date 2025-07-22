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
    public class CarController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CarController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Car> objCarList = _unitOfWork.Car.GetAll(includeProperties: "Brand").ToList();

            return View(objCarList);
        }
        public IActionResult Upsert(int? id)
        {

            CarVM carVM = new()
            {
                BrandList = _unitOfWork.Brand.GetAll().Select(u => new SelectListItem
                {
                    Text = u.BrandName,
                    Value = u.Id.ToString(),
                }),
                Car = new Car()

            };
            if (id == null || id == 0)
            {
                //create
                return View(carVM);

            }
            else
            {
                //update
                carVM.Car = _unitOfWork.Car.Get(u => u.Id == id);
                return View(carVM);
            }

        }
        [HttpPost]
        public IActionResult Upsert(CarVM carVM, IFormFile? file)
        {


            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string carPath = Path.Combine(wwwRootPath, @"images\car");
                    if (!string.IsNullOrEmpty(carVM.Car!.CarPhoto))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, carVM.Car.CarPhoto.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(carPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                    }
                    carVM.Car!.CarPhoto = @"\images\car\" + fileName;
                }

                if (carVM.Car!.Id == 0)
                {
                    _unitOfWork.Car.Add(carVM.Car);
                }
                else
                {
                    _unitOfWork.Car.Update(carVM.Car);
                }

                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");

            }
            else
            {
                carVM.BrandList = _unitOfWork.Brand.GetAll().Select(u => new SelectListItem
                {
                    Text = u.BrandName,
                    Value = u.Id.ToString(),
                });
                return View(carVM);

            }

        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Car> objCarList = _unitOfWork.Car.GetAll(includeProperties: "Brand").ToList();
            return Json(new { data = objCarList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var carToDeleted = _unitOfWork.Car.Get(u => u.Id == id);
            if (carToDeleted == null)
            {
                return Json(new { success = false, message = "error while deleting" });
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, carToDeleted.CarPhoto!.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Car.Remove(carToDeleted);
            _unitOfWork.Save();
            return Json(new { success = false, message = "Delete Successful" });
        }
        #endregion
    }
}
