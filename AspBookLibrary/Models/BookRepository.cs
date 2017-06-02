using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspBookLibrary.App_Data;
using System.Data.Entity;

namespace AspBookLibrary.Models
{
    public class BookRepository: IBookRepository
    {
        private BookContext context;

        public BookRepository(BookContext context)
        {
            this.context = context;
        }

        public void Delete(BookModel book)
        {
            if(context.Books.Find(book) !=null)
            {
                context.Books.Remove(book);
            }
        }

        public void Update(BookModel book)
        {
            context.Entry(book).State = EntityState.Modified;
        }

        public int Save()
        {
            return context.SaveChanges();
        }
        
        public List<BookModel> GetAll()
        {
            return context.Books.ToList();
        }

        public BookModel GetById(long id)
        {
            return context.Books.Find(id);
        }

        public void Insert(BookModel book)
        {
            context.Books.Add(book);
        }
    }
}