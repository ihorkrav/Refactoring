using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using DatabaseFunctions;
using Classes;
using MyWindowsFormsApp;
using System.Threading;

namespace TestingProject
{
    [TestClass]
    public class Form1Tests
    {
        private Mock<IDatabase> _mockDatabase;
        private Form1 _form;

        [TestInitialize]
        [STAThread]
        public void Setup()
        {
            _mockDatabase = new Mock<IDatabase>();
            _form = new Form1(_mockDatabase.Object);
        }

        [TestMethod]
        [STAThread]
        public void CalculateMean_ShouldReturnZero_WhenNoSalaries()
        {
            // Arrange
            var salaries = new List<int>();

            // Act
            var result = _form.CalculateMean(salaries);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [STAThread]
        public void CalculateMean_ShouldReturnCorrectMean()
        {
            // Arrange
            var salaries = new List<int> { 1000, 2000, 3000 };

            // Act
            var result = _form.CalculateMean(salaries);

            // Assert
            Assert.AreEqual(2000, result);
        }

        [TestMethod]
        [STAThread]
        public void GenerateSampleEmployees_ShouldCreateCorrectNumberOfEmployees()
        {
            // Act
            var result = _form.GenerateSampleEmployees<PartTimeEmployee>("parttime", 5);

            // Assert
            Assert.AreEqual(5, result.Count);
            Assert.IsInstanceOfType(result.First(), typeof(PartTimeEmployee));
        }

        [TestMethod]
        [STAThread]
        public void DrawHistogram_ShouldCallReadExcelData_AndSaveMethods()
        {
            // Arrange
            _mockDatabase.Setup(db => db.ReadExcelData(It.IsAny<string>())).Returns(new List<FullTimeEmployee>());
            _form = new Form1(_mockDatabase.Object);

            // Act
            _form.DrawHistogram();

            // Assert
            _mockDatabase.Verify(db => db.ReadExcelData(It.IsAny<string>()), Times.Once);
            _mockDatabase.Verify(db => db.SaveDatabase(It.IsAny<string>(), It.IsAny<List<FullTimeEmployee>>()), Times.Once);
            _mockDatabase.Verify(db => db.SaveHiredDatabase(It.IsAny<string>(), It.IsAny<List<HiredEmployee>>()), Times.Once);
            _mockDatabase.Verify(db => db.SavePartTimeDatabase(It.IsAny<string>(), It.IsAny<List<PartTimeEmployee>>()), Times.Once);
        }
    }
}

