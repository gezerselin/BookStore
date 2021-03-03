using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using BookStore.Services;

namespace BookStore.Controllers
{
    
    public class AuthorsController : Controller
    {
        private readonly BookStoreDbContext _context;

        IAuthorService authorService;
        public AuthorsController(BookStoreDbContext context, IAuthorService authorService)
        {
            this.authorService = authorService;
            _context = context;
        }

        // GET: Authors
        [Authorize(Roles = "Admin,Boss")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Authors.ToListAsync());
        }

        // GET: Authors/Details/5
        [Authorize(Roles = "Admin,Boss")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }


        public IActionResult DetailsOfAuthor(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _context.Authors
                .Include(x=>x.Books).ThenInclude(b => b.Genre)
                 .Include(x => x.Books).ThenInclude(b => b.Publisher)
                .FirstOrDefault(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        [Authorize(Roles = "Admin,Boss")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Boss")]
        public async Task<IActionResult> Create([Bind("Id,Name,LastName,Info,ImageUrl")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        [Authorize(Roles = "Admin,Boss")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      
       

        [HttpPost]
        [Authorize(Roles = "Admin,Boss")]
        public IActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                int affectedRowsCount = authorService.EditAuthor(author);
                string message = affectedRowsCount > 0 ? "Kitap bilgileri güncellendi" : "Bir sorunla karşılaştık :(";
                return Json(message);

            }
         
            return View();
        }



        // GET: Authors/Delete/5
        [Authorize(Roles = "Admin,Boss")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return Json(author.Name);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Boss")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return Json("OK");
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
