using System;
using System.Collections.Generic;
using System.IO;
using BMI.Service;
using BMI.Service.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace Bmi.Service.Test
{
    [TestFixture]
    public class FileUploadTest
    {
        private Mock<IFormFile> _file;

        [SetUp]
        public void Init()
        {
            _file = new Mock<IFormFile>();
            _file.Setup(x => x.OpenReadStream()).Returns(new MemoryStream());
        }

        [Test]
        public void FileUpload()
        {
            
            var sut = new FileUpload();

            var response = sut.UploadFile(_file.Object);

            Assert.IsInstanceOf<IEnumerable<BmiModel>>(response);
        }

        [Test]
        public void FileUploadNullFile()
        {
            var sut = new FileUpload();

            Assert.Throws<ArgumentNullException>(() => sut.UploadFile(It.IsAny<IFormFile>()));

        }
    }
}
