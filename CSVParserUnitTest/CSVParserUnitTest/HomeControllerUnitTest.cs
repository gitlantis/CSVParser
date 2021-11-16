using CSVParser.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.IO;

namespace CSVParserUnitTest
{
    [TestFixture]
    public class HomeControllerUnitTest
    {
        private readonly HomeController _homeController;

        public HomeControllerUnitTest()
        {
            //Arrange
            _homeController = new HomeController(null);

        }

        [Test]
        public void Index_Returns_IActionResult()
        {   
            //Act
            var res = _homeController.Index() as IActionResult;
            
            //Assert
            Assert.AreEqual("ViewResult", res.GetType().Name);            
        }
        
        [Test]
        public void Upload_Returns_IActionResult()
        {

            string content = "";
            var fileName = @"..\..\..\TestFiles\dataset.csv";

            using (var csvFile = new StreamReader(File.OpenRead(fileName)))
            {
                content = csvFile.ReadToEnd();
            }

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile file = new FormFile(stream, 0, stream.Length, "file", fileName);

            //Act
            var result = _homeController.Upload(file);

            //Assert
            Assert.AreEqual("NotFoundObjectResult", result.GetType().Name);
        }

        [Test]
        public void GetEmployees_Returns_IActionResult()
        {
            //Act
            var result = _homeController.GetEmployees() as JsonResult;

            // Assert
            Assert.AreEqual("JsonResult", result.GetType().Name);
        }
    }
}