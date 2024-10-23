using LibraryManagementSystem.Models;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
           using LibraryDbContext context = new LibraryDbContext();
            var author = new Author { Name = "George Orwell" };
            var book1 = new Book { Title = "1984", Author = author };
            var book2 = new Book { Title = "Animal Farm", Author = author };

            // Create Categories
            var category1 = new Category { Name = "Dystopian" };
            var category2 = new Category { Name = "Political Satire" };


            // Add Author, Books, and Categories
            context.Categories.Add(category1);
            context.Categories.Add(category2);
            context.Authors.Add(author);
            context.SaveChanges();

        }
    }
}
