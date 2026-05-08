using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanniNikolBeadando
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public bool Favorite { get; set; }

        public override string ToString()
        {
            return $"{Title} - {Author}";
        }
    }
}
