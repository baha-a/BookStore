using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext() : base() { }
        public BookDbContext(DbContextOptions options) 
            : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasIndex(p => p.Title)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(p => new { p.FirstName, p.LastName})
                .IsUnique();

            modelBuilder.Entity<Checkout>()
                .HasIndex(p => new { p.BookId })
                .HasFilter("Deleted = 0")
                .IsUnique();
        }
    }
}
