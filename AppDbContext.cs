using Microsoft.EntityFrameworkCore;

namespace Course_Project_OOP_3
{
    public class AppDbContext : DbContext
    {
        // List of tables 
        public DbSet<Product> Products { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Рядок підключення. Переконайся, що Initial Catalog збігається з назвою твоєї бази
            optionsBuilder.UseSqlServer(@"Server=desktop-oa4tbtq\sqlexpress;Database=Products;Integrated Security=True;TrustServerCertificate=True;");
        }
    }
}
