using NoteBook_ASP.Models;

namespace NoteBook_ASP.ViewModels
{
    /// <summary>
    /// на основе этой модели создаем объекты и записываем в него нужные значения,
    /// чтобы потом передать во View 1 обьект, а не несколько
    /// </summary>
    public class HomeViewModel
    {
        /// <summary>
        /// показываем всех клиентов на главной
        /// </summary>
        public IEnumerable<Person> AllPerson { get; set; }
    }
}
