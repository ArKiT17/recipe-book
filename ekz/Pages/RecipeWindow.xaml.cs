using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
//using static System.Net.Mime.MediaTypeNames;

namespace ekz.Pages {
	/// <summary>
	/// Логика взаимодействия для RecipeWindow.xaml
	/// </summary>
	public partial class RecipeWindow : Window {
		public event EventHandler DataUpdated;
		public RecipeWindow() {
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			Pages.RecipePage recipePage = new Pages.RecipePage();
			recipePage.DataUpdated += DataUpdatedHandler;
			frame.Navigate(recipePage);
		}
		private void DataUpdatedHandler(object sender, EventArgs e) {
			DataUpdated?.Invoke(this, EventArgs.Empty);
		}
	}
}
