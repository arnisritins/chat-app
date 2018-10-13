using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ChatApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        /// <summary>
        /// Chat home page
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}
