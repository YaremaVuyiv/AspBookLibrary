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
        private IBookRepository bookRepository;

        public BooksController()
        {
            this.bookRepository = new BookRepository(new BookContext());
        }

        public BooksController(IBookRepository context)
        {
            bookRepository = context;
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Manage");
            }


            var book = bookRepository.GetBookById(id);


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

                bookRepository.DeleteBook(id);
                bookRepository.Save();

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


            var book = bookRepository.GetBookById(id);

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
            //BookContext db = new BookContext();
            var bookToUpdate = bookRepository.GetBookById(model.BookId);
            bookToUpdate.Title = model.Title;
            bookToUpdate.Author = model.Author;
            bookToUpdate.Description = model.Description;
            bookRepository.Save();

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

                bookRepository.InsertBook(book);
                bookRepository.Save();


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Fuck you";
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