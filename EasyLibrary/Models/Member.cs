using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLibrary.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BorrowRecord> BorrowRecords { get; set; }=new List<BorrowRecord>();
    }
}
