using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using AspBookLibrary.App_Data;
using AspBookLibrary.Models;
using Microsoft.AspNet.Identity;

namespace AspBookLibrary.Controllers
{
    public class BooksController : Controller
    {
        private IBookRepository repository;

        public BooksController()
        {
            repository = new BookRepository(new BookContext());
        }



        public BooksController(IBookRepository rep)
        {
            repository = rep;
        }

        public ActionResult Delete(int? id)
        {
            

            if (id == null)
            {
                return RedirectToAction("Index", "Manage", new { Value = "Id is null" });
            }

            var book = repository.GetById((long)id);


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

                repository.Delete(book);
                repository.Save();

                return RedirectToAction("Index", "Manage", new { Value = "Succeed" });
            }
            else
            {
                return RedirectToAction("Index", "Manage", new { Value = "Book not found" });
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Manage", new { Value = "Id is null" });
            }

            var book = repository.GetById((long)id);


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
            var bookToUpdate = repository.GetById(model.BookId);
            bookToUpdate.Title = model.Title;
            bookToUpdate.Author = model.Author;
            bookToUpdate.Description = model.Description;


            repository.Update(bookToUpdate);
            repository.Save();

            ViewBag.StatusMessage = "Book information for " + model.Title + " was updated successfuly.";

            return RedirectToAction("Index", "Manage", new { Value = "Book information for " + model.Title + " was updated successfuly." });
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
                    UserId = User.Identity.GetUserId()
                };

                string imagePath = UploadFile(model.PictureFile, "images/thumbnails");
                string bookPath = UploadFile(model.BookFile, "books");

                book.PictureFileUrl = imagePath;
                book.BookFileUrl = bookPath;

                repository.Insert(book);
                int result = repository.Save();

                return RedirectToAction("Index", "Home", new { Value = "Book inserted" });
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