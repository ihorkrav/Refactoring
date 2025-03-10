using System;
using System.Windows.Forms;
using LiveCharts;
using System.Collections.Generic;
using System.Linq;
using Classes;
using DatabaseFunctions;
using LiveCharts.Wpf;


namespace FromFunctions
{
    public  class FormFunctions
    {
       
        public  void DrawHistogram(IDatabase database, CartesianChart chart, string employeeDataPath, string databasePath)
        {
            var fullTimeEmployees = database.ReadExcelData(employeeDataPath);
            var partTimeEmployees = GenerateSampleEmployees<PartTimeEmployee>("parttime", 10);
            var hiredEmployees = GenerateSampleEmployees<HiredEmployee>("hired", 10);

            var partTimeMean = CalculateMean(partTimeEmployees.Select(e => e.MonthlySalary).ToList());
            var hiredMean = CalculateMean(hiredEmployees.Select(e => e.MonthlySalary).ToList());
            var fullTimeMean = CalculateMean(fullTimeEmployees.Select(e => e.MonthlySalary).ToList());

            UpdateChart(chart, partTimeMean, hiredMean, fullTimeMean);

            SaveData(database, databasePath, fullTimeEmployees, partTimeEmployees, hiredEmployees);
        }

        public  List<T> GenerateSampleEmployees<T>(string employeeType, int count) where T : Employee
        {
            var employees = new List<T>();
            for (int i = 1; i <= count; i++)
            {
                employees.Add((T)EmployeeFactory.CreateEmployee(employeeType, i, $"John{i}", $"Doe{i}", "Software Development", 1000 + (i * 200), 160 + i));
            }
            return employees;
        }

        public  void UpdateChart(CartesianChart chart, double partTimeMean, double hiredMean, double fullTimeMean)
        {
            chart.Series = new SeriesCollection
            {
                new ColumnSeries { Title = "PartTime", Values = new ChartValues<double> { partTimeMean } },
                new ColumnSeries { Title = "Hired", Values = new ChartValues<double> { hiredMean } },
                new ColumnSeries { Title = "FullTime", Values = new ChartValues<double> { fullTimeMean } }
            };

            chart.AxisX.Add(new Axis { Title = "Employee Types", Labels = new[] { "PartTime", "Hired", "FullTime" } });
            chart.AxisY.Add(new Axis { Title = "Mean Monthly Salary", LabelFormatter = value => value.ToString("C") });
        }

        public  void SaveData(IDatabase database, string databasePath, List<FullTimeEmployee> fullTimeEmployees, List<PartTimeEmployee> partTimeEmployees, List<HiredEmployee> hiredEmployees)
        {
            database.SaveDatabase(databasePath, fullTimeEmployees);
            database.SaveHiredDatabase(databasePath, hiredEmployees);
            database.SavePartTimeDatabase(databasePath, partTimeEmployees);
        }

        public  double CalculateMean(List<int> salaries)
        {
            return salaries.Count == 0 ? 0 : salaries.Average();
        }
    }
}