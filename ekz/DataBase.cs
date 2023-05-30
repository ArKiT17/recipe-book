using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ekz {
	internal class DataBase {
		static public List<Meal> getMeals(string mealType) {
			using (var connection = new SqlConnection(Buffer.connectionString)) {
				connection.Open();
				return connection.Query<Meal>($"select * from Meals where MealType = '{mealType}';").ToList();
			}
		}
		static public void addMeal(Meal meal) {
			using (var connection = new SqlConnection(Buffer.connectionString)) {
				connection.Open();
				connection.Execute($"insert into Meals (MealName, MealType, Kitchen, Ingredients, Descript) value ('{meal.MealName}', '{meal.MealType}', '{meal.Kitchen}', '{meal.Ingredients}', '{meal.Descript}')");
			}
		}
	}
}
