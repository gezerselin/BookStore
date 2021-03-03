using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen adres başlığı girin")]
        [Display(Name = "Adres Başlığı")]
        public string AddressTitle { get; set; }

        [Required(ErrorMessage = "Lütfen adres detaylarını girin")]
        [Display(Name = "Adres detayları")]
        public string AddressDetails { get; set; }

        [Display(Name = "Kullanıcı")]
        public int UserId  { get; set; }
        public User User { get; set; }

    }
}
