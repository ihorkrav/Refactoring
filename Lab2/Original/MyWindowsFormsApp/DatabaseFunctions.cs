using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using Classes;

namespace DatabaseFunctions{
 public class Database{

    public static List<FullTimeEmployee> ReadExcelData(string filePath)
    {
         ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var employees = new List<FullTimeEmployee>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Error: File not found!");
            return employees;
        }

        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first sheet
            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++) // Start from row 2 (skip headers)
            {
                int id = int.Parse(worksheet.Cells[row, 1].Text);
                string firstName = worksheet.Cells[row, 2].Text;
                string lastName = worksheet.Cells[row, 3].Text;
                string department = worksheet.Cells[row, 4].Text;
                int monthlySalary, annualSalary;

                if (!int.TryParse(worksheet.Cells[row, 1].Text, out id))
                {
                    Console.WriteLine($"Skipping row {row}: Invalid ID format '{worksheet.Cells[row, 1].Text}'");
                    continue;
                }

                // Check if Monthly Salary is valid
                if (!int.TryParse(worksheet.Cells[row, 5].Text, out monthlySalary))
                {
                    Console.WriteLine($"Skipping row {row}: Invalid Monthly Salary format '{worksheet.Cells[row, 5].Text}'");
                    continue;
                }

                // Check if Annual Salary is valid
                if (!int.TryParse(worksheet.Cells[row, 6].Text, out annualSalary))
                {
                    Console.WriteLine($"Skipping row {row}: Invalid Annual Salary format '{worksheet.Cells[row, 6].Text}'");
                    continue;
                }
                var employee = new FullTimeEmployee(id, firstName, lastName, department, monthlySalary);
                employees.Add(employee);
            }
        }

        return employees;
    }

    public static void AddFulltimeEmployee(List<FullTimeEmployee> people, FullTimeEmployee person){
        people.Add(person);
    }

    public static void AddPartTimeEmployee(List<PartTimeEmployee> people, PartTimeEmployee person){
        people.Add(person);
    }

    public static void AddHiredEmployee(List<HiredEmployee> people, HiredEmployee person){
        people.Add(person);
    }

    public static void SaveDatabase(string folderPath, List<FullTimeEmployee> people)
    {
        // Ensure the directory exists
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Define the file path
        string filePath = Path.Combine(folderPath, "FullTime.xlsx");

        // Set EPPlus License Context
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var package = new ExcelPackage())
        {
            if (File.Exists(filePath))
            {
                // Load existing file if it exists
                package.Load(File.OpenRead(filePath));
            }

            var worksheet = package.Workbook.Worksheets.FirstOrDefault(ws => ws.Name == "FullTime") ??
                            package.Workbook.Worksheets.Add("FullTime");

            // If it's a new file, add headers
            if (worksheet.Dimension == null)
            {
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "First Name";
                worksheet.Cells[1, 3].Value = "Last Name";
                worksheet.Cells[1, 4].Value = "Department";
                worksheet.Cells[1, 5].Value = "Monthly Salary";
                worksheet.Cells[1, 6].Value = "Annual Salary";
            }

            // Add the data
            for (int i = 0; i < people.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = people[i].ID;
                worksheet.Cells[i + 2, 2].Value = people[i].FirstName;
                worksheet.Cells[i + 2, 3].Value = people[i].LastName;
                worksheet.Cells[i + 2, 4].Value = people[i].Department;
                worksheet.Cells[i + 2, 5].Value = people[i].MonthlySalary;
                worksheet.Cells[i + 2, 6].Value = people[i].AnnualSalary;
            }

            // Save the file
            File.WriteAllBytes(filePath, package.GetAsByteArray());
        }
    }

    public static void SaveHiredDatabase(string folderPath, List<HiredEmployee> people)
    {
        // Ensure the directory exists
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Define the file path
        string filePath = Path.Combine(folderPath, "Hired.xlsx");

        // Set EPPlus License Context
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var package = new ExcelPackage())
        {
            if (File.Exists(filePath))
            {
                // Load existing file if it exists
                package.Load(File.OpenRead(filePath));
            }

            var worksheet = package.Workbook.Worksheets.FirstOrDefault(ws => ws.Name == "Hired") ??
                            package.Workbook.Worksheets.Add("Hired");

            // If it's a new sheet, add headers
            if (worksheet.Dimension == null)
            {
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "First Name";
                worksheet.Cells[1, 3].Value = "Last Name";
                worksheet.Cells[1, 4].Value = "Department";
                worksheet.Cells[1, 5].Value = "Monthly Salary";
                worksheet.Cells[1, 6].Value = "Completed Projects";
                worksheet.Cells[1, 7].Value = "Annual Salary";
            }

            // Add the data
            for (int i = 0; i < people.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = people[i].ID;
                worksheet.Cells[i + 2, 2].Value = people[i].FirstName;
                worksheet.Cells[i + 2, 3].Value = people[i].LastName;
                worksheet.Cells[i + 2, 4].Value = people[i].Department;
                worksheet.Cells[i + 2, 5].Value = people[i].MonthlySalary;
                worksheet.Cells[i + 2, 6].Value = people[i].CompletedProjects;
                worksheet.Cells[i + 2, 7].Value = people[i].AnnualSalary;
            }

            // Save the file
            File.WriteAllBytes(filePath, package.GetAsByteArray());
        }
    }

    public static void SavePartTimeDatabase(string folderPath, List<PartTimeEmployee> people)
    {
        // Ensure the directory exists
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Define the file path
        string filePath = Path.Combine(folderPath, "PartTime.xlsx");

        // Set EPPlus License Context
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var package = new ExcelPackage())
        {
            if (File.Exists(filePath))
            {
                // Load existing file if it exists
                package.Load(File.OpenRead(filePath));
            }

            var worksheet = package.Workbook.Worksheets.FirstOrDefault(ws => ws.Name == "PartTime") ??
                            package.Workbook.Worksheets.Add("PartTime");

            // If it's a new sheet, add headers
            if (worksheet.Dimension == null)
            {
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "First Name";
                worksheet.Cells[1, 3].Value = "Last Name";
                worksheet.Cells[1, 4].Value = "Department";
                worksheet.Cells[1, 5].Value = "Monthly Salary";
                worksheet.Cells[1, 6].Value = "Hours Worked";
                worksheet.Cells[1, 7].Value = "Annual Salary";
            }

            // Add the data
            for (int i = 0; i < people.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = people[i].ID;
                worksheet.Cells[i + 2, 2].Value = people[i].FirstName;
                worksheet.Cells[i + 2, 3].Value = people[i].LastName;
                worksheet.Cells[i + 2, 4].Value = people[i].Department;
                worksheet.Cells[i + 2, 5].Value = people[i].MonthlySalary;
                worksheet.Cells[i + 2, 6].Value = people[i].HoursWorked;
                worksheet.Cells[i + 2, 7].Value = people[i].AnnualSalary;
            }

            // Save the file
            File.WriteAllBytes(filePath, package.GetAsByteArray());
        }
    }
}
}