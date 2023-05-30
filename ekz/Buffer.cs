using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ekz.Pages;

namespace ekz {
	internal class Buffer {
		static public string connectionString = @"Data Source = ASUS-TUF\SQLEXPRESS; Initial Catalog = Recipes; Trusted_Connection=True; Encrypt=False";
		static public string MealType { get; set; }
		static private AddRecipeWindow addRecipeWindow = null;
		static public AddRecipeWindow AddRecipeWindow {
			get {
				if (addRecipeWindow != null)
					return addRecipeWindow;
				else {
					addRecipeWindow = new AddRecipeWindow();
					return addRecipeWindow;
				}
			}
			set { addRecipeWindow = value; }
		}
	}
}
