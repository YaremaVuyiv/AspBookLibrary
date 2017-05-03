using Microsoft.VisualStudio.TestTools.UnitTesting;
using AspBookLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AspBookLibrary.Models;
using System.IO.Abstractions.TestingHelpers;
using Moq;

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
            // Arrange
            BooksController controller = new BooksController();

            // Act
            ViewResult result = controller.AddBook() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void DeleteBookTestNull()
        {
            //Arrange
            BooksController target = new BooksController();

            //Act
            var result = target.Delete(null) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Id is null", result.RouteValues["Value"]);
        }

        [TestMethod()]
        public void DeleteBookTestNotFound()
        {
            BookModel book = new BookModel();

            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(x => x.Delete(book));

            //Arrange
            BooksController target = new BooksController(mock.Object);

            //Act
            var result = target.Delete(1) as RedirectToRouteResult;
            //Assert
            Assert.AreEqual("Book not found", result.RouteValues["Value"]);
        }

        [TestMethod()]
        public void DeleteBookTest()
        {
            BookModel book = new BookModel
            {
                BookId = 1,
                Title = "Lord of the rings",
                Author = "Tolkien",
                Description = "Book about rings",
                BookFileUrl = @"C:\Users\Yarema\Downloads\Ioisn_2015_11_35.pdf",
                PictureFileUrl = @"C:\Users\Yarema\Desktop\FjfZ5S00C_Q.jpg",
                Rating = 7
            };

            BookAddViewModel addBook = new BookAddViewModel
            {
                Title = "Lord of the rings",
                Author = "Tolkien",
                Description = "Book about rings",
                BookFile=null,
                PictureFile=null
            };

            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(x => x.Delete(new BookModel
            {
                BookId = 1,
                Title = "Lord of the rings",
                Author = "Tolkien",
                Description = "Book about rings",
                BookFileUrl = @"C:\Users\Yarema\Downloads\Ioisn_2015_11_35.pdf",
                PictureFileUrl = @"C:\Users\Yarema\Desktop\FjfZ5S00C_Q.jpg",
                Rating = 7
            })).Verifiable();

            //Arrange
            BooksController target = new BooksController(mock.Object);

            target.AddBook(addBook);
            //Act
            var result = target.Delete(1) as RedirectToRouteResult;
            //Assert
            //Assert.AreEqual("Succeed", result.RouteValues["Value"]);
            mock.Verify(x => x.Delete(book));
        }

        [TestMethod()]
        public void InsertBookTest()
        {
            BookAddViewModel book = new BookAddViewModel
            {

                Title = "Lord of the rings",
                Author = "Tolkien",
                Description = "Book about rings",
                BookFile = null,
                PictureFile = null,
            };

            BookModel book1 = new BookModel
            {

                Title = "Lord of the rings",
                Author = "Tolkien",
                Description = "Book about rings",
                BookFileUrl = null,
                PictureFileUrl = null,
            };

            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(x => x.Insert(new BookModel
            {
                Title = "Lord of the rings",
                Author = "Tolkien",
                Description = "Book about rings"
            })).Verifiable();

            //Arrange
            BooksController target = new BooksController(mock.Object);

            //Act
            var result = target.AddBook(book) as RedirectToRouteResult;
            //Assert
            //Assert.AreEqual("Book inserted", result.RouteValues["Value"]);

            mock.Verify(x=>x.Insert(book1), Times.Never);
        }

        /*[TestMethod()]
        public void EditBookTestIdNull()
        {
            //Arrange
            BooksController target = new BooksController();

            //Act
            var result = target.Edit() as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Id is null", result.RouteValues["Value"]);
        }*/

        [TestMethod()]
        public void EditBookTest()
        {
            BookModel book = new BookModel
            {
                BookId = 0,
                Title = "Lord of the rings",
                Author = "Tolkien",
                Description = "Book about rings",
                BookFileUrl = null,
                PictureFileUrl = null,
                Rating = 7
            };

            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(x => x.Update(new BookModel
            {
                BookId = 0,
                Title = "Lord of the rings",
                Author = "Tolkien",
                Description = "Book about rings",
                BookFileUrl = null,
                PictureFileUrl = null,
                Rating = 7
            }));

            //Arrange
            BooksController target = new BooksController(mock.Object);

            //Act
            var result = target.Edit(book) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Id is null", result.RouteValues["Value"]);
        }
    }
}