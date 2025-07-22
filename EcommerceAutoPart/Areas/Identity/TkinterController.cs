using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceAutoPart.Areas.Identity
{
    public class TkinterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RunTkinter()
        {
            try
            {
                // استبدل المسار بمسار البرنامج الذي يحتوي على كود Tkinter
                Process.Start("python", @"C:\Users\ROG STRIX\Desktop\BootcampPython\finally.py");
                return Json(new { success = true });
            }
            catch 
            {
                return Json(new { success = false, error = "Lol"});
            }
        }
    }
}
