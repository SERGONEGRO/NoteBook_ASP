using Microsoft.AspNetCore.Mvc;
using NoteBook_ASP.Data.Interfaces;
using NoteBook_ASP.Models;

namespace NoteBook_ASP.Controllers
{
    public class EditPersonController:Controller
    {
        /// <summary>
        /// список клиентов
        /// </summary>
        private IAllPersons _AllPersons;

        private Person _personToEdit;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="appDBContent"></param>
        /// <param name=""></param>
        public EditPersonController(IAllPersons allPersons)
        {
            _AllPersons = allPersons;
        }

        [Route("EditPerson/Index")]
        [Route("EditPerson/Index/{id}")]
        public IActionResult Index(int id)
        {
            _personToEdit = _AllPersons.Persons.FirstOrDefault(x => x.Id == id);
            ViewBag.Title = "Страница с клиентом";
            return View(_personToEdit);
        }

        [HttpPost]
        [Route("EditPerson/Confirm")]
        [Route("EditPerson/Confirm/{id}")]
        public IActionResult Confirm(Person person)
        {

            _AllPersons.UpdatePerson(person);

            ViewBag.Message = "Запись успешно отредактирована!";
            return View();
        }

    }
}
