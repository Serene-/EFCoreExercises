using EasyLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace EasyLibrary
{
    public class Program
    {
      
        static void Main(string[] args)
        {
            Console.WriteLine("Library Management system");
            Console.WriteLine("Choose option");
            Console.WriteLine("1.Adding book");
            Console.WriteLine("2. Adding member");
            Console.WriteLine("3. List of all books");
            Console.WriteLine("4. List of all authors");
            Console.WriteLine("5. Search books by author");
            Console.WriteLine("6. Search books by category");
            Console.WriteLine("7. Edit book by name");
            Console.WriteLine("8. Delete author");
            Console.WriteLine("9. Delete book");
            Console.WriteLine("10. Exit");
            int option = int.Parse(Console.ReadLine());
            while(option!=10)
            {
                switch(option)
                {
                    case 1:
                        {
                            AddingBook();
                            break;
                        }
                    case 2:
                        {
                            break;
                        }
                    case 3:
                        {
                            ListAllBooks();
                            break;
                        }
                    case 4:
                        {
                            ListAllAuthors();
                            break;
                        }
                    case 5:
                        {
                            SearchBooksByAuthor();
                            break;
                        }
                    case 6:
                        {
                            SearchBooksByCategory();
                            break;
                        }
                    case 7:
                        {
                            EditBook();
                            break;
                        }
                    case 8:
                        {
                            DeleteAuthor();
                            break;
                        }
                    case 9:
                        {
                            DeleteBook();
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Wrong input");
                            break;
                        }
                }
                option = int.Parse(Console.ReadLine());
            }


        }

        private static async void DeleteBook()
        {
           using LibraryContext context=new LibraryContext();
            Console.WriteLine("Enter the book title you want to delete");
            string title=Console.ReadLine();
            var book=await context.Books.FirstOrDefaultAsync(x=>x.Title==title);
            if (book != null)
            {
                context.Books.Remove(book);
                context.SaveChanges();
                Console.WriteLine("Book is deleted.");
            }
            else
            {
                Console.WriteLine("Book with this title doesn't exists");
            }
        }

        private static async void DeleteAuthor()
        {
            using LibraryContext context = new LibraryContext();
            Console.WriteLine("Enter the name of the author you want to delete:");
            string authorName = Console.ReadLine();
            var author = await context.Authors.FirstOrDefaultAsync(x => x.Name == authorName);
            if (author == null)
            {
                Console.WriteLine("Such author doesn't exists");
            }
            else
            {
                context.Authors.Remove(author);
                context.SaveChanges();
                Console.WriteLine("Author is deleted successfully!");
            }
        }
        private static async void EditBook()
        {
            using LibraryContext context = new LibraryContext();
            Console.WriteLine("Enter the title of the book");
            string title=Console.ReadLine();
            var book = await context.Books.FirstOrDefaultAsync(x => x.Title == title);
           
            if (book != null)
            {
                Console.WriteLine("Enter the new title:");
                string newTitle = Console.ReadLine();
                Console.WriteLine("Enter the new author:");
                var newAuthor = Console.ReadLine();
                Console.WriteLine("Enter the new categories separeted by comma:");
                var newCategories = Console.ReadLine().Split(',').ToList();
                book.Title = newTitle;
                var authorSearch = await context.Authors.FirstOrDefaultAsync(x => x.Name.ToLower() == newAuthor.ToLower());
                if (authorSearch == default)
                {
                    var author = new Author() { Name = newAuthor };
                    book.Author = author;
                }
                else
                {
                    book.Author = authorSearch;
                }
                foreach (var category in newCategories)
                {
                        var categorySearch = await context.Categories.FirstOrDefaultAsync(x => x.Name.ToLower() == category.ToLower());
                        if (categorySearch == null)
                        {
                            var categoryNew = new Category() { Name = category };
                            book.Categories.Add(categoryNew);
                        }
                        else
                        {
                            book.Categories.Add(categorySearch);
                        }
                }
                context.Books.Update(book);
                await context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Book with this name doesn't exist");
            }
        }

        private static async void SearchBooksByCategory()
        {
            using LibraryContext context = new LibraryContext();
            Console.WriteLine("Enter the category:");
            string category = Console.ReadLine();
            var books = await context.CategoryBooks
                .Where(x => x.Category.Name.ToLower() == category.ToLower())
                .Include(x => x.Book)
                .ToListAsync();
            foreach (var book in books)
            {
                Console.WriteLine(book.Book.Title);
            }
        }

        private static async void SearchBooksByAuthor()
        {
            using LibraryContext context = new LibraryContext();
            Console.WriteLine("Enter the name of the author:");
            string authorName=Console.ReadLine();
            var books = await context.Books.Where(x => x.Author.Name == authorName)
                .OrderBy(y=>y.Title)
                .ToListAsync();
            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }

        private static async void ListAllAuthors()
        {
            using LibraryContext context = new LibraryContext();
            var autors=await context.Authors.ToListAsync();
            Console.WriteLine("All authors:");
            foreach (var author in autors)
            {
                Console.WriteLine(author.Name);
            }
        }

        private static async void ListAllBooks()
        {
            using LibraryContext context = new LibraryContext();
            StringBuilder sb=new StringBuilder();
            Console.WriteLine("All books:");
            await context.Books
                .Include(x => x.Author)
                .Include(y=>y.Categories)
                .ToArrayAsync();
            foreach (var book in context.Books)
            {
                foreach (var category in book.Categories)
                {
                    sb.Append(category.Name);
                    sb.Append(',');
                }
                if (sb.Length == 0) sb.Append("No category");
                Console.WriteLine($"{book.Title} - {book.Author.Name} - {sb.ToString()}");
                sb.Clear();
            }
        }

        private static async void AddingBook()
        {
            using LibraryContext context = new LibraryContext();
            Console.WriteLine("Enter book titile:");
            string title=Console.ReadLine();
            Console.WriteLine("Enter book author:");
            string authorName=Console.ReadLine();
            Console.WriteLine("Enter book categories separated by comma:");
            string[] categories = Console.ReadLine().Split(',');
            List<Category> categoriesList = new List<Category>();
            foreach (var cat in categories)
            {
                var category = context.Categories.FirstOrDefault(x => x.Name == cat);
                if (category == null)
                {
                    categoriesList.Add(new Category() { Name=cat});
                }
                else
                {
                    categoriesList.Add(category);
                }
            }
            var author = context.Authors.FirstOrDefault(x => x.Name == authorName);
            var book = context.Books.FirstOrDefault(x => x.Title == title);
            Book newBook;
            if (author==null && book==null)
            {
                Author newAuthor = new Author() { Name=authorName};
                newBook = new Book()
                {
                    Title = title,
                    Author = newAuthor,
                    Categories= categoriesList
                };
                await context.Books.AddAsync(newBook);
                await context.SaveChangesAsync();

            }
            else if(author!=null)
            {
                newBook = new Book()
                {
                    Title = title,
                    Author = author,
                    Categories = categoriesList
                };
                await context.Books.AddAsync(newBook);
                await context.SaveChangesAsync();
            }
            else if (book != null)
            {
                Console.WriteLine("Book already exists");
            }
           
        }
    }
}
