namespace LibraryManagementSystem.Api.Models
{
    public class Loan
    {
        public long BookId { get; set; }
        public long MemberId { get; set; }
        public DateTime? LoanDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnDate {  get; set; }
        public decimal? FineAmount { get; set; }

        public Book Book { get; set; }
        public Member Member { get; set; }
    }
}
