using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Services
{
    public class FakeBookService : IBookService
    {
        public List<Book> GetBookByGenreId(int genreId)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetBooks()
        {
            return new List<Book>
            {
                new Book{Id=1,Name="Kitap1", ImageUrl="https://cdn.kidega.com/product-images-opt/publication/97/86/25/9786257737999.png?v=2021-01-04",Price=30, Discount=0.33,Info="Çok iyi" },
                new Book{Id=2,Name="Kitap2", ImageUrl="https://cdn.kidega.com/product-images-opt/publication/97/86/25/9786257737999.png?v=2021-01-04",Price=40, Discount=0.33,Info="Çok iyi" },
                new Book{Id=3,Name="Kitap3", ImageUrl="https://cdn.kidega.com/product-images-opt/publication/97/86/25/9786257737999.png?v=2021-01-04",Price=60, Discount=0.33,Info="Çok iyi" },
                new Book{Id=4,Name="Kitap4", ImageUrl="https://cdn.kidega.com/product-images-opt/publication/97/86/25/9786257737999.png?v=2021-01-04",Price=35, Discount=0.33,Info="Çok iyi" },
                new Book{Id=5,Name="Kitap5", ImageUrl="https://cdn.kidega.com/product-images-opt/publication/97/86/25/9786257737999.png?v=2021-01-04",Price=45, Discount=0.33,Info="Çok iyi" },
                new Book{Id=6,Name="Kitap6", ImageUrl="https://cdn.kidega.com/product-images-opt/publication/97/86/25/9786257737999.png?v=2021-01-04",Price=27, Discount=0.33,Info="Çok iyi" },
                new Book{Id=7,Name="Kitap7", ImageUrl="https://cdn.kidega.com/product-images-opt/publication/97/86/25/9786257737999.png?v=2021-01-04",Price=33, Discount=0.33,Info="Çok iyi" },
                new Book{Id=8,Name="Kitap8", ImageUrl="https://cdn.kidega.com/product-images-opt/publication/97/86/25/9786257737999.png?v=2021-01-04",Price=27, Discount=0.33,Info="Çok iyi" },
                new Book{Id=9,Name="Kitap9", ImageUrl="https://cdn.kidega.com/product-images-opt/publication/97/86/25/9786257737999.png?v=2021-01-04",Price=33, Discount=0.33,Info="Çok iyi" },
                new Book{Id=10,Name="Kitap10", ImageUrl="https://cdn.kidega.com/product-images-opt/publication/97/86/25/9786257737999.png?v=2021-01-04",Price=33, Discount=0.33,Info="Çok iyi" },
                new Book{Id=11,Name="Kitap11", ImageUrl="https://cdn.kidega.com/product-images-opt/publication/97/86/25/9786257737999.png?v=2021-01-04",Price=27, Discount=0.33,Info="Çok iyi" },
                new Book{Id=12,Name="Kitap12", ImageUrl="https://cdn.kidega.com/product-images-opt/publication/97/86/25/9786257737999.png?v=2021-01-04",Price=33, Discount=0.33,Info="Çok iyi" },
                new Book{Id=13,Name="Kitap13", ImageUrl="https://cdn.kidega.com/product-images-opt/publication/97/86/25/9786257737999.png?v=2021-01-04",Price=30, Discount=0.33,Info="Çok iyi" }
            };
        }
    }
}
