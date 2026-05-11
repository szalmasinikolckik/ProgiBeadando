using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FanniNikolBeadando
{
    public class Book
    {
        public Book(string title, string author, int pages, bool favorite)
        {
            Title = title;
            Author = author;
            Pages = pages;
            Favorite = favorite;
            
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public bool Favorite { get; set; }

        public string Category
        {
            get
            {
                if (Pages > 300)
                    return "Hosszú könyv";
                else
                    return "Rövid könyv";
            }
        }

        public override string ToString()
        {
            return $"{Title} - {Author}";
        }
    }
}
