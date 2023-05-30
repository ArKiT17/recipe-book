using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
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
				if (meal.Image == null)
					connection.Execute($"insert into Meals (MealName, MealType, Kitchen, Ingredients, Descript) values ('{meal.MealName}', '{meal.MealType}', '{meal.Kitchen}', '{meal.Ingredients}', '{meal.Descript}')");
				else
					connection.Execute($"insert into Meals (MealName, MealType, Kitchen, Ingredients, Descript, Img) values ('{meal.MealName}', '{meal.MealType}', '{meal.Kitchen}', '{meal.Ingredients}', '{meal.Descript}', '{BitConverter.ToString(meal.Image).Replace("-", "")}')");
			}
		}
	}
}
