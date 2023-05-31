using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace ekz
{
	class Meal : ICloneable
	{
		public string? MealName { get; set; }
		public string? MealType { get; set; }
		public string? Kitchen { get; set; }
		public string? Ingredients { get; set; }
		public string? Descript { get; set; }
		public string? Img { get; set; }
		public Meal(string mealName = null, string mealType = null, string kitchen = null,
			string ingredients = null, string descript = null, string img = null) { 
			MealName = mealName;
			MealType = mealType;
			Kitchen = kitchen;
			Ingredients = ingredients;
			Descript = descript;
			Img = img;
		}
		public object Clone() {
			return new Meal(MealName, MealType, Kitchen, Ingredients, Descript, Img);
		}
	}
}
