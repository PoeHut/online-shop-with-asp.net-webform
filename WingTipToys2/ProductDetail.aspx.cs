using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingTipToys2.Models;
using System.Web.ModelBinding;

namespace WingTipToys2
{
	public partial class ProductDetail : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		public IQueryable<Product> GetProducts([QueryString("productID")] int? productId)
		{
			var _db = new WingTipToys2.Models.ProductContext();
			IQueryable<Product> result = _db.Products;

			if (productId.HasValue && productId > 0)
			{
				result = result.Where(res => res.ProductID == productId);
			}
			else
			{
				result = null;
			}

			return result;
		}
	}
}