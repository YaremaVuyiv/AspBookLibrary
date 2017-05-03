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

        public ActionResult About ()
        {
            ViewBag.Message = "Your application description page.";

            return View ();
        }

        public ActionResult Contact ()
        {
            ViewBag.Message = "Your contact page.";

            return View ();
        }
    }
}