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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ekz.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditMealPage.xaml
    /// </summary>
    public partial class EditMealPage : Page
    {
		private Meal newEditMeal;
        public EditMealPage()
        {
            InitializeComponent();
			newEditMeal = (Meal)Buffer.Meal.Clone();
			DataContext = newEditMeal;
        }

		private void Page_Loaded(object sender, RoutedEventArgs e) {
			if (Buffer.Meal.Img != null && Buffer.Meal.Img != "") {
				BitmapImage bi = new BitmapImage();
				bi.BeginInit();
				bi.StreamSource = new MemoryStream(Convert.FromBase64String(Buffer.Meal.Img));
				bi.EndInit();
				photo.Source = bi;
			}
			else {
				photoButton.Content = "Додати фото";
			}
		}

		private void Add_Photo(object sender, RoutedEventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
			if (openFileDialog.ShowDialog() == true) {
				newEditMeal.Img = Convert.ToBase64String(File.ReadAllBytes(openFileDialog.FileName));
				BitmapImage bi = new BitmapImage();
				bi.BeginInit();
				bi.StreamSource = new MemoryStream(Convert.FromBase64String(newEditMeal.Img));
				bi.EndInit();
				photo.Source = bi;
				photoButton.Content = "Змінити фото";
			}
		}

		private void Save_button(object sender, RoutedEventArgs e) {
			DataBase.updateMeal(Buffer.Meal, newEditMeal);
			Buffer.Meal = newEditMeal;
			NavigationService.GoBack();
		}
	}
}
