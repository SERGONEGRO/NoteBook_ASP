using Microsoft.AspNetCore.Mvc;
using NoteBook_ASP.Data.Interfaces;
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

        public ViewResult Index()
        {
            var homePersons = new HomeViewModel
            {
                AllPerson = _personRep.Persons
            };
            return View(homePersons);
        }
    }
}
