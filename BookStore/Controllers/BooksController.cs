using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Controllers
{
    
    public class BooksController : Controller
    {
        private readonly BookStoreDbContext _context;
        
     

        private IPublisherService publisherService;
        private IAuthorService authorService;
        private IGenreService genreService;
        private IBookService bookService;

        public BooksController(BookStoreDbContext context, IBookService bookService, IGenreService genreService, IPublisherService publisherService, IAuthorService authorService)
        {
            this.publisherService = publisherService;
            this.authorService = authorService;
            this.genreService = genreService;
            this.bookService = bookService;
            _context = context;

        }


        // GET: Books
        [Authorize(Roles = "Admin,Boss")]
        public async Task<IActionResult> Index()
        {
            var bookStoreDbContext = _context.Books.Include(b => b.Author).Include(b => b.Genre).Include(b => b.Publisher);
            return View(await bookStoreDbContext.ToListAsync());
        }

        // GET: Books/Details/5
        [Authorize(Roles = "Admin,Boss")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        
        public IActionResult DetailsOfBook(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.Comments)
                .FirstOrDefault(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);

        }

        [HttpGet]
        public IActionResult AddComment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddComment( string CommentOfBook,string CommentTitleOfBook, int id)
        {
            Comment comment = new Comment();
            comment.CommentOfBook = CommentOfBook;
            comment.CommentTitle = CommentTitleOfBook;
            comment.BookId = id;
            bookService.addComment(comment);
            
            return Redirect("/Books/DetailsOfBook/" + id.ToString());
            
        }
        private List<SelectListItem> GetPublisherForselect()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            var publishers = publisherService.GetPublishers();
            publishers.ToList().ForEach(publisher => selectListItems.Add(new SelectListItem { Text = publisher.Name, Value = publisher.Id.ToString() }));
            return selectListItems;
        }

        private List<SelectListItem> GetAuthorForSelect()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            var authors = authorService.GetAuthors();
            authors.ToList().ForEach(author => selectListItems.Add(new SelectListItem { Text = author.Name + " " + author.LastName, Value = author.Id.ToString() }));
            return selectListItems;
        }

        private List<SelectListItem> getGenreForSelect()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            var genres = genreService.GetGenres();
            genres.ToList().ForEach(genre => selectListItems.Add(new SelectListItem { Text = genre.Name, Value = genre.Id.ToString() }));
            return selectListItems;
        }


        [Authorize(Roles = "Admin,Boss")]
        public IActionResult Create()
        {
            List<SelectListItem> selectListItems = getGenreForSelect();
            ViewBag.Genre = selectListItems;
            selectListItems = GetAuthorForSelect();
            ViewBag.Author = selectListItems;
            selectListItems = GetPublisherForselect();
            ViewBag.Publisher = selectListItems;

            return View();
        }





        [Authorize(Roles = "Admin,Boss")]
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                bookService.AddBook(book);
                return RedirectToAction(nameof(Index));
            }
            List<SelectListItem> selectListItems = getGenreForSelect();
            ViewBag.Genre = selectListItems;
            selectListItems = GetAuthorForSelect();
            ViewBag.Author = selectListItems;
            selectListItems = GetPublisherForselect();
            ViewBag.Publisher = selectListItems;
            return View();
        }




        [Authorize(Roles = "Admin,Boss")]
        // GET: Books/Edit/5
        public IActionResult Edit(int id)
        {


            var existingBook = bookService.GetBookById(id);

            if (existingBook == null)
            {
                return NotFound();
            }
            ViewBag.Genre = getGenreForSelect();
            ViewBag.Author = GetAuthorForSelect();
            ViewBag.Publisher = GetPublisherForselect();
            return View(existingBook);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Boss")]

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                int affectedRowsCount = bookService.EditBook(book);
                string message = affectedRowsCount > 0 ? "Kitap bilgileri güncellendi" : "Bir sorunla karşılaştık :(";
                return Json(message);
                
            }
            ViewBag.Genre = getGenreForSelect();
            ViewBag.Author = GetAuthorForSelect();
            ViewBag.Publisher = GetPublisherForselect();
            return View();
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin,Boss")]
        public IActionResult Delete(int id)
        {
            var deletingBook = bookService.GetBookById(id);
           // return View(deletingBook);
            return Json(deletingBook.Name);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Boss")]
        public IActionResult DeleteConfirmed(int id)
        {
            var deletingBook = bookService.GetBookById(id);
            bookService.DeleteBook(deletingBook);
            // return RedirectToAction(nameof(Index));
            return Json("OK"); 
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
