﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int BookId { get; set; }
        public Book BorrowedBook { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }

    }
}
