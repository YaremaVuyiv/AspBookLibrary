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
using System.IO;
using System.Web;
using AspBookLibrary.App_Data;
using AspBookLibrary.Migrations;
using System.Security.Principal;

namespace AspBookLibrary.Tests.Controllers
{
    [TestClass()]
    class ManageControllerTests
    {
        [TestMethod()]
        public void EditAccountTest()
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
