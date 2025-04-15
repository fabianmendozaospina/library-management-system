namespace LibraryManagementSystem.Model.ViewModels {
    public class ReaderLoan {
        public Reader Reader { get; set; }
        public List<Loan> Loans { get; set; }
    }
}
