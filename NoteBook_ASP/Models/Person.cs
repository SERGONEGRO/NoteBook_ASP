namespace NoteBook_ASP.Models
{
    public class Person
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string FullName
        {
            get { return SurName + " " + Name + " " + LastName; }
        }
    }
}
