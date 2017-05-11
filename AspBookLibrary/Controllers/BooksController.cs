using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspBookLibrary.Extensions;
using AspBookLibrary.Models;
using Microsoft.AspNet.Identity;

namespace AspBookLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _repository;
        private readonly ApplicationDbContext _db;


        public BooksController()
        {
            _db = new ApplicationDbContext();
            _repository = new BookRepository(new BookContext());
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Manage");
            }

            var book = _repository.GetBookById(id.Value);
            if (book != null)
            {
                try
                {
                    System.IO.File.Delete("~/Content/books/" + book.BookFileUrl);
                    System.IO.File.Delete("~/Content/images/thumbnails/" + book.PictureFileUrl);
                }
                catch
                {
                    // ignored
                }

                _repository.DeleteBook(id.Value);
                _repository.Save();

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

            var book = _repository.GetBookById(id.Value);
            if (book != null)
            {
                var bookViewModel = new BookEditViewModel
                {
                    Author = book.Author,
                    BookId = book.BookId,
                    Description = book.Description,
                    Genre = (GenreTypes)Enum.Parse(typeof(GenreTypes), book.Genre),
                    Title = book.Title
                };

                return View(bookViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Manage");
            }
        }

        [HttpPost]
        public ActionResult Edit(BookEditViewModel model)
        {
            var book = _repository.GetBookById(model.BookId);

            book.Title = model.Title;
            book.Author = model.Author;
            book.Description = model.Description;
            book.Genre = model.Genre.ToString();

            _repository.UpdateBook(book);
            _repository.Save();

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
                    Title = model.Title,
                    Genre = model.Genre.ToString(),
                    UserId = User.Identity.GetUserId()
                };

                string imagePath = UploadFile(model.PictureFile, "images/thumbnails");
                string bookPath = UploadFile(model.BookFile, "books");

                book.PictureFileUrl = imagePath;
                book.BookFileUrl = bookPath;

                _repository.InsertBook(book);
                _repository.Save();

                return RedirectToAction("Index", "Home");
            }
            return View(model);
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