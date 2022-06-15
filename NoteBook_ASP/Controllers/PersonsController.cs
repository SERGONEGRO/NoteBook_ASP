using Microsoft.AspNetCore.Mvc;
using NoteBook_ASP.Data.Interfaces;
using NoteBook_ASP.Models;
using NoteBook_ASP.ViewModels;

namespace NoteBook_ASP.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IAllPersons _allPersons;

        //поскольку в файле startup мы связали интерфейсы с классами, кторые их реализуют,
        //то теперь через них могут передаваться данные классов
        public PersonsController(IAllPersons iAllPersons)
        {
            _allPersons = iAllPersons;
        }

        [Route("Persons/List")]

        public ViewResult List()
        {
            IEnumerable<Person> persons = null;
            persons = _allPersons.Persons.OrderBy(i => i.Id);

            var personObject = new HomeViewModel
            {
                AllPerson = persons
            };

            ViewBag.Title = "Страница с контактами";   //title можно передавать во viewbag

            return View(personObject);

        }   
    }
}
