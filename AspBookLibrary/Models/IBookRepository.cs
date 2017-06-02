using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspBookLibrary.Models
{
    public interface IBookRepository: IDisposable
    {
        IEnumerable<BookModel> GetBooks();
        BookModel GetBookById(int? Id);
        void InsertBook(BookModel book);
        void DeleteBook(int? BookId);
        void UpdateBook(BookModel book);
        void Save();
    }
}
