using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Comment
    {
        public int Id { get; set; }
        
        public string CommentTitle { get; set; }
        
        public string CommentOfBook { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
