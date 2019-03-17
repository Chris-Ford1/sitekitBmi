using BMI.Service.Models;
using NUnit.Framework;

namespace Bmi.Service.Test.Models
{
    [TestFixture]
    public class BmiModelTest
    {
        [Test]
        public void PopulateModel()
        {
            var sut = new BmiModel
            {
                Forename = "test",
                Height = 1.70,
                Surname = "test",
                Weight = 65
            };

            Assert.AreEqual(22.49, sut.Bmi);
            Assert.AreEqual(BmiCategory.NormalWeight, sut.Category);
        }

        [Test]
        public void PopulateModelPreObese()
        {
            var sut = new BmiModel
            {
                Forename = "test",
                Height = 1.70,
                Surname = "test",
                Weight = 80
            };


            Assert.AreEqual(BmiCategory.PreObesity, sut.Category);
        }

        [Test]
        public void PopulateModelUnderweight()
        {
            var sut = new BmiModel
            {
                Forename = "test",
                Height = 1.70,
                Surname = "test",
                Weight = 50
            };


            Assert.AreEqual(BmiCategory.Underweight, sut.Category);
        }

        [Test]
        public void PopulateModelObseseClassI()
        {
            var sut = new BmiModel
            {
                Forename = "test",
                Height = 1.70,
                Surname = "test",
                Weight = 100
            };


            Assert.AreEqual(BmiCategory.ObesityClassI, sut.Category);
        }

        [Test]
        public void PopulateModelInvalidInput()
        {
            var sut = new BmiModel
            {
                Forename = "test",
                Height = 1.70,
                Surname = "test",
                Weight = 125
            };


            Assert.AreEqual(BmiCategory.Unknown, sut.Category);
        }
    }
}
