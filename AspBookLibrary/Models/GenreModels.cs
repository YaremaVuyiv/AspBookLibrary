using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace AspBookLibrary.Models
{
    [Table("Genres")]
    public class GenreModels
    {
        [Key]
        public int GenreId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}