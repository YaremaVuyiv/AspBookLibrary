using Microsoft.VisualStudio.TestTools.UnitTesting;
using AspBookLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AspBookLibrary.Models;

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
    }
}