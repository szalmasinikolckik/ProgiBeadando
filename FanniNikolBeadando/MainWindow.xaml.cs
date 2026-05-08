using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Reflection.Metadata.BlobBuilder;

namespace FanniNikolBeadando
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<Book> Books { get; set; }
        

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Books = new ObservableCollection<Book>();
            booksListBox.ItemsSource = Books;
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Szöveges fájlok|*.txt|Minden fájl|*.*";
            Books.Clear();
            if (dialog.ShowDialog() != true) return;
            string path = dialog.FileName;

            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string[] sor = reader.ReadLine().Split(';');
                    string title = sor[0];
                    string author = sor[1];
                    int pages = int.Parse(sor[2]);
                    bool favourite = bool.Parse(sor[3]);

                    Book book = new Book(title, author, pages, favourite);

                    Books.Add(book);
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string title = titleTextBox.Text.Trim();
            string author = authorTextBox.Text.Trim();
            int pages = (int)pagesUpDown.Value;
            bool favourite = favoriteCheckBox.IsChecked == true;


            Book uj = new Book(title, author, pages, favourite);

            Books.Add(uj);

            titleTextBox.Clear();
            authorTextBox.Clear();
            pagesUpDown.Value = 0;
            favoriteCheckBox.IsChecked = false;

            if (string.IsNullOrWhiteSpace(title) ||
               string.IsNullOrWhiteSpace(author) ||
               pagesUpDown == null)
            {
                MessageBox.Show(
                    "Adj meg minden adattagot!",
                    "Hiányzó adatok",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }
        }

        private void booksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (booksListBox.SelectedItem is Book selectedBook)
            {
                titleTextBox.Text = selectedBook.Title;
                authorTextBox.Text = selectedBook.Author;
                pagesUpDown.Value = selectedBook.Pages;
                favoriteCheckBox.IsChecked = selectedBook.Favorite;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (booksListBox.SelectedItem is Book selectedBook)
            {
                Books.Remove(selectedBook);

                titleTextBox.Clear();
                authorTextBox.Clear();
                pagesUpDown.Value = 0;
                favoriteCheckBox.IsChecked = false;

            }
        }
    }
}
