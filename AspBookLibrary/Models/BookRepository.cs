using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspBookLibrary.App_Data;
using System.Data;
using System.Data.Entity;

namespace AspBookLibrary.Models
{
    public class BookRepository: IBookRepository, IDisposable
    {
        private BookContext context;

        public BookRepository(BookContext context)
        {
            this.context = context;
        }

        public IEnumerable<BookModel> GetBooks()
        {
            return context.Books.ToList();
        }

        public BookModel GetBookById(int? BookId)
        {
            return context.Books.Find(BookId);
        }

        public void InsertBook(BookModel book)
        {
            context.Books.Add(book);
        }

        public void DeleteBook(int? BookId)
        {
            BookModel book = context.Books.Find(BookId);
            context.Books.Remove(book);
        }

        public void UpdateBook(BookModel book)
        {
            context.Entry(book).State = EntityState.Modified; throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}