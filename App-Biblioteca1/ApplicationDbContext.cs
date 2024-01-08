using App_Biblioteca1.Models;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace App_Biblioteca1
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        public DbSet<Books> Books { get; set; }
        public DbSet<BookStore> BookStore { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<StateBook> StateBooks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
