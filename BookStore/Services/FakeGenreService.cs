using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class FakeGenreService : IGenreService
    {
        public IList<Genre> GetGenres()
        {
            return new List<Genre>
            {
                new Genre{Id=1, Name="Roman"},
                new Genre{Id=2, Name="Şiir" },
                new Genre{Id=3, Name="Kişisel Gelişim"}
            };
        }
    }
}
