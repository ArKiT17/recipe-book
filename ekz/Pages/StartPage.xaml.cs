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

		private void Page_Loaded(object sender, RoutedEventArgs e) {
			//public UserBasicWindow(Users users) {
			//	InitializeComponent();

			//	byte[] image = users.UsersImage;

			//	MemoryStream memorystream = new MemoryStream();
			//	memorystream.Write(image, 0, (int)image.Length);

			//	BitmapImage imgsource = new BitmapImage();
			//	imgsource.BeginInit();
			//	imgsource.StreamSource = memorystream;
			//	imgsource.EndInit();
			//	ImageView.Source = imgsource;






			//	private string ByteImage(Image image) {
			//		image = ImageView.Source;
			//		System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
			//		image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
			//		byte[] b = memoryStream.ToArray();
			//	}
			//}
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
