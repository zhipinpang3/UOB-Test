namespace LibraryManagementSystem.Api.Models
{
    public class Member
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Loan> LoanBook { get; set; } = new List<Loan>();
    }
}
