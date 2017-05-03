using System.Net.Cache;
using System.Web.Mvc;
using AspBookLibrary.App_Data;
using AspBookLibrary.Migrations;
using Microsoft.Ajax.Utilities;

namespace AspBookLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index ()
        {
            BookContext context = new BookContext();
            ViewBag.Books = context.Books;
            ViewBag.Genres = context.Genres;

            return View();
        }

        public ActionResult Genres()
        {
            string[] pathToImages = new string[12];

            for (int i = 1; i <= 12; i++)
            {
                pathToImages[i-1] = i + ".png";
            }

            ViewBag.Paths = pathToImages;

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