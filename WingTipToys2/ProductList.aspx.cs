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
	public partial class ProductList : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		public IQueryable<Product> GetProducts([QueryString("categoryID")] int? categoryId)
		{
			var _db = new WingTipToys2.Models.ProductContext();
			IQueryable<Product> result = _db.Products;

			if (categoryId.HasValue && categoryId > 0)
			{
				result = result.Where(res => res.CategoryID == categoryId);
			}

			return result;
		}
	}
}