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
            ViewBag.Books = _repository.GetBooks();

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