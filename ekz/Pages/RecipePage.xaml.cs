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

namespace ekz.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecipePage.xaml
    /// </summary>
    public partial class RecipePage : Page
    {
		private Meal currentMeal;
		public event EventHandler DataUpdated;
		public RecipePage()
        {
            InitializeComponent();
			currentMeal = (Meal)Buffer.Meal.Clone();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e) {
			DataUpdated?.Invoke(this, EventArgs.Empty);
			DataContext = Buffer.Meal;
			if (Buffer.Meal.Img != null && Buffer.Meal.Img != "") {
				BitmapImage bi = new BitmapImage();
				bi.BeginInit();
				bi.StreamSource = new MemoryStream(Convert.FromBase64String(Buffer.Meal.Img));
				bi.EndInit();
				photo.Source = bi;
			}
		}

		private void Delete_Click(object sender, RoutedEventArgs e) {
			if (currentMeal != null)
				if (MessageBox.Show("Ви точно хочете видалити цей рецепт?", "УВАГА!", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
					DataBase.deleteMeal(currentMeal);
					Buffer.Meal = null;
					Window.GetWindow(this).Close();
					DataUpdated?.Invoke(this, EventArgs.Empty);
				}
		}

		private void Edit_Click(object sender, RoutedEventArgs e) {
			if (currentMeal != null) {
				Buffer.Meal = currentMeal;
				NavigationService.Navigate(new EditMealPage());
			}
		}
	}
}
