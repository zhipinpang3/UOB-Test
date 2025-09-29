namespace LibraryManagementSystem.Api.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; }
    }
}
