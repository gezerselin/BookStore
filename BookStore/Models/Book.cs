using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen isim girin")]
        [Display(Name="Kitap Adı")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen ücret girin")]
        [Display(Name="Ücret")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Lütfen indirim oranı girin")]
        [Display(Name="İndirim Oranı")]
        public double Discount { get; set; }

        [Required(ErrorMessage = "Lütfen kitap bilgisi girin")]
        [Display(Name="Kitap Açıklaması")]
        public string Info { get; set; }

        [Display(Name="Kitap Resim Url")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Lütfen yazar seçin")]
        [Display(Name="Yazar Adı")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [Required(ErrorMessage = "Lütfen yayın evi seçin")]
        [Display(Name = "Yayın Evi")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        [Required(ErrorMessage = "Lütfen kitap türü seçin")]
        [Display(Name="Türü")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public IList<Comment> Comments { get; set; }

    }
}
