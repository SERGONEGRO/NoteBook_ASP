using NoteBook_ASP.Models;

namespace NoteBook_ASP.Data.Interfaces
{
    /// <summary>
    /// Get all about persons
    /// </summary>
    public interface IAllPersons
    {
        IEnumerable<Person> Persons { get; }
    }
}
