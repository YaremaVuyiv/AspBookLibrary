using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspBookLibrary.Models
{
    [Table("Books")]
    public class BookModel
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        
        [Range(0, 10, ErrorMessage = "Rating is between 0 and 10")]
        public int Rating { get; set; }

        [Required(ErrorMessage ="File is required")]
        public string BookFileUrl { get; set; }

        [Required(ErrorMessage = "File is required")]
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

        [Display(Name = "Image .png")]
        public HttpPostedFileBase PictureFile { get; set; }

        [Display(Name = "Book .pdf")]
        public HttpPostedFileBase BookFile { get; set; }
    }
}