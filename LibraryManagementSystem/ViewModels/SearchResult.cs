using LibraryManagementSystem.Model;

namespace LibraryManagementSystem.ViewModels
{
    public class SearchResult
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public int SubjectId { get; set; }
        public string? Subject { get; set; }
        public string? Synopsis { get; set; }
        public List<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
