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

        /// <summary>
        /// создать клиента
        /// </summary>
        /// <param name="order"></param>
        public void CreatePerson(Person person)
        {
            Person newPerson = new Person()
            {
                Name = person.Name,
                SurName = person.SurName,
                LastName = person.LastName,
                PhoneNumber = person.PhoneNumber,
                Address = person.Address,
                Description = person.Description
            };
            appDBContent.Person.Add(newPerson);

            appDBContent.SaveChanges();
        }

        /// <summary>
        /// Редактировать клиента
        /// </summary>
        /// <param name="order"></param>
        public void UpdatePerson(Person person)
        {
            Person newPerson = GetObjectPerson(person.Id);
            newPerson.Name = person.Name;
            newPerson.SurName = person.SurName;
            newPerson.LastName = person.LastName;
            newPerson.PhoneNumber = person.PhoneNumber;
            newPerson.Address = person.Address;
            newPerson.Description = person.Description;
            
            appDBContent.Person.Update(newPerson);

            appDBContent.SaveChanges();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="person"></param>
        public void DeletePerson(Person person)
        {
            Person newPerson = GetObjectPerson(person.Id);
            appDBContent.Person.Remove(newPerson);
            appDBContent.SaveChanges();
        }
    }
}
