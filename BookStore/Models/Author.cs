using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Display(Name="Yazar Adı")]
        public string Name { get; set; }
        [Display(Name = "Yazar Soyadı")]
        public string LastName { get; set; }
        [Display(Name = "Yazar Biyografisi")]
        public string Info { get; set; }
        [Display(Name = "Yazar Resim Url")]
        public string ImageUrl { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
