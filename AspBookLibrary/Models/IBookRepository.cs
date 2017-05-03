using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspBookLibrary.Models
{
    public interface IBookRepository
    {
        void Update(BookModel book);
        void Delete(BookModel book);
        void Insert(BookModel book);
        int Save();
        List<BookModel> GetAll();
        BookModel GetById(long id);
    }
}
