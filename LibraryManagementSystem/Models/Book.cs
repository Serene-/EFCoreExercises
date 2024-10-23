using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<BorrowRecord> BorrowRecords { get; set; }=new List<BorrowRecord>();

    }
}
