using Microsoft.AspNetCore.Mvc;
using NoteBook_ASP.Data.Interfaces;
using NoteBook_ASP.Models;
using NoteBook_ASP.ViewModels;

namespace NoteBook_ASP.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// список клиентов
        /// </summary>
        private IAllPersons _personRep;


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="personRep"></param>
        /// <param name="shopCart"></param>
        public HomeController(IAllPersons personRep)
        {
            _personRep = personRep;
        }

        /// <summary>
        /// показывает всех клиентов
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            var homePersons = new HomeViewModel
            {
                AllPerson = _personRep.Persons
            };
            return View(homePersons);
        }

        /// <summary>
        /// Удаляет запись. Такое имя ,чтобы не добавлять еще одну View для подтверждения
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Complete(int id)
        {
            _personRep.DeletePerson(id);

            ViewBag.Message = "Запись успешно Удалена!";
            return View();
        }
    }
}
