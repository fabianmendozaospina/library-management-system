namespace LibraryManagementSystem.Model
{
    public class Book
    {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public string Synopsis { get; set; }
        public string Photo { get; set; }
        public Subject? Subject { get; set; }
        public List<Edition>? Editions { get; set; } = new List<Edition>();
        public ICollection<BookAuthor>? BookAuthors { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<LoanDetail>? LoanDetails { get; set; }
    }
}
