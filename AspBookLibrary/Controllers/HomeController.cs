using System.Linq;
using System.Web.Mvc;

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