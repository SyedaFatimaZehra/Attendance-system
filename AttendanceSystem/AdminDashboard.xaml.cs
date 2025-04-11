using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;

namespace AttendanceSystem
{
    public partial class AdminDashboard : Window
    {
        private string connectionString = "Server=DESKTOP-38Q4IVF\\SA;Database=AttendanceDB;Integrated Security=True;TrustServerCertificate=True;";
        public AdminDashboard()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set the license context
        }

        private void btnAdminLogin_Click(object sender, RoutedEventArgs e)
        {
            string adminUsername = txtAdminUsername.Text;
            string adminPassword = txtAdminPassword.Password;

            if (adminUsername == "Fatima" && adminPassword == "admin123")
            {
                MessageBox.Show("Login Successful.");
                // Show the Generate Report button after successful login
                btnGenerateReport.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Invalid Admin credentials.");
            }
        }

        // Function to generate the Excel report
        private void GenerateExcelReport()
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddTicks(-1);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM Attendance WHERE TimeIn >= @startDate AND TimeIn <= @endDate";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                SqlDataReader reader = cmd.ExecuteReader();

                // Get the directory of the project (parent folder of the bin)
                string? baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string? projectDirectory = Directory.GetParent(baseDirectory)?.Parent?.FullName;

                if (projectDirectory == null)
                {
                    MessageBox.Show("Project directory not found.");
                    return;
                }

                // Generate a unique file name
                string fileName = $"AttendanceReport_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                string filePath = System.IO.Path.Combine(projectDirectory, fileName);

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Attendance Report");

                    // Add headers
                    worksheet.Cells[1, 1].Value = "Staff Name";
                    worksheet.Cells[1, 2].Value = "Time In";
                    worksheet.Cells[1, 3].Value = "Time Out";

                    int row = 2; // Start from the second row for data

                    // Read data and write to Excel sheet
                    while (reader.Read())
                    {
                        worksheet.Cells[row, 1].Value = reader["StaffName"]?.ToString() ?? "Unknown";
                        worksheet.Cells[row, 2].Value = reader["TimeIn"] != DBNull.Value ? Convert.ToDateTime(reader["TimeIn"]).ToString("g") : "";
                        worksheet.Cells[row, 3].Value = reader["TimeOut"] != DBNull.Value ? Convert.ToDateTime(reader["TimeOut"]).ToString("g") : "";
                        row++;
                    }

                    // Save the Excel report
                    FileInfo fileInfo = new FileInfo(filePath);
                    package.SaveAs(fileInfo);
                    MessageBox.Show("Attendance Report saved at " + filePath);
                }
            }
        }

        // Function to load attendance records
        private void LoadAttendanceRecords(DateTime? startDate = null, DateTime? endDate = null)
        {
            // Clear existing records (if any)
            // Assuming you have a data structure to hold the records, e.g., a ListView or DataGrid
            // attendanceRecords.Clear();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT StaffName, TimeIn, TimeOut FROM Attendance WHERE 1=1";
                if (startDate.HasValue)
                {
                    query += " AND TimeIn >= @startDate";
                }
                if (endDate.HasValue)
                {
                    query += " AND TimeIn <= @endDate";
                }
                SqlCommand cmd = new SqlCommand(query, con);
                if (startDate.HasValue)
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate.Value);
                }
                if (endDate.HasValue)
                {
                    cmd.Parameters.AddWithValue("@endDate", endDate.Value);
                }
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string staffName = reader["StaffName"]?.ToString() ?? "Unknown";
                    DateTime timeIn = reader["TimeIn"] != DBNull.Value ? Convert.ToDateTime(reader["TimeIn"]) : DateTime.MinValue;
                    DateTime? timeOut = reader["TimeOut"] != DBNull.Value ? Convert.ToDateTime(reader["TimeOut"]) : (DateTime?)null;

                    // Add the record to your data structure
                    // attendanceRecords.Add(new AttendanceRecord(staffName, timeIn, timeOut));
                }
            }
        }

        // Button click event to generate the report
        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            GenerateExcelReport();
        }

        // Example method to load today's attendance records
        private void btnLoadTodayAttendance_Click(object sender, RoutedEventArgs e)
        {
            DateTime today = DateTime.Today;
            LoadAttendanceRecords(today, today.AddDays(1).AddTicks(-1));
        }
    }
}
