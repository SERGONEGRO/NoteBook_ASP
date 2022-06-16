using Microsoft.AspNetCore.Mvc;
using NoteBook_ASP.Data.Interfaces;
using NoteBook_ASP.Models;

namespace NoteBook_ASP.Controllers
{
    public class NewPersonController: Controller
    {
        private IAllPersons _personRep;

        public NewPersonController(IAllPersons allPersons){
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

        [HttpPost]
        public IActionResult Confirm(Person newPerson)
        {
            if (newPerson != null)
            {
                _personRep.CreatePerson(newPerson);
            }
            ViewBag.Message = "Новая запись успешно создана!";
            return View();
        }
      
    }
}
