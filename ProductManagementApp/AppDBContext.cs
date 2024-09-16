using Microsoft.EntityFrameworkCore;
using ProductManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductManagementApp
{
    public class AppDBContext: DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source =DESKTOP-UST16T5; Database=ProductManagement; Trusted_Connection=True; TrustServerCertificate=True; Encrypt = False");

        }
    }
}
