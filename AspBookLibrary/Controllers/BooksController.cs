using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using AspBookLibrary.App_Data;
using AspBookLibrary.Models;
using Microsoft.Ajax.Utilities;

namespace AspBookLibrary.Controllers
{
    public class BooksController : Controller
    {
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Manage");
            }

            BookContext context = new BookContext();
            var book = context.Books.Find(id);
            

            if (book != null)
            {
                context.Books.Remove(book);
                context.SaveChanges();

                return RedirectToAction("Index", "Manage");
            }
            else
            {
                return RedirectToAction("Index", "Manage");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Manage");
            }

            BookContext context = new BookContext();
            var book = context.Books.Find(id);

            if (book != null)
            {
                return View(book);
            }
            else
            {
                return RedirectToAction("Index", "Manage");
            }
        }

        [HttpPost]
        public ActionResult Edit(BookModels model)
        {
            BookContext db = new BookContext();
            var bookToUpdate = db.Books.Find(model.BookId);
            bookToUpdate.Title = model.Title;
            bookToUpdate.Author = model.Author;
            bookToUpdate.Description = model.Description;
            db.SaveChanges();

            ViewBag.StatusMessage = "Book information for " + model.Title + " was updated successfuly.";

            return RedirectToAction("Index", "Manage", ViewBag.StatusMessage);
        }

        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(BookModels newBook)
        {
            if (ModelState.IsValid)
            {
                using (var bookDb = new BookContext())
                {
                    newBook.Rating = 0;
                    
                    bookDb.Books.Add(newBook);
                    int result = bookDb.SaveChanges();
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(newBook);
            }
        }
    }
}