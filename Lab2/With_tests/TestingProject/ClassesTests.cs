using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Classes;

namespace TestingProject
{
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        public void PartTimeEmployee_Should_Initialize_Correctly()
        {
            // Arrange & Act
            var employee = new PartTimeEmployee(1, "John", "Doe", "IT", 3000, 160);

            // Assert
            Assert.AreEqual(1, employee.ID);
            Assert.AreEqual("John", employee.FirstName);
            Assert.AreEqual("Doe", employee.LastName);
            Assert.AreEqual("IT", employee.Department);
            Assert.AreEqual(3000, employee.MonthlySalary);
            Assert.AreEqual(36000, employee.AnnualSalary); // 3000 * 12
            Assert.AreEqual(160, employee.HoursWorked);
        }

        [TestMethod]
        public void HiredEmployee_Should_Initialize_Correctly()
        {
            // Arrange & Act
            var employee = new HiredEmployee(2, "Alice", "Smith", "Marketing", 4000, 5);

            // Assert
            Assert.AreEqual(2, employee.ID);
            Assert.AreEqual("Alice", employee.FirstName);
            Assert.AreEqual("Smith", employee.LastName);
            Assert.AreEqual("Marketing", employee.Department);
            Assert.AreEqual(4000, employee.MonthlySalary);
            Assert.AreEqual(48000, employee.AnnualSalary);
            Assert.AreEqual(5, employee.CompletedProjects);
        }

        [TestMethod]
        public void FullTimeEmployee_Should_Initialize_Correctly()
        {
            // Arrange & Act
            var employee = new FullTimeEmployee(3, "Bob", "Johnson", "HR", 5000);

            // Assert
            Assert.AreEqual(3, employee.ID);
            Assert.AreEqual("Bob", employee.FirstName);
            Assert.AreEqual("Johnson", employee.LastName);
            Assert.AreEqual("HR", employee.Department);
            Assert.AreEqual(5000, employee.MonthlySalary);
            Assert.AreEqual(60000, employee.AnnualSalary);
        }

        [TestMethod]
        public void EmployeeFactory_Should_Create_PartTimeEmployee()
        {
            // Act
            var employee = EmployeeFactory.CreateEmployee("parttime", 4, "Emma", "Brown", "Finance", 3500, 120);

            // Assert
            Assert.IsInstanceOfType(employee, typeof(PartTimeEmployee));
            var partTimeEmployee = employee as PartTimeEmployee;
            Assert.IsNotNull(partTimeEmployee);
            Assert.AreEqual(120, partTimeEmployee.HoursWorked);
        }

        [TestMethod]
        public void EmployeeFactory_Should_Create_HiredEmployee()
        {
            // Act
            var employee = EmployeeFactory.CreateEmployee("hired", 5, "Liam", "Taylor", "Sales", 4500, 8);

            // Assert
            Assert.IsInstanceOfType(employee, typeof(HiredEmployee));
            var hiredEmployee = employee as HiredEmployee;
            Assert.IsNotNull(hiredEmployee);
            Assert.AreEqual(8, hiredEmployee.CompletedProjects);
        }

        [TestMethod]
        public void EmployeeFactory_Should_Create_FullTimeEmployee()
        {
            // Act
            var employee = EmployeeFactory.CreateEmployee("fulltime", 6, "Sophia", "Wilson", "Engineering", 5500);

            // Assert
            Assert.IsInstanceOfType(employee, typeof(FullTimeEmployee));
            var fullTimeEmployee = employee as FullTimeEmployee;
            Assert.IsNotNull(fullTimeEmployee);
            Assert.AreEqual(5500, fullTimeEmployee.MonthlySalary);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeFactory_Should_Throw_Exception_For_Invalid_Type()
        {
            // Act
            EmployeeFactory.CreateEmployee("invalidType", 7, "Mia", "Anderson", "Support", 3200);
        }

        [TestMethod]
        public void Employee_AnnualSalary_Should_Be_Correct()
        {
            // Arrange
            var employee = new FullTimeEmployee(8, "Oliver", "Moore", "Legal", 6000);

            // Assert
            Assert.AreEqual(72000, employee.AnnualSalary);
        }
    }
}
