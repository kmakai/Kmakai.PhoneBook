using Kmakai.PhoneBook.Models;
using Microsoft.EntityFrameworkCore;

namespace Kmakai.PhoneBook.Data;

public class AppDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.;Trusted_Connection=True;database=PhoneBook;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().HasData(
                       new Contact { Id = 1, Name = "John Doe", PhoneNumber = "1234567890", Email = "example@gmail.com" });
    }
}
