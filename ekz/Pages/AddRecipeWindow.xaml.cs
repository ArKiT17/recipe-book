using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;


namespace ekz.Pages {
	/// <summary>
	/// Логика взаимодействия для AddRecipeWindow.xaml
	/// </summary>
	public partial class AddRecipeWindow : Window {
		private Meal newMeal;
		public AddRecipeWindow() {
			InitializeComponent();
			newMeal = new Meal();
			DataContext = newMeal;
		}

		private void Form_Closed(object sender, EventArgs e) {
			Buffer.AddRecipeWindow = null;
		}

		private void Add_Photo(object sender, RoutedEventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
			if (openFileDialog.ShowDialog() == true) {
				newMeal.Img = Convert.ToBase64String(File.ReadAllBytes(openFileDialog.FileName));
				MessageBox.Show("Фото збережено");
				photo.Content = "Вибрати інше фото";
			}
		}

		private void Save_button(object sender, RoutedEventArgs e) {
			newMeal.MealType = mealType.Text;
			try {
				DataBase.addMeal(newMeal);
				MessageBox.Show("Рецепт додано!");
				this.Close();
			}
			catch (SqlException ex) {
				MessageBox.Show("Database error");
			}
		}
	}
}
