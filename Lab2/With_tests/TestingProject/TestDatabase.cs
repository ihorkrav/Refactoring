using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using DatabaseFunctions;
using Moq;
using Classes;
namespace TestingProject;

    [TestClass]
    public class DatabaseTests
    {
        private const string TestFilePath = "test.xlsx";
        private const string TestFolderPath = "TestDatabase";

        private readonly IDatabase _database = new Database();

        [TestInitialize]
        public void Setup()
        {
            // Ensure test environment is clean
            if (File.Exists(TestFilePath))
                File.Delete(TestFilePath);

            if (Directory.Exists(TestFolderPath))
                Directory.Delete(TestFolderPath, true);
        }

        [TestMethod]
        public void ReadExcelData_FileNotFound_ReturnsEmptyList()
        {
            var employees = _database.ReadExcelData("non_existent.xlsx");
            Assert.AreEqual(0, employees.Count);
        }

        [TestMethod]
        public void ReadExcelData_ValidData_ReturnsEmployees()
        {
            // Create a test Excel file
            using (var package = new ExcelPackage(new FileInfo(TestFilePath)))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "First Name";
                worksheet.Cells[1, 3].Value = "Last Name";
                worksheet.Cells[1, 4].Value = "Department";
                worksheet.Cells[1, 5].Value = "Monthly Salary";
                worksheet.Cells[1, 6].Value = "Annual Salary";
                
                worksheet.Cells[2, 1].Value = 1;
                worksheet.Cells[2, 2].Value = "John";
                worksheet.Cells[2, 3].Value = "Doe";
                worksheet.Cells[2, 4].Value = "IT";
                worksheet.Cells[2, 5].Value = 5000;
                worksheet.Cells[2, 6].Value = 60000;
                
                package.Save();
            }

            var employees = _database.ReadExcelData(TestFilePath);
            Assert.AreEqual(1, employees.Count);
            Assert.AreEqual("John", employees[0].FirstName);
        }

        [TestMethod]
        public void SaveDatabase_CreatesFileSuccessfully()
        {
            var employees = new List<FullTimeEmployee>
            {
                new FullTimeEmployee(1, "Alice", "Smith", "HR", 4500)
            };

            _database.SaveDatabase(TestFolderPath, employees);
            string filePath = Path.Combine(TestFolderPath, "FullTime.xlsx");
            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod]
        public void SaveHiredDatabase_CreatesFileSuccessfully()
        {
            var hiredEmployees = new List<HiredEmployee>
            {
                new HiredEmployee(2, "Bob", "Brown", "Finance", 4000, 5)
            };

            _database.SaveHiredDatabase(TestFolderPath, hiredEmployees);
            string filePath = Path.Combine(TestFolderPath, "Hired.xlsx");
            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod]
        public void SavePartTimeDatabase_CreatesFileSuccessfully()
        {
            var partTimeEmployees = new List<PartTimeEmployee>
            {
                new PartTimeEmployee(3, "Charlie", "Davis", "Marketing", 3000, 20)
            };

            _database.SavePartTimeDatabase(TestFolderPath, partTimeEmployees);
            string filePath = Path.Combine(TestFolderPath, "PartTime.xlsx");
            Assert.IsTrue(File.Exists(filePath));
        }
    }
