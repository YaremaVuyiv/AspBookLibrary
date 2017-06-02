using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspBookLibrary.Extensions;

namespace AspBookLibrary.Models
{
    [Table("Books")]
    public class BookModel
    {
        [Key]
        public int BookId { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public string Genre { get; set; }

        public string BookFileUrl { get; set; }

        public string PictureFileUrl { get; set; }
    }

    public class BookAddViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public GenreTypes Genre { get; set; }

        [Display(Name = "Image .png")]
        [Required(ErrorMessage = "Image file is required")]
        public HttpPostedFileBase PictureFile { get; set; }

        [Display(Name = "Book .pdf")]
        [Required(ErrorMessage = "Book PDF file is required")]
        public HttpPostedFileBase BookFile { get; set; }
    }

    public class BookEditViewModel
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public GenreTypes Genre { get; set; }
    }
}