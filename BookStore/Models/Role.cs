﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<User> Users { get; set; }
    }
}
