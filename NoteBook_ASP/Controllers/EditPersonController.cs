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

        /// <summary>
        /// При открытии окна или при неверном вводе данных вызывается этот метод
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("EditPerson/Index")]
        [Route("EditPerson/Index/{id}")]
        public IActionResult Index(int id)
        {
            _personToEdit = _AllPersons.Persons.FirstOrDefault(x => x.Id == id);
            ViewBag.Title = "Страница с клиентом";
            return View(_personToEdit);
        }

        /// <summary>
        /// Вызывается при нажатии на кнопку "сохранить"
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditPerson/Index")]
        [Route("EditPerson/Index/{id}")]
        public IActionResult Index(Person person)
        {
            if (ModelState.IsValid)
            {
                _AllPersons.UpdatePerson(person);
                return RedirectToAction("Complete");
            }
            return View(person);

        }

        /// <summary>
        /// Если прошло успешно
        /// </summary>
        /// <returns></returns>
        public IActionResult Complete()
        {
            ViewBag.Message = "Запись успешно отредактирована!";
            return View();
        }

    }
}
