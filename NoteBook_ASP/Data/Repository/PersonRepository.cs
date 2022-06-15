using NoteBook_ASP.Data.Interfaces;
using NoteBook_ASP.Models;

namespace NoteBook_ASP.Data.Repository
{
    /// <summary>
    /// Класс для работы с БД
    /// </summary>
    public class PersonRepository : IAllPersons
    {
        //переменная для работы с файлос AppDBContent
        private readonly AppDBContent appDBContent;

        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public PersonRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        /// <summary>
        /// получаем всех клиентов
        /// </summary>
        public IEnumerable<Person> Persons => appDBContent.Person;


        /// <summary>
        /// получение пользователя по id
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Person GetObjectPerson (int personId) => appDBContent.Person.FirstOrDefault(p => p.Id == personId);
    }
}
