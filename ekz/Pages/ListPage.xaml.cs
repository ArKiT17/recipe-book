using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ekz.Pages {
	/// <summary>
	/// Логика взаимодействия для ListPage.xaml
	/// </summary>
	public partial class ListPage : Page {
		public ListPage() {
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e) {
			title.Text = Buffer.MealType;
			list.ItemsSource = DataBase.getMeals(Buffer.MealType);
		}

		private void Return_Click(object sender, RoutedEventArgs e) {
			NavigationService.GoBack();
        }

		private void Recipe_chosen(object sender, RoutedEventArgs e) {
			if (list.SelectedItem != null) {
				Buffer.Meal = list.SelectedItem as Meal;
				RecipeWindow recipeWindow = new RecipeWindow();
				recipeWindow.DataUpdated += DataUpdatedHandler;
				recipeWindow.Show();
			}
        }
		private void DataUpdatedHandler(object sender, EventArgs e) {
			list.ItemsSource = DataBase.getMeals(Buffer.MealType);
		}

		private void search_textChanged(object sender, TextChangedEventArgs e) {
			if (search.Text == "")
				list.ItemsSource = DataBase.getMeals(Buffer.MealType);
			else {
				List<Meal> tmp = DataBase.getMeals(Buffer.MealType);
				for (int i = 0; i < tmp.Count; i++) {
					if (tmp[i].MealName.ToLower().Contains(search.Text.ToLower()) == false &&
						tmp[i].Kitchen.ToLower().Contains(search.Text.ToLower()) == false &&
						tmp[i].Ingredients.ToLower().Contains(search.Text.ToLower()) == false) {
						tmp.RemoveAt(i);
						i--;
					}
				}
				list.ItemsSource = tmp;
			}
		}
    }
}
