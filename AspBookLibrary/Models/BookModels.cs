using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspBookLibrary.Models
{
    [Table("Books")]
    public class BookModels
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
}