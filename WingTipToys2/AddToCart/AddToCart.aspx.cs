using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using WingTipToys2.Process;

namespace WingTipToys2.AddToCart
{
	public partial class AddToCart : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string rawId = Request.QueryString["ProductID"];

			int productId;

			if (!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out productId))
			{
				using (ShoppingCartActions userShoppingCart = new ShoppingCartActions())
				{
					userShoppingCart.AddToCart(Convert.ToInt16(rawId));
				}
			}
			else
			{
				Debug.Fail("Error: We do not get ProductID");
				throw new Exception("Error: It is illegal to load AddToCart.aspx without setting a ProductId.");
			}
			Response.Redirect("ShoppingCart.aspx");
		}
	}
}