using Microsoft.AspNetCore.Mvc;

namespace NoteBook_ASP.Controllers
{
    public class AboutController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
