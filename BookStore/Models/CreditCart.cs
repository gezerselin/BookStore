using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class CreditCart
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CartName { get; set; }
        public string CartNo { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int CVV { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
