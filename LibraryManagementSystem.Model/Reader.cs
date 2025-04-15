namespace LibraryManagementSystem.Model
{
    public class Reader
    {
        public int ReaderId { get; set; }
        public string CoreId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public DateTime BirthDay { get; set; }
        public ICollection<Loan>? Loans { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
    }
}
