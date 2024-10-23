using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLibrary.Models
{
    public class CategoryBook
    {
        public int BookId { get; set; }
        public int CatgoryId { get; set; }
        public Book Book { get; set; }
        public Category Category { get; set; }

    }
}
