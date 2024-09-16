using Microsoft.EntityFrameworkCore;
using ProductManagementApp.Models;

namespace ProductManagementApp
{
    public class Program
    {
        private static AppDBContext context;
        static async Task Main(string[] args)
        {
            using (context = new AppDBContext())
            {
                Console.WriteLine("Product Management System");
                Console.WriteLine("1.Add Product");
                Console.WriteLine("2.View All Products");
                Console.WriteLine("3.Update Product");
                Console.WriteLine("4.Delete Product");
                Console.WriteLine("5.Search product by name");
                Console.WriteLine("6.Search for products with stock lower than:");
                Console.WriteLine("0.Exit");
                Console.WriteLine("Select an option:");
                string option = Console.ReadLine();
                while (true)
                {
                    switch (option)
                    {
                        case "1":
                            {
                                Console.WriteLine("Name:");
                                string name = Console.ReadLine();
                                Console.WriteLine("Price");
                                decimal price = Decimal.Parse(Console.ReadLine());
                                Console.WriteLine("Stock");
                                int stock = int.Parse(Console.ReadLine());
                                await AddProduct(name, price, stock);
                                break;
                            }
                        case "2":
                            {
                                await ViewAllProducts();
                                break;
                            }
                        case "3":
                            {
                                Console.WriteLine("Enter product ID:");
                                int id=int.Parse(Console.ReadLine());
                                await Update(id);
                                break;
                            }
                        case "4":
                            {
                                Console.WriteLine("Enter product ID for deleting:");
                                int id = int.Parse(Console.ReadLine());
                                await Delete(id);
                                break;
                            }
                        case "5":
                            {
                                Console.WriteLine("Enter the name of the product:");
                                string name = Console.ReadLine();
                                await SearchProductByName(name);
                                break;
                            }
                        case "6":
                            {
                                Console.WriteLine("Enter the value for stock:");
                                int stock = int.Parse(Console.ReadLine());
                                await StockLower(stock);
                                break;
                            }
                        case "0":
                            {
                                return;
                            }
                        default:
                            {
                                Console.WriteLine("Wrong input");
                                break;
                            }
                    }
                    option = Console.ReadLine();
                }
            }
        }

        private static async Task StockLower(int stock)
        {
            var products = await context.Products.Where(q => q.Stock <= stock).ToListAsync();
            if (products.Any())
            {
                Console.WriteLine("All products with lower stock:");
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Id}. {product.Name} {product.Price} {product.Stock}");
                }
            }
            else
            {
                Console.WriteLine("No products meet the criteria");
            }
        }

        private static async Task SearchProductByName(string? name)
        {
            var product = await context.Products.FirstOrDefaultAsync(q => q.Name == name);
            if(product!=null)
            {
                Console.WriteLine($"{product.Id}. {product.Name} {product.Price} {product.Stock}");
            }
            else
            {
                Console.WriteLine("Such product is not found.");
            }

            
        }

        private static async Task Delete(int id)
        {
            var product=await context.Products.FindAsync(id);
            if(product!=null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
            else 
            {
                Console.WriteLine("Such product doen't exists.");
            }
        }

        private static async Task Update(int id)
        {
            var product=await context.Products.FindAsync(id);
            if(product!=null)
            {
                Console.WriteLine("Enter new product name:");
                string name = Console.ReadLine();
                product.Name = name;
                Console.WriteLine("Enter new product price:");
                decimal price = Decimal.Parse(Console.ReadLine());
                product.Price = price;  
                Console.WriteLine("Enter new product stock:");
                int stock = int.Parse(Console.ReadLine());
                product.Stock = stock;
                context.Products.Update(product);
                await context.SaveChangesAsync();
            }
            else 
            {
                Console.WriteLine("The product is not found!");
            }
        }

        private static async Task ViewAllProducts()
        {
            var products = await context.Products.ToListAsync();
            if (products.Any())
            {
                Console.WriteLine("All products:");
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Id}. {product.Name} {product.Price} {product.Stock}");
                }
            }
            else
            {
                Console.WriteLine("No products found.");
            }
        }

        private static async Task AddProduct(string? name, decimal price, int stock)
        {
            Product product = new Product()
            {
                Name = name,
                Price = price,
                Stock = stock
            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            Console.WriteLine("Product is successfully added");
        }
    }
}
