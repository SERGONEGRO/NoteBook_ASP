using Microsoft.AspNetCore.Mvc;
using NoteBook_ASP.Data.Interfaces;
using NoteBook_ASP.Models;

namespace NoteBook_ASP.Controllers
{
    public class NewPersonController : Controller
    {
        private IAllPersons _personRep;

        public NewPersonController(IAllPersons allPersons)
        {
            _personRep = allPersons;
        }

        /// <summary>
        /// новое окно
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// при нажатии на СОХРАНИТЬ
        /// </summary>
        /// <param name="newPerson"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(Person newPerson)
        {
            if (ModelState.IsValid)
            {
                _personRep.CreatePerson(newPerson);
                return RedirectToAction("Complete");
            }
            return View(newPerson);
        }


        /// <summary>
        /// Если успешно
        /// </summary>
        /// <returns></returns>
        public IActionResult Complete()
        {
            ViewBag.Message = "Новая запись успешно создана!";
            return View();

        }
    }
}
