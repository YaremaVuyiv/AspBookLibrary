using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AspBookLibrary;
using System.Web;
using System;

namespace AspBookLibrary.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        private readonly IBookRepository _repository;

        public HomeController()
        {
            _repository = new BookRepository(new BookContext());
        }

        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;

            List<string> cultures = new List<string>() { "ru", "en", "de" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }

            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;
            else
            {

                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }
        public ActionResult Index()
        {
            ViewBag.Books = _repository.GetBooks().Take(3);

            return View();
        }

        public ActionResult Books(string title, string genre)
        {
            var books = _repository.GetBooks();

            if (!string.IsNullOrEmpty(genre))
            {
                ViewBag.Books = !string.IsNullOrEmpty(title)
                    ? books.Where(book => book.Genre == genre && book.Title.ToLower().Contains(title.ToLower()))
                    : books.Where(book => book.Genre == genre);
            }
            else
            {
                ViewBag.Books = !string.IsNullOrEmpty(title)
                    ? books.Where(book => book.Title.ToLower().Contains(title.ToLower()))
                    : books;
            }

            return View();
        }
    }
}