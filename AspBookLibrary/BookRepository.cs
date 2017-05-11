using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AspBookLibrary.Models;

namespace AspBookLibrary
{
    public class BookRepository : IBookRepository, IDisposable
    {
        private BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
            _disposed = false;
        }

        public IEnumerable<BookModel> GetBooks()
        {
            return _context.Books.ToList();
        }

        public BookModel GetBookById(int bookId)
        {
            return _context.Books.Find(bookId);
        }

        public void InsertBook(BookModel book)
        {
            _context.Books.Add(book);
        }

        public void DeleteBook(int bookId)
        {
            BookModel student = _context.Books.Find(bookId);
            _context.Books.Remove(student);
        }

        public void UpdateBook(BookModel book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}