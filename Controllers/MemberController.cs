using LibraryManagementSystem.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly LibraryContext _context;

        public MemberController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return await _context.Members.Include(m => m.LoanBook).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(long id)
        {
            var member = await _context.Members
                .Include(m => m.LoanBook)
                .ThenInclude(l => l.Book)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (member == null) return NotFound();
            return member;
        }

        [HttpPost]
        public async Task<ActionResult<Member>> CreateMember(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMember), new { id = member.Id }, member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(long id, Member member)
        {
            if (id != member.Id) return BadRequest();

            _context.Entry(member).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(long id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null) return NotFound();

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
