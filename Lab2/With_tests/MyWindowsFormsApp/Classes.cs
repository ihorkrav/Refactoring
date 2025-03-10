using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Classes{


    public abstract class Employee{
        [Required]
        public int ID { get; set; }
        [Required]

        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Department { get; set; }
        [NotNull]
        public int MonthlySalary { get; set; }
        [NotNull]
        public int AnnualSalary { get; set; }

        protected Employee(int id, string firstName, string lastName, string department, int monthlySalary)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Department = department;
            MonthlySalary = monthlySalary;
            AnnualSalary = monthlySalary * 12;
        }
    }

    public class PartTimeEmployee :Employee
    {
        [Required, NotNull]
         public int HoursWorked { get; set; }

        public PartTimeEmployee(int id, string firstName, string lastName, string department, int monthlySalary, int hoursWorked)
            : base(id, firstName, lastName, department, monthlySalary)
        {
            HoursWorked = hoursWorked;
        }
    }

    public class HiredEmployee: Employee{
        [Required, NotNull]

        public int CompletedProjects { get; set; }

        public HiredEmployee(int id, string firstName, string lastName, string department, int monthlySalary, int completedProjects)
            : base(id, firstName, lastName, department, monthlySalary)
        {
            CompletedProjects = completedProjects;
        }
    }

    public class FullTimeEmployee : Employee
    {
        public FullTimeEmployee(int id, string firstName, string lastName, string department, int monthlySalary)
            : base(id, firstName, lastName, department, monthlySalary)
        {
        }
    }

    public static class EmployeeFactory
    {
        public static Employee CreateEmployee(string employeeType, int id, string firstName, string lastName, string department, int monthlySalary, int additionalValue = 0)
        {
            switch (employeeType.ToLower())
            {
                case "parttime":
                    return new PartTimeEmployee(id, firstName, lastName, department, monthlySalary, additionalValue);

                case "hired":
                    return new HiredEmployee(id, firstName, lastName, department, monthlySalary, additionalValue);

                case "fulltime":
                    return new FullTimeEmployee(id, firstName, lastName, department, monthlySalary);

                default:
                    throw new ArgumentException("Invalid employee type");
            }
        }
    }
}