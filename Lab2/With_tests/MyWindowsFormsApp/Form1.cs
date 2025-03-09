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
    private const string EmployeeDataPath = "D:\\Studying\\Univercity\\Architecture\\Lab1\\MyWindowsFormsApp\\Employees_Cleaned.xlsx";
    private const string DatabasePath = "D:\\Studying\\Univercity\\Architecture\\Lab1\\MyWindowsFormsApp";
    
    private LiveCharts.WinForms.CartesianChart chart;
    private Button btnDrawHistogram;
    private readonly IDatabase _database;

    public Form1(IDatabase database)
    {
        _database = database;
        InitializeComponent();
        InitializeUI();
    }

    public void InitializeUI()
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

    public void BtnDrawHistogram_Click(object sender, EventArgs e)
    {
        try
        {
            DrawHistogram();
            chart.Visible = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}");
        }
    }

    public void DrawHistogram()
    {
        var fullTimeEmployees = _database.ReadExcelData(EmployeeDataPath);
        var partTimeEmployees = GenerateSampleEmployees<PartTimeEmployee>("parttime", 10);
        var hiredEmployees = GenerateSampleEmployees<HiredEmployee>("hired", 10);

        var partTimeMean = CalculateMean(partTimeEmployees.Select(e => e.MonthlySalary).ToList());
        var hiredMean = CalculateMean(hiredEmployees.Select(e => e.MonthlySalary).ToList());
        var fullTimeMean = CalculateMean(fullTimeEmployees.Select(e => e.MonthlySalary).ToList());

        UpdateChart(partTimeMean, hiredMean, fullTimeMean);

        SaveData(fullTimeEmployees, partTimeEmployees, hiredEmployees);
    }

    public List<T> GenerateSampleEmployees<T>(string employeeType, int count) where T : Employee
    {
        var employees = new List<T>();
        for (int i = 1; i <= count; i++)
        {
            employees.Add((T)EmployeeFactory.CreateEmployee(employeeType, i, $"John{i}", $"Doe{i}", "Software Development", 1000 + (i * 200), 160 + i));
        }
        return employees;
    }

    public void UpdateChart(double partTimeMean, double hiredMean, double fullTimeMean)
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

    public void SaveData(List<FullTimeEmployee> fullTimeEmployees, List<PartTimeEmployee> partTimeEmployees, List<HiredEmployee> hiredEmployees)
    {
        _database.SaveDatabase(DatabasePath, fullTimeEmployees);
        _database.SaveHiredDatabase(DatabasePath, hiredEmployees);
        _database.SavePartTimeDatabase(DatabasePath, partTimeEmployees);
    }

    public double CalculateMean(List<int> salaries)
    {
        return salaries.Count == 0 ? 0 : salaries.Average();
    }
}
