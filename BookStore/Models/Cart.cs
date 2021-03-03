using BookStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Cart
    {
        private List<BookInCart> books = new List<BookInCart>();
        public IList<Book> DeletedBooks=new List<Book>();
        

        public void AddItem(Book book, int quantity)
        {

            var existingBook = books.FirstOrDefault(x => x.Book.Id == book.Id);
            if (existingBook == null)
            {
                foreach (var item in DeletedBooks)
                {
                 
                    if (book.Id == item.Id)
                    {
                        DeletedBooks.Remove(DeletedBooks.FirstOrDefault(x=>x.Id==item.Id));
                        break;
                    }
                }


                books.Add(new BookInCart { Book = book, Quantity = quantity });
            }
            else
            {
                existingBook.Quantity += quantity;
            }
        }
         public void RemoveDeletedBook(Book book)
         {
             DeletedBooks.Remove(book);
         }
        public void RemoveAllBook(Book book)
        {

             DeletedBooks.Add(book);
            books.RemoveAll(p => p.Book.Id == book.Id);
        }
        public void RemoveOne(Book book)
        {
            var existingBook = books.FirstOrDefault(x => x.Book.Id == book.Id);
            if (existingBook.Quantity > 1)
            {
                existingBook.Quantity -= 1;
            }
            else if (existingBook.Quantity == 1)
            {
                DeletedBooks.Add(book);
                books.RemoveAll(p => p.Book.Id == book.Id);
            }


        }

        public void Clear() => books.Clear();

        public decimal GetTotalValue() => books.Sum(x => x.Book.Price * (decimal)(1 - x.Book.Discount) * x.Quantity);

        public IEnumerable<BookInCart> Books => books;

    }
}
