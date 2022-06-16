using NoteBook_ASP.Models;

namespace NoteBook_ASP.Data
{
    public class DBObjects
    {
        /// <summary>
        /// Add objects into DB
        /// </summary>
        /// <param name="app">app</param>
        public static void Initial (AppDBContent content)
        {
            //добавляем товары в БД, если их нет
            if (!content.Person.Any())
            {
                content.AddRange(
                    new Person
                    {
                        Name = "Гилл",
                        SurName = "Бейтс",
                        LastName = "",
                        PhoneNumber = "+74597123504",
                        Address = "ЮАР",
                        Description = "Что-то пошло не так",
                    },
                     new Person
                     {
                         Name = "Барак",
                         SurName = "Обама",
                         LastName = "Обаракович",
                         PhoneNumber = "15654845",
                         Address = "Вашингтонская область, Вашингтон, улица Ленина, дом 55, кв.67",
                         Description = "Осторожнее в подъездах",
                     },
                      new Person
                      {
                          Name = "Арнольдушка",
                          SurName = "Шварцнигерян",
                          LastName = "",
                          PhoneNumber = "f2:c9:5d:12:4c:60",
                          Address = "Калифорнийская республика, пгт. Лос-Анджелес",
                          Description = "",
                      }
                    );
            }
            //сохраняем изменения
            content.SaveChanges();
        }
    }
}
