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
				if (meal.Img == null)
					connection.Execute($"insert into Meals (MealName, MealType, Kitchen, Ingredients, Descript) values ('{meal.MealName}', '{meal.MealType}', '{meal.Kitchen}', '{meal.Ingredients}', '{meal.Descript}')");
				else
					connection.Execute($"insert into Meals (MealName, MealType, Kitchen, Ingredients, Descript, Img) values ('{meal.MealName}', '{meal.MealType}', '{meal.Kitchen}', '{meal.Ingredients}', '{meal.Descript}', '{meal.Img}')");
			}
		}
		static public void deleteMeal(Meal meal) {
			using (var connection = new SqlConnection(Buffer.connectionString)) {
				connection.Open();
				connection.Execute($"delete from Meals where MealName = '{meal.MealName}'");
			}
		}
		static public void updateMeal(Meal oldMeal, Meal newMeal) {
			using (var connection = new SqlConnection(Buffer.connectionString)) {
				connection.Open();
				connection.Execute($"update Meals set MealName = '{newMeal.MealName}', MealType = '{newMeal.MealType}', Kitchen = '{newMeal.Kitchen}', Ingredients = '{newMeal.Ingredients}', Descript = '{newMeal.Descript}', Img = '{newMeal.Img}' where MealName = '{oldMeal.MealName}'");
			}
		}
	}
}
