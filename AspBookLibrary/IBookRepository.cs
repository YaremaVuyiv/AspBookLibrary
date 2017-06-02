using System;
using System.Collections.Generic;
using AspBookLibrary.Models;

namespace AspBookLibrary
{
    public interface IBookRepository : IDisposable
    {
        IEnumerable<BookModel> GetBooks();

        BookModel GetBookById(int bookId);

        void InsertBook(BookModel book);

        void DeleteBook(int bookId);

        void UpdateBook(BookModel book);

        void Save();
    }
}