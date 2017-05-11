using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AspBookLibrary.Extensions;

namespace AspBookLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository _repository;

        public HomeController()
        {
            _repository = new BookRepository(new BookContext());
        }

        public ActionResult Index ()
        {
            ViewBag.Books = _repository.GetBooks();

            return View();
        }

        public ActionResult Genres(string name)
        {
            var books = _repository.GetBooks();
            var numberOfBooksForGenre =
                Enum.GetNames(typeof(GenreTypes))
                    .Select(
                        genre =>
                            new KeyValuePair<string, int>(genre, books.Where(b => b.Genre == genre).ToArray().Length))
                    .ToList();

            ViewBag.NumberOfBooksForGenre = numberOfBooksForGenre;

            if (name != null)
                ViewBag.Books = books.Where(book => book.Genre == name);

            return View();
        }

        public ActionResult Search()
        {
            if (!string.IsNullOrEmpty(Request["s"]))
            {
                ViewBag.SearchKeyword = Request["s"];
                return View();
            }
            return View();
        }
    }
}