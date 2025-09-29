using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Api.Models;
{
    public class LibraryContext : DbContext
    {
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("CONNECTION_STRING");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>()
                .HasKey(l => new { l.BookId, l.MemberId });

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book)
                .WithMany()
                .HasForeignKey(l => l.BookId);

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Member)
                .WithMany()
                .HasForeignKey(l => l.MemberId);

            modelBuilder.Entity<Loan>()
                .Property(l => l.FineAmount)
                .HasColumnType("DECIMAL(10, 2)");
        }
    }
}
