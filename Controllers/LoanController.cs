using LibraryManagementSystem.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly LibraryContext _context;

        public LoanController(LibraryContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Loan>> BorrowBook(long bookId, long memberId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || !book.IsAvailable)
                return BadRequest("Book not available");

            var loan = new Loan
            {
                BookId = bookId,
                MemberId = memberId,
                LoanDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14)
            };

            book.IsAvailable = false;

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return loan;
        }

        [HttpPost("return/{bookId}")]
        public async Task<IActionResult> ReturnBook(long bookId)
        {
            var loan = await _context.Loans
                .Where(l => l.BookId == bookId && l.ReturnDate == null)
                .FirstOrDefaultAsync();

            if (loan == null) return BadRequest("Loan not found");

            loan.ReturnDate = DateTime.UtcNow;
            loan.Book.IsAvailable = true;

            if (loan.ReturnDate > loan.DueDate)
            {
                var overdueDays = (loan.ReturnDate.Value - loan.DueDate.Value).Days;
                loan.FineAmount = overdueDays * 1;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("member/{memberId}")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoansForMember(long memberId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Where(l => l.MemberId == memberId)
                .ToListAsync();
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetActiveLoans()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Where(l => l.ReturnDate == null)
                .ToListAsync();
        }
    }

}
