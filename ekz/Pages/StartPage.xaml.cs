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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ekz.Pages {
	/// <summary>
	/// Логика взаимодействия для StartPage.xaml
	/// </summary>
	public partial class StartPage : Page {
		public StartPage() {
			InitializeComponent();
			firstButton.DataContext = null;
			secondButton.DataContext = null;
			thirdButton.DataContext = null;
		}

		private void firstButton_Click(object sender, RoutedEventArgs e) {
			Buffer.MealType = "Гарячі страви";
			NavigationService.Navigate(new ListPage());
		}

		private void secondButton_Click(object sender, RoutedEventArgs e) {
			Buffer.MealType = "Гарніри";
			NavigationService.Navigate(new ListPage());
		}

		private void thirdButton_Click(object sender, RoutedEventArgs e) {
			Buffer.MealType = "Закуски";
			NavigationService.Navigate(new ListPage());
		}

		private void Add_Click(object sender, RoutedEventArgs e) {
			Buffer.AddRecipeWindow.Show();
			Buffer.AddRecipeWindow.Focus();
		}
	}
}
