using EasyLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLibrary
{
    public class LibraryContext :DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryBook> CategoryBooks { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-UST16T5;Database=EasyLibrary;Integrated Security=True;Encrypt=False");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(x => x.Categories)
                .WithMany(x => x.Books)
                .UsingEntity<CategoryBook>();

            modelBuilder.Entity<Category>()
             .HasIndex(x => x.Name)
             .IsUnique();

        }
    }
}
