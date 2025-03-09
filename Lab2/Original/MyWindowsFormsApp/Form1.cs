namespace MyWindowsFormsApp;
using LiveCharts;
using LiveCharts.WinForms;
using Classes;
using DatabaseFunctions;
using System.Windows.Forms.Integration; // Import this for ElementHost
using System.ComponentModel.DataAnnotations.Schema;
using LiveCharts.Wpf;

public partial class Form1 : Form
{
    private LiveCharts.WinForms.CartesianChart chart;
    private Button btnDrawHistogram;
    
    public Form1()
    {
        InitializeComponent();
        InitializeUI();
    }

    private void InitializeUI()
    {
        chart = new LiveCharts.WinForms.CartesianChart
        {
            Dock = DockStyle.Fill,
            Visible = false 
        };
        this.Controls.Add(chart);

        btnDrawHistogram = new Button
        {
            Text = "Draw Histogram",
            Dock = DockStyle.Top
        };
        btnDrawHistogram.Click += BtnDrawHistogram_Click;
        this.Controls.Add(btnDrawHistogram);
    }

    private void BtnDrawHistogram_Click(object sender, EventArgs e)
    {
        DrawHistogram();
        chart.Visible = true;
    }

    private void DrawHistogram()
    {
        // Read data from the database
        List<FullTimeEmployee> fullTimeEmployees = Database.ReadExcelData("D:\\Studying\\Univercity\\Architecture\\Lab1\\MyWindowsFormsApp\\Employees_Cleaned.xlsx");
        var partTimeEmployees = new List<PartTimeEmployee>();
        var hiredEmployees = new List<HiredEmployee>();

        // Generate sample employees using Factory Pattern
        for (int i = 1; i <= 10; i++)
        {
            partTimeEmployees.Add((PartTimeEmployee)EmployeeFactory.CreateEmployee("parttime", i, $"John{i}", $"Doe{i}", "Software Development", 1000 + (i * 200), 160 + i));
            hiredEmployees.Add((HiredEmployee)EmployeeFactory.CreateEmployee("hired", i, $"John{i}", $"Doe{i}", "Software Development", 2000 + (i * 200), i));
        }

        // Calculate mean salaries
        var partTimeMean = CalculateMean(partTimeEmployees.Select(e => e.MonthlySalary).ToList());
        var hiredMean = CalculateMean(hiredEmployees.Select(e => e.MonthlySalary).ToList());
        var fullTimeMean = CalculateMean(fullTimeEmployees.Select(e => e.MonthlySalary).ToList());

        // Create column series for histogram
        chart.Series = new SeriesCollection
        {
            new ColumnSeries { Title = "PartTime", Values = new ChartValues<double> { partTimeMean } },
            new ColumnSeries { Title = "Hired", Values = new ChartValues<double> { hiredMean } },
            new ColumnSeries { Title = "FullTime", Values = new ChartValues<double> { fullTimeMean } }
        };

        // Set axis labels
        chart.AxisX.Add(new Axis { Title = "Employee Types", Labels = new[] { "PartTime", "Hired", "FullTime" } });
        chart.AxisY.Add(new Axis { Title = "Mean Monthly Salary", LabelFormatter = value => value.ToString("C") });

        // Save data to the database
        Database.SaveDatabase("D:\\Studying\\Univercity\\Architecture\\Lab1\\MyWindowsFormsApp", fullTimeEmployees);
        Database.SaveHiredDatabase("D:\\Studying\\Univercity\\Architecture\\Lab1\\MyWindowsFormsApp", hiredEmployees);
        Database.SavePartTimeDatabase("D:\\Studying\\Univercity\\Architecture\\Lab1\\MyWindowsFormsApp", partTimeEmployees);
    }

    private double CalculateMean(List<int> salaries)
    {
        return salaries.Count == 0 ? 0 : salaries.Average();
    }
}
