using Microsoft.AspNetCore.Mvc;
using NoteBook_ASP.Models;

namespace NoteBook_ASP.Controllers
{
    public class NewPersonController: Controller
    {
        private readonly Person _newPerson;

        public NewPersonController(){ }

        /// <summary>
        /// новое окно
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

      
    }
}
