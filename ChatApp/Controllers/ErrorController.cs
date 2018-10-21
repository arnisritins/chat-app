using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Error page
        /// </summary>
        public IActionResult Index()
        {
            return View("Error");
        }
    }
}
