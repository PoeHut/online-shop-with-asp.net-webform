using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingTipToys2.Models;

namespace WingTipToys2.Process
{
	public class ShoppingCartActions : IDisposable
	{
		public string ShoppingCartId { get; set; }

		private ProductContext _db = new ProductContext();

		public const string CartSessionKey = "CartId";

		public void AddToCart(int id)
		{
			ShoppingCartId = GetCartId();

			var cartItem = _db.ShoppingCartItems.SingleOrDefault(
					c => c.CartId == ShoppingCartId && c.ProductId == id
				);

			if (cartItem == null)
			{
				cartItem = new CartItem
				{
					ItemId = Guid.NewGuid().ToString(),
					ProductId = id,
					CartId = ShoppingCartId,
					Product = _db.Products.SingleOrDefault(p => p.ProductID == id),
					Quantity = 1,
					DateCreated = DateTime.Now
				};

				_db.ShoppingCartItems.Add(cartItem);
			}
			else
			{
				cartItem.Quantity++;
			}

			_db.SaveChanges();
		}

		public string GetCartId()
		{
			if (HttpContext.Current.Session[CartSessionKey] == null)
			{
				// Create CartId with login user name
				if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
				{
					HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
				}
				else
				{
					// Generate a new random GUID using System.Guid class. for unregister user
					Guid tempCartId = Guid.NewGuid();
					HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
				}
			}
			return HttpContext.Current.Session[CartSessionKey].ToString();
		}

		public void Dispose()
		{
			if (_db != null)
			{
				_db.Dispose();
				_db = null;
			}
		}

		public List<CartItem> GetCartItems()
		{
			ShoppingCartId = GetCartId();

			return _db.ShoppingCartItems.Where(c => c.CartId == ShoppingCartId).ToList();
		}
	}
}