using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMI.Service.Models;
using BMI.Service.Queries;
using BMI.Service.Repository;
using Moq;
using NUnit.Framework;

namespace Bmi.Service.Test.Queries
{
    [TestFixture]
    public class BmiQueriesTest
    {
        private Mock<IBmiRepository> _bmiRepository;
        private IList<BmiModel> _bmiRecords;
        [SetUp]
        public void Init()
        {
            _bmiRepository = new Mock<IBmiRepository>();
            _bmiRecords = new List<BmiModel>
            {
                new BmiModel {Forename = "test1", Height = 1.67, Surname = "test1", Weight = 85},
                new BmiModel {Forename = "test2", Height = 1.88, Surname = "test2", Weight = 90},
                new BmiModel {Forename = "test3", Height = 1.82, Surname = "test3", Weight = 92}
            };
        }

        [Test]
        public async Task GetAggregatedRecords()
        {
            
            _bmiRepository.Setup(x => x.GetRecords()).ReturnsAsync(_bmiRecords);
            var sut = new BmiQueries(_bmiRepository.Object);

            var result = await sut.GetAggregatedRecords();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(BmiCategory.PreObesity, result.ToList()[0].Category);
            Assert.AreEqual(2, result.ToList()[0].Count);
            Assert.AreEqual(BmiCategory.ObesityClassI, result.ToList()[1].Category);
            Assert.AreEqual(1, result.ToList()[1].Count);
            Assert.IsInstanceOf<IEnumerable<AggregatedBmiData>>(result);
            _bmiRepository.Verify(x => x.GetRecords(), Times.Once);
        }

        [Test]
        public async Task GetAggregatedRecordsNoImportedData()
        {
            
            _bmiRepository.Setup(x => x.GetRecords()).ReturnsAsync(It.IsAny<IEnumerable<BmiModel>>());
            var sut = new BmiQueries(_bmiRepository.Object);

            var result = await sut.GetAggregatedRecords();

            Assert.AreEqual(0, result.Count());
            _bmiRepository.Verify(x => x.GetRecords(), Times.Once);
        }

        [Test]
        public void BmiQueriesRepositoryNotInitialized()
        {

            _bmiRepository.Setup(x => x.GetRecords()).ReturnsAsync(It.IsAny<IEnumerable<BmiModel>>());
            Assert.Throws<ArgumentNullException>(() => new BmiQueries(null));

        }

        [Test]
        public async Task GetRecords()
        {
            _bmiRepository.Setup(x => x.GetRecords()).ReturnsAsync(_bmiRecords);
            var sut = new BmiQueries(_bmiRepository.Object);

            var result = await sut.GetRecords();

            Assert.AreEqual(3, result.Count());
            Assert.IsInstanceOf<IEnumerable<BmiModel>>(result);
            _bmiRepository.Verify(x => x.GetRecords(), Times.Once);
        }
    }
}
