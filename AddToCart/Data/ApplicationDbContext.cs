using AddToCart.Models;
using Microsoft.EntityFrameworkCore;

namespace AddToCart.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) { }
        public DbSet<Product>products { get; set; }
        public DbSet<CartItem> cartItems { get; set; }
    }
}
