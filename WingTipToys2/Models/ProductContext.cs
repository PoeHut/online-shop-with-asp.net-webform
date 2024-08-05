using System.Data.Entity;

namespace WingTipToys2.Models
{
	public class ProductContext : DbContext
	{
		public ProductContext() : base("WingTipToys2")
		{
		}

		public DbSet<Category> Categories { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<CartItem> ShoppingCartItems { get; set; }
	}
}