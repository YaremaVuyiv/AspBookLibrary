using Microsoft.VisualStudio.TestTools.UnitTesting;
using AspBookLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AspBookLibrary.Models;
using Moq;
using AspBookLibrary.App_Data;



namespace AspBookLibrary.Controllers.Tests
{
    [TestClass()]
    public class BookControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            // Arrange
            BooksController controller = new BooksController();

            // Act
            ViewResult result = controller.AddBook() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void AddBookTest()
        {


            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.InsertBook(new BookModel())).Verifiable();/*new BookModel
            {
              
                    Title = "Hobbit",
                    Author = "Tolkien",
                    Description = "Book about adventures",
                    BookFileUrl=null,
                    PictureFileUrl=null
                
            });*/
            // Arrange
            BooksController controller = new BooksController(mock.Object);


            BookAddViewModel book = new BookAddViewModel
            {
                Title = null, //"Hobbit",
                Author = "Tolkien",
                Description = "Book about adventures",
                BookFile = null,
                PictureFile = null
            };
            // Act
            var result = controller.AddBook(book) as ViewResult;

            // Assert
            //mock.Verify(a => a.Save());
            Assert.AreEqual("Fuck you", result.ViewBag.Error);
        }

        [TestMethod()]
        public void FailAddBookTest()
        {


            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.GetBooks()).Returns(new List<BookModel>
            {
                new BookModel
                {
                    Title = "Hobbit",
                    Author = "Tolkien",
                    Description = "Book about adventures",
                    BookFileUrl=null,
                    PictureFileUrl=null
                }
            });
            // Arrange
            BooksController controller = new BooksController(mock.Object);


            BookAddViewModel book = new BookAddViewModel();
            // Act
            controller.AddBook(null);

            // Assert
            mock.Verify(a => a.Save(), Times.Never);
        }

        [TestMethod()]
        public void EditBookTest()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.GetBookById(2)).Returns(new BookModel
            {
                /*new BookModel
                {
                    BookId=1,
                    Title = "Hobbit",
                    Author = "Tolkien",
                    Description = "Book about adventures",
                    BookFileUrl=@"C:\Users\Yarema\Downloads\Ioisn_2015_11_35.pdf",
                    PictureFileUrl = @"C:\Users\Yarema\Desktop\FjfZ5S00C_Q.jpg",
                    Rating=7
                },*/
               
                    BookId=2,
                    Title = "Lord of the rings",
                    Author = "Tolkien",
                    Description = "Book about rings",
                    BookFileUrl=@"C:\Users\Yarema\Downloads\Ioisn_2015_11_35.pdf",
                    PictureFileUrl=@"C:\Users\Yarema\Desktop\FjfZ5S00C_Q.jpg",
                    Rating=7
                
            });

            // Arrange
            BooksController controller = new BooksController(mock.Object);

            // Act
            BookModel book1 = new BookModel
            {
                BookId = 2,
                Title = "Lord of the rings",
                Author = "Tolkien",
                Description = "Book not about rings",
                BookFileUrl = @"C:\Users\Yarema\Downloads\Ioisn_2015_11_35.pdf",
                PictureFileUrl = @"C:\Users\Yarema\Desktop\FjfZ5S00C_Q.jpg",
                Rating = 7
            };
            BookModel book = new BookModel();

            var result = controller.Edit(book1) as RedirectToRouteResult;
            //book = mock.Object.GetBooks().ToList()[0];
            
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), 63);
        }
    }
}