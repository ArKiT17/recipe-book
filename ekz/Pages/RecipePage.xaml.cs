using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Word = Microsoft.Office.Interop.Word;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace ekz.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecipePage.xaml
    /// </summary>
    public partial class RecipePage : System.Windows.Controls.Page {
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
					System.Windows.Window.GetWindow(this).Close();
					DataUpdated?.Invoke(this, EventArgs.Empty);
				}
		}

		private void Edit_Click(object sender, RoutedEventArgs e) {
			if (currentMeal != null) {
				Buffer.Meal = currentMeal;
				NavigationService.Navigate(new EditMealPage());
			}
		}
		private void Save_docx_Click(object sender, RoutedEventArgs e) {
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Word file (*.docx)|*.docx";
			if (saveFileDialog.ShowDialog() == true) {
				Word.Application app = new Word.Application();
				Word.Document doc = app.Documents.Add(Visible: true);

				Word.Paragraph paragraphName = doc.Content.Paragraphs.Add();
				paragraphName.Range.Font.Size = 16;
				paragraphName.Range.Font.Bold = 1;
				paragraphName.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter; // Выравнивание по центру
				paragraphName.Range.Text = currentMeal.MealName;
				paragraphName.Range.InsertParagraphAfter();

				Word.Paragraph paragraphKitchen = doc.Content.Paragraphs.Add();
				paragraphKitchen.Range.Font.Bold = 1;
				paragraphKitchen.Range.Text = currentMeal.Kitchen;
				paragraphKitchen.Range.InsertParagraphAfter();

				Word.Paragraph paragraphIngredients = doc.Content.Paragraphs.Add();
				paragraphIngredients.Range.Text = $"\nІнгредієнти:\n{currentMeal.Ingredients}";
				paragraphIngredients.Range.InsertParagraphAfter();

				Word.Paragraph paragraphDescription = doc.Content.Paragraphs.Add();
				paragraphDescription.Range.Text = $"\nСпосіб приготування:\n{currentMeal.Descript}";
				paragraphDescription.Range.InsertParagraphAfter();

				doc.SaveAs2(saveFileDialog.FileName);
				doc.Close();
				app.Quit();
				MessageBox.Show("Docx документ створено");
			}
		}

		private void Save_pdf_Click(object sender, RoutedEventArgs e) {
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf";
			if (saveFileDialog.ShowDialog() == true) {
				PdfDocument document = new PdfDocument();

				PdfPage page = document.AddPage();

				XGraphics graphics = XGraphics.FromPdfPage(page);

				XFont fontName = new XFont("Arial", 16, XFontStyle.Bold);
				XFont fontBold = new XFont("Arial", 12, XFontStyle.Bold);
				XFont fontNormal = new XFont("Arial", 12);
				graphics.DrawString(currentMeal.MealName, fontName, XBrushes.Black, new XRect(10, 10, page.Width, page.Height), XStringFormats.TopCenter);
				graphics.DrawString(currentMeal.Kitchen, fontBold, XBrushes.Black, new XRect(10, 30, page.Width, page.Height), XStringFormats.TopCenter);
				graphics.DrawString("Інгредієнти:", fontBold, XBrushes.Black, new XRect(20, 70, page.Width, page.Height), XStringFormats.TopLeft);
				string newIng = currentMeal.Ingredients.Replace('\r', ' ');
				string[] tmp = newIng.Split('\n');
				int coordY = 90;
				for (int i = 0; i < tmp.Count(); i++) {
					graphics.DrawString(tmp[i], fontNormal, XBrushes.Black, new XRect(20, coordY, page.Width, page.Height), XStringFormats.TopLeft);
					coordY += 20;
				}
				coordY += 20;
				graphics.DrawString("Спосіб приготування:", fontBold, XBrushes.Black, new XRect(20, coordY, page.Width, page.Height), XStringFormats.TopLeft);
				
				newIng = currentMeal.Descript.Replace('\r', ' ');
				tmp = newIng.Split('\n');
				coordY += 20;
				for (int i = 0; i < tmp.Count(); i++) {
					graphics.DrawString(tmp[i], fontNormal, XBrushes.Black, new XRect(20, coordY, page.Width, page.Height), XStringFormats.TopLeft);
					coordY += 20;
				}
				coordY += 20;

				if (currentMeal.Img != null && currentMeal.Img != "") {
					BitmapEncoder encoder = new JpegBitmapEncoder();
					encoder.Frames.Add(BitmapFrame.Create((BitmapSource)photo.Source));
					using (FileStream stream = new FileStream("tmp.jpeg", FileMode.Create)) {
						encoder.Save(stream);
					}

					XImage image = XImage.FromFile("tmp.jpeg");
					File.Delete("tmp.jpeg");
					graphics.DrawImage(image, page.Width / 2 + 30, 70, 250, 200);
				}

				document.Save(saveFileDialog.FileName);
				document.Close();
				MessageBox.Show("PDF документ створено");
			}
		}

	}
}
