﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        [Display(Name = "Yayın Evi")]
        public string Name { get; set; }
        public IList<Book> Books { get; set; }
    }
}
