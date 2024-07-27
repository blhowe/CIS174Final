using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CIS174Final.Models;

namespace CIS174Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BookController : Controller
    {
        private BookContext _context { get; set; }

        public BookController(BookContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Book());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var book = _context.Books.Find(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                if (book.BookId == 0)
                {
                    _context.Books.Add(book);
                }
                else
                {
                    _context.Books.Update(book);
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Action = (book.BookId == 0) ? "Add" : "Edit";
                return View(book);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
