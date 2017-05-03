using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using AspBookLibrary.App_Data;
using AspBookLibrary.Models;

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
                try
                {
                    System.IO.File.Delete("~/Content/books/" + book.BookFileUrl);
                    System.IO.File.Delete("~/Content/images/thumbnails/" + book.PictureFileUrl);
                }
                catch
                {
                }

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
        public ActionResult Edit(BookModel model)
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
        public ActionResult AddBook(BookAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var bookDb = new BookContext())
                {
                    BookModel book = new BookModel
                    {
                        Rating = 0,
                        Author = model.Author,
                        Description = model.Description,
                        Title = model.Title
                    };

                    string imagePath = UploadFile(model.PictureFile, "images/thumbnails");
                    string bookPath = UploadFile(model.BookFile, "books");

                    book.PictureFileUrl = imagePath;
                    book.BookFileUrl = bookPath;

                    bookDb.Books.Add(book);
                    int result = bookDb.SaveChanges();
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }

        public string UploadFile(HttpPostedFileBase file, string pathPart)
        {
            if (file == null) return string.Empty;

            var fileName = Path.GetFileName(file.FileName);
            var random = Guid.NewGuid() + fileName;
            var path = Path.Combine(HttpContext.Server.MapPath("~/Content/" + pathPart), random);
            if (!Directory.Exists(HttpContext.Server.MapPath("~/Content/" + pathPart)))
            {
                Directory.CreateDirectory(HttpContext.Server.MapPath("~/Content/" + pathPart));
            }
            file.SaveAs(path);

            return random;
        }
    }
}