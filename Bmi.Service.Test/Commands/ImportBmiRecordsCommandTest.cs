using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BMI.Service;
using BMI.Service.Commands;
using BMI.Service.Commands.Handlers;
using BMI.Service.Models;
using BMI.Service.Repository;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace Bmi.Service.Test.Commands
{
    [TestFixture]
    public class ImportBmiRecordsCommandTest
    {
        private Mock<IFileUpload> _fileUpload;
        private Mock<IBmiRepository> _bmiRepository;
        private Mock<IFormFile> _file;

        [SetUp]
        public void Init()
        {
            _fileUpload = new Mock<IFileUpload>();
            _bmiRepository = new Mock<IBmiRepository>();
            _file = new Mock<IFormFile>();
        }

        [Test]
        public async Task ImportFile()
        {
            var command = new ImportBmiRecordsCommand(_file.Object);

            _fileUpload.Setup(x => x.UploadFile(It.IsAny<IFormFile>())).Returns(It.IsAny<IEnumerable<BmiModel>>());
            _bmiRepository.Setup(x => x.InsertRecords(It.IsAny<IEnumerable<BmiModel>>())).ReturnsAsync(true);
            var sut = new ImportBmiRecordsCommandHandler(_fileUpload.Object, _bmiRepository.Object);

            var response = await sut.Handle(command, CancellationToken.None);

            Assert.IsTrue(response);
        }

        [Test]
        public void ImportFileNullCommand()
        {
            _fileUpload.Setup(x => x.UploadFile(It.IsAny<IFormFile>())).Returns(It.IsAny<IEnumerable<BmiModel>>());
            _bmiRepository.Setup(x => x.InsertRecords(It.IsAny<IEnumerable<BmiModel>>())).ReturnsAsync(true);
            var sut = new ImportBmiRecordsCommandHandler(_fileUpload.Object, _bmiRepository.Object);

            Assert.ThrowsAsync<ArgumentNullException>(() => sut.Handle(It.IsAny<ImportBmiRecordsCommand>(), CancellationToken.None));

        }

        [Test]
        public void ImportFileNullFile()
        {
            Assert.Throws<ArgumentNullException>(() => new ImportBmiRecordsCommand(It.IsAny<IFormFile>()));
        }
    }
}
