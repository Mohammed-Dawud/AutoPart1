using EcommerceAutoPart.DataAccess.Repository.IRepository;
using EcommerceAutoPart.Models;
using EcommerceAutoPart.Models.ViewModels;
using EcommerceAutoPart.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System.Diagnostics;
using System.Security.Claims;
using System.Web.Helpers;

namespace EcommerceAutoPart.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private static List<ShoppingCart> carts = new List<ShoppingCart>();

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var brands = _unitOfWork.Brand.GetAll();
            return View(brands);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult SearchByName()
        {
            var initialBrands = _unitOfWork.Brand.GetAll(); // Retrieve initial data
            return View(initialBrands);
        }
        [HttpPost]
        public IActionResult SearchByName(SearchBrandName model)
        {
            model.Brands = _unitOfWork.Brand.GetAll();

            var SearchBrands = _unitOfWork.Brand.GetAll().Where(p => p.BrandName.ToLower().Contains(model.Name.ToLower()));
            return View(SearchBrands);

        }


        public IActionResult CarView()
        {
            var cars = _unitOfWork.Car.GetAll(includeProperties: "Brand"); // Fetch cars and their brands
            return View(cars);
        }

        // View for displaying auto parts related to a specific car
        public IActionResult AutoPartView(int carId)
        {
            var autoParts = _unitOfWork.AutoPart.GetList(u => u.CarId == carId, includeProperties: "Car");
            AutoPartViewModel viewModel = new()
            {
                AutoParts = autoParts.ToList(), // All auto parts for the selected car
                ShoppingCart = new ShoppingCart() // Empty shopping cart for now
            };
            return View(viewModel);
        }

        // POST: Add selected auto parts to the cart
        [HttpPost]
        [Authorize]
        public IActionResult AddToCart(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            // Fetch the AutoPart from the database to get its CarId (this should exist)
            var autoPart = _unitOfWork.AutoPart.Get(u => u.Id == shoppingCart.AutoPartId, includeProperties: "Car");

            if (autoPart == null)
            {
                TempData["error"] = "Auto part not found!";
                return RedirectToAction(nameof(Index));
            }

            // Now set the CarId on the ShoppingCart (based on the AutoPart)
            shoppingCart.CarId = autoPart.CarId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId &&
            u.AutoPartId == shoppingCart.AutoPartId);

            if (cartFromDb != null)
            {
                // If the item already exists in the cart, update the quantity
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
                _unitOfWork.Save();
            }
            else
            {
                // Add new auto part to the shopping cart
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                // Update the session count for the cart
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }

            TempData["success"] = "Cart updated successfully";
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
