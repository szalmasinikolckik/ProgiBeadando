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
            try
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
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (pagesUpDown.Value == null)
            {
                return;
            }
            string title = titleTextBox.Text.Trim();
            string author = authorTextBox.Text.Trim();
            int pages = (int)pagesUpDown.Value;
            bool favourite = favoriteCheckBox.IsChecked == true;

            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(author))
            {
                MessageBox.Show(
                    "Adj meg minden adatot!",
                    "Hiányzó adatok",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            Book uj = new Book(title, author, pages, favourite);

            Books.Add(uj);


            titleTextBox.Clear();
            authorTextBox.Clear();
            pagesUpDown.Value = 0;
            favoriteCheckBox.IsChecked = false;
            addButton.Background = Brushes.LightGreen;
            
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
            else
            {
                MessageBox.Show("Nincs kiválasztott könyv!");
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Filter = "Szöveges fájlok|*.txt|Minden fájl|*.*";
            if (savefile.ShowDialog() != true) return;
            string path = savefile.FileName;
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    foreach (Book book in Books)
                    {
                        writer.WriteLine($"{book.Title};{book.Author};{book.Pages};{book.Favorite}");
                    }
                    MessageBox.Show("Sikeres mentés!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (booksListBox.SelectedItem is Book selectedBook)
            {
                selectedBook.Title = titleTextBox.Text;
                selectedBook.Author = authorTextBox.Text;
                selectedBook.Pages = (int)pagesUpDown.Value;
                selectedBook.Favorite = favoriteCheckBox.IsChecked == true;

                booksListBox.Items.Refresh();

            }
            else
            {
                MessageBox.Show("Válassz ki egy könyvet!");
            }
        }
    }
}
