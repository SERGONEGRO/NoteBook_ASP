using Microsoft.EntityFrameworkCore;
using NoteBook_ASP.Models;

namespace NoteBook_ASP.Data
{
    public class AppDBContent : DbContext
    {
        /// <summary>
        /// Конструктор по умолчанию. Получает данные и передает в базовый конструктор
        /// </summary>
        /// <param name="options"></param>
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options) { }

        /// <summary>
        /// получает всех клиентов
        /// </summary>
        public DbSet<Person> Person { get; set; }
    }
}
