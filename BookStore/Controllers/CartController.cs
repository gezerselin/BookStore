using BookStore.Data;
using BookStore.Models;
using BookStore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {

        BookStoreDbContext dbContext = new BookStoreDbContext();
        private IBookService bookService;
        private List<BookInCart> books = new List<BookInCart>();
        public CartController(IBookService bookService,BookStoreDbContext dbContext)
        {
            this.bookService = bookService;
            this.dbContext = dbContext;
        }
        
        public IActionResult Index(string returnUrl)
        {
            var cart = GetCart();
            ViewBag.ReturnUrl = returnUrl;
            return View(cart);
        }
        
        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            
            var selectedBook = bookService.GetBookById(id);
            if (selectedBook == null)
            {
                return NotFound();
            }

   
            Cart cart = GetCart() ?? new Cart();  //boşsa null döndür değilse Cart oluştur
            cart.AddItem(selectedBook, 1);
            SaveCart(cart);

            return RedirectToAction(nameof(Index), nameof(Cart));
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var selectedBook = bookService.GetBookById(id);
            if (selectedBook == null)
            {
                return NotFound();
            }
            Cart cart = GetCart();

            cart.RemoveAllBook(selectedBook);
            SaveCart(cart);
            return RedirectToAction(nameof(Index), nameof(Cart));
        }

        [HttpPost]
        public IActionResult StepRemoveFromCart(int id)
        {
            
            var selectedBook = bookService.GetBookById(id);
            if (selectedBook == null)
            {
                return NotFound();
            }
        
            var existingBook = books.FirstOrDefault(x => x.Book.Id == selectedBook.Id);
            Cart cart = GetCart();

            cart.RemoveOne(selectedBook);
            
            
            SaveCart(cart);
            return RedirectToAction(nameof(Index), nameof(Cart));
        }

        Cart GetCart()
        {
            var data = HttpContext.Session.GetString("Sepetim");
            if (data == null)
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<Cart>(data);
            }
        }
        void SaveCart(Cart cart)
        {

            HttpContext.Session.SetString("Sepetim", JsonConvert.SerializeObject(cart));
        }



        [Authorize]
        public IActionResult PaymentDetail()
        {
            int idOfUser = Convert.ToInt32(User.Identity.Name);

            var user = dbContext.Users.Include(x => x.CreditCarts)
                .Include(x=>x.Addresses)
                .FirstOrDefault(m => m.Id == idOfUser);



            
            Cart cart = GetCart();
            if (cart != null)
            {
                decimal total = cart.GetTotalValue();

                ViewBag.Total = total;

            }

            return View(user);
        }

    }
}
