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

        }
    }
}
